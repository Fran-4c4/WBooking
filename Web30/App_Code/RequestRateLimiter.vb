Imports System.Security.Cryptography
Imports System.Text
Imports System.Web

''' <summary>
''' Provides a bounded, process-local fixed-window limiter for sensitive public endpoints.
''' </summary>
''' <remarks>
''' Entries are scoped by an endpoint-owned scope name and a SHA-256 digest of the
''' supplied subject, so login names and booking identifiers are not retained in
''' cache keys. Limits apply independently in each web process; a multi-instance
''' deployment must replace this implementation with a shared store while keeping
''' the same endpoint limits and reset semantics.
''' </remarks>
Public NotInheritable Class RequestRateLimiter
    Private NotInheritable Class AttemptWindow
        Public StartedAtUtc As DateTime
        Public Failures As Integer
    End Class

    Private Shared ReadOnly Gate As New Object()
    Private Const CacheKeyPrefix As String = "__WBookingRateLimit:"

    Private Sub New()
    End Sub

    Public Shared Function IsBlocked(ByVal scope As String,
                                     ByVal subject As String,
                                     ByVal maximumFailures As Integer,
                                     ByVal window As TimeSpan) As Boolean
        ValidateContract(scope, maximumFailures, window)
        Dim key As String = BuildKey(scope, subject)
        SyncLock Gate
            Dim current As AttemptWindow = TryCast(HttpRuntime.Cache(key), AttemptWindow)
            If current Is Nothing OrElse DateTime.UtcNow - current.StartedAtUtc >= window Then
                HttpRuntime.Cache.Remove(key)
                Return False
            End If
            Return current.Failures >= maximumFailures
        End SyncLock
    End Function

    Public Shared Sub RecordFailure(ByVal scope As String,
                                    ByVal subject As String,
                                    ByVal maximumFailures As Integer,
                                    ByVal window As TimeSpan)
        ValidateContract(scope, maximumFailures, window)
        Dim key As String = BuildKey(scope, subject)
        SyncLock Gate
            Dim nowUtc As DateTime = DateTime.UtcNow
            Dim current As AttemptWindow = TryCast(HttpRuntime.Cache(key), AttemptWindow)
            If current Is Nothing OrElse nowUtc - current.StartedAtUtc >= window Then
                current = New AttemptWindow With {.StartedAtUtc = nowUtc, .Failures = 0}
            End If
            If current.Failures < Integer.MaxValue Then
                current.Failures += 1
            End If
            HttpRuntime.Cache.Insert(key, current, Nothing, current.StartedAtUtc.Add(window), System.Web.Caching.Cache.NoSlidingExpiration)
        End SyncLock
    End Sub

    Public Shared Sub Reset(ByVal scope As String, ByVal subject As String)
        If String.IsNullOrWhiteSpace(scope) Then
            Throw New ArgumentException("A rate-limit scope is required.", "scope")
        End If
        HttpRuntime.Cache.Remove(BuildKey(scope, subject))
    End Sub

    Private Shared Sub ValidateContract(ByVal scope As String,
                                        ByVal maximumFailures As Integer,
                                        ByVal window As TimeSpan)
        If String.IsNullOrWhiteSpace(scope) Then
            Throw New ArgumentException("A rate-limit scope is required.", "scope")
        End If
        If maximumFailures < 1 Then
            Throw New ArgumentOutOfRangeException("maximumFailures")
        End If
        If window <= TimeSpan.Zero Then
            Throw New ArgumentOutOfRangeException("window")
        End If
    End Sub

    Private Shared Function BuildKey(ByVal scope As String, ByVal subject As String) As String
        Dim normalizedSubject As String = If(subject, String.Empty).Trim().ToUpperInvariant()
        Dim digest As Byte()
        Using hasher As SHA256 = SHA256.Create()
            digest = hasher.ComputeHash(Encoding.UTF8.GetBytes(normalizedSubject))
        End Using
        Return CacheKeyPrefix & scope & ":" & Convert.ToBase64String(digest)
    End Function
End Class
