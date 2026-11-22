
Imports System.Linq

Partial Class Admin_Login
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Maximum lifetime, in days, of an explicitly remembered administrator session.
    ''' </summary>
    ''' <remarks>
    ''' The database logon timestamp and persistent cookie use the same lifetime.
    ''' Changing it affects validation of every existing remembered session.
    ''' </remarks>
    Private Const PersistentSessionDays As Integer = 60
    Private Const LoginIpRateLimitScope As String = "admin-login-ip"
    Private Const LoginAccountRateLimitScope As String = "admin-login-account"
    Private Const MaximumLoginFailures As Integer = 5
    Private Shared ReadOnly LoginFailureWindow As TimeSpan = TimeSpan.FromMinutes(15)
    Private Const LoginFailureMessage As String = "No se ha podido iniciar sesión con los datos proporcionados. Inténtelo de nuevo más tarde."

    Protected Overrides Sub OnPreInit(ByVal e As EventArgs)
        If Me.Context IsNot Nothing AndAlso Me.Context.Session IsNot Nothing Then
            Me.Page.ViewStateUserKey = Me.Context.Session.SessionID
        End If
        MyBase.OnPreInit(e)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Me.Page.IsPostBack = False Then
                Dim closesession As String = Request.QueryString("logout")
                If Not closesession Is Nothing Then
                    If closesession.ToUpper.Trim = "TRUE" Then
                        Me.logoutByUser()
                        ExpireCookie("UserSessionKey")
                    End If
                Else
                    If Not Request.Cookies("UserName") Is Nothing Then
                        RegisteredLogin.Text = Request.Cookies("UserName").Value
                        chkRememberMe.Checked = True
                    Else
                        chkRememberMe.Checked = False
                    End If

                    If String.Equals(Request.QueryString("resume"), "1", StringComparison.Ordinal) Then
                        validateSessionKey()
                    End If
                End If
                'Remove the obsolete plaintext-password cookie if an old
                'installation or browser still has one.
                ExpireCookie("Password")
            Else
                validateSessionKey()
            End If
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' logout by user. userkey is deleted from table of sessions
    ''' </summary>
    Private Sub logoutByUser()
        If Not Request.Cookies("UserSessionKey") Is Nothing Then
            Dim usersessionkey As String = Request.Cookies("UserSessionKey").Value
            If Not String.IsNullOrEmpty(usersessionkey) Then
                Dim storedFingerprint As String = cfr.security.PersistentSessionToken.FingerprintForStorage(usersessionkey)
                Using ctx As New cfrbookEntities
                    Dim sessionRecord As R_UsuariosSession =
                        (From candidate In ctx.R_UsuariosSession
                         Where candidate.sessionkey = storedFingerprint
                         Select candidate).FirstOrDefault()
                    If sessionRecord IsNot Nothing Then
                        sessionRecord.logoff = DateTime.Now
                        ctx.SaveChanges()
                    End If
                End Using
            End If
        End If
        ExpireCookie("UserSessionKey")
        Me.CloseSession()
    End Sub

    Private Function validateSessionKey()
        Dim userHotelData As cfr.mgr.UserHotelMgr

        If Not Request.Cookies("UserSessionKey") Is Nothing Then
            Dim usersessionkey As String = Request.Cookies("UserSessionKey").Value
            If usersessionkey <> "" Then
                Dim storedFingerprint As String = cfr.security.PersistentSessionToken.FingerprintForStorage(usersessionkey)
                'find userid and load data if ok redirect, else show this page and 
                Using ctx As New cfrbookEntities
                    Dim oldestAllowedLogon As DateTime = DateTime.Now.AddDays(-PersistentSessionDays)
                    Dim u As R_UsuariosSession =
                        (From s In ctx.R_UsuariosSession
                         Where s.sessionkey = storedFingerprint AndAlso
                               Not s.logoff.HasValue AndAlso
                               s.logon >= oldestAllowedLogon
                         Select s).FirstOrDefault()
                    If u Is Nothing Then
                        Return Nothing
                    Else
                        'get user data
                        userHotelData = cfr.mgr.UserHotelMgr.GetUserByID(u.id_usuario)
                        userHotelData.usersessionkey = usersessionkey
                        loadUserData(userHotelData)
                    End If

                End Using
            End If
        End If
        Return Nothing
    End Function


    Public Sub CloseSession()
        System.Diagnostics.Debug.WriteLine("Session clossing on closesession method: logUserLogout")
        logUserLogout()
        System.Diagnostics.Debug.WriteLine("Session clossing on closesession method: Session.Clear & Session.Abandon()")
        Session.Clear()
        Session.Abandon()
        FormsAuthentication.SignOut()
    End Sub

    Private Sub logUserLogout()
        Try
            Dim AdminSession As cfr.admin.AdminSessionObj = Session(REQCte.session.hotelAdmin)
            If AdminSession Is Nothing Then
                Exit Sub
            End If

            'querystring
            Dim sessionlinkObj As Object = Request.QueryString(cfr.REQCte.query.sessionlink)
            If TypeOf sessionlinkObj Is String Then
                Dim sessionlink As String = DirectCast(sessionlinkObj, String)
                If sessionlink <> "" Then
                    Dim sessionCnt As cfr.admin.sessionContainer = AdminSession.getEntityByID(sessionlink)
                    Dim userhotel As cfr.mgr.UserHotelMgr = sessionCnt.user
                    cfr.mgr.UserHotelMgr.userLog(userhotel.userlogaccess, False)

                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim claveStr As String, loginStr As String
            loginStr = Me.RegisteredLogin.Text.Trim
            claveStr = Me.RegisteredPwd.Text
            Dim ipAddress As String = cfr.tool.Tools.GetIPAddress

            If String.IsNullOrEmpty(claveStr) OrElse String.IsNullOrEmpty(loginStr) OrElse
               loginStr.Length > 256 OrElse claveStr.Length > 256 Then
                RegisterLoginFailure(ipAddress, loginStr)
                ShowLoginFailure()
                Exit Sub
            End If

            If RequestRateLimiter.IsBlocked(LoginIpRateLimitScope, ipAddress, MaximumLoginFailures, LoginFailureWindow) OrElse
               RequestRateLimiter.IsBlocked(LoginAccountRateLimitScope, loginStr, MaximumLoginFailures, LoginFailureWindow) Then
                ShowLoginFailure()
                Exit Sub
            End If

            Dim userHotelData As cfr.mgr.UserHotelMgr = cfr.mgr.UserHotelMgr.GetUserByCredentials(loginStr, claveStr, ipAccess:=ipAddress)
            If userHotelData Is Nothing Then
                RegisterLoginFailure(ipAddress, loginStr)
                ShowLoginFailure()
                Exit Sub
            End If

            RequestRateLimiter.Reset(LoginIpRateLimitScope, ipAddress)
            RequestRateLimiter.Reset(LoginAccountRateLimitScope, loginStr)

            If chkRememberMe.Checked Then
                WriteCookie("UserName", loginStr, DateTime.Now.AddDays(PersistentSessionDays))
                WriteCookie("UserSessionKey", userHotelData.usersessionkey, DateTime.Now.AddDays(PersistentSessionDays))
            Else
                ExpireCookie("UserName")
                WriteSessionCookie("UserSessionKey", userHotelData.usersessionkey)
            End If

            'Do not copy authenticated state into the anonymous session. The
            'next request receives a new ASP.NET session and resolves the
            'short-lived or remembered token there.
            Session.Clear()
            Session.Abandon()
            ExpireAspNetSessionCookie()
            Response.Redirect("Login.aspx?resume=1", False)
            Context.ApplicationInstance.CompleteRequest()
            Return

            'Dim sessionContain As New cfr.admin.sessionContainer(userHotelData, cfr.mgr.HotelMgr.GetEntityFull(userHotelData.cod_hotel))
            'FormsAuthentication.RedirectFromLoginPage(loginStr, True)

            'Dim ticket As FormsAuthenticationTicket = New FormsAuthenticationTicket(1, _
            '  userHotelData.id_usuario.ToString, _
            '  DateTime.Now, _
            '  DateTime.Now.AddMinutes(30), _
            '   True, _
            '   sessionContain.sessionlink, _
            '  FormsAuthentication.FormsCookiePath)

            '' Encrypt the ticket.
            'Dim encTicket As String = FormsAuthentication.Encrypt(ticket)

            '' Create the cookie.
            'Response.Cookies.Add(New HttpCookie(FormsAuthentication.FormsCookieName, encTicket))

            ' Redirect back to original URL.

        Catch ex As Exception
            cfr.tool.LogMgr.log(ex)
            cfr.pages.SiteBasePage.showMessage("Lo sentimos, se ha producido un error al iniciar sesión.", Me.Page, CFRConst.msgEnum.warning)
        End Try
    End Sub

    Private Sub RegisterLoginFailure(ByVal ipAddress As String, ByVal login As String)
        RequestRateLimiter.RecordFailure(LoginIpRateLimitScope, ipAddress, MaximumLoginFailures, LoginFailureWindow)
        RequestRateLimiter.RecordFailure(LoginAccountRateLimitScope, login, MaximumLoginFailures, LoginFailureWindow)
    End Sub

    Private Sub ShowLoginFailure()
        cfr.pages.SiteBasePage.showMessage(LoginFailureMessage, Me.Page, CFRConst.msgEnum.warning)
    End Sub


    Private Sub loadUserData(userHotelData As cfr.mgr.UserHotelMgr)

        If userHotelData Is Nothing Then
            Exit Sub
        End If

        Dim sessionCtainer As New cfr.admin.sessionContainer(userHotelData, cfr.mgr.HotelMgr.GetEntityFull(userHotelData.cod_hotel))

        Dim AdminSession As cfr.admin.AdminSessionObj = AdminBasePage.getAdminSession(Me.Context)
        If AdminSession Is Nothing Then
            'new session
            AdminSession = New cfr.admin.AdminSessionObj
            AdminBasePage.setAdminSession(Me.Context, AdminSession)
        End If

        AdminSession.addSessionContainer(sessionCtainer)
        ViewState("hotelID") = userHotelData.cod_hotel

        Try
            Dim urlCodHotelKey As String = String.Format("{0}={1}", cfr.REQCte.query.cod_hotel, userHotelData.cod_hotel)
            Dim urlUserIDKey As String = String.Format("{0}={1}", cfr.REQCte.query.userIDAdmin, userHotelData.id_usuario)
            Dim urlSessionkey As String = String.Format("{0}={1}", cfr.REQCte.query.sessionlink, sessionCtainer.sessionlink)

            Dim urlDefault As String = AdminBasePage.LinkConfig(sessionCtainer, "default.aspx") ' String.Format("rep_book.aspx?{0}&{1}&{2}", urlCodHotelKey, urlUserIDKey, urlSessionkey)
            Dim urlreturn As String = String.Empty
            If urlreturn = "" Then
                Response.Redirect(urlDefault, True)
            Else
                urlreturn = String.Format(urlreturn & "?{0}", urlSessionkey)
                Response.Redirect(urlreturn)
            End If

        Catch exTh As Threading.ThreadAbortException
            'nothing all is ok due to redirection
        End Try

    End Sub


    Public Function checkInjection(ByVal cWord As String) As Boolean
        If cWord.Trim = "" Then
            Return False
        End If

        If InStr(1, cWord, "'") > 0 Then
            Return True
        End If


        Return Nothing
    End Function

    Private Sub WriteCookie(ByVal name As String, ByVal value As String, ByVal expiresAt As DateTime)
        Dim cookie As New HttpCookie(name, value)
        cookie.Expires = expiresAt
        ConfigureCookie(cookie)
        Response.Cookies.Set(cookie)
    End Sub

    Private Sub WriteSessionCookie(ByVal name As String, ByVal value As String)
        Dim cookie As New HttpCookie(name, value)
        ConfigureCookie(cookie)
        Response.Cookies.Set(cookie)
    End Sub

    Private Sub ConfigureCookie(ByVal cookie As HttpCookie)
        cookie.HttpOnly = True
        cookie.Secure = Request.IsSecureConnection
        cookie.SameSite = SameSiteMode.Lax
        cookie.Path = FormsAuthentication.FormsCookiePath
    End Sub

    Private Sub ExpireCookie(ByVal name As String)
        WriteCookie(name, "", DateTime.UtcNow.AddDays(-1))
    End Sub

    Private Sub ExpireAspNetSessionCookie()
        Dim sessionConfiguration As Global.System.Web.Configuration.SessionStateSection =
            DirectCast(Global.System.Web.Configuration.WebConfigurationManager.GetSection("system.web/sessionState"),
                       Global.System.Web.Configuration.SessionStateSection)
        Dim cookieName As String = sessionConfiguration.CookieName
        If String.IsNullOrEmpty(cookieName) Then
            cookieName = "ASP.NET_SessionId"
        End If

        Dim cookie As New HttpCookie(cookieName, "")
        cookie.Expires = DateTime.UtcNow.AddDays(-1)
        cookie.HttpOnly = True
        cookie.Secure = Request.IsSecureConnection
        cookie.SameSite = SameSiteMode.Lax
        cookie.Path = "/"
        Response.Cookies.Set(cookie)
    End Sub
End Class
