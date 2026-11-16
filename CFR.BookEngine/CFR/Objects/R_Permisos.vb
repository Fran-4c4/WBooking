
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
Public Class R_Permisos
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


    private mID as System.Int32
  <DBColumnName("ID")>
  <DataMemberAttribute()>
   Public Overridable Property ID() As System.Int32  Implements IBObase.ID
    Get 
      return  mID
    End Get 
    Set(value As System.Int32) 
      IF mID<>value THEN 
        mID=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_permiso as System.Byte
  <DBColumnName("cod_permiso")>
  <DataMemberAttribute()>
   Public Overridable Property cod_permiso() As System.Byte 
    Get 
      return  mcod_permiso
    End Get 
    Set(value As System.Byte) 
      IF mcod_permiso<>value THEN 
        mcod_permiso=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mPermiso as System.String
  <DBColumnName("Permiso")>
  <DataMemberAttribute()>
   Public Overridable Property Permiso() As System.String 
    Get 
      return  mPermiso
    End Get 
    Set(value As System.String) 
      IF mPermiso<>value THEN 
        mPermiso=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCategoria as System.String
  <DBColumnName("Categoria")>
  <DataMemberAttribute()>
   Public Overridable Property Categoria() As System.String 
    Get 
      return  mCategoria
    End Get 
    Set(value As System.String) 
      IF mCategoria<>value THEN 
        mCategoria=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private morden as System.Byte
  <DBColumnName("orden")>
  <DataMemberAttribute()>
   Public Overridable Property orden() As System.Byte 
    Get 
      return  morden
    End Get 
    Set(value As System.Byte) 
      IF morden<>value THEN 
        morden=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const ID As String="ID" 'int
  Public Const cod_permiso As String="cod_permiso" 'tinyint
  Public Const Permiso As String="Permiso" 'varchar
  Public Const Categoria As String="Categoria" 'varchar
  Public Const orden As String="orden" 'tinyint
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Permisos) as R_Permisos
  If obj2Fill Is Nothing Then
      obj2Fill = New R_Permisos
  End If

  obj2Fill.ID =IIf(IsDBNull(dr(DBCol.ID)), Nothing,dr(DBCol.ID)) 
  obj2Fill.cod_permiso =IIf(IsDBNull(dr(DBCol.cod_permiso)), Nothing,dr(DBCol.cod_permiso)) 
  obj2Fill.Permiso ="" & dr(DBCol.Permiso)
  obj2Fill.Categoria ="" & dr(DBCol.Categoria)
  obj2Fill.orden =IIf(IsDBNull(dr(DBCol.orden)), Nothing,dr(DBCol.orden)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_PermisosCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_Permisos
     Get
         Return DirectCast(List(index), R_Permisos)
     End Get
     Set(value As R_Permisos)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_Permisos)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_Permisos)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
