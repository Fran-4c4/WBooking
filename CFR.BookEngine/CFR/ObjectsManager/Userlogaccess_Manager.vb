Imports SystemImports System.TextImports System.DataImports System.Data.SqlClientImports System.Xml.SerializationImports System.Runtime.SerializationImports System.Runtime.InteropServicesNamespace ind.bk.obj<Serializable()><DataContractAttribute(IsReference:=True)><ClassInterface(ClassInterfaceType.AutoDual)>Public Class Userlogaccess_Manager      inherits ind.db.DBHelper''' <summary>''' A creatable COM class must have a Public Sub New() ''' with no parameters, otherwise, the class will not be ''' registered in the COM registry and cannot be created ''' via CreateObject.''' </summary>Public Sub New()    MyBase.New()End SubPublic Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Userlogaccess_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Userlogaccess_GetByID")
            addSqlParameterSimple(cmd, "id",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionPublic Function insert(obj2 as Userlogaccess, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Userlogaccess_Insert")
addSqlParameterSimple(cmd, Userlogaccess.dbcol.userid,  obj2.userid)addSqlParameterSimple(cmd, Userlogaccess.dbcol.logon,  obj2.logon)addSqlParameterSimple(cmd, Userlogaccess.dbcol.logoff,  obj2.logoff)addSqlParameterSimple(cmd, Userlogaccess.dbcol.ipaccess,  obj2.ipaccess)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id = dt.Rows(0)(0)
            Return dt
        End FunctionPublic Function Update(obj2 as Userlogaccess, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Userlogaccess_UpdateByID")
            addSqlParameterSimple(cmd, Userlogaccess.DBCol.id, obj2.id)            addSqlParameterSimple(cmd, Userlogaccess.DBCol.userid, obj2.userid)            addSqlParameterSimple(cmd, Userlogaccess.DBCol.logon, IIf(IsNothing(obj2.logon), "", obj2.logon))            addSqlParameterSimple(cmd, Userlogaccess.DBCol.logoff, IIf(IsNothing(obj2.logoff), "", obj2.logoff))            addSqlParameterSimple(cmd, Userlogaccess.DBCol.ipaccess, IIf(IsNothing(obj2.ipaccess), "", obj2.ipaccess))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionPublic Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("Userlogaccess_DeleteByID")
addSqlParameterSimple(cmd, Userlogaccess.dbcol.id,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionEnd  Class 
End Namespace 
