Imports System.Data
Imports System.Diagnostics

Partial Class Admin_tariffOperatorMatrix
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P15_Tarifas_Crear_Editar_Borrar

    Public dvOperatorsVar As DataView
    Public Property dvOperators As DataView
        Get
            If Me.dvOperatorsVar Is Nothing Then
                Dim dv As New DataView(Me.getDvClients.Table)
                dv.RowFilter = " freesale<>0 "
                Me.dvOperatorsVar = dv
            End If
            Return Me.dvOperatorsVar

        End Get
        Set(value As DataView)
            Me.dvOperatorsVar = value
        End Set
    End Property

    Public dvTariffVar As DataView
    Public Property dvTariff As DataView
        Get
            If Me.dvTariffVar Is Nothing Then
                Dim dv As New DataView(Me.getDvTariffByHotel.Table)
                Me.dvTariffVar = dv
            End If
            Return Me.dvTariffVar

        End Get
        Set(value As DataView)
            Me.dvTariffVar = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.tariff)

            If Not Page.IsPostBack Then

            Else

            End If
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

    End Sub


    Public Function getTariffTempMatrix() As SortedList
        Try
            Dim numTemp As Integer = Me.dvOperators.Count
            Dim alTemp As New ArrayList
            For Each drv As DataRowView In Me.dvOperators
                alTemp.Add(CInt(drv(ind.bk.obj.R_operadores.DBCol.cod_operador)))
            Next

            Dim numTariff As Integer = Me.dvTariff.Count


            Dim tariffAl As New Hashtable    'result
            For Each drv As DataRowView In Me.dvTariffVar
                Dim tar As mattt = New mattt(drv(ind.bk.obj.R_tarifas.DBCol.cod_tarifa), alTemp)
                tar.tarifa = drv(ind.bk.obj.R_tarifas.DBCol.tarifa)
                tariffAl.Add(tar.cod_tarifa, tar)
            Next


            Dim dtTariffOperator As DataTable = ind.bk.admin.AdminTariffManager.getdtTariffOperatorMatrix(Me.codhotel, 1)





            For Each drvList As DataRowView In dtTariffOperator.DefaultView
                Dim tar As mattt = tariffAl(drvList(ind.bk.obj.R_tarifas.DBCol.cod_tarifa))
                'If tar Is Nothing Then
                '    tar = New mattt(drvList(ind.bk.obj.R_tarifas.DBCol.cod_tarifa), alTemp)
                '    tar.tarifa = drvList(ind.bk.obj.R_tarifas.DBCol.tarifa)
                '    tariffAl.Add(tar.cod_tarifa, tar)
                'End If

                'set assigned
                Dim objid As Integer = drvList(ind.bk.obj.R_operadores.DBCol.cod_operador)
                For x As Integer = 0 To tar.operadorList.Count - 1
                    Dim kv As mattt.tariffOperadorcls = tar.operadorList(x)
                    If kv.cod_operador = objid Then
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
                        Throw New InvalidOperationException("Invalid tariff-customer relation.")
                    End If
                    AdminTenantSecurity.RequireTariff(Me.codhotel, CInt(matcodes(0)))
                    AdminTenantSecurity.RequireOperator(Me.codhotel, CInt(matcodes(1)))
                Next
            End If

            Dim tabMng As New ind.bk.obj.R_tarifas_Manager
            ocon = ind.bk.DBMgr.getNewConn
            trans = ocon.BeginTransaction


            If mat Is Nothing Then
                'all relations where deleted
                Dim oper As New ind.bk.obj.R_tarifas_Manager
                oper.deleteTariff_AllOperatorByCod_hotel(Me.codhotel)
            Else

                Dim vectorOrigin As New ArrayList
                Dim vectorModified As New ArrayList

                'get originals
                Dim dtTariffOperator As DataTable = ind.bk.admin.AdminTariffManager.getdtTariffOperatorMatrix(Me.codhotel, 1)
                For Each dr As DataRow In dtTariffOperator.Rows
                    Dim vp As New CompareVectorMatrix.VectorPar(dr(ind.bk.obj.R_tarifas.DBCol.cod_tarifa), dr(ind.bk.obj.R_operadores.DBCol.cod_operador))
                    vectorOrigin.Add(vp)
                Next

                'get actual changes

                For Each relCode As String In mat
                    Dim matcodes() As String = relCode.Split("_")
                    Dim vp As New CompareVectorMatrix.VectorPar(CInt(matcodes(0)), CInt(matcodes(1)))
                    vectorModified.Add(vp)
                Next

                Dim compareMatrix As New CompareVectorMatrix(vectorOrigin, vectorModified)
                compareMatrix.compare()

                Dim oper As New ind.bk.obj.R_Tarifa_operador_Manager
                'new relations
                For Each vp As CompareVectorMatrix.VectorPar In compareMatrix.newItems
                    Dim objto As New ind.bk.obj.R_Tarifa_operador
                    objto.cod_tarifa = vp.elem1
                    objto.cod_operador = vp.elem2
                    oper.insert(objto, trans)
                Next
                'deleted items
                For Each vp As CompareVectorMatrix.VectorPar In compareMatrix.deletedItems
                    Dim objto As New ind.bk.obj.R_Tarifa_operador
                    objto.cod_tarifa = vp.elem1
                    objto.cod_operador = vp.elem2
                    Const relationDeleteSql As String =
                        "DELETE FROM R_Tarifa_operador WHERE COD_TARIFA = @tariffId AND COD_OPERADOR = @operatorId"
                    ind.bk.DBMgr.Execute(
                        relationDeleteSql,
                        trans,
                        New SqlClient.SqlParameter("@tariffId", SqlDbType.Int) With {.Value = objto.cod_tarifa},
                        New SqlClient.SqlParameter("@operatorId", SqlDbType.Int) With {.Value = objto.cod_operador})
                Next

            End If

            trans.Commit()

            Me.litResRelationsSaved.Text = UIMsg.UIMessageDismiss("La asignación de tarifas a operadores se ha guardado.", CFRConst.msgEnum.Ok)
        Catch ex As Exception
            If Not trans Is Nothing Then
                trans.Rollback()
            End If
            Me.litResRelationsSaved.Text = UIMsg.UIMessageDismiss("Se ha producido un error: " & ex.Message, CFRConst.msgEnum.errorType)
        Finally
            ind.bk.DBMgr.closeConn(ocon)
        End Try
    End Sub


    Public Class CompareVectorMatrix
        Public vectorOrigin As New ArrayList
        Public vectorModified As New ArrayList

        Public deletedItems As New ArrayList
        Public newItems As New ArrayList

        Public Class VectorPar
            Public elem1 As Integer
            Public elem2 As Integer
            Public Sub New(elem1 As Integer, elem2 As Integer)
                Me.elem1 = elem1
                Me.elem2 = elem2
            End Sub
        End Class

        Public Sub New(vectorOriginal As ArrayList, vectorModified As ArrayList)
            Me.vectorOrigin = vectorOriginal
            Me.vectorModified = vectorModified
        End Sub

        Public Sub compare()
            Me.checkDeleted()
            Me.checkNew()
        End Sub

        Public Sub checkDeleted()
            'check wich exists
            For Each vporig As VectorPar In Me.vectorOrigin
                Dim originalExistInModified As Boolean = False
                For Each vpMod As VectorPar In Me.vectorModified
                    If vporig.elem1 = vpMod.elem1 And vporig.elem2 = vpMod.elem2 Then
                        'original exist in modified
                        originalExistInModified = True
                        Continue For
                    End If
                Next
                If originalExistInModified = False Then
                    Me.deletedItems.Add(vporig)
                End If
            Next


        End Sub

        Public Sub checkNew()
            'check wich exists
            For Each vpMod As VectorPar In Me.vectorModified
                Dim modifiedExistInOrigin As Boolean = False
                For Each vpOrig As VectorPar In Me.vectorOrigin
                    If vpMod.elem1 = vpOrig.elem1 And vpMod.elem2 = vpOrig.elem2 Then
                        'modified exist in original
                        modifiedExistInOrigin = True
                        Continue For
                    End If
                Next
                If modifiedExistInOrigin = False Then
                    Me.newItems.Add(vpMod)
                End If
            Next
        End Sub
    End Class


    Friend Class mattt
        Public cod_tarifa As Integer
        Public tarifa As String
        Public operadorList As New ArrayList

        Public Sub New(cod_tarifa As Integer, alOperators As ArrayList)
            Me.cod_tarifa = cod_tarifa
            For Each el As Integer In alOperators
                Dim kv As New tariffOperadorcls(el)
                operadorList.Add(kv)
            Next
        End Sub

        Friend Class tariffOperadorcls
            Public cod_operador As Integer
            Public value As Integer = 0
            Public Sub New(cod_operador As Integer)
                Me.cod_operador = cod_operador
            End Sub
        End Class

    End Class

End Class
