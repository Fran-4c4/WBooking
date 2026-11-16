Namespace cfr.security
    ''' <summary>
    ''' Sanitizes administrator-authored rich text before it reaches storage.
    ''' </summary>
    ''' <remarks>
    ''' The contract intentionally permits only structural text formatting and
    ''' ordinary http, https and mailto links. Images, embedded content, CSS,
    ''' event attributes, forms and scripts are never accepted. Existing stored
    ''' values are sanitized again when loaded into an editor and saved.
    ''' </remarks>
    Public NotInheritable Class RichHtmlSanitizer
        Private Shared ReadOnly AllowedElementNames As String() = {
            "p", "br", "strong", "b", "em", "i", "u", "ul", "ol", "li",
            "h2", "h3", "h4", "blockquote", "a", "span"
        }

        Private Sub New()
        End Sub

        Public Shared Function Sanitize(ByVal html As String) As String
            If String.IsNullOrEmpty(html) Then
                Return String.Empty
            End If

            Dim sanitizer As New Ganss.Xss.HtmlSanitizer()
            sanitizer.AllowedTags.Clear()
            For Each elementName As String In AllowedElementNames
                sanitizer.AllowedTags.Add(elementName)
            Next

            sanitizer.AllowedAttributes.Clear()
            sanitizer.AllowedAttributes.Add("href")
            sanitizer.AllowedAttributes.Add("title")
            sanitizer.AllowedSchemes.Clear()
            sanitizer.AllowedSchemes.Add("http")
            sanitizer.AllowedSchemes.Add("https")
            sanitizer.AllowedSchemes.Add("mailto")
            sanitizer.AllowedCssProperties.Clear()
            sanitizer.AllowedAtRules.Clear()

            Return sanitizer.Sanitize(html)
        End Function
    End Class
End Namespace
