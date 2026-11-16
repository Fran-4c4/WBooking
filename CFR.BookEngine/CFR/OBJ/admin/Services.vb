Imports Microsoft.VisualBasic

Namespace cfr.obj

    <Serializable()> _
    Public Class Services
        Implements IIDObject
        Public Property idEntity() As Object Implements IIDObject.idEntity
            Get
                Return Me.cod_Servicio
            End Get
            Set(ByVal value As Object)
                Me.cod_Servicio = value
            End Set
        End Property
        Public cod_Servicio As Integer
        Public servicio As String
        Public Ingles As String
        Public Aleman As String
        Public Frances As String
        Public Catalan As String
        Public Buscar As Boolean
        Public Padre As Integer
        Public tipo_carac As Integer

    End Class
End Namespace