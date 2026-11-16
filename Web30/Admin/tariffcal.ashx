<%@ WebHandler Language="VB" Class="tariffcalhandler" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Linq
Imports System.Diagnostics
Imports System.Collections.Generic

Public Class tariffcalhandler
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

            If Not validateUserAccessToSectionByContext(context, cfr.web.enum.adminWebPageEnum.tariff) Then
                Return
            End If
            AdminRequestSecurity.ValidateStateChangingRequest(context)



            btnsaveavailability_Click(context)



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

    Protected Sub btnsaveavailability_Click(ByVal context As HttpContext)

        Dim ctx As New cfrbookEntities
        Try
            Dim daySelected As Date = getFieldByID(context, "cbomonth")
            Dim yearSel As Integer = daySelected.Year
            Dim monthSel As Integer = daySelected.Month
            Dim cod_operadorAvalibilitySelected As Integer = getFieldByID(context, "lstclients")

            Dim codhabitacionhotel As Integer

            Dim dayStart As Integer = 1
            Dim dayEnd As Integer = 31

            Dim dispHotel As New ArrayList
            Dim dvRooms As DataView = Me.getDvRoomsAll
            For Each drRoom As DataRowView In dvRooms
                codhabitacionhotel = CInt(drRoom(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel))
                For iday As Integer = dayStart To dayEnd
                    Dim dispValueObj As Object = context.Request.Form("h" & codhabitacionhotel & "d" & iday)
                    If Not dispValueObj Is Nothing Then
                        If dispValueObj = "" Then dispValueObj = 0

                        Dim disp As New ind.bk.web.Availability_mgr.caldisp(New Date(yearSel, monthSel, iday))
                        disp.cod_habitacion = codhabitacionhotel
                        disp.availability = dispValueObj
                        dispHotel.Add(disp)
                    Else
                        Debug.WriteLine("h" & codhabitacionhotel & "d" & iday)
                    End If
                Next
            Next

            ind.bk.web.Availability_mgr.UpdateDisponib(Me.codhotel, cod_operadorAvalibilitySelected, dispHotel, monthSel, yearSel)

            Dim alPrices As New ArrayList

            'ctx.Database.BeginTransaction()
            Dim modifydate As Date = Date.Now

            For Each keyForm As String In context.Request.Form.AllKeys

                If Not keyForm Is Nothing Then
                    If keyForm.StartsWith("tarcode_") Then

                        Dim key As String = keyForm.Remove(0, 7)
                        Dim keySplit As String() = key.Split("_")

                        'Debug.WriteLine("Tar code: " & keySplit(0) & " Value: " & keySplit(1) & " for " & keyForm)
                        Dim priceID As Integer
                        If keySplit.Length < 3 OrElse Not Integer.TryParse(keySplit(2), priceID) Then
                            Throw New HttpException(400, "The price identifier is invalid.")
                        End If
                        Dim priceEF As R_PriceDayEF =
                            (From price In ctx.R_PriceDayEF
                             Where price.id = priceID AndAlso
                                   price.R_Tarifa_habitacion.R_tarifas.cod_hotel = Me.codhotel
                             Select price).FirstOrDefault()
                        If priceEF Is Nothing Then
                            Throw New HttpException(404, "The price was not found.")
                        End If
                        Dim newPrice As Double = CDbl(cfr.tool.Tools.cNullStr(context.Request.Params(keyForm), 0))

                        Dim keyMinDays As String = keyForm.Replace("tarcode_", "tarcodeminday_")
                        Dim newMinimumDays As Integer = CInt(cfr.tool.Tools.cNullStr(context.Request.Params(keyMinDays), 0))

                        Dim keyRelease As String = keyForm.Replace("tarcode_", "tarcoderelease_")
                        Dim newRelease As Integer = CInt(cfr.tool.Tools.cNullStr(context.Request.Params(keyRelease), 0))


                        Dim ischanged As Boolean = False
                        If priceEF.price <> newPrice And newPrice >= 0 Then
                            priceEF.price = newPrice
                            ischanged = True
                        End If
                        If priceEF.minimumdays <> newMinimumDays And newMinimumDays >= 0 Then
                            priceEF.minimumdays = newMinimumDays
                            ischanged = True
                        End If
                        If priceEF.release <> newRelease And newRelease >= 0 Then
                            priceEF.release = newRelease
                            ischanged = True
                        End If

                        If ischanged = True Then
                            priceEF.ModifyDate = modifydate

                            ctx.SaveChanges()
                        End If

                    End If
                End If
            Next

            context.Response.Write("<div class='msgOk'>La disponibilidad y las tarifas  han sido modificados</div>")

            'Me.debugRequestConsole()


            'ctx.Database.CurrentTransaction.Commit()

        Catch ex As Exception
            'ctx.Database.CurrentTransaction.Rollback()
            Throw ex
        Finally

        End Try
    End Sub

    Public Shared Function getFieldByID(ByVal context As HttpContext, fieldKeyID As String) As String
        For Each keyForm As String In context.Request.Form.AllKeys
            If keyForm.EndsWith(fieldKeyID) Then
                Return context.Request(keyForm)
            End If
        Next

        Return ""
    End Function



    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class
