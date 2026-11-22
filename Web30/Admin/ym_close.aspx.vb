Imports System.Collections.Generic
Imports System.Data

Partial Class Admin_ym_close
    Inherits AdminBasePage
    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P5_Yield_Management_Cierre_Ventas

    Friend mycal As New CalendarUI.calendarjs
    Friend objId As String
    Friend objIdDec As String

    Private dvClientsVar As DataView
    Public Overloads Property dvClients As DataView
        Get
            If Me.dvClientsVar Is Nothing Then
                Me.dvClientsVar = Me.getDvClients
            End If
            Return Me.dvClientsVar
        End Get
        Set(value As DataView)
            dvClientsVar = value
        End Set
    End Property
    Private dvRoomsvar As DataView
    Public Overloads Property dvRooms As DataView
        Get
            If Me.dvRoomsvar Is Nothing Then
                Me.dvRoomsvar = Me.getDvRoomsAll
            End If
            Return Me.dvRoomsvar
        End Get
        Set(value As DataView)
            Me.dvRoomsvar = value
        End Set
    End Property
    Private dvPeriodsVar As DataView
    Public Property dvPeriods As DataView
        Get
            If Me.dvPeriodsVar Is Nothing Then
                Me.dvPeriodsVar = Me.getDvListPeriodos
            End If
            Return Me.dvPeriodsVar
        End Get
        Set(value As DataView)
            Me.dvPeriodsVar = value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.closeSelling)

        Try
            Me.litmsgresult.Text = ""

            If Not Page.IsPostBack Then
                'mycal.israngeplusone = False
                'mycal.configure(Me.checkindate, Me.checkoutdate, calendarjsLit)
                Me.configCombos()

                objId = Request.QueryString("objid")
                If objId <> "0" Then
                    'objIdDec = UIHelper.URLDeCryptData(objId)
                    'Me.objId = objIdDec
                    objIdDec = objId
                    Me.getDvListPeriodos()
                End If
            Else

            End If

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

#Region "Config cbo and load object"


    Private Sub configCombos()


        Dim dayNow As Date = Date.Now
        Me.checkindate.Text = dayNow.ToString("dd/MM/yyyy")
        Me.checkoutdate.Text = dayNow.ToString("dd/MM/yyyy")

        Me.dvClients = getDvClients()
        Dim ventalibrecode As Integer = 0
        With Me.cbooperatorsymclose.Items
            .Add(New ListItem("Aplicar a todos los operadores", "-2"))

            For Each drv As DataRowView In Me.dvClients
                If drv(ind.bk.obj.R_operadores.DBCol.FreeSale) <> cfr.CFRConst.genericOperator.PrivateGeneral Then

                    If drv(ind.bk.obj.R_operadores.DBCol.FreeSale) = cfr.CFRConst.genericOperator.freeSale Then
                        .Add(New ListItem("para venta libre o particular", drv(ind.bk.obj.R_operadores.DBCol.cod_operador)))
                        ventalibrecode = drv(ind.bk.obj.R_operadores.DBCol.cod_operador)
                    Else
                        .Add(New ListItem(drv(ind.bk.obj.R_operadores.DBCol.Operador), drv(ind.bk.obj.R_operadores.DBCol.cod_operador)))
                    End If

                End If
            Next
        End With
        Me.cbooperatorsymclose.SelectedValue = "-2"

        Me.dvRooms = Me.getDvRoomsAll
        With Me.lstrooms.Items
            .Add(New ListItem("Aplicar a todas las unidades", "-1"))

            For Each drv As DataRowView In Me.dvRooms
                .Add(New ListItem(drv(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel), drv(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel)))
            Next
        End With
        Me.lstrooms.SelectedValue = "-1"
    End Sub


    Public Function getDvListPeriodos(Optional onlyDataview As Boolean = False) As DataView
        Try
            Dim tableRes As New cfr.YieldMngClosedInventory

            Dim dt As DataTable = tableRes.getDtClosedDaysByCod_hotel(Me.codhotel)
            Me.dvPeriods = dt.DefaultView
            If onlyDataview = True Then
                Return dt.DefaultView
            End If

            Dim lst As New List(Of periodUI)
            For Each drv As DataRowView In Me.dvPeriods
                Dim codoperador As String = drv(ind.bk.obj.R_YieldMng_Cierre.DBCol.cod_operador)
                Dim operador As String
                If codoperador = -2 Then
                    operador = "Todos los operadores"
                Else
                    Dim dv As New DataView(Me.dvClients.Table)
                    dv.RowFilter = ind.bk.obj.R_YieldMng_Cierre.DBCol.cod_operador & "=" & codoperador
                    operador = dv(0)(ind.bk.obj.R_operadores.DBCol.Operador)
                End If

                Dim unitID As String = drv(ind.bk.obj.R_YieldMng_Cierre.DBCol.Cod_habitacion)
                Dim unityName As String
                If unitID = -1 Then
                    unityName = "Todas las unidades"

                Else
                    Dim dv As New DataView(Me.dvRooms.Table)
                    dv.RowFilter = ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel & "=" & unitID
                    unityName = dv(0)(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel)
                End If

                lst.Add(New periodUI() With {
                        .id = CInt(drv(ind.bk.obj.R_YieldMng_Cierre.DBCol.id_YieldMNG)),
                        .fechainicio = drv(ind.bk.obj.R_Temporadas_Periodos.DBCol.fechainicio),
                        .fechafin = drv(ind.bk.obj.R_Temporadas_Periodos.DBCol.fechafin),
                        .operador = operador,
                        .unitDescription = unityName
                 })
            Next

            Me.ListView1.DataSource = lst
            Me.ListView1.DataBind()
            Return Me.dvPeriodsVar
        Catch ex As Exception
            Throw New Exception("Error configuring periods for : " & ex.Message, ex)
        End Try

    End Function

    Friend Class periodUI
        Default Public ReadOnly Property idindex(ByVal index As Integer) As Integer
            Get
                Return index
            End Get
        End Property
        Public Property fechainicio As Date
        Public Property fechafin As Date
        Public Property id As Integer
        Public Property codoperador As Integer
        Public Property operador As String
        Public Property unitID As Integer
        Public Property unitDescription As String
    End Class


#End Region

    Protected Sub btnsubmit_Click(sender As Object, e As EventArgs) Handles btnsubmit.Click
        Try
            'save
            If Page.IsValid = False Then
                Exit Sub
            End If

            Dim datein As Date = DateTime.ParseExact(Me.checkindate.Text, "dd/MM/yyyy", Nothing)
            Dim dateOut As Date = DateTime.ParseExact(Me.checkoutdate.Text, "dd/MM/yyyy", Nothing)
            If dateOut < datein Then
                cfr.pages.SiteBasePage.showMessage("La fecha final no puede ser inferior a la inicial", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            ElseIf datein < Date.Now.Date Then
                cfr.pages.SiteBasePage.showMessage("La fecha de inicio no puede ser inferior a la actual", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If

            Dim operMng As New cfr.YieldMngClosedInventory

            Dim obj2 As New ind.bk.obj.R_YieldMng_Cierre
            obj2.cod_hotel = Me.codhotel
            obj2.fechainicio = datein
            obj2.fechafin = dateOut
            obj2.Cod_habitacion = Me.lstrooms.SelectedValue
            obj2.cod_operador = Me.cbooperatorsymclose.SelectedValue
            If obj2.Cod_habitacion <> -1 Then
                AdminTenantSecurity.RequireRoom(Me.codhotel, obj2.Cod_habitacion)
            End If
            If obj2.cod_operador <> -2 Then
                AdminTenantSecurity.RequireOperator(Me.codhotel, obj2.cod_operador)
            End If
            operMng.insert(obj2)
          




            Me.litmsgresult.Text = UIMsg.UIMessageDismiss("Periodo creado", CFRConst.msgEnum.Ok)
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        Finally
            Me.dvClients = getDvClients()
            getDvListPeriodos()
            saveSeasonScript()
        End Try
    End Sub




    Private Sub saveSeasonScript()

        Dim jsFunction As String = "" ' String.Format("setnewrelations({0},'seasonlist');", objID)
        Dim sCal As String = Me.showCalendar()

        Dim jsFunctionCal As String = String.Format("$(""#seasoncal_{0}"").html("""");", objID)
        jsFunctionCal += String.Format("$(""#seasoncal_{0}"").html(""{1}"");", objID, sCal)

        UIMsg.registerScript(Me, jsFunction & jsFunctionCal, "asignscript")
    End Sub

    Friend Function showCalendar() As String
        Dim cui As New CalendarUI
        cui.ConfigureN_Month(11)
        cui.fillCalendarByRange(Me.dvPeriods)
        Return cui.getCalendarUIHtml
    End Function





    Protected Sub ListView1_ItemDeleting(sender As Object, e As ListViewDeleteEventArgs) Handles ListView1.ItemDeleting

        Try
            Dim lnkbtn As LinkButton = CType((ListView1.Items(e.ItemIndex).FindControl("lnkitem2delete")), LinkButton)
            AdminTenantSecurity.RequireYieldClosure(Me.codhotel, CInt(lnkbtn.CommandArgument))
            Dim tableRes As New cfr.YieldMngClosedInventory
            tableRes.Delete(lnkbtn.CommandArgument)
            Me.getDvListPeriodos()
            saveSeasonScript()
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

        Dim ocon As SqlClient.SqlConnection
        Dim trans As SqlClient.SqlTransaction = Nothing

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
