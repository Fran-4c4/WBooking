
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
Public Class T_Continentes
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


    private mPrefix as System.String
  <DBColumnName("Prefix")>
  <DataMemberAttribute()>
   Public Overridable Property Prefix() As System.String 
    Get 
      return  mPrefix
    End Get 
    Set(value As System.String) 
      IF mPrefix<>value THEN 
        mPrefix=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcontinente as System.String
  <DBColumnName("continente")>
  <DataMemberAttribute()>
   Public Overridable Property continente() As System.String 
    Get 
      return  mcontinente
    End Get 
    Set(value As System.String) 
      IF mcontinente<>value THEN 
        mcontinente=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const ID As String="ID" 'int
  Public Const cod_cont As String="cod_cont" 'int
  Public Const Prefix As String="Prefix" 'nvarchar
  Public Const continente As String="continente" 'nvarchar
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_Continentes) as T_Continentes
  If obj2Fill Is Nothing Then
      obj2Fill = New T_Continentes
  End If

  obj2Fill.ID =IIf(IsDBNull(dr(DBCol.ID)), Nothing,dr(DBCol.ID)) 
  obj2Fill.cod_cont =IIf(IsDBNull(dr(DBCol.cod_cont)), Nothing,dr(DBCol.cod_cont)) 
  obj2Fill.Prefix ="" & dr(DBCol.Prefix)
  obj2Fill.continente ="" & dr(DBCol.continente)
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_ContinentesCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_Continentes
     Get
         Return DirectCast(List(index), T_Continentes)
     End Get
     Set(value As T_Continentes)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_Continentes)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_Continentes)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
