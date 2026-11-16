Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Security.Cryptography
Imports System.Text
Imports System.Web

Namespace cfr.security
    ''' <summary>
    ''' Plaintext cancellation credential returned only when a token is issued.
    ''' </summary>
    ''' <remarks>
    ''' Token is a case-sensitive, 43-character base64url value carrying 256 bits
    ''' of entropy. It is sent once by email and is never stored in SQL Server.
    ''' ExpiresUtc is the exclusive UTC validity boundary persisted with its hash.
    ''' </remarks>
    Public NotInheritable Class CancellationTokenIssue
        Public Property Token As String
        Public Property ExpiresUtc As DateTime
    End Class

    ''' <summary>
    ''' Holds an exclusive database lease for one valid cancellation token.
    ''' </summary>
    ''' <remarks>
    ''' Complete consumes the token. Disposing without completion rolls the lease
    ''' back, allowing another attempt when the booking cancellation itself failed.
    ''' </remarks>
    Public NotInheritable Class CancellationTokenLease
        Implements IDisposable

        Private ReadOnly connection As SqlConnection
        Private ReadOnly transaction As SqlTransaction
        Private ReadOnly tokenId As Long
        Private completed As Boolean
        Private disposed As Boolean

        Friend Sub New(ByVal ownedConnection As SqlConnection,
                       ByVal ownedTransaction As SqlTransaction,
                       ByVal acquiredTokenId As Long)
            connection = ownedConnection
            transaction = ownedTransaction
            tokenId = acquiredTokenId
        End Sub

        Public Sub Complete()
            If disposed Then Throw New ObjectDisposedException(NameOf(CancellationTokenLease))
            If completed Then Throw New InvalidOperationException("The cancellation token was already consumed.")

            Using command As New SqlCommand(
                "UPDATE dbo.BookingCancellationToken " &
                "SET ConsumedUtc=@consumedUtc " &
                "WHERE TokenId=@tokenId AND ConsumedUtc IS NULL;",
                connection,
                transaction)
                command.Parameters.Add("@consumedUtc", SqlDbType.DateTime2).Value = DateTime.UtcNow
                command.Parameters.Add("@tokenId", SqlDbType.BigInt).Value = tokenId
                If command.ExecuteNonQuery() <> 1 Then
                    Throw New System.Security.SecurityException("The cancellation token is no longer valid.")
                End If
            End Using

            transaction.Commit()
            completed = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            If disposed Then Return
            disposed = True
            Try
                If Not completed Then transaction.Rollback()
            Catch
                'The connection may already be broken; disposal still must close it.
            Finally
                transaction.Dispose()
                connection.Dispose()
            End Try
        End Sub
    End Class

    ''' <summary>
    ''' Issues, validates and consumes single-use guest cancellation tokens.
    ''' </summary>
    ''' <remarks>
    ''' The database owns only SHA-256 hashes. One unconsumed token is allowed per
    ''' booking; issuing a replacement consumes every previous token. Tokens expire
    ''' at the later of two hours after issue or the day after the local arrival
    ''' date, converted with the server time zone. Booking cancellation rules remain
    ''' authoritative and may reject a token before this storage expiry.
    '''
    ''' PublicBaseUrl is a required appSetting and must be an absolute HTTP(S) URL
    ''' without credentials, query or fragment. It is consumed only when building
    ''' the cancellation page URL in confirmation email. The checked-in web.config
    ''' is the authoritative deployment value.
    ''' </remarks>
    Public NotInheritable Class CancellationTokenService
        Public Const PublicBaseUrlConfigurationKey As String = "PublicBaseUrl"
        Private Const TokenByteLength As Integer = 32
        Private Const EncodedTokenLength As Integer = 43

        Private Sub New()
        End Sub

        Public Shared Function Issue(ByVal bookingId As Integer,
                                     ByVal arrivalDateLocal As DateTime) As CancellationTokenIssue
            If bookingId <= 0 Then Throw New ArgumentOutOfRangeException(NameOf(bookingId))

            Dim createdUtc As DateTime = DateTime.UtcNow
            Dim localExpiry As DateTime = arrivalDateLocal.Date.AddDays(1)
            Dim expiresUtc As DateTime = TimeZoneInfo.ConvertTimeToUtc(
                DateTime.SpecifyKind(localExpiry, DateTimeKind.Unspecified),
                TimeZoneInfo.Local)
            If expiresUtc <= createdUtc.AddHours(2) Then expiresUtc = createdUtc.AddHours(2)

            Dim token As String = GenerateToken()
            Dim tokenHash As Byte() = ComputeHash(token)

            Using connection As SqlConnection = ind.bk.DBMgr.getNewConn()
                Using transaction As SqlTransaction = connection.BeginTransaction()
                    Using invalidate As New SqlCommand(
                        "UPDATE dbo.BookingCancellationToken " &
                        "SET ConsumedUtc=@consumedUtc " &
                        "WHERE BookingId=@bookingId AND ConsumedUtc IS NULL;",
                        connection,
                        transaction)
                        invalidate.Parameters.Add("@consumedUtc", SqlDbType.DateTime2).Value = createdUtc
                        invalidate.Parameters.Add("@bookingId", SqlDbType.Int).Value = bookingId
                        invalidate.ExecuteNonQuery()
                    End Using

                    Using insert As New SqlCommand(
                        "INSERT INTO dbo.BookingCancellationToken " &
                        "(BookingId, TokenHash, CreatedUtc, ExpiresUtc) " &
                        "VALUES (@bookingId, @tokenHash, @createdUtc, @expiresUtc);",
                        connection,
                        transaction)
                        insert.Parameters.Add("@bookingId", SqlDbType.Int).Value = bookingId
                        insert.Parameters.Add("@tokenHash", SqlDbType.Binary, 32).Value = tokenHash
                        insert.Parameters.Add("@createdUtc", SqlDbType.DateTime2).Value = createdUtc
                        insert.Parameters.Add("@expiresUtc", SqlDbType.DateTime2).Value = expiresUtc
                        insert.ExecuteNonQuery()
                    End Using
                    transaction.Commit()
                End Using
            End Using

            Return New CancellationTokenIssue With {.Token = token, .ExpiresUtc = expiresUtc}
        End Function

        Public Shared Function Acquire(ByVal bookingId As Integer,
                                       ByVal suppliedToken As String) As CancellationTokenLease
            If bookingId <= 0 OrElse Not IsTokenShapeValid(suppliedToken) Then Return Nothing

            Dim connection As SqlConnection = ind.bk.DBMgr.getNewConn()
            Dim transaction As SqlTransaction = connection.BeginTransaction(IsolationLevel.Serializable)
            Try
                Dim tokenId As Long
                Dim storedHash As Byte()
                Using command As New SqlCommand(
                    "SELECT TOP (1) TokenId, TokenHash " &
                    "FROM dbo.BookingCancellationToken WITH (UPDLOCK, HOLDLOCK) " &
                    "WHERE BookingId=@bookingId AND ConsumedUtc IS NULL AND ExpiresUtc>@nowUtc " &
                    "ORDER BY TokenId DESC;",
                    connection,
                    transaction)
                    command.Parameters.Add("@bookingId", SqlDbType.Int).Value = bookingId
                    command.Parameters.Add("@nowUtc", SqlDbType.DateTime2).Value = DateTime.UtcNow
                    Using reader As SqlDataReader = command.ExecuteReader(CommandBehavior.SingleRow)
                        If Not reader.Read() Then
                            transaction.Rollback()
                            transaction.Dispose()
                            connection.Dispose()
                            Return Nothing
                        End If
                        tokenId = reader.GetInt64(0)
                        storedHash = DirectCast(reader(1), Byte())
                    End Using
                End Using

                If Not FixedTimeEquals(storedHash, ComputeHash(suppliedToken.Trim())) Then
                    transaction.Rollback()
                    transaction.Dispose()
                    connection.Dispose()
                    Return Nothing
                End If

                Return New CancellationTokenLease(connection, transaction, tokenId)
            Catch
                Try
                    transaction.Rollback()
                Catch
                End Try
                transaction.Dispose()
                connection.Dispose()
                Throw
            End Try
        End Function

        Public Shared Function BuildCancellationUrl(ByVal bookData As cfr.BookRes) As String
            If bookData Is Nothing OrElse bookData.idEntity <= 0 Then
                Throw New ArgumentException("A persisted booking is required.", NameOf(bookData))
            End If

            Dim configured As String = ConfigurationManager.AppSettings(PublicBaseUrlConfigurationKey)
            Dim baseUri As Uri = Nothing
            If String.IsNullOrWhiteSpace(configured) OrElse
               Not Uri.TryCreate(configured.Trim().TrimEnd("/"c) & "/", UriKind.Absolute, baseUri) OrElse
               (baseUri.Scheme <> Uri.UriSchemeHttp AndAlso baseUri.Scheme <> Uri.UriSchemeHttps) OrElse
               Not String.IsNullOrEmpty(baseUri.UserInfo) OrElse
               Not String.IsNullOrEmpty(baseUri.Query) OrElse
               Not String.IsNullOrEmpty(baseUri.Fragment) Then
                Throw New ConfigurationErrorsException(
                    PublicBaseUrlConfigurationKey & " must be an absolute HTTP(S) URL without credentials, query or fragment.")
            End If

            Dim relative As String = String.Format(
                CultureInfo.InvariantCulture,
                "B_Cancel.aspx?hotelcode={0}&site={1}&language={2}&idBook={3}",
                HttpUtility.UrlEncode(CStr(bookData.hotel.id)),
                HttpUtility.UrlEncode(CStr(bookData.site.id)),
                CInt(bookData.language),
                bookData.idEntity)
            Return New Uri(baseUri, relative).AbsoluteUri
        End Function

        Public Shared Function BuildEmailInstructions(ByVal bookData As cfr.BookRes,
                                                      ByVal issue As CancellationTokenIssue) As String
            If issue Is Nothing OrElse Not IsTokenShapeValid(issue.Token) Then
                Throw New ArgumentException("A valid cancellation token issue is required.", NameOf(issue))
            End If

            Dim title As String = "Cancel this booking"
            Dim instruction As String = "Open the cancellation page and enter the booking number and cancellation code."
            Dim bookingLabel As String = "Booking number"
            Dim codeLabel As String = "Cancellation code"
            If bookData.language = CFRConst.LanguageEnum.es Then
                title = "Cancelar esta reserva"
                instruction = "Abra la página de cancelación e introduzca el número de reserva y el código de cancelación."
                bookingLabel = "Número de reserva"
                codeLabel = "Código de cancelación"
            End If

            Dim url As String = BuildCancellationUrl(bookData)
            Return "<hr><h2>" & HttpUtility.HtmlEncode(title) & "</h2>" &
                   "<p>" & HttpUtility.HtmlEncode(instruction) & "</p>" &
                   "<p><a href='" & HttpUtility.HtmlAttributeEncode(url) & "'>" & HttpUtility.HtmlEncode(title) & "</a></p>" &
                   "<p><strong>" & HttpUtility.HtmlEncode(bookingLabel) & ":</strong> " & Convert.ToString(bookData.idEntity, CultureInfo.InvariantCulture) & "<br>" &
                   "<strong>" & HttpUtility.HtmlEncode(codeLabel) & ":</strong> <code>" & HttpUtility.HtmlEncode(issue.Token) & "</code></p>"
        End Function

        Private Shared Function GenerateToken() As String
            Dim bytes(TokenByteLength - 1) As Byte
            Using random As RandomNumberGenerator = RandomNumberGenerator.Create()
                random.GetBytes(bytes)
            End Using
            Return Convert.ToBase64String(bytes).TrimEnd("="c).Replace("+", "-").Replace("/", "_")
        End Function

        Private Shared Function IsTokenShapeValid(ByVal token As String) As Boolean
            If String.IsNullOrWhiteSpace(token) Then Return False
            Dim value As String = token.Trim()
            If value.Length <> EncodedTokenLength Then Return False
            For Each character As Char In value
                If Not (Char.IsLetterOrDigit(character) OrElse character = "-"c OrElse character = "_"c) Then Return False
            Next
            Return True
        End Function

        Private Shared Function ComputeHash(ByVal token As String) As Byte()
            Using hasher As SHA256 = SHA256.Create()
                Return hasher.ComputeHash(Encoding.UTF8.GetBytes(token))
            End Using
        End Function

        Private Shared Function FixedTimeEquals(ByVal left As Byte(), ByVal right As Byte()) As Boolean
            If left Is Nothing OrElse right Is Nothing OrElse left.Length <> right.Length Then Return False
            Dim difference As Integer = 0
            For index As Integer = 0 To left.Length - 1
                difference = difference Or (left(index) Xor right(index))
            Next
            Return difference = 0
        End Function
    End Class
End Namespace
