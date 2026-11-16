
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
Public Class T_HABITACIONES
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


    private mHabitación as System.Int32
  <DBColumnName("Habitación")>
  <DataMemberAttribute()>
   Public Overridable Property Habitación() As System.Int32  Implements IBObase.ID
    Get 
      return  mHabitación
    End Get 
    Set(value As System.Int32) 
      IF mHabitación<>value THEN 
        mHabitación=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescripción as System.String
  <DBColumnName("Descripción")>
  <DataMemberAttribute()>
   Public Overridable Property Descripción() As System.String 
    Get 
      return  mDescripción
    End Get 
    Set(value As System.String) 
      IF mDescripción<>value THEN 
        mDescripción=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mIngles as System.String
  <DBColumnName("Ingles")>
  <DataMemberAttribute()>
   Public Overridable Property Ingles() As System.String 
    Get 
      return  mIngles
    End Get 
    Set(value As System.String) 
      IF mIngles<>value THEN 
        mIngles=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAleman as System.String
  <DBColumnName("Aleman")>
  <DataMemberAttribute()>
   Public Overridable Property Aleman() As System.String 
    Get 
      return  mAleman
    End Get 
    Set(value As System.String) 
      IF mAleman<>value THEN 
        mAleman=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mFrances as System.String
  <DBColumnName("Frances")>
  <DataMemberAttribute()>
   Public Overridable Property Frances() As System.String 
    Get 
      return  mFrances
    End Get 
    Set(value As System.String) 
      IF mFrances<>value THEN 
        mFrances=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCatalan as System.String
  <DBColumnName("Catalan")>
  <DataMemberAttribute()>
   Public Overridable Property Catalan() As System.String 
    Get 
      return  mCatalan
    End Get 
    Set(value As System.String) 
      IF mCatalan<>value THEN 
        mCatalan=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mBuscar as System.Boolean
  <DBColumnName("Buscar")>
  <DataMemberAttribute()>
   Public Overridable Property Buscar() As System.Boolean 
    Get 
      return  mBuscar
    End Get 
    Set(value As System.Boolean) 
      IF mBuscar<>value THEN 
        mBuscar=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mhabitaciongenerica as System.String
  <DBColumnName("habitaciongenerica")>
  <DataMemberAttribute()>
   Public Overridable Property habitaciongenerica() As System.String 
    Get 
      return  mhabitaciongenerica
    End Get 
    Set(value As System.String) 
      IF mhabitaciongenerica<>value THEN 
        mhabitaciongenerica=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mOrden as System.Int16
  <DBColumnName("Orden")>
  <DataMemberAttribute()>
   Public Overridable Property Orden() As System.Int16 
    Get 
      return  mOrden
    End Get 
    Set(value As System.Int16) 
      IF mOrden<>value THEN 
        mOrden=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const Habitación As String="Habitación" 'int
  Public Const Descripción As String="Descripción" 'nvarchar
  Public Const Ingles As String="Ingles" 'nvarchar
  Public Const Aleman As String="Aleman" 'nvarchar
  Public Const Frances As String="Frances" 'nvarchar
  Public Const Catalan As String="Catalan" 'nvarchar
  Public Const Buscar As String="Buscar" 'bit
  Public Const habitaciongenerica As String="habitaciongenerica" 'nvarchar
  Public Const Orden As String="Orden" 'smallint
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_HABITACIONES) as T_HABITACIONES
  If obj2Fill Is Nothing Then
      obj2Fill = New T_HABITACIONES
  End If

  obj2Fill.Habitación =IIf(IsDBNull(dr(DBCol.Habitación)), Nothing,dr(DBCol.Habitación)) 
  obj2Fill.Descripción ="" & dr(DBCol.Descripción)
  obj2Fill.Ingles ="" & dr(DBCol.Ingles)
  obj2Fill.Aleman ="" & dr(DBCol.Aleman)
  obj2Fill.Frances ="" & dr(DBCol.Frances)
  obj2Fill.Catalan ="" & dr(DBCol.Catalan)
  obj2Fill.Buscar =IIf(IsDBNull(dr(DBCol.Buscar)), Nothing,dr(DBCol.Buscar)) 
  obj2Fill.habitaciongenerica ="" & dr(DBCol.habitaciongenerica)
  obj2Fill.Orden =IIf(IsDBNull(dr(DBCol.Orden)), Nothing,dr(DBCol.Orden)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_HABITACIONESCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_HABITACIONES
     Get
         Return DirectCast(List(index), T_HABITACIONES)
     End Get
     Set(value As T_HABITACIONES)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_HABITACIONES)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_HABITACIONES)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
