
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
Public Class R_Res_Hot_Param
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


    private mEdadMaxNinoGratis as System.Byte
  <DBColumnName("EdadMaxNinoGratis")>
  <DataMemberAttribute()>
   Public Overridable Property EdadMaxNinoGratis() As System.Byte 
    Get 
      return  mEdadMaxNinoGratis
    End Get 
    Set(value As System.Byte) 
      IF mEdadMaxNinoGratis<>value THEN 
        mEdadMaxNinoGratis=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mEdadMaxNinoPago as System.Byte
  <DBColumnName("EdadMaxNinoPago")>
  <DataMemberAttribute()>
   Public Overridable Property EdadMaxNinoPago() As System.Byte 
    Get 
      return  mEdadMaxNinoPago
    End Get 
    Set(value As System.Byte) 
      IF mEdadMaxNinoPago<>value THEN 
        mEdadMaxNinoPago=value
        me.objChanged=true
      END IF
    End Set 
   End  Property




    private mimpuestos as System.Double
  <DBColumnName("impuestos")>
  <DataMemberAttribute()>
   Public Overridable Property impuestos() As System.Double 
    Get 
      return  mimpuestos
    End Get 
    Set(value As System.Double) 
      IF mimpuestos<>value THEN 
        mimpuestos=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mmoneda as System.String
  <DBColumnName("moneda")>
  <DataMemberAttribute()>
   Public Overridable Property moneda() As System.String 
    Get 
      return  mmoneda
    End Get 
    Set(value As System.String) 
      IF mmoneda<>value THEN 
        mmoneda=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAlojamiento as System.Boolean
  <DBColumnName("Alojamiento")>
  <DataMemberAttribute()>
   Public Overridable Property Alojamiento() As System.Boolean 
    Get 
      return  mAlojamiento
    End Get 
    Set(value As System.Boolean) 
      IF mAlojamiento<>value THEN 
        mAlojamiento=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAD as System.Boolean
  <DBColumnName("AD")>
  <DataMemberAttribute()>
   Public Overridable Property AD() As System.Boolean 
    Get 
      return  mAD
    End Get 
    Set(value As System.Boolean) 
      IF mAD<>value THEN 
        mAD=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mMP as System.Boolean
  <DBColumnName("MP")>
  <DataMemberAttribute()>
   Public Overridable Property MP() As System.Boolean 
    Get 
      return  mMP
    End Get 
    Set(value As System.Boolean) 
      IF mMP<>value THEN 
        mMP=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mPC as System.Boolean
  <DBColumnName("PC")>
  <DataMemberAttribute()>
   Public Overridable Property PC() As System.Boolean 
    Get 
      return  mPC
    End Get 
    Set(value As System.Boolean) 
      IF mPC<>value THEN 
        mPC=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCCC as System.String
  <DBColumnName("CCC")>
  <DataMemberAttribute()>
   Public Overridable Property CCC() As System.String 
    Get 
      return  mCCC
    End Get 
    Set(value As System.String) 
      IF mCCC<>value THEN 
        mCCC=value
        me.objChanged=true
      END IF
    End Set 
        End Property

        Private mHasTPV As Boolean
        <DBColumnName("HasTPV")>
        <DataMemberAttribute()>
        Public Overridable Property HasTPV() As Boolean
            Get
                Return mHasTPV
            End Get
            Set(value As Boolean)
                If mHasTPV <> value Then
                    mHasTPV = value
                    Me.objChanged = True
                End If
            End Set
        End Property


    private memailConfirmacion as System.String
  <DBColumnName("emailConfirmacion")>
  <DataMemberAttribute()>
   Public Overridable Property emailConfirmacion() As System.String 
    Get 
      return  memailConfirmacion
    End Get 
    Set(value As System.String) 
      IF memailConfirmacion<>value THEN 
        memailConfirmacion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtxtConfirmacion as System.String
  <DBColumnName("txtConfirmacion")>
  <DataMemberAttribute()>
   Public Overridable Property txtConfirmacion() As System.String 
    Get 
      return  mtxtConfirmacion
    End Get 
    Set(value As System.String) 
      IF mtxtConfirmacion<>value THEN 
        mtxtConfirmacion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mlogo as System.String
  <DBColumnName("logo")>
  <DataMemberAttribute()>
   Public Overridable Property logo() As System.String 
    Get 
      return  mlogo
    End Get 
    Set(value As System.String) 
      IF mlogo<>value THEN 
        mlogo=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mdiasdeAntelacion as System.Byte
  <DBColumnName("diasdeAntelacion")>
  <DataMemberAttribute()>
   Public Overridable Property diasdeAntelacion() As System.Byte 
    Get 
      return  mdiasdeAntelacion
    End Get 
    Set(value As System.Byte) 
      IF mdiasdeAntelacion<>value THEN 
        mdiasdeAntelacion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtxtConfirmacionIngles as System.String
  <DBColumnName("txtConfirmacionIngles")>
  <DataMemberAttribute()>
   Public Overridable Property txtConfirmacionIngles() As System.String 
    Get 
      return  mtxtConfirmacionIngles
    End Get 
    Set(value As System.String) 
      IF mtxtConfirmacionIngles<>value THEN 
        mtxtConfirmacionIngles=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtxtConfirmacionFrances as System.String
  <DBColumnName("txtConfirmacionFrances")>
  <DataMemberAttribute()>
   Public Overridable Property txtConfirmacionFrances() As System.String 
    Get 
      return  mtxtConfirmacionFrances
    End Get 
    Set(value As System.String) 
      IF mtxtConfirmacionFrances<>value THEN 
        mtxtConfirmacionFrances=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtxtConfirmacionAleman as System.String
  <DBColumnName("txtConfirmacionAleman")>
  <DataMemberAttribute()>
   Public Overridable Property txtConfirmacionAleman() As System.String 
    Get 
      return  mtxtConfirmacionAleman
    End Get 
    Set(value As System.String) 
      IF mtxtConfirmacionAleman<>value THEN 
        mtxtConfirmacionAleman=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtxtConfirmacionItaliano as System.String
  <DBColumnName("txtConfirmacionItaliano")>
  <DataMemberAttribute()>
   Public Overridable Property txtConfirmacionItaliano() As System.String 
    Get 
      return  mtxtConfirmacionItaliano
    End Get 
    Set(value As System.String) 
      IF mtxtConfirmacionItaliano<>value THEN 
        mtxtConfirmacionItaliano=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtxtConfirmacionCatalan as System.String
  <DBColumnName("txtConfirmacionCatalan")>
  <DataMemberAttribute()>
   Public Overridable Property txtConfirmacionCatalan() As System.String 
    Get 
      return  mtxtConfirmacionCatalan
    End Get 
    Set(value As System.String) 
      IF mtxtConfirmacionCatalan<>value THEN 
        mtxtConfirmacionCatalan=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mTodoIncluido as System.Boolean
  <DBColumnName("TodoIncluido")>
  <DataMemberAttribute()>
   Public Overridable Property TodoIncluido() As System.Boolean 
    Get 
      return  mTodoIncluido
    End Get 
    Set(value As System.Boolean) 
      IF mTodoIncluido<>value THEN 
        mTodoIncluido=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mpolCatalan as System.String
  <DBColumnName("polCatalan")>
  <DataMemberAttribute()>
   Public Overridable Property polCatalan() As System.String 
    Get 
      return  mpolCatalan
    End Get 
    Set(value As System.String) 
      IF mpolCatalan<>value THEN 
        mpolCatalan=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mpolItaliano as System.String
  <DBColumnName("polItaliano")>
  <DataMemberAttribute()>
   Public Overridable Property polItaliano() As System.String 
    Get 
      return  mpolItaliano
    End Get 
    Set(value As System.String) 
      IF mpolItaliano<>value THEN 
        mpolItaliano=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mpolAleman as System.String
  <DBColumnName("polAleman")>
  <DataMemberAttribute()>
   Public Overridable Property polAleman() As System.String 
    Get 
      return  mpolAleman
    End Get 
    Set(value As System.String) 
      IF mpolAleman<>value THEN 
        mpolAleman=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mpolFrances as System.String
  <DBColumnName("polFrances")>
  <DataMemberAttribute()>
   Public Overridable Property polFrances() As System.String 
    Get 
      return  mpolFrances
    End Get 
    Set(value As System.String) 
      IF mpolFrances<>value THEN 
        mpolFrances=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mpolIngles as System.String
  <DBColumnName("polIngles")>
  <DataMemberAttribute()>
   Public Overridable Property polIngles() As System.String 
    Get 
      return  mpolIngles
    End Get 
    Set(value As System.String) 
      IF mpolIngles<>value THEN 
        mpolIngles=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mpolEsp as System.String
        <DBColumnName("polEsp")>
        <DataMemberAttribute()>
        Public Overridable Property polEsp() As System.String
            Get
                Return mpolEsp
            End Get
            Set(value As System.String)
                If mpolEsp <> value Then
                    mpolEsp = value
                    Me.objChanged = True
                End If
            End Set
        End Property


    private mtxtCancelEsp as System.String
  <DBColumnName("txtCancelEsp")>
  <DataMemberAttribute()>
   Public Overridable Property txtCancelEsp() As System.String 
    Get 
      return  mtxtCancelEsp
    End Get 
    Set(value As System.String) 
      IF mtxtCancelEsp<>value THEN 
        mtxtCancelEsp=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtxtCancelIngles as System.String
  <DBColumnName("txtCancelIngles")>
  <DataMemberAttribute()>
   Public Overridable Property txtCancelIngles() As System.String 
    Get 
      return  mtxtCancelIngles
    End Get 
    Set(value As System.String) 
      IF mtxtCancelIngles<>value THEN 
        mtxtCancelIngles=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtxtCancelFrances as System.String
  <DBColumnName("txtCancelFrances")>
  <DataMemberAttribute()>
   Public Overridable Property txtCancelFrances() As System.String 
    Get 
      return  mtxtCancelFrances
    End Get 
    Set(value As System.String) 
      IF mtxtCancelFrances<>value THEN 
        mtxtCancelFrances=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtxtCancelAleman as System.String
  <DBColumnName("txtCancelAleman")>
  <DataMemberAttribute()>
   Public Overridable Property txtCancelAleman() As System.String 
    Get 
      return  mtxtCancelAleman
    End Get 
    Set(value As System.String) 
      IF mtxtCancelAleman<>value THEN 
        mtxtCancelAleman=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtxtCancelItaliano as System.String
  <DBColumnName("txtCancelItaliano")>
  <DataMemberAttribute()>
   Public Overridable Property txtCancelItaliano() As System.String 
    Get 
      return  mtxtCancelItaliano
    End Get 
    Set(value As System.String) 
      IF mtxtCancelItaliano<>value THEN 
        mtxtCancelItaliano=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mtxtCancelCatalan as System.String
  <DBColumnName("txtCancelCatalan")>
  <DataMemberAttribute()>
   Public Overridable Property txtCancelCatalan() As System.String 
    Get 
      return  mtxtCancelCatalan
    End Get 
    Set(value As System.String) 
      IF mtxtCancelCatalan<>value THEN 
        mtxtCancelCatalan=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mformatoEmailConfirmacion as System.String
  <DBColumnName("formatoEmailConfirmacion")>
  <DataMemberAttribute()>
   Public Overridable Property formatoEmailConfirmacion() As System.String 
    Get 
      return  mformatoEmailConfirmacion
    End Get 
    Set(value As System.String) 
      IF mformatoEmailConfirmacion<>value THEN 
        mformatoEmailConfirmacion=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private memailConfirmacion2 as System.String
  <DBColumnName("emailConfirmacion2")>
  <DataMemberAttribute()>
   Public Overridable Property emailConfirmacion2() As System.String 
    Get 
      return  memailConfirmacion2
    End Get 
    Set(value As System.String) 
      IF memailConfirmacion2<>value THEN 
        memailConfirmacion2=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mmaxhab2see as System.Int16
  <DBColumnName("maxhab2see")>
  <DataMemberAttribute()>
   Public Overridable Property maxhab2see() As System.Int16 
    Get 
      return  mmaxhab2see
    End Get 
    Set(value As System.Int16) 
      IF mmaxhab2see<>value THEN 
        mmaxhab2see=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcomisionyahnormal as System.Double
  <DBColumnName("comisionyahnormal")>
  <DataMemberAttribute()>
   Public Overridable Property comisionyahnormal() As System.Double 
    Get 
      return  mcomisionyahnormal
    End Get 
    Set(value As System.Double) 
      IF mcomisionyahnormal<>value THEN 
        mcomisionyahnormal=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcomisionyahregistrados as System.Double
  <DBColumnName("comisionyahregistrados")>
  <DataMemberAttribute()>
   Public Overridable Property comisionyahregistrados() As System.Double 
    Get 
      return  mcomisionyahregistrados
    End Get 
    Set(value As System.Double) 
      IF mcomisionyahregistrados<>value THEN 
        mcomisionyahregistrados=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mEstado as System.Byte
  <DBColumnName("Estado")>
  <DataMemberAttribute()>
   Public Overridable Property Estado() As System.Byte 
    Get 
      return  mEstado
    End Get 
    Set(value As System.Byte) 
      IF mEstado<>value THEN 
        mEstado=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCCCIBAN as System.String
  <DBColumnName("CCCIBAN")>
  <DataMemberAttribute()>
   Public Overridable Property CCCIBAN() As System.String 
    Get 
      return  mCCCIBAN
    End Get 
    Set(value As System.String) 
      IF mCCCIBAN<>value THEN 
        mCCCIBAN=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCCCBIC as System.String
  <DBColumnName("CCCBIC")>
  <DataMemberAttribute()>
   Public Overridable Property CCCBIC() As System.String 
    Get 
      return  mCCCBIC
    End Get 
    Set(value As System.String) 
      IF mCCCBIC<>value THEN 
        mCCCBIC=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCCCBanco as System.String
  <DBColumnName("CCCBanco")>
  <DataMemberAttribute()>
   Public Overridable Property CCCBanco() As System.String 
    Get 
      return  mCCCBanco
    End Get 
    Set(value As System.String) 
      IF mCCCBanco<>value THEN 
        mCCCBanco=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCCCSucursal as System.String
  <DBColumnName("CCCSucursal")>
  <DataMemberAttribute()>
   Public Overridable Property CCCSucursal() As System.String 
    Get 
      return  mCCCSucursal
    End Get 
    Set(value As System.String) 
      IF mCCCSucursal<>value THEN 
        mCCCSucursal=value
        me.objChanged=true
      END IF
    End Set 
        End Property

        'GENERAL BOARD

        Private mboardAD As System.Double
        <DBColumnName("boardAD")>
        <DataMemberAttribute()>
        Public Overridable Property boardAD() As System.Double
            Get
                Return mboardAD
            End Get
            Set(value As System.Double)
                If mboardAD <> value Then
                    mboardAD = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mboardMP As System.Double
        <DBColumnName("boardMP")>
        <DataMemberAttribute()>
        Public Overridable Property boardMP() As System.Double
            Get
                Return mboardMP
            End Get
            Set(value As System.Double)
                If mboardMP <> value Then
                    mboardMP = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mboardPC As System.Double
        <DBColumnName("boardPC")>
        <DataMemberAttribute()>
        Public Overridable Property boardPC() As System.Double
            Get
                Return mboardPC
            End Get
            Set(value As System.Double)
                If mboardPC <> value Then
                    mboardPC = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mboardTI As System.Double
        <DBColumnName("boardTI")>
        <DataMemberAttribute()>
        Public Overridable Property boardTI() As System.Double
            Get
                Return mboardTI
            End Get
            Set(value As System.Double)
                If mboardTI <> value Then
                    mboardTI = value
                    Me.objChanged = True
                End If
            End Set
        End Property


#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const ID As String="ID" 'int
  Public Const cod_hotel As String="cod_hotel" 'int
  Public Const EdadMaxNinoGratis As String="EdadMaxNinoGratis" 'tinyint
  Public Const EdadMaxNinoPago As String="EdadMaxNinoPago" 'tinyint
            Public Const impuestos As String = "impuestos" 'float
  Public Const moneda As String="moneda" 'varchar
  Public Const Alojamiento As String="Alojamiento" 'bit
  Public Const AD As String="AD" 'bit
  Public Const MP As String="MP" 'bit
            Public Const PC As String = "PC" 'bit

  Public Const CCC As String="CCC" 'varchar
  Public Const emailConfirmacion As String="emailConfirmacion" 'varchar
  Public Const txtConfirmacion As String="txtConfirmacion" 'varchar
  Public Const logo As String="logo" 'varchar
  Public Const diasdeAntelacion As String="diasdeAntelacion" 'tinyint
  Public Const txtConfirmacionIngles As String="txtConfirmacionIngles" 'nvarchar
  Public Const txtConfirmacionFrances As String="txtConfirmacionFrances" 'nvarchar
  Public Const txtConfirmacionAleman As String="txtConfirmacionAleman" 'nvarchar
  Public Const txtConfirmacionItaliano As String="txtConfirmacionItaliano" 'nvarchar
  Public Const txtConfirmacionCatalan As String="txtConfirmacionCatalan" 'nvarchar
  Public Const TodoIncluido As String="TodoIncluido" 'bit
  Public Const polCatalan As String="polCatalan" 'varchar
  Public Const polItaliano As String="polItaliano" 'varchar
  Public Const polAleman As String="polAleman" 'varchar
  Public Const polFrances As String="polFrances" 'varchar
  Public Const polIngles As String="polIngles" 'varchar
  Public Const polEsp As String="polEsp" 'varchar
  Public Const txtCancelEsp As String="txtCancelEsp" 'varchar
  Public Const txtCancelIngles As String="txtCancelIngles" 'nvarchar
  Public Const txtCancelFrances As String="txtCancelFrances" 'nvarchar
  Public Const txtCancelAleman As String="txtCancelAleman" 'nvarchar
  Public Const txtCancelItaliano As String="txtCancelItaliano" 'nvarchar
  Public Const txtCancelCatalan As String="txtCancelCatalan" 'nvarchar
  Public Const formatoEmailConfirmacion As String="formatoEmailConfirmacion" 'varchar
  Public Const emailConfirmacion2 As String="emailConfirmacion2" 'varchar
  Public Const maxhab2see As String="maxhab2see" 'smallint
  Public Const comisionyahnormal As String="comisionyahnormal" 'float
  Public Const comisionyahregistrados As String="comisionyahregistrados" 'float
  Public Const Estado As String="Estado" 'tinyint
  Public Const CCCIBAN As String="CCCIBAN" 'varchar
  Public Const CCCBIC As String="CCCBIC" 'varchar
  Public Const CCCBanco As String="CCCBanco" 'varchar
            Public Const CCCSucursal As String = "CCCSucursal" 'varchar
            Public Const HasTPV As String = "HasTPV" 'bit
            Public Const boardAD As String = "boardAD" 'float
            Public Const boardMP As String = "boardMP" 'float
            Public Const boardPC As String = "boardPC" 'float
            Public Const boardTI As String = "boardTI" 'float
End Class  
#End Region

        Public Function FillByRow(ByRef dr As DataRow) As R_Res_Hot_Param
            Return FillObjectByRow(dr, Me)
        End Function

        Public Shared Function FillObjectByRow(ByRef dr As DataRow, Optional ByRef obj2Fill As R_Res_Hot_Param = Nothing) As R_Res_Hot_Param
            If obj2Fill Is Nothing Then
                obj2Fill = New R_Res_Hot_Param
            End If

            obj2Fill.cod_hotel = IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing, dr(DBCol.cod_hotel))
            obj2Fill.EdadMaxNinoGratis = IIf(IsDBNull(dr(DBCol.EdadMaxNinoGratis)), Nothing, dr(DBCol.EdadMaxNinoGratis))
            obj2Fill.EdadMaxNinoPago = IIf(IsDBNull(dr(DBCol.EdadMaxNinoPago)), Nothing, dr(DBCol.EdadMaxNinoPago))
            obj2Fill.impuestos = IIf(IsDBNull(dr(DBCol.impuestos)), Nothing, dr(DBCol.impuestos))
            obj2Fill.moneda = "" & dr(DBCol.moneda)
            obj2Fill.Alojamiento = IIf(IsDBNull(dr(DBCol.Alojamiento)), Nothing, dr(DBCol.Alojamiento))
            obj2Fill.AD = IIf(IsDBNull(dr(DBCol.AD)), Nothing, dr(DBCol.AD))
            obj2Fill.MP = IIf(IsDBNull(dr(DBCol.MP)), Nothing, dr(DBCol.MP))
            obj2Fill.PC = IIf(IsDBNull(dr(DBCol.PC)), Nothing, dr(DBCol.PC))
            obj2Fill.HasTPV = IIf(IsDBNull(dr(DBCol.HasTPV)), Nothing, dr(DBCol.HasTPV))
            obj2Fill.CCC = "" & dr(DBCol.CCC)
            obj2Fill.emailConfirmacion = "" & dr(DBCol.emailConfirmacion)
            obj2Fill.txtConfirmacion = "" & dr(DBCol.txtConfirmacion)
            obj2Fill.logo = "" & dr(DBCol.logo)
            obj2Fill.diasdeAntelacion = IIf(IsDBNull(dr(DBCol.diasdeAntelacion)), Nothing, dr(DBCol.diasdeAntelacion))
            obj2Fill.txtConfirmacionIngles = "" & dr(DBCol.txtConfirmacionIngles)
            obj2Fill.txtConfirmacionFrances = "" & dr(DBCol.txtConfirmacionFrances)
            obj2Fill.txtConfirmacionAleman = "" & dr(DBCol.txtConfirmacionAleman)
            obj2Fill.txtConfirmacionItaliano = "" & dr(DBCol.txtConfirmacionItaliano)
            obj2Fill.txtConfirmacionCatalan = "" & dr(DBCol.txtConfirmacionCatalan)
            obj2Fill.TodoIncluido = IIf(IsDBNull(dr(DBCol.TodoIncluido)), Nothing, dr(DBCol.TodoIncluido))
            obj2Fill.polCatalan = "" & dr(DBCol.polCatalan)
            obj2Fill.polItaliano = "" & dr(DBCol.polItaliano)
            obj2Fill.polAleman = "" & dr(DBCol.polAleman)
            obj2Fill.polFrances = "" & dr(DBCol.polFrances)
            obj2Fill.polIngles = "" & dr(DBCol.polIngles)
            obj2Fill.polEsp = "" & dr(DBCol.polEsp)
            obj2Fill.txtCancelEsp = "" & dr(DBCol.txtCancelEsp)
            obj2Fill.txtCancelIngles = "" & dr(DBCol.txtCancelIngles)
            obj2Fill.txtCancelFrances = "" & dr(DBCol.txtCancelFrances)
            obj2Fill.txtCancelAleman = "" & dr(DBCol.txtCancelAleman)
            obj2Fill.txtCancelItaliano = "" & dr(DBCol.txtCancelItaliano)
            obj2Fill.txtCancelCatalan = "" & dr(DBCol.txtCancelCatalan)
            obj2Fill.formatoEmailConfirmacion = "" & dr(DBCol.formatoEmailConfirmacion)
            obj2Fill.emailConfirmacion2 = "" & dr(DBCol.emailConfirmacion2)
            obj2Fill.maxhab2see = IIf(IsDBNull(dr(DBCol.maxhab2see)), Nothing, dr(DBCol.maxhab2see))
            obj2Fill.comisionyahnormal = IIf(IsDBNull(dr(DBCol.comisionyahnormal)), Nothing, dr(DBCol.comisionyahnormal))
            obj2Fill.comisionyahregistrados = IIf(IsDBNull(dr(DBCol.comisionyahregistrados)), Nothing, dr(DBCol.comisionyahregistrados))
            obj2Fill.Estado = IIf(IsDBNull(dr(DBCol.Estado)), Nothing, dr(DBCol.Estado))
            obj2Fill.CCCIBAN = "" & dr(DBCol.CCCIBAN)
            obj2Fill.CCCBIC = "" & dr(DBCol.CCCBIC)
            obj2Fill.CCCBanco = "" & dr(DBCol.CCCBanco)
            obj2Fill.CCCSucursal = "" & dr(DBCol.CCCSucursal)

            obj2Fill.boardAD = IIf(IsDBNull(dr(DBCol.boardAD)), Nothing, dr(DBCol.boardAD))
            obj2Fill.boardMP = IIf(IsDBNull(dr(DBCol.boardMP)), Nothing, dr(DBCol.boardMP))
            obj2Fill.boardPC = IIf(IsDBNull(dr(DBCol.boardPC)), Nothing, dr(DBCol.boardPC))
            obj2Fill.boardTI = IIf(IsDBNull(dr(DBCol.boardTI)), Nothing, dr(DBCol.boardTI))

            Return obj2Fill
        End Function
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_Res_Hot_ParamCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_Res_Hot_Param
     Get
         Return DirectCast(List(index), R_Res_Hot_Param)
     End Get
     Set(value As R_Res_Hot_Param)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_Res_Hot_Param)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_Res_Hot_Param)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
