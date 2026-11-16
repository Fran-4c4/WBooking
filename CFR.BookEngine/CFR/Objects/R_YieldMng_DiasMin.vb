
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
Public Class R_YieldMng_DiasMin
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


    private mid_YieldMNG as System.Int32
  <DBColumnName("id_YieldMNG")>
  <DataMemberAttribute()>
   Public Overridable Property id_YieldMNG() As System.Int32  Implements IBObase.ID
    Get 
      return  mid_YieldMNG
    End Get 
    Set(value As System.Int32) 
      IF mid_YieldMNG<>value THEN 
        mid_YieldMNG=value
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


    private mfechainicio as System.DateTime
  <DBColumnName("fechainicio")>
  <DataMemberAttribute()>
   Public Overridable Property fechainicio() As System.DateTime 
    Get 
      return  mfechainicio
    End Get 
    Set(value As System.DateTime) 
      IF mfechainicio<>value THEN 
        mfechainicio=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mfechafin as System.DateTime
  <DBColumnName("fechafin")>
  <DataMemberAttribute()>
   Public Overridable Property fechafin() As System.DateTime 
    Get 
      return  mfechafin
    End Get 
    Set(value As System.DateTime) 
      IF mfechafin<>value THEN 
        mfechafin=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDiasMinimos as System.Int32
  <DBColumnName("DiasMinimos")>
  <DataMemberAttribute()>
   Public Overridable Property DiasMinimos() As System.Int32 
    Get 
      return  mDiasMinimos
    End Get 
    Set(value As System.Int32) 
      IF mDiasMinimos<>value THEN 
        mDiasMinimos=value
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

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id_YieldMNG As String="id_YieldMNG" 'int
  Public Const cod_hotel As String="cod_hotel" 'int
  Public Const fechainicio As String="fechainicio" 'smalldatetime
  Public Const fechafin As String="fechafin" 'smalldatetime
  Public Const DiasMinimos As String="DiasMinimos" 'int
  Public Const cod_operador As String="cod_operador" 'int
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_YieldMng_DiasMin) as R_YieldMng_DiasMin
  If obj2Fill Is Nothing Then
      obj2Fill = New R_YieldMng_DiasMin
  End If

  obj2Fill.id_YieldMNG =IIf(IsDBNull(dr(DBCol.id_YieldMNG)), Nothing,dr(DBCol.id_YieldMNG)) 
  obj2Fill.cod_hotel =IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing,dr(DBCol.cod_hotel)) 
  obj2Fill.fechainicio =IIf(IsDBNull(dr(DBCol.fechainicio)), Nothing,dr(DBCol.fechainicio)) 
  obj2Fill.fechafin =IIf(IsDBNull(dr(DBCol.fechafin)), Nothing,dr(DBCol.fechafin)) 
  obj2Fill.DiasMinimos =IIf(IsDBNull(dr(DBCol.DiasMinimos)), Nothing,dr(DBCol.DiasMinimos)) 
  obj2Fill.cod_operador =IIf(IsDBNull(dr(DBCol.cod_operador)), Nothing,dr(DBCol.cod_operador)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_YieldMng_DiasMinCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_YieldMng_DiasMin
     Get
         Return DirectCast(List(index), R_YieldMng_DiasMin)
     End Get
     Set(value As R_YieldMng_DiasMin)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_YieldMng_DiasMin)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_YieldMng_DiasMin)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
