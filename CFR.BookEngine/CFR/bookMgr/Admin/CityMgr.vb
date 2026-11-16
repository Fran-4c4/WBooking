Imports Microsoft.VisualBasic
Imports System.Data
Imports cfr.tool


Namespace cfr.mgr


    Public Class CityMgr

        Public Shared Function CREATE(ByRef obj As cfr.obj.City, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.City
            Dim paramCol(6) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("cod_ciudad", obj.cod_ciudad)
            paramCol(1) = ind.bk.DBMgr.getSpParameter("cod_pais", obj.country.idEntity)
            paramCol(2) = ind.bk.DBMgr.getSpParameter("ciudad", obj.desc_esp)
            paramCol(3) = ind.bk.DBMgr.getSpParameter("ciudad1", obj.ciudad1)
            paramCol(4) = ind.bk.DBMgr.getSpParameter("ciudad2", obj.ciudad2)
            paramCol(5) = ind.bk.DBMgr.getSpParameter("ciudad3", obj.ciudad3)
            paramCol(6) = ind.bk.DBMgr.getSpParameter("ciudad4", obj.ciudad4)
            Dim dt As System.Data.DataTable = ind.bk.DBMgr.getDT("T_CIUDADES_insert", paramCol, trans)

            Return obj
        End Function


        Public Shared Function UPDATE(ByRef obj As cfr.obj.City, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.City
            Dim paramCol(6) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("cod_ciudad", obj.cod_ciudad)
            paramCol(1) = ind.bk.DBMgr.getSpParameter("cod_pais", obj.country.idEntity)
            paramCol(2) = ind.bk.DBMgr.getSpParameter("ciudad", obj.desc_esp)
            paramCol(3) = ind.bk.DBMgr.getSpParameter("ciudad1", obj.ciudad1)
            paramCol(4) = ind.bk.DBMgr.getSpParameter("ciudad2", obj.ciudad2)
            paramCol(5) = ind.bk.DBMgr.getSpParameter("ciudad3", obj.ciudad3)
            paramCol(6) = ind.bk.DBMgr.getSpParameter("ciudad4", obj.ciudad4)
            ind.bk.DBMgr.ExecuteSp("T_CIUDADES_update", paramCol, trans)
            Return obj
        End Function

        Public Shared Sub Delete(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing)
            Dim paramCol(0) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            ind.bk.DBMgr.ExecuteSp("T_CIUDADES_delete", paramCol, trans)
        End Sub

        Public Shared Function GetEntity(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.City
            Dim paramCol(0) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            Dim dt As DataTable = ind.bk.DBMgr.getDT("T_CIUDADES_GetByID", paramCol, trans)
            Dim obj As cfr.obj.City = Nothing
            If dt.Rows.Count > 0 Then
                obj = New cfr.obj.City
                obj.cod_ciudad = Tools.cNull(dt.Rows(0)("cod_ciudad"), 0)
                obj.desc_esp = Tools.cNull(dt.Rows(0)("ciudad"), "")
                obj.ciudad1 = Tools.cNull(dt.Rows(0)("ciudad1"), "")
                obj.ciudad2 = Tools.cNull(dt.Rows(0)("ciudad2"), "")
                obj.ciudad3 = Tools.cNull(dt.Rows(0)("ciudad3"), "")
                obj.ciudad4 = Tools.cNull(dt.Rows(0)("ciudad4"), "")

                obj.country = cfr.mgr.CountryMgr.GetEntity(Tools.cNull(dt.Rows(0)("cod_pais"), 0), trans)

            End If
            Return obj
        End Function

    End Class
End Namespace



