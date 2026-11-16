
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
Public Class T_HOTELES_PAGOS
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

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const cod_hotel As String="cod_hotel" 'int
  Public Const cod_formapago As String="cod_formapago" 'int
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_HOTELES_PAGOS) as T_HOTELES_PAGOS
  If obj2Fill Is Nothing Then
      obj2Fill = New T_HOTELES_PAGOS
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.cod_hotel =IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing,dr(DBCol.cod_hotel)) 
  obj2Fill.cod_formapago =IIf(IsDBNull(dr(DBCol.cod_formapago)), Nothing,dr(DBCol.cod_formapago)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_HOTELES_PAGOSCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_HOTELES_PAGOS
     Get
         Return DirectCast(List(index), T_HOTELES_PAGOS)
     End Get
     Set(value As T_HOTELES_PAGOS)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_HOTELES_PAGOS)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_HOTELES_PAGOS)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
