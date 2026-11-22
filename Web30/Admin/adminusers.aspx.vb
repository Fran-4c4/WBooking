

Imports System.Data

Imports System.Linq

Partial Class Admin_adminusers
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P17_Usuarios_Crear_Editar_Borrar


    Friend objid As Integer
    Friend dvListUsers As New DataView
    Friend userSelectedPermisos As ind.bk.obj.R_Usuarios_PermisosCollection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.userSecurity)
        Try

            If Not Page.IsPostBack Then
                Me.getDvList()
            Else

            End If
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try


    End Sub

    Friend Function getDvList() As DataView
        Try
            Dim tableRes As New ind.bk.obj.R_Usuarios

            Dim dv As DataView = tableRes.getDataTableByCod_hotel(Me.codhotel).DefaultView
            Me.dvListUsers = dv
            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

        Return Nothing

    End Function

    Friend Function getUserPermissions(id_usuario As Integer)
        Try
            Dim tableRes As New ind.bk.obj.R_Usuarios_Permisos

            Dim dv As DataView = tableRes.getDataTableByIDUsuario(id_usuario).DefaultView

            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

        Return Nothing
    End Function


    Friend Function tienepermiso(el_cod_permiso As Integer) As String
        If Me.userSelectedPermisos Is Nothing Then
            Return ""
        End If
        For Each item As ind.bk.obj.R_Usuarios_Permisos In Me.userSelectedPermisos
            If item.cod_permiso = el_cod_permiso Then
                tienepermiso = " checked class='chk-selected' "
                Return tienepermiso
            End If
        Next

        Return ""
    End Function


    Protected Sub btnseeuserpermisionshdd_Click(sender As Object, e As EventArgs) Handles btnseeuserpermisionshdd.Click
        Dim field_itemobjectDec As String
        Try

            field_itemobjectDec = Me.field_itemobject.Value
            RequireUserOwnership(CInt(field_itemobjectDec))
            'field_itemobjectDecURL = UIHelper.URLDeCryptData(field_itemobjectDec)

            'get permisos
            Dim rp As New ind.bk.obj.R_Usuarios_Permisos
            userSelectedPermisos = ind.bk.obj.R_Usuarios_PermisosCollection.getCollectionByTable(rp.getDataTableByIDUsuario(field_itemobjectDec))
            Me.PlaceHolder1.Visible = True
            Me.litUserName.Text = Me.field_itemname.Value()
            Me.litrespermissions.Text = ""
            ViewState.Add(ind.bk.obj.R_Usuarios.DBCol.id_usuario, field_itemobjectDec)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    
    Protected Sub btnsavepermisions_Click(sender As Object, e As EventArgs) Handles btnsavepermisions.Click
        Dim ocon As SqlClient.SqlConnection
        Dim trans As SqlClient.SqlTransaction = Nothing

        Try
            Dim userid As Integer = ViewState(ind.bk.obj.R_Usuarios.DBCol.id_usuario)
            RequireUserOwnership(userid)
            Dim pemisos As String = "" & Request("permisos")
            

            ocon = ind.bk.DBMgr.getNewConn 'abrimos la conexion para el update
            trans = ind.bk.DBMgr.CreateTransaction(ocon) 'comienzo de la transaccion

            Dim usermgr As New ind.bk.obj.R_Usuarios
            usermgr.DeleteByUserID(userid, trans)
            If pemisos <> "" Then
                Dim permisosVector() As String = pemisos.Split(",")
                For Each per In permisosVector
                    Dim rp As New ind.bk.obj.R_Usuarios_Permisos
                    rp.cod_permiso = per
                    rp.id_usuario = userid
                    rp.insert(rp, trans)
                Next
            End If


            'get permisos
            Dim rper As New ind.bk.obj.R_Usuarios_Permisos
            userSelectedPermisos = ind.bk.obj.R_Usuarios_PermisosCollection.getCollectionByTable(rper.getDataTableByIDUsuario(userid, trans))

            ind.bk.DBMgr.CommitTransaction(trans)
            Me.litrespermissions.Text = UIMsg.UIMessageDismiss("Los nuevos permisos han sido guardados.", CFRConst.msgEnum.Ok)

        Catch ex As Exception
            ind.bk.DBMgr.rollbackTransaction(trans)
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        Finally

            ind.bk.DBMgr.disposeTransaction(trans)
        End Try

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnsubmit_adminusers.Click
        Try
            Dim userid As Integer = useridhhhedit.Value

            Dim rp As New ind.bk.obj.R_Usuarios
            rp.cod_hotel = Me.codhotel
            rp.id_usuario = userid
            rp.usuario = Me.tusername.Text.Trim
            rp.perfil = Me.tperfil.Text.Trim
            rp.login = Me.tLogin.Text.Trim
            Dim newPassword As String = Me.tclave.Text
            rp.email = "" & Me.temail.Text.Trim

            If rp.id_usuario = 0 Then
                If String.IsNullOrWhiteSpace(newPassword) Then
                    Throw New ArgumentException("La contraseña es obligatoria al crear un usuario.")
                End If
                rp.clave = cfr.security.PasswordHasher.HashPassword(newPassword)
                rp.insert(rp)
                Me.liteditmessage.Text = UIMsg.UIMessageDismiss("El usuario ha sido creado. Ahora puede asignarle los permisos.", CFRConst.msgEnum.Ok)
            Else
                RequireUserOwnership(rp.id_usuario)
                If String.IsNullOrEmpty(newPassword) Then
                    Dim currentData As DataTable = rp.getDataTableByID(rp.id_usuario)
                    If currentData.Rows.Count <> 1 Then
                        Throw New InvalidOperationException("No se ha encontrado el usuario que se desea modificar.")
                    End If
                    rp.clave = CStr(currentData.Rows(0)(ind.bk.obj.R_Usuarios.DBCol.clave))
                Else
                    rp.clave = cfr.security.PasswordHasher.HashPassword(newPassword)
                End If
                rp.Update(rp)
                If Not String.IsNullOrEmpty(newPassword) Then
                    ind.bk.DBMgr.Execute(
                        "DELETE FROM R_UsuariosSession WHERE id_usuario=@userId",
                        New SqlClient.SqlParameter("@userId", SqlDbType.Int) With {.Value = rp.id_usuario})
                End If
                Me.liteditmessage.Text = UIMsg.UIMessageDismiss("El usuario ha sido modificado.", CFRConst.msgEnum.Ok)
            End If

            Me.getDvList()
            Me.UpdatePanel_userlist.Update()

        Catch exsql As SqlClient.SqlException
            If exsql.Number = 2627 Then
                Me.liteditmessage.Text = UIMsg.UIMessageDismiss("Ha intentado introducir un login de usuario que ya existe. Por favor seleccione otro.", CFRConst.msgEnum.errorType)
            Else
                Me.liteditmessage.Text = UIMsg.UIMessageDismiss(exsql.Message, CFRConst.msgEnum.errorType)
            End If

        Catch ex As Exception
            Me.liteditmessage.Text = UIMsg.UIMessageDismiss(ex.Message, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Protected Sub btnDeleteItem_Click(sender As Object, e As EventArgs) Handles btnDeleteItem.Click
        Dim field_itemobjectDec As String
        Try
            field_itemobjectDec = Me.field_itemobject.Value
            RequireUserOwnership(CInt(field_itemobjectDec))
            If Me.ViewState(ind.bk.obj.R_Usuarios.DBCol.id_usuario) = field_itemobjectDec Then
                Me.PlaceHolder1.Visible = False
                'Me.UpdatePanel_userpermision.Update()

            End If
            'field_itemobjectDecURL = UIHelper.URLDeCryptData(field_itemobjectDec)

            Dim tableRes As New ind.bk.obj.R_Usuarios
            tableRes.Delete(CInt(field_itemobjectDec))
            ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.delete, "Usuario:" & field_itemobjectDec)
            cfr.pages.SiteBasePage.showMessage("El usuario ha sido borrado.", Me.Page, CFRConst.msgEnum.delete)

            Me.getDvList()
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Sub RequireUserOwnership(ByVal userId As Integer)
        Using ctx As New cfrbookEntities
            Dim ownsUser As Boolean =
                (From candidate In ctx.R_Usuarios
                 Where candidate.id_usuario = userId AndAlso candidate.cod_hotel = Me.codhotel
                 Select candidate.id_usuario).Any()
            If Not ownsUser Then
                Throw New HttpException(404, "Administrator user not found.")
            End If
        End Using
    End Sub

 

End Class
