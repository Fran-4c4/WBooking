Imports Microsoft.VisualBasic
Namespace cfr


    Public Class AvailabilityManager


        ''' <summary>
        ''' Get room id with availability for selected days for the hotel
        ''' </summary>
        ''' <param name="dr"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function AvailabilityByHotel(ByVal dr As BookRes) As Data.DataTable

            Dim p As New Generic.List(Of SqlClient.SqlParameter)
            p.Add(ind.bk.DBMgr.getSpParameter("hotelID", dr.hotel.id))
            p.Add(ind.bk.DBMgr.getSpParameterDate("dateIN", dr.dateIn))
            p.Add(ind.bk.DBMgr.getSpParameterDate("dateOut", dr.dateOut))
            p.Add(ind.bk.DBMgr.getSpParameter("codOperator", dr.bookOperator.id))
            p.Add(ind.bk.DBMgr.getSpParameter("CodOperadorFreeSale", dr.hotel.freesaleOperatorObj.cod_operador))
            p.Add(ind.bk.DBMgr.getSpParameter("daysToBook", dr.bookTotalDays))


            Return ind.bk.DBMgr.getDataTable(Nothing, cfr.db.sp.sproc.Book.Hotel_GetAvailability, p)
        End Function

        ''' <summary>
        ''' return the availability between dates with count of available units (0 or more)  in each date 
        ''' </summary>
        ''' <param name="dr"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function AvailabilityMapByHotel(ByVal dr As BookRes) As Data.DataTable

            Return AvailabilityMapByHotel(dr.hotel.id, dr.dateIn, dr.dateOut, dr.bookOperator.id, dr.hotel.freesaleOperatorObj.cod_operador)
        End Function

        ''' <summary>
        ''' return the availability between dates with count of available units (0 or more)  in each date
        ''' </summary>
        ''' <param name="hotelID"></param>
        ''' <param name="datein"></param>
        ''' <param name="dateOut"></param>
        ''' <param name="freeSaleOperatorID"></param>
        ''' <param name="bookoperatorID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function AvailabilityMapByHotel(hotelID As Integer, datein As Date, dateOut As Date, freeSaleOperatorID As Integer, Optional bookoperatorID As Integer = -1) As Data.DataTable

       
            If bookoperatorID = -1 Then
                bookoperatorID = freeSaleOperatorID
            End If

            Dim p As New Generic.List(Of SqlClient.SqlParameter)
            p.Add(ind.bk.DBMgr.getSpParameter("hotelID", hotelID))
            p.Add(ind.bk.DBMgr.getSpParameterDate("dateIN", datein))
            p.Add(ind.bk.DBMgr.getSpParameterDate("dateOut", dateOut))
            p.Add(ind.bk.DBMgr.getSpParameter("codOperator", bookoperatorID))
            p.Add(ind.bk.DBMgr.getSpParameter("CodOperadorFreeSale", freeSaleOperatorID))


            Return ind.bk.DBMgr.getDataTable(Nothing, cfr.db.sp.sproc.Book.Hotel_GetAvailabilityMap, p)
        End Function





        ''' <summary>
        ''' Update room Availability
        ''' </summary>
        ''' <param name="increment"></param>
        ''' <param name="bookOperatorId"></param>
        ''' <param name="hotelId"></param>
        ''' <param name="roomIdToUpdate"></param>
        ''' <param name="dateIn"></param>
        ''' <param name="dateOut"></param>
        ''' <param name="cupoOrigen"></param>
        ''' <param name="trans"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function UpdateBookedRoomAvailability(ByRef increment As Integer, ByVal bookOperatorId As String _
        , ByVal hotelId As String _
        , ByVal roomIdToUpdate As String, ByVal dateIn As Date, ByVal dateOut As Date _
        , ByRef cupoOrigen As String, ByRef trans As Data.SqlClient.SqlTransaction _
        , Optional ByVal operatorFreeSaleId As String = "") As Integer
            ' **********************************************************************
            '    ACTUALIZA LA DISPONIBILIDAD DE UNA HABITACION
            '***********************************************************************
            'oper=+1 aumenta
            'oper=-1 disminuye

            Dim numreg As Integer 'nº de registros afectados por la instruccion update
            Dim iDias As Integer
            Dim MatcupoOrigen() As String = Nothing
            Dim bCancelando As Boolean
            Dim operatorToIncrement As String = bookOperatorId

            If dateOut <= Date.Now Then
                Exit Function
            End If

            'Nº de dias
            iDias = cfr.tool.DateTools.GetDias(dateIn, dateOut)

            'obtenemos el codigo de operador de venta libre para disponibilidad en caso de que no exista cupo para el operador

            If operatorFreeSaleId = "" Then
                operatorFreeSaleId = OperadorManager.GetCodOperadorGeneric(hotelId, CFRConst.genericOperator.freeSale)
            End If
            '************************************

            If cupoOrigen <> "" Then 'when we cancel a room this field is filled
                MatcupoOrigen = Split(cupoOrigen, "|")
                bCancelando = True
            End If


            For x As Integer = 0 To iDias - 1
                Dim dayToUpdate As Date = dateIn.AddDays(x)
                If dayToUpdate.Date < Date.Now.Date Then
                    Continue For 'no needed to update old days
                End If
                If bCancelando Then 'si es una cancelacion
                    If MatcupoOrigen(x) = operatorFreeSaleId Then
                        operatorToIncrement = operatorFreeSaleId
                    Else
                        operatorToIncrement = bookOperatorId
                    End If
                End If

                'Para hotel
                If operatorFreeSaleId = operatorToIncrement Then 'si es operador de venta libre
                    numreg = AvailabilityManager.UpdateAvailability(trans, increment, hotelId, roomIdToUpdate, operatorFreeSaleId, dayToUpdate)
                    If numreg = 0 Then 'No hay disponibilidad en ninguna categoria(operador y venta libre)
                        Throw New Exception(basERR.E_NoDisponibility)
                        Exit For
                    End If
                    cupoOrigen += operatorFreeSaleId & "|"
                Else
                    numreg = AvailabilityManager.UpdateAvailability(trans, increment, hotelId, roomIdToUpdate, bookOperatorId, dayToUpdate)
                    '** Si no se actualiza ningun registro para el operador elegido probamos con venta libre.
                    If numreg = 0 Then
                        numreg = AvailabilityManager.UpdateAvailability(trans, increment, hotelId, roomIdToUpdate, operatorFreeSaleId, dayToUpdate)
                        If numreg = 0 Then 'No hay disponibilidad en ninguna categoria(operador y venta libre)
                            Throw New Exception(basERR.E_NoDisponibility)
                            Exit For
                        End If
                        cupoOrigen += operatorFreeSaleId & "|"
                    Else
                        cupoOrigen += bookOperatorId & "|"
                    End If
                End If
            Next x



        End Function


        Public Shared Function UpdateAvailability(ByVal trans As Data.SqlClient.SqlTransaction _
        , ByVal increment As Integer, ByVal id_hotel As String, ByVal id_room As String, ByVal id_Operator As String _
            , ByVal daySelected As Date) As Integer

            If increment <> -1 AndAlso increment <> 1 Then
                Throw New ArgumentOutOfRangeException(NameOf(increment), "Availability changes must be -1 or 1.")
            End If

            Dim hotelId As Integer
            Dim roomId As Integer
            Dim operatorId As Integer
            If Not Integer.TryParse(id_hotel, hotelId) OrElse
               Not Integer.TryParse(id_room, roomId) OrElse
               Not Integer.TryParse(id_Operator, operatorId) Then
                Throw New ArgumentException("Hotel, room and operator identifiers must be integers.")
            End If

            If daySelected.Date < Date.Now.Date Then
                Exit Function
            End If

            Const sSql As String =
                "UPDATE r_res_cupos SET r_res_cupos.Disponible=r_res_cupos.Disponible+@increment " &
                "FROM r_Disponibilidad INNER JOIN r_res_cupos " &
                "ON r_Disponibilidad.id_Disp_Hab=r_res_cupos.id_Disp_Hab " &
                "WHERE cod_hotel=@hotelId AND cod_Habitacion=@roomId " &
                "AND (@increment<>-1 OR r_res_cupos.Disponible>=1) " &
                "AND DIA=@day AND cod_Operador=@operatorId"

            Return ind.bk.DBMgr.Execute(
                sSql,
                trans,
                New Data.SqlClient.SqlParameter("@increment", SqlDbType.Int) With {.Value = increment},
                New Data.SqlClient.SqlParameter("@hotelId", SqlDbType.Int) With {.Value = hotelId},
                New Data.SqlClient.SqlParameter("@roomId", SqlDbType.Int) With {.Value = roomId},
                New Data.SqlClient.SqlParameter("@day", SqlDbType.DateTime) With {.Value = daySelected.Date},
                New Data.SqlClient.SqlParameter("@operatorId", SqlDbType.Int) With {.Value = operatorId})

        End Function



        Public Shared Function getHotelWithAvailability(ByVal dr As cfr.BookRes) As Data.DataTable
            Dim s As String = ""
            s = "SELECT distinct    R_disponibilidad.cod_hotel ,MIN(R_Tarifa_habitacion.precio) AS MinimumPrice "
            s += " FROM         r_Temporadas INNER JOIN "
            s += "  R_Temporadas_Periodos ON r_Temporadas.cod_Temporada = R_Temporadas_Periodos.cod_temporada INNER JOIN     "
            s += "  R_disponibilidad INNER JOIN "
            s += "  T_HOTELES ON R_disponibilidad.cod_hotel = T_HOTELES.cod_Hotel INNER JOIN                           "
            s += "  R_Tarifa_habitacion ON R_disponibilidad.cod_habitacion = R_Tarifa_habitacion.cod_habitacion INNER JOIN   "
            s += "  R_Res_cupos ON R_disponibilidad.id_Disp_Hab = R_Res_cupos.id_Disp_Hab INNER JOIN                   "
            s += "  T_HOTELES_HABITACIONES ON T_HOTELES.cod_Hotel = T_HOTELES_HABITACIONES.Hotel AND                   "
            s += "  R_Tarifa_habitacion.cod_habitacion = T_HOTELES_HABITACIONES.codhabitacionhotel ON                  "
            s += "  r_Temporadas.cod_hotel = T_HOTELES.cod_Hotel INNER JOIN          "
            s += "  r_Tarifas_Temporadas ON r_Temporadas.cod_Temporada = r_Tarifas_Temporadas.cod_Temporada INNER JOIN "
            s += "  R_tarifas ON R_Tarifa_habitacion.cod_tarifa = R_tarifas.cod_tarifa AND r_Tarifas_Temporadas.cod_tarifa = R_tarifas.cod_tarifa"
            s += "  INNER JOIN T_zona_ciudad ON T_HOTELES.cod_Ciudad = T_zona_ciudad.cod_ciudad "

            s += " WHERE  T_HOTELES.Estado=0  AND (R_Res_cupos.disponible > 0) "
            s += " AND (R_disponibilidad.dia >= " & cfr.tool.DateTools.getConvertDate(dr.dateIn) & ")"
            s += " AND (R_disponibilidad.dia <= " & cfr.tool.DateTools.getConvertDate(dr.dateOut) & ") "
            s += "  and (R_Temporadas_Periodos.fechainicio < " & cfr.tool.DateTools.getConvertDate(dr.dateOut) & ")  "
            s += "  AND (R_Temporadas_Periodos.fechafin > " & cfr.tool.DateTools.getConvertDate(dr.dateOut) & ") "
            s += "  AND (T_HOTELES_HABITACIONES.Adultos >= " & dr.numAdultos & ") "
            s += "  AND (T_HOTELES_HABITACIONES.MaxPersonas >= " & (dr.ninosfree + dr.ninospago + dr.numAdultos) & ")   "
            s += "  AND (R_Tarifa_habitacion.precio > 0) "


            'If dr.searchCriteria.city.idEntity <> "0" And dr.searchCriteria.city.idEntity <> "-1" Then
            '    s += "   AND (T_HOTELES.cod_Ciudad = " & dr.searchCriteria.city.idEntity & ") "
            'ElseIf dr.searchCriteria.codZona <> "0" And dr.searchCriteria.codZona <> "-1" Then
            '    s += "   AND (T_zona_ciudad.cod_Zona = " & dr.searchCriteria.codZona & ") "

            'ElseIf dr.searchCriteria.country.idEntity <> "0" Then
            '    s += "   AND (T_HOTELES.cod_Pais = " & dr.searchCriteria.country.idEntity & ") "
            'End If

            'If dr.searchCriteria.category <> "" Then
            '    s += "  AND (T_HOTELES.cod_Categoria IN (" & dr.searchCriteria.category & ")) "
            'End If

            'If dr.precio > 0 Then
            '    s+= "  AND (R_Tarifa_habitacion.precio <= " & dr.precio & ")"
            'End If

            s += " GROUP BY R_disponibilidad.cod_hotel "
            s += " ORDER BY MIN(R_Tarifa_habitacion.precio) "


            Return ind.bk.DBMgr.getDT(s)

        End Function


    End Class
End Namespace
