Imports System.Diagnostics
Imports Microsoft.VisualBasic


''' <summary>
''' Base page for book pages
''' </summary>
''' <remarks></remarks>
Public Class BookBasePage
    Inherits cfr.pages.SiteBasePage

#Region "Variables"
    ''' <summary>
    ''' check if there is bookres object. If not then is new session
    ''' </summary>
    ''' <remarks></remarks>
    Public isNewSession As Boolean = True
    ''' <summary>
    ''' object with all the information about the booking in process
    ''' </summary>
    ''' <remarks></remarks>
    Private bookDataVar As cfr.BookRes

    ''' <summary>
    ''' object with all the information about the booking in process
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property bookData() As cfr.BookRes
        Get
            Return Me.bookDataVar
        End Get
        Set(ByVal value As cfr.BookRes)
            Me.bookDataVar = value
        End Set
    End Property

    Public ReadOnly Property holdprop As cfr.obj.Hotel
        Get
            Return Me.bookDataVar.hotel
        End Get
    End Property



#End Region



    ''' <summary>
    ''' load base data for actual request
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub loadBaseData()
        Try

            Dim cod_hotel As String = ""

            Me.bookData = cfr.book.UserHelper.getBookSession(Me.Context)

            If Me.bookData Is Nothing Then
                Me.isNewSession = True
            Else
                Me.isNewSession = False
            End If

            'get hotel/property code
            cod_hotel = Request.QueryString(cfr.REQCte.query.cod_hotel)
            If String.IsNullOrWhiteSpace(cod_hotel) Then
                cod_hotel = Request.Form(cfr.REQCte.query.cod_hotel)
            End If
            
            If Me.IsPostBack = False Then
                If cod_hotel = "" Then
                    'there is no info about hotel
                    Response.Redirect(cfr.NamePage.defaultpage)
                End If
            Else
                If cod_hotel = "" Then
                    cod_hotel = Me.hotelID_ViewState 'from viewstate
                Else
                    Me.hotelID_ViewState = cod_hotel
                End If
            End If

            'check again cod_hotel
            If cod_hotel = "" Then
                'there is no info about hotel
                Response.Redirect(cfr.NamePage.defaultpage)
            End If

            'force update
            Me.configBookingData(cod_hotel)

            'ok we have one hotel/property selected. Load hotel configuration and set to bookdata. 
            'If Me.bookData Is Nothing Then
            '    Debug.WriteLine("LOADING HOTEL DATA DUE TO SESION EMPTY")
            '    Me.configBookingData(cod_hotel)
            'Else
            '    If cod_hotel <> Me.bookData.hotel.id Then 'new hotel
            '        Debug.WriteLine("RELOADING HOTEL DATA FOR DIFERENT REQUEST")
            '        Me.configBookingData(CInt(cod_hotel))
            '    End If
            'End If

            Me.bookData.language = cfr.Language.getLanguage

            'reload language based on hotel type. If type is hotel nothing to do, but if its a car then we need to load car language
            'hotel/property type for language
            Me.langCaption.holdproperty = Me.bookData.hotel.Tipo


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Config the basic data needed to show the information.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function configBookingData(ByVal cod_hotel As Integer) As Boolean

        'hotel
        Dim hotelSelected As cfr.obj.Hotel = cfr.mgr.HotelMgr.GetEntityFullRefreshCache(cod_hotel)
        If hotelSelected Is Nothing Then
            Throw New cfr.exceptions.BookException("Property id not exists.")
        End If

        'bookdata
        Dim bd As New cfr.BookRes(Me.Session.SessionID)
        bd.language = cfr.Language.getLanguage
        bd.hotel = hotelSelected
        bd.bookOperator = hotelSelected.PrivateoperatorObj

        'hotel/property type for language
        Me.langCaption.holdproperty = hotelSelected.Tipo

        'site
        bd.site = New cfr.SiteObj
        If Me.siteWeb.id.ToString = bd.hotel.id.ToString Then
            bd.site.id = bd.hotel.id
            bd.site.description = bd.hotel.hotelName
            bd.site.getTheme()
        End If

        Me.bookData = bd
        Session(cfr.REQCte.session.book) = Me.bookData

    End Function




End Class
