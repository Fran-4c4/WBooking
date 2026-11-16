Imports Microsoft.VisualBasic

Namespace cfr
    <Serializable()> _
    Public Class Agencia
        Inherits BasicDetails

        Private grupovar As String
        Private agenteVar As String

        Public Property Grupo() As String
            Get
                Return Me.grupovar
            End Get
            Set(ByVal value As String)
                Me.grupovar = cfr.tool.Tools.cNull(value)
            End Set
        End Property

        Public Property Agente() As String
            Get
                Return Me.agenteVar
            End Get
            Set(ByVal value As String)
                Me.agenteVar = cfr.tool.Tools.cNull(value)
            End Set
        End Property
    End Class
End Namespace