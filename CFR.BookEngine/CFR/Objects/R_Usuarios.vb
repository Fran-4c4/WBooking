
Imports System
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports System.Runtime.InteropServices


Imports cfr

Namespace ind.bk.obj

    <Serializable()>
    <DataContractAttribute(IsReference:=True)>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Partial Public Class R_Usuarios
        'Inherits ObjBaseClass
        Implements IBObase
        Implements IIDObject
        Public Property idEntity() As Object Implements IIDObject.idEntity
            Get
                Return Me.id_usuario
            End Get
            Set(ByVal value As Object)
                Me.id_usuario = value
            End Set
        End Property

        ''' <summary>
        ''' A creatable COM class must have a Public Sub New() 
        ''' with no parameters, otherwise, the class will not be 
        ''' registered in the COM registry and cannot be created 
        ''' via CreateObject.
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub

#Region "Properties and Fields"
        Private mcod_hotel As System.Int32
        <DBColumnName("cod_hotel")>
        <DataMemberAttribute()>
        Public Overridable Property cod_hotel() As System.Int32
            Get
                Return mcod_hotel
            End Get
            Set(value As System.Int32)
                If mcod_hotel <> value Then
                    mcod_hotel = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcod_CRS As System.Int16
        <DBColumnName("cod_CRS")>
        <DataMemberAttribute()>
        Public Overridable Property cod_CRS() As System.Int16
            Get
                Return mcod_CRS
            End Get
            Set(value As System.Int16)
                If mcod_CRS <> value Then
                    mcod_CRS = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private musuario As System.String
        <DBColumnName("usuario")>
        <DataMemberAttribute()>
        Public Overridable Property usuario() As System.String
            Get
                Return musuario
            End Get
            Set(value As System.String)
                If musuario <> value Then
                    musuario = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mlogin As System.String
        <DBColumnName("login")>
        <DataMemberAttribute()>
        Public Overridable Property login() As System.String
            Get
                Return mlogin
            End Get
            Set(value As System.String)
                If mlogin <> value Then
                    mlogin = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mclave As System.String
        <DBColumnName("clave")>
        <DataMemberAttribute()>
        Public Overridable Property clave() As System.String
            Get
                Return mclave
            End Get
            Set(value As System.String)
                If mclave <> value Then
                    mclave = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mperfil As System.String
        <DBColumnName("perfil")>
        <DataMemberAttribute()>
        Public Overridable Property perfil() As System.String
            Get
                Return mperfil
            End Get
            Set(value As System.String)
                If mperfil <> value Then
                    mperfil = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private memail As System.String
        <DBColumnName("email")>
        <DataMemberAttribute()>
        Public Overridable Property email() As System.String
            Get
                Return memail
            End Get
            Set(value As System.String)
                If memail <> value Then
                    memail = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mid_usuario As System.Int32
        <DBColumnName("id_usuario")>
        <DataMemberAttribute()>
        Public Overridable Property id_usuario() As System.Int32 Implements IBObase.ID
            Get
                Return mid_usuario
            End Get
            Set(value As System.Int32)
                If mid_usuario <> value Then
                    mid_usuario = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private multimoacceso As System.DateTime
        <DBColumnName("ultimoacceso")>
        <DataMemberAttribute()>
        Public Overridable Property ultimoacceso() As System.DateTime
            Get
                Return multimoacceso
            End Get
            Set(value As System.DateTime)
                If multimoacceso <> value Then
                    multimoacceso = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol
            Public Const cod_hotel As String = "cod_hotel" 'int
            Public Const cod_CRS As String = "cod_CRS" 'smallint
            Public Const usuario As String = "usuario" 'varchar
            Public Const login As String = "login" 'varchar
            Public Const clave As String = "clave" 'varchar
            Public Const perfil As String = "perfil" 'varchar
            Public Const email As String = "email" 'varchar
            Public Const id_usuario As String = "id_usuario" 'int
            Public Const ultimoacceso As String = "ultimoacceso" 'datetime
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Usuarios) As R_Usuarios
            If obj2Fill Is Nothing Then
                obj2Fill = New R_Usuarios
            End If
            obj2Fill.cod_hotel = IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing, dr(DBCol.cod_hotel))
            obj2Fill.cod_CRS = IIf(IsDBNull(dr(DBCol.cod_CRS)), Nothing, dr(DBCol.cod_CRS))
            obj2Fill.usuario = "" & dr(DBCol.usuario)
            obj2Fill.login = "" & dr(DBCol.login)
            obj2Fill.clave = "" & dr(DBCol.clave)
            obj2Fill.perfil = "" & dr(DBCol.perfil)
            obj2Fill.email = "" & dr(DBCol.email)
            obj2Fill.id_usuario = IIf(IsDBNull(dr(DBCol.id_usuario)), Nothing, dr(DBCol.id_usuario))
            obj2Fill.ultimoacceso = IIf(IsDBNull(dr(DBCol.ultimoacceso)), Nothing, dr(DBCol.ultimoacceso))
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class R_UsuariosCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As R_Usuarios
            Get
                Return DirectCast(List(index), R_Usuarios)
            End Get
            Set(value As R_Usuarios)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As R_Usuarios)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As R_Usuarios)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
