
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
Public Class T_zona_ciudad
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

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const cod_ciudad As String="cod_ciudad" 'int
  Public Const cod_zona As String="cod_zona" 'int
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_zona_ciudad) as T_zona_ciudad
  If obj2Fill Is Nothing Then
      obj2Fill = New T_zona_ciudad
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.cod_ciudad =IIf(IsDBNull(dr(DBCol.cod_ciudad)), Nothing,dr(DBCol.cod_ciudad)) 
  obj2Fill.cod_zona =IIf(IsDBNull(dr(DBCol.cod_zona)), Nothing,dr(DBCol.cod_zona)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_zona_ciudadCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_zona_ciudad
     Get
         Return DirectCast(List(index), T_zona_ciudad)
     End Get
     Set(value As T_zona_ciudad)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_zona_ciudad)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_zona_ciudad)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
