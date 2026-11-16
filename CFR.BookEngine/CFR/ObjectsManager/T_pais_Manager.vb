
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
Public Class T_pais_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_pais_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_pais_GetByID")
            addSqlParameterSimple(cmd, "id",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as T_pais, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_pais_Insert")

addSqlParameterSimple(cmd, T_pais.dbcol.cod_Pais,  obj2.cod_Pais)
addSqlParameterSimple(cmd, T_pais.dbcol.Pais,  obj2.Pais)
addSqlParameterSimple(cmd, T_pais.dbcol.cod_cont,  obj2.cod_cont)
addSqlParameterSimple(cmd, T_pais.dbcol.prefijoTelfPais,  obj2.prefijoTelfPais)
addSqlParameterSimple(cmd, T_pais.dbcol.idioma1,  obj2.idioma1)
addSqlParameterSimple(cmd, T_pais.dbcol.idioma2,  obj2.idioma2)
addSqlParameterSimple(cmd, T_pais.dbcol.idioma3,  obj2.idioma3)
addSqlParameterSimple(cmd, T_pais.dbcol.idioma4,  obj2.idioma4)
addSqlParameterSimple(cmd, T_pais.dbcol.CODE,  obj2.CODE)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id = dt.Rows(0)(0)
            Return dt
        End Function
Public Function Update(obj2 as T_pais, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_pais_UpdateByID")

addSqlParameterSimple(cmd, T_pais.dbcol.id,  obj2.id)
addSqlParameterSimple(cmd, T_pais.dbcol.cod_Pais,  obj2.cod_Pais)
addSqlParameterSimple(cmd, T_pais.dbcol.Pais,  IIf(IsNothing(obj2.Pais),"",obj2.Pais ))
addSqlParameterSimple(cmd, T_pais.dbcol.cod_cont,  obj2.cod_cont)
addSqlParameterSimple(cmd, T_pais.dbcol.prefijoTelfPais,  IIf(IsNothing(obj2.prefijoTelfPais),"",obj2.prefijoTelfPais ))
addSqlParameterSimple(cmd, T_pais.dbcol.idioma1,  IIf(IsNothing(obj2.idioma1),"",obj2.idioma1 ))
addSqlParameterSimple(cmd, T_pais.dbcol.idioma2,  IIf(IsNothing(obj2.idioma2),"",obj2.idioma2 ))
addSqlParameterSimple(cmd, T_pais.dbcol.idioma3,  IIf(IsNothing(obj2.idioma3),"",obj2.idioma3 ))
addSqlParameterSimple(cmd, T_pais.dbcol.idioma4,  IIf(IsNothing(obj2.idioma4),"",obj2.idioma4 ))
addSqlParameterSimple(cmd, T_pais.dbcol.CODE,  IIf(IsNothing(obj2.CODE),"",obj2.CODE ))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_pais_DeleteByID")

addSqlParameterSimple(cmd, T_pais.dbcol.id,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
