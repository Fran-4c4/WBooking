Imports Microsoft.VisualBasic

Namespace cfr
    ''' <summary>
    ''' site theme behavior
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class SiteThemeObj
        ''' <summary>
        ''' principal background color
        ''' </summary>
        ''' <remarks></remarks>
        Public colorPrincipal As String = "6E78E9"
        Public colorPrincipalFont As String = "FFFFFF"

        Public colorSecundary As String = "B6B6B6"
        Public colorSecundaryFont As String = "000000"

        Public Function getTheme(code As String) As DataRow
            Try
                Dim mgr As New ind.bk.obj.Sites_Manager
                Dim ssql As String = String.Format("SELECT  * FROM [sites] where cod_site='{0}' ", code)
                Dim dt As DataTable = ind.bk.DBMgr.getDT(ssql)
                If dt.Rows.Count > 0 Then
                    Dim dr As DataRow = dt(0)
                    Dim css As String = dr("css")
                    Dim values As String() = css.Split(";")

                    Me.colorPrincipal = values(0)
                    Me.colorPrincipalFont = values(1)
                    Me.colorSecundary = values(2)

                    Me.colorSecundaryFont = values(3)
                    Return dr
                End If
                Return Nothing
            Catch ex As Exception

            End Try
        End Function


    End Class
End Namespace