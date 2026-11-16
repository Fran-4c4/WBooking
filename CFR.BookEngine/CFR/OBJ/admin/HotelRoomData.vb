Imports Microsoft.VisualBasic

Namespace cfr.obj

    Public Class RoomDataCollection
        Inherits Hashtable

        Public Sub addItemObject(itemObject As cfr.obj.HotelRoomData)
            If Me.ContainsKey(itemObject.idEntity) = False Then
                Me.Add(itemObject.idEntity, itemObject)
                Debug.WriteLine("cfr.obj.HotelRoomData added to cache: " & itemObject.idEntity)
            Else
                Me.Item(itemObject.idEntity) = itemObject
                Debug.WriteLine("cfr.obj.HotelRoomData updated to cache: " & itemObject.idEntity)
            End If
        End Sub

        Public Function getHolder(itemObjectID As Integer) As cfr.obj.HotelRoomData
            Dim hld As cfr.obj.HotelRoomData = Me.Item(itemObjectID)
            Return hld
        End Function

        Public Function getHolder(itemObject As cfr.obj.HotelRoomData) As cfr.obj.HotelRoomData
            Dim hld As cfr.obj.HotelRoomData = Me.Item(itemObject.idEntity)
            Return hld
        End Function
    End Class
    

    <Serializable()> _
    Public Class HotelRoomData
        Implements IIDObject

        Public Shared roomCollection As New RoomDataCollection
        Public Shared Event removeUpdatedUnitEvent(ByVal unitID As Integer)
        Public Shared Sub removeUpdatedUnit(ByVal unitID As Integer)
            roomCollection.Remove(unitID)
            RaiseEvent removeUpdatedUnitEvent(unitID)
        End Sub

        Public Property idEntity() As Object Implements IIDObject.idEntity
            Get
                Return Me.codhabitacionhotel
            End Get
            Set(ByVal value As Object)
                Me.codhabitacionhotel = value
            End Set
        End Property

        Public cod_Hotel As Integer
        Public codhabitacionhotel As Integer

        Public codhabitacionGenerica As Integer

        Public Adultos As Short
        Public AdultosExtra1 As Short
        Public AdultosExtra2 As Short
        Public MaxPersonas As Short
        Public NumNino As Short
        ''' <summary>
        ''' max number of adults in a room
        ''' </summary>
        Public MaxAdultos As Short
        Public cod_hab_combinada As Integer
        Public NinoOcupaplaza As Short

        Public description As New DescriptionByLanguage
        Public serviceInfo As New DescriptionByLanguage

    End Class
End Namespace

