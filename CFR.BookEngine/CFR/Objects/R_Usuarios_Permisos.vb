
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
    Partial Public Class R_Usuarios_Permisos
        'Inherits objBaseClass
        'Implements IBObase



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
        Private mid_usuario As System.Int32
        <DBColumnName("id_usuario")>
        <DataMemberAttribute()>
        Public Overridable Property id_usuario() As System.Int32
            Get
                Return mid_usuario
            End Get
            Set(value As System.Int32)
                If mid_usuario <> value Then
                    mid_usuario = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcod_permiso As System.Byte
        <DBColumnName("cod_permiso")>
        <DataMemberAttribute()>
        Public Overridable Property cod_permiso() As System.Byte
            Get
                Return mcod_permiso
            End Get
            Set(value As System.Byte)
                If mcod_permiso <> value Then
                    mcod_permiso = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const id As String = "id" 'int
            Public Const id_usuario As String = "id_usuario" 'int
            Public Const cod_permiso As String = "cod_permiso" 'tinyint
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, Optional ByRef obj2Fill As R_Usuarios_Permisos = Nothing) As R_Usuarios_Permisos
            If obj2Fill Is Nothing Then
                obj2Fill = New R_Usuarios_Permisos
            End If
            obj2Fill.id_usuario = IIf(IsDBNull(dr(DBCol.id_usuario)), Nothing, dr(DBCol.id_usuario))
            obj2Fill.cod_permiso = IIf(IsDBNull(dr(DBCol.cod_permiso)), Nothing, dr(DBCol.cod_permiso))
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class R_Usuarios_PermisosCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As R_Usuarios_Permisos
            Get
                Return DirectCast(List(index), R_Usuarios_Permisos)
            End Get
            Set(value As R_Usuarios_Permisos)
                List(index) = value
            End Set
        End Property

        Public Shared Function getCollectionByTable(dt As DataTable) As R_Usuarios_PermisosCollection
            Dim col As New R_Usuarios_PermisosCollection
            For Each r As DataRow In dt.Rows

                col.Add(R_Usuarios_Permisos.FillObjectByRow(r))
            Next
            Return col
        End Function

        Public Overridable Sub Add(ByVal Item As R_Usuarios_Permisos)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As R_Usuarios_Permisos)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
