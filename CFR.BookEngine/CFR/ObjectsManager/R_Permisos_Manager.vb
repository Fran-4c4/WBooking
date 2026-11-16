
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
Public Class R_Permisos_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Permisos_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Permisos_GetByID")
            addSqlParameterSimple(cmd, "ID",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as R_Permisos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Permisos_Insert")

addSqlParameterSimple(cmd, R_Permisos.dbcol.cod_permiso,  obj2.cod_permiso)
addSqlParameterSimple(cmd, R_Permisos.dbcol.Permiso,  obj2.Permiso)
addSqlParameterSimple(cmd, R_Permisos.dbcol.Categoria,  obj2.Categoria)
addSqlParameterSimple(cmd, R_Permisos.dbcol.orden,  obj2.orden)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.ID = dt.Rows(0)(0)
            Return dt
        End Function
Public Function Update(obj2 as R_Permisos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Permisos_UpdateByID")

addSqlParameterSimple(cmd, R_Permisos.dbcol.ID,  obj2.ID)
addSqlParameterSimple(cmd, R_Permisos.dbcol.cod_permiso,  obj2.cod_permiso)
addSqlParameterSimple(cmd, R_Permisos.dbcol.Permiso,  IIf(IsNothing(obj2.Permiso),"",obj2.Permiso ))
addSqlParameterSimple(cmd, R_Permisos.dbcol.Categoria,  IIf(IsNothing(obj2.Categoria),"",obj2.Categoria ))
addSqlParameterSimple(cmd, R_Permisos.dbcol.orden,  obj2.orden)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Permisos_DeleteByID")

addSqlParameterSimple(cmd, R_Permisos.dbcol.ID,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
