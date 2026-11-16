Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics
Imports Microsoft.VisualBasic

Namespace ind.sched



    ''' <summary>
    ''' draw the map of available rooms
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SchedulerManager

        Public id As String = "mysch"

        Public dayStart As Date
        Public dayEnd As Date
        Public resources As ArrayList
        
        Dim dvcalendarHeader As calendarHeader
        Public strUICalendar As String = ""

        Dim stableOpen As String = "<TABLE class='' style='border-collapse: collapse; -webkit-user-select: none; border: 0px none;'>"
        

        Public Sub New(dayStart As Date, dayEnd As Date)
            Me.dayStart = dayStart
            Me.dayEnd = dayEnd
            Me.setParameters()
            dvcalendarHeader = Me.GetCalendarCAB(Me.dayStart, Me.dayEnd)

        End Sub

        Public Sub configSched()

            Dim stitle As String = String.Format("<h3 class='book-info'><i class='glyphicon glyphicon-calendar'></i> {0}</h3>", "Mapa de reservas")

            Dim sbody As String

            Dim sExternal As String = "<div class='map-outer'>" & stitle & "{0}</div>"


            sExternal = String.Format(sExternal, addDivResources() & addDivCal())

            strUICalendar += sExternal

            Me.configContainerOuter()
        End Sub


        Private Function addDivResources() As String
            Dim divRes As String = "<div id='my-map-sch-res' class='map-sch-res'><div class='map-sch-resh' style=''></div>"
            divRes += "<div id='map-sch-res-list' class='map-sch-res-list'>"
            divRes += stableOpen
            divRes += Me.configBodyResources()
            divRes += "</table>"
            divRes += "</div>" 'map-sch-res-list
            divRes += "</div>"
            Return divRes
        End Function

        Private Function addDivCal() As String
            
            Dim divSch As String
            divSch += "<div class='map-sch-cal-cont'>"
            divSch += "<div class='map-sch-cal'>"
            Dim scalBody As String = ""

            scalBody += stableOpen
            scalBody += Me.configureHeaderBody
            scalBody += Me.configCalendarBody()
            scalBody += "</table>"
            divSch += scalBody
            divSch += "</div>"
            divSch += "</div>"

            Return divSch
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
            strUICalendar = "<div class='row map-container'>" & strUICalendar & "</div>"
        End Sub


        ''' <summary>
        ''' Draw the header. Then the body
        ''' </summary>
        ''' <remarks></remarks>
        Public Function configureHeaderBody() As String
            
            Dim imes As Integer = 0
            Dim sheaderMonth As String '= "<td style='border: 0px solid #ddd;background-color:#fff;'></td><Td style='border: 0px solid #ddd;background-color:#fff;'></td>"

            Dim s As String = ""
            s += "<TR>"
            's += "<th style='border: 0px solid #ddd;background-color:#fff;'></th><Th style='border: 0px solid #ddd;background-color:#fff;'></th>"
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
                    sheaderMonth += String.Format("<TD class='map-month-header' colspan='{0}'><span class=''>{1}</span></td>", numofColumns, cfr.Language.getMonthName(ordt.dia))
                End If

                Dim letraDia As Integer = Weekday(ordt.dia, vbMonday)
                Dim letraDia1 As String = cfr.Language.getWeekDayName(ordt.dia)
                Dim classDia As String = ""
                Select Case letraDia
                    Case 1, 2, 3, 4, 5
                        classDia = ""
                    Case 6, 7
                        classDia = "map-wk"
                End Select
                s += String.Format("<TD class='{0}' > {1}</br>{2}</TD>", classDia, ordt.diaSemana, Day(ordt.dia))
            Next
            s += "</TR>"

            Dim sheaderBody As String = ""
            
            sheaderBody += "<thead>"
            sheaderBody += "<tr>" & sheaderMonth & "</tr>"
            sheaderBody += s
            sheaderBody += "</thead>"
            Return sheaderBody

        End Function

        Private Function configCalendarBody() As String
            Dim s As String = ""
            s += "<tbody>"
            For Each ores In Me.resources
                s += "<TR>"
                For Each ordt As calendarDay In dvcalendarHeader.dayList
                    s += "<TD class=''></td>"
                Next 'ORDT
                s += "</TR>"
            Next 'oRsHAB
            s += "</tbody>"

            Return s
        End Function
        
        Public Function configBodyResources() As String
            Dim s As String = ""
            s += "<tbody>"
            For Each oResValue As String In Me.resources
                s += "<TR>"
                s += String.Format("<Th class='map-room'>{0}</Th>", oResValue)
                s += String.Format("<Td class='map-room-capacity'>{0}<i class='fa fa-male' /></i></Td>", 1)
                s += "</TR>"
            Next 'oRsHAB
            s += "</tbody>"
            Return s
        End Function
        
        ''' <summary>
        ''' Devuelve un calendario con los dias del mes.
        ''' </summary>
        ''' <param name="FEC1"></param>
        ''' <param name="FEC2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetCalendarCAB(FEC1 As Date, FEC2 As Date) As calendarHeader
            Dim fecha1 As Date, fecha2 As Date
            fecha1 = FEC1
            fecha2 = FEC2

            Dim DATE1 As Date = fecha1
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

        Private Class calendarDay
            Public numDia As Integer
            Public dia As Date
            Public diaSemana As String

            Public Sub New(newDay As Date)
                Me.dia = newDay
                Me.numDia = Day(newDay)
                Dim strDia As String = WeekdayName(DatePart("w", newDay), True, vbSunday)
                strDia = Left$(strDia, 2)
                strDia = UCase$(Left$(strDia, 1)) & Mid$(strDia, 2)
                Me.diaSemana = strDia
            End Sub

            Public Function getMonth() As Integer
                Return Month(Me.dia)
            End Function


        End Class
#End Region


    End Class

End Namespace