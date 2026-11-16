
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
    Public Class T_Address_Man
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Address_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Address_GetByID")
            addSqlParameterSimple(cmd, "id_address", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As T_Address, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Address_Insert")

            addSqlParameterSimple(cmd, T_Address.DBCol.address, obj2.address)
            addSqlParameterSimple(cmd, T_Address.DBCol.address1, obj2.address1)
            addSqlParameterSimple(cmd, T_Address.DBCol.city, obj2.city)
            addSqlParameterSimple(cmd, T_Address.DBCol.postalcode, obj2.postalcode)
            addSqlParameterSimple(cmd, T_Address.DBCol.suburb, obj2.suburb)
            addSqlParameterSimple(cmd, T_Address.DBCol.State, obj2.State)
            addSqlParameterSimple(cmd, T_Address.DBCol.country, obj2.country)
            addSqlParameterSimple(cmd, T_Address.DBCol.email, obj2.email)
            addSqlParameterSimple(cmd, T_Address.DBCol.email1, obj2.email1)
            addSqlParameterSimple(cmd, T_Address.DBCol.phone, obj2.phone)
            addSqlParameterSimple(cmd, T_Address.DBCol.phone1, obj2.phone1)
            addSqlParameterSimple(cmd, T_Address.DBCol.fax, obj2.fax)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id_address = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As T_Address, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Address_UpdateByID")

            addSqlParameterSimple(cmd, T_Address.DBCol.id_address, obj2.id_address)
            addSqlParameterSimple(cmd, T_Address.DBCol.address, IIf(IsNothing(obj2.address), "", obj2.address))
            addSqlParameterSimple(cmd, T_Address.DBCol.address1, IIf(IsNothing(obj2.address1), "", obj2.address1))
            addSqlParameterSimple(cmd, T_Address.DBCol.city, IIf(IsNothing(obj2.city), "", obj2.city))
            addSqlParameterSimple(cmd, T_Address.DBCol.postalcode, IIf(IsNothing(obj2.postalcode), "", obj2.postalcode))
            addSqlParameterSimple(cmd, T_Address.DBCol.suburb, IIf(IsNothing(obj2.suburb), "", obj2.suburb))
            addSqlParameterSimple(cmd, T_Address.DBCol.State, IIf(IsNothing(obj2.State), "", obj2.State))
            addSqlParameterSimple(cmd, T_Address.DBCol.country, IIf(IsNothing(obj2.country), "", obj2.country))
            addSqlParameterSimple(cmd, T_Address.DBCol.email, IIf(IsNothing(obj2.email), "", obj2.email))
            addSqlParameterSimple(cmd, T_Address.DBCol.email1, IIf(IsNothing(obj2.email1), "", obj2.email1))
            addSqlParameterSimple(cmd, T_Address.DBCol.phone, IIf(IsNothing(obj2.phone), "", obj2.phone))
            addSqlParameterSimple(cmd, T_Address.DBCol.phone1, IIf(IsNothing(obj2.phone1), "", obj2.phone1))
            addSqlParameterSimple(cmd, T_Address.DBCol.fax, IIf(IsNothing(obj2.fax), "", obj2.fax))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Address_DeleteByID")

            addSqlParameterSimple(cmd, T_Address.DBCol.id_address, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
