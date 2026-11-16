Imports Microsoft.VisualBasic


Namespace cfr.obj

    <Serializable()> _
    Public Class HotelBookParameters
        Inherits ind.bk.obj.R_Res_Hot_Param
        Implements IIDObject
        Public Property idEntity() As Object Implements IIDObject.idEntity
            Get
                Return Me.cod_hotel
            End Get
            Set(ByVal value As Object)
                Me.cod_hotel = value
            End Set
        End Property


        Public Property childAgeFree As Short
            Get
                Return Me.EdadMaxNinoGratis
            End Get
            Set(value As Short)
                Me.EdadMaxNinoGratis = value
            End Set
        End Property
        Public Property childAgeToPay As Short
            Get
                Return Me.EdadMaxNinoPago
            End Get
            Set(value As Short)
                Me.EdadMaxNinoPago = value
            End Set
        End Property

        Public Property Currency As String
            Get
                If Me.moneda = "" Then
                    Me.moneda = "Eur"
                End If
                Return Me.moneda
            End Get
            Set(value As String)
                Me.moneda = value
            End Set
        End Property
 
        Public Property regSA As Boolean
            Get
                Return Me.Alojamiento
            End Get
            Set(value As Boolean)
                Me.Alojamiento = value
            End Set
        End Property

        Public Property regad As Boolean
            Get
                Return Me.AD
            End Get
            Set(value As Boolean)
                Me.AD = value
            End Set
        End Property


        Public Property regmp As Boolean
            Get
                Return Me.MP
            End Get
            Set(value As Boolean)
                Me.MP = value
            End Set
        End Property
        Public Property regpc As Boolean
            Get
                Return Me.PC
            End Get
            Set(value As Boolean)
                Me.PC = value
            End Set
        End Property

        Public Property regTI As Boolean
            Get
                Return Me.TodoIncluido
            End Get
            Set(value As Boolean)
                Me.TodoIncluido = value
            End Set
        End Property

        Public Property emailBooking As String
            Get
                Return Me.emailConfirmacion
            End Get
            Set(value As String)
                Me.emailConfirmacion = value
            End Set
        End Property

        Public Property emailBooking2 As String
            Get
                Return Me.emailConfirmacion2
            End Get
            Set(value As String)
                Me.emailConfirmacion2 = value
            End Set
        End Property

        Public Property daysInAdvance As Short
            Get
                Return Me.diasdeAntelacion
            End Get
            Set(value As Short)
                Me.diasdeAntelacion = value
            End Set
        End Property

        
        Public CCCDC As String
        

        Public regimenCountAvailable As Byte = 0
        Public Sub ValidateRegimenCount()
            If Me.regSA = True Then
                Me.regimenCountAvailable += 1
            End If
            If Me.regAD = True Then
                Me.regimenCountAvailable += 1
            End If
            If Me.regMP = True Then
                Me.regimenCountAvailable += 1
            End If
            If Me.regPC = True Then
                Me.regimenCountAvailable += 1
            End If
            If Me.regTI = True Then
                Me.regimenCountAvailable += 1
            End If
        End Sub



        Public ReadOnly Property politicyBookConditions() As String
            Get

                Select Case cfr.Language.getLanguage
                    Case CFRConst.LanguageEnum.es
                        Return Me.polEsp
                    Case CFRConst.LanguageEnum.en
                        Return Me.polIngles
                    Case CFRConst.LanguageEnum.de
                        Return Me.polAleman
                    Case CFRConst.LanguageEnum.fr
                        Return Me.polFrances
                    Case CFRConst.LanguageEnum.it
                        Return Me.polItaliano
                    Case CFRConst.LanguageEnum.pt
                        Return Me.polIngles
                    Case CFRConst.LanguageEnum.jp
                        Return Me.polIngles
                    Case CFRConst.LanguageEnum.ca
                        Return Me.polIngles
                    Case Else
                        Return Me.polEsp
                End Select

            End Get
        End Property

        Public ReadOnly Property politicyCancelation() As String
            Get

                Select Case cfr.Language.getLanguage
                    Case CFRConst.LanguageEnum.es
                        Return Me.txtCancelEsp
                    Case CFRConst.LanguageEnum.en
                        Return Me.txtCancelIngles
                    Case CFRConst.LanguageEnum.de
                        Return Me.txtCancelAleman
                    Case CFRConst.LanguageEnum.fr
                        Return Me.txtCancelFrances
                    Case CFRConst.LanguageEnum.it
                        Return Me.txtCancelItaliano
                    Case CFRConst.LanguageEnum.pt
                        Return Me.txtCancelEsp
                    Case CFRConst.LanguageEnum.jp
                        Return Me.txtCancelEsp
                    Case CFRConst.LanguageEnum.ca
                        Return Me.txtCancelEsp
                    Case Else
                        Return Me.txtCancelEsp
                End Select

            End Get
        End Property

        Public ReadOnly Property BookConfirmationMessage() As String
            Get

                Select Case cfr.Language.getLanguage
                    Case CFRConst.LanguageEnum.es
                        Return Me.txtConfirmacion
                    Case CFRConst.LanguageEnum.en
                        Return Me.txtConfirmacionIngles
                    Case CFRConst.LanguageEnum.de
                        Return Me.txtConfirmacionAleman
                    Case CFRConst.LanguageEnum.fr
                        Return Me.txtConfirmacionFrances
                    Case CFRConst.LanguageEnum.it
                        Return Me.txtConfirmacionItaliano
                    Case CFRConst.LanguageEnum.pt
                        Return Me.txtConfirmacion
                    Case CFRConst.LanguageEnum.jp
                        Return Me.txtConfirmacion
                    Case CFRConst.LanguageEnum.ca
                        Return Me.txtConfirmacion
                    Case Else
                        Return Me.txtConfirmacion
                End Select

            End Get
        End Property
    End Class
End Namespace
