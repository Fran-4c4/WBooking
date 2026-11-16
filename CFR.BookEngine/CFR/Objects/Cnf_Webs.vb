
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
    Public Class Cnf_Webs
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


        Private mWeb As System.String
        <DBColumnName("Web")>
        <DataMemberAttribute()>
        Public Overridable Property Web() As System.String
            Get
                Return mWeb
            End Get
            Set(value As System.String)
                If mWeb <> value Then
                    mWeb = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mIP As System.String
        <DBColumnName("IP")>
        <DataMemberAttribute()>
        Public Overridable Property IP() As System.String
            Get
                Return mIP
            End Get
            Set(value As System.String)
                If mIP <> value Then
                    mIP = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mArchivoCSS As System.String
        <DBColumnName("ArchivoCSS")>
        <DataMemberAttribute()>
        Public Overridable Property ArchivoCSS() As System.String
            Get
                Return mArchivoCSS
            End Get
            Set(value As System.String)
                If mArchivoCSS <> value Then
                    mArchivoCSS = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private midWeb As System.Int32
        <DBColumnName("idWeb")>
        <DataMemberAttribute()>
        Public Overridable Property idWeb() As System.Int32 Implements IBObase.ID
            Get
                Return midWeb
            End Get
            Set(value As System.Int32)
                If midWeb <> value Then
                    midWeb = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const Web As String = "Web" 'nvarchar
            Public Const IP As String = "IP" 'nvarchar
            Public Const ArchivoCSS As String = "ArchivoCSS" 'nvarchar
            Public Const idWeb As String = "idWeb" 'int
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As Cnf_Webs) As Cnf_Webs
            If obj2Fill Is Nothing Then
                obj2Fill = New Cnf_Webs
            End If

            obj2Fill.Web = "" & dr(DBCol.Web)
            obj2Fill.IP = "" & dr(DBCol.IP)
            obj2Fill.ArchivoCSS = "" & dr(DBCol.ArchivoCSS)
            obj2Fill.idWeb = IIf(IsDBNull(dr(DBCol.idWeb)), Nothing, dr(DBCol.idWeb))
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class Cnf_WebsCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As Cnf_Webs
            Get
                Return DirectCast(List(index), Cnf_Webs)
            End Get
            Set(value As Cnf_Webs)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As Cnf_Webs)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As Cnf_Webs)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
