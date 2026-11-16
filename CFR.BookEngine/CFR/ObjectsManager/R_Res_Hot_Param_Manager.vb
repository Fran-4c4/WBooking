
Imports System
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports System.Runtime.InteropServices

Namespace ind.bk.obj

<Serializable()>
<DataContractAttribute(IsReference:=True)>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_Res_Hot_Param_Manager
      inherits ind.db.DBHelper



''' <summary>
''' A creatable COM class must have a Public Sub New() 
''' with no parameters, otherwise, the class will not be 
''' registered in the COM registry and cannot be created 
''' via CreateObject.
''' </summary>
Public Sub New()
    MyBase.New()
End Sub
Public Function getDataTableAll(Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Res_Hot_Param_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
        Public Function getDataTableByID(cod_hotel As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Res_Hot_Param_GetByID")
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.cod_hotel, cod_hotel)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function insert(obj2 as R_Res_Hot_Param, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Res_Hot_Param_Insert")

addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.cod_hotel,  obj2.cod_hotel)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.EdadMaxNinoGratis,  obj2.EdadMaxNinoGratis)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.EdadMaxNinoPago,  obj2.EdadMaxNinoPago)

addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.impuestos,  obj2.impuestos)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.moneda,  obj2.moneda)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.Alojamiento,  obj2.Alojamiento)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.AD,  obj2.AD)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.MP,  obj2.MP)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.PC, obj2.PC)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.HasTPV, obj2.HasTPV)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.CCC,  obj2.CCC)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.emailConfirmacion,  obj2.emailConfirmacion)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.txtConfirmacion,  obj2.txtConfirmacion)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.logo,  obj2.logo)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.diasdeAntelacion,  obj2.diasdeAntelacion)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.txtConfirmacionIngles,  obj2.txtConfirmacionIngles)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.txtConfirmacionFrances,  obj2.txtConfirmacionFrances)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.txtConfirmacionAleman,  obj2.txtConfirmacionAleman)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.txtConfirmacionItaliano,  obj2.txtConfirmacionItaliano)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.txtConfirmacionCatalan,  obj2.txtConfirmacionCatalan)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.TodoIncluido,  obj2.TodoIncluido)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.polCatalan,  obj2.polCatalan)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.polItaliano,  obj2.polItaliano)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.polAleman,  obj2.polAleman)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.polFrances,  obj2.polFrances)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.polIngles,  obj2.polIngles)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.polEsp,  obj2.polEsp)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.txtCancelEsp,  obj2.txtCancelEsp)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.txtCancelIngles,  obj2.txtCancelIngles)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.txtCancelFrances,  obj2.txtCancelFrances)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.txtCancelAleman,  obj2.txtCancelAleman)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.txtCancelItaliano,  obj2.txtCancelItaliano)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.txtCancelCatalan,  obj2.txtCancelCatalan)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.formatoEmailConfirmacion,  obj2.formatoEmailConfirmacion)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.emailConfirmacion2,  obj2.emailConfirmacion2)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.maxhab2see,  obj2.maxhab2see)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.comisionyahnormal,  obj2.comisionyahnormal)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.comisionyahregistrados,  obj2.comisionyahregistrados)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.Estado,  obj2.Estado)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.CCCIBAN,  obj2.CCCIBAN)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.CCCBIC,  obj2.CCCBIC)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.CCCBanco,  obj2.CCCBanco)
addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.CCCSucursal,  obj2.CCCSucursal)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.ID = dt.Rows(0)(0)
            Return dt
        End Function

        Public Function Update(obj2 As R_Res_Hot_Param, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Res_Hot_Param_UpdateByID")


            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.cod_hotel, obj2.cod_hotel)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.EdadMaxNinoGratis, obj2.EdadMaxNinoGratis)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.EdadMaxNinoPago, obj2.EdadMaxNinoPago)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.impuestos, obj2.impuestos)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.moneda, IIf(IsNothing(obj2.moneda), "", obj2.moneda))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.Alojamiento, obj2.Alojamiento)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.AD, obj2.AD)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.MP, obj2.MP)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.PC, obj2.PC)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.CCC, IIf(IsNothing(obj2.CCC), "", obj2.CCC))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.emailConfirmacion, IIf(IsNothing(obj2.emailConfirmacion), "", obj2.emailConfirmacion))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtConfirmacion, IIf(IsNothing(obj2.txtConfirmacion), "", obj2.txtConfirmacion))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.logo, IIf(IsNothing(obj2.logo), "", obj2.logo))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.diasdeAntelacion, obj2.diasdeAntelacion)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtConfirmacionIngles, IIf(IsNothing(obj2.txtConfirmacionIngles), "", obj2.txtConfirmacionIngles))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtConfirmacionFrances, IIf(IsNothing(obj2.txtConfirmacionFrances), "", obj2.txtConfirmacionFrances))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtConfirmacionAleman, IIf(IsNothing(obj2.txtConfirmacionAleman), "", obj2.txtConfirmacionAleman))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtConfirmacionItaliano, IIf(IsNothing(obj2.txtConfirmacionItaliano), "", obj2.txtConfirmacionItaliano))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtConfirmacionCatalan, IIf(IsNothing(obj2.txtConfirmacionCatalan), "", obj2.txtConfirmacionCatalan))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.TodoIncluido, obj2.TodoIncluido)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.polCatalan, IIf(IsNothing(obj2.polCatalan), "", obj2.polCatalan))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.polItaliano, IIf(IsNothing(obj2.polItaliano), "", obj2.polItaliano))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.polAleman, IIf(IsNothing(obj2.polAleman), "", obj2.polAleman))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.polFrances, IIf(IsNothing(obj2.polFrances), "", obj2.polFrances))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.polIngles, IIf(IsNothing(obj2.polIngles), "", obj2.polIngles))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.polEsp, IIf(IsNothing(obj2.polEsp), "", obj2.polEsp))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelEsp, IIf(IsNothing(obj2.txtCancelEsp), "", obj2.txtCancelEsp))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelIngles, IIf(IsNothing(obj2.txtCancelIngles), "", obj2.txtCancelIngles))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelFrances, IIf(IsNothing(obj2.txtCancelFrances), "", obj2.txtCancelFrances))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelAleman, IIf(IsNothing(obj2.txtCancelAleman), "", obj2.txtCancelAleman))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelItaliano, IIf(IsNothing(obj2.txtCancelItaliano), "", obj2.txtCancelItaliano))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.txtCancelCatalan, IIf(IsNothing(obj2.txtCancelCatalan), "", obj2.txtCancelCatalan))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.formatoEmailConfirmacion, IIf(IsNothing(obj2.formatoEmailConfirmacion), "", obj2.formatoEmailConfirmacion))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.emailConfirmacion2, IIf(IsNothing(obj2.emailConfirmacion2), "", obj2.emailConfirmacion2))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.maxhab2see, obj2.maxhab2see)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.comisionyahnormal, obj2.comisionyahnormal)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.comisionyahregistrados, obj2.comisionyahregistrados)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.Estado, obj2.Estado)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.CCCIBAN, IIf(IsNothing(obj2.CCCIBAN), "", obj2.CCCIBAN))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.CCCBIC, IIf(IsNothing(obj2.CCCBIC), "", obj2.CCCBIC))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.CCCBanco, IIf(IsNothing(obj2.CCCBanco), "", obj2.CCCBanco))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.CCCSucursal, IIf(IsNothing(obj2.CCCSucursal), "", obj2.CCCSucursal))
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.HasTPV, obj2.HasTPV)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.boardAD, obj2.boardAD)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.boardMP, obj2.boardMP)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.boardPC, obj2.boardPC)
            addSqlParameterSimple(cmd, R_Res_Hot_Param.DBCol.boardTI, obj2.boardTI)

            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)

            Return dt

        End Function

Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_Res_Hot_Param_DeleteByID")

addSqlParameterSimple(cmd, R_Res_Hot_Param.dbcol.ID,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
