Imports System.Data
Imports System.Diagnostics

Partial Class Admin_PromotionList
    Inherits AdminBasePage

    Public permisionRequired As cfr.enum.adminAllowEnum = cfr.enum.adminAllowEnum.P7_Informes_de_reservas
    
    Public barchartlabels As String
    Public barchartdata As String
    Public barchartbookdata As String
    Public barchartsubtitle As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.validateUserAccessToSection(cfr.web.enum.adminWebPageEnum.informes)

        If Not Page.IsPostBack Then
            Dim dayNow As Date = Date.Now
            Me.checkindate.Text = dayNow.ToString("dd/MM/yyyy")
            dayNow = dayNow.AddMonths(6)
            Me.checkoutdate.Text = dayNow.ToString("dd/MM/yyyy")

            getRequestsByDay(Me.hotel.id, Date.Now, Date.Now.AddMonths(6))
        Else

        End If
    End Sub

    Public Class bkstat
        Public dt As Date
        Public requests As Integer
        Public books As Integer
        Public availability As Integer
    End Class

    Public bkstatHt As New SortedList

    Public Function getRequestsByDay(cod_hotel As Integer, datein As Date, dateOut As Date)


        Dim sw As New Stopwatch
        Dim frequency As Long = Stopwatch.Frequency
        Debug.WriteLine("  Timer frequency in ticks per second = {0}", frequency)
        Dim nanosecPerTick As Long = 1000000000 / frequency
        Console.WriteLine("  Timer is accurate within {0} nanoseconds", nanosecPerTick)
        sw.Start()
        Dim elap As Integer

        Dim lbl As New StringBuilder
        Dim lbldata As New StringBuilder
        Dim lblbookdata As New StringBuilder

        Me.barchartsubtitle = "Desde " & datein.ToLongDateString & " hasta " & dateOut.ToLongDateString

        Dim scomma As String = ""
        Dim dv As DataView = Me.getRequestsMadeBetween(cod_hotel, datein, dateOut)
        For Each drv As DataRowView In dv
            Dim st As New bkstat
            st.dt = drv("fecha_entrada")

            st.requests = drv("peticiones")

            Me.bkstatHt.Add(st.dt, st)
        Next

        Debug.WriteLine("Get requests: " & (sw.ElapsedTicks / Stopwatch.Frequency) * 1000)
        elap = sw.ElapsedTicks
        dv.Dispose()

        Dim dvbk As DataView = Me.getBooksMadeBetween(cod_hotel, datein, dateOut)
        For Each drv As DataRowView In dvbk
            Dim dt As Date = drv("fecha_entrada")
            Dim st As bkstat = Me.bkstatHt(dt)
            If st Is Nothing Then
                st = New bkstat
                st.dt = dt
                Me.bkstatHt.Add(st.dt, st)
            End If
            st.books = drv("bookcount")

        Next

        Debug.WriteLine("Get books: " & ((sw.ElapsedTicks - elap) / Stopwatch.Frequency) * 1000)
        elap = sw.ElapsedTicks
        dvbk.Dispose()

        Dim dvAval As DataView = Me.getAvailabilityBetween(cod_hotel, datein, dateOut)
        For Each drv As DataRowView In dvAval
            Dim dt As Date = drv("dia")
            Dim st As bkstat = Me.bkstatHt(dt)
            If st Is Nothing Then
                st = New bkstat
                st.dt = dt
                Me.bkstatHt.Add(st.dt, st)
            End If
            st.availability += drv("disponibleTotal") 'query return for each operator and room. Multiples loop but for future
        Next

        Debug.WriteLine("Get availabilty: " & ((sw.ElapsedTicks - elap) / Stopwatch.Frequency) * 1000)
        elap = sw.ElapsedTicks
        dvAval.Dispose()

        'build result
        Dim s As String = ""
        For Each item As bkstat In Me.bkstatHt.Values
            Dim sdt As String = "{0}[""{1}"",{2},{3},{4}]"
            lblbookdata.AppendFormat(sdt, scomma, item.dt.ToShortDateString, item.requests, item.books, item.availability)
            scomma = ","
        Next
        Debug.WriteLine("build result: " & ((sw.ElapsedTicks - elap) / Stopwatch.Frequency) * 1000)
        elap = sw.ElapsedTicks

        Me.barchartbookdata = lblbookdata.ToString
        Debug.WriteLine("build result to string: " & ((sw.ElapsedTicks - elap) / Stopwatch.Frequency) * 1000)

        sw.Stop()
    End Function

    

    Public Function getRequestsMadeBetween(cod_hotel As Integer, datein As Date, dateOut As Date) As DataView
        If cod_hotel <> Me.codhotel Then
            Throw New HttpException(404, "Hotel not found.")
        End If

        Dim s As String
        s = "SELECT     COUNT(fechaEntrada) AS peticiones, fechaEntrada as fecha_Entrada, cod_hotel "
        s = s & " FROM         R_Peticiones "
        s = s & " WHERE fechaEntrada >= @dateFrom "
        s = s & " AND fechaEntrada <= @dateTo AND cod_hotel = @hotelId "
        s = s & " GROUP BY fechaEntrada, cod_hotel "
        s = s & " ORDER BY cod_hotel, fechaEntrada "

        Dim dv As DataView = ind.bk.DBMgr.getDT(
            s,
            New SqlClient.SqlParameter("@dateFrom", SqlDbType.DateTime) With {.Value = datein},
            New SqlClient.SqlParameter("@dateTo", SqlDbType.DateTime) With {.Value = dateOut},
            New SqlClient.SqlParameter("@hotelId", SqlDbType.Int) With {.Value = cod_hotel}).DefaultView

        Return dv

    End Function

    Public Function getBooksMadeBetween(cod_hotel As Integer, datein As Date, dateOut As Date) As DataView
        If cod_hotel <> Me.codhotel Then
            Throw New HttpException(404, "Hotel not found.")
        End If

        Dim s As String
        s = "SELECT     COUNT(R_ResO_Hab.fecha_entrada) AS bookcount, R_ResO_Hab.fecha_entrada"
        s = s & " FROM         R_ResO_Hab inner join R_ReservasO on  R_ReservasO.cod_reservaO = R_ResO_Hab.cod_reservaO "

        s = s & " WHERE R_ResO_Hab.fecha_entrada >= @dateFrom "
        s = s & " AND R_ResO_Hab.fecha_entrada <= @dateTo AND cod_hotel = @hotelId "
        s = s & " GROUP BY R_ResO_Hab.fecha_entrada, cod_hotel "
        s = s & " ORDER BY cod_hotel, R_ResO_Hab.fecha_entrada "

        Dim dv As DataView = ind.bk.DBMgr.getDT(
            s,
            New SqlClient.SqlParameter("@dateFrom", SqlDbType.DateTime) With {.Value = datein},
            New SqlClient.SqlParameter("@dateTo", SqlDbType.DateTime) With {.Value = dateOut},
            New SqlClient.SqlParameter("@hotelId", SqlDbType.Int) With {.Value = cod_hotel}).DefaultView

        Return dv

    End Function


    Public Function getAvailabilityBetween(cod_hotel As Integer, datein As Date, dateOut As Date) As DataView
        If cod_hotel <> Me.codhotel Then
            Throw New HttpException(404, "Hotel not found.")
        End If

        Dim sSql As String
        sSql = " SELECT    R_disponibilidad.dia, sum(R_Res_cupos.disponible) as disponibleTotal "
        sSql = sSql & " FROM R_disponibilidad INNER JOIN "
        sSQL = sSQL & " R_Res_cupos ON R_disponibilidad.id_Disp_Hab = R_Res_cupos.id_Disp_Hab "
        sSQL = sSQL & " WHERE R_disponibilidad.cod_Hotel = @hotelId"
        sSql = sSql & " AND DIA >= @dateFrom AND DIA <= @dateTo"
        sSql = sSql & " group by dia "

        Dim dt As DataTable = ind.bk.DBMgr.getDT(
            sSQL,
            New SqlClient.SqlParameter("@hotelId", SqlDbType.Int) With {.Value = cod_hotel},
            New SqlClient.SqlParameter("@dateFrom", SqlDbType.DateTime) With {.Value = datein},
            New SqlClient.SqlParameter("@dateTo", SqlDbType.DateTime) With {.Value = dateOut})

        Return dt.DefaultView

    End Function

    
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try

            Dim datein As Date = DateTime.ParseExact(Me.checkindate.Text, "dd/MM/yyyy", Nothing)
            Dim dateOut As Date = DateTime.ParseExact(Me.checkoutdate.Text, "dd/MM/yyyy", Nothing)
            If dateOut < datein Then
                cfr.pages.SiteBasePage.showMessage("La fecha final no puede ser inferior a la inicial", Me.Page, CFRConst.msgEnum.errorType)
                Exit Sub
            End If

            getRequestsByDay(Me.hotel.id, datein, dateOut)

        Catch ex As Exception
            cfr.pages.SiteBasePage.showMessage(ex.Message, Me.Page, CFRConst.msgEnum.errorType)
        End Try
    End Sub
End Class
