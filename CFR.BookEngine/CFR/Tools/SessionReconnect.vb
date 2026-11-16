Imports Microsoft.VisualBasic
Imports System.Text


Namespace cfr.tool


    ''' <summary>
    ''' class used to set the reconect script function to maintain session open when the user have the internet browser open.
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SessionReconnect
        Public Shared Function KeepAlive() As String

            Dim int_MilliSecondsTimeOut As Integer = (Web.HttpContext.Current.Session.Timeout * 60000) - 30000
            Dim sScript As New StringBuilder
            sScript.Append("<script type='text/javascript'>" & vbNewLine)
            'Number Of Reconnects  
            sScript.Append("var count=0;" & vbNewLine)
            'Maximum reconnects Setting  
            sScript.Append("var max = 6;" & vbNewLine)
            sScript.Append("function Reconnect(){" & vbNewLine)
            sScript.Append("count++;" & vbNewLine)
            sScript.Append("var d = new Date();" & vbNewLine)
            sScript.Append("var curr_hour = d.getHours();" & vbNewLine)
            sScript.Append("var curr_min = d.getMinutes();" & vbNewLine)
            sScript.Append("if (count < max){" & vbNewLine)
            sScript.Append("window.status = 'Refreshed ' + count.toString() + ' time(s) [' + curr_hour + ':' + curr_min + ']';" & vbNewLine)
            sScript.Append("var img = new Image(1,1);" & vbNewLine)
            sScript.Append("img.src = 'http://localhost/myapp/reconnect.aspx';" & vbNewLine)

            sScript.Append("}" & vbNewLine)
            sScript.Append("}" & vbNewLine)
            sScript.Append("window.setInterval('Reconnect()',")
            sScript.Append(int_MilliSecondsTimeOut.ToString() & "); //Set to length required" & vbNewLine)
            sScript.Append("</script>")

            KeepAlive = sScript.ToString
        End Function
    End Class
End Namespace
