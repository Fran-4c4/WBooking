Imports System.Configuration
Imports System.Web

''' <summary>
''' Owns the deployment switch for guest self-service cancellation.
''' </summary>
''' <remarks>
''' EnablePublicCancellation has a closed 0/1 domain. Missing or 0 means disabled;
''' 1 exposes the hashed, expiring, single-use token workflow. Invalid values fail
''' configuration explicitly. The checked-in default is 1. Changing it to 0 makes
''' cancellation hotel-managed and returns 404 without changing issued tokens.
''' </remarks>
Public NotInheritable Class PublicCancellationGuard
    Public Const ConfigurationKey As String = "EnablePublicCancellation"

    Private Sub New()
    End Sub

    Public Shared Sub RequireEnabled()
        Dim configuredValue As String = ConfigurationManager.AppSettings(ConfigurationKey)
        If String.IsNullOrWhiteSpace(configuredValue) OrElse configuredValue = "0" Then
            Throw New HttpException(404, "Not found.")
        End If
        If configuredValue <> "1" Then
            Throw New ConfigurationErrorsException(ConfigurationKey & " must be 0 or 1.")
        End If
    End Sub
End Class
