
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
Public Class R_ReservasO
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


    private mcod_hotel as System.Int32
  <DBColumnName("cod_hotel")>
  <DataMemberAttribute()>
   Public Overridable Property cod_hotel() As System.Int32 
    Get 
      return  mcod_hotel
    End Get 
    Set(value As System.Int32) 
      IF mcod_hotel<>value THEN 
        mcod_hotel=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private moperador as System.Int32
  <DBColumnName("operador")>
  <DataMemberAttribute()>
   Public Overridable Property operador() As System.Int32 
    Get 
      return  moperador
    End Get 
    Set(value As System.Int32) 
      IF moperador<>value THEN 
        moperador=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_reservaO as System.Int32
  <DBColumnName("cod_reservaO")>
  <DataMemberAttribute()>
   Public Overridable Property cod_reservaO() As System.Int32  Implements IBObase.ID
    Get 
      return  mcod_reservaO
    End Get 
    Set(value As System.Int32) 
      IF mcod_reservaO<>value THEN 
        mcod_reservaO=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mNombre as System.String
  <DBColumnName("Nombre")>
  <DataMemberAttribute()>
   Public Overridable Property Nombre() As System.String 
    Get 
      return  mNombre
    End Get 
    Set(value As System.String) 
      IF mNombre<>value THEN 
        mNombre=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mApellido1 as System.String
  <DBColumnName("Apellido1")>
  <DataMemberAttribute()>
   Public Overridable Property Apellido1() As System.String 
    Get 
      return  mApellido1
    End Get 
    Set(value As System.String) 
      IF mApellido1<>value THEN 
        mApellido1=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mApellido2 as System.String
  <DBColumnName("Apellido2")>
  <DataMemberAttribute()>
   Public Overridable Property Apellido2() As System.String 
    Get 
      return  mApellido2
    End Get 
    Set(value As System.String) 
      IF mApellido2<>value THEN 
        mApellido2=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDNI as System.String
  <DBColumnName("DNI")>
  <DataMemberAttribute()>
   Public Overridable Property DNI() As System.String 
    Get 
      return  mDNI
    End Get 
    Set(value As System.String) 
      IF mDNI<>value THEN 
        mDNI=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtelefono as System.String
  <DBColumnName("telefono")>
  <DataMemberAttribute()>
   Public Overridable Property telefono() As System.String 
    Get 
      return  mtelefono
    End Get 
    Set(value As System.String) 
      IF mtelefono<>value THEN 
        mtelefono=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


        Private mdriverAge As Byte
        <DBColumnName("driverAge")>
        <DataMemberAttribute()>
        Public Overridable Property driverAge() As Byte
            Get
                Return mdriverAge
            End Get
            Set(value As Byte)
                If mdriverAge <> value Then
                    mdriverAge = value
                    Me.objChanged = True
                End If
            End Set
        End Property


    private memail as System.String
  <DBColumnName("email")>
  <DataMemberAttribute()>
   Public Overridable Property email() As System.String 
    Get 
      return  memail
    End Get 
    Set(value As System.String) 
      IF memail<>value THEN 
        memail=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mdireccion as System.String
  <DBColumnName("direccion")>
  <DataMemberAttribute()>
   Public Overridable Property direccion() As System.String 
    Get 
      return  mdireccion
    End Get 
    Set(value As System.String) 
      IF mdireccion<>value THEN 
        mdireccion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_postal as System.String
  <DBColumnName("cod_postal")>
  <DataMemberAttribute()>
   Public Overridable Property cod_postal() As System.String 
    Get 
      return  mcod_postal
    End Get 
    Set(value As System.String) 
      IF mcod_postal<>value THEN 
        mcod_postal=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mciudad as System.String
  <DBColumnName("ciudad")>
  <DataMemberAttribute()>
   Public Overridable Property ciudad() As System.String 
    Get 
      return  mciudad
    End Get 
    Set(value As System.String) 
      IF mciudad<>value THEN 
        mciudad=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mpais as System.String
  <DBColumnName("pais")>
  <DataMemberAttribute()>
   Public Overridable Property pais() As System.String 
    Get 
      return  mpais
    End Get 
    Set(value As System.String) 
      IF mpais<>value THEN 
        mpais=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mhoras1 as System.Byte
  <DBColumnName("horas1")>
  <DataMemberAttribute()>
   Public Overridable Property horas1() As System.Byte 
    Get 
      return  mhoras1
    End Get 
    Set(value As System.Byte) 
      IF mhoras1<>value THEN 
        mhoras1=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mhoras2 as System.Byte
  <DBColumnName("horas2")>
  <DataMemberAttribute()>
   Public Overridable Property horas2() As System.Byte 
    Get 
      return  mhoras2
    End Get 
    Set(value As System.Byte) 
      IF mhoras2<>value THEN 
        mhoras2=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mhoras3 as System.Byte
  <DBColumnName("horas3")>
  <DataMemberAttribute()>
   Public Overridable Property horas3() As System.Byte 
    Get 
      return  mhoras3
    End Get 
    Set(value As System.Byte) 
      IF mhoras3<>value THEN 
        mhoras3=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mhpenaliz1 as System.Byte
  <DBColumnName("hpenaliz1")>
  <DataMemberAttribute()>
   Public Overridable Property hpenaliz1() As System.Byte 
    Get 
      return  mhpenaliz1
    End Get 
    Set(value As System.Byte) 
      IF mhpenaliz1<>value THEN 
        mhpenaliz1=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mhpenaliz2 as System.Byte
  <DBColumnName("hpenaliz2")>
  <DataMemberAttribute()>
   Public Overridable Property hpenaliz2() As System.Byte 
    Get 
      return  mhpenaliz2
    End Get 
    Set(value As System.Byte) 
      IF mhpenaliz2<>value THEN 
        mhpenaliz2=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mhpenaliz3 as System.Byte
  <DBColumnName("hpenaliz3")>
  <DataMemberAttribute()>
   Public Overridable Property hpenaliz3() As System.Byte 
    Get 
      return  mhpenaliz3
    End Get 
    Set(value As System.Byte) 
      IF mhpenaliz3<>value THEN 
        mhpenaliz3=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mmoneda as System.String
  <DBColumnName("moneda")>
  <DataMemberAttribute()>
   Public Overridable Property moneda() As System.String 
    Get 
      return  mmoneda
    End Get 
    Set(value As System.String) 
      IF mmoneda<>value THEN 
        mmoneda=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mimpuestos as System.Double
  <DBColumnName("impuestos")>
  <DataMemberAttribute()>
   Public Overridable Property impuestos() As System.Double 
    Get 
      return  mimpuestos
    End Get 
    Set(value As System.Double) 
      IF mimpuestos<>value THEN 
        mimpuestos=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_formapago as System.Int32
  <DBColumnName("cod_formapago")>
  <DataMemberAttribute()>
   Public Overridable Property cod_formapago() As System.Int32 
    Get 
      return  mcod_formapago
    End Get 
    Set(value As System.Int32) 
      IF mcod_formapago<>value THEN 
        mcod_formapago=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mnTarjeta as System.String
  <DBColumnName("nTarjeta")>
  <DataMemberAttribute()>
   Public Overridable Property nTarjeta() As System.String 
    Get 
      return  mnTarjeta
    End Get 
    Set(value As System.String) 
      IF mnTarjeta<>value THEN 
        mnTarjeta=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mFCaducidad as System.String
  <DBColumnName("FCaducidad")>
  <DataMemberAttribute()>
   Public Overridable Property FCaducidad() As System.String 
    Get 
      return  mFCaducidad
    End Get 
    Set(value As System.String) 
      IF mFCaducidad<>value THEN 
        mFCaducidad=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mTitular as System.String
  <DBColumnName("Titular")>
  <DataMemberAttribute()>
   Public Overridable Property Titular() As System.String 
    Get 
      return  mTitular
    End Get 
    Set(value As System.String) 
      IF mTitular<>value THEN 
        mTitular=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mlogin as System.String
  <DBColumnName("login")>
  <DataMemberAttribute()>
   Public Overridable Property login() As System.String 
    Get 
      return  mlogin
    End Get 
    Set(value As System.String) 
      IF mlogin<>value THEN 
        mlogin=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mclave as System.String
  <DBColumnName("clave")>
  <DataMemberAttribute()>
   Public Overridable Property clave() As System.String 
    Get 
      return  mclave
    End Get 
    Set(value As System.String) 
      IF mclave<>value THEN 
        mclave=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mHoraReserva as System.DateTime
  <DBColumnName("HoraReserva")>
  <DataMemberAttribute()>
   Public Overridable Property HoraReserva() As System.DateTime 
    Get 
      return  mHoraReserva
    End Get 
    Set(value As System.DateTime) 
      IF mHoraReserva<>value THEN 
        mHoraReserva=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_CRS as System.Int32
  <DBColumnName("cod_CRS")>
  <DataMemberAttribute()>
   Public Overridable Property cod_CRS() As System.Int32 
    Get 
      return  mcod_CRS
    End Get 
    Set(value As System.Int32) 
      IF mcod_CRS<>value THEN 
        mcod_CRS=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcodoperadorCRS as System.Double
  <DBColumnName("codoperadorCRS")>
  <DataMemberAttribute()>
   Public Overridable Property codoperadorCRS() As System.Double 
    Get 
      return  mcodoperadorCRS
    End Get 
    Set(value As System.Double) 
      IF mcodoperadorCRS<>value THEN 
        mcodoperadorCRS=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private moperadorCRS as System.String
  <DBColumnName("operadorCRS")>
  <DataMemberAttribute()>
   Public Overridable Property operadorCRS() As System.String 
    Get 
      return  moperadorCRS
    End Get 
    Set(value As System.String) 
      IF moperadorCRS<>value THEN 
        moperadorCRS=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCRS as System.String
  <DBColumnName("CRS")>
  <DataMemberAttribute()>
   Public Overridable Property CRS() As System.String 
    Get 
      return  mCRS
    End Get 
    Set(value As System.String) 
      IF mCRS<>value THEN 
        mCRS=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mSite as System.String
  <DBColumnName("Site")>
  <DataMemberAttribute()>
   Public Overridable Property Site() As System.String 
    Get 
      return  mSite
    End Get 
    Set(value As System.String) 
      IF mSite<>value THEN 
        mSite=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescoperador as System.String
  <DBColumnName("Descoperador")>
  <DataMemberAttribute()>
   Public Overridable Property Descoperador() As System.String 
    Get 
      return  mDescoperador
    End Get 
    Set(value As System.String) 
      IF mDescoperador<>value THEN 
        mDescoperador=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcheckMail as System.Byte
  <DBColumnName("checkMail")>
  <DataMemberAttribute()>
   Public Overridable Property checkMail() As System.Byte 
    Get 
      return  mcheckMail
    End Get 
    Set(value As System.Byte) 
      IF mcheckMail<>value THEN 
        mcheckMail=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcheckHotel as System.Byte
  <DBColumnName("checkHotel")>
  <DataMemberAttribute()>
   Public Overridable Property checkHotel() As System.Byte 
    Get 
      return  mcheckHotel
    End Get 
    Set(value As System.Byte) 
      IF mcheckHotel<>value THEN 
        mcheckHotel=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_Reserva_hotel as System.String
  <DBColumnName("cod_Reserva_hotel")>
  <DataMemberAttribute()>
   Public Overridable Property cod_Reserva_hotel() As System.String 
    Get 
      return  mcod_Reserva_hotel
    End Get 
    Set(value As System.String) 
      IF mcod_Reserva_hotel<>value THEN 
        mcod_Reserva_hotel=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCheckHotelPago as System.Byte
  <DBColumnName("CheckHotelPago")>
  <DataMemberAttribute()>
   Public Overridable Property CheckHotelPago() As System.Byte 
    Get 
      return  mCheckHotelPago
    End Get 
    Set(value As System.Byte) 
      IF mCheckHotelPago<>value THEN 
        mCheckHotelPago=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCheckHotelUser as System.String
  <DBColumnName("CheckHotelUser")>
  <DataMemberAttribute()>
   Public Overridable Property CheckHotelUser() As System.String 
    Get 
      return  mCheckHotelUser
    End Get 
    Set(value As System.String) 
      IF mCheckHotelUser<>value THEN 
        mCheckHotelUser=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAg_Nombre as System.String
  <DBColumnName("Ag_Nombre")>
  <DataMemberAttribute()>
   Public Overridable Property Ag_Nombre() As System.String 
    Get 
      return  mAg_Nombre
    End Get 
    Set(value As System.String) 
      IF mAg_Nombre<>value THEN 
        mAg_Nombre=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAg_Direccion as System.String
  <DBColumnName("Ag_Direccion")>
  <DataMemberAttribute()>
   Public Overridable Property Ag_Direccion() As System.String 
    Get 
      return  mAg_Direccion
    End Get 
    Set(value As System.String) 
      IF mAg_Direccion<>value THEN 
        mAg_Direccion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAg_Cod_postal as System.String
  <DBColumnName("Ag_Cod_postal")>
  <DataMemberAttribute()>
   Public Overridable Property Ag_Cod_postal() As System.String 
    Get 
      return  mAg_Cod_postal
    End Get 
    Set(value As System.String) 
      IF mAg_Cod_postal<>value THEN 
        mAg_Cod_postal=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAg_ciudad as System.String
  <DBColumnName("Ag_ciudad")>
  <DataMemberAttribute()>
   Public Overridable Property Ag_ciudad() As System.String 
    Get 
      return  mAg_ciudad
    End Get 
    Set(value As System.String) 
      IF mAg_ciudad<>value THEN 
        mAg_ciudad=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAg_Provincia as System.String
  <DBColumnName("Ag_Provincia")>
  <DataMemberAttribute()>
   Public Overridable Property Ag_Provincia() As System.String 
    Get 
      return  mAg_Provincia
    End Get 
    Set(value As System.String) 
      IF mAg_Provincia<>value THEN 
        mAg_Provincia=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAg_Pais as System.String
  <DBColumnName("Ag_Pais")>
  <DataMemberAttribute()>
   Public Overridable Property Ag_Pais() As System.String 
    Get 
      return  mAg_Pais
    End Get 
    Set(value As System.String) 
      IF mAg_Pais<>value THEN 
        mAg_Pais=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAg_Telefono as System.String
  <DBColumnName("Ag_Telefono")>
  <DataMemberAttribute()>
   Public Overridable Property Ag_Telefono() As System.String 
    Get 
      return  mAg_Telefono
    End Get 
    Set(value As System.String) 
      IF mAg_Telefono<>value THEN 
        mAg_Telefono=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAg_Fax as System.String
  <DBColumnName("Ag_Fax")>
  <DataMemberAttribute()>
   Public Overridable Property Ag_Fax() As System.String 
    Get 
      return  mAg_Fax
    End Get 
    Set(value As System.String) 
      IF mAg_Fax<>value THEN 
        mAg_Fax=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAg_Grupo as System.String
  <DBColumnName("Ag_Grupo")>
  <DataMemberAttribute()>
   Public Overridable Property Ag_Grupo() As System.String 
    Get 
      return  mAg_Grupo
    End Get 
    Set(value As System.String) 
      IF mAg_Grupo<>value THEN 
        mAg_Grupo=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAg_Agente as System.String
  <DBColumnName("Ag_Agente")>
  <DataMemberAttribute()>
   Public Overridable Property Ag_Agente() As System.String 
    Get 
      return  mAg_Agente
    End Get 
    Set(value As System.String) 
      IF mAg_Agente<>value THEN 
        mAg_Agente=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAg_email as System.String
  <DBColumnName("Ag_email")>
  <DataMemberAttribute()>
   Public Overridable Property Ag_email() As System.String 
    Get 
      return  mAg_email
    End Get 
    Set(value As System.String) 
      IF mAg_email<>value THEN 
        mAg_email=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private moperadorGenerico as System.Byte
  <DBColumnName("operadorGenerico")>
  <DataMemberAttribute()>
   Public Overridable Property operadorGenerico() As System.Byte 
    Get 
      return  moperadorGenerico
    End Get 
    Set(value As System.Byte) 
      IF moperadorGenerico<>value THEN 
        moperadorGenerico=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtipoOperador as System.Int32
  <DBColumnName("tipoOperador")>
  <DataMemberAttribute()>
   Public Overridable Property tipoOperador() As System.Int32 
    Get 
      return  mtipoOperador
    End Get 
    Set(value As System.Int32) 
      IF mtipoOperador<>value THEN 
        mtipoOperador=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mempresa as System.String
  <DBColumnName("empresa")>
  <DataMemberAttribute()>
   Public Overridable Property empresa() As System.String 
    Get 
      return  mempresa
    End Get 
    Set(value As System.String) 
      IF mempresa<>value THEN 
        mempresa=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mprecioReal as System.Double
  <DBColumnName("precioReal")>
  <DataMemberAttribute()>
   Public Overridable Property precioReal() As System.Double 
    Get 
      return  mprecioReal
    End Get 
    Set(value As System.Double) 
      IF mprecioReal<>value THEN 
        mprecioReal=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mempresadireccion as System.String
  <DBColumnName("empresadireccion")>
  <DataMemberAttribute()>
   Public Overridable Property empresadireccion() As System.String 
    Get 
      return  mempresadireccion
    End Get 
    Set(value As System.String) 
      IF mempresadireccion<>value THEN 
        mempresadireccion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mempresatlf as System.String
  <DBColumnName("empresatlf")>
  <DataMemberAttribute()>
   Public Overridable Property empresatlf() As System.String 
    Get 
      return  mempresatlf
    End Get 
    Set(value As System.String) 
      IF mempresatlf<>value THEN 
        mempresatlf=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mempresaemail as System.String
  <DBColumnName("empresaemail")>
  <DataMemberAttribute()>
   Public Overridable Property empresaemail() As System.String 
    Get 
      return  mempresaemail
    End Get 
    Set(value As System.String) 
      IF mempresaemail<>value THEN 
        mempresaemail=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcomentarios as System.String
  <DBColumnName("comentarios")>
  <DataMemberAttribute()>
   Public Overridable Property comentarios() As System.String 
    Get 
      return  mcomentarios
    End Get 
    Set(value As System.String) 
      IF mcomentarios<>value THEN 
        mcomentarios=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mComisionAplicada as System.Double
  <DBColumnName("ComisionAplicada")>
  <DataMemberAttribute()>
   Public Overridable Property ComisionAplicada() As System.Double 
    Get 
      return  mComisionAplicada
    End Get 
    Set(value As System.Double) 
      IF mComisionAplicada<>value THEN 
        mComisionAplicada=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mFechaModPrecioReal as System.DateTime
  <DBColumnName("FechaModPrecioReal")>
  <DataMemberAttribute()>
   Public Overridable Property FechaModPrecioReal() As System.DateTime 
    Get 
      return  mFechaModPrecioReal
    End Get 
    Set(value As System.DateTime) 
      IF mFechaModPrecioReal<>value THEN 
        mFechaModPrecioReal=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCCUTarjeta as System.String
  <DBColumnName("CCUTarjeta")>
  <DataMemberAttribute()>
   Public Overridable Property CCUTarjeta() As System.String 
    Get 
      return  mCCUTarjeta
    End Get 
    Set(value As System.String) 
      IF mCCUTarjeta<>value THEN 
        mCCUTarjeta=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCard_type as System.String
  <DBColumnName("Card_type")>
  <DataMemberAttribute()>
   Public Overridable Property Card_type() As System.String 
    Get 
      return  mCard_type
    End Get 
    Set(value As System.String) 
      IF mCard_type<>value THEN 
        mCard_type=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mSignature as System.String
  <DBColumnName("Signature")>
  <DataMemberAttribute()>
   Public Overridable Property Signature() As System.String 
    Get 
      return  mSignature
    End Get 
    Set(value As System.String) 
      IF mSignature<>value THEN 
        mSignature=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mTPV_order as System.String
  <DBColumnName("TPV_order")>
  <DataMemberAttribute()>
   Public Overridable Property TPV_order() As System.String 
    Get 
      return  mTPV_order
    End Get 
    Set(value As System.String) 
      IF mTPV_order<>value THEN 
        mTPV_order=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mlanguage as System.String
  <DBColumnName("language")>
  <DataMemberAttribute()>
   Public Overridable Property language() As System.String 
    Get 
      return  mlanguage
    End Get 
    Set(value As System.String) 
      IF mlanguage<>value THEN 
        mlanguage=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mnum_factura as System.String
  <DBColumnName("num_factura")>
  <DataMemberAttribute()>
   Public Overridable Property num_factura() As System.String 
    Get 
      return  mnum_factura
    End Get 
    Set(value As System.String) 
      IF mnum_factura<>value THEN 
        mnum_factura=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const cod_hotel As String="cod_hotel" 'int
  Public Const operador As String="operador" 'int
  Public Const cod_reservaO As String="cod_reservaO" 'int
  Public Const Nombre As String="Nombre" 'varchar
  Public Const Apellido1 As String="Apellido1" 'varchar
  Public Const Apellido2 As String="Apellido2" 'varchar
  Public Const DNI As String="DNI" 'varchar
  Public Const telefono As String="telefono" 'varchar
            Public Const driverAge As String = "driverAge" 'tinyint
  Public Const email As String="email" 'varchar
  Public Const direccion As String="direccion" 'varchar
  Public Const cod_postal As String="cod_postal" 'varchar
  Public Const ciudad As String="ciudad" 'varchar
  Public Const pais As String="pais" 'varchar
            Public Const moneda As String = "moneda" 'varchar
  Public Const impuestos As String="impuestos" 'float
  Public Const cod_formapago As String="cod_formapago" 'int
  Public Const nTarjeta As String="nTarjeta" 'varchar
  Public Const FCaducidad As String="FCaducidad" 'varchar
  Public Const Titular As String="Titular" 'varchar
  Public Const login As String="login" 'varchar
  Public Const clave As String="clave" 'varchar
  Public Const HoraReserva As String="HoraReserva" 'datetime
  Public Const cod_CRS As String="cod_CRS" 'int
  Public Const codoperadorCRS As String="codoperadorCRS" 'float
  Public Const operadorCRS As String="operadorCRS" 'varchar
  Public Const CRS As String="CRS" 'varchar
  Public Const Site As String="Site" 'varchar
  Public Const Descoperador As String="Descoperador" 'varchar
  Public Const checkMail As String="checkMail" 'tinyint
  Public Const checkHotel As String="checkHotel" 'tinyint
  Public Const cod_Reserva_hotel As String="cod_Reserva_hotel" 'varchar
  Public Const CheckHotelPago As String="CheckHotelPago" 'tinyint
  Public Const CheckHotelUser As String="CheckHotelUser" 'varchar
  Public Const Ag_Nombre As String="Ag_Nombre" 'varchar
  Public Const Ag_Direccion As String="Ag_Direccion" 'varchar
  Public Const Ag_Cod_postal As String="Ag_Cod_postal" 'varchar
  Public Const Ag_ciudad As String="Ag_ciudad" 'varchar
  Public Const Ag_Provincia As String="Ag_Provincia" 'varchar
  Public Const Ag_Pais As String="Ag_Pais" 'varchar
  Public Const Ag_Telefono As String="Ag_Telefono" 'varchar
  Public Const Ag_Fax As String="Ag_Fax" 'varchar
  Public Const Ag_Grupo As String="Ag_Grupo" 'varchar
  Public Const Ag_Agente As String="Ag_Agente" 'varchar
  Public Const Ag_email As String="Ag_email" 'varchar
  Public Const operadorGenerico As String="operadorGenerico" 'tinyint
  Public Const tipoOperador As String="tipoOperador" 'int
  Public Const empresa As String="empresa" 'varchar
  Public Const precioReal As String="precioReal" 'float
  Public Const empresadireccion As String="empresadireccion" 'varchar
  Public Const empresatlf As String="empresatlf" 'varchar
  Public Const empresaemail As String="empresaemail" 'varchar
  Public Const comentarios As String="comentarios" 'varchar
  Public Const ComisionAplicada As String="ComisionAplicada" 'float
  Public Const FechaModPrecioReal As String="FechaModPrecioReal" 'datetime
  Public Const CCUTarjeta As String="CCUTarjeta" 'varchar
  Public Const Card_type As String="Card_type" 'varchar
  Public Const Signature As String="Signature" 'varchar
  Public Const TPV_order As String="TPV_order" 'varchar
  Public Const language As String="language" 'varchar
  Public Const num_factura As String="num_factura" 'varchar
End Class  
#End Region

        Public Function FillByRow(ByRef dr As DataRow) As R_ReservasO
            Return FillObjectByRow(dr, Me)
        End Function


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_ReservasO) as R_ReservasO
  If obj2Fill Is Nothing Then
      obj2Fill = New R_ReservasO
  End If

  obj2Fill.cod_hotel =IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing,dr(DBCol.cod_hotel)) 
  obj2Fill.operador =IIf(IsDBNull(dr(DBCol.operador)), Nothing,dr(DBCol.operador)) 
  obj2Fill.cod_reservaO =IIf(IsDBNull(dr(DBCol.cod_reservaO)), Nothing,dr(DBCol.cod_reservaO)) 
  obj2Fill.Nombre ="" & dr(DBCol.Nombre)
  obj2Fill.Apellido1 ="" & dr(DBCol.Apellido1)
  obj2Fill.Apellido2 ="" & dr(DBCol.Apellido2)
  obj2Fill.DNI ="" & dr(DBCol.DNI)
  obj2Fill.telefono ="" & dr(DBCol.telefono)
            obj2Fill.driverAge = "0" & dr(DBCol.driverAge)
  obj2Fill.email ="" & dr(DBCol.email)
  obj2Fill.direccion ="" & dr(DBCol.direccion)
  obj2Fill.cod_postal ="" & dr(DBCol.cod_postal)
  obj2Fill.ciudad ="" & dr(DBCol.ciudad)
  obj2Fill.pais ="" & dr(DBCol.pais)
            obj2Fill.moneda = "" & dr(DBCol.moneda)
  obj2Fill.impuestos =IIf(IsDBNull(dr(DBCol.impuestos)), Nothing,dr(DBCol.impuestos)) 
  obj2Fill.cod_formapago =IIf(IsDBNull(dr(DBCol.cod_formapago)), Nothing,dr(DBCol.cod_formapago)) 
  obj2Fill.nTarjeta ="" & dr(DBCol.nTarjeta)
  obj2Fill.FCaducidad ="" & dr(DBCol.FCaducidad)
  obj2Fill.Titular ="" & dr(DBCol.Titular)
  obj2Fill.login ="" & dr(DBCol.login)
  obj2Fill.clave ="" & dr(DBCol.clave)
  obj2Fill.HoraReserva =IIf(IsDBNull(dr(DBCol.HoraReserva)), Nothing,dr(DBCol.HoraReserva)) 
  obj2Fill.cod_CRS =IIf(IsDBNull(dr(DBCol.cod_CRS)), Nothing,dr(DBCol.cod_CRS)) 
  obj2Fill.codoperadorCRS =IIf(IsDBNull(dr(DBCol.codoperadorCRS)), Nothing,dr(DBCol.codoperadorCRS)) 
  obj2Fill.operadorCRS ="" & dr(DBCol.operadorCRS)
  obj2Fill.CRS ="" & dr(DBCol.CRS)
  obj2Fill.Site ="" & dr(DBCol.Site)
  obj2Fill.Descoperador ="" & dr(DBCol.Descoperador)
  obj2Fill.checkMail =IIf(IsDBNull(dr(DBCol.checkMail)), Nothing,dr(DBCol.checkMail)) 
  obj2Fill.checkHotel =IIf(IsDBNull(dr(DBCol.checkHotel)), Nothing,dr(DBCol.checkHotel)) 
  obj2Fill.cod_Reserva_hotel ="" & dr(DBCol.cod_Reserva_hotel)
  obj2Fill.CheckHotelPago =IIf(IsDBNull(dr(DBCol.CheckHotelPago)), Nothing,dr(DBCol.CheckHotelPago)) 
  obj2Fill.CheckHotelUser ="" & dr(DBCol.CheckHotelUser)
  obj2Fill.Ag_Nombre ="" & dr(DBCol.Ag_Nombre)
  obj2Fill.Ag_Direccion ="" & dr(DBCol.Ag_Direccion)
  obj2Fill.Ag_Cod_postal ="" & dr(DBCol.Ag_Cod_postal)
  obj2Fill.Ag_ciudad ="" & dr(DBCol.Ag_ciudad)
  obj2Fill.Ag_Provincia ="" & dr(DBCol.Ag_Provincia)
  obj2Fill.Ag_Pais ="" & dr(DBCol.Ag_Pais)
  obj2Fill.Ag_Telefono ="" & dr(DBCol.Ag_Telefono)
  obj2Fill.Ag_Fax ="" & dr(DBCol.Ag_Fax)
  obj2Fill.Ag_Grupo ="" & dr(DBCol.Ag_Grupo)
  obj2Fill.Ag_Agente ="" & dr(DBCol.Ag_Agente)
  obj2Fill.Ag_email ="" & dr(DBCol.Ag_email)
  obj2Fill.operadorGenerico =IIf(IsDBNull(dr(DBCol.operadorGenerico)), Nothing,dr(DBCol.operadorGenerico)) 
  obj2Fill.tipoOperador =IIf(IsDBNull(dr(DBCol.tipoOperador)), Nothing,dr(DBCol.tipoOperador)) 
  obj2Fill.empresa ="" & dr(DBCol.empresa)
  obj2Fill.precioReal =IIf(IsDBNull(dr(DBCol.precioReal)), Nothing,dr(DBCol.precioReal)) 
  obj2Fill.empresadireccion ="" & dr(DBCol.empresadireccion)
  obj2Fill.empresatlf ="" & dr(DBCol.empresatlf)
  obj2Fill.empresaemail ="" & dr(DBCol.empresaemail)
  obj2Fill.comentarios ="" & dr(DBCol.comentarios)
  obj2Fill.ComisionAplicada =IIf(IsDBNull(dr(DBCol.ComisionAplicada)), Nothing,dr(DBCol.ComisionAplicada)) 
  obj2Fill.FechaModPrecioReal =IIf(IsDBNull(dr(DBCol.FechaModPrecioReal)), Nothing,dr(DBCol.FechaModPrecioReal)) 
  obj2Fill.CCUTarjeta ="" & dr(DBCol.CCUTarjeta)
  obj2Fill.Card_type ="" & dr(DBCol.Card_type)
  obj2Fill.Signature ="" & dr(DBCol.Signature)
  obj2Fill.TPV_order ="" & dr(DBCol.TPV_order)
  obj2Fill.language ="" & dr(DBCol.language)
  obj2Fill.num_factura ="" & dr(DBCol.num_factura)
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_ReservasOCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_ReservasO
     Get
         Return DirectCast(List(index), R_ReservasO)
     End Get
     Set(value As R_ReservasO)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_ReservasO)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_ReservasO)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
