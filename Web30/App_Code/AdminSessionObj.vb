Imports Microsoft.VisualBasic


Namespace cfr.admin

    <Serializable()> _
    Public Class AdminSessionObj
        Implements IIDObject


        Dim sessionId As Object
        Public Property idEntity() As Object Implements IIDObject.idEntity
            Get
                Return Me.sessionId
            End Get
            Set(ByVal value As Object)
                Me.sessionId = value
            End Set
        End Property

        ''' <summary>
        ''' list with hotels in session
        ''' </summary>
        ''' <remarks></remarks>
        Public sessionAccessList As New Hashtable

        Public Sub addSessionContainer(usersessionContainer As sessionContainer)
            Dim id As String = usersessionContainer.user.idEntity & "-" & usersessionContainer.hotel.cod_Hotel
            If Me.sessionAccessList(id) Is Nothing Then
                Me.sessionAccessList.Add(id, usersessionContainer)
            Else
                Me.sessionAccessList(id) = usersessionContainer
            End If

        End Sub

        Public Function getEntityByID(entityID As Object) As sessionContainer
            Return sessionAccessList(entityID)
        End Function


    End Class

    ''' <summary>
    ''' Container with data in the session of the user asociated to hotel operations
    ''' </summary>
    ''' <remarks></remarks>
    Public Class sessionContainer
        ''' <summary>
        ''' link in querystring with user session. in format id_usuario-cod_Hotel
        ''' </summary>
        ''' <remarks></remarks>
        Public sessionlink As String
        Public user As cfr.mgr.UserHotelMgr
        Public hotel As cfr.obj.Hotel
        Public Sub New()

        End Sub
        Public Sub New(user As cfr.mgr.UserHotelMgr, hotel As cfr.obj.Hotel)
            Me.sessionlink = user.id_usuario & "-" & hotel.cod_Hotel
            Me.user = user
            Me.hotel = hotel
        End Sub
    End Class
End Namespace