Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO

Partial Class Admin_roomEdit
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P6_Configuracion_de_Datos_hotel

    'Upload limits are authoritative for room images. The encoded body is
    'limited to 1 MiB and the decoded image to 20 million pixels to bound both
    'request storage and image-decoder memory use.
    Private Const MaximumUploadedImageBytes As Integer = 1024 * 1024
    Private Const MaximumDecodedImagePixels As Long = 20000000

    'Public roomID As Integer

    Public objID As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.hotelConfiguration)

        Try

            Me.configNameFieldsPrivate()
            If Not Page.IsPostBack Then

                Me.configCombos()
                'hotel = cfr.mgr.HotelMgr.GetEntity(Me.codhotel)
                Dim roomid As String = Request.QueryString("roomid")
                If roomid <> "0" Then
                    Dim field_itemobject As String = UIHelper.decryptData(roomid)
                    objID = field_itemobject
                    Me.getUnitByID(field_itemobject)
                    Me.pnluploadimg.Visible = True
                Else
                    Me.objectIDVS = 0
                    Me.pnluploadimg.Visible = False
                End If

            Else

            End If

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub


#Region "Config cbo and load room"

    Private Sub configNameFieldsPrivate()
        Select Case Me.hotel.Tipo
            Case 2
                Me.roomName.Attributes("placeholder") = "Nombre del " & Me.unitname
                Me.roomName_RequiredFieldValidator1.ErrorMessage = String.Format("Debe introducir el nombre de la unidad({0})", Me.unitname)
        End Select
    End Sub


    Private Sub cbounit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbounit.SelectedIndexChanged
        Try
            Dim selectedValue As String = Me.cbounit.SelectedValue
            If selectedValue = "-1" Then
                cfr.pages.SiteBasePage.showMessage("Debe seleccionar una unidad", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If
            Dim field_itemobject As String = UIHelper.decryptData(Me.cbounit.SelectedValue)
            Me.getUnitByID(field_itemobject)
            Me.pnluploadimg.Visible = True
            Me.cbounit.SelectedValue = "-1"
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage("Se ha producido un error: " & ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Sub getUnitByID(unitId As Integer)
        Try

            Dim tableRes As New ind.bk.obj.T_Hoteles_Habitaciones_Manager

            Dim dt As DataTable = tableRes.getDataTableByID(unitId)
            If dt.Rows.Count = 0 Then
                objID = 0
                cfr.pages.SiteBasePage.showMessage("Lo sentimos no se ha encontrado el elemento solicitado.", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If
            If CInt(dt.Rows(0)("Hotel")) <> Me.codhotel Then
                Throw New HttpException(404, "The room was not found.")
            End If
            Dim drv As New cfr.tool.UIDatarow(dt.Rows(0))
            objID = unitId

            Me.roomidhdd.Value = drv.asIntegerStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel)

            Me.objectIDVS = Me.roomidhdd.Value
            configCboRoomsCombinated()

            Me.roomidLbl.Text = Me.roomidhdd.Value
            Me.roomNameLbl.Text = drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel)
            Me.roomName.Text = drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel)

            Me.numRoomsAvailable.Text = drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.numRoomsAvailable)

            Me.roomPaxIncludedCbo.Text = drv.asIntegerStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.MaxPersonas)
            If drv.asInteger(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.AdultosExtra1) = 1 Then
                Me.roomAdultExtraCbo.Text = 1
            ElseIf drv.asInteger(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.AdultosExtra2) = 1 Then
                Me.roomAdultExtraCbo.Text = 2
            End If

            Me.roomPaxInBasePriceCbo.Text = drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.Adultos)
            Me.roomChildIncludedCbo.Text = drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.NinoOcupaplaza)
            Me.roomCombinatedCbo.Text = drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.cod_hab_combinada)
            Me.hddromcombinatedid.Value = drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.cod_hab_combinada)
            Me.roomBaseCbo.Text = drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionGenerica)

            Me.roomname_en.Text = drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.desc_en)
            Me.roomname_de.Text = drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.desc_de)
            Me.roomname_fr.Text = drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.desc_fr)

        Me.roomServ_es.Text = cfr.security.RichHtmlSanitizer.Sanitize(drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.ServiciosEsp))
        Me.roomServ_en.Text = cfr.security.RichHtmlSanitizer.Sanitize(drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.ServiciosIngles))
        Me.roomServ_de.Text = cfr.security.RichHtmlSanitizer.Sanitize(drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.ServiciosAleman))
        Me.roomServ_fr.Text = cfr.security.RichHtmlSanitizer.Sanitize(drv.asStr(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.ServiciosFrances))

            showroomimages(unitId)


        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Sub showroomimages(roomid As Integer)
        Dim photohtml As String = Me.getImgHtmlForRoomView(roomid)
        If photohtml.Length = 0 Then
            Me.litphotosgallery.Visible = False
            Me.litphotosgallery.Text = ""
        Else
            Me.litphotosgallery.Visible = True
            Me.litphotosgallery.Text = photohtml
        End If
    End Sub

    Private Sub configCombos()
        With Me.cbounit.Items
            Dim dvunits As DataView = Me.getDvRoomsAll(filterWithoutCombined:=False)
            .Add(New ListItem("Seleccionar otra una unidad", -1))

            For Each drv As DataRowView In dvunits
                Dim unitIdCrypt As String = UIHelper.encryptData(drv(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel))
                .Add(New ListItem(drv(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel), unitIdCrypt))
            Next
            Me.cbounit.SelectedValue = "-1"
        End With


        With Me.roomPaxIncludedCbo.Items
            For x As Integer = 0 To 12
                .Add(New ListItem(x & " Pax maximo", x))
            Next
        End With
        Me.roomPaxIncludedCbo.Text = 1

        With Me.roomAdultExtraCbo.Items
            .Add(New ListItem("No admite adultos extras", 0))
            .Add(New ListItem("Admite 1 adulto extra", 1))
            .Add(New ListItem("Admite 2 adultos extras", 2))

        End With
        Me.roomAdultExtraCbo.Text = 0

        With Me.roomPaxInBasePriceCbo.Items
            For x As Integer = 1 To 12
                .Add(New ListItem(x & " Pax", x))
            Next
        End With
        Me.roomPaxInBasePriceCbo.Text = 1

        With Me.roomChildIncludedCbo.Items

            .Add(New ListItem("NO estan incluidos en el computo", 0))
            .Add(New ListItem("SI estan incluidos en el computo", 1))

        End With
        Me.roomChildIncludedCbo.Text = 0

        Select Case Me.hotel.Tipo
            Case 1
                Me.configCboRoomsCombinated()
                Me.configCboBasedRooms()
            Case 2
                Me.plhnumpax.Visible = False
                Me.plhnumpaxextra.Visible = False
                Me.plhroomCombinatedCbo.Visible = False
                Me.plhroombase.Visible = False
                Me.plhchild.Visible = False
        End Select

    End Sub

    Friend Function getDTServices(roomIDServices As Integer) As DataView
        Try
            Dim tableRes As New ind.bk.obj.T_Hoteles_Habitaciones_Manager

            Dim dv As DataView = tableRes.getDataTableGetByCodhabitacionForAdmin(roomIDServices, Me.hotel.Tipo).DefaultView

            'dv.Sort = ind.bk.obj.T_SERVICIOS.DBCol.cod_Servicio
            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Function


    Friend Sub configCboRoomsCombinated()
        Dim dv As DataView = Me.getDvRoomsAll()
        dv.Sort = ind.bk.obj.T_Hoteles_Habitaciones.DBCol.descripcion

        With Me.roomCombinatedCbo.Items
            .Clear()

            .Add(New ListItem("No combina.", 0))
            For Each drv As DataRowView In dv
                Dim unitcombinedid As Integer = drv(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel)
                If unitcombinedid <> Me.objID Then
                    .Add(New ListItem(drv(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel), unitcombinedid))
                End If
            Next
        End With
    End Sub

    Friend Sub configCboBasedRooms()
        Select Case Me.hotel.Tipo
            Case 1
            Case Else
                Exit Sub
        End Select

        Dim tableRes As New ind.bk.obj.T_HABITACIONES_Manager

        Dim dv As DataView = tableRes.getDataTableAll().DefaultView
        dv.Sort = ind.bk.obj.T_HABITACIONES.DBCol.Descripción

        With Me.roomBaseCbo.Items
            For Each drv As DataRowView In dv
                .Add(New ListItem(drv(ind.bk.obj.T_HABITACIONES.DBCol.Descripción), drv(ind.bk.obj.T_HABITACIONES.DBCol.Habitación)))
            Next
        End With
    End Sub
#End Region

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            'save
            If Page.IsValid = False Then
                Exit Sub
            End If

            Dim operMng As New ind.bk.obj.T_Hoteles_Habitaciones_Manager

            Dim obj2 As New ind.bk.obj.T_Hoteles_Habitaciones
            'test

            'Dim dt As DataTable = operMng.getDataTableByID(roomid.Value)
            'ind.bk.obj.T_HOTELES_HABITACIONES.FillObjectByRow(dt.Rows(0), obj2)

            obj2.Hotel = Me.codhotel
            obj2.codhabitacionhotel = Me.objectIDVS
            obj2.habitacionhotel = Me.roomName.Text

            obj2.codhabitacionGenerica = "0" & Me.roomBaseCbo.Text
            obj2.cod_hab_combinada = "0" & Me.roomCombinatedCbo.Text


            obj2.NinoOcupaplaza = Me.roomChildIncludedCbo.Text

            obj2.numRoomsAvailable = CInt(Me.numRoomsAvailable.Text)

            obj2.desc_en = Me.roomname_en.Text
            obj2.desc_de = Me.roomname_de.Text
            obj2.desc_fr = Me.roomname_fr.Text

            obj2.ServiciosEsp = cfr.security.RichHtmlSanitizer.Sanitize(Me.roomServ_es.Text)
            obj2.ServiciosIngles = cfr.security.RichHtmlSanitizer.Sanitize(Me.roomServ_en.Text)
            obj2.ServiciosFrances = cfr.security.RichHtmlSanitizer.Sanitize(Me.roomServ_fr.Text)
            obj2.ServiciosAleman = cfr.security.RichHtmlSanitizer.Sanitize(Me.roomServ_de.Text)
            obj2.NumNino = 2


            If Me.roomAdultExtraCbo.Text = 1 Then
                obj2.AdultosExtra1 = 1
                obj2.AdultosExtra2 = 0
            ElseIf Me.roomAdultExtraCbo.Text = 2 Then
                obj2.AdultosExtra1 = 1
                obj2.AdultosExtra2 = 1
            ElseIf Me.roomAdultExtraCbo.Text = 0 Then
                obj2.AdultosExtra1 = 0
                obj2.AdultosExtra2 = 0
            End If

            If Me.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then
                obj2.Adultos = Me.roomPaxInBasePriceCbo.Text
                obj2.MaxPersonas = Me.roomPaxIncludedCbo.Text
                obj2.MaxAdultos = obj2.Adultos + obj2.AdultosExtra1 + obj2.AdultosExtra2
            Else
                obj2.Adultos = Me.roomPaxIncludedCbo.Text
                obj2.MaxPersonas = Me.roomPaxIncludedCbo.Text
                obj2.MaxAdultos = obj2.MaxPersonas
            End If

            If obj2.codhabitacionhotel = 0 Then
                'INSERT
                operMng.insert(obj2)
                Me.objectIDVS = obj2.codhabitacionhotel
                Me.roomidhdd.Value = obj2.codhabitacionhotel
                Me.roomidLbl.Text = obj2.codhabitacionhotel
                Me.roomNameLbl.Text = obj2.habitacionhotel

            Else
                'UPDATE
                operMng.Update(obj2)

                If Me.hddromcombinatedid.Value = 0 And obj2.cod_hab_combinada <> 0 Then
                    'delete availability for this unit
                    Dim ssql As String = "DELETE FROM R_DISPONIBILIDAD WHERE cod_habitacion=@roomId"
                    ind.bk.DBMgr.Execute(
                        ssql,
                        New SqlClient.SqlParameter("@roomId", SqlDbType.Int) With {.Value = obj2.codhabitacionhotel})
                End If
                cfr.obj.HotelRoomData.removeUpdatedUnit(obj2.codhabitacionhotel)
                Me.roomNameLbl.Text = obj2.habitacionhotel
            End If




            Me.saveServices(obj2)


            cfr.pages.SiteBasePage.showMessage("Hemos guardado su selección", Me.Page, CFRConst.msgEnum.Ok)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Sub saveServices(obj2 As ind.bk.obj.T_Hoteles_Habitaciones)

        Dim ocon As SqlClient.SqlConnection = Nothing
        Dim trans As SqlClient.SqlTransaction = Nothing
        Try

            Dim tabMng As New ind.bk.obj.T_Hoteles_Habitaciones_Manager
            'abrimos la conexion y comenzamos la reserva
            ocon = ind.bk.DBMgr.getNewConn
            'comienzo de la transaccion
            trans = ocon.BeginTransaction
            tabMng.DeleteByCod_habitacion(obj2.codhabitacionhotel, trans:=trans)


            Dim HabServicesMgr As New ind.bk.obj.T_Hoteles_Habitaciones_Servicios_Manager
            'Debug.WriteLine(" Request.Form(chkoptServices)" & Request.Form("chkoptServices"))

            Dim mat() As String = Page.Request.Form.GetValues("chkoptServices")
            If Not mat Is Nothing Then
                Dim matunderRequest() As String = getRequestVector("chkbajopet")

                For Each optID In mat
                    Dim THS As New ind.bk.obj.T_Hoteles_Habitaciones_Servicios
                    THS.cod_habitacion = obj2.codhabitacionhotel
                    THS.cod_Servicio = CInt(optID)
                    THS.underrequest = getReqValueFromVector(matunderRequest, optID)
                    HabServicesMgr.insert(THS, trans:=trans)
                Next
            End If

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            ind.bk.DBMgr.closeConn(ocon)
        End Try

    End Sub

    Friend Function getImgHtmlForRoomView(roomID As Integer) As String
        Const sMainContainer As String = "  <div id='roomphotos_{0}' class='pop-gallery room-edit-thumbnails'><ul class='reorder_ul reorder-photos-list list-inline'>{1}</ul></div>"

        Dim dv As DataView = ind.bk.obj.T_Hoteles_Habitaciones_Manager.GetRoomFotosByRoomCode(roomID).DefaultView

        Dim smain As String = ""
        If dv.Count > 0 Then
            For Each drv As DataRowView In dv
                Dim s As String = ""
                Dim lnkFoto As String
                Dim lnkFotothumb As String
                Dim photoid As Integer = drv("id")
                If "" & drv("fotourl") = "" Then
                    Dim imageFileName As String = String.Format("{0}_{1}_{2}{3}.jpg", Me.codhotel, roomID, photoid, drv("VERSION"))
                    lnkFoto = RoomImageStorage.BuildPublicUrl(Me.ResolveUrl("~/RoomImage.ashx"), imageFileName)
                    If drv("thumbnail") = True Then
                        Dim thumbnailFileName As String = String.Format("{0}_{1}_{2}{3}_TH.jpg", Me.codhotel, roomID, photoid, drv("VERSION"))
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


                Dim btnDelete As String = "<div class='btn-img-gallery-delete'><button type ='button' onclick='return deleteimgroom({0});' name='delete' value='{0}' Class='btn btn-sm btn-animated btn-danger btn-delete-image' data-objid='{0}'>Borrar<i Class='fa fa-times'></i></button></div>"
                btnDelete = String.Format(btnDelete, photoid)
                s += String.Format("<li id='image_li_{0}' class='ui-sortable-handle'>", photoid)
                's += "<div Class='col-xs-2 image_link'>"
                s += vbCr & "<div class='overlay-container overlay-visible mb-10'>"
                s += vbCr & String.Format("<img class='img-order' src='{0}' />", lnkFotothumb)
                s += String.Format("<a class='img-order popup-img overlay-link' href='{0}'><i class='fa fa-plus'></i></a>", lnkFoto)
                s += vbCr & btnDelete
                s += vbCr & "</div>"

                's += vbCr & "</div>"
                s += vbCr & "</li>"
                smain += s

            Next

            smain = String.Format(sMainContainer, roomID, smain)
            Return smain

        Else
            Return ""
        End If


    End Function

    Private Sub btndeleteimg_Click(sender As Object, e As EventArgs) Handles btndeleteimg.Click
        Try
            deleteImage(Me.hddimgidoperation.Value)
            showroomimages(Me.objectIDVS)
            cfr.pages.SiteBasePage.showMessage("La imagen ha sido borrada", Me.Page, CFRConst.msgEnum.Ok)
        Catch ex As Exception

            cfr.pages.SiteBasePage.showMessage("Se ha producido un error con la imagen. " & ex.Message, Me.Page, CFRConst.msgEnum.errorType)

        End Try
    End Sub


    Private Function deleteImage(id As Integer)
        Dim noOfRowDeleted As Integer
        Using ctx As New cfrbookEntities

            Dim photo As T_HOTELES_HABITACIONES_FotosEF = ctx.T_HOTELES_HABITACIONES_FotosEF.Find(id)
            If Not photo Is Nothing Then
                RequireRoomOwnership(photo.codhabitacionhotel)
                Dim pathQuestion As String = RoomImageStorage.GetPhysicalPath(Me.codhotel & "_" & photo.codhabitacionhotel & "_" & photo.id & photo.version & ".jpg", False)
                Dim pathQuestionThumb As String = RoomImageStorage.GetPhysicalPath(Me.codhotel & "_" & photo.codhabitacionhotel & "_" & photo.id & photo.version & "_TH.jpg", False)

                Me.Deletefile(pathQuestion)
                Me.Deletefile(pathQuestionThumb)
                ctx.T_HOTELES_HABITACIONES_FotosEF.Remove(photo)
                ctx.SaveChanges()
            End If

        End Using
    End Function



#Region "Save foto"

    Private Sub btnimgupdateorder_Click(sender As Object, e As EventArgs) Handles btnimgupdateorder.Click
        Try
            'System.Threading.Thread.Sleep(3000)
            Dim msg As String = "El orden se ha guardado. "

            Dim ids() As String = Me.hddimgidoperation.Value.Split(",")

            Dim order As Integer = 0
            Using ctx As New cfrbookEntities

                For Each id As String In ids
                    order += 1
                    Dim photo As T_HOTELES_HABITACIONES_FotosEF = ctx.T_HOTELES_HABITACIONES_FotosEF.Find(CInt(id))
                    If Not photo Is Nothing Then
                        RequireRoomOwnership(photo.codhabitacionhotel)
                        photo.orderui = order
                        ctx.SaveChanges()
                    End If
                Next

            End Using

            showroomimages(Me.objectIDVS)


            cfr.pages.SiteBasePage.showMessage(msg, Me.Page, CFRConst.msgEnum.info)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage("Se ha producido un error: " & ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Sub btnsaveimagen_Click(sender As Object, e As EventArgs) Handles btnsaveimagen.Click
        Try
            Me.saveroomphoto()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub saveroomphoto()
        Dim msg As String = ""
        Dim ctx As cfrbookEntities = Nothing
        Dim temporaryUploadPath As String = Nothing
        Try
            If fileuploadImagecontrol.HasFile = False Then
                Throw New Exception("Debes seleccionar una imagen.")
            End If


            If Me.fileuploadImagecontrol.PostedFile.ContentLength <= 0 OrElse
               Me.fileuploadImagecontrol.PostedFile.ContentLength > MaximumUploadedImageBytes Then
                Throw New Exception("Lo sentimos, el fichero no puede ser mayor de 1Mb, por favor reduzca el tamaño.")
                Exit Sub
            End If
            ctx = New cfrbookEntities

            Dim cod_habitacion As Integer = Me.objectIDVS
            RequireRoomOwnership(cod_habitacion)
            ctx.Database.BeginTransaction()
            Dim roomidphoto As Integer = insertfoto(cod_habitacion, ctx)

            Dim pathQuestion As String = RoomImageStorage.GetPhysicalPath(Me.codhotel & "_" & cod_habitacion & "_" & roomidphoto & "A.jpg", True)
            Dim pathQuestionThumb As String = RoomImageStorage.GetPhysicalPath(Me.codhotel & "_" & cod_habitacion & "_" & roomidphoto & "A_TH.jpg", True)

            temporaryUploadPath = IO.Path.GetTempFileName()
            fileuploadImagecontrol.SaveAs(temporaryUploadPath)
            ValidateUploadedImage(temporaryUploadPath)

            Me.Deletefile(pathQuestion)
            Me.Deletefile(pathQuestionThumb)

            'Only decoder-validated, re-encoded image bytes reach the external data directory.
            Me.CreateImage(temporaryUploadPath, pathQuestion, New System.Drawing.Size(1080, 720))
            Me.CreateImage(temporaryUploadPath, pathQuestionThumb, New System.Drawing.Size(270, 180))


            msg = "La imagen ha sido guardada."
            cfr.pages.SiteBasePage.showMessage(msg, Me.Page, CFRConst.msgEnum.info)
            ctx.Database.CurrentTransaction.Commit()

            showroomimages(cod_habitacion) 'reload images

        Catch ex As Exception
            If Not ctx Is Nothing Then
                ctx.Database.CurrentTransaction.Rollback()
            End If
            cfr.pages.SiteBasePage.showMessage("Se ha producido un error con la imagen. " & ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        Finally
            If Not String.IsNullOrEmpty(temporaryUploadPath) Then
                Me.Deletefile(temporaryUploadPath)
            End If
            If Not ctx Is Nothing Then
                ctx.Dispose()
            End If
        End Try

    End Sub

    Private Function insertfoto(cod_habitacion As Integer, ctx As cfrbookEntities) As Integer

        Dim dv As DataView = ind.bk.obj.T_Hoteles_Habitaciones_Manager.GetRoomFotosByRoomCode(cod_habitacion).DefaultView
        Dim lastUIorder As Integer = 1
        lastUIorder += dv.Count

        Dim photo As New T_HOTELES_HABITACIONES_FotosEF
        photo.codhabitacionhotel = cod_habitacion
        photo.orderui = lastUIorder
        photo.thumbnail = True
        photo.modified = Date.Now
        photo.version = "A"
        ctx.T_HOTELES_HABITACIONES_FotosEF.Add(photo)

        ctx.SaveChanges()

        Return photo.id

    End Function

    ''' <summary>
    ''' Rejects a room identifier that is not owned by the hotel in the
    ''' authenticated administrator session.
    ''' </summary>
    Private Sub RequireRoomOwnership(ByVal roomId As Integer)
        Dim roomManager As New ind.bk.obj.T_Hoteles_Habitaciones_Manager
        Dim roomTable As DataTable = roomManager.getDataTableByID(roomId)
        If roomTable.Rows.Count <> 1 OrElse CInt(roomTable.Rows(0)("Hotel")) <> Me.codhotel Then
            Throw New HttpException(404, "The room was not found.")
        End If
    End Sub

    Private Sub ValidateUploadedImage(ByVal filePath As String)
        Using uploadedImage As Image = Image.FromFile(filePath, True)
            Dim decodedPixels As Long = CLng(uploadedImage.Width) * CLng(uploadedImage.Height)
            If uploadedImage.Width < 1 OrElse uploadedImage.Height < 1 OrElse
               decodedPixels > MaximumDecodedImagePixels Then
                Throw New InvalidDataException("The uploaded image dimensions are invalid.")
            End If
        End Using
    End Sub



    ' create thumbnail for the specified photo
    Public Sub CreateImage(ByVal imagePath As String, ByVal newImagePath As String, ByVal sz As Size)
        Dim largeImage As Bitmap
        Dim newImg As Image

        Try
            ' get photo image
            largeImage = New Bitmap(imagePath)

            ' decide how to scale the thumbnail, 
            ' determine if wider or taller and keep same aspect ratio
            Dim width, height As Integer
            If largeImage.Width > largeImage.Height Then
                width = sz.Width
                height = CInt(largeImage.Height * sz.Width / largeImage.Width)
            Else
                height = sz.Height
                width = CInt(largeImage.Width * sz.Width / largeImage.Height)
            End If

            ' get image								
            newImg = ConvertImage(largeImage, width, height)
            largeImage.Dispose()
            largeImage = Nothing


            newImg.Save(newImagePath, System.Drawing.Imaging.ImageFormat.Jpeg)


        Catch ex As Exception
            Throw ex
        Finally
            ' very important to dispose of images, otherwise images might be locked
            ' and can't update since aspnet process has handle to files
            If Not (largeImage Is Nothing) Then largeImage.Dispose()
            If Not (newImg Is Nothing) Then newImg.Dispose()
        End Try
    End Sub
    Public Function ConvertImage(ByRef source As Image, ByVal MaxWidth As Integer, ByVal MaxHeight As Integer) As Image

        Dim MaxRatio As Double = MaxWidth / MaxHeight
        Dim ImgRatio As Double = source.Width / source.Height
        Dim resized As Bitmap = Nothing
        Dim g As Graphics
        Try


            If (source.Width >= MaxWidth) Then
                resized = New Bitmap(MaxWidth, CInt(Math.Round(MaxWidth / ImgRatio, 0)))
            ElseIf (source.Height >= MaxHeight) Then
                resized = New Bitmap(CInt(Math.Round(MaxWidth * ImgRatio, 0)), MaxHeight)
            End If

            g = Graphics.FromImage(resized)
            g.DrawImage(source, New Rectangle(0, 0, resized.Width, resized.Height), 0, 0, source.Width, source.Height, GraphicsUnit.Pixel)
            g.Dispose()


            Return resized


        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message)
        Finally
            If Not g Is Nothing Then
                g.Dispose()
                g = Nothing
            End If
        End Try

    End Function



    Public Sub Deletefile(ByVal filenamePath As String)
        Try
            If filenamePath.Trim.Length = 0 Then
                Return
            End If
            System.IO.File.Delete(filenamePath)
        Catch ex As Exception
            Diagnostics.Debug.WriteLine(("Error deleting file. " & ex.Message))
        End Try
    End Sub



#End Region
End Class
