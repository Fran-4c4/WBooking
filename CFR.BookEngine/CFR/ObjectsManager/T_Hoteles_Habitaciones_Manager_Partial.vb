Imports cfr.tool

Namespace ind.bk.obj

    Partial Public Class T_Hoteles_Habitaciones_Manager

        Public Shared Function GetRoomFotosByRoomCode(ByVal codhabitacionhotel As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As DataTable
            Dim paramCol As New Generic.List(Of SqlClient.SqlParameter)
            paramCol.Add(ind.bk.DBMgr.getSpParameter("codhabitacionhotel", codhabitacionhotel))
            Dim dt As DataTable = ind.bk.DBMgr.getDataTable(trans, "T_HOTELES_HABITACIONES_fotos_GetByID", paramCol)
            Return dt
        End Function
        ''' <summary>
        ''' Return room data of the hotel
        ''' </summary>
        ''' <param name="cod_Hotel"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getHotelRoomColection(ByVal cod_Hotel As Integer) As System.Collections.Generic.Dictionary(Of Integer, cfr.obj.HotelRoomData)
            Dim a As New System.Collections.Generic.Dictionary(Of Integer, cfr.obj.HotelRoomData)
            Dim dt As Data.DataTable = ind.bk.obj.T_Hoteles_Habitaciones_Manager.GetHotelRoomsData(cod_Hotel)
            For Each dr As Data.DataRow In dt.Rows
                Dim o As cfr.obj.HotelRoomData = getRoomObjectByRow(dr)
                a.Add(o.idEntity, o)
            Next

            Return a
        End Function

        Public Shared Function GetEntity(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.HotelRoomData
            Dim paramCol(0) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            Dim dt As DataTable = ind.bk.DBMgr.getDT("HotelRoom_GetByID", paramCol, trans)
            Dim obj As cfr.obj.HotelRoomData = Nothing
            If dt.Rows.Count > 0 Then
                obj = getRoomObjectByRow(dt.Rows(0))
            End If
            Return obj
        End Function


        Public Shared Function getRoomObjectByRow(dr As DataRow) As cfr.obj.HotelRoomData
            Dim o As cfr.obj.HotelRoomData = cfr.obj.HotelRoomData.roomCollection(dr("codhabitacionhotel"))
            If Not o Is Nothing Then
                Return o
            End If

            o = New cfr.obj.HotelRoomData
            
            o.cod_Hotel = dr("Hotel")
            o.idEntity = cfr.tool.Tools.cNull(dr("codhabitacionhotel"))

            o.cod_hab_combinada = cfr.tool.Tools.cNull(dr("cod_hab_combinada"), 0)
            o.codhabitacionGenerica = cfr.tool.Tools.cNull(dr("codhabitacionGenerica"), 0)

            o.Adultos = cfr.tool.Tools.cNull(dr("adultos"), 0)
            o.MaxPersonas = cfr.tool.Tools.cNull(dr("MaxPersonas"), 0)

            o.NinoOcupaplaza = cfr.tool.Tools.cNull(dr("NinoOcupaplaza"), 0)
            o.AdultosExtra1 = cfr.tool.Tools.cNull(dr("AdultosExtra1"), 0)
            o.AdultosExtra2 = cfr.tool.Tools.cNull(dr("AdultosExtra2"), 0)
            o.NumNino = cfr.tool.Tools.cNull(dr("NumNino"), 0)
            o.MaxAdultos = cfr.tool.Tools.cNull(dr("maxAdultos"), 0)

            o.description.es = cfr.tool.Tools.cNull(dr("habitacionhotel"))
            o.description.en = cfr.tool.Tools.cNull(dr("desc_en"))
            o.description.de = cfr.tool.Tools.cNull(dr("desc_de"))
            o.description.fr = cfr.tool.Tools.cNull(dr("desc_fr"))
            o.description.it = cfr.tool.Tools.cNull(dr("desc_it"))
            o.description.ca = cfr.tool.Tools.cNull(dr("desc_ca"))

            o.serviceInfo.es = cfr.tool.Tools.cNull(dr("ServiciosEsp"))
            o.serviceInfo.en = cfr.tool.Tools.cNull(dr("ServiciosIngles"))
            o.serviceInfo.de = cfr.tool.Tools.cNull(dr("ServiciosAleman"))
            o.serviceInfo.fr = cfr.tool.Tools.cNull(dr("ServiciosFrances"))
            o.serviceInfo.it = cfr.tool.Tools.cNull(dr("ServiciosItaliano"))
            o.serviceInfo.ca = cfr.tool.Tools.cNull(dr("ServiciosCatalan"))

            cfr.obj.HotelRoomData.roomCollection.addItemObject(o)

            Return o

        End Function


        Public Shared Function GetHotelRoomsData(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As DataTable
            Dim paramCol(0) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            Dim dt As DataTable = ind.bk.DBMgr.getDT("HotelRoom_GetListByHotelID", paramCol, trans)
            Return dt
        End Function


        Public Function DeleteByCod_habitacion(cod_habitacion As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_habitaciones_SERVICIOS_DeleteByCodhabitacion")

            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones_Servicios.DBCol.cod_habitacion, cod_habitacion)

            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

        Public Function getDataTableGetByCodhabitacionForAdmin(cod_habitacion As Integer, propertyType As cfr.CFRConst.unitTypeEnum, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_habitaciones_Servicios_GetByCodhabitacionForAdmin")
            addSqlParameterSimple(cmd, T_Hoteles_Habitaciones_Servicios.DBCol.cod_habitacion, cod_habitacion)
            addSqlParameterSimple(cmd, "tipohotel", propertyType)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function

    End Class

End Namespace