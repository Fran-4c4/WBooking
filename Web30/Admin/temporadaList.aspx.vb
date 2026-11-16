Imports System.Data
Imports System.Diagnostics

Partial Class Admin_temporadaList
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P15_Tarifas_Crear_Editar_Borrar

    Public dvTemporadasVar As DataView
    Public Property dvTemporadas As DataView
        Get
            If Me.dvTemporadasVar Is Nothing Then
                Return Me.getDvTemporadas
            Else
                Return Me.dvTemporadasVar
            End If
        End Get
        Set(value As DataView)
            Me.dvTemporadasVar = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.tariff)

            If Not Page.IsPostBack Then
                Me.getDvTemporadas()
                Me.litTemporadasContainer.Text = Me.showCalendar
                Dim ucal As New CalendarUI
                Me.litcsscal.Text = ucal.getColorCSS
            Else

            End If
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Sub

    Private Function getDvTemporadas() As DataView
        Dim tableRes As New ind.bk.obj.R_Temporadas_Manager

        Dim dv As DataView = tableRes.getTemporadasByCodHotel(Me.codhotel).DefaultView
        dv.Sort = ind.bk.obj.R_Temporadas.DBCol.Temporada
        Me.dvTemporadas = dv
        Return dv
    End Function

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDeleteItem.Click
        Dim field_itemobjectDec As String
        Dim field_itemobjectDecURL As String
        Try
            field_itemobjectDec = Me.field_itemobject.Value
            field_itemobjectDecURL = UIHelper.URLDeCryptData(field_itemobjectDec)
            AdminTenantSecurity.RequireSeason(Me.codhotel, CInt(field_itemobjectDecURL))

            Dim tableRes As New ind.bk.obj.R_Temporadas_Manager
            tableRes.Delete(field_itemobjectDecURL)
            ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.delete, "Temporada:" & field_itemobjectDec)
            cfr.pages.SiteBasePage.showMessage("La temporada ha sido borrada.", Me.Page, CFRConst.msgEnum.errorType)
            getDvTemporadas()
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            
            Dim field_operationVal As String = Me.field_operation.Value
            
            If field_operationVal = "changename" Then

                Dim field_itemobjectDec As String = Me.field_itemobject.Value
                field_itemobjectDec = UIHelper.URLDeCryptData(field_itemobjectDec)
                AdminTenantSecurity.RequireSeason(Me.codhotel, CInt(field_itemobjectDec))

                If Me.field_itemname.Value.Trim = "" Then
                    cfr.pages.SiteBasePage.showMessage("Debe introducir un nombre valido", Me.Page, CFRConst.msgEnum.errorType)
                    Exit Sub
                End If

                Dim tableRes As New ind.bk.obj.R_Temporadas_Manager
                Dim obj2 As New ind.bk.obj.R_Temporadas
                obj2.cod_hotel = Me.codhotel
                obj2.cod_Temporada = field_itemobjectDec
                obj2.Temporada = Me.field_itemname.Value.Trim
                tableRes.Update(obj2)
                Me.getDvTemporadas() 'refresh

                ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.update, "Temporada:" & obj2.cod_Temporada)

                Me.litresmessageedit.Text = UIMsg.UIMessageDismiss("El nombre de la temporada ha sido cambiado.", CFRConst.msgEnum.Ok)


            ElseIf field_operationVal = "newitem" Then
                If Me.field_itemname.Value.Trim = "" Then
                    cfr.pages.SiteBasePage.showMessage("Debe introducir un nombre valido", Me.Page, CFRConst.msgEnum.errorType)
                    Exit Sub
                End If
                Dim tableRes As New ind.bk.obj.R_Temporadas_Manager
                Dim obj2 As New ind.bk.obj.R_Temporadas
                obj2.cod_hotel = Me.codhotel
                obj2.Temporada = Me.field_itemname.Value.Trim
                tableRes.insert(obj2)
                Me.getDvTemporadas() 'refresh
                ind.bk.obj.LogOperationsMgr.LogOper(Me.sessionCnt.user.id_usuario, ind.bk.obj.LogOperationsMgr.actionCte.insert, "Temporada:" & obj2.cod_Temporada)
                Me.litresmessageedit.Text = UIMsg.UIMessageDismiss("La temporada ha sido creada, ahora pulse en asignar fechas.", CFRConst.msgEnum.Ok)
                Exit Sub
            End If


        Catch ex As Exception
            Me.litresmessageedit.Text = UIMsg.UIMessageDismiss(ex.Message, CFRConst.msgEnum.errorType)
        End Try

    End Sub

#Region "Calendar"

    Friend Function showCalendar() As String
        Dim uical As New CalendarUI
        uical.ConfigureN_Month(23, 0)

        For Each dr As DataRowView In dvTemporadas
            Dim cod_temporada As Integer = dr(ind.bk.obj.R_Temporadas.DBCol.cod_Temporada)
            uical.fillCalendarByTemporada(cod_temporada, True)
        Next

        Return uical.getCalendarUI(uical.calendarDaysObj)

    End Function



    Public Function showCalendarForTemporada(cod_temporada As Integer) As String
        Dim dtCalendarResult As DataTable = Me.calendarForTemporada(cod_temporada)
        Return Me.getCalendarByDatatable(dtCalendarResult)
    End Function

    Private Function calendarForTemporada(cod_temporada As Integer) As DataTable
        AdminTenantSecurity.RequireSeason(Me.codhotel, cod_temporada)
        Dim tableRes As New ind.bk.obj.R_Temporadas_Manager

        Dim rsPeriodos As DataView = tableRes.getPeriodosByIDTemporada(cod_temporada).DefaultView

        Dim rsCalen As DataTable

        rsCalen = Display12meses(11, 0)

        Dim CalendarioTemporadas As New DataTable

        For Each dr As DataRow In rsCalen.Rows
            'Debug.Print rsCalen.Fields("dia")
            Dim strFilter As String = "fechainicio<=#" & dr("diaINGLES") & "# AND fechafin>=#" & dr("diaINGLES") & "#"
            rsPeriodos.RowFilter = strFilter
            If rsPeriodos.Count > 0 Then
                dr.BeginEdit()
                dr("Periodo") = "1"
                dr.EndEdit()
                dr.AcceptChanges()
            End If
        Next

        'For Each dr As DataRow In rsCalen.Rows
        '    Debug.WriteLine("dia: " & dr("dia") & "periodo= " & dr("Periodo"))
        'Next

        Return rsCalen

    End Function

    Private Function Display12meses(Optional iMeses As Integer = 0, Optional iDesdeMes As Integer = 0) As DataTable

        Dim vDATE As Date
        Dim orDT As New DataTable
        Try
            orDT.Columns.Add(New DataColumn("numdia", GetType(Integer)))
            orDT.Columns.Add("dia")
            orDT.Columns.Add("diaINGLES")
            orDT.Columns.Add("diasemana")
            orDT.Columns.Add("iDia", GetType(Integer))
            orDT.Columns.Add("Periodo")

            If iDesdeMes = 0 Then 'DESDE EL MES ACTUAL
                iDesdeMes = Month(Now)
            End If

            If iMeses = 0 Then
                iMeses = 12
            End If

            Dim DiaFinal As Date, strDia As String
            DiaFinal = DateSerial(Year(Now), Month(Now) + iMeses + 1, 1) 'un mes mas para obtener luego el ultimo dia del mes al restar
            DiaFinal = DiaFinal.AddDays(-1)
            vDATE = DateSerial(Year(Now), Month(Now), 1)

            While vDATE <= DiaFinal
                Dim drNew As DataRow = orDT.NewRow()
                drNew("numdia") = Day(vDATE)
                drNew("dia") = vDATE.ToString("dd/MM/yyyy")
                drNew("diaINGLES") = vDATE.ToString("yyyy/MM/dd")
                drNew("iDia") = DatePart("w", vDATE, vbMonday)
                strDia = WeekdayName(DatePart("w", vDATE), True, vbMonday)
                strDia = UCase$(Left$(strDia, 1)) & Mid$(strDia, 2)
                drNew("diasemana") = strDia
                orDT.Rows.Add(drNew)
                vDATE = vDATE.AddDays(1)
            End While

            Return orDT
        Catch ex As Exception
            Throw New Exception("Error configuring calendar")
        End Try

    End Function


    Private Function getCalendarByDatatable(ordt As DataTable) As String


        Dim resCab As String = "<div class=' cal-container'><TABLE class=cal-month>"
        resCab += "<TR>"
        resCab += "<TD colspan=7 class='cal-monthname' align=center>{Monthname}&nbsp;{yeartoset}</TD>"
        resCab += "</TR>"
        resCab += "<TR>"
        resCab += "<TH >Lu</TH>"
        resCab += "<TH >Ma</TH>"
        resCab += "<TH >Mi</TH>"
        resCab += "<TH >Ju</TH>"
        resCab += "<TH >Vi</TH>"
        resCab += "<Th class='cal-weekend'>Sa</Th>"
        resCab += "<Th class='cal-weekend'>Do</Th>"
        resCab += "</TR>"
        'resCab += "<TR>"

        Dim res As String
        Dim rowIsclosed As Boolean = False
        Dim monthActual As String = ""
        Dim firstMonth As Boolean = True
        Dim idia As Integer
        For Each dr As DataRow In ordt.Rows
            monthActual = getMonthName(dr("dia"))
            idia = dr("iDIA")
            Dim numDia As Integer = dr("numdia")
            Dim selDate As Date = dr("dia")

            If rowIsclosed = False Then

            End If
            If numDia = 1 Then 'new month
                'close previous month
                'check if closed month
                If firstMonth = False Then
                    If idia > 1 Then
                        For x As Integer = idia To 7 'añadimos los dias extras hasta comenzar el mes
                            res += "<TD>&nbsp;</TD>"
                        Next
                    End If

                    res += "</TR>"
                    'close month
                    res += "</table>"
                    res += "</div>"
                Else
                    firstMonth = False
                End If

                'open new month
                Dim newHead As String = resCab.Replace("{Monthname}", getMonthName(selDate))
                newHead = newHead.Replace("{yeartoset}", Year(selDate))
                res += newHead
                res += "<TR>"
                rowIsclosed = False
                For x As Integer = 1 To idia - 1 'añadimos los dias extras hasta comenzar el mes
                    res += "<TD>&nbsp;</TD>"
                Next
            ElseIf idia = 1 And numDia <> 1 Then 'nueva fila
                res += "</TR>"
                res += "<TR>"

            End If



            Dim vclass As String
            Dim periodIs As String = cfr.tool.Tools.cNullStr(dr("periodo"), "0")

            If periodIs = "1" Then
                If idia = 6 Or idia = 7 Then 'SI ES fin de semana
                    vclass = " class='cal-sel-weekend' "
                Else
                    vclass = " class='cal-sel' "
                End If
            Else
                If idia = 6 Or idia = 7 Then 'It´s weekend
                    vclass = " class='cal-weekend'"
                Else
                    vclass = ""
                End If
            End If

            res += "<TD " & vclass & ">" & Day(selDate) & "</TD>"
            If idia = 7 Then 'last day in weekend? Then new line
                rowIsclosed = True
            End If


        Next
        'for the las month it´s not closed
        If idia > 1 And idia <= 7 Then
            For x As Integer = idia + 1 To 7 'añadimos los dias extras hasta comenzar el mes
                res += "<TD>&nbsp;</TD>"
            Next
        End If
        res += "</TR>"
        res += "</TABLE>"
        res += "</div>"

        Return res
    End Function


    Public Function getMonthName(day2 As Date) As String
        Select Case Month(day2)
            Case 1
                Return "Enero"
            Case 2
                Return "Febrero"
            Case 3
                Return "Marzo"
            Case 4
                Return "Abril"
            Case 5
                Return "Mayo"
            Case 6
                Return "Junio"
            Case 7
                Return "Julio"
            Case 8
                Return "Agosto"
            Case 9
                Return "Septiembre"
            Case 10
                Return "Octubre"
            Case 11
                Return "Noviembre"
            Case 12
                Return "Diciembre"
        End Select
    End Function

    Private Function ConfigureCalendar(ordt As DataTable)
        Dim res As String = ""
        res += "<TABLE class=b1 BORDER=0 cellspacing=2 cellpadding=3>"
        For xFilaMes As Integer = 1 To 1
            res += "<TR>"
            For yColMes As Integer = 1 To 12

                res += "<TD valign=top class=fff>"
                Dim resMonth As String = getCalendarByDatatable(ordt)
                If resMonth <> "" Then
                    res += resMonth
                Else
                    res += "&nbsp;"
                End If
                res += "</TD>"

            Next
            res += "</TR>"
        Next
        res += "</TABLE>"
        Return res
    End Function

#End Region


    Public Function getTariffTempMatrix() As SortedList
        Try
            Dim numTemp As Integer = Me.dvTemporadas.Count
            Dim alTemp As New ArrayList
            For Each drv As DataRowView In Me.dvTemporadas
                alTemp.Add(CInt(drv(ind.bk.obj.R_Temporadas.DBCol.cod_Temporada)))
            Next


            Dim sql As String = "SELECT     R_tarifas.cod_tarifa, R_tarifas.tarifa, r_Tarifas_Temporadas.cod_Temporada " _
                                & " FROM         R_tarifas INNER JOIN" _
                      & " r_Tarifas_Temporadas ON R_tarifas.cod_tarifa = r_Tarifas_Temporadas.cod_tarifa INNER JOIN " _
                      & " r_Temporadas On r_Tarifas_Temporadas.cod_Temporada = r_Temporadas.cod_Temporada " _
                      & " WHERE     (R_tarifas.cod_hotel = @hotelId) order by tarifa ,cod_Temporada "

            Dim dtTariffTemporadas As DataTable = ind.bk.DBMgr.getDT(
                sql,
                New SqlClient.SqlParameter("@hotelId", SqlDbType.Int) With {.Value = Me.codhotel})

            Dim tariffAl As New Hashtable    'result

            For Each drvList As DataRowView In dtTariffTemporadas.DefaultView
                Dim tar As mattt = tariffAl(drvList(ind.bk.obj.R_tarifas.DBCol.cod_tarifa))
                If tar Is Nothing Then
                    tar = New mattt(drvList(ind.bk.obj.R_tarifas.DBCol.cod_tarifa), alTemp)
                    tar.tarifa = drvList(ind.bk.obj.R_tarifas.DBCol.tarifa)
                    tariffAl.Add(tar.cod_tarifa, tar)
                End If

                'set assigned seasons (temporadas)
                Dim objid As Integer = drvList(ind.bk.obj.R_Temporadas.DBCol.cod_Temporada)
                For x As Integer = 0 To tar.temporadasList.Count - 1
                    Dim kv As mattt.tartempcls = tar.temporadasList(x)
                    If kv.cod_temporada = objid Then
                        kv.value = 1
                        Exit For
                    End If
                Next


            Next


            Dim tariffAlByName As New SortedList   'result
            For Each val As mattt In tariffAl.Values
                tariffAlByName.Add(val.tarifa, val)
            Next

            Return tariffAlByName

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnsaverelations_Click(sender As Object, e As EventArgs) Handles btnsaverelations.Click
        Dim ocon As SqlClient.SqlConnection = Nothing
        Dim trans As SqlClient.SqlTransaction = Nothing
        Me.litResRelationsSaved.Visible = True
        Try

            Dim mat() As String = Page.Request.Form.GetValues("chkreltartemp")
            If Not mat Is Nothing Then
                For Each relCode As String In mat
                    Dim matcodes() As String = relCode.Split("_"c)
                    If matcodes.Length <> 2 Then
                        Throw New InvalidOperationException("Invalid tariff-season relation.")
                    End If
                    AdminTenantSecurity.RequireTariff(Me.codhotel, CInt(matcodes(0)))
                    AdminTenantSecurity.RequireSeason(Me.codhotel, CInt(matcodes(1)))
                Next
            End If

            Dim tabMng As New ind.bk.obj.R_tarifas_Manager
            'abrimos la conexion y comenzamos la reserva
            ocon = ind.bk.DBMgr.getNewConn
            'comienzo de la transaccion
            trans = ocon.BeginTransaction
            tabMng.deleteTariff_AllTemporadasByCod_hotel(Me.codhotel, trans:=trans)

            If Not mat Is Nothing Then

                Dim oper As New ind.bk.obj.R_Tarifas_Temporadas_Manager
                For Each relCode As String In mat
                    Dim objto As New ind.bk.obj.R_Tarifas_Temporadas
                    Dim matcodes() As String = relCode.Split("_")
                    objto.cod_tarifa = CInt(matcodes(0))
                    objto.cod_Temporada = CInt(matcodes(1))
                    oper.insert(objto, trans)
                Next
            End If

            trans.Commit()

            Me.litResRelationsSaved.Text = UIMsg.UIMessageDismiss("La asignación de tarifas a temporadas se ha guardado.", CFRConst.msgEnum.Ok)
        Catch ex As Exception
            trans.Rollback()
            Me.litResRelationsSaved.Text = UIMsg.UIMessageDismiss("Se ha producido un error: " & ex.Message, CFRConst.msgEnum.errorType)
        Finally
            ind.bk.DBMgr.closeConn(ocon)
        End Try
    End Sub

    Friend Class mattt
        Public cod_tarifa As Integer
        Public tarifa As String
        Public temporadasList As New ArrayList
        'Public Sub New(numTemporadas As Integer)
        '    For x As Integer = 1 To numTemporadas
        '        temporadas.Add("-1")
        '    Next
        'End Sub
        Public Sub New(cod_tarifa As Integer, alTemporadas As ArrayList)
            Me.cod_tarifa = cod_tarifa
            For Each el As Integer In alTemporadas
                Dim kv As New tartempcls(el)
                temporadasList.Add(kv)
            Next
        End Sub

        Friend Class tartempcls
            Public cod_temporada As Integer
            Public value As Integer = 0
            Public Sub New(cod_temporada As Integer)
                Me.cod_temporada = cod_temporada
            End Sub
        End Class

    End Class

End Class
