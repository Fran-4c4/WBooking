Imports Microsoft.VisualBasic

Namespace cfr
    <Serializable()> _
    Public Class CreditCardObj
        Public Enum CardTypeEnum As Integer
            Visa = 1
            Amex = 2
            master = 3
        End Enum
        Public Card_Type As String
        Public ntarjeta As String
        Public titular As String
        Public CCUTarjeta As String
        Public expiricyYear As Integer
        Public expiricyMonth As Integer
    End Class
End Namespace