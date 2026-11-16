''' <summary>
''' Defines the payment methods that the open-source booking flow can execute.
''' </summary>
''' <remarks>
''' Transfer and direct payment create a reservation without WBooking receiving
''' card data. Card capture and the historical TPV values are deliberately
''' rejected until a hosted provider adapter with signed notifications exists.
''' This class is the authoritative allowlist used by both Admin configuration
''' and public booking submission.
''' </remarks>
Public NotInheritable Class PaymentMethodPolicy
    Private Sub New()
    End Sub

    Public Shared Function IsSupported(ByVal method As cfr.CFRConst.formaPago) As Boolean
        Return method = cfr.CFRConst.formaPago.Transfer OrElse
               method = cfr.CFRConst.formaPago.DirectPay
    End Function

    Public Shared Function TryParseSupported(ByVal value As String,
                                             ByRef method As cfr.CFRConst.formaPago) As Boolean
        Dim numericValue As Integer
        If Not Integer.TryParse(value, numericValue) OrElse
           Not [Enum].IsDefined(GetType(cfr.CFRConst.formaPago), numericValue) Then
            Return False
        End If

        method = CType(numericValue, cfr.CFRConst.formaPago)
        Return IsSupported(method)
    End Function
End Class
