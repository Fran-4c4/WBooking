Imports System.Data

Partial Class policyCancel
    Inherits AdminBasePage
    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.bookConfiguration)

        Try
            Me.showMessageClear()
            If Not Page.IsPostBack Then
                Me.configCbos()
            Else

            End If

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Sub configCbos()

        Me.txt_policy_es.Text = cfr.security.RichHtmlSanitizer.Sanitize(Me.hotel.BookParam.txtCancelEsp)
        Me.txt_policy_en.Text = cfr.security.RichHtmlSanitizer.Sanitize(Me.hotel.BookParam.txtCancelIngles)
        Me.txt_policy_de.Text = cfr.security.RichHtmlSanitizer.Sanitize(Me.hotel.BookParam.txtCancelAleman)
        Me.txt_policy_fr.Text = cfr.security.RichHtmlSanitizer.Sanitize(Me.hotel.BookParam.txtCancelFrances)

        Me.txt_bookconditions_es.Text = cfr.security.RichHtmlSanitizer.Sanitize(Me.hotel.BookParam.polEsp)
        Me.txt_bookconditions_en.Text = cfr.security.RichHtmlSanitizer.Sanitize(Me.hotel.BookParam.polIngles)
        Me.txt_bookconditions_de.Text = cfr.security.RichHtmlSanitizer.Sanitize(Me.hotel.BookParam.polAleman)
        Me.txt_bookconditions_fr.Text = cfr.security.RichHtmlSanitizer.Sanitize(Me.hotel.BookParam.polFrances)

        Me.txt_bookconfirmation_es.Text = cfr.security.RichHtmlSanitizer.Sanitize(Me.hotel.BookParam.txtConfirmacion)
        Me.txt_bookconfirmation_en.Text = cfr.security.RichHtmlSanitizer.Sanitize(Me.hotel.BookParam.txtConfirmacionIngles)
        Me.txt_bookconfirmation_de.Text = cfr.security.RichHtmlSanitizer.Sanitize(Me.hotel.BookParam.txtConfirmacionAleman)
        Me.txt_bookconfirmation_fr.Text = cfr.security.RichHtmlSanitizer.Sanitize(Me.hotel.BookParam.txtConfirmacionFrances)
        

    End Sub

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            Dim hasError As Boolean = False


            If hasError = True Then
                Exit Sub
            End If

            Dim obj As New ind.bk.obj.R_Res_Hot_Param

            Dim omgr As New ind.bk.obj.R_Res_Hot_Param_Manager
            obj = cfr.mgr.HotelBookParameters.GetEntity(Me.codhotel)

        obj.txtCancelEsp = cfr.security.RichHtmlSanitizer.Sanitize(Me.txt_policy_es.Text)
        obj.txtCancelIngles = cfr.security.RichHtmlSanitizer.Sanitize(Me.txt_policy_en.Text)
        obj.txtCancelAleman = cfr.security.RichHtmlSanitizer.Sanitize(Me.txt_policy_de.Text)
        obj.txtCancelFrances = cfr.security.RichHtmlSanitizer.Sanitize(Me.txt_policy_fr.Text)

        obj.polEsp = cfr.security.RichHtmlSanitizer.Sanitize(Me.txt_bookconditions_es.Text)
        obj.polIngles = cfr.security.RichHtmlSanitizer.Sanitize(Me.txt_bookconditions_en.Text)
        obj.polAleman = cfr.security.RichHtmlSanitizer.Sanitize(Me.txt_bookconditions_de.Text)
        obj.polFrances = cfr.security.RichHtmlSanitizer.Sanitize(Me.txt_bookconditions_fr.Text)

        obj.txtConfirmacion = cfr.security.RichHtmlSanitizer.Sanitize(Me.txt_bookconfirmation_es.Text)
        obj.txtConfirmacionIngles = cfr.security.RichHtmlSanitizer.Sanitize(Me.txt_bookconfirmation_en.Text)
        obj.txtConfirmacionAleman = cfr.security.RichHtmlSanitizer.Sanitize(Me.txt_bookconfirmation_de.Text)
        obj.txtConfirmacionFrances = cfr.security.RichHtmlSanitizer.Sanitize(Me.txt_bookconfirmation_fr.Text)


            omgr.Update(obj)
            'reload hotel configuration

            Me.hotel = cfr.mgr.HotelMgr.GetEntityFullRefreshCache(Me.codhotel)
            'Me.sessionCnt.hotel = Me.hotel

            cfr.pages.SiteBasePage.showMessage("La configuración se ha guardado correctamente.", Me.Page, CFRConst.msgEnum.Ok, True)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub




End Class
