
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
Public Class R_Clientes_Hoteles_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Clientes_Hoteles_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Clientes_Hoteles_GetByID")
            addSqlParameterSimple(cmd, "id",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as R_Clientes_Hoteles, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Clientes_Hoteles_Insert")

addSqlParameterSimple(cmd, R_Clientes_Hoteles.dbcol.Id_Cliente,  obj2.Id_Cliente)
addSqlParameterSimple(cmd, R_Clientes_Hoteles.dbcol.cod_hotel,  obj2.cod_hotel)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id = dt.Rows(0)(0)
            Return dt
        End Function
Public Function Update(obj2 as R_Clientes_Hoteles, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Clientes_Hoteles_UpdateByID")

addSqlParameterSimple(cmd, R_Clientes_Hoteles.dbcol.id,  obj2.id)
addSqlParameterSimple(cmd, R_Clientes_Hoteles.dbcol.Id_Cliente,  obj2.Id_Cliente)
addSqlParameterSimple(cmd, R_Clientes_Hoteles.dbcol.cod_hotel,  obj2.cod_hotel)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Clientes_Hoteles_DeleteByID")

addSqlParameterSimple(cmd, R_Clientes_Hoteles.dbcol.id,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
