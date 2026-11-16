Imports System.Configuration
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Web

''' <summary>
''' Owns the physical and public contract for generated room images.
''' </summary>
Public NotInheritable Class RoomImageStorage
    Private Const DataDirectorySetting As String = "DataDirectory"
    Private Const ImagesDirectoryName As String = "images"
    Private Shared ReadOnly SafeFileName As New Regex("\A[A-Za-z0-9][A-Za-z0-9_.-]*\.jpg\z", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)

    Private Sub New()
    End Sub

    ''' <summary>
    ''' Returns the configured image directory, creating it only for a write.
    ''' DataDirectory must be an absolute path. The fixed images child prevents
    ''' configuration from changing the URL contract or making arbitrary files public.
    ''' </summary>
    Public Shared Function GetImagesDirectory(ByVal createForWrite As Boolean) As String
        Dim configuredRoot As String = ConfigurationManager.AppSettings(DataDirectorySetting)
        If String.IsNullOrWhiteSpace(configuredRoot) OrElse Not Path.IsPathRooted(configuredRoot) Then
            Throw New ConfigurationErrorsException("DataDirectory must be a non-empty absolute path.")
        End If

        Dim root As String = Path.GetFullPath(configuredRoot)
        Dim images As String = Path.GetFullPath(Path.Combine(root, ImagesDirectoryName))
        Dim rootPrefix As String = root.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) & Path.DirectorySeparatorChar
        If Not images.StartsWith(rootPrefix, StringComparison.OrdinalIgnoreCase) Then
            Throw New ConfigurationErrorsException("The image directory must remain inside DataDirectory.")
        End If

        If createForWrite Then
            Directory.CreateDirectory(images)
        End If
        Return images
    End Function

    Public Shared Function GetPhysicalPath(ByVal fileName As String, ByVal createForWrite As Boolean) As String
        ValidateFileName(fileName)
        Return Path.Combine(GetImagesDirectory(createForWrite), fileName)
    End Function

    Public Shared Function BuildPublicUrl(ByVal handlerUrl As String, ByVal fileName As String) As String
        ValidateFileName(fileName)
        Return handlerUrl & "?name=" & HttpUtility.UrlEncode(fileName)
    End Function

    Public Shared Sub ValidateFileName(ByVal fileName As String)
        If String.IsNullOrWhiteSpace(fileName) OrElse
           Not String.Equals(Path.GetFileName(fileName), fileName, StringComparison.Ordinal) OrElse
           Not SafeFileName.IsMatch(fileName) Then
            Throw New HttpException(404, "Image not found.")
        End If
    End Sub
End Class
