<%@ Page Title="CFR-Control" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Admin_Default" %>

<%-- Add content controls here --%>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    
      <style>
          .ui-tooltip {max-width: 350px;
                       background: black;
                       color: white;

              font: bold 14px "Helvetica Neue", Sans-Serif;
              text-transform: uppercase;
              box-shadow: 0 0 7px black;
          }
          .tooltip {background: black;
                    color: white;
                    box-shadow: 0 0 7px black;
          }

          .ymclose , td.sch-wkend.ymclose{
              background: red;
          }
      </style>
    <div class="container">
        <div class="row">
            
        <div class="col-xs-12 col-sm-6">
            <h1 class="sectiontitle"><i class="fa fa-home"></i>Panel de control</h1>
        </div>
        <div class="col-xs-12 col-sm-6">
           
                    <ul class="nav nav-pills quick">
                        <li><a href="<%= linkcnf("rep_book.aspx")%>" class="btn btn-warning btn-block" role="button"><i class="fa fa-sign-in"></i>
                            <br />
                            Reservas pendientes</a></li>
                        <li role="presentation"><a href="<%= linkcnf("rep_book.aspx")%>" class="btn btn-primary btn-block" role="button"><i class="fa fa-table"></i>
                            <br />
                            Informes</a></li>
                        <li role="presentation"><a href="<%= linkcnf("availability.aspx")%>" class="btn btn-primary btn-block" role="button"><i class="fa fa-table"></i>
                            <br />
                            Cambiar disponibilidad</a></li>
                        

                    </ul>
                    <div id="sch-event-options" class="sch-event-options list-group" style="display: none;">

                        <a class="list-group-item" href="#" onclick="event_editbook_click();"><span class="glyphicon glyphicon-pencil"></span>Editar</a>
                        <a class="list-group-item" href="#" onclick="event_masterviewbook();"><span class="fa fa-file-archive-o"></span>Ver reserva</a>
                    </div>
               
           
        </div>
 </div>
    </div>

    <div class="container-full">
        <div class="row">
            <div class="col-xs-12 col-sm-12">

                <asp:Literal ID="litScheduler" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    
    <div class="container">
            <div class="row">
                <div class="col-xs-12 col-sm-6">
                    
                    <h3>Leyenda Estados de reserva</h3>
                    <ul class="list-icons">
                        <li>
                            <div class="inf-legend" style="background-color: blue;"></div>
                            Reservas Activas pendientes de confirmar</li>
                        <li>
                            <div class="inf-legend" style="background-color: red;"></div>
                            Reservas canceladas por el cliente</li>
                        <li>
                            <div class="inf-legend" style="background-color: green;"></div>
                            Reservas confirmadas</li>
                        <li>
                            <div class="inf-legend" style="background-color: maroon;"></div>
                            Reservas canceladas por el hotel</li>
                        <li>
                            <div class="inf-legend" style="background-color: black;"></div>
                            NoShow</li>
                        <li>
                            <div class="inf-legend" style="background-color: #FF22C5;"></div>
                            Reservas canceladas por el operador <small>(Agencia, Empresa, etc.)</small></li>
                    </ul>
                

                </div>
                 <div class="col-xs-12 col-sm-6">
                    
                    <h3>Información</h3>
                    <ul class="list-icons">
                        <li>
                            <div class="inf-legend" style="background-color: red;"></div>
                            Los cierres de ventas visibles solo son para todos los clientes. Los cierres particulares a un operador tendrá que verlos en la pantalla de cierre de ventas</li>
                    </ul>
            

                </div>               
            </div>

        </div>
 
    
    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
    
        <script>
            $(document).ready(function () {
                $('.map-sch-cal').on('scroll', function () {
                    $('.map-sch-res-list').scrollTop($(this).scrollTop());
                    $('.map-sch-cal-header').scrollLeft($(this).scrollLeft());
                });

                // Enabling Popover Example 1 - HTML (content and title from html tags of element)
                console.log("tooltip-enabled")
              $('[data-toggle="tooltip"]').tooltip();

              $(".booktt").tooltip({
                  html: "true",
                  container: "body",
                  placement: "auto top",
                  title: function() {
                      return bookdatatooltip(this);
                  }
                  , template: '<div class="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-head"></div><div class="tooltip-inner"></div></div>'
              });

              //$(".headerday_click").tooltip({
              //    html: "true",
              //    container: "body",
              //    placement: "auto top",
              //    title: "Cerrar ventas."
              //    , template: '<div class="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-head"></div><div class="tooltip-inner"></div></div>'
              //});

              //$(".headerday_click").click(function () {
              //    var button = $(event.currentTarget); // Button that triggered the modal
              //    //var itemid = button.data('objid');
              //    alert("New feature: Open close days for rooms.");
              //});
          });

          function event_editbook_click() {
              var divoptions = $("#sch-event-options");
              var containerid = $(divoptions).data("containerid");
              var container=$("#"+containerid);
              var bookid= $(container).data("bookid");
              editbook_click(bookid);
          }

          function event_masterviewbook() {
              var divoptions = $("#sch-event-options");
              var containerid = $(divoptions).data("containerid");
              var container=$("#"+containerid);
              var bookid= $(container).data("bookid");
              masterviewbook(bookid);
          }

          function bookdatatooltip(tttitle) {
              var bookid = $(tttitle).data("bookid");
              var cliendata = $("#client_data_id_" + bookid);
              var innerhtml = "Reserva Nº: " + bookid + "<br>" + cliendata.html();
              return innerhtml;
          }

          function viewcelleventslist(cellid) {
              var celldiv = $("#" + cellid);
              //celldiv.appendTo("#sch-map-outer");
              celldiv.toggle();
          }

          function vieweventoptions(eventtarget) {
              
              var divoptions = $("#sch-event-options");
              var width = $(eventtarget).outerWidth();
              var pos = $(eventtarget).position();         
              var containerid = $(divoptions).data("containerid");
                
              if (containerid==eventtarget.id) {
                  divoptions.toggle();
                  return;
              }
              
              $(divoptions).data("containerid",eventtarget.id);          
           
              $(divoptions).prependTo(eventtarget);
              divoptions.css({
                  position: "absolute",
                  top: (pos.top +2) + "px",
                  left: (pos.left + width) + "px"
              });
              divoptions.show();
          }

          function setnewbookvalues(bookid, bookstatenew, bookpaystatenew, bookpricenew, bookstatestring, bookidproperty) {

              var keynode = "bookdata_id_" + bookid;
              var bookdata = $("#"+keynode).data("bookdata");
              bookdata.state = bookstatenew;
              bookdata.paystate = bookpaystatenew;
              bookdata.bookidproperty = bookidproperty;
              
              var barstate = $("#event_"+keynode + " .sch-event-bar");

              if (bookdata.state==<%=CInt(CFRConst.bookStateEnum.Active)%>) {barstate.css("background-color"," blue"); }
              else if (bookdata.state==<%=CInt(CFRConst.bookStateEnum.CancelByClient)%>) {barstate.css("background-color"," red"); }
              else if (bookdata.state==<%=CInt(CFRConst.bookStateEnum.NoShow)%>) {                  barstate.css("background-color"," black");              }
              else if (bookdata.state==<%=CInt(CFRConst.bookStateEnum.confirmedByHotel)%>) {                  barstate.css("background-color"," green");              }
              else if (bookdata.state==<%=CInt(CFRConst.bookStateEnum.CancelByHotelUser)%>) {                  barstate.css("background-color"," maroon");              }
              else if (bookdata.state==<%=CInt(CFRConst.bookStateEnum.canceledByOperator)%>) {                barstate.css("background-color"," #FF22C5");              }

          }

          

      </script>
<%--Public Enum bookStateEnum As Integer
          Active = 0 'reserva normal
          CancelByClient = 1 'cancelada x cliente
          NoShow = 2 'noshow
          estado3 = 3 'modificada
          CancelByHotelUser = 4 'cancelada x usuario
          confirmedByHotel = 5 'confirmada
          canceledByOperator = 6 'cancelada x operador
          End Enum--%>
  </asp:Content>  
