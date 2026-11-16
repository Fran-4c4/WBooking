Imports System.Security.Cryptography
Imports System.Text
Imports System.Web

''' <summary>
''' Owns the anti-forgery contract for state-changing administrator requests.
''' </summary>
''' <remarks>
''' The token is generated once per ASP.NET session, rendered by administrator
''' pages in a form field named <see cref="FormFieldName"/>, and validated by
''' ASHX handlers before any state change. Changing either key invalidates open
''' administrator pages and requires updating every producer and consumer.
''' </remarks>
Public NotInheritable Class AdminRequestSecurity
    ''' <summary>
    ''' External form key used by administrator pages and handlers.
    ''' </summary>
    Public Const FormFieldName As String = "__WBAntiForgeryToken"

    Private Const SessionKey As String = "WBooking.Admin.AntiForgeryToken"
    Private Const TokenSizeBytes As Integer = 32

    Private Sub New()
    End Sub

    Public Shared Function GetOrCreateToken(ByVal context As HttpContext) As String
        If context Is Nothing OrElse context.Session Is Nothing Then
            Throw New HttpException(403, "An administrator session is required.")
        End If

        Dim token As String = TryCast(context.Session(SessionKey), String)
        If String.IsNullOrEmpty(token) Then
            Dim tokenBytes(TokenSizeBytes - 1) As Byte
            Using randomGenerator As RandomNumberGenerator = RandomNumberGenerator.Create()
                randomGenerator.GetBytes(tokenBytes)
            End Using
            token = Convert.ToBase64String(tokenBytes)
            context.Session(SessionKey) = token
        End If

        Return token
    End Function

    Public Shared Sub ValidateStateChangingRequest(ByVal context As HttpContext)
        If context Is Nothing OrElse
           Not String.Equals(context.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase) Then
            Throw New HttpException(405, "POST is required.")
        End If

        Dim expectedToken As String = GetOrCreateToken(context)
        Dim submittedToken As String = context.Request.Form(FormFieldName)
        If Not FixedTimeEquals(expectedToken, submittedToken) Then
            Throw New HttpException(403, "The anti-forgery token is invalid.")
        End If
    End Sub

    Private Shared Function FixedTimeEquals(ByVal expected As String, ByVal actual As String) As Boolean
        If String.IsNullOrEmpty(expected) OrElse String.IsNullOrEmpty(actual) Then
            Return False
        End If

        Dim expectedBytes As Byte() = Encoding.UTF8.GetBytes(expected)
        Dim actualBytes As Byte() = Encoding.UTF8.GetBytes(actual)
        Dim difference As Integer = expectedBytes.Length Xor actualBytes.Length
        Dim comparisonLength As Integer = Math.Min(expectedBytes.Length, actualBytes.Length)

        For index As Integer = 0 To comparisonLength - 1
            difference = difference Or (expectedBytes(index) Xor actualBytes(index))
        Next

        Return difference = 0
    End Function
End Class
