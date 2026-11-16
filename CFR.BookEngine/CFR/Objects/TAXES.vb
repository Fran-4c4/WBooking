
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
    Public Class TAXES
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


        Private mTAXE_ID As System.Int32
        <DBColumnName("TAXE_ID")>
        <DataMemberAttribute()>
        Public Overridable Property TAXE_ID() As System.Int32 Implements IBObase.ID
            Get
                Return mTAXE_ID
            End Get
            Set(value As System.Int32)
                If mTAXE_ID <> value Then
                    mTAXE_ID = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mTAXE_Description As System.String
        <DBColumnName("TAXE_Description")>
        <DataMemberAttribute()>
        Public Overridable Property TAXE_Description() As System.String
            Get
                Return mTAXE_Description
            End Get
            Set(value As System.String)
                If mTAXE_Description <> value Then
                    mTAXE_Description = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mTAXE_Value As System.Double
        <DBColumnName("TAXE_Value")>
        <DataMemberAttribute()>
        Public Overridable Property TAXE_Value() As System.Double
            Get
                Return mTAXE_Value
            End Get
            Set(value As System.Double)
                If mTAXE_Value <> value Then
                    mTAXE_Value = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mTAXE_ts As System.Object
        <DBColumnName("TAXE_ts")>
        <DataMemberAttribute()>
        Public Overridable Property TAXE_ts() As System.Object
            Get
                Return mTAXE_ts
            End Get
            Set(value As System.Object)
                If mTAXE_ts <> value Then
                    mTAXE_ts = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mTAXE_state As System.Byte
        <DBColumnName("TAXE_state")>
        <DataMemberAttribute()>
        Public Overridable Property TAXE_state() As System.Byte
            Get
                Return mTAXE_state
            End Get
            Set(value As System.Byte)
                If mTAXE_state <> value Then
                    mTAXE_state = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mTAXE_ValidUntil As System.DateTime
        <DBColumnName("TAXE_ValidUntil")>
        <DataMemberAttribute()>
        Public Overridable Property TAXE_ValidUntil() As System.DateTime
            Get
                Return mTAXE_ValidUntil
            End Get
            Set(value As System.DateTime)
                If mTAXE_ValidUntil <> value Then
                    mTAXE_ValidUntil = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const TAXE_ID As String = "TAXE_ID" 'int
            Public Const TAXE_Description As String = "TAXE_Description" 'varchar
            Public Const TAXE_Value As String = "TAXE_Value" 'float
            Public Const TAXE_ts As String = "TAXE_ts" 'timestamp
            Public Const TAXE_state As String = "TAXE_state" 'tinyint
            Public Const TAXE_ValidUntil As String = "TAXE_ValidUntil" 'datetime
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As TAXES) As TAXES
            If obj2Fill Is Nothing Then
                obj2Fill = New TAXES
            End If

            obj2Fill.TAXE_ID = IIf(IsDBNull(dr(DBCol.TAXE_ID)), Nothing, dr(DBCol.TAXE_ID))
            obj2Fill.TAXE_Description = "" & dr(DBCol.TAXE_Description)
            obj2Fill.TAXE_Value = IIf(IsDBNull(dr(DBCol.TAXE_Value)), Nothing, dr(DBCol.TAXE_Value))
            obj2Fill.TAXE_state = IIf(IsDBNull(dr(DBCol.TAXE_state)), Nothing, dr(DBCol.TAXE_state))
            obj2Fill.TAXE_ValidUntil = IIf(IsDBNull(dr(DBCol.TAXE_ValidUntil)), Nothing, dr(DBCol.TAXE_ValidUntil))
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class TAXESCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As TAXES
            Get
                Return DirectCast(List(index), TAXES)
            End Get
            Set(value As TAXES)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As TAXES)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As TAXES)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
