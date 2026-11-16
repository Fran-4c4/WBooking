
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
    Public Class T_HOTELES
        Inherits objBaseClass
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


        Private mcod_Hotel As System.Int32
        <DBColumnName("cod_Hotel")>
        <DataMemberAttribute()>
        Public Overridable Property cod_Hotel() As System.Int32 Implements IBObase.ID
            Get
                Return mcod_Hotel
            End Get
            Set(value As System.Int32)
                If mcod_Hotel <> value Then
                    mcod_Hotel = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mTipo As Byte
        <DBColumnName("Tipo")>
        <DataMemberAttribute()>
        Public Overridable Property Tipo() As cfr.CFRConst.unitTypeEnum
            Get
                Return mTipo
            End Get
            Set(value As cfr.CFRConst.unitTypeEnum)
                If mTipo <> value Then
                    mTipo = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mhotel As System.String
        ''' <summary>
        ''' Hotel name
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <DBColumnName("hotel")>
        <DataMemberAttribute()>
        <Obsolete("Use hotelname instead")>
        Public Overridable Property hotel() As System.String
            Get
                Return mhotel
            End Get
            Set(value As System.String)
                If mhotel <> value Then
                    mhotel = value
                    Me.objChanged = True
                End If
            End Set
        End Property





        Private mDireccion As System.String
        <DBColumnName("Direccion")>
        <DataMemberAttribute()>
        Public Overridable Property Direccion() As System.String
            Get
                Return mDireccion
            End Get
            Set(value As System.String)
                If mDireccion <> value Then
                    mDireccion = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mPrefijoTelf As System.String
        <DBColumnName("PrefijoTelf")>
        <DataMemberAttribute()>
        Public Overridable Property PrefijoTelf() As System.String
            Get
                Return mPrefijoTelf
            End Get
            Set(value As System.String)
                If mPrefijoTelf <> value Then
                    mPrefijoTelf = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mTelefono As System.String
        <DBColumnName("Telefono")>
        <DataMemberAttribute()>
        Public Overridable Property Telefono() As System.String
            Get
                Return mTelefono
            End Get
            Set(value As System.String)
                If mTelefono <> value Then
                    mTelefono = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mFax As System.String
        <DBColumnName("Fax")>
        <DataMemberAttribute()>
        Public Overridable Property Fax() As System.String
            Get
                Return mFax
            End Get
            Set(value As System.String)
                If mFax <> value Then
                    mFax = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcod_Ciudad As System.Int32
        <DBColumnName("cod_Ciudad")>
        <DataMemberAttribute()>
        Public Overridable Property cod_Ciudad() As System.Int32
            Get
                Return mcod_Ciudad
            End Get
            Set(value As System.Int32)
                If mcod_Ciudad <> value Then
                    mcod_Ciudad = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mCod_Pais As System.Int32
        <DBColumnName("Cod_Pais")>
        <DataMemberAttribute()>
        Public Overridable Property Cod_Pais() As System.Int32
            Get
                Return mCod_Pais
            End Get
            Set(value As System.Int32)
                If mCod_Pais <> value Then
                    mCod_Pais = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcod_Categoria As System.Int32
        <DBColumnName("cod_Categoria")>
        <DataMemberAttribute()>
        Public Overridable Property cod_Categoria() As System.Int32
            Get
                Return mcod_Categoria
            End Get
            Set(value As System.Int32)
                If mcod_Categoria <> value Then
                    mcod_Categoria = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcodpostal As System.String
        <DBColumnName("codpostal")>
        <DataMemberAttribute()>
        Public Overridable Property codpostal() As System.String
            Get
                Return mcodpostal
            End Get
            Set(value As System.String)
                If mcodpostal <> value Then
                    mcodpostal = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mdesc_es As System.String
        <DBColumnName("desc_es")>
        <DataMemberAttribute()>
        Public Overridable Property desc_es() As System.String
            Get
                Return mdesc_es
            End Get
            Set(value As System.String)
                If mdesc_es <> value Then
                    mdesc_es = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mdesc_en As System.String
        <DBColumnName("desc_en")>
        <DataMemberAttribute()>
        Public Overridable Property desc_en() As System.String
            Get
                Return mdesc_en
            End Get
            Set(value As System.String)
                If mdesc_en <> value Then
                    mdesc_en = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mdesc_de As System.String
        <DBColumnName("desc_de")>
        <DataMemberAttribute()>
        Public Overridable Property desc_de() As System.String
            Get
                Return mdesc_de
            End Get
            Set(value As System.String)
                If mdesc_de <> value Then
                    mdesc_de = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mdesc_fr As System.String
        <DBColumnName("desc_fr")>
        <DataMemberAttribute()>
        Public Overridable Property desc_fr() As System.String
            Get
                Return mdesc_fr
            End Get
            Set(value As System.String)
                If mdesc_fr <> value Then
                    mdesc_fr = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mdesc_cat As System.String
        <DBColumnName("desc_cat")>
        <DataMemberAttribute()>
        Public Overridable Property desc_cat() As System.String
            Get
                Return mdesc_cat
            End Get
            Set(value As System.String)
                If mdesc_cat <> value Then
                    mdesc_cat = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mCadena1 As System.Int32
        <DBColumnName("Cadena1")>
        <DataMemberAttribute()>
        Public Overridable Property Cadena1() As System.Int32
            Get
                Return mCadena1
            End Get
            Set(value As System.Int32)
                If mCadena1 <> value Then
                    mCadena1 = value
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


        Private mWeb As System.String
        <DBColumnName("Web")>
        <DataMemberAttribute()>
        Public Overridable Property Web() As System.String
            Get
                Return mWeb
            End Get
            Set(value As System.String)
                If mWeb <> value Then
                    mWeb = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mHabitaciones As System.Int16
        <DBColumnName("Habitaciones")>
        <DataMemberAttribute()>
        Public Overridable Property Habitaciones() As System.Int16
            Get
                Return mHabitaciones
            End Get
            Set(value As System.Int16)
                If mHabitaciones <> value Then
                    mHabitaciones = value
                    Me.objChanged = True
                End If
            End Set
        End Property






        Private mDatos As System.Boolean
        <DBColumnName("Datos")>
        <DataMemberAttribute()>
        Public Overridable Property Datos() As System.Boolean
            Get
                Return mDatos
            End Get
            Set(value As System.Boolean)
                If mDatos <> value Then
                    mDatos = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDirector As System.String
        <DBColumnName("Director")>
        <DataMemberAttribute()>
        Public Overridable Property Director() As System.String
            Get
                Return mDirector
            End Get
            Set(value As System.String)
                If mDirector <> value Then
                    mDirector = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDirectorcomercial As System.String
        <DBColumnName("Directorcomercial")>
        <DataMemberAttribute()>
        Public Overridable Property Directorcomercial() As System.String
            Get
                Return mDirectorcomercial
            End Get
            Set(value As System.String)
                If mDirectorcomercial <> value Then
                    mDirectorcomercial = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mnfotos As System.Int16
        <DBColumnName("nfotos")>
        <DataMemberAttribute()>
        Public Overridable Property nfotos() As System.Int16
            Get
                Return mnfotos
            End Get
            Set(value As System.Int16)
                If mnfotos <> value Then
                    mnfotos = value
                    Me.objChanged = True
                End If
            End Set
        End Property



        Private mf1 As System.String
        <DBColumnName("f1")>
        <DataMemberAttribute()>
        Public Overridable Property f1() As System.String
            Get
                Return mf1
            End Get
            Set(value As System.String)
                If mf1 <> value Then
                    mf1 = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mEstado As System.Int32
        <DBColumnName("Estado")>
        <DataMemberAttribute()>
        Public Overridable Property Estado() As System.Int32
            Get
                Return mEstado
            End Get
            Set(value As System.Int32)
                If mEstado <> value Then
                    mEstado = value
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


        Private mclave As System.String
        <DBColumnName("clave")>
        <DataMemberAttribute()>
        Public Overridable Property clave() As System.String
            Get
                Return mclave
            End Get
            Set(value As System.String)
                If mclave <> value Then
                    mclave = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mrowId As System.Int32
        <DBColumnName("rowId")>
        <DataMemberAttribute()>
        Public Overridable Property rowId() As System.Int32
            Get
                Return mrowId
            End Get
            Set(value As System.Int32)
                If mrowId <> value Then
                    mrowId = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mlatitud As System.Double
        <DBColumnName("latitud")>
        <DataMemberAttribute()>
        Public Overridable Property latitud() As System.Double
            Get
                Return mlatitud
            End Get
            Set(value As System.Double)
                If mlatitud <> value Then
                    mlatitud = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mlongitud As System.Double
        <DBColumnName("longitud")>
        <DataMemberAttribute()>
        Public Overridable Property longitud() As System.Double
            Get
                Return mlongitud
            End Get
            Set(value As System.Double)
                If mlongitud <> value Then
                    mlongitud = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mlastUpdate As System.DateTime
        <DBColumnName("lastUpdate")>
        <DataMemberAttribute()>
        Public Overridable Property lastUpdate() As System.DateTime
            Get
                Return mlastUpdate
            End Get
            Set(value As System.DateTime)
                If mlastUpdate <> value Then
                    mlastUpdate = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mdesc_it As System.String
        <DBColumnName("desc_it")>
        <DataMemberAttribute()>
        Public Overridable Property desc_it() As System.String
            Get
                Return mdesc_it
            End Get
            Set(value As System.String)
                If mdesc_it <> value Then
                    mdesc_it = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mdesc_pt As System.String
        <DBColumnName("desc_pt")>
        <DataMemberAttribute()>
        Public Overridable Property desc_pt() As System.String
            Get
                Return mdesc_pt
            End Get
            Set(value As System.String)
                If mdesc_pt <> value Then
                    mdesc_pt = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mdesc_jp As System.String
        <DBColumnName("desc_jp")>
        <DataMemberAttribute()>
        Public Overridable Property desc_jp() As System.String
            Get
                Return mdesc_jp
            End Get
            Set(value As System.String)
                If mdesc_jp <> value Then
                    mdesc_jp = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcurrencycode As System.String
        <DBColumnName("currencycode")>
        <DataMemberAttribute()>
        Public Overridable Property currencycode() As System.String
            Get
                Return mcurrencycode
            End Get
            Set(value As System.String)
                If mcurrencycode <> value Then
                    mcurrencycode = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mMNGProp As System.String
        <DBColumnName("MNGProp")>
        <DataMemberAttribute()>
        Public Overridable Property MNGProp() As System.String
            Get
                Return mMNGProp
            End Get
            Set(value As System.String)
                If mMNGProp <> value Then
                    mMNGProp = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mUltimoAcceso As System.DateTime
        <DBColumnName("UltimoAcceso")>
        <DataMemberAttribute()>
        Public Overridable Property UltimoAcceso() As System.DateTime
            Get
                Return mUltimoAcceso
            End Get
            Set(value As System.DateTime)
                If mUltimoAcceso <> value Then
                    mUltimoAcceso = value
                    Me.objChanged = True
                End If
            End Set
        End Property

        Private mcheckintime1 As System.String
        <DBColumnName("checkintime1")>
        <DataMemberAttribute()>
        Public Overridable Property checkintime1() As System.String
            Get
                Return mcheckintime1
            End Get
            Set(value As System.String)
                If mcheckintime1 <> value Then
                    mcheckintime1 = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcheckintime2 As System.String
        <DBColumnName("checkintime2")>
        <DataMemberAttribute()>
        Public Overridable Property checkintime2() As System.String
            Get
                Return mcheckintime2
            End Get
            Set(value As System.String)
                If mcheckintime2 <> value Then
                    mcheckintime2 = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcheckOuttime1 As System.String
        <DBColumnName("checkOuttime1")>
        <DataMemberAttribute()>
        Public Overridable Property checkOuttime1() As System.String
            Get
                Return mcheckOuttime1
            End Get
            Set(value As System.String)
                If mcheckOuttime1 <> value Then
                    mcheckOuttime1 = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcheckOuttime2 As System.String
        <DBColumnName("checkOuttime2")>
        <DataMemberAttribute()>
        Public Overridable Property checkOuttime2() As System.String
            Get
                Return mcheckOuttime2
            End Get
            Set(value As System.String)
                If mcheckOuttime2 <> value Then
                    mcheckOuttime2 = value
                    Me.objChanged = True
                End If
            End Set
        End Property
#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const cod_Hotel As String = "cod_Hotel" 'int
            Public Const Tipo As String = "Tipo" 'nvarchar
            Public Const hotel As String = "hotel" 'nvarchar
            Public Const Direccion As String = "Direccion" 'nvarchar
            Public Const PrefijoTelf As String = "PrefijoTelf" 'nvarchar
            Public Const Telefono As String = "Telefono" 'nvarchar
            Public Const Fax As String = "Fax" 'nvarchar
            Public Const cod_Categoria As String = "cod_Categoria" 'int
            Public Const codpostal As String = "codpostal" 'nvarchar
            Public Const postalcode As String = "postalcode" 'nvarchar
            'todo:Eliminar cuando actualizar la BBDD
            Public Const cod_postal As String = "cod_postal" 'nvarchar
            Public Const desc_es As String = "desc_es" 'ntext
            Public Const desc_en As String = "desc_en" 'ntext
            Public Const desc_de As String = "desc_de" 'ntext
            Public Const desc_fr As String = "desc_fr" 'ntext
            Public Const desc_cat As String = "desc_cat" 'ntext
            Public Const desc_it As String = "desc_it" 'ntext
            Public Const desc_pt As String = "desc_pt" 'ntext
            Public Const desc_jp As String = "desc_jp" 'ntext

            Public Const Cadena1 As String = "cadena1" 'int

            Public Const Email As String = "Email" 'nvarchar
            Public Const Web As String = "Web" 'nvarchar
            Public Const Habitaciones As String = "Habitaciones" 'smallint
            Public Const Datos As String = "Datos" 'bit
            Public Const Director As String = "Director" 'nvarchar
            Public Const Directorcomercial As String = "Directorcomercial" 'nvarchar
            Public Const nfotos As String = "nfotos" 'smallint
            Public Const f1 As String = "f1" 'char
            Public Const Estado As String = "Estado" 'int
            Public Const login As String = "login" 'nvarchar
            Public Const clave As String = "clave" 'nvarchar
            Public Const rowId As String = "rowId" 'int
            Public Const latitud As String = "latitud" 'float
            Public Const longitud As String = "longitud" 'float
            Public Const lastUpdate As String = "lastUpdate" 'datetime

            Public Const currencycode As String = "currencycode" 'varchar
            Public Const MNGProp As String = "MNGProp" 'varchar
            Public Const UltimoAcceso As String = "lastaccess" 'datetime
            Public Const checkintime1 As String = "checkintime1" 'tinyint
            Public Const checkintime2 As String = "checkintime2" 'tinyint
            Public Const checkOuttime1 As String = "checkOuttime1" 'tinyint
            Public Const checkOuttime2 As String = "checkOuttime2" 'tinyint
        End Class
#End Region


        
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class T_HOTELESCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As T_HOTELES
            Get
                Return DirectCast(List(index), T_HOTELES)
            End Get
            Set(value As T_HOTELES)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As T_HOTELES)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As T_HOTELES)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
