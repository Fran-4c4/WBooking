Imports Microsoft.VisualBasic

Public Class FileManager
    Public Shared Function fileExist(ByVal path As String, ByVal fileName As String) As Boolean
        Try
            Return IO.File.Exists(IO.Path.Combine(path, fileName))
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
