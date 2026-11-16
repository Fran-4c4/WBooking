<%@ Page Title="" Language="VB"  AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Admin_Login" %>


<!DOCTYPE html >
<html lang="es">

<head id="Head1" runat="server">
    <title>Admin</title>
     <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="author" content="Francisco Moreno Santana">

        <!-- styles master --> 
    <link href="css/messages.css" rel="stylesheet" type="text/css"  >
    
    	<!-- Font Awesome CSS -->
    <link rel="stylesheet" href="../vendor/font-awesome-4.5.0/css/font-awesome.min.css">

    <!-- Bootstrap theme -->
    <link rel="stylesheet" href="../vendor/bootstrap-3.2.0/css/bootstrap.min.css">
	<%--<link href="css/bootstrap.css" rel="stylesheet" type="text/css" />--%>
    <!-- styles master -->
   

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    
   <style>
       .validatorbox {
        position:relative;
        display:block;
        color:red;
        float:right;
        font-size: 90%;
        }
       .password-field {
        position: relative;
       }
       .password-field .form-control {
        padding-right: 42px;
       }
       .password-toggle {
        position: absolute;
        top: 0;
        right: 0;
        z-index: 2;
        width: 42px;
        height: 34px;
        padding: 0;
        border: 0;
        background: transparent;
        color: #555;
        line-height: 34px;
        text-align: center;
       }
       .password-toggle:hover,
       .password-toggle:focus {
        background: transparent;
        color: #1b75bb;
        outline: 1px dotted #1b75bb;
        outline-offset: -4px;
       }
   </style>

</head>
<body  >
    <form runat="server" >


        <div class="container">
            <asp:Literal ID="litMsgErr" runat="server" Visible="true" ClientIDMode="static"></asp:Literal>
            <div class="logincontainer ">
                <h3><i class="fa fa-lock fa-lg"></i> Introduce tus datos de acceso.</h3>
                <div class="row">
                    <div class="col-xs-hidden col-sm-4"></div>
                    <div class="col-xs-12 col-sm-7">
                        <div class="form-vertical">
                         <div class="form-group">
                            <label for="RegisteredLogin">Usuario</label>
                            <asp:TextBox ID="RegisteredLogin" runat="server" Columns="14" MaxLength="50" CssClass="form-control" Text=""></asp:TextBox>
                            <div class="validatorbox">
                                <asp:RequiredFieldValidator ID="ReqLogin" runat="server" ControlToValidate="RegisteredLogin"
                                    Display="Dynamic" ErrorMessage="Introduce tu usuario" ValidationGroup="RegisteredLoginValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="RegisteredPwd">Contraseña</label>
                            <div class="password-field">
                                <asp:TextBox ID="RegisteredPwd" runat="server" Columns="14" MaxLength="128" CssClass="form-control" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
                                <button id="toggle-password" class="password-toggle" type="button" aria-label="Mostrar contraseña" aria-pressed="false">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </button>
                            </div>
                            <div class="validatorbox">
                                <asp:RequiredFieldValidator ID="ReqClave" runat="server" ControlToValidate="RegisteredPwd"
                                    Display="Dynamic" ErrorMessage="Introduce tu contraseña" ValidationGroup="RegisteredLoginValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                            <div>
                                <asp:CheckBox ID="chkRememberMe" runat="server" ClientIDMode="static" />
                                <asp:Label ID="lblrememberuser" runat="server" ClientIDMode ="Static" AssociatedControlID="chkRememberMe">Recordar en este ordenador</asp:Label>
                            </div>
                        <div style="float: right;">
                            <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-primary btn-lg " ValidationGroup="RegisteredLoginValidation" />
                        </div>                           
                        </div>


                    </div>
                    <div class="col-xs-hidden col-sm-1"></div>

                </div>
            </div>
        </div>


    </form>
    		<!-- Jquery and Bootstap core js files -->
		<script type="text/javascript" src="plugins/jquery.min.js"></script>
        <script type="text/javascript" src="plugins/jquery-ui.min.js"></script>
		<script type="text/javascript" src="bootstrap/js/bootstrap.min.js"></script>    
        <script type="text/javascript">
            (function () {
                var button = document.getElementById("toggle-password");
                var password = document.getElementById("RegisteredPwd");
                button.addEventListener("click", function () {
                    var show = password.type === "password";
                    password.type = show ? "text" : "password";
                    button.setAttribute("aria-pressed", show ? "true" : "false");
                    button.setAttribute("aria-label", show ? "Ocultar contraseña" : "Mostrar contraseña");
                    button.firstElementChild.className = show ? "fa fa-eye-slash" : "fa fa-eye";
                });
            }());
        </script>
    </body>
    </html>


