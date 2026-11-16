<%@ Page Title="CFR-Usuarios Sistema" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="adminusers.aspx.vb" Inherits="Admin_adminusers" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
    <style>
        .widget-user .list-group { margin-bottom: 0; }
        .widget-user .label-info { float: right; }
        .widget-user li.list-group-item {border-radius: 0;border-top: 1px solid #ddd; margin-bottom: 5px}
        .widget-user li.list-group-item:hover { background-color: rgba(86,61,124,.1); }
        .widget-user .mic-info { font-size: 11px; }
        .widget-user .action {     position: absolute;top: 0px;right: 10px;}
        .widget-user .comment-text { font-size: 12px; }
       
        #permisionsdiv input[type="checkbox"]:checked{ color: #0c9ec7;} 
        #permisionsdiv .checkbox label { color: #0c9ec7;}

        .list-group-item.active,  .list-group-item.active:focus , li.active {background-color: rgba(76, 64, 95, 0.73);}
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
                     <h1 class="sectiontitle"><i class="fa fa-user "></i> Usuarios del sistema</h1>
                </div>
            <div class="col-sm-4">
                <button type="button" name="newitem" value="<%=UIHelper.URLencryptData("0")%>" class="openedititem-newitemuser  btn btn-animated btn-primary btn-lg" data-toggle="modal" data-target="#modaledit" data-objname="Nueva temporada" data-objid="0">Crear nuevo usuario<i class="glyphicon glyphicon-plus-sign"></i></button>
            </div>
       </div>
        <div class="separator-2"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
                   <div class="hidden">
                    <asp:Button ID="btnseeuserpermisionshdd" runat="server" Text="Button" cssclass="hidden" ClientIDMode="static" ValidationGroup="userseepermission" />
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="container">
        <div class="row">
            <div class="col-sm-4">
 <asp:UpdatePanel ID="UpdatePanel_userlist" runat="server" UpdateMode="Conditional">
                 <%--<Triggers>
                     <asp:AsyncPostBackTrigger ControlID="btnsubmit_adminusers"  />
                 </Triggers>--%>
                        <ContentTemplate>

                    <div class="widget-user">
                       <%-- <%  Dim dt As Date = Date.Now %>
                        <%= "Time:" & dt.ToLongTimeString & " -- " & dt.Millisecond%>--%>
                       
    <% 'Loop configuration
        Dim dvList As DataView = Me.dvListUsers
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
                           
            Else%>
                <p>Seleccione un usuario para ver/modificar los permisos</p>
                <ul class="list-group">
                    
                            
             
                <%'LOOP OVER ROWS 
                    For Each drvList As DataRowView In dvList
                        'CONFIG VALUES NEEDED FOR PRESENTATION
                        Dim objid As String = drvList(ind.bk.obj.R_Usuarios.DBCol.id_usuario)
                        Dim objidCrypt As String = UIHelper.URLencryptData(objid)
                   
                        'START ITEMTEMPLATE +++++++++++++++++++++++++++++++++
                    %>
                                    
                    <li id="li_<%=objid %>" class="list-group-item " data-objid="<%=objid %>" >
                        <div class="row" >
                        <div class="col-sm-12">
                            <div class="click seeuserpermisions_click" data-objid="<%=objid %>" >
                             <div>
                                <strong><i class="fa fa-user"></i> <span id="username_<%=objid %>"><%= drvList(ind.bk.obj.R_Usuarios.DBCol.usuario)%></span></strong>
                                <div class="mic-info">
                                    <span  id="perfilholder_<%=objid %>"><%= drvList(ind.bk.obj.R_Usuarios.DBCol.perfil)%></span>
                                </div>
                                <div><i class="fa fa-lock"></i> <strong>Login:</strong> <span id="loginholder_<%=objid %>"><%= drvList(ind.bk.obj.R_Usuarios.DBCol.login)%></span> <strong>Clave:</strong> no recuperable</div>
                            </div>
                            <div class="comment-text">
                                <i class="fa fa-envelope"></i> <span id="emailholder_<%=objid %>"><%= drvList(ind.bk.obj.R_Usuarios.DBCol.email)%></span>
                            </div>                               
                            </div>

                            <div class="action" data-objid="<%=objid %>">
                                      <span data-placement="top" data-toggle="tooltip" title="Editar usuario"><button type="button" class="btn-primary btn-sm edituser_click" data-title="Edit" data-toggle="modal" data-target="#modaledit" data-objid="<%=objid %>" ><span class="fa fa-pencil"></span></button></span>
                                      <span data-placement="top" data-toggle="tooltip" title="Borrar este usuario"><button type="button"  class="btn-danger btn-sm" data-title="Delete" data-toggle="modal" data-target="#modaldeleteItem" data-objid="<%=objid %>" data-objnameid="username_"><span class="fa fa-trash"></span></button></span>
                             </div>
                                               

                        </div>

                            </div>
                    </li>
        
                        <%'END ITEMTEMPLATE +++++++++++++++++++++++++++++++++
                        Next
                        
                    End If 'END CONDITIONAL LOOP +++++++++++++++++++++++++++++++++
                    
        %>
                           
        </ul>
                </div>
    </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
            
   <div class="col-sm-8">
       
           
               <asp:UpdatePanel ID="UpdatePanel_userpermision" runat="server">
                   <ContentTemplate>
                       <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="false">
                       <h3><i class="fa fa-lock"></i>Permisos del usuario <strong><asp:Literal ID="litUserName" runat="server"></asp:Literal></strong></h3>
                           <div id="permisionsdiv" class="">
                               <div class="row">
                                   <div class="col-sm-6">
                                       <h4 class="security-title">Permisos de acceso generico </h4>
                                       <p class="help-block"><small>(Recomendado para administradores)</small></p>
                                       <ul class="list-icons">
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox1" value="1" <%=tienepermiso(1)%>>
                                                   Control del Sistema</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox2" value="12" <%=tienepermiso(12)%>>
                                                   Configurar sistema de reservas</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox3" value="6" <%=tienepermiso(6)%>>
                                                   Configuracion de Datos hotel</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox4" value="17" <%=tienepermiso(17)%>>
                                                   Usuarios Crear/Editar/Borrar</label></li>
                                       </ul>
                                   </div>
                                   <div class="col-sm-6">
                                       <h4 class="security-title">Control Yield Management</h4>
                                       <ul class="list-icons">
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox5" value="5" <%=tienepermiso(5)%>>
                                                   Control Cierre Ventas</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox6" value="3" <%=tienepermiso(3)%>>
                                                   Control Precio minimo</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox7" value="4" <%=tienepermiso(4)%>>
                                                   Control Dias Minimos</label></li>
                                       </ul>
                                   </div>
                               </div>
                               <div class="row">
                                   <div class="col-sm-6">
                                       <h4 class="security-title"><i class="fa fa-bar-chart"></i>Informes</h4>
                                       <ul class="list-icons">
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox10" value="18" <%=tienepermiso(18)%>>
                                                   Informes Disponibilidad</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox11" value="7" <%=tienepermiso(7)%>>
                                                   Informes de reservas</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox12" value="10" <%=tienepermiso(10)%>>
                                                   Informes de Confirmaciones</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox13" value="9" <%=tienepermiso(9)%>>
                                                   Informes de Cancelaciones</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox14" value="19" <%=tienepermiso(19)%>>
                                                   Informes de Tarifas</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox15" value="8" <%=tienepermiso(8)%>>
                                                   Informes de facturacion</label></li>
                                       </ul>
                                   </div>
                                   <div class="col-sm-6">

                                       <h4 class="security-title"><i class="fa fa-cogs"></i> Sistema de reservas</h4>
                                       <ul class="list-icons">
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox16" value="11" <%=tienepermiso(11)%>>
                                               Clientes Crear/Editar/Borrar</></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox17" value="13" <%=tienepermiso(13)%>>
                                                   Ofertas Crear/Editar/Borrar</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox18" value="15" <%=tienepermiso(15)%>>
                                                   Tarifas Crear/Editar/Borrar</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox8" value="2" <%=tienepermiso(2)%>>
                                                   Cambiar Disponibilidad</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox9" value="20" <%=tienepermiso(20)%>>
                                                   Reservas Confirmar/Cancelar</label></li>
                                           <%--<li class="security-li click"><label><input  TYPE="checkbox" NAME="permisos" id="permisos"  value="14" <%=tienepermiso(14)%>> Configurar Diseño reservas</label></li>--%>
                                       </ul>
                                   </div>
                               </div>
                               <div class="row">
                                   <div class="col-sm-6">
                                       <h4 class="security-title">Facturacion Sistema</h4>
                                       <ul class="list-icons">
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="permisos" value="23" <%=tienepermiso(23)%>>
                                                   Control Total Facturacion Sistema</label></li>
                                           <li class="security-li click">
                                               <label>
                                                   <input type="checkbox" name="permisos" id="Checkbox20" value="22" <%=tienepermiso(22)%>>
                                                   Acceso Solo Lectura Facturacion Sistema</label></li>
                                       </ul>
                                   </div>
                               </div>
                           </div>
                           

                        <div class="row">
                            <div class="clearfix">
                               <asp:Literal ID="litrespermissions" runat="server"></asp:Literal>
                                

                                <div class="col-xs-4">
                                    <asp:LinkButton ID="btnsavepermisions" runat="server" CssClass="btn btn-primary btn-lg btn-block" ValidationGroup="usersavepermissions">Guardar permisos <span class="fa fa-save"></span></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                           

                        </div>

                      </asp:PlaceHolder>
                   
                   </ContentTemplate>
                   <Triggers>
                       <asp:AsyncPostBackTrigger  ControlID="btnseeuserpermisionshdd"/>
                   </Triggers>
               </asp:UpdatePanel>
                       </div>                 

   
    </div>
        
        <div class="footerinfo">
            <h3>Información </h3>
            <ul>
                <li>El campo perfil es solamente descriptivo y es solo para ayudarle al mantenimiento de usuarios.
                </li>
                <li>Las contraseñas no se pueden consultar ni enviar por email. Un administrador puede asignar una nueva.</li>
            </ul>
        </div>

        </div>
    
    
    <div class="modal " id="modaledit" tabindex="-1" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span class="glyphicon glyphicon-remove"></span></button>
                    <h4 class="modal-title custom_align" id="Heading">Modificar datos de usuario</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="form-horizontal">
                        <div class="form-group form-ctrol-admin ">
                            <asp:HiddenField ID="useridhhhedit" runat="server" Value="0" ClientIDMode="Static" />
                            <label for="tusername" class="col-sm-4 control-label">Nombre de usuario</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tusername" runat="server"  CssClass="form-control" maxlength="50" ClientIDMode="Static"></asp:TextBox>
                                <p class="help-block with-errors">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="tusername" runat="server" ValidationGroup="createuser" ErrorMessage="El nombre de usuario debe tener mas de 8 caracteres" Display="Dynamic"></asp:RequiredFieldValidator></p>
                            </div>
                        </div>
                        <div class="form-group form-ctrol-admin ">
                            <label for="tperfil" class="col-sm-4 control-label">Perfil</label>
                            <div class="col-sm-6">
                                <asp:TextBox runat="server"   cssclass="form-control" id="tperfil" maxlength='50' value="" ClientIDMode="static"></asp:TextBox>
                                <p class="help-block">El perfil es informativo. Sirve para saber la categoria.</p>
                            </div>
                        </div>

                        <div class="form-group form-ctrol-admin ">
                            <label for="tLogin" class="col-sm-4 control-label">Login</label>

                            <div class="col-sm-6">
                                <span data-placement="top" data-toggle="tooltip" title="El login de usuario debe ser unico en el sistema. Maximo 50 caracteres."><asp:TextBox runat="server"   cssclass="form-control" id="tlogin" maxlength='50' value=""   ClientIDMode="static"></asp:TextBox></span>
                                <p class="help-block with-errors"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="tlogin" runat="server"   ValidationGroup="createuser" ErrorMessage="Falta el login de usuario" Display="Dynamic"></asp:RequiredFieldValidator>
                                   
                                </p>
                            </div>
                            
                        </div>
                        <div class="form-group form-ctrol-admin ">
                            <label for="tclave" class="col-sm-4 control-label">Nueva clave</label>
                            <div class="col-sm-6">
                                <asp:TextBox runat="server" cssclass="form-control" id="tclave" maxlength="128" TextMode="Password" ClientIDMode="static" data-placement="top" data-toggle="tooltip" title="Déjela vacía al editar para conservar la clave actual."></asp:TextBox>
                                <p class="help-block with-errors">Obligatoria al crear. Al editar, déjela vacía para conservarla.</p>
                            </div>
                             
                        </div>
                        <div class="form-group form-ctrol-admin ">
                            <label for="temail" class="col-sm-4 control-label">Email</label>
                            <div class="col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox runat="server"   cssclass="form-control" id="temail" maxlength="255"  ClientIDMode="static"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-envelope fa-lg"></i></span>
                                </div>
                                <p class="help-block with-errors"><asp:RegularExpressionValidator ID="regExpEmail" runat="server" ControlToValidate="temail"
                            ErrorMessage="El email no es correcto" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ValidationGroup="createuser"></asp:RegularExpressionValidator></p>
                            </div>
                        </div>
                    </div>
                            <asp:Literal ID="liteditmessage" runat="server"></asp:Literal>
                            <div class="hidden">
                                <asp:LinkButton ID="btnsubmit_adminusers" runat="server" CssClass="btn btn-primary"   ClientIDMode="Static">Crear Usuario <i class="fa fa-plus"></i></asp:LinkButton>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnsubmit_adminusers" />
                        </Triggers>

                   </asp:UpdatePanel>
                    


                </div>
                <div class="modal-footer ">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button id="modaleditprimarybutton" type="button" class="btn btn-primary" OnClick="validateeditinput();" >Crear usuario <i class="fa fa-plus"></i></button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    
    
            <div class="modal " id="modaldeleteItem" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="H1">Borrar Temporada</h4>
                    </div>
                    <div class="modal-body">
                        Esta seguro de que quiere borrar el usuario seleccionado?
                                <h3 id="itemplacenameto" class="text-center"></h3>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>

                        <asp:Button ID="btnDeleteItem" CssClass="btn btn-danger" runat="server" Text="Borrar" ClientIDMode="Static" ValidationGroup="deletevalidation"/>
                    </div>
                </div>
            </div>
        </div>
    


</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
    <script >
        
        //Initial bind
        $(document).ready(function () {
            bindcontrolevents();

            

            $("#mytable #checkall").click(function () {
                if ($("#mytable #checkall").is(':checked')) {
                    $("#mytable input[type=checkbox]").each(function () {
                        $(this).prop("checked", true);
                    });

                } else {
                    $("#mytable input[type=checkbox]").each(function () {
                        $(this).prop("checked", false);
                    });
                }
            });


        });

        //Re-bind for callbacks


        prm.add_endRequest(function () {
            unbindcontrolevents();
            bindcontrolevents();
            console.log("prm.add_endRequest");
        });



        function unbindcontrolevents() {
            //$("[data-toggle=tooltip]").unbind();
            $(".seeuserpermisions_click").unbind();
            $(".edituser_click").unbind();
        }
        
        function bindcontrolevents() {
                //jQuery is wrapped in BindEvents function so it can be re-bound after each callback.
                //Your code would replace the following line:
                $("[data-toggle=tooltip]").tooltip();
                
       
                $(".seeuserpermisions_click").on("click", function () {
                    var button = $(event.currentTarget); // Button that triggered the modal
                    var itemid = button.data('objid');
                    $(".list-group-item.active").removeClass("active");
                    $("#li_" + itemid).addClass("active");

                    $("#field_itemobject").val(itemid);
                    var name = $("#username_" + itemid).html();
                    $("#field_itemname").val(name);
                    $("#field_itemobject").val(itemid);
                    document.getElementById('btnseeuserpermisionshdd').click();

                });
   
                 $(".edituser_click").click(function () {
                        var button = $(event.currentTarget); // Button that triggered the modal
                        var itemid = button.data('objid');
                        $("#useridhhhedit").val(itemid);
                        $("#field_itemobject").val(itemid);

                        var name = $("#username_" + itemid).html();
                        $("#field_itemname").val(name);

                        $("#tusername").val(name);
                        $("#tlogin").val($("#loginholder_"+itemid).html());
                        $("#tclave").val("");
                        $("#tperfil").val($("#perfilholder_" + itemid).html());
                        $("#temail").val($("#emailholder_" + itemid).html());
                        $("#modaleditprimarybutton").html("Modificar usuario <i class='fa fa-save'></i>");


                    });

        }
            
             $(".openedititem-newitemuser").click(function () {
                    $("#useridhhhedit").val(0);
                    $("#tusername").val("");
                    $("#tlogin").val("");
                    $("#tclave").val("");
                    $("#tperfil").val("");
                    $("#temail").val("");
                    $("#modaleditprimarybutton").html("Crear usuario <i class='fa fa-plus'></i>");

                });


    function validateeditinput() {
            if (Page_ClientValidate("createuser")) {
                document.getElementById('btnsubmit_adminusers').click();
            }
    }




    </script>
    
    <script type="text/javascript">
        function pageLoad() {
            Sys.WebForms.PageRequestManager.getInstance().
                add_endRequest(onEndRequest);
        }

        function onEndRequest(sender, args) {
            //var lbl = document.getElementById("lblerrors");
            //lbl.innerHTML =""+ args.get_error().message;
            //args.set_errorHandled(true);
        }
    </script>
</asp:Content>

