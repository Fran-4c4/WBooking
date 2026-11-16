
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
    Public Class T_CADENAS
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


        Private mID As System.Int32
        <DBColumnName("ID")>
        <DataMemberAttribute()>
        Public Overridable Property ID() As System.Int32 Implements IBObase.ID
            Get
                Return mID
            End Get
            Set(value As System.Int32)
                If mID <> value Then
                    mID = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcod_Cadena As System.Int16
        <DBColumnName("cod_Cadena")>
        <DataMemberAttribute()>
        Public Overridable Property cod_Cadena() As System.Int16
            Get
                Return mcod_Cadena
            End Get
            Set(value As System.Int16)
                If mcod_Cadena <> value Then
                    mcod_Cadena = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mCadena As System.String
        <DBColumnName("Cadena")>
        <DataMemberAttribute()>
        Public Overridable Property Cadena() As System.String
            Get
                Return mCadena
            End Get
            Set(value As System.String)
                If mCadena <> value Then
                    mCadena = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mCadenaIngles As System.String
        <DBColumnName("CadenaIngles")>
        <DataMemberAttribute()>
        Public Overridable Property CadenaIngles() As System.String
            Get
                Return mCadenaIngles
            End Get
            Set(value As System.String)
                If mCadenaIngles <> value Then
                    mCadenaIngles = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mCadenaAleman As System.String
        <DBColumnName("CadenaAleman")>
        <DataMemberAttribute()>
        Public Overridable Property CadenaAleman() As System.String
            Get
                Return mCadenaAleman
            End Get
            Set(value As System.String)
                If mCadenaAleman <> value Then
                    mCadenaAleman = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mCadenaFrances As System.String
        <DBColumnName("CadenaFrances")>
        <DataMemberAttribute()>
        Public Overridable Property CadenaFrances() As System.String
            Get
                Return mCadenaFrances
            End Get
            Set(value As System.String)
                If mCadenaFrances <> value Then
                    mCadenaFrances = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mCadenaCatalan As System.String
        <DBColumnName("CadenaCatalan")>
        <DataMemberAttribute()>
        Public Overridable Property CadenaCatalan() As System.String
            Get
                Return mCadenaCatalan
            End Get
            Set(value As System.String)
                If mCadenaCatalan <> value Then
                    mCadenaCatalan = value
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

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const ID As String = "ID" 'int
            Public Const cod_Cadena As String = "cod_Cadena" 'smallint
            Public Const Cadena As String = "Cadena" 'nvarchar
            Public Const CadenaIngles As String = "CadenaIngles" 'nvarchar
            Public Const CadenaAleman As String = "CadenaAleman" 'nvarchar
            Public Const CadenaFrances As String = "CadenaFrances" 'nvarchar
            Public Const CadenaCatalan As String = "CadenaCatalan" 'nvarchar
            Public Const login As String = "login" 'varchar
            Public Const Clave As String = "Clave" 'varchar
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_CADENAS) As T_CADENAS
            If obj2Fill Is Nothing Then
                obj2Fill = New T_CADENAS
            End If

            obj2Fill.ID = IIf(IsDBNull(dr(DBCol.ID)), Nothing, dr(DBCol.ID))
            obj2Fill.cod_Cadena = IIf(IsDBNull(dr(DBCol.cod_Cadena)), Nothing, dr(DBCol.cod_Cadena))
            obj2Fill.Cadena = "" & dr(DBCol.Cadena)
            obj2Fill.CadenaIngles = "" & dr(DBCol.CadenaIngles)
            obj2Fill.CadenaAleman = "" & dr(DBCol.CadenaAleman)
            obj2Fill.CadenaFrances = "" & dr(DBCol.CadenaFrances)
            obj2Fill.CadenaCatalan = "" & dr(DBCol.CadenaCatalan)
            obj2Fill.login = "" & dr(DBCol.login)
            obj2Fill.Clave = "" & dr(DBCol.Clave)
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class T_CADENASCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As T_CADENAS
            Get
                Return DirectCast(List(index), T_CADENAS)
            End Get
            Set(value As T_CADENAS)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As T_CADENAS)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As T_CADENAS)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
