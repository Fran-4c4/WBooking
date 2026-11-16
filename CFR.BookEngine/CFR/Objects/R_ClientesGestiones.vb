
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
    Public Class R_ClientesGestiones
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


        Private mid_Gestion As System.Int32
        <DBColumnName("id_Gestion")>
        <DataMemberAttribute()>
        Public Overridable Property id_Gestion() As System.Int32 Implements IBObase.ID
            Get
                Return mid_Gestion
            End Get
            Set(value As System.Int32)
                If mid_Gestion <> value Then
                    mid_Gestion = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mid_Cliente As System.Int32
        <DBColumnName("id_Cliente")>
        <DataMemberAttribute()>
        Public Overridable Property id_Cliente() As System.Int32
            Get
                Return mid_Cliente
            End Get
            Set(value As System.Int32)
                If mid_Cliente <> value Then
                    mid_Cliente = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mid_tipoContacto As System.Int32
        <DBColumnName("id_tipoContacto")>
        <DataMemberAttribute()>
        Public Overridable Property id_tipoContacto() As System.Int32
            Get
                Return mid_tipoContacto
            End Get
            Set(value As System.Int32)
                If mid_tipoContacto <> value Then
                    mid_tipoContacto = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mFechaContacto As System.DateTime
        <DBColumnName("FechaContacto")>
        <DataMemberAttribute()>
        Public Overridable Property FechaContacto() As System.DateTime
            Get
                Return mFechaContacto
            End Get
            Set(value As System.DateTime)
                If mFechaContacto <> value Then
                    mFechaContacto = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mAsunto As System.String
        <DBColumnName("Asunto")>
        <DataMemberAttribute()>
        Public Overridable Property Asunto() As System.String
            Get
                Return mAsunto
            End Get
            Set(value As System.String)
                If mAsunto <> value Then
                    mAsunto = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mNotas As System.String
        <DBColumnName("Notas")>
        <DataMemberAttribute()>
        Public Overridable Property Notas() As System.String
            Get
                Return mNotas
            End Get
            Set(value As System.String)
                If mNotas <> value Then
                    mNotas = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mResultado As System.String
        <DBColumnName("Resultado")>
        <DataMemberAttribute()>
        Public Overridable Property Resultado() As System.String
            Get
                Return mResultado
            End Get
            Set(value As System.String)
                If mResultado <> value Then
                    mResultado = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mFechaModificacion As System.DateTime
        <DBColumnName("FechaModificacion")>
        <DataMemberAttribute()>
        Public Overridable Property FechaModificacion() As System.DateTime
            Get
                Return mFechaModificacion
            End Get
            Set(value As System.DateTime)
                If mFechaModificacion <> value Then
                    mFechaModificacion = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mEstado As System.Int32
        <DBColumnName("Estado")>
        <DataMemberAttribute()>
        Public Overridable Property Estado() As System.Int32
            Get
                Return mEstado
            End Get
            Set(value As System.Int32)
                If mEstado <> value Then
                    mEstado = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const id_Gestion As String = "id_Gestion" 'int
            Public Const id_Cliente As String = "id_Cliente" 'int
            Public Const id_tipoContacto As String = "id_tipoContacto" 'int
            Public Const FechaContacto As String = "FechaContacto" 'datetime
            Public Const Asunto As String = "Asunto" 'varchar
            Public Const Notas As String = "Notas" 'varchar
            Public Const Resultado As String = "Resultado" 'varchar
            Public Const FechaModificacion As String = "FechaModificacion" 'datetime
            Public Const Estado As String = "Estado" 'int
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_ClientesGestiones) As R_ClientesGestiones
            If obj2Fill Is Nothing Then
                obj2Fill = New R_ClientesGestiones
            End If

            obj2Fill.id_Gestion = IIf(IsDBNull(dr(DBCol.id_Gestion)), Nothing, dr(DBCol.id_Gestion))
            obj2Fill.id_Cliente = IIf(IsDBNull(dr(DBCol.id_Cliente)), Nothing, dr(DBCol.id_Cliente))
            obj2Fill.id_tipoContacto = IIf(IsDBNull(dr(DBCol.id_tipoContacto)), Nothing, dr(DBCol.id_tipoContacto))
            obj2Fill.FechaContacto = IIf(IsDBNull(dr(DBCol.FechaContacto)), Nothing, dr(DBCol.FechaContacto))
            obj2Fill.Asunto = "" & dr(DBCol.Asunto)
            obj2Fill.Notas = "" & dr(DBCol.Notas)
            obj2Fill.Resultado = "" & dr(DBCol.Resultado)
            obj2Fill.FechaModificacion = IIf(IsDBNull(dr(DBCol.FechaModificacion)), Nothing, dr(DBCol.FechaModificacion))
            obj2Fill.Estado = IIf(IsDBNull(dr(DBCol.Estado)), Nothing, dr(DBCol.Estado))
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class R_ClientesGestionesCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As R_ClientesGestiones
            Get
                Return DirectCast(List(index), R_ClientesGestiones)
            End Get
            Set(value As R_ClientesGestiones)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As R_ClientesGestiones)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As R_ClientesGestiones)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
