
Imports System.Data

Partial Class hoteles
    Inherits cfr.pages.SiteBasePage


    Public dtSearchedHotels As Data.DataView
    Public hotelsAvailables As New System.Collections.Generic.Dictionary(Of Integer, hotelsSearchedForView)

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        If Not Me.Page.IsPostBack Then
            Me.findHotels()

        End If

    End Sub



    Private Sub findHotels()
        Dim drOfertas As New cfr.BookRes
        drOfertas = Me.getResBookOfertas()

        Dim dv As Data.DataView = cfr.AvailabilityManager.getHotelWithAvailability(drOfertas).DefaultView



        For Each drv As DataRowView In dv
            Dim h As New hotelsSearchedForView
            h.hotel = cfr.mgr.HotelMgr.GetEntity(drv("cod_hotel"))
            h.minimum_price = drv("MinimumPrice")
            hotelsAvailables.Add(h.hotel.id, h)
        Next
    End Sub

    Public Function getResBookOfertas() As cfr.BookRes
        Dim dr As New cfr.BookRes
        dr.setDates(Date.Now, Date.Now.AddDays(3))

        'dr.searchCriteria.categoria = ""
        'dr.searchCriteria.servicios = ""
        'dr.searchCriteria.city.idEntity = 0
        'dr.searchCriteria.codZona = 0
        'dr.searchCriteria.country.idEntity = 0

        dr.ninosfree = 0
        dr.ninospago = 0
        dr.numAdultos = 1
        dr.regimen = 0
        dr.language = cfr.Language.getLanguage

        Return dr
    End Function



    Public Class hotelsSearchedForView
        Public hotel As cfr.obj.Hotel
        Public minimum_price As Double
    End Class

End Class
