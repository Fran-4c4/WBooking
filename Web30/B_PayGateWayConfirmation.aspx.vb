Imports cfr
Imports System.Diagnostics

''' <summary>
''' This class receives response from Bank and do the book
''' </summary>
''' <remarks></remarks>
Partial Class B_PayGateWayConfirmation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not PaymentEndpointGuard.AllowLocalSimulation(Me.Context) Then
            Return
        End If
        Debug.WriteLine("Starting book from bank")
        Me.controlBankResponse()
    End Sub


    Private Sub controlBankResponse()

        Try
            Dim ds_order As String = cfr.pages.SiteBasePage.GetRequestValue("Ds_Order", Request.Form)
            Dim Ds_Signature As String = cfr.pages.SiteBasePage.GetRequestValue("Ds_Signature", Request.Form)
            Dim Ds_Response As String = cfr.pages.SiteBasePage.GetRequestValue("Ds_Response", Request.Form)


            Dim bookOperation As String = "" & Request.QueryString(cfr.REQCte.query.sysOper)
            If bookOperation.ToUpper <> "PAYGATECAIXA" Then
                Me.lblErr.Text = "request oper fail"
                cfr.tool.LogMgr.log("Request received without oper=PAYGATECAIXA")
                Exit Sub
            End If

            If ds_order = "" Then
                Me.lblErr.Text = "request id fail"
                cfr.tool.LogMgr.log("Request received without Ds_order")
                Exit Sub
            End If
            If Ds_Signature = "" Then
                Me.lblErr.Text = "request sig fail"
                cfr.tool.LogMgr.log("Request received without Ds_Signature")
                Exit Sub
            End If


            Dim pd As New cfr.PayGateWayManager
            pd.Ds_Merchant_Order = ds_order
            pd.Ds_Signature = Ds_Signature
            pd.Ds_Response = Ds_Response
            'UPDATE BASKET, at least we know if the bank send the post.
            pd.UpdateBasketBankResponse(pd.Ds_Merchant_Order, CStr(Ds_Response))

            If CInt(Ds_Response) < 99 Then 'ver al final los codigos
                'response from CAIXA it´s ok
                Me.lblErr.Text = "Bank Response ok"
            Else
                Me.lblErr.Text = "Bank Response fail " & Ds_Response
                cfr.tool.LogMgr.log("Request received from CAIXA with error " & Ds_Response)
                Exit Sub
            End If


            'All is ok, do the book
            Dim bookData As BookRes
            Dim bs As New BasketManager
            bookData = bs.getBookResFromStorageBySignature(pd.Ds_Signature)

            If bookData Is Nothing Then
                'this book doesn´t exist
                Me.lblErr.Text = "book not exist "
                cfr.tool.LogMgr.log("Error booking this book doesn´t exist:" & pd.Ds_Signature & pd.Ds_Merchant_Order)
                Exit Sub
            End If

            'do book
            Dim bm As New BookManager(bookData)

            Try
                bm.NewBook(bookData)
                'BasketManager.deleteBookResFromStorage(pd.Ds_Merchant_Order)
                Me.Panel1.Visible = True
                Dim squeryNormal As String = cfr.pages.SiteBasePage.CreateQueryNormal(bookData.hotel.id, bookData.site.id, bookData.language, CInt(bookData.bookOperator.tipoOperador))
            Catch ex As Exception
                Me.lblErr.Text = "booking error"
                cfr.tool.LogMgr.log("Error booking" & ex.Message)
                Exit Sub
            End Try
        Catch ex As Exception
            Me.lblErr.Text = "booking error"
            cfr.tool.LogMgr.log("Error booking from bank" & ex.Message)
            Exit Sub
        End Try
    End Sub


    'Codigos de la respuesta del TPV
    '0000 a 0099 Transacción autorizada para pagos y preautorizaciones
    '0900 Transacción autorizada para devoluciones y confirmaciones
    '101 Tarjeta caducada
    '102 Tarjeta en excepción transitoria o bajo sospecha de fraude
    '104 Operación no permitida para esa tarjeta o terminal
    '116 Disponible insuficiente
    '118 Tarjeta no registrada
    '129 Código de seguridad (CVV2/CVC2) incorrecto
    '180 Tarjeta ajena al servicio
    '184 Error en la autenticación del titular
    '190 Denegación sin especificar Motivo
    '191 Fecha de caducidad errónea
End Class
