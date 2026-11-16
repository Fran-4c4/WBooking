Imports Microsoft.VisualBasic
Imports System.Net.Mail

Namespace cfr

    ''' <summary>
    ''' class to send email
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EmailManager

        '********** EMAIL **************************************
        'cambiar tambien en el archivo de emailfunctions
        ' The host applicationSettings section is the authoritative owner of these values.
        ' Empty credentials intentionally fail at send time; no production fallback is embedded.
        Public Shared host_MAILyahoo As String
        Public Shared user_MAILyahoo As String
        Public Shared pwd_MAILyahoo As String
        Public Shared mailFrom_MAILyahoo As String

        ''' <summary>
        ''' the email list to send
        ''' </summary>
        ''' <remarks></remarks>
        Public emailsList As New ArrayList
        ''' <summary>
        ''' indicates if class was configurated 
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared configSet As Boolean = False

        Public Sub New()
            If EmailManager.configSet = False Then
                EmailManager.Configure()
                EmailManager.configSet = True
                AddHandler cfr.tool.ConfigWatcher.configFileChanged, AddressOf EmailManager.Configure
            End If
        End Sub

        Public Shared Sub Configure()
            Try
                Dim appSettingSection As Configuration.ClientSettingsSection = cfr.cte.Config.getConfigSection
                EmailManager.host_MAILyahoo = cfr.cte.Config.getSetting(appSettingSection, "host_mailyahoo")
                EmailManager.user_MAILyahoo = cfr.cte.Config.getSetting(appSettingSection, "user_mailyahoo")
                EmailManager.pwd_MAILyahoo = cfr.cte.Config.getSetting(appSettingSection, "pwd_mailyahoo")
                EmailManager.mailFrom_MAILyahoo = cfr.cte.Config.getSetting(appSettingSection, "mailfrom_mailyahoo")
                Dim slog As String = "EmailManager configuration reloaded to " & _
                "host_MAILyahoo:" & EmailManager.host_MAILyahoo & _
                "user_MAILyahoo:" & EmailManager.user_MAILyahoo & _
                "mailFrom_MAILyahoo:" & EmailManager.mailFrom_MAILyahoo
                cfr.tool.LogMgr.log(slog)
            Catch ex As Exception
                cfr.tool.LogMgr.log(ex)
            End Try

        End Sub




        ''' <summary>
        ''' Send email using a list
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub sendEmailList()
            For Each EmailData As MailDataObj In Me.emailsList
                Try
                    Me.sendEmail(EmailData)
                Catch ex As Exception
                    cfr.tool.LogMgr.log("Error sending email : " & ex.Message)
                End Try

            Next
        End Sub

        ''' <summary>
        ''' Send email
        ''' </summary>
        ''' <param name="EmailData"></param>
        ''' <remarks></remarks>
        Public Sub sendEmail(ByVal EmailData As MailDataObj)
            Dim message As MailMessage = New MailMessage()

            For Each mailTo As String In EmailData.MailTo
                If Not mailTo Is Nothing Then
                    Try
                        message.To.Add(New MailAddress(mailTo))
                    Catch ex As Exception
                        cfr.tool.LogMgr.log("Error with email: " & ex.Message)
                    End Try
                End If
            Next

            'message.From = New MailAddress(EmailData.MailFrom, EmailData.FromName)
            'we must send the email from an our account or the system reject by security restrictions
            message.From = New MailAddress(EmailManager.mailFrom_MAILyahoo, EmailData.FromName)


            For Each mailCC As String In EmailData.MailCC
                If Not mailCC Is Nothing Then
                    Try
                        message.CC.Add(New MailAddress(mailCC))
                    Catch ex As Exception
                        cfr.tool.LogMgr.log("Error with email: " & ex.Message)
                    End Try

                End If
            Next

            For Each mailbCC As String In EmailData.MailBCC
                If Not mailbCC Is Nothing Then
                    Try
                        message.Bcc.Add(New MailAddress(mailbCC))
                    Catch ex As Exception
                        cfr.tool.LogMgr.log("Error with email: " & ex.Message)
                    End Try

                End If
            Next

            message.Subject = EmailData.subject

            message.Body = EmailData.bodyHtml
            message.IsBodyHtml = EmailData.isHtml

            message.ReplyToList.Add(New MailAddress(EmailData.MailFrom, EmailData.FromName))


            Dim client As SmtpClient = New SmtpClient(EmailManager.host_MAILyahoo, 25)

            client.Credentials = New System.Net.NetworkCredential(EmailManager.user_MAILyahoo, EmailManager.pwd_MAILyahoo)
            client.Send(message)
        End Sub

    End Class
End Namespace
