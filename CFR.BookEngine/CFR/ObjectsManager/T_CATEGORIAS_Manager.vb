
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
Public Class T_CATEGORIAS_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CATEGORIAS_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CATEGORIAS_GetByID")
            addSqlParameterSimple(cmd, "id",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as T_CATEGORIAS, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CATEGORIAS_Insert")

addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.Categoria,  obj2.Categoria)
addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.Descripción,  obj2.Descripción)
addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.Ingles,  obj2.Ingles)
addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.Aleman,  obj2.Aleman)
addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.Frances,  obj2.Frances)
addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.Catalan,  obj2.Catalan)
addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.bookingsClass,  obj2.bookingsClass)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id = dt.Rows(0)(0)
            Return dt
        End Function
Public Function Update(obj2 as T_CATEGORIAS, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CATEGORIAS_UpdateByID")

addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.id,  obj2.id)
addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.Categoria,  obj2.Categoria)
addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.Descripción,  IIf(IsNothing(obj2.Descripción),"",obj2.Descripción ))
addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.Ingles,  IIf(IsNothing(obj2.Ingles),"",obj2.Ingles ))
addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.Aleman,  IIf(IsNothing(obj2.Aleman),"",obj2.Aleman ))
addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.Frances,  IIf(IsNothing(obj2.Frances),"",obj2.Frances ))
addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.Catalan,  IIf(IsNothing(obj2.Catalan),"",obj2.Catalan ))
addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.bookingsClass,  obj2.bookingsClass)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CATEGORIAS_DeleteByID")

addSqlParameterSimple(cmd, T_CATEGORIAS.dbcol.id,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
