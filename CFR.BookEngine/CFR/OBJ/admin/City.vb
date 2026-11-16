Imports Microsoft.VisualBasic

Namespace cfr.obj

    <Serializable()> _
    Public Class City
        Implements IIDObject


        Public cod_ciudad As Integer
        Public country As cfr.obj.Country
        Public desc_esp As String 'español
        Public ciudad1 As String
        Public ciudad2 As String
        Public ciudad3 As String
        Public ciudad4 As String

        Public Property idEntity() As Object Implements IIDObject.idEntity
            Get
                Return Me.cod_ciudad
            End Get
            Set(ByVal value As Object)
                Me.cod_ciudad = value
            End Set
        End Property

        Public ReadOnly Property DescriptionByLang() As String
            Get
                Select Case cfr.Language.getLanguage
                    Case CFRConst.LanguageEnum.es
                        Return Me.desc_esp
                    Case CFRConst.LanguageEnum.en
                        Return Me.ciudad1
                    Case CFRConst.LanguageEnum.de
                        Return Me.ciudad2
                    Case CFRConst.LanguageEnum.fr
                        Return Me.ciudad3
                        'Case CFRConst.LanguageEnum.it
                        '    Return Me.desc_it
                        'Case CFRConst.LanguageEnum.pt
                        '    Return Me.desc_pt
                        'Case CFRConst.LanguageEnum.jp
                        '    Return Me.desc_jp
                        'Case CFRConst.LanguageEnum.ca
                        '    Return Me.desc_cat
                    Case Else
                        Return Me.desc_esp
                End Select
            End Get
        End Property
    End Class
End Namespace
