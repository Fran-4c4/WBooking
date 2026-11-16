
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
Public Class R_Clientes_Facturacion
      inherits objBaseClass
      Implements IBObase



''' <summary>
''' A creatable COM class must have a Public Sub New() 
''' with no parameters, otherwise, the class will not be 
''' registered in the COM registry and cannot be created 
''' via CreateObject.
''' </summary>
Public Sub New()
    MyBase.New()
End Sub

#Region "Properties and Fields"


    private mId_Cliente as System.Int32
  <DBColumnName("Id_Cliente")>
  <DataMemberAttribute()>
   Public Overridable Property Id_Cliente() As System.Int32  Implements IBObase.ID
    Get 
      return  mId_Cliente
    End Get 
    Set(value As System.Int32) 
      IF mId_Cliente<>value THEN 
        mId_Cliente=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCliente as System.String
  <DBColumnName("Cliente")>
  <DataMemberAttribute()>
   Public Overridable Property Cliente() As System.String 
    Get 
      return  mCliente
    End Get 
    Set(value As System.String) 
      IF mCliente<>value THEN 
        mCliente=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDireccion as System.String
  <DBColumnName("Direccion")>
  <DataMemberAttribute()>
   Public Overridable Property Direccion() As System.String 
    Get 
      return  mDireccion
    End Get 
    Set(value As System.String) 
      IF mDireccion<>value THEN 
        mDireccion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCodigoPostal as System.String
  <DBColumnName("CodigoPostal")>
  <DataMemberAttribute()>
   Public Overridable Property CodigoPostal() As System.String 
    Get 
      return  mCodigoPostal
    End Get 
    Set(value As System.String) 
      IF mCodigoPostal<>value THEN 
        mCodigoPostal=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mLocalidad as System.String
  <DBColumnName("Localidad")>
  <DataMemberAttribute()>
   Public Overridable Property Localidad() As System.String 
    Get 
      return  mLocalidad
    End Get 
    Set(value As System.String) 
      IF mLocalidad<>value THEN 
        mLocalidad=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mZona as System.String
  <DBColumnName("Zona")>
  <DataMemberAttribute()>
   Public Overridable Property Zona() As System.String 
    Get 
      return  mZona
    End Get 
    Set(value As System.String) 
      IF mZona<>value THEN 
        mZona=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mPais as System.String
  <DBColumnName("Pais")>
  <DataMemberAttribute()>
   Public Overridable Property Pais() As System.String 
    Get 
      return  mPais
    End Get 
    Set(value As System.String) 
      IF mPais<>value THEN 
        mPais=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mNumeroFiscal as System.String
  <DBColumnName("NumeroFiscal")>
  <DataMemberAttribute()>
   Public Overridable Property NumeroFiscal() As System.String 
    Get 
      return  mNumeroFiscal
    End Get 
    Set(value As System.String) 
      IF mNumeroFiscal<>value THEN 
        mNumeroFiscal=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mEmailFacturacion as System.String
  <DBColumnName("EmailFacturacion")>
  <DataMemberAttribute()>
   Public Overridable Property EmailFacturacion() As System.String 
    Get 
      return  mEmailFacturacion
    End Get 
    Set(value As System.String) 
      IF mEmailFacturacion<>value THEN 
        mEmailFacturacion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mContactoNombre as System.String
  <DBColumnName("ContactoNombre")>
  <DataMemberAttribute()>
   Public Overridable Property ContactoNombre() As System.String 
    Get 
      return  mContactoNombre
    End Get 
    Set(value As System.String) 
      IF mContactoNombre<>value THEN 
        mContactoNombre=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mContactoTelefono as System.String
  <DBColumnName("ContactoTelefono")>
  <DataMemberAttribute()>
   Public Overridable Property ContactoTelefono() As System.String 
    Get 
      return  mContactoTelefono
    End Get 
    Set(value As System.String) 
      IF mContactoTelefono<>value THEN 
        mContactoTelefono=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mContactoFax as System.String
  <DBColumnName("ContactoFax")>
  <DataMemberAttribute()>
   Public Overridable Property ContactoFax() As System.String 
    Get 
      return  mContactoFax
    End Get 
    Set(value As System.String) 
      IF mContactoFax<>value THEN 
        mContactoFax=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCuentaBancaria as System.String
  <DBColumnName("CuentaBancaria")>
  <DataMemberAttribute()>
   Public Overridable Property CuentaBancaria() As System.String 
    Get 
      return  mCuentaBancaria
    End Get 
    Set(value As System.String) 
      IF mCuentaBancaria<>value THEN 
        mCuentaBancaria=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mFormaPago as System.Byte
  <DBColumnName("FormaPago")>
  <DataMemberAttribute()>
   Public Overridable Property FormaPago() As System.Byte 
    Get 
      return  mFormaPago
    End Get 
    Set(value As System.Byte) 
      IF mFormaPago<>value THEN 
        mFormaPago=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mfechaAlta as System.DateTime
  <DBColumnName("fechaAlta")>
  <DataMemberAttribute()>
   Public Overridable Property fechaAlta() As System.DateTime 
    Get 
      return  mfechaAlta
    End Get 
    Set(value As System.DateTime) 
      IF mfechaAlta<>value THEN 
        mfechaAlta=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mfechaBaja as System.DateTime
  <DBColumnName("fechaBaja")>
  <DataMemberAttribute()>
   Public Overridable Property fechaBaja() As System.DateTime 
    Get 
      return  mfechaBaja
    End Get 
    Set(value As System.DateTime) 
      IF mfechaBaja<>value THEN 
        mfechaBaja=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mFechaModificacion as System.DateTime
  <DBColumnName("FechaModificacion")>
  <DataMemberAttribute()>
   Public Overridable Property FechaModificacion() As System.DateTime 
    Get 
      return  mFechaModificacion
    End Get 
    Set(value As System.DateTime) 
      IF mFechaModificacion<>value THEN 
        mFechaModificacion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mLogin as System.String
  <DBColumnName("Login")>
  <DataMemberAttribute()>
   Public Overridable Property Login() As System.String 
    Get 
      return  mLogin
    End Get 
    Set(value As System.String) 
      IF mLogin<>value THEN 
        mLogin=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mClave as System.String
  <DBColumnName("Clave")>
  <DataMemberAttribute()>
   Public Overridable Property Clave() As System.String 
    Get 
      return  mClave
    End Get 
    Set(value As System.String) 
      IF mClave<>value THEN 
        mClave=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mWebCliente as System.String
  <DBColumnName("WebCliente")>
  <DataMemberAttribute()>
   Public Overridable Property WebCliente() As System.String 
    Get 
      return  mWebCliente
    End Get 
    Set(value As System.String) 
      IF mWebCliente<>value THEN 
        mWebCliente=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mContactoCargo as System.String
  <DBColumnName("ContactoCargo")>
  <DataMemberAttribute()>
   Public Overridable Property ContactoCargo() As System.String 
    Get 
      return  mContactoCargo
    End Get 
    Set(value As System.String) 
      IF mContactoCargo<>value THEN 
        mContactoCargo=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mNotas as System.String
  <DBColumnName("Notas")>
  <DataMemberAttribute()>
   Public Overridable Property Notas() As System.String 
    Get 
      return  mNotas
    End Get 
    Set(value As System.String) 
      IF mNotas<>value THEN 
        mNotas=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mContactoTelefono2 as System.String
  <DBColumnName("ContactoTelefono2")>
  <DataMemberAttribute()>
   Public Overridable Property ContactoTelefono2() As System.String 
    Get 
      return  mContactoTelefono2
    End Get 
    Set(value As System.String) 
      IF mContactoTelefono2<>value THEN 
        mContactoTelefono2=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mContactoTelefono3 as System.String
  <DBColumnName("ContactoTelefono3")>
  <DataMemberAttribute()>
   Public Overridable Property ContactoTelefono3() As System.String 
    Get 
      return  mContactoTelefono3
    End Get 
    Set(value As System.String) 
      IF mContactoTelefono3<>value THEN 
        mContactoTelefono3=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mblockBook as System.Byte
  <DBColumnName("blockBook")>
  <DataMemberAttribute()>
   Public Overridable Property blockBook() As System.Byte 
    Get 
      return  mblockBook
    End Get 
    Set(value As System.Byte) 
      IF mblockBook<>value THEN 
        mblockBook=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mstate as System.Byte
  <DBColumnName("state")>
  <DataMemberAttribute()>
   Public Overridable Property state() As System.Byte 
    Get 
      return  mstate
    End Get 
    Set(value As System.Byte) 
      IF mstate<>value THEN 
        mstate=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const Id_Cliente As String="Id_Cliente" 'int
  Public Const Cliente As String="Cliente" 'varchar
  Public Const Direccion As String="Direccion" 'varchar
  Public Const CodigoPostal As String="CodigoPostal" 'varchar
  Public Const Localidad As String="Localidad" 'varchar
  Public Const Zona As String="Zona" 'varchar
  Public Const Pais As String="Pais" 'varchar
  Public Const NumeroFiscal As String="NumeroFiscal" 'varchar
  Public Const EmailFacturacion As String="EmailFacturacion" 'varchar
  Public Const ContactoNombre As String="ContactoNombre" 'varchar
  Public Const ContactoTelefono As String="ContactoTelefono" 'varchar
  Public Const ContactoFax As String="ContactoFax" 'varchar
  Public Const CuentaBancaria As String="CuentaBancaria" 'varchar
  Public Const FormaPago As String="FormaPago" 'tinyint
  Public Const fechaAlta As String="fechaAlta" 'datetime
  Public Const fechaBaja As String="fechaBaja" 'datetime
  Public Const FechaModificacion As String="FechaModificacion" 'datetime
  Public Const Login As String="Login" 'varchar
  Public Const Clave As String="Clave" 'varchar
  Public Const WebCliente As String="WebCliente" 'varchar
  Public Const ContactoCargo As String="ContactoCargo" 'varchar
  Public Const Notas As String="Notas" 'varchar
  Public Const ContactoTelefono2 As String="ContactoTelefono2" 'varchar
  Public Const ContactoTelefono3 As String="ContactoTelefono3" 'varchar
  Public Const blockBook As String="blockBook" 'tinyint
  Public Const state As String="state" 'tinyint
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Clientes_Facturacion) as R_Clientes_Facturacion
  If obj2Fill Is Nothing Then
      obj2Fill = New R_Clientes_Facturacion
  End If

  obj2Fill.Id_Cliente =IIf(IsDBNull(dr(DBCol.Id_Cliente)), Nothing,dr(DBCol.Id_Cliente)) 
  obj2Fill.Cliente ="" & dr(DBCol.Cliente)
  obj2Fill.Direccion ="" & dr(DBCol.Direccion)
  obj2Fill.CodigoPostal ="" & dr(DBCol.CodigoPostal)
  obj2Fill.Localidad ="" & dr(DBCol.Localidad)
  obj2Fill.Zona ="" & dr(DBCol.Zona)
  obj2Fill.Pais ="" & dr(DBCol.Pais)
  obj2Fill.NumeroFiscal ="" & dr(DBCol.NumeroFiscal)
  obj2Fill.EmailFacturacion ="" & dr(DBCol.EmailFacturacion)
  obj2Fill.ContactoNombre ="" & dr(DBCol.ContactoNombre)
  obj2Fill.ContactoTelefono ="" & dr(DBCol.ContactoTelefono)
  obj2Fill.ContactoFax ="" & dr(DBCol.ContactoFax)
  obj2Fill.CuentaBancaria ="" & dr(DBCol.CuentaBancaria)
  obj2Fill.FormaPago =IIf(IsDBNull(dr(DBCol.FormaPago)), Nothing,dr(DBCol.FormaPago)) 
  obj2Fill.fechaAlta =IIf(IsDBNull(dr(DBCol.fechaAlta)), Nothing,dr(DBCol.fechaAlta)) 
  obj2Fill.fechaBaja =IIf(IsDBNull(dr(DBCol.fechaBaja)), Nothing,dr(DBCol.fechaBaja)) 
  obj2Fill.FechaModificacion =IIf(IsDBNull(dr(DBCol.FechaModificacion)), Nothing,dr(DBCol.FechaModificacion)) 
  obj2Fill.Login ="" & dr(DBCol.Login)
  obj2Fill.Clave ="" & dr(DBCol.Clave)
  obj2Fill.WebCliente ="" & dr(DBCol.WebCliente)
  obj2Fill.ContactoCargo ="" & dr(DBCol.ContactoCargo)
  obj2Fill.Notas ="" & dr(DBCol.Notas)
  obj2Fill.ContactoTelefono2 ="" & dr(DBCol.ContactoTelefono2)
  obj2Fill.ContactoTelefono3 ="" & dr(DBCol.ContactoTelefono3)
  obj2Fill.blockBook =IIf(IsDBNull(dr(DBCol.blockBook)), Nothing,dr(DBCol.blockBook)) 
  obj2Fill.state =IIf(IsDBNull(dr(DBCol.state)), Nothing,dr(DBCol.state)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_Clientes_FacturacionCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_Clientes_Facturacion
     Get
         Return DirectCast(List(index), R_Clientes_Facturacion)
     End Get
     Set(value As R_Clientes_Facturacion)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_Clientes_Facturacion)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_Clientes_Facturacion)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
