



Namespace cfr.erp
    ''' <summary>
    ''' class to manage customer objects
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CustomerMgr

        ''' <summary>
        ''' Get the customer
        ''' </summary>
        ''' <param name="Id_Cliente"></param>
        ''' <returns></returns>
        Public Function CustomerGet(ByVal Id_Cliente As Integer) As CustomerObj
            Dim dr As New CustomerObj
            Dim paramCol(0) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("Id_Cliente", Id_Cliente)
            Dim dt As DataTable = ind.bk.DBMgr.getDT(cfr.db.sp.sproc.Customer.CustomerGet, paramCol, Nothing)

            If dt.Rows.Count = 0 Then
                Return Nothing
            End If

            Dim oRs As DataRow = dt.Rows(0)
            fillObjectByRow(dr, oRs)

            Return dr
        End Function


        Public Function CustomerGetByCodHotelAsociated(ByVal cod_hotel As Integer) As CustomerObj
            Dim customer As New CustomerObj
            Dim paramCol(0) as SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("cod_hotel", cod_hotel)
            Dim dt As DataTable = ind.bk.DBMgr.getDT(cfr.db.sp.sproc.Customer.CustomerGetByCodHotelAsociated, paramCol, Nothing)

            If dt.Rows.Count = 0 Then
                Return Nothing
            End If

            Dim oRs As DataRow = dt.Rows(0)
            fillObjectByRow(customer, oRs)

            Return customer
        End Function

        Private Sub fillObjectByRow(ByRef customer2Fill As CustomerObj, ByRef oRs As DataRow)
            customer2Fill.id_Cliente = cfr.tool.Tools.cNull(oRs("id_cliente"))
            customer2Fill.Description = cfr.tool.Tools.cNull(oRs("cliente"))
            'Dim blockBook As Object = cfr.tool.Tools.cNull(oRs("blockBook"), cfr.erp.CustomerObj.BlockedOptions.Unblocked)
            'If blockBook = True Then
            '    customer2Fill.blockBook = cfr.erp.CustomerObj.BlockedOptions.Blocked
            'Else
            '    customer2Fill.blockBook = cfr.erp.CustomerObj.BlockedOptions.Unblocked
            'End If

            customer2Fill.blockBook = cfr.tool.Tools.cNull(oRs("blockBook"), cfr.erp.CustomerObj.BlockedOptions.Unblocked)

        End Sub



        '        Public Function HotelesAsignados(ByRef pvError As Object, ByVal codigo As Double) As ADODB.Recordset
        '        On Error GoTo error
        '            pvError = S_OK
        '            Dim s As String
        '            s = "SELECT * "
        '            s = s & " FROM R_Clientes_Hoteles INNER JOIN T_HOTELES "
        '            s = s & " ON T_HOTELES.COD_HOTEL=R_Clientes_Hoteles.COD_HOTEL"
        '            s = s & "  WHERE id_Cliente = " & codigo
        '            s = s & " ORDER BY HOTEL "

        '            HotelesAsignados = BasDB.getRS(s, pvError)
        '            Exit Function
        'error:
        '            pvError = IIf(pvError = S_OK, E_HotelesAsignados, pvError)

        '        End Function


        '        Public Function ClienteFill(ByRef pvError As Object, ByVal codigo As Double, ByRef clienteFacVar As Object)
        '        On Error GoTo error
        '            pvError = S_OK
        '            Dim ds As ADODB.Recordset
        '            If codigo = 0 Then Exit Function
        '            ds = ClienteGet(pvError, codigo)
        '            If ds.RecordCount = 0 Then
        '                pvError = E_ClienteNotExist
        '                GoTo error
        '            End If

        '            Dim clienteFac As ClienteFacturacion
        '            clienteFac = clienteFacVar

        '            clienteFac.id_Cliente = chxNull(ds("Id_Cliente"), 0)
        '            clienteFac.clienteDesc = chxNull(ds("Cliente"), "")
        '            clienteFac.Direccion = chxNull(ds("Direccion"), "")
        '            clienteFac.CodigoPostal = chxNull(ds("CodigoPostal"), "")
        '            clienteFac.Localidad = chxNull(ds("Localidad"), "")
        '            clienteFac.Zona = chxNull(ds("Zona"), "")
        '            clienteFac.Pais = chxNull(ds("Pais"), "")
        '            clienteFac.NumeroFiscal = chxNull(ds("NumeroFiscal"), "")
        '            clienteFac.EmailFacturacion = chxNull(ds("EmailFacturacion"), "")
        '            clienteFac.ContactoNombre = chxNull(ds("ContactoNombre"), "")
        '            clienteFac.ContactoTelefono = chxNull(ds("ContactoTelefono"), "")
        '            clienteFac.ContactoTelefono2 = chxNull(ds("ContactoTelefono2"), "")
        '            clienteFac.ContactoTelefono3 = chxNull(ds("ContactoTelefono3"), "")
        '            clienteFac.ContactoFax = chxNull(ds("ContactoFax"), "")
        '            clienteFac.ContactoCargo = chxNull(ds("ContactoCargo"), "")
        '            clienteFac.Login = chxNull(ds("login"), "")
        '            clienteFac.clave = chxNull(ds("clave"), "")
        '            clienteFac.CuentaBancaria = chxNull(ds("CuentaBancaria"), "")
        '            clienteFac.FormaPago = chxNull(ds("FormaPago"), 0)
        '            'clienteFac.fechaAlta = chxNull(ds("fechaAlta"), New Date)
        '            'clienteFac.fechaBaja = chxNull(ds("fechaBaja"), New Date)
        '            clienteFac.FechaModificacion = chxNull(ds("FechaModificacion"), Now)
        '            clienteFac.WebCliente = chxNull(ds("WebCliente"), "")
        '            Exit Function
        'error:
        '            pvError = IIf(pvError = S_OK, E_ClienteGet, pvError)
        '        End Function



        '        Public Function ClienteInsert(ByRef pvError As Object, ByRef ClienteRef As Object)
        '        On Error GoTo error
        '            pvError = S_OK
        '            Dim clienteFac As ClienteFacturacion
        '            clienteFac = ClienteRef
        '            Dim ocon As SqlClient.SqlConnection
        '            Dim oRec As Recordset

        '            Dim s As String
        '            s = "INSERT INTO R_CLIENTES_FACTURACION "
        '            s = s & "(Cliente, Direccion, CodigoPostal, Localidad, Zona, Pais, NumeroFiscal, EmailFacturacion "
        '            s = s & ", ContactoNombre, ContactoTelefono,ContactoTelefono2,ContactoTelefono3, ContactoFax, ContactoCargo "
        '            s = s & ",CuentaBancaria, FormaPago, fechaAlta,login,clave,WebCliente)"
        '            s = s & " VALUES ('" & clienteFac.clienteDesc & "', '" & clienteFac.Direccion & "', '" & clienteFac.CodigoPostal & "', '" & clienteFac.Localidad & "', '" & clienteFac.Zona & "', '" & clienteFac.Pais & "', '" & clienteFac.NumeroFiscal & "', '" & clienteFac.EmailFacturacion & "' "
        '            s = s & " , '" & clienteFac.ContactoNombre & "', '" & clienteFac.ContactoTelefono & "' "
        '            s = s & " , '" & clienteFac.ContactoTelefono2 & "', '" & clienteFac.ContactoTelefono3 & "' "

        '            s = s & ", '" & clienteFac.ContactoFax & "',' " & clienteFac.ContactoCargo & "' "
        '            s = s & ",'" & clienteFac.CuentaBancaria & "'," & clienteFac.FormaPago
        '            s = s & ",CONVERT(DATETIME, '" & Year(Now) & "-" & Month(Now) & "-" & Day(Now) & "',102)"
        '            s = s & ",'" & clienteFac.Login & "','" & clienteFac.clave & "','" & clienteFac.WebCliente & "' "
        '            s = s & ")"

        '            'abrimos la conexion y comenzamos la reserva
        '            If Not bOpenConnection(ocon) Then
        '                pvError = E_DB_CONN
        '               GoTo error
        '            End If

        '            Call UpdateSql(s, pvError, ocon)
        '            Call yhErr.checkError(pvError, E_ClienteInsert)

        '            s = "Select id_Cliente from R_CLIENTES_FACTURACION where id_Cliente=@@IDENTITY"

        '            Call UpdateSql(s, pvError)
        '            Call yhErr.checkError(pvError, E_ClienteInsert)

        '            clienteFac.id_Cliente = oRec("id_Cliente")

        '            ClienteRef = clienteFac
        '            clienteFac = Nothing
        '            Call closeCon(ocon, pvError)
        '            Exit Function
        'error:
        '            On Error Resume Next
        '            pvError = IIf(pvError = S_OK, E_ClienteInsert, pvError)

        '            clienteFac = Nothing
        '            Call closeCon(ocon, pvError)

        '        End Function

        '        Public Function ClienteUpdate(ByRef pvError As Object, ByVal clienteFac As ClienteFacturacion)
        '        On Error GoTo error
        '            pvError = S_OK
        '            Dim d As Date

        '            Dim s As String
        '            s = "UPDATE R_CLIENTES_FACTURACION SET "
        '            s = s & " Cliente                 ='" & clienteFac.clienteDesc & "'"
        '            s = s & ",NumeroFiscal            ='" & clienteFac.NumeroFiscal & "'"
        '            s = s & ",CodigoPostal            ='" & clienteFac.CodigoPostal & "'"
        '            s = s & ",ContactoFax             ='" & clienteFac.ContactoFax & "'"
        '            s = s & ",ContactoNombre          ='" & clienteFac.ContactoNombre & "'"
        '            s = s & ",ContactoTelefono        ='" & clienteFac.ContactoTelefono & "'"
        '            s = s & ",ContactoTelefono2        ='" & clienteFac.ContactoTelefono2 & "'"
        '            s = s & ",ContactoTelefono3        ='" & clienteFac.ContactoTelefono3 & "'"
        '            s = s & ",ContactoCargo        ='" & clienteFac.ContactoCargo & "'"
        '            s = s & ",Direccion               ='" & clienteFac.Direccion & "'"
        '            s = s & ",EmailFacturacion        ='" & clienteFac.EmailFacturacion & "'"
        '            s = s & ",Localidad               ='" & clienteFac.Localidad & "'"
        '            s = s & ",FormaPago          ='" & clienteFac.FormaPago & "'"
        '            s = s & ",CuentaBancaria          ='" & clienteFac.CuentaBancaria & "'"
        '            s = s & ",Pais                    ='" & clienteFac.Pais & "'"
        '            s = s & ",Zona                    ='" & clienteFac.Zona & "'"

        '            s = s & ",login                    ='" & clienteFac.Login & "'"
        '            s = s & ",clave                    ='" & clienteFac.clave & "'"
        '            s = s & ",FechaModificacion       =CONVERT(DATETIME, '" & Year(Now) & "-" & Month(Now) & "-" & Day(Now) & "',102)"
        '            s = s & ",WebCliente                    ='" & clienteFac.WebCliente & "'"

        '            s = s & " WHERE id_Cliente        =" & clienteFac.id_Cliente

        '            Call UpdateSql(s, pvError)
        '            Call yhErr.checkError(pvError, E_ClienteUpdateData)

        '            Exit Function
        'error:
        '            pvError = IIf(pvError = S_OK, E_ClienteUpdateData, pvError)


        '        End Function

        '        Public Function ClienteDelete(ByRef pvError As Object, ByVal id_Cliente As Double) As Boolean
        '        On Error GoTo error
        '            pvError = S_OK
        '            Dim s As String
        '            Dim oFac As New Facturacion
        '            If oFac.ClienteHaveFacturas(pvError, id_Cliente) = True Then
        '                pvError = E_ClienteDeleteHaveFacturas
        '                GoTo error
        '            End If
        '            s = "DELETE FROM R_CLIENTES_FACTURACION WHERE ID_CLIENTE=" & id_Cliente
        '            Call UpdateSql(s, pvError)
        '            Call yhErr.checkError(pvError, E_ClienteDelete)

        '            ClienteDelete = True
        '            Exit Function
        'error:
        '            pvError = IIf(pvError = S_OK, E_ClienteDelete, pvError)
        '            ClienteDelete = False

        '        End Function


        '        Public Function asignarHotel(ByRef pvError As Object, ByVal id_Cliente As Double, cod_Hotel As Double)
        '        On Error GoTo error
        '            pvError = S_OK
        '            Dim s As String
        '            s = "INSERT INTO R_Clientes_Hoteles (ID_CLIENTE,COD_HOTEL)"
        '            s = s & " VALUES('" & id_Cliente & "','" & cod_Hotel & "') "
        '            Call UpdateSql(s, pvError)
        '            Call yhErr.checkError(pvError, E_ClienteAsignHotel)

        '            Exit Function
        'error:
        '            pvError = IIf(pvError = S_OK, E_ClienteAsignHotel, pvError)

        '        End Function

        '        Public Function DesAsignarhotel(ByRef pvError As Object, ByVal id_Cliente As Double, cod_Hotel As Double)
        '    On Error GoTo error
        '            pvError = S_OK
        '            Dim s As String
        '            s = "DELETE FROM R_Clientes_Hoteles WHERE ID_CLIENTE=" & id_Cliente & " AND COD_HOTEL=" & cod_Hotel
        '            Call UpdateSql(s, pvError)
        '            Call yhErr.checkError(pvError, E_DesAsignarhotel)

        '            Exit Function
        'error:
        '            pvError = IIf(pvError = S_OK, E_DesAsignarhotel, pvError)


        '        End Function


    End Class

End Namespace