
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
    Public Class R_TPVDATA
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


        Private mDs_Merchant_MerchantCode As System.String
        <DBColumnName("Ds_Merchant_MerchantCode")>
        <DataMemberAttribute()>
        Public Overridable Property Ds_Merchant_MerchantCode() As System.String
            Get
                Return mDs_Merchant_MerchantCode
            End Get
            Set(value As System.String)
                If mDs_Merchant_MerchantCode <> value Then
                    mDs_Merchant_MerchantCode = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDs_Merchant_Terminal As System.String
        <DBColumnName("Ds_Merchant_Terminal")>
        <DataMemberAttribute()>
        Public Overridable Property Ds_Merchant_Terminal() As System.String
            Get
                Return mDs_Merchant_Terminal
            End Get
            Set(value As System.String)
                If mDs_Merchant_Terminal <> value Then
                    mDs_Merchant_Terminal = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDs_Merchant_Password As System.String
        <DBColumnName("Ds_Merchant_Password")>
        <DataMemberAttribute()>
        Public Overridable Property Ds_Merchant_Password() As System.String
            Get
                Return mDs_Merchant_Password
            End Get
            Set(value As System.String)
                If mDs_Merchant_Password <> value Then
                    mDs_Merchant_Password = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDs_Merchant_Currency As System.String
        <DBColumnName("Ds_Merchant_Currency")>
        <DataMemberAttribute()>
        Public Overridable Property Ds_Merchant_Currency() As System.String
            Get
                Return mDs_Merchant_Currency
            End Get
            Set(value As System.String)
                If mDs_Merchant_Currency <> value Then
                    mDs_Merchant_Currency = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDs_Merchant_ConsumerLanguage As System.String
        <DBColumnName("Ds_Merchant_ConsumerLanguage")>
        <DataMemberAttribute()>
        Public Overridable Property Ds_Merchant_ConsumerLanguage() As System.String
            Get
                Return mDs_Merchant_ConsumerLanguage
            End Get
            Set(value As System.String)
                If mDs_Merchant_ConsumerLanguage <> value Then
                    mDs_Merchant_ConsumerLanguage = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDs_Merchant_TransactionType As System.String
        <DBColumnName("Ds_Merchant_TransactionType")>
        <DataMemberAttribute()>
        Public Overridable Property Ds_Merchant_TransactionType() As System.String
            Get
                Return mDs_Merchant_TransactionType
            End Get
            Set(value As System.String)
                If mDs_Merchant_TransactionType <> value Then
                    mDs_Merchant_TransactionType = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const ID As String = "ID" 'int
            Public Const cod_hotel As String = "cod_hotel" 'int
            Public Const Ds_Merchant_MerchantCode As String = "Ds_Merchant_MerchantCode" 'varchar
            Public Const Ds_Merchant_Terminal As String = "Ds_Merchant_Terminal" 'varchar
            Public Const Ds_Merchant_Password As String = "Ds_Merchant_Password" 'varchar
            Public Const Ds_Merchant_Currency As String = "Ds_Merchant_Currency" 'varchar
            Public Const Ds_Merchant_ConsumerLanguage As String = "Ds_Merchant_ConsumerLanguage" 'varchar
            Public Const Ds_Merchant_TransactionType As String = "Ds_Merchant_TransactionType" 'varchar
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_TPVDATA) As R_TPVDATA
            If obj2Fill Is Nothing Then
                obj2Fill = New R_TPVDATA
            End If

            obj2Fill.ID = IIf(IsDBNull(dr(DBCol.ID)), Nothing, dr(DBCol.ID))
            obj2Fill.cod_hotel = IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing, dr(DBCol.cod_hotel))
            obj2Fill.Ds_Merchant_MerchantCode = "" & dr(DBCol.Ds_Merchant_MerchantCode)
            obj2Fill.Ds_Merchant_Terminal = "" & dr(DBCol.Ds_Merchant_Terminal)
            obj2Fill.Ds_Merchant_Password = "" & dr(DBCol.Ds_Merchant_Password)
            obj2Fill.Ds_Merchant_Currency = "" & dr(DBCol.Ds_Merchant_Currency)
            obj2Fill.Ds_Merchant_ConsumerLanguage = "" & dr(DBCol.Ds_Merchant_ConsumerLanguage)
            obj2Fill.Ds_Merchant_TransactionType = "" & dr(DBCol.Ds_Merchant_TransactionType)
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class R_TPVDATACollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As R_TPVDATA
            Get
                Return DirectCast(List(index), R_TPVDATA)
            End Get
            Set(value As R_TPVDATA)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As R_TPVDATA)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As R_TPVDATA)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
