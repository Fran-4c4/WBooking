
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
    Public Class T_SITUACIONES
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


        Private mcod_situacion As System.Int32
        <DBColumnName("cod_situacion")>
        <DataMemberAttribute()>
        Public Overridable Property cod_situacion() As System.Int32 Implements IBObase.ID
            Get
                Return mcod_situacion
            End Get
            Set(value As System.Int32)
                If mcod_situacion <> value Then
                    mcod_situacion = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private msituacion As System.String
        <DBColumnName("situacion")>
        <DataMemberAttribute()>
        Public Overridable Property situacion() As System.String
            Get
                Return msituacion
            End Get
            Set(value As System.String)
                If msituacion <> value Then
                    msituacion = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mIngles As System.String
        <DBColumnName("Ingles")>
        <DataMemberAttribute()>
        Public Overridable Property Ingles() As System.String
            Get
                Return mIngles
            End Get
            Set(value As System.String)
                If mIngles <> value Then
                    mIngles = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mAleman As System.String
        <DBColumnName("Aleman")>
        <DataMemberAttribute()>
        Public Overridable Property Aleman() As System.String
            Get
                Return mAleman
            End Get
            Set(value As System.String)
                If mAleman <> value Then
                    mAleman = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mFrances As System.String
        <DBColumnName("Frances")>
        <DataMemberAttribute()>
        Public Overridable Property Frances() As System.String
            Get
                Return mFrances
            End Get
            Set(value As System.String)
                If mFrances <> value Then
                    mFrances = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mCatalan As System.String
        <DBColumnName("Catalan")>
        <DataMemberAttribute()>
        Public Overridable Property Catalan() As System.String
            Get
                Return mCatalan
            End Get
            Set(value As System.String)
                If mCatalan <> value Then
                    mCatalan = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mBuscar As System.Boolean
        <DBColumnName("Buscar")>
        <DataMemberAttribute()>
        Public Overridable Property Buscar() As System.Boolean
            Get
                Return mBuscar
            End Get
            Set(value As System.Boolean)
                If mBuscar <> value Then
                    mBuscar = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const cod_situacion As String = "cod_situacion" 'int
            Public Const situacion As String = "situacion" 'nvarchar
            Public Const Ingles As String = "Ingles" 'nvarchar
            Public Const Aleman As String = "Aleman" 'nvarchar
            Public Const Frances As String = "Frances" 'nvarchar
            Public Const Catalan As String = "Catalan" 'nvarchar
            Public Const Buscar As String = "Buscar" 'bit
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_SITUACIONES) As T_SITUACIONES
            If obj2Fill Is Nothing Then
                obj2Fill = New T_SITUACIONES
            End If

            obj2Fill.cod_situacion = IIf(IsDBNull(dr(DBCol.cod_situacion)), Nothing, dr(DBCol.cod_situacion))
            obj2Fill.situacion = "" & dr(DBCol.situacion)
            obj2Fill.Ingles = "" & dr(DBCol.Ingles)
            obj2Fill.Aleman = "" & dr(DBCol.Aleman)
            obj2Fill.Frances = "" & dr(DBCol.Frances)
            obj2Fill.Catalan = "" & dr(DBCol.Catalan)
            obj2Fill.Buscar = IIf(IsDBNull(dr(DBCol.Buscar)), Nothing, dr(DBCol.Buscar))
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class T_SITUACIONESCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As T_SITUACIONES
            Get
                Return DirectCast(List(index), T_SITUACIONES)
            End Get
            Set(value As T_SITUACIONES)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As T_SITUACIONES)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As T_SITUACIONES)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
