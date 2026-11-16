
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
Public Class R_operadores
      inherits objBaseClass
      Implements IBObase



Public Sub New()
    MyBase.New()
End Sub

#Region "Properties and Fields"


    private mcod_operador as System.Int32
  <DBColumnName("cod_operador")>
  <DataMemberAttribute()>
   Public Overridable Property cod_operador() As System.Int32  Implements IBObase.ID
    Get 
      return  mcod_operador
    End Get 
            Set(value As System.Int32)
                If mcod_operador <> value Then
                    mcod_operador = value
                    Me.objChanged = True
                End If
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


    private mOperador as System.String
  <DBColumnName("Operador")>
  <DataMemberAttribute()>
   Public Overridable Property Operador() As System.String 
    Get 
      return  mOperador
    End Get 
    Set(value As System.String) 
      IF mOperador<>value THEN 
        mOperador=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mFreeSale as System.Byte
  <DBColumnName("FreeSale")>
  <DataMemberAttribute()>
   Public Overridable Property FreeSale() As System.Byte 
    Get 
      return  mFreeSale
    End Get 
    Set(value As System.Byte) 
      IF mFreeSale<>value THEN 
        mFreeSale=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcomision as System.Double
  <DBColumnName("comision")>
  <DataMemberAttribute()>
   Public Overridable Property comision() As System.Double 
    Get 
      return  mcomision
    End Get 
    Set(value As System.Double) 
      IF mcomision<>value THEN 
        mcomision=value
        me.objChanged=true
      END IF
    End Set 
        End Property



        




    private mforma_pago as System.Int32
  <DBColumnName("forma_pago")>
  <DataMemberAttribute()>
   Public Overridable Property forma_pago() As System.Int32 
    Get 
      return  mforma_pago
    End Get 
    Set(value As System.Int32) 
      IF mforma_pago<>value THEN 
        mforma_pago=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mpolitica as System.String
        <DBColumnName("politica")>
        <DataMemberAttribute()>
        Public Overridable Property politica() As System.String
            Get
                Return mpolitica
            End Get
            Set(value As System.String)
                If mpolitica <> value Then
                    mpolitica = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mRelease As System.Byte
        <DBColumnName("Release")>
        <DataMemberAttribute()>
        Public Overridable Property Release() As System.Byte
            Get
                Return mRelease
            End Get
            Set(value As System.Byte)
                If mRelease <> value Then
                    mRelease = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mlogin As System.String
        <DBColumnName("login")>
        <DataMemberAttribute()>
        Public Overridable Property login() As System.String
            Get
                Return mlogin
            End Get
            Set(value As System.String)
                If mlogin <> value Then
                    mlogin = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mClave As System.String
        <DBColumnName("Clave")>
        <DataMemberAttribute()>
        Public Overridable Property Clave() As System.String
            Get
                Return mClave
            End Get
            Set(value As System.String)
                If mClave <> value Then
                    mClave = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mEmail As System.String
        <DBColumnName("Email")>
        <DataMemberAttribute()>
        Public Overridable Property Email() As System.String
            Get
                Return mEmail
            End Get
            Set(value As System.String)
                If mEmail <> value Then
                    mEmail = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mN_T_Credito As System.String
        <DBColumnName("N_T_Credito")>
        <DataMemberAttribute()>
        Public Overridable Property N_T_Credito() As System.String
            Get
                Return mN_T_Credito
            End Get
            Set(value As System.String)
                If mN_T_Credito <> value Then
                    mN_T_Credito = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mCaducidadTarjeta As System.String
        <DBColumnName("CaducidadTarjeta")>
        <DataMemberAttribute()>
        Public Overridable Property CaducidadTarjeta() As System.String
            Get
                Return mCaducidadTarjeta
            End Get
            Set(value As System.String)
                If mCaducidadTarjeta <> value Then
                    mCaducidadTarjeta = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mTitularTarjeta As System.String
        <DBColumnName("TitularTarjeta")>
        <DataMemberAttribute()>
        Public Overridable Property TitularTarjeta() As System.String
            Get
                Return mTitularTarjeta
            End Get
            Set(value As System.String)
                If mTitularTarjeta <> value Then
                    mTitularTarjeta = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcod_CRS As System.Int16
        <DBColumnName("cod_CRS")>
        <DataMemberAttribute()>
        Public Overridable Property cod_CRS() As System.Int16
            Get
                Return mcod_CRS
            End Get
            Set(value As System.Int16)
                If mcod_CRS <> value Then
                    mcod_CRS = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mpolIngles As System.String
        <DBColumnName("polIngles")>
        <DataMemberAttribute()>
        Public Overridable Property polIngles() As System.String
            Get
                Return mpolIngles
            End Get
            Set(value As System.String)
                If mpolIngles <> value Then
                    mpolIngles = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mpolFrances As System.String
        <DBColumnName("polFrances")>
        <DataMemberAttribute()>
        Public Overridable Property polFrances() As System.String
            Get
                Return mpolFrances
            End Get
            Set(value As System.String)
                If mpolFrances <> value Then
                    mpolFrances = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mpolAleman As System.String
        <DBColumnName("polAleman")>
        <DataMemberAttribute()>
        Public Overridable Property polAleman() As System.String
            Get
                Return mpolAleman
            End Get
            Set(value As System.String)
                If mpolAleman <> value Then
                    mpolAleman = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mpolItaliano As System.String
        <DBColumnName("polItaliano")>
        <DataMemberAttribute()>
        Public Overridable Property polItaliano() As System.String
            Get
                Return mpolItaliano
            End Get
            Set(value As System.String)
                If mpolItaliano <> value Then
                    mpolItaliano = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mpolCatalan As System.String
        <DBColumnName("polCatalan")>
        <DataMemberAttribute()>
        Public Overridable Property polCatalan() As System.String
            Get
                Return mpolCatalan
            End Get
            Set(value As System.String)
                If mpolCatalan <> value Then
                    mpolCatalan = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mtipo As System.Byte
        <DBColumnName("tipo")>
        <DataMemberAttribute()>
        Public Overridable Property tipo() As System.Byte
            Get
                Return mtipo
            End Get
            Set(value As System.Byte)
                If mtipo <> value Then
                    mtipo = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mCOD_OPERADOR_hotel As System.String
        <DBColumnName("COD_OPERADOR_hotel")>
        <DataMemberAttribute()>
        Public Overridable Property COD_OPERADOR_hotel() As System.String
            Get
                Return mCOD_OPERADOR_hotel
            End Get
            Set(value As System.String)
                If mCOD_OPERADOR_hotel <> value Then
                    mCOD_OPERADOR_hotel = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mCOD_Agencia_Grupo As System.Int32
        <DBColumnName("COD_Agencia_Grupo")>
        <DataMemberAttribute()>
        Public Overridable Property COD_Agencia_Grupo() As System.Int32
            Get
                Return mCOD_Agencia_Grupo
            End Get
            Set(value As System.Int32)
                If mCOD_Agencia_Grupo <> value Then
                    mCOD_Agencia_Grupo = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcod_Cadena As System.Int32
        <DBColumnName("cod_Cadena")>
        <DataMemberAttribute()>
        Public Overridable Property cod_Cadena() As System.Int32
            Get
                Return mcod_Cadena
            End Get
            Set(value As System.Int32)
                If mcod_Cadena <> value Then
                    mcod_Cadena = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mgenerico As System.Byte
        <DBColumnName("generico")>
        <DataMemberAttribute()>
        Public Overridable Property generico() As System.Byte
            Get
                Return mgenerico
            End Get
            Set(value As System.Byte)
                If mgenerico <> value Then
                    mgenerico = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mmaxdiasdereserva As System.Int16
        <DBColumnName("maxdiasdereserva")>
        <DataMemberAttribute()>
        Public Overridable Property maxdiasdereserva() As System.Int16
            Get
                Return mmaxdiasdereserva
            End Get
            Set(value As System.Int16)
                If mmaxdiasdereserva <> value Then
                    mmaxdiasdereserva = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private maplicarYMprecio As System.Boolean
        <DBColumnName("aplicarYMprecio")>
        <DataMemberAttribute()>
        Public Overridable Property aplicarYMprecio() As System.Boolean
            Get
                Return maplicarYMprecio
            End Get
            Set(value As System.Boolean)
                If maplicarYMprecio <> value Then
                    maplicarYMprecio = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private maplicarYMdiasmin As System.Boolean
        <DBColumnName("aplicarYMdiasmin")>
        <DataMemberAttribute()>
        Public Overridable Property aplicarYMdiasmin() As System.Boolean
            Get
                Return maplicarYMdiasmin
            End Get
            Set(value As System.Boolean)
                If maplicarYMdiasmin <> value Then
                    maplicarYMdiasmin = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mModifyDate As System.DateTime
        <DBColumnName("ModifyDate")>
        <DataMemberAttribute()>
        Public Overridable Property ModifyDate() As System.DateTime
            Get
                Return mModifyDate
            End Get
            Set(value As System.DateTime)
                If mModifyDate <> value Then
                    mModifyDate = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mdiscount As System.Double
        <DBColumnName("discount")>
        <DataMemberAttribute()>
        Public Overridable Property discount() As System.Double
            Get
                Return mdiscount
            End Get
            Set(value As System.Double)
                If mdiscount <> value Then
                    mdiscount = value
                    Me.objChanged = True
                End If
            End Set
        End Property

        Private mdiscountapply As Boolean
        <DBColumnName("discountapply")>
        <DataMemberAttribute()>
        Public Overridable Property discountapply() As Boolean
            Get
                Return mdiscountapply
            End Get
            Set(value As Boolean)
                If mdiscountapply <> value Then
                    mdiscountapply = value
                    Me.objChanged = True
                End If
            End Set
        End Property

        Private mdiscountapplyfrom As Date
        <DBColumnName("discountapplyfrom")>
        <DataMemberAttribute()>
        Public Overridable Property discountapplyfrom() As Date
            Get
                Return mdiscountapplyfrom
            End Get
            Set(value As Date)
                If mdiscountapplyfrom <> value Then
                    mdiscountapplyfrom = value
                    Me.objChanged = True
                End If
            End Set
        End Property

        Private mdiscountapplyuntil As Date
        <DBColumnName("discountapplyuntil")>
        <DataMemberAttribute()>
        Public Overridable Property discountapplyuntil() As Date
            Get
                Return mdiscountapplyuntil
            End Get
            Set(value As Date)
                If mdiscountapplyuntil <> value Then
                    mdiscountapplyuntil = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mActive As System.Boolean
        <DBColumnName("Active")>
        <DataMemberAttribute()>
        Public Overridable Property Active() As System.Boolean
            Get
                Return mActive
            End Get
            Set(value As System.Boolean)
                If mActive <> value Then
                    mActive = value
                    Me.objChanged = True
                End If
            End Set
        End Property

        Private mdiscountNoRefund As System.Boolean
        <DBColumnName("discountNoRefund")>
        <DataMemberAttribute()>
        Public Overridable Property discountNoRefund() As System.Boolean
            Get
                Return mdiscountNoRefund
            End Get
            Set(value As System.Boolean)
                If mdiscountNoRefund <> value Then
                    mdiscountNoRefund = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mdiscountNotApplyWithNoRefundTariff As System.Boolean
        <DBColumnName("discountNotApplyWithNoRefundTariff")>
        <DataMemberAttribute()>
        Public Overridable Property discountNotApplyWithNoRefundTariff() As System.Boolean
            Get
                Return mdiscountNotApplyWithNoRefundTariff
            End Get
            Set(value As System.Boolean)
                If mdiscountNotApplyWithNoRefundTariff <> value Then
                    mdiscountNotApplyWithNoRefundTariff = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const cod_operador As String = "cod_operador" 'int
            Public Const cod_hotel As String = "cod_hotel" 'int
            Public Const Operador As String = "Operador" 'nvarchar
            Public Const FreeSale As String = "FreeSale" 'tinyint
            Public Const comision As String = "comision" 'float
            Public Const forma_pago As String = "forma_pago" 'int
            Public Const politica As String = "politica" 'nvarchar
            Public Const Release As String = "Release" 'tinyint
            Public Const login As String = "login" 'nvarchar
            Public Const Clave As String = "Clave" 'nvarchar
            Public Const Email As String = "Email" 'nvarchar
            Public Const N_T_Credito As String = "N_T_Credito" 'varchar
            Public Const CaducidadTarjeta As String = "CaducidadTarjeta" 'varchar
            Public Const TitularTarjeta As String = "TitularTarjeta" 'nvarchar
            Public Const cod_CRS As String = "cod_CRS" 'smallint
            Public Const polIngles As String = "polIngles" 'nvarchar
            Public Const polFrances As String = "polFrances" 'nvarchar
            Public Const polAleman As String = "polAleman" 'nvarchar
            Public Const polItaliano As String = "polItaliano" 'nvarchar
            Public Const polCatalan As String = "polCatalan" 'nvarchar
            Public Const tipo As String = "tipo" 'tinyint
            Public Const COD_OPERADOR_hotel As String = "COD_OPERADOR_hotel" 'char
            Public Const COD_Agencia_Grupo As String = "COD_Agencia_Grupo" 'int
            Public Const cod_Cadena As String = "cod_Cadena" 'int
            Public Const generico As String = "generico" 'tinyint
            Public Const maxdiasdereserva As String = "maxdiasdereserva" 'smallint
            Public Const aplicarYMprecio As String = "aplicarYMprecio" 'bit
            Public Const aplicarYMdiasmin As String = "aplicarYMdiasmin" 'bit
            Public Const ModifyDate As String = "ModifyDate" 'datetime
            Public Const discount As String = "discount" 'float
            Public Const discountapply As String = "discountapply" 'bit
            Public Const discountapplyfrom As String = "discountapplyfrom" 'datetime
            Public Const discountapplyuntil As String = "discountapplyuntil" 'datetime
            Public Const Active As String = "Active" 'bit
            Public Const discountNoRefund As String = "discountNoRefund" 'bit
            Public Const discountNotApplyWithNoRefundTariff As String = "discountNotApplyWithNoRefundTariff" 'bit
        End Class
#End Region

        Public Function FillByRow(ByRef dr As DataRow) As R_operadores

            Return FillObjectByRow(dr, Me)

        End Function
        Public Function FillByRowView(ByRef drv As DataRowView) As R_operadores

            Return FillObjectByRow(drv.row, Me)

        End Function
        Public Shared Function FillObjectByRowView(ByRef drv As DataRowView, Optional ByRef obj2Fill As R_operadores = Nothing) As R_operadores

            Return FillObjectByRow(drv.row, obj2Fill)

        End Function
        Public Shared Function FillObjectByRow(ByRef dr As DataRow, Optional ByRef obj2Fill As R_operadores = Nothing) As R_operadores
            If obj2Fill Is Nothing Then
                obj2Fill = New R_operadores
            End If

            obj2Fill.cod_operador = IIf(IsDBNull(dr(DBCol.cod_operador)), Nothing, dr(DBCol.cod_operador))
            obj2Fill.cod_hotel = IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing, dr(DBCol.cod_hotel))
            obj2Fill.Operador = "" & dr(DBCol.Operador)
            obj2Fill.FreeSale = IIf(IsDBNull(dr(DBCol.FreeSale)), Nothing, dr(DBCol.FreeSale))
            obj2Fill.comision = IIf(IsDBNull(dr(DBCol.comision)), Nothing, dr(DBCol.comision))
            obj2Fill.forma_pago = IIf(IsDBNull(dr(DBCol.forma_pago)), Nothing, dr(DBCol.forma_pago))
            obj2Fill.politica = "" & dr(DBCol.politica)
            obj2Fill.Release = IIf(IsDBNull(dr(DBCol.Release)), Nothing, dr(DBCol.Release))
            obj2Fill.login = "" & dr(DBCol.login)
            obj2Fill.Clave = "" & dr(DBCol.Clave)
            obj2Fill.Email = "" & dr(DBCol.Email)
            obj2Fill.N_T_Credito = "" & dr(DBCol.N_T_Credito)
            obj2Fill.CaducidadTarjeta = "" & dr(DBCol.CaducidadTarjeta)
            obj2Fill.TitularTarjeta = "" & dr(DBCol.TitularTarjeta)
            obj2Fill.cod_CRS = IIf(IsDBNull(dr(DBCol.cod_CRS)), Nothing, dr(DBCol.cod_CRS))
            obj2Fill.polIngles = "" & dr(DBCol.polIngles)
            obj2Fill.polFrances = "" & dr(DBCol.polFrances)
            obj2Fill.polAleman = "" & dr(DBCol.polAleman)
            obj2Fill.polItaliano = "" & dr(DBCol.polItaliano)
            obj2Fill.polCatalan = "" & dr(DBCol.polCatalan)
            obj2Fill.tipo = IIf(IsDBNull(dr(DBCol.tipo)), Nothing, dr(DBCol.tipo))
            obj2Fill.COD_OPERADOR_hotel = "" & dr(DBCol.COD_OPERADOR_hotel)
            obj2Fill.COD_Agencia_Grupo = IIf(IsDBNull(dr(DBCol.COD_Agencia_Grupo)), Nothing, dr(DBCol.COD_Agencia_Grupo))
            obj2Fill.cod_Cadena = IIf(IsDBNull(dr(DBCol.cod_Cadena)), Nothing, dr(DBCol.cod_Cadena))
            obj2Fill.generico = IIf(IsDBNull(dr(DBCol.generico)), Nothing, dr(DBCol.generico))
            obj2Fill.maxdiasdereserva = IIf(IsDBNull(dr(DBCol.maxdiasdereserva)), Nothing, dr(DBCol.maxdiasdereserva))
            obj2Fill.aplicarYMprecio = IIf(IsDBNull(dr(DBCol.aplicarYMprecio)), Nothing, dr(DBCol.aplicarYMprecio))
            obj2Fill.aplicarYMdiasmin = IIf(IsDBNull(dr(DBCol.aplicarYMdiasmin)), Nothing, dr(DBCol.aplicarYMdiasmin))
            obj2Fill.ModifyDate = IIf(IsDBNull(dr(DBCol.ModifyDate)), Nothing, dr(DBCol.ModifyDate))

            obj2Fill.discount = IIf(IsDBNull(dr(DBCol.discount)), Nothing, dr(DBCol.discount))
            obj2Fill.discountapply = IIf(IsDBNull(dr(DBCol.discountapply)), Nothing, dr(DBCol.discountapply))
            obj2Fill.discountapplyfrom = IIf(IsDBNull(dr(DBCol.discountapplyfrom)), Nothing, dr(DBCol.discountapplyfrom))
            obj2Fill.discountapplyuntil = IIf(IsDBNull(dr(DBCol.discountapplyuntil)), Nothing, dr(DBCol.discountapplyuntil))
            obj2Fill.Active = IIf(IsDBNull(dr(DBCol.Active)), Nothing, dr(DBCol.Active))
            obj2Fill.discountNoRefund = IIf(IsDBNull(dr(DBCol.discountNoRefund)), Nothing, dr(DBCol.discountNoRefund))
            obj2Fill.discountNotApplyWithNoRefundTariff = IIf(IsDBNull(dr(DBCol.discountNotApplyWithNoRefundTariff)), Nothing, dr(DBCol.discountNotApplyWithNoRefundTariff))
            obj2Fill.objChanged = False 'reset fill
            Return obj2Fill
        End Function
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_operadoresCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_operadores
     Get
         Return DirectCast(List(index), R_operadores)
     End Get
     Set(value As R_operadores)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_operadores)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_operadores)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
