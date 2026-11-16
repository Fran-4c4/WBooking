
Imports System
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports System.Runtime.InteropServices

Namespace ind.bk.obj

    '<Serializable()>
    '<DataContractAttribute(IsReference:=True)>
    '<ClassInterface(ClassInterfaceType.AutoDual)>
    Partial Public Class R_Usuarios

        Public Function getDataTableByCod_hotel(Cod_hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Usuarios_GetByCod_hotel")
            addSqlParameterSimple(cmd, ind.bk.obj.T_HOTELES.DBCol.cod_Hotel, Cod_hotel)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function DeleteByUserID(id_usuario As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Usuarios_Permisos_DeleteByUserID")

            addSqlParameterSimple(cmd, R_Usuarios_Permisos.DBCol.id_usuario, id_usuario)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function


        Public Shared Sub updateUserLastAccess(ByVal userID As Integer)
            Try
                Dim s As String = "UPDATE r_usuarios SET ultimoacceso=GETDATE() WHERE id_usuario=@userId"
                ind.bk.DBMgr.Execute(
                    s,
                    New SqlParameter("@userId", SqlDbType.Int) With {.Value = userID})
            Catch ex As Exception

            End Try


        End Sub

    End Class



End Namespace
