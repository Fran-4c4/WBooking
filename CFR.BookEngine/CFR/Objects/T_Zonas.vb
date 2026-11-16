
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
Public Class T_Zonas
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


    private mCod_Zona as System.Int32
  <DBColumnName("Cod_Zona")>
  <DataMemberAttribute()>
   Public Overridable Property Cod_Zona() As System.Int32 
    Get 
      return  mCod_Zona
    End Get 
    Set(value As System.Int32) 
      IF mCod_Zona<>value THEN 
        mCod_Zona=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_pais as System.Int32
  <DBColumnName("cod_pais")>
  <DataMemberAttribute()>
   Public Overridable Property cod_pais() As System.Int32 
    Get 
      return  mcod_pais
    End Get 
    Set(value As System.Int32) 
      IF mcod_pais<>value THEN 
        mcod_pais=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_cont as System.Int32
  <DBColumnName("cod_cont")>
  <DataMemberAttribute()>
   Public Overridable Property cod_cont() As System.Int32 
    Get 
      return  mcod_cont
    End Get 
    Set(value As System.Int32) 
      IF mcod_cont<>value THEN 
        mcod_cont=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mZona as System.String
  <DBColumnName("Zona")>
  <DataMemberAttribute()>
   Public Overridable Property Zona() As System.String 
    Get 
      return  mZona
    End Get 
    Set(value As System.String) 
      IF mZona<>value THEN 
        mZona=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const Cod_Zona As String="Cod_Zona" 'int
  Public Const cod_pais As String="cod_pais" 'int
  Public Const cod_cont As String="cod_cont" 'int
  Public Const Zona As String="Zona" 'nvarchar
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_Zonas) as T_Zonas
  If obj2Fill Is Nothing Then
      obj2Fill = New T_Zonas
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.Cod_Zona =IIf(IsDBNull(dr(DBCol.Cod_Zona)), Nothing,dr(DBCol.Cod_Zona)) 
  obj2Fill.cod_pais =IIf(IsDBNull(dr(DBCol.cod_pais)), Nothing,dr(DBCol.cod_pais)) 
  obj2Fill.cod_cont =IIf(IsDBNull(dr(DBCol.cod_cont)), Nothing,dr(DBCol.cod_cont)) 
  obj2Fill.Zona ="" & dr(DBCol.Zona)
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_ZonasCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_Zonas
     Get
         Return DirectCast(List(index), T_Zonas)
     End Get
     Set(value As T_Zonas)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_Zonas)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_Zonas)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
