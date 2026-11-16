
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
Public Class T_Zonas_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Zonas_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Zonas_GetByID")
            addSqlParameterSimple(cmd, "id",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as T_Zonas, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Zonas_Insert")

addSqlParameterSimple(cmd, T_Zonas.dbcol.Cod_Zona,  obj2.Cod_Zona)
addSqlParameterSimple(cmd, T_Zonas.dbcol.cod_pais,  obj2.cod_pais)
addSqlParameterSimple(cmd, T_Zonas.dbcol.cod_cont,  obj2.cod_cont)
addSqlParameterSimple(cmd, T_Zonas.dbcol.Zona,  obj2.Zona)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id = dt.Rows(0)(0)
            Return dt
        End Function
Public Function Update(obj2 as T_Zonas, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Zonas_UpdateByID")

addSqlParameterSimple(cmd, T_Zonas.dbcol.id,  obj2.id)
addSqlParameterSimple(cmd, T_Zonas.dbcol.Cod_Zona,  obj2.Cod_Zona)
addSqlParameterSimple(cmd, T_Zonas.dbcol.cod_pais,  obj2.cod_pais)
addSqlParameterSimple(cmd, T_Zonas.dbcol.cod_cont,  obj2.cod_cont)
addSqlParameterSimple(cmd, T_Zonas.dbcol.Zona,  IIf(IsNothing(obj2.Zona),"",obj2.Zona ))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Zonas_DeleteByID")

addSqlParameterSimple(cmd, T_Zonas.dbcol.id,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
