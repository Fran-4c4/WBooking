Imports System.Data
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports Newtonsoft.Json

Namespace ind.bk.reports



    Public Class ReportBooks

        Public Enum searchTypeEnum As Byte
            byBookID = 0
            byBookIDProperty = 1
            byCustomerName = 2
            byCustomerEmail = 3
        End Enum

        Public Function GetReservTOTALBy(Cod_Hotel As Integer, holdPropertyType As cfr.CFRConst.unitTypeEnum _
        , searchType As searchTypeEnum, searchCondition As Object _
        , Optional estado As Integer = -1) As DataView
            ' **********************************************************************
            ' Obtenemos un Rs con TODOS los datos de la reserva.
            '***********************************************************************

            Dim sSQL As String

            sSQL = "SELECT *,convert(varchar(10),fecha_entrada,103)as fecha1, fecha_entrada as dateIN"
            sSQL = sSQL & " ,convert(varchar(10),fecha_Salida,103)as fecha2 , fecha_Salida as dateOUT"
            sSQL = sSQL & " ,convert(varchar(20),horareserva,113)as fechareserva "
            sSQL = sSQL & " ,convert(varchar(20),HoraCancelacion,113)as fechacancelacion "
            sSQL = sSQL & " ,convert(varchar(20),HoraConfirmacion,113)as fechaConfirmacion"
            sSQL = sSQL & " ,convert(varchar(20),Horamodificacion,113)as fechamodificacion"

            fillSqlForCarCompany(sSQL, holdPropertyType, 0)

            sSQL = sSQL & " FROM (R_ReservasO INNER JOIN"
            sSQL = sSQL & " R_ResO_Hab ON R_ReservasO.cod_reservaO = R_ResO_Hab.cod_reservaO)"
            'sSQL = sSQL & " INNER JOIN R_operadores ON R_operadores.cod_operador= R_ReservasO.operador   "

            fillSqlForCarCompany(sSQL, holdPropertyType, 1)

            sSQL = sSQL & " Where R_ReservasO.cod_hotel = " & Cod_Hotel

            fillSqlForCarCompany(sSQL, holdPropertyType, 2)

            If estado <> "-1" Then 'ESTADO DE LA RESERVA, ACTIVA, CANCELADA, ETC
                If estado = "1" Then 'all cancel states
                    sSQL = sSQL & " AND R_ResO_Hab.Estado IN (1,2,4,6)"
                Else
                    sSQL = sSQL & " AND R_ResO_Hab.Estado in(" & estado & ")"
                End If
            End If

            If searchType = searchTypeEnum.byBookID Then 'por codigo interno de reserva
                sSQL = sSQL & " AND R_ReservasO.cod_reservaO ='" & cfr.tool.Tools.ParserSql(searchCondition) & "'"
            ElseIf searchType = searchTypeEnum.byBookIDProperty Then 'por codigo externo de reserva
                sSQL = sSQL & " AND R_ReservasO.cod_reserva_hotel like '%" & cfr.tool.Tools.ParserSql(searchCondition) & "%'"
            ElseIf searchType = searchTypeEnum.byCustomerName Then 'por apellidos de cliente

                Dim surname As String = cfr.tool.Tools.ParserSql(searchCondition)

                sSQL = sSQL & " AND R_ReservasO.apellido1 like '%" & surname & "%'"

            ElseIf searchType = searchTypeEnum.byCustomerEmail Then 'por email de cliente
                sSQL = sSQL & " AND R_ReservasO.email like '%" & cfr.tool.Tools.ParserSql(searchCondition) & "%'"
            End If

            sSQL = sSQL & " ORDER BY R_ResO_Hab.fecha_entrada, R_ReservasO.HoraReserva "



            Dim dt As DataTable = ind.bk.DBMgr.getDT(sSQL)
            Return dt.DefaultView



        End Function



        Public Function GetReservTOTAL(cod_hotel As Integer, datein As Date, dateOut As Date, holdPropertyType As cfr.CFRConst.unitTypeEnum _
        , Optional estado As String = "-1") As DataView
            ' **********************************************************************
            ' Obtenemos un Rs con TODOS los datos de la reserva. Pasandole el codigo
            '***********************************************************************

            Dim sSQL As String

            Dim fecha1Str As String = datein.ToString("yyyyMMdd")
            Dim fecha2Str As String = dateOut.ToString("yyyyMMdd")


            sSQL = "SELECT *,convert(varchar(10),fecha_entrada,103)as fecha1, fecha_entrada as dateIN"
            sSQL = sSQL & " ,convert(varchar(10),fecha_Salida,103)as fecha2 , fecha_Salida as dateOUT"
            sSQL = sSQL & " ,convert(varchar(20),horareserva,113)as fechareserva "
            sSQL = sSQL & " ,convert(varchar(20),HoraCancelacion,113)as fechacancelacion "
            sSQL = sSQL & " ,convert(varchar(20),HoraConfirmacion,113)as fechaConfirmacion"
            sSQL = sSQL & " ,convert(varchar(20),Horamodificacion,113)as fechamodificacion"

            fillSqlForCarCompany(sSQL, holdPropertyType, 0)

            sSQL = sSQL & " FROM (R_ReservasO INNER JOIN"
            sSQL = sSQL & " R_ResO_Hab ON R_ReservasO.cod_reservaO = R_ResO_Hab.cod_reservaO)"
            'sSQL = sSQL & " INNER JOIN R_operadores ON R_operadores.cod_operador= R_ReservasO.operador   "

            fillSqlForCarCompany(sSQL, holdPropertyType, 1)

            '---------------WHERE SECTION
            sSQL = sSQL & " Where R_ReservasO.cod_hotel = " & cod_hotel

            fillSqlForCarCompany(sSQL, holdPropertyType, 2)


            If estado <> -1 Then 'ESTADO DE LA RESERVA, ACTIVA, CANCELADA, ETC
                If estado = 1 Then
                    sSQL = sSQL & " AND R_ResO_Hab.Estado IN (1,2,4,6)"
                Else
                    sSQL = sSQL & " AND R_ResO_Hab.Estado IN (" & estado & ")"
                End If
            End If

            'SUBCONSULTA POR CULPA DEL JODIDO OR
            sSQL = sSQL & " AND R_ReservasO.cod_reservaO IN (SELECT cod_reservaO FROM R_ResO_Hab WHERE"
            sSQL = sSQL & " (R_ResO_Hab.fecha_entrada >=CONVERT(DATETIME,'" & fecha1Str & "',113) "
            sSQL = sSQL & " AND R_ResO_Hab.fecha_entrada<=CONVERT(DATETIME,'" & fecha2Str & "',113))"
            sSQL = sSQL & " OR (R_ResO_Hab.fecha_Salida <=CONVERT(DATETIME,'" & fecha2Str & "',113) "
            sSQL = sSQL & " AND R_ResO_Hab.fecha_Salida>=CONVERT(DATETIME,'" & fecha1Str & "',113)))"

            sSQL = sSQL & " ORDER BY R_ResO_Hab.fecha_entrada, R_ReservasO.HoraReserva "


            Dim dt As DataTable = ind.bk.DBMgr.getDT(sSQL)
            Return dt.DefaultView


        End Function


        Private Function fillSqlForCarCompany(ByRef sqlSearch As String, holdPropertyType As CFRConst.unitTypeEnum, sqlSection As Integer) As String
            If holdPropertyType <> CFRConst.unitTypeEnum.car Then
                Return ""
            End If
            Dim s As String = ""
            Select Case sqlSection
                Case 0
                    s = " ,R_ResO_CarPickup.unit_location_GET, R_ResO_CarPickup.unit_location_Get_Price, R_ResO_CarPickup.unit_location_Return, " _
                     & " R_ResO_CarPickup.unit_location_Return_Price, R_UnitLocation_Lang.id_Lang, R_UnitLocation_Lang.Description AS descriptionLocationGet, " _
                     & " R_UnitLocation_Lang_1.Description AS descriptionLocationReturn "
                    sqlSearch += s

                Case 1
                    s = " INNER JOIN R_ResO_CarPickup ON R_ReservasO.cod_reservaO = R_ResO_CarPickup.cod_reservaO " _
                        & "INNER JOIN R_UnitLocation ON R_ResO_CarPickup.unit_location_GET = R_UnitLocation.id_unit_location INNER JOIN " _
                      & "R_UnitLocation_Lang ON R_UnitLocation.id_unit_location = R_UnitLocation_Lang.id_unit_location INNER JOIN " _
                      & "R_UnitLocation AS R_UnitLocation_Return ON R_ResO_CarPickup.unit_location_Return = R_UnitLocation_Return.id_unit_location INNER JOIN " _
                      & "R_UnitLocation_Lang AS R_UnitLocation_Lang_1 ON R_UnitLocation_Return.id_unit_location = R_UnitLocation_Lang_1.id_unit_location "
                    sqlSearch += s


                Case 2

                    s = " AND (R_UnitLocation_Lang.id_Lang = N'es') AND (R_UnitLocation_Lang_1.id_Lang = N'es') "
                    sqlSearch += s

            End Select

            Return s 'only de string to use isolate


        End Function



        Public Function GetInfTarifas(cod_hotel As Integer, Cod_Tarifa As Integer) As DataView

            Dim sSQL As String = ""

            sSQL = sSQL & " SELECT     R_tarifas.cod_tarifa, R_tarifas.tarifa, T_HOTELES_HABITACIONES.habitacionhotel, R_Tarifa_habitacion.precio, R_Tarifa_habitacion.precioAD, "
            sSQL = sSQL & "   R_Tarifa_habitacion.preciomp, R_Tarifa_habitacion.preciopc, R_Tarifa_habitacion.TodoIncluido AS precioTI, R_Tarifa_habitacion.DescAdultosExtra1, "
            sSQL = sSQL & "   R_Tarifa_habitacion.DescAdultosExtra2, R_Tarifa_habitacion.DescNinoPago1, R_Tarifa_habitacion.DescNinoPago2, r_Temporadas.Temporada, CONVERT(varchar(10),"
            sSQL = sSQL & "    R_Temporadas_Periodos.fechainicio, 103) AS fech1, CONVERT(varchar(10), R_Temporadas_Periodos.fechafin, 103) AS fech2, R_tarifas.DiasMinimos, "
            sSQL = sSQL & "   R_Tarifa_habitacion.desayunonino, R_Tarifa_habitacion.mpnino, R_Tarifa_habitacion.pcnino, R_Tarifa_habitacion.TodoIncluidoNino, R_Tarifa_habitacion.camaExtra2, "
            sSQL = sSQL & "   R_Tarifa_habitacion.camaExtra1, R_Tarifa_habitacion.camaExtra1nino, R_Tarifa_habitacion.camaExtra2nino, R_Tarifa_habitacion.SupDiasMinimos, "
            sSQL = sSQL & "   R_Tarifa_habitacion.SupFinDeSemana, R_tarifas.ModifyDate, R_tarifas.TPVMinimum, "
            sSQL = sSQL & "   R_tarifas.TPVOneNight , R_Tarifa_habitacion.cod_habitacion"
            sSQL = sSQL & "     ,T_HOTELES_HABITACIONES.Adultos, T_HOTELES_HABITACIONES.AdultosExtra1, T_HOTELES_HABITACIONES.AdultosExtra2, "
            sSQL = sSQL & "    T_HOTELES_HABITACIONES.MaxPersonas, T_HOTELES_HABITACIONES.NumNino, T_HOTELES_HABITACIONES.MaxAdultos,"
            sSQL = sSQL & "    T_HOTELES_HABITACIONES.cod_hab_combinada, T_HOTELES_HABITACIONES.NinoOcupaplaza"
            sSQL = sSQL & " FROM R_tarifas INNER JOIN"
            sSQL = sSQL & " R_Tarifa_habitacion ON R_tarifas.cod_tarifa = R_Tarifa_habitacion.cod_tarifa INNER JOIN"
            sSQL = sSQL & " T_HOTELES_HABITACIONES ON R_Tarifa_habitacion.cod_habitacion = T_HOTELES_HABITACIONES.codhabitacionhotel INNER JOIN"
            sSQL = sSQL & " r_Tarifas_Temporadas ON R_tarifas.cod_tarifa = r_Tarifas_Temporadas.cod_tarifa INNER JOIN"
            sSQL = sSQL & " r_Temporadas ON r_Tarifas_Temporadas.cod_Temporada = r_Temporadas.cod_Temporada INNER JOIN"
            sSQL = sSQL & " R_Temporadas_Periodos ON r_Temporadas.cod_Temporada = R_Temporadas_Periodos.cod_temporada"
            sSQL = sSQL & " Where R_tarifas.cod_Tarifa = " & Cod_Tarifa & " And R_tarifas.cod_hotel = " & cod_hotel
            sSQL = sSQL & " ORDER BY R_Temporadas_Periodos.fechainicio, R_Temporadas_Periodos.fechafin, r_Temporadas.Temporada, T_HOTELES_HABITACIONES.habitacionhotel"

            Dim dt As DataTable = ind.bk.DBMgr.getDT(sSQL)
            Return dt.DefaultView

        End Function


    End Class


    Public Class pricesInfoObject
        Public tariffid As Integer = 0
        Public roomid As Integer = 0
        Public UI_DiscountPax1 As String
        Public UI_DiscountPax2 As String

        Public UI_DiscountChild1 As String
        Public UI_DiscountChild2 As String

        Public priceinfo As String
        Public drvTariff As DataRowView
        Public Const selector As String = "rprice_{0}_{1}"

        Public Sub New()

        End Sub

        Public Sub New(row As DataRowView)

            tariffid = row(ind.bk.obj.R_Tarifa_habitacion.DBCol.cod_tarifa)
            roomid = row(ind.bk.obj.R_Tarifa_habitacion.DBCol.cod_habitacion)
            Me.drvTariff = row
            'Me.configurePriceForUI(row)
        End Sub

        Public Function getRoomIdUIf() As String
            Return String.Format(selector, Me.tariffid, Me.roomid)
        End Function


        Public Function getRoomIdUI(field As tariffFieldEnum) As String
            Return String.Format(selector, Me.tariffid, Me.roomid) & "_" & field
        End Function

        Public Enum tariffFieldEnum As Byte
            precio = 1
            supdiasmin = 2
            supfindesemana = 3
            'adult board
            boardAD = 4
            boardMP = 5
            boardPC = 6
            boardTI = 7
            'child board
            boardADchild = 8
            boardMPchild = 9
            boardPCchild = 10
            boardTIchild = 11
            'Discount
            discountPax1 = 12
            discountPax2 = 13
            discountchild1 = 14
            discountchild2 = 15

        End Enum


        Public Sub configurePriceForUI(oRSlstHabTar As DataRowView)
            Dim adultosextras As Integer = oRSlstHabTar(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.AdultosExtra2) + oRSlstHabTar(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.AdultosExtra2)
            'PRECIO POR ADULTO
            Dim descPrecxAdulto As String = ""
            Dim PrecxAdulto As Double
            If oRSlstHabTar(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.Adultos) > 1 Then
                PrecxAdulto = oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.precio) / oRSlstHabTar(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.Adultos)
                descPrecxAdulto = "[Precio x Pax=" & PrecxAdulto.ToString("#,##") & "€]"
            End If

            'DESCUENTOS
            Dim descprecioextras As String = ""
            If oRSlstHabTar(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.AdultosExtra1) = 1 Then
                If oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra1) <> 0 Then
                    'fixed price
                    descprecioextras = " + 1 Pax Extra a " & oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra1) & "€"
                    Me.UI_DiscountPax1 = oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra1)
                Else
                    '% price
                    descprecioextras = " + 1 Pax Extra con descuento del " & oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.DescAdultosExtra1) & "%[" & FormatNumber((PrecxAdulto * oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.DescAdultosExtra1)) / 100, 2) & "]"
                    Me.UI_DiscountPax1 = oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.DescAdultosExtra1) & "%"
                End If
                If oRSlstHabTar(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.AdultosExtra2) = 1 Then
                    If oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra2) <> 0 Then
                        'fixed price
                        descprecioextras = descprecioextras & " + Pax Extra a " & oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra2)
                        Me.UI_DiscountPax1 = oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra2)
                    Else
                        '% price
                        descprecioextras = descprecioextras & " + Pax Extra con descuento del " & oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.DescAdultosExtra2) & "%[" & FormatNumber((PrecxAdulto * oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.DescAdultosExtra2)) / 100, 2) & "]"
                        Me.UI_DiscountPax1 = oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.DescAdultosExtra2) & "%"
                    End If
                End If
            End If

            Me.getChildDiscount()


            Dim s As String = "{0} Pax ({1} Adultos a {2}€ {3} {4})"
            Me.priceinfo = String.Format(s, oRSlstHabTar(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.MaxPersonas), oRSlstHabTar(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.Adultos), oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.precio), descPrecxAdulto, descprecioextras)

        End Sub

        Private Sub getChildDiscount()
            Dim oRSlstHabTar As DataRowView = Me.drvTariff
            If oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra1nino) <> 0 Then
                'fixed price
                Me.UI_DiscountChild1 = oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra1nino)
            Else
                '% price
                If oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.DescNinoPago1) <> 0 Then
                    Me.UI_DiscountChild1 = oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.DescNinoPago1) & "%"
                End If

            End If

            If oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra2nino) <> 0 Then
                'fixed price
                Me.UI_DiscountChild2 = oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra2nino)
            Else
                '% price
                If oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.DescNinoPago2) <> 0 Then
                    Me.UI_DiscountChild2 = oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.DescNinoPago2) & "%"
                End If

            End If

        End Sub




    End Class


    Public Class bookReportUI

        Public holderProp As cfr.obj.Hotel
        Public ors As DataRowView
        Public bookID As Integer
        Public bookData As New BookRes

#Region "Var for presentation"
        Public strEstadoReserva As String
        Public strEstadoPago As String
        Public strBoard As String
        Public stroperador As String
        Public ClientDataStr As String
        Public paySelectedStr As String

        Public paxStr As String

        Public priceStr As String
#End Region

        Public priceFinal As Double
        Public priceChanged As Double

        Public Sub New(bookID As Integer)
            Me.bookID = bookID
        End Sub

        Public Sub New(ors As DataRowView, holderProp As cfr.obj.Hotel)
            Me.holderProp = holderProp
            Me.ors = ors
            Me.bookID = ors(ind.bk.obj.R_ReservasO.DBCol.cod_reservaO)
            Me.bookData.idEntity = ors(ind.bk.obj.R_ReservasO.DBCol.cod_reservaO)


            Me.bookData = BookRes.getBookDataByID(bookID)
            
            With Me.bookData
                .setDates(ors("datein"), ors("dateout"))
                If holderProp.Tipo = CFRConst.unitTypeEnum.car Then
                    .carLocationSelected = New R_ResO_CarPickup
                    .carLocationSelected.unit_location_Get_Price = ors("unit_location_Get_Price")
                    .carLocationSelected.unit_location_Return_Price = ors("unit_location_Return_Price")
                End If
            End With

            Me.priceFinal = ors(ind.bk.obj.R_ResO_Hab.DBCol.preciofinal)
            Me.priceChanged = cfr.tool.Tools.cNull(ors(ind.bk.obj.R_ReservasO.DBCol.precioReal), Me.priceFinal)
            Me.config()

        End Sub

        Public Function getLocation(txt2Get As String) As String
            Select Case txt2Get.ToUpper
                Case "GET"
                    Return ors("descriptionLocationGet")
                Case "RETURN"
                    Return ors("descriptionLocationReturn")
                Case "PRICEGET"
                    Return Me.bookData.carLocationSelected.unit_location_Get_Price
                Case "PRICERETURN"
                    Return Me.bookData.carLocationSelected.unit_location_Return_Price
            End Select
            Return Nothing
        End Function


        Public Function getBookStateUI(bookState As CFRConst.bookStateEnum) As String
            Select Case bookState
                Case CFRConst.bookStateEnum.Active
                    strEstadoReserva = "Activa/Sin confirmar"
                Case CFRConst.bookStateEnum.CancelByClient
                    strEstadoReserva = "Cancelada por cliente " & ors("fechacancelacion")
                Case CFRConst.bookStateEnum.canceledByOperator, CFRConst.bookStateEnum.CancelByHotelUser
                    strEstadoReserva = "Cancelada por " & ors("canceladax") & " " & ors("fechacancelacion")
                Case CFRConst.bookStateEnum.confirmedByHotel
                    strEstadoReserva = "Confirmada por " & ors("confirmadax") & " " & ors("fechaconfirmacion")
                Case CFRConst.bookStateEnum.NoShow
                    strEstadoReserva = "NoShow por " & ors("canceladax") & " " & ors("fechacancelacion")
            End Select
            Return strEstadoReserva
        End Function


        Public Function getBookPayStateUI(bookPayState As cfr.CFRConst.payStateEnum) As String
            'Pay state
            If bookPayState = CFRConst.payStateEnum.payed Then
                strEstadoReserva += String.Format("<br/><span id='paypen_{0}' >Pagada</span>", Me.bookID)
            ElseIf bookPayState = CFRConst.payStateEnum.noactioneeded Then
                'strEstadoReserva += String.Format("<br/><span class='paypen' id='paypen_{0}' >Pe</span>", Me.bookID)
            ElseIf bookPayState = CFRConst.payStateEnum.paybycancellationneeded Then
                strEstadoReserva += String.Format("<br/><span class='paypen' id='paypen_{0}' >Cargar por cancelación</span>", Me.bookID)
            ElseIf bookPayState = CFRConst.payStateEnum.paybycancellationOK Then
                strEstadoReserva += String.Format("<br/><span id='paypen_{0}' >Cancelación cargada</span>", Me.bookID)
            ElseIf bookPayState = CFRConst.payStateEnum.PrechargeMade Then
                strEstadoReserva += String.Format("<br/><span id='paypen_{0}' >Cargo inicial realizado</span>", Me.bookID)
            ElseIf bookPayState = CFRConst.payStateEnum.devolutionPending Then
                strEstadoReserva += String.Format("<br/><span id='paypen_{0}' >Realizar devolución</span>", Me.bookID)
            ElseIf bookPayState = CFRConst.payStateEnum.DevolutionOK Then
                strEstadoReserva += String.Format("<br/><span id='paypen_{0}' >Devolución realizada</span>", Me.bookID)
            Else
                strEstadoReserva += String.Format("<br/><span class='paypen' id='paypen_{0}' >Pendiente pago</span>", Me.bookID)
            End If
            Return strEstadoReserva
        End Function


        Public Sub config()

            getBookStateUI(ors(ind.bk.obj.R_ResO_Hab.DBCol.Estado))

            getBookPayStateUI(ors(ind.bk.obj.R_ReservasO.DBCol.CheckHotelPago))

            strBoard = cfr.LodgeManager.getdescRegimen(ors(ind.bk.obj.R_ResO_Hab.DBCol.regimen_aloj))

            'datos del OPERADOR%>

            stroperador = ors(ind.bk.obj.R_ReservasO.DBCol.Descoperador)

            If ors(ind.bk.obj.R_ReservasO.DBCol.operadorGenerico) = 1 Then
                stroperador = "<B>Generico Basado en</B>: " & stroperador
            End If
            If CStr(ors(ind.bk.obj.R_ReservasO.DBCol.tipoOperador)) = cfr.CFRConst.ClientTypeEnum.HotelAgency Then 'agencia
                stroperador = stroperador & "<BR><B>Agencia</B>: " & ors(ind.bk.obj.R_ReservasO.DBCol.Ag_Nombre)
                stroperador = stroperador & "<BR><B>Direccion</B>: " & ors(ind.bk.obj.R_ReservasO.DBCol.Ag_Direccion) & ", " & ors(ind.bk.obj.R_ReservasO.DBCol.Ag_ciudad) & ", " & ors(ind.bk.obj.R_ReservasO.DBCol.Ag_Cod_postal) & ", " & ors(ind.bk.obj.R_ReservasO.DBCol.Ag_Provincia) & ", " & ors(ind.bk.obj.R_ReservasO.DBCol.Ag_Pais)
                stroperador = stroperador & "<BR><B>Tlf/Fax</B>: " & ors(ind.bk.obj.R_ReservasO.DBCol.Ag_Telefono) & "/" & ors(ind.bk.obj.R_ReservasO.DBCol.Ag_Fax)
                stroperador = stroperador & "<BR><B>Email</B>: " & ors(ind.bk.obj.R_ReservasO.DBCol.Ag_email)
                stroperador = stroperador & "<BR><B>Agente</B>: " & ors(ind.bk.obj.R_ReservasO.DBCol.Ag_Agente)
            End If

            'stroperador = stroperador & "<BR><B>Origen:</B>" & getSiteDescription(ors( ind.bk.obj.R_ReservasO.DBCol.site))

            'datos del cliente

            ClientDataStr = "<i class='fa fa-user'></i> " & ors(ind.bk.obj.R_ReservasO.DBCol.Apellido1) & "," & ors(ind.bk.obj.R_ReservasO.DBCol.Nombre)
            If Me.holderProp.Tipo = CFRConst.unitTypeEnum.car Then
                ClientDataStr = ClientDataStr & "<BR>" & "Edad: " & ors(ind.bk.obj.R_ReservasO.DBCol.driverAge)
            End If

            If Len(ors(ind.bk.obj.R_ReservasO.DBCol.telefono)) > 0 Then
                Dim clientTel As String = ors(ind.bk.obj.R_ReservasO.DBCol.telefono).ToString
                ClientDataStr = ClientDataStr & "<BR>" & String.Format("<a href='tel:{0}'><i class='fa fa-phone'></i> {0}</a>", clientTel)
            End If
            If Len(ors(ind.bk.obj.R_ReservasO.DBCol.email)) > 0 Then
                ClientDataStr = ClientDataStr & String.Format("<BR><i class='fa fa-envelope'></i> <a href='mailto:{0}' target='sendemail'>{0}</a>", ors(ind.bk.obj.R_ReservasO.DBCol.email))
            End If

            If Len(ors(ind.bk.obj.R_ReservasO.DBCol.empresa)) > 0 Then
                ClientDataStr = ClientDataStr & "<BR><B>Empresa::</B> " & ors(ind.bk.obj.R_ReservasO.DBCol.empresa)
                ClientDataStr = ClientDataStr & "<BR>" & Chr(187) & " " & ors(ind.bk.obj.R_ReservasO.DBCol.empresadireccion)
                ClientDataStr = ClientDataStr & "<BR><B>Tlf:</B>" & ors(ind.bk.obj.R_ReservasO.DBCol.empresatlf)
                ClientDataStr = ClientDataStr & "<BR>" & ors(ind.bk.obj.R_ReservasO.DBCol.empresaemail)
            End If

            'pay 

            Select Case ors(ind.bk.obj.R_ReservasO.DBCol.cod_formapago)
                Case cfr.CFRConst.formaPago.AnyPay
                    paySelectedStr += "Elección:"
                    If ors(ind.bk.obj.R_ReservasO.DBCol.nTarjeta) <> "" Then
                        paySelectedStr += "Tarjeta de Credito"
                        paySelectedStr += "<BR>Nº: " & ors(ind.bk.obj.R_ReservasO.DBCol.nTarjeta)
                        paySelectedStr += "<BR>Caducidad : " & ors(ind.bk.obj.R_ReservasO.DBCol.FCaducidad)
                        paySelectedStr += "<BR>Titular : " & ors(ind.bk.obj.R_ReservasO.DBCol.Titular)
                    Else
                        paySelectedStr += " PREPAGO"
                    End If
                Case cfr.CFRConst.formaPago.CreditCard
                    paySelectedStr += "Tarjeta de Credito"
                    paySelectedStr += "<BR>Nº: " & ors(ind.bk.obj.R_ReservasO.DBCol.nTarjeta)
                    paySelectedStr += "<BR>Caducidad : " & ors(ind.bk.obj.R_ReservasO.DBCol.FCaducidad)
                    paySelectedStr += "<BR>Titular : " & ors(ind.bk.obj.R_ReservasO.DBCol.Titular)
                Case cfr.CFRConst.formaPago.RegisteredCreditCard
                    paySelectedStr += "Tarjeta Agencia"
                    paySelectedStr += "<BR>Nº: " & ors(ind.bk.obj.R_ReservasO.DBCol.nTarjeta)
                    paySelectedStr += "<BR>Caducidad : " & ors(ind.bk.obj.R_ReservasO.DBCol.FCaducidad)
                    paySelectedStr += "<BR>Titular : " & ors(ind.bk.obj.R_ReservasO.DBCol.Titular)
                Case cfr.CFRConst.formaPago.Transfer
                    paySelectedStr += "Prepago"
                Case cfr.CFRConst.formaPago.DirectPay
                    paySelectedStr += "A credito, Pago Directo"
                Case cfr.CFRConst.formaPago.TPVHotel
                    paySelectedStr += "Pasarela de pago Hotel"
                Case cfr.CFRConst.formaPago.TPVSystem
                    paySelectedStr += "Pasarela de pago WBooking"
            End Select


            'book data persons
            Dim paxnum As Integer = ors(ind.bk.obj.R_ResO_Hab.DBCol.num_personas)
            Dim paxChildPay As Integer = ors(ind.bk.obj.R_ResO_Hab.DBCol.edadnino1)
            Dim paxChildFree As Integer = ors(ind.bk.obj.R_ResO_Hab.DBCol.edadnino2)
            Dim paxAdult As Integer = paxnum - paxChildPay - paxChildFree
            paxStr += String.Format("<b>PAX:</b> {0}", paxnum)
            paxStr += String.Format("<br/><b>Adultos:</b> {0}", paxAdult)
            If paxnum > paxAdult Then
                'there are childs included in room
                paxStr += String.Format("<br/><B>Niños Pago:</B> {0} <B>Gratis:</B>{1}", paxChildPay, paxChildFree)
            End If



            Dim price2show As String = ""
            Dim priceForLocation As Double = 0
            Dim priceFinalSum As Double = Me.priceFinal

            If Me.holderProp.Tipo = CFRConst.unitTypeEnum.car Then

                If Me.bookData.carLocationSelected.unit_location_Get_Price >= Me.bookData.carLocationSelected.unit_location_Return_Price Then
                    priceForLocation = Me.bookData.carLocationSelected.unit_location_Get_Price
                Else
                    priceForLocation = Me.bookData.carLocationSelected.unit_location_Return_Price
                End If

                priceFinalSum = Me.priceFinal + priceForLocation
                price2show = String.Format("<div>Unidad: {0}€</div><div> <i class='fa fa-map-marker'></i>+{1}€</div><div>Total= {2}€</div>", Me.priceFinal, priceForLocation, priceFinalSum)

            ElseIf Me.holderProp.Tipo = CFRConst.unitTypeEnum.hotel Then
                price2show = String.Format("{0}€", priceFinalSum)

            Else
                price2show = String.Format("{0}€", priceFinalSum)

            End If

            Dim priceStrDiv As String = "<div id='pricechg_" & Me.bookID & "' {2}>{1}</div><div id='pricefinal_" & Me.bookID & "'>{0}</div>"
            Dim strTooltip As String = " data-placement='top'  data-toggle='tooltip' title='El precio ha sido modificado manualmente.' "



            If priceFinalSum <> Me.priceChanged Then
                Me.priceStr = String.Format(priceStrDiv, price2show, Me.priceChanged & "€", "class='lblwarn lblwarn-lg'")
            Else
                Me.priceStr = String.Format(priceStrDiv, price2show, "", "")
            End If



        End Sub

        Public Function getBookJsonStruct(Optional onlyjson As Boolean = False) As String

            Dim bkjson As New bookdatajson
            bkjson.bkid = Me.bookID
            bkjson.state = Me.ors("estado")
            bkjson.pricefinal = Me.priceFinal
            bkjson.pricechanged = Me.priceChanged
            bkjson.paystate = ors("checkHotelpago")
            bkjson.bookidproperty = "" & ors("cod_reserva_hotel")


            Dim bookStruct1 As String = ""
            If onlyjson = True Then
                bookStruct1 = JsonConvert.SerializeObject(bkjson)
            Else
                bookStruct1 = String.Format("data-bookdata='{0}'", JsonConvert.SerializeObject(bkjson))
            End If

            Return bookStruct1

            'Dim bookStruct As String = " data-bookdata='{""type"":""bookdata"",""bkid"":[bkid],""state"": [state] ,""pricefinal"":""[pricefinal]"", ""pricechanged"":""[pricechanged]""  ,""paystate"":[paystate] } '"
            'bookStruct = bookStruct.Replace("[bkid]", Me.bookID)
            'bookStruct = bookStruct.Replace("[state]", ors("estado"))
            'bookStruct = bookStruct.Replace("[pricefinal]", Me.priceFinal)
            'bookStruct = bookStruct.Replace("[pricechanged]", Me.priceChanged)
            'bookStruct = bookStruct.Replace("[paystate]", ors("checkHotelpago"))

            'Return bookStruct
        End Function

        Public Class bookdatajson
            Public bkid As Integer
            Public state As Integer
            Public pricefinal As String
            Public pricechanged As String
            Public paystate As Integer
            Public bookidproperty As String
        End Class

    End Class

End Namespace
