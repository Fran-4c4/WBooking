Imports Microsoft.VisualBasic


Namespace ind.bk.book

    Public Class bookCancelManager
        Public Sub SendCancelConfirmationEF(ByVal bd As BookRes)

            Try

                Dim sh As String
                Dim strToMail As String
                If bd.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.HotelAgency Then 'si es agencia se le envia a su email
                    strToMail = bd.bookOperator.Email 'email de la agencia
                ElseIf bd.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.Company Then 'si es una empresa se le envia a su email
                    strToMail = bd.EmpresaRes.email  'email de la agencia
                Else
                    strToMail = bd.CliRes.email   'email del cliente
                End If

                sh = "<H2>" & String.Format(cfr.Language.GetCaption(192), bd.idEntity) & "</H2>" & Chr(13) & Chr(10)

                sh = sh & "<B>" & cfr.Language.GetCaption(208) & "</B>" & Chr(13) & Chr(10) 'booking conditions message

                sh = sh & "<br>" & cfr.Language.GetCaption(165) & ": " & bd.CliRes.Apellido1 & " " & bd.CliRes.Nombre & Chr(13) & Chr(10)
                sh = sh & "<br>" & cfr.Language.GetCaption(121) & ": " & strToMail & Chr(13) & Chr(10)
                sh = sh & "<br><BR>" & Chr(13) & Chr(10)
                'rooms
                For Each bo As BookedRoom In bd.roomsSelected.Values
                    sh = sh & "<br>Unidad: " & bo.roomSelected.description.getDes & Chr(13) & Chr(10)
                    sh = sh & "<br>" & cfr.Language.GetCaption(189) & ": " & bo.bookCancelDate & Chr(13) & Chr(10)
                Next

                sh = sh & "<br/><br/><H3<b>" & bd.hotel.hotelName & "</B></h3>" & Chr(13) & Chr(10)
                'strHTML=strHtml & "<br><B><A HREF=" & web & ">" & web & "</A></B>" & chr(13) & chr(10)
                sh = sh & "<br>" & cfr.Language.GetCaption(108) & ": " & bd.hotel.Direccion & Chr(13) & Chr(10)
                sh = sh & "<br>" & cfr.Language.GetCaption(107) & ": " & bd.hotel.Telefono & Chr(13) & Chr(10)
                sh = sh & "<br>" & cfr.Language.GetCaption(121) & ": " & bd.hotel.Email & Chr(13) & Chr(10)

                Dim shComposite As String = cfr.IUFunctions.getCompositeHtmlForMail("", sh)
                Dim subject As String = String.Format(cfr.Language.GetCaption(192), bd.idEntity) & cfr.Language.GetCaption(193) & " " & bd.CliRes.Apellido1 & " " & bd.CliRes.Nombre


                Dim emailData As New MailDataObj
                With emailData
                    .MailFrom = bd.hotel.BookParam.emailBooking
                    .FromName = "WBooking"
                    .subject = subject
                    .bodyHtml = shComposite
                    .MailTo.Add(bd.CliRes.email)

                    If bd.hotel.BookParam.emailBooking <> "" Then
                        .MailCC.Add(bd.hotel.BookParam.emailBooking)
                    End If
                    If bd.hotel.BookParam.emailBooking2 <> "" Then
                        .MailCC.Add(bd.hotel.BookParam.emailBooking2)
                    End If

                    If cfr.cte.Config.BookingNotificationBcc <> "" Then
                        .MailBCC.Add(cfr.cte.Config.BookingNotificationBcc)
                    End If

                    .emailToResponse = bd.hotel.BookParam.emailBooking
                End With

                Dim em As New EmailManager
                em.emailsList.Add(emailData)
                Dim th As New System.Threading.Thread(AddressOf em.sendEmailList)
                th.Start()
                System.Diagnostics.Debug.WriteLine("Sending email process started")
            Catch ex As Exception
                cfr.tool.LogMgr.log(ex)
            End Try

        End Sub
        Public Sub SendCancelConfirmation(ByVal bd As BookRes)

            Try

                Dim sh As String
                Dim strToMail As String
                If bd.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.HotelAgency Then 'si es agencia se le envia a su email
                    strToMail = bd.bookOperator.email 'email de la agencia
                ElseIf bd.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.Company Then 'si es una empresa se le envia a su email
                    strToMail = bd.EmpresaRes.email  'email de la agencia
                Else
                    strToMail = bd.CliRes.email   'email del cliente
                End If

                sh = "<H2>" & String.Format(cfr.Language.GetCaption(192), bd.idEntity) & "</H2>" & Chr(13) & Chr(10)

                sh = sh & "<B>" & cfr.Language.GetCaption(208) & "</B>" & Chr(13) & Chr(10) 'booking conditions message

                sh = sh & "<br>" & cfr.Language.GetCaption(165) & ": " & bd.CliRes.Apellido1 & " " & bd.CliRes.Nombre & Chr(13) & Chr(10)
                sh = sh & "<br>" & cfr.Language.GetCaption(121) & ": " & strToMail & Chr(13) & Chr(10)
                sh = sh & "<br><BR>" & Chr(13) & Chr(10)
                'rooms
                For Each bo As BookedRoom In bd.roomsSelected.Values
                    sh = sh & "<br>Unidad: " & bo.roomSelected.description.getDes & Chr(13) & Chr(10)
                    sh = sh & "<br>" & cfr.Language.GetCaption(189) & ": " & bo.bookCancelDate & Chr(13) & Chr(10)
                Next

                sh = sh & "<br/><br/><H3<b>" & bd.hotel.hotelName & "</B></h3>" & Chr(13) & Chr(10)
                'strHTML=strHtml & "<br><B><A HREF=" & web & ">" & web & "</A></B>" & chr(13) & chr(10)
                sh = sh & "<br>" & cfr.Language.GetCaption(108) & ": " & bd.hotel.Direccion & Chr(13) & Chr(10)
                sh = sh & "<br>" & cfr.Language.GetCaption(107) & ": " & bd.hotel.Telefono & Chr(13) & Chr(10)
                sh = sh & "<br>" & cfr.Language.GetCaption(121) & ": " & bd.hotel.Email & Chr(13) & Chr(10)

                Dim shComposite As String = cfr.IUFunctions.getCompositeHtmlForMail("", sh)
                Dim subject As String = String.Format(cfr.Language.GetCaption(192), bd.idEntity) & cfr.Language.GetCaption(193) & " " & bd.CliRes.Apellido1 & " " & bd.CliRes.Nombre


                Dim emailData As New MailDataObj
                With emailData
                    .MailFrom = bd.hotel.BookParam.emailBooking
                    .FromName = "WBooking"
                    .subject = subject
                    .bodyHtml = shComposite
                    .MailTo.Add(bd.CliRes.email)

                    If bd.hotel.BookParam.emailBooking <> "" Then
                        .MailCC.Add(bd.hotel.BookParam.emailBooking)
                    End If
                    If bd.hotel.BookParam.emailBooking2 <> "" Then
                        .MailCC.Add(bd.hotel.BookParam.emailBooking2)
                    End If

                    If cfr.cte.Config.BookingNotificationBcc <> "" Then
                        .MailBCC.Add(cfr.cte.Config.BookingNotificationBcc)
                    End If

                    .emailToResponse = bd.hotel.BookParam.emailBooking
                End With

                Dim em As New EmailManager
                em.emailsList.Add(emailData)
                Dim th As New System.Threading.Thread(AddressOf em.sendEmailList)
                th.Start()
                System.Diagnostics.Debug.WriteLine("Sending email process started")
            Catch ex As Exception
                cfr.tool.LogMgr.log(ex)
            End Try

        End Sub

    End Class

End Namespace
