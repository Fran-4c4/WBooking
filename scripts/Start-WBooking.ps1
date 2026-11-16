<#
.SYNOPSIS
Starts WBooking with IIS Express against a selected local SQL Server database.
.PARAMETER Port
Local HTTP TCP port. Valid values are 1 through 65535; the default is 8080.
.PARAMETER Database
Exact local SQL Server database name used by every WBooking data-access path.
The authoritative compatibility default is cfrbook. Use wbooking_demo for the
sanitized public demo. Names are restricted to letters, digits, and underscores.
#>
[CmdletBinding()]
param(
    [ValidateRange(1, 65535)]
    [int]$Port = 8080,

    [ValidatePattern('^[A-Za-z0-9_]+$')]
    [string]$Database = 'cfrbook'
)

$ErrorActionPreference = 'Stop'
$repoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$envFile = Join-Path $repoRoot '.local\sqlserver\.env'
$runtimeRoot = Join-Path $repoRoot '.local\iisexpress'
$pidFile = Join-Path $runtimeRoot "iisexpress-$Port.pid"

if (-not (Test-Path -LiteralPath $envFile -PathType Leaf)) {
    throw 'Run scripts\Initialize-LocalSqlServer.ps1 first.'
}

$environment = @{}
foreach ($line in [IO.File]::ReadAllLines($envFile)) {
    if ([string]::IsNullOrWhiteSpace($line) -or $line.TrimStart().StartsWith('#')) { continue }
    $parts = $line.Split('=', 2)
    if ($parts.Count -ne 2) { throw "Invalid environment entry in $envFile." }
    $environment[$parts[0].Trim()] = $parts[1]
}
foreach ($required in @('WBOOKING_SQL_PORT', 'WBOOKING_SQL_APP_PASSWORD')) {
    if (-not $environment.ContainsKey($required) -or [string]::IsNullOrWhiteSpace($environment[$required])) {
        throw "$required is missing from $envFile."
    }
}

if (Test-Path -LiteralPath $pidFile) {
    $existingPid = [int]([IO.File]::ReadAllText($pidFile).Trim())
    if (Get-Process -Id $existingPid -ErrorAction SilentlyContinue) {
        Write-Host "WBooking is already running with PID $existingPid at http://localhost:$Port/."
        return
    }
}

$iisExpress = @(
    'C:\Program Files (x86)\IIS Express\iisexpress.exe',
    'C:\Program Files\IIS Express\iisexpress.exe'
) | Where-Object { Test-Path -LiteralPath $_ } | Select-Object -First 1
if (-not $iisExpress) {
    throw 'IIS Express was not found. Install it through Visual Studio or Build Tools.'
}

New-Item -ItemType Directory -Force -Path $runtimeRoot | Out-Null
$env:WBOOKING_SQL_CONNECTION_STRING = "Server=localhost,$($environment.WBOOKING_SQL_PORT);Database=$Database;User ID=wbooking;Password=$($environment.WBOOKING_SQL_APP_PASSWORD);Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True;Application Name=WBooking"
$arguments = @(
    "/path:`"$(Join-Path $repoRoot 'Web30')`"",
    "/port:$Port",
    '/systray:false'
)
$process = Start-Process -FilePath $iisExpress -ArgumentList $arguments -WindowStyle Hidden -PassThru `
    -RedirectStandardOutput (Join-Path $runtimeRoot "stdout-$Port.log") `
    -RedirectStandardError (Join-Path $runtimeRoot "stderr-$Port.log")
[IO.File]::WriteAllText($pidFile, [string]$process.Id, (New-Object Text.UTF8Encoding($false)))

$started = $false
for ($attempt = 1; $attempt -le 30; $attempt++) {
    if ($process.HasExited) { break }
    try {
        $response = Invoke-WebRequest -Uri "http://localhost:$Port/" -UseBasicParsing -TimeoutSec 5
        if ($response.StatusCode -ge 200 -and $response.StatusCode -lt 500) {
            $started = $true
            break
        }
    } catch {
        if ($_.Exception.Response -and [int]$_.Exception.Response.StatusCode -lt 500) {
            $started = $true
            break
        }
    }
    Start-Sleep -Seconds 1
}

if (-not $started) {
    $errorExcerpt = @()
    foreach ($path in @((Join-Path $runtimeRoot "stderr-$Port.log"), (Join-Path $runtimeRoot "stdout-$Port.log"))) {
        if (Test-Path -LiteralPath $path) {
            $errorExcerpt += Get-Content -LiteralPath $path -Tail 20 | ForEach-Object {
                if ($_.Length -gt 500) { $_.Substring(0, 500) } else { $_ }
            }
        }
    }
    throw "IIS Express did not serve WBooking successfully.`n$($errorExcerpt -join [Environment]::NewLine)"
}

Write-Host "WBooking is running at http://localhost:$Port/ against $Database (PID $($process.Id))."
