Imports Microsoft.VisualBasic

Namespace cfr.obj

    <Serializable()> _
    Public Class Country
        Implements IIDObject

        Public Property idEntity() As Object Implements IIDObject.idEntity
            Get
                Return Me.cod_Pais
            End Get
            Set(ByVal value As Object)
                Me.cod_Pais = value
            End Set
        End Property

        Public cod_Pais As Integer
        Public Description As String
        Public cod_cont As Integer
        Public prefijoTelfPais As String
        Public idioma1 As String
        Public idioma2 As String
        Public idioma3 As String
        Public idioma4 As String


        Public ReadOnly Property DescriptionByLang() As String
            Get
                Select Case cfr.Language.getLanguage
                    Case CFRConst.LanguageEnum.es
                        Return Me.Description
                    Case CFRConst.LanguageEnum.en
                        Return Me.idioma1
                    Case CFRConst.LanguageEnum.de
                        Return Me.idioma2
                    Case CFRConst.LanguageEnum.fr
                        Return Me.idioma3
                    Case CFRConst.LanguageEnum.it
                        Return Me.idioma4
                        'Case CFRConst.LanguageEnum.pt
                        '    Return Me.desc_pt
                        'Case CFRConst.LanguageEnum.jp
                        '    Return Me.desc_jp
                        'Case CFRConst.LanguageEnum.ca
                        '    Return Me.desc_cat
                    Case Else
                        Return Me.Description
                End Select
            End Get
        End Property
    End Class
End Namespace