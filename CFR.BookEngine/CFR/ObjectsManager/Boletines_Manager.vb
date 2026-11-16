
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
    Public Class Boletines_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Boletines_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Boletines_GetByID")
            addSqlParameterSimple(cmd, "id_email", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As Boletines, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Boletines_Insert")

            addSqlParameterSimple(cmd, Boletines.dbcol.email, obj2.email)
            addSqlParameterSimple(cmd, Boletines.dbcol.boletin, obj2.boletin)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id_email = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As Boletines, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Boletines_UpdateByID")

            addSqlParameterSimple(cmd, Boletines.dbcol.id_email, obj2.id_email)
            addSqlParameterSimple(cmd, Boletines.dbcol.email, IIf(IsNothing(obj2.email), "", obj2.email))
            addSqlParameterSimple(cmd, Boletines.dbcol.boletin, obj2.boletin)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Boletines_DeleteByID")

            addSqlParameterSimple(cmd, Boletines.dbcol.id_email, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
