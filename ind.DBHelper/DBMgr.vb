Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Namespace ind.db
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
        Friend Shared connString As String = ind.db.DBMgr.configure
        Friend Shared DDBBSERVER As String
        Friend Shared DDBBCATALOG As String
        Friend Shared DDBBUSERID As String
        ''' <summary>
        ''' SQL Server password used only by the legacy DBMgr connection path.
        ''' It must be supplied by the host configuration and must never be logged.
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
        ''' <remarks></remarks>
        Public Shared Function configure() As String
            ind.db.DBMgr.connString = ind.db.DBMgr.getConnString
            ind.db.DBMgr.dbctx = New ind.db.DBHelper(ind.db.DBMgr.connString)
            Return ind.db.DBMgr.connString
        End Function


        ''' <summary>
        ''' Set connection string to the class and to (dbctx As ind.db.DBHelper)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function getConnString() As String
            Dim connStr As String = "packet size=4096;Max Pool Size=100;Min Pool Size=10;Pooling=true;" & _
            "User ID={DDBBUSERID};" & _
            "Password={DDBBPASSWORD};" & _
            "Initial Catalog={DDBBCATALOG};" & _
            "Data Source={DDBBSERVER};" & _
            "persist security info=true;"

            Try



                connStr = connStr.Replace("{DDBBSERVER}", DBMgr.DDBBSERVER)
                connStr = connStr.Replace("{DDBBCATALOG}", DBMgr.DDBBCATALOG)
                connStr = connStr.Replace("{DDBBUSERID}", DBMgr.DDBBUSERID)
                connStr = connStr.Replace("{DDBBPASSWORD}", DBMgr.DDBBPASSWORD)

                'it will set in configure but in any case
                ind.db.DBHelper.connStr = connStr

                Return connStr
            Catch ex As Exception
                Throw ex
            End Try

        End Function




#End Region

        Public Shared Function getDT(ByVal SqlCommand As String) As Data.DataTable
            Dim dt As DataTable = ind.db.DBHelper.ExecuteDataset(connString, Data.CommandType.Text, SqlCommand).Tables(0)
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

        <Obsolete("Use getdatatable instead")>
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
            Dim conn As New SqlClient.SqlConnection
            conn.ConnectionString = connectionString
            conn.Open()
            Return conn
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
        ''' Creates a transaction with isolationlevel set to IsolationLevel.Serializable by default.
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
            Dim conn As SqlClient.SqlConnection
            Dim myTrans As SqlClient.SqlTransaction
            Try
                conn = DBMgr.getNewConn()
                myTrans = conn.BeginTransaction(TransIsolationLevel)
                Return myTrans
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        ''' <summary>
        ''' return a new transaction
        ''' </summary>
        ''' <param name="conn"></param>
        ''' <returns></returns>
        Public Shared Function CreateTransaction(ByRef conn As SqlClient.SqlConnection) As SqlClient.SqlTransaction
            Dim myTrans As SqlClient.SqlTransaction
            Try
                If conn Is Nothing Then
                    conn = DBMgr.getNewConn()
                ElseIf conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                myTrans = conn.BeginTransaction(DBMgr.isolationLevelCte)
                Return myTrans
            Catch ex As Exception
                Throw ex
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
            Try
                If Not mytrans Is Nothing Then
                    mytrans.Commit()
                End If
            Catch ex As Exception
                Try
                    DBMgr.rollbackTransaction(mytrans)
                Catch exroll As Exception

                End Try
            Finally
                'this is due to a bug in .net that set the subsequents proc to an isolation level diferent from readcommited
                Try
                    Dim tempTrans As SqlClient.SqlTransaction = CreateTransaction(IsolationLevel.ReadCommitted)
                    tempTrans.Commit()
                    closeConn(tempTrans.Connection)
                Catch ex As Exception

                End Try
            End Try
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
                If Not mytrans Is Nothing Then
                    mytrans.Rollback()
                End If

            Catch ex As Exception
                Throw New Exception("Fatal error, The system was not perform the rollback transaction during error")
            Finally
                'this is due to a bug in .net that set the subsequents proc to an isolation level diferent from readcommited

                Try
                    Dim tempTrans As SqlClient.SqlTransaction = CreateTransaction(IsolationLevel.ReadCommitted)
                    tempTrans.Commit()
                    closeConn(tempTrans.Connection)
                Catch ex As Exception

                End Try
            End Try
        End Sub
        ''' <summary>
        ''' dispose the transaction and close connection
        ''' </summary>
        ''' <param name="trans"></param>
        ''' <param name="transactionOwner"></param>
        Public Shared Sub disposeTransaction(ByRef trans As SqlClient.SqlTransaction, Optional ByVal transactionOwner As Boolean = True)
            Try
                If transactionOwner = False Then
                    Exit Sub
                End If
                If trans Is Nothing Then
                    Exit Sub
                End If

                If Not trans.Connection Is Nothing Then
                    closeConn(trans.Connection)
                End If

                trans.Dispose()

                trans = Nothing

            Catch ex As Exception
                Throw ex
            End Try

        End Sub

#End Region


        Public Shared Function getSpParameter(ByVal parameterName As String, ByVal parameterValue As String _
            , Optional ByVal parameterType As System.Data.SqlDbType = SqlDbType.NVarChar _
            , Optional ByVal ParameterDirection As System.Data.ParameterDirection = System.Data.ParameterDirection.Input) As SqlClient.SqlParameter
            ' Add the input parameter and set its properties.

            parameterName = parameterName.Replace("@", "")
            Dim parameter As New SqlClient.SqlParameter
            parameter.ParameterName = "@" & parameterName ' the parameter must come without @
            parameter.DbType = Data.DbType.String
            parameter.Direction = ParameterDirection
            parameter.Value = parameterValue

            Return parameter
        End Function

        Public Shared Function getSpParameterDate(ByVal parameterName As String, ByVal parameterValue As Date _
            , Optional ByVal parameterType As System.Data.SqlDbType = SqlDbType.NVarChar _
            , Optional ByVal ParameterDirection As System.Data.ParameterDirection = System.Data.ParameterDirection.Input) As SqlClient.SqlParameter
            ' Add the input parameter and set its properties.

            parameterName = parameterName.Replace("@", "")
            Dim parameter As New SqlClient.SqlParameter
            parameter.ParameterName = "@" & parameterName ' the parameter must come without @
            parameter.DbType = Data.DbType.DateTime
            parameter.Direction = ParameterDirection
            parameter.Value = parameterValue

            Return parameter
        End Function

    End Class
End Namespace
