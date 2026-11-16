
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
    Partial Public Class R_Usuarios_Permisos
        Inherits ind.db.DBHelper

        Public Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Usuarios_Permisos_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByIDUsuario(id_usuario As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Usuarios_Permisos_GetByIdUsuario")
            addSqlParameterSimple(cmd, "id_usuario", id_usuario)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function




        Public Function insert(obj2 As R_Usuarios_Permisos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Usuarios_Permisos_Insert")

            addSqlParameterSimple(cmd, R_Usuarios_Permisos.DBCol.id_usuario, obj2.id_usuario)
            addSqlParameterSimple(cmd, R_Usuarios_Permisos.DBCol.cod_permiso, obj2.cod_permiso)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            'obj2.id = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As R_Usuarios_Permisos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Usuarios_Permisos_UpdateByID")

            'addSqlParameterSimple(cmd, R_Usuarios_Permisos.DBCol.id, obj2.id)
            addSqlParameterSimple(cmd, R_Usuarios_Permisos.DBCol.id_usuario, obj2.id_usuario)
            addSqlParameterSimple(cmd, R_Usuarios_Permisos.DBCol.cod_permiso, obj2.cod_permiso)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Usuarios_Permisos_DeleteByID")

            addSqlParameterSimple(cmd, R_Usuarios_Permisos.DBCol.id, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
