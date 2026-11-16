
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
    Public Class R_Temporadas_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Temporadas_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Temporadas_GetByID")
            addSqlParameterSimple(cmd, "cod_Temporada", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As R_Temporadas, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Temporadas_Insert")

            addSqlParameterSimple(cmd, R_Temporadas.DBCol.cod_Temporada, obj2.cod_Temporada)
            addSqlParameterSimple(cmd, R_Temporadas.dbcol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_Temporadas.dbcol.Temporada, obj2.Temporada)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.cod_Temporada = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As R_Temporadas, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Temporadas_UpdateByID")

            addSqlParameterSimple(cmd, R_Temporadas.dbcol.cod_Temporada, obj2.cod_Temporada)
            addSqlParameterSimple(cmd, R_Temporadas.dbcol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_Temporadas.dbcol.Temporada, IIf(IsNothing(obj2.Temporada), "", obj2.Temporada))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Temporadas_DeleteByID")

            addSqlParameterSimple(cmd, R_Temporadas.dbcol.cod_Temporada, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
