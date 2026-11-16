Imports Microsoft.VisualBasic

Public Class IUOptionsObj

    Private widthVar As String = String.Empty

    Public Property Width() As String
        Get
            Return Me.widthVar
        End Get
        Set(ByVal value As String)
            If Not value Is Nothing Then
                Me.widthVar = " width='" & value & "'"
            End If
        End Set
    End Property

End Class
