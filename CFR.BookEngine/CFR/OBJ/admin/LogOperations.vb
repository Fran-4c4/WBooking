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
    Public Class LogOperationsMgr
        Inherits ind.db.DBHelper
        ''' <summary>
        ''' constants for common actions
        ''' </summary>
        ''' <remarks></remarks>
        Public Class actionCte
            Public Const insert As String = "insert"
            Public Const delete As String = "delete"
            Public Const update As String = "update"
        End Class


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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("sp_LogOperationsGetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByCodhotel(cod_Hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("sp_LogOperationsGetAllByCodHotel")
            addSqlParameterSimple(cmd, "cod_Hotel", cod_Hotel)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Sub insert(userid As Integer, action As String, data As String, Optional ByRef trans As Data.IDbTransaction = Nothing)
            Try
                Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("sp_LogOperationsInsert")
                addSqlParameterSimple(cmd, "userid", userid)
                addSqlParameterSimple(cmd, "action", action)
                addSqlParameterSimple(cmd, "data", data)
                Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)

            Catch ex As Exception
                'logpendent
            End Try

        End Sub

        Public Shared Sub LogOper(userid As Integer, action As String, data As String, Optional ByRef trans As Data.IDbTransaction = Nothing)
            Dim logmgr As New ind.bk.obj.LogOperationsMgr
            logmgr.insert(userid, action, data, trans)
        End Sub

    End Class

End Namespace