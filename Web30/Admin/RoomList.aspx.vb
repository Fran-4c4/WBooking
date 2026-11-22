Imports System.Data
Imports System.Diagnostics

Partial Class RoomList
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P6_Configuracion_de_Datos_hotel

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.hotelConfiguration)

        Me.configNameFieldsPrivate()

        If Not Page.IsPostBack Then

        Else
            
        End If

        
    End Sub

    Private Sub configNameFieldsPrivate()
        Select Case Me.hotel.Tipo
            Case 2
               
        End Select
    End Sub


    Friend Function getDvList() As DataView
        Try
            Dim tableRes As New ind.bk.obj.T_HOTELES_HABITACIONES_Manager

            Dim dv As DataView = tableRes.getDataTableGetByCodhotel(Me.codhotel).DefaultView
            dv.Sort = ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.habitacionhotel
            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
        Return Nothing
    End Function

    

    Protected Sub btnDeleteItem_Click(sender As Object, e As EventArgs) Handles btnDeleteItem.Click
        Try
            Dim field_itemobjectDec As String = Me.field_itemobject.Value
            field_itemobjectDec = UIHelper.URLDeCryptData(field_itemobjectDec)
            AdminTenantSecurity.RequireRoom(Me.codhotel, CInt(field_itemobjectDec))

            Dim tableRes As New ind.bk.obj.T_Hoteles_Habitaciones_Manager
            tableRes.Delete(field_itemobjectDec)
            cfr.obj.HotelRoomData.removeUpdatedUnit(field_itemobjectDec)
            ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.delete, "unit deleted id:" & field_itemobjectDec)
            cfr.pages.SiteBasePage.showMessage("La unidad ha sido borrada.", Me.Page, CFRConst.msgEnum.errorType)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Sub

    Friend Function getImgHtmlForRoomViewCaroussel(roomID As Integer) As String
        AdminTenantSecurity.RequireRoom(Me.codhotel, roomID)
        Const sMainContainer As String = "  <div id='pop_{0}' class='pop-gallery owl-carousel content-slider-with-controls owl-room-pic'>{1}</div>"
        Const sSubContainer As String = "  <div class='overlay-container overlay-visible'>{0}</div>"
        Dim dv As DataView = ind.bk.obj.T_Hoteles_Habitaciones_Manager.GetRoomFotosByRoomCode(roomID).DefaultView

        Dim smain As String = ""
        If dv.Count > 0 Then
            For Each drv As DataRowView In dv
                Dim s As String = ""
                Dim lnkFoto As String
                Dim lnkFotothumb As String
                If "" & drv("fotourl") = "" Then
                    Dim imageFileName As String = String.Format("{0}_{1}_{2}{3}.jpg", Me.codhotel, roomID, drv("id"), drv("VERSION"))
                    lnkFoto = RoomImageStorage.BuildPublicUrl(Me.ResolveUrl("~/RoomImage.ashx"), imageFileName)
                    If drv("thumbnail") = True Then
                        Dim thumbnailFileName As String = String.Format("{0}_{1}_{2}{3}_TH.jpg", Me.codhotel, roomID, drv("id"), drv("VERSION"))
                        lnkFotothumb = RoomImageStorage.BuildPublicUrl(Me.ResolveUrl("~/RoomImage.ashx"), thumbnailFileName)
                    Else
                        lnkFotothumb = lnkFoto
                    End If

                Else
                    Dim imageFileName As String = String.Format(CStr(drv("fotourl")), "")
                    lnkFoto = RoomImageStorage.BuildPublicUrl(Me.ResolveUrl("~/RoomImage.ashx"), imageFileName)
                    If drv("thumbnail") = True Then
                        Dim thumbnailFileName As String = String.Format(CStr(drv("fotourl")), "_TH")
                        lnkFotothumb = RoomImageStorage.BuildPublicUrl(Me.ResolveUrl("~/RoomImage.ashx"), thumbnailFileName)
                    Else
                        lnkFotothumb = lnkFoto
                    End If
                End If


                s += vbCr & "<div class='overlay-container overlay-visible'>"
                s += vbCr & String.Format("<img class='lazyOwl img-carousel' data-src='{0}' />", lnkFotothumb)
                's += vbCr & String.Format("<img class='' src='{0}' />", lnkFotothumb)
                s += String.Format("<a class='popup-img overlay-link' href='{0}'><i class='fa fa-plus'></i></a>", lnkFoto)
                s += vbCr & "</div>"
                smain += s
            Next
            smain = String.Format(sMainContainer, roomID, smain)
            'Debug.WriteLine(smain)
            Return smain

        Else
            Dim s As String = "<img class='' src='images/noimage-th.jpg'/>"
            s = String.Format(sSubContainer, s)
            Return s
        End If


    End Function

End Class
