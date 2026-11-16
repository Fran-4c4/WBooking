
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
    Public Class R_Cad_Hot_Sub
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


        Private mid_Hot_Sub As System.Int32
        <DBColumnName("id_Hot_Sub")>
        <DataMemberAttribute()>
        Public Overridable Property id_Hot_Sub() As System.Int32 Implements IBObase.ID
            Get
                Return mid_Hot_Sub
            End Get
            Set(value As System.Int32)
                If mid_Hot_Sub <> value Then
                    mid_Hot_Sub = value
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


        Private mcod_hotelSub As System.Int32
        <DBColumnName("cod_hotelSub")>
        <DataMemberAttribute()>
        Public Overridable Property cod_hotelSub() As System.Int32
            Get
                Return mcod_hotelSub
            End Get
            Set(value As System.Int32)
                If mcod_hotelSub <> value Then
                    mcod_hotelSub = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const id_Hot_Sub As String = "id_Hot_Sub" 'int
            Public Const cod_hotel As String = "cod_hotel" 'int
            Public Const cod_hotelSub As String = "cod_hotelSub" 'int
        End Class
#End Region


        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Cad_Hot_Sub) As R_Cad_Hot_Sub
            If obj2Fill Is Nothing Then
                obj2Fill = New R_Cad_Hot_Sub
            End If

            obj2Fill.id_Hot_Sub = IIf(IsDBNull(dr(DBCol.id_Hot_Sub)), Nothing, dr(DBCol.id_Hot_Sub))
            obj2Fill.cod_hotel = IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing, dr(DBCol.cod_hotel))
            obj2Fill.cod_hotelSub = IIf(IsDBNull(dr(DBCol.cod_hotelSub)), Nothing, dr(DBCol.cod_hotelSub))
            Return obj2Fill
        End Function
    End Class



#Region "Collection asociated"
    <Serializable()>
    <ClassInterface(ClassInterfaceType.AutoDual)>
    Public Class R_Cad_Hot_SubCollection
        Inherits System.Collections.CollectionBase

        Default Public Overridable Property Item(ByVal index As Integer) As R_Cad_Hot_Sub
            Get
                Return DirectCast(List(index), R_Cad_Hot_Sub)
            End Get
            Set(value As R_Cad_Hot_Sub)
                List(index) = value
            End Set
        End Property

        Public Overridable Sub Add(ByVal Item As R_Cad_Hot_Sub)
            List.Add(Item)
        End Sub

        Public Overridable Sub Remove(ByVal Item As R_Cad_Hot_Sub)
            List.Remove(Item)
        End Sub
    End Class
#End Region
End Namespace
