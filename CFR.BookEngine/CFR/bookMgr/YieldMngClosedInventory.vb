Imports Microsoft.VisualBasic

Namespace cfr

    ''' <summary>
    ''' class to control yield management inventory for closed days
    ''' </summary>
    ''' <remarks></remarks>
    Public Class YieldMngClosedInventory
        Inherits ind.bk.obj.R_YieldMng_Cierre_Manager

        ''' <summary>
        ''' return the closed days for rooms in a hotel
        ''' </summary>
        ''' <param name="cod_Hotel">hotel code</param>
        ''' <param name="dateIn"></param>
        ''' <param name="dateOut"></param>
        ''' <param name="cod_operador">optional operator code</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ListClosed(ByVal cod_Hotel As Integer _
                , ByVal dateIn As Date _
                , ByVal dateOut As Date _
                , Optional ByVal cod_operador As Integer = 0 _
                ) As Data.DataTable


            Dim sSQL As String

            Dim parameters As New System.Collections.Generic.List(Of Data.SqlClient.SqlParameter)

            sSQL = "SELECT  *, convert(varchar(10),fechainicio,103)as fecha1 ,convert(varchar(10),fechafin,103)as fecha2"
            sSQL = sSQL & " FROM R_YieldMng_Cierre "
            sSQL = sSQL & " WHERE R_YieldMng_Cierre.cod_HOTEL = @hotelId"
            parameters.Add(New Data.SqlClient.SqlParameter("@hotelId", Data.SqlDbType.Int) With {.Value = cod_Hotel})

            If cod_operador > 0 Then 'all operators and the selected operator
                sSQL = sSQL & " AND (R_YieldMng_Cierre.cod_Operador = -2 OR R_YieldMng_Cierre.cod_Operador = @operatorId)"
                parameters.Add(New Data.SqlClient.SqlParameter("@operatorId", Data.SqlDbType.Int) With {.Value = cod_operador})
            ElseIf cod_operador = -2 Then 'all operators only
                sSQL = sSQL & " AND (R_YieldMng_Cierre.cod_Operador =-2 )"
            ElseIf cod_operador <> 0 Then
                Throw New ArgumentOutOfRangeException("cod_operador")
            End If

            sSQL = sSQL & " AND FECHAINICIO <= @dateOut AND fechafin >= @dateIn"
            parameters.Add(New Data.SqlClient.SqlParameter("@dateOut", Data.SqlDbType.DateTime) With {.Value = dateOut})
            parameters.Add(New Data.SqlClient.SqlParameter("@dateIn", Data.SqlDbType.DateTime) With {.Value = dateIn})

            sSQL = sSQL & " ORDER BY fechainicio "

            Return ind.bk.DBMgr.getDT(sSQL, parameters.ToArray())

        End Function

        Public Function getDtClosedDaysByCod_hotel(cod_hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_YieldMng_Cierre_GetByCod_hotel")
            addSqlParameterSimple(cmd, ind.bk.obj.T_HOTELES.DBCol.cod_Hotel, cod_hotel)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

    End Class
End Namespace
