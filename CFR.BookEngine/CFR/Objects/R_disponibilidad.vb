
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
Public Class R_disponibilidad
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


    private mid_Disp_Hab as System.Int32
  <DBColumnName("id_Disp_Hab")>
  <DataMemberAttribute()>
   Public Overridable Property id_Disp_Hab() As System.Int32  Implements IBObase.ID
    Get 
      return  mid_Disp_Hab
    End Get 
    Set(value As System.Int32) 
      IF mid_Disp_Hab<>value THEN 
        mid_Disp_Hab=value
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


    private mcod_habitacion as System.Int32
  <DBColumnName("cod_habitacion")>
  <DataMemberAttribute()>
   Public Overridable Property cod_habitacion() As System.Int32 
    Get 
      return  mcod_habitacion
    End Get 
    Set(value As System.Int32) 
      IF mcod_habitacion<>value THEN 
        mcod_habitacion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mdia as System.DateTime
  <DBColumnName("dia")>
  <DataMemberAttribute()>
   Public Overridable Property dia() As System.DateTime 
    Get 
      return  mdia
    End Get 
    Set(value As System.DateTime) 
      IF mdia<>value THEN 
        mdia=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_CRS as System.Int16
  <DBColumnName("cod_CRS")>
  <DataMemberAttribute()>
   Public Overridable Property cod_CRS() As System.Int16 
    Get 
      return  mcod_CRS
    End Get 
    Set(value As System.Int16) 
      IF mcod_CRS<>value THEN 
        mcod_CRS=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id_Disp_Hab As String="id_Disp_Hab" 'int
  Public Const cod_hotel As String="cod_hotel" 'int
  Public Const cod_habitacion As String="cod_habitacion" 'int
  Public Const dia As String="dia" 'smalldatetime
  Public Const cod_CRS As String="cod_CRS" 'smallint
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_disponibilidad) as R_disponibilidad
  If obj2Fill Is Nothing Then
      obj2Fill = New R_disponibilidad
  End If

  obj2Fill.id_Disp_Hab =IIf(IsDBNull(dr(DBCol.id_Disp_Hab)), Nothing,dr(DBCol.id_Disp_Hab)) 
  obj2Fill.cod_hotel =IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing,dr(DBCol.cod_hotel)) 
  obj2Fill.cod_habitacion =IIf(IsDBNull(dr(DBCol.cod_habitacion)), Nothing,dr(DBCol.cod_habitacion)) 
  obj2Fill.dia =IIf(IsDBNull(dr(DBCol.dia)), Nothing,dr(DBCol.dia)) 
  obj2Fill.cod_CRS =IIf(IsDBNull(dr(DBCol.cod_CRS)), Nothing,dr(DBCol.cod_CRS)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_disponibilidadCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_disponibilidad
     Get
         Return DirectCast(List(index), R_disponibilidad)
     End Get
     Set(value As R_disponibilidad)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_disponibilidad)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_disponibilidad)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
