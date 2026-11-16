
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
    Public Class R_Ofertas
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


        Private mid_oferta As System.Int32
        <DBColumnName("id_oferta")>
        <DataMemberAttribute()>
        Public Overridable Property id_oferta() As System.Int32 Implements IBObase.ID
            Get
                Return mid_oferta
            End Get
            Set(value As System.Int32)
                If mid_oferta <> value Then
                    mid_oferta = value
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


        Private mcod_CRS As System.Int16
        <DBColumnName("cod_CRS")>
        <DataMemberAttribute()>
        Public Overridable Property cod_CRS() As System.Int16
            Get
                Return mcod_CRS
            End Get
            Set(value As System.Int16)
                If mcod_CRS <> value Then
                    mcod_CRS = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mfechainicio As System.DateTime
        <DBColumnName("fechainicio")>
        <DataMemberAttribute()>
        Public Overridable Property fechainicio() As System.DateTime
            Get
                Return mfechainicio
            End Get
            Set(value As System.DateTime)
                If mfechainicio <> value Then
                    mfechainicio = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mfechafin As System.DateTime
        <DBColumnName("fechafin")>
        <DataMemberAttribute()>
        Public Overridable Property fechafin() As System.DateTime
            Get
                Return mfechafin
            End Get
            Set(value As System.DateTime)
                If mfechafin <> value Then
                    mfechafin = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mpopup As System.Byte
        <DBColumnName("popup")>
        <DataMemberAttribute()>
        Public Overridable Property popup() As System.Byte
            Get
                Return mpopup
            End Get
            Set(value As System.Byte)
                If mpopup <> value Then
                    mpopup = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDescEsp As System.String
        <DBColumnName("DescEsp")>
        <DataMemberAttribute()>
        Public Overridable Property DescEsp() As System.String
            Get
                Return mDescEsp
            End Get
            Set(value As System.String)
                If mDescEsp <> value Then
                    mDescEsp = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDescIngles As System.String
        <DBColumnName("DescIngles")>
        <DataMemberAttribute()>
        Public Overridable Property DescIngles() As System.String
            Get
                Return mDescIngles
            End Get
            Set(value As System.String)
                If mDescIngles <> value Then
                    mDescIngles = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDescFrances As System.String
        <DBColumnName("DescFrances")>
        <DataMemberAttribute()>
        Public Overridable Property DescFrances() As System.String
            Get
                Return mDescFrances
            End Get
            Set(value As System.String)
                If mDescFrances <> value Then
                    mDescFrances = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDescAleman As System.String
        <DBColumnName("DescAleman")>
        <DataMemberAttribute()>
        Public Overridable Property DescAleman() As System.String
            Get
                Return mDescAleman
            End Get
            Set(value As System.String)
                If mDescAleman <> value Then
                    mDescAleman = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDescItalian As System.String
        <DBColumnName("DescItalian")>
        <DataMemberAttribute()>
        Public Overridable Property DescItalian() As System.String
            Get
                Return mDescItalian
            End Get
            Set(value As System.String)
                If mDescItalian <> value Then
                    mDescItalian = value
                    Me.objChanged = True
                End If
            End Set
        End Property


        Private mDescCatalan As System.String
        <DBColumnName("DescCatalan")>
        <DataMemberAttribute()>
        Public Overridable Property DescCatalan() As System.String
            Get
                Return mDescCatalan
            End Get
            Set(value As System.String)
                If mDescCatalan <> value Then
                    mDescCatalan = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const id_oferta As String = "id_oferta" 'int
            Public Const cod_hotel As String = "cod_hotel" 'int
            Public Const cod_CRS As String = "cod_CRS" 'smallint
            Public Const fechainicio As String = "fechainicio" 'smalldatetime
            Public Const fechafin As String = "fechafin" 'smalldatetime
            Public Const popup As String = "popup" 'tinyint
            Public Const DescEsp As String = "DescEsp" 'varchar
            Public Const DescIngles As String = "DescIngles" 'varchar
            Public Const DescFrances As String = "DescFrances" 'varchar
            Public Const DescAleman As String = "DescAleman" 'varchar
            Public Const DescItalian As String = "DescItalian" 'varchar
            Public Const DescCatalan As String = "DescCatalan" 'varchar
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Ofertas) As R_Ofertas
            If obj2Fill Is Nothing Then
                obj2Fill = New R_Ofertas
            End If

            obj2Fill.id_oferta = IIf(IsDBNull(dr(DBCol.id_oferta)), Nothing, dr(DBCol.id_oferta))
            obj2Fill.cod_hotel = IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing, dr(DBCol.cod_hotel))
            obj2Fill.cod_CRS = IIf(IsDBNull(dr(DBCol.cod_CRS)), Nothing, dr(DBCol.cod_CRS))
            obj2Fill.fechainicio = IIf(IsDBNull(dr(DBCol.fechainicio)), Nothing, dr(DBCol.fechainicio))
            obj2Fill.fechafin = IIf(IsDBNull(dr(DBCol.fechafin)), Nothing, dr(DBCol.fechafin))
            obj2Fill.popup = IIf(IsDBNull(dr(DBCol.popup)), Nothing, dr(DBCol.popup))
            obj2Fill.DescEsp = "" & dr(DBCol.DescEsp)
            obj2Fill.DescIngles = "" & dr(DBCol.DescIngles)
            obj2Fill.DescFrances = "" & dr(DBCol.DescFrances)
            obj2Fill.DescAleman = "" & dr(DBCol.DescAleman)
            obj2Fill.DescItalian = "" & dr(DBCol.DescItalian)
            obj2Fill.DescCatalan = "" & dr(DBCol.DescCatalan)
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class R_OfertasCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As R_Ofertas
            Get
                Return DirectCast(List(index), R_Ofertas)
            End Get
            Set(value As R_Ofertas)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As R_Ofertas)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As R_Ofertas)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
