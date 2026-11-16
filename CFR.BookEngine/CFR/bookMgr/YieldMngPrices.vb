Imports Microsoft.VisualBasic

Namespace cfr


    Public Class YieldMngPrices
        Public Shared Function ListYieldMngPrices(ByVal cod_Hotel As Integer _
            , ByVal dateIn As Date _
            , ByVal dateOut As Date _
            , Optional ByVal id_room As Integer = CFRConst.YesNo.No
            ) As Data.DataTable


            Dim sSQL As String = String.Empty
            Dim endDate As Date = dateOut.AddDays(-1) 'the last day is the out day.

            sSQL = "SELECT  *,convert(varchar(10),fechainicio,103)as fecha1 ,convert(varchar(10),fechafin,103)as fecha2"
            sSQL = sSQL & " FROM R_YieldMng INNER JOIN T_HOTELES_HABITACIONES"
            sSQL = sSQL & " ON T_HOTELES_HABITACIONES.codhabitacionhotel=R_YieldMng.cod_habitacion "
            sSQL = sSQL & " Where R_YieldMng.cod_HOTEL = " & cod_Hotel
            If id_room <> CFRConst.YesNo.No Then
                sSQL = sSQL & " AND COD_HABITACION = " & id_room
            End If

            'sSQL = sSQL & " AND cod_CRS = " & cod_CRS
            sSQL = sSQL & " AND (FECHAINICIO<=CONVERT(DATETIME,'" & cfr.tool.DateTools.getyyyymmddShort(dateOut) & "',113) "
            sSQL = sSQL & " AND fechafin>=CONVERT(DATETIME,'" & cfr.tool.DateTools.getyyyymmddShort(dateIn) & "',113))"

            sSQL = sSQL & " ORDER BY fechainicio,cod_habitacion "

            Return ind.bk.DBMgr.getDT(sSQL)

        End Function
        ''' <summary>
        ''' Get dataview with prices for each day for selected tariff, unit ,dates interval and total days
        ''' </summary>
        ''' <param name="cod_tarifa"></param>
        ''' <param name="id_room"></param>
        ''' <param name="dateIn"></param>
        ''' <param name="dateOut"></param>
        ''' <param name="totaldays"></param>
        ''' <returns></returns>
        Public Shared Function ListPrices(ByVal cod_tarifa As Integer, ByVal id_room As Integer _
            , ByVal dateIn As Date _
            , ByVal dateOut As Date _
            , ByVal totaldays As Integer
            ) As Data.DataTable


            Dim sSQL As String = String.Empty
            Dim endDate As Date = dateOut.AddDays(-1) 'the last day is the out day.

            sSQL = "SELECT R_PriceDay.id, R_PriceDay.id_Tar_Hab, R_PriceDay.dayprice, R_PriceDay.price, R_PriceDay.ModifyDate,R_PriceDay.minimumdays,R_PriceDay.RELEASE , R_tarifas.cod_hotel " _
                            & " , R_tarifas.cod_tarifa, R_Tarifa_habitacion.cod_habitacion " _
                            & " FROM R_tarifas INNER JOIN R_Tarifa_habitacion ON R_tarifas.cod_tarifa = R_Tarifa_habitacion.cod_tarifa " _
                            & " INNER JOIN  R_PriceDay ON R_Tarifa_habitacion.id = R_PriceDay.id_Tar_Hab " _
                            & " WHERE    (R_PriceDay.dayprice >= CONVERT(DATETIME, '{0}', 113)) AND (R_PriceDay.dayprice <= CONVERT(DATETIME, '{1}', 113)) " _
                            & " And (R_tarifas.cod_tarifa = {2}) And (R_Tarifa_habitacion.cod_habitacion = {3}) " _
                            & " AND minimumdays<= {4} " _
                            & " AND (DATEADD(DAY,-R_PriceDay.release, R_PriceDay.dayprice))>= CAST(CONVERT(CHAR(11),GETDATE(),113) AS datetime)" _
                            & " ORDER BY R_PriceDay.dayprice "

            sSQL = String.Format(sSQL, cfr.tool.DateTools.getyyyymmddShort(dateIn), cfr.tool.DateTools.getyyyymmddShort(dateOut), cod_tarifa, id_room, totaldays)


            Return ind.bk.DBMgr.getDT(sSQL)

        End Function

    End Class
End Namespace