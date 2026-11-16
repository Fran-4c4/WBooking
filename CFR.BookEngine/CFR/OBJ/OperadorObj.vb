Imports Microsoft.VisualBasic

Namespace cfr

    <Serializable()> _
    Public Class OperatorObj
        Inherits ind.bk.obj.R_operadores
        Implements IIDObject
        
        'Datos del Operador

        Public Property id() As Object Implements IIDObject.idEntity
            Get
                Return Me.cod_operador
            End Get
            Set(ByVal value As Object)
                Me.cod_operador = value
            End Set
        End Property



        Public Shadows Property Freesale As cfr.CFRConst.genericOperator
            Get
                Return MyBase.FreeSale
            End Get
            Set(value As cfr.CFRConst.genericOperator)
                MyBase.FreeSale = value
            End Set
        End Property

      
        Public Shadows Property tipoOperador As CFRConst.ClientTypeEnum  'Tipo en bbdd

            Get
                Return Me.tipo
            End Get
            Set(value As CFRConst.ClientTypeEnum)
                Me.tipo = value
            End Set
        End Property

        Public Shadows Property forma_pago As CFRConst.formaPago
            Get
                Return MyBase.forma_pago
            End Get
            Set(value As CFRConst.formaPago)
                MyBase.forma_pago = value
            End Set
        End Property
        
        Public ReadOnly Property politicyBookConditions() As String
            Get

                Select Case cfr.Language.getLanguage
                    Case CFRConst.LanguageEnum.es
                        Return Me.politica
                    Case CFRConst.LanguageEnum.en
                        Return Me.polIngles
                    Case CFRConst.LanguageEnum.de
                        Return Me.polAleman
                    Case CFRConst.LanguageEnum.fr
                        Return Me.polFrances
                    Case CFRConst.LanguageEnum.it
                        Return Me.polItaliano
                    Case CFRConst.LanguageEnum.pt
                        Return Me.polCatalan
                    Case CFRConst.LanguageEnum.jp
                        Return Me.polIngles
                    Case CFRConst.LanguageEnum.ca
                        Return Me.politica
                    Case Else
                        Return Me.polIngles
                End Select

            End Get
        End Property

    End Class
End Namespace