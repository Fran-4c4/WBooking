Imports Microsoft.VisualBasic

Namespace cfr

    <Serializable()> _
    Public Class DescriptionByLanguage
        Public es As String
        Public en As String
        Public de As String
        Public fr As String
        Public ca As String
        Public jp As String
        Public it As String
        Public pt As String

        Public Sub setLangDes(ByVal value As String)
            Dim lang As CFRConst.LanguageEnum = cfr.Language.getLanguage
            Select Case lang
                Case CFRConst.LanguageEnum.es
                    Me.es = value
                Case CFRConst.LanguageEnum.en
                    Me.en = value
                Case CFRConst.LanguageEnum.de
                    Me.de = value
                Case CFRConst.LanguageEnum.fr
                    Me.fr = value
                Case CFRConst.LanguageEnum.it
                    Me.it = value
                Case CFRConst.LanguageEnum.ca
                    Me.ca = value
                Case CFRConst.LanguageEnum.pt
                    Me.pt = value
                Case Else
                    Me.en = value
            End Select
        End Sub

        ''' <summary>
        ''' get room name by language
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getDes() As String
            Return Me.getDes(cfr.Language.getLanguage)
        End Function



        Public Function getDes(ByVal lang As CFRConst.LanguageEnum) As String
            Select Case lang
                Case CFRConst.LanguageEnum.es
                    Return Me.es
                Case CFRConst.LanguageEnum.en
                    Return Me.en
                Case CFRConst.LanguageEnum.de
                    Return Me.de
                Case CFRConst.LanguageEnum.fr
                    Return Me.fr
                Case CFRConst.LanguageEnum.it
                    Return Me.it
                Case CFRConst.LanguageEnum.ca
                    Return Me.ca
                Case CFRConst.LanguageEnum.pt
                    Return Me.pt
                Case Else
                    Return Me.en
            End Select
        End Function
    End Class
End Namespace