
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
    Public Class R_tarifas_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_tarifas_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_tarifas_GetByID")
            addSqlParameterSimple(cmd, "cod_tarifa", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function


        Public Function insert(obj2 As R_tarifas, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_tarifas_Insert")

            addSqlParameterSimple(cmd, R_tarifas.dbcol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.tarifa, obj2.tarifa)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.DiasMinimos, obj2.DiasMinimos)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.ImpuestoTarifa, obj2.ImpuestoTarifa)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.Categoria, obj2.Categoria)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.PrecNetoBruto, obj2.PrecNetoBruto)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.TI, obj2.TI)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.PC, obj2.PC)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.MP, obj2.MP)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.AD, obj2.AD)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.SA, obj2.SA)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.AplicDescACapac, obj2.AplicDescACapac)
            
            addSqlParameterSimple(cmd, R_tarifas.DBCol.TPVOneNight, obj2.TPVOneNight)
            addSqlParameterSimple(cmd, R_tarifas.DBCol.TPVMinimum, obj2.TPVMinimum)

            addSqlParameterSimple(cmd, R_tarifas.DBCol.ModifyCodUsuario, obj2.ModifyCodUsuario)

            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.cod_tarifa = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As R_tarifas, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_tarifas_UpdateByID")

            addSqlParameterSimple(cmd, R_tarifas.dbcol.cod_tarifa, obj2.cod_tarifa)
            'addSqlParameterSimple(cmd, R_tarifas.dbcol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.tarifa, IIf(IsNothing(obj2.tarifa), "", obj2.tarifa))
            addSqlParameterSimple(cmd, R_tarifas.dbcol.DiasMinimos, obj2.DiasMinimos)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.ImpuestoTarifa, obj2.ImpuestoTarifa)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.Categoria, obj2.Categoria)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.PrecNetoBruto, obj2.PrecNetoBruto)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.TI, obj2.TI)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.PC, obj2.PC)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.MP, obj2.MP)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.AD, obj2.AD)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.SA, obj2.SA)
            addSqlParameterSimple(cmd, R_tarifas.dbcol.AplicDescACapac, obj2.AplicDescACapac)
           
            addSqlParameterSimple(cmd, R_tarifas.DBCol.TPVOneNight, obj2.TPVOneNight)
            addSqlParameterSimple(cmd, R_tarifas.DBCol.TPVMinimum, obj2.TPVMinimum)

            addSqlParameterSimple(cmd, R_tarifas.DBCol.ModifyCodUsuario, obj2.ModifyCodUsuario)

            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_tarifas_DeleteByID")

            addSqlParameterSimple(cmd, R_tarifas.dbcol.cod_tarifa, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
