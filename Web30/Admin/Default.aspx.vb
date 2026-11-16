Imports System.Data
Imports System.Diagnostics

Partial Class Admin_Default
    Inherits AdminBasePage


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Me.Title = "CFR-Inicio"
            Me.loadScheduler()

        End If
    End Sub


    Private Sub loadScheduler()
        Dim sch As New SchedulerManager("", Date.Now.AddDays(-5), Date.Now.AddMonths(3))
        sch.TitleForScheduler = String.Format("<h3 class='book-info'><i class='glyphicon glyphicon-calendar'></i> {0}</h3>", "Mapa de reservas")

        Me.configResourcesScheduler(sch)
        Me.configureSchedulerData(sch)

        sch.configSched()
        Dim cal As String = sch.strUICalendar
        Me.litScheduler.Text = cal
    End Sub



    ''' <summary>
    ''' Config resources (room, cars, o whatever) for heading
    ''' </summary>
    ''' <param name="sch"></param>
    ''' <remarks></remarks>
    Private Sub configResourcesScheduler(sch As SchedulerManager)
        'unit data
        Dim hotelRoomDataCol As System.Collections.Generic.Dictionary(Of Integer, cfr.obj.HotelRoomData)
        hotelRoomDataCol = ind.bk.obj.T_Hoteles_Habitaciones_Manager.getHotelRoomColection(Me.codhotel)

        'build your own content, then add to the list of resources. Scheduler will build the list with this
        For Each oRsHAB As cfr.obj.HotelRoomData In hotelRoomDataCol.Values

            Dim s As String = "<div style='position:absolute;'>"
            s += String.Format("<div class='sch-res-col-1'>{0}</div>", oRsHAB.description.getDes)
            s += String.Format("<div class='sch-res-col-2'>{0}<i class='fa fa-male' /></i></div>", oRsHAB.MaxPersonas)
            s += "</div>"
            Dim res As New SchedulerManager.resourceSch(oRsHAB.codhabitacionhotel)
            'res.id = oRsHAB.codhabitacionhotel 'in constructor
            res.innerHtml = s
            sch.addResource(res)
        Next

        For Each ores As SchedulerManager.resourceSch In sch.resourcesList.Values 'for each resource one table
            For Each ordt As calendarDay In sch.dvcalendarHeader.dayList
                Dim resday As New SchedulerManager.resourceDayData
                resday.rDay = ordt
                resday.cssData = ""
                ores.DayDataList.Add(resday)
            Next 'ORDT
        Next
    End Sub

    Private Sub configureSchedulerData(sch As SchedulerManager)
        Try

            'Dim datein As Date = DateTime.ParseExact(Me.checkindate.Text, "dd-MM-yyyy", Nothing)
            'Dim dateOut As Date = DateTime.ParseExact(Me.checkoutdate.Text, "dd-MM-yyyy", Nothing)
            Dim datein As Date = Date.Now
            Dim dateOut As Date = Date.Now.AddMonths(3)

            If dateOut < datein Then
                cfr.pages.SiteBasePage.showMessage("La fecha final no puede ser inferior a la inicial", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If

            Dim oInfRes As New ind.bk.reports.ReportBooks
            Dim oRsDV As DataView

            Dim inf As String = String.Format("{0},{1}", CInt(cfr.CFRConst.bookStateEnum.Active), CInt(cfr.CFRConst.bookStateEnum.confirmedByHotel))
            'If Me.rdbbookstatepending.Checked = True Then
            '    inf = cfr.CFRConst.bookStateEnum.Active
            'ElseIf Me.rdbbookstatecancel.Checked = True Then
            '    inf = 1 'all canceled
            'ElseIf Me.rdbbookstateconfirmed.Checked = True Then
            '    inf = cfr.CFRConst.bookStateEnum.confirmedByHotel
            'End If
            oRsDV = oInfRes.GetReservTOTAL(Me.codhotel, sch.dayStart, sch.dayEnd, Me.hotel.Tipo, inf)


            Dim div As String = "<div class='booktt' id='event_bookdata_id_{0}' data-bookid='{0}' style='width: [div-width]px; height: 25px; white-space: nowrap; overflow: hidden;' onclick='vieweventoptions(this);'>"

            div += "<div class='sch-event-bar' style='background-color: [statecolor]'></div>"
            div += "<div class='sch-event' style='height:[div-sch-event-height]px;'>{1}</div>"

            div += "<div class='hidden' id='client_data_id_{0}'>[client-data]</div>"

            div += "</div>"


            Dim startDayOfYearOfScheduler As Integer = sch.dayStart.DayOfYear

            For Each ors As DataRowView In oRsDV

                Dim reporUI As New ind.bk.reports.bookReportUI(ors, Me.hotel)

                Dim daysCol As Integer = DateDiff(DateInterval.Day, reporUI.bookData.dateIn.Date, reporUI.bookData.dateOut.Date)
                Dim dayDiff As Integer = sch.getDayPosition(reporUI.bookData.dateIn)

                Dim toposition As Integer = 2
                Dim res As SchedulerManager.resourceSch = sch.resourcesList.getRes(ors("tipo_habitacion"))
                If Not res Is Nothing Then
                    toposition = (res.row * sch.map_cell_height) + 2
                End If

                Dim bookid As Integer = ors(ind.bk.obj.R_ReservasO.DBCol.cod_reservaO)

                Dim clientDataTooltip As String = ors(ind.bk.obj.R_ReservasO.DBCol.Apellido1) & "," & ors(ind.bk.obj.R_ReservasO.DBCol.Nombre)
                clientDataTooltip += "<br/>" & daysCol & " dias del " & reporUI.bookData.dateIn.ToLongDateString & " al " & reporUI.bookData.dateOut.ToLongDateString

                Dim lnkBookEvent As String = String.Format("<span class='dark' id='bookdata_id_{0}' {1} data-bookid='{0}' href='#'>{0}</span>", bookid, reporUI.getBookJsonStruct)

                Dim left As Integer = (dayDiff * sch.map_cell_width) + 23
                Dim width As Integer = daysCol * sch.map_cell_width


                Dim divSchEvent As String = String.Format(div, bookid, lnkBookEvent)

                If daysCol > 1 Then 'if higher then we set height minor than normal events to avoid overlapping
                    divSchEvent = divSchEvent.Replace("[div-sch-event-height]", 18)
                    toposition += 2
                Else
                    divSchEvent = divSchEvent.Replace("[div-sch-event-height]", 20)
                End If
                divSchEvent = divSchEvent.Replace("[client-data]", clientDataTooltip)
                divSchEvent = divSchEvent.Replace("[left-position]", left)
                divSchEvent = divSchEvent.Replace("[top-position]", toposition)
                divSchEvent = divSchEvent.Replace("[div-width]", width)
                Select Case CType(ors("estado"), CFRConst.bookStateEnum)
                    Case CFRConst.bookStateEnum.Active
                        divSchEvent = divSchEvent.Replace("[statecolor]", "blue")
                    Case CFRConst.bookStateEnum.confirmedByHotel
                        divSchEvent = divSchEvent.Replace("[statecolor]", "green")
                    Case CFRConst.bookStateEnum.CancelByClient
                        divSchEvent = divSchEvent.Replace("[statecolor]", "red")
                    Case CFRConst.bookStateEnum.CancelByHotelUser
                        divSchEvent = divSchEvent.Replace("[statecolor]", "maroon")
                    Case CFRConst.bookStateEnum.NoShow
                        divSchEvent = divSchEvent.Replace("[statecolor]", "black")
                    Case CFRConst.bookStateEnum.canceledByOperator
                        divSchEvent = divSchEvent.Replace("[statecolor]", "#FF22C5")

                End Select

                Dim evsch As New SchedulerManager.EventSch(reporUI.bookData.dateIn, reporUI.bookData.dateOut, res.id, bookid)
                evsch.position.setPosition(left, toposition, width)
                evsch.innerHtml = divSchEvent

                Dim cellSch As SchedulerManager.cellRangeSch = sch.cellRangeList.addEvent(evsch)
                cellSch.position.setPositionCheckWidth(left, toposition, width) 'take the higher width to show all
            Next

            getDvListPeriodosClosed(sch)

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Function getDvListPeriodosClosed(sch As SchedulerManager) As DataView
        Dim tableRes As New cfr.YieldMngClosedInventory
        Dim dateRange As New ind.bk.date.DateRangeCls(sch.dayStart, sch.dayEnd)
        Dim oRCupDV As DataView = ind.bk.web.Availability_mgr.getCupoByOperadorByDates(Me.codhotel, Me.hotel.getOperatorFreeSale.cod_operador, dateRange, True)



        For Each dtr As DataRowView In oRCupDV
            Dim unitID As String = dtr(ind.bk.obj.R_YieldMng_Cierre.DBCol.Cod_habitacion)
            If cfr.tool.Tools.cNull(dtr("closed"), False) = True Then
                ymCloseResource(sch, unitID, dtr.Row)
            End If

        Next
        Return oRCupDV
    End Function

    Private Sub ymCloseResource(sch As SchedulerManager, unitID As String, dtr As DataRow)
        Dim ores As SchedulerManager.resourceSch
        ores = sch.resourcesList(unitID)
        If Not ores Is Nothing Then
            Dim checkdayStart As Date = dtr("dia")

            For Each resDay As SchedulerManager.resourceDayData In ores.DayDataList
                Dim ordt As calendarDay = resDay.rDay
                If resDay.dayID = checkdayStart.Date Then
                    resDay.cssData += " ymclose "
                    Exit For
                End If

            Next
        End If
    End Sub

    ''' <summary>
    ''' draw the map of available rooms
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SchedulerManager

#Region "Variables"
        ''' <summary>
        ''' Id for the scheduler. Needed when use more than one scheduler in the same page
        ''' </summary>
        ''' <remarks></remarks>
        Public id As String = "mysch"
        ''' <summary>
        ''' Optional title for sched
        ''' </summary>
        ''' <remarks></remarks>
        Public TitleForScheduler As String
        ''' <summary>
        ''' Day start is inclusive
        ''' </summary>
        ''' <remarks></remarks>
        Public dayStart As Date
        ''' <summary>
        ''' Day end is exclusive
        ''' </summary>
        ''' <remarks></remarks>
        Public dayEnd As Date
        ''' <summary>
        ''' list with elements to build resources
        ''' </summary>
        ''' <remarks></remarks>
        Public resourcesList As New resourcesListClass

        Public cellRangeList As New cellRangeListClass

        Public sEvents As New StringBuilder

        Public dvcalendarHeader As calendarHeader
        Public strUICalendar As String = ""

        Dim stableOpen As String = "<TABLE class='' style='border-collapse: collapse; -webkit-user-select: none; border: 0px none;'>"

        Public eventCommonInnerHtml As String = ""
        Public cellCommonInnerHtml As String = ""

#Region "var for presentation, height, widths, etc"
        ''' <summary>
        ''' total height calculated based on resources and events height
        ''' </summary>
        ''' <remarks></remarks>
        Dim map_sch_cal_height As Integer = 0
        ''' <summary>
        ''' max height of calendar
        ''' </summary>
        ''' <remarks></remarks>
        Public map_sch_cal_height_max_height As Integer = 700
        ''' <summary>
        ''' height of each row of events
        ''' </summary>
        ''' <remarks></remarks>
        Public map_cell_height As Integer = 62

        ''' <summary>
        ''' width of each column
        ''' </summary>
        ''' <remarks></remarks>
        Public map_cell_width As Integer = 46
#End Region

#Region "Header var"
        Public headerCol_callback As String = ""
#End Region

        ''' <summary>
        ''' Add resource header to scheduler collection
        ''' </summary>
        ''' <param name="res"></param>
        ''' <remarks></remarks>
        Public Sub addResource(res As resourceSch)
            Me.resourcesList.addRes(res)
        End Sub



#End Region

#Region "Resources"
        Public Class resourcesListClass
            Inherits SortedList

            Public lastRow As Integer = -1

            Public Sub addRes(res As resourceSch)
                lastRow += 1
                res.row = lastRow
                Me.Add(res.id, res)
            End Sub

            Public Function getRes(id As String) As resourceSch
                Dim res As Object = Me(id)
                If Not res Is Nothing Then
                    Return DirectCast(res, resourceSch)
                Else
                    Return Nothing
                End If

            End Function
        End Class


        ''' <summary>
        ''' class container of resources only.
        ''' </summary>
        ''' <remarks></remarks>
        Public Class resourceSch
            Public Sub New(resourceSchid As String)
                Me.id = resourceSchid
            End Sub
            ''' <summary>
            ''' resource id
            ''' </summary>
            ''' <remarks></remarks>
            Public id As String
            ''' <summary>
            ''' row position, used to calculate height
            ''' </summary>
            ''' <remarks></remarks>
            Public row As Integer
            ''' <summary>
            ''' inner html inside for header resource UI
            ''' </summary>
            ''' <remarks></remarks>
            Public innerHtml As String

            Public DayDataList As New ArrayList
        End Class

        Public Class resourceDayData
            Public ReadOnly Property dayID As Date
                Get
                    Return Me.rDay.dia.Date
                End Get
  
            End Property

            Public cssData As String
            Public rDay As calendarDay
        End Class




#End Region

        Public Sub New()
            Me.configBasicData()
        End Sub
        ''' <summary>
        ''' basic data for use
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub configBasicData()

            Dim divBasicEventIndividual As String = "<div class='sch-event-individual' " _
                                                    & " style='position: absolute; left: [left-position]px; top: [top-position]px; height: 25px; white-space: nowrap;'>"

            divBasicEventIndividual += "{0}</div>"
            Me.eventCommonInnerHtml = divBasicEventIndividual

            Dim divStackedEvents As String = "<div onclick='viewcelleventslist(""[cellid]"");' style='position: absolute; left: [left-position]px; top: [top-position]px; width: [div-width]px; height: 25px; white-space: nowrap; overflow: hidden;'>"
            divStackedEvents += "<div class='sch-event-bar' style='background-color: green'></div>"
            divStackedEvents += "<div class='sch-event '>+[numevents] Reservas</div>"

            divStackedEvents += "</div>"


            Me.cellCommonInnerHtml = divStackedEvents
        End Sub
        ''' <summary>
        ''' build events html format. They are build outside the grid
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub configCells()

            'build div events containers
            For Each cellRange As SchedulerManager.cellRangeSch In Me.cellRangeList.Values

                If cellRange.eventschList.Count = 1 Then
                    For Each evensch As SchedulerManager.EventSch In cellRange.eventschList.Values
                        Dim sEventDiv As String = Me.eventCommonInnerHtml
                        sEventDiv = evensch.setDivPosition(sEventDiv)
                        sEvents.AppendFormat(sEventDiv, evensch.innerHtml)
                    Next

                ElseIf cellRange.eventschList.Count <= 3 Then
                    Dim topIncrement As Integer = 0
                    For Each evensch As SchedulerManager.EventSch In cellRange.eventschList.Values
                        Dim sEventDiv As String = Me.eventCommonInnerHtml
                        sEventDiv = evensch.setDivPosition(sEventDiv, topIncrement)
                        topIncrement += 20
                        sEvents.AppendFormat(sEventDiv, evensch.innerHtml)
                    Next

                Else

                    Dim divhidden As String = "<div  class='sch-cell-stacked' id='{1}' {0}>{2}</div>"
                    Dim divPanel As String = "<div class='panel panel-primary'><div class='panel-heading sch-cell-stacked-close' title='Pulse para cerrar' onclick='viewcelleventslist(""[cellid]"");'><button type='button' class='close'><span >&times;</span></button>Reservas para este dia</div> <div class='panel-body'>{0}</div> </div>"
                    Dim cellRangepositiontop As Integer = cellRange.position.top - 45
                    If cellRangepositiontop < 0 Then
                        cellRangepositiontop = 0
                    End If
                    Dim divHiddenStyle As String = String.Format("style='display:none;position:absolute;left:{0}px; top:{1}px;'", cellRange.position.left + cellRange.position.width, cellRangepositiontop)
                    Dim subEvents As String = ""
                    For Each evensch As SchedulerManager.EventSch In cellRange.eventschList.Values
                        subEvents += evensch.innerHtml
                    Next
                    divPanel = divPanel.Replace("[cellid]", cellRange.id)
                    divPanel = String.Format(divPanel, subEvents)
                    divhidden = String.Format(divhidden, divHiddenStyle, cellRange.id, divPanel)
                    cellRange.innerHtml = cellRange.setPositionHTML(Me.cellCommonInnerHtml)
                    sEvents.Append(cellRange.innerHtml)
                    sEvents.Append(divhidden)
                End If

            Next

        End Sub





#Region "Events"

        ''' <summary>
        ''' list of cells with the resource data of scheduler
        ''' </summary>
        ''' <remarks></remarks>
        Public Class cellRangeListClass
            Inherits SortedList

            Public Function getCellKey(dateStart As Date, resourceID As String) As String
                Dim str As String = dateStart.Date.ToString & "-" & resourceID
                str = str.Replace("/", "-")
                str = str.Replace(":", "")
                str = str.Replace(" ", "_")
                Return str
            End Function

            Public Sub addCellRange(cell2Add As cellRangeSch)

                Me.Add(cell2Add.id, cell2Add)

            End Sub

            Public Function getCellRangeByKey(id As String) As cellRangeSch
                Dim cellRangeExists As Object = Me(id)
                If Not cellRangeExists Is Nothing Then
                    Return DirectCast(cellRangeExists, cellRangeSch)
                Else

                    Return Nothing
                End If

            End Function

            Public Function addEvent(eventTo As EventSch) As cellRangeSch
                Dim cellKey As String = Me.getCellKey(eventTo.dateStart, eventTo.resourceID)
                Dim cellExists As cellRangeSch = Me.getCellRangeByKey(cellKey)

                If cellExists Is Nothing Then
                    cellExists = New cellRangeSch(eventTo.dateStart, eventTo.resourceID)
                    Me.addCellRange(cellExists)
                    cellExists.eventschList.addEvent(eventTo)
                Else
                    cellExists.eventschList.addEvent(eventTo)
                End If

                Return cellExists
            End Function


        End Class

        Public Class cellpositionClass
            Public top As Integer
            Public left As Integer
            Public width As Integer

            Public Sub New()

            End Sub

            Public Sub New(left As Integer, top As Integer, width As Integer)
                Me.top = top
                Me.left = left
                Me.width = width
            End Sub

            Public Sub setPosition(left As Integer, top As Integer, width As Integer)
                Me.top = top
                Me.left = left
                Me.width = width
            End Sub

            Public Sub setPositionCheckWidth(left As Integer, top As Integer, width As Integer)
                Me.top = top
                Me.left = left
                If Me.width > width Then

                Else
                    Me.width = width
                End If

            End Sub

        End Class

        ''' <summary>
        ''' Cell data
        ''' </summary>
        ''' <remarks></remarks>
        Public Class cellRangeSch
            ''' <summary>
            ''' Event id is: dateStart-dateEnd-resourceID
            ''' </summary>
            ''' <remarks></remarks>
            Public id As String
            Public resourceID As String
            Public dateStart As Date
            Public innerHtml As String = ""
            Public eventschList As New eventListClass
            Public position As New cellpositionClass

            Public Sub New()

            End Sub

            Public Sub New(dateStart As Date, resourceID As String)
                Me.id = configKey(dateStart, resourceID)
            End Sub
            Public Function configKey(dateStart As Date, resourceID As String) As String

                Dim str As String = dateStart.Date.ToString & "-" & resourceID
                str = str.Replace("/", "-")
                str = str.Replace(":", "")
                str = str.Replace(" ", "_")
                Return str
            End Function

            Friend Function setPositionHTML(innerHTML As String) As String

                Dim divFor As String = innerHTML
                divFor = divFor.Replace("[cellid]", Me.id)
                divFor = divFor.Replace("[numevents]", Me.eventschList.Count)
                divFor = divFor.Replace("[left-position]", Me.position.left)
                divFor = divFor.Replace("[top-position]", Me.position.top)
                divFor = divFor.Replace("[div-width]", Me.position.width)
                Return divFor
            End Function


        End Class


        Public Class EventSch
            ''' <summary>
            ''' Event id is: dateStart-dateEnd-resourceID
            ''' </summary>
            ''' <remarks></remarks>
            Public id As String
            Public eventid As String
            Public resourceID As String
            Public dateStart As Date
            Public dateEnd As Date
            Public innerHtml As String
            Public position As New cellpositionClass

            Public Sub New()

            End Sub

            Public Sub New(dateStart As Date, dateEnd As Date, resourceID As String, eventID As String)
                Me.dateStart = dateStart
                Me.dateEnd = dateEnd
                Me.resourceID = resourceID
                Me.eventid = eventID
                Me.id = configKey(dateStart, dateEnd, resourceID, eventID)
            End Sub
            Public Function configKey(dateStart As Date, dateEnd As Date, resourceID As String, eventID As String) As String
                Dim str As String = dateStart.Date.ToString & "_" & dateStart.Date.ToString & "_" & resourceID & "_" & eventID
                str = str.Replace("/", "-")
                str = str.Replace(":", "")
                str = str.Replace(" ", "_")
                Return str
            End Function

            Public Sub setInnerHtml(div As String)

            End Sub

            Public Function setDivPosition(mainContainerDiv As String, Optional topIncrement As Integer = 0) As String

                Dim divFor As String = mainContainerDiv

                divFor = divFor.Replace("[left-position]", Me.position.left)
                divFor = divFor.Replace("[top-position]", Me.position.top + topIncrement)
                divFor = divFor.Replace("[div-width]", Me.position.width)
                Return divFor
            End Function


        End Class


        Public Class eventListClass
            Inherits Hashtable

            Public Sub addEvent(eventTo As EventSch)

                Dim eventToExists As Object = Me(eventTo.id)
                If eventToExists Is Nothing Then
                    Me.Add(eventTo.id, eventTo)
                Else
                    Dim eventToLoaded As EventSch = DirectCast(eventToExists, EventSch)
                    If eventToLoaded Is Nothing Then

                    End If
                End If

            End Sub

            Public Function getEvent(id As String) As EventSch
                Dim eventTo As Object = Me(id)
                If Not eventTo Is Nothing Then
                    Return DirectCast(eventTo, EventSch)
                Else
                    Return Nothing
                End If

            End Function
        End Class

#End Region


#Region "Options"

#End Region


        Public Sub New(staticControlID As String, dayStart As Date, dayEnd As Date)
            Me.configBasicData()
            Me.id = staticControlID
            If Me.id = "" Then
                Me.id = "mysch"
            End If

            Me.dayStart = dayStart
            Me.dayEnd = dayEnd
            Me.setParameters()
            dvcalendarHeader = Me.GetCalendarCAB(Me.dayStart, Me.dayEnd)
        End Sub

        ''' <summary>
        ''' Config scheduler drawing each section
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub configSched()

            Me.configCells()

            Dim sExternal As New StringBuilder
            sExternal.Append("<div id='sch-map-outer' class='map-outer'>")
            Dim sDivCal As String = addDivCalendarBodyResources()
            Dim sDivResources As String = addDivColumnResourcesHeaders()
            sExternal.Append(sDivResources)
            sExternal.Append(sDivCal)
            sExternal.Append("</div>")

            strUICalendar += sExternal.ToString

            Me.configContainerOuter()
        End Sub


        Private Function addDivColumnResourcesHeaders() As String
            'Dim divHeight As Integer = ((Me.resourcesList.Count) * 50)
            'If divHeight > map_sch_cal_height Then
            '    divHeight = map_sch_cal_height
            'End If

            Dim divRes As String = String.Format("<div id='my-map-sch-res' class='map-sch-res' style='height:{1}px;'><div class='map-sch-res-corner'>{0}</div>", Me.TitleForScheduler, map_sch_cal_height + 77)
            divRes += String.Format("<div id='map-sch-res-list' class='map-sch-res-list' style='height:{0}px; position:absolute;'>", getFinal_map_sch_cal_height() - 17)
            divRes += Me.configResourcesColumnBody()
            divRes += "</div>" 'map-sch-res-list
            divRes += "</div>"
            Return divRes
        End Function

        Private Function getFinal_map_sch_cal_height() As Integer
            Dim final_map_sch_cal_height As Integer = map_sch_cal_height
            If map_sch_cal_height < map_sch_cal_height_max_height Then
                final_map_sch_cal_height = map_sch_cal_height + 77
            End If
            Return final_map_sch_cal_height
        End Function

        ''' <summary>
        ''' Create body resources based on arraylist with the data. 
        ''' The data must be a <td>data</td> column that is inserted in tr row
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function configResourcesColumnBody() As String

            Dim sortList As New SortedList
            For Each oResValue As resourceSch In Me.resourcesList.Values
                sortList.Add(oResValue.row, oResValue)
            Next
            'header container of resource column
            Dim divContainer As String = "<div id='{0}' class='div-res-container ' style='height:" & Me.map_cell_height & "px;'>{1}</div>"

            Dim s As New StringBuilder
            For Each oResValue As resourceSch In sortList.Values
                s.AppendFormat(divContainer, oResValue.id, oResValue.innerHtml)
            Next 'oRsHAB

            Return s.ToString
        End Function


        Private Sub setParameters()
            Dim fecha1 As Date
            Dim fec1 As Date = dayStart
            If fec1 >= Date.Now.Date Then
                Dim days As Integer = fec1.Date.Subtract(Date.Now.Date).Days
                If days > 10 Then
                    fecha1 = fec1.AddDays(-10)
                Else
                    fecha1 = fec1.AddDays(-days)
                End If
            Else
                fecha1 = Date.Now.Date
            End If


        End Sub
        ''' <summary>
        ''' wrap the result with a container with overflow
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub configContainerOuter()
            strUICalendar = "<div class='map-container'>" & strUICalendar & "</div>"
        End Sub


        ''' <summary>
        ''' Configure header table for calendar.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function configureHeaderBody() As String

            Dim imes As Integer = 0
            Dim sheaderMonth As New StringBuilder '= "<td style='border: 0px solid #ddd;background-color:#fff;'></td><Td style='border: 0px solid #ddd;background-color:#fff;'></td>"

            Dim s As New StringBuilder
            s.Append("<TR>")

            For Each ordt As calendarDay In dvcalendarHeader.dayList
                If imes <> ordt.getMonth Then
                    imes = ordt.getMonth
                    Dim numofColumns As Integer = Date.DaysInMonth(ordt.dia.Year, imes)

                    If ordt.dia.Day <> 1 Then
                        numofColumns = numofColumns - ordt.dia.Day + 1
                    End If

                    Dim dayColLast As Date = New Date(ordt.dia.Year, imes, numofColumns)
                    'check last day in range
                    If Me.dayEnd < dayColLast Then
                        numofColumns = Me.dayEnd.Day - 1
                    End If

                    sheaderMonth.AppendFormat("<TD class='map-month-header' colspan='{0}'><span class=''>{1}</span></td>", numofColumns, cfr.Language.getMonthName(ordt.dia))
                End If

                Dim weekDayNumber As Integer = Weekday(ordt.dia, vbMonday)
                Dim dayCss2Apply As String = ""
                Select Case weekDayNumber
                    Case 1, 2, 3, 4, 5
                        dayCss2Apply = ""
                    Case 6, 7
                        dayCss2Apply = "sch-wkend"
                End Select
                Dim id As String = String.Format("day_{0}_{1}_{2}", ordt.dia.Year, ordt.dia.Month, ordt.dia.Day)
                'this is the column with day number and month
                s.AppendFormat("<TD id='{3}' class='click headerday_click {0}' > {1}</br>{2}</TD>", dayCss2Apply, ordt.getWeekDayLetter(3), Day(ordt.dia), id)
            Next
            s.Append("</TR>")

            Dim sheaderBody As New StringBuilder


            sheaderBody.Append("<thead>")
            sheaderBody.Append("<tr>" & sheaderMonth.ToString & "</tr>")
            sheaderBody.Append(s.ToString)
            sheaderBody.Append("</thead>")
            Return sheaderBody.ToString

        End Function

        Private Function addDivCalendarBodyResources() As String

            Dim divSch As String = ""
            divSch += "<div class='map-sch-cal-cont'>{0}</div>"

            Dim scalBody As String = ""
            'days and month header 
            scalBody += "<div class='map-sch-cal-header '>"
            scalBody += stableOpen
            scalBody += Me.configureHeaderBody
            scalBody += "</table>"
            scalBody += "</div>"
            'body of calendar
            scalBody += Me.configCalendarBody()

            divSch = String.Format(divSch, scalBody)

            Return divSch
        End Function
        ''' <summary>
        ''' for each resource build the table with each column day
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function configCalendarBody() As String
            Dim s As New StringBuilder

            Dim divMainContainer As String = "<div class='map-sch-cal' style='height:{0}px'>" 'container of calendar-scheduler with all resources

            s.Append(Me.sEvents)
            For Each ores As resourceSch In Me.resourcesList.Values 'for each resource one table
                s.AppendFormat("<div id='sch_{0}' class='div-res-sch-container' style='height:{1}px;'>", ores.id, Me.map_cell_height) 'container of each table row
                s.Append(stableOpen)
                s.Append("<tbody>")
                s.AppendFormat("<TR style='height:{0}px;'>", Me.map_cell_height)
                For Each resDay As resourceDayData In ores.DayDataList
                    Dim ordt As calendarDay = resDay.rDay
                    Dim weekDayNumber As Integer = Weekday(ordt.dia, vbMonday)

                    Dim unitDayCol As String = "<TD id='unitday_{0}' class='{1}'></td>"
                    Select Case weekDayNumber
                        Case 1, 2, 3, 4, 5
                            'Nothing
                        Case 6, 7
                            unitDayCol = "<TD id='unitday_{0}' class='sch-wkend {1}'></td>"

                    End Select
                    s.AppendFormat(unitDayCol, ores.id & "_" & ordt.dia.Year & "_" & ordt.dia.Month & "_" & ordt.dia.Day, resDay.cssData)

                Next 'ORDT
                s.Append("</TR>")
                s.Append("</tbody>")
                s.Append("</table>")
                s.Append("</div>") 'container of each table row
                map_sch_cal_height += 50
            Next 'oRsHAB

            If map_sch_cal_height + 77 > map_sch_cal_height_max_height Then
                map_sch_cal_height = map_sch_cal_height_max_height
            Else
                map_sch_cal_height += 20 + 77 'for scroollbar
            End If

            s.Insert(0, addTodayLine)

            divMainContainer = String.Format(divMainContainer, map_sch_cal_height)
            s.Insert(0, divMainContainer)
            s.Append("<div>")



            Return s.ToString
        End Function

        Private Function addTodayLine() As String
            Dim dayDiff As Integer = getDayPosition(Date.Now)
            Dim left As Integer = (dayDiff * 46) + 23

            Dim divToday As String = String.Format("<div id='sch-today' class='sch-today-line' style='left:{0}px;height:{1}px'></div>", left, Me.map_cell_height * Me.resourcesList.Count)
            Return divToday
        End Function


        Public Function getDayPosition(dayStart As Date) As Integer

            Dim startDayOfYearOfScheduler As Integer = Me.dayStart.DayOfYear

            Dim dayDiff As Integer = dayStart.DayOfYear - startDayOfYearOfScheduler

            Return dayDiff


        End Function



#Region "Calendar helper"
        ''' <summary>
        ''' Config calendar for selected days 
        ''' </summary>
        ''' <param name="dayRangeStart">Inclusive</param>
        ''' <param name="dayRangeEndExclusive">Exclusive</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetCalendarCAB(dayRangeStart As Date, dayRangeEndExclusive As Date) As calendarHeader

            Dim dayToCreate As Date
            dayToCreate = dayRangeStart
            Dim calHeader As New calendarHeader

            Do While dayToCreate <> dayRangeEndExclusive
                calHeader.add(dayToCreate)
                dayToCreate = dayToCreate.AddDays(1)
            Loop

            Return calHeader

        End Function

        ''' <summary>
        ''' Class helper for calendar collection of days
        ''' </summary>
        ''' <remarks></remarks>
        Public Class calendarHeader
            Public dayList As New ArrayList

            Public Sub add(newDay As Date)
                Dim cd As New calendarDay(newDay)
                Me.dayList.Add(cd)
            End Sub

        End Class


#End Region


    End Class




    'Protected Sub btnshowbook_Click(sender As Object, e As EventArgs) Handles btnshowbook.Click
    '    Try
    '        Dim bookid As Integer = Me.hddbookidbookview.Value

    '        Dim bookMgr As New ind.bk.obj.R_ReservasO_Manager
    '        'bookData
    '        Dim bookdata As BookRes = Nothing

    '        bookdata = cfr.BookManager.getBookResById(bookid)
    '        Dim iuf As New cfr.IUFunctions(bookdata.site.theme, bookdata)
    '        iuf.bookForStyle = IUFunctions.MailBookStyle.hotel

    '        Dim bookhtml As String = iuf.getHtmlBook()
    '        Me.litbookview.Text = bookhtml

    '    Catch ex As Exception
    '        Me.litbookview.Text = UIMsg.UIMessageDismiss(ex.Message, CFRConst.msgEnum.errorType)
    '    End Try
    'End Sub
End Class
