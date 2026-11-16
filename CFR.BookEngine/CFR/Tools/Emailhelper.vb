'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' SAMPLE: Generates random password, which complies with the strong password
'         rules and does not contain ambiguous characters.
'
' To run this sample, create a new Visual Basic.NET project using the Console
' Application template and replace the contents of the Module1.vb file with
Imports System
Imports System.Security.Cryptography

Namespace ind.tools


    Public Class Emailhelper

        Public Shared Function validateEmail(email As String) As Boolean

            Try
                Dim adr As New System.Net.Mail.MailAddress(email)
                Return True
            Catch ex As Exception
                Return False
            End Try


        End Function

    End Class


End Namespace

