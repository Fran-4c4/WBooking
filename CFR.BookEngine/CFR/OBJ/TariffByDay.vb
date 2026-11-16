Imports Microsoft.VisualBasic

Namespace cfr
    ''' <summary>
    ''' tariff data by day with the price for the day to book
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class TariffByDay
        Implements IIDObject

        ''' <summary>
        ''' Selected day
        ''' </summary>
        ''' <remarks></remarks>
        Public daySelected As Date
        ''' <summary>
        ''' Aplicated tariff for the day
        ''' </summary>
        ''' <remarks></remarks>
        Public tariffBase As New TariffObj
        ''' <summary>
        ''' selected regimen for day?
        ''' </summary>
        ''' <remarks></remarks>
        Public selectedRegimen As CFRConst.RegimenEnum
        ''' <summary>
        ''' The price calculated for the day. diferent from the base tariff due to restrictions
        ''' </summary>
        ''' <remarks></remarks>
        Public price As Double
        ''' <summary>
        ''' The price calculated for the day without board
        ''' </summary>
        ''' <remarks></remarks>
        Public pricewithoutboard As Double
        ''' <summary>
        ''' The no refundable price calculated for the day. diferent from the base tariff due to restrictions
        ''' </summary>
        ''' <remarks></remarks>
        Public priceNoRefund As Double


        ''' <summary>
        ''' board price for this day for all pax included ()basic price is in tariffbase
        ''' </summary>
        ''' <remarks></remarks>
        Public RegimenPriceTotalForAllPax As Double

        Public paxIncludedInPrice As Double

        Public Property id() As Object Implements IIDObject.idEntity
            Get
                Return Me.daySelected
            End Get
            Set(ByVal value As Object)
                Me.daySelected = value
            End Set
        End Property
        ''' <summary>
        ''' set the new value for the day. Price 
        ''' </summary>
        ''' <param name="newPrice"></param>
        ''' <param name="newTariffBase"></param>
        ''' <remarks></remarks>
        Public Sub setNewValue(ByVal newPrice As Double, ByVal newTariffBase As TariffObj)
            Me.price = newPrice
            Me.tariffBase = newTariffBase
        End Sub
    End Class
End Namespace