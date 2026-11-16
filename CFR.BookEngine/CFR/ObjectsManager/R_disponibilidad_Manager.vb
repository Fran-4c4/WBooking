Imports SystemImports System.TextImports System.DataImports System.Data.SqlClientImports System.Xml.SerializationImports System.Runtime.SerializationImports System.Runtime.InteropServicesNamespace ind.bk.obj<Serializable()><DataContractAttribute(IsReference:=True)><ClassInterface(ClassInterfaceType.AutoDual)>Public Class R_disponibilidad_Manager      inherits ind.db.DBHelper''' <summary>''' A creatable COM class must have a Public Sub New() ''' with no parameters, otherwise, the class will not be ''' registered in the COM registry and cannot be created ''' via CreateObject.''' </summary>Public Sub New()    MyBase.New()End SubPublic Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_disponibilidad_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_disponibilidad_GetByID")
            addSqlParameterSimple(cmd, "id_Disp_Hab",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function insert(obj2 as R_disponibilidad, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_disponibilidad_Insert")
            addSqlParameterSimple(cmd, R_disponibilidad.DBCol.cod_hotel, obj2.cod_hotel)            addSqlParameterSimple(cmd, R_disponibilidad.DBCol.cod_habitacion, obj2.cod_habitacion)            addSqlParameterSimple(cmd, R_disponibilidad.DBCol.dia, obj2.dia)            addSqlParameterSimple(cmd, R_disponibilidad.DBCol.cod_CRS, obj2.cod_CRS)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id_Disp_Hab = dt.Rows(0)(0)
            Return dt
        End FunctionPublic Function Update(obj2 as R_disponibilidad, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_disponibilidad_UpdateByID")
addSqlParameterSimple(cmd, R_disponibilidad.dbcol.id_Disp_Hab,  obj2.id_Disp_Hab)addSqlParameterSimple(cmd, R_disponibilidad.dbcol.cod_hotel,  obj2.cod_hotel)addSqlParameterSimple(cmd, R_disponibilidad.dbcol.cod_habitacion,  obj2.cod_habitacion)addSqlParameterSimple(cmd, R_disponibilidad.dbcol.dia,  IIf(IsNothing(obj2.dia),"",obj2.dia ))addSqlParameterSimple(cmd, R_disponibilidad.dbcol.cod_CRS,  obj2.cod_CRS)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionPublic Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_disponibilidad_DeleteByID")
addSqlParameterSimple(cmd, R_disponibilidad.dbcol.id_Disp_Hab,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionEnd  Class 
End Namespace 
