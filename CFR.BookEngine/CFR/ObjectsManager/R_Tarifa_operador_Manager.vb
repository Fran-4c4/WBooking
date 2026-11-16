
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
Public Class R_Tarifa_operador_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Tarifa_operador_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Tarifa_operador_GetByID")
            addSqlParameterSimple(cmd, "id",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as R_Tarifa_operador, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Tarifa_operador_Insert")

            addSqlParameterSimple(cmd, R_Tarifa_operador.DBCol.cod_tarifa, obj2.cod_tarifa)
            addSqlParameterSimple(cmd, R_Tarifa_operador.DBCol.cod_operador, obj2.cod_operador)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)

            Return dt
        End Function
Public Function Update(obj2 as R_Tarifa_operador, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Tarifa_operador_UpdateByID")

addSqlParameterSimple(cmd, R_Tarifa_operador.dbcol.id,  obj2.id)
addSqlParameterSimple(cmd, R_Tarifa_operador.dbcol.cod_tarifa,  obj2.cod_tarifa)
addSqlParameterSimple(cmd, R_Tarifa_operador.dbcol.cod_operador,  obj2.cod_operador)
addSqlParameterSimple(cmd, R_Tarifa_operador.dbcol.tipo_tarifa,  obj2.tipo_tarifa)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Tarifa_operador_DeleteByID")

            addSqlParameterSimple(cmd, R_Tarifa_operador.DBCol.id, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
