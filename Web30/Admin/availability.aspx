<%@ Page Title="CFR-Disponibilidad" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="availability.aspx.vb" Inherits="Admin_availability" %>
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

        #calendar .table{
            width:initial;
        }

        #calendar .table > thead > tr > th
        ,#calendar .table > tbody > tr > th
        ,#calendar .table > tfoot > tr > th
        ,#calendar .table > thead > tr > td
        ,#calendar .table > tbody > tr > td
        ,#calendar .table > tfoot > tr > td {
            padding-right: 3px;
            padding-left: 3px;
            font-size: 14px;
        }

        .rowclick, .rowclick label {
                cursor: pointer; 

        }
        .rowclick:hover {
            background-color:  #5CB85C!important;
        }
        .calroomtitle input[type="checkbox"] {
             margin-right: 10px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnsaveavailability" />
                        </Triggers>
                    </asp:UpdatePanel>

    <div class="container">
        <h1 class="sectiontitle"><i class="fa fa-table"></i> Disponibilidad</h1>
        <div class="separator-2"></div>
    </div>

    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-lg-8">
                <div id="section1" class="clearfix">
                    <h3 class="sectionoption">Seleccione el cliente, y el mes del que quiere modificar la disponibilidad </h3>
                    <div class="form-stacked">
                        <div class="form-group col-xs-12 col-sm-5">
                            <label class="sflabel control-label" for="lstClients">Cliente</label>
                            <div class="input-group ">
                                <div class="input-group-addon">
                                    <span class="fa fa-users"></span>
                                </div>
                                <asp:DropDownList ID="lstClients" runat="server" CssClass="form-control" AutoPostBack="True" ClientIDMode="Static"></asp:DropDownList>
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
                                    <asp:LinkButton ID="btnSeeAvailability" runat="server" class="btn btn-default" role="button">Ver</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                      
                    </div>
                </div><%'section 1 %>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-8 col-lg-8">
                                <div id="section2" class="clearfix" style="">
                                    <h3 class="sectionoption"><i class="fa fa-wrench"></i>Cambie la disponibilidad con estas herramientas o manualmente</h3>
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
                            <div class="col-xs-12" style="">
                                <h3 class="" style="margin-bottom: 0px; background-color: #f5f5f5; padding-left: 10px; padding-top: 10px; padding-bottom: 10px;">
                                    <i class="fa fa-calendar"></i> Disponibilidad
                                    <asp:Literal ID="litSelectedMonth" runat="server"></asp:Literal>
                                </h3>
                            </div>
                        </div>
                  
                    <%--<asp:Literal ID="litroomcombined" runat="server"></asp:Literal>--%>
                   
                    <% 
                        'obtenemos la disponib del cliente 
                        Dim oRCupDV As DataView = Me.getClientCupo()

                        Dim ordtCal As CalendarDays = Me.getCalendarHeader
                    %>

                    <div id="calendar">
                        <table class="cal-availability table table-striped table-condensed ">
                            <thead>
                            <tr>
                                <th class="calroomtitle rowclick chkroom_all" style="width:330px; min-width:330px;" >
                                    <label  data-toggle="tooltip" title="Pulsa para seleccionar todas las unidades." style="width:100%;">
                                        <input class="" id="chkroom_all" type="checkbox" /><%=Me.unitname%> +<br>combinaciones
                                    </label>
                                </th>

                                <% For Each ordt As calendarDay In ordtCal.dayList%>
                                <th class="byday"><%=ordt.getWeekDayLetter(3)%><br>
                                    <%=ordt.getDayNum%></th>
                                <% Next%>
                            </tr>
                                </thead>
                            <tbody>
                            <%  
                                If Me.dvRooms.Count > 0 Then
                                    For Each oRsHAB As DataRowView In Me.dvRooms
                                        Dim cod_room As Integer = oRsHAB(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel)
                                        Dim numRoomsAvailable As Integer = oRsHAB(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.numRoomsAvailable)

                            %>
                            <tr  id="rowroom_<%=cod_room%>">
                                <th class="calroomtitle roomclick rowclick" id="headroom_<%=cod_room%>" data-id="<%=cod_room%>">
                                        <label>
                                            <input class="chkroom" id="chkroom_<%=cod_room%>" type="checkbox"  data-id="<%=cod_room%>"/>
                                              <span class="" data-toggle="tooltip" title="Numero maximo de unidades de este tipo que se pueden introducir para cada dia. Puede modificarlo en el tipo de habitación." >(<%=numRoomsAvailable%>)</span> <%=oRsHAB(ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.habitacionhotel)%>
                                                                                           
                                        </label>
                                    <%=Me.getRoomCombinedIU(cod_room)%>
                                          
                                </th>
                                <% 'days

                        Dim idisp As String = ""
                        Dim xroom As Integer = 1
                        For Each ordt As calendarDay In ordtCal.dayList
                            Dim class2Add As String = ""

                            Dim filterString As String = String.Format("cod_habitacion={0} ", cod_room)
                            Dim filterDateString As String = " AND dia=#{0}#" 'mm/dd/yyyy
                            Dim idia As Integer = ordt.getDayNum
                            Dim strFilter As String = String.Format(filterString, oRsHAB(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel), Me.monthSel, idia, Me.yearSel)
                            Dim dateFor As Date = New Date(Me.yearSel, ordt.getMonth, idia)
                            strFilter = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, filterDateString, dateFor)
                            oRCupDV.RowFilter = filterString & strFilter
                            If oRCupDV.Count = 0 Then
                                idisp = "0"
                                class2Add = "calchg0"
                            Else
                                Dim orCUP As DataRowView = oRCupDV(0)
                                idisp = orCUP("DISPONIBLE")
                            End If

                            Dim txtId As String = String.Format("h{0}d{1}", cod_room, ordt.getDayNum)
                                %>
                                <td>
                                    
                                    <input type='number' class="onlynum calchg <%=class2Add%>" name="<%=txtId%>" id='<%=txtId%>' size='2' maxlength='3' min="0" max="<%=numRoomsAvailable%>" value="<%=iDISP%>" >
                                </td>
                                <%
                            xroom += 1
                        Next
                                %>
                            </tr>



                            <%
                            Next
             
                        End If
                            %>
                                </tbody>
                        </table>
                        <div class="row">
                            <div class="col-xs-hidden col-lg-2">
                            </div>
                            <div class="col-xs-6 col-lg-4">
                                <asp:LinkButton ID="btnsaveavailability" OnClientClick="btnsaveavailability_click()" runat="server" CssClass="btn btn-default btn-lg btn-block">Guardar disponibilidad <i class="fa fa-save"></i></asp:LinkButton>
                            </div>
                            <div class="col-xs-hidden col-sm-1">
                                </div>
                            <div class="col-sm-5">
                                <p id="availabilitychange" class="info" style="display: none;">Se han producido cambios en la disponibilidad. Recuerde pulsar en guardar.<br />
                                    Para cancelar los cambios vuelva a cargar los datos.</p>
                            </div>
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
            <li>Las <%=unitnameplural%> combinadas solo se muestran una vez.</li>
            <li>
                Recuerde que las unidades asignadas a "Venta Libre" son usadas por cualquier cliente una vez que han terminado el cupo asignado a cada uno.
            </li>
            <li>
                La disponibilidad(CUPO) asignada a cada cliente es solo para ese cliente excepto "Venta Libre" que es para todos.
            </li>
        </ul>           
        </div>
    </div>
        
    <%'Container %>
    

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
    <script>

        function btnsaveavailability_click() {
            $("#availabilitychange").hide();
        }

        $(document).ready(function () {
            console.log("tooltip-enabled")
            $('[data-toggle="tooltip"]').tooltip();
        });

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
            $('#availabilitychange').show();
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
                
            $('#availabilitychange').show();
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

        
        $(document).on("click", ".roomclick", function (e) {
            var roomid = String($(this).data('id'));
            var chkselected = $("#chkroom_" + roomid);
            var checked = chkselected.prop("checked");
            chkselected.prop("checked", !checked);
            setmarkedrow(roomid,checked);

        });

        function setmarkedrow(roomid,checked ) {
            if (!checked) {
                $('#rowroom_' + roomid).addClass('rowselected');
                $('#alertselectroom').addClass('hidden');
            } else {
                $('#rowroom_' + roomid).removeClass('rowselected');
            }
        }

        $(document).on("click",".chkroom_all", function (e) {
            var ckall = $("#chkroom_all");
            var check = ckall.prop('checked');
            ckall.prop('checked', !check);

            checkall(!check)
        });

        function checkall(check) {
            var checks = $('.chkroom');
            $.each(checks, function () {
                $(this).prop("checked", check);
                var roomid = String($(this).data('id'));
                setmarkedrow(roomid, !check);
            });
        }


        //$('.chkroom').change(function () {
        //    var ck = $(this);
        //    var check=ck.prop('checked');
        //    var roomid=ck.data('id');
        //   setmarkedrow(roomid,!check);
        //});


    </script>
</asp:Content>

