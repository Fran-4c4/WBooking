Imports System.Data
Imports System.Diagnostics
Imports Microsoft.VisualBasic



Public Class CalendarUI
    Inherits CalendarBase


#Region "Calendar for months"
    ''' <summary>
    ''' Get a calendar empty for selected month from actual
    ''' </summary>
    ''' <param name="nMonth"></param>
    ''' <returns></returns>
    Public Shared Function showCalendarForRange(Optional nMonth As Integer = 11) As String
        Dim uical As New CalendarUI
        uical.ConfigureN_Month(nMonth, 0)

        Return uical.getCalendarUIHtml
    End Function

    Public Shared Function showCalendarForTemporada(cod_temporada As Integer, Optional nMonth As Integer = 11) As String
        Dim uical As New CalendarUI
        uical.ConfigureN_Month(nMonth, 0)
        Dim dtCalendarResult As CalendarDays = uical.fillCalendarByTemporada(cod_temporada)
        Return uical.getCalendarUI(dtCalendarResult)
    End Function

    ''' <summary>
    ''' Get datatable with periods asignated to temporada
    ''' </summary>
    ''' <param name="cod_temporada"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fillCalendarByTemporada(cod_temporada As Integer, Optional autoColor As Boolean = False) As CalendarDays

        Dim tableRes As New ind.bk.obj.R_Temporadas_Manager
        Dim rsPeriodos As DataView = tableRes.getPeriodosByIDTemporada(cod_temporada).DefaultView
        If autoColor = True Then
            Me.groupColor += 1
        End If
        fillCalendarByRange(rsPeriodos, autoColor)

        Return calendarDaysObj

    End Function
    ''' <summary>
    ''' fill the calendar with values in dataview. Mark if the selected day is used.
    ''' </summary>
    ''' <param name="rsPeriodos"></param>
    ''' <param name="autoColor"></param>
    ''' <returns></returns>
    Public Function fillCalendarByRange(rsPeriodos As DataView, Optional autoColor As Boolean = False) As CalendarDays

        For Each dr As calendarDay In calendarDaysObj.dayList
            Dim strFilter As String = "fechainicio<=#" & dr.englishDate & "# AND fechafin>=#" & dr.englishDate & "#"
            rsPeriodos.RowFilter = strFilter
            If rsPeriodos.Count > 0 Then
                If dr.externalValue = 1 Then 'it is marked with 2 ranges
                    If autoColor = True Then
                        dr.cssClass += String.Format(" gpc_{0} ", Me.groupColor)
                        If dr.cssClass.ToUpper.Contains("MIXSEASON") = False Then
                            dr.cssClass += " mixseason "
                        End If
                    End If
                Else
                    dr.externalValue = 1
                    If autoColor = True Then
                        dr.cssClass = String.Format(" gpc_{0} ", Me.groupColor)
                    End If
                End If

            Else
                'dr.externalValue = 0
            End If
        Next

        Return calendarDaysObj

    End Function



#End Region







End Class
