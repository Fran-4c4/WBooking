Imports System.Data
Imports System.Diagnostics

Partial Class Admin_ObjectBookParameters
    Inherits AdminBasePage
    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.bookConfiguration)

        Try
            PropertyUIshow.hideByType(Me.hotel.Tipo, Me)
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
        With Me.cbochildfree.Items
            .Add(New ListItem("Hasta 1 año", 1))
            .Add(New ListItem("Hasta 2 años", 2))
            .Add(New ListItem("Hasta 3 años", 3))
            .Add(New ListItem("Hasta 4 años", 4))
            .Add(New ListItem("Hasta 5 años", 5))
        End With
        Me.cbochildfree.SelectedValue = Me.hotel.BookParam.childAgeFree

        With Me.cbochildPay.Items
            .Add(New ListItem("Hasta 1 año", 1))
            For x As Integer = 2 To 16
                .Add(New ListItem("Hasta " & x & " años", x.ToString))
            Next
        End With


        Me.cbochildPay.SelectedValue = Me.hotel.BookParam.childAgeToPay

        'release
        With Me.cbodayinadvance.Items
            For x As Integer = 0 To 62
                .Add(New ListItem(x & " dias", x.ToString))
            Next
        End With
        Me.cbodayinadvance.SelectedValue = Me.hotel.BookParam.daysInAdvance

        'bank
        Me.txtiban.Text = Me.hotel.BookParam.CCCIBAN
        Me.txtncuenta.Text = Me.hotel.BookParam.CCC

        Me.chkonlyroom.Checked = Me.hotel.BookParam.regSA
        Me.chkboardbreakfast.Checked = Me.hotel.BookParam.regad
        Me.chkboardmp.Checked = Me.hotel.BookParam.regmp
        Me.chkboardpc.Checked = Me.hotel.BookParam.regpc
        Me.chkboardti.Checked = Me.hotel.BookParam.regTI

        Me.txtemail.Text = Me.hotel.BookParam.emailBooking
        Me.txtemailcopy.Text = Me.hotel.BookParam.emailBooking2

        Me.chkHasTpv.Checked = Me.hotel.BookParam.HasTPV

        Me.txtpriceboardbreakfast.Text = Me.hotel.BookParam.boardAD.ToString
        Me.txtpriceboarmp.Text = Me.hotel.BookParam.boardMP.ToString
        Me.txtpriceboarpc.Text = Me.hotel.BookParam.boardPC.ToString
        Me.txtpriceboarti.Text = Me.hotel.BookParam.boardTI.ToString

    End Sub

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If Page.IsValid = False Then
                Throw New Exception("La informarción suministrada no es correcta. Por favor compruebe los datos.")
            End If
            Dim hasError As Boolean = False
            If CInt(Me.cbochildPay.SelectedValue) <= CInt(Me.cbochildfree.SelectedValue) Then
                cfr.pages.SiteBasePage.showMessage("Compruebe que la edad de los niños de pago sea superior a la de los niños gratis.", Me.Page, CFRConst.msgEnum.errorType, True)
                hasError = True
            End If

            If validateBoard() = False Then
                cfr.pages.SiteBasePage.showMessage("Debe seleccionar al menos un regimen.", Me.Page, CFRConst.msgEnum.errorType, True)
                hasError = True
            End If

            hasError = validateEmail()

            If hasError = True Then
                Exit Sub
            End If

            Dim obj As New ind.bk.obj.R_Res_Hot_Param
            obj.cod_hotel = Me.codhotel
            obj.EdadMaxNinoGratis = CByte(Me.cbochildfree.SelectedValue)
            obj.EdadMaxNinoPago = CByte(Me.cbochildPay.SelectedValue)
            obj.diasdeAntelacion = CByte(Me.cbodayinadvance.SelectedValue)
            obj.emailConfirmacion = Me.txtemail.Text
            obj.emailConfirmacion2 = Me.txtemailcopy.Text
            obj.CCC = "" & Me.txtncuenta.Text.Trim
            obj.CCCIBAN = "" & Me.txtiban.Text.Trim
            'board
            obj.Alojamiento = Me.chkonlyroom.Checked
            obj.AD = Me.chkboardbreakfast.Checked
            obj.MP = Me.chkboardmp.Checked
            obj.PC = Me.chkboardpc.Checked
            obj.TodoIncluido = Me.chkboardti.Checked
            obj.HasTPV = Me.chkHasTpv.Checked

            obj.boardAD = Me.txtpriceboardbreakfast.Text
            obj.boardMP = Me.txtpriceboarmp.Text
            obj.boardPC = Me.txtpriceboarpc.Text
            obj.boardTI = Me.txtpriceboarti.Text

            Dim omgr As New ind.bk.obj.R_Res_Hot_Param_Manager
            omgr.UpdateConfigurationBasic(obj)
            'reload hotel configuration

            Me.hotel = cfr.mgr.HotelMgr.GetEntityFullRefreshCache(Me.codhotel)
            'Me.sessionCnt.hotel = Me.hotel

            cfr.pages.SiteBasePage.showMessage("La configuración se ha guardado correctamente.", Me.Page, CFRConst.msgEnum.Ok, True)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Function validateBoard() As Boolean

        If Me.chkonlyroom.Checked = True Then
            Return True
        End If
        If Me.chkboardbreakfast.Checked = True Then
            Return True
        End If
        If Me.chkboardmp.Checked = True Then
            Return True
        End If
        If Me.chkboardpc.Checked = True Then
            Return True
        End If
        If Me.chkboardti.Checked = True Then
            Return True
        End If

        Return False
    End Function

    Private Function validateEmail() As Boolean
        Dim email2Check As String = Me.txtemail.Text.Trim

        If email2Check = "" Then
            cfr.pages.SiteBasePage.showMessage("No ha introducido ningun correo de confirmación. El sistema no lo necesita para es recomendable.", Me.Page, CFRConst.msgEnum.warning, True)
            Return False
            Exit Function
        Else
            Try
                Dim emailcheckmgr As New System.Net.Mail.MailAddress(email2Check)
            Catch ex As Exception
                'wrong email
                cfr.pages.SiteBasePage.showMessage("El correo que ha introducido no es correcto. ", Me.Page, CFRConst.msgEnum.errorType, True)
                Return True
            End Try
        End If

        'check second email

        email2Check = Me.txtemailcopy.Text.Trim

        If email2Check = "" Then
            cfr.pages.SiteBasePage.showMessage("No ha introducido ningun correo de copia de confirmación.", Me.Page, CFRConst.msgEnum.warning, True)
            Return False
            Exit Function
        Else
            Try
                Dim emailcheckmgr As New System.Net.Mail.MailAddress(email2Check)
            Catch ex As Exception
                'wrong email
                cfr.pages.SiteBasePage.showMessage("El correo de copia que ha introducido no es correcto. ", Me.Page, CFRConst.msgEnum.errorType, True)
                Return True
            End Try
        End If

        Return Nothing
    End Function

End Class
