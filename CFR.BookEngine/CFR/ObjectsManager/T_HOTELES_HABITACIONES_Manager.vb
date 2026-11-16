
Imports System
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports System.Runtime.InteropServices

Namespace ind.bk.obj

    <Serializable()>
    <DataContractAttribute(IsReference:=True)>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class T_Hoteles_Habitaciones_Manager
        Inherits ind.db.DBHelper

        ''' <summary>
        ''' A creatable COM class must have a Public Sub New() 
        ''' with no parameters, otherwise, the class will not be 
        ''' registered in the COM registry and cannot be created 
        ''' via CreateObject.
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub
        Public Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_HABITACIONES_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_HABITACIONES_GetByID")
            addSqlParameterSimple(cmd, "codhabitacionhotel", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function getDataTableGetByCodhotel(codHotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_HABITACIONES_GetByCodhotel")
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.cod_Hotel, codHotel)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function insert(obj2 As T_Hoteles_Habitaciones, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_HABITACIONES_Insert")

            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.Hotel, obj2.Hotel)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.habitacionhotel, obj2.habitacionhotel)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.codhabitacionGenerica, obj2.codhabitacionGenerica)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.descripcion, obj2.descripcion)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.Adultos, obj2.Adultos)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.AdultosExtra1, obj2.AdultosExtra1)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.AdultosExtra2, obj2.AdultosExtra2)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.MaxPersonas, obj2.MaxPersonas)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.NumNino, obj2.NumNino)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.DBCol.MaxAdultos, obj2.MaxAdultos)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.DBCol.numRoomsAvailable, obj2.numRoomsAvailable)

            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.ServiciosEsp, obj2.ServiciosEsp)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.ServiciosIngles, obj2.ServiciosIngles)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.ServiciosFrances, obj2.ServiciosFrances)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.ServiciosAleman, obj2.ServiciosAleman)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.ServiciosItaliano, obj2.ServiciosItaliano)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.ServiciosCatalan, obj2.ServiciosCatalan)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.cod_hab_combinada, obj2.cod_hab_combinada)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.NinoOcupaplaza, obj2.NinoOcupaplaza)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.desc_en, obj2.desc_en)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.desc_de, obj2.desc_de)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.desc_fr, obj2.desc_fr)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.desc_it, obj2.desc_it)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.desc_ca, obj2.desc_ca)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.codhabitacionhotel = dt.Rows(0)(0)
            Return dt
        End Function
        Public Function Update(obj2 As T_Hoteles_Habitaciones, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_HABITACIONES_UpdateByID")

            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.Hotel, obj2.Hotel)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.codhabitacionhotel, obj2.codhabitacionhotel)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.habitacionhotel, IIf(IsNothing(obj2.habitacionhotel), "", obj2.habitacionhotel))
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.codhabitacionGenerica, obj2.codhabitacionGenerica)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.descripcion, IIf(IsNothing(obj2.descripcion), "", obj2.descripcion))
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.Adultos, obj2.Adultos)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.AdultosExtra1, obj2.AdultosExtra1)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.AdultosExtra2, obj2.AdultosExtra2)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.MaxPersonas, obj2.MaxPersonas)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.NumNino, obj2.NumNino)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.DBCol.MaxAdultos, obj2.MaxAdultos)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.DBCol.numRoomsAvailable, obj2.numRoomsAvailable)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.ServiciosEsp, IIf(IsNothing(obj2.ServiciosEsp), "", obj2.ServiciosEsp))
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.ServiciosIngles, IIf(IsNothing(obj2.ServiciosIngles), "", obj2.ServiciosIngles))
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.ServiciosFrances, IIf(IsNothing(obj2.ServiciosFrances), "", obj2.ServiciosFrances))
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.ServiciosAleman, IIf(IsNothing(obj2.ServiciosAleman), "", obj2.ServiciosAleman))
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.ServiciosItaliano, IIf(IsNothing(obj2.ServiciosItaliano), "", obj2.ServiciosItaliano))
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.ServiciosCatalan, IIf(IsNothing(obj2.ServiciosCatalan), "", obj2.ServiciosCatalan))
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.cod_hab_combinada, obj2.cod_hab_combinada)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.NinoOcupaplaza, obj2.NinoOcupaplaza)
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.desc_en, IIf(IsNothing(obj2.desc_en), "", obj2.desc_en))
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.desc_de, IIf(IsNothing(obj2.desc_de), "", obj2.desc_de))
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.desc_fr, IIf(IsNothing(obj2.desc_fr), "", obj2.desc_fr))
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.desc_it, IIf(IsNothing(obj2.desc_it), "", obj2.desc_it))
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.desc_ca, IIf(IsNothing(obj2.desc_ca), "", obj2.desc_ca))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_HABITACIONES_DeleteByID")

            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones.dbcol.codhabitacionhotel, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
