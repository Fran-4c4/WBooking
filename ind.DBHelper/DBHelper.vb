Imports System.Data.SqlClient


Namespace ind.db

    Public Class DBHelper
        Inherits SqlHelper

        Private mObjChanged As Boolean = False
        Public Property objChanged As Boolean
            Get
                Return Me.mObjChanged
            End Get
            Set(value As Boolean)
                Me.mObjChanged = True
            End Set
        End Property


        <AttributeUsage(AttributeTargets.Property, Inherited:=True)> _
        Public Class DBColumnNameAttribute
            Inherits Attribute

            Public dbColumn As String = ""
            Public Sub New(dbColumn As String)
                Me.dbColumn = dbColumn
            End Sub

        End Class


#Region "Conection string"
        ''' <summary>
        ''' Connection string to DDBB
        ''' </summary>
        Public Shared connStr As String
#End Region
        Public Sub New()

        End Sub


        Public Sub New(DataBaseConnectionString As String)
            DBHelper.connStr = DataBaseConnectionString
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
                    Return SqlHelper.ExecuteNonQuery(DBHelper.connStr, spName, parameterValues.ToArray)
                Else
                    Return SqlHelper.ExecuteNonQuery(transaction, spName, parameterValues.ToArray)
                End If
            Else
                If transaction Is Nothing Then
                    Return SqlHelper.ExecuteNonQuery(DBHelper.connStr, spName, Nothing)
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
                    Return SqlHelper.ExecuteScalar(DBHelper.connStr, spName, parameterValues.ToArray)
                Else
                    Return SqlHelper.ExecuteScalar(transaction, spName, parameterValues.ToArray)
                End If
            Else
                If transaction Is Nothing Then
                    Return SqlHelper.ExecuteScalar(DBHelper.connStr, spName, Nothing)
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
                    Return SqlHelper.ExecuteDataset(DBHelper.connStr, spName, parameterValues.ToArray)
                Else
                    Return SqlHelper.ExecuteDataset(transaction, spName, parameterValues.ToArray)
                End If
            Else
                If transaction Is Nothing Then
                    Return SqlHelper.ExecuteDataset(DBHelper.connStr, spName, Nothing)
                Else
                    Return SqlHelper.ExecuteDataset(transaction, spName, Nothing)
                End If
            End If



        End Function

        Public Overridable Function GetData(ByVal cmd As System.Data.SqlClient.SqlCommand, Optional trans As IDbTransaction = Nothing) As System.Data.DataTable
            Dim con As System.Data.SqlClient.SqlConnection = Nothing
            Dim isConnectionOwner As Boolean = False
            Try
                If trans Is Nothing Then
                    If cmd.Connection Is Nothing Then
                        con = New System.Data.SqlClient.SqlConnection(DBHelper.connStr)
                        cmd.Connection = con
                        con.Open()
                        isConnectionOwner = True
                    End If
                Else
                    cmd.Connection = trans.Connection
                    cmd.Transaction = trans
                End If

                Dim dr As System.Data.SqlClient.SqlDataReader
                Dim dt As System.Data.DataTable
                dt = New System.Data.DataTable()
                dr = cmd.ExecuteReader
                dt.Load(dr)
                If trans Is Nothing Then
                    If Not con Is Nothing Then
                        con.Close()
                    End If
                End If

                Return dt
            Catch ex As System.Exception
                'log
                Throw ex
            Finally
                If isConnectionOwner = True Then
                    If Not con Is Nothing Then
                        con.Close()
                    End If
                End If
            End Try
        End Function

        Public Shared Function configureSqlCommand(storedProcedureName As String) As System.Data.SqlClient.SqlCommand
            Dim cmd As New System.Data.SqlClient.SqlCommand
            cmd.CommandText = storedProcedureName
            cmd.CommandType = CommandType.StoredProcedure
            Return cmd
        End Function

        Public Shared Sub addSqlParameterFull(ByRef cmdOwner As SqlCommand, ParameterName As String, size As Integer, value As Object)
            Dim cmdParameter As System.Data.SqlClient.SqlParameter = New System.Data.SqlClient.SqlParameter()
            cmdParameter.ParameterName = String.Format("@{0}", ParameterName)
            cmdParameter.Size = size
            cmdParameter.Value = value
            cmdOwner.Parameters.Add(cmdParameter)
        End Sub

        Public Shared Sub addSqlParameterSimple(ByRef cmdOwner As SqlCommand, ParameterName As String, value As Object)
            Dim cmdParameter As System.Data.SqlClient.SqlParameter = New System.Data.SqlClient.SqlParameter()
            cmdParameter.ParameterName = String.Format("@{0}", ParameterName)
            Dim value2Insert As Object = Nothing
            If IsDate(value) Then
                If value = Date.MinValue Then
                    'do not use the value because is the default value of .net framework
                Else
                    value2Insert = value
                End If
            Else
                value2Insert = value
            End If
            cmdParameter.Value = value2Insert
            cmdOwner.Parameters.Add(cmdParameter)
        End Sub

    End Class
End Namespace
