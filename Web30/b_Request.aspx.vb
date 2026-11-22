Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity.Core.Objects
Imports System.Diagnostics
Imports cfr
Imports System.Threading
Imports System.Globalization
Imports System.Linq
Imports System.Data.Entity


''' <summary>
''' booking request page for hotel
''' </summary>
''' <remarks></remarks>
Public Class b_request
    Inherits BookBasePage



#Region "variables to control visibility"
    Public calendarConfig As New calendarObj
    Public Class calendarObj
        Public langSelected As String = "es"
        Public dateFormat As String = "dd/mm/yyyy"
        Public dateIn As String
        Public dateOut As String
        ''' <summary>
        ''' max date for calendar, not for booking
        ''' </summary>
        Public dateMax As String
    End Class


#End Region
    
    Dim bookmgr As BookManager
    Dim cookiename As String = "ckBook_{0}"

    Public Property bookoperatorID As Integer
        Get
            Return CType(ViewState("bookoperatorID"), Integer)
        End Get
        Set(ByVal Value As Integer)
            ViewState("bookoperatorID") = Value
        End Set
    End Property

   
    Protected Sub Page_PreInit1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        Try
            System.Diagnostics.Debug.WriteLine("b_request_Page_PreInit")
            Me.loadBaseData()
            Me.langCaption.holdproperty = Me.holdprop.Tipo
        Catch ex As Exception
            cfr.tool.LogMgr.log(ex)
            Response.Redirect(cfr.NamePage.defaultpage)
        End Try

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            System.Diagnostics.Debug.WriteLine("b_request_Page_load")

            configCookieRegisteredClient()
            Me.cookiename = String.Format(Me.cookiename, Me.holdprop.id)

            If Not Page.IsPostBack Then 'first render
                Me.bookoperatorID = 0
                Me.fillCbos()

                Me.setJavascriptCalendar()
                'Me.setScripts()
                Me.setCaption()
                Me.Page.Title = Me.bookData.site.description

                Me.configUI()

                ' External date fields are the public "search now" contract.
                ' A plain hotelcode/language launch still renders the normal
                ' search page, while a complete external search runs the same
                ' room lookup as btnFindAction after strict validation.
                Dim externalSearchRequested As Boolean = Me.hasExternalSearchCriteria()
                If externalSearchRequested Then
                    Me.applyExternalSearchCriteria()
                End If

                Me.myCFRMasterPage.masterPageLiteral.Text = Me.bookData.site.description

                Dim urlCancel As String = String.Format("javascript:cancelBook('b_cancel.aspx?{0}')", Me.myQuery)
                Dim link As String = String.Format("<a href=""{0}"">{1}</a>", urlCancel, Me.langCaption.GetCaption(353))
                Me.lnkCancelBook.Text = link
                Me.checkStatusSession()
                If externalSearchRequested Then
                    Me.findRoomsByUserSelection()
                End If
                Me.B_ClientData1.fillFormWithBookData(Me.bookData)

                'Dim kk As bookEntities
                'Dim extras As New R_Extras
            

                'show the lpd option
                If cfr.Language.getLanguage = CFRConst.LanguageEnum.es Then
                    Me.pnlLpd.Visible = True
                Else
                    Me.pnlLpd.Visible = False
                End If

                If Me.bookData.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.PrivateClient Then
                    Me.getCookiesClient()
                ElseIf Me.bookData.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.Company Then
                    Me.getCookiesClient()
                    Me.getCookiesCompany()
                ElseIf Me.bookData.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.HotelAgency Then
                    Me.getCookiesClient()
                    Me.getCookiesCompany()
                    Me.getCookiesAgency()
                End If

                If Me.bookData.hotel.Tipo = CFRConst.unitTypeEnum.car Then
                    Me.plhhotel_pax.Visible = False
                End If

            Else

                'second request
                If Me.isNewSession = True Then
                    Me.getSearchCriteriaByPage()
                    'Me.setJavascriptCalendar()
                End If

                Me.configurePromotionCode()

            End If
        Catch ex As Exception

            Me.SesionErrorProp.Add(New BookOperationErrors(ex.Message))
            Me.cancelFlow = True
        End Try


        Dim msgErr As String = Me.controlSesionError(True)
        If msgErr = "" Then
            Me.controlError(boe)
        Else
            Me.showMessage(msgErr)
        End If



    End Sub

    ''' <summary>
    ''' True when a customer site supplied the legacy external-search contract.
    ''' The date fields, rather than hotelcode alone, distinguish a prefilled
    ''' search from a normal iframe/link launch.
    ''' </summary>
    Private Function hasExternalSearchCriteria() As Boolean
        Return Not String.IsNullOrWhiteSpace(Me.getExternalRequestValue(REQCte.forms.dateInDay)) OrElse
            Not String.IsNullOrWhiteSpace(Me.getExternalRequestValue(REQCte.forms.dateInMonth)) OrElse
            Not String.IsNullOrWhiteSpace(Me.getExternalRequestValue(REQCte.forms.dateInYear)) OrElse
            Not String.IsNullOrWhiteSpace(Me.getExternalRequestValue(REQCte.forms.dateOutDay)) OrElse
            Not String.IsNullOrWhiteSpace(Me.getExternalRequestValue(REQCte.forms.dateOutMonth)) OrElse
            Not String.IsNullOrWhiteSpace(Me.getExternalRequestValue(REQCte.forms.dateOutYear))
    End Function

    ''' <summary>POST values take precedence over query-string values.</summary>
    Private Function getExternalRequestValue(ByVal fieldName As String) As String
        Dim value As String = Request.Form(fieldName)
        If String.IsNullOrWhiteSpace(value) Then
            value = Request.QueryString(fieldName)
        End If
        Return value
    End Function

    ''' <summary>
    ''' Validates the external search and transfers it into the normal Web Forms
    ''' controls before Page_Load runs the standard room lookup. Supplying any
    ''' date component opts into this contract; missing, invalid, or
    ''' contradictory fields are rejected rather than replaced with defaults.
    ''' </summary>
    Private Sub applyExternalSearchCriteria()
        Dim requestValues As New System.Collections.Specialized.NameValueCollection
        Dim fields() As String = {
            REQCte.forms.dateInDay, REQCte.forms.dateInMonth, REQCte.forms.dateInYear,
            REQCte.forms.dateOutDay, REQCte.forms.dateOutMonth, REQCte.forms.dateOutYear,
            REQCte.forms.numAdultos, REQCte.forms.ninosPago, REQCte.forms.ninosfree,
            REQCte.forms.regimen
        }
        For Each fieldName As String In fields
            requestValues(fieldName) = Me.getExternalRequestValue(fieldName)
        Next

        Me.getSearchCriteriaByRequest(requestValues)
        If Me.f_regimen.Items.FindByValue(CInt(Me.bookData.regimen).ToString()) Is Nothing Then
            Throw New cfr.exceptions.BookException("The requested board basis is not enabled for this hotel.")
        End If

        Me.checkindate.Text = Me.bookData.dateIn.ToShortDateString()
        Me.checkoutdate.Text = Me.bookData.dateOut.ToShortDateString()
        Me.f_numAdultos.SelectedValue = Me.bookData.numAdultos.ToString()
        Me.f_ninosPago.SelectedValue = Me.bookData.ninospago.ToString()
        Me.f_ninosfree.SelectedValue = Me.bookData.ninosfree.ToString()
        Me.f_regimen.SelectedValue = CInt(Me.bookData.regimen).ToString()
        Me.calendarConfig.dateIn = Me.checkindate.Text
        Me.calendarConfig.dateOut = Me.checkoutdate.Text
        Me.lblDateIn.Text = Me.bookData.dateIn.ToLongDateString()
        Me.lblDateOut.Text = Me.bookData.dateOut.ToLongDateString()
        Me.lblNights.Text = Me.bookData.bookTotalDays.ToString()
        Me.lblnightsCalendar.Text = Me.lblNights.Text
        Me.lblNumAdults.Text = Me.bookData.numAdultos.ToString()
        Me.lblNumChildPay.Text = Me.bookData.ninospago.ToString()
        Me.lblNumChildFree.Text = Me.bookData.ninosfree.ToString()
        Me.saveSearchCriteriaToViewstate(Me.bookData)
    End Sub

    Private Sub configurePromotionCode()
        If Me.txtpromotioncode.Text.Trim <> "" Then
            'Me.txtpromotioncodediv.Attributes.Add("class", "in")
            Me.txtpromotioncodediv.Style.Remove("display")
        Else
            Me.txtpromotioncodediv.Style.Add("display", "none")
        End If
    End Sub



#Region "Views"
    ''' <summary>
    ''' return the html code for the rooms that the user can book
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="bo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getRoomView(ByVal dr As cfr.BookRes, ByVal bo As BookedRoom) As String

        Dim myRoom As cfr.obj.HotelRoomData = bo.roomSelected

        Dim s As New Text.StringBuilder(1500)
        s.AppendFormat("<article class='wb-room-card' id='roomfindedbysearch_{0}'>", myRoom.idEntity)

        'Dim strRoomCapacity As String = bo.roomSelected.MaxAdultos & "<img src='images/pax-1-h.png' class='roompax' />"
        Dim strRoomCapacity As String = bo.PaxIncluidEnPrecio & " <i class='fa fa-male'></i>"
        If bo.PaxIncluidEnPrecio > bo.roomSelected.MaxAdultos Then
            Dim numBoys As Integer = bo.PaxIncluidEnPrecio - bo.roomSelected.MaxAdultos
            'strRoomCapacity += "+" & numBoys & "<img src='images/pax-1-s.png' class='roompax' />"
            strRoomCapacity += "+" & numBoys & "<i class='fa fa-child' /></i>"
        End If

        s.Append(String.Format("<H3 id='roomtitle_{2}' class='wb-room-title'><span class='wb-room-capacity'>{1}</span><span id='roomname_{2}'>{0}</span></H3>", myRoom.description.getDes(dr.language), strRoomCapacity, myRoom.idEntity))

        s.Append("<div class='wb-room-content'>")

        s.Append("<div class='wb-room-media'>")
        s.Append(getImgHtmlForRoomViewCaroussel(myRoom))
        s.Append("</div>")

        s.Append("<div class='wb-room-details'>") 'RIGHT COLUMN
        If myRoom.serviceInfo.getDes().Length > 0 Then
            'room info details
            s.Append("			<div id='ldessup_" & myRoom.idEntity & "'>")
            s.Append("				<div> ")
            s.AppendFormat("			<a class='wb-details-link plusview plusopen' href='#' onclick='return false;' data-toggle='collapse' data-target='#ldes_" & myRoom.idEntity & "' alt=""{1}"">{2}</a>", myRoom.idEntity, Me.langCaption.GetCaption(296), Me.langCaption.GetCaption(308))
            s.Append("				</div> ")
            s.Append("				<div id='ldes_" & myRoom.idEntity & "' class='collapse wb-room-services'>" & myRoom.serviceInfo.getDes(dr.language) & "</div>")
            s.Append("			</div>")
        End If

        s.Append("		<H4 class='wb-rate-name hidden-xs'>" & Me.langCaption.GetCaption(295) & "</H4>")

        If dr.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then
            'pax include in price info
            s.Append("		<div class='wb-included'>")
            s.Append("		  <ul class='icon'>")
            s.Append("		    <li class='hand'>" & String.Format(Me.langCaption.GetCaption(293), bo.PaxIncluidEnPrecio & "<img src='images/pax-1.png' class='roompax' />") & "</li>")
            s.Append(getBoardStringForUI(dr, bo)) 'show board in tariff

            'If myRoom.MaxPersonas > bo.PaxIncluidEnPrecio Then
            '    'message with info about a price with more people
            '    Dim msgBestPriceMorePax As String = "<a href='#' tabindex='0' data-trigger='focus' data-toggle='popover' data-placement='top' title='{0}' data-content='{1}'>{2}</a>"
            '    msgBestPriceMorePax = String.Format(msgBestPriceMorePax, , , String.Format(Me.langCaption.GetCaption(294), myRoom.MaxPersonas))
            '    s.Append("		<li class='info-circle'>" & msgBestPriceMorePax & "</li>")
            'End If

            s.Append("		</ul></div>")
            'end pax include in price info

            'Controla si el regimen ha cambiado y avisa al usuario
            If bo.selectedRegimenChanged = True And dr.hotel.BookParam.regimenCountAvailable > 1 Then
                s.Append("      <div class='selectedRegimenChanged'><i class='glyphicon glyphicon-flag msg-red'></i> " & Me.langCaption.GetCaption(84) & "</div>")
            End If
        End If

        s.Append("</div>") 'end 'RIGHT COLUMN

        s.Append("</div>") 'end 'Row


        If dr.bookTotalDays < bo.minimumDays And bo.applyTariff = CFRConst.tariffValidEnum.NotValid Then
            'show minimum days message
            s.Append("<div class='row'>")
            s.AppendFormat("<div class='bookroomminimumdaysdiv alert alert-warning col-xs-12 col-sm-6 col-pull-sm-6' role='alert'><i class='glyphicon glyphicon-flag msg-red'></i> {0}</div>", String.Format(Me.langCaption.GetCaptionBy(Language.msgEnum.E25_MinimumDays), bo.minimumDays))
            s.Append("</div>") 'end 'Row
        Else
            s.Append("<div class='wb-rate-row'> <div class='wb-rate-footer'>")
            'message info: with more days the tariff is better
            If dr.bookTotalDays < bo.minimumDays And bo.applyTariff = CFRConst.tariffValidEnum.Valid Then
                s.Append("<div class='bookroomminimumdaysdiv col-xs-12 col-sm-8'>")
                s.AppendFormat("<div class='alert alert-info' role='alert'><i class='fa fa-info'></i> {0}</div>", String.Format(Me.langCaption.GetCaptionBy(307), bo.minimumDays))
                s.Append("</div>")
            End If


            s.Append("      <div class='wb-rate-actions'>")
            'select room button
            Dim butonAction As String = String.Format("return selectunitstd('{0}','{1}')", myRoom.idEntity, bo.priceFinalForAllDays)
            s.AppendFormat("    <div class='wb-book-action'><a class='btn btn-primary btn-lg' onclick=""{0}""><span id='btnbook_sp_{2}'>{1}</span> <i class='fa fa-shopping-bag fa-1'></i></a></div>", butonAction, Me.langCaption.GetCaptionBy(Language.msgEnum.E92_bookButton), myRoom.idEntity)
            'end select room button

            'price + cancel description div
            s.Append("<div style='float:Right;'>")
            s.AppendFormat("<div class='wb-price'  onclick=""showroomdesglosedprice(ripd_{0},ribd_{0},ritd_{0})"" title='{1}'>", bo.id, Me.langCaption.GetCaption(215))
            'button to see desglosed price
            s.Append("<div><i class='fa fa-eye'></i> <span class='wb-price-label'>" & Me.langCaption.GetCaption(89) & "</span><span class='wb-price-value'>" & bo.priceFinalForAllDays & "</span><span class='wb-price-currency glyphicon glyphicon-euro' aria-hidden='true'></span></div>")
            s.Append("	</div>") 'bookroomtarrifdiv
            'policy condition link
            Dim cancelConditionDiv As String = getPolicyConditionByTariff(dr, bo, s)

            s.Append("</div>") 'end bookroombuttondiv
            s.Append("</div>") 'end bookroombuttondiv

            s.Append("</div>") 'end rowprice

            s.Append("</div>") 'end row
            s.Append(cancelConditionDiv)
            'no refund tarif
            configNorefundTariff(dr, bo, s)
        End If


        s.Append("</article>  " & vbCr) 'end id='roomfindedbysearch_{0}'
        s.Append("<!--roomfindedbysearch_" & myRoom.idEntity & "-->")

        Return s.ToString
    End Function


#Region "Cancel policy conditions by tariff"

    Private Function getPolicyConditionByTariff(ByVal dr As cfr.BookRes, ByVal bo As BookedRoom, s As StringBuilder) As String

        Dim cancelPolicyObj As R_PolicyCancel = Nothing
        Dim msgCancel As String = ""
        For Each tar As TariffByDay In bo.tarifDailyList.Values
            Dim tarCancelPolicy As R_PolicyCancel = tar.tariffBase.cancelpolicy
            If cancelPolicyObj Is Nothing Then
                cancelPolicyObj = tarCancelPolicy
                Continue For
            End If
            'compare tariff untilday
            If cancelPolicyObj.chargeuntilday <> tarCancelPolicy.chargeuntilday Then
                Dim actualimitday As String = cancelPolicyObj.chargeuntilday
                Dim newLimitday As String = tarCancelPolicy.chargeuntilday
                'until day is different, check which have less weight
                If actualimitday.ToUpper.EndsWith("H") And newLimitday.ToUpper.EndsWith("H") Then
                    'same weight hour
                    Dim actualimitdayNum As Integer = CInt(actualimitday.ToUpper.Replace("H", ""))
                    Dim newlimitdayNum As Integer = CInt(newLimitday.ToUpper.Replace("H", ""))
                    If newlimitdayNum < actualimitdayNum Then
                        'new cancel is 14h and have big weight than 16H
                        cancelPolicyObj = tarCancelPolicy
                        Continue For
                    End If
                ElseIf actualimitday.ToUpper.EndsWith("H") = True And newLimitday.ToUpper.EndsWith("D") Then
                    'actual is hour  and new is day
                    cancelPolicyObj = tarCancelPolicy
                    Continue For
                ElseIf actualimitday.ToUpper.EndsWith("D") = True And newLimitday.ToUpper.EndsWith("D") Then
                    'same weight day, check number
                    Dim actualimitdayNum As Integer = CInt(actualimitday.ToUpper.Replace("D", ""))
                    Dim newlimitdayNum As Integer = CInt(newLimitday.ToUpper.Replace("D", ""))
                    If newlimitdayNum > actualimitdayNum Then
                        'new cancel need more days to cancel
                        cancelPolicyObj = tarCancelPolicy
                        Continue For
                    End If
                End If

            End If
        Next

        If cancelPolicyObj Is Nothing Then
            Return ""
        End If
        Dim divId As String = "cancelconditiondiv_" & bo.id.ToString & "_0"
        Dim canceldata As String = String.Format(" data-cancelpolicy=""{0},{1},{2}""", cancelPolicyObj.chargeuntilday, cancelPolicyObj.chargeunitldaypercent, cancelPolicyObj.chargeafterdaypercent)
        s.AppendFormat("<div id='tariffcancelcondition_{3}_0' class='tariffcancelcondition' onclick='return showcancelcondition(""{0}"");' {2}>{1}</div>", divId, Me.langCaption.GetCaptionBy(389), canceldata, bo.id)

        msgCancel = getCancelPolicyMessage(cancelPolicyObj)
        Return getCancelConditionMessageFormated(bo, msgCancel, 0)

    End Function

    Private Function getPolicyConditionByNoRefundTariff(ByVal dr As cfr.BookRes, ByVal bo As BookedRoom, s As StringBuilder) As String

        Dim divId As String = "cancelconditiondiv_" & bo.id.ToString & "_1"
        s.AppendFormat("<div class='tariffcancelcondition' onclick='return showcancelcondition(""{0}"");'>{1}</div>", divId, Me.langCaption.GetCaptionBy(389))

        Dim msgCancel As String = Me.langCaption.GetCaption(387) '"Esta tarifa no es reembolsable. Si cancela en cualquier momento no se le devolverá ninguna cantidad de la reserva."
        Return getCancelConditionMessageFormated(bo, msgCancel, 1)

    End Function

    Private Function getCancelConditionMessageFormated(ByVal bo As BookedRoom, msgCancel As String, conditiontype As Integer) As String
        Dim divId As String = "cancelconditiondiv_" & bo.id.ToString & "_" & conditiontype

        Dim msgCancelWrapper As String = String.Format("<div id='{1}' class='row cancelconditiondiv' style='display:none;'><div class='col-xs-12'><div class='titconditioncancel'>{0}", Me.langCaption.GetCaptionBy(389), divId)

        msgCancelWrapper += String.Format("<div class='pull-right pr-10'><button type='button' class='close white' onclick='showcancelcondition(""{0}"");' ><span aria-hidden='true'>×</span></button></div>", divId)
        msgCancelWrapper += "</div><p class='tariffconditions ph-10'>{0}</p>"
        msgCancelWrapper += String.Format("<div class='pull-right' style='padding-right:10px;'><button type='button' class='btn btn-default ' onclick='return showcancelcondition(""{1}"");'><i class='fa fa-minus-square-o'></i> {0}</button></div>", Me.langCaption.GetCaptionBy(Language.msgEnum.close), divId)
        msgCancelWrapper += "</div></div>"
        Return String.Format(msgCancelWrapper, msgCancel)
    End Function

    Public Function getCancelPolicyMessage(cancelPolicyObj As R_PolicyCancel) As String

        Dim msg1 As String = "Si cancelas <strong>{mycanceluntil}</strong> pagarás <strong>{myporcentage}</strong>"
        Dim msg2 As String = " , o <strong>{myporcentage1}</strong> si cancelas despues."
        Dim msg3 As String = "<strong>{mycanceluntil}</strong> la cancelación es gratuita."
        Dim msg4 As String = " sino pagarás <strong>{myporcentage1}</strong> si cancelas despues."
        Dim msgcond As String = ""


        Dim valdayuntiltxt As String = Me.getConditionCancelPolicyMessageLimit1(cancelPolicyObj.chargeuntilday)
        Dim cbocanceluntiltxt As String = Me.getConditionCancelPolicyMessageLimit1Value(cancelPolicyObj.chargeunitldaypercent.ToString)
        Dim valdayaftertxt As String = Me.getConditionCancelPolicyMessageAfterValue(cancelPolicyObj.chargeafterdaypercent.ToString)

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

    Private Function getConditionCancelPolicyMessageLimit1(limitDay As String) As String
        limitDay = limitDay.ToUpper
        If limitDay.EndsWith("H") Then
            Dim limit As String = limitDay.Replace("h", "")
            Return String.Format("hasta las {0}:00 del día de llegada", limit)
        ElseIf limitDay.EndsWith("D") Then
            Dim limit As String = limitDay.Replace("D", "")
            If limit = "1" Then
                Return String.Format("hasta {0} día antes de la fecha de llegada", limit)
            Else
                Return String.Format("hasta {0} días antes de la fecha de llegada", limit)
            End If
        End If
        Return Nothing
    End Function

    Private Function getConditionCancelPolicyMessageLimit1Value(limitDayValueToCharge As String) As String

        If limitDayValueToCharge = "0" Then
            Return "Cancelación Gratuita"
        ElseIf limitDayValueToCharge = "1" Then
            Return "la primera noche"

        Else
            Return String.Format("un {0}% del precio total", limitDayValueToCharge)
        End If
    End Function

    Private Function getConditionCancelPolicyMessageAfterValue(limitDayValueToCharge As String) As String

        If limitDayValueToCharge = "1" Then
            Return "la primera noche"

        Else
            Return String.Format("un {0}% del precio total", limitDayValueToCharge)
        End If

    End Function
#End Region


    Private Sub configNorefundTariff(ByVal dr As cfr.BookRes, ByVal bo As BookedRoom, s As StringBuilder)

        Dim isnorefundtariffOK As Boolean = False
        Dim norefundTariffTotal As Double = 0
        For Each tar As TariffByDay In bo.tarifDailyList.Values
            norefundTariffTotal += tar.priceNoRefund
        Next

        If bo.priceFinalForAllDays <> norefundTariffTotal Then
            Dim myRoom As cfr.obj.HotelRoomData = bo.roomSelected
            Dim butonAction As String = String.Format("selectunitnorefund('{0}','{1}')", myRoom.idEntity, norefundTariffTotal)

            s.Append("<div class='wb-rate-row'><div class='wb-rate-footer'>")
            s.Append("<div class='wb-rate-actions'>")
            s.AppendFormat("<div class='wb-book-action'><a class='btn btn-primary btn-lg' onclick=""{0}""><span id='btnbook_sp_norefund{2}'>{1}</span> <i class='fa fa-shopping-bag fa-1'></i></a></div>", butonAction, Me.langCaption.GetCaptionBy(Language.msgEnum.E92_bookButton), myRoom.idEntity)
            
            'price + cancel description div
            s.Append("<div style='float:Right;'>")
            s.AppendFormat("<div class='wb-price'  onclick=""showroomdesglosedprice(ripnorefund_{0},ribd_{0},ritd_{0})"" title='{1}'>", bo.id, Me.langCaption.GetCaption(215))
            'button to see desglosed price
            s.Append("<div><i class='fa fa-eye'></i> <span class='wb-price-label'>" & Me.langCaption.GetCaption(387) & "</span><span class='wb-price-value'>" & norefundTariffTotal & "</span><span class='wb-price-currency glyphicon glyphicon-euro'></span></div>")
            s.Append("	</div>") 'bookroomtarrifdiv
            
            'policy condition link
            Dim cancelConditionDiv As String = getPolicyConditionByNoRefundTariff(dr, bo, s)
            s.Append("</div>") 'end bookroombuttondiv
            s.Append("</div>") 'end bookroombuttondiv

            s.Append("</div>") 'end rowprice

            s.Append("</div>") 'end row
            s.Append(cancelConditionDiv)
        End If

    End Sub

    Private Function getBoardStringForUI(ByVal dr As cfr.BookRes, ByVal bo As BookedRoom) As String

        If dr.hotel.BookParam.regimenCountAvailable = 1 Then
            'When only one board is selected by the hotel the user interface no show the board combo so the user need to know the board selected.
            If bo.boardChangeForDays = True Then
                'todo: cambiar el literal
                Return String.Format("<li class='hand'>El regimen incluido para cada dia es diferente. <a role='button' href='#' onclick='showroomdesglosedprice(ripd_{0},ribd_{0},ritd_{0});'>Ver</a></li>", bo.id)
            Else
                'same board for all days, show to the client
                Dim sboard As String = String.Format("<li class='cutlery'>{0}</li>", getBoardStringForUI(bo.boardTariff))
                Return sboard
            End If
        End If

        Return ""

    End Function
    ''' <summary>
    ''' get the string that represents the board in user selected language
    ''' </summary>
    ''' <param name="boardTariff"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBoardStringForUI(boardTariff As cfr.CFRConst.RegimenEnum) As String
        Select Case boardTariff
            Case CFRConst.RegimenEnum.SA
                Return Me.langCaption.GetCaptionBy(Language.msgEnum.UI_book_board_onlyStay)
            Case CFRConst.RegimenEnum.AD
                Return Me.langCaption.GetCaptionBy(Language.msgEnum.UI_book_board_stayandbreakfast)
            Case CFRConst.RegimenEnum.MP
                Return Me.langCaption.GetCaptionBy(Language.msgEnum.UI_book_board_medium)
            Case CFRConst.RegimenEnum.PC
                Return Me.langCaption.GetCaptionBy(Language.msgEnum.UI_book_board_full)
            Case CFRConst.RegimenEnum.TI
                Return Me.langCaption.GetCaptionBy(Language.msgEnum.UI_book_board_AllInclusive)
        End Select
        Return Nothing
    End Function

    Private Function getImgHtmlForRoomViewCaroussel(myRoom As cfr.obj.HotelRoomData) As String
        Const sMainContainer As String = "  <div id='pop_{0}' class='pop-gallery-bk owl-carousel content-slider-with-controls owl-room-pic'>{1}</div>"
        Const sSubContainer As String = "  <div class='overlay-container overlay-visible'>{0}</div>"
        Dim dv As DataView = ind.bk.obj.T_HOTELES_HABITACIONES_Manager.GetRoomFotosByRoomCode(myRoom.idEntity).DefaultView

        Dim smain As String = ""
        If dv.Count > 0 Then
            For Each drv As DataRowView In dv
                Dim s As String = ""
                Dim lnkFoto As String
                Dim lnkFotothumb As String
                If "" & drv("fotourl") = "" Then
                    Dim imageFileName As String = String.Format("{0}_{1}_{2}{3}.jpg", myRoom.cod_Hotel, myRoom.idEntity, drv("id"), drv("VERSION"))
                    lnkFoto = RoomImageStorage.BuildPublicUrl(Me.ResolveUrl("~/RoomImage.ashx"), imageFileName)
                    If drv("thumbnail") = True Then
                        Dim thumbnailFileName As String = String.Format("{0}_{1}_{2}{3}_TH.jpg", myRoom.cod_Hotel, myRoom.idEntity, drv("id"), drv("VERSION"))
                        lnkFotothumb = RoomImageStorage.BuildPublicUrl(Me.ResolveUrl("~/RoomImage.ashx"), thumbnailFileName)
                    Else
                        lnkFotothumb = lnkFoto
                    End If

                Else
                    Dim imageFileName As String = String.Format(CStr(drv("fotourl")), "")
                    lnkFoto = RoomImageStorage.BuildPublicUrl(Me.ResolveUrl("~/RoomImage.ashx"), imageFileName)
                    If drv("thumbnail") = True Then
                        Dim thumbnailFileName As String = String.Format(CStr(drv("fotourl")), "_TH")
                        lnkFotothumb = RoomImageStorage.BuildPublicUrl(Me.ResolveUrl("~/RoomImage.ashx"), thumbnailFileName)
                    Else
                        lnkFotothumb = lnkFoto
                    End If
                End If


                s += vbCr & "<div class='overlay-container overlay-visible'>"
                s += vbCr & String.Format("<img class='lazyOwl img-carousel' data-src='{0}' />", lnkFotothumb)
                's += vbCr & "<div class='overlay-bottom hidden-xs'>"
                's += vbCr & "   <div class='text'>"
                's += vbCr & "       Nuestra recepcion con vistas al mar"
                's += vbCr & "   </div>"
                's += vbCr & "</div>"
                s += String.Format("<a class='popup-img unitimg overlay-link' href='{0}'><i class='fa fa-plus'></i></a>", lnkFoto)
                s += vbCr & "</div>"
                smain += s
            Next
            smain = String.Format(sMainContainer, myRoom.idEntity, smain)
            'Debug.WriteLine(smain)
            Return smain

        Else
            Dim s As String = "<img class='' src='images/noimage-th.jpg'/>"
            s = String.Format(sSubContainer, s)
            Return s
        End If


    End Function

#End Region



#Region "configuracion"


    Private Sub configUI()

        If Me.siteWeb.showPrincipal = False Then
            Me.pnlpropertyheader.Visible = True
            Me.pnllanguage.Visible = True
            Me.pnlLoginRegisteredUsers.Visible = True

            'Me.pnlbooksummary.Style.Remove("display")
            Me.pnlinfobook.Visible = True
            Exit Sub
        End If

        If Me.siteWeb.masterPageCode.ToUpper <> "BLANK" And Me.siteWeb.showPrincipal = True Then
            Me.pnlpropertyheader.Visible = False
            Me.pnllanguage.Visible = False
            Me.pnlcancellink.Visible = False
            Me.pnlLoginRegisteredUsers.Visible = False
            Me.pnlbooksummary.Style.Add("display", "none")
            Me.pnlinfobook.Visible = True
        ElseIf Me.siteWeb.masterPageCode.ToUpper = "BLANK" Then
            Me.pnlpropertyheader.Visible = True
            Me.pnllanguage.Visible = True
            Me.pnlLoginRegisteredUsers.Visible = True

            Me.pnlbooksummary.Style.Remove("display")
            Me.pnlinfobook.Visible = True

        End If
    End Sub


    Private Sub fillCbos()

        Dim ndayInprecedence As Integer = Me.bookData.hotel.BookParam.daysInAdvance
        If ndayInprecedence < Me.bookData.bookOperator.Release Then
            ndayInprecedence = Me.bookData.bookOperator.Release
        End If

        Dim dayInprecedence As Date = Date.Now.AddDays(ndayInprecedence)
        Dim dayNext As Date = dayInprecedence.AddDays(1)
        Dim daymaxForCalendar As Date = dayInprecedence.AddYears(1)
        If cfr.Language.getLanguage = CFRConst.LanguageEnum.es Then
            calendarConfig.langSelected = "es"
            calendarConfig.dateFormat = "dd/mm/yyyy"
        ElseIf cfr.Language.getLanguage = CFRConst.LanguageEnum.en Then
            calendarConfig.langSelected = "en-GB"
            calendarConfig.dateFormat = "mm/dd/yyyy"
        ElseIf cfr.Language.getLanguage = CFRConst.LanguageEnum.de Then
            calendarConfig.langSelected = "de"
            calendarConfig.dateFormat = "dd/mm/yyyy"
        ElseIf cfr.Language.getLanguage = CFRConst.LanguageEnum.fr Then
            calendarConfig.langSelected = "fr"
            calendarConfig.dateFormat = "dd/mm/yyyy"
        ElseIf cfr.Language.getLanguage = CFRConst.LanguageEnum.ca Then
            calendarConfig.langSelected = "es"
            calendarConfig.dateFormat = "dd/mm/yyyy"
        ElseIf cfr.Language.getLanguage = CFRConst.LanguageEnum.it Then
            calendarConfig.langSelected = "it"
            calendarConfig.dateFormat = "dd/mm/yyyy"
        ElseIf cfr.Language.getLanguage = CFRConst.LanguageEnum.jp Then
            calendarConfig.langSelected = "jp"
            calendarConfig.dateFormat = "yyyy/mm/dd"
        End If

        calendarConfig.dateIn = dayInprecedence.ToShortDateString
        calendarConfig.dateOut = dayNext.ToShortDateString
        calendarConfig.dateMax = daymaxForCalendar.ToShortDateString

        Me.checkindate.Text = dayInprecedence.ToShortDateString
        Me.checkoutdate.Text = dayNext.ToShortDateString

        'Me.fechad2.Text = dayNext.Day ' Day(bookData.dataOut)
        'Me.fechad2.Attributes.Add("onchange", "mifci(2, " & Me.bookData.language & ")")

        'REGIMEN
        configRegimenCBO()

        'CHILDREN
        With Me.f_ninosfree.Items
            For x As Integer = 0 To 2
                .Add(New ListItem(x, x))
            Next
        End With
        Me.f_ninosfree.Text = 0 'bookData.ninosfree
        With Me.f_ninosPago.Items
            For x As Integer = 0 To 5
                .Add(New ListItem(x & " " & Me.langCaption.GetCaption(15), x))
            Next
        End With
        Me.f_ninosPago.Text = 0 'bookData.ninospago
        'ADULTS
        With Me.f_numAdultos.Items
            For x As Integer = 0 To 5
                .Add(New ListItem(x & " " & Me.langCaption.GetCaption(14), x))
            Next
        End With
        Me.f_numAdultos.Text = 1 'bookData.numAdultos

        Me.lblDateIn.Text = dayInprecedence.ToLongDateString
        Me.lblDateOut.Text = dayNext.ToLongDateString
        Me.lblNights.Text = cfr.tool.DateTools.GetDias(dayInprecedence, dayNext).ToString
        Me.lblnightsCalendar.Text = Me.lblNights.Text

        If Me.holdprop.Tipo = CFRConst.unitTypeEnum.car Then
            Me.cbotimecheckindiv.Visible = True
            Me.cbotimecheckoutdiv.Visible = True
            Me.cbotimecheckin.Visible = True
            Me.cbotimecheckout.Visible = True
            Me.cbotimecheckinlabel.Visible = True
            Me.cbotimecheckoutlabel.Visible = True

            With Me.cbotimecheckin.Items
                For x As Integer = 9 To 23
                    .Add(New ListItem(x & ":00", x))
                Next
            End With
            Me.cbotimecheckin.SelectedIndex = 0
            With Me.cbotimecheckout.Items
                'For x As Integer = 9 To 23
                '    .Add(New ListItem(x & ":00", x))
                'Next
                .Add(New ListItem("9:00", 9))
            End With
            Me.cbotimecheckout.SelectedIndex = 0

            Me.CboLocationsLoad()
            'With Me.cbolocationpickup.Items
            '    .Add(New ListItem(Me.langCaption.GetCaption(377), "1"))
            '    .Add(New ListItem(Me.langCaption.GetCaption(378), "2"))
            'End With

            'With Me.cbolocationdrop.Items
            '    .Add(New ListItem(Me.langCaption.GetCaption(379), "1"))
            '    .Add(New ListItem(Me.langCaption.GetCaption(380), "2"))
            'End With
            chkreturntolocation_lbl.Text = Me.langCaption.GetCaption(381)

            Me.plhcar_pickup.Visible = True
        Else
            Me.cbotimecheckindiv.Visible = False
            Me.cbotimecheckoutdiv.Visible = False
            Me.cbotimecheckin.Visible = False
            Me.cbotimecheckout.Visible = False
            Me.cbotimecheckinlabel.Visible = False
            Me.cbotimecheckoutlabel.Visible = False
            Me.plhcar_pickup.Visible = False
        End If
    End Sub


    Private Sub CboLocationsLoad()
        Dim lang As String = CFRConst.GetEnumDescription(Me.bookData.language)
        Dim ctx As New cfrbookEntities
        'Dim queryDef = From t In ctx.R_UnitLocation Join l In ctx.R_UnitLocation_Lang On t.id_unit_location Equals l.id_unit_location
        'Where t.cod_hotel = Me.holdprop.cod_Hotel And l.id_Lang = lang
        '    Order By t.location
        Dim queryDef = _
            From ord In ctx.R_UnitLocation _
            Join det In ctx.R_UnitLocation_Lang _
            On ord.id_unit_location Equals det.id_unit_location _
            Where ord.cod_hotel = Me.holdprop.cod_Hotel _
                    And det.id_Lang = lang _
                    Order By det.Description _
            Select New With _
            { _
                .id_unit_location = ord.id_unit_location, _
                .Description = det.Description, _
                .tipo = ord.tipo, _
                .price = ord.Price _
            }

        Try
            ' Bind the ComboBox control to the query.
            ' To prevent the query from being executed multiple times during binding, 
            ' it is recommended to bind controls to the result of the Execute method. 
            'Me.cbolocationpickup.DataMember = "Location"
            Me.cbolocationpickup.DataTextField = "description"
            Me.cbolocationpickup.DataValueField = "id_unit_location"
            'Me.cbolocationpickup.DataSource = ctx.R_UnitLocation.ToList
            Me.cbolocationpickup.DataSource = queryDef.ToList
            Me.cbolocationpickup.DataBind()

            Me.cbolocationdrop.DataTextField = "description"
            Me.cbolocationdrop.DataValueField = "id_unit_location"
            'Me.cbolocationdrop.DataSource = ctx.R_UnitLocation.ToList
            Me.cbolocationdrop.DataSource = queryDef.ToList
            Me.cbolocationdrop.DataBind()

            Dim defaultVal = (From r In queryDef Where r.tipo = 1).FirstOrDefault
            If Not defaultVal Is Nothing Then

                Me.cbolocationpickup.SelectedValue = defaultVal.id_unit_location
                Me.cbolocationdrop.SelectedValue = defaultVal.id_unit_location
            End If

            Dim s As String = ""
            s += "" & vbCrLf
            s += "<SCRIPT LANGUAGE=JavaScript >" & vbCrLf
            For Each r In queryDef
                s += String.Format("$('body').data('cbolocationprice_{0}','{1}');", r.id_unit_location, r.Price)
            Next
            s += "</SCRIPT>" & vbCrLf

            Me.litcboloactionjs.Text = s

        Catch ex As Exception

        End Try

    End Sub

    Private Sub configRegimenCBO()
        'REGIMEN
        With Me.f_regimen.Items
            If Me.bookData.hotel.BookParam.regSA = True Then
                .Add(New ListItem(Me.langCaption.GetCaptionBy(Language.msgEnum.UI_book_board_onlyStay), CFRConst.RegimenEnum.SA))
                Me.f_regimen.Text = CType(Me.bookData.hotel.BookParam.regSA, String)  'por defecto
            End If
            If Me.bookData.hotel.BookParam.regad = True Then
                .Add(New ListItem(Me.langCaption.GetCaptionBy(Language.msgEnum.UI_book_board_stayandbreakfast), CFRConst.RegimenEnum.AD))
            End If
            If Me.bookData.hotel.BookParam.regmp = True Then
                .Add(New ListItem(Me.langCaption.GetCaption(60), CFRConst.RegimenEnum.MP))
            End If
            If Me.bookData.hotel.BookParam.regpc = True Then
                .Add(New ListItem(Me.langCaption.GetCaption(61), CFRConst.RegimenEnum.PC))
            End If
            If Me.bookData.hotel.BookParam.regTI = True Then
                .Add(New ListItem(Me.langCaption.GetCaption(62), CFRConst.RegimenEnum.TI))
            End If
        End With


        If Me.f_regimen.Items.Count = 0 Or Me.f_regimen.Items.Count = 1 Then
            Me.f_regimen.Visible = False
            Me.placeHolderRegimen.Visible = False
        End If

    End Sub


    Private Sub setJavascriptCalendar1()
        Dim s As String = ""
        s += "<link rel='stylesheet' href='vendor/jquery-ui-1.11.4/jquery-ui.css'>" & vbCr
        s += "<script type='text/javascript' src='vendor/jquery-ui-1.11.4/jquery-ui.min.js'></script>" & vbCr
        s += "  <script>"
        s += "$(function() {"
        s += " $( '#checkindate' ).datepicker({"
        s += "defaultDate:  '0',"
        s += "changeMonth: false,"
        s += "numberOfMonths: 2,"
        s += "onClose: function( selectedDate ) {"
        s += " $( '#checkoutdate' ).datepicker( 'option', 'minDate', selectedDate );"
        s += " }"
        s += " });"
        s += " $( '#checkoutdate' ).datepicker({"
        s += "defaultDate:  '1',"
        s += "changeMonth: false,"
        s += "numberOfMonths: 2,"
        s += "onClose: function( selectedDate ) {"
        s += "  $( '#checkindate' ).datepicker( 'option', 'maxDate', selectedDate );"
        s += "}"
        s += "});"
        s += "});"
        s += "</script>"

        Me.zpcalendarLiteral.Text = s & vbCr
    End Sub

    Private Sub setJavascriptCalendar()

        Dim s As String = ""
        s += "<script type='text/javascript' src='js/dt/bootstrap-datepicker.js' charset='UTF-8'></script>" & vbCr
        s += "<!-- Loading language definition file -->" & vbCr
        Dim lang As CFRConst.LanguageEnum = cfr.Language.getLanguage

        If lang = CFRConst.LanguageEnum.es Then
            s += "    <script type='text/javascript' src='js/dt/dtlang/bootstrap-datepicker.es.min.js'></script>" & vbCr
        ElseIf lang = CFRConst.LanguageEnum.en Then
            s += "   <script type='text/javascript' src='js/dt/dtlang/bootstrap-datepicker.en-GB.min.js'></script>" & vbCr
        ElseIf lang = CFRConst.LanguageEnum.de Then
            s += "   <script type='text/javascript' src='js/dt/dtlang/bootstrap-datepicker.de.min.js'></script>" & vbCr
        ElseIf lang = CFRConst.LanguageEnum.fr Then
            s += "   <script type='text/javascript' src='js/dt/dtlang/bootstrap-datepicker.fr.min.js'></script>" & vbCr
        Else
            s += "   <script type='text/javascript' src='js/dt/dtlang/bootstrap-datepicker.en-GB.min.js'></script>" & vbCr
        End If

        Dim sjs As String = "<script type='text/javascript'>"
        sjs += "var datepickerinputdaterange=  """ & calendarConfig.langSelected & """;"
        sjs += "$('.input-daterange').datepicker({"
        sjs += "language:  """ & calendarConfig.langSelected & ""","
        sjs += "format: """ & calendarConfig.dateFormat & ""","
        sjs += "startDate: """ & calendarConfig.dateIn & ""","
        sjs += "endDate: """ & calendarConfig.dateMax & ""","
        sjs += "autoclose: true,"
        sjs += "orientation:'top auto',"
        sjs += "setStartDate: """ & calendarConfig.dateIn & ""","
        sjs += "disableTouchKeyboard: false,"
        sjs += "});"
        sjs += "</script>"

        Dim sjsf As String = ""
        sjsf += "<script>"
        sjsf += "$('#checkoutdate').focus(function () {"
        sjsf += "   $(this).blur();"
        sjsf += "});"
        sjsf += "$('#checkindate').focus(function () {"
        sjsf += "   $(this).blur();"
        sjsf += "});"
        sjsf += "$('#checkindate').datepicker().on('changeDate', function(ev) {"
        sjsf += "   var checkin = new Date(ev.date);"
        sjsf += "   var checkout = $('#checkoutdate').datepicker('getDate');"
        sjsf += "   var today = new Date();"
        sjsf += "   if (checkin < today) {"

        sjsf += "       $('#checkindate').datepicker('update',today );"
        sjsf += "       checkin=today;"
        sjsf += "   }"

        'sjsf += "console.log('checkin ' + checkin);"
        'sjsf += "console.log('checkout ' + checkout);"

        sjsf += "   if (checkin >= checkout) {"
        'sjsf += "      console.log('checkin >= checkout');"
        sjsf += "       checkout = new Date(checkin.setDate(checkin.getDate() + 1));"
        sjsf += "       $('#checkoutdate').datepicker('update', checkout);"
        sjsf += "       $('#checkoutdate').datepicker('setStartDate', checkout);"

        'sjsf += "       //$('#checkoutdate').trigger('focus');"
        sjsf += "   } else {"
        sjsf += "       var checkoutstart = new Date(checkin.getDate() + 1);"
        sjsf += "       $('#checkoutdate').datepicker('setStartDate', checkoutstart);"
        sjsf += "   }"
        sjsf += "});"
        sjsf += "</script>"

        Me.zpcalendarLiteral.Text = s & vbCr & sjs & vbCr & sjsf
    End Sub

    Public Sub setScriptDaysForjsVarLayerDaysToShowTariff()

        Dim strCabTabDesglPrecDIA As String = String.Empty
        Dim strCabTabDesglPrec As String = String.Empty
        For i As Integer = 0 To Me.bookData.bookTotalDays - 1
            Dim ds As Date = Me.bookData.dateIn.AddDays(i)
            strCabTabDesglPrecDIA += UCase(Left(WeekdayName(DatePart("w", ds, Microsoft.VisualBasic.FirstDayOfWeek.Monday), True, 2), 1)) & "|"
            strCabTabDesglPrec += ds.Day & "/" & ds.Month & "|"
        Next

        Dim s As String = ""
        s += "" & vbCrLf
        's += "<SCRIPT LANGUAGE=JavaScript >" & vbCrLf
        s += "var nNoches='" & strCabTabDesglPrec & "';" & vbCrLf
        s += "var nNameDias='" & strCabTabDesglPrecDIA & "';" & vbCrLf
        s += "var titDays='" & Me.langCaption.GetCaption(217) & "';" & vbCrLf
        s += "var titPrice='" & Me.langCaption.GetCaption(218) & "';" & vbCrLf

        If Me.holdprop.Tipo = CFRConst.unitTypeEnum.hotel Then
            s += "var titReg='" & Me.langCaption.GetCaption(17) & "';" & vbCrLf
        Else
            s += "var titReg='';" & vbCrLf

        End If

        s += "var titTariff='" & Me.langCaption.GetCaption(295) & "';" & vbCrLf
        s += "var regsa='" & Me.langCaption.GetCaptionBy(Language.msgEnum.UI_book_board_onlyStay) & "';" & vbCrLf
        s += "var regad='" & Me.langCaption.GetCaptionBy(Language.msgEnum.UI_book_board_stayandbreakfast) & "';" & vbCrLf
        s += "var regmp='" & Me.langCaption.GetCaptionBy(Language.msgEnum.UI_book_board_medium) & "';" & vbCrLf
        s += "var regpc='" & Me.langCaption.GetCaptionBy(Language.msgEnum.UI_book_board_full) & "';" & vbCrLf
        s += "var regti='" & Me.langCaption.GetCaptionBy(Language.msgEnum.UI_book_board_AllInclusive) & "';" & vbCrLf
        s += "var msg98='" & Me.langCaption.GetCaption(98) & "';" & vbCrLf
        s += "var msg99='" & Me.langCaption.GetCaption(99) & "';" & vbCrLf
        s += "var msg289='" & Me.langCaption.GetCaption(289) & "';" & vbCrLf
        s += "var msg313='" & Me.langCaption.GetCaption(313) & "';" & vbCrLf
        s += "var msg311='" & Me.langCaption.GetCaption(311) & "';" & vbCrLf
        s += "var msg314='" & Me.langCaption.GetCaption(314) & "';" & vbCrLf
        s += "var msg280='" & Me.langCaption.GetCaption(280) & "';" & vbCrLf
        s += "var msg370='" & Me.langCaption.GetCaption(370) & "';" & vbCrLf
        s += "var msg371='" & Me.langCaption.GetCaption(371) & "';" & vbCrLf

        UIMsg.registerScript(Me.Page, s, "showdesglosedpricevarneeded")

    End Sub

    ''' <summary>
    ''' set the text for labels, etc based on language
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setCaption()
        Me.btnFindAction.Text = Me.langCaption.GetCaption(277) & " <i class='fa fa-search'></i>"
        Me.BookConfirmationBtn.Text = Me.langCaption.GetCaption(144)
        'Me.btnCloseSesion.Text = Me.langCaption.GetCaptionBy(Language.msgEnum.closeSesion)
        Me.btnbookloginregisteredusers.Text = Me.langCaption.GetCaptionBy(Language.msgEnum.continueMsg)
        Me.rdbTariffForClientPrivate.Text = Me.langCaption.GetCaption(271)
        Me.rdbTariffForClientCompany.Text = Me.langCaption.GetCaption(265)
        'Me.rdbTariffForClientCompanyRegistered.Text = Me.langCaption.GetCaption(272)
        Me.lblAgencyTitleSelectInfo.Text = Me.langCaption.GetCaption(365)


        Me.ReqLogin.Text = Me.langCaption.GetCaption(cfr.CFRConst.lngBas.required)
        Me.ReqClave.Text = Me.langCaption.GetCaption(cfr.CFRConst.lngBas.required)
        Me.Reqaceptolopd.Text = Me.langCaption.GetCaption(cfr.CFRConst.lngBas.lopdrequired)
    End Sub

    ''' <summary>
    ''' Check the error to show a friendly message
    ''' </summary>
    ''' <param name="boe"></param>
    ''' <remarks></remarks>
    Public Overloads Sub controlError(ByVal boe As BookOperationErrors)

        Dim msgErr As String = ""
        If IsNumeric(boe.errBook) Then
            If boe.errBook <> basERR.S_OK Then
                'first check page control errors
                Select Case boe.errBook
                    Case basERR.E_CheckDiasDeAntelacion '27
                        msgErr = String.Format(Me.langCaption.GetCaption(28), Me.bookData.hotel.BookParam.daysInAdvance)
                    Case basERR.E_Release '28
                        msgErr = String.Format(Me.langCaption.GetCaption(28), Me.bookData.bookOperator.Release)
                    Case basERR.E_No_Hay_Disponibilidad '20 No hay disponibilidad
                        showMapAvailableRooms()
                        Exit Sub
                    Case Else
                        ' check base errors
                        msgErr = MyBase.controlError(boe)
                End Select
            End If
        Else
            msgErr = boe.errBook
        End If



        Me.showMessage(msgErr)
    End Sub

    Private Sub showMapAvailableRooms()
        Try
            Dim amh As New AvailabilityMapHelper(Me.bookData)
            Me.litMapAvailableDates.Text = amh.strUICalendar
            Me.litMapAvailableDates.Visible = True
        Catch ex As Exception
            Me.showMessage(ex.Message, CFRConst.msgEnum.errorType)
        End Try
    End Sub



    Public Overloads Sub catchExceptionControler(ByVal ex As Exception)
        Dim msgErr As String = ""
        msgErr = MyBase.catchExceptionControler(ex)
        Me.showMessage(msgErr)
    End Sub
#End Region

#Region "find actions"


    ''' <summary>
    ''' Find room button action
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindAction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindAction.Click
        Try
            If Me.cancelFlow = True Then
                Exit Sub
            End If
            Me.findRoomsByUserSelection()

        Catch ex As Exception
            Me.catchExceptionControler(ex)
        End Try

    End Sub
    ''' <summary>
    ''' return the array of rooms sorted by price
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getBookedRoomsSortedByPrice() As BookedRoom()
        Dim aSorted(Me.bookData.roomsFinded.Values.Count - 1) As BookedRoom
        Me.bookData.roomsFinded.Values.CopyTo(aSorted, 0)
        Array.Sort(aSorted)
        Return aSorted
    End Function
    ''' <summary>
    ''' save request for statistics
    ''' </summary>
    ''' <param name="bookdata"></param>
    ''' <remarks></remarks>
    Private Sub saveRequest(bookdata As cfr.BookRes)
        Try
            Dim reqpet As New ind.bk.obj.R_Peticiones
            Dim rpetmgr As New ind.bk.obj.R_Peticiones_Manager
            reqpet.cod_hotel = bookdata.hotel.id
            reqpet.cod_operador = bookdata.bookOperator.cod_operador
            reqpet.fechaEntrada = bookdata.dateIn
            reqpet.fechaSalida = bookdata.dateOut

            reqpet.adultos = bookdata.numAdultos
            reqpet.ninospago = bookdata.ninospago
            reqpet.ninosfree = bookdata.numAdultos
            reqpet.regimen = bookdata.regimen
            reqpet.IP = Request.UserHostAddress

            rpetmgr.insert(reqpet)
        Catch ex As Exception
            Debug.WriteLine("Error logging customer request")
        End Try

    End Sub

    ''' <summary>
    ''' Find rooms based on search criteria
    ''' </summary>
    ''' <remarks></remarks>
    Private Function findRoomsByUserSelection() As Boolean

        litmsgerrbookfind.Text = ""
        Me.pnlResults.Visible = False
        Me.pnlbookconfirmation.Visible = False
        Me.divbookloading.CssClass = Me.divbookloading.CssClass.Replace("open", "")

        Me.bookData.uniqueBookId = Microsoft.VisualBasic.Rnd()

        Dim resultOperation As New BookOperationErrors
        Dim optionsScriptResultUpd As String = ""
        Try
            If Me.bookoperatorID = 0 And Me.bookData.bookOperator.Freesale = CFRConst.genericOperator.Registered Then 'there is a bookoperator registered
                'this means that the user logged with credentials in diferent page. we need to reload or show a message.
                Me.showMessage("Ha entrado como usuario registrado en otra pagina. Por favor cierre esta pagina.", CFRConst.msgEnum.errorType)
                Return False
            End If


            If Me.bookoperatorID <> 0 Then 'there is a bookoperator registered
                If Me.bookData.bookOperator.Freesale <> CFRConst.genericOperator.Registered Then
                    'session was recreated or closed
                    Me.closeSession()
                    Return False
                End If

                If Me.rdbTariffForClientPrivate.Checked = False And Me.rdbTariffForClientCompany.Checked = False Then
                    'If Me.rdbTariffForClientPrivate.Checked = False And Me.rdbTariffForClientCompany.Checked = False And Me.rdbTariffForClientCompanyRegistered.Checked = False Then
                    Me.showMessage(Me.langCaption.GetCaption(270), CFRConst.msgEnum.errorType)
                    Return False
                End If


                If Me.rdbTariffForClientPrivate.Checked = True Then
                    Me.bookData.tariffForClientCode = cfr.CFRConst.bookTypeEnum.resParticular
                ElseIf Me.rdbTariffForClientCompany.Checked = True Then
                    Me.bookData.tariffForClientCode = cfr.CFRConst.bookTypeEnum.resEmpresa
                    'ElseIf Me.rdbTariffForClientCompanyRegistered.Checked = True Then
                    '    Me.bookData.tariffForClientCode = Me.cboCompaniesForAgency.SelectedValue
                End If
            End If

            Me.selectedroomid.Value = 0


            Try
                Me.getSearchCriteriaByPage()
            Catch ex As Exception
                resultOperation.errBook = ex.Message
                Me.controlError(resultOperation)
                Me.ScriptBookResults()
            End Try


            If Me.bookData.promotionCode <> "" Then
                Try

                    'if there is no operator with that codes throw an exception and we must control result there
                    Dim bookOperatorPromotion As cfr.OperatorObj = cfr.OperadorManager.getOperatorPromotionObjByCredentials(Me.bookData.hotel.id, Me.bookData.promotionCode, Me.bookData.dateIn, Me.bookData.dateOut)
                    If Not bookOperatorPromotion Is Nothing Then

                        Me.bookData.bookOperatorPromotion = bookOperatorPromotion
                    End If
                Catch ex As Exception
                    Me.bookData.bookOperatorPromotion = Nothing
                    litmsgerrbookfind.Text = UIMsg.UIMessageDismiss(Me.langCaption.GetCaption(32), CFRConst.msgEnum.warning)
                    'Dim script As String = "bookpromotioncode(); "
                    'UIMsg.registerScript(Me.Page, script, "bookpromotioncodescript")

                End Try
            Else
                Me.bookData.bookOperatorPromotion = Nothing
            End If


            Me.saveRequest(Me.bookData)

            saveSearchCriteriaToViewstate(Me.bookData) 'saved in getsearccriteria method and here again due to possible changes

            Me.bookmgr = New cfr.BookManager(Me.bookData)
            Dim findResult As Boolean = bookmgr.GetHabPricesToRes()
            Me.bookData.roomsFinded = bookmgr.bookResult.roomInventory


            If bookmgr.bookResult.boe.errBook <> basERR.S_OK Then
                Me.controlError(bookmgr.bookResult.boe)
                Me.pnlResults.Visible = False
                Me.pnlClientData.Visible = False
                'Me.pnlBookButton.Visible = False 'not used with updatepanel
                Me.ScriptBookResults(optionsScriptResultUpd)
                Return False
            Else


            End If



            If bookmgr.bookResult.roomInventory.Count > 0 Then
                Me.setScriptDaysForjsVarLayerDaysToShowTariff()
                Me.litMapAvailableDates.Visible = False
                Me.litMapAvailableDates.Text = ""
                Me.pnlResults.Visible = True
                Me.B_ClientData1.configUI(Me.bookData)

                Dim sRooms As String = ""
                Dim showpnlClientData As Boolean = False
                For Each bo As cfr.BookedRoom In Me.getBookedRoomsSortedByPrice
                    sRooms += getRoomView(Me.bookData, bo)
                    If bo.applyTariff = CFRConst.tariffValidEnum.Valid Then
                        showpnlClientData = True
                    End If
                Next
                Me.litRoomFinded.Text = sRooms
                Me.configJSVarForRoomDesglosedInfo()

                If showpnlClientData = False Then
                    Me.pnlClientData.Visible = False
                Else
                    Me.pnlClientData.Visible = True
                    '*********
                    'at first is hidden, the client must choose one room and will be showed. 
                    'Next version when user choose diferent rooms it can be showed all the time. 
                    Me.pnlClientData.CssClass = "hidden"
                    '********
                End If
            Else

            End If

            'after search we show panels
            Me.B_ClientData1.fillFormWithBookData(Me.bookData)
            'optionsScriptResultUpd = "['1','1']"

            Me.ScriptBookResults(optionsScriptResultUpd)

            Return True


        Catch ex As Exception
            resultOperation.errBook = ex.Message
            resultOperation.exceptionThrowedVar = ex
            Me.controlError(resultOperation)
            Me.ScriptBookResults()
            Return False
        End Try
    End Function

    Private Sub ScriptBookResults(Optional options As String = "")

        Dim script As String = String.Format("showpnlbooksummary({0}); ", options)

        UIMsg.registerScript(Me.Page, script, "showbookbookscript")

    End Sub

    Private Sub ScriptbookConfirmationresult(Optional options As String = "")

        Dim script As String = "scrolltoresulttop(); "

        UIMsg.registerScript(Me.Page, script, "bookConfirmationScriptResult")

    End Sub

    Private Sub configJSVarForRoomDesglosedInfo()
        Dim s As String = ""
        For Each bo As cfr.BookedRoom In Me.getBookedRoomsSortedByPrice
            s += vbCr & String.Format("var ripd_{0}='{1}';", bo.id, bo.priceDesglosed)
            s += vbCr & String.Format("var ripnorefund_{0}='{1}';", bo.id, bo.getPriceNoRefundDesglosedString)
            s += vbCr & String.Format("var ribd_{0}='{1}';", bo.id, bo.RegimenDesglosed)
            s += vbCr & String.Format("var ritd_{0}='{1}';", bo.id, bo.TariffDescriptionDesglosed)

        Next
        UIMsg.registerScript(Me.Page, s, "desglosedprice")
    End Sub


    ''' <summary>
    ''' Get Room selected by user
    ''' </summary>
    ''' <param name="roomID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getRoomSelectedByID(ByVal roomID As Integer) As cfr.BookedRoom

        If Me.bookData.roomsFinded.ContainsKey(roomID) Then
            Return Me.bookData.roomsFinded(roomID)
        Else
            'session expired, find again
            Me.bookmgr = New cfr.BookManager(Me.bookData)
            Dim findResult As Boolean = bookmgr.GetHabPricesToRes()

            If bookmgr.bookResult.boe.errBook <> basERR.S_OK Then
                Dim be As New cfr.exceptions.BookException
                be.boe = bookmgr.bookResult.boe
                Throw be
            End If


            Me.bookData.roomsFinded = bookmgr.bookResult.roomInventory
            If Me.bookData.roomsFinded.ContainsKey(roomID) Then
                Return Me.bookData.roomsFinded(roomID)
            Else
                Throw New Collections.Generic.KeyNotFoundException
            End If

        End If
    End Function


#End Region

#Region "Book operation "


    ''' <summary>
    ''' book confirmation button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BookConfirmation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BookConfirmationBtn.Click

        If Page.IsValid = False Then
            Me.showMessage(Me.langCaption.GetCaption(371), CFRConst.msgEnum.errorType)
            Exit Sub
        End If

        Try
            Me.getSearchCriteriaFromViewstate(Me.bookData)


            If (Not Me.bookData.uniqueBookId Is Nothing) And Me.bookData.uniqueBookId = 0 Then
                Me.showMessage(Me.langCaption.GetCaption(364), CFRConst.msgEnum.errorType)
                Exit Sub
            End If

            If Me.Page.Session.IsNewSession = True Then
                Me.showMessage(Me.langCaption.GetCaption(306), CFRConst.msgEnum.errorType)
                Me.pnlResults.Visible = False
                'Me.pnlClientData.Visible = False
                Exit Sub
            End If
            
            'room selection
            Try
                Dim roomSelected As BookedRoom
                Me.bookData.roomsSelected.Clear()
                If Me.selectedroomid.Value = 0 Then
                    Me.showMessage(Me.langCaption.GetCaption(227), CFRConst.msgEnum.errorType)
                    Exit Sub
                End If

                roomSelected = Me.getRoomSelectedByID(Me.selectedroomid.Value)

                Me.bookData.roomsSelected.Add(roomSelected.id, roomSelected)
                Me.bookData.TPVMinimum = roomSelected.TPVMinimum


            Catch exbook As cfr.exceptions.BookException
                Me.controlError(exbook.boe)
                Me.pnlResults.Visible = False
                Me.pnlClientData.Visible = False
                'Me.pnlBookButton.Visible = False
                Exit Sub
            Catch ex As Collections.Generic.KeyNotFoundException
                'normally the session expired
                System.Diagnostics.Debug.WriteLine("Session expired or other error. " & ex.Message)
                Me.showMessage(Me.langCaption.GetCaption(Language.msgEnum.sesionExpired), CFRConst.msgEnum.errorType)
                Response.Redirect("b_request.aspx?" & Me.getQuery, True)
                Me.pnlResults.Visible = False
                Exit Sub
            End Try

            'tariff selected
            Me.bookData.noRefundTariffSelected = CBool(Me.norefundtariffselected.Value)
            Dim cancelpolicy As String = Me.hdfpolicycondition.Value
            If cancelpolicy.Length = 0 Then
                Me.bookData.cancelpolicy.chargeuntilday = "-1"
                Me.bookData.cancelpolicy.chargeunitldaypercent = 0
                Me.bookData.cancelpolicy.chargeafterdaypercent = 0
            Else
                Dim cancelpolicyMat As String() = cancelpolicy.Split(",")
                Me.bookData.cancelpolicy.chargeuntilday = cancelpolicyMat(0)
                Me.bookData.cancelpolicy.chargeunitldaypercent = cancelpolicyMat(1)
                Me.bookData.cancelpolicy.chargeafterdaypercent = cancelpolicyMat(2)
            End If

          

            'validate input
            Me.B_ClientData1.getClientDataForBook(Me.bookData)

            Me.bookData.formapagoRes = New FormaPago
            If Not PaymentMethodPolicy.IsSupported(Me.bookData.bookOperator.forma_pago) Then
                Me.showMessage("This booking rate does not have a supported payment method. Please contact the hotel.", CFRConst.msgEnum.errorType)
                Exit Sub
            End If
            Me.bookData.formapagoRes.forma_pago = Me.bookData.bookOperator.forma_pago

            If Me.holdprop.Tipo = CFRConst.unitTypeEnum.car Then
                Me.bookData.carLocationSelected = New R_ResO_CarPickup
                Me.bookData.carLocationSelected.unit_location_GET = Me.selectedlocationpickid.Value
                Me.bookData.carLocationSelected.unit_location_Get_Price = Me.selectedlocationpickprice.Value
                Me.bookData.carLocationSelected.unit_location_Return = Me.selectedlocationdropid.Value
                Me.bookData.carLocationSelected.unit_location_Return_Price = Me.selectedlocationdropprice.Value

            End If


            Me.saveDataToCookies()


            'do book
            Dim bm As New BookManager(Me.bookData)
            boe = bm.NewBook(Me.bookData)
            If boe.errBook <> basERR.S_OK Then
                Me.controlError(boe)
                Exit Sub
            End If

            Me.showbookConfirmation()


            'Try
            '    Dim queryBookResult As String = Me.GetQueryNormal()
            '    Response.Redirect("b_confirmation.aspx?" & queryBookResult)
            'Catch ex As ThreadAbortException
            '    'due to cancel thread of redirecting
            'End Try


        Catch ex As Exception
            boe.exceptionThrowed = ex
            Me.controlError(boe)
        Finally

        End Try
    End Sub

    Private Sub showbookConfirmation()
        Me.pnlResults.Visible = False
        Me.pnlClientData.Visible = False
        Me.pnlbookconfirmation.Visible = True
        Me.pnlbookconfirmationliteral.Visible = True

        'get from database
        'bookData reserved, we do not use the bookdata used in session because conditions made by user. In bookdata from the database are all the options selected by user and stored
        Dim bookdataBooked As BookRes = Nothing
        bookdataBooked = cfr.BookManager.getBookResById(bookData.idEntity)
        
        Dim cfrIU As New cfr.IUFunctions(Me.bookData.site.theme, bookdataBooked)
        cfrIU.showOptions.showDates = True
        cfrIU.showOptions.showEmail = True
        cfrIU.bookForStyle = IUFunctions.MailBookStyle.client
        Me.pnlbookconfirmationliteral.Text = cfrIU.getHtmlBook()
        Me.MaintainScrollPositionOnPostBack = False
        ScriptbookConfirmationresult()

    End Sub




#End Region

    Private Sub configCookieRegisteredClient()
        If Not Me.Page.IsPostBack Then
            If ((Not (Request.Cookies("RegisteredLoginUserName")) Is Nothing)) Then
                RegisteredLogin.Text = Request.Cookies("RegisteredLoginUserName").Value

                'chkRememberMe.Checked = True
            Else
                'chkRememberMe.Checked = False
            End If

        End If
    End Sub


    Protected Sub btnbookloginregisteredusers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbookloginregisteredusers.Click


        Me.pnlResults.Visible = False
        Me.pnlClientData.Visible = False

        Dim RegisteredLoginUserName As String = Me.RegisteredLogin.Text.Trim
        Dim RegisteredLoginUserPwd As String = Me.RegisteredPwd.Text

        If RegisteredLoginUserName = "" Or RegisteredLoginUserPwd = "" Then
            Me.showMessage(Me.langCaption.GetCaption(73))
            Exit Sub
        End If

        Response.Cookies("RegisteredLoginUserName").Value = Me.RegisteredLogin.Text.Trim
        Response.Cookies("RegisteredLoginUserName").Expires = DateTime.Now.AddDays(30)
        Response.Cookies("RegisteredLoginUserName").HttpOnly = True
        Response.Cookies("RegisteredLoginUserName").Secure = Request.IsSecureConnection
        Response.Cookies("RegisteredLoginUserName").SameSite = SameSiteMode.Lax

        Try

            Dim op As New cfr.OperadorManager
            Dim bookOperatorRegistered As cfr.OperatorObj = cfr.OperadorManager.getOperatorObjByCredentials(Me.bookData.hotel.id, RegisteredLoginUserName, RegisteredLoginUserPwd)
            Me.bookData.reset()
            Me.bookData.bookOperator = bookOperatorRegistered
            Me.bookoperatorID = Me.bookData.bookOperator.id
            Me.checkStatusSession()
            Me.showMsgInfo(Me.langCaption.GetCaption(360))
        Catch ex As Exception
            Me.catchExceptionControler(ex)
        End Try
    End Sub



    Public Function loginRegisteredUsers(user As String, userpwd As String) As cfr.OperatorObj
        user = user.Trim
        If user = "" Or userpwd = "" Then
            Throw New Exception(Me.langCaption.GetCaption(73))
        End If

        Try

            Dim op As New cfr.OperadorManager
            Dim bookOperatorRegistered As cfr.OperatorObj = cfr.OperadorManager.getOperatorObjByCredentials(Me.bookData.hotel.id, user, userpwd)
            Me.bookData.reset()
            Me.bookData.bookOperator = bookOperatorRegistered
            Me.checkStatusSession()
            Me.showMsgInfo(Me.langCaption.GetCaption(360))
        Catch ex As Exception
            Me.catchExceptionControler(ex)
        End Try
        Return Nothing
    End Function


    Protected Sub btnCloseSesion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseSesion.Click
        Me.closeSession()

    End Sub

    Private Sub closeSession()
        Try
            Me.bookoperatorID = 0
            Me.pnlResults.Visible = False
            Me.pnlClientData.Visible = False
            Me.bookData.reset()
            Me.bookData.bookOperator = bookData.hotel.PrivateoperatorObj
            Me.showMsgInfo(Me.langCaption.GetCaption(361))
            Me.checkStatusSession()
            Me.pnlBtnAgency.Visible = False

            Me.pnlBtnPrivateClient.Visible = True

        Catch ex As Exception
            Me.catchExceptionControler(ex)
        End Try
    End Sub

    Private Sub checkStatusSession()
        If Not Me.bookData Is Nothing Then
            If Me.bookData.bookOperator.Freesale = CFRConst.genericOperator.Registered Then

                Me.pnlloginregistereduser.Visible = False
                Me.pnlCloseSessionRegistered.Visible = True
                Me.litBookOperatorRegisteredDesc.Text = Me.bookData.bookOperator.Operador

                Me.pnlpromotioncodes.Visible = False

                Me.bookoperatorID = Me.bookData.bookOperator.id
            Else
                Me.pnlpromotioncodes.Visible = True
                Me.bookoperatorID = 0
                Me.pnlloginregistereduser.Visible = True

                Me.RegisteredPwd.Text = ""
                Me.pnlCloseSessionRegistered.Visible = False
                Me.pnlBtnAgency.Visible = False
                'Me.pnlBtnPrivateClient.Visible = True
                Me.paidTypesAdmited.Value = Me.bookData.bookOperator.forma_pago
                Exit Sub
            End If

            Select Case Me.bookData.bookOperator.tipoOperador
                Case CFRConst.ClientTypeEnum.PrivateClient, CFRConst.ClientTypeEnum.Company
                    'Me.pnlBtnPrivateClient.Visible = True
                    Me.pnlBtnAgency.Visible = False
                Case CFRConst.ClientTypeEnum.HotelAgency
                    'Me.pnlBtnPrivateClient.Visible = False
                    Me.pnlBtnAgency.Visible = True
                    Dim dtListCompanies As Data.DataTable = cfr.OperadorManager.ListCompaniesWorkingWithAgency(Me.bookData.hotel.id, Me.bookData.bookOperator.id)
                    'Me.cboCompaniesForAgency.Items.Clear()
                    'For Each dr As Data.DataRow In dtListCompanies.Rows
                    '    Me.cboCompaniesForAgency.Items.Add(New ListItem(dr("Operador"), dr("cod_Empresa")))

                    'Next
            End Select

            Me.paidTypesAdmited.Value = Me.bookData.bookOperator.forma_pago

        End If
    End Sub


#Region "Cookies"
    Public Sub saveDataToCookies()

        Dim objCookie As HttpCookie = Me.Request.Cookies(cookiename)
        If objCookie Is Nothing Then
            objCookie = New HttpCookie(cookiename)
        Else
            objCookie.Values.Clear()
        End If


        'Dim de As DictionaryEntry
        'For Each de In ht
        '    objCookie.Values.Add(de.Key.ToString(), de.Value.ToString())
        'Next

        'client
        objCookie.Values.Add("CliRes.Id", Me.bookData.CliRes.Id)
        objCookie.Values.Add("CliRes.identification", Me.bookData.CliRes.identification)
        objCookie.Values.Add("CliRes.Nombre", Me.bookData.CliRes.Nombre)
        objCookie.Values.Add("CliRes.Apellido1", Me.bookData.CliRes.Apellido1)
        objCookie.Values.Add("CliRes.direccion", Me.bookData.CliRes.direccion)
        objCookie.Values.Add("CliRes.cod_postal", Me.bookData.CliRes.codpostal)
        objCookie.Values.Add("CliRes.Pais.idEntity", Me.bookData.CliRes.Pais.idEntity)
        objCookie.Values.Add("CliRes.Pais.description", Me.bookData.CliRes.Pais.Description)
        objCookie.Values.Add("CliRes.Tlf", Me.bookData.CliRes.Tlf)
        objCookie.Values.Add("CliRes.fax", Me.bookData.CliRes.fax)
        objCookie.Values.Add("CliRes.Zone", Me.bookData.CliRes.Zone)
        objCookie.Values.Add("CliRes.email", Me.bookData.CliRes.email)
        objCookie.Values.Add("CliRes.acceptlopd", Me.aceptolopd.Checked)

        'company
        objCookie.Values.Add("Empresa.Id", Me.bookData.EmpresaRes.Id)
        objCookie.Values.Add("Empresa.identification", Me.bookData.EmpresaRes.identification)
        objCookie.Values.Add("Empresa.Nombre", Me.bookData.EmpresaRes.Nombre)
        objCookie.Values.Add("Empresa.direccion", Me.bookData.EmpresaRes.direccion)
        objCookie.Values.Add("Empresa.cod_postal", Me.bookData.EmpresaRes.codpostal)
        objCookie.Values.Add("Empresa.Pais.idEntity", Me.bookData.EmpresaRes.Pais.idEntity)
        objCookie.Values.Add("Empresa.Pais.description", Me.bookData.EmpresaRes.Pais.Description)
        objCookie.Values.Add("Empresa.Tlf", Me.bookData.EmpresaRes.Tlf)
        objCookie.Values.Add("Empresa.fax", Me.bookData.EmpresaRes.fax)
        objCookie.Values.Add("Empresa.Zone", Me.bookData.EmpresaRes.Zone)
        objCookie.Values.Add("Empresa.email", Me.bookData.EmpresaRes.email)



        'Agency
        objCookie.Values.Add("AgenciaRes.Id", Me.bookData.AgenciaRes.Id)
        objCookie.Values.Add("AgenciaRes.identification", Me.bookData.AgenciaRes.identification)
        objCookie.Values.Add("AgenciaRes.Nombre", Me.bookData.AgenciaRes.Nombre)
        objCookie.Values.Add("AgenciaRes.direccion", Me.bookData.AgenciaRes.direccion)
        objCookie.Values.Add("AgenciaRes.cod_postal", Me.bookData.AgenciaRes.codpostal)
        objCookie.Values.Add("AgenciaRes.Pais.idEntity", Me.bookData.AgenciaRes.Pais.idEntity)
        objCookie.Values.Add("AgenciaRes.Pais.description", Me.bookData.AgenciaRes.Pais.Description)
        objCookie.Values.Add("AgenciaRes.Tlf", Me.bookData.AgenciaRes.Tlf)
        objCookie.Values.Add("AgenciaRes.fax", Me.bookData.AgenciaRes.fax)
        objCookie.Values.Add("AgenciaRes.Zone", Me.bookData.AgenciaRes.Zone)
        objCookie.Values.Add("AgenciaRes.email", Me.bookData.AgenciaRes.email)
        objCookie.Values.Add("AgenciaRes.Grupo", Me.bookData.AgenciaRes.Grupo)

        objCookie.Expires = DateTime.Now.AddDays(14)
        Me.Response.Cookies.Add(objCookie)
    End Sub

    Public Sub getCookiesClient()

        Dim objCookie As HttpCookie = Me.Request.Cookies(cookiename)
        If objCookie Is Nothing Then
            Exit Sub
        End If
        Me.bookData.CliRes.Id = objCookie("CliRes.id")
        Me.bookData.CliRes.identification = objCookie("CliRes.identification")
        Me.bookData.CliRes.Nombre = objCookie("CliRes.Nombre")
        Me.bookData.CliRes.Apellido1 = objCookie("CliRes.Apellido1")
        Me.bookData.CliRes.direccion = objCookie("CliRes.direccion")
        Me.bookData.CliRes.codpostal = objCookie("CliRes.cod_postal")
        Me.bookData.CliRes.Pais.idEntity = objCookie("CliRes.Pais.idEntity")
        Me.bookData.CliRes.Pais.Description = objCookie("CliRes.Pais.description")
        Me.bookData.CliRes.Tlf = objCookie("CliRes.Tlf")
        Me.bookData.CliRes.fax = objCookie("CliRes.fax")
        Me.bookData.CliRes.Zone = objCookie("CliRes.Zone")
        Me.bookData.CliRes.email = objCookie("CliRes.email")

        Dim acceptLOPD As String = objCookie("CliRes.acceptlopd")
        If Not acceptLOPD Is Nothing Then
            If acceptLOPD.Trim <> "" Then
                If CType(acceptLOPD, Boolean) = True Then
                    Me.aceptolopd.Checked = True
                End If
            End If
        End If


    End Sub

    Public Sub getCookiesCompany()
        Dim objCookie As HttpCookie = Me.Request.Cookies(cookiename)
        If objCookie Is Nothing Then
            Exit Sub
        End If
        'company
        Me.bookData.EmpresaRes.Id = objCookie("Empresa.Id")
        Me.bookData.EmpresaRes.identification = objCookie("Empresa.identification")
        Me.bookData.EmpresaRes.Nombre = objCookie("Empresa.Nombre")
        Me.bookData.EmpresaRes.direccion = objCookie("Empresa.direccion")
        Me.bookData.EmpresaRes.codpostal = objCookie("Empresa.cod_postal")
        Me.bookData.EmpresaRes.Pais.idEntity = objCookie("Empresa.Pais.idEntity")
        Me.bookData.EmpresaRes.Pais.Description = objCookie("Empresa.Pais.description")
        Me.bookData.EmpresaRes.Tlf = objCookie("Empresa.Tlf")
        Me.bookData.EmpresaRes.fax = objCookie("Empresa.fax")
        Me.bookData.EmpresaRes.Zone = objCookie("Empresa.Zone")
        Me.bookData.EmpresaRes.email = objCookie("Empresa.email")


    End Sub

    Public Sub getCookiesAgency()
        Dim objCookie As HttpCookie = Me.Request.Cookies(cookiename)
        If objCookie Is Nothing Then
            Exit Sub
        End If
        'agency
        Me.bookData.AgenciaRes.Id = objCookie("AgenciaRes.Id")
        Me.bookData.AgenciaRes.identification = objCookie("AgenciaRes.identification")
        Me.bookData.AgenciaRes.Nombre = objCookie("AgenciaRes.Nombre")
        Me.bookData.AgenciaRes.direccion = objCookie("AgenciaRes.direccion")
        Me.bookData.AgenciaRes.codpostal = objCookie("AgenciaRes.cod_postal")
        Me.bookData.AgenciaRes.Pais.idEntity = objCookie("AgenciaRes.Pais.idEntity")
        Me.bookData.AgenciaRes.Pais.Description = objCookie("AgenciaRes.Pais.description")
        Me.bookData.AgenciaRes.Tlf = objCookie("AgenciaRes.Tlf")
        Me.bookData.AgenciaRes.fax = objCookie("AgenciaRes.fax")
        Me.bookData.AgenciaRes.Zone = objCookie("AgenciaRes.Zone")
        Me.bookData.AgenciaRes.email = objCookie("AgenciaRes.email")
        Me.bookData.AgenciaRes.Grupo = objCookie("AgenciaRes.Grupo")
    End Sub
#End Region


#Region "Required validators"
    Protected Sub CheckBoxRequired_ServerValidate(sender As Object, e As ServerValidateEventArgs)
        e.IsValid = Me.aceptolopd.Checked
    End Sub
    Protected Sub booking_ServerValidate(sender As Object, e As ServerValidateEventArgs)
        'e.IsValid = Me.aceptolopd.Checked
    End Sub
#End Region




    ''' <summary>
    ''' fill the book data with search criteria from a GET request
    ''' </summary>
    ''' <param name="req"></param>
    ''' <remarks></remarks>
    Public Sub getSearchCriteriaByRequest(ByVal req As System.Collections.Specialized.NameValueCollection)

        Dim dateIn As Date, dateOut As Date
        Dim dateInYear As Integer, dateInMonth As Integer, dateInDay As Integer
        Dim dateOutYear As Integer, dateOutMonth As Integer, dateOutDay As Integer
        Dim adults As Integer, payingChildren As Integer, freeChildren As Integer, regimen As Integer
        Try
            If Not Integer.TryParse(req(REQCte.forms.dateInYear), dateInYear) OrElse
                Not Integer.TryParse(req(REQCte.forms.dateInMonth), dateInMonth) OrElse
                Not Integer.TryParse(req(REQCte.forms.dateInDay), dateInDay) OrElse
                Not Integer.TryParse(req(REQCte.forms.dateOutYear), dateOutYear) OrElse
                Not Integer.TryParse(req(REQCte.forms.dateOutMonth), dateOutMonth) OrElse
                Not Integer.TryParse(req(REQCte.forms.dateOutDay), dateOutDay) Then
                Throw New FormatException("All booking date parts are required and must be numeric.")
            End If
            dateIn = New Date(dateInYear, dateInMonth, dateInDay)
            dateOut = New Date(dateOutYear, dateOutMonth, dateOutDay)
        Catch ex As Exception
            boe.errBook = basERR.E_FAIL_VerifyDate
            Throw New cfr.exceptions.BookException("Invalid external booking dates.")
        End Try

        If dateOut <= dateIn Then
            Throw New cfr.exceptions.BookException("Checkout must be after check-in.")
        End If
        If Not Integer.TryParse(req(REQCte.forms.numAdultos), adults) OrElse adults < 1 OrElse adults > 5 Then
            Throw New cfr.exceptions.BookException("Adults must be between 1 and 5.")
        End If
        If Not Integer.TryParse(req(REQCte.forms.ninosPago), payingChildren) OrElse payingChildren < 0 OrElse payingChildren > 5 Then
            Throw New cfr.exceptions.BookException("Paying children must be between 0 and 5.")
        End If
        If Not Integer.TryParse(req(REQCte.forms.ninosfree), freeChildren) OrElse freeChildren < 0 OrElse freeChildren > 2 Then
            Throw New cfr.exceptions.BookException("Free children must be between 0 and 2.")
        End If
        If Not Integer.TryParse(req(REQCte.forms.regimen), regimen) OrElse regimen < 1 OrElse regimen > 5 Then
            Throw New cfr.exceptions.BookException("Board basis must be between 1 and 5.")
        End If

        Me.bookData.setDates(dateIn, dateOut, True)
        Me.bookData.numAdultos = adults
        Me.bookData.ninospago = payingChildren
        Me.bookData.ninosfree = freeChildren
        Me.bookData.regimen = CType(regimen, CFRConst.RegimenEnum)
    End Sub

    ''' <summary>
    ''' fill the book data with search criteria from a POST
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub getSearchCriteriaByPage()

        Dim dateIn As Date, dateOut As Date
        Try
            dateIn = DateTime.Parse(Me.checkindate.Text)
            dateOut = DateTime.Parse(Me.checkoutdate.Text)

            If Me.holdprop.Tipo = CFRConst.unitTypeEnum.car Then
                dateIn = New DateTime(dateIn.Year, dateIn.Month, dateIn.Day, CInt(Me.cbotimecheckin.SelectedValue), 0, 0, 0)
                dateOut = New DateTime(dateOut.Year, dateOut.Month, dateOut.Day, CInt(Me.cbotimecheckout.SelectedValue), 0, 0, 0)
            End If

        Catch ex As Exception
            boe.errBook = basERR.E_FAIL_VerifyDate
            Throw New Exception(basERR.E_FAIL_VerifyDate)
        End Try

        Me.bookData.setDates(dateIn, dateOut, True)

        Dim AdultsCount As Integer = CType(Me.f_numAdultos.Text, Integer)
        Dim ChildPayCount As Integer = CType(Me.f_ninosPago.Text, Integer)

        If ChildPayCount > 2 Then 'if there are more child pay then add as adults
            AdultsCount = AdultsCount + ChildPayCount - 2
            ChildPayCount = 2
        End If

        Me.bookData.numAdultos = AdultsCount
        Me.bookData.ninospago = ChildPayCount
        Me.bookData.ninosfree = CType(Me.f_ninosfree.Text, Integer)
        Me.bookData.regimen = CType(Me.f_regimen.Text, CFRConst.RegimenEnum)

        Me.bookData.promotionCode = Me.txtpromotioncode.Text.Trim
        
        Me.lblnightsCalendar.Text = Me.lblNights.Text

        'labels summary
        Me.lblDateIn.Text = Me.bookData.dateIn.ToLongDateString
        Me.lblDateOut.Text = Me.bookData.dateOut.ToLongDateString
        Me.lblNights.Text = Me.bookData.bookTotalDays.ToString
        Me.lblNumAdults.Text = AdultsCount.ToString
        Me.lblNumChildPay.Text = ChildPayCount.ToString
        Me.lblNumChildFree.Text = Me.bookData.ninosfree.ToString
        
        saveSearchCriteriaToViewstate(Me.bookData)

    End Sub


#Region "User selection critera Viewstate"

    Private Sub saveSearchCriteriaToViewstate(bk As BookRes)
        'save to viewstate
        ViewState("datein") = bk.dateIn
        ViewState("dateout") = bk.dateOut
        ViewState("regimenVS") = CInt(bk.regimen)
        ViewState("numAdultosVS") = bk.numAdultos
        ViewState("numChildPayVS") = bk.ninospago
        ViewState("numChildFreeVS") = bk.ninosfree
        ViewState("promotionCodeVS") = bk.promotionCode
    End Sub

    Private Sub getSearchCriteriaFromViewstate(bk As BookRes)
        'get from viewstate
        Dim d1 As Date = ViewState("datein")
        Dim d2 As Date = ViewState("dateout")
        bk.setDates(d1, d2)
        bk.regimen = ViewState("regimenVS")
        bk.numAdultos = ViewState("numAdultosVS")
        bk.ninospago = ViewState("numChildPayVS")
        bk.ninosfree = ViewState("numChildFreeVS")
        bk.promotionCode = ViewState("promotionCodeVS")

    End Sub

#End Region


End Class
