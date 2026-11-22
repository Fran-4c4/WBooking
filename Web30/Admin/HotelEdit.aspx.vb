
Imports System.Data
Imports System.Diagnostics
Imports System.Linq

Partial Class HotelEdit
    Inherits AdminBasePage

    Public idHotDat() As String
    Public reqradcategoria As Integer

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P6_Configuracion_de_Datos_hotel


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.hotelConfiguration)


            If Not Page.IsPostBack Then

                Me.configCBos()
                Me.fillPage(hotel)
            End If

            Me.fillMatLanguage()
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try


    End Sub


    Private Sub fillPage(ByVal hotel As cfr.obj.Hotel)

        If Not hotel Is Nothing Then
            Me.tnombre.Text = hotel.hotelName
            Me.tdireccion.Text = hotel.Direccion
            Me.tcodpostal.Text = hotel.codpostal
            Me.ttelefono.Text = hotel.Telefono
            Me.temail.Text = hotel.Email
            Me.tweb.Text = hotel.Web
            Me.tnumhabitaciones.Text = hotel.Habitaciones
            Me.tdirector.Text = hotel.Director

            'Me.tsituacion.Text = hotel.city.country.Description & ">" & hotel.city.DescriptionByLang
            'Me.cboChain.SelectedValue = hotel.chain.code
            reqradcategoria = hotel.cod_Categoria
            Me.cbocategoria.Text = hotel.cod_Categoria

        Me.editdesc_es.Text = cfr.security.RichHtmlSanitizer.Sanitize(hotel.desc_es)
        Me.editdesc_en.Text = cfr.security.RichHtmlSanitizer.Sanitize(hotel.desc_en)
        Me.editdesc_de.Text = cfr.security.RichHtmlSanitizer.Sanitize(hotel.desc_de)
        Me.editdesc_fr.Text = cfr.security.RichHtmlSanitizer.Sanitize(hotel.desc_fr)

            Me.cbocheckin1.SelectedValue = hotel.checkintime1
            Me.cbocheckin2.SelectedValue = hotel.checkintime2
            Me.cbocheckout1.SelectedValue = hotel.checkOuttime1
            Me.cbocheckout2.SelectedValue = hotel.checkOuttime2

        End If
    End Sub

    Private Sub fillMatLanguage()
        Me.idHotDat = New String(45) {}
        idHotDat(0) = "'Debe seleccionar una ciudad pulsando en el botón <Ciudad>'"
        idHotDat(1) = "'Debe introducir una clave'"
        idHotDat(2) = "'Debe introducir una 2ª clave igual a la 1ª.'"
        idHotDat(3) = "'Las claves no coinciden'"
        idHotDat(4) = "'Debe introducir un login'"
        idHotDat(5) = "'Compruebe los datos que ha introducido'"
        idHotDat(6) = "'Debe introducir un email valido'"
        idHotDat(7) = "Se ha producido un error al intentar registrar el hotel."
        idHotDat(8) = "Su hotel se ha registrado correctamente."
        idHotDat(9) = "El login ya existe, vuelva a intentarlo con otro."
        idHotDat(10) = "Ficha T&eacute;cnica del Hotel"
        idHotDat(11) = "Datos Generales del Hotel"
        idHotDat(12) = "indica campos Obligatorios"
        idHotDat(13) = "Nombre del hotel"
        idHotDat(14) = "Información "
        idHotDat(15) = "Dirección"
        idHotDat(16) = "Código Postal"
        idHotDat(17) = "Ciudad"
        idHotDat(18) = "Pulse en el boton 'Ciudad' para seleccionar la ciudad donde se encuentra su hotel"
        idHotDat(19) = "Telefono"
        idHotDat(20) = "Fax"
        idHotDat(21) = "E-Mail"
        idHotDat(22) = "Pagina Web"
        idHotDat(23) = "Director"
        idHotDat(24) = "Director Comercial"
        idHotDat(25) = "Nº de Habitaciones "
        idHotDat(26) = "Categoria"
        idHotDat(27) = "Bungalows"
        idHotDat(28) = "Llave"
        idHotDat(29) = "Llaves"
        idHotDat(30) = "Lujo"
        idHotDat(31) = "Login"
        idHotDat(32) = "Clave"
        idHotDat(33) = "Confirme &nbsp;Clave"
        idHotDat(34) = "Registrar"
        idHotDat(35) = "Modificar"
        idHotDat(36) = "Rellene los datos y pulse en 'Modificar' o 'Registrar' según si esta registrando un hotel o modificando sus datos."
        idHotDat(37) = "Si no existe una ciudad para su hotel, creela desde la administracion global antes de continuar."
        idHotDat(38) = "Una vez que haya modificado sus datos pulse en el menu de la izquierda para modificar el resto de sus datos."
        idHotDat(39) = "Para seleccionar su ciudad pulse sobre el boton"
        idHotDat(40) = "El * indica campos Obligatorios"
        idHotDat(41) = "Debe seleccionar una ciudad"
        idHotDat(42) = "Ficha Técnica del Hotel"
        idHotDat(43) = "Casa Rural"
        idHotDat(44) = "Su hotel se ha registrado correctamente. A continuacion rellene el resto de datos. <BR>Es importante rellenar los datos de servicios, situacion y puntos clave. Estos datos ayudan a buscar a los clientes. Si no estan rellenos los clientes no encontraran el hotel."
        idHotDat(45) = "Debe rellenar los campos con un *"
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try

            If Me.codhotel > 0 Then
                'hotel = cfr.mgr.HotelMgr.GetEntity(Me.codhotel)
            Else
                hotel = New cfr.obj.Hotel
                hotel.lastAccess = Date.Now
                hotel.lastUpdate = Date.Now
            End If
            
            hotel.hotelName = checkValueString(Me.tnombre.Text, "Falta el nombre del alojamiento.")
            hotel.Direccion = checkValueString(Me.tdireccion.Text, "Falta la dirección.")
            hotel.codpostal = checkValueString(Me.tcodpostal.Text, "Falta el codigo postal.")
            hotel.Telefono = checkValueString(Me.ttelefono.Text, "Falta el numero de telefono.")
            hotel.Email = checkValueString(Me.temail.Text, "Falta el correo electronico")
            hotel.Web = checkValueString(Me.tweb.Text, "Falta la pagina web")
            hotel.Habitaciones = checkValueString(Me.tnumhabitaciones.Text, "Falta el numero de habitaciones.")
            hotel.desc_es = cfr.security.RichHtmlSanitizer.Sanitize(Me.editdesc_es.Text)
            hotel.desc_en = cfr.security.RichHtmlSanitizer.Sanitize(Me.editdesc_en.Text)
            hotel.desc_de = cfr.security.RichHtmlSanitizer.Sanitize(Me.editdesc_de.Text)
            hotel.desc_fr = cfr.security.RichHtmlSanitizer.Sanitize(Me.editdesc_fr.Text)
            hotel.checkintime1 = Me.cbocheckin1.SelectedValue
            hotel.checkintime2 = Me.cbocheckin2.SelectedValue
            hotel.checkOuttime1 = Me.cbocheckout1.SelectedValue
            hotel.checkOuttime2 = Me.cbocheckout2.SelectedValue
            
            'Debug.WriteLine("editdesc=" & Request.Form("editdesc_es"))
            'For Each req As String In Request.Form.Keys
            '    Debug.WriteLine(req & "=" & Request(req))
            'Next

            'Categoria
            If Me.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then
                hotel.cod_Categoria = checkValueString(Me.cbocategoria.Text, "Seleccione una categoria de hotel")
                Me.saveServices()
                Me.saveSaloons()
            End If

            Me.saveSituation()
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.warning)
            Exit Sub
        End Try


        'MODIFICA LOS DATOS DE UN HOTEL o lo crea
        If hotel.cod_Hotel > 0 Then 'ACTUALIZA LOS DATOS
            Try
                Dim thotelMgr As New cfr.mgr.HotelMgr
                thotelMgr.Update(hotel)
                cfr.pages.SiteBasePage.showMessage("Su hotel se ha guardado correctamente.", Me.Page, CFRConst.msgEnum.Ok)
                Exit Sub
            Catch ex As Exception
                cfr.pages.SiteBasePage.showMessage("Se ha producido un error al intentar registrar el hotel. Debe existir algun problema con algun tipo de dato introducido.", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End Try
        End If

        If hotel.cod_Hotel = 0 Then 'CREA UN NUEVO HOTEL
            Try
                cfr.mgr.HotelMgr.CREATE(hotel)
                cfr.pages.SiteBasePage.showMessage("Su hotel se ha registrado correctamente.", Me.Page, CFRConst.msgEnum.Ok)
                Exit Sub
            Catch ex As Exception
                cfr.pages.SiteBasePage.showMessage("Debe rellenar los campos con un *", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End Try
        End If 'cod_hotel=0  
        

    End Sub

    Private Function checkValueString(value As String, errMessage As String) As String
        If value.Trim = "" Then
            Throw New Exception(errMessage)
        Else
            Return value.Trim
        End If
    End Function

    Private Sub sendEmailHotelDataUpdated(ByVal hotel As cfr.obj.Hotel)
        Try

            'cod_hotel = id_hotel_New
            'Dim aSesionnewhot()
            'ReDim aSesionnewhot(10)
            'aSesionnewhot(0) = cod_hotel
            'aSesionnewhot(1) = 0
            'aSesionnewhot(2) = 0
            'aSesionnewhot(3) = 0
            'Session("h" & cod_hotel) = aSesionnewhot
            'Response.Redirect("adminreghoteldatos.asp?newhotel=1&hotel=" & cod_hotel & "&" & vcadquery)
        Catch ex As Exception
            'nothing
        End Try
    End Sub

    Private Sub configCBos()

        If Me.hotel.Tipo = CFRConst.unitTypeEnum.car Then
            Me.plhCboCategoria.Visible = False
        End If

        With Me.cbocategoria.Items
            '<input type="radio" name="radcategoria" value="15" <%If reqradcategoria = 15 Then Response.Write("CHECKED")%>>* </td>
            .Add(New ListItem("1 Estrella", 15))
            .Add(New ListItem("2 Estrellas", 14))
            .Add(New ListItem("3 Estrellas", 13))
            .Add(New ListItem("4 Estrellas", 12))
            .Add(New ListItem("5 Estrellas", 11))
            .Add(New ListItem("5 Estrellas Lujo", 10))
            .Add(New ListItem("Bungalow", 18))
            .Add(New ListItem("Apartamento 1 Llave", 21))
            .Add(New ListItem("Apartamento 2 Llaves", 20))
            .Add(New ListItem("Apartamento 3 Llaves", 19))
            .Add(New ListItem("Casa rural", 24))
        End With

        cbocheckin2.Items.Add(New ListItem(""))
        cbocheckout1.Items.Add(New ListItem(""))
        fillCBOTime(Me.cbocheckin1, 7, 20)
        fillCBOTime(Me.cbocheckin2, 12, 24)

        fillCBOTime(Me.cbocheckout1, 0, 14)

        fillCBOTime(Me.cbocheckout2, 7, 20)

    End Sub

    Private Sub fillCBOTime(cbo As DropDownList, fromTime As Integer, toTime As Integer)
        Dim timeEnd As New TimeSpan(toTime, 0, 0)
        For chour As Integer = fromTime To toTime
            Dim timeTo As New TimeSpan(chour, 0, 0)
            cbo.Items.Add(New ListItem(Right("0" & timeTo.Hours, 2) & ":00"))
            If timeTo.Hours <> timeEnd.Hours Then
                cbo.Items.Add(New ListItem(Right("0" & timeTo.Hours, 2) & ":30"))
            End If


        Next
    End Sub

    Friend Function getDTSituaciones() As DataView
        Try
            Dim tSituacionmng As New ind.bk.obj.T_HOTELES_SITUACIONES_Manager

            Dim dv As DataView = tSituacionmng.getDataTableGetByCodhotelForAdmin(Me.codhotel).DefaultView
            dv.Sort = ind.bk.obj.T_SITUACIONES.DBCol.situacion
            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

        Return Nothing
    End Function

    Private Sub saveSituation()

        Dim ocon As SqlClient.SqlConnection = Nothing
        Dim trans As SqlClient.SqlTransaction = Nothing
        Try


            Dim tSituacionmng As New ind.bk.obj.T_HOTELES_SITUACIONES_Manager
            'abrimos la conexion y comenzamos la reserva
            ocon = ind.bk.DBMgr.getNewConn
            'comienzo de la transaccion
            trans = ocon.BeginTransaction
            tSituacionmng.Delete(Me.codhotel, trans:=trans)

            'how to get values from a collection inside request, ver esto algun dia, no se ha probado y será mejor que el split
            'Dim productID As String
            'For Each productID In Page.Request.QueryString.GetValues("ProductID")
            '    ' do something with productID
            'Next productID
            'Debug.WriteLine(" Request.Form(chkopt)" & Request.Form("chkopt"))
            Dim opt As Object = Request.Form("chkopt")

            Dim mat1() As String = Page.Request.Form.GetValues("chkopt")
            If Not mat1 Is Nothing Then
                For Each optID In mat1
                    Dim THS As New ind.bk.obj.T_HOTELES_SITUACIONES
                    THS.cod_Hotel = Me.codhotel
                    THS.cod_situacion = CInt(optID)
                    tSituacionmng.insert(THS, trans:=trans)
                Next
            End If


            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            ind.bk.DBMgr.closeConn(ocon)
        End Try

    End Sub

    Friend Function getDTServices() As DataView
        Try
            Dim tableRes As New ind.bk.obj.T_HOTELES_SERVICIOS_Manager

            Dim dv As DataView = tableRes.getDataTableGetByCodhotelForAdmin(Me.codhotel).DefaultView
            dv.RowFilter = "tipo_carac<>2"
            'dv.Sort = ind.bk.obj.T_SERVICIOS.DBCol.cod_Servicio
            Return dv
        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

        Return Nothing
    End Function

    Private Sub saveServices()

        Dim ocon As SqlClient.SqlConnection = Nothing
        Dim trans As SqlClient.SqlTransaction = Nothing
        Try

            Dim tabMng As New ind.bk.obj.T_HOTELES_SERVICIOS_Manager
            'abrimos la conexion y comenzamos la reserva
            ocon = ind.bk.DBMgr.getNewConn
            'comienzo de la transaccion
            trans = ocon.BeginTransaction
            tabMng.DeleteByCodHotel(Me.codhotel, trans:=trans)

            'Debug.WriteLine(" Request.Form(chkoptServices)" & Request.Form("chkoptServices"))
            Dim mat() As String = Page.Request.Form.GetValues("chkoptServices")
            If Not mat Is Nothing Then
                Dim matunderRequest() As String = getRequestVector("chkbajopet")

                For Each optID In mat
                    Dim THS As New ind.bk.obj.T_HOTELES_SERVICIOS
                    THS.cod_Hotel = Me.codhotel
                    THS.cod_Servicio = CInt(optID)
                    THS.Consultar = getReqValueFromVector(matunderRequest, optID)
                    tabMng.insert(THS, trans:=trans)
                Next
            End If


            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            ind.bk.DBMgr.closeConn(ocon)
        End Try

    End Sub
    

    Friend Function getDTSaloons() As DataView

        Dim trans As SqlClient.SqlTransaction = Nothing
        Try

            Dim sSql As String = "SELECT hotel, nombre, m2, teatro, escuela, cocktail, banquete, mesai AS mi, mesau AS mu FROM t_salones WHERE hotel=@hotelId"

            Dim tableRes As DataTable = ind.bk.DBMgr.getDT(
                sSql,
                New SqlClient.SqlParameter("@hotelId", SqlDbType.Int) With {.Value = Me.codhotel})


            Return tableRes.DefaultView

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try

        Return Nothing
    End Function

    Public Sub saveSaloons()
        Dim ocon As SqlClient.SqlConnection = Nothing
        Dim trans As SqlClient.SqlTransaction = Nothing
        Try

            'abrimos la conexion y comenzamos la reserva
            ocon = ind.bk.DBMgr.getNewConn
            'comienzo de la transaccion
            trans = ocon.BeginTransaction

            '1º borramos los bonos que tiene
            Dim sqlDelete As String = "DELETE FROM t_salones WHERE hotel=@hotelId"
            ind.bk.DBMgr.Execute(
                sqlDelete,
                trans,
                New SqlClient.SqlParameter("@hotelId", SqlDbType.Int) With {.Value = Me.codhotel})

            Dim salon(10, 8) As String

            For x As Integer = 0 To 9
                salon(x, 0) = Request.Form("tsalon" & x)
                salon(x, 1) = Request.Form("tsalmetro" & x)
                salon(x, 2) = Request.Form("tsalteatro" & x)
                salon(x, 3) = Request.Form("tsalescuela" & x)
                salon(x, 4) = Request.Form("tsalcock" & x)
                salon(x, 5) = Request.Form("tsalbanq" & x)
                salon(x, 6) = Request.Form("tsalmesai" & x)
                salon(x, 7) = Request.Form("tsalmesu" & x)
            Next

            For x = 0 To 9
                If Len(salon(x, 0)) > 0 Then
                    Const insertSql As String =
                        "INSERT INTO T_salones (hotel, nombre, m2, teatro, escuela, cocktail, banquete, mesai, mesau) " &
                        "VALUES (@hotelId, @name, @m2, @theatre, @school, @cocktail, @banquet, @tableI, @tableU)"
                    ind.bk.DBMgr.Execute(
                        insertSql,
                        trans,
                        New SqlClient.SqlParameter("@hotelId", SqlDbType.Int) With {.Value = Me.codhotel},
                        New SqlClient.SqlParameter("@name", SqlDbType.NVarChar, 250) With {.Value = salon(x, 0).Trim()},
                        New SqlClient.SqlParameter("@m2", SqlDbType.Decimal) With {.Value = ParseSalonNumber(salon(x, 1))},
                        New SqlClient.SqlParameter("@theatre", SqlDbType.Decimal) With {.Value = ParseSalonNumber(salon(x, 2))},
                        New SqlClient.SqlParameter("@school", SqlDbType.Decimal) With {.Value = ParseSalonNumber(salon(x, 3))},
                        New SqlClient.SqlParameter("@cocktail", SqlDbType.Decimal) With {.Value = ParseSalonNumber(salon(x, 4))},
                        New SqlClient.SqlParameter("@banquet", SqlDbType.Decimal) With {.Value = ParseSalonNumber(salon(x, 5))},
                        New SqlClient.SqlParameter("@tableI", SqlDbType.Decimal) With {.Value = ParseSalonNumber(salon(x, 6))},
                        New SqlClient.SqlParameter("@tableU", SqlDbType.Decimal) With {.Value = ParseSalonNumber(salon(x, 7))})
                End If
            Next



            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            ind.bk.DBMgr.closeConn(ocon)
        End Try
    End Sub

    Private Shared Function ParseSalonNumber(ByVal value As String) As Decimal
        If String.IsNullOrWhiteSpace(value) Then
            Return 0D
        End If

        Dim parsed As Decimal
        If Decimal.TryParse(value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, parsed) OrElse
           Decimal.TryParse(value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, parsed) Then
            Return parsed
        End If
        Throw New ArgumentException("Los valores numéricos del salón no son válidos.")
    End Function



End Class
