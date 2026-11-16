<%@ Page Title="CFR-Tarifa-Edición" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="tariffEdit.aspx.vb" Inherits="Admin_tariffEdit" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="ind.bk.reports" %>
<%@ Register Src="adminUC/deletemodal.ascx" TagPrefix="uc1" TagName="deletemodal" %>
<%@ Register Src="~/Admin/adminUC/assignrelationsmodal.ascx" TagPrefix="uc2" TagName="assignrelationsmodal" %>


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

        <div class="breadcrumb-container">
        <div class="container">
            <ol class="breadcrumb">
                <li><i class="fa fa-home pr-10"></i><a href="<%= linkcnf("default.aspx")%>">Inicio</a></li>
                <li><a href='<%= linkcnf("tarifflist.aspx")%>'>Tarifas</a> </li>
                <li class="active">Editar Tarifa</li>
            </ol>
        </div>
    </div>
        <div class="container">
        <div class="row">
             <div class="col-sm-8">
               <h1 class="sectiontitle"><i class="fa fa-eur"></i> Editar Tarifa</h1>
               <i class="fa fa-asterisk"></i><asp:DropDownList ID="cbotariff2select" runat="server" AutoPostBack="True" EnableViewState="True"></asp:DropDownList>    
             </div>
            <div class="col-sm-4">
                <div class="text-right">
                    <a href="<%= linkcnf("Tariffedit.aspx?objid=" & UIHelper.URLencryptData("0"))%>" class="btn btn-animated btn-primary btn-lg">Crear nueva Tarifa <i class="glyphicon glyphicon-plus-sign"></i></a>
                </div>
            </div>
         </div> 
      
        <div class="separator-2"></div>
    
      <div  class="title-block-2"> 
         <h3 class="">
            <asp:HiddenField ID="roomid" runat="server" value="0"/>
           <asp:Label ID="objidLbl" runat="server" Text="" CssClass ="unit-code label label-default"></asp:Label>  <asp:Label ID="objNameLbl" runat="server" text="Nueva tarifa"></asp:Label>
            
        </h3>  
           <div class="edit-buttons">
                              
                    
                        <asp:LinkButton ID="btnsave" runat="server" CssClass="btn btn-default btn-sm">Guardar tarifa <i class="fa fa-save"></i></asp:LinkButton>
                    
                    <asp:PlaceHolder ID="plhbtndeletetariff" runat="server" Visible="false" >
                    
                        <a href="#" Class="btn btn-danger btn-sm"  data-toggle="modal" data-target="#modaldeleteItem" data-objname="<%=Me.objname.Text %>" data-objid="<%=Me.objid  %>"><i class="fa fa-trash"></i></a>
                    
                    </asp:PlaceHolder>

               </div>
          </div>
            <p><small class="modifydate"><asp:Literal ID="litmodifydate" runat="server"></asp:Literal></small></p>
     </div>

        <div class="container">
            <div class="form-inline">
            <div class="form-group" style="width: 400px;"> 
                <label for="objName" class="control-label edit-name">Nombre de la tarifa</label>
                <div class="">
                    <asp:TextBox ID="objname" runat="server" Columns="50" CssClass="form-control" placeholder="Nombre de la tarifa" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="roomName_RequiredFieldValidator1" runat="server" ErrorMessage="Debe introducir el nombre de la tarifa" SetFocusOnError="True" ControlToValidate="objName" Display="Dynamic" CssClass="" Visible="True" ClientIDMode="Static"></asp:RequiredFieldValidator>
                </div>
            </div>
                <asp:PlaceHolder ID="placeholderTariff" runat="server" Visible="false">
                    <div class="form-group"  style="width: 400px;">
                    <label for="cbotariff"  class="control-label">Basada en la tarifa</label>
                      <div class=" ">
                          <asp:DropDownList ID="cbotariff" CssClass="form-control" runat="server" ClientIDMode="Static"></asp:DropDownList>
                          <p class="help-block">
                              Crear la tarifa basada en (seleccione una de la lista). Se creará una nueva tarifa basada en la que seleccione y con los mismos precios y temporadas. El resto de parametros debe seleccionarlos a continuación.
                          </p>
                      </div>
                </div>                     

                </asp:PlaceHolder>

            <div class="form-group"  style="width: 400px;">
                <label for="cbocategory" class="control-label">Categoria</label>

                <div class="" style="width: 100%;">
                    <asp:DropDownList ID="cbocategory" CssClass="form-control" runat="server" ClientIDMode="Static"></asp:DropDownList>
                    <p class="help-block">Las categorias son aplicables sobre todo a nivel de agencias. La categoria "empresas" le indicará al sistema para agencias que no se le puede aplicar esa tarifa a un cliente particular.</p>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <div class="checkbox" style="width: 200px;">
                        <label data-toggle="tooltip" title="Si marca esta opción el sistema mostrará una opción extra al cliente indicandole que esta tarifa no se puede cancelar.">
                            <asp:CheckBox ID="chknorefundtariff" runat="server" /> Tarifa no reembolsable 
                        </label>
                    </div>   
                  </div>    
                   <div class="form-group form-ctrol-admin" style="width: 250px;">
                        <label for="tnorefundprice" class="control-label" data-toggle="tooltip" title="Si no introduce un descuento esta tarifa solo será <<No reembolsable>>.">Descuento a aplicar al precio base</label>
                       <div class="input-group">
                            <asp:TextBox runat="server" CssClass="form-control onlynum" ID="tnorefundprice" MaxLength='10' value="0" ClientIDMode="static"></asp:TextBox>
                            <span class="input-group-addon">%</span>
                        </div>
                   </div>                   
                   
               
            </div>
                
            <div class="form-group"  style="width: 600px;">
                <label for="cbocancelconditions" class="control-label"><i class="fa fa-times-circle-o"></i>  Condiciones de cancelación</label>
                <div class="" style="width: 500px;">
                    <asp:DropDownList ID="cbocancelconditions" CssClass="form-control" runat="server" ClientIDMode="Static" Width="500px"></asp:DropDownList>
                </div>
                 <p class="help-block">No es obligatorio asociar una condición de cancelación. El sistema siempre presentará las condiciones genericas ademas de la aqui seleccionada.</p>
            </div>
            

                <asp:PlaceHolder ID="plhhotel_board" runat="server">
                     <div class="form-row">
            <div class="form-group">
                <label class="control-label"><i class="fa fa-cutlery"></i> Tipos de regimen aplicable a esta tarifa</label>
                <div class="form-options">
                             <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="chkboardonlyroom" runat="server" /> Solo alojamiento
                                </label>
                            </div>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="chkboardbreakfast" runat="server" /> Desayuno incluido
                                </label>
                            </div>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="chkboardmp" runat="server" /> Media Pension
                                </label>
                            </div>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="chkboardpc" runat="server" /> Pension Completa
                                </label>
                            </div>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="chkboardti" runat="server" /> Todo incluido
                                </label>
                            </div>
                </div>
            </div>
                         </div>
        </asp:PlaceHolder>

                 <asp:PlaceHolder ID="plhhotel_childdiscount" runat="server">
                     <div class="form-row">
                     <div class="form-group">
                         <div class="">
                             <div class="checkbox">
                                 <label data-toggle="tooltip" title="Si la habitación admite 3 adultos y solo van 1 adulto y un niño asignar los descuentos de niños al precio x pax.">
                                     <asp:CheckBox ID="chkDiscountCapacityApplyall" runat="server" />Aplicar los Descuentos de niño si ocupa puesto de adulto?
                                 </label>
                             </div>
                         </div>
                     </div>
                         </div>
                 </asp:PlaceHolder>

                <asp:Panel ID="pnlHasTPV" runat="server" Visible="false" >
                    <div class="form-row">
                  <div class="form-group ">
                    <label for="cbotpvminimmum" class="control-label">Valor a cobrar por TPV</label>
                      <div class="">
                        <asp:DropDownList ID="cbotpvminimmum" runat="server" CssClass="form-control" ClientIDMode="static"></asp:DropDownList>
                    </div>
                </div>
                        </div>
                </asp:Panel>
        </div>
    </div>          
    <asp:PlaceHolder ID="plhunitprices" runat="server" Visible="false">
    <div class="container">
        <% 
            'CONFIG VALUES NEEDED FOR PRESENTATION
            Dim objid As String = Me.tariffObj.cod_tarifa
            Dim objidCrypt As String = UIHelper.URLencryptData(objid)

            Dim dvClients As DataView = Me.getDvClientsAsociated(objid)
            Dim hasCustomersMessage As String = ""
            'If dvClients.Count = 0 Then
            '    hasCustomersMessage = UIMsg.UIMessageDismissInline("Esta tarifa no tiene asociados clientes.", CFRConst.msgEnum.info, "msgcustomerelation_" & objid, UIMsg.msgbodyEnum.none)
            'End If
        %>

        <div id="rowobj_<%=objid%>" data-objid="<%=objid%>" class="tariffcontainer">
            <div class="rowtariff row ">
                <div class="colview-2 col-sm-12">
                    <div class="">
                        <a href="<%= linkcnf("tariffcalendar.aspx")%>"><i class="fa fa-calendar-o"></i>Pulse aqui para modificar los precios de las unidades x dia. (Tarifa Calendario)</a>
                        <div class="panel panel-info">
                            <div class="table-responsive">
                                <table id="tarifftable_<%=objid%>" class="table table-bordered table-striped table-condensed tarifftable">

                                    <thead>
                                        <tr class="rpricesheader">
                                            <th></th>
                                            <th colspan="2" class="rprices-strip-1">Suplementos (% sobre el precio x dia)
                                            </th>
                                            <%--<th></th>--%>
                                            <asp:PlaceHolder ID="plhhotelboardth" runat="server" Visible="true">
                                                <th colspan="4"><i class="fa fa-cutlery"></i>Precio Regimen Adultos<br />
                                                    <small>Los regimenes no visibles no estan activados.</small></th>
                                                <%-- <th></th>
                                                        <th></th>
                                                        <th></th>  --%>
                                                <th colspan="4" class="rprices-strip-1"><i class="fa fa-cutlery"></i>Precio Regimen Niños</th>
                                                <th colspan="2">Descuento adultos extra</th>
                                                <%--<th></th>--%>
                                                <%--Discount child extra--%>
                                                <th colspan="2" class="rprices-strip-1">Descuento Niños extra</th>
                                                <%--<th></th>--%>
                                            </asp:PlaceHolder>


                                        </tr>
                                        <tr class="rpricesheader">
                                            <th class="roomdesc">Unidad</th>
                                            <td class="rprices-strip-1 ">
                                                <div class="mindaytooltip"><i class="fa fa-info-circle "></i>Dias minimos</div>
                                            </td>
                                            <td class="rprices-strip-1">
                                                <div class="mindayweekendtooltip"><i class="fa fa-info-circle "></i>Fin de semana</div>
                                            </td>
                                            <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="true">
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
                                            Dim dvRoomPrices As DataView = Me.getDvPricesByRoomByTariff(objid)

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
                                            <%= showCellOn(Me.tariffObj.AD, precioAD)%>
                                            <%= showCellOn(Me.tariffObj.MP, preciomp)%>
                                            <%= showCellOn(Me.tariffObj.PC, preciopc)%>
                                            <%= showCellOn(Me.tariffObj.TI, precioTI)%>

                                            <%--Child board--%>
                                            <%= showCellOn(Me.tariffObj.AD, precioADchild)%>
                                            <%= showCellOn(Me.tariffObj.MP, preciompchild)%>
                                            <%= showCellOn(Me.tariffObj.PC, preciopcchild)%>
                                            <%= showCellOn(Me.tariffObj.TI, precioTIchild)%>

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

                      <%--  <div class="panel-section-header">
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
                                <asp:LinkButton ID="btnshowcustomerrelations" OnclientClick="getrelations();" runat="server" CssClass="btn btn-animated btn-primary btn-sm showrelations" CommandArgument="assigncustomer">Modificar operadores asignados <i class="fa fa-pencil"></i></asp:LinkButton>
                               <a href="#" onclick="getrelations();">test</a>
                            </div>
                        </div>--%>
                    </div>
                </div>
                <%--kk--%>
            </div>
        </div>
    </div>
    </asp:PlaceHolder>

    
 <uc1:deletemodal runat="server" ID="ucxdeletemodal" />
<uc2:assignrelationsmodal runat="server" ID="assignrelationsmodal" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
    <script>
        $(document).ready(function () {
                console.log("tooltip-enabled")
                $('[data-toggle="tooltip"]').tooltip();
                $(".discount").popover({ trigger: "focus", placement: "top", title: "Descuentos", content: "Si introduce un valor con el signo porcentaje el sistema hará los calculos basados en el precio base x persona." });
                $(".mindaytooltip").popover({ trigger: "hover", placement: "auto right", title: "Suplemento", content: "Cuando no se cumpla el minimo de dias se aplicará este suplemento al precio x dia." });
                $(".mindayweekendtooltip").popover({ trigger: "hover", placement: "auto right", title: "Suplemento", content: "Cuando no se cumpla el minimo de dias este valor se añadirá como suplemento al precio solo en fin de semana(Sabado/Domingo)" });
    
            //    var val = parseInt($("#cbotpvminimmum").val());
            //    if (val==0) {
            //        $("#txttpvpercentagediv").show();
            //    }


            //$("#cbotpvminimmum").change(function () {
            //    console.log($("#cbotpvminimmum").val());
            //    var val = parseInt($("#cbotpvminimmum").val());
            //    if (val == 0) {
            //        $("#txttpvpercentagediv").show("slow");
            //    } else {
            //        $("#txttpvpercentagediv").hide("slow");
            //    }
            //});

            $("#cbominimumdays").change(function() {
                var val = parseInt($("#cbominimumdays").val());
            if (val >0) {
                $("#minimumdayssection").show("slow");
                } else {
                $("#minimumdayssection").hide("slow");
                }
            });

            $('#aspnetForm').validate({
                rules: {
                    objname: {
                        minlength: 3,
                        required: true
                    }

                },
                highlight: function (element) {
                    $(element).closest('.form-group').removeClass('success').addClass('error');
                },
                success: function (element) {
                    element.addClass('valid')
                        .closest('.form-group').removeClass('error').addClass('success');
                }
            });

        });
    </script>
    <script>
    $(document).ready(function () {
        $('#modalasignrelations').on('show.bs.modal', function (event) {
            $('#modalasignrelations').appendTo("form");
        });

        $(".showrelations").click(function () {
            var button = $(event.currentTarget); // Button that triggered the modal
            var par = button.parent();
            $("#field_itemobject").val(par.data('objid'));
        });
        function setnewrelations(objid, direldiv) {
            var direl = $('#' + direldiv + "_" + objid);
            $(direl).find('li').remove();
            var iconclass = "fa fa-user";

            $(".chk-rel").each(function (index) {
                var relationid = $(this).attr('id');
                var isChecked = $(this).is(':checked');

                if (isChecked) {
                    var label = $("label[for='" + relationid + "']").text();
                    direl.append("<li><i class='" + iconclass + "'></i> " + label + "</li>");
                }
            });
        }

    });


 function getrelations() {
     PageMethods.getCustomersRelations(OnSucceeded, OnFailed);
 }
 
 function OnSucceeded(result, userContext, methodName) {
     $('#modalasignrelations').appendTo("form");
     $('#modalasignrelations').modal("show");
     $('#kkdelavaca').html(result);
 }
 
 function OnFailed(error, userContext, methodName) {
     $('#litrelations').html( "An error occured.");
 }


</script>
</asp:Content>

