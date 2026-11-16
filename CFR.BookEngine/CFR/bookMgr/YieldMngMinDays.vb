Imports Microsoft.VisualBasic
Namespace cfr


    Public Class YieldMngMinDays


        Public Shared Function ListYieldMngDiasMin(ByVal cod_Hotel As Double _
            , ByVal dateIn As Date, ByVal dateOut As Date _
            ) As Data.DataTable

            Dim sSQL As String


            sSQL = "SELECT  *,convert(varchar(10),fechainicio,103)as fecha1 ,convert(varchar(10),fechafin,103)as fecha2"
            sSQL = sSQL & " FROM R_YieldMng_Diasmin "
            sSQL = sSQL & " Where cod_HOTEL = " & cod_Hotel


            sSQL = sSQL & " AND (FECHAINICIO<=CONVERT(DATETIME,'" & cfr.tool.DateTools.getyyyymmddShort(dateOut) & "',113) "
            sSQL = sSQL & " AND fechafin>=CONVERT(DATETIME,'" & cfr.tool.DateTools.getyyyymmddShort(dateIn) & "',113))"

            sSQL = sSQL & " ORDER BY fechainicio "


            Return ind.bk.DBMgr.getDT(sSQL)

        End Function
    End Class
End Namespace