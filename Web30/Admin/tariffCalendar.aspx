<%@ Page Title="CFR-Tarifas Calendario" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="tariffCalendar.aspx.vb" Inherits="Admin_tariffCalendar" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
    <style>
        .calchg0 {
            color: #d43f3a;
        }
        .table > thead > tr > td.rowselected,
        .table > tbody > tr > td.rowselected,
        .table > tfoot > tr > td.rowselected,
        .table > thead > tr > th.rowselected,
        .table > tbody > tr > th.rowselected,
        .table > tfoot > tr > th.rowselected,
        .table > thead > tr.rowselected > td,
        .table > tbody > tr.rowselected > td,
        .table > tfoot > tr.rowselected > td,
        .table > thead > tr.rowselected > th,
        .table > tbody > tr.rowselected > th,
        .table > tfoot > tr.rowselected > th {
        background-color: #5CB85C;
        }

        #calendar {
            width: 80%;
            margin-left: 350px;
            overflow-x:scroll;  
            overflow-y:visible;
            padding-bottom:1px;
        }

        #calendar .table{
            width:initial;
        }

        #calendar .table .cal-header-week{
           background-color: #CAEAE9;
        }
        #calendar .table .cal-header-week.ymclosed {
           background-color: #d9534f;
        }
        #calendar .table td.ymclosed {
           background-color: #d9534f;
        }
        #calendar .table td.ymclosed a{
            color: #fff;
        }
        .table-striped > tbody > tr:nth-child(odd) > td.ymclosed, .table-striped > tbody > tr:nth-child(odd) > th.ymclosed {
        background-color: #d9534f;
        }

        #calendar .table > thead > tr > th
        ,#calendar .table > tbody > tr > th
        ,#calendar .table > tfoot > tr > th
        ,#calendar .table > thead > tr > td
        ,#calendar .table > tbody > tr > td
        ,#calendar .table > tfoot > tr > td {
            padding-right: 3px;
            padding-left: 3px;
            padding-top: 6px;
            font-size: 14px;
            min-height:53px;
        }

        #calendar button{
            margin-top:0px;
            margin-bottom:0px;
            padding-top:2px;
            padding-bottom:2px;
        }

        .rowclick, .rowclick label {
                cursor: pointer; 
        }
        .rowclick:hover {
            background-color:  #5CB85C!important;
        }
        #calendar .freeze{
            width:350px;
        }
        #calendar .freeze1width{
            width:350px;
            min-width:350px
        }
        #calendar .roomtitlerow ,#calendar .roomtitlerow td{
            border-top-width: 3px;
            border-color: gold;
            border-top-style: solid;
            margin-top:-3px;/*needed for freeze column*/
        }

       
        .table-striped > tbody > tr:nth-child(odd) > td.calroomtitle,
        .table-striped > tbody > tr:nth-child(odd) > th.calroomtitle {
        background-color: gold;
        }

        td.calroomtitle,
        .calroomtitle  {
             width:350px;
             text-align:left;
             background-color:gold;
             height:78px;

        }

        td.availcss{
             background-color:gold;
             height:78px;
        }

        .calroomtitle input[type="checkbox"] {
             margin-right: 10px;
        }
        #calendar .price {
            width: 60px;
            text-align: right !important;
        }

        .modal-open .ui-datepicker{
            z-index:9999!important;/*needed for jquery calendar on bootstrap modal*/
        }

        .day-range label{
            display:inline-block; margin-right: 10px; /*for calendar*/ 
        }

        .roomcapacity {
                width: 45px; font-style: normal; font-size: 11px; color: #0D8E0D;
        }
        .roommaxavailability{
                font-style: normal; font-size: 10px; color: #0D8E0D;
        }

        #calendar td.tariffnamerow{
            height:55px;
        }

        #calendar table > tbody > tr > td:first-child{
            text-align:left;
        }
        #calendar table > tbody > tr > td:first-child{
            border-left-color: gold;
            border-left-width: 2px;
            border-left-style: dotted;
        }

        #calendar td.tariffgroup{
            padding-top:2px;
        }
        #calendar td.td-release{
            padding-bottom: 20px;
        }
        
        #calendar .tariffcode{
                text-align: left;
                float: left;
                font-size: 80%;
        }
        /*div for close link*/
        .fk-closeymdaydiv{ 
            margin-bottom: 0px;
            margin-top: 16px;
            padding-bottom: 3px;
        }


        .btn-unit-option{
            position: absolute;
            bottom: 5px;
            right: 0px;
        }
        .availcss-div{
                position: relative;
                bottom: 0px;
        } 

        .cal-availability .txt-mindays ,.cal-availability .txt-release{
            font-weight:normal;
            width:40px;
            border-style: outset;
            border-width: 1px;
        }
        .txt-mindays {
            border-color: red;
        }
         .txt-release {
            border-color: deepskyblue;
        }

         .pricecss-div{
             margin-top:20px;
         }
         .fk-toggletariffrow{
             width:100%;
             display:inline-block;
         }
         .fk-toggletariffrow:hover{
             background-color:gold;
         }
         .fk-tariffname{
             padding-left:5px;

         }

    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
    <input type="hidden"
           name="<%=AdminRequestSecurity.FormFieldName%>"
           value="<%=HttpUtility.HtmlAttributeEncode(Me.AntiForgeryToken)%>" />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnsaveavailability" />
                        </Triggers>
                    </asp:UpdatePanel>

    <div class="container">
        <h1 class="sectiontitle" style="display:inline-block;"><i class="fa fa-table"></i> Calendario de tarifas</h1>
        <span  class="msgInfo availabilitychange" style="display: none;margin-left: 120px;">Se han producido cambios en la disponibilidad. Recuerde pulsar en guardar.</span>
        <div class="separator-2"></div>
    </div>

    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-lg-8">
                <div id="section1" class="clearfix">
                    
                    <div class="form-stacked">
                         <div class="form-group col-xs-12 col-sm-5">
                            <label class="sflabel control-label" for="lstclients">Ver datos para el operador/contrato</label>
                            <div class="input-group ">
                                <div class="input-group-addon">
                                    <span class="fa fa-users"></span>
                                </div>
                                <asp:DropDownList ID="lstclients" runat="server" CssClass="form-control" AutoPostBack="True" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group col-xs-12 col-sm-5">
                            <label for="cbomonth" class="sflabel control-label">Periodo que desea modificar</label>
                            <div class="row">
                                <div class="col-xs-9 col-lg-7">
                                     <div class="input-group ">
                                <div class="input-group-addon">
                                    <span class="fa fa-calendar"></span>
                                </div>
                                    <asp:DropDownList ID="cbomonth" runat="server" CssClass="form-control " AutoPostBack="True" ClientIDMode="Static"></asp:DropDownList>
                                         </div>
                                    </div>
                                <div class="col-xs-3 col-lg-3 ">
                                    <asp:LinkButton ID="btnSeeAvailability" runat="server" cssclass="btn btn-default" role="button">Ver</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                      
                    </div>
                </div><%'section 1 %>
            </div>
        </div>
        <div class="row hidden">
            <div class="col-xs-12 col-sm-8 col-lg-8">
                                <div id="section2" class="clearfix" style="">
                                    <h3 class="sectionoption"><i class="fa fa-wrench"></i> Cambie la disponibilidad con estas herramientas o manualmente</h3>
                                    <div class="form-stacked">

                                        <div class="form-group ">
                                            <label class="sflabel control-label" for="cboday1">Del dia</label>
                                            <asp:DropDownList ID="cboday1" runat="server" CssClass="form-control " ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                        <div class="form-group ">
                                            <label class="sflabel control-label" for="cboday2">Al dia?</label>
                                            <asp:DropDownList ID="cboday2" runat="server" CssClass="form-control " ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                        <div class="form-btn ">
                                            <a href="#" id="removenewavailability" onclick="setnewvalue(-1);" class="btn btn-danger" data-toggle="tooltip" title="Reducir disponibilidad a las unidades seleccionadas entre los dias seleccionados." >Quitar<span class="hidden-sm"> disponibilidad</span>  <i class="fa fa-minus-square "></i></a>
                                        </div>


                                        <div class="form-group ">
                                            <label class="sflabel control-label" for="txtdays2add">Disponibilidad</label>
                                            <asp:TextBox ID="txtdays2add" runat="server" type="number" MaxLength="3" max="100" min="-100" Text="1" CssClass="form-control onlynum admin-tool" Width="80px" ClientIDMode="Static" data-minlength="1" required></asp:TextBox>
                                        </div>
                                        <div class="form-btn ">
                                            <a href="#" id="addnewavailability" onclick="setnewvalue(+1);" class="btn btn-success" data-toggle="tooltip" title="Añadir disponibilidad a las unidades seleccionadas entre los dias seleccionados." >Añadir<span class="hidden-sm"> disponibilidad</span>  <i class="fa fa-plus-square"></i></a>
                                        </div>
                                        <div class="form-group ">
                                            
                                       
                                        <div class="alert alert-warning alert-dismissible hidden" role="alert" id="alertnumdias">
                                            <button type="button" class="close" data-dismiss="alert"><span>&times;</span></button>
                                            <strong>Aviso!</strong> Debes introducir la disponibilidad.
                                        </div>
                                        <div class="alert alert-warning alert-dismissible hidden" role="alert" id="alertselectroom">
                                            <button type="button" class="close" data-dismiss="alert"><span>&times;</span></button>
                                            <strong>Aviso!</strong> Debes seleccionar una unidad al menos.
                                        </div>
                                       </div>
                                    </div>
                                </div>
                            </div>

            </div>
        
        </div>
 
<div class="container-full">        

                <div id="calendardisp">
                        <div class="row">
                            <div class="col-xs-12" style="background-color: #f5f5f5;">
                                <h3 class="" style="display:inline-block ;background-color: #f5f5f5; padding-left: 10px; margin-top: 10px;margin-bottom:0px; padding-bottom: 10px;">
                                    <i class="fa fa-calendar"></i><asp:Literal ID="litSelectedMonth" runat="server"></asp:Literal>
                                </h3>
                                    
                                    <%--<span style="float:right;"><button type="button" class="btn btn-animated btn-danger btn-sm fk-btncloseymday" data-state="0">Ocultar cierre de ventas<i class="fa fa-eye-slash"></i></button></span>--%>
                                    <span style="float:right;"><asp:LinkButton ID="btnsaveavailability" OnClientClick="return btnsaveavailability_click(this)" runat="server" CssClass="btn btn-default btn-sm btn-block" data-toggle="tooltip" title="Pulsa aqui para guardar los cambios realizados manualmente." data-container="body">Guardar cambios <i class="fa fa-save"></i></asp:LinkButton></span>
                                    
                               
                            </div>
                        </div>
                  
                    <%--<asp:Literal ID="litroomcombined" runat="server"></asp:Literal>--%>
                   
                    <% 
                        'obtenemos la disponib del cliente 
                        Dim oRCupDV As DataView = Me.getClientCupo()
                        Dim ordtCal As CalendarDays = Me.getCalendarHeader
                    %>
                        <div class="outer-scrolling-table-container " style="">
                            <div id="calendar" class="scrolling-table-container" >
                                <table class="cal-availability table table-condensed sticky-column-table sticky-classic-table">
                                    <thead>
                                        <tr>
                                            <th class="freeze freeze1width calroomtitle chkroom_all" >
                                                   <%=Me.unitname%> +<br>combinaciones
                                               
                                            </th>

                                            <% For Each ordt As calendarDay In ordtCal.dayList

                                                    %>
                                            <th class="byday rowclick <%=getWeekendClass(ordt) %> fk-header-cal" onclick="return editymclose(this)" data-idday="<%=ordt.spanishDate  %>" data-toggle="tooltip" title="Pulsa aqui para crear cierres de ventas con mas opciones." data-container="body"><%=ordt.getWeekDayLetter(3)%><br>
                                                <%=ordt.getDayNum%></th>
                                            <% Next%>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%  


                                            For Each oRsHAB As DataRowView In Me.dvRooms
                                                Dim cod_room As Integer = oRsHAB(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel)
                                                Dim numRoomsAvailable As Integer = oRsHAB(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.numRoomsAvailable)
                                                Dim roomName As String = oRsHAB(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel)
                                                Dim PaxIncluidEnPrecio As Integer = oRsHAB(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.MaxPersonas).ToString
                                                Dim MaxAdultos As Integer = oRsHAB(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.MaxAdultos).ToString
                                                Dim strRoomCapacity As String = PaxIncluidEnPrecio & " <i class='fa fa-male'></i>"
                                                If PaxIncluidEnPrecio > MaxAdultos Then
                                                    Dim numBoys As Integer = PaxIncluidEnPrecio - MaxAdultos
                                                    strRoomCapacity += "+" & numBoys & "<i class='fa fa-child' /></i>"
                                                End If
                                                Dim rowHeightStr As String = ""
                                                If roomName.Length > 40 Then
                                                    rowHeightStr = " style='height:60px;' "
                                                Else
                                                    rowHeightStr = ""
                                                End If
                                        %>
                                        <tr id="rowroom_<%=cod_room%>" <%=rowHeightStr %> class="roomtitlerow" >
                                            <td  class="freeze calroomtitle " id="headroom_<%=cod_room%>" data-id="<%=cod_room%>" data-unitid="<%=cod_room %>">
                                                <div>
                                                <span class="tariffcode label label-default" data-toggle="tooltip" title="Codigo de unidad"><%=cod_room %></span> <span class="roommaxavailability" data-toggle="tooltip" title="Numero maximo(recomendado) de unidades de este tipo que se pueden introducir para cada dia. Puede modificarlo en el tipo de habitación. Un numero superior al limite puede generar overbooking">(<%=numRoomsAvailable%>)</span>
                                                <span class="roomcapacity" data-toggle="tooltip" title="Numero de personas que admite esta unidad."><%=strRoomCapacity%></span>
                                                <span class="fk-unitname"  ><%=roomName %></span>
                                                <%=Me.getRoomCombinedIU(cod_room)%>
                                                </div>

                                                <span class="btn-unit-option"><button type="button" class="btn btn-default-transparent btn-sm " onclick="return editunitsavailability(this)" data-unitid="<%=cod_room %>" data-toggle="tooltip" title="Editar la disponibilidad en un intervalo." >Disponibilidad <i class="fa fa-edit"></i></button></span>
                                            </td>
                                            <% 'days

                                                Dim idisp As String = ""
                                                Dim xroom As Integer = 1
                                                Dim ymCloseVal As Integer = 0
                                                Dim ymCloseDesc As String = "Cerrar"
                                                Dim ymCloseClass As String = ""

                                                For Each ordt As calendarDay In ordtCal.dayList
                                                    Dim class2Add As String = ""
                                                    Dim idia As Integer = ordt.getDayNum
                                                    Dim dateFor As Date = New Date(Me.yearSel, ordt.getMonth, idia)
                                                    Dim filterString As String = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, " cod_habitacion={0}  AND dia=#{1}# ", cod_room, dateFor)

                                                    oRCupDV.RowFilter = filterString

                                                    Dim ymid As String = cod_room & "_" & ordt.spanishDate
                                                    If oRCupDV.Count = 0 Then
                                                        idisp = "0"
                                                        class2Add = "calchg0"
                                                        ymCloseVal = "0"
                                                        ymCloseDesc = "Cerrar"
                                                        ymCloseClass = ""
                                                    Else
                                                        Dim orCUP As DataRowView = oRCupDV(0)
                                                        idisp = orCUP("DISPONIBLE")
                                                        ymCloseVal = cfr.tool.Tools.cNull(orCUP("closed"), 0)
                                                        If ymCloseVal = 0 Then
                                                            ymCloseDesc = "Cerrar"
                                                            ymCloseClass = ""
                                                        ElseIf ymCloseVal = -1 Then
                                                            ymCloseDesc = "Abrir"
                                                            ymCloseClass = "ymclosed"
                                                        End If
                                                    End If

                                                    Dim txtId As String = String.Format("h{0}d{1}", cod_room, ordt.getDayNum)

                                            %>
                                            <td class="availcss <%=getWeekendClass(ordt)%> <%=ymCloseClass %>">
                                                <div class="fk-closeymdaydiv"  data-toggle="tooltip" title="Pulsa aqui para abrir o cerrar a la venta esta unidad" > 
                                                    <a class="fk-closeymday" href="#" onclick="return false;" data-ymid="<%=ymid%>" data-ymclose="<%=ymCloseVal %>"><%=ymCloseDesc %></a>
                                                </div>
                                                <div class="availcss-div">
                                               
                                                <input type='number' class="onlynum calchg <%=class2Add%>" name="<%=txtId%>" id='<%=txtId%>' size='2' maxlength='3' min="0" max="<%=numRoomsAvailable%>" value="<%=iDISP%>">

                                                </div>
                                            </td>
                                            <%
                        xroom += 1
                    Next
                                            %>
                                        </tr>

                                        <% 'rows for tariff
                Dim dvTariff = Me.getDvTariffList(cod_room)
                                        %>
                                        <%
                                            For Each tar In dvTariff
                                        %>
                                        <tr>
                                            <td id="tarunit_<%=cod_room %>_<%=tar.cod_tarifa %>" class="freeze tariffnamerow tariffgroup" >
                                                <a href="#" class="fk-toggletariffrow chevronviewa chevronopena " data-tarunit="<%=cod_room %>_<%=tar.cod_tarifa %>" data-toggle="tooltip" title="Pulse para editar los dias minimos y el release">
                                                    <span class="tariffcode label label-default"><%=tar.cod_tarifa %></span> <strong> <span class="fk-tariffname"><%=tar.tarifa %></span></strong>
                                                </a>
                                                <span class="btn-unit-option"><button type="button" class="btn btn-default-transparent btn-sm " onclick="return edittariffpricerange(this)" data-cod_tarifa="<%=tar.cod_tarifa %>" data-unitid="<%=cod_room %>" data-toggle="tooltip" title="Editar el precio de la tarifa para esta unidad en un intervalo determinado y con mas opciones" data-container="body">Precio <i class="fa fa-pencil"></i></button></span>
                                            </td>
                                            <%

                                                Dim dvTariffByDay = Me.getDvTariffPriceList(tar.id_Tar_Hab)
                                                For Each dayPriceCol As R_PriceDayEF In dvTariffByDay

                                                    Dim tarIdstr As String = String.Format("tarcode_{0}_{1}_{2}_{3}", tar.id_Tar_Hab, dayPriceCol.id, dayPriceCol.dayprice.ToString("yyyy-MM-dd"), dayPriceCol.price)
                                            %>
                                            <td class="tariffnamerow <%=getWeekendClass(dayPriceCol.dayprice)%>">
                                                <div class="pricecss-div">
                                                <input type='text' class="onlynum calchg price" name="<%=tarIdstr%>" id='<%=tarIdstr%>' size='5' min="0" value="<%=dayPriceCol.price  %>" autocomplete="off">
                                                    </div>
                                            </td>
                                            <%   Next ' for each day in tariff by each unit
                                                %>
                                        </tr>

                                        <tr class="tarunit_opt_<%=cod_room %>_<%=tar.cod_tarifa %>" style="display:none">
                                            <td class="freeze tariffgroup"><span style="float:right;"><button type="button" class="btn btn-default-transparent btn-sm " onclick="return edittariffmindaysrange(this)" data-cod_tarifa="<%=tar.cod_tarifa %>" data-unitid="<%=cod_room %>" data-toggle="tooltip" title="Editar los dias minimos para poder hacer una reserva con esta tarifa." >Dias minimos <i class="fa fa-edit"></i></button></span></td>
                                            <%

                                                For Each dayPriceCol As R_PriceDayEF In dvTariffByDay

                                                    Dim tarIdstr As String = String.Format("tarcodeminday_{0}_{1}_{2}_{3}", tar.id_Tar_Hab, dayPriceCol.id, dayPriceCol.dayprice.ToString("yyyy-MM-dd"), dayPriceCol.price)
                                            %>
                                            <td class="<%=getWeekendClass(dayPriceCol.dayprice)%>">
                                                <input type='text' class="txt-mindays onlynum calchg " name="<%=tarIdstr%>" id='<%=tarIdstr%>' size='5' min="0" value="<%=dayPriceCol.minimumdays   %>" autocomplete="off">
                                            </td>
                                            <%   Next ' for each day in tariff by each unit
                                                %>
                                        </tr>
                                        <tr class="tarunit_opt_<%=cod_room %>_<%=tar.cod_tarifa %>" style="display:none; margin-bottom:10px;">
                                             <td class="freeze tariffgroup"><span style="float:right;"><button type="button" class="btn btn-default-transparent btn-sm " onclick="return edittariffreleaserange(this)" data-cod_tarifa="<%=tar.cod_tarifa %>" data-unitid="<%=cod_room %>" data-toggle="tooltip" title="Editar los dias de antelación para poder acceder a la tarifa." >Release <i class="fa fa-edit"></i></button></span></td>
                                            <%

                                                For Each dayPriceCol As R_PriceDayEF In dvTariffByDay

                                                    Dim tarIdstr As String = String.Format("tarcoderelease_{0}_{1}_{2}_{3}", tar.id_Tar_Hab, dayPriceCol.id, dayPriceCol.dayprice.ToString("yyyy-MM-dd"), dayPriceCol.price)
                                            %>
                                            <td class="td-release <%=getWeekendClass(dayPriceCol.dayprice)%>">
                                                <input type='text' class="txt-release onlynum calchg " name="<%=tarIdstr%>" id='<%=tarIdstr%>' size='5' min="0" value="<%=dayPriceCol.release   %>" autocomplete="off">
                                            </td>
                                            <%   Next ' for each day in tariff by each unit
                                                %>
                                        </tr>
                                        <%
                                            Next ' For Each tar for each unit
                                            %>

                                        <%
                    Next 'for each unit
                                            %>

                                    </tbody>
                                </table>

                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-hidden col-lg-2">
                            </div>
                            <div class="col-xs-6 col-lg-4">
                                
                            </div>
                            <div class="col-xs-hidden col-sm-1">
                                </div>
                            <div class="col-sm-5">
                                <p  class="info availabilitychange" style="display: none;">Se han producido cambios en la disponibilidad. Recuerde pulsar en guardar.<br />
                                    Para cancelar los cambios vuelva a cargar los datos.</p>
                            </div>
                        </div>
                    </div>

    </div>
    <div class="container">
        <div class="footerinfo">
         <h3>Información </h3>
        <ul>
            <li>Puede modificar la disponibilidad manualmente en cada casilla.
            </li>
            <li>Si no se muestra alguna tarifa en el panel es porque no se habra asociado al operador</li>
            <li>
                Recuerde que la disponibilidad asignada a "Venta Libre" será usada por cualquier operador una vez que han terminado el cupo asignado a cada uno.
            </li>
            <li>Recuerde que si modifica datos en la pantalla manualmente(en las casillas) deberá guardar los cambios antes de ejecutar cualquier otra acción.</li>

        </ul>           
        </div>
    </div>
        
    <%'Container %>

    <div class="modal " id="modal_tarcal_editrange" tabindex="-1" role="dialog" data-backdrop="static">
        <div class="modal-dialog modal-md" style="min-width: 650px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Editar precio de la tarifa para el intervalo</h4>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hddmodaledittariffprice_tariff_cod_tarifa" runat="server" ClientIDMode="static" />
                    <asp:HiddenField ID="hddmodaledittariffprice_operation" runat="server" ClientIDMode="static" />

                    <div  class="form-group modalunitselect">
                        <label class="sflabel control-label" for="lstrooms">Aplicar a la unidad</label>
                        <div class="input-group ">
                            <div class="input-group-addon">
                                <span class="fa fa-asterisk"></span>
                            </div>
                            <asp:DropDownList ID="cbounit2changeprice" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="input-daterange" id="datepicker" style="display: inline-block;">
                        <div class="form-group padleftclear">
                            <label class="sflabel control-label" for="checkInDate">Desde el dia</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </div>
                                <asp:TextBox ClientIDMode="static" ID="checkindate" runat="server" CssClass="checkindate form-control sfinput dtpreadonly " MaxLength="10" AutoComplete="off"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label for="checkOutDate" class="sflabel control-label">Hasta</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </div>
                                <asp:TextBox ClientIDMode="static" ID="checkoutdate" runat="server" CssClass="checkoutdate date-picker form-control sfinput dtpreadonly" MaxLength="10" AutoComplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="fk-tariffedit">
                            <div id="day_range_seletedweekdays" class="form-group">
            <h4 class="h-section"><i class="fa fa-calendar-plus-o"></i> Seleccione que dias va a modificar</h4>
            <div class="day-range form-options">
                <div class="checkbox">
                    <label class="checkbox" for="weekday1">
                        <asp:CheckBox runat="server" ID="weekday1" name="week_valid_days" value="1" Checked="True" ClientIDMode="Static" />
                        Lunes
                    </label>
                    <label class="checkbox" for="weekday2">
                        <asp:CheckBox runat="server" ID="weekday2" name="week_valid_days" value="2" Checked="True" ClientIDMode="Static" />
                        Martes
                    </label>
                    <label class="checkbox" for="weekday3">
                        <asp:CheckBox runat="server" ID="weekday3" name="week_valid_days" value="3" Checked="True" ClientIDMode="Static" />
                        Miércoles
                    </label>
                    <label class="checkbox" for="weekday4">
                        <asp:CheckBox runat="server" ID="weekday4" name="week_valid_days" value="4" Checked="True" ClientIDMode="Static" />
                        Jueves
                    </label>
                    <label class="checkbox" for="weekday5">
                        <asp:CheckBox runat="server" ID="weekday5" name="week_valid_days" value="5" Checked="True" ClientIDMode="Static" />
                        Viernes
                    </label>
                    <label class="checkbox" for="weekday6">
                        <asp:CheckBox runat="server" ID="weekday6" name="week_valid_days" value="6" Checked="True" ClientIDMode="Static" />
                        Sábado
                    </label>
                    <label class="checkbox" for="weekday7">
                        <asp:CheckBox runat="server" ID="weekday7" name="week_valid_days" value="7" Checked="True" ClientIDMode="Static" />
                        Domingo
                    </label>
                </div>
            </div>

        </div>

                   <div class="form-group">
                            <h4 class="h-section"> <i class="fa fa-euro"></i>  Precio para los dias seleccionados</h4>
                            <div class="checkbox">
                                <label class="checkbox" for="chkprice"  data-toggle="tooltip" title="Si desmarca esta opción el precio no se modificará."><asp:CheckBox ID="chkprice" runat="server" Checked="False" ClientIDMode="Static"  /> Seleccione esta opción si desea modificar el precio</label>
                            </div>
                            <div style="display:inline-block;float:left;">
            
                            <div style="display:inline-block;float:left;">
                            <div class="input-group " style="width: 160px;">
                                <div class="input-group-addon">
                                    <span class="fa fa-euro"></span>
                                </div>
                                <asp:TextBox ID="txtmodaledittariffprice" runat="server" ClientIDMode="Static" CssClass="form-control onlynumsign" Width="120px">0</asp:TextBox>

                            </div>
                            </div>
                               <i class="fa fas-2 fa-info-circle " data-toggle="tooltip" title="Si introduce un valor negativo se reducirá el valor del precio. Si al reducir es menor que 0 el precio pasará a ser 0 y la tarifa para ese dia no podrá ser cargada."></i>
        
                            </div>
                            <div style="display:inline-block;">
                            <div class="form-options">
                                <div class="checkbox">
                                    <label class="checkbox" for="optpricepercent">
                                        <asp:RadioButton runat="server" ID="optpricepercent" GroupName="optprice" value="1" ClientIDMode="Static"  />
                                        Aumentar/disminuir el precio en porcentaje
                                    </label>
                                                        </div>
                                <div class="checkbox">
                                    <label class="checkbox" for="optpricevalue">
                                        <asp:RadioButton runat="server" ID="optpricevalue" GroupName="optprice" value="2" ClientIDMode="Static" Checked="True" />
                                        Aumentar/disminuir el precio en valor
                                    </label>
                                                        </div>
                                <div class="checkbox">
                                    <label class="checkbox" for="optpricesubst">
                                        <asp:RadioButton runat="server" ID="optpricesubst" GroupName="optprice" value="3" ClientIDMode="Static" />
                                        Sustituir por el nuevo valor
                                    </label>
                                </div>
                            </div>
                            </div>
                </div>
                        <div class="form-group">
                            <h4 class="h-section fk-mindayreleasetit"> Dias minimos para los dias seleccionados</h4>
                            <div class="checkbox">
                                <label class="checkbox" for="chkmindays"  data-toggle="tooltip" title="Si desmarca esta opción no se modificarán los dias minimos."><asp:CheckBox ID="chkmindays" runat="server" Checked="false" ClientIDMode="Static"  /> Seleccione esta opción si desea modificar el minimo de dias</label>
                            </div>
                            <div class="input-group " style="width: 160px;">
                                <div class="input-group-addon">
                                    <span class="fa fa-calendar-o"></span>
                                </div>
                                <asp:TextBox ID="txtmodaledittariffminimumdays" runat="server" ClientIDMode="Static" CssClass="form-control onlynum" Width="100px">0</asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <h4 class="h-section fk-mindayreleasetit"> Release (dias de antelación para poder reservar)</h4>
                             <div class="checkbox">
                                <label class="checkbox" for="chkreleaseedit"  data-toggle="tooltip" title="Si desmarca esta opción no se modificará el release."><asp:CheckBox ID="chkreleaseedit" runat="server" Checked="false" ClientIDMode="Static"  /> Seleccione esta opción si desea modificar el release</label>
                            </div>
                            <div class="input-group " style="width: 160px;">
                                <div class="input-group-addon">
                                    <span class="fa fa-calendar-o"></span>
                                </div>
                                <asp:TextBox ID="txtmodaledittariffrelease" runat="server" ClientIDMode="Static" CssClass="form-control onlynum" Width="100px">0</asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="fakecontainer"></div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <asp:LinkButton ID="btnmodaledittariffpricesave" runat="server" CssClass="btn btn-primary" Style="display:none;" ClientIDMode="Static" OnClientClick="showprogress(this,1);">Guardar cambios <i class="fa fa-save"></i></asp:LinkButton>

                </div>
            </div>
        </div>
    </div>
    

    <div id="hiddencontainer" class="hidden">
    <div id="edittariffpricediv">

    </div>
    <div id="editunitavailabilitydiv">
        <asp:HiddenField ID="hddunitselectedforavailability" runat="server" ClientIDMode="static" />
        <div class="form-group">
            <label class="checkbox" for="txtmodaleditavailability">Disponibilidad</label>
            <div class="input-group " style="width: 160px;">
                <div class="input-group-addon">
                    <span class="fa fa-calendar-o"></span>
                </div>
                            <asp:TextBox ID="txtmodaleditavailability" runat="server" ClientIDMode="Static" CssClass="form-control onlynum" Width="120px">0</asp:TextBox>
                                </div>

            
        </div>
        <div class="form-group">
            <h4 class="h-section">Seleccione que acción aplicar</h4>
            <div class="form-options">
                <div class="checkbox">
                    <label class="checkbox" for="optavailabilityadd">
                        <asp:RadioButton runat="server" ID="optavailabilityadd" GroupName="optavailability" value="1" Checked="True" ClientIDMode="Static"  />
                        Aumentar la disponibilidad
                    </label>
                                        </div>
                <div class="checkbox">
                    <label class="checkbox" for="optavailabilityreduce">
                        <asp:RadioButton runat="server" ID="optavailabilityreduce" GroupName="optavailability" value="2" ClientIDMode="Static" />
                        Reducir la disponibilidad
                    </label>
                                        </div>
                <div class="checkbox">
                    <label class="checkbox" for="optavailabilitysubst">
                        <asp:RadioButton runat="server" ID="optavailabilitysubst" GroupName="optavailability" value="3" ClientIDMode="Static" />
                        Sustituir por el nuevo valor
                    </label>
                </div>
            </div>

        </div>
        <asp:LinkButton ID="btnsaveunitavailability" runat="server" CssClass="btn btn-primary" ClientIDMode="Static" OnClientClick="showprogress(this,1);" > Guardar Disponibilidad <i class="fa fa-save"></i></asp:LinkButton>
    </div>
    <div id="editymclosediv">
            <div class="form-group">
                <label class="sflabel control-label" for="cbooperatorsymclose">Aplicar al cliente</label>
                <div class="input-group ">
                    <div class="input-group-addon">
                        <span class="fa fa-users"></span>
                    </div>
                    <asp:DropDownList ID="cbooperatorsymclose" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="sflabel control-label" for="lstrooms">Aplicar a las unidades</label>
                <div class="input-group ">
                    <div class="input-group-addon">
                        <span class="fa fa-asterisk"></span>
                    </div>
                    <asp:DropDownList ID="lstrooms" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                </div>
            </div>
          <div class="form-group">
            <h4 class="h-section">Seleccione que acción aplicar</h4>
            <div class="form-options-inline">
                <div class="checkbox">
                    <label class="checkbox bg-danger" for="rdbymclose">
                        <asp:RadioButton runat="server" ID="rdbymclose" GroupName="optymclosesell" value="1" Checked="True" ClientIDMode="Static"  />
                        <span>Cerrar la venta</span>
                    </label>
                    </div>
                <div class="checkbox">
                    <label class="checkbox" for="rdbymopen">
                        <asp:RadioButton runat="server" ID="rdbymopen" GroupName="optymclosesell" value="0" ClientIDMode="Static" />
                        Abrir la venta
                    </label>
                </div>
            </div>

        </div>

            <asp:LinkButton ID="btnsaveymclose" runat="server" CssClass="btn btn-primary" ClientIDMode="Static" OnClientClick="showprogress(this,1);" > Crear cierre de ventas <i class="fa fa-save"></i></asp:LinkButton>
        </div>

       


    </div>
   



</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
    <script>
        $(document).ready(function () {
            console.log("tooltip-enabled")
            $('[data-toggle="tooltip"]').tooltip();
            $('.fk-header-cal').prop('tooltipText', 'Pulse aqui para crear cierres de ventas globales.');
            $(".fk-toggletariffrow").click(function(e){
                e.preventDefault();
                var rowtar=$('.tarunit_opt_'+$(this).attr('data-tarunit'))
                $(rowtar).toggle();
                
                $(this).toggleClass('chevronopena chevronclosea');
            });
        });

        function showprogress(sender) {
            return showprogress(sender, 0);
        }
        function showprogress(sender, option) {
            var width = sender.width;
            $(sender).before("<div style='display:inline-block; width:" + $(sender).width() + "px;'><i class='fa fa-spinner fa-spin fa-2x fa-fw margin-bottom'></i></div>");

            if (option == 1) {
                $(sender).attr('disabled', true);
                $(sender).hide();
            }
            return true;
        }
        
        var editymclose = function (sender) {
            var dayselected = $(sender).data("idday");
            
            $("#checkindate").val(dayselected);
            $("#checkoutdate").val(dayselected);
            $("#cbooperatorsymclose").val($("#lstclients").val());
            
            configuremodal("modal_tarcal_editrange", "Crear cierre de ventas ");

            $("#modal_tarcal_editrange .modal-body .fk-tariffedit").hide();
            $("#modal_tarcal_editrange .modalunitselect").hide();

            $("#modal_tarcal_editrange .modal-body .fakecontainer").append($("#editymclosediv"));
            $("#modal_tarcal_editrange .modal-footer").append($("#btnsaveymclose"));

            var modal = $("#modal_tarcal_editrange");
            modal.appendTo("form");
            modal.modal("show").on('hidden.bs.modal', function () {
                $("#hiddencontainer").append($("#modal_tarcal_editrange .modal-body .fakecontainer").children());
                $("#editymclosediv").append($("#btnsaveymclose"));//return button to div
            })
        };

        function edittariffpricerange(sender){
            $("#chkprice").attr("checked",true);
            $("#chkmindays").attr("checked",false);
            $("#chkreleaseedit").attr("checked",false);
            return edittariff(sender)
        }
        function edittariffmindaysrange(sender){
            $("#chkmindays").attr("checked",true);
            $("#chkreleaseedit").attr("checked",false);
            $("#chkprice").attr("checked",false);
            return edittariff(sender)
        }
        function edittariffreleaserange(sender){
            $("#chkreleaseedit").attr("checked",true);
            $("#chkmindays").attr("checked",false);
            $("#chkprice").attr("checked",false);
            return edittariff(sender)
        }

        function edittariff(sender) {
            var tariff_cod_tarifa = $(sender).data("cod_tarifa");
            var unit_id = $(sender).data("unitid");
            $("#cbounit2changeprice").val(unit_id);
            var tdpar= $("#tarunit_"+unit_id+"_"+ tariff_cod_tarifa);
            $("#hddmodaledittariffprice_tariff_cod_tarifa").val(tariff_cod_tarifa);
            
            configuremodal("modal_tarcal_editrange", "Editar tarifa: <strong>" + $(tdpar).find(".fk-tariffname").html() + "</strong>");
            var modal = $("#modal_tarcal_editrange");
            modal.appendTo("form");
            $("#modal_tarcal_editrange .modalunitselect").show();
            $("#modal_tarcal_editrange .modal-body .fk-tariffedit").show();
            $("#btnmodaledittariffpricesave").show();

            //$("#modal_tarcal_editrange .modal-footer").append($("#btnmodaledittariffpricesave"));
            modal.modal("show").on('hidden.bs.modal', function () {
                //$("#edittariffpricediv").append($("#btnmodaledittariffpricesave"));//return button to div
                $("#modal_tarcal_editrange .modal-body .fk-tariffedit").hide();
                $("#btnmodaledittariffpricesave").hide();
            })
        };


        var editunitsavailability = function (sender) {
            var unit_id = $(sender).data("unitid");
            $("#cbounit2changeprice").val(unit_id);

            var rowpar= $("#headroom_" + unit_id);
            $("#hddunitselectedforavailability").val(unit_id);
            configuremodal("modal_tarcal_editrange", "Cambiar disponibilidad para <strong>" + $(rowpar).find(".fk-unitname").html() + "</strong>");
            var modal = $("#modal_tarcal_editrange");
            modal.appendTo("form");
            $("#modal_tarcal_editrange .modalunitselect").show();
            $("#modal_tarcal_editrange .modal-body .fk-tariffedit").hide();
            $("#modal_tarcal_editrange .modal-body .fakecontainer").append($("#editunitavailabilitydiv"));
            $("#modal_tarcal_editrange .modal-footer").append($("#btnsaveunitavailability"));
            modal.modal("show").on('hidden.bs.modal', function () {
                $("#hiddencontainer").append($("#modal_tarcal_editrange .modal-body .fakecontainer").children());
                $("#editunitavailabilitydiv").append($("#btnsaveunitavailability"));//return button to div
            })


            
            //document.getElementById("btnmastershowbook").click();
        }





        function setnewvalue(signo) {
            var $checks = $('.chkroom');
            var atleastoneischecked = false;
            $.each($checks, function () {
                var chkselected = $(this);
                if (chkselected.is(':checked')) {
                    atleastoneischecked = true;
                    var roomid = $(this).data("id");
                    setnewvalue1(signo, roomid); 
                }
            
            });
            if(atleastoneischecked == false) {
                $('#alertselectroom').removeClass('hidden');
                return;               
            }

        }

        function setnewvalue1(signo, thab) {
            //var tHab = $('#cboroom').val();
            var ded = parseInt($('#cboday1').val());
            var ald = parseInt($('#cboday2').val());
            var vv = parseInt($('#txtdays2add').val());
            if (isNaN(vv)) {
                $('#alertnumdias').removeClass('hidden');
                return;
            } else {
                $('#alertnumdias').addClass('hidden');
            }

            if (ded > ald) {
                var aa = ald;
                ald = ded;
                ded = aa;
            }
            
            for (i = ded; i <= ald; i++) {
                var room = $('#h' + thab + 'd' + i);
                if (room != null) {
                    var dfhab = parseInt(room.val());
                    //if (dfhab == '') dfhab = 0;
                    if (isNaN(parseInt(dfhab, 10))) dfhab = 0;
                    var val2add = signo * vv;

                    var ndisp = dfhab + val2add;
                    if (ndisp < 0) ndisp = 0;
                    console.log(ndisp);
                    room.val(ndisp);
                }

            }
            $('.availabilitychange').show();
        }

        $("#txtdays2add").on('invalid.bs.validator', function (event) {
            //event.preventDefault();
            var rel = $(event.relatedTarget); // Button that triggered the modal
            var itemid = rel.data('objid'); // Extract info from data-* attributes
        });

        $('#cboroom').change(function () {
            var roomid = parseInt($('#cboroom').val());
            $('.calroomtitle').removeClass('calroomtitle-selected');           
            $('#rowroom_' + roomid).addClass('success');
        });

        $('.calchg').on('input', function () {
            var value = $(this).val();
            if (value != null) {
                value = parseInt(value);
                if (value==0) {
                    $(this).css('color', '#d43f3a');
                }else {
                    
                }
            }
                
            $('.availabilitychange').show();
        });

        $('.calchg').change(function () {
            var value = $(this).val();
            if (value != null) {
                value = parseInt(value);
                if (value == 0) {
                    $(this).addClass('calchg0');
                } else {
                    $(this).removeClass('calchg0');
                }
            }
        });

        $('.fk-btncloseymday').on('click', function () {
            var st=$('.fk-btncloseymday').data("state");
            if (st=="0"){
                $('.fk-btncloseymday').html("Mostrar cierre de ventas");
                $('.fk-btncloseymday').data("state","1");
            }
            else{
                $('.fk-btncloseymday').html("Ocultar cierre de ventas");
                $('.fk-btncloseymday').data("state","0");
            }
            $(".fk-closeymdaydiv").toggle();
        });
        

        $('.fk-closeymday').on('click', function () {
            var ymsender=this;
            var tdparent=$(ymsender).parent().parent();
            $(ymsender).hide();
            $(ymsender).parent().append("<i class='fa fa-spinner fa-spin'></i>");

            var ymid = $(ymsender).data("ymid"); //"2016-05-15";
            var ymclose = $(ymsender).data("ymclose"); //"2016-05-15";
            var operatorid=<%=Me.cod_operadorAvalibilitySelected %>;
            
            var obj = {};
            obj.ymid =  $(ymsender).data("ymid"); //"2016-05-15";
            obj.ymclose = $(ymsender).data("ymclose"); //"2016-05-15";
            obj.operatorid = <%=Me.cod_operadorAvalibilitySelected %>;
            var jsonData = JSON.stringify(obj);

            var ymcloseclass=""
            var ymnewvalue;
            if (ymclose==0){
                ymnewvalue=-1;
                ymcloseclass="ymclosed";
            }else{
                ymnewvalue=0;
                ymcloseclass="ymclosed";
            }
            
                
                $.ajax({
                    type: "POST",
                    url: "<%=linkcnf("ymclosehandler.ashx")%>" ,
                    data: {
                        ymid: "" + ymid + "",
                        operatorid: "" + operatorid + "",
                        ymclose: "" + ymclose + "",
                        "<%=AdminRequestSecurity.FormFieldName%>": "<%=HttpUtility.JavaScriptStringEncode(Me.AntiForgeryToken)%>"
                    },
                    success: function(server_response) 
                    {
                        $(ymsender).parent().find(".fa.fa-spinner.fa-spin").remove();
                        $(ymsender).html(server_response);
                        $(ymsender).data("ymclose",ymnewvalue);
                        $(ymsender).show();
                        if (ymnewvalue==0){
                            $(tdparent).removeClass ("ymclosed");
                        }else{
                            $(tdparent).addClass ("ymclosed");
                        }
                        
                    },
                    error: function (xhr, status, error) {
                        $(ymsender).parent().find(".fa.fa-spinner.fa-spin").remove();
                        $(ymsender).show();
                        //var err = eval("(" + xhr.responseText + ")");
                       var errstr= error;
                       showmessageerror( errstr );
                    }  

                }); 
							
                return false;
            });
        function btnsaveavailability_click(sender) {
            //$(".availabilitychange").hide();
            saveavailability();
            return false;
        }

        function saveavailability(){
            var $this = $(this);
            var frmValues = $("#aspnetForm").find("input,textarea,select").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            var monthsel = $("#cbomonth").val();
            var cod_operadorselected =$("#lstclients").val();
            showmessage("modalsuccess", "Guardando cambios de disponibilidad y tarifas", "Por favor, espere mientras se ejecuta la operación. <i class='fa fa-spinner fa-spin fa-2x'></i>")

            $.ajax({
                    type: "POST",
                    url: "<%=linkcnf("tariffcal.ashx")%>" ,
                data: frmValues ,
                //contentType:"application/json; charset=utf-8",
                //dataType:"json",
                    success: function(server_response) 
                    {
                        $(".availabilitychange").hide();
                        //$("#alertdiv").show();
                        //$("#alertmessage").html(server_response);
                        showmessage("modalsuccess", "Guardar cambios de disponibilidad y tarifas", server_response)
                    },
                    error: function (xhr, status, error) {
                        $(".availabilitychange").hide();
                        //var err = eval("(" + xhr.responseText + ")");
                       var errstr= error;
                       //showmessageerror( errstr );
                       showmessageerrorbyid("modalsuccess", "Se ha producido un error", "<div class='msgFail'>" + errstr + "</div>")
                    }  

                }); 
        }
        
        //$(document).on("click", ".roomclick", function (e) {
        //    var roomid = String($(this).data('id'));
        //    var chkselected = $("#chkroom_" + roomid);
        //    var checked = chkselected.prop("checked");
        //    chkselected.prop("checked", !checked);
        //    setmarkedrow(roomid,checked);

        //});

        //function setmarkedrow(roomid,checked ) {
        //    if (!checked) {
        //        $('#rowroom_' + roomid).addClass('rowselected');
        //        $('#alertselectroom').addClass('hidden');
        //    } else {
        //        $('#rowroom_' + roomid).removeClass('rowselected');
        //    }
        //}

        //$(document).on("click",".chkroom_all", function (e) {
        //    var ckall = $("#chkroom_all");
        //    var check = ckall.prop('checked');
        //    ckall.prop('checked', !check);

        //    checkall(!check)
        //});

        //function checkall(check) {
        //    var checks = $('.chkroom');
        //    $.each(checks, function () {
        //        $(this).prop("checked", check);
        //        var roomid = String($(this).data('id'));
        //        setmarkedrow(roomid, !check);
        //    });
        //}


        //$('.chkroom').change(function () {
        //    var ck = $(this);
        //    var check=ck.prop('checked');
        //    var roomid=ck.data('id');
        //   setmarkedrow(roomid,!check);
        //});


    </script>
</asp:Content>

