
Partial Class CFRMasterPage
    Inherits cfr.pages.SiteBaseMasterPage
    
    Public CFRMasterPage_myBasePage As cfr.pages.SiteBasePage

    Public idiom() As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.idiom = cfr.Language.getIdiomaMenuGenData(cfr.Language.getLanguage)


        If Not Page.IsPostBack Then
            'Me.lblMenuSup.Text = cfr.pages.UITemplate.writeScriptMenuSup(Me.getQueryNormal, cfr.Language.getLanguage)
            'Me.imglogoSite.ImageUrl = String.Format("template/topmenu_1x1_{0}.gif", Me.siteWeb.id)
        End If
    End Sub



    Public Overrides ReadOnly Property getLitMsgError() As System.Web.UI.WebControls.Literal
        Get
            Return Me.FindControl("litMsgErr")
        End Get
    End Property

    'Public Overrides ReadOnly Property getmasterPageTitleLiteral As Literal
    '    Get
    '        Return Me.FindControl("masterPageTitleLiteral")
    '    End Get
    'End Property
End Class

