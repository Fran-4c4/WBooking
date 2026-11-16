Imports Microsoft.VisualBasic

Namespace cfr.obj



    <Serializable()> _
    Public Class Hotel
        Inherits ind.bk.obj.T_HOTELES

        Public Property id() As Integer
            Get
                Return Me.cod_Hotel
            End Get
            Set(ByVal value As Integer)
                Me.cod_Hotel = value
            End Set
        End Property

        Public Overridable Property hotelName() As System.String
            Get
                Return hotel
            End Get
            Set(value As System.String)
                Me.hotel = value
            End Set
        End Property

        Public city As cfr.obj.City
        'Public cod_Categoria As cfr.obj.Category.categoryEnum
        Public lastAccess As Date
        ''' <summary>
        ''' unit basic configuration parameters
        ''' </summary>
        ''' <remarks></remarks>
        Public BookParam As cfr.obj.HotelBookParameters
        Public chain As Chain

        Public description As HotelDescription
        Public photos As System.Collections.Generic.Dictionary(Of Integer, cfr.obj.Photo)
        Public services As Data.DataTable
        Public locations As Data.DataTable
        Public saloons As Data.DataTable
        Public tpvData As ind.bk.obj.R_TPVDATA

        Private mPrivateoperatorObj As OperatorObj
        ''' <summary>
        ''' Private operator is the default operator for hotel when user is not logged
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PrivateoperatorObj As OperatorObj
            Get
                If Me.mPrivateoperatorObj Is Nothing Then
                    Me.getOperatorPrivateGeneric()
                End If
                Return Me.mPrivateoperatorObj
            End Get

            Set(value As OperatorObj)
                Me.mPrivateoperatorObj = value
            End Set
        End Property



        Private mfreesaleoperatorObj As OperatorObj
        ''' <summary>
        ''' Is the freesale operator for cupos free of asignement
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property freesaleOperatorObj As OperatorObj
            Get
                If Me.mfreesaleoperatorObj Is Nothing Then
                    Me.getOperatorFreeSale()
                End If
                Return Me.mfreesaleoperatorObj
            End Get

            Set(value As OperatorObj)
                Me.mfreesaleoperatorObj = value
            End Set
        End Property


        Public Sub New()
            Me.BookParam = New cfr.obj.HotelBookParameters
            Me.chain = New Chain
            Me.chain.code = 0
            Me.chain.description = "Independent"
            Me.city = New City
            Me.city.idEntity = 0
            Me.city.desc_esp = ""
            Me.description = New HotelDescription
            Me.photos = New System.Collections.Generic.Dictionary(Of Integer, cfr.obj.Photo)

        End Sub

        ''' <summary>
        ''' get the operator free sale for this property and holds in internal var
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getOperatorFreeSale() As OperatorObj
            If Me.id <= 0 Then
                Return Nothing
            End If
            Dim om As New cfr.OperadorManager
            Me.mfreesaleoperatorObj = om.getOperatorFreeSaleGeneric(Me.id)
            Return Me.mfreesaleoperatorObj
        End Function


        ''' <summary>
        ''' get the operator private generic for this property and holds in internal var
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getOperatorPrivateGeneric() As OperatorObj
            If Me.id <= 0 Then
                Return Nothing
            End If
            Dim om As New cfr.OperadorManager
            Me.mPrivateoperatorObj = om.getOperatorPrivateGeneric(Me.id)
            Return Me.mPrivateoperatorObj
        End Function

        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As ind.bk.obj.T_HOTELES) As ind.bk.obj.T_HOTELES
            If obj2Fill Is Nothing Then
                obj2Fill = New ind.bk.obj.T_HOTELES
            End If

            obj2Fill.cod_Hotel = IIf(IsDBNull(dr(DBCol.cod_Hotel)), Nothing, dr(DBCol.cod_Hotel))
            obj2Fill.Tipo = IIf(IsDBNull(dr(DBCol.Tipo)), 1, dr(DBCol.Tipo)) ' "" & dr(DBCol.Tipo)
            obj2Fill.hotel = "" & dr(DBCol.hotel)
            obj2Fill.Direccion = "" & dr(DBCol.Direccion)
            obj2Fill.PrefijoTelf = "" & dr(DBCol.PrefijoTelf)
            obj2Fill.Telefono = "" & dr(DBCol.Telefono)
            obj2Fill.Fax = "" & dr(DBCol.Fax)
            obj2Fill.cod_Categoria = IIf(IsDBNull(dr(DBCol.cod_Categoria)), Nothing, dr(DBCol.cod_Categoria))
            If dr.Table.Columns.Contains(DBCol.codpostal) Then
                obj2Fill.codpostal = "" & dr(DBCol.codpostal)
            ElseIf dr.Table.Columns.Contains("postalcode") Then
                obj2Fill.codpostal = "" & dr("postalcode")
            ElseIf dr.Table.Columns.Contains("pstalcode") Then
                obj2Fill.codpostal = "" & dr("postalcode")
            End If


            'obj2Fill.desc_cat = IIf(IsDBNull(dr(DBCol.desc_cat)), Nothing, dr(DBCol.desc_cat))
            obj2Fill.Cadena1 = IIf(IsDBNull(dr(DBCol.Cadena1)), Nothing, dr(DBCol.Cadena1))
            obj2Fill.Email = "" & dr(DBCol.Email)
            obj2Fill.Web = "" & dr(DBCol.Web)
            obj2Fill.Habitaciones = IIf(IsDBNull(dr(DBCol.Habitaciones)), Nothing, dr(DBCol.Habitaciones))

            obj2Fill.Datos = IIf(IsDBNull(dr(DBCol.Datos)), Nothing, dr(DBCol.Datos))
            obj2Fill.Director = "" & dr(DBCol.Director)
            If dr.Table.Columns.Contains(DBCol.Directorcomercial) Then
                obj2Fill.Directorcomercial = "" & dr(DBCol.Directorcomercial)
            ElseIf dr.Table.Columns.Contains("Director comercial") Then
                obj2Fill.Directorcomercial = "" & dr("director comercial")
            End If


            obj2Fill.nfotos = IIf(IsDBNull(dr(DBCol.nfotos)), Nothing, dr(DBCol.nfotos))
            obj2Fill.f1 = "" & dr(DBCol.f1)
            obj2Fill.Estado = IIf(IsDBNull(dr(DBCol.Estado)), Nothing, dr(DBCol.Estado))
            obj2Fill.login = "" & dr(DBCol.login)
            obj2Fill.clave = "" & dr(DBCol.clave)
            obj2Fill.rowId = IIf(IsDBNull(dr(DBCol.rowId)), Nothing, dr(DBCol.rowId))
            obj2Fill.latitud = IIf(IsDBNull(dr(DBCol.latitud)), Nothing, dr(DBCol.latitud))
            obj2Fill.longitud = IIf(IsDBNull(dr(DBCol.longitud)), Nothing, dr(DBCol.longitud))
            obj2Fill.lastUpdate = IIf(IsDBNull(dr(DBCol.lastUpdate)), Nothing, dr(DBCol.lastUpdate))


            obj2Fill.desc_es = IIf(IsDBNull(dr(DBCol.desc_es)), Nothing, dr(DBCol.desc_es))
            obj2Fill.desc_en = IIf(IsDBNull(dr(DBCol.desc_en)), Nothing, dr(DBCol.desc_en))
            obj2Fill.desc_de = IIf(IsDBNull(dr(DBCol.desc_de)), Nothing, dr(DBCol.desc_de))
            obj2Fill.desc_fr = IIf(IsDBNull(dr(DBCol.desc_fr)), Nothing, dr(DBCol.desc_fr))
            obj2Fill.desc_it = IIf(IsDBNull(dr(DBCol.desc_it)), Nothing, dr(DBCol.desc_it))
            obj2Fill.desc_pt = IIf(IsDBNull(dr(DBCol.desc_pt)), Nothing, dr(DBCol.desc_pt))
            If dr.Table.Columns.Contains(DBCol.desc_jp) Then
                obj2Fill.desc_jp = IIf(IsDBNull(dr(DBCol.desc_jp)), Nothing, dr(DBCol.desc_jp))
            ElseIf dr.Table.Columns.Contains("desc_ja") Then
                obj2Fill.desc_jp = IIf(IsDBNull(dr("desc_ja")), Nothing, dr("desc_ja"))
            End If



            obj2Fill.currencycode = "" & dr(DBCol.currencycode)
            obj2Fill.MNGProp = "" & dr(DBCol.MNGProp)
            obj2Fill.UltimoAcceso = IIf(IsDBNull(dr(DBCol.UltimoAcceso)), Nothing, dr(DBCol.UltimoAcceso))
            obj2Fill.UltimoAcceso = IIf(IsDBNull(dr(DBCol.UltimoAcceso)), Nothing, dr(DBCol.UltimoAcceso))
            obj2Fill.checkintime1 = IIf(IsDBNull(dr(DBCol.checkintime1)), "12:00", dr(DBCol.checkintime1))
            obj2Fill.checkintime2 = IIf(IsDBNull(dr(DBCol.checkintime2)), "", dr(DBCol.checkintime2))
            obj2Fill.checkOuttime1 = IIf(IsDBNull(dr(DBCol.checkOuttime1)), "", dr(DBCol.checkOuttime1))
            obj2Fill.checkOuttime2 = IIf(IsDBNull(dr(DBCol.checkOuttime2)), "11:00", dr(DBCol.checkOuttime2))
            Return obj2Fill
        End Function

    End Class


End Namespace