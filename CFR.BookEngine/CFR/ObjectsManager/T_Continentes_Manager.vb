
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
Public Class T_Continentes_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Continentes_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Continentes_GetByID")
            addSqlParameterSimple(cmd, "ID",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as T_Continentes, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Continentes_Insert")

addSqlParameterSimple(cmd, T_Continentes.dbcol.cod_cont,  obj2.cod_cont)
addSqlParameterSimple(cmd, T_Continentes.dbcol.Prefix,  obj2.Prefix)
addSqlParameterSimple(cmd, T_Continentes.dbcol.continente,  obj2.continente)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.ID = dt.Rows(0)(0)
            Return dt
        End Function
Public Function Update(obj2 as T_Continentes, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Continentes_UpdateByID")

addSqlParameterSimple(cmd, T_Continentes.dbcol.ID,  obj2.ID)
addSqlParameterSimple(cmd, T_Continentes.dbcol.cod_cont,  obj2.cod_cont)
addSqlParameterSimple(cmd, T_Continentes.dbcol.Prefix,  IIf(IsNothing(obj2.Prefix),"",obj2.Prefix ))
addSqlParameterSimple(cmd, T_Continentes.dbcol.continente,  IIf(IsNothing(obj2.continente),"",obj2.continente ))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Continentes_DeleteByID")

addSqlParameterSimple(cmd, T_Continentes.dbcol.ID,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
