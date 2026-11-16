Imports cfr

''' <summary>
''' control with client data
''' </summary>
''' <remarks></remarks>
Partial Class UC_B_ClientData
    Inherits System.Web.UI.UserControl
    ''' <summary>
    ''' object with the info of the booking
    ''' </summary>
    ''' <remarks></remarks>
    Public bookData As cfr.BookRes

    Protected configurated As Boolean = False
    Protected pnlClientDataAllVisible As Boolean = False


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.setCaption()
          
            Me.configureCbos()

        Else


        End If

        Dim mybookpage As BookBasePage = Me.Page
        Me.bookData = mybookpage.bookData

    End Sub

    ''' <summary>
    ''' accesor to the paid webcontrol
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property getpaidTypeControl() As UC_B_Paid
        Get
            Return Me.B_Paid1
        End Get
        Set(ByVal value As UC_B_Paid)
            Me.B_Paid1 = value
        End Set
    End Property

    ''' <summary>
    ''' configure UI based on booking data
    ''' </summary>
    ''' <param name="bookData"></param>
    ''' <remarks></remarks>
    Public Sub configUI(ByVal bookData As cfr.BookRes)
        Me.bookData = bookData

        If Me.configurated = False Then
            Me.configurated = True
        End If

        Me.pnlCompanyData.Visible = False
        Me.pnlClientDataAllVisible = False
        Me.pnlClientDataAll.Visible = Me.pnlClientDataAllVisible
        Me.pnlAgencyData.Visible = False

        'si es un particular, un website o "no definido" presentamos el resto de cajas de datos del cliente.
        'tlf, email, ciudad
        If bookData.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.NotClasified _
            Or bookData.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.PrivateClient _
            Or bookData.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.WebSite Then
            Me.pnlClientDataAllVisible = True
            Me.pnlClientDataAll.Visible = Me.pnlClientDataAllVisible

        End If

        If bookData.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.Company Then

            'bookData.EmpresaRes.Nombre = cfr.tool.Tools.cNullStr(Request.Cookies("empresa").Value)
            'bookData.EmpresaRes.direccion = cfr.tool.Tools.cNullStr(Request.Cookies("empresadireccion").Value)
            'bookData.EmpresaRes.Tlf = cfr.tool.Tools.cNullStr(Request.Cookies("empresatlf").Value)
            'bookData.EmpresaRes.email = cfr.tool.Tools.cNullStr(Request.Cookies("empresaemail").Value)
            'bookData.EmpresaRes.fax = cfr.tool.Tools.cNullStr(Request.Cookies("empresafax").Value)
            Me.pnlCompanyData.Visible = True

        ElseIf bookData.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.HotelAgency Then
            If bookData.tariffForClientCode = CFRConst.bookTypeEnum.resEmpresa Then
                Me.pnlCompanyData.Visible = True
            ElseIf bookData.tariffForClientCode > CFRConst.bookTypeEnum.resIndefinido Then
                'datos del operador registrado del que se obtienen las tarifas y se le hace la reserva
                'TODO: Aqui deberia guardarse en algun lugar el operador al que se le hace la reserva a traves de la agencia.
                Dim tariffOperator As OperatorObj = OperadorManager.GetOperador(CDbl(bookData.tariffForClientCode))
                If Not tariffOperator Is Nothing Then
                    bookData.EmpresaRes.Nombre = tariffOperator.Operador
                End If

                Me.pnlCompanyData.Visible = True
            End If

            Me.pnlAgencyData.Visible = True

        End If

        'book conditions
        If bookData.hotel.BookParam.politicyBookConditions <> "" Or bookData.bookOperator.politicyBookConditions <> "" Then
            'title
            Me.litBookCondition.Visible = True
            Me.litBookCondition.Text = "<b><u>" & cfr.Language.GetCaption(321) & "</u></b><br/>"

            'body
            If bookData.hotel.BookParam.politicyBookConditions <> "" Then
                Me.litBookCondition.Text += Replace(bookData.hotel.BookParam.politicyBookConditions, Chr(13), "")
            End If

            If bookData.bookOperator.politicyBookConditions <> "" Then
                Me.litBookCondition.Text += Replace(bookData.bookOperator.politicyBookConditions, Chr(13), "")
            End If

            Me.litBookCondition.Text += "<br/>"


        End If
        'container
        Me.pnlpolitica.Visible = True

        '----book(conditions)

        'POLITICY Cancelations
        If bookData.hotel.BookParam.politicyCancelation <> "" Then
            'title
            Me.litpoliticaCancel.Visible = True
            Me.litpoliticaCancel.Text = "<b><u>" & cfr.Language.GetCaption(322) & "</u></b><br/>"

            'body
            Me.litpoliticaCancel.Text += Replace(bookData.hotel.BookParam.politicyCancelation, Chr(13), "")
            
            'container
            Me.pnlpolitica.Visible = True
        End If

        Me.configPaidType()

    End Sub

    ''' <summary>
    ''' Config the type of paid the user can use
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configPaidType()
        If bookData.bookOperator.forma_pago = CFRConst.formaPago.TPVHotel _
            Or bookData.bookOperator.forma_pago = CFRConst.formaPago.TPVSystem _
            Or bookData.bookOperator.forma_pago = CFRConst.formaPago.RegisteredCreditCard Then
            Me.pnlPaidType.Visible = False
            Exit Sub

        Else
            Me.B_Paid1.configureUI(Me.bookData)
            Me.pnlPaidType.Visible = True

        End If


    End Sub


    ''' <summary>
    ''' set the labels of the page
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setCaption()
        Me.lblClientLegend.Text = cfr.Language.getCaption(164)
        Me.lblClientName.Text = cfr.Language.getCaptionBy(Language.msgEnum.name)
        Me.lblSurname1.Text = cfr.Language.getCaptionBy(Language.msgEnum.surname)
        Me.requiredName.Text = cfr.Language.getCaption(100)
        Me.regExpName.Text = cfr.Language.getCaption(100)
        Me.requiredSurname.Text = cfr.Language.getCaption(101)
        Me.lblTlf.Text = cfr.Language.getCaptionBy(Language.msgEnum.tlf)
        Me.regexptlf.Text = cfr.Language.getCaption(102)
        Me.reqTlf.Text = cfr.Language.getCaption(102)

        Me.lblEmail.Text = cfr.Language.getCaptionBy(Language.msgEnum.email)
        'Me.lblEmailCheck.Text = cfr.Language.getCaption(122)
        Me.regExpEmail.Text = cfr.Language.getCaption(104)
        Me.ReqEmail.Text = cfr.Language.getCaption(103)
        Me.compEmail.Text = cfr.Language.getCaption(122)

        Me.lblCountry.Text = cfr.Language.getCaptionBy(Language.msgEnum.country)
        Me.reqCountry.Text = cfr.Language.GetCaption(315)

        Me.lblclientbirth.Text = cfr.Language.GetCaptionBy(375)

        'company
        Me.lblCompanyTitle.Text = cfr.Language.getCaption(265)
        Me.lblCompanyName.Text = cfr.Language.getCaptionBy(Language.msgEnum.name)
        Me.reqEmpresa.Text = cfr.Language.getCaptionBy(Language.msgEnum.valueRequired)
        Me.lblCompanyAddress.Text = cfr.Language.getCaptionBy(Language.msgEnum.address)
        Me.ReqEmpresaDireccion.Text = cfr.Language.getCaptionBy(Language.msgEnum.valueRequired)
        Me.lblCompanyTlf.Text = cfr.Language.getCaptionBy(Language.msgEnum.tlf)
        Me.reqEmpresaTlf.Text = cfr.Language.getCaptionBy(Language.msgEnum.valueRequired)
        Me.lblCompanyEmail.Text = cfr.Language.getCaptionBy(Language.msgEnum.email)
        Me.ReqCompanyEmail.Text = cfr.Language.getCaptionBy(Language.msgEnum.valueRequired)

        'agency
        Me.lblAgencyTitle.Text = cfr.Language.getCaption(367)
        Me.lblAgencyName.Text = cfr.Language.getCaptionBy(Language.msgEnum.name)
        Me.reqAgency.Text = cfr.Language.getCaption(100)
        Me.lblAgencyTravelAgent.Text = cfr.Language.getCaption(258)
        Me.reqAgencyTravelAgent.Text = cfr.Language.getCaption(257)

        Me.lblAgencyAddress.Text = cfr.Language.getCaptionBy(Language.msgEnum.address)
        Me.ReqAgencyDireccion.Text = cfr.Language.getCaptionBy(Language.msgEnum.valueRequired)
        Me.lblAgencyCity.Text = cfr.Language.getCaptionBy(Language.msgEnum.city)
        Me.reqAgencyCity.Text = cfr.Language.getCaption(252)
        Me.lblAgencyCountry.Text = cfr.Language.getCaptionBy(Language.msgEnum.country)
        Me.reqAgencyCountry.Text = cfr.Language.getCaption(315)
        Me.lblAgencyTlf.Text = cfr.Language.getCaptionBy(Language.msgEnum.tlf)
        Me.reqAgencyTlf.Text = cfr.Language.getCaptionBy(Language.msgEnum.valueRequired)
        Me.lblAgencyEmail.Text = cfr.Language.getCaptionBy(Language.msgEnum.email)
        Me.ReqAgencyEmail.Text = cfr.Language.getCaption(104)
        Me.lblAgencyFax.Text = cfr.Language.getCaptionBy(Language.msgEnum.fax)
        Me.reqAgencyFax.Text = cfr.Language.getCaption(Language.msgEnum.valueRequired)


    End Sub

    ''' <summary>
    ''' configure combos of the page
    ''' Countries
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configureCbos()
        If Me.cboCountry.Items.Count <= 0 Then
            'If Me.pnlClientDataAllVisible = True And Me.cboCountry.Items.Count = 0 Then
            Dim dt As Data.DataTable = cfr.mgr.CountryMgr.getCountries(CFRConst.LanguageEnum.es)

            cfr.CBOHelper.fillCbo(Me.cboCountry, dt, "cod_pais", "pais")
            Dim ai() As ListItem = New ListItem(Me.cboCountry.Items.Count) {}
            Me.cboCountry.Items.CopyTo(ai, 1)
            ai(0) = New ListItem(cfr.Language.getCaption(320), CInt(cfr.CFRConst.cboEnum.noSelected))
            cfr.CBOHelper.fillCboByArray(Me.cboCountry, ai, "value", "text")

            Me.cboCountry.SelectedValue = cfr.CFRConst.cboEnum.noSelected
            'End If
        End If

        With Me.cboclientbirth.Items
            Dim year As String = cfr.Language.GetCaption(16)
            For x As Integer = 23 To 75
                .Add(New ListItem(x & " " & year, x))
            Next

        End With
        Me.cboclientbirth.SelectedValue = 21

    End Sub

    ''' <summary>
    ''' fill the client object with the data in page
    ''' </summary>
    ''' <param name="bookData"></param>
    ''' <remarks></remarks>
    Public Sub getClientDataForBook(ByRef bookData As BookRes)
        With bookData
            .CliRes.Nombre = Me.clientname.Text
            .CliRes.Apellido1 = Me.clientsurname.Text
            .CliRes.email = Me.clientemail.Text
            .CliRes.Tlf = Me.clientphone.Text
            .CliRes.Pais.Description = Me.cboCountry.SelectedItem.Text
            .CliRes.Pais.idEntity = Me.cboCountry.SelectedItem.Value
            .comentarios = Me.comentarios.Text
            .CliRes.driverAge = CByte(Me.cboclientbirth.SelectedValue)
            With .EmpresaRes
                .Nombre = "" & Me.empresa.Text
                .direccion = "" & Me.empresadireccion.Text
                .email = "" & Me.empresaemail.Text
                .Tlf = "" & Me.empresaTlf.Text
            End With

            With .AgenciaRes
                .Nombre = "" & Me.Agency.Text
                .direccion = "" & Me.Agencydireccion.Text
                .Pais.Description = "" & Me.AgencyCountry.Text
                .email = "" & Me.Agencyemail.Text
                .Tlf = "" & Me.AgencyTlf.Text
                .fax = "" & Me.AgencyFax.Text
                .Agente = "" & Me.AgencyTravelAgent.Text
            End With
        End With

    End Sub


    ''' <summary>
    ''' fill the form with the data in bookres object
    ''' </summary>
    ''' <param name="bookData"></param>
    ''' <remarks></remarks>
    Public Sub fillFormWithBookData(ByRef bookData As BookRes)
        If bookData Is Nothing Then
            Exit Sub
        End If


        With bookData
            Me.clientname.Text = cfr.tool.Tools.cNullStr(.CliRes.Nombre, Me.clientname.Text).ToString
            Me.clientsurname.Text = cfr.tool.Tools.cNullStr(.CliRes.Apellido1, Me.clientsurname.Text).ToString

            Me.clientemail.Text = cfr.tool.Tools.cNullStr(.CliRes.email, Me.clientemail.Text).ToString
            Me.clientcheckemail.Text = cfr.tool.Tools.cNullStr(.CliRes.email, Me.clientcheckemail.Text).ToString
            Me.clientphone.Text = cfr.tool.Tools.cNullStr(.CliRes.Tlf, Me.clientphone.Text).ToString

            'country
            If Me.cboCountry.Items.Count <> 0 Then
                'If .CliRes.Pais.Description <> "" Then
                '    Me.cboCountry.SelectedItem.Text = .CliRes.Pais.Description
                'Else
                Try
                    If .CliRes.Pais.idEntity > 0 Then
                        Me.cboCountry.SelectedValue = .CliRes.Pais.idEntity
                    End If

                Catch ex As Exception
                    'nothing
                End Try

                'End If
            End If




            Me.comentarios.Text = cfr.tool.Tools.cNullStr(.comentarios, Me.comentarios.Text).ToString


            If bookData.bookOperator.tipoOperador = CFRConst.ClientTypeEnum.Company Then
                With .EmpresaRes
                    Me.empresa.Text = .Nombre
                    Me.empresadireccion.Text = .direccion
                    Me.empresaemail.Text = .email
                    Me.empresaTlf.Text = .Tlf
                End With
            End If

            If Me.pnlAgencyData.Visible = True Then
                With .AgenciaRes
                    Me.Agency.Text = .Nombre
                    Me.Agencydireccion.Text = .direccion
                    Me.AgencyCountry.Text = .Pais.Description
                    Me.Agencyemail.Text = .email
                    Me.AgencyTlf.Text = .Tlf
                    Me.AgencyFax.Text = .fax
                    Me.AgencyTravelAgent.Text = .Agente
                End With
            End If

        End With


        'If My.Computer.Name.ToUpper = "EPC494" Or My.Computer.Name.ToUpper = "FRANPC" And Me.bookData Is Nothing Then
        '    Me.clientname.text = "Fran"
        '    Me.clientsurname.Text = "Moreno"
        '    Me.tlf.Text = "123456789"
        '    Me.Email.Text = "customer@example.com"
        '    Me.ciudad.Text = "Madrid"
        '    'Dim countrySelected As ListItem = 
        '    'Me.cboCountry.Items.FindByValue("123822").Selected = True
        '    'Me.cboCountry.SelectedValue = 123822
        'End If

    End Sub


End Class
