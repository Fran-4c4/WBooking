
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
Public Class R_Peticiones
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


    private mfechaEntrada as System.DateTime
  <DBColumnName("fechaEntrada")>
  <DataMemberAttribute()>
   Public Overridable Property fechaEntrada() As System.DateTime 
    Get 
      return  mfechaEntrada
    End Get 
    Set(value As System.DateTime) 
      IF mfechaEntrada<>value THEN 
        mfechaEntrada=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mfechaSalida as System.DateTime
  <DBColumnName("fechaSalida")>
  <DataMemberAttribute()>
   Public Overridable Property fechaSalida() As System.DateTime 
    Get 
      return  mfechaSalida
    End Get 
    Set(value As System.DateTime) 
      IF mfechaSalida<>value THEN 
        mfechaSalida=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private madultos as System.Int16
  <DBColumnName("adultos")>
  <DataMemberAttribute()>
   Public Overridable Property adultos() As System.Int16 
    Get 
      return  madultos
    End Get 
    Set(value As System.Int16) 
      IF madultos<>value THEN 
        madultos=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mninosfree as System.Int16
  <DBColumnName("ninosfree")>
  <DataMemberAttribute()>
   Public Overridable Property ninosfree() As System.Int16 
    Get 
      return  mninosfree
    End Get 
    Set(value As System.Int16) 
      IF mninosfree<>value THEN 
        mninosfree=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mninospago as System.Int16
  <DBColumnName("ninospago")>
  <DataMemberAttribute()>
   Public Overridable Property ninospago() As System.Int16 
    Get 
      return  mninospago
    End Get 
    Set(value As System.Int16) 
      IF mninospago<>value THEN 
        mninospago=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mregimen as System.Int16
  <DBColumnName("regimen")>
  <DataMemberAttribute()>
   Public Overridable Property regimen() As System.Int16 
    Get 
      return  mregimen
    End Get 
    Set(value As System.Int16) 
      IF mregimen<>value THEN 
        mregimen=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mIP as System.String
  <DBColumnName("IP")>
  <DataMemberAttribute()>
   Public Overridable Property IP() As System.String 
    Get 
      return  mIP
    End Get 
    Set(value As System.String) 
      IF mIP<>value THEN 
        mIP=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const cod_hotel As String="cod_hotel" 'int
  Public Const cod_operador As String="cod_operador" 'int
  Public Const fechaEntrada As String="fechaEntrada" 'datetime
  Public Const fechaSalida As String="fechaSalida" 'datetime
  Public Const adultos As String="adultos" 'smallint
  Public Const ninosfree As String="ninosfree" 'smallint
  Public Const ninospago As String="ninospago" 'smallint
  Public Const regimen As String="regimen" 'smallint
  Public Const IP As String="IP" 'varchar
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Peticiones) as R_Peticiones
  If obj2Fill Is Nothing Then
      obj2Fill = New R_Peticiones
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.cod_hotel =IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing,dr(DBCol.cod_hotel)) 
  obj2Fill.cod_operador =IIf(IsDBNull(dr(DBCol.cod_operador)), Nothing,dr(DBCol.cod_operador)) 
  obj2Fill.fechaEntrada =IIf(IsDBNull(dr(DBCol.fechaEntrada)), Nothing,dr(DBCol.fechaEntrada)) 
  obj2Fill.fechaSalida =IIf(IsDBNull(dr(DBCol.fechaSalida)), Nothing,dr(DBCol.fechaSalida)) 
  obj2Fill.adultos =IIf(IsDBNull(dr(DBCol.adultos)), Nothing,dr(DBCol.adultos)) 
  obj2Fill.ninosfree =IIf(IsDBNull(dr(DBCol.ninosfree)), Nothing,dr(DBCol.ninosfree)) 
  obj2Fill.ninospago =IIf(IsDBNull(dr(DBCol.ninospago)), Nothing,dr(DBCol.ninospago)) 
  obj2Fill.regimen =IIf(IsDBNull(dr(DBCol.regimen)), Nothing,dr(DBCol.regimen)) 
  obj2Fill.IP ="" & dr(DBCol.IP)
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_PeticionesCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_Peticiones
     Get
         Return DirectCast(List(index), R_Peticiones)
     End Get
     Set(value As R_Peticiones)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_Peticiones)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_Peticiones)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
