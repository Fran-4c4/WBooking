
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
    Public Class R_Agencia_Empresa_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Agencia_Empresa_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Agencia_Empresa_GetByID")
            addSqlParameterSimple(cmd, "id", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As R_Agencia_Empresa, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Agencia_Empresa_Insert")

            addSqlParameterSimple(cmd, R_Agencia_Empresa.dbcol.cod_Agencia, obj2.cod_Agencia)
            addSqlParameterSimple(cmd, R_Agencia_Empresa.dbcol.cod_Empresa, obj2.cod_Empresa)
            addSqlParameterSimple(cmd, R_Agencia_Empresa.dbcol.cod_hotel, obj2.cod_hotel)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As R_Agencia_Empresa, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Agencia_Empresa_UpdateByID")

            addSqlParameterSimple(cmd, R_Agencia_Empresa.dbcol.cod_Agencia, obj2.cod_Agencia)
            addSqlParameterSimple(cmd, R_Agencia_Empresa.dbcol.cod_Empresa, obj2.cod_Empresa)
            addSqlParameterSimple(cmd, R_Agencia_Empresa.dbcol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_Agencia_Empresa.dbcol.id, obj2.id)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Agencia_Empresa_DeleteByID")

            addSqlParameterSimple(cmd, R_Agencia_Empresa.dbcol.id, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
