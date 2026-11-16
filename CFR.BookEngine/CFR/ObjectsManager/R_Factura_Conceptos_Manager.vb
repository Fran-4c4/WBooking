
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
    Public Class R_Factura_Conceptos_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Factura_Conceptos_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Factura_Conceptos_GetByID")
            addSqlParameterSimple(cmd, "id_Concepto", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As R_Factura_Conceptos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Factura_Conceptos_Insert")

            addSqlParameterSimple(cmd, R_Factura_Conceptos.dbcol.id_Factura, obj2.id_Factura)
            addSqlParameterSimple(cmd, R_Factura_Conceptos.dbcol.Concepto, obj2.Concepto)
            addSqlParameterSimple(cmd, R_Factura_Conceptos.dbcol.Importe, obj2.Importe)
            addSqlParameterSimple(cmd, R_Factura_Conceptos.dbcol.Impuestos, obj2.Impuestos)
            addSqlParameterSimple(cmd, R_Factura_Conceptos.dbcol.Quantity, obj2.Quantity)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id_Concepto = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As R_Factura_Conceptos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Factura_Conceptos_UpdateByID")

            addSqlParameterSimple(cmd, R_Factura_Conceptos.dbcol.id_Concepto, obj2.id_Concepto)
            addSqlParameterSimple(cmd, R_Factura_Conceptos.dbcol.id_Factura, obj2.id_Factura)
            addSqlParameterSimple(cmd, R_Factura_Conceptos.dbcol.Concepto, IIf(IsNothing(obj2.Concepto), "", obj2.Concepto))
            addSqlParameterSimple(cmd, R_Factura_Conceptos.dbcol.Importe, obj2.Importe)
            addSqlParameterSimple(cmd, R_Factura_Conceptos.dbcol.Impuestos, obj2.Impuestos)
            addSqlParameterSimple(cmd, R_Factura_Conceptos.dbcol.Quantity, obj2.Quantity)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Factura_Conceptos_DeleteByID")

            addSqlParameterSimple(cmd, R_Factura_Conceptos.dbcol.id_Concepto, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
