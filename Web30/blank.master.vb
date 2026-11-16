
Partial Class blank
    Inherits cfr.pages.SiteBaseMasterPage



    Public Overrides ReadOnly Property getLitMsgError() As System.Web.UI.WebControls.Literal
        Get
            Return Me.FindControl("litMsgErr")
        End Get
    End Property




End Class

