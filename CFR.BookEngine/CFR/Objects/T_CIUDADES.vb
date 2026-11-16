
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
Public Class T_CIUDADES
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


    private mcod_pais as System.Int32
  <DBColumnName("cod_pais")>
  <DataMemberAttribute()>
   Public Overridable Property cod_pais() As System.Int32 
    Get 
      return  mcod_pais
    End Get 
    Set(value As System.Int32) 
      IF mcod_pais<>value THEN 
        mcod_pais=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_cont as System.Int32
  <DBColumnName("cod_cont")>
  <DataMemberAttribute()>
   Public Overridable Property cod_cont() As System.Int32 
    Get 
      return  mcod_cont
    End Get 
    Set(value As System.Int32) 
      IF mcod_cont<>value THEN 
        mcod_cont=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_zona as System.Int32
  <DBColumnName("cod_zona")>
  <DataMemberAttribute()>
   Public Overridable Property cod_zona() As System.Int32 
    Get 
      return  mcod_zona
    End Get 
    Set(value As System.Int32) 
      IF mcod_zona<>value THEN 
        mcod_zona=value
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


    private mciudad1 as System.String
  <DBColumnName("ciudad1")>
  <DataMemberAttribute()>
   Public Overridable Property ciudad1() As System.String 
    Get 
      return  mciudad1
    End Get 
    Set(value As System.String) 
      IF mciudad1<>value THEN 
        mciudad1=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mciudad2 as System.String
  <DBColumnName("ciudad2")>
  <DataMemberAttribute()>
   Public Overridable Property ciudad2() As System.String 
    Get 
      return  mciudad2
    End Get 
    Set(value As System.String) 
      IF mciudad2<>value THEN 
        mciudad2=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mciudad3 as System.String
  <DBColumnName("ciudad3")>
  <DataMemberAttribute()>
   Public Overridable Property ciudad3() As System.String 
    Get 
      return  mciudad3
    End Get 
    Set(value As System.String) 
      IF mciudad3<>value THEN 
        mciudad3=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mciudad4 as System.String
  <DBColumnName("ciudad4")>
  <DataMemberAttribute()>
   Public Overridable Property ciudad4() As System.String 
    Get 
      return  mciudad4
    End Get 
    Set(value As System.String) 
      IF mciudad4<>value THEN 
        mciudad4=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_bookings as System.String
  <DBColumnName("cod_bookings")>
  <DataMemberAttribute()>
   Public Overridable Property cod_bookings() As System.String 
    Get 
      return  mcod_bookings
    End Get 
    Set(value As System.String) 
      IF mcod_bookings<>value THEN 
        mcod_bookings=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const cod_ciudad As String="cod_ciudad" 'int
  Public Const cod_pais As String="cod_pais" 'int
  Public Const cod_cont As String="cod_cont" 'int
  Public Const cod_zona As String="cod_zona" 'int
  Public Const ciudad As String="ciudad" 'nvarchar
  Public Const ciudad1 As String="ciudad1" 'nvarchar
  Public Const ciudad2 As String="ciudad2" 'nvarchar
  Public Const ciudad3 As String="ciudad3" 'nvarchar
  Public Const ciudad4 As String="ciudad4" 'nvarchar
  Public Const cod_bookings As String="cod_bookings" 'varchar
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_CIUDADES) as T_CIUDADES
  If obj2Fill Is Nothing Then
      obj2Fill = New T_CIUDADES
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.cod_ciudad =IIf(IsDBNull(dr(DBCol.cod_ciudad)), Nothing,dr(DBCol.cod_ciudad)) 
  obj2Fill.cod_pais =IIf(IsDBNull(dr(DBCol.cod_pais)), Nothing,dr(DBCol.cod_pais)) 
  obj2Fill.cod_cont =IIf(IsDBNull(dr(DBCol.cod_cont)), Nothing,dr(DBCol.cod_cont)) 
  obj2Fill.cod_zona =IIf(IsDBNull(dr(DBCol.cod_zona)), Nothing,dr(DBCol.cod_zona)) 
  obj2Fill.ciudad ="" & dr(DBCol.ciudad)
  obj2Fill.ciudad1 ="" & dr(DBCol.ciudad1)
  obj2Fill.ciudad2 ="" & dr(DBCol.ciudad2)
  obj2Fill.ciudad3 ="" & dr(DBCol.ciudad3)
  obj2Fill.ciudad4 ="" & dr(DBCol.ciudad4)
  obj2Fill.cod_bookings ="" & dr(DBCol.cod_bookings)
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_CIUDADESCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_CIUDADES
     Get
         Return DirectCast(List(index), T_CIUDADES)
     End Get
     Set(value As T_CIUDADES)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_CIUDADES)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_CIUDADES)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
