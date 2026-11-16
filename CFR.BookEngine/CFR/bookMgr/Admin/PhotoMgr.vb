Imports Microsoft.VisualBasic
Imports System.Data

Namespace cfr.mgr


    ''' <summary>
    ''' Class to manage photos
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Photo

        Public Shared Function CREATE(ByRef obj As Object, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Photo
            Dim paramCol(5) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("cod_hotel", obj.cod_hotel)
            paramCol(1) = ind.bk.DBMgr.getSpParameter("id_Foto", obj.id_Foto)
            paramCol(2) = ind.bk.DBMgr.getSpParameter("FotoFileName", obj.FotoFileName)
            paramCol(3) = ind.bk.DBMgr.getSpParameter("Description", obj.Description)
            paramCol(4) = ind.bk.DBMgr.getSpParameter("LastUpdate", obj.LastUpdate)
            paramCol(5) = ind.bk.DBMgr.getSpParameter("id_fotoType", obj.id_fotoType)

            Dim dt As DataTable = ind.bk.DBMgr.getDT("Fotos_insert", paramCol, trans)
            If dt.Rows.Count > 0 Then
                obj.id_Foto = dt.Rows(0)(0)
            End If
            Return obj
        End Function


        Public Shared Function UPDATE(ByRef obj As Object, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Photo
            Dim paramCol(5) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("cod_hotel", obj.cod_hotel)
            paramCol(1) = ind.bk.DBMgr.getSpParameter("id_Foto", obj.id_Foto)
            paramCol(2) = ind.bk.DBMgr.getSpParameter("FotoFileName", obj.FotoFileName)
            paramCol(3) = ind.bk.DBMgr.getSpParameter("Description", obj.Description)

            ind.bk.DBMgr.ExecuteSp("Fotos_update", paramCol, trans)

            Return obj
        End Function

        Public Shared Sub Delete(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing)
            Dim paramCol(0) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            ind.bk.DBMgr.ExecuteSp("Fotos_delete", paramCol, trans)
        End Sub

        Public Shared Function GetEntity(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Photo
            Dim paramCol(0) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            Dim dt As DataTable = ind.bk.DBMgr.getDT("Fotos_GetByID", paramCol, trans)
            Dim obj As New cfr.obj.Photo
            obj.cod_hotel = dt.Rows(0)("cod_hotel")
            obj.id_Foto = dt.Rows(0)("id_Foto")
            obj.FotoFileName = dt.Rows(0)("FotoFileName")
            obj.Description = dt.Rows(0)("Description")
            obj.LastUpdate = dt.Rows(0)("LastUpdate")
            obj.id_fotoType = dt.Rows(0)("id_fotoType")

            Return obj
        End Function


    End Class
End Namespace

