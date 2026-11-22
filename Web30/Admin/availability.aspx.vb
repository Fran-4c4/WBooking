Imports System.Data
Imports System.Diagnostics
Partial Class Admin_availability
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P2_Disponibilidad

    Public monthSel As Integer
    Public yearSel As Integer
    Public cod_operadorSel As Integer

    Public Shadows dvClients As DataView
    Public Shadows dvRooms As DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.availability)

            'Me.configNameFieldsPrivate()

            If Not Page.IsPostBack Then
                Me.configCbos()
                Me.monthSel = Month(Date.Now)
                Me.yearSel = Year(Date.Now)
                Me.getDvRooms()
                Me.getRoomCombined()
                Me.ConfigureSearchValues()
            Else

            End If

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Sub

    Private Sub configCbos()
        Me.dvClients = getDvClients()
        Dim ventalibrecode As Integer = 0
        With Me.lstClients.Items

            For Each drv As DataRowView In Me.dvClients
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
        Me.lstClients.SelectedValue = ventalibrecode

        With Me.cbomonth.Items

            Dim dayLoop As Date = Date.Now
            Dim dayStart As Date = Date.Now
            For imonth As Integer = 0 To 35

                dayLoop = dayStart.AddMonths(imonth)

                Dim monthName As String = CalendarUI.getMonthName(dayLoop)
                .Add(New ListItem(dayLoop.Year & " - " & monthName, dayLoop.Year & "-" & dayLoop.Month & "-1"))
            Next
        End With
        Me.cbomonth.SelectedValue = Month(Date.Now) & " - " & Date.Now.Year

        For x As Integer = 1 To 31
            Me.cboday1.Items.Add(New ListItem(x, x))
            Me.cboday2.Items.Add(New ListItem(x, x))
        Next
        Me.cboday1.SelectedValue = Date.Now.Day
        Me.cboday2.SelectedValue = 31
    End Sub


    Friend Function getCalendarHeader() As CalendarDays
        Dim cui As New CalendarUI
        Dim initialDay As Integer
        If Month(Date.Now) = monthSel Then
            initialDay = Day(Date.Now)
        Else
            initialDay = 1
        End If
        Dim dateStart As New Date(yearSel, monthSel, initialDay)
        cui.ConfigureFromMonth(dateStart, 0, initialDay)
        Return cui.calendarDaysObj
    End Function

    Friend Function getClientCupo() As DataView
        Return getClientCupoByOperador(Me.codhotel, Me.cod_operadorSel, Me.yearSel, Me.monthSel)
    End Function


    Friend Shared Function getClientCupoByOperador(codhotel As Integer, cod_operadorSel As Integer, yearSel As Integer, monthSel As Integer, Optional operUpdate As Boolean = False) As DataView

        Dim sSQL As String

        Dim fecha1 As Date, fecha2 As Date
        If monthSel = Month(Now) And yearSel = Year(Now) Then
            fecha1 = New Date(yearSel, monthSel, Day(Now))
        Else
            fecha1 = New Date(yearSel, monthSel, 1)
        End If

        Dim DaysInMonth As Integer = Date.DaysInMonth(fecha1.Year, fecha1.Month)
        fecha2 = New Date(yearSel, monthSel, DaysInMonth)

        Dim fecha1Str As String = fecha1.ToString("yyyyMMdd")
        Dim fecha2Str As String = fecha2.ToString("yyyyMMdd")

        sSQL = " SELECT     * "
        sSQL = sSQL & " FROM R_disponibilidad INNER JOIN "
        sSQL = sSQL & " R_Res_cupos ON R_disponibilidad.id_Disp_Hab = R_Res_cupos.id_Disp_Hab "
        sSQL = sSQL & " Where R_disponibilidad.cod_Hotel = " & codhotel
        sSQL = sSQL & " AND R_Res_cupos.COD_OPERADOR=" & cod_operadorSel
        sSQL = sSQL & " AND (DIA>=CONVERT(DATETIME,'" & fecha1Str & "',113) AND DIA<=CONVERT(DATETIME,'" & fecha2Str & "',113))"
        If operUpdate = False Then
            sSQL = sSQL & " and disponible >0"
        End If


        Dim dt As DataTable = ind.bk.DBMgr.getDT(sSQL)
        Return dt.DefaultView

    End Function

    Protected Sub btnSeeAvailability_Click(sender As Object, e As EventArgs) Handles btnSeeAvailability.Click

        Me.cboday1.SelectedValue = 1
        ConfigureSearchValues()
    End Sub

    Protected Sub lstClients_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstClients.SelectedIndexChanged
        ConfigureSearchValues()
    End Sub

    Protected Sub cbomonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbomonth.SelectedIndexChanged
        ConfigureSearchValues()
    End Sub


    Friend Sub ConfigureSearchValues()
        Try
            Dim daySelected As Date = Me.cbomonth.SelectedValue
            Me.monthSel = daySelected.Month
            Me.yearSel = daySelected.Year
            Me.litSelectedMonth.Text = " " & CalendarUI.getMonthName(Me.monthSel) & " " & Me.yearSel
            Me.cod_operadorSel = Me.lstClients.SelectedValue
            AdminTenantSecurity.RequireOperator(Me.codhotel, Me.cod_operadorSel)
            Me.dvRooms = Me.getDvRooms
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Sub

    Friend Function getDvRooms() As DataView
        Try
            Me.dvRooms = Me.getDvRoomsAll
            Me.getRoomCombined()

            Return Me.dvRooms
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
        Return Nothing
    End Function


    Friend Shadows roomsCombinedList As New ArrayList
    Friend Shadows roomsCombinedList1 As New ArrayList
    Friend Shadows Class roomCombined
        Public roomWithCupoCode As Integer
        Public roomCombinedCode As Integer
        Public roomCombinedName As String
        Public Sub New(roomWithCupoCode As Integer, roomCombinedCode As Integer, roomCombinedName As String)
            Me.roomWithCupoCode = roomWithCupoCode
            Me.roomCombinedCode = roomCombinedCode
            Me.roomCombinedName = roomCombinedName
        End Sub
    End Class

    Friend Overloads Function getRoomCombined()
        Dim oRscom As New DataView(Me.dvRooms.Table)
        Dim oRscomFull As New DataView(Me.dvRooms.Table)

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
            Dim sbody As String = String.Empty
            For Each roomCombined As String In Me.roomsCombinedList
                sbody += String.Format("<li>{0}</li>", roomCombined)
            Next
            s = String.Format(s, sbody)
            'Me.litroomcombined.Text = s
        End If

        Return Nothing
    End Function

    Friend Overloads Function getRoomCombinedIU(code As Integer) As String
        For Each el As roomCombined In roomsCombinedList1
            If el.roomWithCupoCode = code Then
                Return "<br/><span class='roomcombined lblwarn lblwarn-sm'>Combina con: " & el.roomCombinedName & "</span>"
            End If
        Next
        Return Nothing
    End Function

    Protected Sub btnsaveavailability_Click(sender As Object, e As EventArgs) Handles btnsaveavailability.Click
        Try
            ConfigureSearchValues()

            Dim codhabitacionhotel As Integer
            Dim dayStart As Integer = 1
            Dim dayEnd As Integer = 31

            Dim dispHotel As New ArrayList
            For Each drRoom As DataRowView In Me.dvRooms
                codhabitacionhotel = CInt(drRoom(ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.codhabitacionhotel))
                AdminTenantSecurity.RequireRoom(Me.codhotel, codhabitacionhotel)
                For iday As Integer = dayStart To dayEnd
                    Dim dispValueObj As Object = Request.Form("h" & codhabitacionhotel & "d" & iday)
                    If Not dispValueObj Is Nothing Then
                        If dispValueObj = "" Then dispValueObj = 0

                        Dim disp As New caldisp(New Date(Me.yearSel, Me.monthSel, iday))
                        disp.cod_habitacion = codhabitacionhotel
                        disp.availability = dispValueObj
                        dispHotel.Add(disp)
                    Else
                        Debug.WriteLine("h" & codhabitacionhotel & "d" & iday)
                    End If

                Next
            Next

            'Me.debugRequestConsole()
            
       
            UpdateDisponib(Me.codhotel, Me.cod_operadorSel, dispHotel, Me.monthSel, Me.yearSel)
            

            cfr.pages.SiteBasePage.showMessage("La disponibilidad ha sido cambiada", Me.Page, CFRConst.msgEnum.Ok)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub
    'Public Function mkRSDatosDisp()
    '    Dim orec
    '    orec = server.createobject("ADODB.Recordset")
    '    orec.Fields.Append("cod_hotel", adDouble, 12, adFldIsNullable)
    '    orec.Fields.Append("cod_habitacion", adDouble, 12, adFldIsNullable)
    '    orec.Fields.Append("dia", advarchar, 12, adFldIsNullable)
    '    orec.Fields.Append("disponibilidad", adDouble, 12, adFldIsNullable)
    '    orec.Open()
    '    mkRSDatosDisp = orec
    'End Function
    Private Class caldisp
        Inherits calendarDay
        Public cod_habitacion As Integer
        Public availability As Integer
        Public Sub New(newDay As Date)
            MyBase.New(newDay)
        End Sub
        Public Function LogDisp(Optional logThis As Boolean = True)
            Dim s As String = String.Format(" Day:{0} CodRoom: {1} disp:{2}", Me.dia, Me.cod_habitacion, Me.availability)
            If logThis = True Then
                Debug.WriteLine(s)
            End If
            Return s
        End Function
    End Class

    Public Function UpdateDisponib(cod_hotel As Integer, cod_operador As Integer, oRec As ArrayList, iMes As Integer, iYear As Integer)
        If cod_hotel <> Me.codhotel Then
            Throw New HttpException(404, "Hotel not found.")
        End If
        AdminTenantSecurity.RequireOperator(Me.codhotel, cod_operador)
        For Each dayDisp As caldisp In oRec
            AdminTenantSecurity.RequireRoom(Me.codhotel, dayDisp.cod_habitacion)
        Next

        Dim oRDias As DataView

        Dim id_Disp_Hab As Integer  'el id de la habitacion
        Dim ocon As SqlClient.SqlConnection
        Dim trans As SqlClient.SqlTransaction = Nothing
        
        Try
            If oRec.Count = 0 Then
                Return Nothing
            End If


            'cupo
            Dim dvCupoByClient As DataView = getClientCupoByOperador(cod_hotel, cod_operador, iYear, iMes, True)
            'disponibilidad
            oRDias = listAvailability(cod_hotel, cod_operador, iMes, iYear)

            'abrimos la conexion para el update
            ocon = ind.bk.DBMgr.getNewConn
            'comienzo de la transaccion
            trans = ind.bk.DBMgr.CreateTransaction(ocon)

            For Each dayDisp As caldisp In oRec
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

                        'sSQL = "UPDATE r_res_cupos "
                        'sSQL = sSQL & " Set r_res_cupos.Disponible = " & dayDisp.availability
                        'sSQL = sSQL & " FROM r_Disponibilidad  INNER JOIN r_res_cupos"
                        'sSQL = sSQL & " ON r_Disponibilidad.id_Disp_Hab=r_res_cupos.id_Disp_Hab"
                        'sSQL = sSQL & " Where cod_hotel =" & cod_hotel
                        'sSQL = sSQL & " And cod_Habitacion =" & dayDisp.cod_habitacion
                        'sSQL = sSQL & " And cod_operador =" & cod_operador
                        'sSQL = sSQL & " AND DIA =CONVERT(DATETIME,'" & dayDisp.getSqlDateyyyyMMdd & "',126)"

                        'Dim rowsUpdated As Integer = ind.bk.DBMgr.Execute(sSQL, trans)

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

        Return Nothing
    End Function

    Private Function listAvailability(cod_hotel As Integer, cod_operador As Integer, iMes As Integer, iYear As Integer) As DataView

        Dim sSQL As String
        Dim dayStart As Date = New Date(iYear, iMes, 1)
        Dim dayEnd As Date = CalendarUI.getLastDayOfMonth(dayStart)

        sSQL = "SELECT  * "
        sSQL = sSQL & " FROM r_disponibilidad "
        sSQL = sSQL & " Where cod_hotel =" & cod_hotel
        sSQL = sSQL & " AND (DIA >=CONVERT(DATETIME,'" & dayStart.ToString("yyyy-MM-dd") & "',126)"
        sSQL = sSQL & " AND DIA <=CONVERT(DATETIME,'" & dayEnd.ToString("yyyy-MM-dd") & "',126))"


        Return ind.bk.DBMgr.getDT(sSQL).DefaultView


    End Function


End Class
