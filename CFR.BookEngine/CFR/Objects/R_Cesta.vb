
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
    Public Class R_Cesta
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


        Private msessionID As System.String
        <DBColumnName("sessionID")>
        <DataMemberAttribute()>
        Public Overridable Property sessionID() As System.String
            Get
                Return msessionID
            End Get
            Set(value As System.String)
                If msessionID <> value Then
                    msessionID = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mdata As System.String
        <DBColumnName("data")>
        <DataMemberAttribute()>
        Public Overridable Property data() As System.String
            Get
                Return mdata
            End Get
            Set(value As System.String)
                If mdata <> value Then
                    mdata = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private msessiondate As System.DateTime
        <DBColumnName("sessiondate")>
        <DataMemberAttribute()>
        Public Overridable Property sessiondate() As System.DateTime
            Get
                Return msessiondate
            End Get
            Set(value As System.DateTime)
                If msessiondate <> value Then
                    msessiondate = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mtpvData As System.String
        <DBColumnName("tpvData")>
        <DataMemberAttribute()>
        Public Overridable Property tpvData() As System.String
            Get
                Return mtpvData
            End Get
            Set(value As System.String)
                If mtpvData <> value Then
                    mtpvData = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mSignature As System.String
        <DBColumnName("Signature")>
        <DataMemberAttribute()>
        Public Overridable Property Signature() As System.String
            Get
                Return mSignature
            End Get
            Set(value As System.String)
                If mSignature <> value Then
                    mSignature = value
                    Me.objChanged = True
                End If
            End Set
        End Property


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


        Private mtpvErrorReturned As System.String
        <DBColumnName("tpvErrorReturned")>
        <DataMemberAttribute()>
        Public Overridable Property tpvErrorReturned() As System.String
            Get
                Return mtpvErrorReturned
            End Get
            Set(value As System.String)
                If mtpvErrorReturned <> value Then
                    mtpvErrorReturned = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const id As String = "id" 'int
            Public Const sessionID As String = "sessionID" 'varchar
            Public Const data As String = "data" 'text
            Public Const sessiondate As String = "sessiondate" 'datetime
            Public Const tpvData As String = "tpvData" 'text
            Public Const Signature As String = "Signature" 'varchar
            Public Const cod_hotel As String = "cod_hotel" 'int
            Public Const tpvErrorReturned As String = "tpvErrorReturned" 'varchar
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Cesta) As R_Cesta
            If obj2Fill Is Nothing Then
                obj2Fill = New R_Cesta
            End If

            obj2Fill.id = IIf(IsDBNull(dr(DBCol.id)), Nothing, dr(DBCol.id))
            obj2Fill.sessionID = "" & dr(DBCol.sessionID)
            obj2Fill.data = IIf(IsDBNull(dr(DBCol.data)), Nothing, dr(DBCol.data))
            obj2Fill.sessiondate = IIf(IsDBNull(dr(DBCol.sessiondate)), Nothing, dr(DBCol.sessiondate))
            obj2Fill.tpvData = IIf(IsDBNull(dr(DBCol.tpvData)), Nothing, dr(DBCol.tpvData))
            obj2Fill.Signature = "" & dr(DBCol.Signature)
            obj2Fill.cod_hotel = IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing, dr(DBCol.cod_hotel))
            obj2Fill.tpvErrorReturned = "" & dr(DBCol.tpvErrorReturned)
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class R_CestaCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As R_Cesta
            Get
                Return DirectCast(List(index), R_Cesta)
            End Get
            Set(value As R_Cesta)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As R_Cesta)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As R_Cesta)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
