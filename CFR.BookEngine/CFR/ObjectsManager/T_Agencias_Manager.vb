
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
Public Class T_Agencias_Manager
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
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Agencias_GetAll")
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function getDataTableByID(ID As Integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As DataTable

            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Agencias_GetByID")
            addSqlParameterSimple(cmd, "id",  ID)
            Dim dt As DataTable = Me.GetData(cmd:=cmd, trans:=trans)
            Return dt
End Function
Public Function insert(obj2 as T_Agencias, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Agencias_Insert")

addSqlParameterSimple(cmd, T_Agencias.dbcol.cod_agencia,  obj2.cod_agencia)
addSqlParameterSimple(cmd, T_Agencias.dbcol.Agencia,  obj2.Agencia)
addSqlParameterSimple(cmd, T_Agencias.dbcol.email,  obj2.email)
addSqlParameterSimple(cmd, T_Agencias.dbcol.web,  obj2.web)
addSqlParameterSimple(cmd, T_Agencias.dbcol.tlf,  obj2.tlf)
addSqlParameterSimple(cmd, T_Agencias.dbcol.fax,  obj2.fax)
addSqlParameterSimple(cmd, T_Agencias.dbcol.IATA,  obj2.IATA)
addSqlParameterSimple(cmd, T_Agencias.dbcol.Direccion,  obj2.Direccion)
addSqlParameterSimple(cmd, T_Agencias.dbcol.Cod_Postal,  obj2.Cod_Postal)
addSqlParameterSimple(cmd, T_Agencias.dbcol.CIF,  obj2.CIF)
addSqlParameterSimple(cmd, T_Agencias.dbcol.cod_ciudad,  obj2.cod_ciudad)
addSqlParameterSimple(cmd, T_Agencias.dbcol.VerTodos,  obj2.VerTodos)
addSqlParameterSimple(cmd, T_Agencias.dbcol.VerConDatos,  obj2.VerConDatos)
addSqlParameterSimple(cmd, T_Agencias.dbcol.VerConAcuerdo,  obj2.VerConAcuerdo)
addSqlParameterSimple(cmd, T_Agencias.dbcol.login,  obj2.login)
addSqlParameterSimple(cmd, T_Agencias.dbcol.clave,  obj2.clave)
addSqlParameterSimple(cmd, T_Agencias.dbcol.director,  obj2.director)
addSqlParameterSimple(cmd, T_Agencias.dbcol.director_comercial,  obj2.director_comercial)
addSqlParameterSimple(cmd, T_Agencias.dbcol.nota_Reserva,  obj2.nota_Reserva)
addSqlParameterSimple(cmd, T_Agencias.dbcol.Cod_WEB,  obj2.Cod_WEB)
addSqlParameterSimple(cmd, T_Agencias.dbcol.email_reserva,  obj2.email_reserva)
addSqlParameterSimple(cmd, T_Agencias.dbcol.cod_agencia_gen,  obj2.cod_agencia_gen)
addSqlParameterSimple(cmd, T_Agencias.dbcol.tlf_reservas,  obj2.tlf_reservas)
addSqlParameterSimple(cmd, T_Agencias.dbcol.numtcredito,  obj2.numtcredito)
addSqlParameterSimple(cmd, T_Agencias.dbcol.tituloagencia,  obj2.tituloagencia)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            obj2.id = dt.Rows(0)(0)
            Return dt
        End Function
Public Function Update(obj2 as T_Agencias, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Agencias_UpdateByID")

addSqlParameterSimple(cmd, T_Agencias.dbcol.id,  obj2.id)
addSqlParameterSimple(cmd, T_Agencias.dbcol.cod_agencia,  obj2.cod_agencia)
addSqlParameterSimple(cmd, T_Agencias.dbcol.Agencia,  IIf(IsNothing(obj2.Agencia),"",obj2.Agencia ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.email,  IIf(IsNothing(obj2.email),"",obj2.email ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.web,  IIf(IsNothing(obj2.web),"",obj2.web ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.tlf,  IIf(IsNothing(obj2.tlf),"",obj2.tlf ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.fax,  IIf(IsNothing(obj2.fax),"",obj2.fax ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.IATA,  IIf(IsNothing(obj2.IATA),"",obj2.IATA ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.Direccion,  IIf(IsNothing(obj2.Direccion),"",obj2.Direccion ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.Cod_Postal,  IIf(IsNothing(obj2.Cod_Postal),"",obj2.Cod_Postal ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.CIF,  IIf(IsNothing(obj2.CIF),"",obj2.CIF ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.cod_ciudad,  obj2.cod_ciudad)
addSqlParameterSimple(cmd, T_Agencias.dbcol.VerTodos,  obj2.VerTodos)
addSqlParameterSimple(cmd, T_Agencias.dbcol.VerConDatos,  obj2.VerConDatos)
addSqlParameterSimple(cmd, T_Agencias.dbcol.VerConAcuerdo,  obj2.VerConAcuerdo)
addSqlParameterSimple(cmd, T_Agencias.dbcol.login,  IIf(IsNothing(obj2.login),"",obj2.login ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.clave,  IIf(IsNothing(obj2.clave),"",obj2.clave ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.director,  IIf(IsNothing(obj2.director),"",obj2.director ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.director_comercial,  IIf(IsNothing(obj2.director_comercial),"",obj2.director_comercial ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.nota_Reserva,  IIf(IsNothing(obj2.nota_Reserva),"",obj2.nota_Reserva ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.Cod_WEB,  obj2.Cod_WEB)
addSqlParameterSimple(cmd, T_Agencias.dbcol.email_reserva,  IIf(IsNothing(obj2.email_reserva),"",obj2.email_reserva ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.cod_agencia_gen,  obj2.cod_agencia_gen)
addSqlParameterSimple(cmd, T_Agencias.dbcol.tlf_reservas,  IIf(IsNothing(obj2.tlf_reservas),"",obj2.tlf_reservas ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.numtcredito,  IIf(IsNothing(obj2.numtcredito),"",obj2.numtcredito ))
addSqlParameterSimple(cmd, T_Agencias.dbcol.tituloagencia,  IIf(IsNothing(obj2.tituloagencia),"",obj2.tituloagencia ))
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
Public Function Delete(objID as integer, Optional ByRef trans As Data.IDbTransaction = Nothing) As Object
            Dim cmd As System.Data.SqlClient.SqlCommand = configureSqlCommand("T_Agencias_DeleteByID")

addSqlParameterSimple(cmd, T_Agencias.dbcol.id,  objID)
            Dim dt As DataTable = GetData(cmd:=cmd, trans:=trans)
            Return dt
        End Function
End  Class 

End Namespace 
