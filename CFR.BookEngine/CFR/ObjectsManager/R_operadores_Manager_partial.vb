
Namespace ind.bk.obj
    
    Partial Public Class R_operadores_Manager


        Public Function getOperadoresByIDHotel(cod_Hotel As Integer, Optional TIPO As cfr.CFRConst.ClientTypeEnum = cfr.CFRConst.ClientTypeEnum.None, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand(cfr.db.sp.sproc.Generic.R_Operadores_GetByIDHotel)
            addSqlParameterSimple(cmd, ind.bk.obj.T_HOTELES.DBCol.cod_Hotel, cod_Hotel)
            addSqlParameterSimple(cmd, ind.bk.obj.R_operadores.DBCol.tipo, TIPO)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function DeleteOperador(cod_operador As Integer, cod_hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_operadores_DeleteByID")

            addSqlParameterSimple(cmd, R_operadores.DBCol.cod_operador, cod_operador)
            addSqlParameterSimple(cmd, R_operadores.DBCol.cod_hotel, cod_hotel)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
 

    End Class
End Namespace