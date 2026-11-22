Imports Microsoft.VisualBasic
Imports System.Diagnostics
Imports System.Security.Cryptography
Imports System.Text


Namespace cfr
    <Serializable()> _
    Public Class PayGateWayManager
        Public Ds_Merchant_MerchantName As String
        Public Ds_Merchant_MerchantCode As String
        Public Ds_Merchant_Terminal As String
        Public Ds_Merchant_Password As String
        Public Ds_Merchant_Currency As String
        ''' <summary>
        ''' The pay amount must be an string without separators example: 158,25 equals to 15825 last two digits are always decimals
        ''' </summary>
        ''' <remarks></remarks>
        Public Ds_Merchant_Amount As String
        Public Ds_Merchant_Order As String
        Public Ds_Merchant_TransactionType As String
        ''' <summary>
        ''' This is the url where the bank send confirmation to server to do operations
        ''' </summary>
        ''' <remarks></remarks>
        Public Ds_Merchant_MerchantURL As String

        Public Ds_Signature As String
        ''' <summary>
        ''' url where the bank send the user to continue
        ''' </summary>
        ''' <remarks></remarks>
        Public Ds_Merchant_UrlOK As String
        ''' <summary>
        '''  url where the bank send the user when fail to paid
        ''' </summary>
        ''' <remarks></remarks>
        Public Ds_Merchant_UrlKO As String
        Public Ds_Merchant_ConsumerLanguage As String
        

        ''' <summary>
        ''' returned error from tpv
        ''' </summary>
        ''' <remarks></remarks>
        Public Ds_Response As String

        Public URL_TPVVIRTUAL As String
        Public ipServer As String

        Public data2FormSubmit As String
        Public Cod_Hotel As Long
        Public vCadQuery As String
        Public requestFormData As String

        Public Sub New()
            'BookRes always creates this data holder, including reservations that
            'do not use online payment. Endpoint access is enforced by
            'PaymentEndpointGuard; constructing booking state must remain valid
            'when payment support is disabled.
            ipServer = "http://localhost:3538/Web30"

            

            Ds_Merchant_MerchantName = "WBooking"
            Ds_Merchant_MerchantCode = ""
            Ds_Merchant_Terminal = 1
            Ds_Merchant_Password = ""

            Ds_Merchant_Currency = "978"
            Ds_Merchant_ConsumerLanguage = "001" 'Castellano-001, Inglés-002, Catalán-003,Francés-004, Alemán-005, Holandés-006
            ',Italiano-007, Sueco-008, portugués-009, Valenciano-010, Polaco-011, Gallego-012 y Euskera-013.
            Ds_Merchant_Amount = "0"
            Ds_Merchant_Order = ""
            Ds_Merchant_TransactionType = "0"

            Ds_Merchant_MerchantURL = ipServer & "/B_PayGateWayConfirmation.aspx?OPER=PAYGATECAIXA" 'url where the bank send confirmation

            Ds_Merchant_UrlOK = ipServer & "/b_PayGateReturn.aspx?OPER=PAYGATECAIXA" 'url where the bank send the user to continue
            Ds_Merchant_UrlKO = ipServer & "/b_PayGateReturn.aspx?OPER=ERROR" ' url where the bank send the user when fail to paid

        End Sub

        ''' <summary>
        ''' Configure TPV, this is only for CAIXA bank
        ''' </summary>
        ''' <param name="bookData"></param>
        ''' <param name="vCadQuery"></param>
        ''' <param name="requestFormData"></param>
        ''' <remarks></remarks>
        Public Sub PasarelaConfig(ByVal bookData As BookRes, vCadQuery As String, requestFormData As String)

            Me.Cod_Hotel = bookData.hotel.id
            Me.vCadQuery = vCadQuery
            Me.requestFormData = requestFormData

            'me.Ds_Merchant_Amount=dr.precio * 100
            Me.Ds_Merchant_Amount = bookData.TPVMinimum * 100 'we use TPVMinimum because sometimes the hotel only need first night or percentage

            '280259 is fran pruebas hotel
            If Cod_Hotel = 280259 Or Cod_Hotel = 280255 Then
                URL_TPVVIRTUAL = "https://sis-t.sermepa.es:25443/sis/realizarPago"           'desarrollo
            Else
                URL_TPVVIRTUAL = "https://sis.sermepa.es/sis/realizarPago"           'PRODUCCION
            End If


            Me.GetMerchantTpvdata()

            Ds_Merchant_MerchantName = bookData.hotel.hotelName


            Select Case bookData.language
                Case CFRConst.LanguageEnum.es
                    Ds_Merchant_ConsumerLanguage = "001" 'spanish
                Case CFRConst.LanguageEnum.en
                    Ds_Merchant_ConsumerLanguage = "002" 'english
                Case CFRConst.LanguageEnum.de
                    Ds_Merchant_ConsumerLanguage = "005" 'aleman
                Case CFRConst.LanguageEnum.fr
                    Ds_Merchant_ConsumerLanguage = "004" 'frances
            End Select

            'we take only 12 due to limitations of bank
            'temporal order
            Me.Ds_Merchant_Order = Right("000000" & Me.Cod_Hotel & Rnd(123456789), 12)


            Dim UrlQueryCommon As String = "&hotelcode=" & bookData.hotel.id & "&site=" & bookData.site.id & "&language=" & bookData.language

            'URL for errors sended by TPV
            Me.Ds_Merchant_UrlKO = Me.Ds_Merchant_UrlKO & UrlQueryCommon


        End Sub


        ''' <summary>
        ''' Configure merchantSignature used by TPV bank gateway. We need to get all the parameters first
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ConfigureMerchantSignature()

            Dim strSignature As String
            strSignature = Ds_Merchant_Amount & Ds_Merchant_Order & Ds_Merchant_MerchantCode & Ds_Merchant_Currency & Ds_Merchant_TransactionType & Ds_Merchant_MerchantURL & Ds_Merchant_Password

            Using sha1 As SHA1 = SHA1.Create()
                Dim digest As Byte() = sha1.ComputeHash(Encoding.UTF8.GetBytes(strSignature))
                Me.Ds_Signature = BitConverter.ToString(digest).Replace("-", String.Empty)
            End Using

        End Sub

        ''' <summary>
        ''' Get TPV data for merchant, like password, code, etc.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub GetMerchantTpvdata()
            Try

                Dim sSql As String = "SELECT * FROM R_TPVDATA WHERE cod_hotel=" & Me.Cod_Hotel

                Dim dv As Data.DataView = ind.bk.DBMgr.getDT(sSql).DefaultView

                If dv.Count > 0 Then
                    Dim rs As Data.DataRowView = dv(0)
                    Me.Ds_Merchant_MerchantCode = rs("Ds_Merchant_MerchantCode")
                    Me.Ds_Merchant_Terminal = rs("Ds_Merchant_Terminal")
                    Me.Ds_Merchant_Password = rs("Ds_Merchant_Password")
                    Me.Ds_Merchant_Currency = rs("Ds_Merchant_Currency")
                    Me.Ds_Merchant_ConsumerLanguage = rs("Ds_Merchant_ConsumerLanguage")
                    Me.Ds_Merchant_TransactionType = rs("Ds_Merchant_TransactionType")
                Else
                    Throw New Exception("Error getting hotel data for bank requisites. There is no data for this hotel: " & Me.Cod_Hotel)
                End If

            Catch ex As Exception
                Throw New Exception("Error getting hotel data for bank requisites." & ex.Message)
            End Try

        End Sub

        ''' <summary>
        ''' config the string data for submit purposes
        ''' This method is not used anymore. The correct method is on page with name sendPostToBank
        ''' </summary>
        ''' <remarks></remarks>
        <Obsolete>
        Public Sub ConfigurePasarelaSubmitData()
            Dim data2submit As String

            Me.Ds_Merchant_UrlOK = ipServer & "/b_confirmation.aspx?OPER=PAYGATECAIXA&" & vCadQuery & "&Ds_Merchant_Order=" & Me.Ds_Merchant_Order & "&MerchantSignature=" & Me.Ds_Signature

            data2submit = ""
            data2submit = vbCr & data2submit & " <form name=tpvform action=" & Me.URL_TPVVIRTUAL & " method=post target='tpv'>"
            data2submit = vbCr & data2submit & " <input type=HIDDEN name=Ds_Merchant_Amount value=" & Me.Ds_Merchant_Amount & ">"
            data2submit = vbCr & data2submit & " <input type=HIDDEN name=Ds_Merchant_Currency value=" & Me.Ds_Merchant_Currency & ">"
            data2submit = vbCr & data2submit & " <input type=HIDDEN name=Ds_Merchant_Order  value=" & Me.Ds_Merchant_Order & ">"
            data2submit = vbCr & data2submit & " <input type=HIDDEN name=Ds_Merchant_MerchantCode value=" & Me.Ds_Merchant_MerchantCode & ">"
            data2submit = vbCr & data2submit & " <input type=HIDDEN name=Ds_Merchant_MerchantSignature value=" & Me.Ds_Signature & ">"
            data2submit = vbCr & data2submit & " <input type=HIDDEN name=Ds_Merchant_Terminal value=" & Me.Ds_Merchant_Terminal & ">"
            data2submit = vbCr & data2submit & " <input type=HIDDEN name=Ds_Merchant_TransactionType value=" & Me.Ds_Merchant_TransactionType & ">"
            data2submit = vbCr & data2submit & " <input type=HIDDEN name=Ds_Merchant_MerchantURL value=" & Me.Ds_Merchant_MerchantURL & ">"
            data2submit = vbCr & data2submit & " <input type=HIDDEN name=Ds_Merchant_UrlOK value=" & Me.Ds_Merchant_UrlOK & ">"
            data2submit = vbCr & data2submit & " <input type=HIDDEN name=Ds_Merchant_UrlKO value=" & Me.Ds_Merchant_UrlKO & ">"
            data2submit = vbCr & data2submit & " <input type=HIDDEN name=Ds_Merchant_ConsumerLanguage value=" & Me.Ds_Merchant_ConsumerLanguage & ">"

            data2submit = vbCr & data2submit & "   </form>"

            Me.data2FormSubmit = data2submit

        End Sub



        Public Sub saveBasket()

            Dim value2ChargeInTPV As Double = (Me.Ds_Merchant_Amount / 100) 'we need to divide by due to bank operations.
            'save to basket to get unique id
            Me.Ds_Merchant_Order = saveBasket2Database(Me.Cod_Hotel, Me.Ds_Merchant_Order, Me.Ds_Signature, Me.requestFormData, value2ChargeInTPV)
            'configure signature using new id
            Me.ConfigureMerchantSignature()
            'update signature in table
            Call UpdateBasketMerchantSignature(Me.Ds_Merchant_Order, Me.Ds_Signature)

        End Sub

        ''' <summary>
        ''' Save book request on in basket table. Sessionid is not needed anymore because we use autoindetity id from table.
        ''' </summary>
        ''' <param name="Cod_Hotel"></param>
        ''' <param name="SESSIONID"></param>
        ''' <param name="MerchantSignature"></param>
        ''' <param name="requestFormData"></param>
        ''' <param name="TPVValueCharged"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function saveBasket2Database(Cod_Hotel As Long, SESSIONID As String, MerchantSignature As Object, requestFormData As String, TPVValueCharged As Double) As Integer

            Dim p As New Generic.List(Of SqlClient.SqlParameter)
            p.Add(ind.bk.DBMgr.getSpParameter("SESSIONID", SESSIONID))
            p.Add(ind.bk.DBMgr.getSpParameter("requestFormData", requestFormData))
            p.Add(ind.bk.DBMgr.getSpParameter("MerchantSignature", MerchantSignature))
            p.Add(ind.bk.DBMgr.getSpParameter("cod_hotel", Cod_Hotel))
            p.Add(ind.bk.DBMgr.getSpParameter("TPVValueCharged", TPVValueCharged))

            Dim result As DataTable
            result = ind.bk.DBMgr.getDataTable(Nothing, cfr.db.sp.sproc.Book.SP_Basket_SaveTemporal, p)
            If result.Rows.Count > 0 Then
                Return result.Rows(0)(0)
            End If


        End Function

        ''' <summary>
        ''' update MerchantSignature after we get the id (ds_order) from database used to construct the key signature (MerchantSignature)
        ''' </summary>
        ''' <param name="id"></param>
        ''' <param name="MerchantSignature"></param>
        ''' <param name="oConRef"></param>
        ''' <remarks></remarks>
        Private Sub UpdateBasketMerchantSignature(id As Object, MerchantSignature As String, Optional ByRef oConRef As SqlClient.SqlConnection = Nothing)

            Dim p As New Generic.List(Of SqlClient.SqlParameter)
            p.Add(ind.bk.DBMgr.getSpParameter("ID", id))
            p.Add(ind.bk.DBMgr.getSpParameter("MerchantSignature", MerchantSignature))

            ind.bk.DBMgr.getDataTable(Nothing, cfr.db.sp.sproc.Book.SP_Basket_UpdateMerchantSignature, p)

        End Sub

        ''' <summary>
        ''' update bank response in basket. tpvErrorReturned is stored in DDBB.
        ''' </summary>
        ''' <param name="id"></param>
        ''' <param name="tpvErrorReturned"></param>
        ''' <param name="oConRef"></param>
        ''' <remarks></remarks>
        Public Sub UpdateBasketBankResponse(id As Object, tpvErrorReturned As String, Optional ByRef oConRef As SqlClient.SqlConnection = Nothing)
        
            Dim p As New Generic.List(Of SqlClient.SqlParameter)
            p.Add(ind.bk.DBMgr.getSpParameter("ID", id))
            p.Add(ind.bk.DBMgr.getSpParameter("tpvErrorReturned", tpvErrorReturned))

            ind.bk.DBMgr.getDataTable(Nothing, cfr.db.sp.sproc.Book.SP_Basket_UpdateBasketBankResponse, p)
        End Sub

        ''' <summary>
        ''' After booking we store the book number in DDBB
        ''' </summary>
        ''' <param name="id"></param>
        ''' <param name="Cod_ReservaO"></param>
        ''' <param name="trans"></param>
        ''' <remarks></remarks>
        Public Shared Sub UpdateBasketCodReserva(id As Object, Cod_ReservaO As Long, Optional ByRef trans As SqlClient.SqlTransaction = Nothing)
            Dim p As New Generic.List(Of SqlClient.SqlParameter)
            p.Add(ind.bk.DBMgr.getSpParameter("ID", id))
            p.Add(ind.bk.DBMgr.getSpParameter("Cod_ReservaO", Cod_ReservaO))

            ind.bk.DBMgr.getDataTable(trans, cfr.db.sp.sproc.Book.SP_Basket_UpdateBasketCodReserva, p)
        End Sub

        ''' <summary>
        ''' Get basket by ID (ds_order)
        ''' </summary>
        ''' <param name="id"></param>
        ''' <param name="trans"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function BasketGetByID(id As Object, Optional ByRef trans As SqlClient.SqlTransaction = Nothing) As DataTable

            Dim p As New Generic.List(Of SqlClient.SqlParameter)
            p.Add(ind.bk.DBMgr.getSpParameter("ID", id))

            Dim result As DataTable
            result = ind.bk.DBMgr.getDataTable(trans, cfr.db.sp.sproc.Book.SP_Basket_GetByID, p)

            Return result

        End Function


    End Class


End Namespace



