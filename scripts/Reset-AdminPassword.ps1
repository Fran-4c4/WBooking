[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [ValidateNotNullOrEmpty()]
    [string]$Login,

    [ValidateNotNullOrEmpty()]
    [string]$Database = 'cfrbook'
)

$ErrorActionPreference = 'Stop'

# CFR.BookEngine is intentionally x86. Relaunch before asking for the secret so
# no password has to cross a process boundary or appear in command history.
if ([IntPtr]::Size -ne 4) {
    $x86PowerShell = Join-Path $env:WINDIR 'SysWOW64\WindowsPowerShell\v1.0\powershell.exe'
    if (-not (Test-Path -LiteralPath $x86PowerShell)) {
        throw 'A 32-bit Windows PowerShell host is required to load CFR.BookEngine.'
    }

    & $x86PowerShell -NoProfile -ExecutionPolicy Bypass -File $PSCommandPath `
        -Login $Login -Database $Database
    exit $LASTEXITCODE
}

$repositoryRoot = Split-Path -Parent $PSScriptRoot
$assemblyCandidates = @(
    (Join-Path $repositoryRoot 'Web30\Bin\CFR.BookEngine.dll'),
    (Join-Path $repositoryRoot 'CFR.BookEngine\bin\CFR.BookEngine.dll')
)
$assemblyPath = $assemblyCandidates | Where-Object { Test-Path -LiteralPath $_ } | Select-Object -First 1
if ([string]::IsNullOrWhiteSpace($assemblyPath)) {
    throw 'CFR.BookEngine.dll was not found. Build Web30.sln in the x86 configuration first.'
}
[Reflection.Assembly]::LoadFrom($assemblyPath) | Out-Null

$connectionString = $env:WBOOKING_SQL_CONNECTION_STRING
if ([string]::IsNullOrWhiteSpace($connectionString)) {
    $environmentFile = Join-Path $repositoryRoot '.local\sqlserver\.env'
    if (-not (Test-Path -LiteralPath $environmentFile)) {
        throw 'Set WBOOKING_SQL_CONNECTION_STRING or initialize the local SQL Server environment.'
    }

    $localEnvironment = @{}
    Get-Content -LiteralPath $environmentFile | ForEach-Object {
        if ($_ -match '^([^#=]+)=(.*)$') {
            $localEnvironment[$matches[1]] = $matches[2]
        }
    }

    foreach ($requiredName in @('WBOOKING_SQL_PORT', 'WBOOKING_SQL_APP_PASSWORD')) {
        if ([string]::IsNullOrWhiteSpace($localEnvironment[$requiredName])) {
            throw "$requiredName is missing from the local SQL Server environment."
        }
    }

    $builder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder
    $builder['Data Source'] = "localhost,$($localEnvironment.WBOOKING_SQL_PORT)"
    $builder['User ID'] = 'wbooking'
    $builder['Password'] = $localEnvironment.WBOOKING_SQL_APP_PASSWORD
    $builder['Encrypt'] = $true
    $builder['TrustServerCertificate'] = $true
    $connectionString = $builder.ConnectionString
}

$connectionBuilder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder($connectionString)
$connectionBuilder['Initial Catalog'] = $Database

$firstPassword = Read-Host 'Nueva contraseña' -AsSecureString
$secondPassword = Read-Host 'Repita la nueva contraseña' -AsSecureString
$firstPointer = [IntPtr]::Zero
$secondPointer = [IntPtr]::Zero
$plainPassword = $null
$confirmation = $null

try {
    $firstPointer = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($firstPassword)
    $secondPointer = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($secondPassword)
    $plainPassword = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($firstPointer)
    $confirmation = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($secondPointer)

    if (-not [string]::Equals($plainPassword, $confirmation, [StringComparison]::Ordinal)) {
        throw 'Las contraseñas no coinciden.'
    }

    $passwordHash = [cfr.security.PasswordHasher]::HashPassword($plainPassword)
} finally {
    if ($firstPointer -ne [IntPtr]::Zero) {
        [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($firstPointer)
    }
    if ($secondPointer -ne [IntPtr]::Zero) {
        [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($secondPointer)
    }
    $plainPassword = $null
    $confirmation = $null
}

$connection = New-Object System.Data.SqlClient.SqlConnection($connectionBuilder.ConnectionString)
$connection.Open()
try {
    $schemaCommand = $connection.CreateCommand()
    $schemaCommand.CommandText = "SELECT COL_LENGTH('dbo.R_Usuarios', 'clave')"
    if ([int]$schemaCommand.ExecuteScalar() -lt 512) {
        throw 'The selected database schema is incompatible with this WBooking release: R_Usuarios.clave must hold a versioned password hash.'
    }

    $transaction = $connection.BeginTransaction()
    try {
        $updateCommand = $connection.CreateCommand()
        $updateCommand.Transaction = $transaction
        $updateCommand.CommandText = @'
UPDATE dbo.R_Usuarios
SET clave = @passwordHash,
    modifydate = GETDATE()
WHERE login = @login;
'@
        [void]$updateCommand.Parameters.Add('@passwordHash', [Data.SqlDbType]::NVarChar, 256)
        [void]$updateCommand.Parameters.Add('@login', [Data.SqlDbType]::NVarChar, 255)
        $updateCommand.Parameters['@passwordHash'].Value = $passwordHash
        $updateCommand.Parameters['@login'].Value = $Login

        $affectedUsers = $updateCommand.ExecuteNonQuery()
        if ($affectedUsers -ne 1) {
            throw "Expected one user named '$Login', but updated $affectedUsers."
        }

        $sessionCommand = $connection.CreateCommand()
        $sessionCommand.Transaction = $transaction
        $sessionCommand.CommandText = @'
DELETE sessions
FROM dbo.R_UsuariosSession AS sessions
INNER JOIN dbo.R_Usuarios AS users ON users.id_usuario = sessions.id_usuario
WHERE users.login = @login;
'@
        [void]$sessionCommand.Parameters.Add('@login', [Data.SqlDbType]::NVarChar, 255)
        $sessionCommand.Parameters['@login'].Value = $Login
        [void]$sessionCommand.ExecuteNonQuery()

        $transaction.Commit()
    } catch {
        $transaction.Rollback()
        throw
    }
} finally {
    $connection.Dispose()
}

Write-Host "Password reset for '$Login'. Existing sessions were revoked."
