<%@ WebHandler Language="VB" Class="ymclosehandler" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Linq
Imports System.Diagnostics
Imports System.Collections.Generic
Imports System.Globalization

Public Class ymclosehandler
    Inherits AdminBasePage
    Implements IHttpHandler, System.Web.SessionState.IRequiresSessionState


    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P5_Yield_Management_Cierre_Ventas

    Public Overrides Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            'Threading.Thread.Sleep(3000)

            'AdminSession = AdminBasePage.getAdminSession(context)
            If Me.checkSessionByRequest(context) Is Nothing Then
                Throw New HttpException(401, "An administrator session is required.")
                Exit Sub
            End If

            If Not validateUserAccessToSectionByContext(context, cfr.web.enum.adminWebPageEnum.closeSelling) Then
                Return
            End If
            AdminRequestSecurity.ValidateStateChangingRequest(context)

            Dim ymid As String = context.Request.Form("ymid")
            Dim requestedCloseState As Integer
            Dim operatorid As Integer
            Dim ymResponse As String = "Cerrar"

            If Not Integer.TryParse(context.Request.Form("ymclose"), requestedCloseState) OrElse
               (requestedCloseState <> 0 AndAlso requestedCloseState <> -1) Then
                Throw New HttpException(400, "The close state is invalid.")
            End If
            If Not Integer.TryParse(context.Request.Form("operatorid"), operatorid) OrElse operatorid < 1 Then
                Throw New HttpException(400, "The operator identifier is invalid.")
            End If

            Dim closeState As Integer
            If requestedCloseState = 0 Then
                closeState = 1
                ymResponse = "Abrir"
            Else
                closeState = 0
                ymResponse = "Cerrar"
            End If

            'Throw New Exception("Se ha producido un error")

            If Not String.IsNullOrWhiteSpace(ymid) Then
                Dim codes() As String = ymid.Split("_")
                Dim roomId As Integer
                Dim datein As Date
                If codes.Length <> 2 OrElse
                   Not Integer.TryParse(codes(0), roomId) OrElse roomId < 1 OrElse
                   Not DateTime.TryParseExact(codes(1), "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                              DateTimeStyles.None, datein) Then
                    Throw New HttpException(400, "The room/date identifier is invalid.")
                End If

                Dim DTR As New ind.bk.date.DateRangeCls(datein, datein)
                ind.bk.web.Availability_mgr.UpdateUnitYMClosed(Me.codhotel, operatorid, roomId, closeState, DTR)
                context.Response.ContentType = "text/plain"
                context.Response.Write(ymResponse)
            Else
                Throw New HttpException(400, "The room/date identifier is required.")
            End If

        Catch exhttp As HttpException
            context.Response.ClearHeaders()
            context.Response.ClearContent()
            context.Response.Status = "500 Server error"
            context.Response.StatusCode = exhttp.GetHttpCode
            context.Response.StatusDescription = "Request rejected"
            context.Response.TrySkipIisCustomErrors = False
            context.ApplicationInstance.CompleteRequest()

        Catch ex As Exception
            context.Response.ClearHeaders()
            context.Response.ClearContent()
            context.Response.Status = "500 Server error"
            context.Response.StatusCode = 500
            context.Response.StatusDescription = "Server error"
            context.Response.TrySkipIisCustomErrors = False
            context.ApplicationInstance.CompleteRequest()
            'Throw ex
            Exit Sub
            'Throw New HttpException(500, String.Format("Se ha producido un error: ", ex.Message))
        End Try
    End Sub

    'Public Overrides Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

    '    Try
    '        'AdminSession = AdminBasePage.getAdminSession(context)
    '        If Me.checkSessionByRequest(context) Is Nothing Then
    '            Exit Sub
    '        End If

    '        validateUserAccessToSectionByContext(context, cfr.web.enum.adminWebPageEnum.closeSelling)
    '        cfr.admin.AdminPermissions.UserAccessToSectioIsAllowed(Me.sessionCnt.user, permisionRequired)

    '        Dim ymid As String = context.Request("ymid")
    '        Dim operatorid As String = context.Request("operatorid")




    '    Catch ex As Exception

    '    End Try




    'End Sub



    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class
