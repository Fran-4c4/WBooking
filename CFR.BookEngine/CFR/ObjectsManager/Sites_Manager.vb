
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
    Public Class Sites_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Sites_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Sites_GetByID")
            addSqlParameterSimple(cmd, "id_site", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As Sites, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Sites_Insert")

            addSqlParameterSimple(cmd, Sites.dbcol.cod_site, obj2.cod_site)
            addSqlParameterSimple(cmd, Sites.dbcol.CSS, obj2.CSS)
            addSqlParameterSimple(cmd, Sites.dbcol.Descripcion, obj2.Descripcion)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id_site = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As Sites, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Sites_UpdateByID")

            addSqlParameterSimple(cmd, Sites.dbcol.id_site, obj2.id_site)
            addSqlParameterSimple(cmd, Sites.dbcol.cod_site, IIf(IsNothing(obj2.cod_site), "", obj2.cod_site))
            addSqlParameterSimple(cmd, Sites.dbcol.CSS, IIf(IsNothing(obj2.CSS), "", obj2.CSS))
            addSqlParameterSimple(cmd, Sites.dbcol.Descripcion, IIf(IsNothing(obj2.Descripcion), "", obj2.Descripcion))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Sites_DeleteByID")

            addSqlParameterSimple(cmd, Sites.dbcol.id_site, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
