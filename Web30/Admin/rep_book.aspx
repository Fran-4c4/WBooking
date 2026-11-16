<%@ Page Title="CFR-Informe Reservas" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="rep_book.aspx.vb" Inherits="Admin_rep_book" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Diagnostics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <%--<link rel="stylesheet" href="plugins/jquery-ui.css">--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
       
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
    <style>
        @media (min-width:768px) {
            .searchtabs .tab-content > .tab-pane {
                height: 110px; 
            }
        }
        @media (max-width:767px)  {
            .searchtabs .tab-content > .tab-pane {
                height: 140px; 
            }
        }

        .datagrid table tbody td.tdcomments {
            font-size: 11px;
        }
        .datagrid table tbody td.tdstate {
            font-size: 11px;
            
        }
        .datagrid table thead th.tdstate {
            width: 140px;
        } 
        .datagrid table tbody td.tdprice {
            text-align: right;
            font-weight: bold;
        }
        .datagrid table tbody td.tdsmall {
            font-size: 11px;
        }

        .datagrid table tbody td.tdbookid {
            text-align: center;
            font-weight: bold;
        }
        .bookid {
            text-align: center;
            font-weight: bold;
            font-size: 14px;
        }
        .bookdate {
            margin-top: 10px;
            font-weight: normal;
        }

        .paypen {
            color: red;
            font-weight: bold;
        }

        .norefundtar {
            color: red;
            font-weight: bold;
        }

        .chkin {
            color: #1CB51C;
            font-size: 18px;
            padding-right: 4px;
        }
        .chkout {
            color: red;
            font-size: 18px;
            padding-right: 4px;
        }


        .datagrid {
            font: 12px;
            /*margin-left: 10px;
            margin-right: 10px;*/
            /*background: #fff;*/
            overflow-x:auto;
            border: 1px solid #8C8C8C;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
        }

        .datagrid table { border-collapse: collapse; text-align: left; width: 100%; } 
        .datagrid table th { padding-left: 7px;padding-right: 7px;padding-bottom: 5px;padding-top: 8px; 

        }
        .datagrid table thead th {background-color:#008FD6; color:#FFFFFF; font-size: 15px; font-weight: bold; border-left: 1px solid #A3A3A3; } 
        .datagrid table thead th:first-child { border: none; }

        .datagrid>table>tbody>tr:nth-child(odd) {background-color: #DBE8EC;}
        .datagrid table td{ padding-left: 10px;padding-right: 10px;padding-bottom: 3px;padding-top: 3px;}
        .datagrid table tbody td { color: #000; 
            border-left: 1px solid #DBDBDB;
            font-size: 12px;
            font-weight: normal; }
        .datagrid table tbody .alt td { background: #F2FBFF; color: #7D7D7D; }
        .datagrid table tbody td:first-child { border-left: none; }
        .datagrid table tbody tr:last-child td { border-bottom: none; }
       
        .datagrid table tfoot td div { border-top: 1px solid #8C8C8C;background: #EBEBEB;} 
        .datagrid table tfoot td { padding: 0; font-size: 12px } 
        .datagrid table tfoot td div{ padding: 2px; }
        .datagrid table tfoot td ul { margin: 0; padding:0; list-style: none; text-align: right; }
        .datagrid table tfoot  li { display: inline; }
        .datagrid table tfoot li a { text-decoration: none; display: inline-block; 
            padding: 2px 8px; margin: 1px;color: #F5F5F5;border: 1px solid #8C8C8C;-webkit-border-radius: 3px;
            -moz-border-radius: 3px; border-radius: 3px;
            background-color:#8C8C8C; }
        .datagrid table tfoot ul.active, .datagrid table tfoot ul a:hover { text-decoration: none;border-color: #7D7D7D;
            color: #F5F5F5; background: none; background-color:#8C8C8C;}
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
<%="" %>
         <asp:HiddenField ID="field_operation" runat="server" ClientIDMode="Static" Value="0" />
         <asp:HiddenField ID="field_itemobject" runat="server" ClientIDMode="Static" Value="0" />
         <asp:HiddenField ID="field_itemname" runat="server" ClientIDMode="Static" Value="" />

        <div class="container-full">
            <h1 class="sectiontitle">Informe de reservas</h1>
            <div class="separator-2"></div>
        </div><!--end container -->
    
    <div class="container-full">
        <div class="searchtabs" style="padding-bottom: 20px;" >
                <ul id="tabsearch" class="nav nav-tabs style-3" role="tablist">
                    <li role="presentation" class="active"><a href="#tabsearchbydates" role="tab" data-toggle="tab"  class="tab-col-5"><i class="fa fa-calendar-o"></i> Buscar por fechas</a></li>
                    <li role="presentation" class=""><a href="#tabsearchbybookid" role="tab"data-toggle="tab" class="tab-col-5"><i class="fa fa-tag"></i> Buscar por localizador</a></li>
                    <li role="presentation" class=""><a href="#tabsearchbybookidproperty" role="tab" data-toggle="tab"  class="tab-col-5"><i class="fa fa-tag"></i> Buscar por localizador interno</a></li>
                    <li role="presentation" class=""><a href="#tabsearchbyemail" role="tab"data-toggle="tab" class="tab-col-5"><i class="fa fa-envelope"></i> Buscar por email</a></li>
                    <li role="presentation" class=""><a href="#tabsearchbycustomer" role="tab" data-toggle="tab"  class="tab-col-5"><i class="fa fa-user"></i> Buscar por cliente</a></li>

                </ul>
                <div id="myTabContent" class="tab-content" style="">
                    <div role="tabpanel" class="tab-pane active" id="tabsearchbydates" >
                        <div class="form-stacked">
                            
                            <div class="input-daterange" id="datepicker" >
                            <div class="form-group col-xs-6 col-sm-4" style="width: 160px;">
                                <label class="sflabel control-label" for="checkInDate">Desde</label>
                                <div class="input-group input-group-sm">
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                    <asp:TextBox ClientIDMode="static" ID="checkindate" runat="server" CssClass="checkindate form-control sfinput dtpreadonly " MaxLength="10" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group col-xs-6 col-sm-4" style="width: 160px;">
                                <label for="checkOutDate" class="sflabel control-label">Hasta</label>
                                <div class="input-group input-group-sm">
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                    <asp:TextBox ClientIDMode="static" ID="checkoutdate" runat="server" CssClass="checkoutdate date-picker form-control sfinput dtpreadonly" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                                </div>

                            <div class="form-group">
                                <div class="radio">
                                    <label>
                                        <asp:RadioButton ID="rdbbookstatepending" runat="server" GroupName="rdbbookstategroup" Checked="True" />Reservas pendientes
                                    </label>
                                </div>                                
                                <div class="radio">
                                    <label>
                                        <asp:RadioButton ID="rdbbookstateconfirmed" runat="server" GroupName="rdbbookstategroup"/>Reservas Confirmadas
                                    </label>
                                </div>
                                <div class="radio">
                                    <label>
                                        <asp:RadioButton ID="rdbbookstatecancel" runat="server" GroupName="rdbbookstategroup" />Reservas canceladas
                                    </label>
                                </div>
                            </div>
                            <div class="form-btn col-xs-12 col-sm-4" style="">
                                <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-default btn-lg">Ver <i class="fa fa-search"></i></asp:LinkButton>

                            </div>
                        </div>

                    </div>
                    <div role="tabpanel" class="tab-pane" id="tabsearchbybookid" >
                         <div class="form-stacked">
                            <div class="form-group ">
                                <label for="txtsearchbookid" class="control-label">Localizador reserva</label>
                                <div class="" style="width: 200px;">
                                    <asp:TextBox runat="server"   cssclass="form-control" id="txtsearchbookid" maxlength='50' value="" ClientIDMode="static"></asp:TextBox>

                                    <p class="help-block">Localizador reserva del sistema</p>
                                    <p class="help-block with-errors"><asp:RequiredFieldValidator ID="RequiredFieldValidator1txtsearchbookid" ControlToValidate="txtsearchbookid" runat="server" ValidationGroup="btnsearchbookid" ErrorMessage="Debe introducir el codigo de reserva." Display="Dynamic" ValidateRequestMode="Enabled"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                             <div class="form-btn">
                                 <asp:LinkButton ID="btnsearchbookid" runat="server" CssClass="btn btn-default" ValidationGroup="btnsearchbookid">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                             </div>
                            

                        </div>
                    </div>
                    <div role="tabpanel" class="tab-pane fade" id="tabsearchbybookidproperty" >
                        <div class="form-stacked">
                            <div class="form-group ">
                                <label for="txtsearchbookidproperty" class="control-label">Localizador reserva</label>
                                <div class="" style="width: 200px;">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtsearchbookidproperty" MaxLength='50' value="" ClientIDMode="static"></asp:TextBox>

                                    <p class="help-block">Localizador reserva del hotel</p>
                                    <p class="help-block with-errors">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1txtsearchbookidproperty" ControlToValidate="txtsearchbookidproperty" runat="server" ValidationGroup="btnsearchbookidproperty" ErrorMessage="Debe introducir el codigo de reserva." Display="Dynamic" ValidateRequestMode="Enabled"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-btn">
                                <asp:LinkButton ID="btnsearchbookidproperty" runat="server" CssClass="btn btn-default" ValidationGroup="btnsearchbookidproperty">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                            </div>
                        </div>
                                    
                    </div> 
                    <div role="tabpanel" class="tab-pane fade" id="tabsearchbyemail" >
                        <div class="row">
                            <div class="col-xs-12 col-sm-8 ">
                                <div class="form-stacked">
                            <div class="form-group ">
                                <label for="txtsearchbyemail" class="control-label">Email de la reserva</label>
                                <div class="" style="width: 300px;">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtsearchbyemail" MaxLength='255' value="" ClientIDMode="static"></asp:TextBox>
                                    <p class="help-block with-errors"><asp:RequiredFieldValidator ID="RequiredFieldValidator2txtsearchbyemail" ControlToValidate="txtsearchbyemail" runat="server" ValidationGroup="btnsearchbyemail" ErrorMessage="Debe introducir el email" Display="Dynamic" ValidateRequestMode="Enabled"></asp:RequiredFieldValidator></p>
                                </div>
                            </div>
                            <div class="form-btn">
                                <asp:LinkButton ID="btnsearchbyemail" runat="server" CssClass="btn btn-default" ValidationGroup="btnsearchbyemail">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                            </div>
                            
                        </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 ">
                                <p class="help-block">Introduzca cualquier secuencia de caracteres para buscar. No tiene que escribir el email exacto.</p>
                                </div>

                        </div>
                                    
                    </div>
                    <div role="tabpanel" class="tab-pane fade" id="tabsearchbycustomer" >
                        <div class="form-stacked">
                                <div class="form-group ">
                                <label for="txtsearchbysurname" class="control-label">Apellidos cliente</label>
                                <div class="" style="width: 300px;">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtsearchbysurname" MaxLength='255' value="" ClientIDMode="static"></asp:TextBox>
                                    <p class="help-block with-errors">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1txtsearchbyname" ControlToValidate="txtsearchbysurname" runat="server" ValidationGroup="btnsearchbysurname" ErrorMessage="Debe introducir al menos el apellido del cliente" Display="Dynamic" ValidateRequestMode="Enabled"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>
                            <div class="form-btn">
                                <asp:LinkButton ID="btnsearchbyname" runat="server" CssClass="btn btn-default" ValidationGroup="btnsearchbyname">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                            </div>
                        </div>       
                        </div>               
                                    
                    </div>

                </div>
            </div>
        
        


      
    
    <div class="container-full">
        <asp:UpdatePanel ID="UpdatePanelreport" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <%
                    Dim dateQuery As String = " (Fecha de consulta: " & Date.Now.ToLongDateString() & " " & Date.Now.ToShortTimeString & ")"
                    If Me.dvrep.Count = 0 And searchinprogress = True Then
                        Dim msg As String = UIMsg.UIMessage("No hemos encontrado ninguna coincidencia." & dateQuery, CFRConst.msgEnum.info)
                         
                    %>
                    <%=msg %>
                <%else %>
                Hemos encontrado <%=Me.dvrep.Count %> registros. <small><%=dateQuery %></small>
                

        <div class="datagrid">
            
            <table >
            <thead>           
            <tr>
                <th>Opciones</th>
                <th>Localizador<br />Realizada el</th>
                <th><span data-placement="top" data-toggle="tooltip" title="Localizador del interno en el sistema del hotel">Localizador<br />interno</span></th>
                <th class="tdstate"><span data-placement="top" data-toggle="tooltip" title="Estado de la reserva y estado del pago por parte del cliente.">Estado</span></th>
                <%If Me.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then %>
                         <th><i class="fa fa-calendar"></i> Fecha<br />Entrada</th>
                <th><i class="fa fa-calendar"></i> Fecha<br /> Salida</th>
                 <%ElseIf Me.hotel.Tipo = CFRConst.unitTypeEnum.car Then%>
                 <th><i class="fa fa-calendar"></i> Fecha<br />Recogida</th>
                <th><i class="fa fa-calendar"></i> Fecha<br /> Devolución</th>
                 <%Else%>
                 <th><i class="fa fa-calendar"></i> Fecha<br />Entrada</th>
                <th><i class="fa fa-calendar"></i> Fecha<br /> Salida</th>
                 <% End If%>
               
                <th><%=Me.unitname%></th>
                <th><span data-placement="top" data-toggle="tooltip" title="Precio total de la reserva.">Precio&#8364;</span></th>
                <%If Me.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then%>
                <th><span data-placement="top" data-toggle="tooltip" title="Regimen seleccionado por el cliente. Puede variar por cada dia en función de las tarifas disponibles.">Regimen</span></th>
                <th>Datos Reserva</th>
                <% end if %>
                <th>Cliente Reserva</th>
                <th>Cliente<br />(Operador)</th>
                <th>Forma de Pago</th>
                <th>Comentarios de cliente</th>
            </tr>
            </thead>
            <tbody>
            <%
                Dim sw As New Stopwatch
                sw.Start()
                For Each ors As DataRowView In Me.dvrep
                    
                    Dim reporUI As New ind.bk.reports.bookReportUI(ors, Me.hotel)
                    Dim repBookid As Integer = reporUI.ors(ind.bk.obj.R_ReservasO.DBCol.cod_reservaO)
                    %>
              <tr>
                <td>
                    <div id="bookdata_id_<%=repBookid %>" <%=reporUI.getBookJsonStruct  %> class="action" data-objid="<%=repBookid %>" >
                    
                        <span data-placement="top" data-toggle="tooltip" title="Editar reserva">
                            <button type="button" class="btn-primary btn-sm" onclick="editbook_click(<%=repBookid %>);" data-title="Edit" data-objid="<%=repBookid %>"><span class="fa fa-pencil"></span></button>
                        </span>
                        <%--<span data-placement="top" data-toggle="tooltip" title="Otras opciones, Reenviar reserva.">
                            <button type="button" class="btn-default btn-sm" data-title="Reenviar reserva" data-toggle="modal" data-target="#modalsendbook" data-objid="<%=objid %>"><span class="fa fa-cog"></span></button>
                        </span>--%>
                    </div>
                </td>
                <td class="tdbookid"><a class="dark" data-toggle="tooltip" data-placement="top" title="Ver reserva"  href="#" onclick="masterviewbook(<%=repBookid%>);"><div class="bookid"><%=repBookid%></div><div class="bookdate"><%=oRs("fechareserva")%></div></a></td>
                <td class="tdsmall"><div id="bookidproperty_<%=repBookid%>"><%=oRs("cod_reserva_hotel")%></div></td>
                <td class="tdsmall"><div id="bookstate_<%=repBookid%>"><%=reporUI.strEstadoReserva%></div></td>
                <%If Me.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then %>
                             <td><i class='fa fa-sign-in chkin'></i> <%=ors("fecha1")%></td>
                <td><i class='fa fa-sign-out chkout'></i> <%=ors("fecha2")%></td>
                 <%ElseIf Me.hotel.Tipo = CFRConst.unitTypeEnum.car Then%>
                    <td><i class='fa fa-sign-in chkin'></i> <%=reporUI.bookData.dateIn  %>
                        <% If Not reporUI.bookData.carLocationSelected Is Nothing Then %>
                               <br/><i class="fa fa-map-marker"></i>  <%=reporUI.getLocation("GET")  %> (<%=reporUI.getLocation("PRICEGET")  %>€)
                           <% End If%>
                        

                    </td>
                    <td><i class='fa fa-sign-out chkout'></i> <%=ors("dateout")%>
                         <% If Not reporUI.bookData.carLocationSelected Is Nothing Then %>
                               <br/><i class="fa fa-map-marker"></i>  <%=reporUI.getLocation("return")  %> (<%=reporUI.getLocation("PRICERETURN")  %>€)
                           <% End If%>                       

                    </td>               
                 <%Else%>
               <td><i class='fa fa-sign-in chkin'></i> <%=ors("fecha1")%></td>
                <td><i class='fa fa-sign-out chkout'></i> <%=ors("fecha2")%></td>
                 <% End If%>
            
                <td><%=oRs("DescHabitacion")%></td>
                <td class="tdprice">
                    <% If reporUI.bookData.noRefundTariffSelected = True Then%>
                            <%="<div><span class='norefundtar'>Tarifa no reembolsable.</span></div>" %>
                        <% End If%>
                    <% 
                        Dim price2show As String = ""
                        If Me.hotel.Tipo = CFRConst.unitTypeEnum.car Then
                            Dim priceForLocation As Double = 0
                            If reporUI.bookData.carLocationSelected.unit_location_Get_Price >= reporUI.bookData.carLocationSelected.unit_location_Return_Price Then
                                priceForLocation = reporUI.bookData.carLocationSelected.unit_location_Get_Price
                            Else
                                priceForLocation = reporUI.bookData.carLocationSelected.unit_location_Return_Price
                            End If
                            price2show = reporUI.priceStr
                        ElseIf Me.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then
                            price2show = reporUI.priceStr
                        Else
                            price2show = reporUI.priceStr
                        End If
                    %>
                    <%=price2show%>
                </td>
                  

                <%If Me.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then%>
                <td><%=reporUI.strBoard%></td>
                <td><%=reporUI.paxStr%></td>
                <%End If%>
                <td><%=reporUI.ClientDataStr%></td>
                <td><%=reporUI.stroperador%></td>
                
                <td><%=reporUI.paySelectedStr%></td>
                <td class="tdcomments"><%=oRs("comentarios")%></td>


            </tr>          
            <%
                           
            Next
            Debug.WriteLine("Total time to load report: " & sw.ElapsedMilliseconds)
            %>
            </tbody>
        </table>
            
            </div>
                <div class="reportime">
                    <%--<% Dim datereport As Date = Date.Now
                        Response.write("Informe creado el " & datereport.ToLongDateString & " " & datereport.ToLongTimeString ) 
                        %>--%>
                </div>
                <%End if %>
                </ContentTemplate>
                <Triggers >
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    <asp:AsyncPostBackTrigger ControlID="btnsearchbookid" />
                    <asp:AsyncPostBackTrigger ControlID="btnsearchbookidproperty" />
                    <asp:AsyncPostBackTrigger ControlID="btnsearchbyemail" />
                    <asp:AsyncPostBackTrigger ControlID="btnsearchbyname" />
                </Triggers>
            </asp:UpdatePanel>
    </div>
    
    <div class="container">

    <div class="footerinfo">
            <h3>Información </h3>
            <ul>
                <li>Puede ver la reserva pulsando sobre el codigo de reserva.</li>
                <li>No se pueden cambiar reservas antiguas.</li>
            </ul>
        </div>
        </div>
    

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
    <asp:Literal ID="calendarjsLit" runat="server"></asp:Literal>
    <script>


        
        $(document).ready(function () {
            bindcontrolevents();
            //$('.datagrid').tooltip({selector: '[data-toggle=tooltip]'});
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });
        });

        //Re-bind for callbacks
        prm.add_endRequest(pageEndRequestHandler);

        function pageEndRequestHandler(sender, args) {
            unbindcontrolevents();
            bindcontrolevents();
            console.log("pageEndRequestHandler");
            Request_EndController(sender, args);
        }

        function unbindcontrolevents() {
            //$("[data-toggle=tooltip]").unbind();
            $(".editbook_click").unbind();
        
        }
        
        function bindcontrolevents() {
            //jQuery is wrapped in BindEvents function so it can be re-bound after each callback.
            //Your code would replace the following line:
            //$("[data-toggle=tooltip]").tooltip();

  
        }

    
          function setnewbookvalues(bookid, bookstatenew, bookpaystatenew, bookpricenew, bookstatestring, bookidproperty) {

            var bookdata = $("#bookdata_id_" + bookid).data("bookdata");
            bookdata.state = bookstatenew;
            bookdata.paystate = bookpaystatenew;
            bookdata.bookidproperty = bookidproperty;

            if (bookpricenew != "") {
                bookdata.pricechanged = bookpricenew;
                $("#pricechg_" + bookid).html(bookpricenew + "&#8364;").addClass("lblwarn lblwarning");
            } else {
                $("#pricechg_" + bookid).html("").removeClass("lblwarn lblwarning");
                bookdata.pricechanged = bookdata.pricefinal;
                
            }

            $("#bookstate_" + bookid).html(bookstatestring); //string with html from server
            $("#bookidproperty_" + bookid).html(bookdata.bookidproperty);
        }





    </script>


 
    

    <script>
        
    </script>

</asp:Content>

