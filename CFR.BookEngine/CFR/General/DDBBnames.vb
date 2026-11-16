

Namespace cfr.db.sp
    ''' <summary>
    ''' class with names of stored procedures in DDBB
    ''' Not all SP are here
    ''' </summary>
    ''' <remarks></remarks>
    Public Class sproc


        Public Class Customer
            Public Const CustomerGet As String = "SP_CustomerGet"
            Public Const CustomerGetByCodHotelAsociated As String = "SP_CustomerGetByCodHotelAsociated"
        End Class

        Public Class Book
            Public Const Book_GetPendingToSendEmail As String = "sp_GetPendingBooksToSendEmail"
            Public Const Book_GetByID As String = "SP_GetBookByID"
            Public Const Book_Update_Mail_Send As String = "sp_Book_Update_Mail_Send"
            Public Const SP_SALONES_ListByHotelID As String = "SP_SALONES_ListByHotelID"
            Public Const SP_Basket_SaveTemporal As String = "SP_Basket_SaveTemporal"
            Public Const SP_Basket_UpdateMerchantSignature As String = "SP_Basket_UpdateMerchantSignature"
            Public Const SP_Basket_UpdateBasketBankResponse As String = "SP_Basket_UpdateBasketBankResponse"
            Public Const SP_Basket_UpdateBasketCodReserva As String = "SP_Basket_UpdateBasketCodReserva"
            Public Const SP_Basket_GetByID As String = "SP_Basket_GetByID"
            Public Const Hotel_GetAvailability As String = "sp_Hotel_GetAvailability"
            Public Const Hotel_GetAvailabilityMap As String = "sp_Hotel_GetAvailabilityMap"
        End Class

        Public Class Generic
            'tariff
            Public Const R_Tarifa_operador_GetByCodTarifa As String = "R_Tarifa_operador_GetByCodTarifa"
            Public Const R_Tarifa_operador_GetByCodOperador As String = "R_Tarifa_operador_GetByCodOperador"
            Public Const R_Operadores_GetByIDHotel As String = "R_Operadores_GetByIDHotel"
            Public Const R_OperadorTariffAsociatedUI As String = "R_OperadorTariffAsociatedUI"
            Public Const R_TariffOperatorAsociatedUI As String = "R_TariffOperatorAsociatedUI"
            Public Const R_TariffTemporadasAsociatedUI As String = "R_TariffTemporadasAsociatedUI"


        End Class
    End Class
End Namespace


