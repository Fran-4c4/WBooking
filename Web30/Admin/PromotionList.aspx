<%@ Page Title="CFR-Promociones" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="true" CodeFile="PromotionList.aspx.vb" Inherits="Admin_PromotionList" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Linq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
    <style>
        
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">           
            <ContentTemplate>
    
            </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSaveRelations" />
            </Triggers>
        </asp:UpdatePanel>
         <asp:HiddenField ID="field_operation" runat="server" ClientIDMode="Static" Value="0" />
         <asp:HiddenField ID="field_itemobject" runat="server" ClientIDMode="Static" Value="0" />
         <asp:HiddenField ID="field_itemname" runat="server" ClientIDMode="Static" Value="" />
    


     <div class="container">
        <h1 class="sectiontitle"><i class="fa fa-star"></i> Promociones</h1>
        <div class="row">
             <div class="col-sm-8">
                 <p>Codigos de promoción: Cree un codigo de promoción y asignele las tarifas que desee. 
                     Las tarifas se añadirán al calculo y se le ofrecerá al cliente la más baja de las asociadas al tipo de cliente.
                     Si no tiene descuento unicamente mostrará las tarifas asignadas.
                 </p>
                 <p>El descuento de la promoción no se aplica a servicios/extras o regimen en esta versión.</p>
                 </div>

            <div class="col-sm-4 pull-right">
                <div class="text-right">
                    <button type="button" name="newitem" value="0" UseSubmitBehavior="false" data-dismiss="modal" class="openedititem-newitem  btn btn-animated btn-primary btn-lg" data-modaltitle="Crear nuevo codigo" data-modaldiv="#createitem" data-modalbuttonid="#btncreateitem">
                        Crear nuevo codigo promoción<i class="glyphicon glyphicon-plus-sign"></i>
                    </button>

                </div>
            </div>
        </div>
        <div class="separator-2"></div>
    </div>
    
<div class="container">
    <% 'Loop configuration
        Dim dvList As DataView = Me.dvCustomer
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
                For Each drvList As DataRowView In Me.CustomerListForPresentation
                    'CONFIG VALUES NEEDED FOR PRESENTATION
                    objid = drvList(ind.bk.obj.R_operadores.DBCol.cod_operador)
                    Dim objidCrypt As String = UIHelper.URLencryptData(objid)
                    Dim customerType As String = drvList(ind.bk.obj.R_operadores.DBCol.tipo).ToString
                    'START ITEMTEMPLATE +++++++++++++++++++++++++++++++++
                    %>
    
                    <div class="row customertype_<%=customerType%>" id="rowobj_<%= objid%>"><!-- row start -->
                        <h3 class="title margin-clear"><small><%= objid%></small> <i class="fa fa-star "></i> <%=drvList(ind.bk.obj.R_operadores.DBCol.Operador)%> </h3>

				   
		            <div class="main col-sm-6"><!-- main start -->
					<div class="image-box team-member style-3-b">
					    
                        <div class="separator-2"></div>
                            <p><small>Ultima actualización: <%=drvList(ind.bk.obj.R_operadores.DBCol.ModifyDate)%> </small> </p>
                        
                        <div class="form-horizontal" style='max-width: 900px;'>

                            <div class="form-group form-ctrol-admin ">
                                <label for="tname_<%=objid%>" class="col-sm-3 control-label">Nombre promoción</label>
                                <div class="col-sm-6">
                                    <input type="text" class="form-control" name="tname_<%=objid%>" id="tname_<%=objid%>" maxlength='50' value="<%=drvList(ind.bk.obj.R_operadores.DBCol.Operador)%>" >
                                </div>
                            </div>
                            <div class="form-group form-ctrol-admin ">
                                <label for="tLogin_<%=objid%>" class="col-sm-3 control-label">Codigo promoción</label>
                                <div class="col-sm-3">
                                    <input type="text" class="form-control" name="tLogin_<%=objid%>" id="tLogin_<%=objid%>" maxlength='50' value="<%=drvList(ind.bk.obj.R_operadores.DBCol.login)%>">
                                </div>
                            </div>
                            <div class="form-group form-ctrol-admin ">
                                <label for="tdiscount_<%=objid%>" class="col-sm-3 control-label">Descuento </label>
                                <div class="col-sm-2">
                                    <div class="input-group">
                                    <input type="text" class="form-control discountpromotion" name="tdiscount_<%=objid%>" maxlength='2' value="<%=drvList(ind.bk.obj.R_operadores.DBCol.discount)%>" onkeypress="return solonums();">
                                    <span class="input-group-addon">%</span>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="form-group form-ctrol-admin ">
                                <div class="col-sm-offset-3 col-sm-10">
                                    <div class="checkbox">
                                        <label>
                                            <% Dim chkVal1 As String = ""
                                                If drvList(ind.bk.obj.R_operadores.DBCol.discountNotApplyWithNoRefundTariff) = True Then
                                                    chkVal1 = "checked"
                                                End If
                                                %>
                                            <input id="chkdiscountNotApplyWithNoRefundTariff_<%=objid%>" name="chkdiscountNotApplyWithNoRefundTariff_<%=objid%>" type="checkbox" <%=chkVal1%>>No aplicar descuento a tarifa no reembolsable
                                        </label>
                                    </div>
                                </div>
                            </div>

                            
                            <div class="col-sm-offset-3 col-sm-10">
                            
                            <div class="input-daterange"  style="display: inline-block;">
                            <div class="form-group col-sm-5 padleftclear">
                                <label class="sflabel control-label" for="tdiscountapplyfrom_<%=objid%>">Fecha de inicio promoción</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                    <% Dim tdiscountapplyfrom As Date = Date.Now
                                        If IsDBNull(drvList(ind.bk.obj.R_operadores.DBCol.discountapplyfrom)) = False Then
                                            tdiscountapplyfrom = drvList(ind.bk.obj.R_operadores.DBCol.discountapplyfrom)
                                        
                                        End If
                                        %>   
                                    <input type="text" class="checkindate date-picker form-control sfinput dtpreadonly" id="tdiscountapplyfrom_<%=objid%>" name="tdiscountapplyfrom_<%=objid%>" maxlength='10' value="<%=tdiscountapplyfrom.Date.ToShortDateString%>">
                                </div>
                            </div>

                            <div class="form-group col-sm-5" style="margin-left: 10px;">
                                <label for="tdiscountapplyuntil_<%=objid%>" class="sflabel control-label">Hasta</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                    <% Dim tdiscountapplyuntil As Date = Date.Now
                                        If IsDBNull(drvList(ind.bk.obj.R_operadores.DBCol.discountapplyuntil)) = False Then
                                            tdiscountapplyuntil = drvList(ind.bk.obj.R_operadores.DBCol.discountapplyuntil)
                                        End If
                                        
                                        %>    
                                    <input type="text" class="checkoutdate date-picker form-control sfinput dtpreadonly" id="tdiscountapplyuntil_<%=objid%>" name="tdiscountapplyuntil_<%=objid%>" maxlength='10' value="<%=tdiscountapplyuntil.Date.ToShortDateString%>" >                               
                                </div>
                            </div>
                        </div>
                                
                                </div>

                            <div class="form-group form-ctrol-admin ">
                                <div class="col-sm-offset-3 col-sm-10">
                                    <div class="checkbox">
                                        <label>
                                            <% Dim chkVal As String = ""
                                                If drvList(ind.bk.obj.R_operadores.DBCol.discountapply) = True Then
                                                    chkVal = "checked"
                                                End If
                                                %>
                                            <input id="tdiscountapply_<%=objid%>" name="tdiscountapply_<%=objid%>" type="checkbox" <%=chkVal%>>Activar promoción
                                        </label>
                                    </div>
                                </div>
                            </div>
                            
                          <div class="form-group">
                                <div class="col-sm-offset-3 col-sm-10" data-objid="<%=objidCrypt%>">
                                    <button type="button" name="delete" value="<%=objidCrypt%>" class="btn btn-animated btn-danger btn-sm" data-toggle="modal" data-target="#modaldeleteItem" data-objname="<%=drvList(ind.bk.obj.R_operadores.DBCol.Operador)%>" data-objid="<%=objidCrypt%>">Borrar<i class="fa fa-times"></i></button>
                                    <asp:LinkButton ID="LinkButtonSaveClient" runat="server" cssclass="btn btn-animated btn-primary btn-sm savebutton" >Guardar cambios <i class="fa fa-floppy-o"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                       </div> <!-- team-member end -->     
    				</div><!-- main end -->

					<div class="col-sm-6">
					    
                        <div>
                            <ul id="myTabs_<%=objid%>" class="nav nav-tabs style-3" role="tablist">
                                <li role="presentation" class="active"><a href="#tab_tariff_<%=objid%>" id="tab-tariff" role="tab" data-toggle="tab" aria-controls="home" aria-expanded="true"><i class="fa fa-eur"></i> Tarifas asignadas</a></li>
                           </ul>
                            <div id="myTabContent" class="tab-content">
                                <div role="tabpanel" class="tab-pane fade active in" id="tab_tariff_<%=objid%>" aria-labelledby="Tariff-tab">
                                   <p>Estas son las tarifas que existen actualmente. Las que estan marcadas son las que tiene asignadas esta promoción actualmente.</p>
                                                    <%   dvTariffJoker = Me.getDvCustomerTariffHotel(objid)%>
                                               <ul class="list-icons">
                                                          <%For Each dr As DataRowView In dvTariffJoker%>
                                                          <li class="">
                                                              <div class="checkbox">
                                                                  <% Dim ischecked As string = False
                                                                      If dr(ind.bk.obj.R_operadores.DBCol.cod_operador) <> 0 Then
                                                                          ischecked = " checked "
                                                                      Else
                                                                          ischecked = "  "
                                                                      End If
                                                                            
                                                                      %>
                                                                  
                                                                  <label for="chktariff_<%=objid %>_<%=dr(ind.bk.obj.R_tarifas.DBCol.cod_tarifa)%>">
                                                                      <input type="checkbox" <%=ischecked %> name="chktariff_<%=objid %>_<%=dr(ind.bk.obj.R_tarifas.DBCol.cod_tarifa)%>" id="chktariff_<%=objid %>_<%=dr(ind.bk.obj.R_tarifas.DBCol.cod_tarifa)%>">
                                                                      <%=dr(ind.bk.obj.R_tarifas.DBCol.tarifa)%>
                                                                  </label>
                                                              </div>
                                                          </li>
                                                          <%Next%>
                                                      </ul>                            

                              
                                     <div id="resultmsg_<%=objid%>"></div>
                                     <div data-objid="<%=objid%>"><asp:LinkButton ID="btnSaveRelations" runat="server" cssclass="btn btn-animated btn-primary btn-sm savebutton" >Guardar asignacion <i class="fa fa-floppy-o"></i></asp:LinkButton></div>
                                    
                                </div>

                            </div>
                        </div>
                                                                
					</div>		
				</div><!-- row end -->

                <%'END ITEMTEMPLATE +++++++++++++++++++++++++++++++++
                Next
                        
                End If 'END CONDITIONAL LOOP +++++++++++++++++++++++++++++++++
                    
        %>

  
   
        <div class="modal " id="modaldeleteItem" tabindex="-1" role="dialog" >
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span class="glyphicon glyphicon-remove"></span></button>
                    <h4 class="modal-title" id="myModalLabel">Borrar promoción</h4>
                </div>
                <asp:UpdatePanel ID="upDel" runat="server">
                  <ContentTemplate>
                      <div class="modal-body">
                          <asp:HiddenField ID="itemdeletecode" runat="server" ClientIDMode="static"  />
                            Esta seguro de que quiere borrar el codigo de promoción seleccionado?
                                <h3 id="itemplacenameto" class="text-center"></h3>
                                <div class="resultmessageplace"></div>
                      </div>
                      <div class="modal-footer">
                          <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                          <asp:Button class="btn btn-danger btndelete" ID="btndelete" Text="Borrar promoción" runat="server"  ClientIDMode="static" />
                      </div>
                      </ContentTemplate>
                      <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="btndelete" EventName="Click" />
                      </Triggers>
                </asp:UpdatePanel>                
            </div>
        </div>
    </div>
    <!--Delete Record Modal Ends here -->
    
 
        <div class="modal " id="modaledit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="modaledit_title">MODAL TITLE</h4>
                    </div>
                    <div class="modal-body">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Cancelar</button>
                        <span id="modalfooterdefaultbuttonplace"></span>
                    </div>
                </div>
            </div>
        </div>



    <div id="divhiddencontainer" class="hidden">
        <div id="createitem" class="clearfix">
            <div class="form-horizontal" style='max-width: 900px;'>
                <div class="form-group form-ctrol-admin ">
                    <label for="cliente_0" class="col-sm-4 control-label">Nombre promoción </label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="cliente_0" runat="server" class="form-control" name="" MaxLength="50" value="" placeholder="Introduzca aqui el nombre de la nueva promoción." ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-ctrol-admin ">
                    <label for="promotioncode" class="col-sm-4 control-label">Código promoción </label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="promotioncode" runat="server" class="form-control" name="" MaxLength="50" value="" placeholder="Introduzca aqui el codigo de la nueva promoción." ClientIDMode="Static"></asp:TextBox>
                        <p class="help-block">Este será el codigo que el usuario introduzca para acceder a la promoción. Recomendamos no usar caracteres estandar.</p>
                    </div>
                </div>
            </div>
        </div>
        <asp:LinkButton ID="btncreateitem" runat="server" CssClass="btn btn-animated btn-primary" UseSubmitBehavior="false" ClientIDMode="Static" ValidationGroup="createitem"><i class="fa fa-plus"></i> Crear promoción</asp:LinkButton>
    </div>

    

</div><!-- container end -->
    

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
    <script>
        $(function () {
            $(".discountpromotion")
                .popover({ trigger: 'focus', title: 'Descuento en porcentaje', content: "Descuento que se aplicará sobre las tarifas asignadas unicamente." })
                .blur(function () {
                    $(this).popover('hide');
                });
        });

        $(document).ready(function () {
            $(".savebutton").click(function () {
                var button = $(event.currentTarget); // Button that triggered the modal
                var par = button.parent();
                $("#field_itemobject").val(par.data('objid'));
            });
            
            //$('#modaldeleteItem').on('show.bs.modal', function (event) {
            //    $('#modaldeleteItem').appendTo("form");
            //    $('#resultmessageplace').html('');
            //    $('#btndelete').removeClass('hidden');
            //});


        });


    </script>
</asp:Content>

