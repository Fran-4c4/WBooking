[CmdletBinding()]
param(
    [switch]$StopSqlServer
)

$ErrorActionPreference = 'Stop'
$repoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$pidFiles = @(Get-ChildItem -LiteralPath (Join-Path $repoRoot '.local\iisexpress') -Filter 'iisexpress*.pid' -File -ErrorAction SilentlyContinue)
foreach ($pidFile in $pidFiles) {
    $processId = [int]([IO.File]::ReadAllText($pidFile.FullName).Trim())
    $process = Get-Process -Id $processId -ErrorAction SilentlyContinue
    if ($process) {
        Stop-Process -Id $processId
        Wait-Process -Id $processId -Timeout 10 -ErrorAction SilentlyContinue
        Write-Host "Stopped WBooking (PID $processId)."
    }
    [IO.File]::Delete($pidFile.FullName)
}

if ($StopSqlServer) {
    $envFile = Join-Path $repoRoot '.local\sqlserver\.env'
    $composeFile = Join-Path $repoRoot 'docker-compose.sqlserver.yml'
    if (Test-Path -LiteralPath $envFile) {
        & docker compose --env-file $envFile -f $composeFile stop
        if ($LASTEXITCODE -ne 0) { throw 'Could not stop the SQL Server container.' }
    }
}
