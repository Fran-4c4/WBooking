<%@ Page Title="CFR-Politicas de Cancelación de tarifas" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="policyCancelTariff.aspx.vb" Inherits="policyCancelTariff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div class="container">
        <div class="col-xs-8">
                  <h1 class="sectiontitle"><i class="fa fa-wrench"></i> Condiciones de cancelación para tarifas</h1>
        <p>Estas son las condiciones da cancelación que puede asociar a las diferentes tarifas.</p>
        </div>

        <div class="col-xs-4">
            
                        
                                <a href="#" onclick="setvaluesforedit(0);" class="btn btn-animated btn-primary btn-lg"> Crear nueva condición de cancelación <i class="glyphicon glyphicon-plus-sign"></i></a>
                         
        </div>
        <div class="separator-2"></div>
    </div>

    <div class="container">

        <%
            Me.policyList = Me.getConditionsList()
            %>
              <%--  <script >
            <%=Me.getPolicyCancelDataJson%>
        </script>--%>
        <%
            For Each polCondObj As R_PolicyCancel In Me.policyList%>

        <div class="col-xs-3 mb-20">
            <div id="condition_<%=polCondObj.policyID%>" class="panel panel-primary bordered"  data-cancelconditiondata=<%=Me.getPolicyCancelDataJson(polCondObj)%>>
             <div class="panel-heading " >
                 <div class="">
                        <span class="item-code"><%=polCondObj.policyID%></span> <span class="policyname"><%=polCondObj.policyName%></span>
                    </div>                  
            </div>

                <div class="panel-body pv-10 ph-10 ">
                    <div>
                        
                    </div>
                    <span class="span-item-modify-date "><i class="fa fa-calendar-o"></i><%=polCondObj.datemodified%></span>

                    <div class="mb-20 shadow bordered light-gray-bg text-center">
                        <p class="mb-10 mt-10"><%=Me.getCancelPolicyMessage(polCondObj)%></p>
                    </div>
                    <a href="#" onclick="deletecondition(<%=polCondObj.policyID%>);" class="btn btn-danger btn-sm margin-clear">Borrar<i class="fa fa-trash pl-10"></i></a>
                    <a href="#" onclick="setvaluesforedit(<%=polCondObj.policyID%>);" class="btn btn-default btn-sm   margin-clear">Modificar<i class="fa fa-arrow-right pl-10"></i></a>
                </div>
          
            </div>

            
            
        </div>

        <% Next%>
    </div>
    <div class="container" >
        <asp:Panel ID="pnlcancelconditionedition" runat="server" ClientIDMode="static" style="display: none;">
            <div class="row">
                            <%-- my row --%>
                            <div class="col-xs-12" id="canceledition" style="">
                                
                                <div class="edit-block">
                                    <div class="row edit-block-title nomargin">
                                        <div class="">
                                            <div class="col-md-11">
                                                <h4><i class="fa fa-edit"></i> Editar condición de cancelación</h4>
                                            </div>
                                            <div class="pull-right pr-10"  >
                                                <button type="button" class="close" onclick="closemydiv('pnlcancelconditionedition');" aria-label="Close"><span aria-hidden="true">×</span></button>
                                                <script>
                                                    function closemydiv(div) {
                                                      $("#" + div).hide(500);
                                                    }
                                                </script>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="edit-block-body">
                                        <asp:HiddenField ID="hdfcancelconditioncode" runat="server" ClientIDMode="Static" Value="0" /> 
                                        <div class="row">
                                            <div class="col-xs-12 col-md-7">
                                                <div class="row">
                                                    <div class="col-md-12 control-group form-group">
                                                        <label for="txtconditionname" style="display: block;">Nombre de la condición</label>
                                                        <asp:TextBox ID="txtconditionname" runat="server" CssClass="form-control " Style="width: 300px;" ClientIDMode="static"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="txtconditionnamerequired" runat="server" ErrorMessage="Debes introducir el nombre de la condición" ControlToValidate="txtconditionname" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div id="question-4" class="row">
                                                    <div class="span12 col-md-12 control-group form-group">
                                                        <label for="cbocanceluntilvalue" style="display: block;">
                                                            Cuanto pagará el cliente si cancela la reserva hasta?
                                                        </label>
                                                        <div style="display: inline-block;">
                                                            <div>
                                                                <asp:DropDownList ID="cbocanceluntilday" runat="server" ClientIDMode="static" cssclass="form-control " style="width: 300px;">
                                                                <asp:ListItem Value="-1">Seleccionar...</asp:ListItem>
                                                                <asp:ListItem value="14h">hasta las 14:00 del día de llegada</asp:ListItem >
                                                                <asp:ListItem  value="18h">hasta las 18:00 del día de llegada</asp:ListItem>
                                                                <asp:ListItem  value="1d">hasta 1 día antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="2d">hasta 2 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="3d">hasta 3 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="4d">hasta 4 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="5d">hasta 5 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="6d">hasta 6 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="7d">hasta 7 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="8d">hasta 8 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="9d">hasta 9 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="10d">hasta 10 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="14d">hasta 14 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="30d">hasta 30 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="42d">hasta 42 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="60d">hasta 60 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="90d">hasta 90 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="120d">hasta 120 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="150d">hasta 150 días antes de la fecha de llegada</asp:ListItem>
                                                                <asp:ListItem  value="180d">hasta 180 días antes de la fecha de llegada</asp:ListItem>
                                                            </asp:DropDownList>   

                                                            </div>
                                                            <asp:RequiredFieldValidator ID="cbocanceluntildayRequiredFieldValidator2" runat="server" ErrorMessage="Debes seleccionar una opción" ControlToValidate="cbocanceluntilday" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>                                                  
                                                       </div>
                                                         <div style="display: inline-block;">
                                                             <div>
                                                            <span style="display: inline-block; font-weight: bold; padding-left: 10px; padding-right: 10px;">pagará</span>
                                                                <asp:DropDownList ID="cbocanceluntilvalue" runat="server" ClientIDMode="static" cssclass="form-control " style="width: 300px; display: inline-block;">
                                                                <asp:ListItem  value="-1">Seleccionar...</asp:ListItem>
                                                                <asp:ListItem  value="0">cancelación gratuita</asp:ListItem>
                                                                <asp:ListItem  value="1">la primera noche</asp:ListItem>
                                                                <asp:ListItem  value="30">un 30% del precio total</asp:ListItem>
                                                                <asp:ListItem  value="50">un 50% del precio total</asp:ListItem>
                                                                <asp:ListItem  value="70">un 70% del precio total</asp:ListItem>
                                                                <asp:ListItem  value="90">un 90% del precio total</asp:ListItem>
                                                                <asp:ListItem  value="100">un 100% del precio total</asp:ListItem>
                                                            </asp:DropDownList>
          
                                                             </div>
                                                             <div>
                                                                <asp:RequiredFieldValidator ID="cbocanceluntilvalueRequiredFieldValidator1" runat="server" ErrorMessage="Debes seleccionar una opción" ControlToValidate="cbocanceluntilvalue" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                       
                                                             </div>
                                                         </div>
                                                    </div>
                                                </div>

                                                <div id="div-question-cancelafter" class="row">
                                                    <div class="col-md-12 control-group form-group">
                                                        <label for="cbocancelafterdayvalue" class="">¿Cuánto tendrá que pagar el cliente si cancela la reserva después de la selección anterior?</label>
                                                        <asp:DropDownList ID="cbocancelafterdayvalue" runat="server" ClientIDMode="static" cssclass="form-control " style="width: 300px;"> 
                                                            <asp:ListItem  value="-1">Seleccionar...</asp:ListItem>
                                                            <asp:ListItem  value="1">la primera noche</asp:ListItem>
                                                            <asp:ListItem  value="100">un 100% del precio total</asp:ListItem>
                                                            <asp:ListItem  value="30">un 30% del precio total</asp:ListItem>
                                                            <asp:ListItem  value="50">un 50% del precio total</asp:ListItem>
                                                            <asp:ListItem  value="70">un 70% del precio total</asp:ListItem>
                                                            <asp:ListItem  value="90">un 90% del precio total</asp:ListItem>
                                                        </asp:DropDownList >
                                                        <asp:RequiredFieldValidator ID="cbocancelafterdayvalueRequiredFieldValidator1" runat="server" ErrorMessage="Debes seleccionar una opción" ControlToValidate="cbocancelafterdayvalue" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                  

                                            <div class="cancel-policy-summary col-md-5" style="display: none;">
                                                <h4 style="font-weight: bold;">Resumen de las condiciones como las verá el cliente.</h4>
                                                <div class="" id="cancel-condition-description"></div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row edit-block-footer">
                                        <div class="col-xs-12">
                                            <button type="button" class="btn btn-primary btn-large btn-lg " onclick="btnsavepolicycondition_click();"><span class="fa fa-save" ></span>Guardar condición de cancelación </button>
                                            <div class="hidden "><asp:LinkButton ID="btnsavepolicycondition" runat="server"  cssclass="btn btn-primary btn-large btn-lg" ClientIDMode="static"></asp:LinkButton></div>                                              
                                        </div>
                                    </div>
                                </div>
                                <%-- my row --%>
                            </div>

                        </div>

        </asp:Panel>
        <%--container--%>
        </div>
    
    <div class="container">
        <div class="footerinfo">
         <h3>Información </h3>
        <ul>
            <li>Cree condiciones para usar en las diferentes tarifas. Estas condiciones son mostradas al cliente y pueden ser usadas para automatizar cancelaciones de reservas no confirmadas por ejemplo.
            </li>
            <li>Estas condiciones de reservas se añadiran en la reserva junto con las condiciones generales de cancelación.</li>
        </ul>           
        </div>
    </div>

                                           <div class="hidden ">
                                            <asp:HiddenField ID="hdfdeleteitemid" runat="server" ClientIDMode="Static" />
                                            <asp:LinkButton ID="btndeleteitem" runat="server"  cssclass="" ClientIDMode="Static" ValidationGroup="cancelconditiondeleteelement">test</asp:LinkButton>                                              
                                        </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
    <script>
        
        function deletecondition(objid) {
            $("#hdfdeleteitemid").val(objid);
            showmodal("modaldeleteCondition1", "Eliminar condición de cancelación", "Desea borrar esta condición?","Borrar", "deleteconditionconfirmed();");
        }

        function deleteconditionconfirmed() {
            document.getElementById("btndeleteitem").click();
        }

        function setvaluesforedit(objid) {
            $("#hdfcancelconditioncode").val(objid);
            var txtname = $("#condition_" + objid + " .policyname").html();
            var data = $("#condition_" + objid).data("cancelconditiondata");
            if (objid==0) {
                data = JSON.parse('{ "id": "0", "untilDay": "14d", "untilValue": "0", "afterValue": "1" }');
            }
            console.log(data);
            //data = JSON.parse(data);
            
            $("#txtconditionname").val(txtname);

            $("#cbocanceluntilday").val(data.untilDay);
            $("#cbocanceluntilvalue").val(data.untilValue);
            $("#cbocancelafterdayvalue").val(data.afterValue);

            $("#pnlcancelconditionedition").show(500);

        }

        function btnsavepolicycondition_click() {
            $("#hdfsubmitproc").val(1);
            document.getElementById("btnsavepolicycondition").click();
        }

        
        $(document).ready(function () {
            

            $("#cbocanceluntilvalue").change(function() {
                cancelconditionchange();
            });
            $("#cbocanceluntilday").change(function() {
                cancelconditionchange();
            });
            $("#cbocancelafterdayvalue").change(function () {
                cancelconditionchange();
            });

            function cancelconditionchange() {
                var valpercent1 = $("#cbocanceluntilvalue").val();
                var cbocanceluntiltxt = $("#cbocanceluntilvalue option:selected").text();
                var valdayuntil = $("#cbocanceluntilday").val();
                var valdayuntiltxt = $("#cbocanceluntilday option:selected").text();
                var valdayaftervalpercent = $("#cbocancelafterdayvalue").val();
                var valdayaftertxt = $("#cbocancelafterdayvalue option:selected").text();

                
                var msg1 = "Si cancelas <strong>{mycanceluntil}</strong> pagarás un <strong>{myporcentage}</strong>";
                var msg2 = " , o <strong>{myporcentage1}</strong> si cancelas despues.";
                var msg3 = "<strong>{mycanceluntil}</strong> la cancelación es gratuita.";
                var msg4 = " sino pagarás <strong>{myporcentage1}</strong> si cancelas despues.";
                var msgcond = "";

                if (valpercent1 == '0') {
                    msgcond = msg3+msg4;
                    msgcond = replaceAll(msgcond, "{mycanceluntil}", valdayuntiltxt);
                    msgcond = replaceAll(msgcond, "{myporcentage1}", valdayaftertxt);
                } else {
                    msgcond = msg1 + msg2;
                    msgcond = replaceAll(msgcond, "{myporcentage}", cbocanceluntiltxt);
                    msgcond = replaceAll(msgcond, "{mycanceluntil}", valdayuntiltxt);
                    msgcond = replaceAll(msgcond, "{myporcentage1}", valdayaftertxt);

                }

                if (msgcond!="") {
                    $(".cancel-policy-summary").show(200);
                } else {
                    $(".cancel-policy-summary").show(200);
                }

                $("#cancel-condition-description").html(msgcond);
            }

            function replaceAll(str, find, replace) {
                return str.replace(new RegExp(find, 'g'), replace);
            }

        });

    </script>
</asp:Content>

