
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
    Public Class R_Agencia_Empresa
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


        Private mcod_Agencia As System.Int32
        <DBColumnName("cod_Agencia")>
        <DataMemberAttribute()>
        Public Overridable Property cod_Agencia() As System.Int32
            Get
                Return mcod_Agencia
            End Get
            Set(value As System.Int32)
                If mcod_Agencia <> value Then
                    mcod_Agencia = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcod_Empresa As System.Int32
        <DBColumnName("cod_Empresa")>
        <DataMemberAttribute()>
        Public Overridable Property cod_Empresa() As System.Int32
            Get
                Return mcod_Empresa
            End Get
            Set(value As System.Int32)
                If mcod_Empresa <> value Then
                    mcod_Empresa = value
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

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const cod_Agencia As String = "cod_Agencia" 'int
            Public Const cod_Empresa As String = "cod_Empresa" 'int
            Public Const cod_hotel As String = "cod_hotel" 'int
            Public Const id As String = "id" 'int
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Agencia_Empresa) As R_Agencia_Empresa
            If obj2Fill Is Nothing Then
                obj2Fill = New R_Agencia_Empresa
            End If

            obj2Fill.cod_Agencia = IIf(IsDBNull(dr(DBCol.cod_Agencia)), Nothing, dr(DBCol.cod_Agencia))
            obj2Fill.cod_Empresa = IIf(IsDBNull(dr(DBCol.cod_Empresa)), Nothing, dr(DBCol.cod_Empresa))
            obj2Fill.cod_hotel = IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing, dr(DBCol.cod_hotel))
            obj2Fill.id = IIf(IsDBNull(dr(DBCol.id)), Nothing, dr(DBCol.id))
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class R_Agencia_EmpresaCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As R_Agencia_Empresa
            Get
                Return DirectCast(List(index), R_Agencia_Empresa)
            End Get
            Set(value As R_Agencia_Empresa)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As R_Agencia_Empresa)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As R_Agencia_Empresa)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
