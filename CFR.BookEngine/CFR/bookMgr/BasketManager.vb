Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Namespace cfr

    Public Class BasketManager

        Private Function getBookResBySignature(ByVal signature As String) As DataTable
            Return GetBasketByTextValue("SELECT * FROM R_CESTA WHERE SIGNATURE=@value", "@value", signature)
        End Function

        Private Function getBookResByOrder(ByVal Ds_Order As String) As DataTable
            Return GetBasketByTextValue("SELECT * FROM R_CESTA WHERE SESSIONID=@value", "@value", Ds_Order)
        End Function


        ''' <summary>
        ''' Get bookRes from repository
        ''' </summary>
        ''' <param name="signature"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getBookResFromStorageBySignature(ByVal signature As String) As cfr.BookRes
            Try
                Dim dt As DataTable = Me.getBookResBySignature(signature)
                If dt.Rows.Count = 0 Then
                    Return Nothing
                End If
                Dim bookDataSerialized As String = dt.Rows(0)(ind.bk.obj.R_Cesta.DBCol.tpvData)

                Return getBookResDeserialized(bookDataSerialized)
            Catch ex As Exception
                cfr.tool.LogMgr.log("Error deserializing " & signature & " - " & ex.Message)
            End Try

            Return Nothing


        End Function


        ''' <summary>
        ''' Get bookRes from repository
        ''' </summary>
        ''' <param name="Ds_Order"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getBookResFromStorageByDsOrder(ByVal Ds_Order As String) As cfr.BookRes
            Try
                Dim dt As DataTable = Me.getBookResByOrder(Ds_Order)
                If dt.Rows.Count = 0 Then
                    Return Nothing
                End If
                Dim bookDataSerialized As String = dt.Rows(0)(ind.bk.obj.R_Cesta.DBCol.tpvData)

                Return getBookResDeserialized(bookDataSerialized)
            Catch ex As Exception
                cfr.tool.LogMgr.log("Error deserializing " & Ds_Order & " - " & ex.Message)
            End Try

            Return Nothing

        End Function


        Private Function getBookResDeserialized(ByVal bookDataSerialized As String) As BookRes
            Throw New NotSupportedException(
                "Legacy payment-basket deserialization is disabled. Implement a versioned, validated DTO before enabling a payment provider.")
        End Function

        ''' <summary>
        ''' Save the BookRes with all the data
        ''' </summary>
        ''' <param name="dr"></param>
        ''' <param name="pd"></param>
        ''' <remarks></remarks>
        Public Sub saveBookResToTPVStorage(ByVal dr As cfr.BookRes, ByVal pd As PayGateWayManager)
            Throw New NotSupportedException(
                "Legacy payment-basket persistence is disabled. Implement a versioned, validated DTO before enabling a payment provider.")
        End Sub

        ''' <summary>
        ''' Delete a basket info from repository
        ''' </summary>
        ''' <param name="Order"></param>
        ''' <remarks></remarks>
        Public Shared Sub deleteBookResFromStorage(ByVal Order As String)
            Try
                Dim basketId As Integer
                If Not Integer.TryParse(Order, basketId) Then
                    Throw New ArgumentException("The basket identifier is invalid.", NameOf(Order))
                End If

                Using connection As SqlConnection = ind.bk.DBMgr.getNewConn()
                    Using command As New SqlCommand("DELETE FROM R_CESTA WHERE ID=@id", connection)
                        command.Parameters.Add("@id", SqlDbType.Int).Value = basketId
                        command.ExecuteNonQuery()
                    End Using
                End Using
            Catch ex As Exception
                cfr.tool.LogMgr.log(ex)
            End Try
        End Sub

        Private Shared Function GetBasketByTextValue(ByVal commandText As String,
                                                     ByVal parameterName As String,
                                                     ByVal value As String) As DataTable
            Dim result As New DataTable()
            Using connection As SqlConnection = ind.bk.DBMgr.getNewConn()
                Using command As New SqlCommand(commandText, connection)
                    command.Parameters.Add(parameterName, SqlDbType.NVarChar, 512).Value = If(value, "")
                    Using adapter As New SqlDataAdapter(command)
                        adapter.Fill(result)
                    End Using
                End Using
            End Using
            Return result
        End Function
    End Class
End Namespace
