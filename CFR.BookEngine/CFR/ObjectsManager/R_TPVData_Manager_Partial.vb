
Namespace ind.bk.obj
    
    Partial Public Class R_TPVDATA_Manager


        Public Function getHotelTPVDataByIDHotel(cod_Hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_TPVDATA_GetByIDHotel")
            addSqlParameterSimple(cmd, ind.bk.obj.T_HOTELES.DBCol.cod_Hotel, cod_Hotel)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function



    End Class
End Namespace