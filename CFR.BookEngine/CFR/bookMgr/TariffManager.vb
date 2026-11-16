Option Explicit On
Imports Microsoft.VisualBasic
Imports System.Data
Imports cfr.tool

Namespace cfr
    ''' <summary>
    ''' tariff manager for selected room
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TariffManager
        ''' <summary>
        ''' room loaded for operations
        ''' </summary>
        ''' <remarks></remarks>
        Public bookRoom As BookedRoom

        Public Sub New(ByVal bookRoom As BookedRoom)
            Me.bookRoom = bookRoom
        End Sub
        ''' <summary>
        ''' get tariff for loaded room
        ''' </summary>
        ''' <param name="dr"></param>
        ''' <param name="roomInfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTarifaForUnitType(ByRef dr As BookRes, ByRef roomInfo As cfr.obj.HotelRoomData) As Double

            Dim roomTotalMaxPaxIncluded As Short

            'Personas incluidas en el precio, en principio son todas las personas
            'people included in price for room. At first are all adults, childs and maybe freechilds
            If roomInfo.NinoOcupaplaza = CFRConst.ChildrenUseBed.Yes Then 'ocupa plaza
                roomTotalMaxPaxIncluded = dr.numAdultos + dr.ninospago + dr.ninosfree
            ElseIf roomInfo.NinoOcupaplaza = CFRConst.ChildrenUseBed.No Then  'no ocupa plaza
                roomTotalMaxPaxIncluded = dr.numAdultos + dr.ninospago
            End If

            Me.bookRoom.PaxIncluidEnPrecio = roomTotalMaxPaxIncluded

            'that´s the real day for calculus
            Dim dateOutCom As Date
            dateOutCom = dr.dateOut.AddDays(-1)

            'create a day for each tariff price that will be filled whit price and other data
            For x As Integer = 0 To dr.bookTotalDays - 1
                Dim tar As New TariffByDay
                tar.id = dr.dateIn.AddDays(x)
                Me.bookRoom.tarifDailyList.Add(tar.id, tar)
            Next x

            '*******************************************************************************
            'Calc start
            'this are the available tariff codes for operator,promotion codes, etc
            Dim availableValidTariffAList As New ArrayList

            'get operator tariff codes to use
            'if operator if freesale then get only freesale tariffs
            Dim operatorTariffAList As ArrayList = TariffManager.getOperatorTariffsList(dr.bookOperator, dr.tariffForClientCode)
            For Each operatorCodeTariff As Integer In operatorTariffAList
                availableValidTariffAList.Add(operatorCodeTariff)
            Next

            'if there is a promotion code we load tariffs asociated with promotion code
            Dim bookOperatorPromotionTariffList As New ArrayList
            If Not dr.bookOperatorPromotion Is Nothing Then
                bookOperatorPromotionTariffList = TariffManager.getOperatorTariffsList(dr.bookOperatorPromotion, dr.tariffForClientCode)
                For Each promotionCodeTariff As Integer In bookOperatorPromotionTariffList
                    If availableValidTariffAList.Contains(promotionCodeTariff) = False Then
                        availableValidTariffAList.Add(promotionCodeTariff)
                    End If
                Next
            End If
            
            
            If availableValidTariffAList.Count = 0 Then
                Return basERR.E_TARIFA_INEXISTENTE_oper 'there are no tariff availables
            End If
            

            'Get all hotel tariff for this room for selected dates
            Dim TariffHotelList As System.Collections.Generic.Dictionary(Of Integer, TariffObj)
            TariffHotelList = Me.getHotelRoomTariffByRange(dr.hotel.id, roomInfo.idEntity, dr.dateIn, dateOutCom)
            If TariffHotelList.Count = 0 Then 'no existen tarifas o no existen tarifas a aplicar en ese periodo
                Return basERR.E_TarifasPeriodos_NotExist
            End If

            'Get yield management minimum price
            'Dim oRsYM As Data.DataView = Nothing
            'If dr.bookOperator.aplicarYMprecio = True Then
            '    oRsYM = YieldMngPrices.ListYieldMngPrices(dr.hotel.id, dr.dateIn, dateOutCom, roomInfo.idEntity).DefaultView
            'End If

            '******************************************************************************************************************
            'for each tariff we check the price and conditions, If the next tariff is better then we asign the tarif to the day.
            '******************************************************************************************************************
            For Each idTarifaOperator As Integer In availableValidTariffAList ' For each tariff asociated to operator or promotional code

                'selected tariff to work 
                Dim tariffSelected As TariffObj
                If TariffHotelList.ContainsKey(idTarifaOperator) Then
                    tariffSelected = TariffHotelList.Item(idTarifaOperator)
                Else
                    Continue For
                End If

                'Get the tariff range and fill with prices for each day. 
                'Then if a price tariff is 0(zero) for that they means there are not tariff/periods(temporada=season) configurated or available.

                'Get price (The query searches minimum days and release)
                Dim oRsPrice As Data.DataView = Nothing
                oRsPrice = YieldMngPrices.ListPrices(tariffSelected.id, roomInfo.idEntity, dr.dateIn, dateOutCom, dr.bookTotalDays).DefaultView

                'for each day check the better rate for that day and restrictions
                For Each tarByDay As TariffByDay In Me.bookRoom.tarifDailyList.Values

                    Dim dblPrecioTarifa As Double = 0 'this is the price calculated for each day from tariff. 
                    'The price will be changed due to restrictions like ymminimumprice or ymminimumdays

                    Dim priceDay As R_PriceDayEF = Me.getPriceByDay(tarByDay, oRsPrice)
                    dblPrecioTarifa = priceDay.price
                    If dblPrecioTarifa = 0 Then
                        Continue For
                    End If

                    'check Yield price minimum
                    'before enable again remember that the price here is form the tariff original
                    'dblPrecioTarifa = Me.CheckYMMinimumPrice(tarByDay, tariffSelected, roomInfo.idEntity, oRsYM)

                    'CONTROL YM DIAS MINIMOS Y SUPLEMENTOS DE LOS DIAS
                    'ValidTariffSelectedByMinimumDays es el flag para yield
                    'return true if valid meets minimum days
                    Dim ValidTariffSelectedByMinimumDays As Boolean = False
                    ValidTariffSelectedByMinimumDays = Me.CheckYMMinimumDays(priceDay, dblPrecioTarifa, tarByDay.daySelected, dr.bookTotalDays, tariffSelected)


                    'si la tarifa es aplicable y no habia ninguna aplicable
                    If Me.bookRoom.applyTariff = CFRConst.tariffValidEnum.NotValid And ValidTariffSelectedByMinimumDays = True Then
                        Me.bookRoom.applyTariff = CFRConst.tariffValidEnum.Valid
                        tarByDay.price = 0

                    ElseIf Me.bookRoom.applyTariff = CFRConst.tariffValidEnum.NotValid And ValidTariffSelectedByMinimumDays = False Then
                        'si la tarifa no es aplicable pero no hay ninguna aplicable presentamos la de menor precio
                        Me.bookRoom.applyTariff = CFRConst.tariffValidEnum.NotValid
                        ValidTariffSelectedByMinimumDays = True 'we show it
                    End If

                    'ValidTariffSelectedByMinimumDays si the flag for yield
                    'Cogemos el menor precio distinto de 0
                    If ValidTariffSelectedByMinimumDays = True And dblPrecioTarifa <> 0 Then
                        If tarByDay.price = 0 Then 'if the prices if 0 then is the first time we load so we update this selected tariff
                            Me.bookRoom.setTariffByDay(dblPrecioTarifa, tarByDay, tariffSelected)
                        End If

                        'Si la anterior no cumple el regimen pero la seleccionada si, guardamos la seleccionada
                        If ValidateRegimenForTariff(tarByDay.tariffBase, dr.regimen) = False And ValidateRegimenForTariff(tariffSelected, dr.regimen) = True Then
                            Me.bookRoom.setTariffByDay(dblPrecioTarifa, tarByDay, tariffSelected)
                            'if each tariff 
                        ElseIf ValidateRegimenForTariff(tarByDay.tariffBase, dr.regimen) = True And ValidateRegimenForTariff(tariffSelected, dr.regimen) = True Then
                            If (tarByDay.price > dblPrecioTarifa) Then
                                Me.bookRoom.setTariffByDay(dblPrecioTarifa, tarByDay, tariffSelected)
                            End If
                            'si ninguna cumple el regimen
                        ElseIf ValidateRegimenForTariff(tarByDay.tariffBase, dr.regimen) = False And ValidateRegimenForTariff(tariffSelected, dr.regimen) = False Then
                            If (tarByDay.tariffBase.Precio > dblPrecioTarifa) Then
                                Me.bookRoom.setTariffByDay(dblPrecioTarifa, tarByDay, tariffSelected)
                            End If
                        End If
                    End If 'AplicarTarifa = True And dblPrecioTarifa <> 0

                Next
            Next


            'CALC TOTAL PRICE
            For Each tar As TariffByDay In Me.bookRoom.tarifDailyList.Values
                Dim priceDaily As Double
                Dim priceWithoutRegimen As Double
                'this var check the real price according to the tariff 
                Dim bAdultosExtra1 As Integer
                Dim bAdultosExtra2 As Integer
                'COMPROBACIONES BASICAS
                priceDaily = tar.price
                priceWithoutRegimen = tar.price
                If priceDaily = 0 Then
                    bookRoom.priceFinalForAllDays = 0
                    Return basERR.E_TARIFA_INEXISTENTE_Hab
                End If
                
                'Nº de adultos por habitacion, si es mayor q el nº de adultos entonces coge de los niños
                'adultos extras
                If dr.numAdultos >= roomInfo.Adultos + roomInfo.AdultosExtra1 Then bAdultosExtra1 = 1
                If dr.numAdultos >= roomInfo.Adultos + roomInfo.AdultosExtra1 + roomInfo.AdultosExtra2 And roomInfo.AdultosExtra2 <> 0 Then bAdultosExtra2 = 1

                '----------------------------------------
                'CALCULO DEL REGIMEN
                Dim newRegimen As CFRConst.RegimenEnum = getNewRegimen(tar, dr)
                tar.selectedRegimen = newRegimen
                If newRegimen <> dr.regimen Then
                    Me.bookRoom.selectedRegimenChanged = True
                End If
                Me.bookRoom.validateBoard(tar)

                '----------------------------------------

                'If priceDaily = 0 And dr.regimen <> CFRConst.RegimenEnum.SA Then
                '    priceDaily = tar.price
                '    tar.tariffBase.Regimen = CFRConst.RegimenEnum.SA
                'End If

                'CALCULO DE ADULTOS EXTRAS
                'el descuento es sobre el precio dividido entre los adultos
                'si es cama extra entonces no se aplica el descuento.

                Dim priceByPax As Double = priceWithoutRegimen / roomInfo.Adultos 'rate for each person in basic unit price
                If roomInfo.AdultosExtra1 > 0 Then
                    If bAdultosExtra1 = 1 Then 'primer adulto extra
                        If tar.tariffBase.camaExtra1 = 0 Then 'SE APLICA DESCUENTO
                            priceDaily = priceDaily + ((priceByPax) * (1 - tar.tariffBase.DescAdultosExtra1 / 100))
                        Else 'SE APLICA PRECIO FIJO
                            priceDaily = priceDaily + tar.tariffBase.camaExtra1
                        End If
                        If roomInfo.AdultosExtra2 > 0 Then
                            If bAdultosExtra2 = 1 Then '2º adulto extra
                                If tar.tariffBase.camaExtra2 = 0 Then 'SE APLICA DESCUENTO
                                    priceDaily = priceDaily + ((priceByPax) * (1 - tar.tariffBase.DescAdultosExtra2 / 100))
                                Else 'SE APLICA PRECIO FIJO
                                    priceDaily = priceDaily + tar.tariffBase.camaExtra2
                                End If
                            End If
                        End If
                    End If
                End If
                
                'CALCULO DE LOS NIÑOS DE PAGO EN LA TARIFA
                '----------------------------------------
                priceDaily = priceDaily + GetTarCalculoNiñosPago(tar, dr, roomInfo, priceByPax)
                '----------------------------------------
                
                If Me.bookRoom.PaxIncluidEnPrecio < roomInfo.Adultos Then
                    Me.bookRoom.PaxIncluidEnPrecio = roomInfo.Adultos
                End If

                tar.tariffBase.PaxIncluidEnPrecio = Me.bookRoom.PaxIncluidEnPrecio


                priceDaily = Math.Round(priceDaily, 2)
                
                'set calculated price to all diferents rates for this day. At this moment all are the same. Then set new values depending on board for example
                tar.price = priceDaily
                tar.pricewithoutboard = priceDaily
                tar.priceNoRefund = priceDaily
                

                'Calc norefund tariff
                If tar.tariffBase.norefundtariff = True Then
                    If tar.tariffBase.norefundtariffdiscountapply2tariffonly = True Then
                        tar.priceNoRefund = tar.pricewithoutboard * (1 - (tar.tariffBase.norefundtariffdiscount / 100))
                    Else
                        tar.priceNoRefund = tar.price * (1 - (tar.tariffBase.norefundtariffdiscount / 100))
                    End If
                End If


                'apply promotion discount to price without board
                If Not dr.bookOperatorPromotion Is Nothing Then

                    If bookOperatorPromotionTariffList.Contains(tar.tariffBase.id) Then
                        If dr.bookOperatorPromotion.discount > 0 Then
                            priceDaily = priceDaily * (1 - (dr.bookOperatorPromotion.discount / 100))
                            priceDaily = Math.Round(priceDaily, 2)


                            'check if discount can apply to no refund tariff
                            If dr.bookOperatorPromotion.discountNotApplyWithNoRefundTariff = False Then
                                'apply to Norefund tariff too
                                tar.priceNoRefund = tar.priceNoRefund * (1 - (dr.bookOperatorPromotion.discount / 100))
                                tar.priceNoRefund = Math.Round(tar.priceNoRefund, 2)
                            End If

                        End If
                        
                    End If
                    
                End If
                '*******************************************


                'sum board to price. This must be the last calc due to discounts
                Dim RegimenPlus As Double = Me.GetTarCalculoPrecioRegimen(tar, dr, bAdultosExtra1, bAdultosExtra2)
                tar.RegimenPriceTotalForAllPax = RegimenPlus
                '*******************************************

                priceDaily = priceDaily + RegimenPlus
                tar.price = priceDaily
                tar.priceNoRefund = tar.priceNoRefund + RegimenPlus


                'impuestos
                If tar.tariffBase.impuestos > 0 Then
                    priceDaily = priceDaily * (1 + tar.tariffBase.impuestos / 100)
                    tar.price = priceDaily
                    tar.priceNoRefund = tar.priceNoRefund * (1 + tar.tariffBase.impuestos / 100)
                    tar.pricewithoutboard = tar.pricewithoutboard * (1 + tar.tariffBase.impuestos / 100)
                End If

                bookRoom.priceFinalForAllDays += priceDaily
                bookRoom.priceFinalForAllDaysNoRefund += tar.priceNoRefund


                Me.bookRoom.priceDesglosed += priceDaily & "|" 'Precio de cada dia incluido impuestos y demas
                Me.bookRoom.RegimenDesglosed += tar.selectedRegimen & "|" 'Regimen de cada dia
                Me.bookRoom.TariffDescriptionDesglosed += tar.tariffBase.id & "|" 'tarifa de cada dia
                
                'TPV
                If bookRoom.TPVOneNight = False Then
                    bookRoom.TPVOneNight = tar.tariffBase.TPVOneNight
                End If

                If bookRoom.TPVMinimum = 0 Or bookRoom.TPVMinimum < tar.tariffBase.TPVMinimum Then
                    bookRoom.TPVMinimum = tar.tariffBase.TPVMinimum
                End If



            Next

            'Redondeo 2 digitos
            Me.bookRoom.priceFinalForAllDays = System.Math.Round(Me.bookRoom.priceFinalForAllDays, 2)
            Me.bookRoom.priceFinalForAllDaysNoRefund = System.Math.Round(Me.bookRoom.priceFinalForAllDaysNoRefund, 2)
            

            'TPV CONTROL FOR BIGGEST PRICE
            If bookRoom.TPVOneNight = True Then 'if tariff charge at least first night we need to check which is bigger, first night or percent
                If dr.bookTotalDays = 1 Then 'if client is booking only one nigt then the biggest price always is the first night, that´s it the total price
                    bookRoom.TPVMinimum = bookRoom.priceFinalForAllDays
                ElseIf bookRoom.TPVMinimum = 100 Then 'nothing to do, 100% always is the biggest price,that´s it the total price
                    bookRoom.TPVMinimum = bookRoom.priceFinalForAllDays
                Else 'check biggest
                    Dim priceByPercent As Double
                    priceByPercent = bookRoom.priceFinalForAllDays * (bookRoom.TPVMinimum / 100)
                    'get price for first night
                    Dim pricesDesglosed() As String = bookRoom.GetPriceDesglosedMatrix
                    If pricesDesglosed(0) > priceByPercent Then
                        bookRoom.TPVMinimum = pricesDesglosed(0)
                    End If

                End If
            End If

            Return basERR.S_OK

        End Function
        ''' <summary>
        ''' get bookoperator tariff list codes
        ''' </summary>
        ''' <param name="bookOperator"></param>
        ''' <param name="tariffForClient"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getOperatorTariffsList(ByVal bookOperator As cfr.OperatorObj, ByVal tariffForClient As Integer) As ArrayList
            Dim sSQL As String = String.Empty
            sSQL = "Select rTo.cod_tarifa, rT.tarifa  "
            sSQL = sSQL & " FROM r_tarifas rT , R_tarifa_operador rTO   "
            sSQL = sSQL & " Where "
            sSQL = sSQL & " RTO.cod_tarifa = RT.cod_tarifa "

            If bookOperator.tipoOperador = CFRConst.ClientTypeEnum.HotelAgency Then
                If tariffForClient > CFRConst.bookTypeEnum.resIndefinido Then 'si es mayor que 0 es que trae el codigo del cliente
                    sSQL = sSQL & " AND RTO.cod_Operador= " & tariffForClient 'la tarifa del cliente(el codigo de operador del cliente empresa) elegido por la agencia
                Else
                    sSQL = sSQL & " AND RTO.cod_Operador= " & bookOperator.id
                End If

                'PARA AGENCIAS
                If tariffForClient = CFRConst.bookTypeEnum.resParticular Then 'solo las tarifas de particulares o no definidas o de agencia
                    sSQL = sSQL & " AND (RT.categoria IN ('" & CFRConst.ClientTypeEnum.NotClasified & "','" & CFRConst.ClientTypeEnum.PrivateClient & "','" & CFRConst.ClientTypeEnum.HotelAgency & "'))"
                ElseIf CStr(tariffForClient) = CFRConst.bookTypeEnum.resEmpresa Then  'SI ES de empresa coge todas, incluida las de particular por si son mas bajas
                    sSQL = sSQL & " AND (RT.categoria IN ('" & CFRConst.ClientTypeEnum.NotClasified & "','" & CFRConst.ClientTypeEnum.PrivateClient & "','" & CFRConst.ClientTypeEnum.HotelAgency & "','" & CFRConst.ClientTypeEnum.Company & "'))"
                End If


            Else 'any
                sSQL = sSQL & " AND RTO.cod_Operador= " & bookOperator.id
            End If

            Dim dt As DataTable = ind.bk.DBMgr.getDT(sSQL)
            Dim operatorTariffCodesAList As New ArrayList
            For Each dr As Data.DataRow In dt.Rows
                operatorTariffCodesAList.Add(dr(ind.bk.obj.R_tarifas.DBCol.cod_tarifa))
            Next

            Return operatorTariffCodesAList
        End Function
        ''' <summary>
        ''' Get tariff availables for selected unit in date range verifying price>0 and dates and release.
        ''' </summary>
        ''' <param name="cod_hotel"></param>
        ''' <param name="id_room"></param>
        ''' <param name="DateIn"></param>
        ''' <param name="dateOut"></param>
        ''' <returns></returns>
        Public Function getHotelRoomTariffByRange(ByVal cod_hotel As Integer _
        , ByVal id_room As Integer, ByVal DateIn As Date, ByVal dateOut As Date, Optional checkRelease As Boolean = True) As System.Collections.Generic.Dictionary(Of Integer, TariffObj)

            Dim sSql As String = String.Empty

            sSql = " Select RT.*, RTH.* From R_tarifas As rT INNER Join  R_Tarifa_habitacion As RTH On RTH.cod_tarifa = rT.cod_tarifa INNER Join   R_PriceDay On RTH.id = R_PriceDay.id_Tar_Hab "
            sSql = sSql & " Where  (RTH.cod_habitacion = {0}) "
            sSql = sSql & " And (R_PriceDay.dayprice >= Convert(DateTime, '{1}', 113)) and (R_PriceDay.dayprice <= Convert(DateTime, '{2}', 113)) " 'dates condition
            sSql = sSql & " AND (R_PriceDay.price > 0) "
            If checkRelease Then
                sSql = sSql & " AND (DATEADD(DAY,-R_PriceDay.release, R_PriceDay.dayprice))>= CAST(CONVERT(CHAR(11),GETDATE(),113) AS datetime) " 'release condition
            End If

            'sSql = sSql & " AND minimumdays<= {4} " 'minimum days condition

            sSql = String.Format(sSql, id_room, DateTools.getyyyymmddShort(DateIn), DateTools.getyyyymmddShort(dateOut))

            Dim dt As Data.DataTable = ind.bk.DBMgr.getDT(sSql)

            Dim tariffList As New System.Collections.Generic.Dictionary(Of Integer, TariffObj)

            For Each dr As DataRow In dt.Rows
                Dim t As TariffObj = Nothing
                tariffList.TryGetValue(dr("cod_tarifa"), t)
                If t Is Nothing Then 'we add to the collection
                    t = cfr.TariffObj.FillTariffObj(dr)
                    tariffList.Add(t.id, t)
                End If
            Next
            Return tariffList
        End Function

        Private Function getPriceByDay(ByRef selectedTar As TariffByDay, ByRef oRsYM As Data.DataView) As R_PriceDayEF

            Dim priceDay As New R_PriceDayEF

            oRsYM.RowFilter = "dayprice=#" & DateTools.getUSmmddyyyy(selectedTar.daySelected) & "# "

            If oRsYM.Count > 0 Then
                priceDay.price = CDbl(Tools.cNull(oRsYM(0)("price"), "0"))
                priceDay.minimumdays = CInt(Tools.cNull(oRsYM(0)("minimumdays"), "0"))
                priceDay.release = CInt(Tools.cNull(oRsYM(0)("release"), "0"))
            End If

            Return priceDay
        End Function

        ''' <summary>
        ''' check yield management minimum price
        ''' </summary>
        ''' <param name="selectedTar"></param>
        ''' <param name="tarifToSelect"></param>
        ''' <param name="iCod_habitacion"></param>
        ''' <param name="oRsYM"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CheckYMMinimumPrice(ByRef selectedTar As TariffByDay, ByRef tarifToSelect As TariffObj _
            , ByVal iCod_habitacion As Integer _
            , ByRef oRsYM As Data.DataView) As Double

            Dim ymPriceControled As Double = tarifToSelect.Precio
            Dim ymPrecio As Double

            If oRsYM Is Nothing Then
                Return ymPriceControled
            End If

            oRsYM.RowFilter = ind.bk.obj.R_YieldMng.DBCol.fechainicio & "<=#" & DateTools.getUSmmddyyyy(selectedTar.daySelected) & "# AND " & ind.bk.obj.R_YieldMng.DBCol.fechafin & ">=#" & DateTools.getUSmmddyyyy(selectedTar.daySelected) & "# AND " & ind.bk.obj.R_YieldMng.DBCol.cod_habitacion & "=" & iCod_habitacion

            If oRsYM.Count > 0 Then
                ymPrecio = CDbl(Tools.cNull(oRsYM(0)(ind.bk.obj.R_YieldMng.DBCol.precioMinimo), "0"))
                If ymPriceControled < ymPrecio Then 'si la tarifa es menor que el precio minimo
                    If oRsYM(0)(ind.bk.obj.R_YieldMng.DBCol.AplicarPrecMinim) = 1 Then 'si se aplica el precio minimo
                        ymPriceControled = ymPrecio
                    Else
                        ymPriceControled = 0 'price for that day, later in the final check if one day is 0 then the tariff is not available for book
                    End If
                End If
            End If
            Return ymPriceControled
        End Function
        ''' <summary>
        ''' Control minimum days
        ''' </summary>
        ''' <param name="dblPrecioTarifa"></param>
        ''' <param name="daySelected"></param>
        ''' <param name="bookTotalDays"></param>
        ''' <param name="TariffSelected"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CheckYMMinimumDays(priceDay As R_PriceDayEF, ByRef dblPrecioTarifa As Double, ByVal daySelected As Date _
            , ByVal bookTotalDays As Integer, ByRef TariffSelected As TariffObj) As Boolean

            Dim validTariff As Boolean = False

            'Comprobamos que cumple el numero minimo de dias
            If bookTotalDays >= priceDay.minimumdays Then 'Si cumple el nº de dias minimos entonces es aplicable la tarifa
                validTariff = True
                Return validTariff

            Else 'si no cumple vemos si es aplicable con suplementos

                'weekend
                If CInt(TariffSelected.SupFinDeSemana) > 0 And (DatePart("w", daySelected, vbMonday) = 5 Or DatePart("w", daySelected, vbMonday) = 6) Then
                    dblPrecioTarifa = dblPrecioTarifa + (1 + (TariffSelected.SupFinDeSemana / 100))
                    validTariff = True
                Else
                    'Calculamos el precio con suplemento.
                    If TariffSelected.SupDiasMinimos > 0 Then 'SUPLEMENTO DIAS MINIMOS
                        dblPrecioTarifa = dblPrecioTarifa + (1 + (TariffSelected.SupDiasMinimos / 100))
                        validTariff = True
                    End If

                End If

                Return validTariff

            End If

            Return validTariff

        End Function


        Private Function ValidateRegimenForTariff(ByRef tar As TariffObj, ByVal iReg_alojam As CFRConst.RegimenEnum) As Boolean

            Dim result As Boolean = False
            Select Case iReg_alojam
                Case CFRConst.RegimenEnum.SA
                    If tar.SA = True Then
                        result = True
                    End If

                Case CFRConst.RegimenEnum.AD
                    If tar.AD = True Then
                        result = True
                    End If

                Case CFRConst.RegimenEnum.MP
                    If tar.MP = True Then
                        result = True
                    End If

                Case CFRConst.RegimenEnum.PC
                    If tar.PC = True Then
                        result = True
                    End If

                Case CFRConst.RegimenEnum.TI
                    If tar.TI = True Then
                        result = True
                    End If

            End Select

            Return result
        End Function


        Private Function getNewRegimen(ByRef rsTARout As TariffByDay, ByVal dr As BookRes) As CFRConst.RegimenEnum

            'COMPROBAMOS EL REGIMEN QUE ADMITE LA TARIFA. COGEMOS EL INFERIOR SI LO TIENE, SI NO EL SUPERIOR.
            Dim iRegimen As CFRConst.RegimenEnum
            Dim splitRegimen As String = String.Empty
            If rsTARout.tariffBase.SA = True Then splitRegimen = "1"
            If rsTARout.tariffBase.AD = True Then splitRegimen = splitRegimen & "2"
            If rsTARout.tariffBase.MP = True Then splitRegimen = splitRegimen & "3"
            If rsTARout.tariffBase.PC = True Then splitRegimen = splitRegimen & "4"
            If rsTARout.tariffBase.TI = True Then splitRegimen = splitRegimen & "5"

            If InStr(1, splitRegimen, CInt(dr.regimen).ToString) > 0 Then
                iRegimen = dr.regimen
            Else  'RECORREMOS DEL 5 AL 1 HASTA ENCONTRAR EL QUE HAY MAS BAJO QUE EL ELEGIDO
                For Xreg As Integer = 5 To 1 Step -1
                    If InStr(1, splitRegimen, Xreg.ToString) > 0 Then
                        iRegimen = Xreg
                        If iRegimen < dr.regimen Then Exit For
                    End If
                Next Xreg
            End If

            Return iRegimen
        End Function

        Private Function GetTarCalculoPrecioRegimen(ByRef tarDaySelected As TariffByDay, ByVal dr As BookRes _
            , ByVal bAdultosExtra1 As Integer, ByVal bAdultosExtra2 As Integer) As Double

            Dim priceWithRegimen As Double

            Dim precRegAdulto As Double, precRegNino As Double
            Select Case tarDaySelected.selectedRegimen
                Case CFRConst.RegimenEnum.SA, 0
                    precRegAdulto = 0
                    precRegNino = 0
                    Return 0
                Case CFRConst.RegimenEnum.AD
                    precRegAdulto = tarDaySelected.tariffBase.PrecioAD
                    precRegNino = tarDaySelected.tariffBase.desayunonino
                Case CFRConst.RegimenEnum.MP
                    precRegAdulto = tarDaySelected.tariffBase.PrecioMP
                    precRegNino = tarDaySelected.tariffBase.mpnino
                Case CFRConst.RegimenEnum.PC
                    precRegAdulto = tarDaySelected.tariffBase.PrecioPC
                    precRegNino = tarDaySelected.tariffBase.pcnino
                Case CFRConst.RegimenEnum.TI
                    precRegAdulto = tarDaySelected.tariffBase.TodoIncluido
                    precRegNino = tarDaySelected.tariffBase.TodoIncluidoNino
            End Select

            'calculo del precio mas el regimen
            'ADULTOS
            If tarDaySelected.tariffBase.AplicDescAReg = 1 Then 'SE APLICAN DESCUENTOS A LOS REGIMENES
                If bAdultosExtra1 = 1 Then
                    priceWithRegimen = priceWithRegimen + (precRegAdulto * (1 - tarDaySelected.tariffBase.DescAdultosExtra1 / 100))
                End If
                If bAdultosExtra2 = 1 Then
                    priceWithRegimen = priceWithRegimen + (precRegAdulto * (1 - tarDaySelected.tariffBase.DescAdultosExtra2 / 100))
                End If
            Else 'TODOS PAGAN EL MISMO PRECIO PARA EL REGIMEN ELEGIDO
                priceWithRegimen = priceWithRegimen + (precRegAdulto * (bAdultosExtra1 + bAdultosExtra2))
            End If

            'AÑADIMOS EL REGIMEN DE LOS ADULTOS RESTANTES, LOS PRINCIPALES DE LA HABITACION
            priceWithRegimen = priceWithRegimen + (precRegAdulto * (dr.numAdultos - bAdultosExtra1 - bAdultosExtra2))


            'NIÑOS DE PAGO
            If dr.ninospago > 0 Then
                If tarDaySelected.tariffBase.AplicDescAReg = 1 Or tarDaySelected.tariffBase.AplicDescAReg = 2 Then 'SE APLICAN DESCUENTOS A LOS REGIMENES
                    If precRegNino > 0 Then 'SI SE HA METIDO UN PRECIO ESPECIAL PARA EL REGIMEN DEL NIÑO
                        If dr.ninospago = 1 Then 'UN SOLO NIÑO
                            priceWithRegimen = priceWithRegimen + ((precRegNino) * (1 - tarDaySelected.tariffBase.DescNinoPago1 / 100))
                        ElseIf dr.ninospago >= 2 Then 'DOS NIÑOS
                            priceWithRegimen = priceWithRegimen + ((precRegNino) * (1 - tarDaySelected.tariffBase.DescNinoPago1 / 100))
                            priceWithRegimen = priceWithRegimen + ((precRegNino) * (1 - tarDaySelected.tariffBase.DescNinoPago2 / 100))
                        End If
                    ElseIf precRegNino = 0 Then 'EL REGIMEN DEL NIÑO CUESTA IGUAL QUE EL DEL ADULTO
                        If dr.ninospago = 1 Then 'UN SOLO NIÑO
                            priceWithRegimen = priceWithRegimen + ((precRegAdulto) * (1 - tarDaySelected.tariffBase.DescNinoPago1 / 100))
                        ElseIf dr.ninospago >= 2 Then  'DOS NIÑOS
                            priceWithRegimen = priceWithRegimen + ((precRegAdulto) * (1 - tarDaySelected.tariffBase.DescNinoPago1 / 100))
                            priceWithRegimen = priceWithRegimen + ((precRegAdulto) * (1 - tarDaySelected.tariffBase.DescNinoPago2 / 100))
                        End If

                    End If

                Else  'NO SE APLICAN DESCUENTOS A LOS REGIMENES
                    If precRegNino > 0 Then     'SI SE HA METIDO UN PRECIO ESPECIAL PARA EL REGIMEN DEL NIÑO
                        priceWithRegimen = priceWithRegimen + (precRegNino * dr.ninospago)
                    ElseIf precRegNino = 0 Then 'EL REGIMEN DEL NIÑO CUESTA IGUAL QUE EL DEL ADULTO
                        priceWithRegimen = priceWithRegimen + (precRegAdulto * dr.ninospago)
                    End If
                End If
            End If


            Return priceWithRegimen
        End Function


        Private Function GetTarCalculoNiñosPago(ByRef tar As TariffByDay _
            , ByVal dr As BookRes, ByVal roomInfo As cfr.obj.HotelRoomData, ByVal priceByPax As Double) As Double


            'NIÑOS DE PAGO
            If dr.ninospago = 0 Then 'no child exists
                Return 0
            End If

            Dim priceChildPlus As Double

            Dim nNinosComoadultos As Integer 'numero de niños que entran en el precio base si no hay mas adultos.
            'Si hay niños que pueden ocupar el espacio de adultos entonces no se cobra el niño ya que esta incluido en el precio de la habitacion
            'pero hay que comprobar las condiciones de tarifa
            nNinosComoadultos = roomInfo.adultos - dr.numAdultos

            ' SOLO TENER EN CUENTA SI SE LE APLICA DESCUENTO O NO SEGUN LAS OPCIONES DE LA TARIFA.
            If nNinosComoadultos >= 2 Then '2 niños que sustituyen a los adultos
                'no se cobra nada por ellos
                tar.paxIncludedInPrice = nNinosComoadultos
                'COMPROBAMOS SI AUN ASI HAY QUE APLICAR EL DESCUENTO DE NIÑOS
                'SOLO DESCUENTO, NO PRECIO FIJO
                'Descuento del niño 1 y descuento del niño2 ???

                If tar.tariffBase.AplicDescACapac = True Then 'no se cobra pero se le aplica el descuento del niño al precio de adulto
                    priceChildPlus += -((priceByPax) * (1 - tar.tariffBase.DescNinoPago1 / 100))
                    priceChildPlus += -((priceByPax) * (1 - tar.tariffBase.DescNinoPago2 / 100))
                End If


            ElseIf nNinosComoadultos = 1 And dr.ninospago = 1 Then 'SOLO 1 NIÑO Y OCUPA EL DE UN ADULTO, NO SE LE COBRA.
                tar.paxIncludedInPrice = dr.numAdultos + nNinosComoadultos

                'COMPROBAMOS SI AUN ASI HAY QUE APLICAR EL DESCUENTO AL QUE VA COMO ADULTO
                'SOLO DESCUENTO, NO PRECIO FIJO
                If tar.tariffBase.AplicDescACapac = True Then 'no se cobra pero se le aplica el descuento del niño al precio de adulto
                    priceChildPlus += -((priceByPax) * (1 - tar.tariffBase.DescNinoPago1 / 100))
                End If

            ElseIf nNinosComoadultos = 1 And dr.ninospago = 2 Then 'un solo niño ocupa al de adulto, se le aplica descuento al otro
                'COMPROBAMOS SI AUN ASI HAY QUE APLICAR EL DESCUENTO AL QUE VA COMO ADULTO
                'SOLO DESCUENTO, NO PRECIO FIJO
                If tar.tariffBase.AplicDescACapac = True Then 'no se cobra pero se le aplica el descuento del niño al precio de adulto
                    priceChildPlus += -((priceByPax) * (1 - tar.tariffBase.DescNinoPago1 / 100))
                End If

                If tar.tariffBase.camaExtra1Nino = 0 Then 'SE LE APLICA DESCUENTO EN %
                    priceChildPlus += +((priceByPax) * (1 - tar.tariffBase.DescNinoPago1 / 100))
                Else  'SE LE APLICA PRECIO FIJO
                    priceChildPlus += +tar.tariffBase.camaExtra1Nino
                End If
                tar.paxIncludedInPrice = dr.numAdultos + nNinosComoadultos + 1

            ElseIf nNinosComoadultos <= 0 And dr.ninospago > 0 Then 'Ningun niño ocupa el puesto de un adulto, se les cobra los descuentos si lo tiene
                If dr.ninospago = 2 Then '2 NIÑOS
                    'Por separado puede meter un % en un niño y un fijo en el otro.
                    If tar.tariffBase.camaExtra1Nino = 0 Then 'SE LE APLICA DESCUENTO EN %
                        priceChildPlus += +((priceByPax) * (1 - tar.tariffBase.DescNinoPago1 / 100))
                    Else  'SE LE APLICA PRECIO FIJO
                        priceChildPlus += +tar.tariffBase.camaExtra1Nino
                    End If
                    If tar.tariffBase.camaExtra2Nino = 0 Then '2 cama extra SE LE APLICA DESCUENTO EN %
                        priceChildPlus += +((priceByPax) * (1 - tar.tariffBase.DescNinoPago2 / 100))
                    Else 'SE LE APLICA PRECIO FIJO
                        priceChildPlus += +tar.tariffBase.camaExtra2Nino
                    End If
                ElseIf dr.ninospago = 1 Then '1 NIÑO
                    If tar.tariffBase.camaExtra1Nino = 0 Then 'SE LE APLICA DESCUENTO EN %
                        priceChildPlus += +((priceByPax) * (1 - tar.tariffBase.DescNinoPago1 / 100))
                    Else  'SE LE APLICA PRECIO FIJO
                        priceChildPlus += +tar.tariffBase.camaExtra1Nino
                    End If

                End If
                tar.paxIncludedInPrice = dr.numAdultos + dr.ninospago
            End If


            tar.paxIncludedInPrice += dr.ninosfree


            Return priceChildPlus
        End Function


    End Class
End Namespace