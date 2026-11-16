<%@ Page Title="CFR-Configuración sistema" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="ObjectBookParameters.aspx.vb" Inherits="Admin_ObjectBookParameters" %>

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
        <h1 class="sectiontitle"><i class="fa fa-wrench"></i> Parametros basicos del sistema de reservas</h1>
        <p></p>
        <div class="separator-2"></div>
    </div>
    
    <div class="container">
        
        <asp:Panel ID="pnlchilds" runat="server">
            <asp:PlaceHolder ID="plhhotel_agechild" runat="server">
                <div class="panel panel-default">
                <div class="panel-heading"><i class="fa fa-child fa-lg"></i> Edades de los niños para las tarifas</div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group form-ctrol-admin">
                            <label for="txtemail" class="col-sm-2 control-label">Niños gratis</label>
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <asp:DropDownList ID="cbochildfree" runat="server" CssClass="form-control emailoperator" ></asp:DropDownList>
                                    <span class="input-group-addon"><i class="fa fa-child"></i></span>
                                </div>
                            </div>
                            <p class="help-block">La edad del niño gratis indica hasta que edad el niño no tiene coste adicional.(Por ejemplo hasta 2 años).</p>
                        </div>
                        <div class="form-group form-ctrol-admin">
                            <label for="txtemail" class="col-sm-2 control-label">Niños de pago</label>
                            <div class="col-sm-4">
                                <div class="input-group">
                                     <asp:DropDownList ID="cbochildPay" runat="server" CssClass="form-control emailoperator" ClientIDMode="Static"></asp:DropDownList>
                                    <span class="input-group-addon"><i class="fa fa-male"></i></span>
                                </div>
                            </div>
                            <p class="help-block"> La edad del niño de pago indica hasta que edad el niño tiene condiciones especiales en las tarifas.</p>
                        </div>                        
                    </div>
                    <p class="clearfix">Las edades normalmente son hasta 2 y de 2 a 16.</p>                   
                    
                </div>
            </div>
            </asp:PlaceHolder>
             
            <asp:PlaceHolder ID="plhhotel_board" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading"><i class="fa fa-cutlery fa-lg"></i> Tipos de regimen que ofrece el hotel</div>
                <div class="panel-body">
                    <p class="help-block">Seleccione los regimenes que admite su hotel. Se mostrará en la introducción de tarifas y cuando el cliente realice un busqueda. Introduzca los precios para cada tipo. Despues en cada tarifa podrá modificarlos en función de las necesidades.</p>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <table class="table" style="max-width: 380px;">
                                <tr>
                                    <td>
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="chkonlyroom" runat="server" />Solo alojamiento
                                            </label>
                                        </div>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="chkboardbreakfast" runat="server" />Desayuno incluido
                                            </label>

                                        </div>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpriceboardbreakfast" runat="server" CssClass="onlynum" MaxLength="10" Columns="6"></asp:TextBox>€
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="chkboardmp" runat="server" />Media Pension
                                            </label>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpriceboarmp" runat="server" CssClass="onlynum" MaxLength="10" Columns="6"></asp:TextBox>€
                                    </td>
                                </tr>

                                <tr>
                                    <td>

                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="chkboardpc" runat="server" />Pension Completa
                                            </label>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpriceboarpc" runat="server" CssClass="onlynum " MaxLength="10" Columns="6"></asp:TextBox>€
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="chkboardti" runat="server" />Todo incluido
                                            </label>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpriceboarti" runat="server" CssClass="onlynum " MaxLength="10" Columns="6"></asp:TextBox>€
                                    </td>
                                </tr>
                            </table>




                        </div>
                        
                    </div>                        
                    
                </div><%--panel-body--%>
            </div>
            <%--panel--%>
        </asp:PlaceHolder>
               
        </asp:Panel>

         <div class="panel panel-default">
            <div class="panel-heading"><i class="fa fa-envelope fa-lg"></i> Correos de confirmación</div>
            <div class="panel-body">
                <div class="form-horizontal ">
                    <div class="form-group form-ctrol-admin">
                        <label for="txtemail" class="col-sm-2 control-label">Email responsable reservas</label>
                        <div class="col-sm-6">
                            <div class="input-group">
                                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control emailconfirmation" MaxLength="255" placeholder="Ejemplo: email de recepción"></asp:TextBox>
                                <span class="input-group-addon"><i class="fa fa-envelope fa-lg"></i></span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group form-ctrol-admin">
                        <label for="txtemailcopy" class="col-sm-2 control-label">Email de copia</label>
                        <div class="col-sm-6">
                            <div class="input-group">
                                <asp:TextBox ID="txtemailcopy" runat="server" CssClass="form-control emailconfirmation" MaxLength="255" placeholder="Ejemplo: email de gerencia"></asp:TextBox>
                                <span class="input-group-addon"><i class="fa fa-envelope fa-lg"></i></span>
                            </div>
                        </div>
                    </div>
                </div>
               
            </div><%--panel-body--%> 
        </div>  <%--panel--%>    
        

         <div class="panel panel-default">
            <div class="panel-heading"><i class="fa fa-calendar  fa-lg"></i> Dias de antelación para admitir una reserva (Release) a nivel global</div>
            <div class="panel-body">
                <div class="form-stacked">
                    <div class="form-group form-ctrol-admin">
                        <label for="txtdaybefore" class="col-sm-2 control-label">Dias de antelación</label>
                        <div class="col-sm-2">
                            <div class="input-group">
                                <asp:DropDownList ID="cbodayinadvance" runat="server" CssClass="form-control release"></asp:DropDownList>
                                <span class="input-group-addon"><i class="fa fa-plus-square fa-lg"></i></span>
                            </div>                            
                        </div>
                        <p class="help-block">Puede configurar este valor a nivel global, de cliente y por tarifa. Siempre prevalece el de mayor valor.</p>
                    </div>
                </div>
               
            </div><%--panel-body--%> 
        </div>
        
        <div class="panel-group">

         <div class="panel panel-default">
            <div class="panel-heading">
                <a data-toggle="collapse" href="#collapsebank" class="collapsed">
                <i class="fa fa-credit-card  fa-lg"></i> Formas de pago admitidas
                    </a>
                    </div>
             
            <div id="collapsebank" class="panel-collapse collapse">
                <div class="panel-body">
                    
                 <div class="form-horizontal">
                    <div class="form-group form-ctrol-admin">
                        <div class="col-sm-8">
                           <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="chkHasTpv" runat="server"  data-toggle="tooltip" data-placement="top" title="Si selecciona esta opción aparecerá la opción de pago en tarifas."/>El hotel dispone de TPV online
                                </label>
                            </div>
                        </div>
                    </div>
                </div>   

                <h4><strong>Pago por transferencia</strong></h4>
                <p>Si utiliza la opción pago por transferencia debe rellenar los datos a continuación. El cliente al hacer la reserva recibirá este numero con el codigo de reserva para formalizar el pago. </p>
                <div class="">
                     <div class="form-group form-ctrol-admin" style="width: 200px;">
                        <label for="txtiban" class="control-label">Codigo IBAN</label>
                        <asp:TextBox ID="txtiban" runat="server" CssClass="form-control onlynum" MaxLength="4" placeholder=""></asp:TextBox>
                    </div>                   
                    <div class="form-group  form-ctrol-admin" style="width: 200px;">
                        <label for="txtncuenta" class="control-label">Numero de cuenta</label>
                        <asp:TextBox ID="txtncuenta" runat="server" CssClass="form-control onlynum " MaxLength="20" placeholder="" style="width: 200px;"></asp:TextBox>
                    </div>
                    <p>
                        El codigo IBAN se compone de un maximo de 34 caracteres. Es recomendable no introducir espacios intercalados aunque mejoren la presentación.<br />
                        Ej:IBAN ES07 1234 1234 12 1234567890

                    </p>
                </div>
               
            </div><%--panel-body--%> 
             </div>
        </div>
            

            </div><%--panel-group--%> 
        
        <div class="row">
            <div class="col-sm-6">
                <asp:LinkButton ID="btnsave" runat="server" CssClass="btn btn-default btn-lg">Guardar configuración <i class="fa fa-save"></i></asp:LinkButton>
            </div>
            <div class="col-sm-6">
                <p id="availabilitychange" class="hidden info">Se han producido cambios en la disponibilidad. Recuerde pulsar en guardar.<br />
                    Para cancelar los cambios vuelva a cargar los datos.</p>
            </div>
        </div>
        

    </div> <%--container--%>

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
    <script>
  $(document).ready(function () {
            $(".emailconfirmation")
                .popover({ trigger: 'focus', title: 'Email confirmación de reservas', content: "Introduzca el email donde desea ser notificado de cada reserva realizada." })
                .blur(function () {
                    $(this).popover('hide');
                });
            $(".release")
                .popover({ trigger: 'focus', title: 'Dias de antelación', content: "Introduzca aqui los dias de antelacion necesarios para realizar una reserva. Recuerde que siempre aplica el release de mayor valor." })
                .blur(function () {
                    $(this).popover('hide');
                });
            //$(".openedititem-newitem")(
            //    $("#createitem").appendTo(".modal-body");
            //    );



        });
        
    </script>
</asp:Content>

