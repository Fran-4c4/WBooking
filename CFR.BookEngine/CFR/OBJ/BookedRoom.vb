Imports Microsoft.VisualBasic

Namespace cfr

    <Serializable()> _
    Public Class BookedRoom
        Implements IIDObject
        Implements IComparable


        Public idVar As String
        Public roomSelected As cfr.obj.HotelRoomData
        ''' <summary>
        ''' day collection with the price for each day and other related data
        ''' </summary>
        ''' <remarks></remarks>
        Public tarifDailyList As New System.Collections.Generic.Dictionary(Of Date, TariffByDay)
        Public priceFinalForAllDays As Double
        Public priceFinalForAllDaysNoRefund As Double
        ''' <summary>
        ''' price desglosed in format price1|price2|
        ''' </summary>
        Public priceDesglosed As String
        Public RegimenDesglosed As String
        ''' <summary>
        ''' If there are different tariff each day then value is true
        ''' </summary>
        ''' <remarks></remarks>
        Public boardChangeForDays = False
        ''' <summary>
        ''' If the tariff is diferent from the selected from user.
        ''' </summary>
        ''' <remarks></remarks>
        Public selectedRegimenChanged As Boolean = False
        ''' <summary>
        ''' when there are only one board this has the value
        ''' </summary>
        ''' <remarks></remarks>
        Public boardTariff As CFRConst.RegimenEnum = CFRConst.RegimenEnum.notselected

        Public TariffDescriptionDesglosed As String
        Public PaxIncluidEnPrecio As Integer

        ''' <summary>
        ''' Number of rooms choosed by user
        ''' </summary>
        ''' <remarks></remarks>
        Public bookCount As Integer = 0
        Public availability As Integer = 0

        'tpv data
        ''' <summary>
        ''' value to charge in 
        ''' </summary>
        ''' <remarks></remarks>
        Public TPVMinimum As Double
        ''' <summary>
        ''' indicates if the price to be charge to client, if true it must be the ma
        ''' </summary>
        ''' <remarks></remarks>
        Public TPVOneNight As Boolean

        ''' <summary>
        ''' Book state, active, canceled, noshow,etc
        ''' </summary>
        ''' <remarks></remarks>
        Public bookState As cfr.CFRConst.bookStateEnum = CFRConst.bookStateEnum.Active

        ''' <summary>
        ''' Indicates the original availability to put again if canceled (due to combinated rooms)
        ''' </summary>
        ''' <remarks></remarks>
        Public originalAvailability As String

        ''' <summary>
        ''' Cancel date
        ''' </summary>
        ''' <remarks></remarks>
        Public bookCancelDate As Date
        Public bookCancelBy As String

        Dim applyTariffvar As CFRConst.tariffValidEnum = CFRConst.tariffValidEnum.NotValid

        Public Function getPriceNoRefundDesglosedString() As String
            Dim pricedesglosedStr As String = ""
            For Each tar As TariffByDay In Me.tarifDailyList.Values
                pricedesglosedStr += tar.priceNoRefund & "|"
            Next
            Return pricedesglosedStr
        End Function

        Public Property applyTariff As CFRConst.tariffValidEnum
            Get
                Return Me.applyTariffvar
            End Get
            Set(value As CFRConst.tariffValidEnum)
                Me.applyTariffvar = value
            End Set
        End Property

        Public minimumDays As Integer = 0

        Public Sub New()

        End Sub

        Public Sub New(ByVal id As String)
            Me.idVar = id
        End Sub
        Public Property id() As Object Implements IIDObject.idEntity
            Get
                Return Me.idVar
            End Get
            Set(ByVal value As Object)
                Me.idVar = value
            End Set
        End Property

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim objToCompare As BookedRoom = DirectCast(obj, BookedRoom)
            Return Me.priceFinalForAllDays.CompareTo(objToCompare.priceFinalForAllDays)
        End Function

        Public Function GetPriceDesglosedMatrix() As String()
            'If Me.priceDesglosed = "" Then
            '    Return
            'End If
            Dim pricesMatrix() As String
            pricesMatrix = Split(Me.priceDesglosed, "|")
            Return pricesMatrix
        End Function

        Public Sub setTariffByDay(ByVal newPrice As Double, tariffByDaySelected As TariffByDay, ByVal newTariffBase As TariffObj)
            tariffByDaySelected.setNewValue(newPrice, newTariffBase)
            If Me.minimumDays < newTariffBase.minimumDays Then
                Me.minimumDays = newTariffBase.minimumDays
            End If
        End Sub

        Public Sub validateBoard(tariffDay As TariffByDay)
            
            If Me.boardTariff = CFRConst.RegimenEnum.notselected Then
                Me.boardTariff = tariffDay.selectedRegimen
            ElseIf Me.boardTariff <> tariffDay.selectedRegimen Then
                Me.boardChangeForDays = True
            End If

        End Sub


    End Class
End Namespace