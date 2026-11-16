
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
    Public Class Fotos_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Fotos_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Fotos_GetByID")
            addSqlParameterSimple(cmd, "ID", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As Fotos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Fotos_Insert")

            addSqlParameterSimple(cmd, Fotos.dbcol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, Fotos.dbcol.id_Foto, obj2.id_Foto)
            addSqlParameterSimple(cmd, Fotos.dbcol.id_usuario, obj2.id_usuario)
            addSqlParameterSimple(cmd, Fotos.dbcol.FotoFileName, obj2.FotoFileName)
            addSqlParameterSimple(cmd, Fotos.dbcol.Description, obj2.Description)
            addSqlParameterSimple(cmd, Fotos.dbcol.id_fotoType, obj2.id_fotoType)
            addSqlParameterSimple(cmd, Fotos.dbcol.lastUpdate, obj2.lastUpdate)
            addSqlParameterSimple(cmd, Fotos.dbcol.exist, obj2.exist)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.ID = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As Fotos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Fotos_UpdateByID")

            addSqlParameterSimple(cmd, Fotos.dbcol.ID, obj2.ID)
            addSqlParameterSimple(cmd, Fotos.dbcol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, Fotos.dbcol.id_Foto, obj2.id_Foto)
            addSqlParameterSimple(cmd, Fotos.dbcol.id_usuario, obj2.id_usuario)
            addSqlParameterSimple(cmd, Fotos.dbcol.FotoFileName, IIf(IsNothing(obj2.FotoFileName), "", obj2.FotoFileName))
            addSqlParameterSimple(cmd, Fotos.dbcol.Description, IIf(IsNothing(obj2.Description), "", obj2.Description))
            addSqlParameterSimple(cmd, Fotos.dbcol.id_fotoType, obj2.id_fotoType)
            addSqlParameterSimple(cmd, Fotos.dbcol.lastUpdate, IIf(IsNothing(obj2.lastUpdate), "", obj2.lastUpdate))
            addSqlParameterSimple(cmd, Fotos.dbcol.exist, obj2.exist)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Fotos_DeleteByID")

            addSqlParameterSimple(cmd, Fotos.dbcol.ID, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
