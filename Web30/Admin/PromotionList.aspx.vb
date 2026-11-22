Imports System.Data

Partial Class Admin_PromotionList
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P11_Clientes_Crear_Editar_Borrar

    Friend dvTariffJoker As DataView
    Friend mdvCustomer As DataView
    Friend Property dvCustomer As DataView

        Get
            If Me.mdvCustomer Is Nothing Then
                Return Me.getDvList
            Else
                Return Me.mdvCustomer
            End If
        End Get
        Set(value As DataView)
            Me.mdvCustomer = value
        End Set
    End Property
    Friend objid As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.operators)

        If Not Page.IsPostBack Then
            
        Else

        End If

    End Sub


    Friend CustomerListForPresentation As New ArrayList

    Friend Function getDvList() As DataView
        Try
            Dim tableRes As New ind.bk.obj.R_operadores_Manager

            Dim dv As DataView = tableRes.getOperadoresByIDHotel(Me.codhotel).DefaultView

            dv.RowFilter = ind.bk.obj.R_operadores.DBCol.tipo & "=8"

            For Each drvList As DataRowView In dv
                If drvList(ind.bk.obj.R_operadores.DBCol.FreeSale) <> cfr.CFRConst.genericOperator.PrivateGeneral Then
                    CustomerListForPresentation.Add(drvList)
                ElseIf drvList(ind.bk.obj.R_operadores.DBCol.FreeSale) = cfr.CFRConst.genericOperator.PrivateGeneral Then
                    CustomerListForPresentation.Insert(0, drvList)
                End If

            Next

            Me.dvCustomer = dv
            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

        Return Nothing

    End Function


    Protected Sub btncreateitem_Click(sender As Object, e As EventArgs) Handles btncreateitem.Click
        Try

            If Me.cliente_0.Text.Trim = "" Or promotioncode.Text.Trim = "" Then
                cfr.pages.SiteBasePage.showMessage("Debe introducir un nombre valido", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If

            Try
                Dim ctx As New cfrbookEntities
                Dim obj2 As New R_operadorEF
                With obj2
                    obj2.cod_hotel = Me.codhotel
                    obj2.Operador = Me.cliente_0.Text.Trim
                    obj2.tipo = CByte(cfr.CFRConst.ClientTypeEnum.promotionCode)
                    obj2.FreeSale = 1
                    obj2.login = promotioncode.Text
                    obj2.Clave = cfr.security.PasswordHasher.HashPassword(ind.tools.RandomPassword.Generate(12, 16))
                    obj2.discountapplyfrom = Date.Now
                    obj2.discountapplyuntil = Date.Now
                    obj2.discountapply = False
                    obj2.discountNoRefund = False
                    obj2.discountNotApplyWithNoRefundTariff = False
                    obj2.Active = True
                End With
                ctx.R_operadorEF.Add(obj2)
                ctx.SaveChanges()
                cfr.pages.SiteBasePage.showMessage("El codigo promocional ha sido guardado.", Me.Page, CFRConst.msgEnum.Ok)
            Catch ex As Exception
                cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
            End Try
            
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Dim ctx As cfrbookEntities = Nothing
        Try

            Dim field_itemobjectDec As String = Me.itemdeletecode.Value
            field_itemobjectDec = UIHelper.URLDeCryptData(field_itemobjectDec)
            AdminTenantSecurity.RequireOperator(Me.codhotel, CInt(field_itemobjectDec))


            ctx = New cfrbookEntities
            Dim operadorPromotion As R_operadorEF = ctx.R_operadorEF.Find(CInt(field_itemobjectDec))

            operadorPromotion.Active = False
            ctx.SaveChanges()
            ctx.Dispose()

            'Dim tableRes As New ind.bk.obj.R_operadores_Manager
            'tableRes.DeleteOperador(CInt(field_itemobjectDec), Me.codhotel)

            ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.delete, "promotion  deleted id:" & field_itemobjectDec)

            Me.btndelete.Attributes.Add("class", Me.btndelete.Attributes("class") & " hidden")

            Dim jsFunction As String = String.Format("$('#rowobj_{0}').remove();", field_itemobjectDec)
            jsFunction += String.Format("$('#modaldeleteItem .resultmessageplace').html(""{0}"");", UIMsg.UIMessage("El codigo promocional ha sido borrado", CFRConst.msgEnum.Ok))
            UIMsg.registerScript(Me, jsFunction, "delHideModalScript")

        Catch ex As Exception
            Try
                If Not ctx Is Nothing Then
                    ctx.Dispose()
                End If
            Finally


            End Try
            Try
                Dim msgclean As String = UIMsg.cleanIllegalStr(ex.Message)
                Dim jsFunction As String = String.Format("$('#resultmessageplace').html(""{0}"" );", UIMsg.UIMessage("Se ha producido un error.<br/>" & msgclean, CFRConst.msgEnum.errorType))
                UIMsg.registerScript(Me, jsFunction, "delHideModalScript")
            Finally

            End Try

        Finally

        End Try
    End Sub

    Protected Sub btnsubmit_Click(sender As Object, e As EventArgs) Handles LinkButtonSaveClient.Click

        Try
            'save
            If Page.IsValid = False Then
                Exit Sub
            End If

            Dim field_itemobjectDec As String = Me.field_itemobject.Value
            field_itemobjectDec = UIHelper.URLDeCryptData(field_itemobjectDec)
            AdminTenantSecurity.RequireOperator(Me.codhotel, CInt(field_itemobjectDec))

            Dim tableRes As New ind.bk.obj.R_operadores_Manager
            Dim operMng As New ind.bk.obj.R_operadores_Manager
            Dim dt As DataTable = operMng.getDataTableByID(field_itemobjectDec)
            If dt.Rows.Count = 0 Then
                cfr.pages.SiteBasePage.showMessage("No se ha encontrado el registro solicitado por su ID:" & field_itemobjectDec, Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If
            Try
                Dim ctx As New cfrbookEntities
                Dim obj2 As R_operadorEF = ctx.R_operadorEF.Find(CInt(field_itemobjectDec))
                With obj2
                    Dim operatorName As Object = Request("tname_" & field_itemobjectDec)
                    If Not operatorName Is Nothing Then 'some fields can´t not be changed and not exist in page
                        .Operador = operatorName
                    End If
                    .login = Request("tLogin_" & field_itemobjectDec)
                    If cfr.security.PasswordHasher.NeedsUpgrade(.Clave) Then
                        .Clave = cfr.security.PasswordHasher.HashPassword(ind.tools.RandomPassword.Generate(12, 16))
                    End If
                    .discount = Request("tdiscount_" & field_itemobjectDec)
                    .Release = 0
                   .discountapply = UIHelper.getCheckBoxValue("" & Request("tdiscountapply_" & field_itemobjectDec))
                

                    .discountapplyfrom = Request("tdiscountapplyfrom_" & field_itemobjectDec)
                    .discountapplyuntil = Request("tdiscountapplyuntil_" & field_itemobjectDec)
                    If .discountapplyuntil.Value.Date < .discountapplyfrom.Value.Date Then
                        Throw New Exception("El dia final no puede ser menor que el inicial. Compruebe las fechas de la promoción.")
                    End If

                    '.discountNoRefund =
                    .discountNotApplyWithNoRefundTariff = UIHelper.getCheckBoxValue("" & Request("chkdiscountNotApplyWithNoRefundTariff_" & field_itemobjectDec))


                End With
                ctx.SaveChanges()
                cfr.pages.SiteBasePage.showMessage("El codigo promocional ha sido guardado.", Me.Page, CFRConst.msgEnum.Ok)
            Catch ex As Exception
                cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
            End Try

            Exit Sub

            'Dim obj2 As ind.bk.obj.R_operadores = ind.bk.obj.R_operadores.FillObjectByRow(dt.Rows(0), Nothing)
            'obj2.objChanged = False

            'ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.delete, "promotion edited id:" & field_itemobjectDec)

            'With obj2

            '    .cod_operador = field_itemobjectDec
            '    Dim operatorName As Object = Request("tname_" & field_itemobjectDec)
            '    If Not operatorName Is Nothing Then 'some fields can´t not be changed and not exist in page
            '        .Operador = operatorName
            '    End If
            '    .login = Request("tLogin_" & field_itemobjectDec)
            '    .Clave = .login
            '    .discount = Request("tdiscount_" & field_itemobjectDec)
            '    .Release = 0

            'End With

            'If obj2.objChanged = False Then
            '    cfr.pages.SiteBasePage.showMessage("No se ha guardado nada porque no se habia modificado ningún valor.", Me.Page, CFRConst.msgEnum.info)
            '    Exit Sub
            'End If

            'operMng.Update(obj2)

            'cfr.pages.SiteBasePage.showMessage("El codigo promocional ha sido guardado.", Me.Page, CFRConst.msgEnum.Ok)

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Friend Function getDvClientTariffAssingned(objId As Integer)
        Try
            AdminTenantSecurity.RequireOperator(Me.codhotel, objId)
            Dim tableRes As New ind.bk.obj.R_tarifas_Manager

            Dim dv As DataView = tableRes.getDT_TariffByClient(objId).DefaultView
            'dv.Sort = ind.bk.obj.R_Temporadas.DBCol.Temporada
            'Return Sorted  from ddbb by operator name
            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

        Return Nothing
    End Function

    Protected Sub btnSaveRelations_Click(sender As Object, e As EventArgs) Handles btnSaveRelations.Click
        Dim ocon As SqlClient.SqlConnection = Nothing
        Dim trans As SqlClient.SqlTransaction = Nothing
        Dim msgplace As String = "$('#resultmsg_{0}')"
        Try

            Dim field_itemobjectDec As String = Me.field_itemobject.Value
            ' field_itemobjectDec = UIHelper.URLDeCryptData(field_itemobjectDec)
            Me.objid = field_itemobjectDec
            AdminTenantSecurity.RequireOperator(Me.codhotel, Me.objid)
            msgplace = String.Format(msgplace, field_itemobjectDec)
            Dim tableRes As New ind.bk.obj.R_operadores_Manager
            'Me.debugRequestConsole()
            Dim tariffcodesAL As New ArrayList
            For Each req As String In Request.Form
                If Not req Is Nothing Then
                    If req.ToUpper.Contains("CHKTARIFF_") = True Then
                        Dim reqSplit As String() = req.Split("_")
                        If reqSplit.Length = 3 Then
                            If CInt(reqSplit(1)) = objid Then
                                Dim reqVal As Integer = CInt(reqSplit(2))
                                tariffcodesAL.Add(reqVal)
                            End If
                        End If
                    End If
                End If
            Next

            For Each tariffCode As Integer In tariffcodesAL
                AdminTenantSecurity.RequireTariff(Me.codhotel, tariffCode)
            Next

            'abrimos la conexion para el update
            ocon = ind.bk.DBMgr.getNewConn
            'comienzo de la transaccion
            trans = ind.bk.DBMgr.CreateTransaction(ocon)


            Dim operDelete As New ind.bk.obj.R_tarifas_Manager
            operDelete.deleteOperator_AllTariffsRelated(objid, trans)

            Dim oper As New ind.bk.obj.R_Tarifa_operador_Manager

            For Each tarcode As Integer In tariffcodesAL
                Dim objto As New ind.bk.obj.R_Tarifa_operador
                objto.cod_operador = objid
                objto.cod_tarifa = tarcode
                oper.insert(objto, trans)
            Next
            ind.bk.DBMgr.CommitTransaction(trans)


            ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.delete, "Customer relations saved id:" & field_itemobjectDec)

            Dim jsFunction As String = String.Format("{0}.html(""{1}"");", msgplace, UIMsg.UIMessageDismiss("Se han actualizado las asignaciones", CFRConst.msgEnum.Ok))

            UIMsg.registerScript(Me, jsFunction, "asignscript")

        Catch ex As Exception
            ind.bk.DBMgr.rollbackTransaction(trans)
            Try
                Dim msgclean As String = UIMsg.cleanIllegalStr(ex.Message)

                Dim jsFunction As String = String.Format("{0}.html(""{1}"");", msgplace, UIMsg.UIMessageDismiss("Se ha producido un error.<br/> " & msgclean, CFRConst.msgEnum.errorType))

                UIMsg.registerScript(Me, jsFunction, "asignscript")
            Catch ex1 As Exception

            End Try

        Finally

            ind.bk.DBMgr.disposeTransaction(trans)

        End Try

    End Sub
    ''' <summary>
    ''' return all tariffs and selected by customer
    ''' </summary>
    ''' <param name="objid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function getDvCustomerTariffHotel(objid As Integer) As DataView
        Try
            AdminTenantSecurity.RequireOperator(Me.codhotel, objid)
            Dim tableRes As New ind.bk.obj.R_tarifas_Manager
            Dim dv As DataView = tableRes.getDT_TariffByClientForUI(objid, Me.codhotel).DefaultView

            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
        Return Nothing
    End Function





End Class
