Imports System.Security.Cryptography

Namespace cfr.security
    ''' <summary>
    ''' Owns the administrator persistent-session token contract.
    ''' </summary>
    ''' <remarks>
    ''' The browser receives a URL-safe token containing 256 random bits. Only a
    ''' 120-bit SHA-256 fingerprint, encoded as 20 URL-safe characters, is stored
    ''' in R_UsuariosSession.sessionkey because that database column is varchar(20).
    ''' The fingerprint is sufficient for lookup but cannot be used as the bearer
    ''' token. Changing either size invalidates every remembered login and requires
    ''' reviewing the database column, cookie consumers and session lookup queries.
    ''' Values written by the former plaintext-token implementation are deliberately
    ''' not accepted and users must sign in again once after this upgrade.
    ''' </remarks>
    Public NotInheritable Class PersistentSessionToken
        Public Const RawTokenSizeBytes As Integer = 32
        Public Const StoredFingerprintSizeBytes As Integer = 15
        Public Const StoredFingerprintLength As Integer = 20

        Private Sub New()
        End Sub

        Public Shared Function Create() As String
            Dim token(RawTokenSizeBytes - 1) As Byte
            Using generator As RandomNumberGenerator = RandomNumberGenerator.Create()
                generator.GetBytes(token)
            End Using
            Return ToBase64Url(token)
        End Function

        Public Shared Function FingerprintForStorage(ByVal rawToken As String) As String
            If String.IsNullOrWhiteSpace(rawToken) OrElse rawToken.Length > 256 Then
                Return Nothing
            End If

            Dim digest As Byte()
            Using hasher As SHA256 = SHA256.Create()
                digest = hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawToken))
            End Using

            Dim fingerprint(StoredFingerprintSizeBytes - 1) As Byte
            Buffer.BlockCopy(digest, 0, fingerprint, 0, fingerprint.Length)
            Dim encoded As String = ToBase64Url(fingerprint)
            If encoded.Length <> StoredFingerprintLength Then
                Throw New CryptographicException("The persistent-session fingerprint has an invalid length.")
            End If
            Return encoded
        End Function

        Private Shared Function ToBase64Url(ByVal value As Byte()) As String
            Return Convert.ToBase64String(value).TrimEnd("="c).Replace("+"c, "-"c).Replace("/"c, "_"c)
        End Function
    End Class
End Namespace
