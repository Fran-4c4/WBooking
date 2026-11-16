Imports System.Data
Imports System.Linq
Imports System.Diagnostics
Imports System.Collections.Generic

Partial Class Admin_tariffCalendar
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P2_Disponibilidad

    Public monthSel As Integer
    Public yearSel As Integer
    Public initialDay As Integer
    Public cod_operadorAvalibilitySelected As Integer = 0
    Public cod_operadorTarifasSelected As Integer = 0
    Public dateStart As Date
    Public dateEnd As Date






    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.availability)

            'Me.configNameFieldsPrivate()

            If Not Page.IsPostBack Then
                Me.configCbos()
                Me.monthSel = Month(Date.Now)
                Me.yearSel = Year(Date.Now)
                Me.getDvUnits()
                Me.ConfigureSearchValues()
            Else
                Me.ConfigureSearchValues()
                Dim checkindateES As String = Me.checkindate.Text.Replace("-", "/")
                Dim checkoutdateES As String = Me.checkoutdate.Text.Replace("-", "/")
                If checkindateES = "" Then
                    Me.checkindate.Text = Date.Now.ToString("dd/MM/yyyy")
                    Me.checkoutdate.Text = Date.Now.ToString("dd/MM/yyyy")
                Else
                    Dim datein As Date = DateTime.ParseExact(checkindateES, "dd/MM/yyyy", New System.Globalization.CultureInfo("es-ES"))
                    Dim dateOut As Date = DateTime.ParseExact(checkoutdateES, "dd/MM/yyyy", New System.Globalization.CultureInfo("es-ES"))
                    Me.checkindate.Text = DateTime.ParseExact(checkindateES, "dd/MM/yyyy", New System.Globalization.CultureInfo("es-ES"))
                    Me.checkoutdate.Text = DateTime.ParseExact(checkoutdateES, "dd/MM/yyyy", New System.Globalization.CultureInfo("es-ES"))
                End If

            End If

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Sub

    Private Sub configCbos()
        Me.cod_operadorAvalibilitySelected = Me.hotel.freesaleOperatorObj.cod_operador

        Me.dvClients = getDvClients()
        fillCboOperators(Me.lstclients, Me.dvClients)

        With Me.cbooperatorsymclose.Items
            .Add(New ListItem("Aplicar a todos los operadores", "-2"))
            fillCboOperators(Me.cbooperatorsymclose, Me.dvClients)
        End With
        Me.cbooperatorsymclose.SelectedValue = "-2"


        Me.dvRooms = Me.getDvRoomsAll(filterWithoutCombined:=True)


        With Me.lstrooms.Items
            .Add(New ListItem("Aplicar a todas las unidades", "-1"))
            fillCboUnits(Me.lstrooms, Me.dvRooms)

        End With
        Me.lstrooms.SelectedValue = "-1"

        'cbos modal
        For Each drv As DataRowView In Me.dvRooms
            Me.cbounit2changeprice.Items.Add(New ListItem(drv(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel), drv(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel)))
        Next


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

        Me.checkindate.Text = Date.Now.ToString("dd/MM/yyyy")
        Me.checkoutdate.Text = Date.Now.ToString("dd/MM/yyyy")

    End Sub



    Friend Function getCalendarHeader() As CalendarDays
        Dim cui As New CalendarUI

        If Month(Date.Now) = monthSel And Date.Now.Year = Me.yearSel Then
            initialDay = Day(Date.Now)
        Else
            initialDay = 1
        End If
        Me.dateStart = New Date(yearSel, monthSel, initialDay)
        cui.ConfigureFromMonth(dateStart, 0, initialDay)
        Return cui.calendarDaysObj
    End Function

    Friend Function getClientCupo() As DataView
        Return ind.bk.web.Availability_mgr.getClientCupoByOperador(Me.codhotel, Me.cod_operadorAvalibilitySelected, Me.yearSel, Me.monthSel, True)
    End Function



    Protected Sub btnSeeAvailability_Click(sender As Object, e As EventArgs) Handles btnSeeAvailability.Click

        Me.cboday1.SelectedValue = 1
        ConfigureSearchValues()
    End Sub

    Protected Sub cbomonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbomonth.SelectedIndexChanged
        Me.cboday1.SelectedValue = 1
        ConfigureSearchValues()
    End Sub
    Private Sub lstClients_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstclients.SelectedIndexChanged
        Me.cboday1.SelectedValue = 1
        ConfigureSearchValues()
    End Sub

    Friend Sub ConfigureSearchValues()
        Try
            Me.cod_operadorAvalibilitySelected = Me.lstclients.SelectedValue
            AdminTenantSecurity.RequireOperator(Me.codhotel, Me.cod_operadorAvalibilitySelected)
            If Me.cod_operadorAvalibilitySelected = Me.hotel.freesaleOperatorObj.cod_operador Then
                'for tariff we need cod operator private client
                Me.cod_operadorTarifasSelected = Me.hotel.PrivateoperatorObj.cod_operador
            Else
                Me.cod_operadorTarifasSelected = Me.cod_operadorAvalibilitySelected
            End If
            AdminTenantSecurity.RequireOperator(Me.codhotel, Me.cod_operadorTarifasSelected)
            Dim daySelected As Date = Me.cbomonth.SelectedValue
            Me.monthSel = daySelected.Month
            Me.yearSel = daySelected.Year
            If Month(Date.Now) = daySelected.Month And daySelected.Year = Date.Now.Year Then
                initialDay = Day(Date.Now)
            Else
                initialDay = 1
            End If
            Me.dateStart = New Date(yearSel, monthSel, initialDay)
            Dim DaysInMonth As Integer = Date.DaysInMonth(dateStart.Year, dateStart.Month)
            Me.dateEnd = New Date(dateStart.Year, dateStart.Month, DaysInMonth)

            'Me.checkindate.Text = dateStart.ToString("dd/MM/yyyy")
            'Me.checkoutdate.Text = dateEnd.ToString("dd/MM/yyyy")

            Me.litSelectedMonth.Text = " Disponibilidad y tarifas " & CalendarUI.getMonthName(Me.monthSel) & " " & Me.yearSel
            Me.dvRooms = Me.getDvUnits
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Sub




    Friend Function getDvTariffList(cod_habitacion As Integer) As Object
        Try
            AdminTenantSecurity.RequireRoom(Me.codhotel, cod_habitacion)
            AdminTenantSecurity.RequireOperator(Me.codhotel, Me.cod_operadorTarifasSelected)
            Dim queryDef
            Using ctx As New cfrbookEntities
                queryDef = (From tar In ctx.R_tarifasEF
                            Join tarHab In ctx.R_Tarifa_habitacionEF
                            On tar.cod_tarifa Equals tarHab.cod_tarifa
                            Join oper In ctx.R_Tarifa_operadorEF On tar.cod_tarifa Equals oper.cod_tarifa
                            Where tar.cod_hotel = Me.codhotel And tarHab.cod_habitacion = cod_habitacion And oper.cod_operador = Me.cod_operadorTarifasSelected
                            Order By tarHab.cod_habitacion
                            Select New With
                            {
                                .id_Tar_Hab = tarHab.id,
                                .cod_tarifa = tar.cod_tarifa,
                                .cod_habitacion = tarHab.cod_habitacion,
                                .tarifa = tar.tarifa,
                                .diasminimos = tar.DiasMinimos
                            }).ToList
            End Using

            Return queryDef


        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Function


    Friend Function getDvTariffPriceList(id_Tar_HabSelected As Integer) As List(Of R_PriceDayEF)
        Try
            AdminTenantSecurity.RequireTariffRoom(Me.codhotel, id_Tar_HabSelected)
            Return Me.getDvTariffPriceListBase(id_Tar_HabSelected, Me.dateStart, Me.dateEnd)


        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Function



    Private Function createPriceValuesForTariff(id_Tar_HabSelected As Integer) As Boolean
        AdminTenantSecurity.RequireTariffRoom(Me.codhotel, id_Tar_HabSelected)
        Dim creationDate As Date = Date.Now
        Using ctx As New cfrbookEntities

            Dim dayTo As Date = Me.dateStart
            While dayTo <= Me.dateEnd

                Dim nDayPrice As New R_PriceDayEF
                nDayPrice.id_Tar_Hab = id_Tar_HabSelected
                nDayPrice.dayprice = dayTo
                nDayPrice.price = 0
                nDayPrice.ModifyDate = creationDate
                dayTo = dayTo.AddDays(1)

                ctx.R_PriceDayEF.Add(nDayPrice)
            End While
            ctx.SaveChanges()
        End Using

    End Function

    Protected Sub btnsaveavailability_Click(sender As Object, e As EventArgs) Handles btnsaveavailability.Click

        Dim ctx As New cfrbookEntities
        Try
            ConfigureSearchValues()
            AdminTenantSecurity.RequireOperator(Me.codhotel, Me.cod_operadorAvalibilitySelected)

            Dim codhabitacionhotel As Integer
            Dim idia As Integer
            Dim dayStart As Integer = 1
            Dim dayEnd As Integer = 31

            Dim dispHotel As New ArrayList
            For Each drRoom As DataRowView In Me.dvRooms
                codhabitacionhotel = CInt(drRoom(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel))
                AdminTenantSecurity.RequireRoom(Me.codhotel, codhabitacionhotel)
                For iday As Integer = dayStart To dayEnd
                    Dim dispValueObj As Object = Request.Form("h" & codhabitacionhotel & "d" & iday)
                    If Not dispValueObj Is Nothing Then
                        If dispValueObj = "" Then dispValueObj = 0

                        Dim disp As New ind.bk.web.Availability_mgr.caldisp(New Date(Me.yearSel, Me.monthSel, iday))
                        disp.cod_habitacion = codhabitacionhotel
                        disp.availability = dispValueObj
                        dispHotel.Add(disp)
                    Else
                        Debug.WriteLine("h" & codhabitacionhotel & "d" & iday)
                    End If
                Next
            Next

            ind.bk.web.Availability_mgr.UpdateDisponib(Me.codhotel, Me.cod_operadorAvalibilitySelected, dispHotel, Me.monthSel, Me.yearSel)

            Dim alPrices As New ArrayList

            'ctx.Database.BeginTransaction()
            Dim modifydate As Date = Date.Now

            For Each keyForm As String In Page.Request.Form.AllKeys

                If Not keyForm Is Nothing Then
                    If keyForm.StartsWith("tarcode_") Then

                        Dim key As String = keyForm.Remove(0, 7)
                        Dim keySplit As String() = key.Split("_")

                        'Debug.WriteLine("Tar code: " & keySplit(0) & " Value: " & keySplit(1) & " for " & keyForm)
                        Dim priceID As Integer
                        If keySplit.Length < 3 OrElse Not Integer.TryParse(keySplit(2), priceID) Then
                            Throw New HttpException(400, "The price identifier is invalid.")
                        End If
                        Dim priceEF As R_PriceDayEF =
                            (From price In ctx.R_PriceDayEF
                             Where price.id = priceID AndAlso
                                   price.R_Tarifa_habitacion.R_tarifas.cod_hotel = Me.codhotel
                             Select price).FirstOrDefault()
                        If priceEF Is Nothing Then
                            Throw New HttpException(404, "The price was not found.")
                        End If
                        Dim newPrice As Double = CDbl(cfr.tool.Tools.cNullStr(Page.Request.Params(keyForm), 0))

                        Dim keyMinDays As String = keyForm.Replace("tarcode_", "tarcodeminday_")
                        Dim newMinimumDays As Integer = CInt(cfr.tool.Tools.cNullStr(Page.Request.Params(keyMinDays), 0))

                        Dim keyRelease As String = keyForm.Replace("tarcode_", "tarcoderelease_")
                        Dim newRelease As Integer = CInt(cfr.tool.Tools.cNullStr(Page.Request.Params(keyRelease), 0))


                        Dim ischanged As Boolean = False
                        If priceEF.price <> newPrice And newPrice >= 0 Then
                            priceEF.price = newPrice
                            ischanged = True
                        End If
                        If priceEF.minimumdays <> newMinimumDays And newMinimumDays >= 0 Then
                            priceEF.minimumdays = newMinimumDays
                            ischanged = True
                        End If
                        If priceEF.release <> newRelease And newRelease >= 0 Then
                            priceEF.release = newRelease
                            ischanged = True
                        End If

                        If ischanged = True Then
                            priceEF.ModifyDate = modifydate

                            ctx.SaveChanges()
                        End If

                    End If
                End If
            Next

            'Me.debugRequestConsole()


            'ctx.Database.CurrentTransaction.Commit()
            cfr.pages.SiteBasePage.showMessage("La disponibilidad y las tarifas  han sido modificados", Me.Page, CFRConst.msgEnum.Ok)
        Catch ex As Exception
            'ctx.Database.CurrentTransaction.Rollback()
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        Finally

        End Try
    End Sub

    Private Sub btnmodaledittariffpricesave_Click(sender As Object, e As EventArgs) Handles btnmodaledittariffpricesave.Click

        Try
            'save
            If Page.IsValid = False Then
                Exit Sub
            End If
            Dim checkindateES As String = Me.checkindate.Text.Replace("-", "/")
            Dim checkoutdateES As String = Me.checkoutdate.Text.Replace("-", "/")

            Dim datein As Date = DateTime.ParseExact(checkindateES, "dd/MM/yyyy", New System.Globalization.CultureInfo("es-ES"))
            Dim dateOut As Date = DateTime.ParseExact(checkoutdateES, "dd/MM/yyyy", New System.Globalization.CultureInfo("es-ES"))
            If dateOut < datein Then
                cfr.pages.SiteBasePage.showMessage("La fecha final no puede ser inferior a la inicial", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            ElseIf datein < Date.Now.Date Then
                cfr.pages.SiteBasePage.showMessage("La fecha de inicio no puede ser inferior a la actual", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If


            Dim cod_tarifa As Integer = Me.hddmodaledittariffprice_tariff_cod_tarifa.Value
            Dim cod_unit As Integer = Me.cbounit2changeprice.SelectedValue
            AdminTenantSecurity.RequireTariffRoomPair(Me.codhotel, cod_tarifa, cod_unit)


            Dim newPrice As Double = CDbl(Me.txtmodaledittariffprice.Text)
            Dim modifyDate As Date = Date.Now

            'get selected week days
            Dim selectedDays As Integer() = getSelectedWeekDays()

            Dim optionprice As Integer
            If Me.optpricepercent.Checked Then
                optionprice = 1
            ElseIf Me.optpricevalue.Checked Then
                optionprice = 2
            ElseIf Me.optpricesubst.Checked Then
                optionprice = 3
            End If



            'for new items we need the id of tariff/room relation wich is id value
            Const sql As String =
                "SELECT id FROM R_Tarifa_habitacion WHERE COD_TARIFA = @tariffId AND cod_habitacion = @roomId"
            Dim DT As DataTable = ind.bk.DBMgr.getDT(
                sql,
                New SqlClient.SqlParameter("@tariffId", SqlDbType.Int) With {.Value = cod_tarifa},
                New SqlClient.SqlParameter("@roomId", SqlDbType.Int) With {.Value = cod_unit})
            Dim id_tar_hab As Integer = DT.Rows(0)(0)

            Dim daySel As Date = datein
            Using ctx As New cfrbookEntities

                Dim priceList As List(Of R_PriceDayEF) = getDvTariffPriceListByTariff_UnitbyRange(cod_tarifa, cod_unit, datein, dateOut)
                Dim dic As Dictionary(Of Date, R_PriceDayEF) = priceList.ToDictionary(Function(k) k.dayprice)

                While daySel <= dateOut
                    Dim dayWeekNumber As Integer = DatePart("w", daySel, vbMonday) '1-7
                    Dim isInweekDays As Integer = Array.Find(selectedDays, Function(x) (x = dayWeekNumber))

                    If isInweekDays = 0 Then
                        daySel = daySel.AddDays(1)
                        Continue While
                    End If
                    Dim priceEF As R_PriceDayEF = Nothing
                    Dim isNew As Boolean = False
                    dic.TryGetValue(daySel, priceEF)
                    If priceEF Is Nothing Then
                        priceEF = New R_PriceDayEF
                        priceEF.id_Tar_Hab = id_tar_hab
                        priceEF.dayprice = daySel
                        priceEF.price = 0
                        ctx.R_PriceDayEF.Add(priceEF)
                        isNew = True
                    End If

                    If chkprice.Checked Then
                        'priceEF.id_Tar_Hab = keySplit(0)
                        Dim priceSet As Double = priceEF.price
                        If optionprice = 1 Then
                            priceSet = priceSet * (1 + (newPrice / 100))
                        ElseIf optionprice = 2 Then
                            priceSet = priceSet + newPrice
                        ElseIf optionprice = 3 Then
                            priceSet = newPrice
                        End If
                        If priceSet < 0 Then
                            priceSet = 0
                        End If
                        If priceEF.price <> priceSet Then
                            priceEF.price = priceSet
                        End If
                    End If

                    If chkmindays.Checked Then
                        priceEF.minimumdays = CInt(Me.txtmodaledittariffminimumdays.Text)
                    End If

                    If chkreleaseedit.Checked Then
                        priceEF.release = CInt(Me.txtmodaledittariffrelease.Text)
                    End If

                    priceEF.ModifyDate = modifyDate
                    If isNew = False Then
                        ctx.Entry(priceEF).State = Entity.EntityState.Modified
                    End If

                    daySel = daySel.AddDays(1)
                End While
                ctx.SaveChanges()
            End Using

            'saveSeasonScript(Me.objidhidden.Value)
            'Me.litmsgresult.Text = UIMsg.UIMessageDismiss("Periodo creado", CFRConst.msgEnum.Ok)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Function getSelectedWeekDays() As Integer()
        'get selected week days
        'Dim wd As String = Request.Form("week_valid_days")
        'Dim selectedDays As Integer() = wd.Split(",").Select(Function(n) Integer.Parse(n)).ToArray
        Dim selectedDays As New ArrayList
        If Me.weekday1.Checked Then
            selectedDays.Add(1)
        End If
        If Me.weekday2.Checked Then
            selectedDays.Add(2)
        End If
        If Me.weekday3.Checked Then
            selectedDays.Add(3)
        End If
        If Me.weekday4.Checked Then
            selectedDays.Add(4)
        End If
        If Me.weekday5.Checked Then
            selectedDays.Add(5)
        End If
        If Me.weekday6.Checked Then
            selectedDays.Add(6)
        End If
        If Me.weekday7.Checked Then
            selectedDays.Add(7)
        End If

        Return selectedDays.ToArray().Select(Function(n) Integer.Parse(n)).ToArray
    End Function




    Private Sub btnsaveunitavailability_Click(sender As Object, e As EventArgs) Handles btnsaveunitavailability.Click

        Dim ctx As New cfrbookEntities
        Try
            ConfigureSearchValues()

            Dim dayStart As Integer = 1
            Dim dayEnd As Integer = 31

            'save
            If Page.IsValid = False Then
                Exit Sub
            End If
            Dim checkindateES As String = Me.checkindate.Text.Replace("-", "/")
            Dim checkoutdateES As String = Me.checkoutdate.Text.Replace("-", "/")

            Dim datein As Date = DateTime.ParseExact(checkindateES, "dd/MM/yyyy", New System.Globalization.CultureInfo("es-ES"))
            Dim dateOut As Date = DateTime.ParseExact(checkoutdateES, "dd/MM/yyyy", New System.Globalization.CultureInfo("es-ES"))
            If dateOut < datein Then
                cfr.pages.SiteBasePage.showMessage("La fecha final no puede ser inferior a la inicial", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            ElseIf datein < Date.Now.Date Then
                cfr.pages.SiteBasePage.showMessage("La fecha de inicio no puede ser inferior a la actual", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If


            Dim unitID As Integer = Me.cbounit2changeprice.SelectedValue 'unit selected
            AdminTenantSecurity.RequireRoom(Me.codhotel, unitID)
            AdminTenantSecurity.RequireOperator(Me.codhotel, Me.cod_operadorAvalibilitySelected)
            Dim newAvailab As Double = CDbl(Me.txtmodaleditavailability.Text) 'availability value
            Dim modifyDate As Date = Date.Now

            'Me.debugRequestConsole()
            Dim dtr As New ind.bk.date.DateRangeCls(datein, dateOut)
            Me.UpdateUnitAvailability(Me.codhotel, Me.cod_operadorAvalibilitySelected, unitID, newAvailab, dtr)

            cfr.pages.SiteBasePage.showMessage("La disponibilidad ha sido cambiada", Me.Page, CFRConst.msgEnum.Ok)
        Catch ex As Exception

            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        Finally

        End Try
    End Sub

    Public Sub UpdateUnitAvailability(cod_hotel As Integer, cod_operador As Integer, unitID As Integer, newAvailabValue As Integer, dateRange As ind.bk.date.DateRangeCls)
        If cod_hotel <> Me.codhotel Then
            Throw New HttpException(404, "Hotel not found.")
        End If
        AdminTenantSecurity.RequireOperator(Me.codhotel, cod_operador)
        AdminTenantSecurity.RequireRoom(Me.codhotel, unitID)
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

            Dim optionavail As Integer
            If Me.optavailabilityadd.Checked Then
                optionavail = 1
            ElseIf Me.optavailabilityreduce.Checked Then
                optionavail = 2
            ElseIf Me.optavailabilitysubst.Checked Then
                optionavail = 3
            End If

            While daySel.Date <= dateRange.dateTo.Date
                Dim dayToCheck As String = daySel.ToString("yyyy/MM/dd")

                dvCupoByClient.RowFilter = "dia=#" & dayToCheck & "# AND cod_habitacion=" & unitID

                If dvCupoByClient.Count > 0 Then 'EXISTE ESE DIA PARA EL OPERADOR=> ACTUALIZAMOS
                    Dim ors1 As DataRowView = dvCupoByClient(0)
                    Dim operCupo As New ind.bk.obj.R_Res_cupos
                    operCupo.ID = CInt(ors1("id"))
                    operCupo.id_Disp_Hab = ors1("id_Disp_Hab")
                    If optionavail = 1 Then
                        operCupo.disponible = newAvailabValue + ors1("disponible")
                    ElseIf optionavail = 2 Then
                        Dim res As Integer = ors1("disponible") - newAvailabValue
                        If res < 0 Then res = 0
                        operCupo.disponible = res
                    ElseIf optionavail = 3 Then
                        operCupo.disponible = newAvailabValue
                    End If

                    operCupo.cod_operador = cod_operador

                    Dim operCupoMgr As New ind.bk.obj.R_Res_cupos_Manager
                    operCupoMgr.Update(operCupo, trans)


                Else 'insert si no existe
                    'with reduce availability we do not need to do nothing. 
                    If optionavail = 1 Or optionavail = 3 Then
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
                        Dim operCupo As New ind.bk.obj.R_Res_cupos
                        operCupo.id_Disp_Hab = id_Disp_Hab
                        operCupo.disponible = newAvailabValue
                        operCupo.cod_operador = cod_operador

                        Dim operCupoMgr As New ind.bk.obj.R_Res_cupos_Manager
                        operCupoMgr.insert(operCupo, trans)
                    End If

                End If
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



#Region " UI helper"
    Public Function getWeekendClass(ordt As calendarDay) As String
        Dim weekendClass As String = ""
        If ordt.weekDay = 6 Or ordt.weekDay = 7 Then
            weekendClass = "cal-header-week"
        End If
        Return weekendClass
    End Function

    Public Function getWeekendClass(daySelected As Date) As String
        Dim weekendClass As String = ""
        Dim weekDay As Integer = DatePart("w", daySelected, vbMonday)
        If weekDay = 6 Or weekDay = 7 Then
            weekendClass = "cal-header-week"
        End If
        Return weekendClass
    End Function

    Private Sub btnsaveymclose_Click(sender As Object, e As EventArgs) Handles btnsaveymclose.Click
        Try
            'save
            If Page.IsValid = False Then
                Exit Sub
            End If

            Dim datein As Date = DateTime.ParseExact(Me.checkindate.Text, "dd/MM/yyyy", Nothing)
            Dim dateOut As Date = DateTime.ParseExact(Me.checkoutdate.Text, "dd/MM/yyyy", Nothing)
            If dateOut < datein Then
                cfr.pages.SiteBasePage.showMessage("La fecha final no puede ser inferior a la inicial", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            ElseIf datein < Date.Now.Date Then
                cfr.pages.SiteBasePage.showMessage("La fecha de inicio no puede ser inferior a la actual", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If

            Dim ymclose As Integer
            If Me.rdbymclose.Checked Then
                ymclose = 1
            Else
                ymclose = 0
            End If


            Dim DTR As New ind.bk.date.DateRangeCls(datein, dateOut)



            Dim unitsTocloseList As New ArrayList
            If Me.lstrooms.SelectedValue = "-1" Then
                For Each drv As DataRowView In Me.dvRooms
                    unitsTocloseList.Add(drv(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel))
                Next
            Else
                unitsTocloseList.Add(Me.lstrooms.SelectedValue)
            End If

            For Each roomCode As Object In unitsTocloseList
                AdminTenantSecurity.RequireRoom(Me.codhotel, CInt(roomCode))
            Next

            If Me.cbooperatorsymclose.SelectedValue = "-2" Then 'all operators
                For Each drv As DataRowView In Me.dvClients
                    AdminTenantSecurity.RequireOperator(Me.codhotel, CInt(drv(ind.bk.obj.R_operadores.DBCol.cod_operador)))
                    ind.bk.web.Availability_mgr.UpdateUnitYMClosed(Me.codhotel, drv(ind.bk.obj.R_operadores.DBCol.cod_operador), unitsTocloseList, ymclose, DTR)
                Next
            Else
                AdminTenantSecurity.RequireOperator(Me.codhotel, CInt(Me.cbooperatorsymclose.SelectedValue))
                ind.bk.web.Availability_mgr.UpdateUnitYMClosed(Me.codhotel, Me.cbooperatorsymclose.SelectedValue, unitsTocloseList, ymclose, DTR)

            End If






            cfr.pages.SiteBasePage.showMessage(
                "Se han creado las restricciones de cierre de ventas. <small>Puede ver todos los cierres en la pantalla de cierre de ventas</small>",
                Me.Page,
                CFRConst.msgEnum.Ok,
                trustedHtml:=True)

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        Finally
            'Me.dvClients = getDvClients()
            'getDvListPeriodos()
            'saveSeasonScript()
        End Try
    End Sub





#End Region

End Class
