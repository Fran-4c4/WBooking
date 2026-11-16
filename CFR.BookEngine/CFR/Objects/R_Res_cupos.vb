
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
Public Class R_Res_cupos
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


    private mid_Disp_Hab as System.Int32
  <DBColumnName("id_Disp_Hab")>
  <DataMemberAttribute()>
   Public Overridable Property id_Disp_Hab() As System.Int32 
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


    private mdisponible as System.Int16
  <DBColumnName("disponible")>
  <DataMemberAttribute()>
   Public Overridable Property disponible() As System.Int16 
    Get 
      return  mdisponible
    End Get 
    Set(value As System.Int16) 
      IF mdisponible<>value THEN 
        mdisponible=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const ID As String="ID" 'int
  Public Const cod_operador As String="cod_operador" 'int
  Public Const id_Disp_Hab As String="id_Disp_Hab" 'int
  Public Const disponible As String="disponible" 'smallint
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Res_cupos) as R_Res_cupos
  If obj2Fill Is Nothing Then
      obj2Fill = New R_Res_cupos
  End If

  obj2Fill.ID =IIf(IsDBNull(dr(DBCol.ID)), Nothing,dr(DBCol.ID)) 
  obj2Fill.cod_operador =IIf(IsDBNull(dr(DBCol.cod_operador)), Nothing,dr(DBCol.cod_operador)) 
  obj2Fill.id_Disp_Hab =IIf(IsDBNull(dr(DBCol.id_Disp_Hab)), Nothing,dr(DBCol.id_Disp_Hab)) 
  obj2Fill.disponible =IIf(IsDBNull(dr(DBCol.disponible)), Nothing,dr(DBCol.disponible)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_Res_cuposCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_Res_cupos
     Get
         Return DirectCast(List(index), R_Res_cupos)
     End Get
     Set(value As R_Res_cupos)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_Res_cupos)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_Res_cupos)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
