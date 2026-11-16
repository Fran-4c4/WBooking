Imports System.Data
Imports System.Linq
Imports Newtonsoft.Json

Partial Class policyCancelTariff
    Inherits AdminBasePage
    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P12_Configurar_Sistema_Reservas

    Public policyList As New System.Collections.Generic.List(Of R_PolicyCancel)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.bookConfiguration)

        Try
            Me.showMessageClear()
            If Not Page.IsPostBack Then
                Me.configCbos()
            Else

            End If
            

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub

    Private Sub configCbos()
        

    End Sub


    Protected Sub btnsavepolicycondition_Click(sender As Object, e As EventArgs) Handles btnsavepolicycondition.Click
        Dim ctx As cfrbookEntities
        Try
        
            Dim obj As New R_PolicyCancel
            ctx = New cfrbookEntities
            Dim policyCode As String = Me.hdfcancelconditioncode.Value
            If policyCode = 0 Then
                obj.cod_hotel = Me.hotel.id
                ctx.R_PolicyCancel.Add(obj)
            Else
                AdminTenantSecurity.RequireCancellationPolicy(Me.codhotel, CInt(policyCode))
                obj = ctx.R_PolicyCancel.Find(CInt(policyCode))
            End If

            obj.policyName = Me.txtconditionname.Text
            obj.chargeuntilday = Me.cbocanceluntilday.SelectedValue
            obj.chargeunitldaypercent = Me.cbocanceluntilvalue.SelectedValue
            obj.chargeafterdaypercent = Me.cbocancelafterdayvalue.SelectedValue
            obj.datemodified = Date.Now

            ctx.SaveChanges()
            Me.pnlcancelconditionedition.Attributes("display") = "none"

            cfr.pages.SiteBasePage.showMessage("La politica de cancelación se ha guardado correctamente.", Me.Page, CFRConst.msgEnum.Ok, True)


        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        Finally

            If Not ctx Is Nothing Then
                ctx.Dispose()
            End If
        End Try
    End Sub


    Public Function getCancelPolicyMessage(cancelPolicyObj As R_PolicyCancel) As String
        
        Dim msg1 As String = "Si cancelas <strong>{mycanceluntil}</strong> pagarás un <strong>{myporcentage}</strong>"
        Dim msg2 As String = " , o <strong>{myporcentage1}</strong> si cancelas despues."
        Dim msg3 As String = "<strong>{mycanceluntil}</strong> la cancelación es gratuita."
        Dim msg4 As String = " sino pagarás <strong>{myporcentage1}</strong> si cancelas despues."
        Dim msgcond As String = ""

        Me.loadConditionsMessages()

        Dim valdayuntiltxt As String = Me.htuntildayTxt(cancelPolicyObj.chargeuntilday)
        Dim cbocanceluntiltxt As String = Me.htuntildayValueTxt(cancelPolicyObj.chargeunitldaypercent.ToString)
        Dim valdayaftertxt As String = Me.htAfterdayValueTxt(cancelPolicyObj.chargeafterdaypercent.ToString)

        If (cancelPolicyObj.chargeuntilday = "0") Then
            msgcond = msg3 & msg4
            msgcond = msgcond.Replace("{mycanceluntil}", valdayuntiltxt)
            msgcond = msgcond.Replace("{myporcentage1}", valdayaftertxt)
        Else
            If cancelPolicyObj.chargeunitldaypercent = 0 Then 'cancelacion gratuita
                msgcond = msg3 & msg4
            Else
                msgcond = msg1 & msg2
            End If
            msgcond = msgcond.Replace("{myporcentage}", cbocanceluntiltxt)
            msgcond = msgcond.Replace("{mycanceluntil}", valdayuntiltxt)
            msgcond = msgcond.Replace("{myporcentage1}", valdayaftertxt)
        End If
        
        Return msgcond

    End Function

 
    Public Function getConditionsList() As System.Collections.Generic.List(Of R_PolicyCancel)
        Using ctx As New cfrbookEntities
            Dim query = (From c In ctx.R_PolicyCancel
                             Where c.cod_hotel = Me.hotel.id)


            Return query.ToList()
        End Using

    End Function

#Region "Conditions messages string to show in UI "
    Dim htuntildayTxt As New Hashtable
    Dim htuntildayValueTxt As New Hashtable
    Dim htAfterdayValueTxt As New Hashtable

    Dim messagesArrayLoaded As Boolean = False
    Public Sub loadConditionsMessages()
        If Me.messagesArrayLoaded = True Then
            Exit Sub
        End If
        config_htuntildaytxt(htuntildayTxt)
        config_htuntildayValueTxt(htuntildayValueTxt)
        config_htAfterdayValueTxt(htAfterdayValueTxt)
        Me.messagesArrayLoaded = True
    End Sub
    
    Public Sub config_htuntildaytxt(ht As Hashtable)
        ht.Add("14h", "hasta las 14:00 del día de llegada")
        ht.Add("18h", "hasta las 18:00 del día de llegada")
        ht.Add("1d", "hasta 1 día antes de la fecha de llegada")
        ht.Add("2d", "hasta 2 días antes de la fecha de llegada")
        ht.Add("3d", "hasta 3 días antes de la fecha de llegada")
        ht.Add("4d", "hasta 4 días antes de la fecha de llegada")
        ht.Add("5d", "hasta 5 días antes de la fecha de llegada")
        ht.Add("6d", "hasta 6 días antes de la fecha de llegada")
        ht.Add("7d", "hasta 7 días antes de la fecha de llegada")
        ht.Add("8d", "hasta 8 días antes de la fecha de llegada")
        ht.Add("9d", "hasta 9 días antes de la fecha de llegada")
        ht.Add("10d", "hasta 10 días antes de la fecha de llegada")
        ht.Add("14d", "hasta 14 días antes de la fecha de llegada")
        ht.Add("30d", "hasta 30 días antes de la fecha de llegada")
        ht.Add("42d", "hasta 42 días antes de la fecha de llegada")
        ht.Add("60d", "hasta 60 días antes de la fecha de llegada")
        ht.Add("90d", "hasta 90 días antes de la fecha de llegada")
        ht.Add("120d", "hasta 120 días antes de la fecha de llegada")
        ht.Add("150d", "hasta 150 días antes de la fecha de llegada")
        ht.Add("180d", "hasta 180 días antes de la fecha de llegada")
    End Sub

    Public Sub config_htuntildayValueTxt(ht As Hashtable)
        ht.Add("0", "cancelación gratuita")
        ht.Add("1", "la primera noche")
        ht.Add("30", "un 30% del precio total")
        ht.Add("50", "un 50% del precio total")
        ht.Add("70", "un 70% del precio total")
        ht.Add("90", "un 90% del precio total")
        ht.Add("100", "un 100% del precio total")
    End Sub

    Public Sub config_htAfterdayValueTxt(ht As Hashtable)
        ht.Add("1", "la primera noche")
        ht.Add("100", "un 100% del precio total")
        ht.Add("30", "un 30% del precio total")
        ht.Add("50", "un 50% del precio total")
        ht.Add("70", "un 70% del precio total")
        ht.Add("90", "un 90% del precio total")
    End Sub



#End Region

    'Public Function getPolicyCancelDataJson() As String
    Public Function getPolicyCancelDataJson(cancelPolicyObj As R_PolicyCancel) As String

        Dim jsdata As String = ""

        jsdata = ""

        Dim cop As New jsCancelCondData
        cop.id = cancelPolicyObj.policyID
        cop.untilDay = cancelPolicyObj.chargeuntilday
        cop.untilValue = cancelPolicyObj.chargeunitldaypercent
        cop.afterValue = cancelPolicyObj.chargeafterdaypercent

        jsdata = JsonConvert.SerializeObject(cop)

  
        Return jsdata

    End Function

    Public Class jsCancelCondData
        Public id As String
        Public untilDay As String
        Public untilValue As String
        Public afterValue As String

    End Class

    Protected Sub btndeleteitem_Click(sender As Object, e As EventArgs) Handles btndeleteitem.Click
        Dim ctx As New cfrbookEntities
        Try

            Dim obj As New R_PolicyCancel
            ctx = New cfrbookEntities
            Dim policyCode As String = Me.hdfdeleteitemid.Value
            AdminTenantSecurity.RequireCancellationPolicy(Me.codhotel, CInt(policyCode))
            obj = ctx.R_PolicyCancel.Find(CInt(policyCode))

            ctx.R_PolicyCancel.Remove(obj)
            ctx.SaveChanges()
            
            cfr.pages.SiteBasePage.showMessage("La condicion se ha eliminado", Me.Page, CFRConst.msgEnum.info)


        Catch ex As Exception

            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        Finally

        End Try
    End Sub

 
End Class
