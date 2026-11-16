
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
Public Class R_tarifas
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


    private mcod_tarifa as System.Int32
  <DBColumnName("cod_tarifa")>
  <DataMemberAttribute()>
   Public Overridable Property cod_tarifa() As System.Int32  Implements IBObase.ID
    Get 
      return  mcod_tarifa
    End Get 
    Set(value As System.Int32) 
      IF mcod_tarifa<>value THEN 
        mcod_tarifa=value
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


    private mtarifa as System.String
  <DBColumnName("tarifa")>
  <DataMemberAttribute()>
   Public Overridable Property tarifa() As System.String 
    Get 
      return  mtarifa
    End Get 
    Set(value As System.String) 
      IF mtarifa<>value THEN 
        mtarifa=value
        me.objChanged=true
      END IF
    End Set 
        End Property



    private mDiasMinimos as System.Byte
  <DBColumnName("DiasMinimos")>
  <DataMemberAttribute()>
   Public Overridable Property DiasMinimos() As System.Byte 
    Get 
      return  mDiasMinimos
    End Get 
    Set(value As System.Byte) 
      IF mDiasMinimos<>value THEN 
        mDiasMinimos=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mImpuestoTarifa as System.Double
  <DBColumnName("ImpuestoTarifa")>
  <DataMemberAttribute()>
   Public Overridable Property ImpuestoTarifa() As System.Double 
    Get 
      return  mImpuestoTarifa
    End Get 
    Set(value As System.Double) 
      IF mImpuestoTarifa<>value THEN 
        mImpuestoTarifa=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mCategoria as System.Byte
  <DBColumnName("Categoria")>
  <DataMemberAttribute()>
   Public Overridable Property Categoria() As System.Byte 
    Get 
      return  mCategoria
    End Get 
    Set(value As System.Byte) 
      IF mCategoria<>value THEN 
        mCategoria=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mPrecNetoBruto as System.Boolean
  <DBColumnName("PrecNetoBruto")>
  <DataMemberAttribute()>
   Public Overridable Property PrecNetoBruto() As System.Boolean 
    Get 
      return  mPrecNetoBruto
    End Get 
    Set(value As System.Boolean) 
      IF mPrecNetoBruto<>value THEN 
        mPrecNetoBruto=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mTI as System.Boolean
  <DBColumnName("TI")>
  <DataMemberAttribute()>
   Public Overridable Property TI() As System.Boolean 
    Get 
      return  mTI
    End Get 
    Set(value As System.Boolean) 
      IF mTI<>value THEN 
        mTI=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mPC as System.Boolean
  <DBColumnName("PC")>
  <DataMemberAttribute()>
   Public Overridable Property PC() As System.Boolean 
    Get 
      return  mPC
    End Get 
    Set(value As System.Boolean) 
      IF mPC<>value THEN 
        mPC=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mMP as System.Boolean
  <DBColumnName("MP")>
  <DataMemberAttribute()>
   Public Overridable Property MP() As System.Boolean 
    Get 
      return  mMP
    End Get 
    Set(value As System.Boolean) 
      IF mMP<>value THEN 
        mMP=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAD as System.Boolean
  <DBColumnName("AD")>
  <DataMemberAttribute()>
   Public Overridable Property AD() As System.Boolean 
    Get 
      return  mAD
    End Get 
    Set(value As System.Boolean) 
      IF mAD<>value THEN 
        mAD=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mSA as System.Boolean
  <DBColumnName("SA")>
  <DataMemberAttribute()>
   Public Overridable Property SA() As System.Boolean 
    Get 
      return  mSA
    End Get 
    Set(value As System.Boolean) 
      IF mSA<>value THEN 
        mSA=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


    private mAplicDescACapac as System.Boolean
  <DBColumnName("AplicDescACapac")>
  <DataMemberAttribute()>
   Public Overridable Property AplicDescACapac() As System.Boolean 
    Get 
      return  mAplicDescACapac
    End Get 
    Set(value As System.Boolean) 
      IF mAplicDescACapac<>value THEN 
        mAplicDescACapac=value
        me.objChanged=true
      END IF
    End Set 
   End  Property


        Private mTPVOneNight As Boolean
        <DBColumnName("TPVOneNight")>
        <DataMemberAttribute()>
        Public Overridable Property TPVOneNight() As Boolean
            Get
                Return mTPVOneNight
            End Get
            Set(value As Boolean)
                If mTPVOneNight <> value Then
                    mTPVOneNight = value
                    Me.objChanged = True
                End If
            End Set
        End Property

        Private mTPVMinimum As System.Double
        <DBColumnName("TPVMinimum")>
        <DataMemberAttribute()>
        Public Overridable Property TPVMinimum() As System.Double
            Get
                Return mTPVMinimum
            End Get
            Set(value As System.Double)
                If mTPVMinimum <> value Then
                    mTPVMinimum = value
                    Me.objChanged = True
                End If
            End Set
        End Property

        Private mModifyDate As System.DateTime
        <DBColumnName("ModifyDate")>
        <DataMemberAttribute()>
        Public Overridable Property ModifyDate() As System.DateTime
            Get
                Return mModifyDate
            End Get
            Set(value As System.DateTime)
               mModifyDate = value
            End Set
        End Property

        Private mModifyCodUsuario As Integer
        <DBColumnName("ModifyCodUsuario")>
        <DataMemberAttribute()>
        Public Overridable Property ModifyCodUsuario() As Integer
            Get
                Return mModifyCodUsuario
            End Get
            Set(value As Integer)
                mModifyCodUsuario = value
            End Set
        End Property

        Private mpolicyID As System.Int32
        <DBColumnName("policyID")>
        <DataMemberAttribute()>
        Public Overridable Property policyID() As System.Int32
            Get
                Return mpolicyID
            End Get
            Set(value As System.Int32)
                If mpolicyID <> value Then
                    mpolicyID = value
                    Me.objChanged = True
                End If
            End Set
        End Property


#End Region


#Region "Column names typed"
Public Class DBCol 

            Public Const cod_tarifa As String = "cod_tarifa" 'int
            Public Const cod_hotel As String = "cod_hotel" 'int
            Public Const tarifa As String = "tarifa" 'varchar
            Public Const DiasMinimos As String = "DiasMinimos" 'tinyint
            Public Const ImpuestoTarifa As String = "ImpuestoTarifa" 'float
            Public Const Categoria As String = "Categoria" 'tinyint
            Public Const PrecNetoBruto As String = "PrecNetoBruto" 'bit
            Public Const TI As String = "TI" 'bit
            Public Const PC As String = "PC" 'bit
            Public Const MP As String = "MP" 'bit
            Public Const AD As String = "AD" 'bit
            Public Const SA As String = "SA" 'bit
            Public Const AplicDescACapac As String = "AplicDescACapac" 'bit
            
            Public Const TPVMinimum As String = "TPVMinimum" 'float
            Public Const TPVOneNight As String = "TPVOneNight" 'bit
            Public Const ModifyDate As String = "ModifyDate" 'datetime
            Public Const ModifyCodUsuario As String = "ModifyCodUsuario" 'integer
            Public Const norefundtariff As String = "norefundtariff" 'bit
            Public Const norefundtariffdiscount As String = "norefundtariffdiscount" 'float
            Public Const norefundtariffdiscountapply2tariffonly As String = "norefundtariffdiscountapply2tariffonly" 'bit
            Public Const policyID As String = "policyID" 'int
End Class  
#End Region


        Public Function FillByRow(ByRef dr As DataRow) As R_tarifas

            Return FillObjectByRow(dr, Me)

        End Function
        Public Function FillByRowView(ByRef drv As DataRowView) As R_tarifas

            Return FillObjectByRow(drv.row, Me)

        End Function
        Public Shared Function FillObjectByRowView(ByRef drv As DataRowView, Optional ByRef obj2Fill As R_tarifas = Nothing) As R_tarifas

            Return FillObjectByRow(drv.row, obj2Fill)

        End Function

        Public Shared Function FillObjectByRow(ByRef dr As DataRow, Optional ByRef obj2Fill As R_tarifas = Nothing) As R_tarifas
            If obj2Fill Is Nothing Then
                obj2Fill = New R_tarifas
            End If

            obj2Fill.cod_tarifa = IIf(IsDBNull(dr(DBCol.cod_tarifa)), Nothing, dr(DBCol.cod_tarifa))
            obj2Fill.cod_hotel = IIf(IsDBNull(dr(DBCol.cod_hotel)), Nothing, dr(DBCol.cod_hotel))
            obj2Fill.tarifa = "" & dr(DBCol.tarifa)
            obj2Fill.DiasMinimos = IIf(IsDBNull(dr(DBCol.DiasMinimos)), Nothing, dr(DBCol.DiasMinimos))
            obj2Fill.ImpuestoTarifa = IIf(IsDBNull(dr(DBCol.ImpuestoTarifa)), Nothing, dr(DBCol.ImpuestoTarifa))
            obj2Fill.Categoria = IIf(IsDBNull(dr(DBCol.Categoria)), Nothing, dr(DBCol.Categoria))
            obj2Fill.PrecNetoBruto = IIf(IsDBNull(dr(DBCol.PrecNetoBruto)), Nothing, dr(DBCol.PrecNetoBruto))
            obj2Fill.TI = IIf(IsDBNull(dr(DBCol.TI)), Nothing, dr(DBCol.TI))
            obj2Fill.PC = IIf(IsDBNull(dr(DBCol.PC)), Nothing, dr(DBCol.PC))
            obj2Fill.MP = IIf(IsDBNull(dr(DBCol.MP)), Nothing, dr(DBCol.MP))
            obj2Fill.AD = IIf(IsDBNull(dr(DBCol.AD)), Nothing, dr(DBCol.AD))
            obj2Fill.SA = IIf(IsDBNull(dr(DBCol.SA)), Nothing, dr(DBCol.SA))
            obj2Fill.AplicDescACapac = IIf(IsDBNull(dr(DBCol.AplicDescACapac)), Nothing, dr(DBCol.AplicDescACapac))
            obj2Fill.TPVMinimum = IIf(IsDBNull(dr(DBCol.TPVMinimum)), Nothing, dr(DBCol.TPVMinimum))
            obj2Fill.TPVOneNight = IIf(IsDBNull(dr(DBCol.TPVOneNight)), Nothing, dr(DBCol.TPVOneNight))

            obj2Fill.ModifyCodUsuario = IIf(IsDBNull(dr(DBCol.ModifyCodUsuario)), Nothing, dr(DBCol.ModifyCodUsuario))
            obj2Fill.ModifyDate = IIf(IsDBNull(dr(DBCol.ModifyDate)), Nothing, dr(DBCol.ModifyDate))
            obj2Fill.policyID = IIf(IsDBNull(dr(DBCol.policyID)), Nothing, dr(DBCol.policyID))
            obj2Fill.objChanged = False 'reset fill
            Return obj2Fill
        End Function
End  Class 



#Region "Collection asociated"
<Serializable()>
<ClassInterface(ClassInterfaceType.AutoDual)>
Public Class R_tarifasCollection
         Inherits System.Collections.CollectionBase

 Default Public Overridable Property Item(ByVal index As Integer) As R_tarifas
     Get
         Return DirectCast(List(index), R_tarifas)
     End Get
     Set(value As R_tarifas)
         List(index) = value
     End Set
 End Property

 Public Overridable Sub Add(ByVal Item As R_tarifas)
    List.Add(Item)
 End Sub

 Public Overridable Sub Remove(ByVal Item As R_tarifas)
    List.Remove(Item)
 End Sub
End Class
#End Region
End Namespace 
