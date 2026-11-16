Imports Microsoft.VisualBasic

Namespace cfr

    Public Class OrderGeneratorManager
        ''' <summary>
        ''' Get ner order from datatable id control.
        ''' </summary>
        ''' <param name="valueToSave">optional Maximum 100 caracters</param>
        ''' <returns>Idorder, as string</returns>
        ''' <remarks></remarks>
        Public Shared Function getNewOrderId(Optional ByVal valueToSave As String = "") As String


            If valueToSave Is Nothing Then
                valueToSave = String.Empty
            End If
            If valueToSave.Length > 100 Then
                Throw New ArgumentException("Order session information cannot exceed 100 characters.", NameOf(valueToSave))
            End If

            Dim orderId As String = ""
            Const insertSql As String =
                "INSERT INTO OrderId (sesioninfo) VALUES (@sessionInfo); SELECT CAST(SCOPE_IDENTITY() AS int);"

            Dim ocon As Data.SqlClient.SqlConnection = Nothing
            Dim trans As Data.SqlClient.SqlTransaction = Nothing
            Try
                ocon = ind.bk.DBMgr.getNewConn
                'comienzo de la transaccion
                trans = ocon.BeginTransaction


                Using command As New Data.SqlClient.SqlCommand(insertSql, ocon, trans)
                    command.Parameters.Add("@sessionInfo", Data.SqlDbType.VarChar, 100).Value = valueToSave
                    orderId = Convert.ToString(command.ExecuteScalar(), Globalization.CultureInfo.InvariantCulture)
                End Using
                trans.Commit()
                trans.Dispose()
                trans = Nothing
                If String.IsNullOrEmpty(orderId) Then
                    Throw New Exception(basERR.E_Get_CodReservaO_Identity_0)
                End If

                Return orderId
            Catch ex As Exception
                Try
                    If Not trans Is Nothing Then
                        trans.Rollback()
                    End If

                Catch ex2 As Exception

                End Try
                orderId = getRandomId()
            Finally
                ind.bk.DBMgr.closeConn(ocon)

            End Try


            Return orderId

        End Function


        ''' <summary>
        ''' Get random id if the table fails.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function getRandomId() As String
            Dim rndValue As Double = Rnd(123)
            Dim rndValue2 As Double = Rnd(321)
            Dim rndValue3 As Double = Rnd(456)
            Dim rndResult As String = ((rndValue + rndValue2 + rndValue3) * 10000) + 90000
            Return rndResult.PadLeft(10).Substring(0, 6)
        End Function

    End Class

End Namespace
