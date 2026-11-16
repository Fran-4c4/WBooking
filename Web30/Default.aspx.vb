Imports System.Data
Imports System.Diagnostics

Partial Class _Default
    Inherits cfr.pages.WBasePage


    Protected Sub Page_PreInit1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try
            System.Diagnostics.Debug.WriteLine("default_Page_PreInit")
            'Me.configRedirection()
        Catch ex As Exception
            Response.Redirect(cfr.NamePage.defaultpage)
        End Try

    End Sub

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Me.Page.IsPostBack Then
            'Me.siteWeb.masterPageCode = "CFRMasterPage.master"

        End If

    End Sub


    Protected Friend Sub configRedirection()

        Dim cod_hotel As String = ""


        'get hotel/property code
        cod_hotel = Request.QueryString(cfr.REQCte.query.cod_hotel)

        If Me.IsPostBack = False Then
            If cod_hotel <> "" Then
                'there is no info about hotel
                Server.TransferRequest("b_request.aspx")
            End If
        End If

    End Sub
   
End Class
