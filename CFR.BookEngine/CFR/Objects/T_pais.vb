
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
Public Class T_pais
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


    private mid as System.Int32
  <DBColumnName("id")>
  <DataMemberAttribute()>
   Public Overridable Property id() As System.Int32  Implements IBObase.ID
    Get 
      return  mid
    End Get 
    Set(value As System.Int32) 
      IF mid<>value THEN 
        mid=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_Pais as System.Int32
  <DBColumnName("cod_Pais")>
  <DataMemberAttribute()>
   Public Overridable Property cod_Pais() As System.Int32 
    Get 
      return  mcod_Pais
    End Get 
    Set(value As System.Int32) 
      IF mcod_Pais<>value THEN 
        mcod_Pais=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mPais as System.String
  <DBColumnName("Pais")>
  <DataMemberAttribute()>
   Public Overridable Property Pais() As System.String 
    Get 
      return  mPais
    End Get 
    Set(value As System.String) 
      IF mPais<>value THEN 
        mPais=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mcod_cont as System.Int32
  <DBColumnName("cod_cont")>
  <DataMemberAttribute()>
   Public Overridable Property cod_cont() As System.Int32 
    Get 
      return  mcod_cont
    End Get 
    Set(value As System.Int32) 
      IF mcod_cont<>value THEN 
        mcod_cont=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mprefijoTelfPais as System.String
  <DBColumnName("prefijoTelfPais")>
  <DataMemberAttribute()>
   Public Overridable Property prefijoTelfPais() As System.String 
    Get 
      return  mprefijoTelfPais
    End Get 
    Set(value As System.String) 
      IF mprefijoTelfPais<>value THEN 
        mprefijoTelfPais=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private midioma1 as System.String
  <DBColumnName("idioma1")>
  <DataMemberAttribute()>
   Public Overridable Property idioma1() As System.String 
    Get 
      return  midioma1
    End Get 
    Set(value As System.String) 
      IF midioma1<>value THEN 
        midioma1=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private midioma2 as System.String
  <DBColumnName("idioma2")>
  <DataMemberAttribute()>
   Public Overridable Property idioma2() As System.String 
    Get 
      return  midioma2
    End Get 
    Set(value As System.String) 
      IF midioma2<>value THEN 
        midioma2=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private midioma3 as System.String
  <DBColumnName("idioma3")>
  <DataMemberAttribute()>
   Public Overridable Property idioma3() As System.String 
    Get 
      return  midioma3
    End Get 
    Set(value As System.String) 
      IF midioma3<>value THEN 
        midioma3=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private midioma4 as System.String
  <DBColumnName("idioma4")>
  <DataMemberAttribute()>
   Public Overridable Property idioma4() As System.String 
    Get 
      return  midioma4
    End Get 
    Set(value As System.String) 
      IF midioma4<>value THEN 
        midioma4=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCODE as System.String
  <DBColumnName("CODE")>
  <DataMemberAttribute()>
   Public Overridable Property CODE() As System.String 
    Get 
      return  mCODE
    End Get 
    Set(value As System.String) 
      IF mCODE<>value THEN 
        mCODE=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id As String="id" 'int
  Public Const cod_Pais As String="cod_Pais" 'int
  Public Const Pais As String="Pais" 'nvarchar
  Public Const cod_cont As String="cod_cont" 'int
  Public Const prefijoTelfPais As String="prefijoTelfPais" 'nvarchar
  Public Const idioma1 As String="idioma1" 'nvarchar
  Public Const idioma2 As String="idioma2" 'nvarchar
  Public Const idioma3 As String="idioma3" 'nvarchar
  Public Const idioma4 As String="idioma4" 'nvarchar
  Public Const CODE As String="CODE" 'varchar
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_pais) as T_pais
  If obj2Fill Is Nothing Then
      obj2Fill = New T_pais
  End If

  obj2Fill.id =IIf(IsDBNull(dr(DBCol.id)), Nothing,dr(DBCol.id)) 
  obj2Fill.cod_Pais =IIf(IsDBNull(dr(DBCol.cod_Pais)), Nothing,dr(DBCol.cod_Pais)) 
  obj2Fill.Pais ="" & dr(DBCol.Pais)
  obj2Fill.cod_cont =IIf(IsDBNull(dr(DBCol.cod_cont)), Nothing,dr(DBCol.cod_cont)) 
  obj2Fill.prefijoTelfPais ="" & dr(DBCol.prefijoTelfPais)
  obj2Fill.idioma1 ="" & dr(DBCol.idioma1)
  obj2Fill.idioma2 ="" & dr(DBCol.idioma2)
  obj2Fill.idioma3 ="" & dr(DBCol.idioma3)
  obj2Fill.idioma4 ="" & dr(DBCol.idioma4)
  obj2Fill.CODE ="" & dr(DBCol.CODE)
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_paisCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_pais
     Get
         Return DirectCast(List(index), T_pais)
     End Get
     Set(value As T_pais)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_pais)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_pais)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
