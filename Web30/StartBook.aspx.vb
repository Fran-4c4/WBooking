
Partial Class StartBook
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("b_request.aspx?hotelcode=280259&site=S1&language=" & cfr.CFRConst.LanguageEnum.es)
    End Sub
End Class
