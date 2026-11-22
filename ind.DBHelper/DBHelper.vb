Imports System.Data.SqlClient


Namespace ind.db

    Public Class DBHelper
        Inherits SqlHelper

        Private mObjChanged As Boolean
        Public Property objChanged As Boolean
            Get
                Return Me.mObjChanged
            End Get
            Set(value As Boolean)
                Me.mObjChanged = value
            End Set
        End Property


        <AttributeUsage(AttributeTargets.Property, Inherited:=True)> _
        Public Class DBColumnNameAttribute
            Inherits Attribute

            Public ReadOnly Property dbColumn As String

            Public Sub New(dbColumn As String)
                If String.IsNullOrWhiteSpace(dbColumn) Then
                    Throw New ArgumentException("The database column name cannot be empty.", NameOf(dbColumn))
                End If

                Me.dbColumn = dbColumn
            End Sub

        End Class


#Region "Connection string"
        Private Shared mConnectionString As String

        ''' <summary>
        ''' Gets the process-wide SQL Server connection string used by overloads that do not
        ''' receive a connection or transaction explicitly.
        ''' </summary>
        ''' <remarks>
        ''' The value has no default and must be supplied explicitly through
        ''' <see cref="DBMgr.configure(String)"/> or the constructor that accepts a connection
        ''' string. Empty values are rejected. The exact value is passed to
        ''' <see cref="SqlConnection"/>; it is never read from an environment variable or
        ''' persisted by this library. Changing it affects all subsequent process-wide calls.
        ''' </remarks>
        Public Shared ReadOnly Property ConnectionString As String
            Get
                If String.IsNullOrWhiteSpace(mConnectionString) Then
                    Throw New InvalidOperationException(
                        "No database connection has been configured. Call DBMgr.configure(connectionString) first.")
                End If

                Return mConnectionString
            End Get
        End Property

        Friend Shared Sub ConfigureConnectionString(value As String)
            If String.IsNullOrWhiteSpace(value) Then
                Throw New ArgumentException("The connection string cannot be empty.", NameOf(value))
            End If

            mConnectionString = value
        End Sub
#End Region
        Public Sub New()

        End Sub


        Public Sub New(DataBaseConnectionString As String)
            ConfigureConnectionString(DataBaseConnectionString)
        End Sub


        Public Function executeDirectSql(ByVal transaction As SqlTransaction,
                                                    ByVal commandText As String
                                                    ) As Integer
            Return SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, commandText, CType(Nothing, SqlParameter()))
        End Function


        Public Function getNonQuery(ByVal transaction As SqlTransaction, _
                                                    ByVal spName As String, _
                                                    Optional ByVal parameterValues As Generic.List(Of System.Data.IDbDataParameter) = Nothing) As Integer


            If Not parameterValues Is Nothing Then
                If transaction Is Nothing Then
                    Return SqlHelper.ExecuteNonQuery(DBHelper.ConnectionString, spName, parameterValues.ToArray)
                Else
                    Return SqlHelper.ExecuteNonQuery(transaction, spName, parameterValues.ToArray)
                End If
            Else
                If transaction Is Nothing Then
                    Return SqlHelper.ExecuteNonQuery(DBHelper.ConnectionString, spName, Nothing)
                Else
                    Return SqlHelper.ExecuteNonQuery(transaction, spName, Nothing)
                End If
            End If



        End Function

        Public Function getEscalar(ByVal transaction As SqlTransaction, _
                                                        ByVal spName As String, _
                                                        Optional ByVal parameterValues As Generic.List(Of System.Data.IDbDataParameter) = Nothing) As Object


            If Not parameterValues Is Nothing Then
                If transaction Is Nothing Then
                    Return SqlHelper.ExecuteScalar(DBHelper.ConnectionString, spName, parameterValues.ToArray)
                Else
                    Return SqlHelper.ExecuteScalar(transaction, spName, parameterValues.ToArray)
                End If
            Else
                If transaction Is Nothing Then
                    Return SqlHelper.ExecuteScalar(DBHelper.ConnectionString, spName, Nothing)
                Else
                    Return SqlHelper.ExecuteScalar(transaction, spName, Nothing)
                End If
            End If



        End Function

        Public Function getDataTable(ByVal transaction As SqlTransaction, _
                                                            ByVal spName As String, _
                                                            Optional ByVal parameterValues As Generic.List(Of System.Data.IDbDataParameter) = Nothing) As DataTable

            Dim ds As DataSet = getDataset(transaction, spName, parameterValues)
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
        Public Function getDataset(ByVal transaction As SqlTransaction, _
                                                        ByVal spName As String, _
                                                        Optional ByVal parameterValues As Generic.List(Of System.Data.IDbDataParameter) = Nothing) As DataSet


            If Not parameterValues Is Nothing Then
                If transaction Is Nothing Then
                    Return SqlHelper.ExecuteDataset(DBHelper.ConnectionString, spName, parameterValues.ToArray)
                Else
                    Return SqlHelper.ExecuteDataset(transaction, spName, parameterValues.ToArray)
                End If
            Else
                If transaction Is Nothing Then
                    Return SqlHelper.ExecuteDataset(DBHelper.ConnectionString, spName, Nothing)
                Else
                    Return SqlHelper.ExecuteDataset(transaction, spName, Nothing)
                End If
            End If



        End Function

        Public Overridable Function GetData(ByVal cmd As System.Data.SqlClient.SqlCommand, Optional trans As IDbTransaction = Nothing) As System.Data.DataTable
            If cmd Is Nothing Then
                Throw New ArgumentNullException(NameOf(cmd))
            End If

            Dim connection As SqlConnection = Nothing
            Dim closeConnection As Boolean
            Dim disposeConnection As Boolean

            Try
                If trans Is Nothing Then
                    If cmd.Connection Is Nothing Then
                        connection = New SqlConnection(DBHelper.ConnectionString)
                        cmd.Connection = connection
                        disposeConnection = True
                    Else
                        connection = cmd.Connection
                    End If

                    If connection.State <> ConnectionState.Open Then
                        connection.Open()
                        closeConnection = True
                    End If
                Else
                    Dim sqlTransaction = TryCast(trans, SqlTransaction)
                    If sqlTransaction Is Nothing Then
                        Throw New ArgumentException(
                            "The transaction must be a System.Data.SqlClient.SqlTransaction.",
                            NameOf(trans))
                    End If

                    If sqlTransaction.Connection Is Nothing Then
                        Throw New ArgumentException("The transaction is no longer active.", NameOf(trans))
                    End If

                    cmd.Connection = sqlTransaction.Connection
                    cmd.Transaction = sqlTransaction
                End If

                Dim table As New DataTable()
                Using reader = cmd.ExecuteReader()
                    table.Load(reader)
                End Using

                Return table
            Finally
                If closeConnection AndAlso connection IsNot Nothing Then
                    connection.Close()
                End If

                If disposeConnection AndAlso connection IsNot Nothing Then
                    connection.Dispose()
                End If
            End Try
        End Function

        Public Shared Function configureSqlCommand(storedProcedureName As String) As System.Data.SqlClient.SqlCommand
            If String.IsNullOrWhiteSpace(storedProcedureName) Then
                Throw New ArgumentException("The stored procedure name cannot be empty.", NameOf(storedProcedureName))
            End If

            Return New SqlCommand(storedProcedureName) With {
                .CommandType = CommandType.StoredProcedure
            }
        End Function

        Public Shared Sub addSqlParameterFull(ByVal cmdOwner As SqlCommand, ParameterName As String, size As Integer, value As Object)
            ValidateParameterOwnerAndName(cmdOwner, ParameterName)
            If size <= 0 Then
                Throw New ArgumentOutOfRangeException(NameOf(size), size, "Parameter size must be greater than zero.")
            End If

            Dim cmdParameter As New SqlParameter With {
                .ParameterName = NormalizeParameterName(ParameterName),
                .Size = size,
                .Value = If(value, DBNull.Value)
            }
            cmdOwner.Parameters.Add(cmdParameter)
        End Sub

        Public Shared Sub addSqlParameterSimple(ByVal cmdOwner As SqlCommand, ParameterName As String, value As Object)
            ValidateParameterOwnerAndName(cmdOwner, ParameterName)

            Dim parameterValue As Object = value
            If value Is Nothing OrElse (TypeOf value Is Date AndAlso DirectCast(value, Date) = Date.MinValue) Then
                parameterValue = DBNull.Value
            End If

            Dim cmdParameter As New SqlParameter With {
                .ParameterName = NormalizeParameterName(ParameterName),
                .Value = parameterValue
            }
            cmdOwner.Parameters.Add(cmdParameter)
        End Sub

        Private Shared Sub ValidateParameterOwnerAndName(cmdOwner As SqlCommand, parameterName As String)
            If cmdOwner Is Nothing Then
                Throw New ArgumentNullException(NameOf(cmdOwner))
            End If

            If String.IsNullOrWhiteSpace(parameterName) OrElse parameterName.TrimStart("@"c).Length = 0 Then
                Throw New ArgumentException("The parameter name cannot be empty.", NameOf(parameterName))
            End If
        End Sub

        Private Shared Function NormalizeParameterName(parameterName As String) As String
            Return "@" & parameterName.Trim().TrimStart("@"c)
        End Function

    End Class
End Namespace
