Imports Microsoft.VisualBasic

Namespace cfr
    <Serializable()> _
    Public Class BasicDetails
        Implements IIDObject



#Region "Variables"
        Private idVar As Object
        Private nombrevar As String
        Private Tlfvar As String
        Private emailvar As String
        Private faxvar As String
        Private direccionvar As String
        Private cod_postalvar As String
        Private zoneVar As String
        Private Paisvar As New cfr.obj.Country
        Private identificationVar As String
#End Region

#Region "properties"
        Public Property Nombre() As String
            Get
                Return Me.nombrevar
            End Get
            Set(ByVal value As String)
                Me.nombrevar = cfr.tool.Tools.cNull(value)
            End Set
        End Property
        Public Property Tlf() As String
            Get
                Return Me.Tlfvar
            End Get
            Set(ByVal value As String)
                Me.Tlfvar = cfr.tool.Tools.cNull(value)
            End Set
        End Property
        Public Property email() As String
            Get
                Return Me.emailvar
            End Get
            Set(ByVal value As String)
                Me.emailvar = cfr.tool.Tools.cNull(value)
            End Set
        End Property
        Public Property fax() As String
            Get
                Return Me.faxvar
            End Get
            Set(ByVal value As String)
                Me.faxvar = cfr.tool.Tools.cNull(value)
            End Set
        End Property
        Public Property direccion() As String
            Get
                Return Me.direccionvar
            End Get
            Set(ByVal value As String)
                Me.direccionvar = cfr.tool.Tools.cNull(value)
            End Set
        End Property
        Public Property codpostal() As String
            Get
                Return Me.cod_postalvar
            End Get
            Set(ByVal value As String)
                Me.cod_postalvar = cfr.tool.Tools.cNull(value)
            End Set
        End Property


        Public Property Zone() As String
            Get
                Return Me.zoneVar
            End Get
            Set(ByVal value As String)
                Me.zoneVar = cfr.tool.Tools.cNull(value)
            End Set
        End Property
        Public Property Pais() As cfr.obj.Country
            Get
                Return Me.Paisvar
            End Get
            Set(ByVal value As cfr.obj.Country)
                Me.Paisvar = value
            End Set
        End Property


        ''' <summary>
        ''' Identification like DNI, DriverId, Company fiscal id
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property identification() As String
            Get
                Return Me.identificationVar
            End Get
            Set(ByVal value As String)
                Me.identificationVar = cfr.tool.Tools.cNull(value)
            End Set
        End Property
#End Region

        ''' <summary>
        ''' unique identifier
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Id() As Object Implements IIDObject.idEntity
            Get
                Return Me.idVar
            End Get
            Set(ByVal value As Object)
                Me.idVar = cfr.tool.Tools.cNull(value)
            End Set
        End Property
    End Class
End Namespace