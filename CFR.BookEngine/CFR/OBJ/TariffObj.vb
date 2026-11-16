Imports Microsoft.VisualBasic
Imports cfr.tool


Namespace cfr
    ''' <summary>
    ''' Tariff data 
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class TariffObj
        Implements IIDObject
        'Public fechainicio As Date
        'Public fechafin As Date

        Public cod_Tarifa As Integer
        Public Precio As Double
        Public PrecioAD As Double
        Public PrecioMP As Double
        Public PrecioPC As Double
        Public DescAdultosExtra1 As Double
        Public DescAdultosExtra2 As Double
        Public camaExtra1 As Double
        Public camaExtra2 As Double
        Public DescNinoPago1 As Double
        Public DescNinoPago2 As Double
        Public camaExtra1Nino As Double
        Public camaExtra2Nino As Double
        Public SA As Boolean
        Public AD As Boolean
        Public MP As Boolean
        Public PC As Boolean
        Public TI As Boolean

        Public minimumDays As Integer

        Public impuestos As Double

        Public Tarifa As String ' 
        Public desayunonino As Double
        Public mpnino As Double
        Public pcnino As Double
        Public TodoIncluido As Double
        Public TodoIncluidoNino As Double
        Public AplicDescAReg As Integer
        Public PaxIncluidEnPrecio As Integer
        Public AplicDescACapac As Integer
        Public TarifaConDiasMinimos As Integer

        Public promotionDiscount As Double = 0
        Public SupDiasMinimos As Double

        Public SupFinDeSemana As Double

        'tpv data
        ''' <summary>
        ''' value to charge in 
        ''' </summary>
        ''' <remarks></remarks>
        Public TPVMinimum As Double
        ''' <summary>
        ''' indicates if the price to be charge to client, if true it must be the ma
        ''' </summary>
        ''' <remarks></remarks>
        Public TPVOneNight As Boolean

        Public norefundtariff As Boolean
        Public norefundtariffdiscount As Double
        Public norefundtariffdiscountapply2tariffonly As Boolean

        Public cancelpolicyID As Integer
        Private cancelpolicyVar As R_PolicyCancel
        Public Property cancelpolicy As R_PolicyCancel
            Get
                If Not Me.cancelpolicyVar Is Nothing Then
                    Return Me.cancelpolicyVar
                Else
                    If Me.cancelpolicyID <> -1 Then
                        Using ctx As New cfrbookEntities
                            cancelpolicyVar = ctx.R_PolicyCancel.Find(Me.cancelpolicyID)
                        End Using
                    End If
                End If

                Return Me.cancelpolicyVar

            End Get
            Set(value As R_PolicyCancel)
                Me.cancelpolicyVar = value
                Me.cancelpolicyID = value.policyID

            End Set
        End Property


        Public Property id() As Object Implements IIDObject.idEntity
            Get
                Return Me.cod_Tarifa
            End Get
            Set(ByVal value As Object)
                Me.cod_Tarifa = value
            End Set
        End Property


        ''' <summary>
        ''' fill tariff data object with row by ref
        ''' </summary>
        ''' <param name="dataRowTariff"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FillTariffObj(ByRef dataRowTariff As DataRow) As TariffObj

            Dim tar As New TariffObj
            tar.id = dataRowTariff("cod_tarifa")
            'tar.fechainicio = dataRowTariff(cfr.tables.R_Temporadas_Periodos.fechainicio)
            'tar.fechafin = dataRowTariff(cfr.tables.R_Temporadas_Periodos.fechafin)
            tar.Precio = dataRowTariff("precio")
            tar.PrecioAD = Tools.cNull(dataRowTariff("PRECIOAD"), 0)
            tar.PrecioMP = Tools.cNull(dataRowTariff("PRECIOMP"), 0)
            tar.PrecioPC = Tools.cNull(dataRowTariff("PRECIOPC"), 0)
            tar.DescAdultosExtra1 = Tools.cNull(dataRowTariff("DescAdultosExtra1"), 0)
            tar.DescAdultosExtra2 = Tools.cNull(dataRowTariff("DescAdultosExtra2"), 0)
            tar.camaExtra1 = Tools.cNull(dataRowTariff("camaExtra1"), 0)
            tar.camaExtra2 = Tools.cNull(dataRowTariff("camaExtra2"), 0)
            tar.DescNinoPago1 = Tools.cNull(dataRowTariff("DescNinoPago1"), 0)
            tar.DescNinoPago2 = Tools.cNull(dataRowTariff("DescNinoPago2"), 0)
            tar.camaExtra1Nino = Tools.cNull(dataRowTariff("camaExtra1Nino"), 0)
            tar.camaExtra2Nino = Tools.cNull(dataRowTariff("camaExtra2Nino"), 0)
            'regimen
            tar.SA = dataRowTariff("SA")
            tar.AD = dataRowTariff("AD")
            tar.MP = dataRowTariff("MP")
            tar.PC = dataRowTariff("PC")
            tar.TI = dataRowTariff("TI")
            tar.desayunonino = Tools.cNull(dataRowTariff("desayunonino"), 0)
            tar.mpnino = Tools.cNull(dataRowTariff("mpnino"), 0)
            tar.pcnino = Tools.cNull(dataRowTariff("pcnino"), 0)
            tar.TodoIncluido = Tools.cNull(dataRowTariff("TodoIncluido"), 0)
            tar.TodoIncluidoNino = Tools.cNull(dataRowTariff("TodoIncluidoNino"), 0)
            tar.AplicDescAReg = dataRowTariff("AplicDescAReg")


            tar.SupDiasMinimos = dataRowTariff("SupDiasMinimos")
            tar.SupFinDeSemana = dataRowTariff("SupFinDeSemana")


            tar.minimumDays = dataRowTariff("diasminimos")
            tar.Tarifa = dataRowTariff("tarifa")
            tar.cod_Tarifa = dataRowTariff("cod_tarifa")
            tar.impuestos = Tools.cNull(dataRowTariff("ImpuestoTarifa"), 0)
            tar.AplicDescACapac = dataRowTariff("AplicDescACapac")
            'tpv
            tar.TPVMinimum = Tools.cNull(dataRowTariff("TPVMinimum"), 0)
            tar.TPVOneNight = Tools.cNull(dataRowTariff("TPVOneNight"), 0)

            tar.norefundtariff = Tools.cNull(dataRowTariff(ind.bk.obj.R_tarifas.DBCol.norefundtariff), False)
            tar.norefundtariffdiscount = Tools.cNull(dataRowTariff(ind.bk.obj.R_tarifas.DBCol.norefundtariffdiscount), 0)
            tar.norefundtariffdiscountapply2tariffonly = Tools.cNull(dataRowTariff(ind.bk.obj.R_tarifas.DBCol.norefundtariffdiscountapply2tariffonly), True)

            tar.cancelpolicyID = Tools.cNull(dataRowTariff(ind.bk.obj.R_tarifas.DBCol.policyID), "-1")


            Return tar
        End Function




    End Class
End Namespace