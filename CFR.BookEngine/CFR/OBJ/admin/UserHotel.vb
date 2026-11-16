Imports ind.bk.obj
Imports Microsoft.VisualBasic



Namespace cfr.obj

    <Serializable()> _
    Public Class UserHotel123
        Inherits r_usuarios
        Implements IIDObject
        Public Property idEntity() As Object Implements IIDObject.idEntity
            Get
                Return Me.id_usuario
            End Get
            Set(ByVal value As Object)
                Me.id_usuario = value
            End Set
        End Property

        'Public cod_hotel As Integer
        'Public usuario As String
        'Public login As String
        'Public clave As String
        'Public perfil As String
        'Public email As String
        'Public id_usuario As Integer
        'Public ultimoacceso As Date

    End Class
End Namespace
