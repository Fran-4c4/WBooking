Imports Microsoft.VisualBasic


Namespace cfr.obj.obsolete

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

        Public Function getEntityByID(entityID)
            Return sessionAccessList(entityID)
        End Function


        ''' <summary>
        ''' Container with data in the session of the user asociated to hotel operations
        ''' </summary>
        ''' <remarks></remarks>
        Public Class sessionContainer
            Public user As cfr.mgr.UserHotelMgr
            Public hotel As cfr.obj.Hotel
            Public Sub New()

            End Sub
            Public Sub New(user As cfr.mgr.UserHotelMgr, hotel As cfr.obj.Hotel)
                Me.user = user
                Me.hotel = hotel
            End Sub
        End Class


        'Private Function validateUserAccess(req As System.Web.HttpRequest) As Boolean
        '    Dim hotelIdObj As Object
        '    'Check in order
        '    'viewstate
        '    hotelID = HotelIDVS
        '    If hotelID <> 0 Then
        '        Return True
        '    End If
        '    'POST
        '    hotelIdObj = Request(cfr.REQCte.query.cod_hotel)
        '    If TypeOf hotelIdObj Is Integer Then
        '        hotelID = DirectCast(hotelIdObj, Integer)
        '    Else
        '        hotelID = 0
        '    End If

        '    If hotelID <> 0 Then
        '        Return True
        '    End If
        '    'querystring
        '    hotelIdObj = CInt(Request.QueryString(cfr.REQCte.query.cod_hotel))
        '    If TypeOf hotelIdObj Is Integer Then
        '        hotelID = DirectCast(hotelIdObj, Integer)
        '    Else
        '        hotelID = 0
        '    End If

        '    If hotelID <> 0 Then
        '        Return True
        '    End If

        '    If hotelID = 0 Then
        '        CloseSession()
        '    End If

        '    Return False
        'End Function

    End Class
End Namespace