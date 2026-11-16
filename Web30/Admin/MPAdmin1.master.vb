
Imports System.Diagnostics
Imports System.Data
Imports System.Linq
Imports ind.bk.reports

Partial Class Admin_MPAdmin1
    Inherits System.Web.UI.MasterPage

    Public AdminSession As New cfr.admin.AdminSessionObj
    Public sessionCnt As cfr.admin.sessionContainer

    Public ownerpage As AdminBasePage
    Public Const cssVersionVar As String = "?v=14"
    Public showoldmenu As Boolean = False


#Region "fields"
    Private _pageTitle As String = "WBooking - Hotel administration"
    Public Property pagetitle() As String
        Get
            Return _pageTitle
        End Get
        Set(ByVal value As String)
            Me._pageTitle = value
        End Set
    End Property

    Public _titulo As String = "WBooking - Hotel administration"
    Public Property Titulo() As String
        Get
            Return _titulo
        End Get
        Set(ByVal value As String)
            Me._titulo = value
        End Set
    End Property

    Public ReadOnly Property cssVersion() As String
        Get
            Return cssVersionVar
        End Get
    End Property

    Public ReadOnly Property holdprop As cfr.obj.Hotel
        Get
            If Not Me.ownerpage Is Nothing Then
                Return Me.ownerpage.hotel
            Else
                Return Nothing
            End If

        End Get
    End Property

#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.ownerpage = CType(Me.Page, AdminBasePage)
        Me.sessionCnt = Me.ownerpage.sessionCnt
        If Not Me.IsPostBack Then
            ConfigcboBookEdition()
        End If


    End Sub

    'Protected Sub closeSessionButton1(sender As Object, e As EventArgs) Handles closeSessionButton.Click

    '    Session.Clear()
    '    Session.Abandon()
    '    FormsAuthentication.SignOut()
    '    Response.Redirect("login.aspx")
    'End Sub

    Public Sub CloseSession()
        Session.Clear()
        Session.Abandon()
        FormsAuthentication.SignOut()
        Response.Redirect("login.aspx")
    End Sub

    Public Property HotelIDVS As Integer

        Get
            Return CInt(ViewState("hotelID"))
        End Get
        Set(value As Integer)
            ViewState("hotelID") = value
        End Set
    End Property


    Public Function linkcnf(link As String) As String
        Return AdminBasePage.LinkConfig(Me.sessionCnt, link)

    End Function



    Public Function getMenuString(menuValue As String)

        If Me.sessionCnt.hotel.Tipo = cfr.CFRConst.unitTypeEnum.hotel Then
            Return menuValue
        End If


        Select Case menuValue.ToUpper
            Case "HOTEL"
                Return "Empresa"
            Case "HABITACIONES"
                Return "Coches"
            Case Else
                Return menuValue
        End Select

    End Function

    Protected Sub ScriptManager1_AsyncPostBackError(sender As Object, e As AsyncPostBackErrorEventArgs)

        ScriptManager1.AsyncPostBackErrorMessage = "An error occurred during the request: " + e.Exception.Message
    End Sub


    Protected Sub btnmastershowbook_Click(sender As Object, e As EventArgs) Handles btnmastershowbook.Click
        Try
            Dim bookid As Integer = Me.hddmasterbookidbookview.Value

            Dim bookMgr As New ind.bk.obj.R_ReservasO_Manager
            'bookData
            Dim bookdata As BookRes = Nothing

            bookdata = cfr.BookManager.getBookResById(bookid)
            Dim showOptions As New EmailShowOptions
            showOptions.showExternalBorder = False
            Dim iuf As New cfr.IUFunctions(bookdata.site.theme, bookdata, showOptions)

            iuf.bookForStyle = IUFunctions.MailBookStyle.hotel

            Dim bookhtml As String = iuf.getHtmlBook()
            Me.litmasterbookview.Text = bookhtml

        Catch ex As Exception
            Me.litmasterbookview.Text = UIMsg.UIMessageDismiss(ex.Message, CFRConst.msgEnum.errorType)
        End Try
    End Sub


    


    Protected Sub ScriptManager1_AsyncPostBackError1(sender As Object, e As AsyncPostBackErrorEventArgs) Handles ScriptManager1.AsyncPostBackError
        If (e.Exception.Data("ExtraInfo") <> Nothing) Then
            ScriptManager1.AsyncPostBackErrorMessage = _
               e.Exception.Message & _
               e.Exception.Data("ExtraInfo").ToString()
        Else
            ScriptManager1.AsyncPostBackErrorMessage = "Se ha producido un error no especificado: " & e.Exception.Message
        End If
    End Sub


#Region "book edition"

    Private Sub ConfigcboBookEdition()
        With Me.cbobookstate.Items
            .Add(New ListItem("Activa/Sin confirmar", cfr.CFRConst.bookStateEnum.Active))
            .Add(New ListItem("CheckIn Confirmado.", cfr.CFRConst.bookStateEnum.confirmedByHotel))
            .Add(New ListItem("No Show", cfr.CFRConst.bookStateEnum.NoShow))
            .Add(New ListItem("Cancelada por el hotel", cfr.CFRConst.bookStateEnum.CancelByHotelUser))
            .Add(New ListItem("Cancelada por el cliente", cfr.CFRConst.bookStateEnum.CancelByClient))
            .Add(New ListItem("Cancelada por el operador", cfr.CFRConst.bookStateEnum.canceledByOperator, False))
        End With
        Me.cbobookstate.SelectedValue = cfr.CFRConst.bookStateEnum.Active

        With Me.cbobookpaystate.Items
            .Add(New ListItem("Pendiente", CInt(cfr.CFRConst.payStateEnum.pending)))
            .Add(New ListItem("Pagada", CInt(cfr.CFRConst.payStateEnum.payed)))
            .Add(New ListItem("No es necesaria acción", CInt(cfr.CFRConst.payStateEnum.noactioneeded)))
            .Add(New ListItem("Cargar valor por cancelación", CInt(cfr.CFRConst.payStateEnum.paybycancellationneeded)))
            .Add(New ListItem("Valor por cancelación cargada", CInt(cfr.CFRConst.payStateEnum.paybycancellationOK)))
            .Add(New ListItem("Realizar devolución", CInt(cfr.CFRConst.payStateEnum.devolutionPending)))
            .Add(New ListItem("Devolución realizada", CInt(cfr.CFRConst.payStateEnum.DevolutionOK)))
            .Add(New ListItem("Precargo realizado", CInt(cfr.CFRConst.payStateEnum.PrechargeMade)))
        End With
        Me.cbobookpaystate.SelectedValue = CInt(cfr.CFRConst.payStateEnum.pending)


        For x As Integer = 9 To 23
            Me.cbobookdateintime_master.Items.Add(New ListItem(x & ":00", x))
            Me.cbobookdateouttime_master.Items.Add(New ListItem(x & ":00", x))
        Next

        'CHILDREN
        With Me.f_ninosfree.Items
            For x As Integer = 0 To 2
                .Add(New ListItem(x & " bebes", x))
            Next
        End With
        Me.f_ninosfree.Text = 0 'bookData.ninosfree
        With Me.f_ninosPago.Items
            For x As Integer = 0 To 5
                .Add(New ListItem(x & " niños", x))
            Next
        End With
        Me.f_ninosPago.Text = 0 'bookData.ninospago
        'ADULTS
        With Me.f_numAdultos.Items
            For x As Integer = 0 To 5
                .Add(New ListItem(x & " adultos", x))
            Next
        End With
        Me.f_numAdultos.Text = 1 'bookData.numAdultos

        If Me.holdprop.Tipo = CFRConst.unitTypeEnum.car Then
            Me.pnlpax.Visible = False
            Me.pnlcarpickup.Visible = True
            Me.pnlcarlocationprices.Visible = True
            CboLocationsLoad()
        ElseIf Me.holdprop.Tipo = CFRConst.unitTypeEnum.hotel Then
            Me.pnlpax.Visible = True
            Me.pnlcarpickup.Visible = False
            Me.pnlcarlocationprices.Visible = False
        End If
    End Sub

    Private Sub CboLocationsLoad()

        Dim ctx As New cfrbookEntities

        Dim queryDef = _
            From ord In ctx.R_UnitLocation _
            Join det In ctx.R_UnitLocation_Lang _
            On ord.id_unit_location Equals det.id_unit_location _
            Where ord.cod_hotel = Me.holdprop.cod_Hotel _
                    And det.id_Lang = "es" _
                    Order By det.Description _
            Select New With _
            { _
                .id_unit_location = ord.id_unit_location, _
                .Description = det.Description, _
                .tipo = ord.tipo, _
                .price = ord.Price _
            }

        Try
            ' Bind the ComboBox control to the query.
            ' To prevent the query from being executed multiple times during binding, 
            ' it is recommended to bind controls to the result of the Execute method. 
            Me.cbolocationpickup.DataTextField = "description"
            Me.cbolocationpickup.DataValueField = "id_unit_location"
            'Me.cbolocationpickup.DataSource = ctx.R_UnitLocation.ToList
            Me.cbolocationpickup.DataSource = queryDef.ToList
            Me.cbolocationpickup.DataBind()

            Me.cbolocationdrop.DataTextField = "description"
            Me.cbolocationdrop.DataValueField = "id_unit_location"
            Me.cbolocationdrop.DataSource = queryDef.ToList
            Me.cbolocationdrop.DataBind()

            Dim defaultVal = (From r In queryDef Where r.tipo = 1).FirstOrDefault
            If Not defaultVal Is Nothing Then
                Me.cbolocationpickup.SelectedValue = defaultVal.id_unit_location
                Me.cbolocationdrop.SelectedValue = defaultVal.id_unit_location
            End If
            
        Catch ex As Exception

        End Try

    End Sub




    Protected Sub btneditbookload_master_Click(sender As Object, e As EventArgs) Handles btneditbookload_master.Click

        Dim ctx As cfrbookEntities
        Try
            Me.plheditbookform.Visible = True
            Me.mastereditbook_liteditmessage.Text = ""
            Dim bookid As Integer = Me.bookidhddedit.Value

            ctx = New cfrbookEntities
            Dim bkdata As BookRes = BookRes.getBookDataByID(bookid)
            bkdata.ctx = ctx
            Dim bkUnit As BookedRoom = bkdata.roomsSelected.Values(0)


            Me.tbookid_master.Text = bkdata.idEntity
            Me.tbookdatein_master.Text = UIHelper.setUIValueDate(bkdata.dateIn)
            Me.tbookdateout_master.Text = UIHelper.setUIValueDate(bkdata.dateOut)

            If bkdata.hotel.Tipo = CFRConst.unitTypeEnum.car Then
                Me.cbobookdateintime_master.SelectedValue = bkdata.dateIn.Hour
                Me.cbobookdateouttime_master.SelectedValue = bkdata.dateOut.Hour

                Me.cbolocationpickup.SelectedValue = bkdata.carLocationSelected.unit_location_GET
                Me.cbolocationdrop.SelectedValue = bkdata.carLocationSelected.unit_location_Return
                Me.tlocationnote.Text = bkdata.carLocationSelected.location_description_manual

              Else
                Me.cbobookdateintime_master.SelectedValue = 12
                Me.cbobookdateouttime_master.SelectedValue = 12
            End If

            Me.tunitbooked.Text = bkUnit.roomSelected.description.getDes

            Me.f_numAdultos.SelectedValue = bkdata.numAdultos
            Me.f_ninosPago.SelectedValue = bkdata.ninospago
            Me.f_ninosfree.SelectedValue = bkdata.ninosfree

            Me.bookcommentsbyclient.Text = bkdata.comentarios
            
            With bkdata.BookDataEF

                'book status/number/etc
                Me.tbookidproperty.Text = .cod_Reserva_hotel
                Me.cbobookpaystate.SelectedValue = .CheckHotelPago
                Me.cbobookstate.SelectedValue = bkUnit.bookState

                'client
                Me.clientname.Text = .Nombre
                Me.clientsurname.Text = .Apellido1
                Me.clientphone.Text = .telefono
                Me.clientemail.Text = .email
                Me.cboclientlanguage.SelectedValue = UIHelper.setUIValueTxt(.language, "2")
                Me.clientbirth.Text = UIHelper.setUIValueDate(.birthdate)
                Me.clientnacionality.Text = .nacionality
                'identificacion
                Me.clientdni.Text = .DNI
                Me.clientpassport.Text = .passport
                Me.clientdriverlicense.Text = .driverlicense
                'address
                Me.clientaddress.Text = .direccion
                Me.clientaddresscp.Text = .cod_postal
                Me.clientaddresscity.Text = .ciudad
                Me.clientaddresscountry.Text = .pais
                'credit card
                Me.clientcardtype.Text = .Card_type
                Me.clientcardnumber.Text = .nTarjeta
                Me.clientcardholder.Text = .Titular
                Me.clientcardcaducity.Text = .FCaducidad
            End With

            'price/extras
            '   unit selected 
            Me.tunitbookeddesc.Text = bkUnit.roomSelected.description.getDes()
            Me.tunitbookedprice.Text = bkUnit.priceFinalForAllDays
            

            '   promotion
            If Not bkdata.BookDataEF.promotioncode Is Nothing Then
                Dim promotion As R_operadorEF = ctx.R_operadorEF.Find(CInt(bkdata.BookDataEF.promotioncode))
                Me.tunitbookedpromotion.Text = promotion.Operador
                Me.tunitbookedpromotiondiscount.Text = promotion.discount
            Else
                Me.tunitbookedpromotion.Text = ""
                Me.tunitbookedpromotiondiscount.Text = ""
            End If
            
            Dim finalprice As Double = bkUnit.priceFinalForAllDays

            If bkdata.hotel.Tipo = CFRConst.unitTypeEnum.car Then
                Dim priceForLocationGet As Double = 0
                Me.tlocationpickup.Text = bkdata.carLocationSelected.R_UnitLocation.location
                Me.tlocationdrop.Text = bkdata.carLocationSelected.R_UnitLocation1.location

                priceForLocationGet = bkdata.getPriceForLocation
                If priceForLocationGet > bkdata.carLocationSelected.unit_location_Get_Price Then
                    Me.tlocationpickupprice.Text = ""
                Else
                    Me.tlocationpickupprice.Text = bkdata.carLocationSelected.unit_location_Get_Price
                End If
                If priceForLocationGet > bkdata.carLocationSelected.unit_location_Return_Price Then
                    Me.tlocationdropprice.Text = ""
                Else
                    Me.tlocationdropprice.Text = bkdata.carLocationSelected.unit_location_Return_Price
                End If

                finalprice += priceForLocationGet
            End If


            Me.tbookpricefinal.Text = Math.Round(finalprice, 2).ToString





            If Not bkdata.BookDataEF.R_ReservasO_Note Is Nothing Then
                Me.tmp_note.Text = bkdata.internalNote
            End If


        Catch ex As Exception
            Me.mastereditbook_liteditmessage.Text = UIMsg.UIMessageDismiss(ex.Message, CFRConst.msgEnum.errorType)
        Finally
            If Not ctx Is Nothing Then
                ctx.Dispose()
            End If
        End Try
    End Sub

    Protected Sub btnSubmit_Click__OLDER(sender As Object, e As EventArgs)

        Dim ocon As SqlClient.SqlConnection
        Dim trans As SqlClient.SqlTransaction
        Try
            Dim bookid As Integer = bookidhddedit.Value

            ocon = ind.bk.DBMgr.getNewConn 'abrimos la conexion para el update
            trans = ind.bk.DBMgr.CreateTransaction(ocon) 'comienzo de la transaccion

            Dim bookMgr As New ind.bk.obj.R_ReservasO_Manager
            'bookData
            Dim bookdata As BookRes = Nothing

            bookdata = cfr.BookManager.getBookResById(bookid)
            If bookdata Is Nothing Then
                Throw New Exception(cfr.Language.GetCaption(177))
            End If

            Dim bookObj As New ind.bk.obj.R_ReservasO
            Dim dt As DataTable = bookMgr.getDataTableByID(bookid)
            If dt.Rows.Count > 0 Then
                bookObj.FillByRow(dt.Rows(0))
            End If

            With bookObj
                .cod_Reserva_hotel = Me.tbookidproperty.Text.Trim
                .CheckHotelPago = Me.cbobookpaystate.SelectedValue
                .precioReal = Me.tbookpricefinal.Text.Trim
                .FechaModPrecioReal = Date.Now
                .CheckHotelUser = Me.sessionCnt.user.usuario
            End With

            'UPDATE BOOK STATUS HISTORY
            Dim bookStatusHist As New ind.bk.obj.R_bookstatehistory
            bookStatusHist.cod_reserva = bookObj.cod_reservaO
            bookStatusHist.username = bookObj.CheckHotelUser
            bookStatusHist.pricereal = bookObj.precioReal

            For Each bo As BookedRoom In bookdata.roomsSelected.Values 'in this version we have only one room
                bookStatusHist.bookstate = bo.bookState
                bookStatusHist.forunit = bo.roomSelected.idEntity
            Next

            Dim operHistorymgr As New ind.bk.obj.R_bookstatehistory_Manager
            operHistorymgr.insert(bookStatusHist, trans)

            bookMgr.Update(bookObj, trans)


            Dim sendEmailCancellation As Boolean = False
            If bookStatusHist.bookstate <> Me.cbobookstate.SelectedValue Then 'boostate changed
                If Me.cbobookstate.SelectedValue = cfr.CFRConst.bookStateEnum.CancelByHotelUser Then
                    'send an email to client with cancellation
                    sendEmailCancellation = True
                End If
            End If


            Me.cancelBookAdmin(bookdata, Me.cbobookstate.SelectedValue, trans)



            Me.mastereditbook_liteditmessage.Text = UIMsg.UIMessageDismiss(String.Format("La reserva {0} ha sido modificada.", bookid), CFRConst.msgEnum.Ok)
            ind.bk.DBMgr.CommitTransaction(trans)

            If sendEmailCancellation = True Then
                sendEmailConfirmation(bookdata)
            End If


            'bookScriptResult(bookdata, Me.cbobookstate.SelectedValue, bookObj.CheckHotelPago, bookObj.precioReal, bookObj.cod_Reserva_hotel)


        Catch exsql As SqlClient.SqlException
            ind.bk.DBMgr.rollbackTransaction(trans)
            If exsql.Number = 2627 Then
                Me.mastereditbook_liteditmessage.Text = UIMsg.UIMessageDismiss("Ha intentado introducir un login de usuario que ya existe. Por favor seleccione otro.", CFRConst.msgEnum.errorType)
            Else
                Me.mastereditbook_liteditmessage.Text = UIMsg.UIMessageDismiss(exsql.Message, CFRConst.msgEnum.errorType)
            End If

        Catch ex As Exception
            ind.bk.DBMgr.rollbackTransaction(trans)

            Me.mastereditbook_liteditmessage.Text = UIMsg.UIMessageDismiss(ex.Message, CFRConst.msgEnum.errorType)
        Finally
            ind.bk.DBMgr.disposeTransaction(trans)
        End Try
    End Sub

    Protected Sub btneditbooksave_master_Click(sender As Object, e As EventArgs) Handles btneditbook_master.Click

        Dim trans As System.Data.Common.DbTransaction = Nothing
        Try
            Me.mastereditbook_liteditmessage.Text = ""
            Dim bookid As Integer = Me.bookidhddedit.Value

            Dim bkdata As BookRes = BookRes.getBookDataByID(bookid)
            Dim ctx As New cfrbookEntities
            ctx.BookResEF.Attach(bkdata.BookDataEF)
            bkdata.ctx = ctx

            Dim bkUnit As BookUnit = bkdata.BookDataEF.R_ResO_Hab(0) 'in this version we have only one room

            'fecha
            Dim dateIn As Date, dateOut As Date

            dateIn = DateTime.Parse(Me.tbookdatein_master.Text)
            dateOut = DateTime.Parse(Me.tbookdateout_master.Text)

            If bkdata.hotel.Tipo = CFRConst.unitTypeEnum.car Then
                'ctx.R_ResO_CarPickup.Attach(bkdata.carLocationSelected)
                dateIn = New DateTime(dateIn.Year, dateIn.Month, dateIn.Day, CInt(Me.cbobookdateintime_master.SelectedValue), 0, 0, 0)
                dateOut = New DateTime(dateOut.Year, dateOut.Month, dateOut.Day, CInt(Me.cbobookdateouttime_master.SelectedValue), 0, 0, 0)

                bkdata.carLocationSelected.location_description_manual = Me.tlocationnote.Text
            End If

            bkdata.setDates(dateIn, dateOut)

            bkUnit.fecha_entrada = dateIn
            bkUnit.fecha_salida = dateOut
            Dim numAdultos As Integer = CInt(Me.f_numAdultos.SelectedValue) + CInt(Me.f_ninosPago.SelectedValue) + CInt(Me.f_ninosfree.SelectedValue)
            bkUnit.num_personas = numAdultos

            bkUnit.edadnino1 = CInt(Me.f_ninosfree.SelectedValue)
            bkUnit.edadnino2 = CInt(Me.f_ninosPago.SelectedValue)

            With bkdata.BookDataEF
                .cod_Reserva_hotel = Me.tbookidproperty.Text.Trim
                .CheckHotelPago = Me.cbobookpaystate.SelectedValue
                .precioReal = Me.tbookpricefinal.Text.Trim
                .FechaModPrecioReal = Date.Now
                .CheckHotelUser = Me.sessionCnt.user.usuario



                'client data
                .Nombre = Me.clientname.Text
                .Apellido1 = Me.clientsurname.Text
                .telefono = Me.clientphone.Text
                .email = Me.clientemail.Text
                .birthdate = UIHelper.getUItxtValue(Me.clientbirth)
                .nacionality = Me.clientnacionality.Text
                .language = Me.cboclientlanguage.SelectedValue

                'ident
                .DNI = Me.clientdni.Text
                .passport = Me.clientpassport.Text
                .driverlicense = Me.clientdriverlicense.Text
                'address
                .direccion = Me.clientaddress.Text
                .cod_postal = Me.clientaddresscp.Text
                .ciudad = Me.clientaddresscity.Text
                .pais = Me.clientaddresscountry.Text
                'credit card
                .Card_type = Me.clientcardtype.Text
                .nTarjeta = Me.clientcardnumber.Text
                .CCUTarjeta = String.Empty
                .Titular = Me.clientcardholder.Text
                .FCaducidad = Me.clientcardcaducity.Text


                If Me.tmp_note.Text.Trim <> "" Then
                    If bkdata.BookDataEF.R_ReservasO_Note Is Nothing Then
                        bkdata.BookDataEF.R_ReservasO_Note = New R_ReservasO_Note
                    End If

                    .R_ReservasO_Note.Note = Me.tmp_note.Text
                    .R_ReservasO_Note.id_usuario = Me.sessionCnt.user.id_usuario
                    .R_ReservasO_Note.datemodify = Date.Now

                ElseIf Me.tmp_note.Text.Trim = "" Then
                    If Not bkdata.BookDataEF.R_ReservasO_Note Is Nothing Then
                        bkdata.BookDataEF.R_ReservasO_Note = Nothing
                    End If
                End If

            End With

            'unit price
            


            'UPDATE BOOK STATUS HISTORY
            Dim bookStatusHist As New ind.bk.obj.R_bookstatehistory
            bookStatusHist.cod_reserva = bkdata.idEntity
            bookStatusHist.username = bkdata.BookDataEF.CheckHotelUser
            bookStatusHist.pricereal = bkdata.BookDataEF.precioReal


            bookStatusHist.bookstate = bkUnit.Estado
            bookStatusHist.forunit = bkUnit.tipo_habitacion


            Dim operHistorymgr As New ind.bk.obj.R_bookstatehistory_Manager

            trans = ctx.Database.BeginTransaction().UnderlyingTransaction

            operHistorymgr.insert(bookStatusHist, trans)

            'save all
            ctx.SaveChanges()

            Dim sendEmailCancellation As Boolean = False
            If bookStatusHist.bookstate <> Me.cbobookstate.SelectedValue Then 'boostate changed
                If Me.cbobookstate.SelectedValue = cfr.CFRConst.bookStateEnum.CancelByHotelUser Then
                    'send an email to client with cancellation
                    sendEmailCancellation = True
                End If
            End If

            Me.cancelBookAdmin(bkdata, Me.cbobookstate.SelectedValue, trans)

            Me.mastereditbook_liteditmessage.Text = UIMsg.UIMessageDismiss(String.Format("La reserva {0} ha sido modificada.", bookid), CFRConst.msgEnum.Ok)
            ind.bk.DBMgr.CommitTransaction(trans)

            If sendEmailCancellation = True Then
                sendEmailConfirmation(bkdata)
            End If

            bookScriptResult(bkdata, Me.cbobookstate.SelectedValue)


        Catch exsql As SqlClient.SqlException
            ind.bk.DBMgr.rollbackTransaction(trans)
            If exsql.Number = 2627 Then
                Me.mastereditbook_liteditmessage.Text = UIMsg.UIMessageDismiss("Ha intentado introducir un login de usuario que ya existe. Por favor seleccione otro.", CFRConst.msgEnum.errorType)
            Else
                Me.mastereditbook_liteditmessage.Text = UIMsg.UIMessageDismiss(exsql.Message, CFRConst.msgEnum.errorType)
            End If

        Catch ex As Exception
            ind.bk.DBMgr.rollbackTransaction(trans)

            Me.mastereditbook_liteditmessage.Text = UIMsg.UIMessageDismiss(ex.Message, CFRConst.msgEnum.errorType)
        Finally
            ind.bk.DBMgr.disposeTransaction(trans)
        End Try
    End Sub


    Private Function bookScriptResult(bkData As BookRes, bookState As CFRConst.bookStateEnum)

        Dim rep As New ind.bk.reports.ReportBooks

        Dim ors As DataView = rep.GetReservTOTALBy(bkData.hotel.cod_Hotel, bkData.hotel.Tipo, ind.bk.reports.ReportBooks.searchTypeEnum.byBookID, bkData.idEntity)
        Dim drv As DataRowView = ors(0)
        Dim repUI As New bookReportUI(drv, bkData.hotel)
        Dim newprice As String
        If repUI.priceFinal = bkData.BookDataEF.precioReal Then
            newprice = ""
        Else
            newprice = bkData.BookDataEF.precioReal
        End If
        Dim script As String = String.Format("setnewbookvalues(""{0}"",""{1}"",""{2}"",""{3}"",""{4}"",""{5}""); ", repUI.bookID, CInt(bookState), CInt(bkData.BookDataEF.CheckHotelPago), newprice, repUI.strEstadoReserva, bkData.BookDataEF.cod_Reserva_hotel)

        UIMsg.registerScript(Me.Page, script, "bookscript")
    End Function

    Private Function sendEmailConfirmation(bookdata As BookRes)
        Try
            'send an email to client with cancellation
            Dim bookCancelMgr As New ind.bk.book.bookCancelManager
            bookCancelMgr.SendCancelConfirmationEF(bookdata)
        Catch ex As Exception

        End Try
    End Function


    Public Sub cancelBookAdmin(bookdata As BookRes _
            , newBookState As cfr.CFRConst.bookStateEnum _
            , trans As SqlClient.SqlTransaction)
        '***********************************************************************
        '    estado=0 reserva normal
        '    estado=1 cancelada x cliente
        '    estado=2 noshow
        '    estado=3 modificada
        '    estado=4 cancelada x usuario
        '    estado=5 confirmada
        '    estado=6 cancelada x operador


        Dim cancelByOperator As cfr.OperatorObj = Nothing
        Dim cancelDate As Date = Date.Now

        'If cancelDate.Date >= bd.dateIn.Date Then
        '    Throw New Exception(cfr.Language.GetCaption(301))
        'End If

        Dim estadoX As String = Me.sessionCnt.user.usuario
        'Obtenemos el nuevo ESTADO y actualizamos

        'EL ESTADO DE LA RESERVA SOLO PUEDE SER
        'Valores de checkHotel para la operativa de este metodo
        '    estado=2 noshow
        '    estado=4 cancelada x usuario
        '    estado=5 confirmada
        'COMPROBAMOS QUE NO VENGA NINGUN ESTADO DIFERENTE
        Select Case newBookState
            Case CFRConst.bookStateEnum.Active, CFRConst.bookStateEnum.CancelByClient, CFRConst.bookStateEnum.NoShow, CFRConst.bookStateEnum.CancelByHotelUser, CFRConst.bookStateEnum.confirmedByHotel
                'OK
            Case Else
                Throw New Exception("El estado de reserva a cambiar es incorrecto.")
        End Select

        'in this version the user cancel all the book
        For Each bo As BookedRoom In bookdata.roomsSelected.Values
            Dim roomIdToUpdateAvailability As Integer
            If bo.roomSelected.cod_hab_combinada > 0 Then
                roomIdToUpdateAvailability = bo.roomSelected.cod_hab_combinada
            Else
                roomIdToUpdateAvailability = bo.roomSelected.idEntity
            End If

            Select Case newBookState

                Case CFRConst.bookStateEnum.NoShow, CFRConst.bookStateEnum.CancelByHotelUser

                    If bo.bookState = CFRConst.bookStateEnum.Active Or bo.bookState = CFRConst.bookStateEnum.confirmedByHotel Then 'if active or confirmed we return availability to BBDD

                        cfr.AvailabilityManager.UpdateBookedRoomAvailability(1, bookdata.bookOperator.id, bookdata.hotel.id, roomIdToUpdateAvailability, bookdata.dateIn, bookdata.dateOut, bo.originalAvailability, trans)

                        'cfr.AvailabilityManager.UpdateBookedRoomAvailability(1, bookdata.bookOperator.id, bookdata.hotel.id, bo.roomSelected.idEntity, bookdata.dateIn, bookdata.dateOut, bo.originalAvailability, trans)

                    End If

                Case CFRConst.bookStateEnum.CancelByClient, CFRConst.bookStateEnum.confirmedByHotel, CFRConst.bookStateEnum.Active  'New state will be confirmed

                    Select Case bo.bookState
                        Case CFRConst.bookStateEnum.canceledByOperator, CFRConst.bookStateEnum.NoShow, CFRConst.bookStateEnum.CancelByClient, CFRConst.bookStateEnum.CancelByHotelUser
                            'it was cancelled before. We need to decrease availability if posible
                            Try
                                cfr.AvailabilityManager.UpdateBookedRoomAvailability(-1, bookdata.bookOperator.id, bookdata.hotel.id, bo.roomSelected.idEntity, bookdata.dateIn, bookdata.dateOut, bo.originalAvailability, trans)
                            Catch ex As Exception
                                If ex.Message = basERR.E_NoDisponibility Then
                                    Throw New Exception("No existe disponibilidad para esta habitación. Por favor aumente la disponibilidad.")
                                Else
                                    Throw ex
                                End If
                            End Try
                    End Select

            End Select


            cfr.BookManager.UpdateBookState(bookdata.idEntity, bo.roomSelected.idEntity, newBookState, cancelDate, estadoX, trans)

        Next


    End Sub



#End Region

End Class

