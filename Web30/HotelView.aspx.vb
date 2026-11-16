Imports System.Data


Partial Class HotelView
    Inherits cfr.pages.SiteBasePage

#Region "fields"
    Public aloj As cfr.obj.Hotel
    Public idhotel() As String

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.idhotel = cfr.Language.getIdiomaHotelView(cfr.Language.getLanguage)
            Dim cod_hotel As Long = Request.QueryString(cfr.REQCte.query.cod_hotel)
            Me.aloj = cfr.mgr.HotelMgr.GetEntityFull(cod_hotel)
            Me.lnkBook1.NavigateUrl = String.Format("~/b_Request.aspx?{0}={1}", cfr.REQCte.query.cod_hotel, cod_hotel)
            Me.lnkBook2.NavigateUrl = Me.lnkBook1.NavigateUrl
        End If
    End Sub




#Region "Converted"

    Public Sub showServic(ByVal services As DataTable, ByVal titCab As String, ByVal tipo As Integer)
        Dim swserv As String
        swserv = ""
        Dim dvServices As New DataView(services)
        dvServices.rowfilter = "tipo_carac=" & tipo
        If dvServices.Count = 0 Then
            Exit Sub
        End If
        Response.Write(String.Format("<h4>{0}</h4>", Server.HtmlEncode(titCab)))
        Response.Write("<div class='hotelitems'>" & vbCr)
        For Each drv As DataRowView In dvServices
            Response.Write("<span class='hitem'>" & Server.HtmlEncode(Convert.ToString(drv("servicio"))) & "</span>")
        Next
        Response.Write("</div>" & vbCr)
    End Sub

    Public Sub noInfo(ByVal titCab As Object)
        Response.Write("<h3>")
        Response.Write(Server.HtmlEncode(Convert.ToString(titCab)))
        Response.Write("</h3>" & vbCr)
        Response.Write(Server.HtmlEncode(idhotel(28)))
    End Sub


    Public Sub showDataInColumns(ByVal dvToShow As DataView, ByVal titCab As String, ByVal columnName As String)
        Dim strdato As String
        Response.Write("    <h4>")
        Response.Write(Server.HtmlEncode(titCab))
        Response.Write("</h4>" & vbCr)
        Response.Write("    <div class=tab1 style=""display:table;position:relative;width:100%;background-color:#F5F5F5;"">" & vbCr)
        If dvToShow.Count = 0 Then
            Response.Write(Server.HtmlEncode(idhotel(16)) & "<br>")
        Else
            '
            For Each drv As DataRowView In dvToShow
                strdato = drv(columnName)
                Response.Write("<div class='litem'>" & Server.HtmlEncode(strdato) & "</div>")
            Next
        End If
        'situaciones
        Response.Write("    </div>" & vbCr)
    End Sub

    Public Function GetPositionString() As String
        Dim strdato As String = ""
        Dim dvToShow As DataView = aloj.locations.DefaultView
        If dvToShow.Count = 0 Then
            strdato = idhotel(16)
        Else
            Dim liststr As String = "<ul class=""hotelsituationlist"">{0}</ul>"

            For Each drv As DataRowView In dvToShow
                strdato += String.Format("<li class=""hotelloc_li{0}"">{1}</li>", CInt(drv("cod_Situacion")), Server.HtmlEncode(Convert.ToString(drv("Situacion"))))
            Next
            liststr = String.Format(liststr, strdato)
            Return liststr
        End If
        Return strdato
    End Function

    Private Function getlocationicon(locationid As Integer) As String
        Select Case locationid
            Case 6             '1ª Linea de la Playa
            Case 24            '1ª Linea de Mar
            Case 9             'Aeropuerto
            Case 16            'Afueras de la Ciudad
            Case 27            'C. de Autopista
            Case 7             'C. de la Playa
            Case 26            'C. del Lago
            Case 21            'Campo de Golf
            Case 25            'Casco Urbano
            Case 4             'Centro
            Case 20            'Est. De Autobús
            Case 13            'Estación de Tren
            Case 22            'Hotel de Carretera
            Case 19            'Montaña
            Case 23            'Sitio Pintoresco
            Case 8             'Z. Comercial
            Case 15            'Z. de Negocios
            Case 18            'Z. Ferial
            Case 11            'Z. Natural
            Case 12            'Z. Residencial

        End Select
    End Function

    Public Function GetServicesString(tipo As Integer) As String
        Dim strdato As String = ""
        Dim dvServices As New DataView(aloj.services)
        dvServices.RowFilter = "tipo_carac=" & tipo

        Dim dvToShow As DataView = dvServices
        If dvToShow.Count = 0 Then
            strdato = idhotel(16)
        Else
            For Each drv As DataRowView In dvToShow
                strdato += drv("servicio") & ", "
            Next

        End If
        Return strdato
    End Function

#End Region


End Class
