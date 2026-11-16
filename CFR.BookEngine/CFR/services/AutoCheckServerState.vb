
Namespace cfr.services


    ''' <summary>
    ''' AutoCheckServer system to send email to target to know that server is running
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AutoCheckServerState
        ''' <summary>
        ''' timer used for autocheck
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared timer As Threading.Timer
        Private Shared stopMode As Boolean = False
        ''' <summary>
        ''' indicates if the class was configured
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared configSet As Boolean = False

        Public Sub New()
            If AutoCheckServerState.configSet = False Then
                AutoCheckServerState.configure()
                AutoCheckServerState.configSet = True
                AddHandler cfr.tool.ConfigWatcher.configFileChanged, AddressOf AutoCheckServerState.configure
            End If
        End Sub

        ''' <summary>
        ''' Configure general settings
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub configure()
            Try
                cfr.tool.LogMgr.log("AutoCheckServerState checkingServerEmail is " & cfr.cte.Config.CheckingServerEmail)
            Catch ex As Exception
                cfr.tool.LogMgr.log(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Start autocheck system to send email
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub startAutoCheck()
            AutoCheckServerState.stopAutoCheck()
            AutoCheckServerState.stopMode = False
            'Dim hour1 As Integer = 20 * 1000 '20 s test
            Dim hour1 As Integer = 12 * 60 * 60 * 1000 '12 hours
            Dim state As New Object
            Dim tc As New Threading.TimerCallback(AddressOf Me.autoCheck)

            AutoCheckServerState.timer = New Threading.Timer(tc, state, 1000, hour1) 'start in XXX seconds
        End Sub

        Private Shared AvailablePhysicalMemory As String

        Private Function GetNextMaintenanceTarget(ByVal time As TimeSpan) As DateTime
            Dim dt As DateTime = DateTime.Today.Add(time)
            If DateTime.Now > dt Then
                dt = dt.AddDays(1) ' already past target time for today, next start is tomorrow
            End If
            Return dt
        End Function

        Private Sub autoCheck()
            Try
                If AutoCheckServerState.stopMode = True Then
                    Exit Sub
                End If
                If "" & cfr.cte.Config.checkingServerEmail = "" Then
                    cfr.tool.LogMgr.log("Autocheck Mail is nothing, please fill it or stop this service.")
                    Exit Sub
                End If

                'Dim hour As Integer = Date.Now.Hour
                'If hour = 9 Then

                'Else
                '    Exit Sub
                'End If

                Dim mo As New Global.cfr.MailDataObj
                mo.subject = "CFR book Server is working at " & Date.Now
                mo.FromName = "CFR Book Server autocheck availability"
                mo.MailFrom = Global.cfr.EmailManager.mailFrom_MAILyahoo
                mo.MailTo.Add(cfr.cte.Config.checkingServerEmail)

                Dim newAvailablePhysicalMemory As String = My.Computer.Info.AvailablePhysicalMemory
                Dim strBody As String = "CFR book Server is running at " & Date.Now
                strBody += "<br>ComputerName: " & My.Computer.Name
                strBody += "<br>OSFullName: " & My.Computer.Info.OSFullName
                strBody += "<br>OSPlatform: " & My.Computer.Info.OSPlatform
                strBody += "<br>OSVersion: " & My.Computer.Info.OSVersion
                strBody += "<br>CurrentCulture: " & My.Computer.Info.InstalledUICulture.DisplayName
                strBody += "<br>AvailablePhysicalMemory: " & newAvailablePhysicalMemory & " Previous: " & AvailablePhysicalMemory
                strBody += "<br>TotalPhysicalMemory: " & My.Computer.Info.TotalPhysicalMemory
                strBody += "<br>AvailableVirtualMemory: " & My.Computer.Info.AvailableVirtualMemory
                strBody += "<br>TotalVirtualMemory: " & My.Computer.Info.TotalVirtualMemory
                strBody += "<br>DirectoryPath: " & My.Application.Info.DirectoryPath

                AvailablePhysicalMemory = newAvailablePhysicalMemory


                mo.bodyHtml = strBody

                Dim em As New Global.cfr.EmailManager
                em.sendEmail(mo)
                Debug.WriteLine("Message sended")
            Catch ex As Exception
                AutoCheckServerState.stopMode = False
                cfr.tool.LogMgr.log("Error sending email from autocheck system: " & ex.Message)
            End Try

        End Sub

        Public Shared Sub sendStopMessage()
            Try
                If AutoCheckServerState.stopMode = True Then
                    Exit Sub
                End If
                If "" & cfr.cte.Config.checkingServerEmail = "" Then
                    cfr.tool.LogMgr.log("Autocheck Mail is nothing, please fill it or stop this service.")
                    Exit Sub
                End If
                Dim mo As New Global.cfr.MailDataObj
                mo.subject = "CFR book Server is stopping at " & Date.Now
                mo.FromName = "CFR book Server stop"
                mo.MailFrom = Global.cfr.EmailManager.mailFrom_MAILyahoo
                mo.MailTo.Add(cfr.cte.Config.checkingServerEmail)

                Dim newAvailablePhysicalMemory As String = My.Computer.Info.AvailablePhysicalMemory
                Dim strBody As String = "CFR book Server is stopping at " & Date.Now
                strBody += "<br>ComputerName: " & My.Computer.Name
                strBody += "<br>OSFullName: " & My.Computer.Info.OSFullName
                strBody += "<br>OSPlatform: " & My.Computer.Info.OSPlatform
                strBody += "<br>OSVersion: " & My.Computer.Info.OSVersion
                strBody += "<br>CurrentCulture: " & My.Computer.Info.InstalledUICulture.DisplayName
                strBody += "<br>AvailablePhysicalMemory: " & newAvailablePhysicalMemory & " Previous: " & AvailablePhysicalMemory
                strBody += "<br>TotalPhysicalMemory: " & My.Computer.Info.TotalPhysicalMemory
                strBody += "<br>AvailableVirtualMemory: " & My.Computer.Info.AvailableVirtualMemory
                strBody += "<br>TotalVirtualMemory: " & My.Computer.Info.TotalVirtualMemory
                strBody += "<br>DirectoryPath: " & My.Application.Info.DirectoryPath

                AvailablePhysicalMemory = newAvailablePhysicalMemory


                mo.bodyHtml = strBody

                Dim em As New Global.cfr.EmailManager
                em.sendEmail(mo)
                Debug.WriteLine("Message sended")
            Catch ex As Exception
                AutoCheckServerState.stopMode = False
                cfr.tool.LogMgr.log("Error sending email from autocheck system: " & ex.Message)
            End Try
        End Sub



        ''' <summary>
        ''' Stop the autocheck machine
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub stopAutoCheck()
            AutoCheckServerState.stopMode = True
            Try
                If Not AutoCheckServerState.timer Is Nothing Then
                    AutoCheckServerState.timer.Dispose()
                End If
            Catch ex As Exception
                cfr.tool.LogMgr.log("Error stopping timer for autocheck machine ", ex.Message)
            End Try
        End Sub
    End Class

End Namespace
