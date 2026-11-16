
Imports System
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports System.Runtime.InteropServices

Namespace ind.bk.obj

    <Serializable()>
    <DataContractAttribute(IsReference:=True)>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class AdminLogin_Manager
        Inherits ind.db.DBHelper



        ''' <summary>
        ''' A creatable COM class must have a Public Sub New() 
        ''' with no parameters, otherwise, the class will not be 
        ''' registered in the COM registry and cannot be created 
        ''' via CreateObject.
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub
        Public Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("AdminLogin_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("AdminLogin_GetByID")
            addSqlParameterSimple(cmd, "id_login", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As AdminLogin, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("AdminLogin_Insert")

            addSqlParameterSimple(cmd, AdminLogin.dbcol.Login, obj2.Login)
            addSqlParameterSimple(cmd, AdminLogin.dbcol.Clave, obj2.Clave)
            addSqlParameterSimple(cmd, AdminLogin.dbcol.bloqueado, obj2.bloqueado)
            addSqlParameterSimple(cmd, AdminLogin.dbcol.fecha, obj2.fecha)
            addSqlParameterSimple(cmd, AdminLogin.dbcol.Perfil, obj2.Perfil)
            addSqlParameterSimple(cmd, AdminLogin.dbcol.Usuario, obj2.Usuario)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id_login = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As AdminLogin, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("AdminLogin_UpdateByID")

            addSqlParameterSimple(cmd, AdminLogin.dbcol.id_login, obj2.id_login)
            addSqlParameterSimple(cmd, AdminLogin.dbcol.Login, IIf(IsNothing(obj2.Login), "", obj2.Login))
            addSqlParameterSimple(cmd, AdminLogin.dbcol.Clave, IIf(IsNothing(obj2.Clave), "", obj2.Clave))
            addSqlParameterSimple(cmd, AdminLogin.dbcol.bloqueado, obj2.bloqueado)
            addSqlParameterSimple(cmd, AdminLogin.dbcol.fecha, IIf(IsNothing(obj2.fecha), "", obj2.fecha))
            addSqlParameterSimple(cmd, AdminLogin.dbcol.Perfil, obj2.Perfil)
            addSqlParameterSimple(cmd, AdminLogin.dbcol.Usuario, IIf(IsNothing(obj2.Usuario), "", obj2.Usuario))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("AdminLogin_DeleteByID")

            addSqlParameterSimple(cmd, AdminLogin.dbcol.id_login, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
