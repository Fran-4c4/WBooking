

Imports System.Data
Imports System.Diagnostics
Imports System.Linq
Imports cfr.tool
Imports ind.bk.obj
Imports ind.bk.reports

Partial Class Admin_tariffEdit
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P15_Tarifas_Crear_Editar_Borrar
    Public objID As Integer = 0
    Public tariffObj As R_tarifasEF

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.tariff)

        PropertyUIshow.hideByType(Me.hotel.Tipo, Me)
        Try
            If Not Page.IsPostBack Then
                Me.configCombos()

                Dim field_itemobjectReq As String = Tools.cNull(Request.QueryString("objid"), 0)
                Dim field_itemobject As String = UIHelper.decryptData(field_itemobjectReq)
                If field_itemobject <> "0" Then
                    AdminTenantSecurity.RequireTariff(Me.codhotel, CInt(field_itemobject))
                    Me.configThisObject(field_itemobject)
                    Me.objID = field_itemobject
                    Me.plhbtndeletetariff.Visible = True
                Else
                    Me.plhunitprices.Visible = False
                    Me.objID = 0
                    Me.configCboTariffCopy()
                End If

            Else

            End If

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Sub configCombos()
        Me.configCboTariffForSelection()

        Me.ucxdeletemodal.modalTitle = "Borrar tarifa"
        Me.ucxdeletemodal.modalmessagebody = "Esta seguro de borrar la tarifa?"
        Me.ucxdeletemodal.modalbuttonprimaryActionCaption = "Borrar tarifa"
        'AddHandler Me.ucxdeletemodal.btnDelete_Click, AddressOf btndelete_Click1


        With Me.cbocategory.Items
            .Add(New ListItem("Todos los clientes", 0))
            .Add(New ListItem("Agencias", cfr.CFRConst.ClientTypeEnum.HotelAgency))
            .Add(New ListItem("CRS", cfr.CFRConst.ClientTypeEnum.CRS))
            .Add(New ListItem("Touroperadores", cfr.CFRConst.ClientTypeEnum.TourOperators))
            .Add(New ListItem("Empresas", cfr.CFRConst.ClientTypeEnum.Company))
            .Add(New ListItem("Particulares", cfr.CFRConst.ClientTypeEnum.PrivateClient))
            .Add(New ListItem("Websites", cfr.CFRConst.ClientTypeEnum.WebSite))
        End With

        With Me.cbotpvminimmum.Items
            .Add(New ListItem("Cobrar primera noche", 1))
            For x As Integer = 5 To 100 Step 5
                .Add(New ListItem(String.Format("Cargar {0}% del total de la tarifa", x), x))
            Next
        End With

        configcboCancelConditions()
    End Sub

    Private Sub configCboTariffForSelection()
        With Me.cbotariff2select.Items
            Me.cbotariff2select.Items.Clear()
            Dim dv As DataView = Me.getDvTariffByHotel
            .Add(New ListItem("Seleccionar otra tarifa", -1))
            Me.fillCboTariff(Me.cbotariff2select, dv)

        End With
        Me.cbotariff2select.SelectedValue = "-1"
    End Sub

    Friend Sub configcboCancelConditions()
        Dim conditionsList As System.Collections.Generic.List(Of R_PolicyCancel)
        Using ctx As New cfrbookEntities
            Dim query = (From c In ctx.R_PolicyCancel
                         Where c.cod_hotel = Me.hotel.id)
            conditionsList = query.ToList()
        End Using

        With Me.cbocancelconditions.Items
            .Add(New ListItem("Usar generica", -1))
            For Each conpol As R_PolicyCancel In conditionsList
                .Add(New ListItem(conpol.policyName, conpol.policyID))
            Next
        End With
    End Sub

    Friend Sub configCboTariffCopy()
        If Me.objID <> 0 Then
            Me.placeholderTariff.Visible = False

            Exit Sub
        End If
        Me.placeholderTariff.Visible = True
        Me.plhbtndeletetariff.Visible = False


        Dim tableRes As New ind.bk.obj.R_tarifas_Manager
        Dim dv As DataView = tableRes.getDtTariffByIDHotel(Me.codhotel).DefaultView
        dv.Sort = ind.bk.obj.R_tarifas.DBCol.tarifa

        Me.cbotariff.DataSource = dv
        Me.cbotariff.DataTextField = ind.bk.obj.R_tarifas.DBCol.tarifa
        Me.cbotariff.DataValueField = ind.bk.obj.R_tarifas.DBCol.cod_tarifa
        Me.cbotariff.DataBind()
        Me.cbotariff.Items.Add(New ListItem("Ninguna", "-1"))
        Me.cbotariff.SelectedValue = "-1"

    End Sub

    Private Sub cbotariff2select_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbotariff2select.SelectedIndexChanged
        Try
            Dim selectedValue As String = Me.cbotariff2select.SelectedValue
            If selectedValue = "-1" Then
                cfr.pages.SiteBasePage.showMessage("Debe seleccionar una tarifa", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If
            Me.cbotariff2select.SelectedValue = "-1"

            Dim objID As String = UIHelper.decryptData(selectedValue)
            AdminTenantSecurity.RequireTariff(Me.codhotel, CInt(objID))
            configThisObject(objID)

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage("Se ha producido un error: " & ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Function configThisObject(objID As Integer)
        Dim tariff As R_tarifasEF = getThisObject(objID)
        Me.setObjectToPage(tariff)
    End Function

    Private Function getThisObject(objID As Integer) As R_tarifasEF
        AdminTenantSecurity.RequireTariff(Me.codhotel, objID)
        Using ctx As New cfrbookEntities
            Dim tariff As R_tarifasEF = ctx.R_tarifasEF.Find(objID)
            Return tariff
        End Using

        'Dim tariff As ind.bk.obj.R_tarifas
        'Dim oper As New ind.bk.obj.R_tarifas_Manager
        'Dim dt As DataTable = oper.getDataTableByID(objID)

        'tariff = ind.bk.obj.R_tarifas.FillObjectByRow(dt.Rows(0))
        'Return tariff
    End Function

    Private Function setObjectToPage(tariff As R_tarifasEF)
        Me.plhunitprices.Visible = True
        Me.tariffObj = tariff
        Me.objidLbl.Text = tariff.cod_tarifa
        Me.objNameLbl.Text = tariff.tarifa
        Me.objname.Text = tariff.tarifa
        Me.litmodifydate.Text = "Ultima modificación: " & tariff.ModifyDate

        Me.chknorefundtariff.Checked = cfr.tool.Tools.cNull(tariff.norefundtariff, False)
        Me.tnorefundprice.Text = cfr.tool.Tools.cNull(tariff.norefundtariffdiscount, 0)

        Me.chkboardonlyroom.Checked = tariff.SA
        Me.chkboardbreakfast.Checked = tariff.AD
        Me.chkboardmp.Checked = tariff.MP
        Me.chkboardpc.Checked = tariff.PC
        Me.chkboardti.Checked = tariff.TI

        If Me.hotel.BookParam.HasTPV = True Then
            Me.pnlHasTPV.Visible = True
            If tariff.TPVOneNight = True Then
                Me.cbotpvminimmum.SelectedValue = 0
            Else
                Me.cbotpvminimmum.SelectedValue = tariff.TPVMinimum
            End If
        Else
            Me.pnlHasTPV.Visible = False
        End If


        Me.chkDiscountCapacityApplyall.Checked = tariff.AplicDescACapac

        If Not tariff.policyID Is Nothing Then
            Me.cbocancelconditions.SelectedValue = tariff.policyID
        Else
            Me.cbocancelconditions.SelectedValue = -1
        End If



    End Function

#Region "Edit tariff prices"
    Public operationinprogress As String = "operationinprogress_1393"
    Public Enum operationEnum As Byte
        assignCustomer = 1
        assignSeason = 2
    End Enum

    Friend Function getDvPricesByRoomByTariff(cod_tarifa As Integer) As DataView
        Try
            AdminTenantSecurity.RequireTariff(Me.codhotel, cod_tarifa)
            Return ind.bk.admin.AdminTariffManager.getDvPricesByRoomByTariff(cod_tarifa)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Function

    Friend Function getDvClientsAsociated(objId As Integer) As DataView
        Try
            AdminTenantSecurity.RequireTariff(Me.codhotel, objId)
            Dim tableRes As New ind.bk.obj.R_tarifas_Manager

            Dim dv As DataView = tableRes.getDT_ClientsByTariff(objId).DefaultView
            'dv.Sort = ind.bk.obj.R_Temporadas.DBCol.Temporada
            'Return Sorted  from ddbb by operator name
            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Function

    Friend Function showCellOn(value As Boolean, valueonCell As String)
        If Me.hotel.Tipo <> 1 Then
            Return ""
        End If
        Dim s As String = "<td class='tdprice'>{0}</td>"
        If value = True Then
            Return String.Format(s, valueonCell)
        Else
            Return String.Format(s, "")
        End If


    End Function
    <System.Web.Services.WebMethod>
    Public Shared Function getCustomersRelations() As String
        Try
            'Dim field_itemobjectDec As String = Me.field_itemobject.Value
            '' field_itemobjectDec = UIHelper.URLDeCryptData(field_itemobjectDec)
            'Dim tableRes As New ind.bk.obj.R_tarifas_Manager

            'Dim dvCustomers As DataView = tableRes.getDT_TariffAsociatedClientForUI(field_itemobjectDec, Me.codhotel).DefaultView

            'Dim s As New StringBuilder("<div class='form-group' id='relationsdivpanel'>")
            'For Each drv As DataRowView In dvCustomers
            '    Dim checked = drv(R_tarifas.DBCol.cod_tarifa) <> 0
            '    Dim operatorName As String = drv(ind.bk.obj.R_operadores.DBCol.Operador)
            '    If drv(ind.bk.obj.R_operadores.DBCol.FreeSale) = 2 Then
            '        operatorName = operatorName & "(Operador generico del sistema)"
            '    End If
            '    s.Append(ind.bk.admin.AdminTariffManager.createCheckbox(drv(ind.bk.obj.R_operadores.DBCol.cod_operador), checked, operatorName))
            'Next

            's.Append("</div>")
            's.AppendFormat("<input type='hidden' name='{0}' id='{0}' value='{1}'>", operationinprogress, Me.operationEnum.assignCustomer.ToString("D"))
            'Me.assignrelationsmodal.litrelationsHandler = s.ToString
            'Me.assignrelationsmodal.litresultmsgHandler = ""
            'Dim jsFunction As String
            'jsFunction = " $('#modalasignrelations').modal('show');"
            'UIMsg.registerScript(Me, jsFunction, "showAsociationModalScript")
            Return "ok list"
        Catch ex As Exception
            'cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
            Return "error wp"
        End Try
    End Function

    Protected Sub btnshowcustomerrelations_Click(sender As Object, e As EventArgs) 'Handles btnshowcustomerrelations.Click
        Try
            Dim field_itemobjectDec As String = Me.field_itemobject.Value
            ' field_itemobjectDec = UIHelper.URLDeCryptData(field_itemobjectDec)
            Dim tableRes As New ind.bk.obj.R_tarifas_Manager

            Dim dvCustomers As DataView = tableRes.getDT_TariffAsociatedClientForUI(field_itemobjectDec, Me.codhotel).DefaultView

            Dim s As New StringBuilder("<div class='form-group' id='relationsdivpanel'>")
            For Each drv As DataRowView In dvCustomers
                Dim checked = drv(R_tarifas.DBCol.cod_tarifa) <> 0
                Dim operatorName As String = drv(ind.bk.obj.R_operadores.DBCol.Operador)
                If drv(ind.bk.obj.R_operadores.DBCol.FreeSale) = 2 Then
                    operatorName = operatorName & "(Operador generico del sistema)"
                End If
                s.Append(ind.bk.admin.AdminTariffManager.createCheckbox(drv(ind.bk.obj.R_operadores.DBCol.cod_operador), checked, operatorName))
            Next

            s.Append("</div>")
            s.AppendFormat("<input type='hidden' name='{0}' id='{0}' value='{1}'>", operationinprogress, Me.operationEnum.assignCustomer.ToString("D"))
            Me.assignrelationsmodal.litrelationsHandler = s.ToString
            Me.assignrelationsmodal.litresultmsgHandler = ""
            Dim jsFunction As String
            jsFunction = " $('#modalasignrelations').modal('show');"
            UIMsg.registerScript(Me, jsFunction, "showAsociationModalScript")

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Sub


    Private Sub assignrelationsmodal_btnsavetariffrelations_Click(sender As Object, e As EventArgs) Handles assignrelationsmodal.btnsavetariffrelations_Click

    End Sub

    Protected Sub savetariff_prices(cod_tarifa As Integer)
        Try
            'Me.debugRequestConsole()

            Dim dvRoomPrices As DataView = Me.getDvPricesByRoomByTariff(cod_tarifa)
            For Each drv As DataRowView In dvRoomPrices ' for each room
                Dim priceInfo As New ind.bk.reports.pricesInfoObject(drv)
                Dim priceObj As New R_Tarifa_habitacion
                Dim newroomprice As Integer = drv("id")

                If newroomprice <> 0 Then
                    priceObj.FillByRow(drv.Row)
                    priceObj.objChanged = False
                End If

                With priceObj
                    .cod_habitacion = priceInfo.roomid
                    .cod_tarifa = priceInfo.tariffid
                    AdminTenantSecurity.RequireRoom(Me.codhotel, .cod_habitacion)
                    .precio = getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.precio))
                    .SupDiasMinimos = getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.supdiasmin))
                    .SupDiasMinimos = getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.supdiasmin))
                    .SupFinDeSemana = getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.supfindesemana))
                    'Adults board
                    .precioAD = getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardAD))
                    .preciomp = getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardMP))
                    .preciopc = getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardPC))
                    .TodoIncluido = getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardTI))
                    'Child board
                    .desayunonino = getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardADchild))
                    .mpnino = getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardMPchild))
                    .pcnino = getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardPCchild))
                    .TodoIncluidoNino = getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardTIchild))
                    'discounts adults
                    ind.bk.admin.AdminTariffManager.setDiscount(priceObj, getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.discountPax1)), .DescAdultosExtra1, .camaExtra1)
                    ind.bk.admin.AdminTariffManager.setDiscount(priceObj, getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.discountPax2)), .DescAdultosExtra2, .camaExtra2)
                    ind.bk.admin.AdminTariffManager.setDiscount(priceObj, getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.discountchild1)), .DescNinoPago1, .camaExtra1nino)
                    ind.bk.admin.AdminTariffManager.setDiscount(priceObj, getRequestValue(priceInfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.discountchild2)), .DescNinoPago2, .camaExtra2nino)
                End With


                Dim rtmgr As New R_Tarifa_habitacion_Manager
                If newroomprice <> 0 Then
                    If priceObj.objChanged = True Then
                        rtmgr.Update(priceObj)
                    End If
                Else
                    rtmgr.insert(priceObj)
                End If
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub



#End Region



    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        'Dim ctx As cfrbookEntities
        Try
            Dim objidStr As String = Me.objidLbl.Text
            Dim objid As Integer = 0
            If objidStr = "" Then
                objid = 0
            Else
                objid = CInt(objidStr)
            End If
            Dim operDDBB As ind.bk.DBMgr.ddbbActionEnum = ind.bk.DBMgr.ddbbActionEnum.update

            Dim tariff As R_tarifasEF
            If objid <> 0 Then
                AdminTenantSecurity.RequireTariff(Me.codhotel, objid)
                operDDBB = ind.bk.DBMgr.ddbbActionEnum.update
                tariff = getThisObject(objid)
            Else
                operDDBB = ind.bk.DBMgr.ddbbActionEnum.create
                tariff = New R_tarifasEF
            End If

            Dim oper As New ind.bk.obj.R_tarifas_Manager
            With tariff
                .cod_hotel = Me.codhotel
                .cod_tarifa = objid
                .tarifa = Me.objname.Text
                .Categoria = Me.cbocategory.SelectedValue
                .SA = Me.chkboardonlyroom.Checked
                .AD = Me.chkboardbreakfast.Checked
                .MP = Me.chkboardmp.Checked
                .PC = Me.chkboardpc.Checked
                .TI = Me.chkboardti.Checked

                .norefundtariff = Me.chknorefundtariff.Checked
                .norefundtariffdiscount = Me.tnorefundprice.Text


                .DiasMinimos = 0
                .AplicDescACapac = Me.chkDiscountCapacityApplyall.Checked
                If Me.hotel.BookParam.HasTPV = True Then
                    Dim tpvvalue As Integer = Me.cbotpvminimmum.SelectedValue
                    If tpvvalue = 0 Then
                        .TPVOneNight = True
                        .TPVMinimum = tpvvalue
                    Else
                        .TPVOneNight = False
                        .TPVMinimum = tpvvalue
                    End If
                End If


                If Me.cbocancelconditions.SelectedValue = "-1" Then
                    .policyID = Nothing
                    .R_PolicyCancel = Nothing
                Else
                    AdminTenantSecurity.RequireCancellationPolicy(Me.codhotel, CInt(Me.cbocancelconditions.SelectedValue))
                    .policyID = Me.cbocancelconditions.SelectedValue
                End If

            End With



            If operDDBB = ind.bk.DBMgr.ddbbActionEnum.update Then
                Using ctx = New cfrbookEntities
                    ctx.R_tarifasEF.Attach(tariff)
                    ctx.Entry(tariff).State = Entity.EntityState.Modified
                    ctx.SaveChanges()
                End Using
                savetariff_prices(objid)
                'oper.Update(tariff)
                cfr.pages.SiteBasePage.showMessage("La tarifa se ha guardado correctamente. Recuerde comprobar los precios.", Me.Page, CFRConst.msgEnum.Ok)
            Else
                Me.saveNewTariff(tariff)
                Me.configCboTariffForSelection()
                Me.plhbtndeletetariff.Visible = True

                cfr.pages.SiteBasePage.showMessage("La tarifa se ha creado correctamente. Recuerde comprobar los precios.", Me.Page, CFRConst.msgEnum.Ok)
            End If


            Me.setObjectToPage(tariff)

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Sub saveNewTariff(tariff As R_tarifasEF)

        Dim cod_tarifa As Integer


        Dim trans As System.Data.Common.DbTransaction = Nothing

        Dim ctx As cfrbookEntities
        Try
            Dim tarifasMgr As New ind.bk.obj.R_tarifas_Manager

            ctx = New cfrbookEntities
            'comienzo de la transaccion
            trans = ctx.Database.BeginTransaction().UnderlyingTransaction

            ctx.R_tarifasEF.Attach(tariff)
            ctx.Entry(tariff).State = Entity.EntityState.Added


            ctx.SaveChanges()


            tariff.ModifyDate = Date.Now 'bacause we don´t get from DDBB

            cod_tarifa = tariff.cod_tarifa

            'Dim ntemporada As New r_TemporadasEF
            'ntemporada.cod_Temporada = cod_tarifa

            'ntemporada.cod_hotel = Me.codhotel
            'ntemporada.Temporada = cod_tarifa
            'ntemporada.temporadaType = 1

            'ctx.r_TemporadasEF.Add(ntemporada)
            'ctx.SaveChanges()

            'Dim nTariffTemporada As New r_Tarifas_TemporadasEF
            'nTariffTemporada.cod_tarifa = cod_tarifa
            'nTariffTemporada.cod_Temporada = cod_tarifa
            'ctx.r_Tarifas_TemporadasEF.Add(nTariffTemporada)

            'ctx.SaveChanges()


            If Me.cbotariff.SelectedValue <> "-1" Then
                'copy tariff relations
                Dim idtariffCopy As Integer = CInt(Me.cbotariff.SelectedValue)
                AdminTenantSecurity.RequireTariff(Me.codhotel, idtariffCopy)
                'save seasons copy relations
                Dim seasonRelationsMgr As New ind.bk.obj.R_Tarifas_Temporadas_Manager

                Dim dtSeasonsRelations As DataTable = tarifasMgr.getDT_temporadasByIdTarifa(idtariffCopy, trans)

                For Each dr As DataRow In dtSeasonsRelations.Rows
                    Dim objto As New ind.bk.obj.R_Tarifas_Temporadas
                    objto.cod_Temporada = dr(ind.bk.obj.R_Temporadas.DBCol.cod_Temporada)
                    AdminTenantSecurity.RequireSeason(Me.codhotel, objto.cod_Temporada)
                    objto.cod_tarifa = cod_tarifa
                    seasonRelationsMgr.insert(objto, trans)
                Next

                'save customers copy relations

                Dim dtCustomersRelations As DataTable = tarifasMgr.getDT_ClientsByTariff(idtariffCopy, trans)
                Dim customerRelationsMgr As New ind.bk.obj.R_Tarifa_operador_Manager

                For Each dr As DataRow In dtCustomersRelations.Rows
                    Dim objto As New ind.bk.obj.R_Tarifa_operador
                    objto.cod_operador = dr(ind.bk.obj.R_Tarifa_operador.DBCol.cod_operador)
                    AdminTenantSecurity.RequireOperator(Me.codhotel, objto.cod_operador)
                    objto.cod_tarifa = cod_tarifa
                    customerRelationsMgr.insert(objto, trans)
                Next


                Dim dvRoomPrices As DataTable = tarifasMgr.getDT_PricesByRoomByTariff(idtariffCopy, trans)
                Dim rtmgr As New R_Tarifa_habitacion_Manager
                For Each drv As DataRow In dvRoomPrices.Rows  ' for each room
                    Dim priceObj As New ind.bk.obj.R_Tarifa_habitacion

                    priceObj.FillByRow(drv)
                    AdminTenantSecurity.RequireRoom(Me.codhotel, priceObj.cod_habitacion)
                    priceObj.cod_tarifa = cod_tarifa
                    rtmgr.insert(priceObj, trans)
                Next

            End If

            ind.bk.DBMgr.CommitTransaction(trans)

            ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.delete, "Tariff-New saved id:" & cod_tarifa)


        Catch ex As Exception
            ind.bk.DBMgr.rollbackTransaction(trans)
            Throw ex
        Finally

            ind.bk.DBMgr.disposeTransaction(trans)

        End Try



    End Sub

    Private Sub ucxdeletemodal_btnDelete_Click(sender As Object, e As EventArgs) Handles ucxdeletemodal.btnDelete_Click
        Try
            Dim objidStr As String = Me.objidLbl.Text
            Dim objid As Integer = CInt(objidStr)
            AdminTenantSecurity.RequireTariff(Me.codhotel, objid)

            Dim operMgr As New ind.bk.obj.R_tarifas_Manager
            operMgr.Delete(CInt(objid))
            ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.delete, "Tariff deleted id:" & objid)

            'Me.ucxdeletemodal.modalbuttonprimary.Attributes.Add("class", Me.ucxdeletemodal.modalbuttonprimary.Attributes("class") & " hidden")
            Response.Redirect(linkcnf("tarifflist.aspx"))
            'cfr.pages.SiteBasePage.showMessage("tarifa borrada", Me.Page, CFRConst.msgEnum.errorType)
        Catch ex As Exception
            'Try
            '    Dim msgclean As String = UIMsg.cleanIllegalStr(ex.Message)
            '    Dim jsFunction As String = String.Format("$('#resultmessageplace').html(""{0}"" );", UIMsg.UIMessage("Se ha producido un error.<br/>" & msgclean, CFRConst.msgEnum.errorType))
            '    UIMsg.registerScript(Me, jsFunction, "delHideModalScript")
            'Catch ex1 As Exception

            'End Try
            cfr.pages.SiteBasePage.showMessage("Se ha producido un error: " & ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        Finally

        End Try
    End Sub



End Class
