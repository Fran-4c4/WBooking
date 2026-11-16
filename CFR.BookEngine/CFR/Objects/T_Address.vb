
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
    Public Class T_Address
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


        Private mid_address As System.Int32
        <DBColumnName("id_address")>
        <DataMemberAttribute()>
        Public Overridable Property id_address() As System.Int32 Implements IBObase.ID
            Get
                Return mid_address
            End Get
            Set(value As System.Int32)
                If mid_address <> value Then
                    mid_address = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private maddress As System.String
        <DBColumnName("address")>
        <DataMemberAttribute()>
        Public Overridable Property address() As System.String
            Get
                Return maddress
            End Get
            Set(value As System.String)
                If maddress <> value Then
                    maddress = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private maddress1 As System.String
        <DBColumnName("address1")>
        <DataMemberAttribute()>
        Public Overridable Property address1() As System.String
            Get
                Return maddress1
            End Get
            Set(value As System.String)
                If maddress1 <> value Then
                    maddress1 = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcity As System.String
        <DBColumnName("city")>
        <DataMemberAttribute()>
        Public Overridable Property city() As System.String
            Get
                Return mcity
            End Get
            Set(value As System.String)
                If mcity <> value Then
                    mcity = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mpostalcode As System.String
        <DBColumnName("postalcode")>
        <DataMemberAttribute()>
        Public Overridable Property postalcode() As System.String
            Get
                Return mpostalcode
            End Get
            Set(value As System.String)
                If mpostalcode <> value Then
                    mpostalcode = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private msuburb As System.String
        <DBColumnName("suburb")>
        <DataMemberAttribute()>
        Public Overridable Property suburb() As System.String
            Get
                Return msuburb
            End Get
            Set(value As System.String)
                If msuburb <> value Then
                    msuburb = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mState As System.String
        <DBColumnName("State")>
        <DataMemberAttribute()>
        Public Overridable Property State() As System.String
            Get
                Return mState
            End Get
            Set(value As System.String)
                If mState <> value Then
                    mState = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcountry As System.String
        <DBColumnName("country")>
        <DataMemberAttribute()>
        Public Overridable Property country() As System.String
            Get
                Return mcountry
            End Get
            Set(value As System.String)
                If mcountry <> value Then
                    mcountry = value
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


        Private memail1 As System.String
        <DBColumnName("email1")>
        <DataMemberAttribute()>
        Public Overridable Property email1() As System.String
            Get
                Return memail1
            End Get
            Set(value As System.String)
                If memail1 <> value Then
                    memail1 = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mphone As System.String
        <DBColumnName("phone")>
        <DataMemberAttribute()>
        Public Overridable Property phone() As System.String
            Get
                Return mphone
            End Get
            Set(value As System.String)
                If mphone <> value Then
                    mphone = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mphone1 As System.String
        <DBColumnName("phone1")>
        <DataMemberAttribute()>
        Public Overridable Property phone1() As System.String
            Get
                Return mphone1
            End Get
            Set(value As System.String)
                If mphone1 <> value Then
                    mphone1 = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mfax As System.String
        <DBColumnName("fax")>
        <DataMemberAttribute()>
        Public Overridable Property fax() As System.String
            Get
                Return mfax
            End Get
            Set(value As System.String)
                If mfax <> value Then
                    mfax = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const id_address As String = "id_address" 'int
            Public Const address As String = "address" 'nvarchar
            Public Const address1 As String = "address1" 'nvarchar
            Public Const city As String = "city" 'nvarchar
            Public Const postalcode As String = "postalcode" 'nvarchar
            Public Const suburb As String = "suburb" 'nvarchar
            Public Const State As String = "State" 'nvarchar
            Public Const country As String = "country" 'nvarchar
            Public Const email As String = "email" 'nvarchar
            Public Const email1 As String = "email1" 'nvarchar
            Public Const phone As String = "phone" 'nvarchar
            Public Const phone1 As String = "phone1" 'nvarchar
            Public Const fax As String = "fax" 'nvarchar
        End Class
#End Region


        Public Function FillByRow(ByRef dr As DataRow) As T_Address

            Return FillObjectByRow(dr, Me)

        End Function

        Public Shared Function FillObjectByRowView(ByRef dr As DataRowView, Optional ByRef obj2Fill As T_Address = Nothing) As T_Address
            Return FillObjectByRow(dr.Row, obj2Fill)
        End Function




        Public Shared Function FillObjectByRow(ByRef dr As DataRow, Optional ByRef obj2Fill As T_Address = Nothing) As T_Address
            If obj2Fill Is Nothing Then
                obj2Fill = New T_Address
            End If

            obj2Fill.id_address = IIf(IsDBNull(dr(DBCol.id_address)), Nothing, dr(DBCol.id_address))
            obj2Fill.address = "" & dr(DBCol.address)
            obj2Fill.address1 = "" & dr(DBCol.address1)
            obj2Fill.city = "" & dr(DBCol.city)
            obj2Fill.postalcode = "" & dr(DBCol.postalcode)
            obj2Fill.suburb = "" & dr(DBCol.suburb)
            obj2Fill.State = "" & dr(DBCol.State)
            obj2Fill.country = "" & dr(DBCol.country)
            obj2Fill.email = "" & dr(DBCol.email)
            obj2Fill.email1 = "" & dr(DBCol.email1)
            obj2Fill.phone = "" & dr(DBCol.phone)
            obj2Fill.phone1 = "" & dr(DBCol.phone1)
            obj2Fill.fax = "" & dr(DBCol.fax)
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class T_AddressCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As T_Address
            Get
                Return DirectCast(List(index), T_Address)
            End Get
            Set(value As T_Address)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As T_Address)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As T_Address)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
