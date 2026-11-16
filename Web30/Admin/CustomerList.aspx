<%@ Page Title="CFR-Clientes" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="true" CodeFile="CustomerList.aspx.vb" Inherits="Admin_CustomerList" %>
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
        
        <div class="row">
             <div class="col-sm-8">
                 <h1 class="sectiontitle"><i class="fa fa-users"></i> Clientes/Operadores/Contratos</h1>
                 
                 <% Dim dvListFilter As DataView = Me.dvCustomer
                     
                     %>

                 <div class="">
                     <strong>Filtrar por: </strong>Filtrar por:  
                     <label  class="checkbox-inline" ><input type="checkbox" class="chkcustomerview" value="5" onclick="filtercustomerview();" checked="checked"> Cliente particular</label>
                
                    <label class="checkbox-inline" ><input type="checkbox" class="chkcustomerview" value="1" onclick="filtercustomerview();" checked="checked">Agencias</label>
                    <label class="checkbox-inline" ><input type="checkbox" class="chkcustomerview" value="4" onclick="filtercustomerview();" checked="checked">Empresas</label>
 
                    <label class="checkbox-inline" ><input type="checkbox" class="chkcustomerview" value="2" onclick="filtercustomerview();" checked="checked">CRS</label>
                    <label class="checkbox-inline" ><input type="checkbox" class="chkcustomerview" value="3" onclick="filtercustomerview();" checked="checked">TourOperadores</label>
                    <label class="checkbox-inline" ><input type="checkbox" class="chkcustomerview" value="6" onclick="filtercustomerview();" checked="checked">Portales web </label>                     
                 </div>
                 </div>

            <div class="col-sm-4 pull-right">
                <div class="text-right">
                    <button type="button" name="newitem" value="0" UseSubmitBehavior="false" data-dismiss="modal" class="openedititem-newitem  btn btn-animated btn-primary btn-lg" data-modaltitle="Crear nuevo operador" data-modaldiv="#createitem" data-modalbuttonid="#btncreateitemcustomer">
                        Crear nuevo cliente<i class="glyphicon glyphicon-plus-sign"></i>
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
                        <div class="title-block-2">
                        <%If drvList(ind.bk.obj.R_operadores.DBCol.FreeSale) = cfr.CFRConst.genericOperator.PrivateGeneral Then%>
                        <h3 class="bg-importantdata"><span class="unit-code label label-default"><%= objid%></span> <span data-toggle="tooltip" title="Necesario para el sistema. Operador usado para venta libre."><i class="fa fa-user "></i> <%=drvList(ind.bk.obj.R_operadores.DBCol.Operador)%></span> </h3>
                        <%Else%>
                        <h3 class=""><span class="unit-code label label-default"><%= objid%></span> <i class="fa fa-user "></i> <%=drvList(ind.bk.obj.R_operadores.DBCol.Operador)%> </h3>
                        <%End If%>
                            <div class="edit-buttons" data-objid="<%=objidCrypt%>">
                                    <asp:LinkButton ID="LinkButtonSaveClient" runat="server" cssclass="btn btn-animated btn-primary btn-sm savebutton" >Guardar cambios <i class="fa fa-floppy-o"></i></asp:LinkButton>
                                    <%If drvList(ind.bk.obj.R_operadores.DBCol.FreeSale) <> cfr.CFRConst.genericOperator.PrivateGeneral Then%>
                                    <button type="button" name="delete" value="<%=objidCrypt%>" class="btn btn-danger btn-sm" data-toggle="modal" data-target="#modaldeleteItem" data-objname="<%=drvList(ind.bk.obj.R_operadores.DBCol.Operador)%>" data-objid="<%=objidCrypt%>"><i class="fa fa-trash"></i></button>
                                    <%End If%>
                                </div>
                            </div>
				   
		            <div class="main col-sm-6"><!-- main start -->
					<div class="image-box team-member style-3-b">
                            <p><small>Ultima actualización: <%=drvList(ind.bk.obj.R_operadores.DBCol.ModifyDate)%> </small> </p>
                        
                        <div class="form-horizontal" style='max-width: 900px;'>
                             <%If drvList(ind.bk.obj.R_operadores.DBCol.FreeSale) <> cfr.CFRConst.genericOperator.PrivateGeneral Then%>
                            <div class="form-group form-ctrol-admin ">
                                <label for="tname_<%=objid%>" class="col-sm-3 control-label">Cliente</label>
                                <div class="col-sm-6">
                                    <input type="text" class="form-control" name="tname_<%=objid%>" id="tname_<%=objid%>" maxlength='50' value="<%=drvList(ind.bk.obj.R_operadores.DBCol.Operador)%>" >
                                </div>
                            </div>
                            <%End If%>
                            <div class="form-group form-ctrol-admin ">
                                <label for="cbotipocliente_<%=objid%>" class="col-sm-3 control-label">Tipo de cliente </label>
                                <div class="col-sm-8">
                                    
                                    <%If drvList(ind.bk.obj.R_operadores.DBCol.FreeSale) <> cfr.CFRConst.genericOperator.PrivateGeneral Then%>
                                    <select class="form-control customerlist-select" name="cbotipocliente_<%=objid%>">
                                        <option value="5" <%=UIHelper.cboSelectValue(customerType, 5)%>>Particulares</option>
                                        
                                        <option value="0" <%=UIHelper.cboSelectValue(customerType, 0)%>>Sin clasificar</option>
                                        <option value="1" <%=UIHelper.cboSelectValue(customerType, 1)%>>Agencias</option>
                                        <option value="2" <%=UIHelper.cboSelectValue(customerType, 2)%>>CRS</option>
                                        <option value="3" <%=UIHelper.cboSelectValue(customerType, 3)%>>TourOperadores</option>
                                        <option value="4" <%=UIHelper.cboSelectValue(customerType, 4)%>>Empresas</option>
                                       <option value="6" <%=UIHelper.cboSelectValue(customerType, 6)%>>Portal Web</option>
                                        
                                    </select>
                                    <%Else%>
                                    <p class="form-control-static">Este es el cliente por defecto usado para la venta libre, no puede cambiarle el tipo, siempre es particular. (Para cadenas solo puede existir en el mismo hotel)</p>
                                    <%End If%>
                                    <% If Me.hotel.Cadena1 <> 1012 And Me.hotel.Cadena1 <> 0 Then
                                            Dim clienteCadena As Integer = CType(drvList(ind.bk.obj.R_operadores.DBCol.cod_Cadena), Integer)
                                    %>
                                             Valido en
                                        <select class="form-control" name="clienteCadena_<%=objid%>">
                                    
                                    <option value='0' <%If clienteCadena = 0 Then Response.Write(" selected")%>>solo este hotel</option>
                                    <% If drvList((ind.bk.obj.R_operadores.DBCol.FreeSale)) = 1 Then%>
                                    <option value='1' <%If clienteCadena <> 0 Then Response.Write(" selected")%>>toda la cadena</option>
                                    <%End If%>
                                </select>
                                    <% End If%>
                                </div>
                            </div>
                             <%If drvList(ind.bk.obj.R_operadores.DBCol.FreeSale) <> cfr.CFRConst.genericOperator.PrivateGeneral Then%>
                            <div class="form-group form-ctrol-admin ">
                                <label for="trelease_<%=objid%>" class="col-sm-3 control-label">Generico</label>
                                <div class="col-sm-8">
                                    <% Dim cboGenerico As Integer = drvList(ind.bk.obj.R_operadores.DBCol.generico)%>
                                     <SELECT class="form-control customerlist-select" name="cbogenerico_<%=objid%>">
                                    <OPTION value=0 <%If cboGenerico = cfr.CFRConst.genericClient.NoGeneric Then Response.Write(" selected")%>>No es generico</OPTION> 
                                    <OPTION value=1 <%If cboGenerico = cfr.CFRConst.genericClient.YesGeneric Then Response.Write(" selected")%>>Si es generico</OPTION> 
                                    </SELECT>
                                    <p class="help-block">Si es generico el cliente(Agencia, empresa, etc) deberá introducir sus datos de empresa ademas de los del cliente final.</p>
                                </div>
                            </div>                            
                             <% End If%>
                            
                            <div class="form-group form-ctrol-admin ">
                                <label for="trelease_<%=objid%>" class="col-sm-3 control-label">Release </label>
                                <div class="col-sm-2">
                                    <div class="input-group">
                                    <input type="text" class="form-control releaseoperator" name="trelease_<%=objid%>" maxlength='2' value="<%=drvList(ind.bk.obj.R_operadores.DBCol.Release)%>" onkeypress="return solonums();">
                                    <span class="input-group-addon">dias</span>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group form-ctrol-admin ">
                                <label for="trelease_<%=objid%>" class="col-sm-3 control-label">Forma de pago</label>
                                <div class="col-sm-6">
                                    <select class="form-control customerlist-select" name="cboformapago_<%=objid%>">
                                        <%If Me.hotel.BookParam.CCC <> "" Then%>
                                            <option value="<%=CInt(CFRConst.formaPago.Transfer)%>" <%If drvList(ind.bk.obj.R_operadores.DBCol.forma_pago) = CInt(CFRConst.formaPago.Transfer) Then Response.Write(" selected")%>>Prepago (por transferencia)</option>
                                        <%End If%>
                                        <option value="<%=CInt(CFRConst.formaPago.DirectPay)%>" <%If drvList(ind.bk.obj.R_operadores.DBCol.forma_pago) = CInt(CFRConst.formaPago.DirectPay) Then Response.Write(" selected")%>>Pago directo en el hotel</option>
                                    </select>

                                </div>
                            </div>
                            
                            <div class="form-group form-ctrol-admin ">
                                <label for="trelease_<%=objid%>" class="col-sm-3 control-label">Restricciones</label>
                                <div class="col-sm-8">
                                    <% Dim aplicarYMprecio As String = drvList(ind.bk.obj.R_operadores.DBCol.aplicarYMprecio).ToString
                                        Dim aplicarYMdiasmin As String = drvList(ind.bk.obj.R_operadores.DBCol.aplicarYMdiasmin).ToString
                                        %>
                                    <%-- <SELECT class="form-control customerlist-select" name="cboaplicarYMprecio_<%=objid%>">
                                    <OPTION value='1' <%If aplicarYMprecio = True Then Response.Write(" selected ")%>>Aplicar YM Precio Minimo</OPTION> 
                                    <OPTION value='0' <%If aplicarYMprecio = False Then Response.Write(" selected ")%>>NO Aplicar YM Precio Minimo</OPTION> 
                                    </SELECT>
                                    <br />--%>
                                    <SELECT class="form-control customerlist-select" name="cboaplicarYMdiasmin_<%=objid%>">
                                    <OPTION value='1' <%If aplicarYMdiasmin = True Then Response.Write(" selected ")%>>Aplicar YM Dias Minimos</OPTION> 
                                    <OPTION value='0' <%If aplicarYMdiasmin = False Then Response.Write(" selected ")%>>NO Aplicar YM Dias Minimos</OPTION> 
                                    </SELECT>
                                </div>
                            </div>

                        </div>
                       </div> <!-- team-member end -->     
    				</div><!-- main end -->

					<div class="col-sm-6">
					    
                        <div>
                            <ul id="myTabs_<%=objid%>" class="nav nav-tabs style-3" role="tablist">
                                <li role="presentation" class="active"><a href="#tab_tariff_<%=objid%>" id="tab-tariff" role="tab" data-toggle="tab" aria-controls="home" aria-expanded="true"><i class="fa fa-eur"></i> Tarifas asignadas</a></li>
                                <li role="presentation" class=""><a href="#tab_access<%=objid%>" role="tab" id="tab-access" data-toggle="tab" aria-controls="profile" aria-expanded="false"><i class="fa fa-lock"></i> Datos de acceso</a></li>
                            </ul>
                            <div id="myTabContent" class="tab-content">
                                <div role="tabpanel" class="tab-pane fade active in" id="tab_tariff_<%=objid%>" aria-labelledby="Tariff-tab">
                                   <p>Estas son las tarifas que existen actualmente. Las que estan marcadas son las que tiene asignadas el cliente actualmente.</p>
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

                                <div role="tabpanel" class="tab-pane fade" id="tab_access<%=objid%>" aria-labelledby="tab-access">
                                    <%
                                        'control to show login and password
                                        If drvList(ind.bk.obj.R_operadores.DBCol.FreeSale) <> cfr.CFRConst.genericOperator.PrivateGeneral Then%>
                                    <p>Para poder acogerse a las tarifas asociadas debe introducir un login usuario para poder acceder al sistema.</p>
                                    <div class="form-horizontal" >
                                    <div class="form-group form-ctrol-admin ">
                                        <label for="tLogin_<%=objid%>" class="col-sm-3 control-label">Login</label>
                                        <div class="col-sm-3">
                                            <input type="text" class="form-control" name="tLogin_<%=objid%>" maxlength='50' value="<%=drvList(ind.bk.obj.R_operadores.DBCol.login)%>">
                                        </div>
                                    </div>
                                    <div class="form-group form-ctrol-admin ">
                                        <label for="tclave_<%=objid%>" class="col-sm-3 control-label">Clave</label>
                                        <div class="col-sm-3">
                                            <input type="password" class="form-control" name="tclave_<%=objid%>" maxlength="128" value="" autocomplete="new-password" placeholder="Vacía: conservar contraseña actual">
                                        </div>
                                    </div>
                                    <div class="form-group form-ctrol-admin ">
                                        <label for="temail_<%=objid%>" class="col-sm-3 control-label">Email</label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <input type="text" class="form-control emailoperator" name="temail_<%=objid%>" maxlength="255" value="<%=drvList(ind.bk.obj.R_operadores.DBCol.Email)%>">
                                                <span class="input-group-addon"><i class="fa fa-envelope fa-lg"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                        </div>
                                    <%else %>
                                    <p>Este es el cliente por defecto usado para la venta libre. No dispone de codigos de acceso.</p>
                                    <%End If%>
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
                    <h4 class="modal-title" id="myModalLabel">Borrar Cliente</h4>
                </div>
                <asp:UpdatePanel ID="upDel" runat="server">
                  <ContentTemplate>
                      <div class="modal-body">
                          <asp:HiddenField ID="itemdeletecode" runat="server" ClientIDMode="static"  />
                            Esta seguro de que quiere borrar el cliente seleccionado?
                                <h3 id="itemplacenameto" class="text-center"></h3>
                                <div class="resultmessageplace"></div>
                      </div>
                      <div class="modal-footer">
                          
                          <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                          <asp:Button class="btn btn-danger btndelete" ID="btndelete" Text="Borrar cliente" runat="server"  ClientIDMode="static" />
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
                            <h4 class="modal-title" id="modaledit_title">modal title</h4>
                        </div>
                        <div class="modal-body">
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                            <span id="modalfooterdefaultbuttonplace"></span>
                        </div>
                    </div>
                </div>
            </div>


    
    <div id="divhiddencontainer" class="hidden">
          <div id="createitem" class="clearfix">
            <div class="form-horizontal" style='max-width: 900px;'>
            <div class="form-group form-ctrol-admin ">
                <label for="cliente_0" class="col-sm-3 control-label">Nombre cliente </label>
                <div class="col-sm-8">
                    <asp:TextBox ID="cliente_0" runat="server" class="form-control" name="" maxlength="255" value="" placeholder="Introduzca aqui el nombre del nuevo cliente." ClientIDMode="Static" ></asp:TextBox>
                    
                 </div>
            </div>
            <div class="form-group form-ctrol-admin ">
                <label for="cbotipocliente_0" class="col-sm-3 control-label">Tipo de cliente </label>
                <div class="col-sm-8">
                    <asp:DropDownList ID="cbotipocliente_0" runat="server" class="form-control customerlist-select" ClientIDMode="Static">
                        <asp:ListItem Value="5">Particulares</asp:ListItem>
                        <asp:ListItem Value="1">Agencias</asp:ListItem>
                        <asp:ListItem Value="2">CRS</asp:ListItem>
                        <asp:ListItem Value="3">TourOperadores</asp:ListItem>
                        <asp:ListItem Value="4">Empresas</asp:ListItem>
                        <asp:ListItem Value="6">WebSite externo</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
                  </div> 
        </div> 
        <asp:LinkButton ID="btncreateitemcustomer" runat="server" CssClass="btn btn-animated btn-primary" UseSubmitBehavior="false" ClientIDMode="Static" ValidationGroup="createitem"><i class="fa fa-plus"></i> Crear cliente</asp:LinkButton>
    
    </div>

    

</div><!-- container end -->
    

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
    <script>
        $(document).ready(function () {
            console.log("tooltip-enabled")
            $('[data-toggle="tooltip"]').tooltip();

            $(".emailoperator")
                .popover({ trigger: 'focus', title: 'Email para recuperar la contraseña', content: "Introduzca el email del cliente para que pueda recuperar la contraseña. Si es un cliente generico deberia ser de la empresa/Agencia." })
                .blur(function () {
                    $(this).popover('hide');
                });
            $(".releaseoperator")
                .popover({ trigger: 'focus', title: 'Dias de antelación', content: "Introduzca aqui los dias de antelacion necesarios para realizar una reserva. Recuerde que siempre aplica el release de mayor valor incluyendo el de la tarifa. El global vale <%=Me.hotel.BookParam.daysInAdvance%> (ver parametros generales)." })
                .blur(function () {
                    $(this).popover('hide');
                });
            //$(".openedititem-newitem")(
            //    $("#createitem").appendTo(".modal-body");
            //    );



        });
        $(document).ready(function () {
            
            $(".savebutton").click(function () {
                
                var button = $(event.currentTarget); // Button that triggered the modal
                var par = button.parent();
                $("#field_itemobject").val(par.data('objid'));
            });

            
            $('#modaldeleteItem').on('show.bs.modal', function (event) {
                $('#modaldeleteItem').appendTo("form");
                $('#resultmessageplace').html('');
                $('#btndelete').removeClass('hidden');
            });


        });

      
    function filtercustomerview() {
        //var n = $("#chkcustomerview:checked").length;
        //$("#test").text(n + (n === 1 ? " is" : " are") + " checked!");

        $(".chkcustomerview").each(function (index) {
            var isChecked = $(this).is(':checked');
            var customertypeval = $(this).val();

            if (isChecked) {
                $(".customertype_" + customertypeval).show();
                console.log("showing " + customertypeval);
            }
            else {
                $(".customertype_" + customertypeval).hide();
                console.log("hidding " + customertypeval);
            }

        });


    }

    </script>
</asp:Content>

