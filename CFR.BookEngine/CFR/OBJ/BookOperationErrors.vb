Imports Microsoft.VisualBasic

Namespace cfr


    Public Class BookOperationErrors

        'Public bookOperation As CFRConst.BookOperationInfoEnum
        Public statusInfo As Object
        Public errBook As String = basERR.S_OK
        Public exceptionThrowedVar As Exception
        Private msgVar As String
        Public Sub New()

        End Sub
        Public Sub New(ByVal errBook As String)
            Me.errBook = errBook
        End Sub

        Public ReadOnly Property msg() As String
            Get
                Return Me.msgVar
            End Get
        End Property


        Public Sub setMsg(ByVal msg As String, Optional ByVal clear As Boolean = True)
            If clear = True Then
                Me.msgVar = msg
            Else
                Me.msgVar += Chr(13) & Chr(10) & msg
            End If
        End Sub

        Public Property exceptionThrowed() As Exception
            Get
                Return Me.exceptionThrowedVar
            End Get
            Set(ByVal value As Exception)
                Me.exceptionThrowedVar = value
                If Me.errBook = basERR.S_OK Then
                    If IsNumeric(value.Message) Then
                        Me.errBook = value.Message
                    Else
                        Me.errBook = basERR.E_FAIL
                        Me.msgVar = value.Message
                    End If
                End If

            End Set
        End Property

    End Class
End Namespace