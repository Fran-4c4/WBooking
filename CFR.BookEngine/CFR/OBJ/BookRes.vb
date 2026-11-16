Imports Microsoft.VisualBasic
Imports System.Web


Namespace cfr
    
    ''' <summary>
    ''' Object with data for booking
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class BookRes
        Implements IIDObject

        Public ctx As cfrbookEntities
        Public Function openCtx() As Boolean
            If Me.ctx Is Nothing Then
                ctx = New cfrbookEntities
                Return True
            Else
                Return False
            End If


        End Function

        Public Sub closeCtx()
            If Not Me.ctx Is Nothing Then
                ctx.Dispose()
            End If
        End Sub

        Public Property idEntity() As Object Implements IIDObject.idEntity
            Get
                Return Me.BookDataEF.cod_reservaO
            End Get
            Set(ByVal value As Object)
                Me.BookDataEF.cod_reservaO = value
            End Set
        End Property

        Public BookDataEF As New BookResEF


        ''' <summary>
        ''' Date the book was made.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property bookDate As Date
            Get
                Return Me.BookDataEF.HoraReserva
            End Get
            Set(value As Date)
                Me.BookDataEF.HoraReserva = value
            End Set
        End Property

        Public sessionId As String
        ''' <summary>
        ''' Value to avoid the user try again the book when book and back again on the navigator.
        ''' </summary>
        ''' <remarks></remarks>
        Public uniqueBookId As Object
        ''' <summary>
        ''' site asociated with the request
        ''' </summary>
        ''' <remarks></remarks>
        Public site As New SiteObj
        Private hotelVar As cfr.obj.Hotel
        ''' <summary>
        ''' the hotel selected in each moment
        ''' </summary>
        ''' <remarks></remarks>
        Public Property hotel As cfr.obj.Hotel
            Get
                Return Me.hotelVar
            End Get
            Set(value As cfr.obj.Hotel)
                Me.hotelVar = value
                Me.BookDataEF.cod_hotel = value.cod_Hotel
            End Set
        End Property

        Private bookOperatorVar As OperatorObj
        ''' <summary>
        ''' the book operator, private client, agency
        ''' </summary>
        ''' <remarks></remarks>
        Public Property bookOperator As OperatorObj
            Get
                Return Me.bookOperatorVar
            End Get
            Set(value As OperatorObj)
                Me.bookOperatorVar = value
            End Set
        End Property

        Public promotionCode As String
        Public bookOperatorPromotion As OperatorObj


        'Public bookOperatorFree As OperatorObj
        ''' <summary>
        ''' selected language
        ''' </summary>
        ''' <remarks></remarks>
        Public Property language As CFRConst.LanguageEnum
            Get
                Return Me.BookDataEF.language
            End Get
            Set(value As CFRConst.LanguageEnum)
                Me.BookDataEF.language = value
            End Set
        End Property



        ''' <summary>
        ''' PayGate data
        ''' </summary>
        ''' <remarks></remarks>
        Public tpvData As New PayGateWayManager
        ''' <summary>
        ''' Value to charge in tpv either than complete price
        ''' </summary>
        ''' <remarks></remarks>
        Public TPVMinimum As Double

        'step 1
        'Search criteria
        ''' <summary>
        ''' Date checkin
        ''' </summary>
        ''' <remarks></remarks>
        Private _dateIn As Date = Date.Now
        ''' <summary>
        ''' Date checkout
        ''' </summary>
        ''' <remarks></remarks>
        Private _dateOut As Date = Date.Now.AddDays(1)

        'Public bookUnitVar As BookUnit

        'Public Property bookUnitSel As BookUnit
        '    Get
        '        If Me.BookDataEF.R_ResO_Hab.Count = 0 Then
        '            Me.bookUnitVar = New BookUnit
        '            Me.BookDataEF.R_ResO_Hab.Add(Me.bookUnitVar)
        '        Else
        '            Me.bookUnitVar = Me.BookDataEF.R_ResO_Hab.FirstOrDefault
        '        End If
        '        Return Me.bookUnitVar
        '    End Get
        '    Set(value As BookUnit)
        '        Me.bookUnitVar = value
        '    End Set
        'End Property


        Public numAdultos As Byte
        Public ninospago As Byte 'edadnino1 en BBDD
        Public ninosfree As Byte 'edadnino2 en BBDD
        Public regimen As CFRConst.RegimenEnum


        ''' <summary>
        ''' total number of days
        ''' </summary>
        ''' <remarks></remarks>
        Public bookTotalDays As Integer
        ''' <summary>
        ''' The code of the operation to the find. If the client is a registered company, this code is the operator code of the company.
        ''' Only for Agency
        ''' </summary>
        ''' <remarks></remarks>
        Public tariffForClientCode As Integer = -1

        'STEP 2
        ''' <summary>
        ''' store rooms finded in previous search
        ''' </summary>
        ''' <remarks></remarks>
        <NonSerialized()> _
        Public roomsFinded As New System.Collections.Generic.Dictionary(Of String, cfr.BookedRoom)
        ''' <summary>
        ''' collection of selected rooms by user
        ''' </summary>
        ''' <remarks></remarks>
        Public roomsSelected As New System.Collections.Generic.Dictionary(Of String, cfr.BookedRoom)

        'step 3
        ''' <summary>
        ''' Client data
        ''' </summary>
        ''' <remarks></remarks>
        Public CliRes As New Cliente
        'Formas de pago seleccionada
        ''' <summary>
        ''' selected pay type
        ''' </summary>
        ''' <remarks></remarks>
        Public formapagoRes As New FormaPago
        ''' <summary>
        ''' Comments to the booking
        ''' </summary>
        ''' <remarks></remarks>
        Public Property comentarios As String
            Get
                Return Me.BookDataEF.comentarios
            End Get
            Set(value As String)
                Me.BookDataEF.comentarios = value
            End Set
        End Property

        
        Public Property noRefundTariffSelected As Boolean
            Get
                Return Me.BookDataEF.noRefundTariffSelected
            End Get
            Set(value As Boolean)
                Me.BookDataEF.noRefundTariffSelected = value
            End Set
        End Property

        Public cancelpolicy As New R_PolicyCancel


        ''' <summary>
        ''' company data
        ''' </summary>
        ''' <remarks></remarks>
        Public EmpresaRes As New Company
        ''' <summary>
        ''' Agency data
        ''' </summary>
        ''' <remarks></remarks>
        Public AgenciaRes As New Agencia


        'Compatibility mapping for historical database columns. New reservations
        'always persist empty values and no authorization consumer may use them.
        'Guest cancellation is owned by CancellationTokenService, which stores a
        'separate 256-bit token hash and lifecycle in BookingCancellationToken.
        Public Property cancellogin As String
            Get
                Return Me.BookDataEF.login
            End Get
            Set(value As String)
                Me.BookDataEF.login = value
            End Set
        End Property


        Public Property cancelClave As String
            Get
                Return Me.BookDataEF.clave
            End Get
            Set(value As String)
                Me.BookDataEF.clave = value
            End Set
        End Property

        Private carLocationSelectedvar As R_ResO_CarPickup
        Public Property carLocationSelected As R_ResO_CarPickup
            Get
                If Me.carLocationSelectedvar Is Nothing Then
                    getCarLocation()
                End If
                Return Me.carLocationSelectedvar
            End Get
            Set(value As R_ResO_CarPickup)
                Me.carLocationSelectedvar = value
            End Set
        End Property

        Public Sub getCarLocation()

            If Me.idEntity > 0 Then
                Dim ctxOwner As Boolean = Me.openCtx
                Dim id As Integer = CInt(Me.idEntity)
                Me.carLocationSelectedvar = (From t In ctx.R_ResO_CarPickup Where t.cod_reservaO = id).FirstOrDefault
                If ctxOwner = True Then
                    closeCtx()
                End If
            Else
                Me.carLocationSelectedvar = New R_ResO_CarPickup
            End If
        End Sub

        Public Function getPriceForLocation() As Double
            If Me.carLocationSelectedvar Is Nothing Then
                Throw New Exception("Dev Tip: Need to load car location values before calc the price")
            End If
            Dim priceForLocation As Double

            If Me.carLocationSelected.unit_location_Get_Price >= Me.carLocationSelected.unit_location_Return_Price Then
                priceForLocation = Me.carLocationSelected.unit_location_Get_Price
            Else
                priceForLocation = Me.carLocationSelected.unit_location_Return_Price
            End If

            Return priceForLocation
        End Function


        Public Sub New()

        End Sub


        Public Sub New(ByVal sessionId As String)
            Me.sessionId = sessionId
        End Sub

        Public Sub New(bkdataEF As BookResEF)
            Me.BookDataEF = bkdataEF
        End Sub

#Region "PROPERTIES"
        Public ReadOnly Property dateIn() As Date
            Get
                Return Me._dateIn
            End Get
        End Property

        Public ReadOnly Property dateOut() As Date
            Get
                Return Me._dateOut
            End Get
        End Property
#End Region


        'DATOS DE LA RESERVA
        'Public cod_CRS As Integer
        'Public cod_clienteRegistrado As String 'codigo del cliente
        'Public operagencia As CFRConst.ClientTypeEnum
        'Public cod_operador_freeSale As String
        'Public operadorGenerico As String 'indica si es un operador registrado pero generico para cualquiera que tenga la clave.
        'Public OperadorValidado As String 'indica si el cliente es un cliente registrado y validado.

        Private internalNoteEF As R_ReservasO_Note

        Public Property internalNote As String
            Get
                Return Me.BookDataEF.R_ReservasO_Note.Note
            End Get
            Set(value As String)
                Me.BookDataEF.R_ReservasO_Note.Note = value
            End Set
        End Property


        Public Property moneda As String
            Get
                Return Me.BookDataEF.moneda
            End Get
            Set(value As String)
                Me.BookDataEF.moneda = value
            End Set
        End Property
        Public impuestos As Double

        'Otros datos de la reserva
        'Public comisionAplicada As Double 'Comision que se aplico a esta reserva en funcion del operador que la hizo.

        ''' <summary>
        ''' set the dates of the book, checking the interval if necesary
        ''' </summary>
        ''' <param name="dateIn2"></param>
        ''' <param name="dateOut2"></param>
        ''' <param name="validateDays">True to check the range</param>
        ''' <remarks></remarks>
        Public Sub setDates(ByVal dateIn2 As Date, ByVal dateOut2 As Date, Optional validateDays As Boolean = False)

            If validateDays = True Then
                validateDaysRange(dateIn2, dateOut2)
            End If


            If dateIn2 > dateOut2 Then
                Throw New Exception(basERR.E_FAIL_VerifyDate)
            End If

            Me._dateIn = dateIn2
            Me._dateOut = dateOut2
            Me.bookTotalDays = cfr.tool.DateTools.GetDias(dateIn2, dateOut2)
        End Sub

        Public Sub validateDaysRange(ByVal dateIn2 As Date, ByVal dateOut2 As Date)

            If dateIn2.Date < Date.Now.Date Then
                Throw New Exception(basERR.E_CheckFechaActual)
            ElseIf dateIn2 > dateOut2 Then
                Throw New Exception(basERR.E_FAIL_VerifyDate)
            ElseIf dateIn2 = dateOut2 Then
                Throw New Exception(basERR.E_FAIL_VerifyDate)
            End If

            Dim days As Integer = cfr.tool.DateTools.GetDias(dateIn2, dateOut2)

            If days > 31 Then
                Throw New Exception(basERR.E_MaximumStay)
            ElseIf days < 0 Then
                Throw New Exception(basERR.E_FAIL_VerifyDate)
            End If
        End Sub


#Region "Tools"

        Public Function rbToString() As String
            Dim s As String
            s = "<br> cod_hotel=" & Me.hotel.id
            s += "<br> cod_operador=" & Me.bookOperator.cod_operador
            s += "<br> dataIn=" & Me.dateIn
            s += "<br> dataOut=" & Me.dateOut
            s += "<br> ninosfree=" & Me.ninosfree
            s += "<br> ninospago=" & Me.ninospago
            s += "<br> numAdultos=" & Me.numAdultos
            s += "<br> regimen=" & Me.regimen
            Return s

        End Function


        Public Shared Function getBookDataByID(ByRef bookID As Integer) As BookRes
            Dim bk As BookRes = cfr.BookManager.getBookResById(bookID)
            Return bk
        End Function



#End Region




        Public Sub reset()
            Dim om As New cfr.OperadorManager
            Me.CliRes = New cfr.Cliente
            Me.AgenciaRes = New Agencia
            Me.EmpresaRes = New cfr.Company
            Me.roomsSelected.Clear()
            Me.roomsFinded.Clear()
            Me.cancelClave = ""
            Me.cancellogin = ""
            Me.tariffForClientCode = cfr.CFRConst.bookTypeEnum.resIndefinido
            Me.formapagoRes = New FormaPago
            Me.comentarios = ""


        End Sub


    End Class
End Namespace
