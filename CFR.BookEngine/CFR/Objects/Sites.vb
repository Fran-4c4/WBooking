
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
Public Class Sites
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


    private mid_site as System.Int32
  <DBColumnName("id_site")>
  <DataMemberAttribute()>
   Public Overridable Property id_site() As System.Int32  Implements IBObase.ID
    Get 
      return  mid_site
    End Get 
    Set(value As System.Int32) 
      IF mid_site<>value THEN 
        mid_site=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_site as System.String
  <DBColumnName("cod_site")>
  <DataMemberAttribute()>
   Public Overridable Property cod_site() As System.String 
    Get 
      return  mcod_site
    End Get 
    Set(value As System.String) 
      IF mcod_site<>value THEN 
        mcod_site=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCSS as System.String
  <DBColumnName("CSS")>
  <DataMemberAttribute()>
   Public Overridable Property CSS() As System.String 
    Get 
      return  mCSS
    End Get 
    Set(value As System.String) 
      IF mCSS<>value THEN 
        mCSS=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescripcion as System.String
  <DBColumnName("Descripcion")>
  <DataMemberAttribute()>
   Public Overridable Property Descripcion() As System.String 
    Get 
      return  mDescripcion
    End Get 
    Set(value As System.String) 
      IF mDescripcion<>value THEN 
        mDescripcion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id_site As String="id_site" 'int
  Public Const cod_site As String="cod_site" 'varchar
  Public Const CSS As String="CSS" 'varchar
  Public Const Descripcion As String="Descripcion" 'varchar
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As Sites) as Sites
  If obj2Fill Is Nothing Then
      obj2Fill = New Sites
  End If

  obj2Fill.id_site =IIf(IsDBNull(dr(DBCol.id_site)), Nothing,dr(DBCol.id_site)) 
  obj2Fill.cod_site ="" & dr(DBCol.cod_site)
  obj2Fill.CSS ="" & dr(DBCol.CSS)
  obj2Fill.Descripcion ="" & dr(DBCol.Descripcion)
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class SitesCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As Sites
     Get
         Return DirectCast(List(index), Sites)
     End Get
     Set(value As Sites)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As Sites)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As Sites)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
