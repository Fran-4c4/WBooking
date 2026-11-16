
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
    Public Class R_Temporadas_Periodos_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Temporadas_Periodos_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Temporadas_Periodos_GetByID")
            addSqlParameterSimple(cmd, "cod_periodo", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As R_Temporadas_Periodos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Temporadas_Periodos_Insert")

            addSqlParameterSimple(cmd, R_Temporadas_Periodos.dbcol.cod_temporada, obj2.cod_temporada)
            addSqlParameterSimple(cmd, R_Temporadas_Periodos.dbcol.fechainicio, obj2.fechainicio)
            addSqlParameterSimple(cmd, R_Temporadas_Periodos.dbcol.fechafin, obj2.fechafin)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.cod_periodo = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As R_Temporadas_Periodos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Temporadas_Periodos_UpdateByID")

            addSqlParameterSimple(cmd, R_Temporadas_Periodos.dbcol.cod_periodo, obj2.cod_periodo)
            addSqlParameterSimple(cmd, R_Temporadas_Periodos.dbcol.cod_temporada, obj2.cod_temporada)
            addSqlParameterSimple(cmd, R_Temporadas_Periodos.dbcol.fechainicio, IIf(IsNothing(obj2.fechainicio), "", obj2.fechainicio))
            addSqlParameterSimple(cmd, R_Temporadas_Periodos.dbcol.fechafin, IIf(IsNothing(obj2.fechafin), "", obj2.fechafin))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Temporadas_Periodos_DeleteByID")

            addSqlParameterSimple(cmd, R_Temporadas_Periodos.dbcol.cod_periodo, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
