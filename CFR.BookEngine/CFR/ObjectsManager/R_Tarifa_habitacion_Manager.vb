
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
Public Class R_Tarifa_habitacion_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Tarifa_habitacion_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Tarifa_habitacion_GetByID")
            addSqlParameterSimple(cmd, "ID",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as R_Tarifa_habitacion, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Tarifa_habitacion_Insert")

addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.cod_tarifa,  obj2.cod_tarifa)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.cod_habitacion,  obj2.cod_habitacion)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.precio,  obj2.precio)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.precioAD,  obj2.precioAD)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.preciomp,  obj2.preciomp)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.preciopc,  obj2.preciopc)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.DescAdultosExtra1,  obj2.DescAdultosExtra1)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.DescAdultosExtra2,  obj2.DescAdultosExtra2)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.DescNinoPago1,  obj2.DescNinoPago1)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.DescNinoPago2,  obj2.DescNinoPago2)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.desayunonino,  obj2.desayunonino)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.mpnino,  obj2.mpnino)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.pcnino,  obj2.pcnino)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.TodoIncluido,  obj2.TodoIncluido)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.TodoIncluidoNino,  obj2.TodoIncluidoNino)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.AplicDescAReg,  obj2.AplicDescAReg)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.camaExtra2,  obj2.camaExtra2)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.camaExtra1,  obj2.camaExtra1)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.camaExtra1nino,  obj2.camaExtra1nino)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.camaExtra2nino,  obj2.camaExtra2nino)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.SupDiasMinimos,  obj2.SupDiasMinimos)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.SupFinDeSemana,  obj2.SupFinDeSemana)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.ID = dt.Rows(0)(0)
            Return dt
        End Function
Public Function Update(obj2 as R_Tarifa_habitacion, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Tarifa_habitacion_UpdateByID")

addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.ID,  obj2.ID)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.cod_tarifa,  obj2.cod_tarifa)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.cod_habitacion,  obj2.cod_habitacion)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.precio,  obj2.precio)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.precioAD,  obj2.precioAD)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.preciomp,  obj2.preciomp)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.preciopc,  obj2.preciopc)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.DescAdultosExtra1,  obj2.DescAdultosExtra1)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.DescAdultosExtra2,  obj2.DescAdultosExtra2)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.DescNinoPago1,  obj2.DescNinoPago1)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.DescNinoPago2,  obj2.DescNinoPago2)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.desayunonino,  obj2.desayunonino)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.mpnino,  obj2.mpnino)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.pcnino,  obj2.pcnino)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.TodoIncluido,  obj2.TodoIncluido)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.TodoIncluidoNino,  obj2.TodoIncluidoNino)
            addSqlParameterSimple(cmd, R_Tarifa_habitacion.DBCol.AplicDescAReg, obj2.AplicDescAReg)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.camaExtra2,  obj2.camaExtra2)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.camaExtra1,  obj2.camaExtra1)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.camaExtra1nino,  obj2.camaExtra1nino)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.camaExtra2nino,  obj2.camaExtra2nino)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.SupDiasMinimos,  obj2.SupDiasMinimos)
addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.SupFinDeSemana,  obj2.SupFinDeSemana)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Tarifa_habitacion_DeleteByID")

addSqlParameterSimple(cmd, R_Tarifa_habitacion.dbcol.ID,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
