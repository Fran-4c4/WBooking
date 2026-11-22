Imports System.Data
Imports System.Diagnostics
Imports Microsoft.VisualBasic



Public Class CalendarBase
    ''' <summary>
    ''' Result of the configuration
    ''' </summary>
    Public calendarDaysObj As CalendarDays

    Public colorList As New ArrayList()

    Public groupColor As Integer = 0
    ''' <summary>
    ''' classes for container
    ''' </summary>
    Public containercsslist As New ArrayList
    ''' <summary>
    ''' list of classes to add to table presentation
    ''' </summary>
    Public tablecsslist As New ArrayList

#Region "Basic methods"

    Public Function getCalendarUIHtml() As String
        Return Me.getCalendarUI(Me.calendarDaysObj)
    End Function

    Public Function getCalendarUIHtmlList() As String
        Return Me.getCalendarUIList(Me.calendarDaysObj)
    End Function

#End Region

#Region " Shared methods"
    ''' <summary>
    ''' Get hecader calendar
    ''' </summary>
    ''' <param name="startDate"></param>
    ''' <param name="nMonth"></param>
    ''' <param name="FromDay"></param>
    ''' <returns></returns>
    Public Shared Function getCalendarHeader(startDate As Date, nMonth As Integer, Optional FromDay As Integer = -1) As CalendarDays
        Dim cui As New CalendarUI
        'Dim nMonth As Integer = endDate.Subtract(startDate)
        Dim firstDay As Integer = 1
        If FromDay = -1 Then
            firstDay = startDate.Day
        End If
        cui.ConfigureFromMonth(startDate, nMonth, startDate.Day)
        Return cui.calendarDaysObj
    End Function

    ''' <summary>
    ''' return the last day in a month for selected date
    ''' </summary>
    ''' <param name="day2Check"></param>
    ''' <returns>Last day of the month</returns>
    Public Shared Function getLastDayOfMonth(day2Check As Date) As Date
        Dim DaysInMonth As Integer = Date.DaysInMonth(day2Check.Year, day2Check.Month) 'to know last day in month
        Dim dayLast As New Date(day2Check.Year, day2Check.Month, DaysInMonth) 'set new last day
        Return dayLast
    End Function

    ''' <summary>
    ''' Get month name in spanish
    ''' </summary>
    ''' <param name="day2"></param>
    ''' <returns></returns>
    Public Shared Function getMonthName(day2 As Date) As String
        Return getMonthName(Month(day2))

    End Function
    ''' <summary>
    ''' Get month name in spanish
    ''' </summary>
    ''' <param name="monthNumber"></param>
    ''' <returns></returns>
    Public Shared Function getMonthName(monthNumber As Integer) As String
        Select Case monthNumber
            Case 1
                Return "Enero"
            Case 2
                Return "Febrero"
            Case 3
                Return "Marzo"
            Case 4
                Return "Abril"
            Case 5
                Return "Mayo"
            Case 6
                Return "Junio"
            Case 7
                Return "Julio"
            Case 8
                Return "Agosto"
            Case 9
                Return "Septiembre"
            Case 10
                Return "Octubre"
            Case 11
                Return "Noviembre"
            Case 12
                Return "Diciembre"
        End Select
        Return Nothing
    End Function


#End Region


#Region "Calendar for months"
    ''' <summary>
    ''' Configure N month calendar. By default is 12 months
    ''' </summary>
    ''' <param name="iMeses">Use 0 to show only actual month  until last day</param>
    ''' <param name="iDesdeMes"></param>
    Public Sub ConfigureN_Month(Optional iMeses As Integer = 12, Optional iDesdeMes As Integer = 0, Optional firstDay As Integer = 1)

        Dim dayInitial As Date
        Dim orDT As New CalendarDays

        If iDesdeMes = 0 Then 'DESDE EL MES ACTUAL
            iDesdeMes = Month(Now)
        End If

        dayInitial = DateSerial(Year(Now), Month(Now), firstDay)

        Dim dayLast As New Date(Year(Now), Month(Now), 1)
        dayLast = dayLast.AddMonths(iMeses)

        dayLast = getLastDayOfMonth(dayLast) 'set new last day

        Do While dayInitial <= dayLast
            orDT.add(dayInitial)
            dayInitial = dayInitial.AddDays(1)
        Loop

        Me.calendarDaysObj = orDT

    End Sub

    Public Sub ConfigureFromMonth(startDate As Date, Optional iMeses As Integer = 12, Optional firstDay As Integer = 1)

        Dim dayInitial As Date
        Dim orDT As New CalendarDays

        Dim startMonth As Integer
        Dim startYear As Integer

        If IsNothing(startDate) Then 'DESDE EL MES ACTUAL
            startMonth = Month(Now)
            startYear = Year(Now)
        Else
            startMonth = Month(startDate)
            startYear = Year(startDate)
        End If

        dayInitial = DateSerial(startYear, startMonth, firstDay)



        Dim dayLast As Date = dayInitial.AddMonths(iMeses)
        dayLast = getLastDayOfMonth(dayLast) 'set new last day

        Do While dayInitial <= dayLast
            orDT.add(dayInitial)
            dayInitial = dayInitial.AddDays(1)
        Loop

        Me.calendarDaysObj = orDT

    End Sub

    Public Function getCalendarUI(ordt As CalendarDays) As String
        If ordt Is Nothing Then
            Return "Calendar is not configurated. Please configure before use."
        End If

        Dim resCab As String = String.Format("<div class=' cal-container {0}'><TABLE class='cal-month {1}'>", getCssListForHtml(Me.containercsslist), getCssListForHtml(Me.tablecsslist))
        resCab += "<TR>"
        resCab += "<TD colspan=7 class='cal-monthname' align=center>{Monthname}&nbsp;{yeartoset}</TD>"
        resCab += "</TR>"
        resCab += "<TR>"
        resCab += "<TH >Lu</TH>"
        resCab += "<TH >Ma</TH>"
        resCab += "<TH >Mi</TH>"
        resCab += "<TH >Ju</TH>"
        resCab += "<TH >Vi</TH>"
        resCab += "<Th class='cal-weekend'>Sa</Th>"
        resCab += "<Th class='cal-weekend'>Do</Th>"
        resCab += "</TR>"
        'resCab += "<TR>"

        Dim res As String = ""
        Dim rowIsclosed As Boolean = False
        Dim monthActualName As String = ""
        Dim firstMonth As Boolean = True
        Dim weekDayNumber As Integer
        Dim rownum As Integer = 0

        For Each dr As calendarDay In ordt.dayList
            monthActualName = getMonthName(dr.dia)
            weekDayNumber = dr.weekDay
            Dim numDia As Integer = Day(dr.dia)
            Dim selDate As Date = dr.dia

            If numDia = 1 Then 'new month
                'close previous month
                'check if closed month
                If firstMonth = False Then
                    If weekDayNumber > 1 Then
                        For x As Integer = weekDayNumber To 7 'añadimos los dias extras hasta comenzar el mes
                            res += "<TD class='empty'>&nbsp;</TD>"
                        Next
                    End If
                    res += "</TR>"
                    If rownum < 5 Then
                        res += "<tr><td colspan=7>&nbsp;</td></tr>"
                        rownum = 0
                    End If
                    'close month
                    res += "</table>"
                    res += "</div>"
                Else
                    firstMonth = False
                End If

                'open new month
                Dim newHead As String = resCab.Replace("{Monthname}", getMonthName(selDate))
                newHead = newHead.Replace("{yeartoset}", Year(selDate))
                res += newHead
                res += "<TR>"
                rownum = 1
                rowIsclosed = False
                For x As Integer = 1 To weekDayNumber - 1 'añadimos los dias extras hasta comenzar el mes
                    res += "<TD class='empty'>&nbsp;</TD>"
                Next
            ElseIf weekDayNumber = 1 And numDia <> 1 Then 'nueva fila

                res += "</TR>"
                res += "<TR>"
                rownum += 1
            End If


            Dim vclass As String = " class='tdtt {0}' "
            Dim nvclass As String = markPeriod(dr) 'obsolete? This is for mark periods in calendar

            vclass = String.Format(vclass, nvclass)


            'tootip
            'res += "<TD " & vclass & " data-original-title='" & selDate & "' data-container='body' data-toggle='tooltip' data-placement='top' title='Temporada'>" & Day(selDate) & "</TD>"
            Dim div As String = "" '"<div class='uavailability'>7</div><div class='uprice'>150€<div>4/3</div></div>"

            res += String.Format("<TD {0} id='cal_td_{2}'><div class='cal-day' onclick=""caldaysel('{2}');"" data-calday=""{2}"">{1}</div><div id=""cal_{2}"">{3}</div></td>", vclass, Day(selDate), dr.getSqlDateyyyyMMdd, dr.divcontainer)

            If weekDayNumber = 7 Then 'last day in weekend? Then new line
                rowIsclosed = True
            End If


        Next
        'for the las month it´s not closed
        If weekDayNumber > 1 And weekDayNumber <= 7 Then
            For x As Integer = weekDayNumber + 1 To 7 'añadimos los dias extras hasta comenzar el mes
                res += "<TD class='empty'>&nbsp;</TD>"
            Next
        End If
        res += "</TR>"
        If rownum < 5 Then
            res += "<tr><td colspan=7 class='empty'>&nbsp;</td></tr>"
            rownum = 0
        End If
        res += "</TABLE>"
        res += "</div>"

        Return res
    End Function

    Private Function markPeriod(dr As calendarDay) As String
        Dim weekDayNumber As Integer = dr.weekDay
        Dim vclass As String = ""
        Dim periodIs As String = cfr.tool.Tools.cNullStr(dr.externalValue, "0")

        If periodIs = "1" Then
            If weekDayNumber = 6 Or weekDayNumber = 7 Then 'SI ES fin de semana

                If dr.cssClass = String.Empty Then
                    vclass = " cal-sel-weekend  "
                Else
                    vclass = dr.cssClass
                End If
            Else
                If dr.cssClass = String.Empty Then
                    vclass = " cal-sel "
                Else
                    vclass = dr.cssClass
                End If

            End If
        Else
            If weekDayNumber = 6 Or weekDayNumber = 7 Then 'It´s weekend
                If dr.cssClass = String.Empty Then
                    vclass = " cal-weekend "
                Else
                    vclass = dr.cssClass
                End If
            Else
                If dr.cssClass = String.Empty Then
                    vclass = ""
                Else
                    vclass = dr.cssClass
                End If
            End If
        End If



        Return vclass
    End Function

    Public Function getCalendarUIList(ordt As CalendarDays) As String

        Dim resCab As String = "<div class=' cal-container cal-container-list'>"
        resCab += "<div class='cal-monthname' align=center>{Monthname}&nbsp;{yeartoset}</div>"

        resCab += "<ul class='cal-month cal-weekdays'>"
        resCab += "<li >Lu</li>"
        resCab += "<li >Ma</li>"
        resCab += "<li >Mi</li>"
        resCab += "<li >Ju</li>"
        resCab += "<li >Vi</li>"
        resCab += "<li class='cal-weekend'>Sa</li>"
        resCab += "<li class='cal-weekend'>Do</li>"
        resCab += "</ul >"


        Dim res As String = ""
        Dim rowIsclosed As Boolean = False
        Dim monthActualName As String = ""
        Dim firstMonth As Boolean = True
        Dim weekDayNumber As Integer
        For Each dr As calendarDay In ordt.dayList
            monthActualName = getMonthName(dr.dia)
            weekDayNumber = dr.weekDay
            Dim numDia As Integer = Day(dr.dia)
            Dim selDate As Date = dr.dia


            If numDia = 1 Then 'new month
                'close last month
                If firstMonth = True Then
                    firstMonth = False
                Else
                    res += "</ul></div>"
                End If
                'open new month
                Dim newHead As String = resCab.Replace("{Monthname}", getMonthName(selDate))
                newHead = newHead.Replace("{yeartoset}", Year(selDate))
                res += newHead
                res += "<ul class='days'>"
                rowIsclosed = False
                For x As Integer = 1 To weekDayNumber - 1 'añadimos los dias extras hasta comenzar el mes
                    res += "<li></li>"
                Next
            End If


            Dim vclass As String = ""
            Dim periodIs As String = cfr.tool.Tools.cNullStr(dr.externalValue, "0")

            If periodIs = "1" Then
                If weekDayNumber = 6 Or weekDayNumber = 7 Then 'SI ES fin de semana

                    If dr.cssClass = String.Empty Then
                        vclass = " cal-sel-weekend  "
                    Else
                        vclass = dr.cssClass
                    End If
                Else
                    If dr.cssClass = String.Empty Then
                        vclass = " cal-sel "
                    Else
                        vclass = dr.cssClass
                    End If

                End If
            Else
                If weekDayNumber = 6 Or weekDayNumber = 7 Then 'It´s weekend
                    If dr.cssClass = String.Empty Then
                        vclass = " cal-weekend "
                    Else
                        vclass = dr.cssClass
                    End If

                Else
                    If dr.cssClass = String.Empty Then
                        vclass = ""
                    Else
                        vclass = dr.cssClass
                    End If

                End If
            End If

            If vclass <> "" Then
                vclass = String.Format(" class='{0}'", vclass)
            End If

            'tootip
            'res += "<TD " & vclass & " data-original-title='" & selDate & "' data-container='body' data-toggle='tooltip' data-placement='top' title='Temporada'>" & Day(selDate) & "</TD>"
            Dim div As String = "" '"<div class='uavailability'>7</div><div class='uprice'>150€<div>4/3</div></div>"

            res += String.Format("<li {0} id='cal_td_{2}'><div class='cal-day' onclick=""caldaysel('{2}');"" data-calday=""{2}"">{1}</div><div id=""cal_{2}"">{3}</div></li>", vclass, Day(selDate), dr.getSqlDateyyyyMMdd, div)



            If weekDayNumber = 7 Then 'last day in weekend? Then new line
                rowIsclosed = True
            End If


        Next
        'for the las month it´s not closed
        If weekDayNumber > 1 And weekDayNumber <= 7 Then
            For x As Integer = weekDayNumber + 1 To 7 'añadimos los dias extras hasta cerrar el mes
                res += "<li></li>"
            Next
        End If
        res += "</ul>"
        res += "</div>"

        Return res
    End Function

    Private Function ConfigureCalendar(calDayList As CalendarDays) As String
        Dim res As String = ""
        res += "<TABLE class=b1 BORDER=0 cellspacing=2 cellpadding=3>"
        For xFilaMes As Integer = 1 To 1
            res += "<TR>"
            For yColMes As Integer = 1 To 12

                res += "<TD valign=>"
                Dim resMonth As String = getCalendarUI(calDayList)
                If resMonth <> "" Then
                    res += resMonth
                Else
                    res += "&nbsp;"
                End If
                res += "</TD>"

            Next
            res += "</TR>"
        Next
        res += "</TABLE>"
        Return res
    End Function

#End Region

#Region "Calendar javascript"
    Public calendarConfig As calendarjs
    Public Class calendarjs
        Public langSelected As String = "es"
        Public dateFormat As String = "dd/mm/yyyy"
        Public dateIn As String
        Public dateOut As String
        ''' <summary>
        ''' max date for calendar, not for booking
        ''' </summary>
        Public dateMax As String
        Public calendarjsplace As Literal = Nothing
        Public israngeplusone As Boolean = True


        Public Sub configure(indate As TextBox, outdate As TextBox, Optional calendarjsLit As Literal = Nothing, Optional ndayInprecedence As Integer = 0)
            Dim dayInprecedence As Date = Date.Now.AddDays(ndayInprecedence)
            Dim dayNext As Date = dayInprecedence.AddDays(1)

            Dim daymaxForCalendar As Date = dayInprecedence.AddYears(1)
            If cfr.Language.getLanguage = CFRConst.LanguageEnum.es Then
                Me.langSelected = "es"
                Me.dateFormat = "dd/mm/yyyy"
            ElseIf cfr.Language.getLanguage = CFRConst.LanguageEnum.en Then
                Me.langSelected = "en-GB"
                Me.dateFormat = "mm/dd/yyyy"
            ElseIf cfr.Language.getLanguage = CFRConst.LanguageEnum.de Then
                Me.langSelected = "de"
                Me.dateFormat = "dd/mm/yyyy"
            ElseIf cfr.Language.getLanguage = CFRConst.LanguageEnum.fr Then
                Me.langSelected = "fr"
                Me.dateFormat = "dd/mm/yyyy"
            ElseIf cfr.Language.getLanguage = CFRConst.LanguageEnum.ca Then
                Me.langSelected = "es"
                Me.dateFormat = "dd/mm/yyyy"
            ElseIf cfr.Language.getLanguage = CFRConst.LanguageEnum.it Then
                Me.langSelected = "it"
                Me.dateFormat = "dd/mm/yyyy"
            ElseIf cfr.Language.getLanguage = CFRConst.LanguageEnum.jp Then
                Me.langSelected = "jp"
                Me.dateFormat = "yyyy/mm/dd"
            End If

            Me.dateIn = dayInprecedence.ToShortDateString
            Me.dateOut = dayNext.ToShortDateString
            Me.dateMax = daymaxForCalendar.ToShortDateString

            indate.Text = dayInprecedence.ToShortDateString
            outdate.Text = dayNext.ToShortDateString
            Me.calendarjsplace = calendarjsLit
            If Not calendarjsplace Is Nothing Then
                calendarjsplace.Text = Me.getJavascriptCalendar
            End If
        End Sub

        Public Function getJavascriptCalendar() As String

            Dim s As String = ""

            s += "<!-- Loading calendar definition file -->"
            s += vbCr & " <script type='text/javascript' src='js/dt/bootstrap-datepicker.js' charset='UTF-8'></script>"
            Dim lang As CFRConst.LanguageEnum = cfr.Language.getLanguage

            If lang = CFRConst.LanguageEnum.es Then
                s += "    <script type='text/javascript' src='js/dt/dtlang/bootstrap-datepicker.es.min.js'></script>" & vbCr
            ElseIf lang = CFRConst.LanguageEnum.en Then
                s += "   <script type='text/javascript' src='js/dt/dtlang/bootstrap-datepicker.en-GB.min.js'></script>" & vbCr
            ElseIf lang = CFRConst.LanguageEnum.de Then
                s += "   <script type='text/javascript' src='js/dt/dtlang/bootstrap-datepicker.de.min.js'></script>" & vbCr
            ElseIf lang = CFRConst.LanguageEnum.fr Then
                s += "   <script type='text/javascript' src='js/dt/dtlang/bootstrap-datepicker.fr.min.js'></script>" & vbCr
            Else
                s += "   <script type='text/javascript' src='js/dt/dtlang/bootstrap-datepicker.en-GB.min.js'></script>" & vbCr
            End If

            Dim sjs As String = "<script type='text/javascript'>"
            sjs += vbCr & "$('.input-daterange').datepicker({"
            sjs += vbCr & "language:  """ & Me.langSelected & ""","
            sjs += vbCr & "format: """ & Me.dateFormat & ""","
            'sjs += vbCr & "startDate: """ & Me.dateIn & ""","
            'sjs += vbCr & "endDate: """ & Me.dateMax & ""","
            sjs += vbCr & "autoclose: true,"
            sjs += vbCr & "orientation:'top auto',"
            sjs += vbCr & "setStartDate: """ & Me.dateIn & ""","
            sjs += vbCr & "disableTouchKeyboard: true,"
            sjs += vbCr & "});"
            sjs += vbCr & "</script>"

            Return s & vbCr & sjs & vbCr & Me.getGenericConfigurationJavascript
        End Function

        Public Function getGenericConfigurationJavascript() As String
            Dim s As String = String.Empty
            's += vbCr & " <script type='text/javascript' src='js/dt/bootstrap-datepicker.js' charset='UTF-8'></script>"
            s += vbCr & " <script>"
            s += vbCr & " $('#checkoutdate').focus(function () {"
            s += vbCr & "  $(this).blur();"
            s += vbCr & " });"
            s += vbCr & " $('#checkindate').focus(function () {"
            s += vbCr & "   $(this).blur();"
            s += vbCr & " });"
            s += vbCr & "  $('#checkindate').datepicker()"
            s += vbCr & "  .on('changeDate', function (ev) {"
            s += vbCr & " var checkin = new Date(ev.date);"
            s += vbCr & " var checkout = $('#checkoutdate').datepicker('getDate');"
            's += vbCr & " console.log('checkin ' + checkin);"
            's += vbCr & " console.log('checkout ' + checkout);"

            If israngeplusone = True Then
                s += vbCr & " if (checkin >= checkout) {"
            Else
                s += vbCr & " if (checkin > checkout) {"
            End If


            s += vbCr & "    console.log('checkin >= checkout');"
            s += vbCr & "    checkout = new Date(checkin.setDate(checkin.getDate() + 1));"
            s += vbCr & "    $('#checkoutdate').datepicker('update', checkout);"
            s += vbCr & "    $('#checkoutdate').datepicker('setStartDate', checkout);"
            s += vbCr & "    //$('#checkoutdate').trigger('focus');"
            s += vbCr & "   } else {"
            s += vbCr & "       console.log('Correct');"
            s += vbCr & "   }"
            s += vbCr & "  });"
            s += vbCr & "  </script>"
            Return s
        End Function


    End Class
#End Region


#Region "Calendar helper"

#End Region

    Private Function getCssListForHtml(cssList As ArrayList) As String
        Dim cssStr As String = ""
        For Each cls As String In cssList
            cssStr += " " & cls
        Next
        Return cssStr
    End Function

    Public Function getColorCSS() As String
        Dim s As New StringBuilder(500)
        Dim x As Integer = 1
        For Each col As String In Me.colorList
            s.Append(".gpc_" & x & " {BACKGROUND-COLOR: " & col & "; }")
            x += 1
        Next
        Return s.ToString

    End Function

    Public Sub New()
        configureColorList()
    End Sub

    Public Sub configureColorList()
        'arraylist of color for calendar
        Me.colorList.Add("#ffbf00")
        Me.colorList.Add("#ffff00")
        Me.colorList.Add("#bfff00")
        'Me.colorList.Add("#05B900")
        Me.colorList.Add("#8AC177") 'green
        'Me.colorList.Add("#00ff00")
        Me.colorList.Add("#00ff40")
        'Me.colorList.Add("#00ff80")
        Me.colorList.Add("#00ffbf")
        'Me.colorList.Add("#00ffff")
        Me.colorList.Add("#00bfff")
        'Me.colorList.Add("#007fff")
        Me.colorList.Add("#0040ff")
        Me.colorList.Add("#0000ff") 'lightblue
        Me.colorList.Add("#4000ff")
        Me.colorList.Add("#7f00ff")
        Me.colorList.Add("#bf00ff") 'blue
        Me.colorList.Add("#ff00ff")
        Me.colorList.Add("#ff00bf")
        Me.colorList.Add("#ff0080")
        Me.colorList.Add("#ff0040")
        Me.colorList.Add("#ff0000") 'red
        Me.colorList.Add("#ff0000")
        Me.colorList.Add("#ff4000")
        Me.colorList.Add("#ff8000")
    End Sub

End Class

Public Class CalendarDays
    Public dayList As New ArrayList

    Public Sub add(newDay As Date)
        Dim cd As New calendarDay(newDay)
        Me.dayList.Add(cd)
    End Sub

    Public Function getCalDay(daySelected As Date) As calendarDay
        For x As Integer = 0 To dayList.Count - 1
            Dim cd As calendarDay = dayList(x)
            If cd.dia.Date = daySelected.Date Then
                Return cd
            End If
        Next
        Return Nothing
    End Function

End Class

Public Class calendarDay
    ''' <summary>
    ''' return week day number 1-7
    ''' </summary>
    ''' <remarks></remarks>
    Public weekDay As Integer
    Public dia As Date
    Public externalValue As Object
    Public divcontainer As String
    Public cssClass As String = String.Empty

    ''' <summary>
    ''' return date in yyyy/MM/dd
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property englishDate As String
        Get
            Return Me.dia.ToString("yyyy/MM/dd")
        End Get
    End Property

    Public ReadOnly Property spanishDate As String
        Get
            Return Me.dia.ToString("dd/MM/yyyy")
        End Get
    End Property

    Public Sub New()

    End Sub

    Public Sub New(newDay As Date)
        configNewDay(newDay)
    End Sub

    Public Sub New(newDay As Date, value As Object)
        configNewDay(newDay)
        Me.externalValue = value
    End Sub

    Public Sub configNewDay(newDay As Date)
        Me.dia = newDay
        Me.weekDay = DatePart("w", newDay, vbMonday)
    End Sub
    ''' <summary>
    ''' return day for sql server in yyyy-MM-dd
    ''' this is iso 8061 format invariant
    ''' in sql use DIA =CONVERT(DATETIME,getSqlDateyyyyMMdd 126)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSqlDateyyyyMMdd() As String
        Return Me.dia.ToString("yyyy-MM-dd")
    End Function

    ''' <summary>
    ''' Return month number
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMonth() As Integer
        Return Month(Me.dia)
    End Function
    ''' <summary>
    ''' return the number day from 1 to 31 for the month selected in day
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDayNum() As Integer
        Return Day(Me.dia)
    End Function
    ''' <summary>
    ''' return first 2 letter of the day
    ''' </summary>
    ''' <remarks></remarks>
    Public Function getWeekDayLetter(Optional numLetters As Integer = 2) As String
        Dim strDia As String = WeekdayName(DatePart("w", Me.dia), True, vbSunday)
        strDia = Left$(strDia, numLetters)
        strDia = UCase$(Left$(strDia, 1)) & Mid$(strDia, 2, numLetters - 1)
        Return strDia
    End Function

    Public Function getMonthName() As String
        Select Case Month(Me.dia)
            Case 1
                Return "Enero"
            Case 2
                Return "Febrero"
            Case 3
                Return "Marzo"
            Case 4
                Return "Abril"
            Case 5
                Return "Mayo"
            Case 6
                Return "Junio"
            Case 7
                Return "Julio"
            Case 8
                Return "Agosto"
            Case 9
                Return "Septiembre"
            Case 10
                Return "Octubre"
            Case 11
                Return "Noviembre"
            Case 12
                Return "Diciembre"
        End Select
        Return Nothing
    End Function

    Public Sub LogObj()
        Debug.WriteLine(" Day: " & Me.dia)
    End Sub


End Class
