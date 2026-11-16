<#
.SYNOPSIS
Restores the two private September 2018 databases into the local SQL Server container.

.PARAMETER SourceArchive
Outer 7-Zip archive containing BBDD_Back\cfrbook_bbdd-produccion_20180919.7z and
BBDD_Back\cfrhotel_bbdd-produccion_20180919.7z. Missing or differently named entries
are rejected. The extracted data stays under ignored .local storage.

.PARAMETER Port
Host TCP port mapped to SQL Server port 1433. The first successful run owns this
value in .local\sqlserver\.env; later conflicting values are rejected.

.PARAMETER ResetDatabases
Restores both databases with REPLACE. Without this switch, existing local databases
and their changes are preserved.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [ValidateScript({ Test-Path -LiteralPath $_ -PathType Leaf })]
    [string]$SourceArchive,

    [ValidateRange(1, 65535)]
    [int]$Port = 14333,

    [switch]$ResetDatabases
)

$ErrorActionPreference = 'Stop'
$repoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$localRoot = Join-Path $repoRoot '.local\sqlserver'
$archiveRoot = Join-Path $localRoot 'archives'
$backupRoot = Join-Path $localRoot 'backups'
$envFile = Join-Path $localRoot '.env'
$composeFile = Join-Path $repoRoot 'docker-compose.sqlserver.yml'
$sevenZip = (Get-Command 7z.exe -ErrorAction Stop).Source

function New-LocalPassword {
    return 'Wb!Aa9' + [Guid]::NewGuid().ToString('N')
}

function Read-LocalEnvironment([string]$Path) {
    $values = @{}
    foreach ($line in [IO.File]::ReadAllLines($Path)) {
        if ([string]::IsNullOrWhiteSpace($line) -or $line.TrimStart().StartsWith('#')) {
            continue
        }
        $parts = $line.Split('=', 2)
        if ($parts.Count -ne 2 -or [string]::IsNullOrWhiteSpace($parts[0])) {
            throw "Invalid environment entry in $Path."
        }
        $values[$parts[0].Trim()] = $parts[1]
    }
    return $values
}

function Invoke-SqlCmdInContainer {
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
    if ($Raw) {
        $arguments += @('-h', '-1', '-s', '|')
    }
    $arguments += @('-Q', $Query)
    $result = @(& docker @arguments 2>&1)
    if ($LASTEXITCODE -ne 0) {
        $excerpt = ($result | Select-Object -First 12) -join [Environment]::NewLine
        throw "SQL command failed.`n$excerpt"
    }
    return $result
}

function Restore-Database {
    param(
        [string]$DatabaseName,
        [string]$BackupFileName,
        [string]$SaPassword,
        [bool]$Replace
    )

    $existsOutput = Invoke-SqlCmdInContainer -SaPassword $SaPassword -Raw -Query "SET NOCOUNT ON; SELECT CASE WHEN DB_ID(N'$DatabaseName') IS NULL THEN 0 ELSE 1 END;"
    $exists = ($existsOutput | Where-Object { $_ -match '^\s*[01]\s*$' } | Select-Object -First 1).Trim() -eq '1'
    if ($exists -and -not $Replace) {
        Write-Host "$DatabaseName already exists; keeping the current local database."
        return
    }

    $containerBackup = "/var/opt/mssql/backup/$BackupFileName"
    $fileList = Invoke-SqlCmdInContainer -SaPassword $SaPassword -Raw -Query "RESTORE FILELISTONLY FROM DISK = N'$containerBackup';"
    $rows = @($fileList | Where-Object { $_ -match '\|' } | ForEach-Object { ,($_ -split '\|') })
    $dataRow = $rows | Where-Object { $_.Count -gt 2 -and $_[2].Trim() -eq 'D' } | Select-Object -First 1
    $logRow = $rows | Where-Object { $_.Count -gt 2 -and $_[2].Trim() -eq 'L' } | Select-Object -First 1
    if (-not $dataRow -or -not $logRow) {
        throw "Could not determine logical data and log names for $DatabaseName."
    }

    $dataLogical = $dataRow[0].Trim().Replace("'", "''")
    $logLogical = $logRow[0].Trim().Replace("'", "''")
    $replaceClause = if ($exists) { ', REPLACE' } else { '' }
    $query = @"
IF DB_ID(N'$DatabaseName') IS NOT NULL
BEGIN
    ALTER DATABASE [$DatabaseName] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
END;
RESTORE DATABASE [$DatabaseName]
FROM DISK = N'$containerBackup'
WITH MOVE N'$dataLogical' TO N'/var/opt/mssql/data/$DatabaseName.mdf',
     MOVE N'$logLogical' TO N'/var/opt/mssql/data/${DatabaseName}_log.ldf'$replaceClause;
ALTER DATABASE [$DatabaseName] SET MULTI_USER;
"@
    Invoke-SqlCmdInContainer -SaPassword $SaPassword -Query $query | Out-Null
    Write-Host "Restored $DatabaseName."
}

New-Item -ItemType Directory -Force -Path $archiveRoot, $backupRoot | Out-Null

$nestedArchives = @(
    'BBDD_Back\cfrbook_bbdd-produccion_20180919.7z',
    'BBDD_Back\cfrhotel_bbdd-produccion_20180919.7z'
)
& $sevenZip e -y "-o$archiveRoot" -- $SourceArchive $nestedArchives | Out-Null
if ($LASTEXITCODE -ne 0) {
    throw 'Could not extract the nested database archives.'
}

$databaseBackups = @(
    @{ Archive = 'cfrbook_bbdd-produccion_20180919.7z'; Backup = 'cfrbook_bbdd-produccion_20180919.bak'; Database = 'cfrbook' },
    @{ Archive = 'cfrhotel_bbdd-produccion_20180919.7z'; Backup = 'cfrhotel_bbdd-produccion_20180919.bak'; Database = 'cfrhotel' }
)
foreach ($item in $databaseBackups) {
    $nestedPath = Join-Path $archiveRoot $item.Archive
    if (-not (Test-Path -LiteralPath $nestedPath -PathType Leaf)) {
        throw "Missing nested archive $($item.Archive)."
    }
    & $sevenZip e -y "-o$backupRoot" -- $nestedPath $item.Backup | Out-Null
    if ($LASTEXITCODE -ne 0) {
        throw "Could not extract $($item.Backup)."
    }
}

if (-not (Test-Path -LiteralPath $envFile -PathType Leaf)) {
    $lines = @(
        "WBOOKING_SQL_PORT=$Port",
        "WBOOKING_SQL_SA_PASSWORD=$(New-LocalPassword)",
        "WBOOKING_SQL_APP_PASSWORD=$(New-LocalPassword)"
    )
    [IO.File]::WriteAllLines($envFile, $lines, (New-Object Text.UTF8Encoding($false)))
}

$environment = Read-LocalEnvironment $envFile
foreach ($required in @('WBOOKING_SQL_PORT', 'WBOOKING_SQL_SA_PASSWORD', 'WBOOKING_SQL_APP_PASSWORD')) {
    if (-not $environment.ContainsKey($required) -or [string]::IsNullOrWhiteSpace($environment[$required]) -or $environment[$required] -eq 'CHANGE_ME') {
        throw "$required must have a non-placeholder value in $envFile."
    }
}
if ([int]$environment.WBOOKING_SQL_PORT -ne $Port) {
    throw "The requested port $Port conflicts with WBOOKING_SQL_PORT in $envFile. Remove the local env file to regenerate it."
}

& docker compose --env-file $envFile -f $composeFile up -d
if ($LASTEXITCODE -ne 0) {
    throw 'Docker Compose could not start SQL Server.'
}

$ready = $false
for ($attempt = 1; $attempt -le 60; $attempt++) {
    & docker exec --env "SQLCMDPASSWORD=$($environment.WBOOKING_SQL_SA_PASSWORD)" wbooking-sql /opt/mssql-tools18/bin/sqlcmd -C -S localhost -U sa -b -Q 'SELECT 1' *> $null
    if ($LASTEXITCODE -eq 0) {
        $ready = $true
        break
    }
    Start-Sleep -Seconds 2
}
if (-not $ready) {
    throw 'SQL Server did not become ready within 120 seconds.'
}

foreach ($item in $databaseBackups) {
    Restore-Database -DatabaseName $item.Database -BackupFileName $item.Backup -SaPassword $environment.WBOOKING_SQL_SA_PASSWORD -Replace $ResetDatabases
}

$escapedPassword = $environment.WBOOKING_SQL_APP_PASSWORD.Replace("'", "''")
$loginQuery = @"
IF SUSER_ID(N'wbooking') IS NULL
    CREATE LOGIN [wbooking] WITH PASSWORD = N'$escapedPassword', CHECK_POLICY = OFF;
ELSE
    ALTER LOGIN [wbooking] WITH PASSWORD = N'$escapedPassword', CHECK_POLICY = OFF;

USE [cfrbook];
IF USER_ID(N'wbooking') IS NULL CREATE USER [wbooking] FOR LOGIN [wbooking];
ELSE ALTER USER [wbooking] WITH LOGIN = [wbooking];
IF IS_ROLEMEMBER(N'db_owner', N'wbooking') <> 1 ALTER ROLE [db_owner] ADD MEMBER [wbooking];

USE [cfrhotel];
IF USER_ID(N'wbooking') IS NULL CREATE USER [wbooking] FOR LOGIN [wbooking];
ELSE ALTER USER [wbooking] WITH LOGIN = [wbooking];
IF IS_ROLEMEMBER(N'db_owner', N'wbooking') <> 1 ALTER ROLE [db_owner] ADD MEMBER [wbooking];
"@
Invoke-SqlCmdInContainer -SaPassword $environment.WBOOKING_SQL_SA_PASSWORD -Query $loginQuery | Out-Null

$summary = Invoke-SqlCmdInContainer -SaPassword $environment.WBOOKING_SQL_SA_PASSWORD -Raw -Query @"
SET NOCOUNT ON;
SELECT N'cfrbook' AS DatabaseName, COUNT_BIG(*) AS UserTableCount FROM cfrbook.sys.tables WHERE is_ms_shipped = 0
UNION ALL
SELECT N'cfrhotel', COUNT_BIG(*) FROM cfrhotel.sys.tables WHERE is_ms_shipped = 0;
"@
Write-Host "SQL Server is ready on localhost:$($environment.WBOOKING_SQL_PORT)."
$summary | Where-Object { $_ -match '\|' } | ForEach-Object { Write-Host $_ }
