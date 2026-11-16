Imports Microsoft.VisualBasic
Imports System.Data
Imports cfr.tool
Imports ind.bk.obj


Namespace cfr.mgr

    Public Class holdPropertyCollection
        Inherits Hashtable

        Public Sub addHolder(holderProperty As cfr.obj.Hotel)
            If Me.ContainsKey(holderProperty.id) = False Then
                Me.Add(holderProperty.id, holderProperty)
                Debug.WriteLine("cfr.obj.Hotel added to cache: " & holderProperty.id)
            Else
                Me.Item(holderProperty.id) = holderProperty
                Debug.WriteLine("cfr.obj.Hotel updated to cache: " & holderProperty.id)
            End If


        End Sub

        Public Function getHolder(holderPropertyID As Integer) As cfr.obj.Hotel
            Dim hld As cfr.obj.Hotel = Me.Item(holderPropertyID)
            Return hld
        End Function

        Public Function getHolder(holderProperty As cfr.obj.Hotel) As cfr.obj.Hotel
            Dim hld As cfr.obj.Hotel = Me.Item(holderProperty.id)
            Return hld
        End Function
    End Class



    Public Class HotelMgr
        Inherits ind.bk.obj.T_HOTELES_Manager

        Public Shared holdpropList As New holdPropertyCollection


        Public Shared Function CREATE(ByRef obj As cfr.obj.Hotel, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Hotel
            Dim paramCol(24) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("cod_Hotel", obj.cod_Hotel)
            paramCol(1) = ind.bk.DBMgr.getSpParameter("hotel", obj.hotelName)
            paramCol(2) = ind.bk.DBMgr.getSpParameter("Direccion", obj.Direccion)
            paramCol(3) = ind.bk.DBMgr.getSpParameter("Telefono", obj.Telefono)
            paramCol(4) = ind.bk.DBMgr.getSpParameter("Fax", obj.Fax)
            paramCol(5) = ind.bk.DBMgr.getSpParameter("cod_Ciudad", obj.city.idEntity)
            paramCol(6) = ind.bk.DBMgr.getSpParameter("cod_Categoria", CInt(obj.cod_Categoria))
            paramCol(7) = ind.bk.DBMgr.getSpParameter("codpostal", obj.codpostal)
            paramCol(8) = ind.bk.DBMgr.getSpParameter("cod_Cadena", obj.chain.code)
            paramCol(9) = ind.bk.DBMgr.getSpParameter("Email", obj.Email)
            paramCol(10) = ind.bk.DBMgr.getSpParameter("Web", obj.Web)
            paramCol(11) = ind.bk.DBMgr.getSpParameter("Habitaciones", obj.Habitaciones)
            paramCol(12) = ind.bk.DBMgr.getSpParameter("Datos", obj.Datos)
            paramCol(16) = ind.bk.DBMgr.getSpParameter("Estado", obj.Estado)
            paramCol(17) = ind.bk.DBMgr.getSpParameter("login", obj.login)
            paramCol(18) = ind.bk.DBMgr.getSpParameter("clave", obj.clave)
            paramCol(20) = ind.bk.DBMgr.getSpParameter("latitud", obj.latitud)
            paramCol(21) = ind.bk.DBMgr.getSpParameter("longitud", obj.longitud)
            paramCol(22) = ind.bk.DBMgr.getSpParameter("lastUpdate", obj.lastUpdate)
            paramCol(23) = ind.bk.DBMgr.getSpParameter("currencycode", obj.currencycode)
            paramCol(24) = ind.bk.DBMgr.getSpParameter("lastAccess", obj.lastAccess)
            Dim dt As System.Data.DataTable = ind.bk.DBMgr.getDT("T_HOTELES_insert", paramCol, trans)
            If dt.Rows.Count > 0 Then
                obj.cod_Hotel = dt.Rows(0)(0)
            End If
            Return obj
        End Function


        Public Shared Function GetEntity(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Hotel
         

            Dim p As New Generic.List(Of SqlClient.SqlParameter)
            p.Add(ind.bk.DBMgr.getSpParameter("id", id))

            Dim dt As DataTable = ind.bk.DBMgr.getDataTable(trans, "sp_HotelGetDataByID", p)
            Dim obj As cfr.obj.Hotel = Nothing
            If dt.Rows.Count > 0 Then
                obj = New cfr.obj.Hotel

                HotelMgr.fillObjectByRow(obj, dt.Rows(0))

                obj.description = New cfr.obj.HotelDescription
                obj.description.cod_Hotel = obj.cod_Hotel
                obj.description.desc_esp = obj.desc_es
                obj.description.desc_en = obj.desc_en
                obj.description.desc_de = obj.desc_de
                obj.description.desc_fr = obj.desc_fr
                obj.description.desc_cat = obj.desc_cat
                obj.description.desc_it = obj.desc_it
                obj.description.desc_pt = obj.desc_pt
                obj.description.desc_jp = obj.desc_jp

                Return obj
                obj.description = cfr.mgr.HotelMgr.DescriptionGetEntity(id)
                obj.photos = cfr.mgr.HotelMgr.GetHotelPhotos(id)
            End If
            Return obj
        End Function



        ''' <summary>
        ''' Get full data from cache or from DDBB
        ''' </summary>
        ''' <param name="id"></param>
        ''' <param name="trans"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetEntityFull(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing, Optional forceUpdate As Boolean = False) As cfr.obj.Hotel
            Dim obj As cfr.obj.Hotel = Nothing
            If forceUpdate = False Then
                obj = holdpropList.getHolder(id)
            End If

            If obj Is Nothing Then
                obj = GetEntityFullFromDDBB(id, trans)
                If Not obj Is Nothing Then
                    holdpropList.addHolder(obj)
                End If
            End If

            Return obj
        End Function

        Public Shared Function GetEntityFullRefreshCache(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Hotel
            Dim obj As cfr.obj.Hotel

            obj = GetEntityFullFromDDBB(id, trans)
            If Not obj Is Nothing Then
                holdpropList.addHolder(obj)
            End If


            Return obj
        End Function

        Public Shared Function GetEntityFullFromDDBB(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Hotel
            Dim obj As cfr.obj.Hotel

            obj = GetEntity(id, trans)
            If Not obj Is Nothing Then
                obj.locations = GetHotelLocations(id, trans)
                obj.services = GetHotelServices(id, trans)
                obj.saloons = GetHotelSaloons(id, trans)
                obj.BookParam = cfr.mgr.HotelBookParameters.GetEntity(id)
            End If

            Return obj
        End Function

        Public Shared Function GetHotelPhotos(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As System.Collections.Generic.Dictionary(Of Integer, cfr.obj.Photo)
            Dim paramCol(0) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            Dim dt As DataTable = ind.bk.DBMgr.getDT("T_HOTELES_GetFotos", paramCol, trans)

            Dim photos As New System.Collections.Generic.Dictionary(Of Integer, cfr.obj.Photo)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim obj As New cfr.obj.Photo
                    obj.cod_hotel = dr("cod_hotel")
                    obj.id_Foto = dr("id_Foto")
                    obj.FotoFileName = dr("FotoFileName")
                    obj.Description = dr("Description")
                    obj.LastUpdate = dr("LastUpdate")
                    obj.id_fotoType = dr("id_fotoType")
                    photos.Add(obj.id_Foto, obj)
                Next
            End If
            Return photos

        End Function
        Public Shared Function GetHotelServices(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As DataTable
            Dim paramCol(0) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            Dim dt As DataTable = ind.bk.DBMgr.getDT("T_Hoteles_GetServices", paramCol, trans)

            Return dt

        End Function

        Public Shared Function GetHotelLocations(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As DataTable
            Dim paramCol(0) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            Dim dt As DataTable = ind.bk.DBMgr.getDT("T_Hoteles_GetLocations", paramCol, trans)

            Return dt

        End Function

        Public Shared Function GetHotelSaloons(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As DataTable
            Dim paramCol(0) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            Dim dt As DataTable = ind.bk.DBMgr.getDT("sp_Hotel_GetSaloons", paramCol, trans)

            Return dt

        End Function

        Public Shared Function GetHotelByCredentials(ByVal login As String, ByVal clave As String, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.Hotel
            Dim paramCol(1) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("login", login)
            paramCol(1) = ind.bk.DBMgr.getSpParameter("clave", clave)
            Dim dt As DataTable = ind.bk.DBMgr.getDT("sp_GetHotelDataByTopCredentials", paramCol, trans)
            Dim obj As cfr.obj.Hotel = Nothing
            If dt.Rows.Count > 0 Then
                obj = New cfr.obj.Hotel
                Dim hotelID As Integer = dt.Rows(0)(ind.bk.obj.T_HOTELES.DBCol.cod_Hotel)
                Return GetEntityFull(hotelID)
            End If
            Return obj
        End Function

        Private Shared Sub fillObjectByRow(ByRef obj As cfr.obj.Hotel, ByVal row As System.Data.DataRow)

            cfr.obj.Hotel.FillObjectByRow(row, obj)

            obj.city = cfr.mgr.CityMgr.GetEntity(obj.cod_Ciudad)

            If obj.Cadena1 = 0 Then
                obj.chain = New Chain
                obj.chain.code = 0
                obj.chain.description = "Independiente"
            Else
                obj.chain = New Chain
                'Dim chainMgr As New ind.bk.obj.T_CADENAS_Manager
                obj.chain.code = Tools.cNull(obj.Cadena1, 0)
                obj.chain.description = Tools.cNull(row(ind.bk.obj.T_HOTELES.DBCol.Cadena1), "Independiente")
            End If



        End Sub

        Public Shared Sub updateUserLastAccess(ByVal cod_hotel As Integer)
            Try
                Dim s As String = "UPDATE t_hoteles SET ultimoacceso=GETDATE() WHERE cod_hotel=@hotelId"
                ind.bk.DBMgr.Execute(
                    s,
                    New SqlClient.SqlParameter("@hotelId", SqlDbType.Int) With {.Value = cod_hotel})
            Catch ex As Exception

            End Try


        End Sub




        Public Shared Function DescriptionCREATE(ByRef obj As cfr.obj.HotelDescription, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.HotelDescription
            Dim paramCol(8) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("cod_Hotel", obj.cod_Hotel)
            paramCol(1) = ind.bk.DBMgr.getSpParameter("desc_esp", obj.desc_esp)
            paramCol(2) = ind.bk.DBMgr.getSpParameter("desc_en", obj.desc_en)
            paramCol(3) = ind.bk.DBMgr.getSpParameter("desc_de", obj.desc_de)
            paramCol(4) = ind.bk.DBMgr.getSpParameter("desc_fr", obj.desc_fr)
            paramCol(5) = ind.bk.DBMgr.getSpParameter("desc_cat", obj.desc_cat)
            paramCol(6) = ind.bk.DBMgr.getSpParameter("desc_it", obj.desc_it)
            paramCol(7) = ind.bk.DBMgr.getSpParameter("desc_pt", obj.desc_pt)
            paramCol(8) = ind.bk.DBMgr.getSpParameter("desc_jp", obj.desc_jp)
            Dim dt As System.Data.DataTable = ind.bk.DBMgr.getDT("T_Hoteles_Desc_insert", paramCol, trans)

            Return obj
        End Function


        Public Shared Function DescriptionUPDATE(ByRef obj As cfr.obj.HotelDescription, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.HotelDescription
            Dim paramCol(8) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("cod_Hotel", obj.cod_Hotel)
            paramCol(1) = ind.bk.DBMgr.getSpParameter("desc_esp", obj.desc_esp)
            paramCol(2) = ind.bk.DBMgr.getSpParameter("desc_en", obj.desc_en)
            paramCol(3) = ind.bk.DBMgr.getSpParameter("desc_de", obj.desc_de)
            paramCol(4) = ind.bk.DBMgr.getSpParameter("desc_fr", obj.desc_fr)
            paramCol(5) = ind.bk.DBMgr.getSpParameter("desc_cat", obj.desc_cat)
            paramCol(6) = ind.bk.DBMgr.getSpParameter("desc_it", obj.desc_it)
            paramCol(7) = ind.bk.DBMgr.getSpParameter("desc_pt", obj.desc_pt)
            paramCol(8) = ind.bk.DBMgr.getSpParameter("desc_jp", obj.desc_jp)
            ind.bk.DBMgr.ExecuteSp("T_Hoteles_Desc_update", paramCol, trans)
            Return obj
        End Function

        Public Shared Sub DescriptionDelete(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing)
            Dim paramCol(0) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            ind.bk.DBMgr.ExecuteSp("T_Hoteles_Desc_delete", paramCol, trans)
        End Sub

        Public Shared Function DescriptionGetEntity(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.HotelDescription
            Dim paramCol(0) As SqlClient.SqlParameter
            paramCol(0) = ind.bk.DBMgr.getSpParameter("id", id)
            Dim dt As DataTable = ind.bk.DBMgr.getDT("sp_HotelGetDataByID", paramCol, trans)
            Dim obj As cfr.obj.HotelDescription = Nothing
            If dt.Rows.Count > 0 Then
                obj = New cfr.obj.HotelDescription
                obj.cod_Hotel = Tools.cNull(dt.Rows(0)("cod_Hotel"), 0)
                obj.desc_esp = Tools.cNull(dt.Rows(0)("desc_es"), "")
                obj.desc_en = Tools.cNull(dt.Rows(0)("desc_en"), "")
                obj.desc_de = Tools.cNull(dt.Rows(0)("desc_de"), "")
                obj.desc_fr = Tools.cNull(dt.Rows(0)("desc_fr"), "")
                obj.desc_cat = Tools.cNull(dt.Rows(0)("desc_cat"), "")
                obj.desc_it = Tools.cNull(dt.Rows(0)("desc_it"), "")
                obj.desc_pt = Tools.cNull(dt.Rows(0)("desc_pt"), "")
                obj.desc_jp = Tools.cNull(dt.Rows(0)("desc_jp"), "")
            End If
            Return obj
        End Function

        Public Function UpdateHotelLoginAndPassword(ByRef obj As cfr.obj.Hotel, newLogin As String, newpwd As String, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("sp_HotelesUpdateLoginPwd")

            addSqlParameterSimple(cmd, T_HOTELes.dbcol.cod_Hotel, obj.cod_Hotel)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.login, newLogin)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.clave, newpwd)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function


    End Class
End Namespace




