Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics
Imports Microsoft.VisualBasic


''' <summary>
''' draw the map of available rooms
''' </summary>
''' <remarks></remarks>
Public Class AvailabilityMapHelper


    Public bookData As cfr.BookRes
    Public bookmgr As BookManager
    Public bookDataNew As cfr.BookRes

    'Room data
    Dim hotelRoomDataCol As System.Collections.Generic.Dictionary(Of Integer, cfr.obj.HotelRoomData)

    Dim dvcalendarHeader As calendarHeader
    Public strUICalendar As String = ""

    Public Sub New(bookData As cfr.BookRes)
        Me.bookData = bookData
        Me.setParameters()
        Me.configMessageInfo()
        Me.configureHeader()
        Me.configCalendarBody()
        Me.configInfoFooter()
        Me.configContainerOuter()
    End Sub

    Private Sub setParameters()
        Me.bookDataNew = New cfr.BookRes
        Dim fecha1 As Date
        Dim fec1 As Date = bookData.dateIn
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


        Me.bookDataNew.setDates(fecha1, bookData.dateOut.AddDays(+11))
        Me.bookDataNew.hotel = Me.bookData.hotel
        Me.bookDataNew.bookOperator = Me.bookData.bookOperator

    End Sub
    ''' <summary>
    ''' wrap the result with a container with overflow
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configContainerOuter()
        strUICalendar = "<div class='row map-container'>" & strUICalendar & "</div>"
    End Sub

    ''' <summary>
    ''' footer info
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configInfoFooter()
        'strUICalendar += String.Format("<h3 class='book-info'><i class='fa fa-info-circle fa-2'></i> {0}</h3>", cfr.Language.GetCaptionBy(285))
        strUICalendar += "<ul class='list-icons'>"
        strUICalendar += "<li><div class='divavailibilitysamplecolor'></div> " & cfr.Language.GetCaptionBy(276) & "</li>"
        strUICalendar += "<li><i class='fa fa-check'></i> " & cfr.Language.GetCaptionBy(205) & "</li>"
        strUICalendar += "</ul>"
    End Sub

    Public Sub configMessageInfo()
        Dim s As String = ""
        s += "<div id='pnlerror' Class='alert  alert-icon alert-info'>"
        s += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>"
        s += "<p class='info'>"
        s += cfr.Language.GetCaptionBy(Language.msgEnum.NoHayDisponibilidad)
        s += "</p>"
        s += "</div>" 'alert 
        strUICalendar += s
    End Sub
    ''' <summary>
    ''' Draw the header. Then the body
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub configureHeader()

        dvcalendarHeader = Me.GetCalendarCAB(Me.bookDataNew.dateIn, Me.bookDataNew.dateOut)
        hotelRoomDataCol = ind.bk.obj.T_Hoteles_Habitaciones_Manager.getHotelRoomColection(Me.bookData.hotel.id)

        Dim numHab As Integer = hotelRoomDataCol.Count
        Dim rowspan As Integer = (numHab) + 1

        Dim s As String
        s += String.Format("<h3 class='book-info'><i class='glyphicon glyphicon-calendar'></i> {0}</h3>", cfr.Language.GetCaptionBy(69))
        s += "<TABLE class='table table-condensed table-bordered map-table'>"
        'month header
        s += "<TR class='mp-month'>"
        s += "<td style='border: 0px solid #ddd;'></td><td style='border: 0px solid #ddd;'></td>"
        Dim imes As Integer = 0
        Dim colSpanMonthHeader As Integer = 0
        For dc As Integer = 0 To dvcalendarHeader.dayList.Count - 1
            Dim ordt As calendarDay = dvcalendarHeader.dayList(dc)
            If imes <> ordt.getMonth And imes <> 0 Then
                s += String.Format("<TD class='monthheader' colspan={1}> {0}</TD>", cfr.Language.getMonthName(ordt.dia.AddDays(-1)), colSpanMonthHeader)
                colSpanMonthHeader = 1
                imes = ordt.getMonth
            ElseIf dc = dvcalendarHeader.dayList.Count - 1 Then
                colSpanMonthHeader += 1
                s += String.Format("<TD class='monthheader' colspan={1}> {0}</TD>", cfr.Language.getMonthName(ordt.dia.AddDays(-1)), colSpanMonthHeader)
                Exit For
            Else
                imes = ordt.getMonth
                colSpanMonthHeader += 1
            End If
            's += String.Format("<TD class='monthheader' > {0}</TD>", cfr.Language.getMonthName(ordt.dia))
        Next
        'last day

        s += "</TR>"

        'days
        s += "<TR>"
        s += "<td style='border: 0px solid #ddd;'></td><td style='border: 0px solid #ddd;'></td>"
        'Dim imes As Integer = 0
        For Each ordt As calendarDay In dvcalendarHeader.dayList
            'If imes <> ordt.getMonth Then
            '    imes = ordt.getMonth
            '    s += String.Format("<TD class='map-month' rowspan='{0}'><div><span class='text-vertical'>{1}</span></div></td>", rowspan, cfr.Language.getMonthName(ordt.dia))
            'End If

            Dim letraDia As Integer = Weekday(ordt.dia, vbMonday)

            Dim classDia As String = ""
            Select Case letraDia
                Case 1, 2, 3, 4, 5
                    classDia = "map-workday"
                Case 6, 7
                    classDia = "map-wk"
            End Select
            s += String.Format("<TD class='{0}' > {1}</br>{2}</TD>", classDia, ordt.getWeekDayLetter, Day(ordt.dia))
        Next
        s += "</TR>"

        Me.strUICalendar += s

    End Sub

    Public bookedRoomList As Dictionary(Of String, BookedRoom)

    Private Sub configMapResult()


        Dim availableRoomWithTariffList As New System.Collections.Generic.Dictionary(Of String, BookedRoom)

        '****************************************************
        'calc tariff for each room
        '****************************************************
        For Each bo As BookedRoom In Me.bookedRoomList.Values
            'Obtenemos el precio. Vemos la tarifa que debe coger
            Dim tariffMgr As New TariffManager(bo)
            Try
                tariffMgr.GetTarifaForUnitType(Me.bookData, bo.roomSelected)
                If bo.priceFinalForAllDays > 0 Then 'presentar solo las habitaciones con precio
                    availableRoomWithTariffList.Add(bo.id, bo)
                End If
            Catch ex As Exception
                Debug.WriteLine("Loop looking tariff error:  " & ex.Message)
            End Try
        Next

    End Sub

    Private Function gettariffForRoom(roomId As Integer, day2Check As Date) As Boolean
        Dim tarMgr As New cfr.TariffManager(Nothing)

        'Dim dateOutCom As Date
        'dateOutCom = Me.bookData.dateOut.AddDays(-1)
        'Seleccionamos todos los tipos de tarifas del hotel/CRS para esa habitacion
        Dim TariffHotelList As System.Collections.Generic.Dictionary(Of Integer, TariffObj)
        TariffHotelList = tarMgr.getHotelRoomTariffByRange(Me.bookData.hotel.id, roomId, day2Check, day2Check, False)
        If TariffHotelList.Count = 0 Then 'no existen tarifas o no existen tarifas a aplicar en ese periodo
            Return False
        Else
            Return True
        End If


    End Function

    Private Sub configCalendarBody()
        Dim s As String = ""
       
        Dim orsMapa As DataView = AvailabilityManager.AvailabilityMapByHotel(Me.bookDataNew).DefaultView

        'For Each col As DataColumn In orsMapa.Table.Columns
        '    Debug.WriteLine("col.DataType: " & col.DataType.FullName)
        'Next


        Dim cod_hab_combinada As Object
        Dim numHab As Integer
        For Each oRsHAB As cfr.obj.HotelRoomData In hotelRoomDataCol.Values

            s += "<TR>"
            'la habitacion admite
            s += String.Format("<Th class='map-room'>{0}</Th>", oRsHAB.description.getDes)
            s += String.Format("<Td class='map-room-capacity'>{0}<i class='fa fa-male' /></i></Td>", oRsHAB.MaxPersonas)
            'DISPONIBILIDAD

            For Each ordt As calendarDay In dvcalendarHeader.dayList
                If oRsHAB.cod_hab_combinada > 0 Then
                    cod_hab_combinada = oRsHAB.cod_hab_combinada
                Else
                    cod_hab_combinada = oRsHAB.codhabitacionhotel
                End If

                'look if have tariff
                Dim hasTariff As Boolean = Me.gettariffForRoom(oRsHAB.codhabitacionhotel, ordt.dia)
                If hasTariff = True Then

                    Dim rowFilter As String = String.Format("fDIA=#{0}# AND cod_habitacion={1}", cfr.tool.DateTools.getUSmmddyyyyShort(ordt.dia), cod_hab_combinada)


                    orsMapa.RowFilter = rowFilter
                    If orsMapa.Count > 0 Then
                        numHab = orsMapa(0)("disp")
                    Else
                        numHab = 0
                    End If
                    If DateDiff("d", Now, ordt.dia.Date) < 0 Then
                        numHab = 0
                    End If
                Else
                    numHab = 0
                End If



                'control de cierre de ventas
                'PARA TODAS LAS HABITACIONES, Y SI NO HAY PARA LA HABITACION EN CURSO
                'oRcierre.FILTER = "FECHAINICIO<=#" & ordt.dia & "# AND FECHAFIN>=#" & ordt.dia & "# AND cod_habitacion=-1"
                'If oRcierre.recordcount > 0 Then
                '    numhab = 0
                'Else
                '    oRcierre.FILTER = "FECHAINICIO<=#" & ordt.dia & "# AND FECHAFIN>=#" & ordt.dia & "# AND cod_habitacion=" & cod_hab_combinada
                '    If oRcierre.recordcount > 0 Then
                '        numhab = 0
                '    End If
                'End If

                If numHab > 0 Then
                    'control de nº maximo de habitaciones a mostrar
                    'If maxhab2see < CInt(numhab) And maxhab2see <> 0 Then 'mostrar el maximo
                    '    numhab = maxhab2see
                    'End If
                    'If maxhab2see = -1 Then 'mostrar solo en verde, sin nº
                    '    numhab = "&nbsp;"
                    'End If
                    s += "<TD class=cdp></td>"
                Else
                    s += "<TD class=cdn></TD>"
                End If


            Next 'ORDT

            s += "</TR>"
        Next 'oRsHAB


        s += "</table>"
        Me.strUICalendar += s
    End Sub
    
    ''' <summary>
    ''' Devuelve un calendario con los dias del mes.
    ''' </summary>
    ''' <param name="FEC1"></param>
    ''' <param name="FEC2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCalendarCAB(FEC1 As Date, FEC2 As Date) As calendarHeader
        Dim fecha1 As Date, fecha2 As Date
        fecha1 = FEC1.Date
        fecha2 = FEC2.Date

        Dim DATE1 As Date
        DATE1 = fecha1.Date
        Dim calHeader As New calendarHeader

        Do While DATE1 <> fecha2
            calHeader.add(DATE1)
            DATE1 = DATE1.AddDays(1)
        Loop

        Return calHeader

    End Function

#Region "Calendar helper"
    Private Class calendarHeader
        Public dayList As New ArrayList

        Public Sub add(newDay As Date)
            Dim cd As New calendarDay(newDay)
            Me.dayList.Add(cd)
        End Sub

    End Class

    'Private Class calendarDay
    '    Public numDia As Integer
    '    Public dia As Date
    '    Public diaSemana As String

    '    Public Sub New(newDay As Date)
    '        Me.dia = newDay
    '        Me.numDia = Day(newDay)
    '        Dim strDia As String = WeekdayName(DatePart("w", newDay), True, vbSunday)
    '        strDia = Left$(strDia, 2)
    '        strDia = UCase$(Left$(strDia, 1)) & Mid$(strDia, 2)
    '        Me.diaSemana = strDia
    '    End Sub

    '    Public Function getMonth() As Integer
    '        Return Month(Me.dia)
    '    End Function


    'End Class
#End Region


End Class
