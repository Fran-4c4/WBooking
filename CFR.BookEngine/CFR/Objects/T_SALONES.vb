
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
Public Class T_SALONES
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


    private mSalon as System.Int32
  <DBColumnName("Salon")>
  <DataMemberAttribute()>
   Public Overridable Property Salon() As System.Int32  Implements IBObase.ID
    Get 
      return  mSalon
    End Get 
    Set(value As System.Int32) 
      IF mSalon<>value THEN 
        mSalon=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mNombre as System.String
  <DBColumnName("Nombre")>
  <DataMemberAttribute()>
   Public Overridable Property Nombre() As System.String 
    Get 
      return  mNombre
    End Get 
    Set(value As System.String) 
      IF mNombre<>value THEN 
        mNombre=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mHotel as System.Int32
  <DBColumnName("Hotel")>
  <DataMemberAttribute()>
   Public Overridable Property Hotel() As System.Int32 
    Get 
      return  mHotel
    End Get 
    Set(value As System.Int32) 
      IF mHotel<>value THEN 
        mHotel=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mm2 as System.Int16
  <DBColumnName("m2")>
  <DataMemberAttribute()>
   Public Overridable Property m2() As System.Int16 
    Get 
      return  mm2
    End Get 
    Set(value As System.Int16) 
      IF mm2<>value THEN 
        mm2=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mTeatro as System.Int16
  <DBColumnName("Teatro")>
  <DataMemberAttribute()>
   Public Overridable Property Teatro() As System.Int16 
    Get 
      return  mTeatro
    End Get 
    Set(value As System.Int16) 
      IF mTeatro<>value THEN 
        mTeatro=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mEscuela as System.Int16
  <DBColumnName("Escuela")>
  <DataMemberAttribute()>
   Public Overridable Property Escuela() As System.Int16 
    Get 
      return  mEscuela
    End Get 
    Set(value As System.Int16) 
      IF mEscuela<>value THEN 
        mEscuela=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCocktail as System.Int16
  <DBColumnName("Cocktail")>
  <DataMemberAttribute()>
   Public Overridable Property Cocktail() As System.Int16 
    Get 
      return  mCocktail
    End Get 
    Set(value As System.Int16) 
      IF mCocktail<>value THEN 
        mCocktail=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mBanquete as System.Int16
  <DBColumnName("Banquete")>
  <DataMemberAttribute()>
   Public Overridable Property Banquete() As System.Int16 
    Get 
      return  mBanquete
    End Get 
    Set(value As System.Int16) 
      IF mBanquete<>value THEN 
        mBanquete=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mMesaI as System.Int16
  <DBColumnName("MesaI")>
  <DataMemberAttribute()>
   Public Overridable Property MesaI() As System.Int16 
    Get 
      return  mMesaI
    End Get 
    Set(value As System.Int16) 
      IF mMesaI<>value THEN 
        mMesaI=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mMesaU as System.Int16
  <DBColumnName("MesaU")>
  <DataMemberAttribute()>
   Public Overridable Property MesaU() As System.Int16 
    Get 
      return  mMesaU
    End Get 
    Set(value As System.Int16) 
      IF mMesaU<>value THEN 
        mMesaU=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const Salon As String="Salon" 'int
  Public Const Nombre As String="Nombre" 'nvarchar
  Public Const Hotel As String="Hotel" 'int
  Public Const m2 As String="m2" 'smallint
  Public Const Teatro As String="Teatro" 'smallint
  Public Const Escuela As String="Escuela" 'smallint
  Public Const Cocktail As String="Cocktail" 'smallint
  Public Const Banquete As String="Banquete" 'smallint
  Public Const MesaI As String="MesaI" 'smallint
  Public Const MesaU As String="MesaU" 'smallint
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As T_SALONES) as T_SALONES
  If obj2Fill Is Nothing Then
      obj2Fill = New T_SALONES
  End If

  obj2Fill.Salon =IIf(IsDBNull(dr(DBCol.Salon)), Nothing,dr(DBCol.Salon)) 
  obj2Fill.Nombre ="" & dr(DBCol.Nombre)
  obj2Fill.Hotel =IIf(IsDBNull(dr(DBCol.Hotel)), Nothing,dr(DBCol.Hotel)) 
  obj2Fill.m2 =IIf(IsDBNull(dr(DBCol.m2)), Nothing,dr(DBCol.m2)) 
  obj2Fill.Teatro =IIf(IsDBNull(dr(DBCol.Teatro)), Nothing,dr(DBCol.Teatro)) 
  obj2Fill.Escuela =IIf(IsDBNull(dr(DBCol.Escuela)), Nothing,dr(DBCol.Escuela)) 
  obj2Fill.Cocktail =IIf(IsDBNull(dr(DBCol.Cocktail)), Nothing,dr(DBCol.Cocktail)) 
  obj2Fill.Banquete =IIf(IsDBNull(dr(DBCol.Banquete)), Nothing,dr(DBCol.Banquete)) 
  obj2Fill.MesaI =IIf(IsDBNull(dr(DBCol.MesaI)), Nothing,dr(DBCol.MesaI)) 
  obj2Fill.MesaU =IIf(IsDBNull(dr(DBCol.MesaU)), Nothing,dr(DBCol.MesaU)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class T_SALONESCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As T_SALONES
     Get
         Return DirectCast(List(index), T_SALONES)
     End Get
     Set(value As T_SALONES)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As T_SALONES)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As T_SALONES)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
