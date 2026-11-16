
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
    Public Class T_CADENAS_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CADENAS_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CADENAS_GetByID")
            addSqlParameterSimple(cmd, "ID", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As T_CADENAS, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CADENAS_Insert")

            addSqlParameterSimple(cmd, T_CADENAS.dbcol.cod_Cadena, obj2.cod_Cadena)
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.Cadena, obj2.Cadena)
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.CadenaIngles, obj2.CadenaIngles)
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.CadenaAleman, obj2.CadenaAleman)
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.CadenaFrances, obj2.CadenaFrances)
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.CadenaCatalan, obj2.CadenaCatalan)
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.login, obj2.login)
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.Clave, obj2.Clave)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.ID = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As T_CADENAS, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CADENAS_UpdateByID")

            addSqlParameterSimple(cmd, T_CADENAS.dbcol.ID, obj2.ID)
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.cod_Cadena, obj2.cod_Cadena)
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.Cadena, IIf(IsNothing(obj2.Cadena), "", obj2.Cadena))
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.CadenaIngles, IIf(IsNothing(obj2.CadenaIngles), "", obj2.CadenaIngles))
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.CadenaAleman, IIf(IsNothing(obj2.CadenaAleman), "", obj2.CadenaAleman))
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.CadenaFrances, IIf(IsNothing(obj2.CadenaFrances), "", obj2.CadenaFrances))
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.CadenaCatalan, IIf(IsNothing(obj2.CadenaCatalan), "", obj2.CadenaCatalan))
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.login, IIf(IsNothing(obj2.login), "", obj2.login))
            addSqlParameterSimple(cmd, T_CADENAS.dbcol.Clave, IIf(IsNothing(obj2.Clave), "", obj2.Clave))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CADENAS_DeleteByID")

            addSqlParameterSimple(cmd, T_CADENAS.dbcol.ID, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
