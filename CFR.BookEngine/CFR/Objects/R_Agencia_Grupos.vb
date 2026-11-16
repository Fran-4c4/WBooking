
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
    Public Class R_Agencia_Grupos
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


        Private mcod_agencia As System.Int32
        <DBColumnName("cod_agencia")>
        <DataMemberAttribute()>
        Public Overridable Property cod_agencia() As System.Int32
            Get
                Return mcod_agencia
            End Get
            Set(value As System.Int32)
                If mcod_agencia <> value Then
                    mcod_agencia = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mcod_grupo As System.Int32
        <DBColumnName("cod_grupo")>
        <DataMemberAttribute()>
        Public Overridable Property cod_grupo() As System.Int32
            Get
                Return mcod_grupo
            End Get
            Set(value As System.Int32)
                If mcod_grupo <> value Then
                    mcod_grupo = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mprioridad As System.Byte
        <DBColumnName("prioridad")>
        <DataMemberAttribute()>
        Public Overridable Property prioridad() As System.Byte
            Get
                Return mprioridad
            End Get
            Set(value As System.Byte)
                If mprioridad <> value Then
                    mprioridad = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const id As String = "id" 'int
            Public Const cod_agencia As String = "cod_agencia" 'int
            Public Const cod_grupo As String = "cod_grupo" 'int
            Public Const prioridad As String = "prioridad" 'tinyint
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Agencia_Grupos) As R_Agencia_Grupos
            If obj2Fill Is Nothing Then
                obj2Fill = New R_Agencia_Grupos
            End If

            obj2Fill.id = IIf(IsDBNull(dr(DBCol.id)), Nothing, dr(DBCol.id))
            obj2Fill.cod_agencia = IIf(IsDBNull(dr(DBCol.cod_agencia)), Nothing, dr(DBCol.cod_agencia))
            obj2Fill.cod_grupo = IIf(IsDBNull(dr(DBCol.cod_grupo)), Nothing, dr(DBCol.cod_grupo))
            obj2Fill.prioridad = IIf(IsDBNull(dr(DBCol.prioridad)), Nothing, dr(DBCol.prioridad))
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class R_Agencia_GruposCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As R_Agencia_Grupos
            Get
                Return DirectCast(List(index), R_Agencia_Grupos)
            End Get
            Set(value As R_Agencia_Grupos)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As R_Agencia_Grupos)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As R_Agencia_Grupos)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
