
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
    Public Class R_bookstatehistory_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_bookstatehistory_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_bookstatehistory_GetByID")
            addSqlParameterSimple(cmd, "id", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As R_bookstatehistory, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_bookstatehistory_Insert")

            addSqlParameterSimple(cmd, R_bookstatehistory.dbcol.cod_reserva, obj2.cod_reserva)
            addSqlParameterSimple(cmd, R_bookstatehistory.dbcol.bookstate, obj2.bookstate)
            'addSqlParameterSimple(cmd, R_bookstatehistory.dbcol.modifyDate, obj2.modifyDate)
            addSqlParameterSimple(cmd, R_bookstatehistory.dbcol.username, obj2.username)
            addSqlParameterSimple(cmd, R_bookstatehistory.DBCol.pricereal, obj2.pricereal)
            addSqlParameterSimple(cmd, R_bookstatehistory.DBCol.forUnit, obj2.forunit)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As R_bookstatehistory, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_bookstatehistory_UpdateByID")

            addSqlParameterSimple(cmd, R_bookstatehistory.dbcol.id, obj2.id)
            addSqlParameterSimple(cmd, R_bookstatehistory.dbcol.cod_reserva, obj2.cod_reserva)
            addSqlParameterSimple(cmd, R_bookstatehistory.dbcol.bookstate, obj2.bookstate)
            addSqlParameterSimple(cmd, R_bookstatehistory.dbcol.modifyDate, IIf(IsNothing(obj2.modifyDate), "", obj2.modifyDate))
            addSqlParameterSimple(cmd, R_bookstatehistory.dbcol.username, obj2.username)
            addSqlParameterSimple(cmd, R_bookstatehistory.dbcol.pricereal, obj2.pricereal)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_bookstatehistory_DeleteByID")

            addSqlParameterSimple(cmd, R_bookstatehistory.dbcol.id, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
