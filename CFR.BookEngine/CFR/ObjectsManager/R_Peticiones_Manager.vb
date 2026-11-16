Imports SystemImports System.TextImports System.DataImports System.Data.SqlClientImports System.Xml.SerializationImports System.Runtime.SerializationImports System.Runtime.InteropServicesNamespace ind.bk.obj<Serializable()><DataContractAttribute(IsReference:=True)><ClassInterface(ClassInterfaceType.AutoDual)>Public Class R_Peticiones_Manager      inherits ind.db.DBHelper''' <summary>''' A creatable COM class must have a Public Sub New() ''' with no parameters, otherwise, the class will not be ''' registered in the COM registry and cannot be created ''' via CreateObject.''' </summary>Public Sub New()    MyBase.New()End SubPublic Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Peticiones_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Peticiones_GetByID")
            addSqlParameterSimple(cmd, "id",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function insert(obj2 as R_Peticiones, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Peticiones_Insert")
addSqlParameterSimple(cmd, R_Peticiones.dbcol.cod_hotel,  obj2.cod_hotel)addSqlParameterSimple(cmd, R_Peticiones.dbcol.cod_operador,  obj2.cod_operador)addSqlParameterSimple(cmd, R_Peticiones.dbcol.fechaEntrada,  obj2.fechaEntrada)addSqlParameterSimple(cmd, R_Peticiones.dbcol.fechaSalida,  obj2.fechaSalida)addSqlParameterSimple(cmd, R_Peticiones.dbcol.adultos,  obj2.adultos)addSqlParameterSimple(cmd, R_Peticiones.dbcol.ninosfree,  obj2.ninosfree)addSqlParameterSimple(cmd, R_Peticiones.dbcol.ninospago,  obj2.ninospago)addSqlParameterSimple(cmd, R_Peticiones.dbcol.regimen,  obj2.regimen)addSqlParameterSimple(cmd, R_Peticiones.dbcol.IP,  obj2.IP)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id = dt.Rows(0)(0)
            Return dt
        End FunctionPublic Function Update(obj2 as R_Peticiones, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Peticiones_UpdateByID")
addSqlParameterSimple(cmd, R_Peticiones.dbcol.id,  obj2.id)addSqlParameterSimple(cmd, R_Peticiones.dbcol.cod_hotel,  obj2.cod_hotel)addSqlParameterSimple(cmd, R_Peticiones.dbcol.cod_operador,  obj2.cod_operador)addSqlParameterSimple(cmd, R_Peticiones.dbcol.fechaEntrada,  IIf(IsNothing(obj2.fechaEntrada),"",obj2.fechaEntrada ))addSqlParameterSimple(cmd, R_Peticiones.dbcol.fechaSalida,  IIf(IsNothing(obj2.fechaSalida),"",obj2.fechaSalida ))addSqlParameterSimple(cmd, R_Peticiones.dbcol.adultos,  obj2.adultos)addSqlParameterSimple(cmd, R_Peticiones.dbcol.ninosfree,  obj2.ninosfree)addSqlParameterSimple(cmd, R_Peticiones.dbcol.ninospago,  obj2.ninospago)addSqlParameterSimple(cmd, R_Peticiones.dbcol.regimen,  obj2.regimen)addSqlParameterSimple(cmd, R_Peticiones.dbcol.IP,  IIf(IsNothing(obj2.IP),"",obj2.IP ))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionPublic Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Peticiones_DeleteByID")
addSqlParameterSimple(cmd, R_Peticiones.dbcol.id,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionEnd  Class 
End Namespace 
