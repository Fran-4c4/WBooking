Imports System.Diagnostics
Imports cfr

''' <summary>
''' This class receive data from book and send to bank gateway
''' </summary>
''' <remarks></remarks>
Partial Class B_PayGateWayPost
    Inherits cfr.pages.WBasePage

    Public bookData As cfr.BookRes

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not PaymentEndpointGuard.AllowLocalSimulation(Me.Context) Then
            Return
        End If

        Try
            Me.bookData = cfr.book.UserHelper.getBookSession(Me.Context)
            Debug.WriteLine("B_PayGateWayPost local simulation")
            Dim qstr As String = "testpostTotpv2.aspx"
            Server.Transfer(qstr, True)
            

        Catch exTh As Threading.ThreadAbortException
            'all is ok, the thread is interrupted due to the post to bank
            'Try
            '    Response.Redirect("TestBankPayment.aspx?oper=PAYGATECAIXA&Ds_Order=" & pd.Ds_Merchant_Order & "&Ds_Signature=" & pd.Ds_Signature & "&site=" & Me.bookData.site.code)

            'Catch ex As Exception
            '    Exit Sub
            'End Try
            Exit Sub
        Catch ex As Exception
            boe.setMsg(ex.Message)
            'delete the basket? or remain
            BasketManager.deleteBookResFromStorage(Me.bookData.tpvData.Ds_Merchant_Order)
            Exit Sub
        Finally

        End Try


        Exit Sub

    End Sub
    
    ''' <summary>
    ''' Send post to bank directly. The bank show a page to the user to pay.
    ''' </summary>
    ''' <param name="pd"></param>
    ''' <remarks></remarks>
    Private Sub sendPostToBank(ByVal pd As cfr.PayGateWayManager)
        Dim rp As New RemotePostHelper(pd.URL_TPVVIRTUAL)
        rp.Add("Ds_Merchant_Amount", pd.Ds_Merchant_Amount)
        rp.Add("Ds_Merchant_Currency", pd.Ds_Merchant_Currency)
        rp.Add("Ds_Merchant_Order", pd.Ds_Merchant_Order)
        rp.Add("Ds_Merchant_MerchantCode", pd.Ds_Merchant_MerchantCode)
        rp.Add("Ds_Merchant_MerchantSignature", pd.Ds_Signature)
        rp.Add("Ds_Merchant_Terminal", pd.Ds_Merchant_Terminal)
        rp.Add("Ds_Merchant_TransactionType", pd.Ds_Merchant_TransactionType)
        rp.Add("Ds_Merchant_MerchantURL", pd.Ds_Merchant_MerchantURL)
        rp.Add("Ds_Merchant_UrlOK", pd.Ds_Merchant_UrlOK)
        rp.Add("Ds_Merchant_UrlKO", pd.Ds_Merchant_UrlKO)
        rp.Add("Ds_Merchant_ConsumerLanguage", pd.Ds_Merchant_ConsumerLanguage)

        rp.Post()
    End Sub
End Class
