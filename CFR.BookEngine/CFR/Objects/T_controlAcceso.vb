
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
Public Class T_controlAcceso
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


    private mfecha as System.String
  <DBColumnName("fecha")>
  <DataMemberAttribute()>
   Public Overridable Property fecha() As System.String 
    Get 
      return  mfecha
    End Get 
    Set(value As System.String) 
      IF mfecha<>value THEN 
        mfecha=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mnotas as System.String
  <DBColumnName("notas")>
  <DataMemberAttribute()>
   Public Overridable Property notas() As System.String 
    Get 
      return  mnotas
    End Get 
    Set(value As System.String) 
      IF mnotas<>value THEN 
        mnotas=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const cod_hotel As String="cod_hotel" 'int
  Public Const fecha As String="fecha" 'varchar
  Public Const notas As String="notas" 'varchar
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_controlAcceso) as T_controlAcceso
  If obj2Fill Is Nothing Then
      obj2Fill = New T_controlAcceso
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.cod_hotel =IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing,dr(DBCol.cod_hotel)) 
  obj2Fill.fecha ="" & dr(DBCol.fecha)
  obj2Fill.notas ="" & dr(DBCol.notas)
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_controlAccesoCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_controlAcceso
     Get
         Return DirectCast(List(index), T_controlAcceso)
     End Get
     Set(value As T_controlAcceso)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_controlAcceso)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_controlAcceso)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
