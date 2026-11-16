Imports System.ComponentModel
Imports System.Reflection
Imports Microsoft.VisualBasic

Namespace cfr

    ''' <summary>
    ''' class helper with constants and enumerations
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CFRConst
        ''' <summary>
        ''' for combos
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum cboEnum As Short
            none = 0
            noSelected = -1 'this is the number indicating no selection yet
        End Enum

        Public Enum msgEnum As Short
            none = -1
            errorType = 0
            Ok = 1
            warning = 2
            help = 3
            info = 4
            delete = 5
        End Enum

        ''' <summary>
        ''' enumeration for base types of elements. Hotel, cars
        ''' Values in DDBB t_unittype
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum unitTypeEnum As Byte
            hotel = 1
            car = 2
        End Enum

        Public Function getLangCode(LanguageEnumSelected As LanguageEnum) As String
            Return GetEnumDescription(LanguageEnumSelected)
        End Function

        Public Shared Function GetEnumDescription(ByVal EnumConstant As System.Enum) As String
            Dim fi As FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
            Dim attr() As DescriptionAttribute = DirectCast(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())

            If attr.Length > 0 Then
                Return attr(0).Description
            Else
                Return EnumConstant.ToString
            End If
        End Function

        Public Enum LanguageEnum As Short
            <Description("es")>
            es = 1
            <Description("en")>
            en = 2
            <Description("de")>
            de = 3
            <Description("fr")>
            fr = 4
            <Description("it")>
            it = 5
            <Description("cat")>
            ca = 6
            <Description("pt")>
            pt = 7
            <Description("jp")>
            jp = 8
        End Enum

        Public Enum RegimenEnum As Short
            notselected = 0
            none = 0
            SA = 1
            AD = 2
            MP = 3
            PC = 4
            TI = 5
        End Enum
        ''' <summary>
        ''' Tariff validation
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum tariffValidEnum As Byte
            NotValid = 0
            Valid = 1
            NotValidDueToMinimumDays = 2
            NotValid_NotInSelectedDays = 3
        End Enum


        Public Enum ClientTypeEnum As Short

            None = -1 'No aplicar FILTROS"
            NotClasified = 0  '0-SIN CLASIFICAR

            HotelAgency = 1  '1-Agencias del hotel  'TipoCliente1 
            CRS = 2  '2-CRS
            TourOperators = 3  '3-Touroperadores
            Company = 4  '4-Empresas
            PrivateClient = 5  '5-Particular
            WebSite = 6  '6-Websites

            registeredClient = 7 'used only when an agency is going to book for a registeredClient
            promotionCode = 8 'the client is a promotional code


            SystemAgency = 200  '7-Agencias yaHootel
            SystemGroup = 201  '8-Grupos yaHootel
            SystemCompany = 202  '9-Empresas/Agencias
            GroupAndCompany = 203  'Grupos y empresas


        End Enum

        Public Enum ChildrenUseBed As Integer
            Yes = 1
            No = 0
        End Enum

        Public Enum YesNo As Integer
            Yes = 1
            No = 0
        End Enum

        Public Enum bookTypeEnum As Integer
            'RESERVA DE AGENCIA, A QUIEN REALIZA LA RESERVA
            resIndefinido = 0 'No se ha definido
            resParticular = -1 'cliente particular
            resEmpresa = -2 'empresa
            resClienteRegistrado = -3 'empresa registrada/gran cuenta
        End Enum
        ''' <summary>
        ''' Codes for the type of client
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum genericOperator As Integer
            freeSale = 0
            Registered = 1
            ''' <summary>
            ''' This is the code for generic private client. It can´t not be deleted. 
            ''' </summary>
            ''' <remarks></remarks>
            PrivateGeneral = 2
        End Enum
        ''' <summary>
        ''' For those operators in boo system
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum genericClient As Integer
            NoGeneric = 0
            YesGeneric = 1
        End Enum

        Public Enum bookStateEnum As Integer
            Active = 0 'reserva normal
            CancelByClient = 1 'cancelada x cliente
            NoShow = 2 'noshow
            estado3 = 3 'modificada
            CancelByHotelUser = 4 'cancelada x usuario
            confirmedByHotel = 5 'confirmada /checkin
            canceledByOperator = 6 'cancelada x operador
            confirmedCheckOut = 7 'confirmada /checkout
        End Enum

        Public Enum BookOperationInfoEnum As Integer
            ok = 0
            OkWarnings = 1
            Fail = 2
        End Enum

        Public Enum payStateEnum As Byte
            pending = 0

            ''' <summary>
            ''' payed int totally
            ''' </summary>
            ''' <remarks></remarks>
            payed = 2
            ''' <summary>
            ''' cancel for example without pay.
            ''' </summary>
            ''' <remarks></remarks>
            noactioneeded = 3
            ''' <summary>
            ''' user cancel and need to be charged
            ''' </summary>
            ''' <remarks></remarks>
            paybycancellationneeded = 4
            ''' <summary>
            ''' when book is payed based on cancellation fee
            ''' </summary>
            ''' <remarks></remarks>
            paybycancellationOK = 5
            ''' <summary>
            ''' When devolution is needed due to some charge
            ''' </summary>
            ''' <remarks></remarks>
            devolutionPending = 6
            ''' <summary>
            ''' devolution ok
            ''' </summary>
            ''' <remarks></remarks>
            DevolutionOK = 7
            ''' <summary>
            ''' charge was made to book, for example in tpv
            ''' </summary>
            ''' <remarks></remarks>
            PrechargeMade = 8


        End Enum

        Public Enum formaPago As Integer
            AnyPay = 0
            CreditCard = 1
            RegisteredCreditCard = 2
            Transfer = 3
            DirectPay = 4
            TPVHotel = 5
            TPVSystem = 6
        End Enum

        ''' <summary>
        ''' codigos de texto de frases 
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum lngBas As Integer
            close = 199
            SiteRequired = 1000
            required = 352
            lopdrequired = 280
        End Enum




    End Class

End Namespace