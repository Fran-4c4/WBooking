<%@ Page Title="CFR-Tarifas" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="tarifflist.aspx.vb" Inherits="Admin_tarifflist" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="ind.bk.reports" %>
<%@ Register Src="adminUC/deletemodal.ascx" TagPrefix="uc1" TagName="deletemodal" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
    <style>
        .rprices{ width: 70px;
                  text-align: right;
        }
        .tdprice{ width: 70px;
                  text-align: right;
        }
        .roomdesc {
            min-width: 250px;
        }
        .table > thead > tr > th {
        }
        .rpricesheader th,  .rpricesheader td {
            background-color: #C9DBF1;
        }
        .table > thead > tr > th.rprices-strip-1,  .table > thead > tr > td.rprices-strip-1{
            background-color: rgb(229, 224, 255);
        }
        .tarifftable {
            width: initial;
        }
        .tariffcontainer {
            margin-bottom: 20px;
        }
        .roomcapacity {
                width: 45px;
                font-size: 12px;
                margin-left: -10px;
                display: inline-block;
        }

    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
          
         <asp:HiddenField ID="field_operation" runat="server" ClientIDMode="Static" Value="0" />
         <asp:HiddenField ID="field_itemobject" runat="server" ClientIDMode="Static" Value="0" />
         <asp:HiddenField ID="field_itemname" runat="server" ClientIDMode="Static" Value="" />
    
 
    <div class="container">
        <div class="row">
            <div class="col-sm-8">
                  <h1 class="sectiontitle"><i class="glyphicon glyphicon-eur"></i> Tarifas</h1>
            </div>
            <div class="col-sm-4">
                <div class="text-right">
                    <a href="<%= linkcnf("Tariffedit.aspx?objid=" & UIHelper.URLencryptData("0"))%>" class="openedititem-newitem  btn btn-animated btn-primary btn-lg">Crear nueva Tarifa <i class="glyphicon glyphicon-plus-sign"></i></a>
                </div>
            </div>
        </div>
    </div>

    <div class="container-full">
        <div class="action-buttons">
            <a href="#" class="btn btn-default btn-sm" id="viewlist" data-toggle="tooltip" title="Ver como tabla"><span class="fa fa-table"></span></a>
            <a href="#" class="btn btn-default btn-sm" id="viewminimmum" data-toggle="tooltip" title="Ver como listado"><span class="fa fa-list"></span></a>
            <a href="#" class="btn btn-default btn-sm" id="viewnormalmosaic" data-toggle="tooltip" title="Ver como mosaico"><span class="fa fa-list-alt"></span></a>

        </div>

        <% 'Loop configuration
            Dim dvList As DataView = Me.getDvList
        %>
        <% 'HEADER TEMPLATE %>
         
        <% 'what to do if no rows?
            If dvList.Count = 0 Then
            %>    
            <div class="alert  alert-icon alert-info" role="alert">
						<i class="fa fa-info-circle"></i>
						No se han encontrado registros.
			</div>
            <%  

                Else
                    'LOOP OVER ROWS 
                    For Each drvList As DataRowView In dvList
                        'CONFIG VALUES NEEDED FOR PRESENTATION
                        Dim objid As String = drvList(ind.bk.obj.R_tarifas.DBCol.cod_tarifa)
                        Dim objidCrypt As String = UIHelper.URLencryptData(drvList(ind.bk.obj.R_tarifas.DBCol.cod_tarifa))

                        Dim dvClients As DataView = Me.getDvClientsAsociated(objid)

                        Dim hasCustomersMessage As String = ""
                        If dvClients.Count = 0 Then
                            hasCustomersMessage = UIMsg.UIMessageDismissInline("Esta tarifa no tiene asociados clientes.", CFRConst.msgEnum.info, "msgcustomerelation_" & objid, UIMsg.msgbodyEnum.none)

                        End If
                        'START ITEMTEMPLATE +++++++++++++++++++++++++++++++++
                    %>

								<div id="rowobj_<%=objid%>" data-objid="<%=objid%>" class="tariffcontainer">
								    <div  class="title-block-2">                                   
                                         <h3 ><span class="unit-code label label-default" data-toggle="tooltip" title="Codigo de tarifa"><%= drvList(ind.bk.obj.R_tarifas.DBCol.cod_tarifa)%></span> 
                                             <%= drvList(ind.bk.obj.R_tarifas.DBCol.tarifa)%> 
                                         </h3>
                                         <div class="edit-buttons">
                                                                 <asp:LinkButton ID="btnsavetariff" runat="server" CssClass="btn btn-primary btn-sm btn-animated " data-toggle="tooltip" title="Guardar todos los cambios realizados a las tarifas. Las asignaciones son guardadas automaticamente.">
                       Guardar cambios <i class="fa fa-save"></i> 
                    </asp:LinkButton>
                                            <a class="btn btn-animated btn-primary btn-sm" href="<%= linkcnf("Tariffedit.aspx?objid=" & objidCrypt)%>" data-toggle="tooltip" title="Editar los datos genericos de la tarifa">Editar<i class="glyphicon glyphicon-pencil"></i></a>
                                             <button type="button" name="delete" value="<%=objidCrypt%>" class="btn btn-danger btn-sm" data-toggle="modal" data-target="#modaldeleteItem" data-objname="<%=drvList(ind.bk.obj.R_tarifas.DBCol.tarifa)%>" data-objid="<%=objidCrypt%>"><i class="fa fa-trash"></i></button>
                                        </div>
                                    <%=hasCustomersMessage %>
                                      
								    
									</div>
                                    <div class="rowtariff row ">
                                        <div class="colview-1 col-sm-2" style="display: none;">
                                            <p class="help-block">Ultima modificación: <%= drvList(ind.bk.obj.R_tarifas.DBCol.ModifyDate)%></p>
                                          
                                    <ul>
                                        <li>
                                            <strong>Tipo de tarifa: </strong><%= Me.getTariffCategoryUI(drvList(ind.bk.obj.R_tarifas.DBCol.Categoria))%>
                                        </li>                                        
                                       
                                        <%If Me.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then %>
                                        <li  class="list-icons">
                                            <strong><i class="fa fa-cutlery"></i> Regimenes aplicables: </strong>
                                            <br />
                                            
                                            <% If drvList(ind.bk.obj.R_tarifas.DBCol.SA) = True Then Response.Write("SA&nbsp;")%>                                      
                                            <% If drvList(ind.bk.obj.R_tarifas.DBCol.AD) = True Then Response.Write("AD&nbsp;")%>    
                                            <% If drvList(ind.bk.obj.R_tarifas.DBCol.MP) = True Then Response.Write("MP&nbsp;")%>                                      
                                            <% If drvList(ind.bk.obj.R_tarifas.DBCol.PC) = True Then Response.Write("PC&nbsp;")%>    
                                            <% If drvList(ind.bk.obj.R_tarifas.DBCol.TI) = True Then Response.Write("TI&nbsp;")%>                                     
                                           
                                        </li>
                                       <%End If%>
                                        <li>
                                            <strong>Dias minimos: </strong><%=drvList(ind.bk.obj.R_tarifas.DBCol.DiasMinimos)%>
                                        </li> 

                                    </ul>

                                    <%--<a class="btn btn-animated btn-primary btn-sm" href="#" onclick="tabopen('tab_Temporadas_<%=objid%>');">Asignar fechas<i class="fa fa-calendar-o"></i></a>
                                    <a class="btn btn-animated btn-primary btn-sm" href="#" onclick="tabopen('tab_clients_<%=objid%>');">Asignar Clientes<i class="fa fa-user"></i></a>  --%>                                         
                                              <div style="margin-bottom: 50px;"></div>
                                        </div>
                                        <div class="colview-2 col-sm-12">
                                            <div class="" >
                                                <ul id="myTabs_<%=objid%>" class="nav nav-tabs style-3" role="tablist">
                                                    <li role="presentation" class="active"><a href="#tab_precios_<%=objid%>" role="tab"data-toggle="tab" class="tabpreciosclick"><i class="glyphicon glyphicon-eur"></i> Precios de regimen y suplementos</a></li>
                                                </ul>
                                                <div id="myTabContent" class="tab-content ">
                                                    
                                                    <div role="tabpanel" class="tab-pane active tabprecios" id="tab_precios_<%=objid%>" >
                                                        <a href="<%= linkcnf("tariffcalendar.aspx")%>"><i class="fa fa-calendar-o"></i>Pulse aqui para modificar los precios de las unidades x dia. (Tarifa Calendario)</a>
                                                        <div class="panel panel-info">
                                                            
                                                            <div class="table-responsive">
                                                                <table id="tarifftable_<%=objid%>" class="table table-bordered table-striped table-condensed tarifftable">

                                                                    <thead>
                                                                    <tr class="rpricesheader">
                                                                        <th><strong>Tipo de tarifa: </strong><%= Me.getTariffCategoryUI(drvList(ind.bk.obj.R_tarifas.DBCol.Categoria))%>
                                                                            
                                                                            <% If drvList(ind.bk.obj.R_tarifas.DBCol.norefundtariff) = True Then%>
                                                                            <span style="display: block;"><strong>Tarifa no reembolsable</strong> Descuento? <%=drvList(ind.bk.obj.R_tarifas.DBCol.norefundtariffdiscount)%> %</span>
                                                                            <% End If%>
                                                                        </th>
                                                                        <th colspan="2" class="rprices-strip-1">Suplementos
                                                                        </th>
                                                                        <%--<th></th>--%>
                                                                        <asp:PlaceHolder ID="plhhotelboardth" runat="server" Visible="true">
                                                                        <th colspan="4" ><i class="fa fa-cutlery"></i> Precio Regimen Adultos<br /><small>Los regimenes no visibles no estan activados.</small></th>
                                                                       <%-- <th></th>
                                                                        <th></th>
                                                                        <th></th>  --%>     
                                                                        <th colspan="4" class="rprices-strip-1"><i class="fa fa-cutlery"></i> Precio Regimen Niños</th> 
                                                                        <th colspan="2">Descuento adultos extra</th>
                                                                        <%--<th></th>--%>                                                                               
                                                                         <%--Discount child extra--%>
                                                                        <th colspan="2" class="rprices-strip-1">Descuento Niños extra</th>
                                                                        <%--<th></th>--%>
                                                                         </asp:PlaceHolder>
                                                                           
                                                                                                                                     
                                                                    </tr>
                                                                    <tr class="rpricesheader" >
                                                                        <th class="roomdesc">Unidad</th>
                                                                        <td class="rprices-strip-1 "><div class="mindaytooltip"><i class="fa fa-info-circle "></i> Dias minimos</div></td>
                                                                        <td class="rprices-strip-1"><div class="mindayweekendtooltip"><i class="fa fa-info-circle "></i> Fin de semana</div></td>
                                                                        <asp:PlaceHolder ID="plhhotel_board" runat="server" Visible="true">
                                                                        <th>Desayuno</th>
                                                                        <th>Media pension</th>
                                                                        <th>Pension completa</th>
                                                                        <th>Todo incluido</th>
                                                                        <th class="rprices-strip-1">Desayuno</th>
                                                                        <th class="rprices-strip-1">Media pension</th>
                                                                        <th class="rprices-strip-1">Pension completa</th>
                                                                        <th class="rprices-strip-1">Todo incluido</th>
                                                                        <%--Discount Pax extra--%>
                                                                        <th>Pax-1 extra</th>
                                                                        <th>Pax-2 extra</th>                                                                               
                                                                         <%--Discount child extra--%>
                                                                        <th class="rprices-strip-1">Niño-1 extra</th>
                                                                        <th class="rprices-strip-1">Niño-2 extra</th>
                                                                        </asp:PlaceHolder>

                                                                    </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <%
                                                                            Dim dvRoomPrices As DataView = Me.getDvPricesByRoomByTariff(drvList(ind.bk.obj.R_tarifas.DBCol.cod_tarifa))

                                                                            'Dim sRow As String = "<tr><td>{0}</td><td class='td-price'>{1}</td><td class='td-price'>{2}</td><td class='td-price'>{3}</td><td class='td-price'>{4}</td><td class='td-price'>{5}</td><td class='td-price'>{6}</td><td class='td-price'>{7}</td></tr>"
                                                                            Dim class2Ad As String = ""
                                                                            Dim scell As String = "<input type='text' class='onlynum rprices {0}' name='{1}' id='{1}'  value='{2}' >"
                                                                            Dim scelldisc As String = "<input type='text' class='onlynum rprices discount {0}' name='{1}' id='{1}'  value='{2}' rel='popover'>"

                                                                            For Each drvRoomprice As DataRowView In dvRoomPrices
                                                                                Dim priceinfo As New ind.bk.reports.pricesInfoObject(drvRoomprice)
                                                                                priceinfo.configurePriceForUI(drvRoomprice)
                                                                                Dim habitacionhotel As String = drvRoomprice(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel).ToString
                                                                                Dim PaxIncluidEnPrecio As Integer = drvRoomprice(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.MaxPersonas).ToString
                                                                                Dim MaxAdultos As Integer = drvRoomprice(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.MaxAdultos).ToString
                                                                                Dim strRoomCapacity As String = PaxIncluidEnPrecio & " <i class='fa fa-male'></i>"
                                                                                If PaxIncluidEnPrecio > MaxAdultos Then
                                                                                    Dim numBoys As Integer = PaxIncluidEnPrecio - MaxAdultos
                                                                                    strRoomCapacity += "+" & numBoys & "<i class='fa fa-child' /></i>"
                                                                                End If

                                                                                Dim SupDiasMinimos As String = String.Format(scell, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.supdiasmin), drvRoomprice(ind.bk.obj.R_Tarifa_habitacion.DBCol.SupDiasMinimos).ToString)
                                                                                Dim SupFinDeSemana As String = String.Format(scell, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.supfindesemana), drvRoomprice(ind.bk.obj.R_Tarifa_habitacion.DBCol.SupFinDeSemana).ToString)
                                                                                'Adults board
                                                                                Dim precioAD As String = String.Format(scell, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardAD), drvRoomprice(ind.bk.obj.R_Tarifa_habitacion.DBCol.precioAD).ToString)
                                                                                Dim preciomp As String = String.Format(scell, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardMP), drvRoomprice(ind.bk.obj.R_Tarifa_habitacion.DBCol.preciomp).ToString)
                                                                                Dim preciopc As String = String.Format(scell, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardPC), drvRoomprice(ind.bk.obj.R_Tarifa_habitacion.DBCol.preciopc).ToString)
                                                                                Dim precioTI As String = String.Format(scell, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardTI), drvRoomprice(ind.bk.obj.R_Tarifa_habitacion.DBCol.TodoIncluido).ToString)
                                                                                'Child board
                                                                                Dim precioADchild As String = String.Format(scell, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardADchild), drvRoomprice(ind.bk.obj.R_Tarifa_habitacion.DBCol.desayunonino).ToString)
                                                                                Dim preciompchild As String = String.Format(scell, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardMPchild), drvRoomprice(ind.bk.obj.R_Tarifa_habitacion.DBCol.mpnino).ToString)
                                                                                Dim preciopcchild As String = String.Format(scell, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardPCchild), drvRoomprice(ind.bk.obj.R_Tarifa_habitacion.DBCol.pcnino).ToString)
                                                                                Dim precioTIchild As String = String.Format(scell, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.boardTIchild), drvRoomprice(ind.bk.obj.R_Tarifa_habitacion.DBCol.TodoIncluidoNino).ToString)
                                                                                'discounts adults
                                                                                Dim discountPax1 As String = String.Format(scelldisc, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.discountPax1), priceinfo.UI_DiscountPax1)
                                                                                Dim discountPax2 As String = String.Format(scelldisc, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.discountPax2), priceinfo.UI_DiscountPax2)
                                                                                'discounts childs
                                                                                Dim discountchild1 As String = String.Format(scelldisc, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.discountchild1), priceinfo.UI_DiscountChild1)
                                                                                Dim discountChild2 As String = String.Format(scelldisc, "", priceinfo.getRoomIdUI(pricesInfoObject.tariffFieldEnum.discountchild2), priceinfo.UI_DiscountChild2)


                                                                                %>
                                                                                <tr>
                                                                                    <td class="roomname">
                                                                                        <span class="roomcapacity"><%=strRoomCapacity%></span>
                                                                                        <%=habitacionhotel%>
                                                                                    </td>
                                                                                    <td class="tdprice"><%=SupDiasMinimos%></td>
                                                                                    <td class="tdprice"><%=SupFinDeSemana%></td>

                                                                                    
                                                                                    <%If Me.hotel.Tipo = 1 Then%>
                                                                                    <%--Adults board--%>
                                                                                    <%= showCellOn(drvList("AD"), precioAD)%> 
                                                                                    <%= showCellOn(drvList("MP"), preciomp)%>
                                                                                    <%= showCellOn(drvList("PC"), preciopc)%>
                                                                                    <%= showCellOn(drvList("TI"), precioTI)%>
                                                                            
                                                                                    <%--Child board--%>
                                                                                    <%= showCellOn(drvList("AD"), precioADchild)%> 
                                                                                    <%= showCellOn(drvList("MP"), preciompchild)%>
                                                                                    <%= showCellOn(drvList("PC"), preciopcchild)%>
                                                                                    <%= showCellOn(drvList("TI"), precioTIchild)%>
                                                                                 
                                                                                    <%--Discount Pax extra--%>
                                                                                     <% If priceinfo.drvTariff("AdultosExtra1") = 0 Then%>
                                                                                     <td class="tdprice"></td>
                                                                                    <%Else%>
                                                                                    <td class="tdprice"><%=discountPax1%></td>
                                                                                    <%End If%>
                                                                                    <% If priceinfo.drvTariff("AdultosExtra2") = 0 Then%>
                                                                                     <td class="tdprice"></td>
                                                                                    <%Else%>
                                                                                    <td class="tdprice"><%=discountPax2%></td>
                                                                                    <%End If%>     
                                                                                    <%--Discount child extra--%>
                                                                                     <% If priceinfo.drvTariff("MaxPersonas") > 1 Then
                                    'admit child
                                                                                             %>
                                                                                    <td class="tdprice"><%=discountchild1%></td>
                                                                                    <%Else%>
                                                                                    <td class="tdprice"></td>
                                                                                    <%End If%>
                                                                                    <% If priceinfo.drvTariff("MaxPersonas") > 2 Then
                                    'admit child
                                                                                             %>
                                                                                    <td class="tdprice"><%=discountChild2%></td>
                                                                                    <%Else%>
                                                                                    <td class="tdprice"></td>
                                                                                    <%End If%>
                                                                                     
                                                                                    <%End If%>

                                                                                </tr>
                                                                                
                                                                                <%


                                Next

                                                                        %>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                                                                                 
                                                            <div class="panel-section-header">
                                                                <h4><span class="glyphicon glyphicon-list"></span>Operadores asignados a la tarifa.</h4>
                                                            </div>
                                                        <div class="panel-section-body">
                                                        <ul class="list-inline list-icons" id="customerlist_<%=objid%>">
                                                            <%For Each dr As DataRowView In dvClients%>
                                                               <li><i class="fa fa-user"></i> <%=dr(ind.bk.obj.R_operadores.DBCol.Operador)%></li>
                                                            <%Next%>
                                                        </ul>
                                                        <div id="resultmsg_<%=objid%>"></div>
                                                        <div data-objid="<%=objid%>">
                                                            <asp:LinkButton ID="btnshowcustomerrelations" runat="server" cssclass="btn btn-animated btn-primary btn-sm showrelations" CommandArgument="assigncustomer">Modificar operadores asignados <i class="fa fa-pencil"></i></asp:LinkButton>
                                                        </div>    
                                                          </div>                                                  
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="separator-2 mt-10"></div>--%>
								</div>

                    <%'END ITEMTEMPLATE +++++++++++++++++++++++++++++++++
                            Next

                        End If 'END CONDITIONAL LOOP +++++++++++++++++++++++++++++++++

        %>


    </div><!--container -->

        <div class="container">
        <div class="footerinfo">
         <h3>Información </h3>
        <ul>
            <li>Se muestran las columnas de precio para los regimenes porque el sistema permite asignar un precio por tarifa. <small>Para mantener la estructura visual se muestra en todas las tarifas aunque no tengan la opción.</small>
            </li>
            <li>Cuando pulse en guardar cambios estos se realizan para todas las tarifas.</li>
            <li>
                Las modificaciones en las asignaciones de tarifa/operador son automaticas. No es necesario guardar nada.
            </li>
            <li>Para modificar el precio de la tarifa x periodos deberá hacerlo en <a href="<%= linkcnf("tariffcalendar.aspx")%>"><i class="fa fa-cal"></i>Tarifa Calendario</a></li>

        </ul>           
        </div>
    </div>


    <%--<asp:UpdateProgress runat="server" id="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1"> 
	            <ProgressTemplate> 
	                Loading Data Please Wait... 
	            </ProgressTemplate> 
	        </asp:UpdateProgress> --%>
          <div class="modal" id="modalasignrelations" tabindex="-1" role="dialog"  data-backdrop="static">
             <div class="modal-dialog" role="document">
                 <div class="modal-content">
                     <div class="modal-header">
                         <button type="button" class="close" data-dismiss="modal" ><span>&times;</span></button>
                         <h4 class="modal-title" id="myModalLabel">Asociar Operadores/Contratos a esta tarifa</h4>
                     </div>
                     <div class="modal-body">                              
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >           
                            <ContentTemplate>
                                <asp:Literal ID="litrelations" runat="server" ClientIDMode="static" ></asp:Literal>
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnshowcustomerrelations" />
                            
                            </Triggers>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Literal ID="litmsgresult" runat="server"></asp:Literal>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnsavetariffrelations" />
                                </Triggers>
                            </asp:UpdatePanel>
    
                     </div>
                     <div class="modal-footer">
                         <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                         <asp:LinkButton ID="btnsavetariffrelations" runat="server" cssclass="btn btn-animated btn-primary">Guardar asociación<i class="fa fa-save"></i></asp:LinkButton>                        
                     </div>
                 </div>
             </div>
         </div>
    
    
    <uc1:deletemodal runat="server" ID="ucxdeletemodal" />

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
    <script >
        
        //Initial bind
        $(document).ready(function() {
            console.log("tooltip-enabled")
            $('[data-toggle="tooltip"]').tooltip();
            $(".discount").popover({ trigger: "focus", placement: "top", title: "Descuentos", content: "Si introduce un valor con el signo porcentaje el sistema hará los calculos basados en el precio base x persona." });
            $(".mindaytooltip").popover({ trigger: "hover", placement: "auto right", title: "Suplemento", content: "Cuando la tarifa tenga dias minimos se aplicará este suplemento al hacer la reserva para cada dia si no se cumple el minimo." });
            $(".mindayweekendtooltip").popover({ trigger: "hover", placement: "auto right", title: "Suplemento", content: "Cuando no se cumpla el minimo de dias este valor se añadirá al suplemento de dias minimos solo en fin de semana(Sabado/Domingo)" });
        });

        $('#modalasignrelations').on('show.bs.modal', function (event) {
            $('#modalasignrelations').appendTo("form");
        });
        $(".showrelations").click(function () {
            var button = $(event.currentTarget); // Button that triggered the modal
            var par = button.parent();
            $("#field_itemobject").val(par.data('objid'));
        });
        function setnewrelations(objid , direldiv) {
            var direl =  $('#'+direldiv+"_"+objid);
            $(direl).find('li').remove();
            var iconclass = "fa fa-user";
                      
            $(".chk-rel").each(function (index) {
                var relationid = $(this).attr('id');
                var isChecked = $(this).is(':checked');

                if (isChecked) {
                    var label = $("label[for='" + relationid + "']").text();
                    direl.append("<li><i class='"+iconclass +"'></i> " + label + "</li>");
                }
            });
        }

        $("#viewlist").click(function () {
            $(".tariffcontainer").removeClass("col-sm-3");
            $(".colview-1").hide();
            $(".colview-2").removeClass("col-sm-10").addClass("col-sm-12").show();
            $(".rowtariff").removeClass("row").addClass("row").show();
        });
        $("#viewminimmum").click(function () {
            $(".tariffcontainer").removeClass("col-sm-3");
            $(".rowtariff").hide();
        });
        $("#viewnormalmosaic").click(function () {
            $(".colview-2").hide();
            $(".colview-1").show().removeClass("col-sm-2");
            $(".rowtariff").removeClass("row");
            $(".rowtariff").show();
            $(".tariffcontainer").addClass("col-sm-3");
        });

        //$(".tabpreciosclick").click(function () {
        //    var mytabs = $('.tabprecios');
        //    tabopenlist(mytabs);

        //});
        //$(".tabseasonsclick").click(function () {
        //    var mytabs = $('.tabseasons');
        //    tabopenlist(mytabs);
        //});
        //$(".tabclientsclick").click(function () {
        //    var mytabs = $('.tabclients');
        //    tabopenlist(mytabs);
        //});


    </script>
</asp:Content>

