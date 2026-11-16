
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
Public Class T_HOTELES_SERVICIOS
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


    private mcod_Hotel as System.Int32
  <DBColumnName("cod_Hotel")>
  <DataMemberAttribute()>
   Public Overridable Property cod_Hotel() As System.Int32 
    Get 
      return  mcod_Hotel
    End Get 
    Set(value As System.Int32) 
      IF mcod_Hotel<>value THEN 
        mcod_Hotel=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_Servicio as System.Int32
  <DBColumnName("cod_Servicio")>
  <DataMemberAttribute()>
   Public Overridable Property cod_Servicio() As System.Int32 
    Get 
      return  mcod_Servicio
    End Get 
    Set(value As System.Int32) 
      IF mcod_Servicio<>value THEN 
        mcod_Servicio=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mNoHabs as System.Boolean
  <DBColumnName("NoHabs")>
  <DataMemberAttribute()>
   Public Overridable Property NoHabs() As System.Boolean 
    Get 
      return  mNoHabs
    End Get 
    Set(value As System.Boolean) 
      IF mNoHabs<>value THEN 
        mNoHabs=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mConsultar as System.Boolean
  <DBColumnName("Consultar")>
  <DataMemberAttribute()>
   Public Overridable Property Consultar() As System.Boolean 
    Get 
      return  mConsultar
    End Get 
    Set(value As System.Boolean) 
      IF mConsultar<>value THEN 
        mConsultar=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


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

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const cod_Hotel As String="cod_Hotel" 'int
  Public Const cod_Servicio As String="cod_Servicio" 'int
  Public Const NoHabs As String="NoHabs" 'bit
  Public Const Consultar As String="Consultar" 'bit
  Public Const id As String="id" 'int
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_HOTELES_SERVICIOS) as T_HOTELES_SERVICIOS
  If obj2Fill Is Nothing Then
      obj2Fill = New T_HOTELES_SERVICIOS
  End If

  obj2Fill.cod_Hotel =IIf(IsDBNull(dr(DBCol.cod_Hotel)), Nothing,dr(DBCol.cod_Hotel)) 
  obj2Fill.cod_Servicio =IIf(IsDBNull(dr(DBCol.cod_Servicio)), Nothing,dr(DBCol.cod_Servicio)) 
  obj2Fill.NoHabs =IIf(IsDBNull(dr(DBCol.NoHabs)), Nothing,dr(DBCol.NoHabs)) 
  obj2Fill.Consultar =IIf(IsDBNull(dr(DBCol.Consultar)), Nothing,dr(DBCol.Consultar)) 
  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_HOTELES_SERVICIOSCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_HOTELES_SERVICIOS
     Get
         Return DirectCast(List(index), T_HOTELES_SERVICIOS)
     End Get
     Set(value As T_HOTELES_SERVICIOS)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_HOTELES_SERVICIOS)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_HOTELES_SERVICIOS)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
