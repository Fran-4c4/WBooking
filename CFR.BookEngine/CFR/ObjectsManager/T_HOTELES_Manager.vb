
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
    Public Class T_HOTELES_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_GetByID")
            addSqlParameterSimple(cmd, "rowId", ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
        Public Function insert(obj2 As T_HOTELES, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_Insert")

            addSqlParameterSimple(cmd, T_HOTELES.DBCol.cod_Hotel, obj2.cod_Hotel)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Tipo, obj2.Tipo)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.hotel, obj2.hotelName)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Direccion, obj2.Direccion)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.PrefijoTelf, obj2.PrefijoTelf)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Telefono, obj2.Telefono)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Fax, obj2.Fax)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.cod_Categoria, obj2.cod_Categoria)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.codpostal, obj2.codpostal)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_es, obj2.desc_es)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_en, obj2.desc_en)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_de, obj2.desc_de)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_fr, obj2.desc_fr)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_cat, obj2.desc_cat)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Cadena1, obj2.Cadena1)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Email, obj2.Email)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Web, obj2.Web)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Habitaciones, obj2.Habitaciones)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Datos, obj2.Datos)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Director, obj2.Director)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Directorcomercial, obj2.Directorcomercial)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.nfotos, obj2.nfotos)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.f1, obj2.f1)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Estado, obj2.Estado)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.login, obj2.login)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.clave, obj2.clave)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.latitud, obj2.latitud)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.longitud, obj2.longitud)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.lastUpdate, obj2.lastUpdate)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_it, obj2.desc_it)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_pt, obj2.desc_pt)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_jp, obj2.desc_jp)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.currencycode, obj2.currencycode)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.MNGProp, obj2.MNGProp)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.UltimoAcceso, obj2.UltimoAcceso)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.checkintime1, obj2.checkintime1)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.checkintime2, obj2.checkintime2)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.checkOuttime1, obj2.checkOuttime1)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.checkOuttime2, obj2.checkOuttime2)

            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.rowId = dt.Rows(0)(0)
            Return dt
        End Function

        Public Function Update(obj2 As T_HOTELES, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object


            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_UpdateByID")

            addSqlParameterSimple(cmd, T_HOTELES.DBCol.cod_Hotel, obj2.cod_Hotel)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Tipo, IIf(IsNothing(obj2.Tipo), "", obj2.Tipo))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.hotel, IIf(IsNothing(obj2.hotelName), "", obj2.hotelName))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Direccion, IIf(IsNothing(obj2.Direccion), "", obj2.Direccion))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.PrefijoTelf, IIf(IsNothing(obj2.PrefijoTelf), "", obj2.PrefijoTelf))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Telefono, IIf(IsNothing(obj2.Telefono), "", obj2.Telefono))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Fax, IIf(IsNothing(obj2.Fax), "", obj2.Fax))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.cod_Categoria, obj2.cod_Categoria)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.codpostal, IIf(IsNothing(obj2.codpostal), "", obj2.codpostal))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_es, obj2.desc_es)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_en, obj2.desc_en)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_de, obj2.desc_de)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_fr, obj2.desc_fr)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_cat, obj2.desc_cat)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Cadena1, obj2.Cadena1)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Email, IIf(IsNothing(obj2.Email), "", obj2.Email))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Web, IIf(IsNothing(obj2.Web), "", obj2.Web))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Habitaciones, obj2.Habitaciones)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Datos, obj2.Datos)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Director, IIf(IsNothing(obj2.Director), "", obj2.Director))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Directorcomercial, IIf(IsNothing(obj2.Directorcomercial), "", obj2.Directorcomercial))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.nfotos, obj2.nfotos)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.f1, IIf(IsNothing(obj2.f1), "", obj2.f1))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.Estado, obj2.Estado)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.login, IIf(IsNothing(obj2.login), "", obj2.login))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.clave, IIf(IsNothing(obj2.clave), "", obj2.clave))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.rowId, obj2.rowId)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.latitud, obj2.latitud)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.longitud, obj2.longitud)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.lastUpdate, IIf(IsNothing(obj2.lastUpdate), "", obj2.lastUpdate))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_it, obj2.desc_it)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_pt, obj2.desc_pt)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.desc_jp, obj2.desc_jp)
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.currencycode, IIf(IsNothing(obj2.currencycode), "", obj2.currencycode))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.MNGProp, IIf(IsNothing(obj2.MNGProp), "", obj2.MNGProp))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.UltimoAcceso, IIf(IsNothing(obj2.UltimoAcceso), "", obj2.UltimoAcceso))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.checkintime1, IIf(IsNothing(obj2.checkintime1), "", obj2.checkintime1))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.checkintime2, IIf(IsNothing(obj2.checkintime2), "", obj2.checkintime2))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.checkOuttime1, IIf(IsNothing(obj2.checkOuttime1), "", obj2.checkOuttime1))
            addSqlParameterSimple(cmd, T_HOTELES.DBCol.checkOuttime2, IIf(IsNothing(obj2.checkOuttime2), "", obj2.checkOuttime2))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function


        Public Function Delete(objID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_HOTELES_DeleteByID")

            addSqlParameterSimple(cmd, T_HOTELES.DBCol.rowId, objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
    End Class

End Namespace
