Imports Microsoft.VisualBasic
Imports System.Data
Imports cfr.tool



Namespace cfr.mgr


    Public Class CountryMgr
        Public Shared Function CREATE(ByRef obj As cfr.obj.Country, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Country
            Dim paramCol(7) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("cod_Pais", obj.cod_Pais)
            paramCol(1) = ind.bk.DBMgr.getSpParameter("Pais", obj.Description)
            paramCol(2) = ind.bk.DBMgr.getSpParameter("cod_cont", obj.cod_cont)
            paramCol(3) = ind.bk.DBMgr.getSpParameter("prefijoTelfPais", obj.prefijoTelfPais)
            paramCol(4) = ind.bk.DBMgr.getSpParameter("idioma1", obj.idioma1)
            paramCol(5) = ind.bk.DBMgr.getSpParameter("idioma2", obj.idioma2)
            paramCol(6) = ind.bk.DBMgr.getSpParameter("idioma3", obj.idioma3)
            paramCol(7) = ind.bk.DBMgr.getSpParameter("idioma4", obj.idioma4)
            Dim dt As System.Data.DataTable = ind.bk.DBMgr.getDT("t_pais_insert", paramCol, trans)

            'If dt.Rows.Count > 0 Then
            '    obj.cod_Pais = dt.Rows(0)(0)
            'End If
            Return obj
        End Function


        Public Shared Function UPDATE(ByRef obj As cfr.obj.Country, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Country
            Dim paramCol(7) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("cod_Pais", obj.cod_Pais)
            paramCol(1) = ind.bk.DBMgr.getSpParameter("Pais", obj.Description)
            paramCol(2) = ind.bk.DBMgr.getSpParameter("cod_cont", obj.cod_cont)
            paramCol(3) = ind.bk.DBMgr.getSpParameter("prefijoTelfPais", obj.prefijoTelfPais)
            paramCol(4) = ind.bk.DBMgr.getSpParameter("idioma1", obj.idioma1)
            paramCol(5) = ind.bk.DBMgr.getSpParameter("idioma2", obj.idioma2)
            paramCol(6) = ind.bk.DBMgr.getSpParameter("idioma3", obj.idioma3)
            paramCol(7) = ind.bk.DBMgr.getSpParameter("idioma4", obj.idioma4)
            ind.bk.DBMgr.ExecuteSp("t_pais_update", paramCol, trans)
            Return obj
        End Function

        Public Shared Sub Delete(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing)
            Dim paramCol(0) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            ind.bk.DBMgr.ExecuteSp("t_pais_delete", paramCol, trans)
        End Sub

        Public Shared Function GetEntity(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Country
            Dim paramCol(0) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            Dim dt As DataTable = ind.bk.DBMgr.getDT("t_pais_GetByID", paramCol, trans)
            Dim obj As cfr.obj.Country = Nothing
            If dt.Rows.Count > 0 Then
                obj = New cfr.obj.Country
                obj.cod_Pais = Tools.cNull(dt.Rows(0)("cod_Pais"), 0)
                obj.Description = Tools.cNull(dt.Rows(0)("Pais"), "")
                obj.cod_cont = Tools.cNull(dt.Rows(0)("cod_cont"), 0)
                obj.prefijoTelfPais = Tools.cNull(dt.Rows(0)("prefijoTelfPais"), "")
                obj.idioma1 = Tools.cNull(dt.Rows(0)("idioma1"), "")
                obj.idioma2 = Tools.cNull(dt.Rows(0)("idioma2"), "")
                obj.idioma3 = Tools.cNull(dt.Rows(0)("idioma3"), "")
                obj.idioma4 = Tools.cNull(dt.Rows(0)("idioma4"), "")
            End If
            Return obj
        End Function

        ''' <summary>
        ''' Get all countries ordered by country name
        ''' </summary>
        ''' <param name="language"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getCountries(ByVal language As CFRConst.LanguageEnum) As Data.DataTable
            Dim sSql As String = String.Empty
            'there is only a country language now
            sSql = "SELECT * FROM t_pais "
            sSql += " ORDER BY pais " '& paisIdioma
            Return ind.bk.DBMgr.getDT(sSql)
        End Function

    End Class
End Namespace






