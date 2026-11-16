Imports cfr
Imports cfr.security

''' <summary>
''' Allows a guest to cancel one booking with its emailed single-use token.
''' </summary>
Partial Class B_Cancel
    Inherits BookBasePage

    Private Const MaximumFailuresPerBooking As Integer = 5
    Private Const MaximumFailuresPerIp As Integer = 10
    Private Shared ReadOnly FailureWindow As TimeSpan = TimeSpan.FromMinutes(15)

    Public Overrides Sub Page_PreInit(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreInit
        PublicCancellationGuard.RequireEnabled()
        Me.pageType = pageTypeEnum.simple
        MyBase.Page_PreInit(Me, e)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.Localizador.Text = Trim("" & Request.QueryString(cfr.REQCte.query.idBook))
        End If
    End Sub

    Protected Sub btnCancelBook_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelBook.Click
        If Not Page.IsValid Then Return

        Dim bookingId As Integer
        If Not Integer.TryParse(Me.Localizador.Text.Trim(), bookingId) OrElse bookingId <= 0 Then
            ShowGenericInvalidToken()
            Return
        End If

        Dim ipSubject As String = If(Request.UserHostAddress, String.Empty)
        Dim bookingSubject As String = bookingId.ToString(System.Globalization.CultureInfo.InvariantCulture)
        If RequestRateLimiter.IsBlocked("cancel-ip", ipSubject, MaximumFailuresPerIp, FailureWindow) OrElse
           RequestRateLimiter.IsBlocked("cancel-booking", bookingSubject, MaximumFailuresPerBooking, FailureWindow) Then
            ShowGenericInvalidToken()
            Return
        End If

        Dim lease As CancellationTokenLease = Nothing
        Try
            lease = CancellationTokenService.Acquire(bookingId, Me.CancellationCode.Text)
            If lease Is Nothing Then
                RecordFailure(ipSubject, bookingSubject)
                ShowGenericInvalidToken()
                Return
            End If

            Dim cancelledBooking As BookRes
            Using lease
                cancelledBooking = BookManager.CancelBookByAuthorizedGuest(bookingId.ToString(System.Globalization.CultureInfo.InvariantCulture))
                lease.Complete()
            End Using
            lease = Nothing

            RequestRateLimiter.Reset("cancel-ip", ipSubject)
            RequestRateLimiter.Reset("cancel-booking", bookingSubject)
            Dim mailer As New ind.bk.book.bookCancelManager()
            mailer.SendCancelConfirmation(cancelledBooking)
            Me.showMsgOk(cfr.Language.GetCaption(186))
            Me.btnCancelBook.Enabled = False
        Catch ex As Exception
            cfr.tool.LogMgr.log(ex)
            Me.showMsgErr(ex)
        Finally
            If lease IsNot Nothing Then lease.Dispose()
        End Try
    End Sub

    Private Sub RecordFailure(ByVal ipSubject As String, ByVal bookingSubject As String)
        RequestRateLimiter.RecordFailure("cancel-ip", ipSubject, MaximumFailuresPerIp, FailureWindow)
        RequestRateLimiter.RecordFailure("cancel-booking", bookingSubject, MaximumFailuresPerBooking, FailureWindow)
    End Sub

    Private Sub ShowGenericInvalidToken()
        Me.showMessage("The booking number or cancellation code is invalid or has expired.", CFRConst.msgEnum.errorType)
    End Sub
End Class
