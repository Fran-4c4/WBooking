
''' <summary>
''' class that redirect to the bank and pass the values needed to do the book
''' </summary>
''' <remarks></remarks>
Partial Class B_PayGateWay
    Inherits BookBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not PaymentEndpointGuard.AllowLocalSimulation(Me.Context) Then
            Return
        End If

        Try
            MyBase.loadBaseData()
            Me.Title = cfr.Language.getCaption(323)
            If Not Page.IsPostBack Then
                Me.returnToStep1.Text = cfr.Language.getCaption(347)
                Me.returnToStep1.NavigateUrl = "b_request.aspx?hotelcode=" & bookData.hotel.id & "&site=" & bookData.site.id & "&language=" & bookData.language

                Me.configPayment()
                Me.Ds_Merchant_Order.Value = Me.bookData.tpvData.Ds_Merchant_Order
                Me.Ds_Merchant_MerchantSignature.Value = Me.bookData.tpvData.Ds_Signature

                Me.configIframe()
            End If
        Catch ex As Exception
            Me.boe.exceptionThrowed = ex
            Me.controlError(boe)
            Me.showMessage(boe.msg, CFRConst.msgEnum.errorType)
        End Try

    End Sub

    ''' <summary>
    ''' Config the payment
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configPayment()
        Dim payGateData As New cfr.PayGateWayManager
        Dim totalPriceForBooking As Double = 0
        Me.bookData.TPVMinimum = 0 '
        For Each bo As cfr.BookedRoom In bookData.roomsSelected.Values
            totalPriceForBooking += bo.priceFinalForAllDays
            Me.bookData.TPVMinimum += bo.TPVMinimum
        Next

        'using CAIXA BANK WE NEED TO SEND THE VALUE WITHOUT DECIMAL SEPARATOR
        Dim totalPriceForBookingRounded As Double = Math.Round(totalPriceForBooking, 2)
        payGateData.Ds_Merchant_Amount = (totalPriceForBookingRounded * 100).ToString  'bank uses the last zeros as decimals

        payGateData.PasarelaConfig(bookData, Request.QueryString.ToString, Request.Form.ToString)
        payGateData.saveBasket()
        bookData.tpvData = payGateData

        Dim basketMgr As New cfr.BasketManager
        basketMgr.saveBookResToTPVStorage(Me.bookData, payGateData)

    End Sub

    ''' <summary>
    ''' Config the iframe that send the post to bank.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configIframe()
        Dim sIframe As String = ""
        sIframe = "<iframe  id='B_PayGateWayPostIframe' width='100%'  height='100%'  src='{0}'   frameborder =no   marginheight =0 marginwidth =0></iframe>"
        Dim sSrc As String = "b_PayGateWayPost.aspx?" & Me.GetQueryNormal

        Me.lit_B_PayGateWayPostIframe.Text = String.Format(sIframe, sSrc)

    End Sub
End Class
