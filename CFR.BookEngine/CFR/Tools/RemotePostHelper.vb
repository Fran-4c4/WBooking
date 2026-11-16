Imports Microsoft.VisualBasic


Namespace cfr
    Public Class RemotePostHelper

        Private Inputs As System.Collections.Specialized.NameValueCollection = New System.Collections.Specialized.NameValueCollection()


        Public Url As String = ""
        Public Method As String = "post"
        Public FormName As String = "form1"

        Public Sub New(ByVal Url As String)
            Me.Url = Url
        End Sub

        Public Sub New(ByVal Url As String, ByVal FormName As String)
            Me.Url = Url
            Me.FormName = FormName
        End Sub

        Public Sub Add(ByVal name As String, ByVal value As String)
            Inputs.Add(name, value)
        End Sub

        Public Sub Post()
            If Not System.Text.RegularExpressions.Regex.IsMatch(FormName, "^[A-Za-z][A-Za-z0-9_]*$") Then
                Throw New InvalidOperationException("The remote-post form name is invalid.")
            End If
            Dim normalizedMethod As String = If(Method, String.Empty).Trim().ToLowerInvariant()
            If normalizedMethod <> "post" AndAlso normalizedMethod <> "get" Then
                Throw New InvalidOperationException("The remote-post method must be GET or POST.")
            End If

            Dim targetUri As Uri = Nothing
            If Not Uri.TryCreate(Url, UriKind.Absolute, targetUri) OrElse
               (targetUri.Scheme <> Uri.UriSchemeHttps AndAlso
                Not (targetUri.Scheme = Uri.UriSchemeHttp AndAlso targetUri.IsLoopback)) Then
                Throw New InvalidOperationException("The remote-post URL must use HTTPS, except for a loopback development target.")
            End If

            Dim encodedFormName As String = System.Web.HttpUtility.HtmlAttributeEncode(FormName)
            Dim encodedMethod As String = System.Web.HttpUtility.HtmlAttributeEncode(normalizedMethod)
            Dim encodedUrl As String = System.Web.HttpUtility.HtmlAttributeEncode(targetUri.AbsoluteUri)
            System.Web.HttpContext.Current.Response.Clear()

            System.Web.HttpContext.Current.Response.Write("<html><head>")

            System.Web.HttpContext.Current.Response.Write(String.Format("</head><body onload=""document.getElementById('{0}').submit()"">", encodedFormName))
            System.Web.HttpContext.Current.Response.Write(String.Format("<form id=""{0}"" name=""{0}"" method=""{1}"" action=""{2}"" >", encodedFormName, encodedMethod, encodedUrl))
            Dim i As Integer = 0
            Do While i < Inputs.Keys.Count
                Dim encodedName As String = System.Web.HttpUtility.HtmlAttributeEncode(Inputs.Keys(i))
                Dim encodedValue As String = System.Web.HttpUtility.HtmlAttributeEncode(Inputs(Inputs.Keys(i)))
                System.Web.HttpContext.Current.Response.Write(String.Format("<input name=""{0}"" type=""hidden"" value=""{1}"">", encodedName, encodedValue))
                i += 1
            Loop
            System.Web.HttpContext.Current.Response.Write("</form>")
            System.Web.HttpContext.Current.Response.Write("</body></html>")
            'System.Web.HttpContext.Current.Response.Flush()
            System.Web.HttpContext.Current.Response.End()

        End Sub
    End Class
End Namespace
