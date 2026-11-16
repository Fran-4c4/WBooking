
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
Public Class T_HOTELES_SITUACIONES
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


    private mcod_situacion as System.Int32
  <DBColumnName("cod_situacion")>
  <DataMemberAttribute()>
   Public Overridable Property cod_situacion() As System.Int32 
    Get 
      return  mcod_situacion
    End Get 
    Set(value As System.Int32) 
      IF mcod_situacion<>value THEN 
        mcod_situacion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const cod_Hotel As String="cod_Hotel" 'int
  Public Const cod_situacion As String="cod_situacion" 'int
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_HOTELES_SITUACIONES) as T_HOTELES_SITUACIONES
  If obj2Fill Is Nothing Then
      obj2Fill = New T_HOTELES_SITUACIONES
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.cod_Hotel =IIf(IsDBNull(dr(DBCol.cod_Hotel)), Nothing,dr(DBCol.cod_Hotel)) 
  obj2Fill.cod_situacion =IIf(IsDBNull(dr(DBCol.cod_situacion)), Nothing,dr(DBCol.cod_situacion)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_HOTELES_SITUACIONESCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_HOTELES_SITUACIONES
     Get
         Return DirectCast(List(index), T_HOTELES_SITUACIONES)
     End Get
     Set(value As T_HOTELES_SITUACIONES)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_HOTELES_SITUACIONES)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_HOTELES_SITUACIONES)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
