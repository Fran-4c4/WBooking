<%@ Page Title="CFR-Clave-Administrador" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="objChangeAdminpwd.aspx.vb" Inherits="Admin_objChangeAdminpwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
    
        <div class="container" >
    <h3 class="loginregisteredusertitle">Cambiar datos de acceso principales.</h3>
    <div class="logincontainer ">
         
                                    <div class="rowc">
                                        
                                        <div class="col-xs-5 hidden-xs"></div>
                                        <div class="col-xs-7">
                                           <div class="form-group">
                                                <label for="RegisteredPwd">Contraseña actual</label>
                                                <asp:TextBox ID="RegisteredPwd" runat="server" Columns="14" MaxLength="128" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                <div class="validatorbox">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RegisteredPwd"
                                                        Display="Dynamic" ErrorMessage="Introduce tu contraseña" ValidationGroup="NewLoginValidation"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>                                            
                                            <div class="form-group">
                                                <label for="NewLogin">Nuevo nombre de usuario</label>
                                                <asp:TextBox ID="NewLogin" runat="server" Columns="14" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                                <div class="validatorbox">
                                                    <asp:RequiredFieldValidator ID="ReqLogin" runat="server" ControlToValidate="NewLogin"
                                                        Display="Dynamic" ErrorMessage="Introduce tu usuario" ValidationGroup="NewLoginValidation"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="NewPwd">Nueva Contraseña</label>
                                                <asp:TextBox ID="NewPwd" runat="server" Columns="14" MaxLength="128" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                <div class="validatorbox">
                                                    <asp:RequiredFieldValidator ID="ReqClave" runat="server" ControlToValidate="NewPwd"
                                                        Display="Dynamic" ErrorMessage="Introduce tu contraseña" ValidationGroup="NewLoginValidation"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div style="float: right;">
                                                <asp:Button ID="btnLogin" runat="server" Text="Modificar claves" CssClass="btn btn-lg btn_green" ValidationGroup="NewLoginValidation" />
                                            </div>

                                        </div>


                                    </div>
                                </div>
    

                <h3 class="c2">Información </h3>
                <ul>
                    <li>La contraseña se almacena como un hash no recuperable. Si se pierde, deberá asignarse una nueva.
                    </li>
                    <li> No utilice caracteres como: '  % &amp; etc.</li>
                </ul>
    </div>


</asp:Content>

