Imports SystemImports System.TextImports System.DataImports System.Data.SqlClientImports System.Xml.SerializationImports System.Runtime.SerializationImports System.Runtime.InteropServicesNamespace ind.bk.obj<Serializable()><DataContractAttribute(IsReference:=True)><ClassInterface(ClassInterfaceType.AutoDual)>Public Class T_SERVICIOS_Manager      inherits ind.db.DBHelper''' <summary>''' A creatable COM class must have a Public Sub New() ''' with no parameters, otherwise, the class will not be ''' registered in the COM registry and cannot be created ''' via CreateObject.''' </summary>Public Sub New()    MyBase.New()End SubPublic Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SERVICIOS_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SERVICIOS_GetByID")
            addSqlParameterSimple(cmd, "cod_Servicio",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function insert(obj2 as T_SERVICIOS, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SERVICIOS_Insert")
addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.servicio,  obj2.servicio)addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.Ingles,  obj2.Ingles)addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.Aleman,  obj2.Aleman)addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.Frances,  obj2.Frances)addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.Catalan,  obj2.Catalan)addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.Buscar,  obj2.Buscar)addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.Padre,  obj2.Padre)addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.tipo_carac,  obj2.tipo_carac)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.cod_Servicio = dt.Rows(0)(0)
            Return dt
        End FunctionPublic Function Update(obj2 as T_SERVICIOS, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SERVICIOS_UpdateByID")
addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.cod_Servicio,  obj2.cod_Servicio)addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.servicio,  IIf(IsNothing(obj2.servicio),"",obj2.servicio ))addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.Ingles,  IIf(IsNothing(obj2.Ingles),"",obj2.Ingles ))addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.Aleman,  IIf(IsNothing(obj2.Aleman),"",obj2.Aleman ))addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.Frances,  IIf(IsNothing(obj2.Frances),"",obj2.Frances ))addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.Catalan,  IIf(IsNothing(obj2.Catalan),"",obj2.Catalan ))addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.Buscar,  obj2.Buscar)addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.Padre,  obj2.Padre)addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.tipo_carac,  obj2.tipo_carac)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionPublic Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SERVICIOS_DeleteByID")
addSqlParameterSimple(cmd, T_SERVICIOS.dbcol.cod_Servicio,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionEnd  Class 
End Namespace 
