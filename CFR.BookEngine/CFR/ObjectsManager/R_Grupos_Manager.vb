Imports SystemImports System.TextImports System.DataImports System.Data.SqlClientImports System.Xml.SerializationImports System.Runtime.SerializationImports System.Runtime.InteropServicesNamespace ind.bk.obj<Serializable()><DataContractAttribute(IsReference:=True)><ClassInterface(ClassInterfaceType.AutoDual)>Public Class R_Grupos_Manager      inherits ind.db.DBHelper''' <summary>''' A creatable COM class must have a Public Sub New() ''' with no parameters, otherwise, the class will not be ''' registered in the COM registry and cannot be created ''' via CreateObject.''' </summary>Public Sub New()    MyBase.New()End SubPublic Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Grupos_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Grupos_GetByID")
            addSqlParameterSimple(cmd, "cod_grupo",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function insert(obj2 as R_Grupos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Grupos_Insert")
addSqlParameterSimple(cmd, R_Grupos.dbcol.Grupo,  obj2.Grupo)addSqlParameterSimple(cmd, R_Grupos.dbcol.TIPO,  obj2.TIPO)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.cod_grupo = dt.Rows(0)(0)
            Return dt
        End FunctionPublic Function Update(obj2 as R_Grupos, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Grupos_UpdateByID")
addSqlParameterSimple(cmd, R_Grupos.dbcol.cod_grupo,  obj2.cod_grupo)addSqlParameterSimple(cmd, R_Grupos.dbcol.Grupo,  IIf(IsNothing(obj2.Grupo),"",obj2.Grupo ))addSqlParameterSimple(cmd, R_Grupos.dbcol.TIPO,  IIf(IsNothing(obj2.TIPO),"",obj2.TIPO ))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionPublic Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Grupos_DeleteByID")
addSqlParameterSimple(cmd, R_Grupos.dbcol.cod_grupo,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionEnd  Class 
End Namespace 
