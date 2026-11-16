Imports Microsoft.VisualBasic

Namespace cfr.obj
    <Serializable()> _
    Public Class HotelDescription
        Implements IIDObject

        Public Property idEntity() As Object Implements IIDObject.idEntity
            Get
                Return Me.cod_Hotel
            End Get
            Set(ByVal value As Object)
                Me.cod_Hotel = value
            End Set
        End Property



        Public cod_Hotel As Integer
        Public desc_esp As String
        Public desc_en As String
        Public desc_de As String
        Public desc_fr As String
        Public desc_cat As String
        Public desc_it As String
        Public desc_pt As String
        Public desc_jp As String


        Public ReadOnly Property DescriptionByLang() As String
            Get
                Select Case cfr.Language.getLanguage
                    Case CFRConst.LanguageEnum.es
                        Return Me.desc_esp
                    Case CFRConst.LanguageEnum.en
                        Return Me.desc_en
                    Case CFRConst.LanguageEnum.de
                        Return Me.desc_de
                    Case CFRConst.LanguageEnum.fr
                        Return Me.desc_fr
                    Case CFRConst.LanguageEnum.it
                        Return Me.desc_it
                    Case CFRConst.LanguageEnum.pt
                        Return Me.desc_pt
                    Case CFRConst.LanguageEnum.jp
                        Return Me.desc_jp
                    Case CFRConst.LanguageEnum.ca
                        Return Me.desc_cat
                    Case Else
                        Return Me.desc_esp
                End Select
            End Get
        End Property

        Public Sub New()

        End Sub

        Public Sub New(ByVal cod_Hotel As Integer)
            Me.cod_Hotel = cod_Hotel
        End Sub

    End Class
End Namespace