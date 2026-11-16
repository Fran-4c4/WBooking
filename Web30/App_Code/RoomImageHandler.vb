Imports System.IO
Imports System.Web

Public Class RoomImageHandler
    Implements IHttpHandler

    Public ReadOnly Property IsReusable As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        If Not String.Equals(context.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase) AndAlso
           Not String.Equals(context.Request.HttpMethod, "HEAD", StringComparison.OrdinalIgnoreCase) Then
            context.Response.StatusCode = 405
            context.Response.AddHeader("Allow", "GET, HEAD")
            Return
        End If

        Dim path As String = RoomImageStorage.GetPhysicalPath(context.Request.QueryString("name"), False)
        If Not File.Exists(path) Then
            context.Response.StatusCode = 404
            Return
        End If

        context.Response.ContentType = "image/jpeg"
        context.Response.Cache.SetCacheability(HttpCacheability.Public)
        context.Response.Cache.SetMaxAge(TimeSpan.FromDays(30))
        context.Response.Cache.SetLastModified(File.GetLastWriteTimeUtc(path))
        context.Response.AddHeader("X-Content-Type-Options", "nosniff")
        If Not String.Equals(context.Request.HttpMethod, "HEAD", StringComparison.OrdinalIgnoreCase) Then
            context.Response.TransmitFile(path)
        End If
    End Sub
End Class
