
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
Public Class R_Clientes_Facturacion_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Clientes_Facturacion_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Clientes_Facturacion_GetByID")
            addSqlParameterSimple(cmd, "Id_Cliente",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as R_Clientes_Facturacion, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Clientes_Facturacion_Insert")

addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Cliente,  obj2.Cliente)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Direccion,  obj2.Direccion)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.CodigoPostal,  obj2.CodigoPostal)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Localidad,  obj2.Localidad)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Zona,  obj2.Zona)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Pais,  obj2.Pais)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.NumeroFiscal,  obj2.NumeroFiscal)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.EmailFacturacion,  obj2.EmailFacturacion)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.ContactoNombre,  obj2.ContactoNombre)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.ContactoTelefono,  obj2.ContactoTelefono)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.ContactoFax,  obj2.ContactoFax)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.CuentaBancaria,  obj2.CuentaBancaria)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.FormaPago,  obj2.FormaPago)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.fechaAlta,  obj2.fechaAlta)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.fechaBaja,  obj2.fechaBaja)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.FechaModificacion,  obj2.FechaModificacion)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Login,  obj2.Login)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Clave,  obj2.Clave)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.WebCliente,  obj2.WebCliente)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.ContactoCargo,  obj2.ContactoCargo)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Notas,  obj2.Notas)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.ContactoTelefono2,  obj2.ContactoTelefono2)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.ContactoTelefono3,  obj2.ContactoTelefono3)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.blockBook,  obj2.blockBook)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.state,  obj2.state)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.Id_Cliente = dt.Rows(0)(0)
            Return dt
        End Function
Public Function Update(obj2 as R_Clientes_Facturacion, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Clientes_Facturacion_UpdateByID")

addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Id_Cliente,  obj2.Id_Cliente)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Cliente,  IIf(IsNothing(obj2.Cliente),"",obj2.Cliente ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Direccion,  IIf(IsNothing(obj2.Direccion),"",obj2.Direccion ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.CodigoPostal,  IIf(IsNothing(obj2.CodigoPostal),"",obj2.CodigoPostal ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Localidad,  IIf(IsNothing(obj2.Localidad),"",obj2.Localidad ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Zona,  IIf(IsNothing(obj2.Zona),"",obj2.Zona ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Pais,  IIf(IsNothing(obj2.Pais),"",obj2.Pais ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.NumeroFiscal,  IIf(IsNothing(obj2.NumeroFiscal),"",obj2.NumeroFiscal ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.EmailFacturacion,  IIf(IsNothing(obj2.EmailFacturacion),"",obj2.EmailFacturacion ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.ContactoNombre,  IIf(IsNothing(obj2.ContactoNombre),"",obj2.ContactoNombre ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.ContactoTelefono,  IIf(IsNothing(obj2.ContactoTelefono),"",obj2.ContactoTelefono ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.ContactoFax,  IIf(IsNothing(obj2.ContactoFax),"",obj2.ContactoFax ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.CuentaBancaria,  IIf(IsNothing(obj2.CuentaBancaria),"",obj2.CuentaBancaria ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.FormaPago,  obj2.FormaPago)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.fechaAlta,  IIf(IsNothing(obj2.fechaAlta),"",obj2.fechaAlta ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.fechaBaja,  IIf(IsNothing(obj2.fechaBaja),"",obj2.fechaBaja ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.FechaModificacion,  IIf(IsNothing(obj2.FechaModificacion),"",obj2.FechaModificacion ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Login,  IIf(IsNothing(obj2.Login),"",obj2.Login ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Clave,  IIf(IsNothing(obj2.Clave),"",obj2.Clave ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.WebCliente,  IIf(IsNothing(obj2.WebCliente),"",obj2.WebCliente ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.ContactoCargo,  IIf(IsNothing(obj2.ContactoCargo),"",obj2.ContactoCargo ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Notas,  IIf(IsNothing(obj2.Notas),"",obj2.Notas ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.ContactoTelefono2,  IIf(IsNothing(obj2.ContactoTelefono2),"",obj2.ContactoTelefono2 ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.ContactoTelefono3,  IIf(IsNothing(obj2.ContactoTelefono3),"",obj2.ContactoTelefono3 ))
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.blockBook,  obj2.blockBook)
addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.state,  obj2.state)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Clientes_Facturacion_DeleteByID")

addSqlParameterSimple(cmd, R_Clientes_Facturacion.dbcol.Id_Cliente,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
