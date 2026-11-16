Imports SystemImports System.TextImports System.DataImports System.Data.SqlClientImports System.Xml.SerializationImports System.Runtime.SerializationImports System.Runtime.InteropServicesNamespace ind.bk.obj<Serializable()><DataContractAttribute(IsReference:=True)><ClassInterface(ClassInterfaceType.AutoDual)>Public Class R_ServiciosExtra_Manager      inherits ind.db.DBHelper''' <summary>''' A creatable COM class must have a Public Sub New() ''' with no parameters, otherwise, the class will not be ''' registered in the COM registry and cannot be created ''' via CreateObject.''' </summary>Public Sub New()    MyBase.New()End SubPublic Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ServiciosExtra_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ServiciosExtra_GetByID")
            addSqlParameterSimple(cmd, "id_servicExtra",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function insert(obj2 as R_ServiciosExtra, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ServiciosExtra_Insert")
addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.cod_hotel,  obj2.cod_hotel)addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.cod_CRS,  obj2.cod_CRS)addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.fechainicio,  obj2.fechainicio)addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.fechafin,  obj2.fechafin)addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.DescEsp,  obj2.DescEsp)addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.DescIngles,  obj2.DescIngles)addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.DescFrances,  obj2.DescFrances)addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.DescAleman,  obj2.DescAleman)addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.DescItalian,  obj2.DescItalian)addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.DescCatalan,  obj2.DescCatalan)addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.Precio,  obj2.Precio)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id_servicExtra = dt.Rows(0)(0)
            Return dt
        End FunctionPublic Function Update(obj2 as R_ServiciosExtra, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ServiciosExtra_UpdateByID")
addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.id_servicExtra,  obj2.id_servicExtra)addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.cod_hotel,  obj2.cod_hotel)addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.cod_CRS,  obj2.cod_CRS)addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.fechainicio,  IIf(IsNothing(obj2.fechainicio),"",obj2.fechainicio ))addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.fechafin,  IIf(IsNothing(obj2.fechafin),"",obj2.fechafin ))addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.DescEsp,  IIf(IsNothing(obj2.DescEsp),"",obj2.DescEsp ))addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.DescIngles,  IIf(IsNothing(obj2.DescIngles),"",obj2.DescIngles ))addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.DescFrances,  IIf(IsNothing(obj2.DescFrances),"",obj2.DescFrances ))addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.DescAleman,  IIf(IsNothing(obj2.DescAleman),"",obj2.DescAleman ))addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.DescItalian,  IIf(IsNothing(obj2.DescItalian),"",obj2.DescItalian ))addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.DescCatalan,  IIf(IsNothing(obj2.DescCatalan),"",obj2.DescCatalan ))addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.Precio,  obj2.Precio)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionPublic Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ServiciosExtra_DeleteByID")
addSqlParameterSimple(cmd, R_ServiciosExtra.dbcol.id_servicExtra,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionEnd  Class 
End Namespace 
