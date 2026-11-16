Imports Microsoft.VisualBasic


Namespace cfr.tool

    ''' <summary>
    ''' class for some utils
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Tools
#Region "BBDD"
        ''' <summary>
        ''' Comprueba que un valor no es null. Si lo es devuelve una cadena vacia.
        ''' </summary>
        ''' <param name="mValue"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function cNull(ByVal mValue As Object) As String
            If mValue Is Nothing Then
                Return ""
            ElseIf mValue.GetType Is Type.GetType("System.DBNull") Then
                Return ""
            Else
                Return mValue
            End If
        End Function

        Public Shared Function cNull(ByVal mValue As Object, ByVal valxDefect As Object) As Object
            If mValue Is Nothing Then
                Return valxDefect
            ElseIf mValue.GetType Is Type.GetType("System.DBNull") Then
                Return valxDefect
            Else
                Return mValue
            End If
        End Function

        Public Shared Function cNullStr(ByVal mValue As Object) As String
            Return cfr.tool.Tools.cNullStr(mValue, "")
        End Function

        Public Shared Function cNullStr(ByVal mValue As Object, ByVal valxDefect As Object) As Object
            If mValue Is Nothing Then
                Return valxDefect
            ElseIf mValue.GetType Is Type.GetType("System.DBNull") Then
                Return valxDefect
            ElseIf CStr(mValue).Trim.Length = 0 Then
                Return valxDefect
            Else
                Return mValue
            End If
        End Function


        Public Shared Function ParserSql(ByVal strString As String) As String
            strString = cNull(strString, "")
            strString = Replace(strString, Chr(34), "''")
            strString = Replace(strString, "''", "'")
            strString = Replace(strString, "'", "''")
            Return strString
        End Function




        Public Shared Function checkForSQLInjection(userInput As String) As Boolean

            Dim sqlCheckList() As String = {"--",
                                           ";--",
                                           ";",
                                           "/*",
                                           "*/",
                                            "@@",
                                            "@",
                                            "char",
                                           "nchar",
                                           "varchar",
                                           "nvarchar",
                                           "alter",
                                           "begin",
                                           "cast",
                                           "create",
                                           "cursor",
                                           "declare",
                                           "delete",
                                           "drop",
                                           "end",
                                           "exec",
                                           "execute",
                                           "fetch",
                                                "insert",
                                              "kill",
                                                 "select",
                                               "sys",
                                                "sysobjects",
                                                "syscolumns",
                                               "table",
                                               "update"
                                           }
            Dim CheckString As String = userInput.Replace("'", "''")
            For i As Integer = 0 To sqlCheckList.Length - 1
                If ((CheckString.IndexOf(sqlCheckList(i), StringComparison.OrdinalIgnoreCase) >= 0)) Then
                    Return False
                End If
            Next

        End Function


#End Region


#Region "Matrix manipulation"
        Public Shared Function getObjectFromMatrix(ByRef idToSearch As Object, ByRef matrix() As Object) As Object()
            For x As Integer = 0 To matrix.Length - 1
                Dim ii As IIDObject
                ii = matrix(x)
                If ii.idEntity = idToSearch Then
                    Return matrix(x)
                End If
            Next

            Return Nothing
        End Function
#End Region



        Public Shared Function getValueToSql(ByVal value As Double) As String
            Dim newValue As String = value.ToString.Replace(",", ".")
            Return newValue
        End Function

        ''' <summary>
        ''' Create random string base
        ''' usage:Dim randObject As New Random
        ''' dim myrandomstring=RandomString(randObject)
        ''' </summary>
        ''' <param name="r"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function RandomString(Optional r As Random = Nothing)
            If r Is Nothing Then
                r = New Random
            End If
            Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            Dim sb As New System.Text.StringBuilder
            Dim cnt As Integer = r.Next(15, 33)
            For i As Integer = 1 To cnt
                Dim idx As Integer = r.Next(0, s.Length)
                sb.Append(s.Substring(idx, 1))
            Next
            Return sb.ToString()
        End Function
        ''' <summary>
        ''' Get ip address of user
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function GetIPAddress() As String
            Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
            Dim sIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If String.IsNullOrEmpty(sIPAddress) Then
                Return context.Request.ServerVariables("REMOTE_ADDR")
            Else
                Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
                Return ipArray(0)
            End If
        End Function

    End Class
End Namespace