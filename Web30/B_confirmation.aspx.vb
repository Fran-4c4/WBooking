Imports System.Diagnostics
Imports cfr


Partial Class B_confirmation
    Inherits BookBasePage
    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.lnkFindAgain.Text = cfr.Language.GetCaption(342)
            Me.lnkFindAgain.PostBackUrl = "b_request.aspx?" & Me.getQueryNormal

            'Debug.WriteLine("B_confirmation QueryString:" & cfr.pages.SiteBasePage.GetQueryString(Me))

            Me.Title = cfr.Language.GetCaption(151)
            If Not Page.IsPostBack Then


                Dim bookOperation As String = "" & Request.QueryString(cfr.REQCte.query.sysOper)
                If bookOperation.ToUpper = "PAYGATECAIXA" Then
                    Try
                        'BUSCAR POR EL CODIGO DE ORDEN, NO POR EL CODIGO DE RESERVA.
                        Dim bookId As String = cfr.BookManager.getCodReservaByOrderAndSignature(Request.QueryString("Ds_Merchant_Order"), Request.QueryString("MerchantSignature"))
                        Me.bookData = cfr.BookManager.getBookResById(bookId)
                        Me.bookData.site = Me.siteWeb
                    Catch ex As Exception
                        Try
                            Me.SesionErrorProp.Add(New cfr.BookOperationErrors(ex.Message))
                            Response.Redirect("b_request.aspx?" & Me.GetQueryNormal, True)
                        Catch exth As Threading.ThreadAbortException
                            Dim kk As String = ""
                            'nothing
                            Exit Sub
                        End Try
                    End Try


                Else

                    Dim pagecall As String = UCase(Request.ServerVariables("HTTP_REFERER"))

                    '    Me.SesionErrorProp.Add(New BookOperationErrors(basERR.E_GetReservByLocalizador))
                    '    Try
                    '        Response.Redirect("b_request.aspx?" & Me.getQueryNormal)
                    '    Catch ex As Exception
                    '        'nothing
                    '        Exit Sub
                    '    End Try

                    'End If
                    Me.bookData = cfr.book.UserHelper.getBookSession(Me.Context)

                End If

                If Me.bookData Is Nothing Then

                    Me.SesionErrorProp.Add(New BookOperationErrors(basERR.E_GetReservByLocalizador))
                    Try
                        Response.Redirect("b_request.aspx?" & Me.GetQueryNormal)
                    Catch ex As Threading.ThreadAbortException
                        'nothing
                        Exit Sub
                    End Try

                Else
                    If Me.bookData.idEntity = 0 Then
                        Try
                            Response.Redirect("b_request.aspx?" & Me.GetQueryNormal)
                        Catch ex As Threading.ThreadAbortException
                            'nothing
                            Exit Sub
                        End Try
                    End If
                    Me.langCaption.holdproperty = Me.bookData.hotel.Tipo

                    Dim cfrIU As New cfr.IUFunctions(Me.bookData.site.theme, Me.bookData)
                    cfrIU.showOptions.showDates = True
                    cfrIU.showOptions.showEmail = True
                    cfrIU.bookForStyle = IUFunctions.MailBookStyle.client
                    Dim s As String = cfrIU.getHtmlBook()
                    Me.litBookConfirmation.Text = s
                End If




            Else

            End If

        Catch exth As Threading.ThreadAbortException
            'nothing
        Catch ex As Exception
            Me.boe.exceptionThrowed = ex
            Me.boe.errBook = ex.Message
            Me.SesionErrorProp.Add(Me.boe)
            Response.Redirect("b_request.aspx?" & Me.getQueryNormal)
        End Try

    End Sub
End Class
