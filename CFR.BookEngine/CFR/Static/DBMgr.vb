Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Namespace ind.bk
    ''' <summary>
    ''' Database manager
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DBMgr

        Public Shared dbctx As ind.db.DBHelper

        Public Enum ddbbActionEnum As Byte
            create = 0
            update = 1
            delete = 2
        End Enum

#Region "private"
        ''' <summary>
        ''' DDBB connection string
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared connString As String = ind.bk.DBMgr.configure
        ''' <summary>SQL Server host from the DDBBSERVER application setting.</summary>
        Friend Shared DDBBSERVER As String
        ''' <summary>Database name from the DDBBCATALOG application setting.</summary>
        Friend Shared DDBBCATALOG As String
        ''' <summary>SQL login name from DDBBUSERID; never written together with its password.</summary>
        Friend Shared DDBBUSERID As String
        ''' <summary>
        ''' SQL Server password read from DDBBPASSWORD in the host applicationSettings section.
        ''' Empty values are rejected; this secret must never be written to a log.
        ''' This setting controls legacy DBMgr queries and is independent of cfrbookEntities.
        ''' </summary>
        Friend Shared DDBBPASSWORD As String
        ''' <summary>
        ''' indicates if the class was configured
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared configSet As Boolean = False

        ''' <summary>
        ''' Configure general connection settings
        ''' </summary>
        ''' <remarks>
        ''' A non-empty environment connection string is authoritative. Otherwise all four
        ''' DDBB application settings are required and are assembled without logging secrets.
        ''' Reloading configuration replaces the process-wide connection used by legacy calls.
        ''' </remarks>
        Public Shared Function configure() As String
            ind.bk.DBMgr.connString = ind.bk.DBMgr.getConnString
            ind.bk.DBMgr.dbctx = New ind.db.DBHelper(ind.bk.DBMgr.connString)
            Return ind.bk.DBMgr.connString
        End Function


        ''' <summary>
        ''' Set connection string to the class and to (dbctx As ind.db.DBHelper)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function getConnString() As String
            Try

                If DBMgr.configSet = False Then
                    AddHandler cfr.tool.ConfigWatcher.configFileChanged, AddressOf DBMgr.configure
                    DBMgr.configSet = True
                End If

                Dim environmentConnectionString As String = cfr.cte.Config.GetEnvironmentSqlConnectionString()
                If Not String.IsNullOrWhiteSpace(environmentConnectionString) Then
                    cfr.tool.LogMgr.log("Database connection configured from " & cfr.cte.Config.SqlConnectionStringEnvironmentVariable & ".")
                    Return environmentConnectionString
                End If


                Dim appSettingSection As Configuration.ClientSettingsSection = cfr.cte.Config.getConfigSection
                DBMgr.DDBBSERVER = cfr.cte.Config.getSetting(appSettingSection, "DDBBSERVER")
                DBMgr.DDBBCATALOG = cfr.cte.Config.getSetting(appSettingSection, "DDBBCATALOG")
                DBMgr.DDBBUSERID = cfr.cte.Config.getSetting(appSettingSection, "DDBBUSERID")
                DBMgr.DDBBPASSWORD = cfr.cte.Config.getSetting(appSettingSection, "DDBBPASSWORD")

                If String.IsNullOrWhiteSpace(DBMgr.DDBBSERVER) OrElse
                   String.IsNullOrWhiteSpace(DBMgr.DDBBCATALOG) OrElse
                   String.IsNullOrWhiteSpace(DBMgr.DDBBUSERID) OrElse
                   String.IsNullOrWhiteSpace(DBMgr.DDBBPASSWORD) Then
                    cfr.tool.LogMgr.log("Database configuration error based on config file.")
                    Throw New System.Configuration.ConfigurationErrorsException("DDBBSERVER, DDBBCATALOG, DDBBUSERID and DDBBPASSWORD are required for legacy DBMgr queries.")
                End If

                Dim builder As New SqlConnectionStringBuilder With {
                    .DataSource = DBMgr.DDBBSERVER,
                    .InitialCatalog = DBMgr.DDBBCATALOG,
                    .UserID = DBMgr.DDBBUSERID,
                    .Password = DBMgr.DDBBPASSWORD,
                    .PacketSize = 4096,
                    .MaxPoolSize = 100,
                    .MinPoolSize = 10,
                    .Pooling = True,
                    .PersistSecurityInfo = False
                }

                cfr.tool.LogMgr.log("Database connection configured for " & DBMgr.DDBBSERVER & ";" & DBMgr.DDBBCATALOG & ".")
                Return builder.ConnectionString
            Catch ex As Exception
                cfr.tool.LogMgr.log(ex)
                Throw
            End Try

        End Function
        



#End Region

        Public Shared Function getDT(ByVal SqlCommand As String) As Data.DataTable
            Dim dt As DataTable = ind.db.DBHelper.ExecuteDataset(connString, Data.CommandType.Text, SqlCommand).Tables(0)
            Return dt
        End Function

        Public Shared Function getDT(ByVal SqlCommand As String,
                                     ByVal ParamArray parameters() As SqlClient.SqlParameter) As Data.DataTable
            Dim dt As DataTable = ind.db.DBHelper.ExecuteDataset(connString, Data.CommandType.Text, SqlCommand, parameters).Tables(0)
            Return dt
        End Function

        Public Shared Function getDT(ByVal SqlCommand As String, ByRef conn As SqlClient.SqlConnection) As Data.DataTable
            Dim dt As DataTable = ind.db.DBHelper.ExecuteDataset(conn, Data.CommandType.Text, SqlCommand).Tables(0)
            Return dt
        End Function

        Public Shared Function getDT(ByVal SqlCommand As String, ByRef trans As SqlClient.SqlTransaction) As Data.DataTable
            Dim dt As DataTable = ind.db.DBHelper.ExecuteDataset(trans, Data.CommandType.Text, SqlCommand).Tables(0)
            Return dt
        End Function

        Public Shared Function getDT(ByVal storedProcName As String, ByVal paramCol() As SqlClient.SqlParameter, Optional ByRef trans As SqlClient.SqlTransaction = Nothing) As Data.DataTable
            If trans Is Nothing Then
                Return ind.db.DBHelper.ExecuteDataset(DBMgr.connString, storedProcName, paramCol).Tables(0)
            Else
                Return ind.db.DBHelper.ExecuteDataset(trans, storedProcName, paramCol).Tables(0)
            End If
        End Function


        Public Shared Function getDataTable(ByRef transaction As SqlTransaction, _
                                                      ByVal spName As String, _
                                                      Optional ByVal parameterValues As Generic.List(Of System.Data.SqlClient.SqlParameter) = Nothing) As DataTable

            Dim ds As DataSet = getDataset(transaction, spName, parameterValues)
            If ds.Tables.Count > 0 Then
                Return ds.Tables(0)
            Else
                Return Nothing
            End If
        End Function


        Public Shared Function getDataTable(ByVal spName As String, _
                                                      Optional ByVal parameterValues As Generic.List(Of System.Data.SqlClient.SqlParameter) = Nothing) As DataTable

            Dim ds As DataSet = getDataset(Nothing, spName, parameterValues)
            If ds.Tables.Count > 0 Then
                Return ds.Tables(0)
            Else
                Return Nothing
            End If
        End Function


        ''' <summary>
        ''' Return a dataset, if transaction is nothing use basic connection string
        ''' </summary>
        ''' <param name="transaction"></param>
        ''' <param name="spName"></param>
        ''' <param name="parameterValues"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getDataset(ByRef transaction As SqlTransaction, _
                                                        ByVal spName As String, _
                                                        Optional ByVal parameterValues As Generic.List(Of System.Data.SqlClient.SqlParameter) = Nothing) As DataSet

            
            If Not parameterValues Is Nothing Then
                If transaction Is Nothing Then
                    Return ind.db.SqlHelper.ExecuteDataset(DBMgr.connString, spName, parameterValues.ToArray)
                Else
                    Return ind.db.SqlHelper.ExecuteDataset(transaction, spName, parameterValues.ToArray)
                End If
            Else
                If transaction Is Nothing Then
                    Return ind.db.SqlHelper.ExecuteDataset(DBMgr.connString, spName, Nothing)
                Else
                    Return ind.db.SqlHelper.ExecuteDataset(transaction, spName, Nothing)
                End If
            End If
        End Function

        Public Shared Function Execute(ByVal SqlCommand As String) As Integer
            Return ind.db.DBHelper.ExecuteNonQuery(connString, Data.CommandType.Text, SqlCommand)
        End Function

        Public Shared Function Execute(ByVal SqlCommand As String,
                                       ByVal ParamArray parameters() As SqlClient.SqlParameter) As Integer
            Return ind.db.DBHelper.ExecuteNonQuery(connString, Data.CommandType.Text, SqlCommand, parameters)
        End Function

        Public Shared Function Execute(ByVal SqlCommand As String,
                                       ByVal trans As SqlClient.SqlTransaction,
                                       ByVal ParamArray parameters() As SqlClient.SqlParameter) As Integer
            Return ind.db.DBHelper.ExecuteNonQuery(trans, Data.CommandType.Text, SqlCommand, parameters)
        End Function

        Public Shared Function Execute(ByVal SqlCommand As String, ByRef conn As SqlClient.SqlConnection) As Integer
            Return ind.db.DBHelper.ExecuteNonQuery(conn, Data.CommandType.Text, SqlCommand)
        End Function

        Public Shared Function Execute(ByVal SqlCommand As String, ByVal trans As SqlClient.SqlTransaction) As Integer
            Return ind.db.DBHelper.ExecuteNonQuery(trans, Data.CommandType.Text, SqlCommand)
        End Function

        Public Shared Function ExecuteSp(ByVal storedProcName As String, ByVal paramCol() As SqlClient.SqlParameter, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As Integer
            If trans Is Nothing Then
                Using conn As SqlConnection = getNewConn()
                    Return ind.db.DBHelper.ExecuteNonQueryB(conn, Data.CommandType.StoredProcedure, storedProcName, paramCol)
                End Using

            Else
                Return ind.db.DBHelper.ExecuteNonQuery(trans, Data.CommandType.StoredProcedure, storedProcName, paramCol)
            End If
        End Function


        Public Const isolationLevelCte As Data.IsolationLevel = IsolationLevel.ReadCommitted
        ''' <summary>
        ''' return a connection with the base string for the aplication
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function getNewConn() As SqlClient.SqlConnection
            Return getNewConn(DBMgr.connString)
        End Function
        ''' <summary>
        ''' Return a connection
        ''' </summary>
        ''' <param name="connectionString"></param>
        ''' <returns></returns>
        Public Shared Function getNewConn(ByVal connectionString As String) As SqlClient.SqlConnection
            If String.IsNullOrWhiteSpace(connectionString) Then
                Throw New ArgumentException("The connection string cannot be empty.", NameOf(connectionString))
            End If

            Dim conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Return conn
            Catch
                conn.Dispose()
                Throw
            End Try
        End Function
        ''' <summary>
        ''' Cierra cualquier conexion
        ''' </summary>
        ''' <param name="conn"></param>
        ''' <returns></returns>
        Public Shared Function closeConn(ByRef conn As SqlClient.SqlConnection) As Boolean
            If Not conn Is Nothing Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                    Return True
                End If
            End If
            Return True
        End Function


#Region "Public shared transaction methods"
        ''' <summary>
        ''' Creates a transaction with IsolationLevel.ReadCommitted by default.
        ''' Use overloaded method to set a diferent isolationlevel
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function CreateTransaction() As SqlClient.SqlTransaction
            Return CreateTransaction(DBMgr.isolationLevelCte)
        End Function
        ''' <summary>
        ''' Returns a new transaction with her conection.
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function CreateTransaction(ByVal TransIsolationLevel As IsolationLevel) As SqlClient.SqlTransaction
            Dim conn As SqlConnection = Nothing
            Try
                conn = DBMgr.getNewConn()
                Return conn.BeginTransaction(TransIsolationLevel)
            Catch
                If conn IsNot Nothing Then
                    conn.Dispose()
                End If
                Throw
            End Try
        End Function
        ''' <summary>
        ''' return a new transaction
        ''' </summary>
        ''' <param name="conn"></param>
        ''' <returns></returns>
        Public Shared Function CreateTransaction(ByRef conn As SqlClient.SqlConnection) As SqlClient.SqlTransaction
            Dim ownsConnection As Boolean
            Try
                If conn Is Nothing Then
                    conn = DBMgr.getNewConn()
                    ownsConnection = True
                ElseIf conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                Return conn.BeginTransaction(DBMgr.isolationLevelCte)
            Catch
                If ownsConnection AndAlso conn IsNot Nothing Then
                    conn.Dispose()
                    conn = Nothing
                End If
                Throw
            End Try
        End Function
        ''' <summary>
        ''' Commit a transaction only if the owner of the transaction
        ''' </summary>
        ''' <param name="mytrans"></param>
        ''' <param name="transactionOwner"></param>
        Public Shared Sub CommitTransaction(ByRef mytrans As SqlClient.SqlTransaction, ByVal transactionOwner As Boolean)
            If transactionOwner = True Then
                CommitTransaction(mytrans)
            End If

        End Sub
        ''' <summary>
        ''' Commit a transaction
        ''' </summary>
        ''' <param name="mytrans"></param>
        Public Shared Sub CommitTransaction(ByRef mytrans As SqlClient.SqlTransaction)
            If mytrans IsNot Nothing Then
                mytrans.Commit()
            End If
        End Sub
        ''' <summary>
        ''' rollback a transaction only if the owner of the transaction
        ''' </summary>
        ''' <param name="mytrans"></param>
        ''' <param name="transactionOwner"></param>
        Public Shared Sub rollbackTransaction(ByRef mytrans As SqlClient.SqlTransaction, ByVal transactionOwner As Boolean)
            If transactionOwner = True Then
                rollbackTransaction(mytrans)
            End If

        End Sub
        ''' <summary>
        ''' RollBack a transaction
        ''' </summary>
        ''' <param name="mytrans"></param>
        Public Shared Sub rollbackTransaction(ByRef mytrans As SqlClient.SqlTransaction)
            Try
                If mytrans IsNot Nothing Then
                    mytrans.Rollback()
                End If
            Catch ex As Exception
                Throw New InvalidOperationException("The transaction could not be rolled back.", ex)
            End Try
        End Sub
        ''' <summary>
        ''' dispose the transaction and close connection
        ''' </summary>
        ''' <param name="trans"></param>
        ''' <param name="transactionOwner"></param>
        Public Shared Sub disposeTransaction(ByRef trans As SqlClient.SqlTransaction, Optional ByVal transactionOwner As Boolean = True)
            If Not transactionOwner OrElse trans Is Nothing Then
                Return
            End If

            Dim connection = trans.Connection
            trans.Dispose()
            trans = Nothing

            If connection IsNot Nothing Then
                connection.Dispose()
            End If

        End Sub

#End Region


        Public Shared Function getSpParameter(ByVal parameterName As String, ByVal parameterValue As String _
            , Optional ByVal parameterType As System.Data.SqlDbType = SqlDbType.NVarChar _
            , Optional ByVal ParameterDirection As System.Data.ParameterDirection = System.Data.ParameterDirection.Input) As SqlClient.SqlParameter
            ' Add the input parameter and set its properties.

            Dim parameter = CreateParameter(parameterName, parameterType, ParameterDirection)
            parameter.Value = If(parameterValue, CType(DBNull.Value, Object))

            Return parameter
        End Function

        Public Shared Function getSpParameterDate(ByVal parameterName As String, ByVal parameterValue As Date _
            , Optional ByVal parameterType As System.Data.SqlDbType = SqlDbType.DateTime2 _
            , Optional ByVal ParameterDirection As System.Data.ParameterDirection = System.Data.ParameterDirection.Input) As SqlClient.SqlParameter
            ' Add the input parameter and set its properties.

            Dim parameter = CreateParameter(parameterName, parameterType, ParameterDirection)
            parameter.Value = parameterValue

            Return parameter
        End Function

        Private Shared Function CreateParameter(parameterName As String,
                                                 parameterType As SqlDbType,
                                                 direction As ParameterDirection) As SqlParameter
            If String.IsNullOrWhiteSpace(parameterName) OrElse parameterName.TrimStart("@"c).Length = 0 Then
                Throw New ArgumentException("The parameter name cannot be empty.", NameOf(parameterName))
            End If

            Return New SqlParameter With {
                .ParameterName = "@" & parameterName.Trim().TrimStart("@"c),
                .SqlDbType = parameterType,
                .Direction = direction
            }
        End Function

    End Class
End Namespace
