Imports System.Linq
Imports System.Web

''' <summary>
''' Enforces that identifiers accepted by hotel-administration pages belong to
''' the hotel in the authenticated administrator session.
''' </summary>
Public NotInheritable Class AdminTenantSecurity
    Private Sub New()
    End Sub

    Public Shared Sub RequireOperator(ByVal hotelId As Integer, ByVal operatorId As Integer)
        Using ctx As New cfrbookEntities
            Dim exists As Boolean =
                (From candidate In ctx.R_operadorEF
                 Where candidate.cod_operador = operatorId AndAlso candidate.cod_hotel = hotelId
                 Select candidate.cod_operador).Any()
            RequireOwned(exists, "Customer or promotion not found.")
        End Using
    End Sub

    Public Shared Sub RequireTariff(ByVal hotelId As Integer, ByVal tariffId As Integer)
        Using ctx As New cfrbookEntities
            Dim exists As Boolean =
                (From candidate In ctx.R_tarifasEF
                 Where candidate.cod_tarifa = tariffId AndAlso candidate.cod_hotel = hotelId
                 Select candidate.cod_tarifa).Any()
            RequireOwned(exists, "Tariff not found.")
        End Using
    End Sub

    Public Shared Sub RequireSeason(ByVal hotelId As Integer, ByVal seasonId As Integer)
        Using ctx As New cfrbookEntities
            Dim exists As Boolean =
                (From candidate In ctx.r_TemporadasEF
                 Where candidate.cod_Temporada = seasonId AndAlso candidate.cod_hotel = hotelId
                 Select candidate.cod_Temporada).Any()
            RequireOwned(exists, "Season not found.")
        End Using
    End Sub

    Public Shared Sub RequireRoom(ByVal hotelId As Integer, ByVal roomId As Integer)
        Using ctx As New cfrbookEntities
            Dim exists As Boolean =
                (From candidate In ctx.T_HOTELES_HABITACIONES
                 Where candidate.codhabitacionhotel = roomId AndAlso candidate.Hotel = hotelId
                 Select candidate.codhabitacionhotel).Any()
            RequireOwned(exists, "Room not found.")
        End Using
    End Sub

    Public Shared Sub RequireCancellationPolicy(ByVal hotelId As Integer, ByVal policyId As Integer)
        Using ctx As New cfrbookEntities
            Dim exists As Boolean =
                (From candidate In ctx.R_PolicyCancel
                 Where candidate.policyID = policyId AndAlso candidate.cod_hotel = hotelId
                 Select candidate.cod_hotel).Any()
            RequireOwned(exists, "Cancellation policy not found.")
        End Using
    End Sub

    Public Shared Sub RequireSeasonPeriod(ByVal hotelId As Integer, ByVal periodId As Integer)
        Using ctx As New cfrbookEntities
            Dim exists As Boolean =
                (From period In ctx.R_Temporadas_PeriodosEF
                 Join season In ctx.r_TemporadasEF On period.cod_temporada Equals season.cod_Temporada
                 Where period.cod_periodo = periodId AndAlso season.cod_hotel = hotelId
                 Select period.cod_periodo).Any()
            RequireOwned(exists, "Season period not found.")
        End Using
    End Sub

    Public Shared Sub RequireTariffRoom(ByVal hotelId As Integer, ByVal tariffRoomId As Integer)
        Using ctx As New cfrbookEntities
            Dim exists As Boolean =
                (From relation In ctx.R_Tarifa_habitacionEF
                 Join tariff In ctx.R_tarifasEF On relation.cod_tarifa Equals tariff.cod_tarifa
                 Join room In ctx.T_HOTELES_HABITACIONES On relation.cod_habitacion Equals room.codhabitacionhotel
                 Where relation.id = tariffRoomId AndAlso tariff.cod_hotel = hotelId AndAlso room.Hotel = hotelId
                 Select relation.id).Any()
            RequireOwned(exists, "Tariff-room relation not found.")
        End Using
    End Sub

    Public Shared Sub RequireTariffRoomPair(ByVal hotelId As Integer, ByVal tariffId As Integer, ByVal roomId As Integer)
        Using ctx As New cfrbookEntities
            Dim exists As Boolean =
                (From relation In ctx.R_Tarifa_habitacionEF
                 Join tariff In ctx.R_tarifasEF On relation.cod_tarifa Equals tariff.cod_tarifa
                 Join room In ctx.T_HOTELES_HABITACIONES On relation.cod_habitacion Equals room.codhabitacionhotel
                 Where relation.cod_tarifa = tariffId AndAlso relation.cod_habitacion = roomId AndAlso
                       tariff.cod_hotel = hotelId AndAlso room.Hotel = hotelId
                 Select relation.id).Any()
            RequireOwned(exists, "Tariff-room relation not found.")
        End Using
    End Sub

    Public Shared Sub RequireYieldClosure(ByVal hotelId As Integer, ByVal closureId As Integer)
        Const sql As String =
            "SELECT TOP (1) id_YieldMNG FROM R_YieldMng_Cierre WHERE id_YieldMNG = @closureId AND cod_hotel = @hotelId"
        Dim result As System.Data.DataTable = ind.bk.DBMgr.getDT(
            sql,
            New System.Data.SqlClient.SqlParameter("@closureId", System.Data.SqlDbType.Int) With {.Value = closureId},
            New System.Data.SqlClient.SqlParameter("@hotelId", System.Data.SqlDbType.Int) With {.Value = hotelId})
        RequireOwned(result.Rows.Count = 1, "Inventory closure not found.")
    End Sub

    Private Shared Sub RequireOwned(ByVal exists As Boolean, ByVal message As String)
        If Not exists Then
            Throw New HttpException(404, message)
        End If
    End Sub
End Class
