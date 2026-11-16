Imports Microsoft.VisualBasic

Namespace cfr
    ''' <summary>
    ''' private client basic data
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Cliente
        Inherits BasicDetails

        'Datos del cliente

        Private _surnames As String

        Public Property Apellido1() As String
            Get
                Return Me._surnames
            End Get
            Set(ByVal value As String)
                Me._surnames = cfr.tool.Tools.cNull(value)
            End Set
        End Property
        ''' <summary>
        ''' driver age for car properties
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property driverAge() As Byte

    End Class
End Namespace