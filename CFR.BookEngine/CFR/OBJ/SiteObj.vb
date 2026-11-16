Imports Microsoft.VisualBasic


Namespace cfr
    ''' <summary>
    ''' site information. For hotel we use hotel information. But CSS styles are here.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class SiteObj
        Implements IIDObject

        Private code As String
        Public description As String

        Public masterPageCode As String = ""

        Public direccion As String
        Public emailcontacto As String
        Public emailreservas As String
        Public website As String
        Public hostname As String
        Public showPrincipal As Boolean = False
        Public tipo As String = "0" '0=site 1=hotel 2=renacar

        Public Shared siteCollection As New Hashtable

        Public Shared Function getSite(siteid As String) As SiteObj
            Dim s As SiteObj = siteCollection(siteid)
            If s Is Nothing Then
                s = New SiteObj
                s.id = siteid
                s.getTheme()
                siteCollection.Add(siteid, s)
            End If

            Return s

        End Function

        Public Property id() As Object Implements IIDObject.idEntity
            Get
                Return Me.code
            End Get
            Set(ByVal value As Object)
                Me.code = value
            End Set
        End Property

        Dim themeVar As SiteThemeObj
        Public Property theme As SiteThemeObj

            Get
                If Me.themeVar Is Nothing Then
                    Me.themeVar = New SiteThemeObj
                    Me.getTheme()

                End If
                Return Me.themeVar
            End Get
            Set(value As SiteThemeObj)
                Me.themeVar = value
            End Set
        End Property

        Public Sub getTheme()
            Try
                If Me.code = "" Then
                    Exit Sub
                End If
                If Me.themeVar Is Nothing Then
                    Me.themeVar = New SiteThemeObj
                End If

                Dim dr As DataRow = Me.themeVar.getTheme(Me.code)

                If Not dr Is Nothing Then

                    Me.masterPageCode = "" & dr("masterPageCode")
                    Me.tipo = "" & dr("tipo")
                End If
            Catch ex As Exception

            End Try
        End Sub


    End Class
End Namespace