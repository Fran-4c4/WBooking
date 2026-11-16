
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
Public Class R_TipoContacto
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


    private mid_tipoContacto as System.Int32
  <DBColumnName("id_tipoContacto")>
  <DataMemberAttribute()>
   Public Overridable Property id_tipoContacto() As System.Int32 
    Get 
      return  mid_tipoContacto
    End Get 
    Set(value As System.Int32) 
      IF mid_tipoContacto<>value THEN 
        mid_tipoContacto=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mTipoContacto as System.String
  <DBColumnName("TipoContacto")>
  <DataMemberAttribute()>
   Public Overridable Property TipoContacto() As System.String 
    Get 
      return  mTipoContacto
    End Get 
    Set(value As System.String) 
      IF mTipoContacto<>value THEN 
        mTipoContacto=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const id_tipoContacto As String="id_tipoContacto" 'int
  Public Const TipoContacto As String="TipoContacto" 'varchar
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_TipoContacto) as R_TipoContacto
  If obj2Fill Is Nothing Then
      obj2Fill = New R_TipoContacto
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.id_tipoContacto =IIf(IsDBNull(dr(DBCol.id_tipoContacto)), Nothing,dr(DBCol.id_tipoContacto)) 
  obj2Fill.TipoContacto ="" & dr(DBCol.TipoContacto)
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_TipoContactoCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_TipoContacto
     Get
         Return DirectCast(List(index), R_TipoContacto)
     End Get
     Set(value As R_TipoContacto)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_TipoContacto)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_TipoContacto)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
