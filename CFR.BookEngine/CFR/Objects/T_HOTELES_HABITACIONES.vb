
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
    Public Class T_Hoteles_Habitaciones
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


        Private mHotel As System.Int32
        ''' <summary>
        ''' hotel code (cod_hotel)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <DBColumnName("Hotel")>
        <DataMemberAttribute()>
        Public Overridable Property Hotel() As System.Int32
            Get
                Return mHotel
            End Get
            Set(value As System.Int32)
                If mHotel <> value Then
                    mHotel = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcodhabitacionhotel As System.Int32
        ''' <summary>
        ''' room code 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <DBColumnName("codhabitacionhotel")>
        <DataMemberAttribute()>
        Public Overridable Property codhabitacionhotel() As System.Int32 Implements IBObase.ID
            Get
                Return mcodhabitacionhotel
            End Get
            Set(value As System.Int32)
                If mcodhabitacionhotel <> value Then
                    mcodhabitacionhotel = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mhabitacionhotel As System.String
        <DBColumnName("habitacionhotel")>
        <DataMemberAttribute()>
        Public Overridable Property habitacionhotel() As System.String
            Get
                Return mhabitacionhotel
            End Get
            Set(value As System.String)
                If mhabitacionhotel <> value Then
                    mhabitacionhotel = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcodhabitacionGenerica As System.Int32
        <DBColumnName("codhabitacionGenerica")>
        <DataMemberAttribute()>
        Public Overridable Property codhabitacionGenerica() As System.Int32
            Get
                Return mcodhabitacionGenerica
            End Get
            Set(value As System.Int32)
                If mcodhabitacionGenerica <> value Then
                    mcodhabitacionGenerica = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mdescripcion As System.String
        <DBColumnName("descripcion")>
        <DataMemberAttribute()>
        Public Overridable Property descripcion() As System.String
            Get
                Return mdescripcion
            End Get
            Set(value As System.String)
                If mdescripcion <> value Then
                    mdescripcion = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mAdultos As System.Byte
        <DBColumnName("Adultos")>
        <DataMemberAttribute()>
        Public Overridable Property Adultos() As System.Byte
            Get
                Return mAdultos
            End Get
            Set(value As System.Byte)
                If mAdultos <> value Then
                    mAdultos = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mAdultosExtra1 As System.Byte
        <DBColumnName("AdultosExtra1")>
        <DataMemberAttribute()>
        Public Overridable Property AdultosExtra1() As System.Byte
            Get
                Return mAdultosExtra1
            End Get
            Set(value As System.Byte)
                If mAdultosExtra1 <> value Then
                    mAdultosExtra1 = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mAdultosExtra2 As System.Byte
        <DBColumnName("AdultosExtra2")>
        <DataMemberAttribute()>
        Public Overridable Property AdultosExtra2() As System.Byte
            Get
                Return mAdultosExtra2
            End Get
            Set(value As System.Byte)
                If mAdultosExtra2 <> value Then
                    mAdultosExtra2 = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mMaxPersonas As System.Byte
        <DBColumnName("MaxPersonas")>
        <DataMemberAttribute()>
        Public Overridable Property MaxPersonas() As System.Byte
            Get
                Return mMaxPersonas
            End Get
            Set(value As System.Byte)
                If mMaxPersonas <> value Then
                    mMaxPersonas = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mNumNino As System.Byte
        <DBColumnName("NumNino")>
        <DataMemberAttribute()>
        Public Overridable Property NumNino() As System.Byte
            Get
                Return mNumNino
            End Get
            Set(value As System.Byte)
                If mNumNino <> value Then
                    mNumNino = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mMaxAdultos As System.Byte
        <DBColumnName("MaxAdultos")>
        <DataMemberAttribute()>
        Public Overridable Property MaxAdultos() As System.Byte
            Get
                Return mMaxAdultos
            End Get
            Set(value As System.Byte)
                If mMaxAdultos <> value Then
                    mMaxAdultos = value
                    Me.objChanged = True
                End If
            End Set
        End Property

        Private mnumRoomsAvailable As System.Int32
        <DBColumnName("numRoomsAvailable")>
        <DataMemberAttribute()>
        Public Overridable Property numRoomsAvailable() As Integer
            Get
                Return mnumRoomsAvailable
            End Get
            Set(value As Integer)
                If mnumRoomsAvailable <> value Then
                    mnumRoomsAvailable = value
                    Me.objChanged = True
                End If
            End Set
        End Property

        Private mServiciosEsp As System.String
        <DBColumnName("ServiciosEsp")>
        <DataMemberAttribute()>
        Public Overridable Property ServiciosEsp() As System.String
            Get
                Return mServiciosEsp
            End Get
            Set(value As System.String)
                If mServiciosEsp <> value Then
                    mServiciosEsp = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mServiciosIngles As System.String
        <DBColumnName("ServiciosIngles")>
        <DataMemberAttribute()>
        Public Overridable Property ServiciosIngles() As System.String
            Get
                Return mServiciosIngles
            End Get
            Set(value As System.String)
                If mServiciosIngles <> value Then
                    mServiciosIngles = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mServiciosFrances As System.String
        <DBColumnName("ServiciosFrances")>
        <DataMemberAttribute()>
        Public Overridable Property ServiciosFrances() As System.String
            Get
                Return mServiciosFrances
            End Get
            Set(value As System.String)
                If mServiciosFrances <> value Then
                    mServiciosFrances = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mServiciosAleman As System.String
        <DBColumnName("ServiciosAleman")>
        <DataMemberAttribute()>
        Public Overridable Property ServiciosAleman() As System.String
            Get
                Return mServiciosAleman
            End Get
            Set(value As System.String)
                If mServiciosAleman <> value Then
                    mServiciosAleman = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mServiciosItaliano As System.String
        <DBColumnName("ServiciosItaliano")>
        <DataMemberAttribute()>
        Public Overridable Property ServiciosItaliano() As System.String
            Get
                Return mServiciosItaliano
            End Get
            Set(value As System.String)
                If mServiciosItaliano <> value Then
                    mServiciosItaliano = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mServiciosCatalan As System.String
        <DBColumnName("ServiciosCatalan")>
        <DataMemberAttribute()>
        Public Overridable Property ServiciosCatalan() As System.String
            Get
                Return mServiciosCatalan
            End Get
            Set(value As System.String)
                If mServiciosCatalan <> value Then
                    mServiciosCatalan = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcod_hab_combinada As System.Int32
        <DBColumnName("cod_hab_combinada")>
        <DataMemberAttribute()>
        Public Overridable Property cod_hab_combinada() As System.Int32
            Get
                Return mcod_hab_combinada
            End Get
            Set(value As System.Int32)
                If mcod_hab_combinada <> value Then
                    mcod_hab_combinada = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mNinoOcupaplaza As System.Byte
        <DBColumnName("NinoOcupaplaza")>
        <DataMemberAttribute()>
        Public Overridable Property NinoOcupaplaza() As System.Byte
            Get
                Return mNinoOcupaplaza
            End Get
            Set(value As System.Byte)
                If mNinoOcupaplaza <> value Then
                    mNinoOcupaplaza = value
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


        Private mdesc_ca As System.String
        <DBColumnName("desc_ca")>
        <DataMemberAttribute()>
        Public Overridable Property desc_ca() As System.String
            Get
                Return mdesc_ca
            End Get
            Set(value As System.String)
                If mdesc_ca <> value Then
                    mdesc_ca = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const Hotel As String = "Hotel" 'int
            Public Const codhabitacionhotel As String = "codhabitacionhotel" 'int
            Public Const habitacionhotel As String = "habitacionhotel" 'nvarchar
            Public Const codhabitacionGenerica As String = "codhabitacionGenerica" 'int
            Public Const descripcion As String = "descripcion" 'nvarchar
            Public Const Adultos As String = "Adultos" 'tinyint
            Public Const AdultosExtra1 As String = "AdultosExtra1" 'tinyint
            Public Const AdultosExtra2 As String = "AdultosExtra2" 'tinyint
            Public Const MaxPersonas As String = "MaxPersonas" 'tinyint
            Public Const NumNino As String = "NumNino" 'tinyint
            Public Const MaxAdultos As String = "MaxAdultos" 'tinyint
            Public Const numRoomsAvailable As String = "numRoomsAvailable" 'tinyint
            Public Const ServiciosEsp As String = "ServiciosEsp" 'varchar
            Public Const ServiciosIngles As String = "ServiciosIngles" 'varchar
            Public Const ServiciosFrances As String = "ServiciosFrances" 'varchar
            Public Const ServiciosAleman As String = "ServiciosAleman" 'varchar
            Public Const ServiciosItaliano As String = "ServiciosItaliano" 'varchar
            Public Const ServiciosCatalan As String = "ServiciosCatalan" 'varchar
            Public Const cod_hab_combinada As String = "cod_hab_combinada" 'int
            Public Const NinoOcupaplaza As String = "NinoOcupaplaza" 'tinyint
            Public Const desc_en As String = "desc_en" 'varchar
            Public Const desc_de As String = "desc_de" 'varchar
            Public Const desc_fr As String = "desc_fr" 'varchar
            Public Const desc_it As String = "desc_it" 'varchar
            Public Const desc_ca As String = "desc_ca" 'varchar
            Public Const HabitacionCombinadaNombre As String = "HabitacionCombinadaNombre" 'name of combined room in stored proc [T_HOTELES_HABITACIONES_GetByCodhotel]
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_Hoteles_Habitaciones) As T_Hoteles_Habitaciones
            If obj2Fill Is Nothing Then
                obj2Fill = New T_Hoteles_Habitaciones
            End If

            obj2Fill.Hotel = IIf(IsDBNull(dr(DBCol.Hotel)), Nothing, dr(DBCol.Hotel))
            obj2Fill.codhabitacionhotel = IIf(IsDBNull(dr(DBCol.codhabitacionhotel)), Nothing, dr(DBCol.codhabitacionhotel))
            obj2Fill.habitacionhotel = "" & dr(DBCol.habitacionhotel)
            obj2Fill.codhabitacionGenerica = IIf(IsDBNull(dr(DBCol.codhabitacionGenerica)), Nothing, dr(DBCol.codhabitacionGenerica))
            obj2Fill.descripcion = "" & dr(DBCol.descripcion)
            obj2Fill.Adultos = IIf(IsDBNull(dr(DBCol.Adultos)), Nothing, dr(DBCol.Adultos))
            obj2Fill.AdultosExtra1 = IIf(IsDBNull(dr(DBCol.AdultosExtra1)), Nothing, dr(DBCol.AdultosExtra1))
            obj2Fill.AdultosExtra2 = IIf(IsDBNull(dr(DBCol.AdultosExtra2)), Nothing, dr(DBCol.AdultosExtra2))
            obj2Fill.MaxPersonas = IIf(IsDBNull(dr(DBCol.MaxPersonas)), Nothing, dr(DBCol.MaxPersonas))
            obj2Fill.NumNino = IIf(IsDBNull(dr(DBCol.NumNino)), Nothing, dr(DBCol.NumNino))
            obj2Fill.MaxAdultos = IIf(IsDBNull(dr(DBCol.MaxAdultos)), Nothing, dr(DBCol.MaxAdultos))
            obj2Fill.numRoomsAvailable = IIf(IsDBNull(dr(DBCol.numRoomsAvailable)), Nothing, dr(DBCol.numRoomsAvailable))
            obj2Fill.ServiciosEsp = "" & dr(DBCol.ServiciosEsp)
            obj2Fill.ServiciosIngles = "" & dr(DBCol.ServiciosIngles)
            obj2Fill.ServiciosFrances = "" & dr(DBCol.ServiciosFrances)
            obj2Fill.ServiciosAleman = "" & dr(DBCol.ServiciosAleman)
            obj2Fill.ServiciosItaliano = "" & dr(DBCol.ServiciosItaliano)
            obj2Fill.ServiciosCatalan = "" & dr(DBCol.ServiciosCatalan)
            obj2Fill.cod_hab_combinada = IIf(IsDBNull(dr(DBCol.cod_hab_combinada)), Nothing, dr(DBCol.cod_hab_combinada))
            obj2Fill.NinoOcupaplaza = IIf(IsDBNull(dr(DBCol.NinoOcupaplaza)), Nothing, dr(DBCol.NinoOcupaplaza))
            obj2Fill.desc_en = "" & dr(DBCol.desc_en)
            obj2Fill.desc_de = "" & dr(DBCol.desc_de)
            obj2Fill.desc_fr = "" & dr(DBCol.desc_fr)
            obj2Fill.desc_it = "" & dr(DBCol.desc_it)
            obj2Fill.desc_ca = "" & dr(DBCol.desc_ca)
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class T_HOTELES_HABITACIONESCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As T_Hoteles_Habitaciones
            Get
                Return DirectCast(List(index), T_Hoteles_Habitaciones)
            End Get
            Set(value As T_Hoteles_Habitaciones)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As T_Hoteles_Habitaciones)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As T_Hoteles_Habitaciones)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
