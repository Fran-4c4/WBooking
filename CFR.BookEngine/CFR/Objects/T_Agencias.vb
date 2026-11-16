
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
Public Class T_Agencias
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


    private mid as System.Int32
  <DBColumnName("id")>
  <DataMemberAttribute()>
   Public Overridable Property id() As System.Int32  Implements IBObase.ID
    Get 
      return  mid
    End Get 
    Set(value As System.Int32) 
      IF mid<>value THEN 
        mid=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_agencia as System.Int16
  <DBColumnName("cod_agencia")>
  <DataMemberAttribute()>
   Public Overridable Property cod_agencia() As System.Int16 
    Get 
      return  mcod_agencia
    End Get 
    Set(value As System.Int16) 
      IF mcod_agencia<>value THEN 
        mcod_agencia=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAgencia as System.String
  <DBColumnName("Agencia")>
  <DataMemberAttribute()>
   Public Overridable Property Agencia() As System.String 
    Get 
      return  mAgencia
    End Get 
    Set(value As System.String) 
      IF mAgencia<>value THEN 
        mAgencia=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


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


    private mweb as System.String
  <DBColumnName("web")>
  <DataMemberAttribute()>
   Public Overridable Property web() As System.String 
    Get 
      return  mweb
    End Get 
    Set(value As System.String) 
      IF mweb<>value THEN 
        mweb=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtlf as System.String
  <DBColumnName("tlf")>
  <DataMemberAttribute()>
   Public Overridable Property tlf() As System.String 
    Get 
      return  mtlf
    End Get 
    Set(value As System.String) 
      IF mtlf<>value THEN 
        mtlf=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mfax as System.String
  <DBColumnName("fax")>
  <DataMemberAttribute()>
   Public Overridable Property fax() As System.String 
    Get 
      return  mfax
    End Get 
    Set(value As System.String) 
      IF mfax<>value THEN 
        mfax=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mIATA as System.String
  <DBColumnName("IATA")>
  <DataMemberAttribute()>
   Public Overridable Property IATA() As System.String 
    Get 
      return  mIATA
    End Get 
    Set(value As System.String) 
      IF mIATA<>value THEN 
        mIATA=value
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


    private mCod_Postal as System.String
  <DBColumnName("Cod_Postal")>
  <DataMemberAttribute()>
   Public Overridable Property Cod_Postal() As System.String 
    Get 
      return  mCod_Postal
    End Get 
    Set(value As System.String) 
      IF mCod_Postal<>value THEN 
        mCod_Postal=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCIF as System.String
  <DBColumnName("CIF")>
  <DataMemberAttribute()>
   Public Overridable Property CIF() As System.String 
    Get 
      return  mCIF
    End Get 
    Set(value As System.String) 
      IF mCIF<>value THEN 
        mCIF=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_ciudad as System.Int32
  <DBColumnName("cod_ciudad")>
  <DataMemberAttribute()>
   Public Overridable Property cod_ciudad() As System.Int32 
    Get 
      return  mcod_ciudad
    End Get 
    Set(value As System.Int32) 
      IF mcod_ciudad<>value THEN 
        mcod_ciudad=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mVerTodos as System.Boolean
  <DBColumnName("VerTodos")>
  <DataMemberAttribute()>
   Public Overridable Property VerTodos() As System.Boolean 
    Get 
      return  mVerTodos
    End Get 
    Set(value As System.Boolean) 
      IF mVerTodos<>value THEN 
        mVerTodos=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mVerConDatos as System.Boolean
  <DBColumnName("VerConDatos")>
  <DataMemberAttribute()>
   Public Overridable Property VerConDatos() As System.Boolean 
    Get 
      return  mVerConDatos
    End Get 
    Set(value As System.Boolean) 
      IF mVerConDatos<>value THEN 
        mVerConDatos=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mVerConAcuerdo as System.Boolean
  <DBColumnName("VerConAcuerdo")>
  <DataMemberAttribute()>
   Public Overridable Property VerConAcuerdo() As System.Boolean 
    Get 
      return  mVerConAcuerdo
    End Get 
    Set(value As System.Boolean) 
      IF mVerConAcuerdo<>value THEN 
        mVerConAcuerdo=value
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


    private mdirector as System.String
  <DBColumnName("director")>
  <DataMemberAttribute()>
   Public Overridable Property director() As System.String 
    Get 
      return  mdirector
    End Get 
    Set(value As System.String) 
      IF mdirector<>value THEN 
        mdirector=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mdirector_comercial as System.String
  <DBColumnName("director_comercial")>
  <DataMemberAttribute()>
   Public Overridable Property director_comercial() As System.String 
    Get 
      return  mdirector_comercial
    End Get 
    Set(value As System.String) 
      IF mdirector_comercial<>value THEN 
        mdirector_comercial=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mnota_Reserva as System.String
  <DBColumnName("nota_Reserva")>
  <DataMemberAttribute()>
   Public Overridable Property nota_Reserva() As System.String 
    Get 
      return  mnota_Reserva
    End Get 
    Set(value As System.String) 
      IF mnota_Reserva<>value THEN 
        mnota_Reserva=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCod_WEB as System.Int16
  <DBColumnName("Cod_WEB")>
  <DataMemberAttribute()>
   Public Overridable Property Cod_WEB() As System.Int16 
    Get 
      return  mCod_WEB
    End Get 
    Set(value As System.Int16) 
      IF mCod_WEB<>value THEN 
        mCod_WEB=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private memail_reserva as System.String
  <DBColumnName("email_reserva")>
  <DataMemberAttribute()>
   Public Overridable Property email_reserva() As System.String 
    Get 
      return  memail_reserva
    End Get 
    Set(value As System.String) 
      IF memail_reserva<>value THEN 
        memail_reserva=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_agencia_gen as System.Int16
  <DBColumnName("cod_agencia_gen")>
  <DataMemberAttribute()>
   Public Overridable Property cod_agencia_gen() As System.Int16 
    Get 
      return  mcod_agencia_gen
    End Get 
    Set(value As System.Int16) 
      IF mcod_agencia_gen<>value THEN 
        mcod_agencia_gen=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtlf_reservas as System.String
  <DBColumnName("tlf_reservas")>
  <DataMemberAttribute()>
   Public Overridable Property tlf_reservas() As System.String 
    Get 
      return  mtlf_reservas
    End Get 
    Set(value As System.String) 
      IF mtlf_reservas<>value THEN 
        mtlf_reservas=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mnumtcredito as System.String
  <DBColumnName("numtcredito")>
  <DataMemberAttribute()>
   Public Overridable Property numtcredito() As System.String 
    Get 
      return  mnumtcredito
    End Get 
    Set(value As System.String) 
      IF mnumtcredito<>value THEN 
        mnumtcredito=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtituloagencia as System.String
  <DBColumnName("tituloagencia")>
  <DataMemberAttribute()>
   Public Overridable Property tituloagencia() As System.String 
    Get 
      return  mtituloagencia
    End Get 
    Set(value As System.String) 
      IF mtituloagencia<>value THEN 
        mtituloagencia=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const cod_agencia As String="cod_agencia" 'smallint
  Public Const Agencia As String="Agencia" 'varchar
  Public Const email As String="email" 'varchar
  Public Const web As String="web" 'varchar
  Public Const tlf As String="tlf" 'varchar
  Public Const fax As String="fax" 'varchar
  Public Const IATA As String="IATA" 'varchar
  Public Const Direccion As String="Direccion" 'varchar
  Public Const Cod_Postal As String="Cod_Postal" 'varchar
  Public Const CIF As String="CIF" 'varchar
  Public Const cod_ciudad As String="cod_ciudad" 'int
  Public Const VerTodos As String="VerTodos" 'bit
  Public Const VerConDatos As String="VerConDatos" 'bit
  Public Const VerConAcuerdo As String="VerConAcuerdo" 'bit
  Public Const login As String="login" 'varchar
  Public Const clave As String="clave" 'varchar
  Public Const director As String="director" 'varchar
  Public Const director_comercial As String="director_comercial" 'varchar
  Public Const nota_Reserva As String="nota_Reserva" 'varchar
  Public Const Cod_WEB As String="Cod_WEB" 'smallint
  Public Const email_reserva As String="email_reserva" 'varchar
  Public Const cod_agencia_gen As String="cod_agencia_gen" 'smallint
  Public Const tlf_reservas As String="tlf_reservas" 'varchar
  Public Const numtcredito As String="numtcredito" 'varchar
  Public Const tituloagencia As String="tituloagencia" 'varchar
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_Agencias) as T_Agencias
  If obj2Fill Is Nothing Then
      obj2Fill = New T_Agencias
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.cod_agencia =IIf(IsDBNull(dr(DBCol.cod_agencia)), Nothing,dr(DBCol.cod_agencia)) 
  obj2Fill.Agencia ="" & dr(DBCol.Agencia)
  obj2Fill.email ="" & dr(DBCol.email)
  obj2Fill.web ="" & dr(DBCol.web)
  obj2Fill.tlf ="" & dr(DBCol.tlf)
  obj2Fill.fax ="" & dr(DBCol.fax)
  obj2Fill.IATA ="" & dr(DBCol.IATA)
  obj2Fill.Direccion ="" & dr(DBCol.Direccion)
  obj2Fill.Cod_Postal ="" & dr(DBCol.Cod_Postal)
  obj2Fill.CIF ="" & dr(DBCol.CIF)
  obj2Fill.cod_ciudad =IIf(IsDBNull(dr(DBCol.cod_ciudad)), Nothing,dr(DBCol.cod_ciudad)) 
  obj2Fill.VerTodos =IIf(IsDBNull(dr(DBCol.VerTodos)), Nothing,dr(DBCol.VerTodos)) 
  obj2Fill.VerConDatos =IIf(IsDBNull(dr(DBCol.VerConDatos)), Nothing,dr(DBCol.VerConDatos)) 
  obj2Fill.VerConAcuerdo =IIf(IsDBNull(dr(DBCol.VerConAcuerdo)), Nothing,dr(DBCol.VerConAcuerdo)) 
  obj2Fill.login ="" & dr(DBCol.login)
  obj2Fill.clave ="" & dr(DBCol.clave)
  obj2Fill.director ="" & dr(DBCol.director)
  obj2Fill.director_comercial ="" & dr(DBCol.director_comercial)
  obj2Fill.nota_Reserva ="" & dr(DBCol.nota_Reserva)
  obj2Fill.Cod_WEB =IIf(IsDBNull(dr(DBCol.Cod_WEB)), Nothing,dr(DBCol.Cod_WEB)) 
  obj2Fill.email_reserva ="" & dr(DBCol.email_reserva)
  obj2Fill.cod_agencia_gen =IIf(IsDBNull(dr(DBCol.cod_agencia_gen)), Nothing,dr(DBCol.cod_agencia_gen)) 
  obj2Fill.tlf_reservas ="" & dr(DBCol.tlf_reservas)
  obj2Fill.numtcredito ="" & dr(DBCol.numtcredito)
  obj2Fill.tituloagencia ="" & dr(DBCol.tituloagencia)
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_AgenciasCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_Agencias
     Get
         Return DirectCast(List(index), T_Agencias)
     End Get
     Set(value As T_Agencias)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_Agencias)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_Agencias)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
