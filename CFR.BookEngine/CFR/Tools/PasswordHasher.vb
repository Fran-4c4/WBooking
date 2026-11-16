Imports System.Globalization
Imports System.Security.Cryptography

Namespace cfr.security
    ''' <summary>
    ''' Owns the persisted password-hash contract for hotel-administration and
    ''' operator/agency users.
    ''' </summary>
    ''' <remarks>
    ''' Stored values use PBKDF2-HMAC-SHA256$iterations$salt$derived-key, with the
    ''' salt and derived key encoded as Base64. Iterations is stored per hash so
    ''' the work factor can be raised and upgraded after a successful login.
    ''' Passwords are never decryptable. Changing the prefix, field order,
    ''' algorithm, salt size or key size requires compatibility verification for
    ''' existing R_Usuarios.clave and R_operadores.Clave values, the offline
    ''' reset utility and the operator batch-migration utility. Persisted
    ''' columns allow 256 characters; the current serialized hash uses 95.
    ''' </remarks>
    Public NotInheritable Class PasswordHasher
        ''' <summary>
        ''' Authoritative work factor for newly created hashes. This value is
        ''' calibrated for the required legacy x86 process to keep one hash near
        ''' one second; benchmark it before increasing it. Existing hashes retain
        ''' their recorded value and are upgraded after authentication.
        ''' </summary>
        Public Const CurrentIterations As Integer = 150000

        Private Const HashPrefix As String = "PBKDF2-HMAC-SHA256"
        Private Const SaltSizeBytes As Integer = 16
        Private Const DerivedKeySizeBytes As Integer = 32
        Private Const MaximumPasswordLength As Integer = 256

        Private Sub New()
        End Sub

        Public Shared Function HashPassword(ByVal password As String) As String
            If String.IsNullOrEmpty(password) Then
                Throw New ArgumentException("La contraseña no puede estar vacía.", NameOf(password))
            End If
            If password.Length > MaximumPasswordLength Then
                Throw New ArgumentException("La contraseña supera la longitud máxima permitida.", NameOf(password))
            End If

            Dim salt(SaltSizeBytes - 1) As Byte
            Using randomGenerator As RandomNumberGenerator = RandomNumberGenerator.Create()
                randomGenerator.GetBytes(salt)
            End Using

            Dim derivedKey As Byte()
            Using deriveBytes As New Rfc2898DeriveBytes(password, salt, CurrentIterations, HashAlgorithmName.SHA256)
                derivedKey = deriveBytes.GetBytes(DerivedKeySizeBytes)
            End Using

            Return String.Join("$",
                               HashPrefix,
                               CurrentIterations.ToString(CultureInfo.InvariantCulture),
                               Convert.ToBase64String(salt),
                               Convert.ToBase64String(derivedKey))
        End Function

        Public Shared Function VerifyPassword(ByVal password As String, ByVal storedHash As String) As Boolean
            If String.IsNullOrEmpty(password) OrElse
               password.Length > MaximumPasswordLength OrElse
               Not IsPasswordHash(storedHash) Then
                Return False
            End If

            Try
                Dim parts As String() = storedHash.Split("$"c)
                If parts.Length <> 4 Then
                    Return False
                End If

                Dim iterations As Integer
                If Not Integer.TryParse(parts(1), NumberStyles.None, CultureInfo.InvariantCulture, iterations) OrElse
                   iterations < 1 Then
                    Return False
                End If

                Dim salt As Byte() = Convert.FromBase64String(parts(2))
                Dim expectedKey As Byte() = Convert.FromBase64String(parts(3))
                If salt.Length < SaltSizeBytes OrElse expectedKey.Length <> DerivedKeySizeBytes Then
                    Return False
                End If

                Dim actualKey As Byte()
                Using deriveBytes As New Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256)
                    actualKey = deriveBytes.GetBytes(expectedKey.Length)
                End Using

                Return FixedTimeEquals(actualKey, expectedKey)
            Catch ex As FormatException
                Return False
            Catch ex As CryptographicException
                Return False
            End Try
        End Function

        Public Shared Function IsPasswordHash(ByVal value As String) As Boolean
            Return Not String.IsNullOrEmpty(value) AndAlso
                   value.StartsWith(HashPrefix & "$", StringComparison.Ordinal)
        End Function

        Public Shared Function NeedsUpgrade(ByVal storedHash As String) As Boolean
            If Not IsPasswordHash(storedHash) Then
                Return True
            End If

            Dim parts As String() = storedHash.Split("$"c)
            Dim iterations As Integer
            Return parts.Length <> 4 OrElse
                   Not Integer.TryParse(parts(1), NumberStyles.None, CultureInfo.InvariantCulture, iterations) OrElse
                   iterations < CurrentIterations
        End Function

        Private Shared Function FixedTimeEquals(ByVal left As Byte(), ByVal right As Byte()) As Boolean
            If left Is Nothing OrElse right Is Nothing OrElse left.Length <> right.Length Then
                Return False
            End If

            Dim difference As Integer = 0
            For index As Integer = 0 To left.Length - 1
                difference = difference Or (left(index) Xor right(index))
            Next
            Return difference = 0
        End Function
    End Class
End Namespace
