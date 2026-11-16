Imports SystemImports System.TextImports System.DataImports System.Data.SqlClientImports System.Xml.SerializationImports System.Runtime.SerializationImports System.Runtime.InteropServicesNamespace ind.bk.obj<Serializable()><DataContractAttribute(IsReference:=True)><ClassInterface(ClassInterfaceType.AutoDual)>Public Class TAXES_Manager      inherits ind.db.DBHelper''' <summary>''' A creatable COM class must have a Public Sub New() ''' with no parameters, otherwise, the class will not be ''' registered in the COM registry and cannot be created ''' via CreateObject.''' </summary>Public Sub New()    MyBase.New()End SubPublic Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("TAXES_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("TAXES_GetByID")
            addSqlParameterSimple(cmd, "TAXE_ID",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function insert(obj2 as TAXES, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("TAXES_Insert")
addSqlParameterSimple(cmd, TAXES.dbcol.TAXE_Description,  obj2.TAXE_Description)addSqlParameterSimple(cmd, TAXES.dbcol.TAXE_Value,  obj2.TAXE_Value)addSqlParameterSimple(cmd, TAXES.dbcol.TAXE_state,  obj2.TAXE_state)addSqlParameterSimple(cmd, TAXES.dbcol.TAXE_ValidUntil,  obj2.TAXE_ValidUntil)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.TAXE_ID = dt.Rows(0)(0)
            Return dt
        End FunctionPublic Function Update(obj2 as TAXES, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("TAXES_UpdateByID")
addSqlParameterSimple(cmd, TAXES.dbcol.TAXE_ID,  obj2.TAXE_ID)addSqlParameterSimple(cmd, TAXES.dbcol.TAXE_Description,  IIf(IsNothing(obj2.TAXE_Description),"",obj2.TAXE_Description ))addSqlParameterSimple(cmd, TAXES.dbcol.TAXE_Value,  obj2.TAXE_Value)addSqlParameterSimple(cmd, TAXES.dbcol.TAXE_state,  obj2.TAXE_state)addSqlParameterSimple(cmd, TAXES.dbcol.TAXE_ValidUntil,  IIf(IsNothing(obj2.TAXE_ValidUntil),"",obj2.TAXE_ValidUntil ))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionPublic Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("TAXES_DeleteByID")
addSqlParameterSimple(cmd, TAXES.dbcol.TAXE_ID,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionEnd  Class 
End Namespace 
