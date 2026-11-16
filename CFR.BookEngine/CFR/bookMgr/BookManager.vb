Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports System.Linq
Imports System.Data.Entity



Namespace cfr


    Public Class BookManager
        ''' <summary>
        ''' book object with the data related with booking
        ''' </summary>
        ''' <remarks></remarks>
        Public dr As BookRes
        ''' <summary>
        ''' result with rooms for selected search criteria
        ''' </summary>
        ''' <remarks></remarks>
        Public bookResult As SearchResult
        Public bookedRoomList As Dictionary(Of String, BookedRoom)
        ''' <summary>
        ''' init the class for booking
        ''' </summary>
        ''' <param name="bookingData">book data object</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal bookingData As BookRes)
            Me.dr = bookingData
            Me.bookResult = New SearchResult
            Me.bookedRoomList = New Dictionary(Of String, BookedRoom)()

        End Sub

        ''' <summary>
        ''' Get the prices for the selected search used in <see cref=" SearchResult ">bookResult</see>
        ''' </summary>
        ''' <param name="operagencia"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetHabPricesToRes(Optional ByVal operagencia As Integer = 0) As Boolean

            If Me.CheckDaysInAdvance() = False Then
                Me.bookResult.boe.errBook = basERR.E_CheckDiasDeAntelacion
                Return False
            End If

            'check operator release dates
            If Me.CheckValidDateForRelease() = False Then
                Return False
            End If

            'If Me.checkYieldManagementMinimumDays = False Then
            '    Return False
            'End If

            'Dim oRYMc As DataView = Nothing
            'If Me.checkClosedDays(oRYMc) = False Then
            '    Return False
            'End If

            '******************************************************************************************
            'Vemos la disponibilidad del hotel
            'obtenemos las habitaciones que tienen disponibilidad,todavia no sabemos si cumplen los criterios
            'del cliente excepto el de numero de adultos
            Dim dvRoomAvailable As Data.DataView

            dvRoomAvailable = AvailabilityManager.AvailabilityByHotel(dr).DefaultView
            If dvRoomAvailable.Count = 0 Then
                Me.bookResult.boe.errBook = basERR.E_No_Hay_Disponibilidad
                Return False
            End If

            'Room data collection
            Dim hotelRoomDataCol As System.Collections.Generic.Dictionary(Of Integer, cfr.obj.HotelRoomData) = ind.bk.obj.T_Hoteles_Habitaciones_Manager.getHotelRoomColection(Me.dr.hotel.id)

            'load info for each unit available
            For Each drv As DataRowView In dvRoomAvailable
                Dim ro As cfr.obj.HotelRoomData = hotelRoomDataCol(drv(ind.bk.obj.R_Tarifa_habitacion.DBCol.cod_habitacion))
                If Me.checkRoomConditions(ro, dvRoomAvailable) = True Then
                    Dim br As New BookedRoom(ro.idEntity)
                    br.roomSelected = ro
                    Me.bookedRoomList.Add(br.id, br)
                End If
            Next

            'CHECK COMBINATED ROOMS
            For Each ro As cfr.obj.HotelRoomData In hotelRoomDataCol.Values
                If ro.cod_hab_combinada <> 0 Then
                    If Me.checkRoomConditions(ro, dvRoomAvailable) = True Then
                        Dim br As New BookedRoom(ro.idEntity)
                        br.roomSelected = ro
                        Me.bookedRoomList.Add(br.id, br)
                    End If
                End If
            Next

            dvRoomAvailable = Nothing

            If Me.bookedRoomList.Count = 0 Then 'there is no room with availability
                Me.bookResult.boe.errBook = basERR.E_No_Hay_Disponibilidad
                Return False
            End If


            Dim availableRoomWithTariffList As New System.Collections.Generic.Dictionary(Of String, BookedRoom)
            '****************************************************
            'calc tariff for each unit
            '****************************************************
            For Each bo As BookedRoom In Me.bookedRoomList.Values
                'Obtenemos el precio. Vemos la tarifa que debe coger
                Dim tariffMgr As New TariffManager(bo)
                Try
                    tariffMgr.GetTarifaForUnitType(dr, bo.roomSelected)
                    If bo.priceFinalForAllDays > 0 Then 'presentar solo las habitaciones con precio
                        availableRoomWithTariffList.Add(bo.id, bo)
                    End If
                Catch ex As Exception
                    Debug.WriteLine("Loop looking tariff error:  " & ex.Message)
                End Try
            Next

            Me.bookResult.roomInventory = availableRoomWithTariffList
            Me.bookedRoomList = availableRoomWithTariffList

            If Me.bookedRoomList.Count = 0 Then
                Me.bookResult.boe.errBook = basERR.E_No_Hay_Disponibilidad
                Return False
            End If


            Return True
        End Function
        ''' <summary>
        ''' Comprobamos que las fechas de peticion de reserva cumplen la restriccion de dias de antelacion
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CheckDaysInAdvance() As Boolean

            Dim dayMax As Date = Date.Now.Date
            If Me.dr.hotel.BookParam.daysInAdvance = 0 Then
                Return True
            End If
            dayMax = dayMax.AddDays(Me.dr.hotel.BookParam.daysInAdvance)

            'Book must do it before the maximum day (or equal)
            If Me.dr.dateIn.Date < dayMax.Date Then
                Me.bookResult.boe.errBook = basERR.E_CheckDiasDeAntelacion
                Me.bookResult.boe.statusInfo = Me.dr.hotel.BookParam.daysInAdvance
                Return False
            Else
                Return True
            End If

            Return True
        End Function
        ''' <summary>
        ''' Check the release of the operator. If there is a release the error is filled and bookstatus is filled with the number
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CheckValidDateForRelease() As Boolean

            Dim release As Integer = Me.dr.bookOperator.Release
            If release > 0 Then
                Dim dayMax As Date = Date.Now.Date
                dayMax = dayMax.AddDays(release)

                If dr.dateIn.Date < dayMax.Date Then
                    Me.bookResult.boe.errBook = basERR.E_Release
                    Me.bookResult.boe.statusInfo = release
                    Return False
                End If
            End If

            Return True
        End Function
        ''' <summary>
        ''' Check the minimum days to do the book. If fail fill the bookresult.bookerror with selected error
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function checkYieldManagementMinimumDays() As Boolean
            '  YIELD MANAGEMENT ************************************
            If Me.dr.bookOperator.aplicarYMdiasmin = False Then
                Return True
            End If

            Dim ResDiasMin As Integer
            Dim oRYM As DataTable = YieldMngMinDays.ListYieldMngDiasMin(dr.hotel.id, dr.dateIn, dr.dateOut)

            ResDiasMin = 0
            For Each drv As DataRow In oRYM.Rows
                If ResDiasMin < drv(ind.bk.obj.R_YieldMng_DiasMin.DBCol.DiasMinimos) Then ResDiasMin = drv(ind.bk.obj.R_YieldMng_DiasMin.DBCol.DiasMinimos)
            Next
            oRYM = Nothing
            If dr.bookTotalDays < ResDiasMin Then
                Me.bookResult.boe.errBook = basERR.E_YM_DiasMinimos
                Me.bookResult.boe.statusInfo = ResDiasMin
                Return False
            End If

            Return True
        End Function
        ''' <summary>
        ''' check closed days
        ''' </summary>
        ''' <param name="oRYMc">byref the dataview is filtered with rooms with closed days</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function checkClosedDays(ByRef oRYMc As DataView) As Boolean
            oRYMc = YieldMngClosedInventory.ListClosed(dr.hotel.id, dr.dateIn, dr.dateOut, dr.bookOperator.cod_operador).DefaultView

            oRYMc.RowFilter = " cod_habitacion=-1" 'All the rooms are closed so book is cancelled.
            If oRYMc.Count > 0 Then
                Me.bookResult.boe.errBook = basERR.E_ExistCierreVentas
                Return False
            End If

            Return True
        End Function

#Region "Book operation"

        Public Function NewBook(ByRef dr As BookRes) As BookOperationErrors


            Dim boe As New BookOperationErrors
            Dim sSQL As String

            'Dim ocon As SqlClient.SqlConnection = Nothing
            'Dim trans As SqlClient.SqlTransaction = Nothing

            'Dim ocon As SqlClient.SqlConnection = Nothing
            Dim trans As System.Data.Common.DbTransaction = Nothing

            Dim comisionAplicada As Double
            Dim ctx As New cfrbookEntities

            Try

                '***************Comprobamos datos necesarios
                If dr.CliRes.Nombre = "" Then boe.errBook = basERR.E_NewReserv_Nombre
                If dr.CliRes.Apellido1 = "" Then boe.errBook = basERR.E_NewReserv_apellido1

                Select Case dr.bookOperator.tipoOperador
                    Case CFRConst.ClientTypeEnum.PrivateClient, CFRConst.ClientTypeEnum.NotClasified 'particular,sin clasificar
                        If dr.CliRes.Tlf = "" Then boe.errBook = basERR.E_NewReserv_telefono
                        If dr.CliRes.email = "" Then boe.errBook = basERR.E_NewReserv_email
                        comisionAplicada = dr.hotel.BookParam.comisionyahnormal
                    Case CFRConst.ClientTypeEnum.Company 'Empresa
                        If "" = "" & dr.EmpresaRes.Nombre Then boe.errBook = basERR.E_NewReserv_Empresa
                        If "" = "" & dr.EmpresaRes.Tlf Then boe.errBook = basERR.E_NewReserv_Empresa_telefono
                        If "" = "" & dr.EmpresaRes.email Then boe.errBook = basERR.E_NewReserv_Empresa_Email
                        If "" = "" & dr.EmpresaRes.direccion Then boe.errBook = basERR.E_NewReserv_Empresa_Direcc
                        comisionAplicada = dr.hotel.BookParam.comisionyahnormal
                    Case CFRConst.ClientTypeEnum.HotelAgency 'agencia
                        'comprobamos los datos de la agencia
                        If BookManager.checkDatosOperadorGenerico(dr, boe) = False Then
                            If boe.errBook <> basERR.S_OK Then
                                Return boe
                            End If
                        End If
                        'If dr.operagencia = CFRConst.ClientTypeEnum.registeredClient Then
                        '    dr.operagencia = dr.cod_oper_tarifa
                        'End If
                        comisionAplicada = dr.hotel.BookParam.comisionyahregistrados
                End Select

                If boe.errBook <> basERR.S_OK Then
                    Return boe
                End If

                '****************************************************************
                'Guest cancellation uses a hashed, expiring, single-use token
                'issued when the confirmation email is built. The historical
                'reusable login/password fields remain empty for new bookings.
                dr.cancellogin = String.Empty
                dr.cancelClave = String.Empty


                dr.bookDate = Date.Now


                Dim expiredDate As String = dr.formapagoRes.creditCard.expiricyMonth & "/" & dr.formapagoRes.creditCard.expiricyYear.ToString.PadLeft(4, "0").Substring(2, 2)
                'Never persist CVV/CVC. It is sensitive authentication data and
                'is not required by the legacy deferred-payment record.
                With dr.BookDataEF
                    .operador = dr.bookOperator.id
                    .cod_hotel = dr.hotel.id
                    .Nombre = RequireMaximumLength(dr.CliRes.Nombre, 50, "customer name")
                    .Apellido1 = RequireMaximumLength(dr.CliRes.Apellido1, 50, "customer surname")
                    .telefono = RequireMaximumLength(dr.CliRes.Tlf, 20, "customer phone")
                    .email = RequireMaximumLength(dr.CliRes.email, 256, "customer email")
                    .driverage = dr.CliRes.driverAge
                    .pais = RequireMaximumLength(dr.CliRes.Pais.Description, 50, "customer country")
                    .moneda = RequireMaximumLength(dr.moneda, 20, "currency")
                    .impuestos = dr.impuestos
                    .cod_formapago = dr.formapagoRes.forma_pago
                    .Signature = RequireMaximumLength(If(dr.tpvData Is Nothing, String.Empty, dr.tpvData.Ds_Signature), 100, "payment signature")
                    .TPV_order = RequireMaximumLength(If(dr.tpvData Is Nothing, String.Empty, dr.tpvData.Ds_Merchant_Order), 50, "payment order")
                    .Card_type = RequireMaximumLength(dr.formapagoRes.creditCard.Card_Type, 50, "card type")
                    .nTarjeta = RequireMaximumLength(dr.formapagoRes.creditCard.ntarjeta, 20, "card number")
                    .FCaducidad = RequireMaximumLength(expiredDate, 5, "card expiry")
                    .Titular = RequireMaximumLength(dr.formapagoRes.creditCard.titular, 150, "card holder")
                    .CCUTarjeta = Nothing
                    .login = String.Empty
                    .clave = String.Empty
                    .Site = RequireMaximumLength(dr.site.id, 50, "site")
                    .HoraReserva = dr.bookDate
                    .Descoperador = RequireMaximumLength(dr.bookOperator.Operador, 50, "operator description")
                    .operadorGenerico = 0
                    .empresa = RequireMaximumLength(dr.EmpresaRes.Nombre, 50, "company name")
                    .empresadireccion = RequireMaximumLength(dr.EmpresaRes.direccion, 150, "company address")
                    .empresatlf = RequireMaximumLength(dr.EmpresaRes.Tlf, 20, "company phone")
                    .empresaemail = RequireMaximumLength(dr.EmpresaRes.email, 100, "company email")
                    .comentarios = If(dr.comentarios, String.Empty)
                    .ComisionAplicada = comisionAplicada
                    .tipoOperador = dr.bookOperator.tipoOperador
                    .CheckHotelPago = dr.formapagoRes.estadoPago
                    .language = CInt(dr.language).ToString(Globalization.CultureInfo.InvariantCulture)
                    .promotioncode = If(dr.bookOperatorPromotion Is Nothing, CType(Nothing, Integer?), dr.bookOperatorPromotion.cod_operador)
                    .noRefundTariffSelected = dr.noRefundTariffSelected
                    .chargeuntilday = RequireMaximumLength(dr.cancelpolicy.chargeuntilday, 50, "cancellation charge date")
                    .chargeuntildayvalue = dr.cancelpolicy.chargeunitldaypercent
                    .chargeafterdayvalue = dr.cancelpolicy.chargeafterdaypercent

                    If dr.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.HotelAgency Then
                        .Ag_Nombre = RequireMaximumLength(dr.AgenciaRes.Nombre, 50, "agency name")
                        .Ag_Grupo = RequireMaximumLength(dr.AgenciaRes.Grupo, 50, "agency group")
                        .Ag_Telefono = RequireMaximumLength(dr.AgenciaRes.Tlf, 50, "agency phone")
                        .Ag_Fax = RequireMaximumLength(dr.AgenciaRes.fax, 50, "agency fax")
                        .Ag_Direccion = RequireMaximumLength(dr.AgenciaRes.direccion, 150, "agency address")
                        .Ag_Cod_postal = RequireMaximumLength(dr.AgenciaRes.codpostal, 20, "agency postal code")
                        .Ag_Provincia = RequireMaximumLength(dr.AgenciaRes.Zone, 50, "agency province")
                        .Ag_Pais = RequireMaximumLength(dr.AgenciaRes.Pais.Description, 50, "agency country")
                        .Ag_email = RequireMaximumLength(dr.AgenciaRes.email, 100, "agency email")
                        .Ag_Agente = RequireMaximumLength(dr.AgenciaRes.Agente, 50, "agency agent")
                    End If
                End With




                'abrimos la conexion y comenzamos la reserva
                'ocon = ind.bk.DBMgr.getNewConn
                'comienzo de la transaccion
                'trans = ocon.BeginTransaction

                trans = ctx.Database.BeginTransaction().UnderlyingTransaction

                'CHECK IF BOOK IS REPEATED BY tpv
                If dr.formapagoRes.forma_pago = 5 And dr.tpvData.Ds_Merchant_Order <> "" Then
                    Dim ORStpv As DataTable = PayGateWayManager.BasketGetByID(dr.tpvData.Ds_Merchant_Order, trans)

                    If ORStpv.Rows.Count > 0 Then
                        If Not IsDBNull(ORStpv.Rows(0)("cod_reservaO")) Then
                            'there is a book with this ID. It means that the bank send the same response twice
                            boe.errBook = basERR.E_TPVSendSameDS_OrderRepeated
                            Throw New Exception(basERR.E_TPVSendSameDS_OrderRepeated)
                        End If

                    End If
                End If

                ctx.BookResEF.Add(dr.BookDataEF)
                ctx.SaveChanges()
                dr.idEntity = dr.BookDataEF.cod_reservaO
                If CInt(dr.idEntity) = 0 Then
                    boe.errBook = basERR.E_Get_CodReservaO_Identity_0
                    Throw New Exception(basERR.E_Get_CodReservaO_Identity_0)
                End If



                '****************************************************
                'Insertamos los datos de reserva de las habitaciones x cada habitacion
                ' restamos la disponibilidad

                For Each bo As BookedRoom In dr.roomsSelected.Values
                    Dim cupoOrigen As String = String.Empty
                    Dim iCod_habitacion As Double
                    'restamos Disponibilidad

                    ' Comprobamos precio de cada habitacion, en esta version solo se puede reservar 1 habitacion
                    'cod_hab_combinada = NewReservCHKtarifa(dr, errDesc)

                    If bo.roomSelected.cod_hab_combinada > 0 Then 'si es habitacion combinada
                        iCod_habitacion = bo.roomSelected.cod_hab_combinada
                    Else
                        iCod_habitacion = bo.roomSelected.idEntity
                    End If

                    AvailabilityManager.UpdateBookedRoomAvailability(-1, dr.bookOperator.id, dr.hotel.id, iCod_habitacion, dr.dateIn, dr.dateOut, cupoOrigen, trans)
                    '---------------------------------------------------------------------
                    Dim calculatedTariff As Double = 0
                    Dim pricedesglosedStr As String = bo.priceDesglosed
                    If dr.noRefundTariffSelected = True Then
                        calculatedTariff = bo.priceFinalForAllDaysNoRefund
                        pricedesglosedStr = bo.getPriceNoRefundDesglosedString
                    Else
                        'pricedesglosedStr remain same price
                        calculatedTariff = bo.priceFinalForAllDays
                    End If

                    'insertamos Datos de la habitacion
                    sSQL = "INSERT INTO R_ResO_Hab " &
                           "(cod_reservaO, fecha_entrada, fecha_salida, num_habitaciones, num_personas, regimen_aloj, " &
                           "tipo_habitacion, descHabitacion, edadnino1, edadnino2, preciofinal, cupoOrigen, " &
                           "cod_hab_combinada, PrecioDesglosado, RegimenDesglosado) " &
                           "VALUES (@bookingId, @checkIn, @checkOut, @roomCount, @people, @board, @roomType, " &
                           "@description, @childrenFree, @childrenPaid, @finalPrice, @quotaOrigin, @combinedRoom, " &
                           "@priceBreakdown, @boardBreakdown)"

                    ind.bk.DBMgr.Execute(
                        sSQL,
                        trans,
                        New SqlClient.SqlParameter("@bookingId", SqlDbType.Int) With {.Value = CInt(dr.idEntity)},
                        New SqlClient.SqlParameter("@checkIn", SqlDbType.DateTime) With {.Value = dr.dateIn},
                        New SqlClient.SqlParameter("@checkOut", SqlDbType.DateTime) With {.Value = dr.dateOut},
                        New SqlClient.SqlParameter("@roomCount", SqlDbType.Int) With {.Value = 1},
                        New SqlClient.SqlParameter("@people", SqlDbType.Int) With {.Value = CInt(dr.numAdultos) + CInt(dr.ninosfree) + CInt(dr.ninospago)},
                        New SqlClient.SqlParameter("@board", SqlDbType.VarChar, 50) With {.Value = CInt(dr.regimen).ToString(Globalization.CultureInfo.InvariantCulture)},
                        New SqlClient.SqlParameter("@roomType", SqlDbType.Int) With {.Value = CInt(bo.roomSelected.idEntity)},
                        New SqlClient.SqlParameter("@description", SqlDbType.NVarChar, 50) With {.Value = RequireMaximumLength(bo.roomSelected.description.getDes(dr.language), 50, "room description")},
                        New SqlClient.SqlParameter("@childrenFree", SqlDbType.TinyInt) With {.Value = dr.ninosfree},
                        New SqlClient.SqlParameter("@childrenPaid", SqlDbType.TinyInt) With {.Value = dr.ninospago},
                        New SqlClient.SqlParameter("@finalPrice", SqlDbType.Float) With {.Value = calculatedTariff},
                        New SqlClient.SqlParameter("@quotaOrigin", SqlDbType.VarChar, 400) With {.Value = RequireMaximumLength(cupoOrigen, 400, "quota origin")},
                        New SqlClient.SqlParameter("@combinedRoom", SqlDbType.Int) With {.Value = bo.roomSelected.cod_hab_combinada},
                        New SqlClient.SqlParameter("@priceBreakdown", SqlDbType.VarChar, 900) With {.Value = RequireMaximumLength(pricedesglosedStr, 900, "price breakdown")},
                        New SqlClient.SqlParameter("@boardBreakdown", SqlDbType.VarChar, 150) With {.Value = RequireMaximumLength(bo.RegimenDesglosed, 150, "board breakdown")})



                    If Me.dr.hotel.Tipo = CFRConst.unitTypeEnum.car Then
                        Me.dr.carLocationSelected.cod_reservaO = dr.idEntity
                        ctx.R_ResO_CarPickup.Add(Me.dr.carLocationSelected)
                        ctx.SaveChanges()
                        'End If
                    End If

                Next

                If Not dr.tpvData Is Nothing And dr.tpvData.Ds_Merchant_Order.Trim <> "" Then
                    PayGateWayManager.UpdateBasketCodReserva(dr.tpvData.Ds_Merchant_Order, dr.idEntity, trans)
                End If


                '****************************************************
                If boe.errBook <> basERR.S_OK Then
                    'ctx.Database.CurrentTransaction.Rollback()
                    trans.Rollback()
                Else
                    'ctx.Database.CurrentTransaction.Commit()
                    trans.Commit()
                End If

                'indicates that the user must find again and can´t back and do a book again.
                dr.uniqueBookId = 0


                Return boe
            Catch ex As Exception
                boe.exceptionThrowed = ex

                If Not trans Is Nothing Then
                    'ctx.Database.CurrentTransaction.Rollback()
                    trans.Rollback()
                End If
            Finally
                'ind.bk.DBMgr.closeConn(ocon)
                ctx.Dispose()
            End Try

            Return boe

        End Function


#End Region


#Region "helpers"
        Private Function GenClave() As String
            Randomize()
            Dim letra As Short = 0
            Dim clave As String = String.Empty
generloginyclave:
            letra = Int((122 * Rnd()) + 1) ' Genera valores aleatorios entre 1 y 122.
            Select Case letra
                Case 48 To 57, 65 To 90, 97 To 122
                    If Len(clave) < 12 Then
                        clave = clave & Chr(letra)
                        GoTo generloginyclave
                    Else
                        GoTo fin
                    End If
                Case Else
                    GoTo generloginyclave
            End Select
fin:
            Return clave
        End Function

        ''' <summary>
        ''' check hotel conditions for selected room. Yield management closed days
        ''' </summary>
        ''' <param name="RoomInfo"></param>
        ''' <param name="dvAvailability"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function checkRoomConditions(ByRef RoomInfo As cfr.obj.HotelRoomData _
        , ByVal dvAvailability As Data.DataView
        ) As Boolean

            'number of total persons that can enter in the room to validate if the selected room can host them.
            Dim bookTotalPaxByHab As Integer

            'Check persons restrictions
            If CInt(RoomInfo.NinoOcupaplaza) = CInt(CFRConst.ChildrenUseBed.Yes) Then
                bookTotalPaxByHab = Me.dr.numAdultos + Me.dr.ninosfree + Me.dr.ninospago
            ElseIf CInt(RoomInfo.NinoOcupaplaza) = CInt(CFRConst.ChildrenUseBed.No) Then
                bookTotalPaxByHab = Me.dr.numAdultos + Me.dr.ninospago
            End If

            'si cumple las restricciones lo añadimos al recordset de salida
            If RoomInfo.MaxPersonas >= bookTotalPaxByHab And RoomInfo.MaxAdultos >= Me.dr.numAdultos Then
                'return true
            Else
                Return False
            End If

            '-----------------Yield management-------------
            'Control closed days
            'oRYMc.RowFilter = " cod_habitacion=" & RoomInfo.idEntity
            'If oRYMc.Count > 0 Then 'This room is closed.
            '    Return False
            'End If


            ''Control closed days for combinated rooms
            'If RoomInfo.cod_hab_combinada <> "0" Then
            '    oRYMc.RowFilter = " cod_habitacion=" & RoomInfo.cod_hab_combinada
            '    If oRYMc.Count > 0 Then 'This room is closed.
            '        Return False
            '    End If
            '    'control availability for combinated room
            '    dvAvailability.RowFilter = " cod_habitacion=" & RoomInfo.cod_hab_combinada
            '    If dvAvailability.Count = 0 Then
            '        Return False
            '    End If
            'End If


            'none of the restrictions is false
            Return True

        End Function


        Private Shared Function checkDatosOperadorGenerico(ByRef dr As BookRes, ByRef boe As BookOperationErrors) As Boolean
            Try

                If Len(Trim(dr.AgenciaRes.nombre)) = 0 Then boe.errBook = basERR.E_NewReserv_Datos_OperadorGenerico
                If Len(Trim(dr.AgenciaRes.tlf)) = 0 Then boe.errBook = basERR.E_NewReserv_Datos_OperadorGenerico
                If Len(Trim(dr.AgenciaRes.fax)) = 0 Then boe.errBook = basERR.E_NewReserv_Datos_OperadorGenerico
                If Len(Trim(dr.AgenciaRes.direccion)) = 0 Then boe.errBook = basERR.E_NewReserv_Datos_OperadorGenerico
                'If Len(Trim(dr.AgenciaRes.cod_postal)) = 0 Then boe.errBook = basERR.E_NewReserv_Datos_OperadorGenerico
                'If Len(Trim(dr.AgenciaRes.zone)) = 0 Then boe.errBook = basERR.E_NewReserv_Datos_OperadorGenerico
                If Len(Trim(dr.AgenciaRes.Pais.description)) = 0 Then boe.errBook = basERR.E_NewReserv_Datos_OperadorGenerico
                If Len(Trim(dr.AgenciaRes.email)) = 0 Then boe.errBook = basERR.E_NewReserv_Datos_OperadorGenerico
                If Len(Trim(dr.AgenciaRes.agente)) = 0 Then boe.errBook = basERR.E_NewReserv_Datos_OperadorGenerico
                If boe.errBook = basERR.S_OK Then
                    Return True
                Else
                    boe.setMsg(cfr.Language.getCaption(366), True)
                    Return False
                End If

            Catch ex As Exception
                Return False
            End Try

        End Function
#End Region

        Public Shared Function getCodReservaByOrderAndSignature(ByVal Ds_Order As String, ByVal Ds_Signature As String) As String
            If String.IsNullOrWhiteSpace(Ds_Order) OrElse String.IsNullOrWhiteSpace(Ds_Signature) Then
                Throw New Exception(basERR.E_PAGONOREALIZADOBYUSER)
            End If

            Using ctx As New cfrbookEntities
                Dim bookingId As Integer =
                    (From booking In ctx.BookResEF
                     Where booking.TPV_order = Ds_Order AndAlso booking.Signature = Ds_Signature
                     Select booking.cod_reservaO).FirstOrDefault()
                If bookingId > 0 Then
                    Return bookingId.ToString()
                End If
            End Using

            Throw New Exception(basERR.E_PAGONOREALIZADOBYUSER)
        End Function

        Public Shared Function getRoomsReservedByBookId(ByVal idBook As String) As DataTable
            Dim bookingId As Integer
            If Not Integer.TryParse(idBook, bookingId) Then
                Throw New ArgumentException("The booking identifier is invalid.", NameOf(idBook))
            End If
            Const sSQL As String = "SELECT * FROM R_ResO_Hab WHERE cod_reservaO=@bookingId"
            Dim dt As DataTable = ind.bk.DBMgr.getDT(
                sSQL,
                New SqlClient.SqlParameter("@bookingId", SqlDbType.Int) With {.Value = bookingId})
            Return dt
        End Function

        ''' <summary>
        ''' Get the book information by code
        ''' </summary>
        ''' <param name="idBook"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBookedById(ByVal idBook As String) As Data.DataTable
            Dim sSQL As String
            sSQL = "SELECT *, fecha_entrada as fecha1 "
            sSQL += " ,fecha_Salida as fecha2 "
            sSQL += " ,convert(varchar(20),horareserva,113)as fechareserva "
            sSQL += " ,convert(varchar(20),HoraCancelacion,113)as fechacancelacion "
            sSQL += " ,convert(varchar(20),HoraConfirmacion,113)as fechaConfirmacion"
            sSQL += " ,convert(varchar(20),Horamodificacion,113)as fechamodificacion"
            sSQL += " FROM (R_ReservasO INNER JOIN"
            sSQL += " R_ResO_Hab ON R_ReservasO.cod_reservaO = R_ResO_Hab.cod_reservaO)"
            sSQL += " WHERE R_ReservasO.cod_reservaO=@bookingId"

            Dim bookingId As Integer
            If Not Integer.TryParse(idBook, bookingId) Then
                Throw New ArgumentException("The booking identifier is invalid.", NameOf(idBook))
            End If
            Dim dt As DataTable = ind.bk.DBMgr.getDT(
                sSQL,
                New SqlClient.SqlParameter("@bookingId", SqlDbType.Int) With {.Value = bookingId})
            Return dt
        End Function

        Public Shared Function GetBookedDatatableByEmailClient(ByVal email As String) As Data.DataTable
            Dim sSQL As String
            sSQL = "SELECT *, fecha_entrada as fecha1 "
            sSQL += " ,fecha_Salida as fecha2 "
            sSQL += " ,convert(varchar(20),horareserva,113)as fechareserva "
            sSQL += " ,convert(varchar(20),HoraCancelacion,113)as fechacancelacion "
            sSQL += " ,convert(varchar(20),HoraConfirmacion,113)as fechaConfirmacion"
            sSQL += " ,convert(varchar(20),Horamodificacion,113)as fechamodificacion"
            sSQL += " FROM (R_ReservasO INNER JOIN"
            sSQL += " R_ResO_Hab ON R_ReservasO.cod_reservaO = R_ResO_Hab.cod_reservaO)"
            sSQL += " WHERE R_ReservasO.email=@email"

            Dim dt As DataTable = ind.bk.DBMgr.getDT(
                sSQL,
                New SqlClient.SqlParameter("@email", SqlDbType.NVarChar, 256) With {
                    .Value = RequireMaximumLength(email, 256, "customer email")})
            Return dt
        End Function

        ''' <summary>
        ''' sin probar
        ''' </summary>
        ''' <param name="email"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getBookResByEmailClient(ByVal email As String) As BookRes
            

            email = "" & email
            If email Is Nothing Or email = "" Then
                Throw New Exception("book email is blank")
            End If


            'Dim dt As DataTable = cfr.BookManager.GetBookedDatatableByEmailClient(email)
            Dim bkdata As BookResEF
            Using ctx As New cfrbookEntities
                bkdata = (From s In ctx.BookResEF
                                       Where s.email = email
                                       Select s).FirstOrDefault
            End Using

            Return getBookResByEFObject(bkdata)
        End Function
        ''' <summary>
        '''  Get bookres object filled with needed data and with BookResEF as entity framework behind
        ''' </summary>
        ''' <param name="idBook"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getBookResById(ByVal idBook As String) As BookRes
            idBook = "" & idBook
            If idBook Is Nothing Or idBook = "" Then
                Throw New Exception("book id is blank.")
            End If


            'Dim dt As DataTable = cfr.BookManager.GetBookedById(idBook)
            Dim bookingId As Integer
            If Not Integer.TryParse(idBook, bookingId) Then
                Throw New ArgumentException("The booking identifier is invalid.", NameOf(idBook))
            End If
            Dim bkdata As BookResEF

            Using ctx As New cfrbookEntities
                bkdata = (From s In ctx.BookResEF.Include("R_ReservasO_Note")
                                       Where s.cod_reservaO = bookingId
                                      Select s).FirstOrDefault
                If bkdata IsNot Nothing Then
                    bkdata.R_ResO_Hab.FirstOrDefault()
                End If
            End Using



            Return getBookResByEFObject(bkdata)
        End Function

        ''' <summary>
        ''' Get bookres object filled with needed data and with BookResEF as entity framework behind
        ''' </summary>
        ''' <param name="bkdata"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getBookResByEFObject(bkdata As BookResEF) As BookRes

            If bkdata Is Nothing Then
                Throw New Exception(cfr.Language.GetCaption(177))
            End If

            Dim dr As New BookRes(bkdata)
            Dim bkUnitSel As BookUnit
            bkUnitSel = bkdata.R_ResO_Hab.FirstOrDefault

            Dim dateIn As New Date()

            'dr.idEntity = oRs(ind.bk.obj.R_ReservasO.DBCol.cod_reservaO)
            dr.setDates(bkdata.R_ResO_Hab.FirstOrDefault.fecha_entrada, bkdata.R_ResO_Hab.FirstOrDefault.fecha_salida)

            dr.ninosfree = cfr.tool.Tools.cNull(bkUnitSel.edadnino1)
            dr.ninospago = cfr.tool.Tools.cNull(bkUnitSel.edadnino2)
            dr.numAdultos = bkUnitSel.num_personas - dr.ninosfree - dr.ninospago
            dr.regimen = cfr.tool.Tools.cNull(bkUnitSel.regimen_aloj)

            'hotel
            dr.hotel = cfr.mgr.HotelMgr.GetEntityFull(bkdata.cod_hotel)

            If dr.hotel.PrivateoperatorObj.cod_operador = bkdata.operador Then
                'operator of the book
                dr.bookOperator = dr.hotel.PrivateoperatorObj
            Else
                'operator of the book
                dr.bookOperator = OperadorManager.GetOperador(bkdata.operador)
            End If

            'paid type
            dr.formapagoRes.forma_pago = cfr.tool.Tools.cNull(bkdata.cod_formapago)
            'if book was paid by bank paygate
            dr.tpvData.Ds_Merchant_Order = cfr.tool.Tools.cNull(bkdata.TPV_order)


            'CLIENT DATA
            dr.CliRes.Nombre = cfr.tool.Tools.cNull(bkdata.Nombre)
            dr.CliRes.Apellido1 = cfr.tool.Tools.cNull(bkdata.Apellido1)
            dr.CliRes.Tlf = cfr.tool.Tools.cNull(bkdata.telefono)
            dr.CliRes.email = cfr.tool.Tools.cNull(bkdata.email)
            dr.CliRes.Pais.Description = cfr.tool.Tools.cNull(bkdata.pais)
            dr.CliRes.driverAge = cfr.tool.Tools.cNull(bkdata.driverage, 0)

            'AGENCY DATA
            dr.AgenciaRes.Nombre = cfr.tool.Tools.cNull(bkdata.Ag_Nombre)
            dr.AgenciaRes.Grupo = cfr.tool.Tools.cNull(bkdata.Ag_Grupo)
            dr.AgenciaRes.Tlf = cfr.tool.Tools.cNull(bkdata.Ag_Telefono)
            dr.AgenciaRes.fax = cfr.tool.Tools.cNull(bkdata.Ag_Fax)
            dr.AgenciaRes.direccion = cfr.tool.Tools.cNull(bkdata.Ag_Direccion)
            dr.AgenciaRes.codpostal = cfr.tool.Tools.cNull(bkdata.Ag_Cod_postal)
            dr.AgenciaRes.Zone = cfr.tool.Tools.cNull(bkdata.Ag_Provincia)
            dr.AgenciaRes.Pais.Description = cfr.tool.Tools.cNull(bkdata.Ag_Pais)
            dr.AgenciaRes.email = cfr.tool.Tools.cNull(bkdata.Ag_email)
            dr.AgenciaRes.Agente = cfr.tool.Tools.cNull(bkdata.Ag_Agente)

            'COMPANY DATA
            dr.EmpresaRes.Nombre = cfr.tool.Tools.cNull(bkdata.empresa)
            dr.EmpresaRes.direccion = cfr.tool.Tools.cNull(bkdata.empresadireccion)
            dr.EmpresaRes.Tlf = cfr.tool.Tools.cNull(bkdata.empresatlf)
            dr.EmpresaRes.email = cfr.tool.Tools.cNull(bkdata.empresaemail)
            dr.EmpresaRes.fax = ""

            'cancel policy
            dr.cancelpolicy.chargeuntilday = cfr.tool.Tools.cNull(bkdata.chargeuntilday, "-1")
            dr.cancelpolicy.chargeunitldaypercent = CDbl(cfr.tool.Tools.cNull(bkdata.chargeuntildayvalue, 0))
            dr.cancelpolicy.chargeafterdaypercent = cfr.tool.Tools.cNull(bkdata.chargeafterdayvalue, 0)


            Dim dtRoomsBooked As DataTable = cfr.BookManager.getRoomsReservedByBookId(dr.idEntity)

            For Each drRoom As BookUnit In bkdata.R_ResO_Hab
                Dim bo As New BookedRoom(drRoom.cod_reso_hab)
                bo.priceFinalForAllDays = cfr.tool.Tools.cNull(drRoom.preciofinal)
                bo.priceDesglosed = cfr.tool.Tools.cNull(drRoom.PrecioDesglosado)
                bo.RegimenDesglosed = cfr.tool.Tools.cNull(drRoom.RegimenDesglosado)
                bo.bookState = cfr.tool.Tools.cNull(drRoom.Estado, CFRConst.bookStateEnum.Active)
                bo.originalAvailability = cfr.tool.Tools.cNull(drRoom.cupoOrigen)
                bo.bookCancelDate = cfr.tool.Tools.cNull(drRoom.HoraCancelacion, Date.Now)
                bo.bookCancelBy = cfr.tool.Tools.cNull(drRoom.Canceladax)
                Dim room As New cfr.obj.HotelRoomData
                Try
                    'maybe the room do not exist anymore. we need som data
                    room = ind.bk.obj.T_Hoteles_Habitaciones_Manager.GetEntity(drRoom.tipo_habitacion)
                Catch ex As Exception
                    room = New cfr.obj.HotelRoomData
                    room.idEntity = drRoom.tipo_habitacion 'the code of the room in the saved book, maybe not exist more in the hotel book, but is necesary for reports, etc.
                    room.description.setLangDes(cfr.tool.Tools.cNull(drRoom.DescHabitacion))
                    room.cod_hab_combinada = cfr.tool.Tools.cNull(drRoom.cod_hab_combinada, "0")
                End Try

                bo.roomSelected = room
                'IMPORTANT, concept: when a room is combinated, in the future may be not, so we try to substract for the actual.
                dr.roomsSelected.Add(bo.id, bo)
            Next

            'base page gets the site using request.
            dr.site = SiteObj.getSite(cfr.tool.Tools.cNull(bkdata.Site))


            Return dr

        End Function


        ''' <summary>
        ''' Cancels a booking after the caller acquired its single-use guest token.
        ''' </summary>
        ''' <remarks>
        ''' The public cancellation endpoint is the only consumer. It must hold a
        ''' CancellationTokenLease. Booking-state and date rules remain authoritative.
        ''' </remarks>
        Public Shared Function CancelBookByAuthorizedGuest(ByVal idBook As String) As BookRes
            Return CancelBookCore(idBook)
        End Function

        Private Shared Function CancelBookCore(ByVal idBook As String) As BookRes
            '***********************************************************************
            '    estado=0 reserva normal
            '    estado=1 cancelada x cliente
            '    estado=2 noshow
            '    estado=3 modificada
            '    estado=4 cancelada x usuario
            '    estado=5 confirmada
            '    estado=6 cancelada x operador

            Dim oCon As SqlClient.SqlConnection = Nothing
            Dim trans As SqlClient.SqlTransaction = Nothing



            Dim cancelDate As Date = Date.Now
            Try
                'bookData
                Dim bd As BookRes = Nothing

                bd = cfr.BookManager.getBookResById(idBook)
                'if book don´t exist method base throw an exception
                'we control here anyway
                If bd Is Nothing Then
                    Throw New Exception(cfr.Language.GetCaption(177))
                End If



                If cancelDate.Date > bd.dateIn.Date Then
                    Throw New Exception(cfr.Language.GetCaption(301))
                ElseIf cancelDate.Date = bd.dateIn.Date Then
                    'if is in the same day check the hour was made, if difference is less than a couple of hours then allow to cancel
                    'allow cancel before hotel checkin

                    If cancelDate.Hour <= 12 Then
                        'allow cancel
                    Else
                        'allow if difference is less than 2 hours
                        Dim ts As TimeSpan = cancelDate - bd.bookDate
                        If ts.TotalMinutes < 120 Then
                            'allow
                        Else
                            Throw New Exception(cfr.Language.GetCaption(301))
                        End If
                    End If



                End If

                oCon = ind.bk.DBMgr.getNewConn
                trans = oCon.BeginTransaction

                Dim estadoX As String = "Cliente"
                Dim newBookState As cfr.CFRConst.bookStateEnum = CFRConst.bookStateEnum.CancelByClient


                'in this version the user cancel all the book
                For Each bo As BookedRoom In bd.roomsSelected.Values
                    If bo.bookState = CFRConst.bookStateEnum.Active Or bo.bookState = CFRConst.bookStateEnum.confirmedByHotel Then 'si esta activa o confirmada puede cancelarla.
                        Dim roomIdToUpdateAvailability As String
                        If bo.roomSelected.cod_hab_combinada > 0 Then
                            roomIdToUpdateAvailability = bo.roomSelected.cod_hab_combinada
                        Else
                            roomIdToUpdateAvailability = bo.roomSelected.idEntity
                        End If

                        Try
                            cfr.AvailabilityManager.UpdateBookedRoomAvailability(1, bd.bookOperator.id, bd.hotel.id, roomIdToUpdateAvailability, bd.dateIn, bd.dateOut, bo.originalAvailability, trans)
                        Catch ex As Exception
                            If ex.Message = basERR.E_NoDisponibility Then
                                'try with actual room
                                cfr.AvailabilityManager.UpdateBookedRoomAvailability(1, bd.bookOperator.id, bd.hotel.id, bo.roomSelected.idEntity, bd.dateIn, bd.dateOut, bo.originalAvailability, trans)
                            Else
                                Throw ex
                            End If
                        End Try

                    ElseIf bo.bookState = CFRConst.bookStateEnum.CancelByClient Or bo.bookState = CFRConst.bookStateEnum.CancelByHotelUser Or bo.bookState = CFRConst.bookStateEnum.canceledByOperator Then
                        Throw New Exception(cfr.Language.GetCaption(182))
                    ElseIf bo.bookState = CFRConst.bookStateEnum.NoShow Then
                        Throw New Exception(cfr.Language.GetCaption(182))
                    Else
                        Throw New Exception(cfr.Language.GetCaption(182))
                    End If


                    cfr.BookManager.UpdateBookState(idBook, bo.roomSelected.idEntity, newBookState, cancelDate, estadoX, trans)

                Next



                trans.Commit()

                Return bd
            Catch ex As Exception
                Try
                    If Not trans Is Nothing Then
                        trans.Rollback()
                    End If

                Catch exTran As Exception

                End Try
                Throw ex
            Finally
                ind.bk.DBMgr.closeConn(oCon)

            End Try

        End Function

        Public Shared Sub UpdateBookState(ByVal idBook As String, ByVal idRoom As String, ByVal bookState As cfr.CFRConst.bookStateEnum _
                , ByVal modificationDate As Date, ByVal estadoX As String, ByVal trans As SqlClient.SqlTransaction)


            Dim bookingId As Integer
            Dim roomId As Integer
            If Not Integer.TryParse(idBook, bookingId) OrElse Not Integer.TryParse(idRoom, roomId) Then
                Throw New ArgumentException("The booking or room identifier is invalid.")
            End If
            If Not System.Enum.IsDefined(GetType(cfr.CFRConst.bookStateEnum), bookState) Then
                Throw New ArgumentOutOfRangeException(NameOf(bookState))
            End If

            Dim sSQL As String = "UPDATE R_ResO_hab SET Estado=@state"
            Dim actorMaximumLength As Integer = 50
            Select Case bookState
                Case CFRConst.bookStateEnum.CancelByClient, CFRConst.bookStateEnum.NoShow, CFRConst.bookStateEnum.CancelByHotelUser
                    sSQL += ", canceladax=@actor, horacancelacion=@modifiedAt"
                Case CFRConst.bookStateEnum.confirmedByHotel
                    sSQL += ", confirmadax=@actor, horaconfirmacion=@modifiedAt"
                    actorMaximumLength = 100
            End Select

            sSQL += " WHERE cod_reservaO=@bookingId AND " & cfr.tables.R_ResO_Hab.tipo_habitacion & "=@roomId"
            ind.bk.DBMgr.Execute(
                sSQL,
                trans,
                New SqlClient.SqlParameter("@state", SqlDbType.TinyInt) With {.Value = CByte(bookState)},
                New SqlClient.SqlParameter("@actor", SqlDbType.VarChar, actorMaximumLength) With {.Value = RequireMaximumLength(estadoX, actorMaximumLength, "booking state actor")},
                New SqlClient.SqlParameter("@modifiedAt", SqlDbType.DateTime) With {.Value = modificationDate},
                New SqlClient.SqlParameter("@bookingId", SqlDbType.Int) With {.Value = bookingId},
                New SqlClient.SqlParameter("@roomId", SqlDbType.Int) With {.Value = roomId})

        End Sub

        Private Shared Function RequireMaximumLength(ByVal value As String,
                                                     ByVal maximumLength As Integer,
                                                     ByVal fieldName As String) As String
            Dim normalized As String = If(value, String.Empty)
            If normalized.Length > maximumLength Then
                Throw New ArgumentException(fieldName & " exceeds its database limit of " & maximumLength & " characters.")
            End If
            Return normalized
        End Function






    End Class
End Namespace
