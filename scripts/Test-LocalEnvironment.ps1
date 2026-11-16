<#
.SYNOPSIS
Checks SQL connectivity and the public HTTP endpoints for a local WBooking run.
.PARAMETER WebPort
HTTP TCP port of the already-running IIS Express instance. The default is 8080.
.PARAMETER Database
Exact database queried through the application login. It must match the value
passed to Start-WBooking.ps1. The authoritative compatibility default is cfrbook;
use wbooking_demo for the sanitized demo. Only letters, digits, and underscores
are accepted.
#>
[CmdletBinding()]
param(
    [ValidateRange(1, 65535)]
    [int]$WebPort = 8080,

    [ValidatePattern('^[A-Za-z0-9_]+$')]
    [string]$Database = 'cfrbook'
)

$ErrorActionPreference = 'Stop'

function Invoke-NoRedirectRequest {
    param(
        [Parameter(Mandatory = $true)]
        [Uri]$Uri,

        [ValidateSet('GET', 'POST')]
        [string]$Method = 'GET',

        [hashtable]$Form,

        [Parameter(Mandatory = $true)]
        [Net.CookieContainer]$Cookies
    )

    $request = [Net.HttpWebRequest]::Create($Uri)
    $request.Method = $Method
    $request.AllowAutoRedirect = $false
    $request.CookieContainer = $Cookies
    $request.Timeout = 15000

    if ($Method -eq 'POST') {
        $body = ($Form.GetEnumerator() | ForEach-Object {
            '{0}={1}' -f [Net.WebUtility]::UrlEncode([string]$_.Key),
                [Net.WebUtility]::UrlEncode([string]$_.Value)
        }) -join '&'
        $payload = [Text.Encoding]::UTF8.GetBytes($body)
        $request.ContentType = 'application/x-www-form-urlencoded'
        $request.ContentLength = $payload.Length
        $stream = $request.GetRequestStream()
        try {
            $stream.Write($payload, 0, $payload.Length)
        } finally {
            $stream.Dispose()
        }
    }

    $response = $request.GetResponse()
    try {
        [PSCustomObject]@{
            Status = [int]$response.StatusCode
            Location = $response.Headers['Location']
        }
    } finally {
        $response.Dispose()
    }
}

function Assert-NoExternalRuntimeAssets {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Content,

        [Parameter(Mandatory = $true)]
        [string]$Path
    )

    $externalAsset = [regex]::Match(
        $Content,
        '<(?:script|link)\b[^>]*?(?:src|href)\s*=\s*["''](?:https?:)?//',
        [Text.RegularExpressions.RegexOptions]::IgnoreCase
    )
    if ($externalAsset.Success) {
        throw "$Path contains an external runtime CSS or JavaScript dependency."
    }
}

$repoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$envFile = Join-Path $repoRoot '.local\sqlserver\.env'
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
if ([string]::IsNullOrWhiteSpace($environment.WBOOKING_SQL_APP_PASSWORD)) {
    throw 'WBOOKING_SQL_APP_PASSWORD is missing from the local environment file.'
}

$sqlOutput = @(& docker exec --env "SQLCMDPASSWORD=$($environment.WBOOKING_SQL_APP_PASSWORD)" `
    wbooking-sql /opt/mssql-tools18/bin/sqlcmd -C -S localhost -U wbooking -d $Database `
    -b -h -1 -W -Q 'SET NOCOUNT ON; SELECT COUNT_BIG(*) FROM sys.tables WHERE is_ms_shipped = 0;' 2>&1)
if ($LASTEXITCODE -ne 0) {
    $excerpt = ($sqlOutput | Select-Object -First 12) -join [Environment]::NewLine
    throw "The application login could not query $Database.`n$excerpt"
}
$tableCount = ($sqlOutput | Where-Object { $_ -match '^\s*\d+\s*$' } | Select-Object -First 1).Trim()

$session = New-Object Microsoft.PowerShell.Commands.WebRequestSession
$results = foreach ($path in @('/', '/Default.aspx', '/Admin/Login.aspx')) {
    $response = Invoke-WebRequest -Uri "http://localhost:$WebPort$path" -WebSession $session -UseBasicParsing -TimeoutSec 15
    Assert-NoExternalRuntimeAssets -Content $response.Content -Path $path
    [PSCustomObject]@{
        Path = $path
        Status = [int]$response.StatusCode
        Bytes = $response.RawContentLength
    }
}

$demoAdminResult = $null
$demoBookingResult = $null
$demoCancellationResult = $null
if ($Database -eq 'wbooking_demo') {
    # The fixture owns this stable search window. External date fields mean
    # "run availability now", so a successful response must render results
    # rather than merely echoing the search form.
    $bookingForm = @{
        fechad1 = '20'; fecham1 = '6'; fechay1 = '2027'
        fechad2 = '23'; fecham2 = '6'; fechay2 = '2027'
        f_numAdultos = '2'; f_ninosPago = '0'; f_ninosfree = '0'; f_regimen = '1'
    }
    $bookingResponse = Invoke-WebRequest `
        -Uri "http://localhost:$WebPort/b_Request.aspx?hotelcode=280316&language=2" `
        -Method Post -Body $bookingForm -UseBasicParsing -TimeoutSec 30
    if ($bookingResponse.Content -notmatch 'id=["'']pnlResults["'']') {
        throw 'The demo external search did not render room results.'
    }
    Assert-NoExternalRuntimeAssets -Content $bookingResponse.Content -Path '/b_Request.aspx'
    $demoBookingResult = [PSCustomObject]@{
        Path = '/b_Request.aspx?<external-search>'
        Status = [int]$bookingResponse.StatusCode
        Bytes = $bookingResponse.RawContentLength
    }

    $cancelSession = New-Object Microsoft.PowerShell.Commands.WebRequestSession
    $cancelUri = "http://localhost:$WebPort/B_Cancel.aspx?hotelcode=280316&language=2&idBook=999999"
    $cancelPage = Invoke-WebRequest -Uri $cancelUri -WebSession $cancelSession -UseBasicParsing -TimeoutSec 15
    if ($cancelPage.Content -notmatch 'CancellationCode' -or
        $cancelPage.Content -match 'name=["''][^"'']*(login|clave)["'']') {
        throw 'The public cancellation page does not expose the single-use token form safely.'
    }
    $cancelForm = @{}
    [regex]::Matches($cancelPage.Content, '<input\b[^>]*>', 'IgnoreCase') | ForEach-Object {
        $nameMatch = [regex]::Match($_.Value, 'name=["'']([^"'']+)', 'IgnoreCase')
        if (-not $nameMatch.Success) { return }
        $valueMatch = [regex]::Match($_.Value, 'value=["'']([^"'']*)', 'IgnoreCase')
        $cancelForm[[Net.WebUtility]::HtmlDecode($nameMatch.Groups[1].Value)] = if ($valueMatch.Success) {
            [Net.WebUtility]::HtmlDecode($valueMatch.Groups[1].Value)
        } else { '' }
    }
    $bookingField = @($cancelForm.Keys | Where-Object { $_ -match 'Localizador$' } | Select-Object -First 1)
    $tokenField = @($cancelForm.Keys | Where-Object { $_ -match 'CancellationCode$' } | Select-Object -First 1)
    $buttonField = @($cancelForm.Keys | Where-Object { $_ -match 'btnCancelBook$' } | Select-Object -First 1)
    if ($bookingField.Count -ne 1 -or $tokenField.Count -ne 1 -or $buttonField.Count -ne 1) {
        throw 'The public cancellation form fields could not be identified.'
    }
    $cancelForm[$bookingField[0]] = '999999'
    $cancelForm[$tokenField[0]] = ('A' * 43)
    $cancelForm[$buttonField[0]] = 'Cancel booking'
    $invalidCancel = Invoke-WebRequest -Uri $cancelUri -Method Post -Body $cancelForm `
        -WebSession $cancelSession -UseBasicParsing -TimeoutSec 15
    if ($invalidCancel.Content -notmatch 'invalid or has expired') {
        throw 'An invalid cancellation token did not fail with the generic response.'
    }
    $demoCancellationResult = [PSCustomObject]@{
        Path = '/B_Cancel.aspx?<invalid-single-use-token>'
        Status = [int]$invalidCancel.StatusCode
        Bytes = $invalidCancel.RawContentLength
    }

    $loginPage = Invoke-WebRequest -Uri "http://localhost:$WebPort/Admin/Login.aspx" -WebSession $session -UseBasicParsing -TimeoutSec 15
    $loginUri = [Uri]"http://localhost:$WebPort/Admin/Login.aspx"
    $preAuthSessionCookie = $session.Cookies.GetCookies($loginUri)['ASP.NET_SessionId']
    $preAuthSessionId = if ($preAuthSessionCookie) { $preAuthSessionCookie.Value } else { $null }
    $form = @{}
    [regex]::Matches($loginPage.Content, '<input\b[^>]*>', 'IgnoreCase') | ForEach-Object {
        $tag = $_.Value
        $name = [regex]::Match($tag, '\bname=["'']([^"'']+)', 'IgnoreCase').Groups[1].Value
        $type = [regex]::Match($tag, '\btype=["'']([^"'']+)', 'IgnoreCase').Groups[1].Value
        $value = [Net.WebUtility]::HtmlDecode([regex]::Match($tag, '\bvalue=["'']([^"'']*)', 'IgnoreCase').Groups[1].Value)
        if ($name -and $type -eq 'hidden') { $form[$name] = $value }
    }
    $form.RegisteredLogin = 'admin'
    $form.RegisteredPwd = 'admin'
    $form.btnLogin = 'Entrar'
    $loginResponse = Invoke-NoRedirectRequest -Uri $loginUri -Method POST -Form $form -Cookies $session.Cookies
    $loginStatus = $loginResponse.Status
    $location = $loginResponse.Location
    if ($loginStatus -ne 302 -or $location -notmatch 'Login\.aspx\?resume=1$') {
        throw 'The demo hotel administrator login did not start session rotation.'
    }
    $resumeUri = [Uri]::new($loginUri, $location)
    $resumeResponse = Invoke-NoRedirectRequest -Uri $resumeUri -Cookies $session.Cookies
    $resumeStatus = $resumeResponse.Status
    $location = $resumeResponse.Location
    if ($resumeStatus -ne 302 -or $location -notmatch '^/Admin/default\.aspx\?sessionlink=') {
        throw 'The rotated administrator session did not redirect to its authenticated session.'
    }
    $postAuthSessionCookie = $session.Cookies.GetCookies($loginUri)['ASP.NET_SessionId']
    if (-not $postAuthSessionCookie -or
        ($preAuthSessionId -and $postAuthSessionCookie.Value -eq $preAuthSessionId)) {
        throw 'The ASP.NET session identifier was not rotated after authentication.'
    }
    $adminPage = Invoke-WebRequest -Uri ("http://localhost:$WebPort" + $location) -WebSession $session -UseBasicParsing -TimeoutSec 15
    if ($adminPage.Content -match 'name=["'']RegisteredLogin["'']') {
        throw 'The demo hotel administrator session returned to the login form.'
    }
    $availabilityLocation = $location.ToString().Replace('/Admin/default.aspx', '/Admin/availability.aspx')
    $availabilityPage = Invoke-WebRequest -Uri ("http://localhost:$WebPort" + $availabilityLocation) -WebSession $session -UseBasicParsing -TimeoutSec 15
    if ($availabilityPage.Content -notmatch 'btnsaveavailability') {
        throw 'The demo administrator could not open the availability configuration screen.'
    }
    $hotelEditLocation = $location.ToString().Replace('/Admin/default.aspx', '/Admin/HotelEdit.aspx')
    $hotelEditPage = Invoke-WebRequest -Uri ("http://localhost:$WebPort" + $hotelEditLocation) -WebSession $session -UseBasicParsing -TimeoutSec 15
    if ($hotelEditPage.Content -notmatch 'editdesc_es') {
        throw 'The demo administrator could not open the sanitized hotel editor.'
    }
    $reportLocation = $location.ToString().Replace('/Admin/default.aspx', '/Admin/Rep_Stat_requests.aspx')
    $reportPage = Invoke-WebRequest -Uri ("http://localhost:$WebPort" + $reportLocation) -WebSession $session -UseBasicParsing -TimeoutSec 30
    if ($reportPage.Content -notmatch 'wbooking-chart\.js' -or $reportPage.Content -match 'gstatic\.com/charts') {
        throw 'The request statistics report did not use the local chart implementation.'
    }
    Assert-NoExternalRuntimeAssets -Content $adminPage.Content -Path '/Admin/default.aspx'
    Assert-NoExternalRuntimeAssets -Content $availabilityPage.Content -Path '/Admin/availability.aspx'
    Assert-NoExternalRuntimeAssets -Content $hotelEditPage.Content -Path '/Admin/HotelEdit.aspx'
    Assert-NoExternalRuntimeAssets -Content $reportPage.Content -Path '/Admin/Rep_Stat_requests.aspx'
    $demoAdminResult = @(
        [PSCustomObject]@{ Path = '/Admin/availability.aspx?sessionlink=<generated>'; Status = [int]$availabilityPage.StatusCode; Bytes = $availabilityPage.RawContentLength }
        [PSCustomObject]@{ Path = '/Admin/HotelEdit.aspx?sessionlink=<generated>'; Status = [int]$hotelEditPage.StatusCode; Bytes = $hotelEditPage.RawContentLength }
        [PSCustomObject]@{ Path = '/Admin/Rep_Stat_requests.aspx?sessionlink=<generated>'; Status = [int]$reportPage.StatusCode; Bytes = $reportPage.RawContentLength }
    )
}

$missingImageStatus = 0
try {
    Invoke-WebRequest -Uri "http://localhost:$WebPort/RoomImage.ashx?name=missing.jpg" -UseBasicParsing -TimeoutSec 15 | Out-Null
} catch {
    if (-not $_.Exception.Response) { throw }
    $missingImageStatus = [int]$_.Exception.Response.StatusCode
}
if ($missingImageStatus -ne 404) {
    throw "The external image handler returned $missingImageStatus instead of 404 for a missing image."
}

$imagePostStatus = 0
try {
    Invoke-WebRequest -Uri "http://localhost:$WebPort/RoomImage.ashx?name=missing.jpg" -Method Post -UseBasicParsing -TimeoutSec 15 | Out-Null
} catch {
    if (-not $_.Exception.Response) { throw }
    $imagePostStatus = [int]$_.Exception.Response.StatusCode
}
if ($imagePostStatus -ne 405) {
    throw "The external image handler returned $imagePostStatus instead of 405 for POST."
}

Write-Host "Database check passed: $Database exposes $tableCount user tables to the wbooking login."
$results | Format-Table -AutoSize
if ($demoAdminResult) {
    Write-Host 'Demo full-control administrator login, availability and sanitized editor access passed.'
    $demoAdminResult | Format-Table -AutoSize
}
Write-Host 'External room-image handler negative checks passed.'
if ($demoBookingResult) {
    Write-Host 'Demo external booking search returned room results.'
    $demoBookingResult | Format-Table -AutoSize
}
if ($demoCancellationResult) {
    Write-Host 'Demo public cancellation token form and invalid-token handling passed.'
    $demoCancellationResult | Format-Table -AutoSize
}
