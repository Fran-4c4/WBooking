Imports Microsoft.VisualBasic

Namespace cfr
    Public Class SiteManager

        Public Function getSite(ByVal id As String) As cfr.SiteObj
            Dim sSQL As String
            sSQL = "SELECT * FROM SITES WHERE COD_SITE=@siteId"

            Dim dvCSS As Data.DataTable = ind.bk.DBMgr.getDT(
                sSQL,
                New Data.SqlClient.SqlParameter("@siteId", Data.SqlDbType.VarChar, 50) With {.Value = id})

            If dvCSS.Rows.Count = 0 Then
                Dim st As New cfr.SiteObj
                st.id = id
                st.description = ""
                st.masterPageCode = ""
                Return st
                'Throw New Exception("Code site not exist, please contact with administrator to configure.")
            Else
                Dim st As New cfr.SiteObj
                st.id = id
                st.description = cfr.tool.Tools.cNullStr(dvCSS.Rows(0)("descripcion"), "")
                st.masterPageCode = cfr.tool.Tools.cNullStr(dvCSS.Rows(0)("masterPageCode"), "")
                st.showPrincipal = cfr.tool.Tools.cNullStr(dvCSS.Rows(0)("showPrincipal"), False)
                st.tipo = cfr.tool.Tools.cNullStr(dvCSS.Rows(0)("tipo"), 0)

                Return st
            End If
        End Function

    End Class
End Namespace
