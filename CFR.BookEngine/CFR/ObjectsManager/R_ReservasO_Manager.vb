
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
Public Class R_ReservasO_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ReservasO_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ReservasO_GetByID")
            addSqlParameterSimple(cmd, "cod_reservaO",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as R_ReservasO, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ReservasO_Insert")

addSqlParameterSimple(cmd, R_ReservasO.dbcol.cod_hotel,  obj2.cod_hotel)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.operador,  obj2.operador)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Nombre,  obj2.Nombre)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Apellido1,  obj2.Apellido1)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Apellido2,  obj2.Apellido2)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.DNI,  obj2.DNI)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.telefono,  obj2.telefono)
            addSqlParameterSimple(cmd, R_ReservasO.DBCol.driverAge, obj2.driverAge)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.email,  obj2.email)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.direccion,  obj2.direccion)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.cod_postal,  obj2.cod_postal)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.ciudad,  obj2.ciudad)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.pais,  obj2.pais)

addSqlParameterSimple(cmd, R_ReservasO.dbcol.moneda,  obj2.moneda)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.impuestos,  obj2.impuestos)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.cod_formapago,  obj2.cod_formapago)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.nTarjeta,  obj2.nTarjeta)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.FCaducidad,  obj2.FCaducidad)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Titular,  obj2.Titular)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.login,  obj2.login)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.clave,  obj2.clave)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.HoraReserva,  obj2.HoraReserva)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.cod_CRS,  obj2.cod_CRS)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.codoperadorCRS,  obj2.codoperadorCRS)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.operadorCRS,  obj2.operadorCRS)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.CRS,  obj2.CRS)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Site,  obj2.Site)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Descoperador,  obj2.Descoperador)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.checkMail,  obj2.checkMail)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.checkHotel,  obj2.checkHotel)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.cod_Reserva_hotel,  obj2.cod_Reserva_hotel)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.CheckHotelPago,  obj2.CheckHotelPago)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.CheckHotelUser,  obj2.CheckHotelUser)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Nombre,  obj2.Ag_Nombre)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Direccion,  obj2.Ag_Direccion)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Cod_postal,  obj2.Ag_Cod_postal)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_ciudad,  obj2.Ag_ciudad)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Provincia,  obj2.Ag_Provincia)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Pais,  obj2.Ag_Pais)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Telefono,  obj2.Ag_Telefono)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Fax,  obj2.Ag_Fax)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Grupo,  obj2.Ag_Grupo)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Agente,  obj2.Ag_Agente)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_email,  obj2.Ag_email)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.operadorGenerico,  obj2.operadorGenerico)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.tipoOperador,  obj2.tipoOperador)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.empresa,  obj2.empresa)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.precioReal,  obj2.precioReal)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.empresadireccion,  obj2.empresadireccion)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.empresatlf,  obj2.empresatlf)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.empresaemail,  obj2.empresaemail)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.comentarios,  obj2.comentarios)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.ComisionAplicada,  obj2.ComisionAplicada)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.FechaModPrecioReal,  obj2.FechaModPrecioReal)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.CCUTarjeta,  obj2.CCUTarjeta)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Card_type,  obj2.Card_type)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Signature,  obj2.Signature)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.TPV_order,  obj2.TPV_order)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.language,  obj2.language)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.num_factura,  obj2.num_factura)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.cod_reservaO = dt.Rows(0)(0)
            Return dt
        End Function
Public Function Update(obj2 as R_ReservasO, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ReservasO_UpdateByID")

addSqlParameterSimple(cmd, R_ReservasO.dbcol.cod_hotel,  obj2.cod_hotel)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.operador,  obj2.operador)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.cod_reservaO,  obj2.cod_reservaO)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Nombre,  IIf(IsNothing(obj2.Nombre),"",obj2.Nombre ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Apellido1,  IIf(IsNothing(obj2.Apellido1),"",obj2.Apellido1 ))
            'addSqlParameterSimple(cmd, R_ReservasO.dbcol.Apellido2,  IIf(IsNothing(obj2.Apellido2),"",obj2.Apellido2 ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.DNI,  IIf(IsNothing(obj2.DNI),"",obj2.DNI ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.telefono,  IIf(IsNothing(obj2.telefono),"",obj2.telefono ))
            addSqlParameterSimple(cmd, R_ReservasO.DBCol.driverAge, IIf(IsNothing(obj2.driverAge), "", obj2.driverAge))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.email,  IIf(IsNothing(obj2.email),"",obj2.email ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.direccion,  IIf(IsNothing(obj2.direccion),"",obj2.direccion ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.cod_postal,  IIf(IsNothing(obj2.cod_postal),"",obj2.cod_postal ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.ciudad,  IIf(IsNothing(obj2.ciudad),"",obj2.ciudad ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.pais,  IIf(IsNothing(obj2.pais),"",obj2.pais ))
            addSqlParameterSimple(cmd, R_ReservasO.DBCol.moneda, IIf(IsNothing(obj2.moneda), "", obj2.moneda))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.impuestos,  obj2.impuestos)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.cod_formapago,  obj2.cod_formapago)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.nTarjeta,  IIf(IsNothing(obj2.nTarjeta),"",obj2.nTarjeta ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.FCaducidad,  IIf(IsNothing(obj2.FCaducidad),"",obj2.FCaducidad ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Titular,  IIf(IsNothing(obj2.Titular),"",obj2.Titular ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.login,  IIf(IsNothing(obj2.login),"",obj2.login ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.clave,  IIf(IsNothing(obj2.clave),"",obj2.clave ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.HoraReserva,  IIf(IsNothing(obj2.HoraReserva),"",obj2.HoraReserva ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.cod_CRS,  obj2.cod_CRS)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.codoperadorCRS,  obj2.codoperadorCRS)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.operadorCRS,  IIf(IsNothing(obj2.operadorCRS),"",obj2.operadorCRS ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.CRS,  IIf(IsNothing(obj2.CRS),"",obj2.CRS ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Site,  IIf(IsNothing(obj2.Site),"",obj2.Site ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Descoperador,  IIf(IsNothing(obj2.Descoperador),"",obj2.Descoperador ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.checkMail,  obj2.checkMail)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.checkHotel,  obj2.checkHotel)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.cod_Reserva_hotel,  IIf(IsNothing(obj2.cod_Reserva_hotel),"",obj2.cod_Reserva_hotel ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.CheckHotelPago,  obj2.CheckHotelPago)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.CheckHotelUser,  IIf(IsNothing(obj2.CheckHotelUser),"",obj2.CheckHotelUser ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Nombre,  IIf(IsNothing(obj2.Ag_Nombre),"",obj2.Ag_Nombre ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Direccion,  IIf(IsNothing(obj2.Ag_Direccion),"",obj2.Ag_Direccion ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Cod_postal,  IIf(IsNothing(obj2.Ag_Cod_postal),"",obj2.Ag_Cod_postal ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_ciudad,  IIf(IsNothing(obj2.Ag_ciudad),"",obj2.Ag_ciudad ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Provincia,  IIf(IsNothing(obj2.Ag_Provincia),"",obj2.Ag_Provincia ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Pais,  IIf(IsNothing(obj2.Ag_Pais),"",obj2.Ag_Pais ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Telefono,  IIf(IsNothing(obj2.Ag_Telefono),"",obj2.Ag_Telefono ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Fax,  IIf(IsNothing(obj2.Ag_Fax),"",obj2.Ag_Fax ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Grupo,  IIf(IsNothing(obj2.Ag_Grupo),"",obj2.Ag_Grupo ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_Agente,  IIf(IsNothing(obj2.Ag_Agente),"",obj2.Ag_Agente ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Ag_email,  IIf(IsNothing(obj2.Ag_email),"",obj2.Ag_email ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.operadorGenerico,  obj2.operadorGenerico)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.tipoOperador,  obj2.tipoOperador)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.empresa,  IIf(IsNothing(obj2.empresa),"",obj2.empresa ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.precioReal,  obj2.precioReal)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.empresadireccion,  IIf(IsNothing(obj2.empresadireccion),"",obj2.empresadireccion ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.empresatlf,  IIf(IsNothing(obj2.empresatlf),"",obj2.empresatlf ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.empresaemail,  IIf(IsNothing(obj2.empresaemail),"",obj2.empresaemail ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.comentarios,  IIf(IsNothing(obj2.comentarios),"",obj2.comentarios ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.ComisionAplicada,  obj2.ComisionAplicada)
addSqlParameterSimple(cmd, R_ReservasO.dbcol.FechaModPrecioReal,  IIf(IsNothing(obj2.FechaModPrecioReal),"",obj2.FechaModPrecioReal ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.CCUTarjeta,  IIf(IsNothing(obj2.CCUTarjeta),"",obj2.CCUTarjeta ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Card_type,  IIf(IsNothing(obj2.Card_type),"",obj2.Card_type ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.Signature,  IIf(IsNothing(obj2.Signature),"",obj2.Signature ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.TPV_order,  IIf(IsNothing(obj2.TPV_order),"",obj2.TPV_order ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.language,  IIf(IsNothing(obj2.language),"",obj2.language ))
addSqlParameterSimple(cmd, R_ReservasO.dbcol.num_factura,  IIf(IsNothing(obj2.num_factura),"",obj2.num_factura ))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ReservasO_DeleteByID")

addSqlParameterSimple(cmd, R_ReservasO.dbcol.cod_reservaO,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
