
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
Public Class T_zona_zona
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


    private mcod_zona_Rel as System.Int32
  <DBColumnName("cod_zona_Rel")>
  <DataMemberAttribute()>
   Public Overridable Property cod_zona_Rel() As System.Int32 
    Get 
      return  mcod_zona_Rel
    End Get 
    Set(value As System.Int32) 
      IF mcod_zona_Rel<>value THEN 
        mcod_zona_Rel=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const cod_zona As String="cod_zona" 'int
  Public Const cod_zona_Rel As String="cod_zona_Rel" 'int
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_zona_zona) as T_zona_zona
  If obj2Fill Is Nothing Then
      obj2Fill = New T_zona_zona
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.cod_zona =IIf(IsDBNull(dr(DBCol.cod_zona)), Nothing,dr(DBCol.cod_zona)) 
  obj2Fill.cod_zona_Rel =IIf(IsDBNull(dr(DBCol.cod_zona_Rel)), Nothing,dr(DBCol.cod_zona_Rel)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_zona_zonaCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_zona_zona
     Get
         Return DirectCast(List(index), T_zona_zona)
     End Get
     Set(value As T_zona_zona)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_zona_zona)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_zona_zona)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
