
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
Public Class R_Tarifa_operador
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


    private mcod_operador as System.Int32
  <DBColumnName("cod_operador")>
  <DataMemberAttribute()>
   Public Overridable Property cod_operador() As System.Int32 
    Get 
      return  mcod_operador
    End Get 
    Set(value As System.Int32) 
      IF mcod_operador<>value THEN 
        mcod_operador=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtipo_tarifa as System.Byte
  <DBColumnName("tipo_tarifa")>
  <DataMemberAttribute()>
   Public Overridable Property tipo_tarifa() As System.Byte 
    Get 
      return  mtipo_tarifa
    End Get 
    Set(value As System.Byte) 
      IF mtipo_tarifa<>value THEN 
        mtipo_tarifa=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const cod_tarifa As String="cod_tarifa" 'int
  Public Const cod_operador As String="cod_operador" 'int
  Public Const tipo_tarifa As String="tipo_tarifa" 'tinyint
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Tarifa_operador) as R_Tarifa_operador
  If obj2Fill Is Nothing Then
      obj2Fill = New R_Tarifa_operador
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.cod_tarifa =IIf(IsDBNull(dr(DBCol.cod_tarifa)), Nothing,dr(DBCol.cod_tarifa)) 
  obj2Fill.cod_operador =IIf(IsDBNull(dr(DBCol.cod_operador)), Nothing,dr(DBCol.cod_operador)) 
  obj2Fill.tipo_tarifa =IIf(IsDBNull(dr(DBCol.tipo_tarifa)), Nothing,dr(DBCol.tipo_tarifa)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_Tarifa_operadorCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_Tarifa_operador
     Get
         Return DirectCast(List(index), R_Tarifa_operador)
     End Get
     Set(value As R_Tarifa_operador)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_Tarifa_operador)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_Tarifa_operador)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
