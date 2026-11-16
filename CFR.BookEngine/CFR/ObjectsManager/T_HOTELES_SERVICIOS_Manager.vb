
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
    Public Class T_HOTELES_SERVICIOS_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_SERVICIOS_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_SERVICIOS_GetByID")
            addSqlParameterSimple(cmd, "id", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function



        Public Function insert(obj2 As T_HOTELES_SERVICIOS, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_SERVICIOS_Insert")

            addSqlParameterSimple(cmd, T_HOTELES_SERVICIOS.dbcol.cod_Hotel, obj2.cod_Hotel)
            addSqlParameterSimple(cmd, T_HOTELES_SERVICIOS.dbcol.cod_Servicio, obj2.cod_Servicio)
            addSqlParameterSimple(cmd, T_HOTELES_SERVICIOS.dbcol.NoHabs, obj2.NoHabs)
            addSqlParameterSimple(cmd, T_HOTELES_SERVICIOS.dbcol.Consultar, obj2.Consultar)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)

            Return dt
        End Function
        Public Function Update(obj2 As T_HOTELES_SERVICIOS, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_SERVICIOS_UpdateByID")

            addSqlParameterSimple(cmd, T_HOTELES_SERVICIOS.dbcol.cod_Hotel, obj2.cod_Hotel)
            addSqlParameterSimple(cmd, T_HOTELES_SERVICIOS.dbcol.cod_Servicio, obj2.cod_Servicio)
            addSqlParameterSimple(cmd, T_HOTELES_SERVICIOS.dbcol.NoHabs, obj2.NoHabs)
            addSqlParameterSimple(cmd, T_HOTELES_SERVICIOS.dbcol.Consultar, obj2.Consultar)
            addSqlParameterSimple(cmd, T_HOTELES_SERVICIOS.dbcol.id, obj2.id)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

    End Class

End Namespace
