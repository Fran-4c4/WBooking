
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
        Inherits ind.db.DBHelper


        Public Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Usuarios_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Usuarios_GetByID")
            addSqlParameterSimple(cmd, "id_usuario", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As R_Usuarios, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Usuarios_Insert")

            addSqlParameterSimple(cmd, R_Usuarios.DBCol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_Usuarios.DBCol.usuario, obj2.usuario)
            addSqlParameterSimple(cmd, R_Usuarios.DBCol.login, obj2.login)
            addSqlParameterSimple(cmd, R_Usuarios.DBCol.clave, obj2.clave)
            addSqlParameterSimple(cmd, R_Usuarios.DBCol.perfil, obj2.perfil)
            addSqlParameterSimple(cmd, R_Usuarios.DBCol.email, obj2.email)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id_usuario = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As R_Usuarios, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Usuarios_UpdateByID")

            addSqlParameterSimple(cmd, R_Usuarios.DBCol.id_usuario, obj2.id_usuario)
            addSqlParameterSimple(cmd, R_Usuarios.DBCol.usuario, IIf(IsNothing(obj2.usuario), "", obj2.usuario))
            addSqlParameterSimple(cmd, R_Usuarios.DBCol.login, IIf(IsNothing(obj2.login), "", obj2.login))
            addSqlParameterSimple(cmd, R_Usuarios.DBCol.clave, IIf(IsNothing(obj2.clave), "", obj2.clave))
            addSqlParameterSimple(cmd, R_Usuarios.DBCol.perfil, IIf(IsNothing(obj2.perfil), "", obj2.perfil))
            addSqlParameterSimple(cmd, R_Usuarios.DBCol.email, IIf(IsNothing(obj2.email), "", obj2.email))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Usuarios_DeleteByID")

            addSqlParameterSimple(cmd, R_Usuarios.DBCol.id_usuario, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
