
Namespace cfr.erp
    Public Class CustomerObj

        Public Enum BlockedOptions As Byte
            Blocked = 1
            Unblocked = 0
        End Enum
        Public id_Cliente As Integer
        Public Description As String           '
        Public blockBook As cfr.erp.CustomerObj.BlockedOptions = BlockedOptions.Unblocked
        'Public Direccion As String         '
        'Public CodigoPostal As String      '
        'Public Localidad As String         '
        'Public Zona As String              '
        'Public Pais As String              '
        'Public NumeroFiscal As String      '
        'Public EmailFacturacion As String  '
        'Public ContactoNombre As String    '
        'Public ContactoTelefono As String  '
        'Public ContactoTelefono2 As String  '
        'Public ContactoTelefono3 As String  '


        'Public ContactoFax As String       '
        'Public ContactoCargo As String       '

        'Public CuentaBancaria As String    '
        'Public FormaPago As yah_FormaPago
        'Public fechaAlta As Date         '
        'Public fechaBaja As Date         '
        'Public FechaModificacion As Date '
        'Public Login As String       '
        'Public clave As String
        'Public WebCliente As String

    End Class
End Namespace

