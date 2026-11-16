
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
Public Class R_operadores_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_operadores_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_operadores_GetByID")
            addSqlParameterSimple(cmd, "cod_operador",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as R_operadores, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_operadores_Insert")

            addSqlParameterSimple(cmd, R_operadores.DBCol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_operadores.DBCol.Operador, obj2.Operador)
            addSqlParameterSimple(cmd, R_operadores.DBCol.FreeSale, obj2.FreeSale)
            addSqlParameterSimple(cmd, R_operadores.DBCol.comision, obj2.comision)

            addSqlParameterSimple(cmd, R_operadores.DBCol.discount, obj2.discount)
            addSqlParameterSimple(cmd, R_operadores.DBCol.discountapply, obj2.discountapply)
            addSqlParameterSimple(cmd, R_operadores.DBCol.discountapplyFrom, obj2.discountapplyfrom)
            addSqlParameterSimple(cmd, R_operadores.DBCol.discountapplyuntil, obj2.discountapplyuntil)

            addSqlParameterSimple(cmd, R_operadores.DBCol.forma_pago, obj2.forma_pago)
            addSqlParameterSimple(cmd, R_operadores.DBCol.politica, obj2.politica)
            addSqlParameterSimple(cmd, R_operadores.DBCol.Release, obj2.Release)
            addSqlParameterSimple(cmd, R_operadores.DBCol.login, obj2.login)
            addSqlParameterSimple(cmd, R_operadores.DBCol.Clave, obj2.Clave)
            addSqlParameterSimple(cmd, R_operadores.DBCol.Email, obj2.Email)
            addSqlParameterSimple(cmd, R_operadores.DBCol.N_T_Credito, obj2.N_T_Credito)
            addSqlParameterSimple(cmd, R_operadores.DBCol.CaducidadTarjeta, obj2.CaducidadTarjeta)
            addSqlParameterSimple(cmd, R_operadores.DBCol.TitularTarjeta, obj2.TitularTarjeta)
            addSqlParameterSimple(cmd, R_operadores.DBCol.cod_CRS, obj2.cod_CRS)
            addSqlParameterSimple(cmd, R_operadores.DBCol.polIngles, obj2.polIngles)
            addSqlParameterSimple(cmd, R_operadores.DBCol.polFrances, obj2.polFrances)
            addSqlParameterSimple(cmd, R_operadores.DBCol.polAleman, obj2.polAleman)
            addSqlParameterSimple(cmd, R_operadores.DBCol.polItaliano, obj2.polItaliano)
            addSqlParameterSimple(cmd, R_operadores.DBCol.polCatalan, obj2.polCatalan)
            addSqlParameterSimple(cmd, R_operadores.DBCol.tipo, obj2.tipo)
            addSqlParameterSimple(cmd, R_operadores.DBCol.COD_OPERADOR_hotel, obj2.COD_OPERADOR_hotel)
            addSqlParameterSimple(cmd, R_operadores.DBCol.COD_Agencia_Grupo, obj2.COD_Agencia_Grupo)
            addSqlParameterSimple(cmd, R_operadores.DBCol.cod_Cadena, obj2.cod_Cadena)
            addSqlParameterSimple(cmd, R_operadores.DBCol.generico, obj2.generico)
            addSqlParameterSimple(cmd, R_operadores.DBCol.maxdiasdereserva, obj2.maxdiasdereserva)
            addSqlParameterSimple(cmd, R_operadores.DBCol.aplicarYMprecio, obj2.aplicarYMprecio)
            addSqlParameterSimple(cmd, R_operadores.DBCol.aplicarYMdiasmin, obj2.aplicarYMdiasmin)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.cod_operador = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As R_operadores, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_operadores_UpdateByID")

            addSqlParameterSimple(cmd, R_operadores.dbcol.cod_operador, obj2.cod_operador)
            addSqlParameterSimple(cmd, R_operadores.dbcol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_operadores.dbcol.Operador, IIf(IsNothing(obj2.Operador), "", obj2.Operador))
            addSqlParameterSimple(cmd, R_operadores.dbcol.FreeSale, obj2.FreeSale)
            addSqlParameterSimple(cmd, R_operadores.DBCol.comision, obj2.comision)

            addSqlParameterSimple(cmd, R_operadores.DBCol.discount, obj2.discount)
            addSqlParameterSimple(cmd, R_operadores.DBCol.discountapply, obj2.discountapply)
            addSqlParameterSimple(cmd, R_operadores.DBCol.discountapplyFrom, obj2.discountapplyfrom)
            addSqlParameterSimple(cmd, R_operadores.DBCol.discountapplyuntil, obj2.discountapplyuntil)

            addSqlParameterSimple(cmd, R_operadores.dbcol.forma_pago, obj2.forma_pago)
            addSqlParameterSimple(cmd, R_operadores.dbcol.politica, IIf(IsNothing(obj2.politica), "", obj2.politica))
            addSqlParameterSimple(cmd, R_operadores.dbcol.Release, obj2.Release)
            addSqlParameterSimple(cmd, R_operadores.dbcol.login, IIf(IsNothing(obj2.login), "", obj2.login))
            addSqlParameterSimple(cmd, R_operadores.dbcol.Clave, IIf(IsNothing(obj2.Clave), "", obj2.Clave))
            addSqlParameterSimple(cmd, R_operadores.dbcol.Email, IIf(IsNothing(obj2.Email), "", obj2.Email))
            addSqlParameterSimple(cmd, R_operadores.dbcol.N_T_Credito, IIf(IsNothing(obj2.N_T_Credito), "", obj2.N_T_Credito))
            addSqlParameterSimple(cmd, R_operadores.dbcol.CaducidadTarjeta, IIf(IsNothing(obj2.CaducidadTarjeta), "", obj2.CaducidadTarjeta))
            addSqlParameterSimple(cmd, R_operadores.dbcol.TitularTarjeta, IIf(IsNothing(obj2.TitularTarjeta), "", obj2.TitularTarjeta))
            addSqlParameterSimple(cmd, R_operadores.dbcol.cod_CRS, obj2.cod_CRS)
            addSqlParameterSimple(cmd, R_operadores.dbcol.polIngles, IIf(IsNothing(obj2.polIngles), "", obj2.polIngles))
            addSqlParameterSimple(cmd, R_operadores.dbcol.polFrances, IIf(IsNothing(obj2.polFrances), "", obj2.polFrances))
            addSqlParameterSimple(cmd, R_operadores.dbcol.polAleman, IIf(IsNothing(obj2.polAleman), "", obj2.polAleman))
            addSqlParameterSimple(cmd, R_operadores.dbcol.polItaliano, IIf(IsNothing(obj2.polItaliano), "", obj2.polItaliano))
            addSqlParameterSimple(cmd, R_operadores.dbcol.polCatalan, IIf(IsNothing(obj2.polCatalan), "", obj2.polCatalan))
            addSqlParameterSimple(cmd, R_operadores.dbcol.tipo, obj2.tipo)
            addSqlParameterSimple(cmd, R_operadores.dbcol.COD_OPERADOR_hotel, IIf(IsNothing(obj2.COD_OPERADOR_hotel), "", obj2.COD_OPERADOR_hotel))
            addSqlParameterSimple(cmd, R_operadores.dbcol.COD_Agencia_Grupo, obj2.COD_Agencia_Grupo)
            addSqlParameterSimple(cmd, R_operadores.dbcol.cod_Cadena, obj2.cod_Cadena)
            addSqlParameterSimple(cmd, R_operadores.dbcol.generico, obj2.generico)
            addSqlParameterSimple(cmd, R_operadores.dbcol.maxdiasdereserva, obj2.maxdiasdereserva)
            addSqlParameterSimple(cmd, R_operadores.dbcol.aplicarYMprecio, obj2.aplicarYMprecio)
            addSqlParameterSimple(cmd, R_operadores.dbcol.aplicarYMdiasmin, obj2.aplicarYMdiasmin)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_operadores_DeleteByID")

addSqlParameterSimple(cmd, R_operadores.dbcol.cod_operador,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
