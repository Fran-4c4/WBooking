<#
.SYNOPSIS
Starts SQL Server in Docker and restores the checked-in sanitized demo database.
.PARAMETER Port
Host TCP port mapped to SQL Server port 1433. The authoritative default is
14333. After the local environment file exists, its value is authoritative and
a conflicting requested port is rejected.
.PARAMETER ResetDatabase
Replaces an existing wbooking_demo database. Without this switch, an existing
database is preserved so local test changes are not silently discarded.
#>
[CmdletBinding()]
param(
    [ValidateRange(1, 65535)]
    [int]$Port = 14333,

    [switch]$ResetDatabase
)

$ErrorActionPreference = 'Stop'
$repoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$composeFile = Join-Path $repoRoot 'docker-compose.sqlserver.yml'
$fixture = Join-Path $repoRoot 'database\wbooking_demo.bak'
$localRoot = Join-Path $repoRoot '.local\sqlserver'
$backupRoot = Join-Path $localRoot 'backups'
$envFile = Join-Path $localRoot '.env'
$localBackup = Join-Path $backupRoot 'wbooking_demo.bak'

if (-not (Test-Path -LiteralPath $fixture -PathType Leaf)) {
    throw "Sanitized fixture not found: $fixture"
}
if (-not (Get-Command docker -ErrorAction SilentlyContinue)) {
    throw 'Docker was not found on PATH.'
}

function New-LocalPassword {
    $bytes = [byte[]]::new(24)
    [Security.Cryptography.RandomNumberGenerator]::Fill($bytes)
    return 'Wb!' + [Convert]::ToBase64String($bytes).Replace('+', 'A').Replace('/', 'B').TrimEnd('=') + '9a'
}

function Read-EnvironmentFile([string]$Path) {
    $values = @{}
    foreach ($line in [IO.File]::ReadAllLines($Path)) {
        if ([string]::IsNullOrWhiteSpace($line) -or $line.TrimStart().StartsWith('#')) { continue }
        $parts = $line.Split('=', 2)
        if ($parts.Count -ne 2 -or [string]::IsNullOrWhiteSpace($parts[0])) {
            throw "Invalid environment entry in $Path."
        }
        $values[$parts[0].Trim()] = $parts[1]
    }
    return $values
}

function Invoke-Sql {
    param(
        [Parameter(Mandatory = $true)][string]$SaPassword,
        [Parameter(Mandatory = $true)][string]$Query,
        [switch]$Raw
    )
    $arguments = @(
        'exec', '-i', '--env', "SQLCMDPASSWORD=$SaPassword", 'wbooking-sql',
        '/opt/mssql-tools18/bin/sqlcmd', '-C', '-S', 'localhost', '-U', 'sa',
        '-b', '-r', '1', '-W'
    )
    if ($Raw) { $arguments += @('-h', '-1', '-s', '|') }
    $arguments += @('-Q', $Query)
    $result = @(& docker @arguments 2>&1)
    if ($LASTEXITCODE -ne 0) {
        $excerpt = ($result | Select-Object -First 12 | ForEach-Object {
            if ($_.ToString().Length -gt 500) { $_.ToString().Substring(0, 500) } else { $_.ToString() }
        }) -join [Environment]::NewLine
        throw "SQL command failed.`n$excerpt"
    }
    return $result
}

New-Item -ItemType Directory -Force -Path $backupRoot | Out-Null
Copy-Item -LiteralPath $fixture -Destination $localBackup -Force

if (-not (Test-Path -LiteralPath $envFile -PathType Leaf)) {
    @(
        "WBOOKING_SQL_PORT=$Port",
        "WBOOKING_SQL_SA_PASSWORD=$(New-LocalPassword)",
        "WBOOKING_SQL_APP_PASSWORD=$(New-LocalPassword)"
    ) | Set-Content -LiteralPath $envFile -Encoding utf8NoBOM
}
$environment = Read-EnvironmentFile $envFile
foreach ($required in @('WBOOKING_SQL_PORT', 'WBOOKING_SQL_SA_PASSWORD', 'WBOOKING_SQL_APP_PASSWORD')) {
    if (-not $environment.ContainsKey($required) -or [string]::IsNullOrWhiteSpace($environment[$required])) {
        throw "$required is missing from $envFile."
    }
}
if ([int]$environment.WBOOKING_SQL_PORT -ne $Port) {
    throw "The requested port $Port conflicts with WBOOKING_SQL_PORT in $envFile. Remove the local env file to regenerate it."
}

& docker compose --env-file $envFile -f $composeFile up -d
if ($LASTEXITCODE -ne 0) { throw 'Docker Compose could not start SQL Server.' }

$ready = $false
for ($attempt = 1; $attempt -le 60; $attempt++) {
    & docker exec --env "SQLCMDPASSWORD=$($environment.WBOOKING_SQL_SA_PASSWORD)" wbooking-sql `
        /opt/mssql-tools18/bin/sqlcmd -C -S localhost -U sa -b -Q 'SELECT 1' *> $null
    if ($LASTEXITCODE -eq 0) { $ready = $true; break }
    Start-Sleep -Seconds 1
}
if (-not $ready) { throw 'SQL Server did not become ready within 60 seconds.' }

$existsRows = Invoke-Sql -SaPassword $environment.WBOOKING_SQL_SA_PASSWORD -Raw `
    -Query "SET NOCOUNT ON; SELECT CASE WHEN DB_ID(N'wbooking_demo') IS NULL THEN 0 ELSE 1 END;"
$exists = ($existsRows | Where-Object { $_ -match '^\s*[01]\s*$' } | Select-Object -First 1).Trim() -eq '1'
if (-not $exists -or $ResetDatabase) {
    $containerBackup = '/var/opt/mssql/backup/wbooking_demo.bak'
    $fileList = Invoke-Sql -SaPassword $environment.WBOOKING_SQL_SA_PASSWORD -Raw `
        -Query "RESTORE FILELISTONLY FROM DISK=N'$containerBackup';"
    $rows = @($fileList | Where-Object { $_ -match '\|' } | ForEach-Object { ,($_ -split '\|') })
    $dataRow = $rows | Where-Object { $_.Count -gt 2 -and $_[2].Trim() -eq 'D' } | Select-Object -First 1
    $logRow = $rows | Where-Object { $_.Count -gt 2 -and $_[2].Trim() -eq 'L' } | Select-Object -First 1
    if (-not $dataRow -or -not $logRow) { throw 'Could not determine fixture logical file names.' }
    $dataLogical = $dataRow[0].Trim().Replace("'", "''")
    $logLogical = $logRow[0].Trim().Replace("'", "''")
    $replace = if ($exists) { ',REPLACE' } else { '' }
    Invoke-Sql -SaPassword $environment.WBOOKING_SQL_SA_PASSWORD -Query @"
IF DB_ID(N'wbooking_demo') IS NOT NULL ALTER DATABASE [wbooking_demo] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
RESTORE DATABASE [wbooking_demo] FROM DISK=N'$containerBackup'
WITH MOVE N'$dataLogical' TO N'/var/opt/mssql/data/wbooking_demo.mdf',
     MOVE N'$logLogical' TO N'/var/opt/mssql/data/wbooking_demo_log.ldf'$replace;
ALTER DATABASE [wbooking_demo] SET MULTI_USER;
"@ | Out-Null
    Write-Host 'Restored sanitized wbooking_demo database.'
} else {
    Write-Host 'wbooking_demo already exists; keeping the current local database.'
}

$appPassword = $environment.WBOOKING_SQL_APP_PASSWORD.Replace("'", "''")
Invoke-Sql -SaPassword $environment.WBOOKING_SQL_SA_PASSWORD -Query @"
IF SUSER_ID(N'wbooking') IS NULL CREATE LOGIN [wbooking] WITH PASSWORD=N'$appPassword',CHECK_POLICY=OFF;
ELSE ALTER LOGIN [wbooking] WITH PASSWORD=N'$appPassword',CHECK_POLICY=OFF;
USE [wbooking_demo];
IF USER_ID(N'wbooking') IS NULL CREATE USER [wbooking] FOR LOGIN [wbooking];
ELSE ALTER USER [wbooking] WITH LOGIN=[wbooking];
IF IS_ROLEMEMBER(N'db_owner',N'wbooking')<>1 ALTER ROLE [db_owner] ADD MEMBER [wbooking];
DBCC CHECKDB ([wbooking_demo]) WITH NO_INFOMSGS;
"@ | Out-Null

Write-Host "Sanitized SQL Server demo is ready on localhost:$($environment.WBOOKING_SQL_PORT)."
