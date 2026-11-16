Imports System.Diagnostics
Imports cfr.obj
Imports ind.bk.obj
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic

Public Class AdminBasePage
    Inherits cfr.pages.WBasePage

    Private Const PersistentSessionDays As Integer = 60

    Public sessionlink As String
    Public AdminSession As cfr.admin.AdminSessionObj
    Public sessionCnt As cfr.admin.sessionContainer

    ''' <summary>
    ''' Anti-forgery token rendered by administrator pages that call ASHX handlers.
    ''' </summary>
    Public ReadOnly Property AntiForgeryToken As String
        Get
            Return AdminRequestSecurity.GetOrCreateToken(Me.Context)
        End Get
    End Property

    Protected Overrides Sub OnPreInit(ByVal e As EventArgs)
        If Me.Context IsNot Nothing AndAlso Me.Context.Session IsNot Nothing Then
            'Bind ViewState MACs to this browser session so a ViewState captured
            'from another session cannot be replayed for an administrator postback.
            Me.Page.ViewStateUserKey = Me.Context.Session.SessionID
        End If
        MyBase.OnPreInit(e)
    End Sub
    ''' <summary>
    ''' hotel object
    ''' </summary>
    ''' <remarks></remarks>
    Public Property hotel As cfr.obj.Hotel
        Get
            Return Me.sessionCnt.hotel
        End Get
        Set(value As cfr.obj.Hotel)
            Me.sessionCnt.hotel = value
        End Set
    End Property


    Public Sub TitleMasterSet(title As String)

    End Sub

#Region "Fields fo pages"

    Public unitname As String = "Habitación"
    Public unitnameplural As String = "Habitaciones"
    Public unitnameDesc As String = "Habitaciones, chalets, villas, etc."

    Public Sub configNameFieldsBase()
        Select Case Me.hotel.Tipo
            Case 2
                Me.unitname = "coche"
                Me.unitnameplural = "coches"
                Me.unitnameDesc = "Coches, motos, etc"

        End Select
    End Sub
#End Region




    Public codhotelVar As Integer

    Public Property codhotel As Integer
        Get
            Return Me.codhotelVar
        End Get
        Set(value As Integer)
            Me.codhotelVar = value
            Me.HotelIDVS = value
        End Set
    End Property



    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            cfr.pages.SiteBasePage.showMessage("", Me.Page) 'clean messages

            AdminSession = checkSessionByRequest(Me.Context)
            If AdminSession Is Nothing Then
                Try
                    CloseSession()
                    Exit Sub
                Catch ex As Exception
                End Try
            End If

            Me.configNameFieldsBase()

            If Me.Page.IsPostBack Then

            End If

            Me.checkErrors()

        Catch exth As Threading.ThreadAbortException
            Debug.WriteLine("Close session in ThreadAbortException from Page_Load in adminbasepage")

        Catch ex As Exception

            Debug.WriteLine("Exception unhandled in Page_Load in adminbasepage")
        End Try


    End Sub

    Public Function checkSessionByRequest(context As HttpContext) As admin.AdminSessionObj

        AdminSession = getAdminSession()
        If AdminSession Is Nothing Then
            'validate with sessionkey cookie
            validateSessionKey()
        End If
        'check again
        If AdminSession Is Nothing Then
            Return Nothing
        End If

        Me.validateUserAccess(context.Request)

        Return AdminSession

    End Function



    Public Sub CloseSession()
        Try
            Dim cookie As HttpCookie = Request.Cookies("UserSessionKey")
            If cookie IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cookie.Value) Then
                Dim storedFingerprint As String = cfr.security.PersistentSessionToken.FingerprintForStorage(cookie.Value)
                Using ctx As New cfrbookEntities
                    Dim sessionRecord As R_UsuariosSession =
                        (From candidate In ctx.R_UsuariosSession
                         Where candidate.sessionkey = storedFingerprint AndAlso Not candidate.logoff.HasValue
                         Select candidate).FirstOrDefault()
                    If sessionRecord IsNot Nothing Then
                        sessionRecord.logoff = DateTime.Now
                        ctx.SaveChanges()
                    End If
                End Using
            End If
        Catch ex As Exception
            cfr.tool.LogMgr.log(ex)
        End Try

        If Session.Count > 0 Then
            Session.Clear()
            Session.Abandon()
            FormsAuthentication.SignOut()
        End If

        Dim expiredSessionCookie As New HttpCookie("UserSessionKey", "")
        expiredSessionCookie.Expires = DateTime.UtcNow.AddDays(-1)
        expiredSessionCookie.HttpOnly = True
        expiredSessionCookie.Secure = Request.IsSecureConnection
        expiredSessionCookie.SameSite = SameSiteMode.Lax
        expiredSessionCookie.Path = FormsAuthentication.FormsCookiePath
        Response.Cookies.Set(expiredSessionCookie)

        Try
            Response.Redirect("login.aspx")
        Catch ex As Threading.ThreadAbortException
            Debug.WriteLine("CloseSession redirect")
        End Try

    End Sub

    Public Property HotelIDVS As Integer
        Get
            Return CInt(ViewState("hotelID"))
        End Get
        Set(value As Integer)
            ViewState("hotelID") = value
        End Set
    End Property

    ''' <summary>
    ''' when possible we use ID of the object for actions in page
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property objectIDVS As String

        Get
            Return CInt(ViewState("adminobjectIDforoperation"))
        End Get
        Set(value As String)
            ViewState("adminobjectIDforoperation") = value
        End Set
    End Property

#Region "Validation of user and sections"
    ''' <summary>
    ''' Validate session of the user and access to selected hotel
    ''' </summary>
    ''' <param name="context_request"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function validateUserAccess(context_request As System.Web.HttpRequest) As Boolean
        'Private Function validateUserAccess(req As System.Web.HttpRequest) As Boolean
        Dim sessionlinkObj As Object
        'Check in order
        'viewstate
        'sessionlink = HotelIDVS
        'If sessionlink <> 0 Then
        '    Return True
        'End If
        'POST
        sessionlinkObj = context_request(cfr.REQCte.query.sessionlink)
        If TypeOf sessionlinkObj Is String Then
            sessionlink = DirectCast(sessionlinkObj, String)
            If sessionlink <> "" Then
                Me.sessionCnt = Me.AdminSession.getEntityByID(sessionlink)
                Me.codhotel = Me.sessionCnt.hotel.cod_Hotel
                Me.hotel = Me.sessionCnt.hotel
                Return True
            End If
        End If


        'querystring
        sessionlinkObj = context_request.QueryString(cfr.REQCte.query.sessionlink)
        If TypeOf sessionlinkObj Is String Then
            sessionlink = DirectCast(sessionlinkObj, String)
            If sessionlink <> "" Then
                Me.sessionCnt = Me.AdminSession.getEntityByID(sessionlink)
                Me.codhotel = Me.sessionCnt.hotel.cod_Hotel
                Me.hotel = Me.sessionCnt.hotel
                Return True
            End If
        End If


        If sessionlink = "" Then
            CloseSession()
        End If

        Return False
    End Function

    Public Function validateUserAccessToSection(permisionRequired As cfr.web.enum.adminWebPageEnum) As Boolean
        Return validateUserAccessToSectionByContext(Me.Context, permisionRequired)
    End Function

    Public Function validateUserAccessToSectionByContext(context As System.Web.HttpContext, permisionRequired As cfr.web.enum.adminWebPageEnum) As Boolean
        Dim res As Boolean = UserAccessToSectioIsAllowed(permisionRequired)
        If res = False Then
            NoAccessAllowed(context)
            context.Response.Redirect(context.Request.UrlReferrer.AbsoluteUri)
            Return False
        End If

        Return True
    End Function

    Public Function UserAccessToSectioIsAllowed(permisionRequired As cfr.web.enum.adminWebPageEnum) As Boolean
        Return cfr.admin.AdminPermissions.UserAccessToSectioIsAllowed(Me.sessionCnt.user, permisionRequired)
    End Function
    ''' <summary>
    ''' called from load page, if there is an error the page show the message and delete it
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkErrors()
        Dim sesErr As Object = Session(REQCte.session.SesionError)
        If Not sesErr Is Nothing Then
            Dim errmsg As String
            If sesErr.GetType Is GetType(ArrayList) Then
                If sesErr.count = 0 Then
                    Session(REQCte.session.SesionError) = ""
                    Exit Sub
                Else
                    errmsg = sesErr(0)
                End If
            Else

                errmsg = Session(REQCte.session.SesionError)

            End If

            cfr.pages.SiteBasePage.showMessage(Session(REQCte.session.SesionError), Me.Page, CFRConst.msgEnum.errorType)
            Session(REQCte.session.SesionError) = ""
        Else

        End If
    End Sub

    Private Shared Sub NoAccessAllowed(context As System.Web.HttpContext, Optional clean As Boolean = False)
        If clean = False Then
            context.Session(REQCte.session.SesionError) = "No tiene permisos de acceso a la seccion solicitada."
        Else
            context.Session(REQCte.session.SesionError) = ""
        End If

    End Sub

#End Region

#Region "Control cookie state"
    Private Sub validateSessionKey()
        validateSessionKeyByContext(Me.Context)
    End Sub

    Private Sub validateSessionKeyByContext(ctxReq As HttpContext)
        Dim userHotelData As cfr.mgr.UserHotelMgr

        If Not ctxReq.Request.Cookies("UserSessionKey") Is Nothing Then
            Dim usersessionkey As String = ctxReq.Request.Cookies("UserSessionKey").Value
            If usersessionkey <> "" Then
                Dim storedFingerprint As String = cfr.security.PersistentSessionToken.FingerprintForStorage(usersessionkey)
                'find userid and load data if ok  

                Using ctx As New cfrbookEntities
                    Dim oldestAllowedLogon As DateTime = DateTime.Now.AddDays(-PersistentSessionDays)
                    Dim u As R_UsuariosSession =
                        (From s In ctx.R_UsuariosSession
                         Where s.sessionkey = storedFingerprint AndAlso
                               Not s.logoff.HasValue AndAlso
                               s.logon >= oldestAllowedLogon
                         Select s).FirstOrDefault()
                    If u Is Nothing Then
                        Exit Sub
                    Else
                        'get user data
                        userHotelData = cfr.mgr.UserHotelMgr.GetUserByID(u.id_usuario)
                        userHotelData.usersessionkey = usersessionkey
                        loadUserData(userHotelData)
                    End If

                End Using
            End If
        End If
    End Sub


    Public Function loadUserData(userHotelData As cfr.mgr.UserHotelMgr) As cfr.admin.AdminSessionObj

        If userHotelData Is Nothing Then
            Return Nothing
        End If

        Dim sessionCtainer As New cfr.admin.sessionContainer(userHotelData, cfr.mgr.HotelMgr.GetEntityFull(userHotelData.cod_hotel))
        
        'new session
        Me.AdminSession = New cfr.admin.AdminSessionObj
        AdminBasePage.setAdminSession(Me.Context, AdminSession)

        AdminSession.addSessionContainer(sessionCtainer)

        Return AdminSession

    End Function
#End Region



#Region "Tools"

#Region "Session tools"
    ''' <summary>
    ''' Return hotel user session data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getAdminSession() As cfr.admin.AdminSessionObj
        Return Session(REQCte.session.hotelAdmin)
    End Function

    ''' <summary>
    ''' Return hotel user session data
    ''' </summary>
    ''' <param name="ctx"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getAdminSession(ByVal ctx As System.Web.HttpContext) As cfr.admin.AdminSessionObj
        Return ctx.Session(REQCte.session.hotelAdmin)
    End Function

    Public Shared Sub setAdminSession(ByVal ctx As System.Web.HttpContext, ByVal AdminSession As cfr.admin.AdminSessionObj)
        ctx.Session(REQCte.session.hotelAdmin) = AdminSession
    End Sub


#End Region


#End Region
    ''' <summary>
    ''' return actual page with session link idusuario-cod_hotel
    ''' </summary>
    ''' <param name="link"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function linkcnf(link As String) As String

        Return LinkConfig(Me.sessionCnt, link)

    End Function



    ''' <summary>
    ''' return actual page with session link idusuario-cod_hotel
    ''' </summary>
    ''' <param name="sessionCnt"></param>
    ''' <param name="link"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function LinkConfig(sessionCnt As cfr.admin.sessionContainer, link As String) As String
        If link.EndsWith("?") = True Then
            link = link & "{0}={1}"

        ElseIf link.Contains(".aspx?") Then
            link = link & "&{0}={1}"
        Else
            link = link & "?{0}={1}"

        End If
        Dim resolvelink As String = String.Format(link, cfr.REQCte.query.sessionlink, sessionCnt.sessionlink)
        Return resolvelink
    End Function

    ''' <summary>
    ''' Get list of operators/Not final clients
    ''' </summary>
    ''' <returns></returns>
    Public Function getDvClients() As DataView
        Dim tableRes As New ind.bk.obj.R_operadores_Manager

        Dim dv As DataView = tableRes.getOperadoresByIDHotel(Me.codhotel, CFRConst.ClientTypeEnum.None).DefaultView
        'dv.Sort = ind.bk.obj.R_operadores.DBCol.Operador
        Return dv
    End Function
    ''' <summary>
    ''' return all rooms without combined rooms
    ''' </summary>
    ''' <param name="filterWithoutCombined"></param>
    ''' <returns></returns>
    Public Function getDvRoomsAll(Optional filterWithoutCombined As Boolean = True) As DataView
        Dim tableRes As New ind.bk.obj.T_Hoteles_Habitaciones_Manager

        Dim dv As DataView = tableRes.getDataTableGetByCodhotel(Me.codhotel).DefaultView
        dv.Sort = ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel
        If filterWithoutCombined = True Then
            Dim roomCombined As New DataView(dv.Table)
            roomCombined.RowFilter = ind.bk.obj.T_Hoteles_Habitaciones.DBCol.cod_hab_combinada & " =0"
            roomCombined.Sort = ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel
            Return roomCombined
        Else
            Return dv
        End If
    End Function

    Public Function getDvTariffByHotel() As DataView
        Dim tableRes As New ind.bk.obj.R_tarifas_Manager

        Dim dv As DataView = tableRes.getDtTariffByIDHotel(Me.codhotel).DefaultView
        dv.Sort = ind.bk.obj.R_tarifas.DBCol.tarifa
        Return dv
    End Function

    Public Function getRequestValue(key As String) As String
        Return Me.Request(key)
    End Function

#Region "Operators helper"
    Public dvClientsm As DataView
    Public Property dvClients As DataView
        Get
            If Me.dvClientsm Is Nothing Then
                Me.dvClientsm = Me.getDvClients
            End If
            Return Me.dvClientsm
        End Get
        Set(value As DataView)
            Me.dvClientsm = value
        End Set
    End Property


    Public Function fillCboOperators(lst As DropDownList, dvoperators As DataView) As Integer
        Dim ventalibrecode As Integer = 0
        With lst.Items

            For Each drv As DataRowView In dvoperators
                If drv(ind.bk.obj.R_operadores.DBCol.FreeSale) <> cfr.CFRConst.genericOperator.PrivateGeneral Then

                    If drv(ind.bk.obj.R_operadores.DBCol.FreeSale) = cfr.CFRConst.genericOperator.freeSale Then
                        .Add(New ListItem("para venta libre o particular", drv(ind.bk.obj.R_operadores.DBCol.cod_operador)))
                        ventalibrecode = drv(ind.bk.obj.R_operadores.DBCol.cod_operador)
                    Else
                        .Add(New ListItem(drv(ind.bk.obj.R_operadores.DBCol.Operador), drv(ind.bk.obj.R_operadores.DBCol.cod_operador)))
                    End If

                End If
            Next
        End With
        lst.SelectedValue = ventalibrecode
    End Function

#End Region

#Region "Units helper"

    Private dvRoomsMem As DataView
    Public Property dvRooms As DataView
        Get
            If Me.dvRoomsMem Is Nothing Then
                Me.dvRoomsMem = Me.getDvUnits
            End If
            Return Me.dvRoomsMem

        End Get
        Set(value As DataView)
            Me.dvRoomsMem = value
        End Set
    End Property

    Public Function getDvUnits() As DataView
        Try
            Dim dv As DataView = Me.getDvRoomsAll(filterWithoutCombined:=False)
            Me.getRoomCombined(dv)

            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Function
    ''' <summary>
    ''' Dropdown with units
    ''' </summary>
    ''' <param name="lst"></param>
    ''' <param name="dvList"></param>
    Public Sub fillCboUnits(lst As DropDownList, dvList As DataView)

        With lst.Items
            For Each drv As DataRowView In dvList
                .Add(New ListItem(drv(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel), drv(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel)))
            Next
        End With

    End Sub


    Public roomsCombinedList As New ArrayList
    Public roomsCombinedList1 As New ArrayList
    Public Class roomCombined
        Public roomWithCupoCode As Integer
        Public roomCombinedCode As Integer
        Public roomCombinedName As String
        Public Sub New(roomWithCupoCode As Integer, roomCombinedCode As Integer, roomCombinedName As String)
            Me.roomWithCupoCode = roomWithCupoCode
            Me.roomCombinedCode = roomCombinedCode
            Me.roomCombinedName = roomCombinedName
        End Sub
    End Class

    Public Function getRoomCombined(dvRooms As DataView) As String
        Dim oRscom As New DataView(dvRooms.Table)
        Dim oRscomFull As New DataView(dvRooms.Table)

        For Each oRsHAB As DataRowView In oRscomFull
            oRscom.RowFilter = "codhabitacionhotel=" & oRsHAB("cod_hab_combinada")
            If oRscom.Count > 0 Then 'por si borra la habitacion combinada
                Dim dr0 As DataRowView = oRscom(0)
                roomsCombinedList.Add("<B>" & dr0("habitacionhotel") & "</B> esta combinada con la habitacion <B>" & oRsHAB("habitacionhotel") & "</B>")
                roomsCombinedList1.Add(New roomCombined(dr0("codhabitacionhotel"), oRsHAB("cod_hab_combinada"), oRsHAB("habitacionhotel")))
            End If
        Next

        If Me.roomsCombinedList.Count > 0 Then
            Dim s As String = "<ul>{0}</ul>"
            Dim sbody As String = ""
            For Each roomCombined As String In Me.roomsCombinedList
                sbody += String.Format("<li>{0}</li>", roomCombined)
            Next
            s = String.Format(s, sbody)
            'Me.litroomcombined.Text = s
            Return s
        End If

        Return ""
    End Function

    Public Function getRoomCombinedIU(code As Integer) As String
        For Each el As roomCombined In roomsCombinedList1
            If el.roomWithCupoCode = code Then
                Return "<br/><span class='roomcombined lblwarn lblwarn-sm'>Combina con: " & el.roomCombinedName & "</span>"
            End If
        Next
    End Function

#End Region

#Region "Tariff helper"
    ''' <summary>
    ''' Dropdown with units
    ''' </summary>
    ''' <param name="lst"></param>
    ''' <param name="dvList"></param>
    Public Sub fillCboTariff(lst As DropDownList, dvList As DataView)

        With lst.Items
            For Each drv As DataRowView In dvList
                Dim unitIdCrypt As String = UIHelper.encryptData(drv(ind.bk.obj.R_tarifas.DBCol.cod_tarifa))
                .Add(New ListItem(drv(ind.bk.obj.R_tarifas.DBCol.tarifa), unitIdCrypt))
            Next
        End With

    End Sub

    Public Function getDvTariffPriceListBase(id_Tar_HabSelected As Integer, dateStart As Date, dateEnd As Date) As List(Of R_PriceDayEF)

        Dim priceList As List(Of R_PriceDayEF) = getDvTariffPriceList2(id_Tar_HabSelected, dateStart, dateEnd)
        Dim dic As Dictionary(Of Date, R_PriceDayEF) = priceList.ToDictionary(Function(k) k.dayprice)
        'get default price, minimum days and release for empty pricebyday



        Dim dayTo As Date = dateStart
        Dim creationDate As Date = Date.Now
        Dim someoneCreated As Boolean = False
        Using ctx As New cfrbookEntities
            While dayTo <= dateEnd
                Dim priceEF As R_PriceDayEF = Nothing

                dic.TryGetValue(dayTo, priceEF)
                If priceEF Is Nothing Then 'price for selected day not exist we need to create
                    priceEF = New R_PriceDayEF
                    priceEF.id_Tar_Hab = id_Tar_HabSelected
                    priceEF.dayprice = dayTo
                    priceEF.price = 0
                    priceEF.ModifyDate = creationDate
                    ctx.R_PriceDayEF.Add(priceEF)
                    someoneCreated = True
                End If

                dayTo = dayTo.AddDays(1)

            End While
            If someoneCreated = True Then
                ctx.SaveChanges()
            End If

        End Using

        If someoneCreated = True Then
            'createPriceValuesForTariff(id_Tar_HabSelected)
            priceList = getDvTariffPriceList2(id_Tar_HabSelected, dateStart, dateEnd)
        End If

        Return priceList



    End Function

    Public Function getDvTariffPriceList2(id_Tar_HabSelected As Integer, dateStart As Date, dateEnd As Date) As List(Of R_PriceDayEF)

        Dim priceList As List(Of R_PriceDayEF)
        Using ctx As New cfrbookEntities
            priceList = (From pric In ctx.R_PriceDayEF
                         Where pric.id_Tar_Hab = id_Tar_HabSelected And pric.dayprice >= dateStart And pric.dayprice <= dateEnd
                         Order By pric.dayprice
                            ).ToList

        End Using

        Return priceList

    End Function

    Public Function getDvTariffPriceListByTariff_UnitbyRange(cod_tarifa As Integer, cod_habitacion As Integer, dateStart As Date, dateEnd As Date) As List(Of R_PriceDayEF)

        Dim queryDef As List(Of R_PriceDayEF)
        Using ctx As New cfrbookEntities
            queryDef = (From pric In ctx.R_PriceDayEF Join tarHab In ctx.R_Tarifa_habitacionEF
                            On pric.id_Tar_Hab Equals tarHab.id
                        Where tarHab.cod_tarifa = cod_tarifa And tarHab.cod_habitacion = cod_habitacion And pric.dayprice >= dateStart And pric.dayprice <= dateEnd
                        Order By pric.dayprice
                        Select pric).ToList
        End Using

        Return queryDef

    End Function
#End Region

End Class

Public Class PropertyUIshow

    Public controlParent As Control
    Public PropertyType As cfr.CFRConst.unitTypeEnum = CFRConst.unitTypeEnum.hotel
    Public Sub New(PropertyTypeSet As cfr.CFRConst.unitTypeEnum)
        Me.PropertyType = PropertyTypeSet
    End Sub

    Public Shared Sub hideByType(tipo As cfr.CFRConst.unitTypeEnum, mypage As Page)
        Dim c As New PropertyUIshow(tipo)
        c.GetAllControls(mypage)
    End Sub

    Public Function GetAllControls(parent As Control) As ControlCollection

        For Each control As Control In parent.Controls
            hideByType(control)

            Dim ctrolCol As ControlCollection = GetAllControls(control)
            If Not ctrolCol Is Nothing Then
                For Each descendant As Control In ctrolCol
                    hideByType(descendant)
                Next
            End If

        Next
    End Function

    Private Sub hideByType(control2Check As Control)
        Dim control As PlaceHolder
        If TypeOf control2Check Is PlaceHolder Then
            control = DirectCast(control2Check, PlaceHolder)
        Else
            Exit Sub
        End If


        Select Case Me.PropertyType
            Case CFRConst.unitTypeEnum.hotel
                If control.ClientID.IndexOf("plhhotel") > -1 Then
                    control.Visible = True
                End If
            Case CFRConst.unitTypeEnum.car
                If control.ClientID.IndexOf("plhhotel") > -1 Then
                    control.Visible = False
                End If
        End Select

    End Sub



End Class


