
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
    Public Class R_Ofertas_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Ofertas_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Ofertas_GetByID")
            addSqlParameterSimple(cmd, "id_oferta", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As R_Ofertas, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Ofertas_Insert")

            addSqlParameterSimple(cmd, R_Ofertas.dbcol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.cod_CRS, obj2.cod_CRS)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.fechainicio, obj2.fechainicio)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.fechafin, obj2.fechafin)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.popup, obj2.popup)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.DescEsp, obj2.DescEsp)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.DescIngles, obj2.DescIngles)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.DescFrances, obj2.DescFrances)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.DescAleman, obj2.DescAleman)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.DescItalian, obj2.DescItalian)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.DescCatalan, obj2.DescCatalan)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id_oferta = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As R_Ofertas, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Ofertas_UpdateByID")

            addSqlParameterSimple(cmd, R_Ofertas.dbcol.id_oferta, obj2.id_oferta)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.cod_CRS, obj2.cod_CRS)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.fechainicio, IIf(IsNothing(obj2.fechainicio), "", obj2.fechainicio))
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.fechafin, IIf(IsNothing(obj2.fechafin), "", obj2.fechafin))
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.popup, obj2.popup)
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.DescEsp, IIf(IsNothing(obj2.DescEsp), "", obj2.DescEsp))
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.DescIngles, IIf(IsNothing(obj2.DescIngles), "", obj2.DescIngles))
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.DescFrances, IIf(IsNothing(obj2.DescFrances), "", obj2.DescFrances))
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.DescAleman, IIf(IsNothing(obj2.DescAleman), "", obj2.DescAleman))
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.DescItalian, IIf(IsNothing(obj2.DescItalian), "", obj2.DescItalian))
            addSqlParameterSimple(cmd, R_Ofertas.dbcol.DescCatalan, IIf(IsNothing(obj2.DescCatalan), "", obj2.DescCatalan))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Ofertas_DeleteByID")

            addSqlParameterSimple(cmd, R_Ofertas.dbcol.id_oferta, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
