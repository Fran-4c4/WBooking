Imports System.Data
Imports System.Diagnostics
Imports System.Linq
Imports ind.bk.obj
Imports ind.bk.reports

Partial Class Admin_tarifflist
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P15_Tarifas_Crear_Editar_Borrar

    Public WithEvents myucxdelete As Admin_adminUC_deletemodal
    Public operationinprogress As String = "operationinprogress_1393"

    Public Enum operationEnum As Byte
        assignCustomer = 1
        assignSeason = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.tariff)

        PropertyUIshow.hideByType(Me.hotel.Tipo, Me)
        'Me.configNameFieldsPrivate()

        If Not Page.IsPostBack Then
            Me.ucxdeletemodal.modalTitle = "Borrar tarifa"
            Me.ucxdeletemodal.modalmessagebody = "Esta seguro de borrar la tarifa?"
            Me.ucxdeletemodal.modalbuttonprimaryActionCaption = "Borrar tarifa"

        Else

        End If

        'AddHandler Me.ucxdeletemodal.btnDelete_Click, AddressOf btndelete_Click1

    End Sub
    Private Sub ucxdeletemodal_btnDelete_Click(sender As Object, e As EventArgs) Handles ucxdeletemodal.btnDelete_Click

        Try
            Dim field_itemobjectDec As String = Me.ucxdeletemodal.modalitemdeletecode.Value
            field_itemobjectDec = UIHelper.URLDeCryptData(field_itemobjectDec)
            AdminTenantSecurity.RequireTariff(Me.codhotel, CInt(field_itemobjectDec))

            Dim tableRes As New ind.bk.obj.R_tarifas_Manager
            tableRes.Delete(CInt(field_itemobjectDec))
            ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.delete, "Tariff deleted id:" & field_itemobjectDec)

            Me.ucxdeletemodal.modalbuttonprimary.Attributes.Add("class", Me.ucxdeletemodal.modalbuttonprimary.Attributes("class") & " hidden")

            Dim jsFunction As String = String.Format("$('#rowobj_{0}').remove();", field_itemobjectDec)
            jsFunction += String.Format("$('#resultmessageplace').html(""{0}"");", UIMsg.UIMessage("La tarifa ha sido borrada", CFRConst.msgEnum.Ok))
            'sb.Append("$('#modaldeleteItem').modal('hide');")
            UIMsg.registerScript(Me, jsFunction, "delHideModalScript")

        Catch ex As Exception
            Try
                Dim msgclean As String = UIMsg.cleanIllegalStr(ex.Message)
                Dim jsFunction As String = String.Format("$('#resultmessageplace').html(""{0}"" );", UIMsg.UIMessage("Se ha producido un error.<br/>" & msgclean, CFRConst.msgEnum.errorType))
                UIMsg.registerScript(Me, jsFunction, "delHideModalScript")
            Catch ex1 As Exception
                Debug.WriteLine(ex1)

            End Try

        Finally

        End Try



    End Sub

    Friend Function getDvList() As DataView
        Try
            Dim tableRes As New ind.bk.obj.R_tarifas_Manager

            Dim dv As DataView = tableRes.getDtTariffByIDHotel(Me.codhotel).DefaultView
            dv.Sort = ind.bk.obj.R_tarifas.DBCol.tarifa
            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Function

    ''' <summary>
    ''' based on database category returns the ui string
    ''' </summary>
    ''' <param name="tariffCategoryVal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function getTariffCategoryUI(tariffCategoryVal As cfr.CFRConst.ClientTypeEnum) As String
        Dim strCategoria As String

        If tariffCategoryVal = CFRConst.ClientTypeEnum.NotClasified Then
            strCategoria = "Todos los clientes"
        ElseIf tariffCategoryVal = CFRConst.ClientTypeEnum.HotelAgency Then
            strCategoria = "Agencias"
        ElseIf tariffCategoryVal = CFRConst.ClientTypeEnum.CRS Then
            strCategoria = "CRS"
        ElseIf tariffCategoryVal = CFRConst.ClientTypeEnum.TourOperators Then
            strCategoria = "TourOperadores"
        ElseIf tariffCategoryVal = CFRConst.ClientTypeEnum.Company Then
            strCategoria = "Empresas"
        ElseIf tariffCategoryVal = CFRConst.ClientTypeEnum.PrivateClient Then
            strCategoria = "Cliente Particular"
        ElseIf tariffCategoryVal = CFRConst.ClientTypeEnum.WebSite Then
            strCategoria = "Portal Web(Websites)"
        End If

        Return strCategoria
    End Function

    Friend Function getDvTemporadasByTariff(idtariff As Integer) As DataView
        Try
            AdminTenantSecurity.RequireTariff(Me.codhotel, idtariff)
            Dim tableRes As New ind.bk.obj.R_tarifas_Manager

            Dim dv As DataView = tableRes.getDT_temporadasByIdTarifa(idtariff).DefaultView
            dv.Sort = ind.bk.obj.R_Temporadas.DBCol.Temporada
            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Function

    Friend Function getDvPricesByRoomByTariff(cod_tarifa As Integer) As DataView
        Try
            AdminTenantSecurity.RequireTariff(Me.codhotel, cod_tarifa)

            Return ind.bk.admin.AdminTariffManager.getDvPricesByRoomByTariff(cod_tarifa)

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Function

    Friend Function showCalendar(objid As Integer) As String
        Dim uical As New CalendarUI
        uical.ConfigureN_Month(11, 0)
        Dim dvTemporadas As DataView = Me.getDvTemporadasByTariff(objid)

        For Each dr As DataRowView In dvTemporadas
            Dim cod_temporada As Integer = dr(ind.bk.obj.R_Temporadas.DBCol.cod_Temporada)
            uical.fillCalendarByTemporada(cod_temporada)
        Next

        Return uical.getCalendarUI(uical.calendarDaysObj)

    End Function

#Region "Relations"


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

    <Obsolete("Se utilizaba para asignar temporadas a una tarifa")>
    Protected Sub btnshowseason_Click(sender As Object, e As EventArgs) 'Handles btnshowseason.Click
        'Try

        '    Dim field_itemobjectDec As String = Me.field_itemobject.Value
        '    ' field_itemobjectDec = UIHelper.URLDeCryptData(field_itemobjectDec)
        '    Dim tableRes As New ind.bk.obj.R_tarifas_Manager

        '    Dim dvRelations As DataView = tableRes.getDT_TariffTemporadasAsociatedUI(field_itemobjectDec, Me.codhotel).DefaultView

        '    Dim s As New StringBuilder("<div class='form-group' id='relationsdivpanel'>")
        '    For Each drv As DataRowView In dvRelations
        '        Dim checked = drv(R_tarifas.DBCol.cod_tarifa) <> 0
        '        s.Append(createCheckbox(drv(ind.bk.obj.R_Temporadas.DBCol.cod_Temporada), checked, drv(ind.bk.obj.R_Temporadas.DBCol.Temporada)))
        '    Next

        '    s.Append("</div>")
        '    s.AppendFormat("<input type='hidden' name='{0}' id='{0}' value='{1}'>", operationinprogress, Me.operationEnum.assignSeason.ToString("D"))

        '    Me.litrelations.Text = s.ToString
        '    Me.litmsgresult.Text = ""
        '    Dim jsFunction As String
        '    jsFunction = " $('#modalasignrelations').modal('show');"
        '    UIMsg.registerScript(Me, jsFunction, "showAsociationModalScript")

        'Catch ex As Exception
        '    cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        'End Try
    End Sub

    Protected Sub btnshowcustomerrelations_Click(sender As Object, e As EventArgs) Handles btnshowcustomerrelations.Click
        Try
            Dim field_itemobjectDec As String = Me.field_itemobject.Value
            ' field_itemobjectDec = UIHelper.URLDeCryptData(field_itemobjectDec)
            AdminTenantSecurity.RequireTariff(Me.codhotel, CInt(field_itemobjectDec))
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
            Me.litrelations.Text = s.ToString
            Me.litmsgresult.Text = ""
            Dim jsFunction As String
            jsFunction = " $('#modalasignrelations').modal('show');"
            UIMsg.registerScript(Me, jsFunction, "showAsociationModalScript")

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Sub




    Protected Sub btnsavetariffrelations_Click(sender As Object, e As EventArgs) Handles btnsavetariffrelations.Click
        Try
            Dim btn As LinkButton = sender
            Dim field_itemobjectDec As String = Me.field_itemobject.Value
            AdminTenantSecurity.RequireTariff(Me.codhotel, CInt(field_itemobjectDec))

            Dim relationCodesAL As New ArrayList
            For Each req As String In Request.Form
                If Not req Is Nothing Then
                    If req.ToUpper.Contains("RELATION_") = True Then
                        Dim reqSplit As String() = req.Split("_")
                        If reqSplit.Length = 2 Then
                            Dim reqVal As Integer = CInt(reqSplit(1))
                            relationCodesAL.Add(reqVal)
                        End If
                    End If
                End If
            Next

            Dim argument As operationEnum = CByte(Request.Form(operationinprogress))
            If argument = operationEnum.assignCustomer Then
                saveCustomerRelations(field_itemobjectDec, relationCodesAL)
            ElseIf argument = operationEnum.assignSeason Then
                saveSeasonRelations(field_itemobjectDec, relationCodesAL)
            Else
                Me.litmsgresult.Text = UIMsg.UIMessageDismiss("Existe un error con el tipo de operación.", CFRConst.msgEnum.errorType)
            End If

        Catch ex As Exception
            Me.litmsgresult.Text = UIMsg.UIMessageDismiss(ex.Message, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Sub saveCustomerRelations(objID As Integer, relationCodesAL As ArrayList)

        Me.saveTariffCustomerRelations(objID, relationCodesAL)
        Me.litmsgresult.Text = UIMsg.UIMessageDismiss("Se han asociado los clientes a esta tarifa.", CFRConst.msgEnum.Ok)
        Dim jsFunction As String = String.Format("setnewrelations({0},'customerlist');", objID)

        UIMsg.registerScript(Me, jsFunction, "asignscript")
    End Sub

    Private Sub saveSeasonRelations(objID As Integer, relationCodesAL As ArrayList)
        Me.saveTariffSeasonRelations(objID, relationCodesAL)
        Me.litmsgresult.Text = UIMsg.UIMessageDismiss("Se han asociado las temporadas a esta tarifa.", CFRConst.msgEnum.Ok)
        Dim jsFunction As String = String.Format("setnewrelations({0},'seasonlist');", objID)
        Dim sCal As String = Me.showCalendar(objID)
        Dim scalEnc As String = Server.HtmlEncode(sCal)
        Dim jsFunctionCal As String = String.Format("$(""#seasoncal_{0}"").html("""");", objID)
        jsFunctionCal += String.Format("$(""#seasoncal_{0}"").html(""{1}"");", objID, sCal)

        UIMsg.registerScript(Me, jsFunction & jsFunctionCal, "asignscript")
    End Sub
    
    Private Function saveTariffCustomerRelations(cod_tarifa As Integer, relationCodesAL As ArrayList)
        Dim ocon As SqlClient.SqlConnection = Nothing
        Dim trans As SqlClient.SqlTransaction = Nothing
       
        Try
            AdminTenantSecurity.RequireTariff(Me.codhotel, cod_tarifa)
            For Each relCode As Integer In relationCodesAL
                AdminTenantSecurity.RequireOperator(Me.codhotel, relCode)
            Next

            'abrimos la conexion para el update
            ocon = ind.bk.DBMgr.getNewConn
            'comienzo de la transaccion
            trans = ind.bk.DBMgr.CreateTransaction(ocon)


            Dim operDelete As New ind.bk.obj.R_tarifas_Manager
            operDelete.deleteTariff_AllOperatorsRelated(cod_tarifa, trans)

            Dim oper As New ind.bk.obj.R_Tarifa_operador_Manager

            For Each relCode As Integer In relationCodesAL
                Dim objto As New ind.bk.obj.R_Tarifa_operador
                objto.cod_operador = relCode
                objto.cod_tarifa = cod_tarifa
                oper.insert(objto, trans)
            Next

            ind.bk.DBMgr.CommitTransaction(trans)

            ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.delete, "Tariff-clients relations saved id:" & cod_tarifa)


        Catch ex As Exception
            ind.bk.DBMgr.rollbackTransaction(trans)
            Throw ex
        Finally

            ind.bk.DBMgr.disposeTransaction(trans)

        End Try
    End Function
    
    Private Function saveTariffSeasonRelations(cod_tarifa As Integer, relationCodesAL As ArrayList)
        Dim ocon As SqlClient.SqlConnection = Nothing
        Dim trans As SqlClient.SqlTransaction = Nothing

        Try
            AdminTenantSecurity.RequireTariff(Me.codhotel, cod_tarifa)
            For Each relCode As Integer In relationCodesAL
                AdminTenantSecurity.RequireSeason(Me.codhotel, relCode)
            Next

            'abrimos la conexion para el update
            ocon = ind.bk.DBMgr.getNewConn
            'comienzo de la transaccion
            trans = ind.bk.DBMgr.CreateTransaction(ocon)


            Dim operDelete As New ind.bk.obj.R_tarifas_Manager
            operDelete.deleteTariff_AllTemporadasRelated(cod_tarifa, trans)

            Dim oper As New ind.bk.obj.R_Tarifas_Temporadas_Manager

            For Each relCode As Integer In relationCodesAL
                Dim objto As New ind.bk.obj.R_Tarifas_Temporadas
                objto.cod_Temporada = relCode
                objto.cod_tarifa = cod_tarifa
                oper.insert(objto, trans)
            Next

            ind.bk.DBMgr.CommitTransaction(trans)

            ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.delete, "Tariff-seasons relations saved id:" & cod_tarifa)


        Catch ex As Exception
            ind.bk.DBMgr.rollbackTransaction(trans)
            Throw ex
        Finally

            ind.bk.DBMgr.disposeTransaction(trans)

        End Try
    End Function
#End Region

#Region "UI helpers"



#End Region



    Protected Sub btnsavetariff_Click(sender As Object, e As EventArgs) Handles btnsavetariff.Click
        Try
            'Me.debugRequestConsole()
            'get tariff and loop in loaded tariff. Tariff not in dataview will not saved
            Dim dvtar As DataView = Me.getDvList
            For Each drvtar In dvtar 'for each tariff
                Dim dvRoomPrices As DataView = Me.getDvPricesByRoomByTariff(drvtar(ind.bk.obj.R_tarifas.DBCol.cod_tarifa))
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



                    'Dim ValueObj As Object = =Request.Form(priceInfo.getRoomIdUI)
                    'If Not ValueObj Is Nothing Then
                    '    Dim values() As String = Request.Form.GetValues(priceInfo.getRoomIdUI)


                    '    For Each value As String In values
                    '        Debug.WriteLine(value)
                    '    Next
                    'Else

                    'End If
                Next

            Next

            cfr.pages.SiteBasePage.showMessage("Las tarifas se han guardado.", Me.Page, CFRConst.msgEnum.Ok)

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Function getRequestValue(reqName As String, Optional returnValue As Double = 0)
        Dim objResult As Object = Request.Form(reqName)
        If objResult Is Nothing Then
            Return returnValue
        Else
            Return objResult
        End If
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


End Class
