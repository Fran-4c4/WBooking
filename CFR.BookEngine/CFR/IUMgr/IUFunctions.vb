Imports System.Text
Imports Microsoft.VisualBasic


Namespace cfr

    ''' <summary>
    ''' class to show book confirmation data to user in html 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class IUFunctions

        Dim myTheme As cfr.SiteThemeObj
        Dim dr As cfr.BookRes
        Dim sh As String = ""
        ''' <summary>
        ''' must be set when the property is set to know the language set to use
        ''' </summary>
        ''' <remarks></remarks>
        Public langCaption As New cfr.languagePageManager

        Dim styCab1 As String = ""
        Dim styTit As String = " style='font-weight: bold;'"

        Dim tbopenOuter As String = ""
        Dim tbopen_basic As String = ""

        ''' <summary>
        ''' end of table
        ''' </summary>
        Dim tbclose As String = "</table>" & Chr(13) & Chr(10)
        ''' <summary>
        ''' carry return as br
        ''' </summary>
        ''' <remarks></remarks>
        Dim tagBr As String = "<br/>" & Chr(13) & Chr(10)

        Dim carryRet As String = String.Empty

        Dim colN As Byte = 4

        Public showOptions As New EmailShowOptions

        Public bookForStyle As MailBookStyle = MailBookStyle.hotel


        Public Enum MailBookStyle As Byte
            ''' <summary>
            ''' send to client
            ''' </summary>
            client = 2
            ''' <summary>
            ''' send to hotel
            ''' </summary>
            hotel = 1
            clientCancel = 3
        End Enum


        Public Sub New()

        End Sub
        Public Sub New(ByVal myTheme As cfr.SiteThemeObj, bookData As cfr.BookRes, iuOption As EmailShowOptions)
            Me.showOptions = iuOption
            config(myTheme, bookData)
        End Sub


        Public Sub New(ByVal myTheme As cfr.SiteThemeObj, bookData As cfr.BookRes)
            config(myTheme, bookData)
        End Sub

        Public Sub config(ByVal myTheme As cfr.SiteThemeObj, bookData As cfr.BookRes)
            Me.dr = bookData
            Me.langCaption.holdproperty = bookData.hotel.Tipo
#If DEBUG Then
            carryRet = Chr(13) & Chr(10)
#End If
            Me.myTheme = myTheme

            configStyle()

        End Sub

        Public Sub configStyle()
            Dim tableStyle As String = ""
            tableStyle = "style='border: 1px solid #" & Me.myTheme.colorPrincipal & "; border-collapse:separate;"
            'tableStyle += "margin-left: 3px;"
            'tableStyle += "margin-right: 3px;"
            tableStyle += "margin-bottom: 10px;"
            tableStyle += "margin-top: 10px;"
            'tableStyle += "padding: 5px;"
            tableStyle += "width: 100%;"
            tableStyle += "border-spacing: 2px;"
            tableStyle += "background-color: white;" 'important when background is not white or light
            tableStyle += "'"
            Me.styCab1 = "style='background-color: #" & Me.myTheme.colorPrincipal & "  ; color: #" & Me.myTheme.colorPrincipalFont & "; font-FAMILY: Arial; font-size: 14px; font-weight: bold; PADDING: 2px;'"
            Me.tbopen_basic = "<table class='conf-table' " & tableStyle & " cellspacing='4px' cellpadding='4px' {0}>" & Chr(13) & Chr(10)

            'Me.tbopenOuter = "<table class='conf-table-outer' " & tableStyle & " cellspacing='4px' cellpadding='4px' itemscope itemtype='http://schema.org/LodgingReservation'>" & Chr(13) & Chr(10)
            If Me.showOptions.showExternalBorder = True Then
                Me.tbopenOuter = "<table class='conf-table-outer' " & tableStyle & " cellspacing='4px' cellpadding='4px'>" & Chr(13) & Chr(10)
            Else
                Me.tbopenOuter = "<table class='conf-table-outer' cellspacing='0px' cellpadding='0px'>" & Chr(13) & Chr(10)
            End If
        End Sub

#Region " table open"


        Private Function tbopen_1(Optional itemprop As String = "") As String
            If itemprop <> "" Then
                Dim withItemprop As String = String.Format(Me.tbopen_basic, itemprop)
                Return withItemprop
            Else
                Return String.Format(Me.tbopen_basic, "")
            End If

        End Function
#End Region
        
  

        'Public Shared Function sendBookConfirmation(ByVal dr As cfr.BookRes) As String
        '    'TODO: regimen
        '    'mdescregimen = getdescRegimen(regimen, Me.langCaption.getCaption)
        '    Dim cfrIU As New cfr.IUFunctions(dr.site.theme)
        '    Return cfr.IUFunctions.getCompositeHtmlForMail("RESERVA-BOOK", cfrIU.getHtmlBook(dr))
        'End Function

        Public Shared Function getCompositeHtmlForMail(ByVal titleHtml As String, ByVal bodyHtml As String) As String
            Dim sHtml As String

            'TODO: regimen
            'mdescregimen = getdescRegimen(regimen, Me.langCaption.getCaption)
            sHtml = ""
            sHtml = "<html><head>" & Chr(13) & Chr(10)
            sHtml = "<TITLE>" & titleHtml & "</TITLE>" & Chr(13) & Chr(10)
            sHtml += "<meta content='text/html; charset=utf-8' http-equiv='Content-Type'>" & Chr(13) & Chr(10)
            sHtml += "<style type= 'text/css'>a:link{font-family:Arial;color:navy;font-size:12px;text-decoration:none;font-weight:bold;}" & Chr(13) & Chr(10)
            sHtml += "a:hover{font-family:Arial;text-decoration:none;color:orange;font-weight:bold;}" & Chr(13) & Chr(10)
            sHtml += "a:visited{font-family:Arial;color:navy;font-size:12px;text-decoration:none;font-weight:bold;}" & Chr(13) & Chr(10)
            sHtml += "a:active{font-family:Arial;text-decoration:none;color:blue;font-weight:bold;}" & Chr(13) & Chr(10)
            sHtml += " .avisoR{  font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold; color: #CC0000}" & Chr(13) & Chr(10)
            'TODO: colocar clases de correo
            'sHtml += vClasesCorreo
            sHtml += "</style></head>" & Chr(13) & Chr(10)

            sHtml += "<body>" & Chr(13) & Chr(10)
            sHtml += bodyHtml

            sHtml += "</body></html>" & Chr(13) & Chr(10)

            Return sHtml

        End Function

        Private Enum sectionEmailEnum As Byte
            msgconfirmation = 0
            hotelData = 1
            cancelBookBody = 2
            paysection = 4
            googleSection = 5
        End Enum

        Private Function showBy(section As sectionEmailEnum) As Boolean
            Select Case section
                Case sectionEmailEnum.msgconfirmation
                    Select Case Me.bookForStyle
                        Case MailBookStyle.client
                            Return True
                        Case Else
                            Return False
                    End Select

                Case sectionEmailEnum.hotelData
                    'validateSection(New Byte() {1, 2})
                    Select Case Me.bookForStyle
                        Case MailBookStyle.client, MailBookStyle.clientCancel
                            Return True
                        Case Else
                            Return False
                    End Select

                Case sectionEmailEnum.cancelBookBody
                    Select Case Me.bookForStyle
                        Case MailBookStyle.client, MailBookStyle.clientCancel
                            Return True
                        Case Else
                            Return False
                    End Select


                Case sectionEmailEnum.paysection
                    Select Case Me.bookForStyle
                        Case MailBookStyle.client
                            Return True
                        Case Else
                            Return False
                    End Select

                Case sectionEmailEnum.googleSection
                    Select Case Me.bookForStyle
                        Case MailBookStyle.client, MailBookStyle.clientCancel
                            Return True
                        Case Else
                            Return False
                    End Select


            End Select
        End Function

        Private Function validateSection(showStyle() As Byte) As Boolean
            For x As Integer = 0 To showStyle.Length - 1
                If Me.bookForStyle = x Then
                    Return True
                End If
            Next

            Return False
        End Function


        Public Function getGoogleLodgingReservation() As String
            If showBy(sectionEmailEnum.cancelBookBody) = False Then
                Return ""
            End If
            Dim sg As String = ""
            Try

     
            Dim urlCancel As String = cfr.security.CancellationTokenService.BuildCancellationUrl(dr)

            sg += "<script type=""application/ld+json"">"
            sg += "{"
            sg += """@context"" : ""http://schema.org"", ""@type"" : ""LodgingReservation"","
            sg += String.Format("""reservationNumber"" : ""{0}"",", Me.dr.idEntity)
            sg += """reservationStatus"": ""http://schema.org/Confirmed"","
                sg += String.Format("""cancelReservationUrl"": ""{0}"",", urlCancel)
                sg += """underName"" : {"
                sg += """@type"" : ""Person"","
                sg += String.Format("""name"" : ""{0}""", Me.dr.CliRes.Apellido1 & " " & Me.dr.CliRes.Nombre)
                'sg += String.Format("""email"" : ""{0}", Me.dr.CliRes.email)
                sg += " },"

            sg += """reservationFor"" : {"
            sg += """@type"" : ""LodgingBusiness"","
            sg += String.Format("""name"" : ""{0}"",", dr.hotel.hotelName)
            sg += """address"" : {"
            sg += """@type"" : ""PostalAddress"","
            sg += String.Format("""streetAddress"" : ""{0}"",", dr.hotel.Direccion)
            sg += String.Format("""addressLocality"" : ""{0}"",", "Corralejo")
            sg += String.Format("""addressRegion"" : ""{0}"",", "Fuerteventura")
            sg += String.Format("""addressCountry"" : ""{0}"",", "España")
            sg += String.Format("""postalCode"" : ""{0}""", Me.dr.hotel.codpostal)
            sg += "},"
            sg += String.Format("""telephone"": ""{0}""", Me.dr.hotel.Telefono)
            sg += "},"

            'sg += String.Format("""price"" : ""{0}"",", Me.dr.)
                sg += String.Format("""checkinDate"" : ""{0}"",", dr.dateIn.ToString("yyyy-MM-dd") & "T12:00:00")
            sg += String.Format("""checkoutDate"" : ""{0}""", dr.dateOut.ToString("yyyy-MM-dd") & "T12:00:00")
            sg += "}"
            sg += "</script>"


                Return sg
            Catch ex As Exception
                Throw New Exception("Error configuring Google Email Schema", ex)
            End Try
        End Function


        Public Function getHtmlBook() As String
            sh = "" 'reset field 

            If dr.idEntity = 0 Then
                Throw New Exception(basERR.E_GetReservByLocalizador)
            End If

            sh += Me.getGoogleLodgingReservation
            'principal table container
            sh += "<div id='bookingCFR'>" & carryRet

            'open base table
            sh += tbopenOuter
            sh += "<TR ><TD>" & carryRet

            If showBy(sectionEmailEnum.msgconfirmation) Then
                'hotel thanks message
                If dr.hotel.BookParam.BookConfirmationMessage <> "" Then
                    sh += "<B style='font-size: 12px;'>" & dr.hotel.BookParam.BookConfirmationMessage & "</B>" & carryRet
                End If
            End If
            If dr.site.description <> "" Then
                'hotel description principal
                sh += "<B style='font-size: 18px;'>" & dr.site.description & "</B>" & carryRet
            End If

            'header
            sh += tbopen_1
            sh += "<TR ><TD>" & carryRet
            sh += "<B style='font-size: 16px;'>" & Me.langCaption.GetCaption(152) & "</B>" & carryRet
            sh += "<B style='font-size: 18px;' itemprop='reservationNumber'>" & dr.idEntity & "</B> <span itemprop='bookingTime' style='margin-left:20px;'>" & dr.bookDate.ToLongDateString & " " & dr.bookDate.ToShortTimeString & "</span>"
            sh += "</td></tr>" & carryRet
            sh += tbclose

            If showBy(sectionEmailEnum.hotelData) Then
                'hotel description
                sh += tbopen_1(" itemprop='reservationFor' itemscope itemtype='http://schema.org/LodgingBusiness'")
                'sh += tbopen_1()
                sh += IUFunctions.getRTit(Me.langCaption.GetCaption(317), " colspan=2 ", styCab1)
                sh += IUFunctions.getRF(Me.langCaption.GetCaption(318), "<B style='font-size: 16px;'><span itemprop='reservationFor'>" & dr.hotel.hotelName & "</span></B>")
                Dim addresslink As String = String.Format("<a rel='nofollow' href=""https://maps.google.com/?q={0}"" target='_blank'><span itemprop='streetAddress'>{0}</span></a>", dr.hotel.Direccion)
                sh += IUFunctions.getRF(Me.langCaption.GetCaption(108), addresslink)
                sh += IUFunctions.getRF(Me.langCaption.GetCaption(107), "<span itemprop='telephone'>" & dr.hotel.Telefono & "</span>")
                sh += IUFunctions.getRF(Me.langCaption.GetCaption(374), dr.hotel.Email)
                sh += tbclose
            End If

            'book data
            Dim icalStr As String = "<A rel='nofollow' target='googlecal' HREF='https://www.google.com/calendar/render?action=TEMPLATE&text=[Your+Event+Name]&dates=[date+in]/[date+out]&details=[details+body]&location=[book+location]&sf=true&output=xml'>Google</a>"
            icalStr = icalStr.Replace("[Your+Event+Name]", dr.hotel.hotelName.Replace(" ", "+") & "++" & Me.langCaption.GetCaption(11).Replace(" ", "+") & "+" & dr.idEntity)
            icalStr = icalStr.Replace("[date+in]", dr.dateIn.ToString("yyyyMMdd") & "T110000Z")
            icalStr = icalStr.Replace("[date+out]", dr.dateOut.ToString("yyyyMMdd") & "T110000Z")
            'Dim comments As String = dr.comentarios.Replace(" ", "+")
            icalStr = icalStr.Replace("[details+body]", dr.hotel.hotelName.Replace(" ", "+"))

            icalStr = icalStr.Replace("[book+location]", dr.hotel.Direccion)

            sh += tbopen_1
            sh += IUFunctions.getRTit(Me.langCaption.GetCaption(11), " colspan=2 ", styCab1)
            If showOptions.showDates = True Then
                If dr.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then
                    Dim checkintime As String = " desde " & dr.hotel.checkintime1
                    If dr.hotel.checkintime2 <> "" Then
                        checkintime += " hasta " & dr.hotel.checkintime2
                    End If
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(81), "<span itemprop='checkinDate'>" & dr.dateIn.ToLongDateString & "</span> " & checkintime)

                    Dim checkouttime As String = " hasta " & dr.hotel.checkOuttime2
                    If dr.hotel.checkOuttime1 <> "" Then
                        checkouttime = " desde " & dr.hotel.checkOuttime1 & checkouttime
                    End If
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(82), "<span itemprop='checkoutDate'>" & dr.dateOut.ToLongDateString & "</span>" & checkouttime)

                ElseIf dr.hotel.Tipo = CFRConst.unitTypeEnum.car Then
                    Dim ctx As New cfrbookEntities
                    Dim langSelected As String = Language.CurrentCulture.TwoLetterISOLanguageName

                    Dim queryDef As R_UnitLocation_Lang = (From l In ctx.R_UnitLocation_Lang Join t In ctx.R_UnitLocation On t.id_unit_location Equals l.id_unit_location
                        Where t.id_unit_location = dr.carLocationSelected.unit_location_GET And l.id_Lang = langSelected Select l).FirstOrDefault


                    Dim locationGet As String = queryDef.Description

                    queryDef = (From l In ctx.R_UnitLocation_Lang Join t In ctx.R_UnitLocation On t.id_unit_location Equals l.id_unit_location
                                            Where t.id_unit_location = dr.carLocationSelected.unit_location_Return And l.id_Lang = langSelected Select l).FirstOrDefault
                    Dim locationRet As String = queryDef.Description

                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(81), "<span itemprop='checkinDate'>" & dr.dateIn.ToLongDateString & "  - " & dr.dateIn.ToShortTimeString & "</span>")
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(383), "<span>" & locationGet & "</span>")
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(82), "<span itemprop='checkoutDate'>" & dr.dateOut.ToLongDateString & "  - " & dr.dateOut.ToShortTimeString & "</span>")
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(384), "<span>" & locationRet & "</span>")

                Else
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(81), "<span itemprop='checkinDate'>" & dr.dateIn.ToLongDateString & "</span>")
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(82), "<span itemprop='checkoutDate'>" & dr.dateOut.ToLongDateString & "</span>")
                End If

                If showBy(sectionEmailEnum.hotelData) Then
                    sh += IUFunctions.getRF("Añadir al calendario", icalStr)
                End If

            Else
                sh += IUFunctions.getRF(Language.GetCaption(81), "Atencion: Esta informacion esta desactivada. Contacte con el administrador del hotel.")
            End If

            If Me.dr.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then
                sh += IUFunctions.getRF(Me.langCaption.GetCaption(14), dr.numAdultos)
                If dr.ninosfree = 0 And dr.ninospago = 0 Then
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(15), "0")
                Else
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(15) & "<" & dr.hotel.BookParam.childAgeFree & " " & Me.langCaption.GetCaption(16), dr.ninosfree)
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(15) & "<" & dr.hotel.BookParam.childAgeToPay & " " & Me.langCaption.GetCaption(16), dr.ninospago)
                End If
            End If



            sh += "<tr><td colspan=2>" & carryRet

            ''*****rooms selected & price
            GetRoomSelectedBlock()

            sh += "</td></tr>" & carryRet

            sh += "<tr><td colspan=2>" & carryRet
            ''*****COMMENTS
            GetCommentsBlock()


            sh += "</td></tr>" & carryRet

            sh += "<tr><td colspan=2>" & carryRet

            ''*****Client Data
            GetClientDataBlock()
            sh += "</td></tr>" & carryRet

            sh += tbclose 'close book data


            ''*****block for company
            getCompanyBlock(dr.EmpresaRes)
            ''*****agency data
            If dr.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.HotelAgency Then
                sh = GetAgencyBlock(dr.AgenciaRes)
            End If

            '***** FORMA DE PAGO
            GetPayTypeBlock()

            ''*****cancel policy 
            GetCancelPolicyBlock()

            ''*****price desglosed by day
            GetDesglosedPrice()


            sh += IUFunctions.getPoweredFooter()

            'principal container closed
            sh += "</td></tr>" & carryRet
            sh += tbclose

            sh += "</div >"

            Return sh
        End Function

        Private Function GetDesglosedPrice() As String

            sH += tbopen_1
            sH += IUFunctions.getRTit(Me.langCaption.GetCaption(216), " colspan=2 ", styCab1)
            For Each bo As BookedRoom In dr.roomsSelected.Values
                sH += "<tr>"
                sH += "<td noWrap width=150px style='background-color: #EEEEEE;'>" & bo.roomSelected.description.getDes(dr.language) & "</td>" & carryRet
                sH += "<td noWrap style='background-color: #EEEEEE;'>" & bo.priceFinalForAllDays & dr.moneda & "</td>" & carryRet
                sH += "</tr>" & carryRet
                sH += "<tr><td colspan=2 > "
                sH += Me.getDesglosedPriceInfo(dr.dateIn, bo.priceDesglosed, bo.RegimenDesglosed)
                sH += "</td></tr>" & carryRet
            Next
            sH += tbclose
            Return sH
        End Function
        ''' <summary>
        ''' policy block
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetCancelPolicyBlock() As String

            If dr.hotel.BookParam.politicyCancelation <> "" Or dr.bookOperator.politicyBookConditions <> "" _
               Or dr.hotel.BookParam.politicyBookConditions <> "" _
               Or dr.hotel.BookParam.politicyCancelation <> "" Then
                sh += tbopen_1()
                sh += IUFunctions.getRTit(Me.langCaption.GetCaption(140), "", styCab1)

                sh += "<tr><td>"

                'book conditions/remarks/info
                If dr.bookOperator.politicyBookConditions <> "" Or dr.hotel.BookParam.politicyBookConditions <> "" Then
                    sh += "<b><u>" & Me.langCaption.GetCaption(321) & "</u></b><br/>" & carryRet

                    If dr.bookOperator.politicyBookConditions <> "" Then
                        sh += dr.bookOperator.politicyBookConditions & "<BR/>" & carryRet
                    End If

                    If dr.hotel.BookParam.politicyBookConditions <> "" Then
                        sh += dr.hotel.BookParam.politicyBookConditions & "<BR/>" & carryRet
                    End If

                End If


                'cancel policiy
                If dr.hotel.BookParam.politicyCancelation <> "" Then
                    Dim polCancel As String = dr.hotel.BookParam.politicyCancelation
                    polCancel = Replace(polCancel, Chr(13), "<BR/>")
                    sh += "<b><u>" & Me.langCaption.GetCaption(322) & "</u></b><br/>" & polCancel & "<BR/>" & carryRet
                End If

                If dr.cancelpolicy.chargeuntilday = "-1" Then
                    'show norefund description?
                Else

                    sh += getCancelPolicyMessage(dr.cancelpolicy)
                End If

                sh += "</td></tr>"
                sh += tbclose
                'sH += tagBr
            End If
            Return sH
        End Function

        Private Function GetClientDataBlock() As String

            sh += tbopen_1
            sh += IUFunctions.getRTit(Me.langCaption.GetCaption(164), " colspan=2 ", styCab1)
            sh += IUFunctions.getRF(Me.langCaption.GetCaption(165), dr.CliRes.Nombre & "&nbsp;" & dr.CliRes.Apellido1)
            If Me.dr.hotel.Tipo = CFRConst.unitTypeEnum.car Then
                sh += IUFunctions.getRF(Me.langCaption.GetCaption(375), dr.CliRes.driverAge)
            End If

            If dr.CliRes.Pais.Description <> "" Then
                sh += IUFunctions.getRF(Me.langCaption.GetCaption(126), dr.CliRes.Pais.Description)
            End If

            If showOptions.showEmail = True Then
                If dr.CliRes.Tlf <> "" Then
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(119), dr.CliRes.Tlf)
                End If
                If dr.CliRes.email <> "" Then
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(121), dr.CliRes.email)
                End If
            Else
                sh += IUFunctions.getRF(Language.GetCaption(121), "Atencion: Esta informacion esta desactivada. Contacte con el administrador del hotel.")
            End If
            
            sh += tbclose
            Return sh
        End Function

        Private Function GetCommentsBlock() As String

            sH += tbopen_1
            sH += IUFunctions.getRTit(Me.langCaption.GetCaption(139), "", styCab1)
            If dr.comentarios <> "" Then
                sH += "<tr><TD>" & dr.comentarios & "</TD></tr>" & carryRet
            Else
                sH += "<tr><TD>---</TD></tr>" & carryRet
            End If
            sH += tbclose
            Return sH
        End Function

        Private Function GetRoomSelectedBlock() As String

            Dim totalBookPrice As Double = 0

            sH += tbopen_1
            
            sh += "<thead>"
            sh += IUFunctions.getRTit(Me.langCaption.GetCaption(216), " colspan=4 ", styCab1)
            sH += "<tr>"
            sh += "<td  width=150px style='background-color: #EEEEEE; text-align:center'  noWrap align=middle></TD>" & carryRet
            sH += "<td  width=50px style='background-color: #EEEEEE; text-align:center'  noWrap align=middle>&nbsp;<b>" & Me.langCaption.GetCaption(89) & "</b>&nbsp;</TD>" & carryRet
            sh += "<td  width=50px style='background-color: #EEEEEE; text-align:center'  noWrap align=middle>&nbsp;<b>" & Me.langCaption.GetCaption(110) & "</b>&nbsp;</TD>" & carryRet
            'sH += "<td  width=50px " & styCab1 & "  noWrap align=middle>&nbsp;" & Me.langCaption.getCaption(89) & "&nbsp;</TD>" & carryRet
            'sH += "<td  width=50px " & styCab1 & "  noWrap align=middle>&nbsp;" & Me.langCaption.getCaption(110) & "&nbsp;</TD>" & carryRet
            sh += "<TD>&nbsp;</td>" 'no refund policy
            sh += "</tr>"
            sh += "</thead>"

            sh += "<tbody>"
            For Each bo As BookedRoom In dr.roomsSelected.Values
                totalBookPrice += bo.priceFinalForAllDays
                sh += "<tr>"
                sh += "<td noWrap align=middle  class=rtxtn>" & bo.roomSelected.description.getDes() & "</TD>" & carryRet
                sh += "<td noWrap align=middle>" & bo.priceFinalForAllDays & "&nbsp;" & dr.moneda & "</TD>" & carryRet
                sh += "<td noWrap align=middle><span style='font-size: 10px;'>" & Me.langCaption.GetCaption(111) & "</span></TD>" & carryRet
                If dr.noRefundTariffSelected = False Then
                    sh += "<TD>&nbsp;</td>"
                Else
                    sh += "<TD style='color: red;'>" & Me.langCaption.GetCaption(387) & "</td>"
                End If

                sh += "</tr>"
            Next

            If Me.dr.hotel.Tipo = CFRConst.unitTypeEnum.car Then
                Dim PriceForLocation As Double = dr.getPriceForLocation

                sh += "<tr>"
                sh += "<td noWrap align=middle  class=rtxtn>Cargos por Entrega/Recogida</TD>" & carryRet
                sh += "<td noWrap align=middle>" & PriceForLocation & "&nbsp;" & dr.moneda & "</TD>" & carryRet
                sh += "<td noWrap align=middle><span style='font-size: 10px;'>" & Me.langCaption.GetCaption(111) & "</span></TD>" & carryRet
                sh += "<TD>&nbsp;</td>"
                sh += "</tr>"

                Dim totalBookAndExtras As Double = Math.Round(dr.getPriceForLocation + totalBookPrice, 2)

                sh += "<tr>"
                sh += "<td noWrap class='rtxtn' style='background-color: #EEEEEE; text-align:right'><b>" & Me.langCaption.GetCaption(89) & "</b></TD>" & carryRet
                sh += "<td noWrap align='middle' style='background-color: #EEEEEE; text-align:center'><b>" & totalBookAndExtras & "&nbsp;" & dr.moneda & "</b></TD>" & carryRet
                sh += "<td noWrap align='middle' style='background-color: #EEEEEE; text-align:center'>" & Me.langCaption.GetCaption(111) & "</TD>" & carryRet
                sh += "<TD>&nbsp;</td>"
                sh += "</tr>"

            End If


            sh += "</tbody>"
            sH += tbclose
            Return sH
            'close rooms selected
        End Function

        Private Sub GetPayTypeBlock()

            If showBy(sectionEmailEnum.paysection) = False Then
                Exit Sub
            End If

            sh += tbopen_1
            sh += IUFunctions.getRTit(Me.langCaption.GetCaption(316), " colspan=2 ", styCab1)

            If dr.formapagoRes.forma_pago = CFRConst.formaPago.CreditCard Then 'TARJETA DE CREDITO
                sh += "<tr><TD  colspan=2><B>" & Me.langCaption.GetCaption(135) & ":</B>&nbsp;************" & Right(dr.formapagoRes.creditCard.ntarjeta, 4) & "" & carryRet
                sh += "<BR><B>" & Me.langCaption.GetCaption(136) & ":</B>&nbsp;&nbsp;" & dr.formapagoRes.creditCard.expiricyMonth & "/" & dr.formapagoRes.creditCard.expiricyYear & "" & carryRet
                sh += "<BR><B>" & Me.langCaption.GetCaption(137) & "</B>:" & dr.formapagoRes.creditCard.titular & "</TD></tr>" & carryRet


            ElseIf (dr.formapagoRes.forma_pago = CFRConst.formaPago.Transfer) Then
                sh += "<tr><td  colspan=2><B>" & Me.langCaption.GetCaption(166) & "</B>:&nbsp;" & formatoCuenta(dr.hotel.BookParam.CCC) & "<BR>" & Me.langCaption.GetCaption(155) & ":&nbsp;&nbsp;&nbsp;&nbsp;<B style='font-size:14px;'>" & dr.idEntity & "</B>	"

                'transfer internacional
                sh += "<BR><p><B>" & Me.langCaption.GetCaption(290) & ":</B>" & carryRet
                sh += "<BR><B>IBAN:</B>&nbsp;" & dr.hotel.BookParam.CCCIBAN & "&nbsp;&nbsp;&nbsp;<B>BIC:</B>&nbsp;" & dr.hotel.BookParam.CCCBIC
                sh += "&nbsp;&nbsp;&nbsp;<B>" & Me.langCaption.GetCaption(291) & ":</B>&nbsp;" & dr.hotel.BookParam.CCCIBAN & "&nbsp;&nbsp;&nbsp;<B>" & Me.langCaption.GetCaption(292) & ":</B>&nbsp;" & dr.hotel.BookParam.CCCSucursal & "" & carryRet
                sh += "</p>"
                sh += "</TD></tr>" & carryRet


            ElseIf dr.formapagoRes.forma_pago = CFRConst.formaPago.TPVHotel Then
                sh += "<tr><td  colspan=2>" & carryRet
                sh += "&nbsp;&nbsp;<B>" & Me.langCaption.GetCaption(348) & ":</B>&nbsp;" & dr.tpvData.Ds_Merchant_Order
                sh += "</TD></tr>" & carryRet
            End If

            sh += tbclose

        End Sub

        Private Function GetAgencyBlock(ByVal AgencyBook As cfr.Agencia) As String

            If AgencyBook Is Nothing Then
                Return Nothing
            End If
            sh += tbopen_1
            sh += IUFunctions.getRTit("Datos de la agencia.", " colspan=2 ", styCab1)
            sh += IUFunctions.getRF("Agencia/Agrupacion", AgencyBook.Nombre & "/" & AgencyBook.Grupo)
            sh += IUFunctions.getRF("Direccion", AgencyBook.direccion)
            sh += IUFunctions.getRF("Telefono/Fax", AgencyBook.Tlf & "/" & AgencyBook.fax)
            sh += IUFunctions.getRF(Me.langCaption.GetCaption(124), AgencyBook.codpostal)
            sh += IUFunctions.getRF("Provincia/" & Me.langCaption.GetCaption(126), AgencyBook.Zone & "/" & AgencyBook.Pais.Description)
            sh += IUFunctions.getRF(Me.langCaption.GetCaption(121), AgencyBook.email)
            sh += IUFunctions.getRF(Me.langCaption.GetCaption(258), AgencyBook.Agente)
            sh += tbclose
            'sH += tagBr
            Return sh
            'tipo_operador=TipoCliente1 
        End Function

        Private Function getCompanyBlock(ByVal company As cfr.Company) As String

            If Not company Is Nothing Then
                If company.Nombre <> "" Then
                    sh += tbopen_1
                    sh += IUFunctions.getRTit(Me.langCaption.GetCaption(265), " colspan=2 ", styCab1)
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(265), company.Nombre)
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(266), company.direccion)
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(267), company.Tlf)
                    sh += IUFunctions.getRF(Me.langCaption.GetCaption(268), company.email)
                    sh += tbclose
                    'sH += tagBr
                End If

            End If
            Return sh
        End Function


        Public Shared Function getPoweredFooter(Optional ByVal WithBox As Boolean = False) As String
            Dim shi As String = ""

            shi += "<table width='100%' cellspacing='4' cellpadding='4'>" & Chr(13) & Chr(10)
            shi += "<tr><td >" & Chr(13) & Chr(10)
            shi += "<div style='font-size: 10px; text-align: right;'>" & Chr(13) & Chr(10)
            shi += "Powered by WBooking" & Chr(13) & Chr(10)
            shi += "Copyright 2012-" & Date.Now.Year & Chr(13) & Chr(10)
            shi += "</div>" & Chr(13) & Chr(10)
            shi += "</td></tr>" & Chr(13) & Chr(10)
            shi += "</table>" & Chr(13) & Chr(10)
            'sh += tagBr
            Return shi
        End Function

        ''' <summary>
        ''' cancel links
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getHtmlBodyToCancelBook() As String
            Dim shi As String = ""

            If showBy(sectionEmailEnum.cancelBookBody) Then
                Dim urlCancel As String = cfr.security.CancellationTokenService.BuildCancellationUrl(dr)
                shi += tbopen_1
                shi += IUFunctions.getRTit(Me.langCaption.GetCaption(176), Me.styCab1)
                shi += "<tr><td>" & Chr(13) & Chr(10)
                Dim lnkcancelstyle As String = "display:inline-block;padding:7px 15px;margin-right:10px;background-color:#d44b38;color:#fff;font-size:15px;font-weight:bold;border:solid 1px #c43b28;white-space:nowrap;text-decoration:none"
                shi += String.Format("<a target='_blank' href='{0}' style='{2}'>{1} </a>", urlCancel, Me.langCaption.GetCaption(170), lnkcancelstyle) & Chr(13) & Chr(10)




                shi += "</TD></tr>" & Chr(13) & Chr(10)
                shi += "<tr><td>" & Me.langCaption.GetCaption(171) & "</td></tr>" & Chr(13) & Chr(10)
                shi += tbclose
            End If

            Return shi

        End Function

        Public Shared Function formatoCuenta(ByVal cuentaStr As String) As String
            Dim formatedAccount As String
            Dim entidad As String
            Dim oficina As String
            Dim dc As String
            Dim cuenta As String
            entidad = Mid(cuentaStr, 1, 4)
            oficina = Mid(cuentaStr, 5, 4)
            dc = Mid(cuentaStr, 9, 2)
            cuenta = Mid(cuentaStr, 11, 10)
            formatedAccount = entidad & "." & oficina & "." & dc & "." & cuenta
            Return formatedAccount
        End Function

#Region "format out for book confirmation"
        Private Shared Function getRTit(ByVal tit As String, Optional ByVal colspan As String = "", Optional ByVal style As String = "") As String

            Dim s As String = "<tr><td class='conf-table-title' " & colspan & " " & style & "  HEIGHT=17px>&nbsp;&nbsp;{0}</TD></tr>" & Chr(13) & Chr(10)
            Return String.Format(s, tit)
        End Function
        ''' <summary>
        ''' Get a row with 2 columns for email presentation
        ''' </summary>
        ''' <param name="tit"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function getRF(ByVal tit As String, ByVal value As String) As String
            Dim s As String = "<tr><td width='150px' style='background-color: #EEEEEE; text-align:right'><b>{0}</b>&nbsp;</td><td>&nbsp;{1}</td>" & Chr(13) & Chr(10)
            Return String.Format(s, tit, value)
        End Function


        ''' <summary>
        ''' Get the price and lodge(regimen) desglosed by day
        ''' </summary>
        ''' <param name="fechaIn"></param>
        ''' <param name="priceDesglosed"></param>
        ''' <param name="regimenDesglosed"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getDesglosedPriceInfo(ByVal fechaIn As Date, ByVal priceDesglosed As String, ByVal regimenDesglosed As String) As String

            'CALCULAMOS LAS CABECERAS DE LOS DIAS
            'Matrices de los precios y el regimen
            Dim matPrecios1() As String = Split(priceDesglosed, "|")
            Dim matRegimen1() As String = Split(regimenDesglosed, "|")

            'Construimos la cabecera lateral de los dias Ej:
            'V 19/5
            'S 20/5
            Dim matPrecios As New ArrayList
            Dim matRegimen As New ArrayList
            For x As Integer = 0 To matPrecios1.Length - 1
                If matPrecios1(x).Trim <> "" Then
                    matPrecios.Add(matPrecios1(x))
                    matRegimen.Add(matRegimen1(x))
                End If
            Next
            Dim cabnoches As New ArrayList
            Dim cabNameDias As New ArrayList

            For i As Integer = 0 To matPrecios.Count - 1
                Dim ds As Date = fechaIn.AddDays(i)
                cabnoches.Add(UCase(Left(WeekdayName(DatePart("w", ds, Microsoft.VisualBasic.FirstDayOfWeek.Monday), True, 2), 1)))
                cabNameDias.Add(ds.Day & "/" & ds.Month)
            Next

            'Fin construccion *******************************++


            Dim sHtm As String = String.Empty
            sHtm += "<table border=0 cellpadding=4px cellspacing=4px  WIDTH=400px style='border: 1px solid #CCC;border-spacing: 4px;border-collapse:separate;font-size: 90%;' >"

            sHtm += "<thead><tr>"
            sHtm += "<td colspan=2  WIDTH=60px style='background-color: #EEEEEE;'><b>" & Me.langCaption.GetCaption(217) & "</b></TD>"
            sHtm += "<td WIDTH=80px ALIGN=RIGHT style='background-color: #EEEEEE;'><b>" & Me.langCaption.GetCaption(218) & "</b></TD>"
            If Me.dr.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then
                sHtm += "<td WIDTH=260px ALIGN=CENTER style='background-color: #EEEEEE;'><b>" & Me.langCaption.GetCaption(17) & "</b></TD>"
            End If


            sHtm += "</tr></thead>"
            sHtm += "<tbody>"
            For i As Integer = 0 To matPrecios.Count - 1

                sHtm += "<tr>"
                sHtm += "<td style='background-color: #EEEEEE;'>" & cabnoches(i) & "</TD>"
                sHtm += "<TD>" & cabNameDias(i) & "</TD>"
                sHtm += "<td ALIGN=RIGHT>" & matPrecios(i) & "€</TD>"

                If Me.dr.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then
                    Dim strRegimen As String = cfr.LodgeManager.getdescRegimen(matRegimen(i))
                    sHtm += "<TD align=center>" & strRegimen & "</TD>"
                End If

                sHtm += "</tr>"
            Next
            sHtm += "</tbody>"
            sHtm = sHtm & "</table>"

            Return sHtm

        End Function




#End Region

#Region "Cancel policy conditions by tariff"


        Public Function getCancelPolicyMessage(cancelPolicyObj As R_PolicyCancel) As String

            If (cancelPolicyObj.chargeuntilday = "-1") Then
                Return ""
            End If

            'Dim msg1 As String = "Si cancelas <strong>{mycanceluntil}</strong> pagarás <strong>{myporcentage}</strong> , o <strong>{myporcentage1}</strong> si cancelas despues."
            Dim msg1 As String = Me.langCaption.GetCaption(391)
            'Dim msg3 As String = "<strong>{mycanceluntil}</strong> la cancelación es gratuita. Sino pagarás <strong>{myporcentage1}</strong> si cancelas despues."
            Dim msg3 As String = Me.langCaption.GetCaption(392)
            Dim msgcond As String = ""


            Dim valdayuntiltxt As String = Me.getConditionCancelPolicyMessageLimit1(cancelPolicyObj.chargeuntilday)
            Dim cbocanceluntiltxt As String = Me.getConditionCancelPolicyMessageLimit1Value(cancelPolicyObj.chargeunitldaypercent.ToString)
            Dim valdayaftertxt As String = Me.getConditionCancelPolicyMessageAfterValue(cancelPolicyObj.chargeafterdaypercent.ToString)

            If (cancelPolicyObj.chargeuntilday = "0") Then
                msgcond = msg3
                msgcond = msgcond.Replace("{mycanceluntil}", valdayuntiltxt)
                msgcond = msgcond.Replace("{myporcentage1}", valdayaftertxt)
            Else
                If cancelPolicyObj.chargeunitldaypercent = 0 Then 'cancelacion gratuita
                    msgcond = msg3
                Else
                    msgcond = msg1
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
                'Return String.Format("hasta las {0}:00 del día de llegada", limit)
                Return String.Format(Me.langCaption.GetCaption(395), limit)
            ElseIf limitDay.EndsWith("D") Then
                Dim limit As String = limitDay.Replace("D", "")
                If limit = "1" Then
                    'Return "hasta 1 día antes de la fecha de llegada"
                    Return Me.langCaption.GetCaption(393)

                Else
                    'Return String.Format("hasta {0} días antes de la fecha de llegada", limit)
                    Return String.Format(Me.langCaption.GetCaption(394), limit)
                End If
            End If
            Return Nothing
        End Function

        Private Function getConditionCancelPolicyMessageLimit1Value(limitDayValueToCharge As String) As String
            If limitDayValueToCharge = "0" Then
                'Return "Cancelación Gratuita"
                Return Me.langCaption.GetCaption(396)
            ElseIf limitDayValueToCharge = "1" Then
                'Return "la primera noche"
                Return Me.langCaption.GetCaption(397)
            Else
                'Return String.Format("un {0}% del precio total", limitDayValueToCharge)
                Return String.Format(Me.langCaption.GetCaption(398), limitDayValueToCharge)
            End If
        End Function

        Private Function getConditionCancelPolicyMessageAfterValue(limitDayValueToCharge As String) As String
            If limitDayValueToCharge = "1" Then
                'Return "la primera noche"
                Return Me.langCaption.GetCaption(399)
            Else
                'Return String.Format("un {0}% del precio total", limitDayValueToCharge)
                Return String.Format(Me.langCaption.GetCaption(400), limitDayValueToCharge)
            End If

        End Function
#End Region


    End Class
End Namespace
