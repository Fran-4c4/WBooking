
Imports System.Data

Partial Class Admin_temporada
    Inherits AdminBasePage
    'Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P6_Configuracion_de_Datos_hotel

    Friend mycal As New CalendarUI.calendarjs
    Friend objId As String
    Friend objIdDec As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.hotelConfiguration)

        Try
            Me.litmsgresult.Text = ""

            If Not Page.IsPostBack Then
                Dim dayNow As Date = Date.Now
                Me.checkindate.Text = dayNow.ToString("dd/MM/yyyy")
                dayNow = dayNow.AddMonths(6)
                Me.checkoutdate.Text = dayNow.ToString("dd/MM/yyyy")

                Me.configCombos()

                objId = Request.QueryString("objid")
                If objId <> "0" Then
                    AdminTenantSecurity.RequireSeason(Me.codhotel, CInt(objId))
                    'objIdDec = UIHelper.URLDeCryptData(objId)
                    'Me.objId = objIdDec
                    objIdDec = objId
                    Me.temporadaCbo.SelectedValue = objIdDec
                    Me.getItemObjectByID(objIdDec)

                End If

            Else


            End If

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

#Region "Config cbo and load object"

    Private Sub getItemObjectByID(objId As Integer)
        AdminTenantSecurity.RequireSeason(Me.codhotel, objId)
        Dim tableRes As New ind.bk.obj.R_Temporadas_Manager

        Dim dt As DataTable = tableRes.getDataTableByID(objId)
        If dt.Rows.Count = 0 Then
            cfr.pages.SiteBasePage.showMessage("Lo sentimos no se ha encontrado el elemento solicitado.", Me.Page, CFRConst.msgEnum.errorType)
            Exit Sub
        End If
        Dim drv As New cfr.tool.UIDatarow(dt.Rows(0))

        Me.objidhidden.Value = drv.asIntegerStr(ind.bk.obj.R_Temporadas.DBCol.cod_Temporada)
        Me.objidLbl.Text = Me.objidhidden.Value
        Me.objNameLbl.Text = drv.asStr(ind.bk.obj.R_Temporadas.DBCol.Temporada)
        Me.litmsgresult.Text = ""

        Me.getDvListPeriodos(objId)

    End Sub

    Private Sub configCombos()
        Dim tableRes As New ind.bk.obj.R_Temporadas_Manager

            Dim dv As DataView = tableRes.getTemporadasByCodHotel(Me.codhotel).DefaultView
            dv.Sort = ind.bk.obj.R_Temporadas.DBCol.Temporada
            With Me.temporadaCbo.Items
                For Each drv As DataRowView In dv
                    .Add(New ListItem(drv(ind.bk.obj.R_Temporadas.DBCol.Temporada), drv(ind.bk.obj.R_Temporadas.DBCol.cod_Temporada)))
                Next
            End With

            With Me.firstdaycbo.Items
                .Add(New ListItem("Seleccionar dia inicial", 0))
                .Add(New ListItem("Desde el Lunes", 1))
                .Add(New ListItem("Desde el Martes", 2))
                .Add(New ListItem("Desde el Miercoles", 3))
                .Add(New ListItem("Desde el Jueves", 4))
                .Add(New ListItem("Desde el Viernes", 5))
                .Add(New ListItem("Desde el Sabado", 6))
                .Add(New ListItem("Desde el Domingo", 7))
            End With

            With Me.lastdaycbo.Items
                .Add(New ListItem("Seleccionar dia final", 0))
                .Add(New ListItem("el Lunes", 1))
                .Add(New ListItem("el Martes", 2))
                .Add(New ListItem("el Miercoles", 3))
                .Add(New ListItem("el Jueves", 4))
                .Add(New ListItem("el Viernes", 5))
                .Add(New ListItem("el Sabado", 6))
                .Add(New ListItem("el Domingo", 7))
            End With


    End Sub

    Friend Function getDvList() As DataView
        Try
            Dim tableRes As New ind.bk.obj.R_Temporadas_Manager

            Dim dv As DataView = tableRes.getTemporadasByCodHotel(Me.codhotel).DefaultView
            dv.Sort = ind.bk.obj.R_Temporadas.DBCol.Temporada
            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Function

    Public Sub getDvListPeriodos(objId As Integer)
        Try
            AdminTenantSecurity.RequireSeason(Me.codhotel, objId)
            Dim tableRes As New ind.bk.obj.R_Temporadas_Manager
            Dim rsPeriodos As DataView = tableRes.getPeriodosActiveByIDTemporada(objId).DefaultView
            Me.ListView1.DataSource = rsPeriodos
            Me.ListView1.DataBind()
        Catch ex As Exception
            Throw New Exception("Error configuring periods for : " & objId & " " & ex.Message, ex)
        End Try

    End Sub

    Protected Sub temporadaCbo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles temporadaCbo.SelectedIndexChanged
        Try
            Me.objId = Me.temporadaCbo.SelectedValue
            Me.getItemObjectByID(Me.objId)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

#End Region

    Protected Sub btnsubmit_Click(sender As Object, e As EventArgs) Handles btnsubmit.Click
        Try


            'save
            If Page.IsValid = False Then
                Exit Sub
            End If
            Dim checkindateES As String = Me.checkindate.Text.Replace("-", "/")
            Dim checkoutdateES As String = Me.checkoutdate.Text.Replace("-", "/")

            Dim datein As Date = DateTime.ParseExact(checkindateES, "dd/MM/yyyy", New System.Globalization.CultureInfo("es-ES"))
            Dim dateOut As Date = DateTime.ParseExact(checkoutdateES, "dd/MM/yyyy", New System.Globalization.CultureInfo("es-ES"))
            If dateOut < datein Then
                cfr.pages.SiteBasePage.showMessage("La fecha final no puede ser inferior a la inicial", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            ElseIf datein < Date.Now.Date Then
                'cfr.pages.SiteBasePage.showMessage("La fecha de inicio no puede ser inferior a la actual", Me.Page, CFRConst.msgEnum.errorType)
                'Exit Sub
            End If

            Dim operMng As New ind.bk.obj.R_Temporadas_Periodos_Manager

            Dim obj2 As New ind.bk.obj.R_Temporadas_Periodos


            obj2.cod_temporada = Me.objidhidden.Value
            AdminTenantSecurity.RequireSeason(Me.codhotel, obj2.cod_temporada)

            If Me.rdballdays.Checked = True Then
                'all days
                obj2.fechainicio = datein
                obj2.fechafin = dateOut
                operMng.insert(obj2)

            ElseIf Me.rdbweekendonly.Checked = True Then
                'only weekend
                Me.NewPeriodoByRange(obj2.cod_temporada, datein, dateOut, 5, 7)

            ElseIf Me.rdbselecteddays.Checked = True Then
                'by range
                Dim range1 As Integer = Me.firstdaycbo.Text
                Dim range2 As Integer = Me.lastdaycbo.Text
                If range2 <= range1 Then
                    cfr.pages.SiteBasePage.showMessage("El rango de dias debe ser entre dias logicos: De viernes a Domingo o de Lunes a Jueves", Me.Page, CFRConst.msgEnum.errorType)
                    Exit Sub
                End If
                Me.NewPeriodoByRange(obj2.cod_temporada, datein, dateOut, range1, range2)
            End If

            getDvListPeriodos(Me.objidhidden.Value)
            saveSeasonScript(Me.objidhidden.Value)


            Me.litmsgresult.Text = UIMsg.UIMessageDismiss("Periodo creado", CFRConst.msgEnum.Ok)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

   


    Private Sub saveSeasonScript(objID As Integer)

        Dim jsFunction As String = "" ' String.Format("setnewrelations({0},'seasonlist');", objID)
        Dim sCal As String = Me.showCalendar(objID)

        Dim jsFunctionCal As String = String.Format("$(""#seasoncal_{0}"").html("""");", objID)
        jsFunctionCal += String.Format("$(""#seasoncal_{0}"").html(""{1}"");", objID, sCal)

        UIMsg.registerScript(Me, jsFunction & jsFunctionCal, "asignscript")
    End Sub

    Friend Function showCalendar(objid As Integer) As String
        AdminTenantSecurity.RequireSeason(Me.codhotel, objid)
        Return CalendarUI.showCalendarForTemporada(objid, 20)
    End Function





    Protected Sub ListView1_ItemDeleting(sender As Object, e As ListViewDeleteEventArgs) Handles ListView1.ItemDeleting

        Try
            Dim lnkbtn As LinkButton = CType((ListView1.Items(e.ItemIndex).FindControl("lnkitem2delete")), LinkButton)
            AdminTenantSecurity.RequireSeason(Me.codhotel, CInt(Me.objidhidden.Value))
            AdminTenantSecurity.RequireSeasonPeriod(Me.codhotel, CInt(lnkbtn.CommandArgument))
            Dim tableRes As New ind.bk.obj.R_Temporadas_Periodos_Manager
            tableRes.Delete(lnkbtn.CommandArgument)
            getDvListPeriodos(Me.objidhidden.Value)
            Me.saveSeasonScript(Me.objidhidden.Value)
            Me.litmsgresult.Text = UIMsg.UIMessageDismiss("El periodo ha sido borrado ", CFRConst.msgEnum.Ok)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub


    ''' <summary>
    ''' Inserta un periodo por cada intervalo
    ''' </summary>
    ''' <param name="cod_temporada"></param>
    ''' <param name="FEC1"></param>
    ''' <param name="FEC2"></param>
    ''' <param name="dia1"></param>
    ''' <param name="dia2"></param>
    ''' <remarks></remarks>
    Public Sub NewPeriodoByRange(cod_temporada As Integer, FEC1 As Date, FEC2 As Date, dia1 As Integer, dia2 As Integer)
        AdminTenantSecurity.RequireSeason(Me.codhotel, cod_temporada)
        Dim ocon As SqlClient.SqlConnection
        Dim trans As SqlClient.SqlTransaction

        Try
            Dim diaSemanaInicio As Integer

            diaSemanaInicio = DatePart("w", FEC1, vbMonday)
            'vemos el dia de inicio de cada periodo de fin de semana
            'vemos los dias de cada periodo

            Dim nDias As Integer
            If dia1 > dia2 Then
                nDias = ((7 - dia1) + dia2) + 1 'ejemplo del miercoles al sabdo son 4 noches
            ElseIf dia2 > dia1 Then
                nDias = dia2 - dia1 + 1 'ejemplo del miercoles al sabdo son 4 noches
            ElseIf dia2 = dia1 Then
                nDias = 1
            End If

            Dim fecPeriodo As Date, fecPeriodo2 As Date
            fecPeriodo = FEC1.AddDays((dia1 - diaSemanaInicio))
            fecPeriodo2 = fecPeriodo.AddDays(nDias - 1)
            If fecPeriodo2 > FEC2 Then
                cfr.pages.SiteBasePage.showMessage("La fecha final esta fuera del intervalo. Puede reducir o incrementar el intervalo de fechas para corregir esta opcion.", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If

            Dim operMng As New ind.bk.obj.R_Temporadas_Periodos_Manager
            Dim obj2 As New ind.bk.obj.R_Temporadas_Periodos
            obj2.cod_temporada = cod_temporada

            'abrimos la conexion para el update
            ocon = ind.bk.DBMgr.getNewConn
            'comienzo de la transaccion
            trans = ind.bk.DBMgr.CreateTransaction(ocon)

            Do While fecPeriodo <= FEC2
                obj2.fechainicio = fecPeriodo
                obj2.fechafin = fecPeriodo2
                operMng.insert(obj2, trans)

                If fecPeriodo2 = FEC2 Then
                    Exit Do
                End If
                fecPeriodo = fecPeriodo.AddDays(7)
                fecPeriodo2 = fecPeriodo2.AddDays(7)
                If fecPeriodo2 > FEC2 Then
                    cfr.pages.SiteBasePage.showMessage("El ultimo intervalo no se ha podido crear por caer fuera de las fechas seleccionadas. Puede reducir o incrementar el intervalo de fechas para corregir esta opcion.", Me.Page, CFRConst.msgEnum.errorType)
                    cfr.pages.SiteBasePage.showMessage("El resto de periodos han sido creados", Me.Page, CFRConst.msgEnum.Ok)
                    Exit Do
                End If
            Loop

            ind.bk.DBMgr.CommitTransaction(trans)

        Catch ex As Exception
            ind.bk.DBMgr.rollbackTransaction(trans)
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        Finally

            ind.bk.DBMgr.disposeTransaction(trans)
        End Try
    End Sub

End Class
