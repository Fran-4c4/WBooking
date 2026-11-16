
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
    Public Class R_BookStatus
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


        Private mstatusID As System.Int32
        <DBColumnName("statusID")>
        <DataMemberAttribute()>
        Public Overridable Property statusID() As System.Int32 Implements IBObase.ID
            Get
                Return mstatusID
            End Get
            Set(value As System.Int32)
                If mstatusID <> value Then
                    mstatusID = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDescription As System.String
        <DBColumnName("Description")>
        <DataMemberAttribute()>
        Public Overridable Property Description() As System.String
            Get
                Return mDescription
            End Get
            Set(value As System.String)
                If mDescription <> value Then
                    mDescription = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const statusID As String = "statusID" 'int
            Public Const Description As String = "Description" 'varchar
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_BookStatus) As R_BookStatus
            If obj2Fill Is Nothing Then
                obj2Fill = New R_BookStatus
            End If

            obj2Fill.statusID = IIf(IsDBNull(dr(DBCol.statusID)), Nothing, dr(DBCol.statusID))
            obj2Fill.Description = "" & dr(DBCol.Description)
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class R_BookStatusCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As R_BookStatus
            Get
                Return DirectCast(List(index), R_BookStatus)
            End Get
            Set(value As R_BookStatus)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As R_BookStatus)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As R_BookStatus)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
