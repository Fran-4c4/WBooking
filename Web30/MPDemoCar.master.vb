Partial Class MPDemoCar
    Inherits cfr.pages.SiteBaseMasterPage

    Public Overrides ReadOnly Property getLitMsgError() As System.Web.UI.WebControls.Literal
        Get
            Return DirectCast(Me.FindControl("litMsgErr"), System.Web.UI.WebControls.Literal)
        End Get
    End Property
End Class
