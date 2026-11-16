Imports Microsoft.VisualBasic

Namespace cfr.tool
    ''' <summary>
    ''' class to log data.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LogMgr

        Public Enum logTypeEnum As Short
            debug = 1
            info = 2
            errorApp = 3
            fatal = 4
        End Enum

#Region "Logger Setup"
        Protected Shared ReadOnly applog As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
#End Region


        Public Shared Sub log(ByVal message As String, Optional ByVal logType As logTypeEnum = logTypeEnum.info)
            Select Case logType
                Case logTypeEnum.debug
                    LogMgr.applog.Debug(message)
                Case logTypeEnum.info
                    LogMgr.applog.Info(message)
                Case logTypeEnum.fatal
                    LogMgr.applog.Fatal(message)
                Case logTypeEnum.errorApp
                    LogMgr.applog.Error(message)
            End Select
        End Sub

        Public Shared Sub log(ByVal ex As Exception, Optional ByVal logType As logTypeEnum = logTypeEnum.info)
            Select Case logType
                Case logTypeEnum.debug
                    LogMgr.applog.Debug(ex)
                Case logTypeEnum.info
                    LogMgr.applog.Info(ex)
                Case logTypeEnum.fatal
                    LogMgr.applog.Fatal(ex)
                Case logTypeEnum.errorApp
                    LogMgr.applog.Error(ex)
            End Select

        End Sub


    End Class
End Namespace

