
Partial Class HDefault
    Inherits BookBasePage


    Protected Sub Page_PreInit1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try

            System.Diagnostics.Debug.WriteLine("hdefault_Page_PreInit")
            Dim requestURL As String = Request.Url.AbsoluteUri
            System.Diagnostics.Debug.WriteLine("requestURL: " & requestURL)
            Me.loadBaseData()
            Me.langCaption.holdproperty = Me.holdprop.Tipo
        Catch ex As Exception
            Response.Redirect("~/" & cfr.NamePage.defaultpage)
        End Try

    End Sub

    Protected Sub Page_Load1(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
End Class
