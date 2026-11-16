Imports SystemImports System.TextImports System.DataImports System.Data.SqlClientImports System.Xml.SerializationImports System.Runtime.SerializationImports System.Runtime.InteropServicesNamespace ind.bk.obj<Serializable()><DataContractAttribute(IsReference:=True)><ClassInterface(ClassInterfaceType.AutoDual)>Public Class R_Facturas_Manager      inherits ind.db.DBHelper''' <summary>''' A creatable COM class must have a Public Sub New() ''' with no parameters, otherwise, the class will not be ''' registered in the COM registry and cannot be created ''' via CreateObject.''' </summary>Public Sub New()    MyBase.New()End SubPublic Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Facturas_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Facturas_GetByID")
            addSqlParameterSimple(cmd, "id_Factura",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End FunctionPublic Function insert(obj2 as R_Facturas, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Facturas_Insert")
addSqlParameterSimple(cmd, R_Facturas.dbcol.id_Cliente,  obj2.id_Cliente)addSqlParameterSimple(cmd, R_Facturas.dbcol.Num_Factura,  obj2.Num_Factura)addSqlParameterSimple(cmd, R_Facturas.dbcol.FechaEmision,  obj2.FechaEmision)addSqlParameterSimple(cmd, R_Facturas.dbcol.FechaPago,  obj2.FechaPago)addSqlParameterSimple(cmd, R_Facturas.dbcol.Estado,  obj2.Estado)addSqlParameterSimple(cmd, R_Facturas.dbcol.fecha_modificacion,  obj2.fecha_modificacion)addSqlParameterSimple(cmd, R_Facturas.dbcol.NotaPago,  obj2.NotaPago)addSqlParameterSimple(cmd, R_Facturas.dbcol.Comentarios,  obj2.Comentarios)addSqlParameterSimple(cmd, R_Facturas.dbcol.importeSinImpuestos,  obj2.importeSinImpuestos)addSqlParameterSimple(cmd, R_Facturas.dbcol.importeSoloImpuestos,  obj2.importeSoloImpuestos)addSqlParameterSimple(cmd, R_Facturas.dbcol.importeTotal,  obj2.importeTotal)addSqlParameterSimple(cmd, R_Facturas.dbcol.facturaFile,  obj2.facturaFile)addSqlParameterSimple(cmd, R_Facturas.dbcol.facturaReservasExcelFile,  obj2.facturaReservasExcelFile)addSqlParameterSimple(cmd, R_Facturas.dbcol.InternalFacNumber,  obj2.InternalFacNumber)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id_Factura = dt.Rows(0)(0)
            Return dt
        End FunctionPublic Function Update(obj2 as R_Facturas, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Facturas_UpdateByID")
addSqlParameterSimple(cmd, R_Facturas.dbcol.id_Factura,  obj2.id_Factura)addSqlParameterSimple(cmd, R_Facturas.dbcol.id_Cliente,  obj2.id_Cliente)addSqlParameterSimple(cmd, R_Facturas.dbcol.Num_Factura,  IIf(IsNothing(obj2.Num_Factura),"",obj2.Num_Factura ))addSqlParameterSimple(cmd, R_Facturas.dbcol.FechaEmision,  IIf(IsNothing(obj2.FechaEmision),"",obj2.FechaEmision ))addSqlParameterSimple(cmd, R_Facturas.dbcol.FechaPago,  IIf(IsNothing(obj2.FechaPago),"",obj2.FechaPago ))addSqlParameterSimple(cmd, R_Facturas.dbcol.Estado,  obj2.Estado)addSqlParameterSimple(cmd, R_Facturas.dbcol.fecha_modificacion,  IIf(IsNothing(obj2.fecha_modificacion),"",obj2.fecha_modificacion ))addSqlParameterSimple(cmd, R_Facturas.dbcol.NotaPago,  IIf(IsNothing(obj2.NotaPago),"",obj2.NotaPago ))addSqlParameterSimple(cmd, R_Facturas.dbcol.Comentarios,  IIf(IsNothing(obj2.Comentarios),"",obj2.Comentarios ))addSqlParameterSimple(cmd, R_Facturas.dbcol.importeSinImpuestos,  obj2.importeSinImpuestos)addSqlParameterSimple(cmd, R_Facturas.dbcol.importeSoloImpuestos,  obj2.importeSoloImpuestos)addSqlParameterSimple(cmd, R_Facturas.dbcol.importeTotal,  obj2.importeTotal)addSqlParameterSimple(cmd, R_Facturas.dbcol.facturaFile,  IIf(IsNothing(obj2.facturaFile),"",obj2.facturaFile ))addSqlParameterSimple(cmd, R_Facturas.dbcol.facturaReservasExcelFile,  IIf(IsNothing(obj2.facturaReservasExcelFile),"",obj2.facturaReservasExcelFile ))addSqlParameterSimple(cmd, R_Facturas.dbcol.InternalFacNumber,  obj2.InternalFacNumber)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionPublic Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Facturas_DeleteByID")
addSqlParameterSimple(cmd, R_Facturas.dbcol.id_Factura,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End FunctionEnd  Class 
End Namespace 
