Imports Microsoft.VisualBasic





Public Class UIMsg

    Public Enum msgbodyEnum As Byte
        normal = 1 'with format
        none = 0 'only message
    End Enum


    ''' <summary>
    ''' Show a message formated with bootstrap style
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="msgType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UIMessage(msg As String, Optional ByVal msgType As cfr.CFRConst.msgEnum = CFRConst.msgEnum.errorType) As String
        Dim s As String = ""

        Select Case msgType
            Case CFRConst.msgEnum.Ok
                s = "<div role='alert'  Class='alert alert-icon alert-success' ><p class='ok'>{0}</p></div>"
            Case CFRConst.msgEnum.info
                s = "<div role='alert'  Class='alert alert-icon alert-info' ><p class='info'>{0}</p></div>"
            Case CFRConst.msgEnum.warning
                s = "<div role='alert'  Class='alert alert-icon alert-warning' ><p class='warning'>{0}</p></div>"
            Case CFRConst.msgEnum.help
                s = "<div role='alert'  Class='alert alert-icon alert-help' ><p class='help'>{0}</p></div>"
            Case CFRConst.msgEnum.errorType
                s = "<div role='alert'  Class='alert alert-icon alert-danger' ><p class='fail'>{0}</p></div>"
        End Select


        Return String.Format(s, msg)
    End Function
    Public Shared Function UIMessageDismissInline(msg2Show As String _
                                            , Optional ByVal msgType As cfr.CFRConst.msgEnum = CFRConst.msgEnum.errorType _
                                            , Optional ByVal idpanel As String = "pnlerror" _
                                            , Optional msgBodyType As msgbodyEnum = msgbodyEnum.normal ) As String
        Return UIMessageDismiss(msg2Show, msgType, idpanel, msgBodyType, True)
    End Function

    ''' <summary>
    ''' Show a dismissible message formated with bootstrap style
    ''' </summary>
    ''' <param name="msg2Show"></param>
    ''' <param name="msgType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UIMessageDismiss(msg2Show As String _
                                            , Optional ByVal msgType As cfr.CFRConst.msgEnum = CFRConst.msgEnum.errorType _
                                            , Optional ByVal idpanel As String = "pnlerror" _
                                            , Optional msgBodyType As msgbodyEnum = msgbodyEnum.normal _
                                            , Optional inline As Boolean = False) As String


        msg2Show = cleanIllegalStr(msg2Show)

        Dim s As New StringBuilder
        Dim smsg As String = ""
        Dim alertClass As String = ""
        If msgBodyType = msgbodyEnum.normal Then
            Select Case msgType
                Case CFRConst.msgEnum.Ok
                    smsg = "<span class='ok'>{0}</span>"
                Case CFRConst.msgEnum.errorType
                    smsg = "<span class='fail'>{0}</span>"
                Case CFRConst.msgEnum.warning
                    alertClass = "warning"
                    smsg = "<span class='warning'>{0}</span>"
                Case CFRConst.msgEnum.info
                    alertClass = "info"
                    smsg = "<span class='info'>{0}</span>"
                Case Else
                    smsg = "{0}"
            End Select

            smsg = String.Format(smsg, msg2Show)

        ElseIf msgBodyType = msgbodyEnum.none Then
            smsg = msg2Show
        End If

        Select Case msgType
            Case CFRConst.msgEnum.Ok
                alertClass = "success"
            Case CFRConst.msgEnum.errorType
                alertClass = "danger"
            Case CFRConst.msgEnum.warning
                alertClass = "warning"
            Case CFRConst.msgEnum.info
                alertClass = "info"
        End Select

        Dim inlineclass As String = ""
        If inline = True Then
            inlineclass = " alert-inline "
        End If

        s.AppendFormat("<div id='{1}' role='alert'  Class='alert alert-{0} alert-dismissible {2}' >", alertClass, idpanel, inlineclass)
        s.Append("<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span class='glyphicon glyphicon-remove'></span></button>")
        s.AppendFormat(smsg)
        s.Append("</div>")

        Return s.ToString
    End Function

    Public Shared Sub registerScript(mypage As Page, jsFunctions As String, functionName As String)
        Dim sb As New System.Text.StringBuilder()
        'sb.Append("<script type='text/javascript'>")
        'functions to execute
        sb.Append(jsFunctions)
        'sb.Append("</script>")
        ScriptManager.RegisterClientScriptBlock(mypage, mypage.GetType(), functionName, sb.ToString(), True)
    End Sub
    ''' <summary>
    ''' clean vbCrLf in exception messages to show to the client
    ''' </summary>
    ''' <param name="inputException"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function cleanIllegalStr(inputException As Exception) As String
        Return cleanIllegalStr(inputException.Message)
    End Function

    ''' <summary>
    ''' clean vbCrLf in exception messages to show to the client
    ''' </summary>
    ''' <param name="inputString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function cleanIllegalStr(inputString As String) As String
        Dim msgclean As String = inputString
        'Dim illegalInFileName As New Regex("[\\/:*?""<>|]")
        'msgclean = illegalInFileName.Replace(msgclean, "")
        msgclean = msgclean _
                    .Replace(vbCrLf, "") _
                    .Replace(vbLf, "") _
                    .Replace(vbCr, "")
        msgclean = HttpUtility.HtmlEncode(msgclean)
        Return msgclean
    End Function

End Class
