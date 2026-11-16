
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
    Public Class R_Cesta_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Cesta_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Cesta_GetByID")
            addSqlParameterSimple(cmd, "id", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As R_Cesta, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Cesta_Insert")

            addSqlParameterSimple(cmd, R_Cesta.dbcol.sessionID, obj2.sessionID)
            addSqlParameterSimple(cmd, R_Cesta.dbcol.data, obj2.data)
            addSqlParameterSimple(cmd, R_Cesta.dbcol.sessiondate, obj2.sessiondate)
            addSqlParameterSimple(cmd, R_Cesta.dbcol.tpvData, obj2.tpvData)
            addSqlParameterSimple(cmd, R_Cesta.dbcol.Signature, obj2.Signature)
            addSqlParameterSimple(cmd, R_Cesta.dbcol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_Cesta.dbcol.tpvErrorReturned, obj2.tpvErrorReturned)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As R_Cesta, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Cesta_UpdateByID")

            addSqlParameterSimple(cmd, R_Cesta.dbcol.id, obj2.id)
            addSqlParameterSimple(cmd, R_Cesta.dbcol.sessionID, IIf(IsNothing(obj2.sessionID), "", obj2.sessionID))
            addSqlParameterSimple(cmd, R_Cesta.dbcol.data, obj2.data)
            addSqlParameterSimple(cmd, R_Cesta.dbcol.sessiondate, IIf(IsNothing(obj2.sessiondate), "", obj2.sessiondate))
            addSqlParameterSimple(cmd, R_Cesta.dbcol.tpvData, obj2.tpvData)
            addSqlParameterSimple(cmd, R_Cesta.dbcol.Signature, IIf(IsNothing(obj2.Signature), "", obj2.Signature))
            addSqlParameterSimple(cmd, R_Cesta.dbcol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_Cesta.dbcol.tpvErrorReturned, IIf(IsNothing(obj2.tpvErrorReturned), "", obj2.tpvErrorReturned))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Cesta_DeleteByID")

            addSqlParameterSimple(cmd, R_Cesta.dbcol.id, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
