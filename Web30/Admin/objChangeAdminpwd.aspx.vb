Imports System.Data

Partial Class Admin_objChangeAdminpwd
    Inherits AdminBasePage


    Public Property changePwdCount As Integer

        Get
            Return CInt(ViewState("changePwdCount"))
        End Get
        Set(value As Integer)
            ViewState("changePwdCount") = value
        End Set
    End Property

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim trans As SqlClient.SqlTransaction = Nothing
        Try
            Dim pwdActual As String = Me.RegisteredPwd.Text

            Dim currentPasswordMatches As Boolean
            If cfr.security.PasswordHasher.IsPasswordHash(Me.sessionCnt.user.clave) Then
                currentPasswordMatches = cfr.security.PasswordHasher.VerifyPassword(pwdActual, Me.sessionCnt.user.clave)
            Else
                currentPasswordMatches = String.Equals(pwdActual, Me.sessionCnt.user.clave, StringComparison.Ordinal)
            End If

            If Not currentPasswordMatches Then
                cfr.pages.SiteBasePage.showMessage("La contraseña actual no coincide. Tiene " & Me.changePwdCount & " intentos antes de que la aplicación le desconecte.", Me.Page, CFRConst.msgEnum.warning)
                Me.changePwdCount += 1
                If Me.changePwdCount = 3 Then
                    Me.CloseSession()
                End If
                Exit Sub
            End If

            Dim newLoginStr As String = Me.NewLogin.Text.Trim
            Dim newClaveStr As String = Me.NewPwd.Text


            If String.IsNullOrWhiteSpace(newLoginStr) OrElse String.IsNullOrEmpty(newClaveStr) Then
                cfr.pages.SiteBasePage.showMessage("Por favor, introduzca su usuario y clave.", Me.Page, CFRConst.msgEnum.warning)
                Exit Sub
            End If

            'If checkInjection(loginStr) = True Or checkInjection(claveStr) = True Then
            '    cfr.pages.SiteBasePage.showMessage("Por favor, introduzca un usuario y clave correctamente. No introduzca signos: ' ? ¿ = ", Me.Page, CFRConst.msgEnum.warning)
            '    Exit Sub
            'End If 'clave<>"" and login <>"" 


            Dim userData As New ind.bk.obj.R_Usuarios
            Dim currentData As DataTable = userData.getDataTableByID(Me.sessionCnt.user.id_usuario)
            If currentData.Rows.Count <> 1 Then
                Throw New InvalidOperationException("No se ha encontrado el usuario autenticado.")
            End If

            ind.bk.obj.R_Usuarios.FillObjectByRow(currentData.Rows(0), userData)
            userData.login = newLoginStr
            userData.clave = cfr.security.PasswordHasher.HashPassword(newClaveStr)
            userData.Update(userData)

            Me.sessionCnt.user.clave = userData.clave
            Me.sessionCnt.user.login = newLoginStr


            cfr.pages.SiteBasePage.showMessage("Su usuario y contraseña han sido modificados.", Me.Page, CFRConst.msgEnum.warning)
            Exit Sub


        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Protected Sub Page_Load1(sender As Object, e As EventArgs) Handles Me.Load
        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.modifyhotelpwdadmin)

    End Sub
End Class
