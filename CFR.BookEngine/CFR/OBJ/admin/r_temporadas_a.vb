Imports System
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports System.Runtime.InteropServices

Namespace ind.bk.obj
    Partial Class R_Temporadas_Manager

        Public Function getTemporadasByCodHotel(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("r_Temporadas_GetAllByIdHotel")
            addSqlParameterSimple(cmd, ind.bk.obj.T_HOTELES.DBCol.cod_Hotel, ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function getPeriodosByIDTemporada(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Temporadas_Periodos_GetByIDTemporada")
            addSqlParameterSimple(cmd, ind.bk.obj.R_Temporadas_Periodos.DBCol.cod_temporada, ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function getPeriodosActiveByIDTemporada(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Temporadas_PeriodosActive_GetByIDTemporada")
            addSqlParameterSimple(cmd, ind.bk.obj.R_Temporadas_Periodos.DBCol.cod_temporada, ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

    End Class
End Namespace

