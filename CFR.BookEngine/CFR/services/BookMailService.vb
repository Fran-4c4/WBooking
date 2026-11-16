
Imports System.Diagnostics

Namespace cfr
    ''' <summary>
    ''' Service to send mail to client and hotel from books
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BookMailService

#Region "Fields"
        Private WithEvents timerToSendMailObj As Timers.Timer
        ''' <summary>
        ''' indicates if the class was configured
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared configSet As Boolean = False
#End Region

        ''' <summary>
        ''' Mail status
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum MailStatusEnum As Byte
            pending = 0
            sended = 1
        End Enum

        ''' <summary>
        ''' For book mail it indicates if is for client or for hotel
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum MailBookCopyNumber As Byte
            ''' <summary>
            ''' send to client
            ''' </summary>
            ''' <remarks></remarks>
            client = 2
            ''' <summary>
            ''' send to hotel
            ''' </summary>
            ''' <remarks></remarks>
            hotel = 1
        End Enum

        Public Sub New()
            If BookMailService.configSet = False Then
                BookMailService.configure()
                BookMailService.configSet = True
                AddHandler cfr.tool.ConfigWatcher.configFileChanged, AddressOf BookMailService.configure
            End If

        End Sub

        ''' <summary>
        ''' Configure general settings
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub configure()
            Try
                cfr.tool.LogMgr.log("BookMailService central BCC configured: " & (cfr.cte.Config.BookingNotificationBcc <> ""))
            Catch ex As Exception
                cfr.tool.LogMgr.log(ex)
                Throw ex
            End Try
        End Sub
        ''' <summary>
        ''' start service to send booking mails
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub startService()
            If Me.timerToSendMailObj Is Nothing Then
                Me.configureTimer()
            End If

            Me.timerToSendMailObj.Enabled = True
        End Sub
        ''' <summary>
        ''' stop service to send booking mails
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub stopService()
            If Me.timerToSendMailObj Is Nothing Then
                Exit Sub
            End If

            Me.timerToSendMailObj.Enabled = False
        End Sub




#Region "Configuration"
        Private Sub configureTimer()
            Me.timerToSendMailObj = New Timers.Timer
            Me.timerToSendMailObj.Interval = 1000 '1 second for the first time
            Me.timerToSendMailObj.AutoReset = True 'true indicate that the timer is started again byself
        End Sub
#End Region

        Dim timerSyncLock As New Object

        Private Sub timerToSendMailObj_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timerToSendMailObj.Elapsed
            Try
                'SyncLock timerSyncLock
                '    If Me.tim.Enabled = True Then
                Me.timerToSendMailObj.Enabled = False
                '    Else
                'Exit Sub
                '    End If

                'End SyncLock

                Me.checkBookToSend()

            Catch ex As Exception
                cfr.tool.LogMgr.log(ex)
            Finally
                Me.timerToSendMailObj.Interval = 10000 '10 seconds
                Me.timerToSendMailObj.Enabled = True
            End Try

        End Sub

        ''' <summary>
        ''' Check the books not sended and send a book for each
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub checkBookToSend()

            Dim dt As DataTable = Me.getPendingBookToSend
            For Each dr As DataRow In dt.Rows
                Try
                    Dim bookId As Integer = dr("cod_reservao")
                    Dim bookData As cfr.BookRes = cfr.BookManager.getBookResById(bookId)
                    Me.sendBookConfirmationMail(bookData)
                    Dim logData As String = "Book Email sent: BookID({0}); bookDate({1});  dateIn({2});  dateOut({3}); CliRes.email({4}); hotelName({5})) "
                    cfr.tool.LogMgr.log(String.Format(logData, bookId, bookData.bookDate, bookData.dateIn, bookData.dateOut, bookData.CliRes.email, bookData.hotel.hotelName))
                Catch ex As Exception
                    Debug.WriteLine(ex)
                    cfr.tool.LogMgr.log(ex)
                End Try

            Next
        End Sub

        ''' <summary>
        ''' Get a datatable with the books to send
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function getPendingBookToSend() As DataTable
            Dim paramCol(0) As SqlClient.SqlParameter
            Dim dt As DataTable = ind.bk.DBMgr.getDataTable(cfr.db.sp.sproc.Book.Book_GetPendingToSendEmail)
            Return dt
        End Function

        ''' <summary>
        ''' update the status for the book sended
        ''' </summary>
        ''' <param name="bookID"></param>
        ''' <param name="status"></param>
        ''' <remarks></remarks>
        Private Function SetBookMailStatus(ByVal bookID As Integer, ByVal status As MailStatusEnum) As Integer
            Try
                Dim paramCol(1) As SqlClient.SqlParameter
                paramCol(0) = ind.bk.DBMgr.getSpParameter("bookID", bookID)
                paramCol(1) = ind.bk.DBMgr.getSpParameter("MailSendStatus", status)
                Return ind.bk.DBMgr.ExecuteSp(cfr.db.sp.sproc.Book.Book_Update_Mail_Send, paramCol, Nothing)
            Catch ex As Exception
                cfr.tool.LogMgr.log("Error updating status for bookID:" & bookID & "." & ex.Message)
                Return 0
            End Try

        End Function
        ''' <summary>
        ''' Send book confirmation by mail
        ''' </summary>
        ''' <param name="bookData"></param>
        ''' <remarks></remarks>
        Public Sub sendBookConfirmationMail(ByRef bookData As BookRes)

            Dim cfrIU As New cfr.IUFunctions(bookData.site.theme, bookData)

            'send to hotel in spanish language
            Language.ConfigCulture(CFRConst.LanguageEnum.es)

            Dim emailShowOptions As EmailShowOptions = New EmailShowOptions()
            Dim blockedOptions As cfr.erp.CustomerObj.BlockedOptions = Me.checkIfCustomerIsBlocked(bookData.hotel.cod_Hotel)

            If blockedOptions = erp.CustomerObj.BlockedOptions.Blocked Then
                emailShowOptions.showDates = False
                emailShowOptions.showEmail = False
            Else
                emailShowOptions.showDates = True
                emailShowOptions.showEmail = True
            End If

            cfrIU.showOptions = emailShowOptions


            'SEND TO HOTEL
            cfrIU.bookForStyle = IUFunctions.MailBookStyle.hotel
            Dim sBodyBookSpanish As String = cfrIU.getHtmlBook()

            Dim mailSendedOk As Boolean = False
            mailSendedOk = Me.sendBookMail(bookData, sBodyBookSpanish, MailBookCopyNumber.hotel) 'send to hotel in spanish


            'send to client in client selected language
            cfrIU.bookForStyle = IUFunctions.MailBookStyle.client
            Dim sBodyBookByLanguage As String

            If bookData.language <> CFRConst.LanguageEnum.es Then
                Language.ConfigCulture(bookData.language)
                sBodyBookByLanguage = cfrIU.getHtmlBook()
            Else
                sBodyBookByLanguage = cfrIU.getHtmlBook()
            End If

            Dim cancellationIssue As cfr.security.CancellationTokenIssue =
                cfr.security.CancellationTokenService.Issue(CInt(bookData.idEntity), bookData.dateIn)
            sBodyBookByLanguage &= cfr.security.CancellationTokenService.BuildEmailInstructions(bookData, cancellationIssue)

            mailSendedOk = Me.sendBookMail(bookData, sBodyBookByLanguage, MailBookCopyNumber.client) 'send to client
            If mailSendedOk = True Then
                Me.SetBookMailStatus(bookData.idEntity, MailStatusEnum.sended)
            End If

        End Sub

        ''' <summary>
        ''' Send email confirmation
        ''' </summary>
        ''' <param name="bookData"></param>
        ''' <param name="sBodyBook"></param>
        ''' <param name="copyNumberFor">1 to send to hotel only, 2 to send to client or company</param>
        ''' <remarks></remarks>
        Public Function sendBookMail(ByVal bookData As BookRes, ByVal sBodyBook As String, ByVal copyNumberFor As MailBookCopyNumber) As Boolean
            Try
                Dim testing As Boolean = False

                Dim emailData As New MailDataObj
                With emailData
                    .MailFrom = bookData.hotel.BookParam.emailBooking
                    If bookData.site.description Is Nothing Or bookData.site.description = "" Then
                        .FromName = bookData.hotel.hotelName
                    Else
                        .FromName = bookData.site.description
                    End If

                    .subject = bookData.idEntity & " " & cfr.Language.GetCaption(174) & " " & bookData.CliRes.Apellido1 & " " & bookData.CliRes.Nombre
                    .bodyHtml = sBodyBook

                    If testing = False Then

                        If copyNumberFor = MailBookCopyNumber.client Then
                            If bookData.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.PrivateClient _
                                Or bookData.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.registeredClient Then
                                .MailTo.Add(bookData.CliRes.email)

                            ElseIf bookData.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.Company Then
                                If bookData.CliRes.email <> "" Then
                                    .MailTo.Add(bookData.CliRes.email)
                                    If bookData.EmpresaRes.email <> "" Then
                                        .MailCC.Add(bookData.EmpresaRes.email)
                                    End If
                                Else
                                    .MailTo.Add(bookData.EmpresaRes.email)
                                End If

                            ElseIf bookData.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.HotelAgency Then
                                If bookData.tariffForClientCode = cfr.CFRConst.bookTypeEnum.resParticular Then
                                    .MailTo.Add(bookData.AgenciaRes.email)
                                ElseIf bookData.tariffForClientCode = cfr.CFRConst.bookTypeEnum.resEmpresa Then
                                    .MailTo.Add(bookData.AgenciaRes.email)
                                ElseIf bookData.tariffForClientCode > 0 Then
                                    .MailTo.Add(bookData.AgenciaRes.email)
                                End If
                            End If
                        End If

                        If copyNumberFor = MailBookCopyNumber.hotel Then
                            'Email of hotels
                            If bookData.hotel.BookParam.emailBooking <> "" Then
                                .MailCC.Add(bookData.hotel.BookParam.emailBooking)
                            End If
                            If bookData.hotel.BookParam.emailBooking2 <> "" Then
                                .MailCC.Add(bookData.hotel.BookParam.emailBooking2)
                            End If
                        End If



                        'BCC recipient
                        If cfr.cte.Config.BookingNotificationBcc <> "" Then
                            .MailBCC.Add(cfr.cte.Config.BookingNotificationBcc)
                        End If


                    ElseIf testing = True Then
                        If String.IsNullOrWhiteSpace(cfr.cte.Config.CheckingServerEmail) Then
                            Throw New System.Configuration.ConfigurationErrorsException("checkingserveremail is required when sending a test booking email.")
                        End If
                        .MailTo.Add(cfr.cte.Config.CheckingServerEmail)
                    End If

                    .emailToResponse = bookData.hotel.BookParam.emailBooking

                End With
                Dim em As New EmailManager
                em.emailsList.Add(emailData)
                em.sendEmailList()
                Return True
            Catch ex As Exception
                cfr.tool.LogMgr.log(ex)
                Return False
            End Try
        End Function



        ' cfr.BookMailService
        Public Function checkIfCustomerIsBlocked(cod_hotel As Integer) As erp.CustomerObj.BlockedOptions
            Dim result As erp.CustomerObj.BlockedOptions
            Try
                Dim customerMgr As cfr.erp.CustomerMgr = New cfr.erp.CustomerMgr()
                Dim customerObj As cfr.erp.CustomerObj = customerMgr.CustomerGetByCodHotelAsociated(cod_hotel)
                Dim flag As Boolean = customerObj Is Nothing
                If flag Then
                    result = cfr.erp.CustomerObj.BlockedOptions.Unblocked
                Else
                    result = customerObj.blockBook
                End If
            Catch ex As Exception

                tool.LogMgr.log("Error getting block status for cod_hotel:" + CStr(cod_hotel) + "." + ex.Message, tool.LogMgr.logTypeEnum.info)
                result = cfr.erp.CustomerObj.BlockedOptions.Unblocked

            End Try
            Return result
        End Function

    End Class


End Namespace
