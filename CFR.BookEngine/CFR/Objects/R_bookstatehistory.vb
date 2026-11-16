
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
    Public Class R_bookstatehistory
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


        Private mcod_reserva As System.Int32
        <DBColumnName("cod_reserva")>
        <DataMemberAttribute()>
        Public Overridable Property cod_reserva() As System.Int32
            Get
                Return mcod_reserva
            End Get
            Set(value As System.Int32)
                If mcod_reserva <> value Then
                    mcod_reserva = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mbookstate As System.Byte
        <DBColumnName("bookstate")>
        <DataMemberAttribute()>
        Public Overridable Property bookstate() As System.Byte
            Get
                Return mbookstate
            End Get
            Set(value As System.Byte)
                If mbookstate <> value Then
                    mbookstate = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mmodifyDate As System.DateTime
        <DBColumnName("modifyDate")>
        <DataMemberAttribute()>
        Public Overridable Property modifyDate() As System.DateTime
            Get
                Return mmodifyDate
            End Get
            Set(value As System.DateTime)
                If mmodifyDate <> value Then
                    mmodifyDate = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private musername As String
        <DBColumnName("username")>
        <DataMemberAttribute()>
        Public Overridable Property username() As String
            Get
                Return musername
            End Get
            Set(value As String)
                If musername <> value Then
                    musername = value
                    Me.objChanged = True
                End If
            End Set
        End Property

        Private mforunit As System.Int32
        <DBColumnName("forunit")>
        <DataMemberAttribute()>
        Public Overridable Property forunit() As System.Int32
            Get
                Return mforunit
            End Get
            Set(value As System.Int32)
                If mforunit <> value Then
                    mforunit = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mpricereal As System.Double
        <DBColumnName("pricereal")>
        <DataMemberAttribute()>
        Public Overridable Property pricereal() As System.Double
            Get
                Return mpricereal
            End Get
            Set(value As System.Double)
                If mpricereal <> value Then
                    mpricereal = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const id As String = "id" 'int
            Public Const cod_reserva As String = "cod_reserva" 'int
            Public Const bookstate As String = "bookstate" 'tinyint
            Public Const modifyDate As String = "modifyDate" 'datetime
            Public Const username As String = "username" 'int
            Public Const pricereal As String = "pricereal" 'float
            Public Const forUnit As String = "forunit" 'int
        End Class
#End Region


        Public Function FillByRow(ByRef dr As DataRow) As R_bookstatehistory

            Return FillObjectByRow(dr, Me)

        End Function


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, Optional ByRef obj2Fill As R_bookstatehistory = Nothing) As R_bookstatehistory
            If obj2Fill Is Nothing Then
                obj2Fill = New R_bookstatehistory
            End If

            obj2Fill.id = IIf(IsDBNull(dr(DBCol.id)), Nothing, dr(DBCol.id))
            obj2Fill.cod_reserva = IIf(IsDBNull(dr(DBCol.cod_reserva)), Nothing, dr(DBCol.cod_reserva))
            obj2Fill.bookstate = IIf(IsDBNull(dr(DBCol.bookstate)), Nothing, dr(DBCol.bookstate))
            obj2Fill.modifyDate = IIf(IsDBNull(dr(DBCol.modifyDate)), Nothing, dr(DBCol.modifyDate))
            obj2Fill.username = IIf(IsDBNull(dr(DBCol.username)), Nothing, dr(DBCol.username))
            obj2Fill.pricereal = IIf(IsDBNull(dr(DBCol.pricereal)), Nothing, dr(DBCol.pricereal))
            obj2Fill.forunit = IIf(IsDBNull(dr(DBCol.forUnit)), Nothing, dr(DBCol.forUnit))
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class R_bookstatehistoryCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As R_bookstatehistory
            Get
                Return DirectCast(List(index), R_bookstatehistory)
            End Get
            Set(value As R_bookstatehistory)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As R_bookstatehistory)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As R_bookstatehistory)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
