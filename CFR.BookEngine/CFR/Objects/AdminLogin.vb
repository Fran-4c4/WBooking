
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
    Public Class AdminLogin
        Inherits objBaseClass
        Implements IBObase



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


        Private mid_login As System.Int32
        <DBColumnName("id_login")>
        <DataMemberAttribute()>
        Public Overridable Property id_login() As System.Int32 Implements IBObase.ID
            Get
                Return mid_login
            End Get
            Set(value As System.Int32)
                If mid_login <> value Then
                    mid_login = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mLogin As System.String
        <DBColumnName("Login")>
        <DataMemberAttribute()>
        Public Overridable Property Login() As System.String
            Get
                Return mLogin
            End Get
            Set(value As System.String)
                If mLogin <> value Then
                    mLogin = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mClave As System.String
        <DBColumnName("Clave")>
        <DataMemberAttribute()>
        Public Overridable Property Clave() As System.String
            Get
                Return mClave
            End Get
            Set(value As System.String)
                If mClave <> value Then
                    mClave = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mbloqueado As System.Byte
        <DBColumnName("bloqueado")>
        <DataMemberAttribute()>
        Public Overridable Property bloqueado() As System.Byte
            Get
                Return mbloqueado
            End Get
            Set(value As System.Byte)
                If mbloqueado <> value Then
                    mbloqueado = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mfecha As System.DateTime
        <DBColumnName("fecha")>
        <DataMemberAttribute()>
        Public Overridable Property fecha() As System.DateTime
            Get
                Return mfecha
            End Get
            Set(value As System.DateTime)
                If mfecha <> value Then
                    mfecha = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mPerfil As System.Byte
        <DBColumnName("Perfil")>
        <DataMemberAttribute()>
        Public Overridable Property Perfil() As System.Byte
            Get
                Return mPerfil
            End Get
            Set(value As System.Byte)
                If mPerfil <> value Then
                    mPerfil = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mUsuario As System.String
        <DBColumnName("Usuario")>
        <DataMemberAttribute()>
        Public Overridable Property Usuario() As System.String
            Get
                Return mUsuario
            End Get
            Set(value As System.String)
                If mUsuario <> value Then
                    mUsuario = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const id_login As String = "id_login" 'int
            Public Const Login As String = "Login" 'varchar
            Public Const Clave As String = "Clave" 'varchar
            Public Const bloqueado As String = "bloqueado" 'tinyint
            Public Const fecha As String = "fecha" 'datetime
            Public Const Perfil As String = "Perfil" 'tinyint
            Public Const Usuario As String = "Usuario" 'varchar
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As AdminLogin) As AdminLogin
            If obj2Fill Is Nothing Then
                obj2Fill = New AdminLogin
            End If

            obj2Fill.id_login = IIf(IsDBNull(dr(DBCol.id_login)), Nothing, dr(DBCol.id_login))
            obj2Fill.Login = "" & dr(DBCol.Login)
            obj2Fill.Clave = "" & dr(DBCol.Clave)
            obj2Fill.bloqueado = IIf(IsDBNull(dr(DBCol.bloqueado)), Nothing, dr(DBCol.bloqueado))
            obj2Fill.fecha = IIf(IsDBNull(dr(DBCol.fecha)), Nothing, dr(DBCol.fecha))
            obj2Fill.Perfil = IIf(IsDBNull(dr(DBCol.Perfil)), Nothing, dr(DBCol.Perfil))
            obj2Fill.Usuario = "" & dr(DBCol.Usuario)
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class AdminLoginCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As AdminLogin
            Get
                Return DirectCast(List(index), AdminLogin)
            End Get
            Set(value As AdminLogin)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As AdminLogin)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As AdminLogin)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
