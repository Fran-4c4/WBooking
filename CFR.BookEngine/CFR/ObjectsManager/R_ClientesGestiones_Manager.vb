Imports SystemImports System.TextImports System.DataImports System.Data.SqlClientImports System.Xml.SerializationImports System.Runtime.SerializationImports System.Runtime.InteropServicesNamespace ind.bk.obj<Serializable()><DataContractAttribute(IsReference:=True)><ClassInterface(ClassInterfaceType.AutoDual)>Public Class R_ClientesGestiones_Manager      inherits ind.db.DBHelper''' <summary>''' A creatable COM class must have a Public Sub New() ''' with no parameters, otherwise, the class will not be ''' registered in the COM registry and cannot be created ''' via CreateObject.''' </summary>Public Sub New()    MyBase.New()End SubPublic Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ClientesGestiones_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ClientesGestiones_GetByID")
            addSqlParameterSimple(cmd, "id_Gestion",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function insert(obj2 as R_ClientesGestiones, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ClientesGestiones_Insert")
addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.id_Cliente,  obj2.id_Cliente)addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.id_tipoContacto,  obj2.id_tipoContacto)addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.FechaContacto,  obj2.FechaContacto)addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.Asunto,  obj2.Asunto)addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.Notas,  obj2.Notas)addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.Resultado,  obj2.Resultado)addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.FechaModificacion,  obj2.FechaModificacion)addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.Estado,  obj2.Estado)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id_Gestion = dt.Rows(0)(0)
            Return dt
        End FunctionPublic Function Update(obj2 as R_ClientesGestiones, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ClientesGestiones_UpdateByID")
addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.id_Gestion,  obj2.id_Gestion)addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.id_Cliente,  obj2.id_Cliente)addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.id_tipoContacto,  obj2.id_tipoContacto)addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.FechaContacto,  IIf(IsNothing(obj2.FechaContacto),"",obj2.FechaContacto ))addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.Asunto,  IIf(IsNothing(obj2.Asunto),"",obj2.Asunto ))addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.Notas,  IIf(IsNothing(obj2.Notas),"",obj2.Notas ))addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.Resultado,  IIf(IsNothing(obj2.Resultado),"",obj2.Resultado ))addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.FechaModificacion,  IIf(IsNothing(obj2.FechaModificacion),"",obj2.FechaModificacion ))addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.Estado,  obj2.Estado)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionPublic Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_ClientesGestiones_DeleteByID")
addSqlParameterSimple(cmd, R_ClientesGestiones.dbcol.id_Gestion,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionEnd  Class 
End Namespace 
