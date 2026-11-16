
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
Public Class R_Res_cupos_Manager
      inherits ind.db.DBHelper



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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Res_cupos_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Res_cupos_GetByID")
            addSqlParameterSimple(cmd, "ID",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
        Public Function insert(obj2 As R_Res_cupos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Res_cupos_Insert")

            addSqlParameterSimple(cmd, R_Res_cupos.DBCol.cod_operador, obj2.cod_operador)
            addSqlParameterSimple(cmd, R_Res_cupos.DBCol.id_Disp_Hab, obj2.id_Disp_Hab)
            addSqlParameterSimple(cmd, R_Res_cupos.DBCol.disponible, obj2.disponible)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.ID = dt.Rows(0)(0)
            Return dt
        End Function

        Public Function Update(obj2 as R_Res_cupos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Res_cupos_UpdateByID")

            addSqlParameterSimple(cmd, R_Res_cupos.DBCol.ID, obj2.ID)
            addSqlParameterSimple(cmd, R_Res_cupos.DBCol.cod_operador, obj2.cod_operador)
            addSqlParameterSimple(cmd, R_Res_cupos.DBCol.id_Disp_Hab, obj2.id_Disp_Hab)
            addSqlParameterSimple(cmd, R_Res_cupos.DBCol.disponible, obj2.disponible)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Res_cupos_DeleteByID")

addSqlParameterSimple(cmd, R_Res_cupos.dbcol.ID,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
