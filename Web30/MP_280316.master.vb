
Imports System.Linq

Partial Class blank
    Inherits cfr.pages.SiteBaseMasterPage

    'Public Property holdProperty As cfr.

    Public urlLangSelected As String '= "b_request.aspx?hotelCode=" & Me.holdProperty.id & "&site=" & Me.siteWeb.id & "&language={0}"


    Public Overrides ReadOnly Property getLitMsgError() As System.Web.UI.WebControls.Literal
        Get
            Return Me.FindControl("litMsgErr")
        End Get
    End Property





    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Me.configCancelLink()

            Else

            End If

            Me.configLanLink()
        Catch ex As Exception
            'Me.showMessage(msgErr)
        End Try


    End Sub

    Private Sub configCancelLink()
        Dim lnk As Literal = Me.FindControl("lnkMasterCancelBook")
        If lnk Is Nothing Or Me.bookData Is Nothing Then
            Exit Sub
        End If
        Dim urlCancel As String = String.Format("javascript:cancelBook('b_cancel.aspx?{0}')", Me.myBasePage.myQuery)
        Dim link As String = String.Format("<a href=""{0}"">{1}</a>", urlCancel, Me.langCaption.GetCaption(353))
        Me.lnkMasterCancelBook.Text = link
    End Sub

    Private Sub configLanLink()
        'sample
        '"b_request.aspx?hotelCode=" & Me.holdProperty.id & "&site=" & Me.siteWeb.id & "&language={0}"
        If Me.bookData Is Nothing Then
            urlLangSelected = Me.Request.Url.AbsolutePath & "?&language={0}"
        Else
            Dim query As String = cfr.pages.WBasePage.CreateQueryNormal(Me.bookData.hotel.id, Me.bookData.site.id, "", "")

            urlLangSelected = Request.Url.Segments.Last & "?" & query & "&language={0}"
        End If
    End Sub


End Class

