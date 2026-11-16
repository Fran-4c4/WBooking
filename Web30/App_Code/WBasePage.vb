
Imports System.Globalization
Imports System.Linq
Imports Microsoft.VisualBasic
Imports cfr

Namespace cfr.pages
    ''' <summary>
    ''' base page for all pages in site
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WBasePage
        Inherits System.Web.UI.Page
        
#Region "Variables"
        ''' <summary>
        ''' type page
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum pageTypeEnum As Short
            ''' <summary>
            ''' simple page without masterpage(blank masterpage)
            ''' </summary>
            ''' <remarks></remarks>
            simple = 1
            ''' <summary>
            ''' page with masterpage
            ''' </summary>
            ''' <remarks></remarks>
            composed = 2
        End Enum

        ''' <summary>
        ''' page type, by default is composed
        ''' </summary>
        ''' <remarks></remarks>
        Public pageType As pageTypeEnum = pageTypeEnum.composed
        ''' <summary>
        ''' Indicates if an was encontered so we must cancel the page flow.
        ''' </summary>
        ''' <remarks></remarks>
        Public cancelFlow As Boolean = False

        Public siteWeb As cfr.SiteObj

        Public boe As New cfr.BookOperationErrors

        Public myQuery As String = ""
        ''' <summary>
        ''' This is the basic query needed to work. It must includes:
        ''' site id
        ''' language id
        ''' </summary>
        ''' <remarks></remarks>
        Public myQueryBase As String = ""

        ''' <summary>
        ''' must be set when the property is set to know the language set to use
        ''' </summary>
        ''' <remarks></remarks>
        Public langCaption As New cfr.languagePageManager

        Public Property siteCode_ViewState() As String
            Get
                Return CType(ViewState("siteCode"), String)
            End Get
            Set(ByVal Value As String)
                ViewState("siteCode") = Value
            End Set
        End Property

        Public Property languageID_ViewState() As String
            Get
                Return CType(ViewState("languageID"), String)
            End Get
            Set(ByVal Value As String)
                ViewState("languageID") = Value
            End Set
        End Property

        Public Property hotelID_ViewState() As String
            Get
                Return CType(ViewState("hotelID"), String)
            End Get
            Set(ByVal Value As String)
                ViewState("hotelID") = Value
            End Set
        End Property

#End Region

#Region "Query helpers"

        ''' <summary>
        ''' Get the querySting asociated with this page
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getQuery() As String
            Return GetQueryString(Page)
        End Function

        ''' <summary>
        ''' return query string as string and concat using ampersand
        ''' </summary>
        ''' <param name="sPage"></param>
        ''' <returns></returns>
        Public Shared Function GetQueryString(sPage As System.Web.UI.Page) As String
            Dim s As String = ""
            Dim sw As String = ""
            For x As Integer = 0 To sPage.Request.QueryString.Count - 1
                Dim keyValue As String = sPage.Request.QueryString.GetKey(x)
                s += sw & keyValue & "=" & sPage.Request.QueryString(x)
                sw = "&"
            Next
            Return s
        End Function

        ''' <summary>
        ''' Get the base querystring with basic parameters like:
        ''' cod_hotel,site,language,userType
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetQueryNormal() As String

            Dim hotelCode As String = cfr.tool.Tools.cNullStr(Page.Request.QueryString(cfr.REQCte.query.cod_hotel), "")
            Dim siteCode As String = cfr.tool.Tools.cNullStr(Page.Request.QueryString(cfr.REQCte.query.site), "")
            Dim languageCode As String = cfr.tool.Tools.cNullStr(Page.Request.QueryString(cfr.REQCte.query.language), "")
            Dim userTypeCode As String = cfr.tool.Tools.cNullStr(Page.Request.QueryString(cfr.REQCte.query.userType), "")

            Return cfr.pages.SiteBasePage.CreateQueryNormal(hotelCode, siteCode, languageCode, userTypeCode)
        End Function



        ''' <summary>
        ''' create basic query string with the data passed
        ''' </summary>
        ''' <param name="hotelCode"></param>
        ''' <param name="siteCode"></param>
        ''' <param name="languageCode"></param>
        ''' <param name="userTypeCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CreateQueryNormal(ByVal hotelCode As String, ByVal siteCode As String, ByVal languageCode As String, ByVal userTypeCode As String) As String
            Dim s As String = ""
            Dim sw As String = ""

            'hotelcode
            If hotelCode <> "" Then
                s += sw + cfr.REQCte.query.cod_hotel & "=" & hotelCode
                sw = "&"
            End If

            If siteCode <> "" Then
                s += sw + cfr.REQCte.query.site & "=" & siteCode
                sw = "&"
            End If

            If languageCode <> "" Then
                s += sw + cfr.REQCte.query.language & "=" & languageCode
                sw = "&"
            End If

            If userTypeCode <> "" Then
                s += sw + cfr.REQCte.query.userType & "=" & userTypeCode
                sw = "&"
            End If

            Return s
        End Function

        ''' <summary>
        ''' used to find the value of request.form(name) when it is name was changed by asp.net to container_name
        ''' </summary>
        ''' <param name="idStr"></param>
        ''' <param name="requestform"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetRequestValue(idStr As String, requestform As Specialized.NameValueCollection) As String

            Dim value As String = requestform.Cast(Of String)().Where(Function(key) key.EndsWith(idStr)).ToDictionary(Function(key) key, Function(key) requestform(key)).Values.FirstOrDefault()
            Return value
        End Function
#End Region

#Region "Messages "
        Public Sub showMsgOk(ByVal msg As String)
            Me.showMessage(msg, CFRConst.msgEnum.Ok)
        End Sub

        Public Sub showMsgInfo(ByVal msg As String)
            Me.showMessage(msg, CFRConst.msgEnum.info)
        End Sub

        Public Sub showMsgHelp(ByVal msg As String)
            Me.showMessage(msg, CFRConst.msgEnum.help)
        End Sub

        Public Sub showMsgErr(ByVal ex As Exception)
            Me.showMessage(ex.Message, CFRConst.msgEnum.errorType)
        End Sub

        Public Sub showMessage(ByVal msg As String, Optional ByVal msgType As cfr.CFRConst.msgEnum = CFRConst.msgEnum.errorType)
            SiteBasePage.showMessage(msg, Me, msgType)
        End Sub

        Public Sub showMessageClear()
            SiteBasePage.showMessage("", Me)
        End Sub



        ''' <summary>
        ''' Renders a user-facing message. Text is HTML-encoded by default.
        ''' </summary>
        ''' <param name="trustedHtml">
        ''' True only for application-owned constant markup. Request values,
        ''' database text and exception messages must always leave this False.
        ''' </param>
        Public Shared Sub showMessage(ByVal msg As String,
                                      ByVal pageContainer As Object,
                                      Optional ByVal msgType As cfr.CFRConst.msgEnum = CFRConst.msgEnum.errorType,
                                      Optional ByVal addMessage As Boolean = False,
                                      Optional ByVal trustedHtml As Boolean = False)
            Dim ctl As Literal = pageContainer.FindControl("litMsgErr")
            If ctl Is Nothing Then

                If Not pageContainer.Master Is Nothing Then
                    ctl = pageContainer.Master.FindControl("litMsgErr")
                    If ctl Is Nothing Then
                        Exit Sub
                    End If

                Else
                    Exit Sub
                End If

            End If
            If msg Is Nothing Or msg = "" Then
                ctl.Text = ""
                'ctl.Visible = False
                Exit Sub
            Else
                ctl.Visible = True
            End If
            Dim s As String = ""
            's += "<div id='pnlerror' Class='msgerr container'>"
            's += "<BR>"
            s += "<div class='container'>"
            Dim sParag As String = "<p class='{0}'>"
            Dim sParagClass As String = ""

            Dim sbuttonClose As String = "<button type='button' class='close' data-dismiss='alert'><span class='glyphicon glyphicon-remove'></span></button>"

            If msgType = CFRConst.msgEnum.errorType Then
                s += "<div id='pnlerror' role='alert' Class='alert alert-icon alert-danger alert-dismissible' >"
                s += sbuttonClose
                's += "<i class='fa fa-exclamation-circle fa-3x'></i>"
                sParagClass = "fail"
            ElseIf msgType = CFRConst.msgEnum.Ok Then
                s += "<div id='pnlerror' role='alert'  Class='alert alert-icon alert-success alert-dismissible'>"
                s += sbuttonClose
                's += "<i class='fa fa-check fa-3x'></i>"
                sParagClass = "ok"
            ElseIf msgType = CFRConst.msgEnum.info Then
                s += "<div id='pnlerror'  role='alert' Class='alert  alert-icon alert-info alert-dismissible'>"
                s += sbuttonClose
                's += "<i class='fa fa-info-circle fa-3x'></i>"
                sParagClass = "info"
            ElseIf msgType = CFRConst.msgEnum.warning Then
                s += "<div id='pnlerror' role='alert'  Class='alert alert-icon alert-warning alert-dismissible'>"
                s += sbuttonClose
                's += "<i class='fa fa-warning fa-3x'></i>"
                sParagClass = "fail"
            ElseIf msgType = CFRConst.msgEnum.help Then
                s += "<div id='pnlerror' role='alert'  Class='alert alert-icon alert-info alert-dismissible'>"
                s += sbuttonClose
                's += "<i class='fa fa-question fa-3x'></i>"
                sParagClass = "help"
            ElseIf msgType = CFRConst.msgEnum.delete Then
                s += "<div id='pnlerror' role='alert'  Class='alert alert-icon alert-info alert-dismissible'>"
                s += sbuttonClose
                s += "<i class='fa fa-trash fa-2x'></i>"
                sParagClass = "delete"
            End If

            s += String.Format(sParag, sParagClass)
            If trustedHtml Then
                s += msg
            Else
                s += HttpUtility.HtmlEncode(msg)
            End If
            s += "</p>"
            's += "<div  style='text-align:right;'><a class='closePan' href=""javascript:toggleLayer('pnlerror');"">" & cfr.Language.GetCaptionBy(Language.msgEnum.closeDiv) & "</a></div>"
            s += "</div>" 'alert 
            s += "</div>" 'container
            If addMessage = False Then
                ctl.Text = s
            Else
                ctl.Text += s
            End If

            ctl.Visible = True
        End Sub



#End Region

#Region "Error Control"
        ''' <summary>
        ''' get an array list with errors in session
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SesionErrorProp() As ArrayList
            Get
                Dim a As ArrayList
                Dim obj As Object = Me.Session(cfr.REQCte.session.SesionError)
                If TypeOf (obj) Is ArrayList Then
                    a = obj
                Else
                    a = Nothing
                End If

                If a Is Nothing Then
                    a = New ArrayList
                    Me.Session(cfr.REQCte.session.SesionError) = a
                End If
                Return Me.Session(cfr.REQCte.session.SesionError)
            End Get
            Set(ByVal value As ArrayList)
                Me.Session(cfr.REQCte.session.SesionError) = value
            End Set
        End Property


        Public Function controlSesionError(Optional cleanErrorSession As Boolean = False) As String
            Dim ae As ArrayList = Me.SesionErrorProp
            Dim msg As String = ""
            If ae.Count = 0 Then
                Return ""
            End If

            If ae.Count > 1 Then
                msg += "<h2>" & cfr.Language.GetCaption(359) & "</h2>"
                msg += "<ul>"
                For Each boe As cfr.BookOperationErrors In ae
                    msg += "<li>" & Me.controlError(boe) & "</li>"
                Next
                msg += "</ul>"
            ElseIf ae.Count = 1 Then
                msg = Me.controlError(ae(0))
            End If
            ae.Clear()
            If cleanErrorSession = True Then
                Me.SesionErrorProp = Nothing
            End If

            Return msg
        End Function

        ''' <summary>
        ''' Control the error throwed to show a more friendly error message .
        ''' </summary>
        ''' <param name="boe"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function controlError(ByRef boe As cfr.BookOperationErrors) As String
            Dim msgErr As String = ""
            If boe.errBook <> CStr(basERR.S_OK) Then
                Select Case boe.errBook
                    Case basERR.E_No_Hay_Disponibilidad '20 No hay disponibilidad
                        msgErr = cfr.Language.GetCaptionBy(Language.msgEnum.NoHayDisponibilidad)
                        ''Enlace HOTELES SUSTITUTOS
                        'If UCase(Left(vsite, 1)) = "C" And UCase(Left(vsite, 3)) <> "CRS" Then
                        'msgErr = msgErr & "<BR><div style=""text-align:left; "">" & cfr.language.getCaption(30) & "<BR>" & getHotelesSubstitutos(dr) & "</div>"
                        'End If

                    Case basERR.E_ExistCierreVentas 'Cierre de ventas, se ejecuta como no disponibilidad
                        'Enlace HOTELES SUSTITUTOS
                        'If UCase(Left(vsite, 1)) = "C" And UCase(Left(vsite, 3)) <> "CRS" Then
                        '    msgErr = cfr.language.getCaption(26) & "<BR><BR>" & getHotelesSubstitutos(dr)
                        'Else
                        msgErr = cfr.Language.GetCaptionBy(Language.msgEnum.E26_YMClosedDays)
                        'End If

                    Case 22
                        msgErr = cfr.Language.GetCaptionBy(Language.msgEnum.E_TariffNotConfiguratedForNotRegisteredClients)
                    Case basERR.E_TarifasPeriodos_NotExist '23
                        msgErr = cfr.Language.GetCaptionBy(Language.msgEnum.E_TariffNotConfiguratedForSelectedDays)
                    Case basERR.E_CheckFechaActual '24
                        msgErr = cfr.Language.GetCaption(24)
                    Case basERR.E_YM_DiasMinimos '25
                        'TODO:DIAS MINIMOS DE LA TARIFA
                        msgErr = String.Format(cfr.Language.GetCaptionBy(Language.msgEnum.E25_MinimumDays), boe.statusInfo)
                    Case 27
                        msgErr = cfr.Language.GetCaptionBy(Language.msgEnum.E27_InactiveSession)
                    Case basERR.E_OperadorNoExiste '21 y 29
                        msgErr = cfr.Language.GetCaption(259)
                    Case basERR.E_FAIL_VerifyDate '30
                        msgErr = cfr.Language.GetCaption(4)
                    Case basERR.E_FechaReservaMenorActual '
                        msgErr = cfr.Language.GetCaption(4)
                    Case basERR.E_FechaReservaFueraDeHora '
                        msgErr = cfr.Language.GetCaption(303)
                    Case basERR.E_PAGONOREALIZADO '
                        msgErr = cfr.Language.GetCaption(305)
                    Case basERR.E_PAGONOREALIZADOBYUSER '
                        msgErr = cfr.Language.GetCaption(305)
                    Case basERR.E_SessionExpired '
                        msgErr = cfr.Language.GetCaption(306)
                    Case basERR.E_GetOperadorbyCod_NoExiste '
                        msgErr = cfr.Language.GetCaption(310)
                    Case basERR.E_MaximumStay
                        msgErr = cfr.Language.GetCaption(309)
                    Case basERR.E_GetReservByLocalizador
                        msgErr = cfr.Language.GetCaptionBy(Language.msgEnum.E_GetReservByLocalizador)
                    Case Else
                        If boe.msg = String.Empty Then
                            msgErr = boe.errBook
                        Else
                            msgErr = boe.msg
                        End If

                End Select
            End If

            Return msgErr
        End Function


        ''' <summary>
        ''' Catch general exception and show on page
        ''' </summary>
        ''' <param name="ex"></param>
        ''' <remarks></remarks>
        Public Function catchExceptionControler(ByVal ex As Exception) As String
            Try
                boe.exceptionThrowed = ex
                Return Me.controlError(boe)

            Catch myEx As Exception
                'and now? Redirect to principal error page
                Return ""
            End Try
        End Function



#End Region
        
#Region "Shared methods"

#End Region

#Region "Shared methods"

#End Region
        ''' <summary>
        ''' configure culture based on user preferences
        ''' </summary>
        ''' <param name="language"></param>
        ''' <remarks></remarks>
        Public Sub configCulture(ByVal language As cfr.CFRConst.LanguageEnum)
            Select Case language
                Case cfr.CFRConst.LanguageEnum.es
                    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-ES")
                Case cfr.CFRConst.LanguageEnum.en
                    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
                Case cfr.CFRConst.LanguageEnum.de
                    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("de-DE")
                Case cfr.CFRConst.LanguageEnum.fr
                    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("fr-FR")
            End Select
        End Sub
        ''' <summary>
        ''' get user language based on culture of the system, not by selection of the user
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getUserLanguage() As String
            Dim languageSelected As cfr.CFRConst.LanguageEnum = CFRConst.LanguageEnum.en
            Dim cul As CultureInfo
            Try
                cul = CultureInfo.CreateSpecificCulture(Request.UserLanguages(0))
            Catch ex As Exception
                cul = CultureInfo.CreateSpecificCulture("en")
            End Try

            Select Case cul.Parent.ToString
                Case "en"
                    languageSelected = cfr.CFRConst.LanguageEnum.en
                Case "es", "cat"
                    languageSelected = cfr.CFRConst.LanguageEnum.es
                Case "br", "pt"
                    languageSelected = cfr.CFRConst.LanguageEnum.pt
                Case "fr"
                    languageSelected = cfr.CFRConst.LanguageEnum.fr
                Case "de"
                    languageSelected = cfr.CFRConst.LanguageEnum.de
                Case Else
                    languageSelected = cfr.CFRConst.LanguageEnum.en
            End Select

            System.Threading.Thread.CurrentThread.CurrentCulture = cul
            System.Threading.Thread.CurrentThread.CurrentUICulture = cul
            Return languageSelected
        End Function


#Region "Common methods"
        ''' <summary>
        ''' get all values from a vector in request
        ''' </summary>
        ''' <param name="mat"></param>
        ''' <param name="valueRef"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getReqValueFromVector(mat() As String, valueRef As String) As Boolean
            If mat Is Nothing Then
                Return False
            End If
            For x As Integer = 0 To mat.Length - 1
                If mat(x) = valueRef Then
                    Return True
                End If
            Next
            Return False

        End Function

        Public Function getRequestVector(reqName As String) As String()
            Dim mat(0) As String
            Dim obj As Object = Request.Form(reqName)
            If obj Is Nothing Then
                Return mat
            Else
                mat = Page.Request.Form.GetValues(reqName)
                Return mat
            End If



        End Function
#End Region



    End Class
End Namespace

