
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
Public Class T_CATEGORIAS
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


    private mCategoria as System.Int32
  <DBColumnName("Categoria")>
  <DataMemberAttribute()>
   Public Overridable Property Categoria() As System.Int32 
    Get 
      return  mCategoria
    End Get 
    Set(value As System.Int32) 
      IF mCategoria<>value THEN 
        mCategoria=value
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


    private mbookingsClass as System.Int32
  <DBColumnName("bookingsClass")>
  <DataMemberAttribute()>
   Public Overridable Property bookingsClass() As System.Int32 
    Get 
      return  mbookingsClass
    End Get 
    Set(value As System.Int32) 
      IF mbookingsClass<>value THEN 
        mbookingsClass=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const Categoria As String="Categoria" 'int
  Public Const Descripción As String="Descripción" 'nvarchar
  Public Const Ingles As String="Ingles" 'nvarchar
  Public Const Aleman As String="Aleman" 'nvarchar
  Public Const Frances As String="Frances" 'nvarchar
  Public Const Catalan As String="Catalan" 'nvarchar
  Public Const bookingsClass As String="bookingsClass" 'int
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_CATEGORIAS) as T_CATEGORIAS
  If obj2Fill Is Nothing Then
      obj2Fill = New T_CATEGORIAS
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.Categoria =IIf(IsDBNull(dr(DBCol.Categoria)), Nothing,dr(DBCol.Categoria)) 
  obj2Fill.Descripción ="" & dr(DBCol.Descripción)
  obj2Fill.Ingles ="" & dr(DBCol.Ingles)
  obj2Fill.Aleman ="" & dr(DBCol.Aleman)
  obj2Fill.Frances ="" & dr(DBCol.Frances)
  obj2Fill.Catalan ="" & dr(DBCol.Catalan)
  obj2Fill.bookingsClass =IIf(IsDBNull(dr(DBCol.bookingsClass)), Nothing,dr(DBCol.bookingsClass)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_CATEGORIASCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_CATEGORIAS
     Get
         Return DirectCast(List(index), T_CATEGORIAS)
     End Get
     Set(value As T_CATEGORIAS)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_CATEGORIAS)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_CATEGORIAS)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
