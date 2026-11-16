Imports Microsoft.VisualBasic
Imports cfr.tool
Imports System.Data

Namespace cfr


    Public Class OperadorManager

        Public operador As OperatorObj
        
#Region "Ctor"
        Public Sub New()

        End Sub
        Public Sub New(ByVal cod_operador As String)

            Me.operador = OperadorManager.GetOperador(cod_operador)
        End Sub
#End Region


#Region "Methods"
        Public Function getOperatorPrivateGeneric(ByVal cod_hotel As Integer) As OperatorObj
            Dim code As String = OperadorManager.GetCodOperadorGeneric(cod_hotel, CFRConst.genericOperator.PrivateGeneral)
            Return OperadorManager.GetOperador(code)
        End Function


        Public Function getOperatorFreeSaleGeneric(ByVal cod_hotel As Integer) As OperatorObj
            Dim code As String = OperadorManager.GetCodOperadorGeneric(cod_hotel, CFRConst.genericOperator.freeSale)
            Return OperadorManager.GetOperador(code)
        End Function


        Public Shared Function GetOperador(ByVal cod_operador As Integer) As OperatorObj
 
            Dim dt As Data.DataTable

            Dim opermgr As New ind.bk.obj.R_operadores_Manager
            dt = opermgr.getDataTableByID(cod_operador, Nothing)
            If dt.Rows.Count = 0 Then
                Throw New Exception(cfr.Language.getCaption(349))
            End If
            
            Dim oRs As Data.DataRow = dt.Rows(0)
            Dim o As New OperatorObj

            o.FillByRow(oRs)
            Return o

        End Function


        Public Shared Function GetCodOperadorGeneric(ByVal cod_Hotel As String, ByVal operatorType As CFRConst.genericOperator) As String
            Dim sSQL As String

            sSQL = "Select * from R_operadores "
            sSQL = sSQL & " Where freesale='" & CInt(operatorType).ToString & "'"
            sSQL = sSQL & " and cod_Hotel = '" & cod_Hotel & "'"

            Dim dt As Data.DataTable = ind.bk.DBMgr.getDT(sSQL)
            If dt.Rows.Count = 0 Then
                Throw New Exception("This operator not exist.")
            Else
                Return dt.Rows(0)("cod_operador")
            End If
        End Function


        Public Shared Function getOperatorByCredentials(ByVal Login As String, ByVal clave As String) As Data.DataTable
            Dim result As New DataTable
            result.Columns.Add("cod_operador", GetType(Integer))
            result.Columns.Add("operador", GetType(String))
            result.Columns.Add("cod_cadena", GetType(Integer))

            Dim operatorEntity As R_operadorEF = FindOperatorByCredentials(Login, clave)
            If operatorEntity IsNot Nothing Then
                Dim row As DataRow = result.NewRow()
                row("cod_operador") = operatorEntity.cod_operador
                row("operador") = operatorEntity.Operador
                row("cod_cadena") = If(operatorEntity.cod_Cadena.HasValue,
                                        CType(operatorEntity.cod_Cadena.Value, Object),
                                        DBNull.Value)
                result.Rows.Add(row)
            End If

            Return result
        End Function

        Public Shared Function getOperatorPromotionObjByCredentials(ByVal hotelCode As String, ByVal promotioncode As String, datein As Date, dateOut As Date) As cfr.OperatorObj

            Dim parsedHotelCode As Integer
            If Not Integer.TryParse(hotelCode, parsedHotelCode) Then
                Throw New Exception(cfr.Language.GetCaption(259))
            End If

            Using ctx As New cfrbookEntities
                'A promotion code identifies a discount; it is not an account
                'password and therefore never needs to equal Clave.
                Dim promotion = (From candidate In ctx.R_operadorEF
                                 Where candidate.cod_hotel = parsedHotelCode AndAlso
                                       candidate.login = promotioncode AndAlso
                                       candidate.discountapply = True AndAlso
                                       candidate.discountapplyfrom <= datein AndAlso
                                       candidate.discountapplyuntil >= dateOut
                                 Select candidate).FirstOrDefault()

                If promotion Is Nothing Then
                    Throw New Exception(cfr.Language.GetCaption(259))
                End If

                Return GetOperador(promotion.cod_operador)
            End Using
        End Function

        Public Shared Function getOperatorObjByCredentials(ByVal hotelCode As String, ByVal Login As String, ByVal clave As String) As cfr.OperatorObj
            Dim parsedHotelCode As Integer
            If Not Integer.TryParse(hotelCode, parsedHotelCode) Then
                Throw New Exception(cfr.Language.getCaption(259))
            End If

            Dim operatorEntity As R_operadorEF = FindOperatorByCredentials(Login, clave, parsedHotelCode)
            If operatorEntity Is Nothing Then
                Throw New Exception(cfr.Language.getCaption(259))
            End If

            Return GetOperador(operatorEntity.cod_operador)
        End Function

        Private Shared Function FindOperatorByCredentials(ByVal login As String,
                                                          ByVal password As String,
                                                          Optional ByVal hotelCode As Nullable(Of Integer) = Nothing) As R_operadorEF
            Using ctx As New cfrbookEntities
                Dim candidates = From candidate In ctx.R_operadorEF
                                 Where candidate.login = login
                                 Select candidate

                If hotelCode.HasValue Then
                    candidates = candidates.Where(Function(candidate) candidate.cod_hotel = hotelCode.Value)
                End If

                For Each candidate As R_operadorEF In candidates.ToList()
                    Dim passwordMatches As Boolean
                    If cfr.security.PasswordHasher.IsPasswordHash(candidate.Clave) Then
                        passwordMatches = cfr.security.PasswordHasher.VerifyPassword(password, candidate.Clave)
                    Else
                        'Compatibility for a database restored from an old backup:
                        'the first successful login replaces plaintext immediately.
                        passwordMatches = String.Equals(candidate.Clave, password, StringComparison.Ordinal)
                    End If

                    If passwordMatches Then
                        If cfr.security.PasswordHasher.NeedsUpgrade(candidate.Clave) Then
                            candidate.Clave = cfr.security.PasswordHasher.HashPassword(password)
                            ctx.SaveChanges()
                        End If
                        Return candidate
                    End If
                Next
            End Using

            Return Nothing
        End Function

        ''' <summary>
        '''  Check if a user have privileges to cancel a book
        ''' </summary>
        ''' <param name="Login"></param>
        ''' <param name="clave"></param>
        ''' <param name="idBook"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function checkOperatorPrivilege2CancelBook(ByVal Login As String, ByVal clave As String, ByVal idBook As String) As Boolean


            Dim cod_Operador_reserva As String = ""
            Dim cod_operador As String = ""


            Dim dt As Data.DataTable = cfr.OperadorManager.getOperatorByCredentials(Login, clave)

            If dt.Rows.Count > 0 Then
                cod_operador = dt.Rows(0)("cod_operador")
            End If


            Dim dtBook As Data.DataTable = cfr.BookManager.GetBookedById(idBook)


            If dtBook.Rows.Count > 0 Then
                cod_Operador_reserva = dtBook.Rows(0)("operador")
                If Login = dtBook.Rows(0)("login") And clave = dtBook.Rows(0)("clave") Then
                    cod_operador = cod_Operador_reserva
                End If
            End If
            'if the bookOperator is diferent then he haven´t privileges
            If cod_Operador_reserva <> cod_operador Then
                'don´t have privilege
                Return False
            End If

            'have privilege to cancel
            Return True


        End Function


        ''' <summary>
        ''' Grandes cuentas/tarifas asignadas al operador tipo agencia
        ''' </summary>
        ''' <param name="cod_Hotel"></param>
        ''' <param name="cod_Agencia"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ListCompaniesWorkingWithAgency(ByRef cod_Hotel As Double, ByVal cod_Agencia As Double) As DataTable
            Dim sSQL As String

            sSQL = "SELECT R_Agencia_Empresa.cod_Agencia, R_Agencia_Empresa.cod_Empresa "
            sSQL = sSQL & " , R_operadores.cod_operador, R_operadores.cod_hotel "
            sSQL = sSQL & " , R_operadores.Operador "
            sSQL = sSQL & " FROM R_Agencia_Empresa INNER JOIN "
            sSQL = sSQL & " R_operadores ON R_Agencia_Empresa.cod_Empresa = R_operadores.cod_operador "
            sSQL = sSQL & " WHERE (R_Agencia_Empresa.cod_Agencia = '" & cod_Agencia & "')"
            sSQL = sSQL & " AND R_Agencia_Empresa.cod_hotel=" & cod_Hotel

            sSQL = sSQL & " ORDER BY R_operadores.Operador "

            Return ind.bk.DBMgr.getDT(sSQL)
        End Function


#End Region

#Region "Internal Helpers"

#End Region


    End Class
End Namespace
