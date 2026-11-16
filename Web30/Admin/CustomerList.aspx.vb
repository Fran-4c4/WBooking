Imports System.Data

Partial Class Admin_CustomerList
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P11_Clientes_Crear_Editar_Borrar

    Friend hasTpv As Boolean = False

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
            Dim tpvmgr As New ind.bk.obj.R_TPVDATA_Manager
            Dim dt As DataTable = tpvmgr.getHotelTPVDataByIDHotel(Me.codhotel)
            If dt.Rows.Count > 0 Then
                hasTpv = True
            End If
        Else

        End If
        
    End Sub




    Friend CustomerListForPresentation As New ArrayList

    Friend Function getDvList() As DataView
        Try
            Dim tableRes As New ind.bk.obj.R_operadores_Manager

            Dim dv As DataView = tableRes.getOperadoresByIDHotel(Me.codhotel).DefaultView

            dv.RowFilter = ind.bk.obj.R_operadores.DBCol.FreeSale & ">0 and tipo<>" & CFRConst.ClientTypeEnum.promotionCode

            For Each drvList As DataRowView In dv
                If drvList(ind.bk.obj.R_operadores.DBCol.FreeSale) <> cfr.CFRConst.genericOperator.PrivateGeneral Then
                    CustomerListForPresentation.Add(drvList)
                ElseIf drvList(ind.bk.obj.R_operadores.DBCol.FreeSale) = cfr.CFRConst.genericOperator.PrivateGeneral Then
                    CustomerListForPresentation.Insert(0, drvList)
                End If

            Next


            'dv.Sort = ind.bk.obj.R_operadores.DBCol.Operador
            Me.dvCustomer = dv
            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

        Return Nothing

    End Function



    
    Friend Function getUICustomerType(tipo As Integer) As String
        Dim strtipo As String = ""
        If tipo = cfr.CFRConst.ClientTypeEnum.NotClasified Then
            strtipo = "Sin clasificar"
        ElseIf tipo = cfr.CFRConst.ClientTypeEnum.HotelAgency Then
            strtipo = "Agencias"
        ElseIf tipo = cfr.CFRConst.ClientTypeEnum.CRS Then
            strtipo = "CRS"
        ElseIf tipo = cfr.CFRConst.ClientTypeEnum.TourOperators Then
            strtipo = "TourOperadores"
        ElseIf tipo = cfr.CFRConst.ClientTypeEnum.Company Then
            strtipo = "Empresas"
        ElseIf tipo = cfr.CFRConst.ClientTypeEnum.PrivateClient Then
            strtipo = "Particulares"
        ElseIf tipo = cfr.CFRConst.ClientTypeEnum.WebSite Then
            strtipo = "Websites"
        End If
        Return strtipo
    End Function

    Protected Sub btncreateitemcustomer_Click(sender As Object, e As EventArgs) Handles btncreateitemcustomer.Click
        Try

            If Me.cliente_0.Text.Trim = "" Then
                cfr.pages.SiteBasePage.showMessage("Debe introducir un nombre valido", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If
            Dim tableRes As New ind.bk.obj.R_operadores_Manager
            Dim obj2 As New ind.bk.obj.R_operadores
            obj2.cod_hotel = Me.codhotel
            obj2.Operador = Me.cliente_0.Text.Trim
            obj2.tipo = Me.cbotipocliente_0.Text
            obj2.FreeSale = 1
            Dim login As String = Me.cliente_0.Text.Trim.Replace(" ", "").Replace("/", "").Replace("+", "")
            obj2.login = login
            obj2.Clave = cfr.security.PasswordHasher.HashPassword(ind.tools.RandomPassword.Generate(12, 16))

            tableRes.insert(obj2)
            ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.insert, "Operador:" & obj2.cod_operador)
            cfr.pages.SiteBasePage.showMessage("El cliente ha sido creado, ahora puede modificar el resto de parametros.", Me.Page, CFRConst.msgEnum.Ok)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            Dim field_itemobjectDec As String = Me.itemdeletecode.Value
            field_itemobjectDec = UIHelper.URLDeCryptData(field_itemobjectDec)
            AdminTenantSecurity.RequireOperator(Me.codhotel, CInt(field_itemobjectDec))

            Dim tableRes As New ind.bk.obj.R_operadores_Manager
            tableRes.DeleteOperador(CInt(field_itemobjectDec), Me.codhotel)
            ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.delete, "Customer deleted id:" & field_itemobjectDec)
            Me.btndelete.Attributes.Add("class", Me.btndelete.Attributes("class") & " hidden")

            Dim jsFunction As String = String.Format("$('#rowobj_{0}').remove();", field_itemobjectDec)
            jsFunction += String.Format("$('#modaldeleteItem .resultmessageplace').html(""{0}"");", UIMsg.UIMessage("El cliente ha sido borrado", CFRConst.msgEnum.Ok))
            'sb.Append("$('#modaldeleteItem').modal('hide');")
           UIMsg.registerScript(Me, jsFunction, "delHideModalScript")

        Catch ex As Exception
            Try
                Dim msgclean As String = UIMsg.cleanIllegalStr(ex.Message)
                Dim jsFunction As String = String.Format("$('#resultmessageplace').html(""{0}"" );", UIMsg.UIMessage("Se ha producido un error.<br/>" & msgclean, CFRConst.msgEnum.errorType))
                UIMsg.registerScript(Me, jsFunction, "delHideModalScript")
            Catch ex1 As Exception

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
            Dim obj2 As ind.bk.obj.R_operadores = ind.bk.obj.R_operadores.FillObjectByRow(dt.Rows(0), Nothing)
            obj2.objChanged = False

            Dim paymentMethod As CFRConst.formaPago
            If Not PaymentMethodPolicy.TryParseSupported(Request("cboformapago_" & field_itemobjectDec), paymentMethod) Then
                cfr.pages.SiteBasePage.showMessage("Seleccione transferencia o pago directo en el hotel.", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If

            ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.delete, "Customer edited id:" & field_itemobjectDec)


            With obj2

                .cod_operador = field_itemobjectDec
                Dim operatorName As Object = Request("tname_" & field_itemobjectDec)
                If Not operatorName Is Nothing Then 'some fields can´t not be changed and not exist in page
                    .Operador = operatorName
                End If

                .tipo = Request("cbotipocliente_" & field_itemobjectDec)
                .cod_Cadena = Request("clienteCadena_" & field_itemobjectDec)
                .generico = Request("cbogenerico_" & field_itemobjectDec)
                .login = Request("tLogin_" & field_itemobjectDec)
                Dim newPassword As String = Request("tclave_" & field_itemobjectDec)
                If Not String.IsNullOrEmpty(newPassword) Then
                    .Clave = cfr.security.PasswordHasher.HashPassword(newPassword)
                End If
                .Email = Request("temail_" & field_itemobjectDec)
                .Release = Request("trelease_" & field_itemobjectDec)
                .forma_pago = CInt(paymentMethod)
                '.aplicarYMprecio = Request("cboaplicarYMprecio_" & field_itemobjectDec)
                .aplicarYMdiasmin = Request("cboaplicarYMdiasmin_" & field_itemobjectDec)
            End With

            'If obj2.objChanged = False Then
            '    cfr.pages.SiteBasePage.showMessage("No se ha guardado nada porque no se habia modificado ningún registro.", Me.Page, CFRConst.msgEnum.info)
            '    Exit Sub
            'End If

            operMng.Update(obj2)


            cfr.pages.SiteBasePage.showMessage("El cliente ha sido guardado.", Me.Page, CFRConst.msgEnum.Ok)
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
    End Function





End Class
