Imports System.Configuration
Imports System.Globalization
Imports System.Web

''' <summary>
''' Owns HTTPS enforcement and browser transport-policy behavior.
''' </summary>
''' <remarks>
''' ForceHttps has a closed 0/1 domain: missing or 0 leaves HTTP available for
''' local development; 1 permanently redirects non-HTTPS requests to the same
''' host and path using HTTPS. Request.IsSecureConnection is authoritative, so a
''' reverse proxy must preserve the HTTPS scheme for ASP.NET before enabling it.
''' HstsMaxAgeSeconds is an integer from 0 through 63072000; missing uses the
''' authoritative one-year default. HSTS is emitted only on HTTPS responses.
''' The CSP intentionally retains inline script/style compatibility required by
''' this Web Forms UI while blocking plug-ins, foreign framing, foreign forms and
''' non-HTTPS third-party resources. Tightening it requires testing every public
''' and administrator page and replacing inline code with nonces or local files.
''' </remarks>
Public NotInheritable Class TransportSecurityPolicy
    Public Const ForceHttpsConfigurationKey As String = "ForceHttps"
    Public Const HstsMaxAgeConfigurationKey As String = "HstsMaxAgeSeconds"
    Public Const DefaultHstsMaxAgeSeconds As Integer = 31536000
    Public Const MaximumHstsMaxAgeSeconds As Integer = 63072000

    Private Const ContentSecurityPolicy As String =
        "default-src 'self' https: data:; " &
        "script-src 'self' 'unsafe-inline' 'unsafe-eval' https:; " &
        "style-src 'self' 'unsafe-inline' https:; " &
        "img-src 'self' data: https:; font-src 'self' data: https:; " &
        "connect-src 'self' https:; object-src 'none'; base-uri 'self'; " &
        "form-action 'self'; frame-ancestors 'self'"

    Private Sub New()
    End Sub

    Public Shared Sub Apply(ByVal context As HttpContext)
        If context Is Nothing Then
            Throw New ArgumentNullException("context")
        End If

        context.Response.Headers.Set("Content-Security-Policy", ContentSecurityPolicy)

        If context.Request.IsSecureConnection Then
            context.Response.Headers.Set(
                "Strict-Transport-Security",
                "max-age=" & ReadHstsMaxAgeSeconds().ToString(CultureInfo.InvariantCulture) & "; includeSubDomains")
            Return
        End If

        If ReadForceHttps() Then
            Dim secureUri As New UriBuilder(context.Request.Url) With {.Scheme = Uri.UriSchemeHttps, .Port = -1}
            context.Response.RedirectPermanent(secureUri.Uri.AbsoluteUri, True)
        End If
    End Sub

    Private Shared Function ReadForceHttps() As Boolean
        Dim configuredValue As String = ConfigurationManager.AppSettings(ForceHttpsConfigurationKey)
        If String.IsNullOrWhiteSpace(configuredValue) OrElse configuredValue = "0" Then
            Return False
        End If
        If configuredValue = "1" Then
            Return True
        End If
        Throw New ConfigurationErrorsException(ForceHttpsConfigurationKey & " must be 0 or 1.")
    End Function

    Private Shared Function ReadHstsMaxAgeSeconds() As Integer
        Dim configuredValue As String = ConfigurationManager.AppSettings(HstsMaxAgeConfigurationKey)
        If String.IsNullOrWhiteSpace(configuredValue) Then
            Return DefaultHstsMaxAgeSeconds
        End If

        Dim seconds As Integer
        If Not Integer.TryParse(configuredValue, NumberStyles.None, CultureInfo.InvariantCulture, seconds) OrElse
           seconds < 0 OrElse seconds > MaximumHstsMaxAgeSeconds Then
            Throw New ConfigurationErrorsException(
                HstsMaxAgeConfigurationKey & " must be an integer from 0 through " & MaximumHstsMaxAgeSeconds.ToString(CultureInfo.InvariantCulture) & ".")
        End If
        Return seconds
    End Function
End Class
