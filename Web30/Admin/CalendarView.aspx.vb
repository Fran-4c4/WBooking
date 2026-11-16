
Imports System.Data

Partial Class CalendarView
    Inherits AdminBasePage
    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P15_Tarifas_Crear_Editar_Borrar

    Friend mycal As New CalendarUI.calendarjs
    Friend objId As String
    Friend objIdDec As String

    Dim dateSearchStart As Date = Date.Now
    Dim dateSearchEnd As Date = Date.Now.AddMonths(3)

    Public oRsHAB As DataRowView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.tariff)

        Try

            If Not Page.IsPostBack Then
                Dim dayNow As Date = Date.Now
                Me.checkindate.Text = dayNow.ToString("dd/MM/yyyy")
                dayNow = dayNow.AddMonths(3)
                Me.checkoutdate.Text = dayNow.ToString("dd/MM/yyyy")

                Me.configCombos()

                objId = Request.QueryString("objid")
                If objId <> "0" Then
                    AdminTenantSecurity.RequireTariff(Me.codhotel, CInt(objId))
                    'objIdDec = UIHelper.URLDeCryptData(objId)
                    'Me.objId = objIdDec
                    objIdDec = objId
                    Me.getItemObjectByID(objIdDec)

                End If

            Else


            End If

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

#Region "Config cbo and load object"

    Private Sub getItemObjectByID(objId As Integer)




    End Sub

    Private Sub configCombos()
        fillCboOperators(Me.lstclients, Me.dvClients)
        Me.dvRooms = Me.getDvRoomsAll(filterWithoutCombined:=True)
        fillCboUnits(Me.cbounits, Me.dvRooms)
        Dim dv As DataView = Me.getDvTariffByHotel

        Me.fillCboTariff(Me.cbotariff, dv)
    End Sub


#End Region


    Public uical As New CalendarUI

    Friend Function showCalendar() As String

        'Dim numMonths As Integer = ((Me.dateSearchEnd.Year - Me.dateSearchStart.Year) * 12) + Me.dateSearchEnd.Month - Me.dateSearchStart.Month

        'uical.ConfigureFromMonth(Me.dateSearchStart, numMonths, 1)
        'uical.containercsslist.AddRange({"inline"})
        'uical.tablecsslist.AddRange({"borderinternal", "noalign", "caldayround"})

        'For Each dr As calendarDay In uical.calendarDaysObj.dayList
        '    Dim div As String = "<div class='cal-tariff'><div class='uavailability'><div class='ut-lock'><i class='fa fa-unlock'></i></div> <div class='ut-avai'>7</div></div><div class='uprice'><span class='t-price'>150€</span><span class='t-mindays'>4</span><span class='t-release'>3</span></div></div>"
        '    dr.divcontainer = div


        'Next


        Return uical.getCalendarUIHtml
    End Function

    Private Sub lstclients_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstclients.SelectedIndexChanged
        seeSearch()
    End Sub

    Private Sub cbounits_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbounits.SelectedIndexChanged
        seeSearch()
    End Sub


    Private Sub cbotariff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbotariff.SelectedIndexChanged
        seeSearch()
    End Sub

    Private Sub btnseecalendar_Click(sender As Object, e As EventArgs) Handles btnseecalendar.Click
        seeSearch()
    End Sub
    Public Sub seeSearch()
        Try

            Dim checkindateES As String = Me.checkindate.Text.Replace("-", "/")
            Dim checkoutdateES As String = Me.checkoutdate.Text.Replace("-", "/")

            Dim datein As Date = DateTime.ParseExact(checkindateES, "dd/MM/yyyy", New System.Globalization.CultureInfo("es-ES"))
            Dim dateOut As Date = DateTime.ParseExact(checkoutdateES, "dd/MM/yyyy", New System.Globalization.CultureInfo("es-ES"))
            If dateOut < datein Then
                cfr.pages.SiteBasePage.showMessage("La fecha final no puede ser inferior a la inicial", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            ElseIf datein < Date.Now.Date Then
                'cfr.pages.SiteBasePage.showMessage("La fecha de inicio no puede ser inferior a la actual", Me.Page, CFRConst.msgEnum.errorType)
                'Exit Sub
            End If

            Me.dateSearchStart = datein
            Me.dateSearchEnd = dateOut
            Dim DaysInMonth As Integer = Date.DaysInMonth(dateOut.Year, dateOut.Month)
            Dim dateRangeFrom As New Date(datein.Year, datein.Month, 1)
            Dim dateRangeTo As New Date(dateOut.Year, dateOut.Month, DaysInMonth)
            Dim dateRange As New ind.bk.date.DateRangeCls(dateRangeFrom, dateRangeTo)

            Dim numMonths As Integer = ((Me.dateSearchEnd.Year - Me.dateSearchStart.Year) * 12) + Me.dateSearchEnd.Month - Me.dateSearchStart.Month

            uical.ConfigureFromMonth(Me.dateSearchStart, numMonths, 1)
            uical.containercsslist.AddRange({"inline"})
            uical.tablecsslist.AddRange({"borderinternal", "noalign", "caldayround"})

            Dim unitID As Integer = Me.cbounits.SelectedValue
            Dim operatorID As Integer = Me.lstclients.SelectedValue
            Dim tariffID As Integer = UIHelper.decryptData(Me.cbotariff.SelectedValue)
            AdminTenantSecurity.RequireRoom(Me.codhotel, unitID)
            AdminTenantSecurity.RequireOperator(Me.codhotel, operatorID)
            AdminTenantSecurity.RequireTariff(Me.codhotel, tariffID)

            For Each drv As DataRowView In Me.dvRooms
                If drv(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel) = unitID Then
                    Me.oRsHAB = drv
                End If
            Next

            Dim oRCupDV As DataView = ind.bk.web.Availability_mgr.getCupoByOperadorByDates(codhotel, Me.lstclients.SelectedValue, dateRange, True)
            Dim filterString As String = String.Format(" cod_habitacion={0} ", unitID)
            Dim div As String = "<div class='cal-tariff'><div class='uavailability {avaiclosed}'><div class='ut-lock'><i class='fa fa-{avaiclosed}'></i></div> <div class='ut-avai'>{avaivalue}</div></div><div class='uprice'><span class='t-price'>{t-price}</span><div class='t-mindaysrele'><span class='t-mindays'>{t-mindays}</span><span class='t-release'>{t-release}</span></div></div></div>"

            oRCupDV.RowFilter = filterString
            For Each drv As DataRowView In oRCupDV
                Dim dr As calendarDay = Me.uical.calendarDaysObj.getCalDay(drv("dia"))
                If Not dr Is Nothing Then
                    Dim res As String = div
                    Dim avai As Integer = drv("DISPONIBLE")
                    Dim ymCloseVal As Integer = cfr.tool.Tools.cNull(drv("closed"), 0)
                    Dim ymCloseDesc As String = ""
                    If ymCloseVal = 0 Then
                        res = res.Replace("{avaiclosed}", "unlock")

                    ElseIf ymCloseVal = -1 Then
                        res = res.Replace("{avaiclosed}", "lock")

                    End If
                    res = res.Replace("{avaivalue}", avai)

                    dr.divcontainer = res
                End If
            Next

            Dim dvTariffByDay = Me.getDvTariffPriceListByTariff_UnitbyRange(tariffID, unitID, dateRangeFrom, dateRangeTo)
            For Each dayPriceCol As R_PriceDayEF In dvTariffByDay
                Dim dr As calendarDay = Me.uical.calendarDaysObj.getCalDay(dayPriceCol.dayprice)
                If Not dr Is Nothing Then
                    If dr.divcontainer Is Nothing Then
                        dr.divcontainer = div
                    End If
                    dr.divcontainer = dr.divcontainer.Replace("{t-price}", dayPriceCol.price & "€")
                    dr.divcontainer = dr.divcontainer.Replace("{t-mindays}", dayPriceCol.minimumdays)
                    dr.divcontainer = dr.divcontainer.Replace("{t-release}", dayPriceCol.release)
                Else
                    dr.divcontainer = dr.divcontainer.Replace("{t-price}", "0")
                    dr.divcontainer = dr.divcontainer.Replace("{t-mindays}", "0")
                    dr.divcontainer = dr.divcontainer.Replace("{t-release}", "0")
                End If
            Next

            For Each dr As calendarDay In Me.uical.calendarDaysObj.dayList
                If dr.divcontainer Is Nothing Then
                    dr.divcontainer = div
                End If
                dr.divcontainer = dr.divcontainer.Replace("{avaiclosed}", "unlock")
                dr.divcontainer = dr.divcontainer.Replace("{avaivalue}", "0")
                dr.divcontainer = dr.divcontainer.Replace("{t-price}", "0")
                dr.divcontainer = dr.divcontainer.Replace("{t-mindays}", "0")
                dr.divcontainer = dr.divcontainer.Replace("{t-release}", "0")
            Next

            'Me.cod_operadorAvalibilitySelected = Me.lstclients.SelectedValue
            'If Me.cod_operadorAvalibilitySelected = Me.hotel.freesaleOperatorObj.cod_operador Then
            '    'for tariff we need cod operator private client
            '    Me.cod_operadorTarifasSelected = Me.hotel.PrivateoperatorObj.cod_operador
            'Else
            '    Me.cod_operadorTarifasSelected = Me.cod_operadorAvalibilitySelected
            'End If
            'Dim daySelected As Date = Me.cbomonth.SelectedValue
            'Me.monthSel = daySelected.Month
            'Me.yearSel = daySelected.Year
            'If Month(Date.Now) = daySelected.Month And daySelected.Year = Date.Now.Year Then
            '    initialDay = Day(Date.Now)
            'Else
            '    initialDay = 1
            'End If
            'Me.dateStart = New Date(yearSel, monthSel, initialDay)
            'Dim DaysInMonth As Integer = Date.DaysInMonth(dateStart.Year, dateStart.Month)
            'Me.dateEnd = New Date(dateStart.Year, dateStart.Month, DaysInMonth)

            'Me.checkindate.Text = dateStart.ToString("dd/MM/yyyy")
            'Me.checkoutdate.Text = dateEnd.ToString("dd/MM/yyyy")

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub


End Class
