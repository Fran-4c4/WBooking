
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
    Public Class T_HABITACIONES_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HABITACIONES_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HABITACIONES_GetByID")
            addSqlParameterSimple(cmd, "Habitación", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As T_HABITACIONES, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HABITACIONES_Insert")

            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Descripción, obj2.Descripción)
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Ingles, obj2.Ingles)
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Aleman, obj2.Aleman)
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Frances, obj2.Frances)
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Catalan, obj2.Catalan)
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Buscar, obj2.Buscar)
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.habitaciongenerica, obj2.habitaciongenerica)
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Orden, obj2.Orden)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.Habitación = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As T_HABITACIONES, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HABITACIONES_UpdateByID")

            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Habitación, obj2.Habitación)
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Descripción, IIf(IsNothing(obj2.Descripción), "", obj2.Descripción))
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Ingles, IIf(IsNothing(obj2.Ingles), "", obj2.Ingles))
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Aleman, IIf(IsNothing(obj2.Aleman), "", obj2.Aleman))
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Frances, IIf(IsNothing(obj2.Frances), "", obj2.Frances))
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Catalan, IIf(IsNothing(obj2.Catalan), "", obj2.Catalan))
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Buscar, obj2.Buscar)
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.habitaciongenerica, IIf(IsNothing(obj2.habitaciongenerica), "", obj2.habitaciongenerica))
            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Orden, obj2.Orden)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HABITACIONES_DeleteByID")

            addSqlParameterSimple(cmd, T_HABITACIONES.dbcol.Habitación, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
