
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
Public Class R_Tarifas_Temporadas
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


    private mcod_tarifa as System.Int32
  <DBColumnName("cod_tarifa")>
  <DataMemberAttribute()>
   Public Overridable Property cod_tarifa() As System.Int32 
    Get 
      return  mcod_tarifa
    End Get 
    Set(value As System.Int32) 
      IF mcod_tarifa<>value THEN 
        mcod_tarifa=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_Temporada as System.Int32
  <DBColumnName("cod_Temporada")>
  <DataMemberAttribute()>
   Public Overridable Property cod_Temporada() As System.Int32 
    Get 
      return  mcod_Temporada
    End Get 
    Set(value As System.Int32) 
      IF mcod_Temporada<>value THEN 
        mcod_Temporada=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const cod_tarifa As String="cod_tarifa" 'int
  Public Const cod_Temporada As String="cod_Temporada" 'int
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Tarifas_Temporadas) as R_Tarifas_Temporadas
  If obj2Fill Is Nothing Then
      obj2Fill = New R_Tarifas_Temporadas
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.cod_tarifa =IIf(IsDBNull(dr(DBCol.cod_tarifa)), Nothing,dr(DBCol.cod_tarifa)) 
  obj2Fill.cod_Temporada =IIf(IsDBNull(dr(DBCol.cod_Temporada)), Nothing,dr(DBCol.cod_Temporada)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_Tarifas_TemporadasCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_Tarifas_Temporadas
     Get
         Return DirectCast(List(index), R_Tarifas_Temporadas)
     End Get
     Set(value As R_Tarifas_Temporadas)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_Tarifas_Temporadas)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_Tarifas_Temporadas)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
