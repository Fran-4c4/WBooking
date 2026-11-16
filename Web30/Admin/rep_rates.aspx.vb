
Imports System.Data

Partial Class Admin_rep_rates
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P19_Informes_de_Tarifas

    Friend dvrep As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.informes)
            PropertyUIshow.hideByType(Me.hotel.Tipo, Me)
    
            If Not Page.IsPostBack Then
                Me.configCbos()
            Else

            End If

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Sub

    Private Sub configCbos()
        Dim dvTariffs As DataView = getDvTarifas()

        Me.lsttariffs.DataSource = dvTariffs
        Me.lsttariffs.DataValueField = ind.bk.obj.R_tarifas.DBCol.cod_tarifa
        Me.lsttariffs.DataTextField = ind.bk.obj.R_tarifas.DBCol.tarifa
        Me.lsttariffs.DataBind()



    End Sub

    Friend Function getDvTarifas() As DataView

        Dim tableRes As New ind.bk.obj.R_tarifas_Manager

        Dim dv As DataView = tableRes.getDtTariffByIDHotel(Me.codhotel).DefaultView
        dv.Sort = ind.bk.obj.R_tarifas.DBCol.tarifa
        Return dv

    End Function


    Protected Sub btnviewreport_Click(sender As Object, e As EventArgs) Handles btnviewreport.Click
        viewReport()
    End Sub

    Protected Sub lsttariffs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lsttariffs.SelectedIndexChanged
        viewReport()
    End Sub

    Private Sub viewReport()
        Try
            Dim oInfRes As New ind.bk.reports.ReportBooks
            Dim oRs As DataView
            Dim tariffId As Integer = CInt(Me.lsttariffs.SelectedValue)
            AdminTenantSecurity.RequireTariff(Me.codhotel, tariffId)
            oRs = oInfRes.GetInfTarifas(Me.codhotel, tariffId)
            Me.dvrep = oRs
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub


    Private Sub getChildDiscount(ByRef priceInfo As ind.bk.reports.pricesInfoObject)
        Dim oRSlstHabTar As DataRowView = priceInfo.drvTariff
        If oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra1nino) <> 0 Then
            'fixed price
            priceInfo.UI_DiscountChild1 = oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra1nino)
        Else
            '% price
            priceInfo.UI_DiscountChild1 = oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.DescNinoPago1) & "%"
        End If

        If oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra2nino) <> 0 Then
            'fixed price
            priceInfo.UI_DiscountChild2 = oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.camaExtra2nino)
        Else
            '% price
            priceInfo.UI_DiscountChild2 = oRSlstHabTar(ind.bk.obj.R_Tarifa_habitacion.DBCol.DescNinoPago2) & "%"
        End If

    End Sub


End Class
