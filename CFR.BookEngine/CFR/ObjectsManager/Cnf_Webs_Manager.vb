
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
    Public Class Cnf_Webs_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Cnf_Webs_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Cnf_Webs_GetByID")
            addSqlParameterSimple(cmd, "idWeb", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As Cnf_Webs, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Cnf_Webs_Insert")

            addSqlParameterSimple(cmd, Cnf_Webs.dbcol.Web, obj2.Web)
            addSqlParameterSimple(cmd, Cnf_Webs.dbcol.IP, obj2.IP)
            addSqlParameterSimple(cmd, Cnf_Webs.dbcol.ArchivoCSS, obj2.ArchivoCSS)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.idWeb = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As Cnf_Webs, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Cnf_Webs_UpdateByID")

            addSqlParameterSimple(cmd, Cnf_Webs.dbcol.Web, IIf(IsNothing(obj2.Web), "", obj2.Web))
            addSqlParameterSimple(cmd, Cnf_Webs.dbcol.IP, IIf(IsNothing(obj2.IP), "", obj2.IP))
            addSqlParameterSimple(cmd, Cnf_Webs.dbcol.ArchivoCSS, IIf(IsNothing(obj2.ArchivoCSS), "", obj2.ArchivoCSS))
            addSqlParameterSimple(cmd, Cnf_Webs.dbcol.idWeb, obj2.idWeb)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Cnf_Webs_DeleteByID")

            addSqlParameterSimple(cmd, Cnf_Webs.dbcol.idWeb, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
