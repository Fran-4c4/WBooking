
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
Public Class Boletines
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


    private mid_email as System.Int32
  <DBColumnName("id_email")>
  <DataMemberAttribute()>
   Public Overridable Property id_email() As System.Int32  Implements IBObase.ID
    Get 
      return  mid_email
    End Get 
    Set(value As System.Int32) 
      IF mid_email<>value THEN 
        mid_email=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private memail as System.String
  <DBColumnName("email")>
  <DataMemberAttribute()>
   Public Overridable Property email() As System.String 
    Get 
      return  memail
    End Get 
    Set(value As System.String) 
      IF memail<>value THEN 
        memail=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mboletin as System.Byte
  <DBColumnName("boletin")>
  <DataMemberAttribute()>
   Public Overridable Property boletin() As System.Byte 
    Get 
      return  mboletin
    End Get 
    Set(value As System.Byte) 
      IF mboletin<>value THEN 
        mboletin=value
        me.objChanged=true
      END IF
    End Set 
   End  Property

#End Region


#Region "Column names typed"
Public Class DBCol 

  Public Const id_email As String="id_email" 'int
  Public Const email As String="email" 'varchar
  Public Const boletin As String="boletin" 'tinyint
End Class  
#End Region


Public Shared function FillObjectByRow(ByRef dr As DataRow, ByRef obj2Fill As Boletines) as Boletines
  If obj2Fill Is Nothing Then
      obj2Fill = New Boletines
  End If

  obj2Fill.id_email =IIf(IsDBNull(dr(DBCol.id_email)), Nothing,dr(DBCol.id_email)) 
  obj2Fill.email ="" & dr(DBCol.email)
  obj2Fill.boletin =IIf(IsDBNull(dr(DBCol.boletin)), Nothing,dr(DBCol.boletin)) 
   return  obj2Fill 
End function  
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class BoletinesCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As Boletines
     Get
         Return DirectCast(List(index), Boletines)
     End Get
     Set(value As Boletines)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As Boletines)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As Boletines)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
