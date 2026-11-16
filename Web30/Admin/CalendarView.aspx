<%@ Page Title="CFR-Calendario" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="CalendarView.aspx.vb" Inherits="CalendarView" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
    <style>
    .cal-container .uavailability{
        font-size: 110%;
        font-weight: bold;
        /* border: 1px solid; */
        padding-left: 2px;
        text-align: right;
        background-color: chartreuse;
        padding: 2px;
        width: 100%;
        height:19px;
    }
        .cal-container .uavailability.lock {
            background-color: red;
        }
        .cal-container .uavailability.unlock {
            background-color: chartreuse;
        }
        .cal-container .uavailability .ut-avai {
            float:right;
        }
        .cal-container .uavailability .ut-lock {
            float:left;
        }

    .cal-container .cal-tariff{
        border-width: 1px;
        border-style: solid;
        border-color: cadetblue;
        background-color: ghostwhite;
        padding:3px;
        width:90px;
        position:relative;
    }
    .cal-container .t-price{
        font-weight:bold;
        padding-right: 10px;
        font-size: 110%;
    }
     .cal-container .cal-tariff .t-mindaysrele{
         display:inline-block;
         position:absolute;
         right:0px;
         bottom: 0px;
     }
    .cal-container .cal-tariff .t-mindays{
    background-color: darkorange;
    padding-left: 2px;
    padding-right: 2px;
    }
    .cal-container .cal-tariff .t-release{
    background-color: darkgray;
    padding-left: 2px;
    padding-right: 2px;
    }
    .tt-cal-tariff{
        text-align: left;
    }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div class="container">
        <h1 class="sectiontitle">Calendario Sistema   </h1>
        <p>En esta versión aún no se pueden editar los datos. Para editar los precios vaya a <a href="<%= linkcnf("tariffcalendar.aspx")%>"><i class="fa fa-calendar-o"></i>Tarifa Calendario x Mes</a></p>
        </div>
        
        <div class="container-full">
                <div id="section1" class="clearfix">
                    
                    <div class="form-stacked">
                        <div class="form-group " style="width: 350px;">
                            <label class="sflabel control-label" for="lstclients">Para la unidad</label>
                            <div class="input-group ">
                                <div class="input-group-addon">
                                    <span class="fa fa-asterisk"></span>
                                </div>
                                <asp:DropDownList ID="cbounits" runat="server" CssClass="form-control" AutoPostBack="True" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group " style="width: 350px;">
                            <label class="sflabel control-label" for="lstclients">ver la tarifa</label>
                            <div class="input-group ">
                                <div class="input-group-addon">
                                    <span class="fa fa-eur"></span>
                                </div>
                                <asp:DropDownList ID="cbotariff" runat="server" CssClass="form-control" AutoPostBack="True" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                         <div class="form-group " style="width: 350px;">
                            <label class="sflabel control-label" for="lstclients">y disponibilidad para el operador/contrato</label>
                            <div class="input-group ">
                                <div class="input-group-addon">
                                    <span class="fa fa-users"></span>
                                </div>
                                <asp:DropDownList ID="lstclients" runat="server" CssClass="form-control" AutoPostBack="True" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>


                        <div class="form-group">
                                    <div class="input-daterange" id="datepicker" style="display: inline-block;">
                                                                <div class="form-group col-sm-5 padleftclear">
                                                                    <label class="sflabel control-label" for="checkInDate">Ver desde</label>
                                                                    <div class="input-group">
                                                                        <div class="input-group-addon">
                                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                                        </div>
                                                                        <asp:TextBox ClientIDMode="static" ID="checkindate" runat="server" CssClass="checkindate form-control sfinput dtpreadonly " MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group col-sm-5">
                                                                    <label for="checkOutDate" class="sflabel control-label">Hasta</label>
                                                                    <div class="input-group">
                                                                        <div class="input-group-addon">
                                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                                        </div>
                                                                        <asp:TextBox ClientIDMode="static" ID="checkoutdate" runat="server" CssClass="checkoutdate date-picker form-control sfinput dtpreadonly" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                  </div>                                      
                        <div class="form-group">
                            <div class="form-group" style="height: 65px;  position: relative;">
                            <div style="bottom: 5px; position: absolute;">
                                 <asp:LinkButton ID="btnseecalendar" runat="server" cssclass="btn btn-default" role="button">Ver</asp:LinkButton>
                            </div>
                                   
                                </div>
                            <div>

                            </div>

                        </div>
                      </div>
  
                </div>
            <% If Not Me.oRsHAB Is Nothing Then%>
            <div class="row">
                <div class="col-xs-12">
                                <%  
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
                                     %>
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                               <div>
                                                <span class="tariffcode label label-default" data-toggle="tooltip" title="Codigo de unidad"><%=cod_room %></span> <span class="roommaxavailability" data-toggle="tooltip" title="Numero maximo(recomendado) de unidades de este tipo que se pueden introducir para cada dia. Puede modificarlo en el tipo de habitación. Un numero superior al limite puede generar overbooking">(<%=numRoomsAvailable%>)</span>
                                                <span class="roomcapacity" data-toggle="tooltip" title="Numero de personas que admite esta unidad."><%=strRoomCapacity%></span>
                                                <span class="fk-unitname"  ><%=roomName %></span>
                                                <%=Me.getRoomCombinedIU(cod_room)%>
                                                </div> 

                            </div>
                            <div class="panel-body">
                                <div >
                                    <%= Me.showCalendar()%>
                                </div>

                            
                                </div>
                        </div> 
                    

                </div>
            </div>
            <%End If %>
            
        </div>


    <div class="container">
        <div id="footerinfo" class="footerinfo">
            <h3>Información</h3>
            <ul>
                <li>
                    <div class="inf-legend" style="background-color: chartreuse;"></div>
                   Disponibilidad para el operador seleccionado</li>
                <li>
                    <div class="inf-legend" style="background-color: darkorange;"></div>
                    Dias minimos</li>
                <li>
                    <div class="inf-legend" style="background-color: darkgray;"></div>
                    Release</li>
 
                <li>Para editar solo la disponibilidad vaya a la pantalla de disponibilidad. En la pantalla Tarifa Calendario podrá editar la disponibilidad y los precios al mismo tiempo.
                </li>
                <li>En la proxima versión tambien podrá editar disponibilidad, precios y cierre de ventas.</li>

            </ul>
        </div>

    </div>
    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
            <script>
            $(document).ready(function () {
                // Enabling Popover Example 1 - HTML (content and title from html tags of element)
                console.log("tooltip-enabled")
              $('[data-toggle="tooltip"]').tooltip();

              $(".tdtt").tooltip({
                  html: "true",
                  container: "body",
                  placement: "auto top",
                  title: function() {
                      return tarifftooltip(this);
                  }
                  , template: '<div class="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-head"></div><div class="tooltip-inner"></div></div>'
              });

              function tarifftooltip(tttitle) {
                  var price = $(tttitle).find(".t-price").html();
                  var mindays = $(tttitle).find(".t-mindays").html();
                  var release = $(tttitle).find(".t-release").html();
                  var innerhtml = "<div class='tt-cal-tariff'>Precio por dia: " + price + "<br>Dias minimos: " + mindays  + "<br> Release: " + release + "</div>";
                  return innerhtml;
              }

            });
                </script>
</asp:Content>

