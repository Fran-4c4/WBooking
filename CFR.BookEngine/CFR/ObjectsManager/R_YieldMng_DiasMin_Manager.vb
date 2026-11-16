
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
Public Class R_YieldMng_DiasMin_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_YieldMng_DiasMin_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_YieldMng_DiasMin_GetByID")
            addSqlParameterSimple(cmd, "id_YieldMNG",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as R_YieldMng_DiasMin, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_YieldMng_DiasMin_Insert")

addSqlParameterSimple(cmd, R_YieldMng_DiasMin.dbcol.cod_hotel,  obj2.cod_hotel)
addSqlParameterSimple(cmd, R_YieldMng_DiasMin.dbcol.fechainicio,  obj2.fechainicio)
addSqlParameterSimple(cmd, R_YieldMng_DiasMin.dbcol.fechafin,  obj2.fechafin)
addSqlParameterSimple(cmd, R_YieldMng_DiasMin.dbcol.DiasMinimos,  obj2.DiasMinimos)
addSqlParameterSimple(cmd, R_YieldMng_DiasMin.dbcol.cod_operador,  obj2.cod_operador)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id_YieldMNG = dt.Rows(0)(0)
            Return dt
        End Function
Public Function Update(obj2 as R_YieldMng_DiasMin, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_YieldMng_DiasMin_UpdateByID")

addSqlParameterSimple(cmd, R_YieldMng_DiasMin.dbcol.id_YieldMNG,  obj2.id_YieldMNG)
addSqlParameterSimple(cmd, R_YieldMng_DiasMin.dbcol.cod_hotel,  obj2.cod_hotel)
addSqlParameterSimple(cmd, R_YieldMng_DiasMin.dbcol.fechainicio,  IIf(IsNothing(obj2.fechainicio),"",obj2.fechainicio ))
addSqlParameterSimple(cmd, R_YieldMng_DiasMin.dbcol.fechafin,  IIf(IsNothing(obj2.fechafin),"",obj2.fechafin ))
addSqlParameterSimple(cmd, R_YieldMng_DiasMin.dbcol.DiasMinimos,  obj2.DiasMinimos)
addSqlParameterSimple(cmd, R_YieldMng_DiasMin.dbcol.cod_operador,  obj2.cod_operador)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_YieldMng_DiasMin_DeleteByID")

addSqlParameterSimple(cmd, R_YieldMng_DiasMin.dbcol.id_YieldMNG,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
