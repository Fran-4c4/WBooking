
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
    Public Class Userlogaccess
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


        Private mid As System.Int32
        <DBColumnName("id")>
        <DataMemberAttribute()>
        Public Overridable Property id() As System.Int32 Implements IBObase.ID
            Get
                Return mid
            End Get
            Set(value As System.Int32)
                If mid <> value Then
                    mid = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private muserid As System.Int32
        <DBColumnName("userid")>
        <DataMemberAttribute()>
        Public Overridable Property userid() As System.Int32
            Get
                Return muserid
            End Get
            Set(value As System.Int32)
                If muserid <> value Then
                    muserid = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mlogon As System.DateTime
        <DBColumnName("logon")>
        <DataMemberAttribute()>
        Public Overridable Property logon() As System.DateTime
            Get
                Return mlogon
            End Get
            Set(value As System.DateTime)
                If mlogon <> value Then
                    mlogon = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mlogoff As System.DateTime
        <DBColumnName("logoff")>
        <DataMemberAttribute()>
        Public Overridable Property logoff() As System.DateTime
            Get
                Return mlogoff
            End Get
            Set(value As System.DateTime)
                If mlogoff <> value Then
                    mlogoff = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mipaccess As System.String
        <DBColumnName("ipaccess")>
        <DataMemberAttribute()>
        Public Overridable Property ipaccess() As System.String
            Get
                Return mipaccess
            End Get
            Set(value As System.String)
                If mipaccess <> value Then
                    mipaccess = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const id As String = "id" 'int
            Public Const userid As String = "userid" 'int
            Public Const logon As String = "logon" 'datetime
            Public Const logoff As String = "logoff" 'datetime
            Public Const ipaccess As String = "ipaccess" 'varchar
        End Class
#End Region


        Public Function FillByRow(ByRef dr As DataRow) As Userlogaccess

            Return FillObjectByRow(dr, Me)

        End Function


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, Optional ByRef obj2Fill As Userlogaccess = Nothing) As Userlogaccess
            If obj2Fill Is Nothing Then
                obj2Fill = New Userlogaccess
            End If

            obj2Fill.id = IIf(IsDBNull(dr(DBCol.id)), Nothing, dr(DBCol.id))
            obj2Fill.userid = IIf(IsDBNull(dr(DBCol.userid)), Nothing, dr(DBCol.userid))
            obj2Fill.logon = IIf(IsDBNull(dr(DBCol.logon)), Nothing, dr(DBCol.logon))
            obj2Fill.logoff = IIf(IsDBNull(dr(DBCol.logoff)), Nothing, dr(DBCol.logoff))
            obj2Fill.ipaccess = "" & dr(DBCol.ipaccess)
            Return obj2Fill
        End Function


    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class UserlogaccessCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As Userlogaccess
            Get
                Return DirectCast(List(index), Userlogaccess)
            End Get
            Set(value As Userlogaccess)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As Userlogaccess)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As Userlogaccess)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
