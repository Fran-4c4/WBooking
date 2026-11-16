Imports Microsoft.VisualBasic

Namespace cfr.obj

    <Serializable()> _
    Public Class Photo
        Implements IIDObject

        Public Enum photoType As Short
            Front = 1
            Room = 2
            saloon = 3
            Other = 4

        End Enum


        Public Property idEntity() As Object Implements IIDObject.idEntity
            Get
                Return Me.id_Foto
            End Get
            Set(ByVal value As Object)
                Me.id_Foto = value
            End Set
        End Property

        Public cod_hotel As Integer
        Public id_Foto As Integer
        Public FotoFileName As String
        Public Description As String
        Public LastUpdate As Date
        Public id_fotoType As cfr.obj.Photo.photoType


    End Class
End Namespace
