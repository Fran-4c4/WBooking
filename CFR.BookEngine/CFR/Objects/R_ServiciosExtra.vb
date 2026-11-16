
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
Public Class R_ServiciosExtra
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


    private mid_servicExtra as System.Int32
  <DBColumnName("id_servicExtra")>
  <DataMemberAttribute()>
   Public Overridable Property id_servicExtra() As System.Int32  Implements IBObase.ID
    Get 
      return  mid_servicExtra
    End Get 
    Set(value As System.Int32) 
      IF mid_servicExtra<>value THEN 
        mid_servicExtra=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


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


    private mcod_CRS as System.Int16
  <DBColumnName("cod_CRS")>
  <DataMemberAttribute()>
   Public Overridable Property cod_CRS() As System.Int16 
    Get 
      return  mcod_CRS
    End Get 
    Set(value As System.Int16) 
      IF mcod_CRS<>value THEN 
        mcod_CRS=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mfechainicio as System.DateTime
  <DBColumnName("fechainicio")>
  <DataMemberAttribute()>
   Public Overridable Property fechainicio() As System.DateTime 
    Get 
      return  mfechainicio
    End Get 
    Set(value As System.DateTime) 
      IF mfechainicio<>value THEN 
        mfechainicio=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mfechafin as System.DateTime
  <DBColumnName("fechafin")>
  <DataMemberAttribute()>
   Public Overridable Property fechafin() As System.DateTime 
    Get 
      return  mfechafin
    End Get 
    Set(value As System.DateTime) 
      IF mfechafin<>value THEN 
        mfechafin=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescEsp as System.String
  <DBColumnName("DescEsp")>
  <DataMemberAttribute()>
   Public Overridable Property DescEsp() As System.String 
    Get 
      return  mDescEsp
    End Get 
    Set(value As System.String) 
      IF mDescEsp<>value THEN 
        mDescEsp=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescIngles as System.String
  <DBColumnName("DescIngles")>
  <DataMemberAttribute()>
   Public Overridable Property DescIngles() As System.String 
    Get 
      return  mDescIngles
    End Get 
    Set(value As System.String) 
      IF mDescIngles<>value THEN 
        mDescIngles=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescFrances as System.String
  <DBColumnName("DescFrances")>
  <DataMemberAttribute()>
   Public Overridable Property DescFrances() As System.String 
    Get 
      return  mDescFrances
    End Get 
    Set(value As System.String) 
      IF mDescFrances<>value THEN 
        mDescFrances=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescAleman as System.String
  <DBColumnName("DescAleman")>
  <DataMemberAttribute()>
   Public Overridable Property DescAleman() As System.String 
    Get 
      return  mDescAleman
    End Get 
    Set(value As System.String) 
      IF mDescAleman<>value THEN 
        mDescAleman=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescItalian as System.String
  <DBColumnName("DescItalian")>
  <DataMemberAttribute()>
   Public Overridable Property DescItalian() As System.String 
    Get 
      return  mDescItalian
    End Get 
    Set(value As System.String) 
      IF mDescItalian<>value THEN 
        mDescItalian=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescCatalan as System.String
  <DBColumnName("DescCatalan")>
  <DataMemberAttribute()>
   Public Overridable Property DescCatalan() As System.String 
    Get 
      return  mDescCatalan
    End Get 
    Set(value As System.String) 
      IF mDescCatalan<>value THEN 
        mDescCatalan=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mPrecio as System.Double
  <DBColumnName("Precio")>
  <DataMemberAttribute()>
   Public Overridable Property Precio() As System.Double 
    Get 
      return  mPrecio
    End Get 
    Set(value As System.Double) 
      IF mPrecio<>value THEN 
        mPrecio=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id_servicExtra As String="id_servicExtra" 'int
  Public Const cod_hotel As String="cod_hotel" 'int
  Public Const cod_CRS As String="cod_CRS" 'smallint
  Public Const fechainicio As String="fechainicio" 'smalldatetime
  Public Const fechafin As String="fechafin" 'smalldatetime
  Public Const DescEsp As String="DescEsp" 'varchar
  Public Const DescIngles As String="DescIngles" 'varchar
  Public Const DescFrances As String="DescFrances" 'varchar
  Public Const DescAleman As String="DescAleman" 'varchar
  Public Const DescItalian As String="DescItalian" 'varchar
  Public Const DescCatalan As String="DescCatalan" 'varchar
  Public Const Precio As String="Precio" 'float
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_ServiciosExtra) as R_ServiciosExtra
  If obj2Fill Is Nothing Then
      obj2Fill = New R_ServiciosExtra
  End If

  obj2Fill.id_servicExtra =IIf(IsDBNull(dr(DBCol.id_servicExtra)), Nothing,dr(DBCol.id_servicExtra)) 
  obj2Fill.cod_hotel =IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing,dr(DBCol.cod_hotel)) 
  obj2Fill.cod_CRS =IIf(IsDBNull(dr(DBCol.cod_CRS)), Nothing,dr(DBCol.cod_CRS)) 
  obj2Fill.fechainicio =IIf(IsDBNull(dr(DBCol.fechainicio)), Nothing,dr(DBCol.fechainicio)) 
  obj2Fill.fechafin =IIf(IsDBNull(dr(DBCol.fechafin)), Nothing,dr(DBCol.fechafin)) 
  obj2Fill.DescEsp ="" & dr(DBCol.DescEsp)
  obj2Fill.DescIngles ="" & dr(DBCol.DescIngles)
  obj2Fill.DescFrances ="" & dr(DBCol.DescFrances)
  obj2Fill.DescAleman ="" & dr(DBCol.DescAleman)
  obj2Fill.DescItalian ="" & dr(DBCol.DescItalian)
  obj2Fill.DescCatalan ="" & dr(DBCol.DescCatalan)
  obj2Fill.Precio =IIf(IsDBNull(dr(DBCol.Precio)), Nothing,dr(DBCol.Precio)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_ServiciosExtraCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_ServiciosExtra
     Get
         Return DirectCast(List(index), R_ServiciosExtra)
     End Get
     Set(value As R_ServiciosExtra)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_ServiciosExtra)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_ServiciosExtra)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
