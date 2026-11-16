Imports Microsoft.VisualBasic

Namespace cfr

    <Serializable()> _
    Public Class Chain
        Public code As String
        Public description As String
        Public Sub New()

        End Sub
        Public Sub New(ByVal code As String, ByVal description As String)
            Me.code = code
            Me.description = description
        End Sub
    End Class
End Namespace