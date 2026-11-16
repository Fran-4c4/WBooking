
Imports System
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports System.Runtime.InteropServices

Namespace ind.bk.obj

    Partial Public Class T_HOTELES_SERVICIOS_Manager


        Public Function getDataTableGetByCodhotelForAdmin(codHotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_Servicios_GetByCodhotelForAdmin")
            addSqlParameterSimple(cmd, T_HOTELES_SERVICIOS.DBCol.cod_Hotel, codHotel)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        
       
        Public Function DeleteByCodHotel(cod_hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_SERVICIOS_DeleteByCodHotel")

            addSqlParameterSimple(cmd, T_HOTELES.DBCol.cod_Hotel, cod_hotel)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
