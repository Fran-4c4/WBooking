
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
Public Class R_Tarifa_habitacion
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


    private mprecio as System.Double
  <DBColumnName("precio")>
  <DataMemberAttribute()>
   Public Overridable Property precio() As System.Double 
    Get 
      return  mprecio
    End Get 
    Set(value As System.Double) 
      IF mprecio<>value THEN 
        mprecio=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mprecioAD as System.Double
  <DBColumnName("precioAD")>
  <DataMemberAttribute()>
   Public Overridable Property precioAD() As System.Double 
    Get 
      return  mprecioAD
    End Get 
    Set(value As System.Double) 
      IF mprecioAD<>value THEN 
        mprecioAD=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mpreciomp as System.Double
  <DBColumnName("preciomp")>
  <DataMemberAttribute()>
   Public Overridable Property preciomp() As System.Double 
    Get 
      return  mpreciomp
    End Get 
    Set(value As System.Double) 
      IF mpreciomp<>value THEN 
        mpreciomp=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mpreciopc as System.Double
  <DBColumnName("preciopc")>
  <DataMemberAttribute()>
   Public Overridable Property preciopc() As System.Double 
    Get 
      return  mpreciopc
    End Get 
    Set(value As System.Double) 
      IF mpreciopc<>value THEN 
        mpreciopc=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescAdultosExtra1 as System.Double
  <DBColumnName("DescAdultosExtra1")>
  <DataMemberAttribute()>
   Public Overridable Property DescAdultosExtra1() As System.Double 
    Get 
      return  mDescAdultosExtra1
    End Get 
    Set(value As System.Double) 
      IF mDescAdultosExtra1<>value THEN 
        mDescAdultosExtra1=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescAdultosExtra2 as System.Double
  <DBColumnName("DescAdultosExtra2")>
  <DataMemberAttribute()>
   Public Overridable Property DescAdultosExtra2() As System.Double 
    Get 
      return  mDescAdultosExtra2
    End Get 
    Set(value As System.Double) 
      IF mDescAdultosExtra2<>value THEN 
        mDescAdultosExtra2=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescNinoPago1 as System.Double
  <DBColumnName("DescNinoPago1")>
  <DataMemberAttribute()>
   Public Overridable Property DescNinoPago1() As System.Double 
    Get 
      return  mDescNinoPago1
    End Get 
    Set(value As System.Double) 
      IF mDescNinoPago1<>value THEN 
        mDescNinoPago1=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescNinoPago2 as System.Double
  <DBColumnName("DescNinoPago2")>
  <DataMemberAttribute()>
   Public Overridable Property DescNinoPago2() As System.Double 
    Get 
      return  mDescNinoPago2
    End Get 
    Set(value As System.Double) 
      IF mDescNinoPago2<>value THEN 
        mDescNinoPago2=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mdesayunonino as System.Double
  <DBColumnName("desayunonino")>
  <DataMemberAttribute()>
   Public Overridable Property desayunonino() As System.Double 
    Get 
      return  mdesayunonino
    End Get 
    Set(value As System.Double) 
      IF mdesayunonino<>value THEN 
        mdesayunonino=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mmpnino as System.Double
  <DBColumnName("mpnino")>
  <DataMemberAttribute()>
   Public Overridable Property mpnino() As System.Double 
    Get 
      return  mmpnino
    End Get 
    Set(value As System.Double) 
      IF mmpnino<>value THEN 
        mmpnino=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mpcnino as System.Double
  <DBColumnName("pcnino")>
  <DataMemberAttribute()>
   Public Overridable Property pcnino() As System.Double 
    Get 
      return  mpcnino
    End Get 
    Set(value As System.Double) 
      IF mpcnino<>value THEN 
        mpcnino=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mTodoIncluido as System.Double
  <DBColumnName("TodoIncluido")>
  <DataMemberAttribute()>
   Public Overridable Property TodoIncluido() As System.Double 
    Get 
      return  mTodoIncluido
    End Get 
    Set(value As System.Double) 
      IF mTodoIncluido<>value THEN 
        mTodoIncluido=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mTodoIncluidoNino as System.Double
  <DBColumnName("TodoIncluidoNino")>
  <DataMemberAttribute()>
   Public Overridable Property TodoIncluidoNino() As System.Double 
    Get 
      return  mTodoIncluidoNino
    End Get 
    Set(value As System.Double) 
      IF mTodoIncluidoNino<>value THEN 
        mTodoIncluidoNino=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAplicDescAReg as System.Byte
  <DBColumnName("AplicDescAReg")>
  <DataMemberAttribute()>
   Public Overridable Property AplicDescAReg() As System.Byte 
    Get 
      return  mAplicDescAReg
    End Get 
    Set(value As System.Byte) 
      IF mAplicDescAReg<>value THEN 
        mAplicDescAReg=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcamaExtra2 as System.Double
  <DBColumnName("camaExtra2")>
  <DataMemberAttribute()>
   Public Overridable Property camaExtra2() As System.Double 
    Get 
      return  mcamaExtra2
    End Get 
    Set(value As System.Double) 
      IF mcamaExtra2<>value THEN 
        mcamaExtra2=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcamaExtra1 as System.Double
  <DBColumnName("camaExtra1")>
  <DataMemberAttribute()>
   Public Overridable Property camaExtra1() As System.Double 
    Get 
      return  mcamaExtra1
    End Get 
    Set(value As System.Double) 
      IF mcamaExtra1<>value THEN 
        mcamaExtra1=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcamaExtra1nino as System.Double
  <DBColumnName("camaExtra1nino")>
  <DataMemberAttribute()>
   Public Overridable Property camaExtra1nino() As System.Double 
    Get 
      return  mcamaExtra1nino
    End Get 
    Set(value As System.Double) 
      IF mcamaExtra1nino<>value THEN 
        mcamaExtra1nino=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcamaExtra2nino as System.Double
  <DBColumnName("camaExtra2nino")>
  <DataMemberAttribute()>
   Public Overridable Property camaExtra2nino() As System.Double 
    Get 
      return  mcamaExtra2nino
    End Get 
    Set(value As System.Double) 
      IF mcamaExtra2nino<>value THEN 
        mcamaExtra2nino=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mSupDiasMinimos as System.Double
  <DBColumnName("SupDiasMinimos")>
  <DataMemberAttribute()>
   Public Overridable Property SupDiasMinimos() As System.Double 
    Get 
      return  mSupDiasMinimos
    End Get 
    Set(value As System.Double) 
      IF mSupDiasMinimos<>value THEN 
        mSupDiasMinimos=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mSupFinDeSemana as System.Double
  <DBColumnName("SupFinDeSemana")>
  <DataMemberAttribute()>
   Public Overridable Property SupFinDeSemana() As System.Double 
    Get 
      return  mSupFinDeSemana
    End Get 
    Set(value As System.Double) 
      IF mSupFinDeSemana<>value THEN 
        mSupFinDeSemana=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const ID As String="ID" 'int
  Public Const cod_tarifa As String="cod_tarifa" 'int
  Public Const cod_habitacion As String="cod_habitacion" 'int
  Public Const precio As String="precio" 'float
  Public Const precioAD As String="precioAD" 'float
  Public Const preciomp As String="preciomp" 'float
  Public Const preciopc As String="preciopc" 'float
  Public Const DescAdultosExtra1 As String="DescAdultosExtra1" 'float
  Public Const DescAdultosExtra2 As String="DescAdultosExtra2" 'float
  Public Const DescNinoPago1 As String="DescNinoPago1" 'float
  Public Const DescNinoPago2 As String="DescNinoPago2" 'float
  Public Const desayunonino As String="desayunonino" 'float
  Public Const mpnino As String="mpnino" 'float
  Public Const pcnino As String="pcnino" 'float
  Public Const TodoIncluido As String="TodoIncluido" 'float
  Public Const TodoIncluidoNino As String="TodoIncluidoNino" 'float
  Public Const AplicDescAReg As String="AplicDescAReg" 'tinyint
  Public Const camaExtra2 As String="camaExtra2" 'float
  Public Const camaExtra1 As String="camaExtra1" 'float
  Public Const camaExtra1nino As String="camaExtra1nino" 'float
  Public Const camaExtra2nino As String="camaExtra2nino" 'float
  Public Const SupDiasMinimos As String="SupDiasMinimos" 'float
  Public Const SupFinDeSemana As String="SupFinDeSemana" 'float
End Class  
#End Region

        Public Sub FillByRow(ByRef dr As DataRow)
            FillObjectByRow(dr, Me)
        End Sub


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, Optional obj2Fill As R_Tarifa_habitacion = Nothing) As R_Tarifa_habitacion
            If obj2Fill Is Nothing Then
                obj2Fill = New R_Tarifa_habitacion
            End If

            obj2Fill.ID = IIf(IsDBNull(dr(DBCol.ID)), Nothing, dr(DBCol.ID))
            obj2Fill.cod_tarifa = IIf(IsDBNull(dr(DBCol.cod_tarifa)), Nothing, dr(DBCol.cod_tarifa))
            obj2Fill.cod_habitacion = IIf(IsDBNull(dr(DBCol.cod_habitacion)), Nothing, dr(DBCol.cod_habitacion))
            obj2Fill.precio = IIf(IsDBNull(dr(DBCol.precio)), Nothing, dr(DBCol.precio))
            obj2Fill.precioAD = IIf(IsDBNull(dr(DBCol.precioAD)), Nothing, dr(DBCol.precioAD))
            obj2Fill.preciomp = IIf(IsDBNull(dr(DBCol.preciomp)), Nothing, dr(DBCol.preciomp))
            obj2Fill.preciopc = IIf(IsDBNull(dr(DBCol.preciopc)), Nothing, dr(DBCol.preciopc))
            obj2Fill.DescAdultosExtra1 = IIf(IsDBNull(dr(DBCol.DescAdultosExtra1)), Nothing, dr(DBCol.DescAdultosExtra1))
            obj2Fill.DescAdultosExtra2 = IIf(IsDBNull(dr(DBCol.DescAdultosExtra2)), Nothing, dr(DBCol.DescAdultosExtra2))
            obj2Fill.DescNinoPago1 = IIf(IsDBNull(dr(DBCol.DescNinoPago1)), Nothing, dr(DBCol.DescNinoPago1))
            obj2Fill.DescNinoPago2 = IIf(IsDBNull(dr(DBCol.DescNinoPago2)), Nothing, dr(DBCol.DescNinoPago2))
            obj2Fill.desayunonino = IIf(IsDBNull(dr(DBCol.desayunonino)), Nothing, dr(DBCol.desayunonino))
            obj2Fill.mpnino = IIf(IsDBNull(dr(DBCol.mpnino)), Nothing, dr(DBCol.mpnino))
            obj2Fill.pcnino = IIf(IsDBNull(dr(DBCol.pcnino)), Nothing, dr(DBCol.pcnino))
            obj2Fill.TodoIncluido = IIf(IsDBNull(dr(DBCol.TodoIncluido)), Nothing, dr(DBCol.TodoIncluido))
            obj2Fill.TodoIncluidoNino = IIf(IsDBNull(dr(DBCol.TodoIncluidoNino)), Nothing, dr(DBCol.TodoIncluidoNino))
            'obj2Fill.AplicDescAReg = IIf(IsDBNull(dr(DBCol.AplicDescAReg)), Nothing, dr(DBCol.AplicDescAReg))
            obj2Fill.camaExtra2 = IIf(IsDBNull(dr(DBCol.camaExtra2)), Nothing, dr(DBCol.camaExtra2))
            obj2Fill.camaExtra1 = IIf(IsDBNull(dr(DBCol.camaExtra1)), Nothing, dr(DBCol.camaExtra1))
            obj2Fill.camaExtra1nino = IIf(IsDBNull(dr(DBCol.camaExtra1nino)), Nothing, dr(DBCol.camaExtra1nino))
            obj2Fill.camaExtra2nino = IIf(IsDBNull(dr(DBCol.camaExtra2nino)), Nothing, dr(DBCol.camaExtra2nino))
            obj2Fill.SupDiasMinimos = IIf(IsDBNull(dr(DBCol.SupDiasMinimos)), Nothing, dr(DBCol.SupDiasMinimos))
            obj2Fill.SupFinDeSemana = IIf(IsDBNull(dr(DBCol.SupFinDeSemana)), Nothing, dr(DBCol.SupFinDeSemana))
            Return obj2Fill
        End Function
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_Tarifa_habitacionCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_Tarifa_habitacion
     Get
         Return DirectCast(List(index), R_Tarifa_habitacion)
     End Get
     Set(value As R_Tarifa_habitacion)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_Tarifa_habitacion)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_Tarifa_habitacion)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
