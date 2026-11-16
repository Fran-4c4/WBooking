
Namespace cfr.exceptions
    Public Class BookException
        Inherits ApplicationException

        Public bookExceptionDescription As String = ""
        Public boe As New BookOperationErrors

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(ByVal message As String, FileName As String)
            MyBase.New(message)
            Me.bookExceptionDescription = FileName
        End Sub

        Public Sub New(ByVal message As String, FileName As String, innerException As Exception)
            MyBase.New(message, innerException)
            Me.bookExceptionDescription = FileName
        End Sub

        Public Sub New(ByVal message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        '''This constructor is needed for serialization.
        ''' </summary>
        ''' <param name="info"></param>
        ''' <param name="context"></param>
        ''' <remarks></remarks>
        Protected Sub New(info As Runtime.Serialization.SerializationInfo, context As Runtime.Serialization.StreamingContext)
            MyBase.New(info:=info, context:=context)
        End Sub
    End Class
End Namespace


