Imports System.Web

''' <summary>
''' Enforces the checked-in payment endpoint policy.
''' </summary>
''' <remarks>
''' WBooking does not currently ship a verified production payment-provider
''' protocol. TPVTest=1 enables only the local simulation used by developers;
''' TPVTest=0 disables every payment endpoint. No other value is valid. A
''' production integration must replace this guard with a provider-specific,
''' signature-verified implementation rather than relaxing the local check.
''' </remarks>
Public NotInheritable Class PaymentEndpointGuard
    Private Sub New()
    End Sub

    Public Shared Function AllowLocalSimulation(ByVal context As HttpContext) As Boolean
        If context IsNot Nothing AndAlso
           cfr.cte.Config.TPVTest = 1 AndAlso
           context.Request.IsLocal Then
            Return True
        End If

        If context IsNot Nothing Then
            context.Response.StatusCode = 404
            context.Response.StatusDescription = "Not Found"
            context.Response.TrySkipIisCustomErrors = False
            context.Response.SuppressContent = True
            context.ApplicationInstance.CompleteRequest()
        End If
        Return False
    End Function
End Class
