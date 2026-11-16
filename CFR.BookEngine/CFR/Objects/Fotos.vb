
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
    Public Class Fotos
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


        Private mid_Foto As System.Int32
        <DBColumnName("id_Foto")>
        <DataMemberAttribute()>
        Public Overridable Property id_Foto() As System.Int32
            Get
                Return mid_Foto
            End Get
            Set(value As System.Int32)
                If mid_Foto <> value Then
                    mid_Foto = value
                    Me.objChanged = True
                End If
            End Set
        End Property


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


        Private mFotoFileName As System.String
        <DBColumnName("FotoFileName")>
        <DataMemberAttribute()>
        Public Overridable Property FotoFileName() As System.String
            Get
                Return mFotoFileName
            End Get
            Set(value As System.String)
                If mFotoFileName <> value Then
                    mFotoFileName = value
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


        Private mid_fotoType As System.Int32
        <DBColumnName("id_fotoType")>
        <DataMemberAttribute()>
        Public Overridable Property id_fotoType() As System.Int32
            Get
                Return mid_fotoType
            End Get
            Set(value As System.Int32)
                If mid_fotoType <> value Then
                    mid_fotoType = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mlastUpdate As System.DateTime
        <DBColumnName("lastUpdate")>
        <DataMemberAttribute()>
        Public Overridable Property lastUpdate() As System.DateTime
            Get
                Return mlastUpdate
            End Get
            Set(value As System.DateTime)
                If mlastUpdate <> value Then
                    mlastUpdate = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mexist As System.Int32
        <DBColumnName("exist")>
        <DataMemberAttribute()>
        Public Overridable Property exist() As System.Int32
            Get
                Return mexist
            End Get
            Set(value As System.Int32)
                If mexist <> value Then
                    mexist = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const ID As String = "ID" 'int
            Public Const cod_hotel As String = "cod_hotel" 'int
            Public Const id_Foto As String = "id_Foto" 'int
            Public Const id_usuario As String = "id_usuario" 'int
            Public Const FotoFileName As String = "FotoFileName" 'varchar
            Public Const Description As String = "Description" 'varchar
            Public Const id_fotoType As String = "id_fotoType" 'int
            Public Const lastUpdate As String = "lastUpdate" 'datetime
            Public Const exist As String = "exist" 'int
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As Fotos) As Fotos
            If obj2Fill Is Nothing Then
                obj2Fill = New Fotos
            End If

            obj2Fill.ID = IIf(IsDBNull(dr(DBCol.ID)), Nothing, dr(DBCol.ID))
            obj2Fill.cod_hotel = IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing, dr(DBCol.cod_hotel))
            obj2Fill.id_Foto = IIf(IsDBNull(dr(DBCol.id_Foto)), Nothing, dr(DBCol.id_Foto))
            obj2Fill.id_usuario = IIf(IsDBNull(dr(DBCol.id_usuario)), Nothing, dr(DBCol.id_usuario))
            obj2Fill.FotoFileName = "" & dr(DBCol.FotoFileName)
            obj2Fill.Description = "" & dr(DBCol.Description)
            obj2Fill.id_fotoType = IIf(IsDBNull(dr(DBCol.id_fotoType)), Nothing, dr(DBCol.id_fotoType))
            obj2Fill.lastUpdate = IIf(IsDBNull(dr(DBCol.lastUpdate)), Nothing, dr(DBCol.lastUpdate))
            obj2Fill.exist = IIf(IsDBNull(dr(DBCol.exist)), Nothing, dr(DBCol.exist))
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class FotosCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As Fotos
            Get
                Return DirectCast(List(index), Fotos)
            End Get
            Set(value As Fotos)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As Fotos)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As Fotos)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
