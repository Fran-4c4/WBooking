
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
Public Class R_Temporadas_Periodos
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


    private mcod_periodo as System.Int32
  <DBColumnName("cod_periodo")>
  <DataMemberAttribute()>
   Public Overridable Property cod_periodo() As System.Int32  Implements IBObase.ID
    Get 
      return  mcod_periodo
    End Get 
    Set(value As System.Int32) 
      IF mcod_periodo<>value THEN 
        mcod_periodo=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_temporada as System.Int32
  <DBColumnName("cod_temporada")>
  <DataMemberAttribute()>
   Public Overridable Property cod_temporada() As System.Int32 
    Get 
      return  mcod_temporada
    End Get 
    Set(value As System.Int32) 
      IF mcod_temporada<>value THEN 
        mcod_temporada=value
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

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const cod_periodo As String="cod_periodo" 'int
  Public Const cod_temporada As String="cod_temporada" 'int
  Public Const fechainicio As String="fechainicio" 'datetime
  Public Const fechafin As String="fechafin" 'datetime
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Temporadas_Periodos) as R_Temporadas_Periodos
  If obj2Fill Is Nothing Then
      obj2Fill = New R_Temporadas_Periodos
  End If

  obj2Fill.cod_periodo =IIf(IsDBNull(dr(DBCol.cod_periodo)), Nothing,dr(DBCol.cod_periodo)) 
  obj2Fill.cod_temporada =IIf(IsDBNull(dr(DBCol.cod_temporada)), Nothing,dr(DBCol.cod_temporada)) 
  obj2Fill.fechainicio =IIf(IsDBNull(dr(DBCol.fechainicio)), Nothing,dr(DBCol.fechainicio)) 
  obj2Fill.fechafin =IIf(IsDBNull(dr(DBCol.fechafin)), Nothing,dr(DBCol.fechafin)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_Temporadas_PeriodosCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_Temporadas_Periodos
     Get
         Return DirectCast(List(index), R_Temporadas_Periodos)
     End Get
     Set(value As R_Temporadas_Periodos)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_Temporadas_Periodos)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_Temporadas_Periodos)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
