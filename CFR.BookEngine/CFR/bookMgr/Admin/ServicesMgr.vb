Imports Microsoft.VisualBasic
Imports System.Data
Imports cfr.tool

Namespace cfr.mgr


    Public Class ServicesMgr
        Public Function CREATE(ByRef obj As cfr.obj.Services, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Services
            Dim paramCol(8) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("cod_Servicio", obj.cod_Servicio)
            paramCol(1) = ind.bk.DBMgr.getSpParameter("servicio", obj.servicio)
            paramCol(2) = ind.bk.DBMgr.getSpParameter("Ingles", obj.Ingles)
            paramCol(3) = ind.bk.DBMgr.getSpParameter("Aleman", obj.Aleman)
            paramCol(4) = ind.bk.DBMgr.getSpParameter("Frances", obj.Frances)
            paramCol(5) = ind.bk.DBMgr.getSpParameter("Catalan", obj.Catalan)
            paramCol(6) = ind.bk.DBMgr.getSpParameter("Buscar", obj.Buscar)
            paramCol(7) = ind.bk.DBMgr.getSpParameter("Padre", obj.Padre)
            paramCol(8) = ind.bk.DBMgr.getSpParameter("tipo_carac", obj.tipo_carac)
            Dim dt As System.Data.DataTable = ind.bk.DBMgr.getDT("T_SERVICIOS_insert", paramCol, trans)
            If dt.Rows.Count > 0 Then
                obj.cod_Servicio = dt.Rows(0)(0)
            End If
            Return obj
        End Function


        Public Function UPDATE(ByRef obj As cfr.obj.Services, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Services
            Dim paramCol(8) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("cod_Servicio", obj.cod_Servicio)
            paramCol(1) = ind.bk.DBMgr.getSpParameter("servicio", obj.servicio)
            paramCol(2) = ind.bk.DBMgr.getSpParameter("Ingles", obj.Ingles)
            paramCol(3) = ind.bk.DBMgr.getSpParameter("Aleman", obj.Aleman)
            paramCol(4) = ind.bk.DBMgr.getSpParameter("Frances", obj.Frances)
            paramCol(5) = ind.bk.DBMgr.getSpParameter("Catalan", obj.Catalan)
            paramCol(6) = ind.bk.DBMgr.getSpParameter("Buscar", obj.Buscar)
            paramCol(7) = ind.bk.DBMgr.getSpParameter("Padre", obj.Padre)
            paramCol(8) = ind.bk.DBMgr.getSpParameter("tipo_carac", obj.tipo_carac)
            ind.bk.DBMgr.ExecuteSp("T_SERVICIOS_update", paramCol, trans)
            Return obj
        End Function

        Public Sub Delete(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing)
            Dim paramCol(0) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            ind.bk.DBMgr.ExecuteSp("T_SERVICIOS_delete", paramCol, trans)
        End Sub

        Public Function GetEntity(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Services
            Dim paramCol(0) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            Dim dt As dataTable = ind.bk.DBMgr.getDT("T_SERVICIOS_GetByID", paramCol, trans)
            Dim obj As cfr.obj.Services = Nothing
            If dt.rows.count > 0 Then
                obj = New cfr.obj.Services
                obj.cod_Servicio = Tools.cNull(dt.rows(0)("cod_Servicio"))
                obj.servicio = Tools.cNull(dt.rows(0)("servicio"))
                obj.Ingles = Tools.cNull(dt.rows(0)("Ingles"))
                obj.Aleman = Tools.cNull(dt.rows(0)("Aleman"))
                obj.Frances = Tools.cNull(dt.rows(0)("Frances"))
                obj.Catalan = Tools.cNull(dt.rows(0)("Catalan"))
                obj.Buscar = Tools.cNull(dt.rows(0)("Buscar"))
                obj.Padre = Tools.cNull(dt.rows(0)("Padre"))
                obj.tipo_carac = Tools.cNull(dt.rows(0)("tipo_carac"))
            End If
            Return obj
        End Function




    End Class
End Namespace
