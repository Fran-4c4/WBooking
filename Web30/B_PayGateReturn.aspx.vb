
Partial Class B_PayGateReturn
    Inherits cfr.pages.SiteBasePage

    Public Overrides Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Me.pageType = pageTypeEnum.simple
        MyBase.Page_PreInit(sender, e)
    End Sub

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not PaymentEndpointGuard.AllowLocalSimulation(Me.Context) Then
            Return
        End If

        Try
            Me.btnCancelBack.NavigateUrl = "b_request.aspx?" & Me.getQueryNormal
            Dim bookOperation As String = "" & Request.QueryString(cfr.REQCte.query.sysOper)
            If bookOperation.ToUpper = "PAYGATECAIXA" Then
                validateRequest()
            Else
                'todo: reenviar a la pagina de inicio con el mensaje de error
                Me.pnlOk.Visible = False
                Me.pnlErr.Visible = True
            End If
        Catch ex As Exception
            Me.boe.exceptionThrowed = ex
            Me.controlError(boe)
        End Try
    End Sub

    Private Sub validateRequest()
        Dim Ds_Merchant_Order As String = "" & Request.QueryString("Ds_Merchant_Order")
        Dim MerchantSignature As String = "" & Request.QueryString("MerchantSignature")
        If Ds_Merchant_Order = "" Or MerchantSignature = "" Then
            Me.pnlOk.Visible = False
            Me.pnlErr.Visible = True
            Exit Sub
        Else
            Me.pnlOk.Visible = True
            Me.pnlErr.Visible = False
        End If
        Dim urlStr As String = "B_CONFIRMATION.ASPX?OPER=PAYGATECAIXA&" & Me.getQueryNormal & "&Ds_Merchant_Order={0}&MerchantSignature={1}"
        urlStr = String.Format(urlStr, Ds_Merchant_Order, MerchantSignature)
        Me.btnContinueBook.NavigateUrl = urlStr

        Dim sScript As String = ""
        sScript += "<script language =javascript >"
        sScript += "function redirect(){"
        sScript += "    parent.document.location.href='" & Me.btnContinueBook.ResolveClientUrl(Me.btnContinueBook.NavigateUrl) & "'; "
        sScript += " }"
        'IMPORTANT IMPORTANT IMPORTANT
        sScript += "redirect();" 'LOOK HERE, WE REDIRECT THE USER AFTER OPERATION

        sScript += "</script>"
        Me.litOperationScript.Text = sScript
    End Sub
End Class
