

Namespace ind.bk.date
    ''' <summary>
    ''' helper class to define range between dates
    ''' </summary>
    Public Class DateRangeCls
        Public dateFrom As Date
        Public dateTo As Date
        Public Sub New(dateFrom As Date, dateTo As Date)
            Me.dateFrom = dateFrom
            Me.dateTo = dateTo
        End Sub
        ''' <summary>
        ''' return the last day as date  for selected month of day passed
        ''' </summary>
        ''' <param name="day2Check"></param>
        ''' <returns></returns>
        Public Shared Function getLastDayOfMonth(day2Check As Date) As Date
            Dim DaysInMonth As Integer = Date.DaysInMonth(day2Check.Year, day2Check.Month) 'to know last day in month
            Dim dayLast As New Date(day2Check.Year, day2Check.Month, DaysInMonth) 'set new last day
            Return dayLast
        End Function

        Public Function getSqlConvert(day2Convert As dateSelected, Optional convertType As sqlConvertType = sqlConvertType.c126) As String
            Dim dateSel As Date = Date.Now
            If day2Convert = dateSelected.dateFrom Then
                dateSel = Me.dateFrom
            Else
                dateSel = Me.dateTo
            End If
            Select Case convertType
                Case sqlConvertType.c126
                    Return " CONVERT(DATETIME,'" & dateSel.ToString("yyyy-MM-dd") & "',126) "
                Case Else
                    Return " CONVERT(DATETIME,'" & dateSel.ToString("yyyy-MM-dd") & "',126) "
            End Select
        End Function

        Public Enum dateSelected As Byte
            dateFrom = 1
            dateTo = 2
        End Enum

        Public Enum sqlConvertType As Integer
            c126 = 126
            c113 = 113
        End Enum



    End Class
End Namespace


