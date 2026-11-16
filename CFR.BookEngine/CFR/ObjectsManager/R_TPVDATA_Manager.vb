
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
Public Class R_TPVDATA_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_TPVDATA_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_TPVDATA_GetByID")
            addSqlParameterSimple(cmd, "ID",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as R_TPVDATA, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_TPVDATA_Insert")

addSqlParameterSimple(cmd, R_TPVDATA.dbcol.cod_hotel,  obj2.cod_hotel)
addSqlParameterSimple(cmd, R_TPVDATA.dbcol.Ds_Merchant_MerchantCode,  obj2.Ds_Merchant_MerchantCode)
addSqlParameterSimple(cmd, R_TPVDATA.dbcol.Ds_Merchant_Terminal,  obj2.Ds_Merchant_Terminal)
addSqlParameterSimple(cmd, R_TPVDATA.dbcol.Ds_Merchant_Password,  obj2.Ds_Merchant_Password)
addSqlParameterSimple(cmd, R_TPVDATA.dbcol.Ds_Merchant_Currency,  obj2.Ds_Merchant_Currency)
addSqlParameterSimple(cmd, R_TPVDATA.dbcol.Ds_Merchant_ConsumerLanguage,  obj2.Ds_Merchant_ConsumerLanguage)
addSqlParameterSimple(cmd, R_TPVDATA.dbcol.Ds_Merchant_TransactionType,  obj2.Ds_Merchant_TransactionType)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.ID = dt.Rows(0)(0)
            Return dt
        End Function
Public Function Update(obj2 as R_TPVDATA, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_TPVDATA_UpdateByID")

addSqlParameterSimple(cmd, R_TPVDATA.dbcol.ID,  obj2.ID)
addSqlParameterSimple(cmd, R_TPVDATA.dbcol.cod_hotel,  obj2.cod_hotel)
addSqlParameterSimple(cmd, R_TPVDATA.dbcol.Ds_Merchant_MerchantCode,  IIf(IsNothing(obj2.Ds_Merchant_MerchantCode),"",obj2.Ds_Merchant_MerchantCode ))
addSqlParameterSimple(cmd, R_TPVDATA.dbcol.Ds_Merchant_Terminal,  IIf(IsNothing(obj2.Ds_Merchant_Terminal),"",obj2.Ds_Merchant_Terminal ))
addSqlParameterSimple(cmd, R_TPVDATA.dbcol.Ds_Merchant_Password,  IIf(IsNothing(obj2.Ds_Merchant_Password),"",obj2.Ds_Merchant_Password ))
addSqlParameterSimple(cmd, R_TPVDATA.dbcol.Ds_Merchant_Currency,  IIf(IsNothing(obj2.Ds_Merchant_Currency),"",obj2.Ds_Merchant_Currency ))
addSqlParameterSimple(cmd, R_TPVDATA.dbcol.Ds_Merchant_ConsumerLanguage,  IIf(IsNothing(obj2.Ds_Merchant_ConsumerLanguage),"",obj2.Ds_Merchant_ConsumerLanguage ))
addSqlParameterSimple(cmd, R_TPVDATA.dbcol.Ds_Merchant_TransactionType,  IIf(IsNothing(obj2.Ds_Merchant_TransactionType),"",obj2.Ds_Merchant_TransactionType ))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("R_TPVDATA_DeleteByID")

addSqlParameterSimple(cmd, R_TPVDATA.dbcol.ID,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
