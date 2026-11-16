
Partial Class Admin_objphotos
    Inherits AdminBasePage

    Public Sub validateFoto()
        Dim validFileTypes As String() = {"bmp", "gif", "png", "jpg", "jpeg", "doc", "xls"}
        Dim ext As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName)
        Dim isValidFile As Boolean = False
        For i As Integer = 0 To validFileTypes.Length - 1
            If ext = "." & validFileTypes(i) Then
                isValidFile = True
                Exit For
            End If
        Next
        If Not isValidFile Then
            
        Else
            'Label1.ForeColor = System.Drawing.Color.Green
            'Label1.Text = "File uploaded successfully."
        End If
    End Sub

End Class
