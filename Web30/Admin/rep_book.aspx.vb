
Imports System.Data
Imports System.Diagnostics
Imports ind.bk.reports

Partial Class Admin_rep_book
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P7_Informes_de_reservas

    Friend dvrep As New DataView
    Friend mycal As New CalendarUI.calendarjs
    Friend bookId As String
    Friend objIdDec As String
    Friend searchinprogress As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.informes)


        If Not Page.IsPostBack Then
            'mycal.configure(Me.checkindate, Me.checkoutdate, calendarjsLit)
            Me.cboConfig()
            Dim dayNow As Date = Date.Now
            Me.checkindate.Text = dayNow.ToString("dd/MM/yyyy")
            dayNow = dayNow.AddMonths(6)
            Me.checkoutdate.Text = dayNow.ToString("dd/MM/yyyy")
            btnSearch_Click(Me, Nothing)
        Else
            searchinprogress = True

        End If


    End Sub

    Private Sub cboConfig()

    End Sub


    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try

            Dim datein As Date = DateTime.ParseExact(Me.checkindate.Text, "dd/MM/yyyy", Nothing)
            Dim dateOut As Date = DateTime.ParseExact(Me.checkoutdate.Text, "dd/MM/yyyy", Nothing)
            If dateOut < datein Then
                cfr.pages.SiteBasePage.showMessage("La fecha final no puede ser inferior a la inicial", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If

            Dim oInfRes As New ind.bk.reports.ReportBooks
            Dim oRs As DataView
            
            Dim inf As Integer = -1
            If Me.rdbbookstatepending.Checked = True Then
                inf = cfr.CFRConst.bookStateEnum.Active
            ElseIf Me.rdbbookstatecancel.Checked = True Then
                inf = 1 'all canceled
            ElseIf Me.rdbbookstateconfirmed.Checked = True Then
                inf = cfr.CFRConst.bookStateEnum.confirmedByHotel
            End If
            oRs = oInfRes.GetReservTOTAL(Me.codhotel, datein, dateOut, Me.hotel.Tipo, inf)

            Me.dvrep = oRs

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Protected Sub btnsearchbookid_Click(sender As Object, e As EventArgs) Handles btnsearchbookid.Click
        Try
            Dim oInfRes As New ind.bk.reports.ReportBooks
            Dim oRs As DataView

            Dim searchcondition As String = Me.txtsearchbookid.Text.Trim
            If IsNumeric(searchcondition) = False Then
                Me.showMessage("Codigo de reserva incorrecto.", CFRConst.msgEnum.errorType)
                Exit Sub
            End If
            

            oRs = oInfRes.GetReservTOTALBy(Me.codhotel, Me.hotel.Tipo, ReportBooks.searchTypeEnum.byBookID, CStr(searchcondition), -1)

            Me.dvrep = oRs
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Protected Sub btnsearchbookidproperty_Click(sender As Object, e As EventArgs) Handles btnsearchbookidproperty.Click
        Try
            Dim oInfRes As New ind.bk.reports.ReportBooks
            Dim oRs As DataView

            Dim searchcondition As String = Me.txtsearchbookidproperty.Text.Trim

            oRs = oInfRes.GetReservTOTALBy(Me.codhotel, ReportBooks.searchTypeEnum.byBookIDProperty, CStr(searchcondition), -1)

            Me.dvrep = oRs
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Protected Sub btnsearchbyemail_Click(sender As Object, e As EventArgs) Handles btnsearchbyemail.Click
        Try
            Dim oInfRes As New ind.bk.reports.ReportBooks
            Dim oRs As DataView

            Dim searchcondition As String = Me.txtsearchbyemail.Text.Trim

            oRs = oInfRes.GetReservTOTALBy(Me.codhotel, Me.hotel.Tipo, ReportBooks.searchTypeEnum.byCustomerEmail, CStr(searchcondition), -1)

            Me.dvrep = oRs
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Protected Sub btnsearchbyname_Click(sender As Object, e As EventArgs) Handles btnsearchbyname.Click
        Try
            Dim oInfRes As New ind.bk.reports.ReportBooks
            Dim oRs As DataView

            Dim searchcondition As String = Me.txtsearchbysurname.Text.Trim


            oRs = oInfRes.GetReservTOTALBy(Me.codhotel, ReportBooks.searchTypeEnum.byCustomerName, searchcondition, -1)

            Me.dvrep = oRs
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    
    
End Class
