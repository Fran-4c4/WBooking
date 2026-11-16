
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
Public Class R_Factura_Conceptos
      inherits objBaseClass
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


    private mid_Concepto as System.Int32
  <DBColumnName("id_Concepto")>
  <DataMemberAttribute()>
   Public Overridable Property id_Concepto() As System.Int32  Implements IBObase.ID
    Get 
      return  mid_Concepto
    End Get 
    Set(value As System.Int32) 
      IF mid_Concepto<>value THEN 
        mid_Concepto=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mid_Factura as System.Int32
  <DBColumnName("id_Factura")>
  <DataMemberAttribute()>
   Public Overridable Property id_Factura() As System.Int32 
    Get 
      return  mid_Factura
    End Get 
    Set(value As System.Int32) 
      IF mid_Factura<>value THEN 
        mid_Factura=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mConcepto as System.String
  <DBColumnName("Concepto")>
  <DataMemberAttribute()>
   Public Overridable Property Concepto() As System.String 
    Get 
      return  mConcepto
    End Get 
    Set(value As System.String) 
      IF mConcepto<>value THEN 
        mConcepto=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mImporte as System.Double
  <DBColumnName("Importe")>
  <DataMemberAttribute()>
   Public Overridable Property Importe() As System.Double 
    Get 
      return  mImporte
    End Get 
    Set(value As System.Double) 
      IF mImporte<>value THEN 
        mImporte=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mImpuestos as System.Double
  <DBColumnName("Impuestos")>
  <DataMemberAttribute()>
   Public Overridable Property Impuestos() As System.Double 
    Get 
      return  mImpuestos
    End Get 
    Set(value As System.Double) 
      IF mImpuestos<>value THEN 
        mImpuestos=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mQuantity as System.Int32
  <DBColumnName("Quantity")>
  <DataMemberAttribute()>
   Public Overridable Property Quantity() As System.Int32 
    Get 
      return  mQuantity
    End Get 
    Set(value As System.Int32) 
      IF mQuantity<>value THEN 
        mQuantity=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id_Concepto As String="id_Concepto" 'int
  Public Const id_Factura As String="id_Factura" 'int
  Public Const Concepto As String="Concepto" 'varchar
  Public Const Importe As String="Importe" 'float
  Public Const Impuestos As String="Impuestos" 'float
  Public Const Quantity As String="Quantity" 'int
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As R_Factura_Conceptos) as R_Factura_Conceptos
  If obj2Fill Is Nothing Then
      obj2Fill = New R_Factura_Conceptos
  End If

  obj2Fill.id_Concepto =IIf(IsDBNull(dr(DBCol.id_Concepto)), Nothing,dr(DBCol.id_Concepto)) 
  obj2Fill.id_Factura =IIf(IsDBNull(dr(DBCol.id_Factura)), Nothing,dr(DBCol.id_Factura)) 
  obj2Fill.Concepto ="" & dr(DBCol.Concepto)
  obj2Fill.Importe =IIf(IsDBNull(dr(DBCol.Importe)), Nothing,dr(DBCol.Importe)) 
  obj2Fill.Impuestos =IIf(IsDBNull(dr(DBCol.Impuestos)), Nothing,dr(DBCol.Impuestos)) 
  obj2Fill.Quantity =IIf(IsDBNull(dr(DBCol.Quantity)), Nothing,dr(DBCol.Quantity)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_Factura_ConceptosCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_Factura_Conceptos
     Get
         Return DirectCast(List(index), R_Factura_Conceptos)
     End Get
     Set(value As R_Factura_Conceptos)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_Factura_Conceptos)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_Factura_Conceptos)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
