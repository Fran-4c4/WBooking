<%@ Page Title="CFR-Unidad-Edición" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="roomEdit.aspx.vb" Inherits="Admin_roomEdit" ValidateRequest="false" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
    <style>
        .btn-img-gallery-delete{
                position: absolute;   bottom:0px;  z-index: 15;   right:3px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="breadcrumb-container">
        <div class="container-full">
            <ol class="breadcrumb">
                <li><i class="fa fa-home pr-10"></i><a href="<%= linkcnf("default.aspx")%>">Inicio</a></li>
                <li><a href='<%= linkcnf("roomlist.aspx")%>'>Unidades</a> </li>
                <li class="active">Editar Unidad</li>
            </ol>
        </div>
    </div>
    <div class="container">
        <div class="row">
             <div class="col-xs-8 col-sm-8">
                 <h1 class="sectiontitle">Editar unidad</h1>
                 
                  <i class="fa fa-asterisk"></i><asp:DropDownList ID="cbounit" runat="server" AutoPostBack="True"></asp:DropDownList>
             </div>
             <div class="col-xs-4 col-sm-4">
                 <div class="text-right"><a class="btn btn-animated btn-primary btn-lg" href="<%= linkcnf("roomedit.aspx?roomid=0")%>">Crear nueva unidad<i class="glyphicon glyphicon-plus-sign"></i></a></div>
             </div>
         </div>
        <div class="separator-2"></div>
        
    </div>

    <div class="container">
        <asp:HiddenField ID="roomidhdd" runat="server" value="0"/>
        <div class="title-block-2">
        <h3>
            <span Class="unit-code label label-default"><asp:Literal ID="roomidLbl" runat="server" Text="00000"></asp:Literal></span> <asp:Label ID="roomNameLbl" runat="server" text="Nueva unidad"></asp:Label>
        </h3>
            </div>



        <div class="container">
            <div class="row form-group form-ctrol-admin">
                <div class="col-sm-2 adminlabel "><img src="images/lang/lang_es.gif" style="display:inline-block;"/> <span class="edit-name">Nombre</span></div>
                <div class="col-sm-10 ">
                    <asp:TextBox ID="roomName" runat="server" value="" Columns="50" CssClass="" placeholder="Nombre de la habitacion" ClientIDMode="static"  data-toggle="tooltip" title="Introduzca aqui el nombre de la unidad en español."></asp:TextBox>
                    <asp:RequiredFieldValidator ID="roomName_RequiredFieldValidator1" runat="server" ErrorMessage="Debe introducir el nombre de la unidad(habitacion)" SetFocusOnError="True" ControlToValidate="roomName" Display="Dynamic" CssClass="" Visible="True" ClientIDMode="Static"></asp:RequiredFieldValidator>
                   <asp:PlaceHolder ID="plhroombase" runat="server" Visible="true">
                     basada en generica<asp:DropDownList ID="roomBaseCbo" class="" runat="server" ClientIDMode="Static"></asp:DropDownList>
                       </asp:PlaceHolder>
                    <p class="help-block">Mas abajo deberá introducir el nombre en los diferentes idiomas.</p>

                </div>
            </div>
           
            <div class="row form-group form-ctrol-admin">
                <div class="col-sm-2 adminlabel">Numero de unidades de este tipo</div>
                <div class="col-sm-10 ">
                    <asp:TextBox ID="numRoomsAvailable" runat="server" value="0" Columns="10" CssClass="onlynum" placeholder="Numero de unidades" ClientIDMode="static"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="numRoomsAvailable_RequiredFieldValidator1" runat="server" ErrorMessage="Debe introducir el numero de unidades de este tipo." SetFocusOnError="True" ControlToValidate="roomName" Display="Dynamic" CssClass="" Visible="True" ClientIDMode="Static"></asp:RequiredFieldValidator>
                  
                    <p class="help-block">Introduzca el numero de unidades de este tipo que posee. El sistema le avisará cuando intente sobrepasar el numero maximo, sin limitarselo.</p>

                </div>
            </div>
            <asp:PlaceHolder ID="plhroomCombinatedCbo" runat="server" Visible="true">
            <div class="row form-group form-ctrol-admin ">
                <div class="col-sm-2 adminlabel">Habitacion combinada?</div>
                <div class="col-sm-10">
                    <asp:DropDownList ID="roomCombinatedCbo" class="" runat="server" ClientIDMode="Static"></asp:DropDownList>
                    <asp:HiddenField ID="hddromcombinatedid" runat="server" Value="0" />
                    <p class="help-block">La habitación combinada se utiliza cuando ambas habitaciones son la misma (Doble uso individual=Doble) y tienen la misma disponibilidad.</p>
                </div>
            </div>
                </asp:PlaceHolder>

            <div class="row form-group form-ctrol-admin">
                <div class="col-sm-2 adminlabel">Numero maximo de personas:</div>
                <div class="col-sm-10">
                    <asp:DropDownList ID="roomPaxIncludedCbo" class="" runat="server" ClientIDMode="Static"></asp:DropDownList>
                <asp:PlaceHolder ID="plhnumpaxextra" runat="server" Visible="true">
                    <div class="small " style="display: inline;">
                        <asp:DropDownList ID="roomAdultExtraCbo" runat="server"></asp:DropDownList></div>
                </asp:PlaceHolder>
                </div>
                
            </div>
            <asp:PlaceHolder ID="plhnumpax" runat="server" Visible="true">
             <div class="row form-group form-ctrol-admin">
                <div class="col-sm-2 adminlabel">Numero personas en el precio base:</div>
                <div class="col-sm-10">
                    <asp:DropDownList ID="roomPaxInBasePriceCbo" runat="server"></asp:DropDownList>
                    <small>(Este valor se utiliza para dividir el precio base x persona en tarifas)</small>
                </div>
            </div>               
            </asp:PlaceHolder>
             <asp:PlaceHolder ID="plhchild" runat="server" Visible="true">
            <div class="row form-group form-ctrol-admin">
                <div class="col-sm-2 adminlabel">Niños pequeños ocupan plaza en computo?</div>
                <div class="col-sm-10">
                    <asp:DropDownList ID="roomChildIncludedCbo" runat="server" CssClass="form-control-sm"></asp:DropDownList>
                </div>
            </div>
                 </asp:PlaceHolder>
        </div>

        <h4 class="subsection-gray"> <img src="images/lang/lang_multi.png" style="display:inline-block;" /> Nombre en varios idiomas</h4>
        <div class="panel-body">
            <p class="">Esta es la descripción de la unidad en los diferentes idiomas.</p>
            <div class="row">
                <div class="col-sm-4">
                    <label><img src="images/lang/lang_en.gif" /> Nombre de la unidad en Ingles.</label>
                    <div>
                        <asp:TextBox ID="roomname_en" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="roomname_en" ErrorMessage="Debe introducir el nombre" SetFocusOnError="True"  Display="Dynamic" CssClass="has-error" Visible="True" ClientIDMode="Static"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-4">
                    <label><img src="images/lang/lang_de.gif" /> Nombre de la unidad en Aleman.</label>
                    <div>
                        <asp:TextBox ID="roomname_de" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="roomname_de" ErrorMessage="Debe introducir el nombre" SetFocusOnError="True"  Display="Dynamic" CssClass="has-error" Visible="True" ClientIDMode="Static"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-4">
                    <label><img src="images/lang/lang_fr.gif" /> Nombre de la unidad en Frances.</label>
                    <div>
                        <asp:TextBox ID="roomname_fr" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="roomname_fr" ErrorMessage="Debe introducir el nombre" SetFocusOnError="True"  Display="Dynamic" CssClass="has-error" Visible="True" ClientIDMode="Static"></asp:RequiredFieldValidator>
                    </div>
                </div>

            </div>
        </div>


        <%-- descripcion idiomas --%>
        <div class="panel-group collapse-style-2 ">
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingServices">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#servicescollpase" aria-expanded="false" aria-controls="descriptioncollapse" class="collapsed"> <img src="images/lang/lang_multi.png" style="display:inline-block;" /> Descripcion en varios idiomas
                        </a>
                    </h4>
                </div>
                <div id="servicescollpase" class="panel-collapse collapse  " role="tabpanel" aria-labelledby="headingServices" aria-expanded="false">
                    <div class="panel-body panel-admin">
                        <p class="">Esta es la descripción de la unidad en los diferentes idiomas.<span class="alert-danger">Cuidado al copiar texto desde una pagina web porque tambien se copia el codigo HTML oculto lo que puede provocar resultados inesperados.</span></p>
                        <div class="row">
                            <div class="col-sm-6">
                                <label><img src="images/lang/lang_es.gif" /> Descripcion en español.</label>
                                <div>
                                    <asp:TextBox ID="roomServ_es" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>
                                    
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <label><img src="images/lang/lang_en.gif" /> Descripcion en Ingles.</label>
                                <div>
                                    <asp:TextBox ID="roomServ_en" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <label><img src="images/lang/lang_de.gif" /> Descripcion en Aleman.</label>
                                <div>
                                    <asp:TextBox ID="roomServ_de" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>
                                    
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <label><img src="images/lang/lang_fr.gif" /> Descripcion en Frances.</label>
                                <div>
                                    <asp:TextBox ID="roomServ_fr" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>
                                    
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%-- descripcion idiomas end--%>
        
        <%-- fotos --%>
        <div class="panel-group collapse-style-2 ">
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingfotos">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#collapsefotos" aria-expanded="false" aria-controls="descriptioncollapse" class="collapsed"><i class="fa fa-picture-o"></i> Fotos de la unidad
                        </a>
                    </h4>
                </div>
         
                    <div id="collapsefotos" class="panel-collapse collapse  " role="tabpanel" aria-labelledby="headingfotos" aria-expanded="false">
                    <div class="panel-body panel-admin">
                                <div class="hidden">
                                    <script>
                                        function deleteimgroom(id) {
                                            $("#hddimgidoperation").val(id);
                                            document.getElementById("btndeleteimg").click();
                                           
                                        }
                                    </script>
                                    <asp:Button ID="btnimgupdateorder" runat="server" ClientIDMode="Static" />
                                    <asp:Button ID="btndeleteimg" runat="server" ClientIDMode="Static" />
                                </div>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <div>
                                    <asp:Panel ID="pnluploadimg" runat="server" CssClass="clearfix" Style="clear: both;">
                                        <fieldset style="margin-top:0px;">
                                            <legend><strong>Quieres subir una nueva imagen?</strong> </legend>
                                            Pulsa en el boton inferior y luego en "guardar imagen". Recuerda que la imagen debe ser mayor de 1080px por 720px y mantener esa relación de aspecto. De lo contrario las imagenes se mostrarán incorrectamente en el sistema.
                                        <div class=" ">
                                            <asp:FileUpload ID="fileuploadImagecontrol" runat="server" CssClass="btn btn-default" Width="400px" ClientIDMode="Static" ToolTip="Pulsa aqui para generar" style="display:inline-block;"/>
                                            <%--<button onclick="return imagefileupclick();" class="btn btn-default ">Cargar nueva imagen</button>--%>
                                             
                                            <asp:LinkButton ID="btnsaveimagen" runat="server" CssClass="btn btn-default" ClientIDMode="Static"><i class="fa fa-save "></i> Guardar imagen</asp:LinkButton>
                                        </div>
                                        </fieldset>
                                        

                                    </asp:Panel>
                                </div>
                                <div class="order-image-btn-div">
                                    <a href="javascript:void(0);" onclick="reorderphotos();" class="btn btn-default reorder_link" id="save_reorder"><i class="fa fa-sort"></i> Reordenar Posicion de fotos</a>
                                    <a href="javascript:void(0);" onclick="cancelarreorderphotos();" class="btn btn-danger hidden" id="cancelsave_reorder"><i class="fa fa-times"></i> Cancelar operacion</a>
                                <div id="reorder-helper" class="bg-info" style="display:none;">1. Arrastre las fotos para posicionarlas.<br>2. Pulse en 'guardar' cuando finalize.</div>
                                </div>
                               
                                <asp:HiddenField ID="hddimgidoperation" runat="server" ClientIDMode="Static" Value="0" />
                                <asp:Literal ID="litphotosgallery" runat="server" Visible="False"></asp:Literal>


                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnsaveimagen" />
                                <asp:AsyncPostBackTrigger ControlID="btndeleteimg" />
                               <asp:AsyncPostBackTrigger ControlID="btnimgupdateorder" />
                            </Triggers>
                        </asp:UpdatePanel>
                            
                           <script>
                               function imagefileupclick() {
                                   document.getElementById("fileuploadImagecontrol").click();
                                   $("#btnsaveimagen").removeClass("hidden");
                                                return false;
                                            }
                                        </script>
                    </div>
                </div>
                 
            </div>
        </div>
        <%-- fotos end--%>
        
        
        <%-- services --%>
        <div class="panel-group collapse-style-2 ">
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingservices">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#collapseservices" aria-expanded="false" aria-controls="descriptioncollapse" class="collapsed"><i class="fa fa-asterisk"></i>  Servicios disponibles en esta unidad
                        </a>
                    </h4>
                </div>
                <div id="collapseservices" class="panel-collapse collapse  " role="tabpanel" aria-labelledby="headingservices" aria-expanded="false">
                    <div class="panel-body panel-admin">
                        <p>Estos tipos de servicios son genericos del sistema y pueden ser mostrados al cliente de diferentes formas para mejorar la usabilidad en la venta.</p>
      
                        <ul class="optionsdiv" >
                            <% 'Carga de situaciones
                                Dim dvServices As DataView = Me.getDTServices(Me.roomidhdd.Value)
                                Dim vcol As Integer = 2
                                If Me.hotel.Tipo = CFRConst.unitTypeEnum.car Then
                                    vcol = 4
                                End If

                                For rowServic As Integer = 0 To dvServices.Count - 1
                                    'xregistros  indica si hay mas de un servicio para comenzar en otra fila
                                    Dim drvSit As DataRowView
                                    Dim iconvarClass As String = "<i class='{0}'></i>"
                                    For xCol = 1 To vcol  'comienzo de escritura de los servicios por cada apartado

                                        Dim ischecked As String = ""
                                        Dim isCheckedunderRequest As String = ""
                                        Dim iconvarValue As String = ""
                                        drvSit = dvServices(rowServic)
                                        If IsDBNull(drvSit("hasservice")) = False Then
                                            ischecked = " checked"
                                            'If drvSit("underRequest") = True Then
                                            '    isCheckedunderRequest = " checked"
                                            'End If
                                        End If

                                        If IsDBNull(drvSit("iconvar")) = True Then

                                        Else
                                            iconvarValue = String.Format(iconvarClass, drvSit("iconvar"))
                                        End If

                            %>

                            <li class="optionsection" >
                                <input type='checkbox' name='chkoptServices' value='<%=drvSit("cod_servicio")%>' id='cs_<%=drvSit("cod_servicio")%>' <%=ischecked%>>
                                <label for="cs_<%=drvSit("cod_servicio")%>"><%=iconvarValue%> <%=drvSit("servicio")%></label>
                            </li>
                           <%-- <% If Me.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then%>
                            <div style="float: left; width: 100px;">
                                <div><label><input type='checkbox' name='chkBajopet' value='<%=drvSit("cod_servicio")%>' <%=isCheckedunderRequest%>> Bajo petición</label></div>
                            </div>
                            <%End If%>--%>
                            <% 
                                If rowServic = dvServices.Count - 1 Then 'si llega al final rellenamos
                                    'For xColFill = xCol To vcol
                                    '    Response.Write("<td >-</td>")
                                    'Next
                                    'Response.Write("</tr> ")
                                    Exit For
                                End If
                                
                                If xCol <> vcol Then
                                    rowServic += 1
                                End If
                            Next
                            'Response.Write("</tr> ")
                        Next
                        dvServices.Dispose()
                            %>
                        </ul>
                            
                      
                    </div>
                </div>
            </div>
        </div>
        <%-- services end--%>
        
        <div class="center-block text-center" >
            <asp:LinkButton ID="btnsave" runat="server" CssClass="btn btn-default btn-lg">Guardar <i class="fa fa-save"></i></asp:LinkButton>
        </div>

        <div id="footerinfo" class="footerinfo">
        <h3>Información</h3>
        <ul>
            <li>
                El * indica campos obligatorios
            </li>
            <li>Los servicios disponibles son para filtros y mostrar la información de una manera más visual al cliente.</li>
        </ul>
            </div>

    </div><!--end container -->


</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">

    <script lang="javascript">
        
    $(document).ready(function() {
        console.log("tooltip-enabled")
        $('[data-toggle="tooltip"]').tooltip();
    
        $('#roomName').change(function () {

            if ($('#roomName_RequiredFieldValidator1').is(':visible')) {
                $('#roomName_RequiredFieldValidator1').addClass('has-error-img');
                $('#roomName').addClass('has-error');
        } else {
                $('#roomName').removeClass('has-error-img');
        }

        });
    });
        
            function reorderphotos(){
                // $('.reorder_link').on('click',function(){
                $(".btn-delete-image").hide();
                $("#cancelsave_reorder").show();
                $("ul.reorder-photos-list").sortable({ tolerance: 'pointer' });
                $('.reorder_link').html('Guardar nuevo orden');
                $('.reorder_link').attr("id", "save_reorder");
                $('#reorder-helper').slideDown('slow');
                //$('.image_link').attr("href","javascript:void(0);");
                $('img.img-order').css("cursor", "move");
                $("a.img-order").hide();

                $("#save_reorder").click(function( e ){
                    if( !$("#save_reorder i").length )
                    {
                        $("#save_reorder").hide();
                        $("#reorder-helper").html('').prepend('<img src="images/ajax-loader.gif"/> Guardando, por favor espere.');
                        $("ul.reorder-photos-list").sortable('destroy');
               
                        var h = [];
                        $("ul.reorder-photos-list li").each(function () { h.push($(this).attr('id').substr(9)); });
                        $("#hddimgidoperation").val(h);
                        document.getElementById("btnimgupdateorder").click();
                        //$(".btn-delete-image").show();
                        return false;
                    }   
                    e.preventDefault();     
                });
            }
            function cancelarreorderphotos() {

            }

</script>

</asp:Content>

