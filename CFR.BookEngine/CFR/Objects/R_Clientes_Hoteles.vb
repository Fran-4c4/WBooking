
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
Public Class R_Clientes_Hoteles
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


    private mId_Cliente as System.Int32
  <DBColumnName("Id_Cliente")>
  <DataMemberAttribute()>
   Public Overridable Property Id_Cliente() As System.Int32 
    Get 
      return  mId_Cliente
    End Get 
    Set(value As System.Int32) 
      IF mId_Cliente<>value THEN 
        mId_Cliente=value
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

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const Id_Cliente As String="Id_Cliente" 'int
  Public Const cod_hotel As String="cod_hotel" 'int
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Clientes_Hoteles) as R_Clientes_Hoteles
  If obj2Fill Is Nothing Then
      obj2Fill = New R_Clientes_Hoteles
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.Id_Cliente =IIf(IsDBNull(dr(DBCol.Id_Cliente)), Nothing,dr(DBCol.Id_Cliente)) 
  obj2Fill.cod_hotel =IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing,dr(DBCol.cod_hotel)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_Clientes_HotelesCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_Clientes_Hoteles
     Get
         Return DirectCast(List(index), R_Clientes_Hoteles)
     End Get
     Set(value As R_Clientes_Hoteles)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_Clientes_Hoteles)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_Clientes_Hoteles)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
