
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
Public Class R_ResO_Hab
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


    private mcod_reservaO as System.Int32
  <DBColumnName("cod_reservaO")>
  <DataMemberAttribute()>
   Public Overridable Property cod_reservaO() As System.Int32 
    Get 
      return  mcod_reservaO
    End Get 
    Set(value As System.Int32) 
      IF mcod_reservaO<>value THEN 
        mcod_reservaO=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_reso_hab as System.Int32
  <DBColumnName("cod_reso_hab")>
  <DataMemberAttribute()>
   Public Overridable Property cod_reso_hab() As System.Int32  Implements IBObase.ID
    Get 
      return  mcod_reso_hab
    End Get 
    Set(value As System.Int32) 
      IF mcod_reso_hab<>value THEN 
        mcod_reso_hab=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mfecha_entrada as System.DateTime
  <DBColumnName("fecha_entrada")>
  <DataMemberAttribute()>
   Public Overridable Property fecha_entrada() As System.DateTime 
    Get 
      return  mfecha_entrada
    End Get 
    Set(value As System.DateTime) 
      IF mfecha_entrada<>value THEN 
        mfecha_entrada=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mfecha_salida as System.DateTime
  <DBColumnName("fecha_salida")>
  <DataMemberAttribute()>
   Public Overridable Property fecha_salida() As System.DateTime 
    Get 
      return  mfecha_salida
    End Get 
    Set(value As System.DateTime) 
      IF mfecha_salida<>value THEN 
        mfecha_salida=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mnum_habitaciones as System.Byte
  <DBColumnName("num_habitaciones")>
  <DataMemberAttribute()>
   Public Overridable Property num_habitaciones() As System.Byte 
    Get 
      return  mnum_habitaciones
    End Get 
    Set(value As System.Byte) 
      IF mnum_habitaciones<>value THEN 
        mnum_habitaciones=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mnum_personas as System.Byte
  <DBColumnName("num_personas")>
  <DataMemberAttribute()>
   Public Overridable Property num_personas() As System.Byte 
    Get 
      return  mnum_personas
    End Get 
    Set(value As System.Byte) 
      IF mnum_personas<>value THEN 
        mnum_personas=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mregimen_aloj as System.String
  <DBColumnName("regimen_aloj")>
  <DataMemberAttribute()>
   Public Overridable Property regimen_aloj() As System.String 
    Get 
      return  mregimen_aloj
    End Get 
    Set(value As System.String) 
      IF mregimen_aloj<>value THEN 
        mregimen_aloj=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtipo_habitacion as System.Int32
  <DBColumnName("tipo_habitacion")>
  <DataMemberAttribute()>
   Public Overridable Property tipo_habitacion() As System.Int32 
    Get 
      return  mtipo_habitacion
    End Get 
    Set(value As System.Int32) 
      IF mtipo_habitacion<>value THEN 
        mtipo_habitacion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private medadnino1 as System.Byte
  <DBColumnName("edadnino1")>
  <DataMemberAttribute()>
   Public Overridable Property edadnino1() As System.Byte 
    Get 
      return  medadnino1
    End Get 
    Set(value As System.Byte) 
      IF medadnino1<>value THEN 
        medadnino1=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private medadnino2 as System.Byte
  <DBColumnName("edadnino2")>
  <DataMemberAttribute()>
   Public Overridable Property edadnino2() As System.Byte 
    Get 
      return  medadnino2
    End Get 
    Set(value As System.Byte) 
      IF medadnino2<>value THEN 
        medadnino2=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcomentarios1 as System.String
  <DBColumnName("comentarios1")>
  <DataMemberAttribute()>
   Public Overridable Property comentarios1() As System.String 
    Get 
      return  mcomentarios1
    End Get 
    Set(value As System.String) 
      IF mcomentarios1<>value THEN 
        mcomentarios1=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mpreciofinal as System.Double
  <DBColumnName("preciofinal")>
  <DataMemberAttribute()>
   Public Overridable Property preciofinal() As System.Double 
    Get 
      return  mpreciofinal
    End Get 
    Set(value As System.Double) 
      IF mpreciofinal<>value THEN 
        mpreciofinal=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mHoraConfirmacion as System.DateTime
  <DBColumnName("HoraConfirmacion")>
  <DataMemberAttribute()>
   Public Overridable Property HoraConfirmacion() As System.DateTime 
    Get 
      return  mHoraConfirmacion
    End Get 
    Set(value As System.DateTime) 
      IF mHoraConfirmacion<>value THEN 
        mHoraConfirmacion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mConfirmadax as System.String
  <DBColumnName("Confirmadax")>
  <DataMemberAttribute()>
   Public Overridable Property Confirmadax() As System.String 
    Get 
      return  mConfirmadax
    End Get 
    Set(value As System.String) 
      IF mConfirmadax<>value THEN 
        mConfirmadax=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mHoraModificacion as System.DateTime
  <DBColumnName("HoraModificacion")>
  <DataMemberAttribute()>
   Public Overridable Property HoraModificacion() As System.DateTime 
    Get 
      return  mHoraModificacion
    End Get 
    Set(value As System.DateTime) 
      IF mHoraModificacion<>value THEN 
        mHoraModificacion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mModificadax as System.String
  <DBColumnName("Modificadax")>
  <DataMemberAttribute()>
   Public Overridable Property Modificadax() As System.String 
    Get 
      return  mModificadax
    End Get 
    Set(value As System.String) 
      IF mModificadax<>value THEN 
        mModificadax=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCanceladax as System.String
  <DBColumnName("Canceladax")>
  <DataMemberAttribute()>
   Public Overridable Property Canceladax() As System.String 
    Get 
      return  mCanceladax
    End Get 
    Set(value As System.String) 
      IF mCanceladax<>value THEN 
        mCanceladax=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mEstado as System.Int32
  <DBColumnName("Estado")>
  <DataMemberAttribute()>
   Public Overridable Property Estado() As System.Int32 
    Get 
      return  mEstado
    End Get 
    Set(value As System.Int32) 
      IF mEstado<>value THEN 
        mEstado=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mEstadoPago as System.Byte
  <DBColumnName("EstadoPago")>
  <DataMemberAttribute()>
   Public Overridable Property EstadoPago() As System.Byte 
    Get 
      return  mEstadoPago
    End Get 
    Set(value As System.Byte) 
      IF mEstadoPago<>value THEN 
        mEstadoPago=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mHoraCancelacion as System.DateTime
  <DBColumnName("HoraCancelacion")>
  <DataMemberAttribute()>
   Public Overridable Property HoraCancelacion() As System.DateTime 
    Get 
      return  mHoraCancelacion
    End Get 
    Set(value As System.DateTime) 
      IF mHoraCancelacion<>value THEN 
        mHoraCancelacion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcupoOrigen as System.String
  <DBColumnName("cupoOrigen")>
  <DataMemberAttribute()>
   Public Overridable Property cupoOrigen() As System.String 
    Get 
      return  mcupoOrigen
    End Get 
    Set(value As System.String) 
      IF mcupoOrigen<>value THEN 
        mcupoOrigen=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mDescHabitacion as System.String
  <DBColumnName("DescHabitacion")>
  <DataMemberAttribute()>
   Public Overridable Property DescHabitacion() As System.String 
    Get 
      return  mDescHabitacion
    End Get 
    Set(value As System.String) 
      IF mDescHabitacion<>value THEN 
        mDescHabitacion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_hab_combinada as System.Int32
  <DBColumnName("cod_hab_combinada")>
  <DataMemberAttribute()>
   Public Overridable Property cod_hab_combinada() As System.Int32 
    Get 
      return  mcod_hab_combinada
    End Get 
    Set(value As System.Int32) 
      IF mcod_hab_combinada<>value THEN 
        mcod_hab_combinada=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mPrecioDesglosado as System.String
  <DBColumnName("PrecioDesglosado")>
  <DataMemberAttribute()>
   Public Overridable Property PrecioDesglosado() As System.String 
    Get 
      return  mPrecioDesglosado
    End Get 
    Set(value As System.String) 
      IF mPrecioDesglosado<>value THEN 
        mPrecioDesglosado=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mRegimenDesglosado as System.String
  <DBColumnName("RegimenDesglosado")>
  <DataMemberAttribute()>
   Public Overridable Property RegimenDesglosado() As System.String 
    Get 
      return  mRegimenDesglosado
    End Get 
    Set(value As System.String) 
      IF mRegimenDesglosado<>value THEN 
        mRegimenDesglosado=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const cod_reservaO As String="cod_reservaO" 'int
  Public Const cod_reso_hab As String="cod_reso_hab" 'int
  Public Const fecha_entrada As String="fecha_entrada" 'datetime
  Public Const fecha_salida As String="fecha_salida" 'datetime
  Public Const num_habitaciones As String="num_habitaciones" 'tinyint
  Public Const num_personas As String="num_personas" 'tinyint
  Public Const regimen_aloj As String="regimen_aloj" 'varchar
  Public Const tipo_habitacion As String="tipo_habitacion" 'int
  Public Const edadnino1 As String="edadnino1" 'tinyint
  Public Const edadnino2 As String="edadnino2" 'tinyint
  Public Const comentarios1 As String="comentarios1" 'varchar
  Public Const preciofinal As String="preciofinal" 'float
  Public Const HoraConfirmacion As String="HoraConfirmacion" 'datetime
  Public Const Confirmadax As String="Confirmadax" 'varchar
  Public Const HoraModificacion As String="HoraModificacion" 'datetime
  Public Const Modificadax As String="Modificadax" 'varchar
  Public Const Canceladax As String="Canceladax" 'varchar
  Public Const Estado As String="Estado" 'int
  Public Const EstadoPago As String="EstadoPago" 'tinyint
  Public Const HoraCancelacion As String="HoraCancelacion" 'datetime
  Public Const cupoOrigen As String="cupoOrigen" 'varchar
  Public Const DescHabitacion As String="DescHabitacion" 'nvarchar
  Public Const cod_hab_combinada As String="cod_hab_combinada" 'int
  Public Const PrecioDesglosado As String="PrecioDesglosado" 'varchar
  Public Const RegimenDesglosado As String="RegimenDesglosado" 'varchar
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_ResO_Hab) as R_ResO_Hab
  If obj2Fill Is Nothing Then
      obj2Fill = New R_ResO_Hab
  End If

  obj2Fill.cod_reservaO =IIf(IsDBNull(dr(DBCol.cod_reservaO)), Nothing,dr(DBCol.cod_reservaO)) 
  obj2Fill.cod_reso_hab =IIf(IsDBNull(dr(DBCol.cod_reso_hab)), Nothing,dr(DBCol.cod_reso_hab)) 
  obj2Fill.fecha_entrada =IIf(IsDBNull(dr(DBCol.fecha_entrada)), Nothing,dr(DBCol.fecha_entrada)) 
  obj2Fill.fecha_salida =IIf(IsDBNull(dr(DBCol.fecha_salida)), Nothing,dr(DBCol.fecha_salida)) 
  obj2Fill.num_habitaciones =IIf(IsDBNull(dr(DBCol.num_habitaciones)), Nothing,dr(DBCol.num_habitaciones)) 
  obj2Fill.num_personas =IIf(IsDBNull(dr(DBCol.num_personas)), Nothing,dr(DBCol.num_personas)) 
  obj2Fill.regimen_aloj ="" & dr(DBCol.regimen_aloj)
  obj2Fill.tipo_habitacion =IIf(IsDBNull(dr(DBCol.tipo_habitacion)), Nothing,dr(DBCol.tipo_habitacion)) 
  obj2Fill.edadnino1 =IIf(IsDBNull(dr(DBCol.edadnino1)), Nothing,dr(DBCol.edadnino1)) 
  obj2Fill.edadnino2 =IIf(IsDBNull(dr(DBCol.edadnino2)), Nothing,dr(DBCol.edadnino2)) 
  obj2Fill.comentarios1 ="" & dr(DBCol.comentarios1)
  obj2Fill.preciofinal =IIf(IsDBNull(dr(DBCol.preciofinal)), Nothing,dr(DBCol.preciofinal)) 
  obj2Fill.HoraConfirmacion =IIf(IsDBNull(dr(DBCol.HoraConfirmacion)), Nothing,dr(DBCol.HoraConfirmacion)) 
  obj2Fill.Confirmadax ="" & dr(DBCol.Confirmadax)
  obj2Fill.HoraModificacion =IIf(IsDBNull(dr(DBCol.HoraModificacion)), Nothing,dr(DBCol.HoraModificacion)) 
  obj2Fill.Modificadax ="" & dr(DBCol.Modificadax)
  obj2Fill.Canceladax ="" & dr(DBCol.Canceladax)
  obj2Fill.Estado =IIf(IsDBNull(dr(DBCol.Estado)), Nothing,dr(DBCol.Estado)) 
  obj2Fill.EstadoPago =IIf(IsDBNull(dr(DBCol.EstadoPago)), Nothing,dr(DBCol.EstadoPago)) 
  obj2Fill.HoraCancelacion =IIf(IsDBNull(dr(DBCol.HoraCancelacion)), Nothing,dr(DBCol.HoraCancelacion)) 
  obj2Fill.cupoOrigen ="" & dr(DBCol.cupoOrigen)
  obj2Fill.DescHabitacion ="" & dr(DBCol.DescHabitacion)
  obj2Fill.cod_hab_combinada =IIf(IsDBNull(dr(DBCol.cod_hab_combinada)), Nothing,dr(DBCol.cod_hab_combinada)) 
  obj2Fill.PrecioDesglosado ="" & dr(DBCol.PrecioDesglosado)
  obj2Fill.RegimenDesglosado ="" & dr(DBCol.RegimenDesglosado)
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_ResO_HabCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_ResO_Hab
     Get
         Return DirectCast(List(index), R_ResO_Hab)
     End Get
     Set(value As R_ResO_Hab)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_ResO_Hab)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_ResO_Hab)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
