Imports System.Data
Imports Microsoft.VisualBasic


Namespace ind.bk.web
    Public Class Availability_mgr
        ''' <summary>
        ''' Get availability asociated to operator for selected month/year
        ''' </summary>
        ''' <param name="codhotel"></param>
        ''' <param name="cod_operadorSel"></param>
        ''' <param name="yearSel"></param>
        ''' <param name="monthSel"></param>
        ''' <param name="operUpdate"></param>
        ''' <returns></returns>
        Public Shared Function getClientCupoByOperador(codhotel As Integer, cod_operadorSel As Integer, yearSel As Integer, monthSel As Integer, Optional operUpdate As Boolean = False) As DataView
            Dim dateFrom As Date, dateTo As Date
            If monthSel = Month(Now) And yearSel = Year(Now) Then
                dateFrom = New Date(yearSel, monthSel, Day(Now))
            Else
                dateFrom = New Date(yearSel, monthSel, 1)
            End If
            Dim DaysInMonth As Integer = Date.DaysInMonth(dateFrom.Year, dateFrom.Month)
            dateTo = New Date(yearSel, monthSel, DaysInMonth)
            Dim dateRange As New ind.bk.date.DateRangeCls(dateFrom, dateTo)
            Return ind.bk.web.Availability_mgr.getCupoByOperadorByDates(codhotel, cod_operadorSel, dateRange, operUpdate)

        End Function

        ''' <summary>
        ''' Get availability asociated to operator by range
        ''' </summary>
        ''' <param name="codhotel"></param>
        ''' <param name="cod_operadorSel"></param>
        ''' <param name="dateRange"></param>
        ''' <param name="operUpdate"></param>
        ''' <returns></returns>
        Public Shared Function getCupoByOperadorByDates(codhotel As Integer, cod_operadorSel As Integer, dateRange As ind.bk.date.DateRangeCls, Optional operUpdate As Boolean = False) As DataView

            Dim dateFromStr As String = dateRange.dateFrom.ToString("yyyyMMdd")
            Dim dateToStr As String = dateRange.dateTo.ToString("yyyyMMdd")

            Dim sSQL As String = " SELECT * FROM R_disponibilidad INNER JOIN "
            sSQL = sSQL & " R_Res_cupos ON R_disponibilidad.id_Disp_Hab = R_Res_cupos.id_Disp_Hab "
            sSQL = sSQL & " Where R_disponibilidad.cod_Hotel = " & codhotel
            sSQL = sSQL & " AND R_Res_cupos.COD_OPERADOR=" & cod_operadorSel
            sSQL = sSQL & " AND (DIA>=CONVERT(DATETIME,'" & dateFromStr & "',113) AND DIA<=CONVERT(DATETIME,'" & dateToStr & "',113))"
            If operUpdate = False Then
                sSQL = sSQL & " and disponible >0"
            End If

            Dim dt As DataTable = ind.bk.DBMgr.getDT(sSQL)
            Return dt.DefaultView

        End Function



        ''' <summary>
        ''' return generic availability for selected month
        ''' </summary>
        ''' <param name="cod_hotel"></param>
        ''' <param name="cod_operador"></param>
        ''' <param name="iMes"></param>
        ''' <param name="iYear"></param>
        ''' <returns></returns>
        Public Shared Function listAvailability(cod_hotel As Integer, cod_operador As Integer, iMes As Integer, iYear As Integer) As DataView
            Dim dayStart As Date = New Date(iYear, iMes, 1)
            Dim dayEnd As Date = ind.bk.date.DateRangeCls.getLastDayOfMonth(dayStart)

            Dim dateRange As New ind.bk.date.DateRangeCls(dayStart, dayEnd)
            Return listAvailability(cod_hotel, cod_operador, dateRange)
        End Function



        Public Shared Function listAvailability(cod_hotel As Integer, cod_operador As Integer, dateRange As ind.bk.date.DateRangeCls) As DataView
            Dim sSQL As String = "SELECT  * FROM r_disponibilidad "
            sSQL = sSQL & " Where cod_hotel =" & cod_hotel
            sSQL = sSQL & " AND (DIA >= " & dateRange.getSqlConvert(ind.bk.date.DateRangeCls.dateSelected.dateFrom)
            sSQL = sSQL & " AND DIA <=" & dateRange.getSqlConvert(ind.bk.date.DateRangeCls.dateSelected.dateTo) & " )"

            Return ind.bk.DBMgr.getDT(sSQL).DefaultView

        End Function

        ''' <summary>
        ''' update availability for selected month
        ''' </summary>
        ''' <param name="cod_hotel"></param>
        ''' <param name="cod_operador"></param>
        ''' <param name="newAvailabList"></param>
        ''' <param name="iMes"></param>
        ''' <param name="iYear"></param>
        ''' <returns></returns>
        Public Shared Function UpdateDisponib(cod_hotel As Integer, cod_operador As Integer, newAvailabList As ArrayList, iMes As Integer, iYear As Integer)

            Dim oRDias As DataView

            Dim id_Disp_Hab As Integer  'el id de la habitacion
            Dim ocon As SqlClient.SqlConnection
            Dim trans As SqlClient.SqlTransaction
            Dim sSQL As String

            Try
                If newAvailabList.Count = 0 Then
                    Exit Function
                End If
                'cupo
                Dim dvCupoByClient As DataView = ind.bk.web.Availability_mgr.getClientCupoByOperador(cod_hotel, cod_operador, iYear, iMes, True)
                'disponibilidad
                oRDias = ind.bk.web.Availability_mgr.listAvailability(cod_hotel, cod_operador, iMes, iYear)

                'abrimos la conexion para el update
                ocon = ind.bk.DBMgr.getNewConn
                'comienzo de la transaccion
                trans = ind.bk.DBMgr.CreateTransaction(ocon)

                For Each dayDisp As caldisp In newAvailabList
                    Dim dayToCheck As String = dayDisp.englishDate

                    dvCupoByClient.RowFilter = "dia=#" & dayToCheck & "# AND cod_habitacion=" & dayDisp.cod_habitacion

                    If dvCupoByClient.Count > 0 Then 'EXISTE ESE DIA PARA EL OPERADOR=> ACTUALIZAMOS
                        Dim ors1 As DataRowView = dvCupoByClient(0)
                        If dayDisp.availability <> ors1("disponible") Then

                            Dim operCupo As New ind.bk.obj.R_Res_cupos
                            operCupo.ID = CInt(ors1("id"))
                            operCupo.id_Disp_Hab = ors1("id_Disp_Hab")
                            operCupo.disponible = CShort(dayDisp.availability)
                            operCupo.cod_operador = cod_operador

                            Dim operCupoMgr As New ind.bk.obj.R_Res_cupos_Manager
                            operCupoMgr.Update(operCupo, trans)
                        End If 'oRec("disponibilidad") <> oRs1("disponibilidad")

                    Else 'insert si no existe
                        oRDias.RowFilter = "dia=#" & dayToCheck & "# AND cod_habitacion=" & dayDisp.cod_habitacion
                        If oRDias.Count = 0 Then 'no existe ni el dia ni la asociacion con el operador
                            'we need to insert into table R_disponibilidad first
                            Dim operDisp As New ind.bk.obj.R_disponibilidad_Manager
                            Dim objDisp As New ind.bk.obj.R_disponibilidad
                            objDisp.cod_habitacion = dayDisp.cod_habitacion
                            objDisp.cod_hotel = cod_hotel
                            objDisp.dia = dayDisp.dia
                            operDisp.insert(objDisp, trans)

                            id_Disp_Hab = objDisp.id_Disp_Hab 'get the id
                        Else
                            id_Disp_Hab = oRDias(0)("id_Disp_Hab")
                        End If
                        'INSERT AVAILABILITY INTO TABLE R_RES_CUPOS
                        Dim operCupo As New ind.bk.obj.R_Res_cupos
                        operCupo.id_Disp_Hab = id_Disp_Hab
                        operCupo.disponible = CShort(dayDisp.availability)
                        operCupo.cod_operador = cod_operador

                        Dim operCupoMgr As New ind.bk.obj.R_Res_cupos_Manager
                        operCupoMgr.insert(operCupo, trans)

                    End If

                Next

                ind.bk.DBMgr.CommitTransaction(trans)

            Catch ex As Exception
                ind.bk.DBMgr.rollbackTransaction(trans)
                Throw ex
            Finally

                ind.bk.DBMgr.disposeTransaction(trans)

            End Try


        End Function

        Public Shared Sub UpdateUnitYMClosed(cod_hotel As Integer, cod_operador As Integer, unitID As Integer, closeOper As Integer, dateRange As ind.bk.date.DateRangeCls)
            Dim unitsTocloseList As New ArrayList
            unitsTocloseList.Add(unitID)
            UpdateUnitYMClosed(cod_hotel, cod_operador, unitsTocloseList, closeOper, dateRange)
        End Sub


        Public Shared Sub UpdateUnitYMClosed(cod_hotel As Integer, cod_operador As Integer, unitsTocloseList As ArrayList, closeOper As Integer, dateRange As ind.bk.date.DateRangeCls)

            Dim oRDias As DataView

            Dim id_Disp_Hab As Integer  'el id de la habitacion
            Dim ocon As SqlClient.SqlConnection
            Dim trans As SqlClient.SqlTransaction

            Try
                'cupo by client
                Dim dvCupoByClient As DataView = ind.bk.web.Availability_mgr.getCupoByOperadorByDates(cod_hotel, cod_operador, dateRange, True)
                'availability generic
                oRDias = ind.bk.web.Availability_mgr.listAvailability(cod_hotel, cod_operador, dateRange)

                'abrimos la conexion para el update
                ocon = ind.bk.DBMgr.getNewConn
                'comienzo de la transaccion
                trans = ind.bk.DBMgr.CreateTransaction(ocon)
                Dim daySel As Date = dateRange.dateFrom
                Dim operCupoMgr As New ind.bk.obj.R_Res_cupos_Manager

                While daySel.Date <= dateRange.dateTo.Date
                    Dim dayToCheck As String = daySel.ToString("yyyy/MM/dd")

                    For Each unitid As Integer In unitsTocloseList
                        dvCupoByClient.RowFilter = "dia=#" & dayToCheck & "# AND cod_habitacion=" & unitid

                        If dvCupoByClient.Count > 0 Then 'EXISTE ESE DIA PARA EL OPERADOR=> ACTUALIZAMOS
                            Dim ors1 As DataRowView = dvCupoByClient(0)
                            Dim ssql As String = "UPDATE R_RES_CUPOS SET CLOSED=@closed WHERE id=@id"
                            ind.bk.DBMgr.Execute(
                                ssql,
                                trans,
                                New SqlClient.SqlParameter("@closed", SqlDbType.Bit) With {.Value = closeOper},
                                New SqlClient.SqlParameter("@id", SqlDbType.Int) With {.Value = CInt(ors1("id"))})

                        Else 'insert si no existe
                            'with reduce availability we do not need to do nothing. 
                            oRDias.RowFilter = "dia=#" & dayToCheck & "# AND cod_habitacion=" & unitID
                            If oRDias.Count = 0 Then 'no existe ni el dia ni la asociacion con el operador
                                'we need to insert into table R_disponibilidad first
                                Dim operDisp As New ind.bk.obj.R_disponibilidad_Manager
                                Dim objDisp As New ind.bk.obj.R_disponibilidad
                                objDisp.cod_habitacion = unitID
                                objDisp.cod_hotel = cod_hotel
                                objDisp.dia = daySel.Date
                                operDisp.insert(objDisp, trans)

                                id_Disp_Hab = objDisp.id_Disp_Hab 'get the id
                            Else
                                id_Disp_Hab = oRDias(0)("id_Disp_Hab")
                            End If
                            'INSERT AVAILABILITY INTO TABLE R_RES_CUPOS
                            Dim ssql As String = "INSERT INTO R_RES_CUPOS (id_Disp_Hab,cod_operador, disponible,CLOSED) VALUES ({0},{1},{2},{3}) "
                            ssql = String.Format(ssql, id_Disp_Hab, cod_operador, 0, closeOper)
                            ind.bk.DBMgr.Execute(ssql, trans)
                        End If

                    Next

                    daySel = daySel.AddDays(1)
                End While

                ind.bk.DBMgr.CommitTransaction(trans)

            Catch ex As Exception
                ind.bk.DBMgr.rollbackTransaction(trans)
                Throw ex
            Finally
                ind.bk.DBMgr.disposeTransaction(trans)
            End Try
        End Sub


        Public Class caldisp
            Inherits calendarDay
            Public cod_habitacion As Integer
            Public availability As Integer
            Public Sub New(newDay As Date)
                MyBase.New(newDay)
            End Sub
            Public Function LogDisp(Optional logThis As Boolean = True)
                Dim s As String = String.Format(" Day:{0} CodRoom: {1} disp:{2}", Me.dia, Me.cod_habitacion, Me.availability)
                If logThis = True Then
                    System.Diagnostics.Debug.WriteLine(s)
                End If
                Return s
            End Function
        End Class

    End Class



End Namespace

