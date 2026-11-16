
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
Public Class R_YieldMng
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


    private mid_YieldMNG as System.Int32
  <DBColumnName("id_YieldMNG")>
  <DataMemberAttribute()>
   Public Overridable Property id_YieldMNG() As System.Int32  Implements IBObase.ID
    Get 
      return  mid_YieldMNG
    End Get 
    Set(value As System.Int32) 
      IF mid_YieldMNG<>value THEN 
        mid_YieldMNG=value
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


    private mcod_habitacion as System.Int32
  <DBColumnName("cod_habitacion")>
  <DataMemberAttribute()>
   Public Overridable Property cod_habitacion() As System.Int32 
    Get 
      return  mcod_habitacion
    End Get 
    Set(value As System.Int32) 
      IF mcod_habitacion<>value THEN 
        mcod_habitacion=value
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


    private mprecioMinimo as System.Double
  <DBColumnName("precioMinimo")>
  <DataMemberAttribute()>
   Public Overridable Property precioMinimo() As System.Double 
    Get 
      return  mprecioMinimo
    End Get 
    Set(value As System.Double) 
      IF mprecioMinimo<>value THEN 
        mprecioMinimo=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAplicarPrecMinim as System.Byte
  <DBColumnName("AplicarPrecMinim")>
  <DataMemberAttribute()>
   Public Overridable Property AplicarPrecMinim() As System.Byte 
    Get 
      return  mAplicarPrecMinim
    End Get 
    Set(value As System.Byte) 
      IF mAplicarPrecMinim<>value THEN 
        mAplicarPrecMinim=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id_YieldMNG As String="id_YieldMNG" 'int
  Public Const cod_hotel As String="cod_hotel" 'int
  Public Const cod_habitacion As String="cod_habitacion" 'int
  Public Const fechainicio As String="fechainicio" 'smalldatetime
  Public Const fechafin As String="fechafin" 'smalldatetime
  Public Const precioMinimo As String="precioMinimo" 'float
  Public Const AplicarPrecMinim As String="AplicarPrecMinim" 'tinyint
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_YieldMng) as R_YieldMng
  If obj2Fill Is Nothing Then
      obj2Fill = New R_YieldMng
  End If

  obj2Fill.id_YieldMNG =IIf(IsDBNull(dr(DBCol.id_YieldMNG)), Nothing,dr(DBCol.id_YieldMNG)) 
  obj2Fill.cod_hotel =IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing,dr(DBCol.cod_hotel)) 
  obj2Fill.cod_habitacion =IIf(IsDBNull(dr(DBCol.cod_habitacion)), Nothing,dr(DBCol.cod_habitacion)) 
  obj2Fill.fechainicio =IIf(IsDBNull(dr(DBCol.fechainicio)), Nothing,dr(DBCol.fechainicio)) 
  obj2Fill.fechafin =IIf(IsDBNull(dr(DBCol.fechafin)), Nothing,dr(DBCol.fechafin)) 
  obj2Fill.precioMinimo =IIf(IsDBNull(dr(DBCol.precioMinimo)), Nothing,dr(DBCol.precioMinimo)) 
  obj2Fill.AplicarPrecMinim =IIf(IsDBNull(dr(DBCol.AplicarPrecMinim)), Nothing,dr(DBCol.AplicarPrecMinim)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_YieldMngCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_YieldMng
     Get
         Return DirectCast(List(index), R_YieldMng)
     End Get
     Set(value As R_YieldMng)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_YieldMng)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_YieldMng)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
