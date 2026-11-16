
Namespace ind.bk.obj
    
    Partial Public Class R_tarifas_Manager


        Public Function getDtTariffByIDHotel(cod_Hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_tarifas_GetByIDHotel")
            addSqlParameterSimple(cmd, ind.bk.obj.T_HOTELES.DBCol.cod_Hotel, cod_Hotel)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function getDT_temporadasByIdTarifa(cod_tarifa As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("r_Tarifas_Temporadas_GetByIDTarifa")
            addSqlParameterSimple(cmd, ind.bk.obj.R_tarifas.DBCol.cod_tarifa, cod_tarifa)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function getDT_PricesByRoomByTariff(cod_tarifa As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_tarifas_getPricesForRooms")
            addSqlParameterSimple(cmd, ind.bk.obj.R_tarifas.DBCol.cod_tarifa, cod_tarifa)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function getDT_ClientsByTariff(cod_tarifa As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand(cfr.db.sp.sproc.Generic.R_Tarifa_operador_GetByCodTarifa)
            addSqlParameterSimple(cmd, ind.bk.obj.R_tarifas.DBCol.cod_tarifa, cod_tarifa)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function getDT_TariffByClient(cod_operador As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand(cfr.db.sp.sproc.Generic.R_Tarifa_operador_GetByCodOperador)
            addSqlParameterSimple(cmd, ind.bk.obj.R_operadores.DBCol.cod_operador, cod_operador)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function getDT_TariffAsociatedClientForUI(cod_tarifa As Integer, cod_hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand(cfr.db.sp.sproc.Generic.R_TariffOperatorAsociatedUI)
            addSqlParameterSimple(cmd, ind.bk.obj.R_tarifas.DBCol.cod_tarifa, cod_tarifa)
            addSqlParameterSimple(cmd, ind.bk.obj.T_HOTELES.DBCol.cod_Hotel, cod_hotel)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function getDT_TariffByClientForUI(cod_operador As Integer, cod_hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand(cfr.db.sp.sproc.Generic.R_OperadorTariffAsociatedUI)
            addSqlParameterSimple(cmd, ind.bk.obj.R_operadores.DBCol.cod_operador, cod_operador)
            addSqlParameterSimple(cmd, ind.bk.obj.T_HOTELES.DBCol.cod_Hotel, cod_hotel)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function getDT_TariffTemporadasAsociatedUI(cod_tarifa As Integer, cod_hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand(cfr.db.sp.sproc.Generic.R_TariffTemporadasAsociatedUI)
            addSqlParameterSimple(cmd, ind.bk.obj.R_tarifas.DBCol.cod_tarifa, cod_tarifa)
            addSqlParameterSimple(cmd, ind.bk.obj.T_HOTELES.DBCol.cod_Hotel, cod_hotel)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        ''' <summary>
        ''' delete all tariff asociated to operator
        ''' </summary>
        ''' <param name="cod_operador"></param>
        ''' <param name="trans"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function deleteOperator_AllTariffsRelated(cod_operador As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Tarifa_operador_DeleteByCod_operador")
            addSqlParameterSimple(cmd, ind.bk.obj.R_operadores.DBCol.cod_operador, cod_operador)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        ''' <summary>
        ''' delete all the operators asociated to selected tariff
        ''' </summary>
        ''' <param name="cod_tarifa"></param>
        ''' <param name="trans"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function deleteTariff_AllOperatorsRelated(cod_tarifa As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Tarifa_operador_DeleteByCod_cod_tarifa")
            addSqlParameterSimple(cmd, ind.bk.obj.R_tarifas.DBCol.cod_tarifa, cod_tarifa)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        ''' <summary>
        ''' remove all seasons asociated with tariff
        ''' </summary>
        ''' <param name="cod_tarifa"></param>
        ''' <param name="trans"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function deleteTariff_AllTemporadasRelated(cod_tarifa As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Tarifa_temporadas_DeleteByCod_cod_tarifa")
            addSqlParameterSimple(cmd, ind.bk.obj.R_tarifas.DBCol.cod_tarifa, cod_tarifa)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        ''' <summary>
        ''' remove all seasons asociated with hotel
        ''' </summary>
        ''' <param name="cod_hotel"></param>
        ''' <param name="trans"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function deleteTariff_AllTemporadasByCod_hotel(cod_hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Integer

            Dim ssql As String = "DELETE FROM r_Tarifas_Temporadas FROM         r_Tarifas_Temporadas INNER JOIN " _
                      & " R_tarifas ON r_Tarifas_Temporadas.cod_tarifa = R_tarifas.cod_tarifa WHERE     (R_tarifas.cod_hotel = {0})"
            ssql = String.Format(ssql, cod_hotel)

            Return Me.executeDirectSql(trans, ssql)

        End Function

        ''' <summary>
        ''' remove all seasons asociated with hotel
        ''' </summary>
        ''' <param name="cod_hotel"></param>
        ''' <param name="trans"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function deleteTariff_AllOperatorByCod_hotel(cod_hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Integer

            Dim ssql As String = "DELETE FROM R_Tarifa_operador FROM R_Tarifa_operador INNER JOIN " _
                      & " R_tarifas ON R_Tarifa_operador.cod_tarifa = R_tarifas.cod_tarifa  WHERE (R_tarifas.cod_hotel = {0})"
            ssql = String.Format(ssql, cod_hotel)

            Return Me.executeDirectSql(trans, ssql)

        End Function


        ''' <summary>
        ''' remove all seasons asociated with hotel
        ''' </summary>
        ''' <param name="cod_hotel"></param>
        ''' <param name="trans"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getTariff_Unit_ByCod_hotel(cod_hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object

            'Dim ssql As String = "SELECT R_Tarifa_habitacion.id, R_tarifas.cod_tarifa, R_tarifas.cod_hotel, R_tarifas.tarifa, R_Tarifa_habitacion.cod_habitacion " _
            '& " ,R_Tarifa_habitacion.precio, R_tarifas.DiasMinimos " _
            '& " FROM         R_tarifas INNER JOIN  R_Tarifa_habitacion ON R_tarifas.cod_tarifa = R_Tarifa_habitacion.cod_tarifa " _
            '& " WHERE     (R_tarifas.cod_hotel = {0}) " _
            '& " ORDER BY R_Tarifa_habitacion.cod_habitacion, R_tarifas.tarifa  "
            'ssql = String.Format(ssql, cod_hotel)

            'Dim mlist As Object
            'Using ctx As New cfrbookEntities
            '    mlist = ctx.Database.SqlQuery(Of Object)(ssql).ToList
            'End Using


            'Return mlist

            Dim queryDef
            Using ctx As New cfrbookEntities
                queryDef = (From ord In ctx.R_tarifasEF
                            Join det In ctx.R_Tarifa_habitacionEF
                           On ord.cod_tarifa Equals det.cod_tarifa
                            Where ord.cod_hotel = cod_hotel
                            Order By det.cod_habitacion
                            Select New With
                            {
                                .id = det.id,
                                .tarifa = ord.tarifa
                            }).ToList


            End Using

            Return queryDef


        End Function


    End Class
End Namespace