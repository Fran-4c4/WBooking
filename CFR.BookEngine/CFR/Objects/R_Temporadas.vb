
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
Public Class R_Temporadas
      inherits objBaseClass
      Implements IBObase


        Public Sub New()
    MyBase.New()
End Sub

#Region "Properties and Fields"


    private mcod_Temporada as System.Int32
  <DBColumnName("cod_Temporada")>
  <DataMemberAttribute()>
   Public Overridable Property cod_Temporada() As System.Int32  Implements IBObase.ID
    Get 
      return  mcod_Temporada
    End Get 
    Set(value As System.Int32) 
      IF mcod_Temporada<>value THEN 
        mcod_Temporada=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_hotel as System.Int32
  <DBColumnName("cod_hotel")>
  <DataMemberAttribute()>
   Public Overridable Property cod_hotel() As System.Int32 
    Get 
      return  mcod_hotel
    End Get 
    Set(value As System.Int32) 
      IF mcod_hotel<>value THEN 
        mcod_hotel=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mTemporada as System.String
        <DBColumnName("Temporada")>
        <DataMemberAttribute()>
        Public Overridable Property Temporada() As System.String
            Get
                Return mTemporada
            End Get
            Set(value As System.String)
                If mTemporada <> value Then
                    mTemporada = value
                    Me.objChanged = True
                End If
            End Set
        End Property

        Private mtemporadaType As System.Boolean
        <DBColumnName("temporadaType")>
        <DataMemberAttribute()>
        Public Overridable Property temporadaType() As System.Boolean
            Get
                Return mtemporadaType
            End Get
            Set(value As System.Boolean)
                If mtemporadaType <> value Then
                    mtemporadaType = value
                    Me.objChanged = True
                End If
            End Set
        End Property

#End Region


#Region "Column names typed"
        Public Class DBCol

            Public Const cod_Temporada As String = "cod_Temporada" 'int
            Public Const cod_hotel As String = "cod_hotel" 'int
            Public Const Temporada As String = "Temporada" 'varchar
            Public Const temporadaType As String = "temporadaType" 'bit
        End Class
#End Region

        Public Function FillByRow(ByRef dr As DataRow) As R_Temporadas

            Return FillObjectByRow(dr, Me)

        End Function
        Public Function FillByRowView(ByRef drv As DataRowView) As R_Temporadas

            Return FillObjectByRow(drv.Row, Me)

        End Function
        Public Shared Function FillObjectByRowView(ByRef drv As DataRowView, Optional ByRef obj2Fill As R_Temporadas = Nothing) As R_Temporadas

            Return FillObjectByRow(drv.Row, obj2Fill)

        End Function

        Public Shared Function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Temporadas) As R_Temporadas
            If obj2Fill Is Nothing Then
                obj2Fill = New R_Temporadas
            End If
            obj2Fill.cod_Temporada = IIf(IsDBNull(dr(DBCol.cod_Temporada)), Nothing, dr(DBCol.cod_Temporada))
            obj2Fill.cod_hotel = IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing, dr(DBCol.cod_hotel))
            obj2Fill.Temporada = "" & dr(DBCol.Temporada)
            obj2Fill.temporadaType = IIf(IsDBNull(dr(DBCol.temporadaType)), Nothing, dr(DBCol.temporadaType))
            obj2Fill.objChanged = False 'reset fill
            Return obj2Fill
        End Function
    End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_TemporadasCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_Temporadas
     Get
         Return DirectCast(List(index), R_Temporadas)
     End Get
     Set(value As R_Temporadas)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_Temporadas)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_Temporadas)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
