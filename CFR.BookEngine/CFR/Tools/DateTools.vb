Imports Microsoft.VisualBasic


Namespace cfr.tool


    Public Class DateTools
#Region "BBDD"
        
        Public Shared Function getyyyymmdd(ByVal fecha As Date) As String
            Dim fechaCom As String
            fechaCom = Year(fecha) & Right("0" & Month(fecha), 2) & Right("0" & Day(fecha), 2) & " " & fecha.ToLongTimeString
            Return fechaCom
        End Function

        Public Shared Function getyyyymmddShort(ByVal fecha As Date) As String
            Dim fechaCom As String
            fechaCom = Year(fecha) & Right("0" & Month(fecha), 2) & Right("0" & Day(fecha), 2)
            Return fechaCom
        End Function

        Public Shared Function getUSmmddyyyy(ByVal fecha As Date) As String
            Dim fechaCom As String
            fechaCom = Right("0" & Month(fecha), 2) & "/" & Right("0" & Day(fecha), 2) & "/" & Year(fecha) & " " & fecha.ToLongTimeString
            Return fechaCom
        End Function

        Public Shared Function getUSmmddyyyyShort(ByVal fecha As Date) As String
            Dim fechaCom As String
            fechaCom = Right("0" & Month(fecha), 2) & "/" & Right("0" & Day(fecha), 2) & "/" & Year(fecha)
            Return fechaCom
        End Function

        ''' <summary>
        ''' used for sql querys
        ''' </summary>
        ''' <param name="dateValue"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getConvertDate(ByVal dateValue As Date) As String
            Dim ds As String = getyyyymmdd(dateValue)
            Return "CONVERT(DATETIME, '" & ds & "', 113)"
        End Function

#End Region

        Public Shared Function GetDias(ByRef fecha1 As Date, ByRef fecha2 As Date) As Integer
            Dim datediference As Integer
            '**********************************
            ' Devuelve el nº de dias entre dos fechas, pero orientado a la reserva, si la diferencia es 0
            ' devolverá 1 dia.
            '**********************************
            datediference = fecha2.Date.Subtract(fecha1.Date).Days  ' DateDiff(Microsoft.VisualBasic.DateInterval.Day, fechaCom1, fechaCom2)
            If datediference = 0 And fecha1.Date = fecha2.Date Then
                Return 1
            Else
                Return datediference
            End If
        End Function

    End Class
End Namespace