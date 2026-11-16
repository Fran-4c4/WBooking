Imports Microsoft.VisualBasic


Namespace cfr

    ''' <summary>
    ''' Class object that encapsulate the data needed to send an email
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MailDataObj


        'origin
        Public MailFrom As String = String.Empty
        Public emailToResponse As String
        Public FromName As String = String.Empty

        'destiny
        Public MailTo As New ArrayList
        Public subject As String = String.Empty
        Public bodyHtml As String = String.Empty
        Public bodyAlt As String = String.Empty

        'second email, only 1 is allowed in aspEmail component
        Public MailCC As New ArrayList
        Public MailBCC As New ArrayList

        'type
        Public isHtml As Boolean = True


    End Class
End Namespace