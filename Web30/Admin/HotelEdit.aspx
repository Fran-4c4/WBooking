<%@ Page Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="HotelEdit.aspx.vb" Inherits="HotelEdit" title="Hotel-Edicion" ResponseEncoding="iso-8859-1" ValidateRequest="false" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Diagnostics" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadScript" id="HeadScriptpage" >
    
     <script language="JavaScript">

function checkAddress() {
    var df = document.frm;
    var URL = "http://maps.google.es/maps?q=" + $("#tdireccion").val();
    var sit;
    sit = window.open(URL, "Situacion", "width=800,height=500,top=70,left=70,alwaysraised=yes");
    sit.focus();

}
     </script>


 </asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="container">
        <%If Me.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then %>
           <h1 class="sectiontitle">Datos del hotel</h1>    
        <%else %>
          <h1 class="sectiontitle">Datos de la empresa</h1>  
            <%End If%>
        
        <div class="pull-right">*indica campos Obligatorios</div>
        <table class="admintable table">
            <tbody>
            <tr>
                <td width="150"><b>Nombre  *</b></td>
                <td width="300">
                    <asp:TextBox ClientIDMode="Static" runat="server" ID="tnombre" Columns="60" MaxLength="50" placeholder="Nombre de la propiedad/empresa"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_postacode0" runat="server" ControlToValidate="tnombre" CssClass="has-error" Display="Dynamic" ErrorMessage="Falta el nombre" ValidationGroup="hotelValidation"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><b>Dirección *</b></td>
                <td>
                    <asp:TextBox ClientIDMode="Static" runat="server" ID="tdireccion" Columns="60" MaxLength="500" Text=""></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_address" runat="server" ControlToValidate="tdireccion" CssClass="has-error" Display="Dynamic" ErrorMessage="Falta la direccion" ValidationGroup="hotelValidation"></asp:RequiredFieldValidator>
                    <input type="BUTTON" name="botCambiar" value="Comprobar direccion en google" class="btn btn-sm btn-link" onclick="checkAddress()">
                    
                    <p>
                        
                        Introduzca su informacion de la siguiente forma, asi será posible encontrarlo en google maps de forma automatica:</p>
                    <p>
                        Ejemplos:
                        <br>
                        <a target="_blank" href="http://maps.google.es/maps?q=perez%2C+22%2CCorralejo%2C+espa%C3%B1a&sourceid=navclient-ff&ie=UTF-8&rlz=1B3GGGL_esES245ES246">Pérez Galdós,Corralejo, españa</a>
                        <br>
                        <a target="_blank" href="https://www.google.es/maps/place/Paseo+Castellana,+22,+28015+Madrid/@40.4361844,-3.7039388,17z/data=!3m1!4b1!4m2!3m1!1s0xd42285f733d5a71:0xa33d4c0518ccbfd1">Paseo de la Castellana, 22,Madrid,españa </a>
                    </p>
                </td>
            </tr>
            <tr>
                <td><b>Código Postal *</b></td>
                <td>
                    <asp:TextBox ClientIDMode="Static" runat="server" ID="tcodpostal" Columns="10" MaxLength="12"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_postacode" runat="server" ControlToValidate="tcodpostal" CssClass="has-error" Display="Dynamic" ErrorMessage="Falta el codigo postal" ValidationGroup="hotelValidation"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <asp:PlaceHolder ID="plhCboCategoria" runat="server">
                
              <tr>
                <td><b>Categoria *</b></td>
                <td>
                    <asp:DropDownList ID="cbocategoria" runat="server"></asp:DropDownList>
                </td>
            </tr>          

            </asp:PlaceHolder>

            <tr>
                <td><b>Nº de <%=Me.unitnameplural%> *</b></td>
                <td>
                    <asp:TextBox ClientIDMode="Static" runat="server" ID="tnumhabitaciones" Columns="9" MaxLength="4" Text="0" onkeypress="return solonums();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_rooms" runat="server" ControlToValidate="tnumhabitaciones" CssClass="has-error" Display="Dynamic" ErrorMessage="Falta el numero de habitaciones" ValidationGroup="hotelValidation"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="RequiredNumberValidator_rooms" runat="server" ControlToValidate="tnumhabitaciones" Type="Integer" Operator="DataTypeCheck" CssClass="has-error" ErrorMessage="Debe introducir un valor numerico entero."  />

                </td>
            </tr>
            <tr>
                <td><b>Telefono *</b></td>
                <td>
                    <asp:TextBox ClientIDMode="Static" runat="server" ID="ttelefono" Columns="14" MaxLength="14" Text=""></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_phone" runat="server" ControlToValidate="ttelefono" CssClass="has-error" Display="Dynamic" ErrorMessage="Falta el numero de telefono" ValidationGroup="hotelValidation">Falta el numero de telefono</asp:RequiredFieldValidator>
                  
                </td>
            </tr>
          
            <tr>
                <td><b>E-Mail *</b></td>
                <td>
                    <asp:TextBox ClientIDMode="Static" runat="server" ID="temail" Columns="60" Text=""></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="temail" ErrorMessage="Introduzca un email valido."
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="hotelValidation" CssClass="has-error" Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_email" runat="server" ControlToValidate="temail" CssClass="has-error" Display="Dynamic" ErrorMessage="Falta el correo electronico" ValidationGroup="hotelValidation"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><b>Pagina Web</b> </td>
                <td>
                    <asp:TextBox ClientIDMode="Static" runat="server" ID="tweb" Columns="60" Text=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><b>Director</b> </td>
                <td>
                    <asp:TextBox ClientIDMode="Static" runat="server" ID="tdirector" Columns="60" Text=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><b>Horario checkin</b> </td>
                <td>
                     <div class="form-inline">Desde <asp:DropDownList ID="cbocheckin1" runat="server" CssClass="form-control form-control-static" Width="100"></asp:DropDownList> Hasta  <asp:DropDownList ID="cbocheckin2" runat="server" CssClass="form-control form-control-static" Width="100"></asp:DropDownList></div>
                        Deje en blanco el campo "Hasta" si el cliente puede hacer checkin hasta cualquier hora.
                </td>
            </tr>
             <tr>
                <td><b>Horario checkout</b> </td>
                <td>
                    <div class="form-inline">Desde <asp:DropDownList ID="cbocheckout1" runat="server" CssClass="form-control"  Width="100"></asp:DropDownList> Hasta  <asp:DropDownList ID="cbocheckout2" runat="server" CssClass="form-control" Width="100"></asp:DropDownList></div>
                    Deje en blanco el campo "Desde" si el cliente puede hacer checkout en cualquier momento antes de la hora "Hasta"
                </td>
            </tr>                           
            </tbody>
        </table>

        <div class="panel-group collapse-style-2" id="accordion-2">


            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingSituation">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#situationcollapse" aria-expanded="false" aria-controls="situationcollapse" class="collapsed">Situación de la propiedad
                        </a>
                    </h4>
                </div>
                <div id="situationcollapse" aria-labelledby="headingSituation"  class="panel-collapse collapse" role="tabpanel" aria-expanded="false" >
                    <div class="panel-body">
                        <table class="table table-striped table-bordered">
                            <% 'Carga de situaciones
                                Dim dvSituaciones As DataView = Me.getDTSituaciones
                                For rowSit As Integer = 0 To dvSituaciones.Count - 1
                                    Response.Write("<tr> ")
                                    For xCol As Integer = 1 To 4
                                        If rowSit = dvSituaciones.Count - 1 Then
                                            For xColFill = xCol To 4
                                                Response.Write("<td>&nbsp;</td>")
                                            Next
                                            Response.Write("</tr> ")
                                            Exit For
                                        Else
                                            Dim drvSit As DataRowView = dvSituaciones(rowSit)
                                            Dim ischecked As String = ""
                                            If IsDBNull(drvSit("hassituation")) = False Then
                                                ischecked = " checked"
                                            End If
                            %>
                            <td>
                                <input type='checkbox' name='chkopt' value='<%=drvSit("cod_situacion")%>' <%= ischecked%>><%=drvSit("situacion")%></td>
                            <% 
                                If xCol <> 4 Then
                                    rowSit += 1
                                End If
                            End If
                        Next
                        Response.Write("</tr> ")
                    Next
                    dvSituaciones.Dispose()

                            %>
                        </table>
                    </div>

                </div>
            </div>
        </div>
        <%If Me.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then%>
        <div class="panel-group collapse-style-2" id="Div1">
            <div class="panel panel-default">
                <div class="panel-heading" id="accordion-services">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#servicecollapse" aria-expanded="false" aria-controls="servicecollapse" class="collapsed">Servicios que se ofrecen
                        </a>
                    </h4>
                </div>
                <div id="servicecollapse" class="panel-collapse collapse" role="tabpanel" aria-expanded="false">
                    <div class="panel-body">

                        <table class="table table-striped table-bordered">
                            <% 'Carga de situaciones
                                Dim dvServices As DataView = Me.getDTServices

                                Dim tipo As Integer = 0
                                For rowServic As Integer = 0 To dvServices.Count - 1
                                    'xregistros  indica si hay mas de un servicio para comenzar en otra fila
                                    Dim drvSit As DataRowView = dvServices(rowServic)
                                    Dim newTypeCarac As Integer = CInt(drvSit("tipo_carac"))
                                    
                                    If newTypeCarac = 1 And tipo <> newTypeCarac Then
                                        Response.Write("<tr> <td colspan=4 ><h3 class=c2>Servicios Generales</h3></td></tr><tr>")
                                        tipo = 1
                                    End If
                                    If newTypeCarac = 3 And tipo <> newTypeCarac Then
                                        Response.Write("<tr> <td colspan=4 ><h3 class=c2>Servicios Externos/Concertados </h3></td></tr>")
                                        tipo = 3
                                    End If
                                    If newTypeCarac = 4 And tipo <> newTypeCarac Then
                                        Response.Write("<tr> <td colspan=4 ><h3 class=c2>Servicios de Recreo/Deportes/Shows </h3></td></tr>")
                                        tipo = 4
                                    End If
                                    If newTypeCarac = 5 And tipo <> newTypeCarac Then
                                        Response.Write("<tr> <td colspan=4 ><h3 class=c2>Servicios de Negocios </h3></td></tr>")
                                        tipo = 5
                                    End If
      
                                    Response.Write("<tr> ")
  
                                    Dim vcol As Integer
      
                                    If newTypeCarac = 2 Or newTypeCarac = 3 Or newTypeCarac = 4 Then vcol = 2 Else vcol = 4
                                    For xCol = 1 To vcol  'comienzo de escritura de los servicios por cada apartado
                                        Dim ischecked As String = ""
                                        Dim isCheckednotAllRooms As String = ""
                                        Dim isCheckedunderRequest As String = ""
                                        drvSit = dvServices(rowServic)
                                        If rowServic = dvServices.Count - 1 Then 'si llega al final rellenamos
                                            For xColFill = xCol To vcol
                                                Response.Write("<td >&nbsp;</td>")
                                            Next
                                            Response.Write("</tr> ")
                                            Exit For
                                        ElseIf newTypeCarac <> tipo Then
                                            If newTypeCarac = 2 Or newTypeCarac = 3 Or newTypeCarac = 4 Then Response.Write("<td class='Estandard'>&nbsp;</td>")
                                            Response.Write("<td >&nbsp;</td>")
                                        Else
                                            If IsDBNull(drvSit("hasservice")) = False Then
                                                ischecked = " checked"
                                                'Debug.WriteLine(drvSit("cod_servicio") & "-" & drvSit("notAllRooms") & "-" & drvSit("underRequest"))
                                                If drvSit("notAllRooms") = True Then
                                                    isCheckednotAllRooms = " checked"
                                                End If
                                                If drvSit("underRequest") = True Then
                                                    isCheckedunderRequest = " checked"
                                                End If
                                            End If

                                        End If
                            %>

                            <td>
                                <div class="optionsection">
                                <input type='checkbox' name='chkoptServices' value='<%=drvSit("cod_servicio")%>' id='cs_<%=drvSit("cod_servicio")%>' <%=ischecked%>>
                                <label for="cs_<%=drvSit("cod_servicio")%>"><%=drvSit("servicio")%></label>

                                </div>

                                <% If newTypeCarac = 2 Or newTypeCarac = 3 Or newTypeCarac = 4 Then%>
                            </td>
                            <td width="25%">&nbsp;B.P.<input type='checkbox' name='chkBajopet' value='<%=drvSit("cod_servicio")%>' <%=isCheckedunderRequest%>>
                                <% End If%>

                                <%If newTypeCarac = 2 Then%>
                                &nbsp;N.T.<input type='checkbox' name='chknohab' value='<%=drvSit("cod_servicio")%>' <%=isCheckednotAllRooms%>>
                                <% End If%>
                            </td>
                            <% 
                                If xCol <> vcol Then
                                    rowServic += 1
                                End If
                            Next
                            Response.Write("</tr> ")
                        Next
                        dvServices.Dispose()
                            %>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <%End If%>
        

        <div class="panel-group collapse-style-2" id="Div2">

            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingDescriptions">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#descriptioncollapse" aria-expanded="false" aria-controls="descriptioncollapse" class="collapsed">Descripcion en varios idiomas
                        </a>
                    </h4>
                </div>
                <div id="descriptioncollapse" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingDescriptions" aria-expanded="false">
                    <div class="panel-body">
                        <div class="col-xs-12 col-sm-6">
                            <h4>Descripcion en español</h4>
                            <asp:TextBox ID="editdesc_es" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>               
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h4>Descripcion en Ingles</h4>
                            <asp:TextBox ID="editdesc_en" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>    
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h4>Descripcion en Aleman</h4>
                             <asp:TextBox ID="editdesc_de" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>  
                        </div>
                        <div class="col-xs-12 col-sm-6">
                             <h4>Descripcion en Frances</h4>
                             <asp:TextBox ID="editdesc_fr" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>  
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%If Me.hotel.Tipo = CFRConst.unitTypeEnum.hotel Then%>
        <div class="panel-group collapse-style-2" id="Div3">


            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingsaloon">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#salooncollapse" aria-expanded="false" aria-controls="salooncollapse" class="collapsed">Salones
                        </a>
                    </h4>
                </div>
                <div id="salooncollapse" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingsaloon" aria-expanded="false">
                    <div class="panel-body">
                        <table class="table table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th width="160"><b>Nombre</b></th>
                                    <th width="30">m2</th>
                                    <th>Teatro</th>
                                    <th>Escuela</th>
                                    <th>Cocktail</th>
                                    <th>Banquete</th>
                                    <th>Mesa imperial</th>
                                    <th>Mesa u</th>
                                </tr>
                            </thead>

                            <% 'Carga de salones
                                Dim dvSaloon As DataView = Me.getDTSaloons
   
                                For rowSal As Integer = 0 To 9
                                
                                    Dim drvSal As DataRowView
                                    Dim salon(8) As String
                                    If rowSal <= dvSaloon.Count - 1 Then
                                        drvSal = dvSaloon(rowSal)
                                        salon(0) = drvSal("nombre")
                                        salon(1) = drvSal("m2")
                                        salon(2) = drvSal("teatro")
                                        salon(3) = drvSal("escuela")
                                        salon(4) = drvSal("cocktail")
                                        salon(5) = drvSal("banquete")
                                        salon(6) = drvSal("mi")
                                        salon(7) = drvSal("mu")
                                    
                                    End If
                               
                            %>
                            <tr>
                                <td>
                                    <input type="text" name="tsalon<%=rowSal%>" size="30" maxlength="30" value="<%=salon(0)%>">
                                </td>
                                <td>
                                    <input type="text" name="tsalmetro<%=rowSal%>" size="3" maxlength="4" value="<%=salon(1)%>" onkeypress="return solonums();">
                                </td>
                                <td>
                                    <input type="text" name="tsalteatro<%=rowSal%>" size="3" maxlength="4" value="<%=salon(2)%>" onkeypress="return solonums();">p
                                </td>
                                <td>
                                    <input type="text" name="tsalescuela<%=rowSal%>" size="3" maxlength="4" value="<%=salon(3)%>" onkeypress="return solonums();">p
                                </td>
                                <td>
                                    <input type="text" name="tsalcock<%=rowSal%>" size="3" maxlength="4" value="<%=salon(4)%>" onkeypress="return solonums();">p
                                </td>
                                <td>
                                    <input type="text" name="tsalbanq<%=rowSal%>" size="3" maxlength="4" value="<%=salon(5)%>">p
                                </td>
                                <td>
                                    <input type="text" name="tsalmesai<%=rowSal%>" size="3" maxlength="4" value="<%=salon(6)%>" onkeypress="return solonums();">p
                                </td>
                                <td>
                                    <input type="text" name="tsalmesu<%=rowSal%>" size="3" maxlength="4" value="<%=salon(7)%>" onkeypress="return solonums();">p
                                </td>
                            </tr>
                            <% Next
                                dvSaloon.Dispose()
                            %>
                        </table>


                    </div>

                </div>
            </div>
        </div>

        <%End If%>
        <div class="center-block text-center" >
            <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary btn-lg " ValidationGroup="hotelValidation" >Guardar cambios <i class="fa fa-save"></i> </asp:LinkButton>
            
        </div>



        <div class="footerinfo">
         <h3>Información </h3>
        <ul>
            <li>El * indica campos Obligatorios.
            </li>
        </ul>           
        </div>


    </div>
    
    

    <div class="modal " id="modal-editor" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h3>Cambiar descripción del hotel</h3>
                </div>
                <div class="modal-body">


                        <div id="editor1">
                            <textarea class="form-control" rows="3" placeholder="Textarea" id="editor"></textarea>
                        </div>
                   
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm " data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-default btn-sm" onclick="applychangesfromeditor()">Aplicar cambio<i class="fa fa-floppy-o pl-10"></i></button>
                    <input type="hidden" id="textboxcaller">

                </div>
            </div>
        </div>
    </div>

    </asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" id="Content1" >
    
</asp:Content>
    

