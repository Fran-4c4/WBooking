<%@ Page Title="CFR-Politicas del sistema" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="policyCancelConditions.aspx.vb" Inherits="policyCancel" ValidateRequest="false" %>
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
        <h1 class="sectiontitle"><i class="fa fa-wrench"></i> Politicas y mensajes del sistema de reservas</h1>
        <p>Introduzca las descripciones en los idiomas que desee.</p>
        <div class="separator-2"></div>
    </div>
    
    <div class="container">

        
        <div class="panel-group collapse-style-2 ">
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingServices">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#servicescollpase" class="collapsed">Politica de cancelación de reservas
                        </a>
                    </h4>
                </div>
                <div id="servicescollpase" class="panel-collapse collapse " role="tabpanel" >
                    <div class="panel-body panel-admin">
                        <div class="row">
                            <div class="col-xs-12 col-sm-6">
                                <label>Descripcion en español.</label>
                                <div>
                                    <asp:TextBox ID="txt_policy_es" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <label>Descripcion en Ingles.</label>
                                <div>
                                    <asp:TextBox ID="txt_policy_en" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>
                                    
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-6">
                                <label>Descripcion en Aleman.</label>
                                <div>
                                    <asp:TextBox ID="txt_policy_de" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>
                                 </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <label>Descripcion en Frances.</label>
                                <div>
                                    <asp:TextBox ID="txt_policy_fr" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>
                                     
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
      
        <div class="panel-group collapse-style-2 ">
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingbookconditions">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#bookconditionscollpase" class="collapsed">Condiciones de reservas
                        </a>
                    </h4>
                </div>
                <div id="bookconditionscollpase" class="panel-collapse  collapse  " role="tabpanel">
                    <div class="panel-body panel-admin">
                        <p>Introduzca las condiciones de reserva que deberia conocer el cliente. Por ejemplo: "Todos los impuestos incluidos. No se admiten mascostas".</p>
                        <div class="row">
                            <div class="col-xs-12 col-sm-6">
                                <label>Descripcion en español.</label>
                                <div>
                                    <asp:TextBox ID="txt_bookconditions_es" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <label>Descripcion en Ingles.</label>
                                <div>
                                    <asp:TextBox ID="txt_bookconditions_en" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>

                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-6">
                                <label>Descripcion en Aleman.</label>
                                <div>
                                    <asp:TextBox ID="txt_bookconditions_de" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <label>Descripcion en Frances.</label>
                                <div>
                                    <asp:TextBox ID="txt_bookconditions_fr" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="panel-group collapse-style-2 ">
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingbookconfirmation">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#bookconfirmationcollpase" class="collapsed">Mensaje de confirmación de reservas
                        </a>
                    </h4>
                </div>
                <div id="bookconfirmationcollpase" class="panel-collapse  collapse  " role="tabpanel">
                    <div class="panel-body panel-admin">
                        <p>Este mensaje se muestra al realizar una reserva y en el email de confirmación</p>
                        <div class="row">
                            <div class="col-xs-12 col-sm-6">
                                <label>Descripcion en español.</label>
                                <div>
                                    <asp:TextBox ID="txt_bookconfirmation_es" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <label>Descripcion en Ingles.</label>
                                <div>
                                    <asp:TextBox ID="txt_bookconfirmation_en" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>

                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-6">
                                <label>Descripcion en Aleman.</label>
                                <div>
                                    <asp:TextBox ID="txt_bookconfirmation_de" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <label>Descripcion en Frances.</label>
                                <div>
                                    <asp:TextBox ID="txt_bookconfirmation_fr" runat="server" TextMode="MultiLine" Rows="10" CssClass="wb-rich-editor form-control"></asp:TextBox>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-xs-12 col-sm-6">
                <asp:LinkButton ID="btnsave" runat="server" CssClass="btn btn-default btn-lg">Guardar configuración <i class="fa fa-save"></i></asp:LinkButton>
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

