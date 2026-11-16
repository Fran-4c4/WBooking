Imports System.Configuration
Imports System.Data.SqlClient


Namespace cfr.cte

    ''' <summary>
    ''' class wit configuration data
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class Config
        ' web.config applicationSettings is the authoritative owner of this key.
        Private Const BookingNotificationBccKey As String = "bookingNotificationBcc"
        Private Shared bookingNotificationBcc_m As String
        Private Shared serverUrl_m As String
        Private Shared checkingServerEmail_m As String
        Private Shared TPVTest_m As Byte

        Public Shared configSection As String = "applicationSettings/CFRService.My.MySettings"


        ''' <summary>
        ''' Optional blind-copy recipient for booking and cancellation messages.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>An empty value means that no central BCC is added.</remarks>
        Public Shared Property BookingNotificationBcc As String
            Get
                initConfig()
                Return bookingNotificationBcc_m
            End Get
            Set(value As String)
                bookingNotificationBcc_m = If(value, String.Empty).Trim()
            End Set
        End Property
        ''' <summary>
        ''' Server URL used when generating application links.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Property ServerUrl As String
            Get
                initConfig()
                Return serverUrl_m
            End Get
            Set(value As String)
                serverUrl_m = value
            End Set
        End Property
        ''' <summary>
        ''' email used in autocheck server data
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Property CheckingServerEmail As String
            Get
                initConfig()
                Return checkingServerEmail_m
            End Get
            Set(value As String)
                checkingServerEmail_m = value
            End Set
        End Property
        ''' <summary>
        ''' Controls access to the payment simulation endpoints.
        ''' </summary>
        ''' <value>0 disables payment endpoints; 1 permits local simulation only.</value>
        ''' <remarks>
        ''' The checked-in application has no enabled production payment-provider
        ''' protocol. Values outside the closed 0/1 domain are rejected during
        ''' configuration initialization. Changing this contract affects every
        ''' payment page and must not be used to bypass provider signature checks.
        ''' </remarks>
        Public Shared Property TPVTest As Byte
            Get
                initConfig()
                Return TPVTest_m
            End Get
            Set(value As Byte)
                TPVTest_m = value
            End Set
        End Property

        Private Shared developingVar As Byte
        ''' <summary>
        ''' used to know if we are in developer state
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Property developing As Byte
            Get
                initConfig()
                Return developingVar
            End Get
            Set(value As Byte)
                developingVar = value
            End Set
        End Property

        Private Shared configInit As Boolean = False
        ''' <summary>
        ''' configure fields in the class
        ''' </summary>
        ''' <param name="forceInitialize"></param>
        ''' <remarks></remarks>
        Public Shared Sub initConfig(Optional forceInitialize As Boolean = False)
            If configInit = True And forceInitialize = False Then
                Exit Sub
            End If

            Dim appSettingSection As ClientSettingsSection = cfr.cte.Config.getConfigSection

            BookingNotificationBcc = CStr(getSetting(appSettingSection, BookingNotificationBccKey, String.Empty))
            ServerUrl = getSetting(appSettingSection, "serverurl")
            CheckingServerEmail = getSetting(appSettingSection, "checkingserveremail")
            Dim tpvModeRaw As String = CStr(getSetting(appSettingSection, "tpvtest", "0"))
            Dim tpvMode As Byte
            If Not Byte.TryParse(tpvModeRaw, tpvMode) OrElse (tpvMode <> 0 AndAlso tpvMode <> 1) Then
                Throw New ConfigurationErrorsException("tpvtest must be 0 (disabled) or 1 (local simulation only).")
            End If
            TPVTest = tpvMode
            developingVar = getSetting(appSettingSection, "developing", 0)

            configInit = True

        End Sub
        ''' <summary>
        ''' return setting, and can validate if needed
        ''' </summary>
        ''' <param name="appSettingSection"></param>
        ''' <param name="settingKey"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getSetting(appSettingSection As ClientSettingsSection, settingKey As String, Optional valueDefault As Object = Nothing)
            Try
                Return appSettingSection.Settings.Get(settingKey).Value.ValueXml.InnerText
            Catch ex As Exception

                Return valueDefault
            End Try
        End Function


        ''' <summary>
        ''' Return config section asociated in web config.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getConfigSection() As ClientSettingsSection
            ConfigurationManager.RefreshSection(Config.configSection)
            Return ConfigurationManager.GetSection(Config.configSection)
        End Function

        ''' <summary>
        ''' Name of the optional process-level SQL Server connection-string override.
        ''' </summary>
        ''' <remarks>
        ''' The value is a provider connection string, not an Entity Framework entity
        ''' connection string. Unset or whitespace means that web.config remains
        ''' authoritative. When set, both cfrbookEntities and the legacy ind.bk.DBMgr
        ''' use it. Data Source and Initial Catalog are required. Invalid values are
        ''' rejected and the value must never be written to logs.
        ''' </remarks>
        Public Const SqlConnectionStringEnvironmentVariable As String = "WBOOKING_SQL_CONNECTION_STRING"

        ''' <summary>
        ''' Returns the validated environment override, or Nothing when it is absent.
        ''' </summary>
        Public Shared Function GetEnvironmentSqlConnectionString() As String
            Dim rawValue As String = Environment.GetEnvironmentVariable(SqlConnectionStringEnvironmentVariable)
            If String.IsNullOrWhiteSpace(rawValue) Then
                Return Nothing
            End If

            Try
                Dim builder As New SqlConnectionStringBuilder(rawValue)
                If String.IsNullOrWhiteSpace(builder.DataSource) OrElse
                   String.IsNullOrWhiteSpace(builder.InitialCatalog) Then
                    Throw New ConfigurationErrorsException(
                        SqlConnectionStringEnvironmentVariable & " requires Data Source and Initial Catalog.")
                End If
                Return builder.ConnectionString
            Catch ex As ConfigurationErrorsException
                Throw
            Catch ex As Exception
                Throw New ConfigurationErrorsException(
                    SqlConnectionStringEnvironmentVariable & " is not a valid SQL Server connection string.", ex)
            End Try
        End Function

    End Class
End Namespace


