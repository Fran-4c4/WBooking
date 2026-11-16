
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
Public Class T_CIUDADES_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CIUDADES_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CIUDADES_GetByID")
            addSqlParameterSimple(cmd, "id",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as T_CIUDADES, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CIUDADES_Insert")

addSqlParameterSimple(cmd, T_CIUDADES.dbcol.cod_ciudad,  obj2.cod_ciudad)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.cod_pais,  obj2.cod_pais)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.cod_cont,  obj2.cod_cont)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.cod_zona,  obj2.cod_zona)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.ciudad,  obj2.ciudad)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.ciudad1,  obj2.ciudad1)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.ciudad2,  obj2.ciudad2)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.ciudad3,  obj2.ciudad3)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.ciudad4,  obj2.ciudad4)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.cod_bookings,  obj2.cod_bookings)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id = dt.Rows(0)(0)
            Return dt
        End Function
Public Function Update(obj2 as T_CIUDADES, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CIUDADES_UpdateByID")

addSqlParameterSimple(cmd, T_CIUDADES.dbcol.id,  obj2.id)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.cod_ciudad,  obj2.cod_ciudad)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.cod_pais,  obj2.cod_pais)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.cod_cont,  obj2.cod_cont)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.cod_zona,  obj2.cod_zona)
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.ciudad,  IIf(IsNothing(obj2.ciudad),"",obj2.ciudad ))
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.ciudad1,  IIf(IsNothing(obj2.ciudad1),"",obj2.ciudad1 ))
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.ciudad2,  IIf(IsNothing(obj2.ciudad2),"",obj2.ciudad2 ))
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.ciudad3,  IIf(IsNothing(obj2.ciudad3),"",obj2.ciudad3 ))
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.ciudad4,  IIf(IsNothing(obj2.ciudad4),"",obj2.ciudad4 ))
addSqlParameterSimple(cmd, T_CIUDADES.dbcol.cod_bookings,  IIf(IsNothing(obj2.cod_bookings),"",obj2.cod_bookings ))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_CIUDADES_DeleteByID")

addSqlParameterSimple(cmd, T_CIUDADES.dbcol.id,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
