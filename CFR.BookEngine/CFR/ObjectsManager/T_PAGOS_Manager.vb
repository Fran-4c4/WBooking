Imports SystemImports System.TextImports System.DataImports System.Data.SqlClientImports System.Xml.SerializationImports System.Runtime.SerializationImports System.Runtime.InteropServicesNamespace ind.bk.obj<Serializable()><DataContractAttribute(IsReference:=True)><ClassInterface(ClassInterfaceType.AutoDual)>Public Class T_PAGOS_Manager      inherits ind.db.DBHelper''' <summary>''' A creatable COM class must have a Public Sub New() ''' with no parameters, otherwise, the class will not be ''' registered in the COM registry and cannot be created ''' via CreateObject.''' </summary>Public Sub New()    MyBase.New()End SubPublic Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_PAGOS_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_PAGOS_GetByID")
            addSqlParameterSimple(cmd, "cod_formapago",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function insert(obj2 as T_PAGOS, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_PAGOS_Insert")
addSqlParameterSimple(cmd, T_PAGOS.dbcol.formapago,  obj2.formapago)addSqlParameterSimple(cmd, T_PAGOS.dbcol.Ingles,  obj2.Ingles)addSqlParameterSimple(cmd, T_PAGOS.dbcol.Aleman,  obj2.Aleman)addSqlParameterSimple(cmd, T_PAGOS.dbcol.Frances,  obj2.Frances)addSqlParameterSimple(cmd, T_PAGOS.dbcol.Catalan,  obj2.Catalan)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.cod_formapago = dt.Rows(0)(0)
            Return dt
        End FunctionPublic Function Update(obj2 as T_PAGOS, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_PAGOS_UpdateByID")
addSqlParameterSimple(cmd, T_PAGOS.dbcol.cod_formapago,  obj2.cod_formapago)addSqlParameterSimple(cmd, T_PAGOS.dbcol.formapago,  IIf(IsNothing(obj2.formapago),"",obj2.formapago ))addSqlParameterSimple(cmd, T_PAGOS.dbcol.Ingles,  IIf(IsNothing(obj2.Ingles),"",obj2.Ingles ))addSqlParameterSimple(cmd, T_PAGOS.dbcol.Aleman,  IIf(IsNothing(obj2.Aleman),"",obj2.Aleman ))addSqlParameterSimple(cmd, T_PAGOS.dbcol.Frances,  IIf(IsNothing(obj2.Frances),"",obj2.Frances ))addSqlParameterSimple(cmd, T_PAGOS.dbcol.Catalan,  IIf(IsNothing(obj2.Catalan),"",obj2.Catalan ))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionPublic Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_PAGOS_DeleteByID")
addSqlParameterSimple(cmd, T_PAGOS.dbcol.cod_formapago,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionEnd  Class 
End Namespace 
