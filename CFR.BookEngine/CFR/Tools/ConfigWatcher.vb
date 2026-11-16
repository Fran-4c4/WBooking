

Imports System.IO

Namespace cfr.tool


    ''' -----------------------------------------------------------------------------
    ''' Project	 : ARDAC
    ''' Class	 : ARIConnectionFileWatcher<summary>
    ''' Class to control changes in the configuration of the server for 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[userepc]	13/07/2010	Created
    ''' </history>
    Public Class ConfigWatcher

        Public Shared Event configFileChanged()

        '
        ' Create a FileSystemWatcher object passing it the folder to watch.
        '
        Public Shared fsw As FileSystemWatcher

        Public Shared Sub startWatcher()
            Try
                Dim cfgw As New ConfigWatcher
                cfgw.configure()

                cfr.tool.LogMgr.log("Config watcher is working")
            Catch ex As Exception
                cfr.tool.LogMgr.log("Config watcher Error" & ex.Message)
            End Try

        End Sub

        Public Sub configure()

            Dim path As String = My.Application.Info.DirectoryPath
            path = path.Replace("file:\", "")
            'If path.EndsWith("\") = False Then
            '    path = path & "\"
            'End If
            fsw = New FileSystemWatcher

            '
            ' Assign event procedures to the events to watch.
            '
            'AddHandler fsw.Created, AddressOf OnChanged
            AddHandler fsw.Changed, AddressOf OnChanged
            'AddHandler fsw.Deleted, AddressOf OnChanged
            'AddHandler fsw.Renamed, AddressOf OnRenamed



            With fsw
                .Path = path
                .EnableRaisingEvents = True
                '.IncludeSubdirectories = True
                '
                ' Watch certain file types.
                '
                .Filter = My.Application.Info.AssemblyName & ".exe.config"
                cfr.tool.LogMgr.log("My.Application.Info.AssemblyName: " & My.Application.Info.AssemblyName & " On " & path)
                '
                ' Specify file change notifications.
                '           
                .NotifyFilter = (NotifyFilters.LastWrite)


                '
                ' Specif the event to watch for.
                '
                '.WaitForChanged(WatcherChangeTypes.Changed)

            End With



            System.Diagnostics.Debug.WriteLine("OrbServerConfig File watcher was configured")
        End Sub

        Public Sub OnChanged(ByVal source As Object, ByVal e As FileSystemEventArgs)
            Try
                cfr.tool.LogMgr.log("File changed: " & e.FullPath & " change type: " & e.ChangeType)

                System.Diagnostics.Debug.WriteLine("File changed: " & e.FullPath & " change type: " & e.ChangeType)
                If e.ChangeType = WatcherChangeTypes.Changed Then

                    fsw.EnableRaisingEvents = False

                    RaiseEvent configFileChanged()

                    fsw.EnableRaisingEvents = True
                End If
            Catch ex As Exception
                fsw.EnableRaisingEvents = True
            End Try


        End Sub

        Public Sub OnRenamed(ByVal source As Object, ByVal e As RenamedEventArgs)
            'Debug.Fail("File renamed from: " & e.OldName & " to: " & e.Name)
        End Sub



    End Class
End Namespace