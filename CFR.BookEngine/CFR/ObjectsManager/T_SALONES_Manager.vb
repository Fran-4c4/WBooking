Imports SystemImports System.TextImports System.DataImports System.Data.SqlClientImports System.Xml.SerializationImports System.Runtime.SerializationImports System.Runtime.InteropServicesNamespace ind.bk.obj<Serializable()><DataContractAttribute(IsReference:=True)><ClassInterface(ClassInterfaceType.AutoDual)>Public Class T_SALONES_Manager      inherits ind.db.DBHelper''' <summary>''' A creatable COM class must have a Public Sub New() ''' with no parameters, otherwise, the class will not be ''' registered in the COM registry and cannot be created ''' via CreateObject.''' </summary>Public Sub New()    MyBase.New()End SubPublic Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SALONES_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SALONES_GetByID")
            addSqlParameterSimple(cmd, "Salon",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function insert(obj2 as T_SALONES, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SALONES_Insert")
addSqlParameterSimple(cmd, T_SALONES.dbcol.Nombre,  obj2.Nombre)addSqlParameterSimple(cmd, T_SALONES.dbcol.Hotel,  obj2.Hotel)addSqlParameterSimple(cmd, T_SALONES.dbcol.m2,  obj2.m2)addSqlParameterSimple(cmd, T_SALONES.dbcol.Teatro,  obj2.Teatro)addSqlParameterSimple(cmd, T_SALONES.dbcol.Escuela,  obj2.Escuela)addSqlParameterSimple(cmd, T_SALONES.dbcol.Cocktail,  obj2.Cocktail)addSqlParameterSimple(cmd, T_SALONES.dbcol.Banquete,  obj2.Banquete)addSqlParameterSimple(cmd, T_SALONES.dbcol.MesaI,  obj2.MesaI)addSqlParameterSimple(cmd, T_SALONES.dbcol.MesaU,  obj2.MesaU)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.Salon = dt.Rows(0)(0)
            Return dt
        End FunctionPublic Function Update(obj2 as T_SALONES, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SALONES_UpdateByID")
addSqlParameterSimple(cmd, T_SALONES.dbcol.Salon,  obj2.Salon)addSqlParameterSimple(cmd, T_SALONES.dbcol.Nombre,  IIf(IsNothing(obj2.Nombre),"",obj2.Nombre ))addSqlParameterSimple(cmd, T_SALONES.dbcol.Hotel,  obj2.Hotel)addSqlParameterSimple(cmd, T_SALONES.dbcol.m2,  obj2.m2)addSqlParameterSimple(cmd, T_SALONES.dbcol.Teatro,  obj2.Teatro)addSqlParameterSimple(cmd, T_SALONES.dbcol.Escuela,  obj2.Escuela)addSqlParameterSimple(cmd, T_SALONES.dbcol.Cocktail,  obj2.Cocktail)addSqlParameterSimple(cmd, T_SALONES.dbcol.Banquete,  obj2.Banquete)addSqlParameterSimple(cmd, T_SALONES.dbcol.MesaI,  obj2.MesaI)addSqlParameterSimple(cmd, T_SALONES.dbcol.MesaU,  obj2.MesaU)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionPublic Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_SALONES_DeleteByID")
addSqlParameterSimple(cmd, T_SALONES.dbcol.Salon,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionEnd  Class 
End Namespace 
