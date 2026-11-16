
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
    Public Class T_SITUACIONES_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SITUACIONES_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SITUACIONES_GetByID")
            addSqlParameterSimple(cmd, "cod_situacion", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As T_SITUACIONES, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SITUACIONES_Insert")

            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.situacion, obj2.situacion)
            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.Ingles, obj2.Ingles)
            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.Aleman, obj2.Aleman)
            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.Frances, obj2.Frances)
            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.Catalan, obj2.Catalan)
            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.Buscar, obj2.Buscar)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.cod_situacion = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As T_SITUACIONES, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SITUACIONES_UpdateByID")

            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.cod_situacion, obj2.cod_situacion)
            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.situacion, IIf(IsNothing(obj2.situacion), "", obj2.situacion))
            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.Ingles, IIf(IsNothing(obj2.Ingles), "", obj2.Ingles))
            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.Aleman, IIf(IsNothing(obj2.Aleman), "", obj2.Aleman))
            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.Frances, IIf(IsNothing(obj2.Frances), "", obj2.Frances))
            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.Catalan, IIf(IsNothing(obj2.Catalan), "", obj2.Catalan))
            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.Buscar, obj2.Buscar)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SITUACIONES_DeleteByID")

            addSqlParameterSimple(cmd, T_SITUACIONES.dbcol.cod_situacion, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
