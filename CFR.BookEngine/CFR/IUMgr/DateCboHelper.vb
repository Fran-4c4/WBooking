Imports Microsoft.VisualBasic

Namespace cfr


    Public Class DateCboHelper
        Public Shared Function getcboDays(ByVal daySelected As Integer) As String
            Dim s As String = String.Empty

            For x As Integer = 1 To 31
                If CStr(x) = CStr(daySelected) Then
                    s += "<option value='" & x & "'selected>" & x & "</option>"
                Else
                    s += "<option value='" & x & "'>" & x & "</option>"
                End If
            Next

            Return s
        End Function

        Public Shared Function getCbomonth(ByVal month2Select As Integer, ByVal lang As CFRConst.LanguageEnum) As String
            Dim monthSelected As String
            Dim s As String = String.Empty
            Dim monthList() As String = getMatMeses(lang)

            For x As Integer = 1 To 12
                If x = month2Select Then
                    monthSelected = " Selected "
                Else
                    monthSelected = " "
                End If
                s += "<option value='" & x & "' " & monthSelected & ">" & monthList(x) & "</option>"
            Next

            Return s

        End Function

        Public Shared Function getCboYear(ByVal selectedYear As Integer, ByVal lowerYear As Integer, ByVal UpperYear As Integer) As String
            Dim s As String = ""
            For x As Integer = lowerYear To UpperYear
                If x = selectedYear Then
                    s += "<option value='" & x & "'selected>" & x & "</option>"
                Else
                    s += "<option value='" & x & "'>" & x & "</option>"
                End If
            Next
            Return s
        End Function



        Public Shared Function getMatMeses(ByVal lang As CFRConst.LanguageEnum) As String()
            Dim monthList(12) As String
            Select Case lang
                Case CFRConst.LanguageEnum.es, 0
                    monthList(1) = "Ene"
                    monthList(2) = "Feb"
                    monthList(3) = "Mar"
                    monthList(4) = "Abr"
                    monthList(5) = "May"
                    monthList(6) = "Jun"
                    monthList(7) = "Jul"
                    monthList(8) = "Ago"
                    monthList(9) = "Sep"
                    monthList(10) = "Oct"
                    monthList(11) = "Nov"
                    monthList(12) = "Dic"

                Case CFRConst.LanguageEnum.en
                    monthList(1) = "Jan"
                    monthList(2) = "Feb"
                    monthList(3) = "Mar"
                    monthList(4) = "Apr"
                    monthList(5) = "May"
                    monthList(6) = "Jun"
                    monthList(7) = "Jul"
                    monthList(8) = "Augt"
                    monthList(9) = "Sep"
                    monthList(10) = "Oct"
                    monthList(11) = "Nov"
                    monthList(12) = "Dec"

                Case CFRConst.LanguageEnum.de
                    monthList(1) = "Jan"
                    monthList(2) = "Feb"
                    monthList(3) = "Mar"
                    monthList(4) = "Apr"
                    monthList(5) = "May"
                    monthList(6) = "Jun"
                    monthList(7) = "Jul"
                    monthList(8) = "Augt"
                    monthList(9) = "Sep"
                    monthList(10) = "Oct"
                    monthList(11) = "Nov"
                    monthList(12) = "Dec"

                Case CFRConst.LanguageEnum.fr
                    monthList(1) = "Jan"
                    monthList(2) = "Feb"
                    monthList(3) = "Mar"
                    monthList(4) = "Apr"
                    monthList(5) = "May"
                    monthList(6) = "Jun"
                    monthList(7) = "Jul"
                    monthList(8) = "Augt"
                    monthList(9) = "Sep"
                    monthList(10) = "Oct"
                    monthList(11) = "Nov"
                    monthList(12) = "Dec"

            End Select

            Return monthList
        End Function
    End Class
End Namespace