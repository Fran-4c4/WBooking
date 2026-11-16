<%@ WebHandler Language="VB" Class="subscribe" %>

Imports System
Imports System.Web
Imports System.Linq
Imports System.Data.Entity

Public Class subscribe : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Try

            Dim hotelcode As Integer = context.Request("properid")
            Dim emailsubscribe As String = context.Request("emailsubscribe")
            Dim lang As String = context.Request("langselected")

            If hotelcode <> 0 And emailsubscribe <> "" Then

                If ind.tools.Emailhelper.validateEmail(emailsubscribe) = False Then
                    Throw New HttpException("El email es incorrecto")
                End If

                Try
                    Using ctx As New cfrbookEntities
                        Dim queryDef =
                            (From ord In ctx.BoletinesEF
                             Where ord.cod_hotel = hotelcode And ord.email = emailsubscribe).FirstOrDefault

                        If Not queryDef Is Nothing Then
                            'email exists
                            Exit Sub
                        End If

                        Dim bol As New BoletinesEF

                        bol.email = emailsubscribe
                        bol.cod_hotel = hotelcode
                        bol.boletin = 1
                        bol.modifydate = Date.Now
                        ctx.BoletinesEF.Add(bol)
                        ctx.SaveChanges()
                    End Using
                Catch ex As Exception
                    'if error ocurrs we assume that there is a repeated email. Others we ignored
                    'context.Response.ClearHeaders()
                    'context.Response.ClearContent()
                    'context.Response.Status = "503 ServiceUnavailable"
                    'context.Response.StatusCode = 503
                    'context.Response.StatusDescription = ex.Message
                    'context.Response.Flush()
                    Exit Sub
                End Try

            End If

        Catch ex As Exception
            'context.Response.ClearHeaders()
            'context.Response.ClearContent()
            'context.Response.Status = "503 ServiceUnavailable"
            'context.Response.StatusCode = 503
            'context.Response.StatusDescription = ex.Message
            context.Response.Flush()
            Exit Sub
            'Throw New HttpException(503, String.Format("Se ha producido un error: ", ex.Message))
        End Try
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class