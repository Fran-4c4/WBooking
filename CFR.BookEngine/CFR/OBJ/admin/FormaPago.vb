Imports Microsoft.VisualBasic

Namespace cfr

    <Serializable()> _
        Public Class FormaPago
        'FORMA DE PAGO
        Public forma_pago As CFRConst.formaPago = CFRConst.formaPago.CreditCard
        Public creditCard As New CreditCardObj

        'Estado
        Public estadoPago As String


        Public Sub New()
            Me.estadoPago = "0"
        End Sub
    End Class
End Namespace