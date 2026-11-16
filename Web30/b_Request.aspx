<%@ Page Language="VB" MasterPageFile="~/CFRMasterPage.master" AutoEventWireup="false" CodeFile="B_request.aspx.vb" Inherits="B_request" Title="CFR-Book" MaintainScrollPositionOnPostback="true" %>

<%@ Import Namespace="cfr" %>
<%@ Register Src="UC/B_Paid.ascx" TagName="B_Paid" TagPrefix="uc2" %>
<%@ Register Src="UC/B_ClientData.ascx" TagName="B_ClientData" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <link href="css/bootstrapdatepicker/bootstrap-datepicker3.standalone.css" rel="stylesheet" media="screen">
    <link href="plugins/magnific-popup/magnific-popup.css" rel="stylesheet">
    <link href="css/booking-search-compact.css?v=10" rel="stylesheet">

    <script language="JavaScript" src="js/checkNumbers.js" type="text/javascript"></script>
    <script language="javascript" src="js/creditcard.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="display: none;">
        <asp:LinkButton ID="btnbookloginregisteredusers" CssClass="btn btn-default btn-sm" ValidationGroup="RegisteredLoginValidation" ClientIDMode="static" runat="server"></asp:LinkButton>
    </div>
    <asp:HiddenField ClientIDMode="static" ID="selectedroomid" runat="server" Value="0" />
    <asp:HiddenField ClientIDMode="static" ID="norefundtariffselected" runat="server" Value="0" />
    <asp:HiddenField ClientIDMode="static" ID="paidTypesAdmited" runat="server" />
    <asp:HiddenField ClientIDMode="static" ID="hdfpolicycondition" runat="server" Value="" />

    <div id="bookbody">


        <div class="modal" id="bookingloginmodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h3 class="loginregisteredusertitle">Acceso como cliente registrado</h3>
                    </div>
                    <div class="modal-body">
                        <div class="ADCAccess ">
                            <div class="">
                                <div class="col-sm-5 hidden-xs"></div>
                                <div class="col-xs-12 col-sm-7">
                                    <div class="form-group">
                                        <label for="RegisteredLogin"><%=Me.langCaption.getCaption(75)%></label>
                                        <asp:TextBox ID="RegisteredLogin" runat="server" Columns="30" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <div class="validatorbox">
                                            <asp:RequiredFieldValidator ID="ReqLogin" runat="server" ControlToValidate="RegisteredLogin"
                                                Display="Dynamic" ErrorMessage="RequiredFieldValidator" ValidationGroup="RegisteredLoginValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="RegisteredPwd"><%=Me.langCaption.getCaption(76)%></label>
                                        <asp:TextBox ID="RegisteredPwd" runat="server" Columns="30" MaxLength="128" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <div class="validatorbox">
                                            <asp:RequiredFieldValidator ID="ReqClave" runat="server" ControlToValidate="RegisteredPwd"
                                                Display="Dynamic" ErrorMessage="RequiredFieldValidator" ValidationGroup="RegisteredLoginValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div style="float: right;">
                                        <button type="button" class="btn btn-default btn-lg" onclick="document.getElementById('btnbookloginregisteredusers').click();"><i class="fa fa-lock pr-10"></i>Acceso</button>
                                    </div>

                                </div>


                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal"><% =Me.langCaption.GetCaptionBy ( Language.msgEnum.close) %></button>
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <asp:Panel ID="pnlcancellink" runat="server">
                <div class="col-xs-4">
                    <h4 class="cancelbooklabel">
                        <asp:Literal ID="lnkCancelBook" runat="server"></asp:Literal></h4>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlLoginRegisteredUsers" runat="server">
                <div class="col-xs-4">

                    <asp:Panel ID="pnlloginregistereduser" runat="server">
                        <h4 class="loginregistereduser">
                            <a href="#" onclick="showloginform();"><%=Me.langCaption.getCaption(72)%></a>
                        </h4>
                    </asp:Panel>

                </div>

            </asp:Panel>

            <asp:Panel ID="pnllanguage" runat="server">
                <div class="col-xs-4">
                    <div class="booklangselection">
                        <%-- ReSharper disable once VBWarnings::BC42016 --%>
                        <%
                            Dim urlLangSelected As String = "b_request.aspx?hotelCode=" & Me.holdprop.id & "&site=" & Me.siteWeb.id & "&language={0}"
                        %>
                        <ul class="navbar-right">
                            <li id="lang-menu" class="dropdown">
                                <a id="langdrop" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" role="button" aria-expanded="true">

                                    <img class="flag" src="images/idioma<%=Me.bookData.language   %>.gif" alt="" /><span class="caret"></span>
                                </a>
                                <ul class="dropdown-menu .dropdown-menu-right" role="menu" aria-labelledby="langdrop">
                                    <li role="presentation"><a role="menuitem" tabindex="-1" href="<%=string.Format (urlLangSelected,1) %>">
                                        <img class="flag" src="images/idioma1.gif" alt="" />Español</a></li>
                                    <li role="presentation"><a role="menuitem" tabindex="-1" href="<%=string.Format (urlLangSelected,2) %>">
                                        <img class="flag" src="images/idioma2.gif" alt="" />English</></a></li>
                                    <li role="presentation"><a role="menuitem" tabindex="-1" href="<%=string.Format (urlLangSelected,3) %>">
                                        <img class="flag" src="images/idioma3.gif" alt="" />German</a></li>
                                    <li role="presentation"><a role="menuitem" tabindex="-1" href="<%=string.Format (urlLangSelected,4) %>">
                                        <img class="flag" src="images/idioma4.gif" alt="" />French</a></li>
                                </ul>
                            </li>
                        </ul>
                    </div>

                </div>
            </asp:Panel>

        </div>
        <asp:Panel ID="pnlpropertyheader" runat="server">
            <%-- Hotel name --%>
            <div class="row">
                <div class="col-xs-12">
                    <div class="bookhotelheader hoteldescbody">
                        <%=Me.bookData.hotel.hotelName  %>
                        <%If Me.holdprop.Tipo = CFRConst.unitTypeEnum.hotel Then%>
                        <span class="hotelstars"><%=(cfr.iu.HotelIU.getCategoryImghtml(Me.bookData.hotel.cod_Categoria, 1))%></span>
                        <% End If%>

                        <p class="bookhotellocalization hidden-xs"><%=Me.bookData.hotel.Direccion  %></p>
                    </div>
                </div>
            </div>

        </asp:Panel>


        <div id="bookerForm" class="container">

            <header class="booking-step-heading">
                <div class="booking-step-number" aria-hidden="true">1</div>
                <div class="booking-step-copy">
                    <span class="booking-step-eyebrow"><% =Me.langCaption.getCaption (319) %>&nbsp;01</span>
                    <h1 class="booking-step-title"><%=Me.langCaption.getCaption(10)%></h1>
                </div>
            </header>
            <!--STEP 1 formSearch -->
            <div id="wbSearchForm" class="wb-search-panel">
                <div class="wb-search-fields">
                    <div class="wb-date-group">
                        <div class="input-daterange wb-date-range" id="mydatepicker">
                            <div class="wb-field wb-date-field">
                                <label class="wb-field-label" for="checkInDate"><%=Me.langCaption.getCaption(81)%></label>
                                <div class="input-group date ">

                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                    <asp:TextBox ClientIDMode="static" ID="checkindate" runat="server" CssClass="form-control sfinput dtpreadonly cbosearchdate " MaxLength="10" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="wb-field wb-time-field" id="cbotimecheckindiv" runat="server">
                                <label class="wb-field-label" for="cbotimecheckin" runat="server" id="cbotimecheckinlabel">Hora</label>
                                <asp:DropDownList ID="cbotimecheckin" runat="server" CssClass="form-control cbosearchtime" ClientIDMode="Static"></asp:DropDownList>
                            </div>

                            <div class="wb-field wb-date-field">
                                <label for="checkOutDate" class="wb-field-label"><%=Me.langCaption.getCaption(82)%></label>
                                <div class="input-group ">
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                    <asp:TextBox ClientIDMode="static" ID="checkoutdate" runat="server" CssClass="date-picker form-control sfinput dtpreadonly cbosearchdate" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                </div>



                            </div>
                            <div class="wb-field wb-time-field" id="cbotimecheckoutdiv" runat="server">
                                <label class="wb-field-label" for="cbotimecheckout" runat="server" id="cbotimecheckoutlabel">Hora</label>
                                <asp:DropDownList ID="cbotimecheckout" runat="server" CssClass="form-control cbosearchtime" ClientIDMode="Static" Width="4"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="wb-nights">
                            <span class="wb-nights-label"><%=Me.langCaption.getCaption(341)%></span>
                            <strong class="wb-nights-value">
                                <asp:Label ID="lblnightsCalendar" runat="server" ClientIDMode="Static" CssClass="cal-nights"></asp:Label></strong>
                        </div>

                    </div>

                    <asp:PlaceHolder ID="plhcar_pickup" runat="server">
                        <asp:HiddenField ClientIDMode="static" ID="selectedlocationpickid" runat="server" Value="0" />
                        <asp:HiddenField ClientIDMode="static" ID="selectedlocationpickprice" runat="server" Value="0" />
                        <asp:HiddenField ClientIDMode="static" ID="selectedlocationdropid" runat="server" Value="0" />
                        <asp:HiddenField ClientIDMode="static" ID="selectedlocationdropprice" runat="server" Value="0" />

                        <div class="wb-field">

                            <label class="wb-field-label" for="cbolocationpickup"><%=Me.langCaption.getCaption(383) %></label>
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <span class="fa fa-map-marker"></span>
                                </span>
                                <asp:DropDownList ID="cbolocationpickup" runat="server" CssClass='form-control' ClientIDMode="static"></asp:DropDownList>
                            </div>

                        </div>
                        <div class="wb-field">

                            <asp:CheckBox ID="chkreturntolocation" runat="server" Checked="false" ClientIDMode="static" onclick="chkreturntolocation_click();" />
                            <asp:Label ID="chkreturntolocation_lbl" runat="server" Text="Label" ClientIDMode="static" CssClass="wb-field-label" AssociatedControlID="chkreturntolocation">Devolver el coche en la misma oficina</asp:Label>
                            <script>
                                function chkreturntolocation_click() {
                                    var checklocation = $("#chkreturntolocation");
                                    if (checklocation.is(':checked')) {
                                        $("#cbolocationdropdiv").show(500);
                                    }
                                    else {
                                        $("#cbolocationdropdiv").hide(500);
                                    }

                                    caroptionssummary();
                                }

                            </script>

                        </div>
                        <div id="cbolocationdropdiv" class="wb-field" style="display: none;">
                            <label class="wb-field-label" for="cbolocationdrop"><%=Me.langCaption.getCaption(384) %></label>
                            <div class="input-group">

                                <span class="input-group-addon">
                                    <span class="fa fa-map-marker"></span>
                                </span>
                                <asp:DropDownList ID="cbolocationdrop" runat="server" CssClass='form-control' ClientIDMode="static"></asp:DropDownList>
                            </div>
                        </div>
                        <div id="locationpickcost" style="display: none;"><%=Me.langCaption.GetCaption(386) %><span id="locationpickcostvalue"></span>€</div>
                    </asp:PlaceHolder>

                    <asp:PlaceHolder ID="plhhotel_pax" runat="server">
                        <div class="wb-guest-grid">
                            <div class="wb-field">
                                <label class="wb-field-label" for="f_numAdultos"><%=Me.langCaption.getCaption(14)%></label>
                                <asp:DropDownList ID="f_numAdultos" runat="server" ClientIDMode="Static" CssClass="form-control booksearch">
                                </asp:DropDownList>
                            </div>
                            <div class="wb-field">
                                <label class="wb-field-label" for="f_ninosPago">
                                    <%=Me.langCaption.getCaption(15)%> &lt; <%=Me.bookData.hotel.BookParam.childAgeToPay%> <%=Me.langCaption.getCaption(16)%>
                                </label>
                                <asp:DropDownList ID="f_ninosPago" runat="server" ClientIDMode="Static" CssClass="form-control booksearch">
                                </asp:DropDownList>
                            </div>
                            <div class="wb-field">
                                <label class="wb-field-label" for="f_ninosfree">
                                    <%=Me.langCaption.getCaption(15)%> &lt; <%=Me.bookData.hotel.BookParam.childAgeFree%> <%=Me.langCaption.getCaption(16)%>
                                </label>
                                <asp:DropDownList ID="f_ninosfree" runat="server" ClientIDMode="Static" CssClass="form-control booksearch">
                                </asp:DropDownList>
                            </div>
                            <asp:PlaceHolder ID="placeHolderRegimen" runat="server">
                                <div class="wb-field wb-board-field">
                                    <label class="wb-field-label" for="f_regimen"><%=Me.langCaption.getCaption(17)%></label>
                                    <asp:DropDownList ID="f_regimen" runat="server" ClientIDMode="Static" CssClass="form-control booksearch">
                                    </asp:DropDownList>
                                </div>
                            </asp:PlaceHolder>
                        </div>
                    </asp:PlaceHolder>

                    <asp:UpdatePanel ID="updpnlagencydata" runat="server">

                        <ContentTemplate>
                            <asp:Panel ID="pnlBtnAgency" runat="server" Visible="false">
                                <div class="">


                                    <h3>
                                        <asp:Literal ID="lblAgencyTitleSelectInfo" runat="server" Text="Seleccione el tipo de cliente"></asp:Literal></h3>
                                    <ul class="list-icons form-control-agency">
                                        <li>
                                            <asp:RadioButton ID="rdbTariffForClientPrivate" runat="server" GroupName="rdbTariffForClient" Text="Cliente Particular" />
                                        </li>
                                        <li>
                                            <asp:RadioButton ID="rdbTariffForClientCompany" runat="server" GroupName="rdbTariffForClient" Text="Cliente Empresas" />

                                        </li>
                                        <%-- <li>
                                            <asp:RadioButton ID="rdbTariffForClientCompanyRegistered" runat="server" GroupName="rdbTariffForClient"
                                                Text="Tarifa Empresa Registrada" />
                                            <asp:DropDownList ID="cboCompaniesForAgency" runat="server">
                                            </asp:DropDownList>
                                        </li>--%>
                                    </ul>
                                </div>

                                <asp:Panel ID="pnlCloseSessionRegistered" Enabled="true" runat="server" Width="100%" Visible="false" Style="padding: 20px;">
                                    <i class="fa fa-user"></i>
                                    <asp:Literal ID="litBookOperatorRegisteredDesc" runat="server"></asp:Literal>
                                    <asp:LinkButton ID="btnCloseSesion" runat="server" ToolTip="Pulse para cerrar la sesión"><i class="fa fa-power-off fa-2x"></i></asp:LinkButton>
                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>


                    </asp:UpdatePanel>
                </div>
                <div class="wb-search-actions">
                    <div class="wb-search-submit">

                        <asp:Panel ID="pnlpromotioncodes" runat="server" ClientIDMode="static">
                            <script>
                                function promotioncodecollapseonclick() {
                                    $("#txtpromotioncodediv").slideToggle();
                                    $("#txtpromotioncode").focus();
                                }
                            </script>


                            <div class="" style="padding-bottom: 12px;">
                                <a class="#" onclick="promotioncodecollapseonclick()" style="cursor: pointer;"><%=Me.langCaption.getCaption(29)%></a>
                                <div id="txtpromotioncodediv" class="" runat="server" clientidmode="static" style="display: none;">
                                    <asp:TextBox ID="txtpromotioncode" runat="server" MaxLength="50" CssClass="form-control" ClientIDMode="static"></asp:TextBox>
                                    <div id="promotioncodeerrordiv" class='has-error' style="width: 200px; display: none;"><%=Me.langCaption.GetCaption(32)%></div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlBtnPrivateClient" runat="server" Visible="true" CssClass="wb-search-button-panel">
                            <asp:LinkButton ID="btnFindAction" runat="server" CssClass="btn btn-primary btn-lg" OnClientClick="return validateDates();" ValidationGroup="BookSelecctionGroup" ClientIDMode="Static" CausesValidation="True"></asp:LinkButton>
                        </asp:Panel>
                    </div>

                </div>


            </div>
            <% 'formSearch end %>

            <div id="resultdivscroll"></div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Literal ID="litmsgerrbookfind" runat="server"></asp:Literal>
                    <asp:Panel ID="divbookloading" runat="server" ClientIDMode="static">
                        <div class="text-center" style="padding: 20px;">
                            <span style="color: white; font-size: 24px; display: inline-block; position: relative; top: -17px; margin-right: 68px;">Buscando </span><i class="fa fa-spinner fa-spin fa-5x"></i>
                        </div>
                    </asp:Panel>


                    <asp:Literal ID="litMapAvailableDates" runat="server"></asp:Literal>

                    <asp:Panel ClientIDMode="static" ID="pnlResults" runat="server" Visible="false">


                        <header class="booking-step-heading booking-step-heading-results">
                            <a class="booking-step-number" href="#" onclick="changeroom();return false;" aria-label="<% =Me.langCaption.getCaption (319) %> 2">2</a>
                            <div class="booking-step-copy">
                                <span class="booking-step-eyebrow"><% =Me.langCaption.getCaption (319) %>&nbsp;02</span>
                                <h2 class="booking-step-title"><%=Me.langCaption.getCaption(344)%></h2>
                            </div>
                        </header>

                        <div id="wbResults">
                            <div id="wbRoomList">
                                <asp:Literal ID="litRoomFinded" runat="server" EnableViewState="False"></asp:Literal>
                            </div>
                        </div>
                        <% 'formResults %>
                        <div style="text-align: right;" class="changepref"><a href="#" class="changepref" onclick="changeroom();return false;"><%=Me.langCaption.getCaption(369)%></a></div>

                    </asp:Panel>

                    <asp:Panel ClientIDMode="static" ID="pnlClientData" Enabled="true" runat="server" Visible="false">
                        <header class="booking-step-heading steprow">
                            <div class="booking-step-number" aria-hidden="true">3</div>
                            <div class="booking-step-copy">
                                <span class="booking-step-eyebrow"><% =Me.langCaption.getCaption (319) %>&nbsp;03</span>
                                <h2 class="booking-step-title"><%=Me.langCaption.getCaption(112)%></h2>
                            </div>
                        </header>
                        <uc1:B_ClientData ClientIDMode="static" ID="B_ClientData1" runat="server" />


                    </asp:Panel>

                    <asp:Panel ID="pnlbookconfirmation" runat="server" ClientIDMode="static" Visible="false" CssClass="row">
                        <header class="booking-step-heading">
                            <div class="booking-step-number" aria-hidden="true">4</div>
                            <div class="booking-step-copy">
                                <span class="booking-step-eyebrow"><% =cfr.Language.getCaption (319) %>&nbsp;04</span>
                                <h2 class="booking-step-title"><%=cfr.Language.GetCaption(151)%></h2>
                            </div>
                        </header>
                        <div id="pnlbookconfirmationliteraldata2print">
                            <asp:Literal ID="pnlbookconfirmationliteral" runat="server"></asp:Literal>
                        </div>


                        <div class="text-center">
                            <button id="btnviewbookprint_req" type="button" class="btn btn-lg  btn-primary" onclick="printdiv('pnlbookconfirmationliteraldata2print');"><i class="fa fa-print fa-4"></i><%=cfr.Language.GetCaption(207)%></button>
                        </div>

                    </asp:Panel>


                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFindAction" />
                </Triggers>
            </asp:UpdatePanel>


            <asp:Panel ID="pnlbooksummary" runat="server" CssClass="wb-summary-panel" ClientIDMode="Static" Style="display: none;">

                <div id="booksummary" class="wb-summary">
                    <header class="wb-summary-header">
                        <i class="fa fa-shopping-cart" aria-hidden="true"></i>
                        <h2><%=Me.langCaption.getCaption(340)%></h2>
                    </header>

                    <div class="wb-summary-grid">
                        <div class="wb-summary-details">
                            <div class="wb-summary-item">
                                <span class="wb-summary-label"><i class="glyphicon glyphicon-calendar"></i><%=Me.langCaption.getCaption(12)%></span>
                                <asp:Label ID="lblDateIn" runat="server" Text="24 Julio 2008" CssClass="wb-summary-value" ClientIDMode="Static"></asp:Label>
                                <%If Me.holdprop.Tipo = CFRConst.unitTypeEnum.car Then%>
                                <div id="cbolocationpickupsummary" class="wb-summary-note"><i class="fa fa-map-marker"></i><span id="cbolocationpickupsummarydesc"></span></div>
                                <%End If%>
                            </div>

                            <div class="wb-summary-item">
                                <span class="wb-summary-label"><i class="glyphicon glyphicon-calendar"></i><%=Me.langCaption.getCaption(13)%></span>
                                <asp:Label ID="lblDateOut" runat="server" Text="25 Julio 2008" CssClass="wb-summary-value" ClientIDMode="Static"></asp:Label>
                                <%If Me.holdprop.Tipo = CFRConst.unitTypeEnum.car Then%>
                                <div id="cbolocationdropsummary" class="wb-summary-note"><i class="fa fa-map-marker"></i><span id="cbolocationdropsummarydesc"></span></div>
                                <%End If%>
                            </div>

                            <div class="wb-summary-item wb-summary-item-compact">
                                <span class="wb-summary-label"><i class="fa fa-moon-o"></i><%=Me.langCaption.getCaption(341)%></span>
                                <asp:Label ID="lblNights" runat="server" Text="1" ClientIDMode="Static" CssClass="wb-summary-value"></asp:Label>
                            </div>

                            <% If Me.holdprop.Tipo = CFRConst.unitTypeEnum.hotel Then %>
                            <div class="wb-summary-item wb-summary-item-compact">
                                <span class="wb-summary-label"><i class="fa fa-male"></i><%=Me.langCaption.getCaption(14)%></span>
                                <asp:Label ID="lblNumAdults" runat="server" Text="1" CssClass="wb-summary-value"></asp:Label>
                            </div>

                            <div class="wb-summary-item wb-summary-item-wide">
                                <span class="wb-summary-label"><i class="fa fa-child"></i><%=Me.langCaption.getCaption(15)%></span>
                                <span class="wb-summary-value"><strong><asp:Label ID="lblNumChildFree" runat="server" Text="0"></asp:Label></strong> &lt; <%=Me.bookData.hotel.BookParam.childAgeFree%> <%=Me.langCaption.getCaption(16)%>&nbsp;&nbsp;·&nbsp;&nbsp;<strong><asp:Label ID="lblNumChildPay" runat="server" Text="0"></asp:Label></strong> &lt; <%=Me.bookData.hotel.BookParam.childAgeToPay%> <%=Me.langCaption.getCaption(16)%></span>
                            </div>
                            <% End If%>

                            <div id="summaryRoomSelected" class="wb-summary-item wb-summary-item-wide wb-summary-room">
                                <% Dim unittypeimage As String ="fa fa-hotel"
                                                            If Me.holdprop.Tipo = CFRConst.unitTypeEnum.car Then
                                                                unittypeimage = "fa fa-car"
                                                            End If%>
                                <span class="wb-summary-label"><i class="<%=unittypeimage %>"></i><%=Me.langCaption.getCaptionby(Language.msgEnum.SelectRoomField) %></span>
                                <span id="roomselecteddescription" class="wb-summary-value"></span>
                            </div>
                        </div>

                        <aside class="wb-summary-total">
                            <div id="locationpickcostsummaryrow" class="wb-summary-extra" style="display: none;">
                                <i class="fa fa-info-circle"></i><%=Me.langCaption.getCaption(386)%> <span id="locationpickcostsummary"></span>€
                            </div>
                            <div id="bookpricetotaldiv" class="hidden">
                                <span class="wb-summary-total-label"><%=Me.langCaption.getCaption(163)%></span>
                                <div class="wb-summary-total-value"><span id="bookpricetotal">0</span><span class="glyphicon glyphicon-euro" aria-hidden="true"></span></div>
                                <div class="norefundtariff wb-summary-norefund" style="display: none;"><%=Me.langCaption.getCaption(387)%></div>
                            </div>
                        </aside>
                    </div>
                </div>


                <asp:Panel ID="pnlBookButton" runat="server" Visible="true" Style="display: none;" ClientIDMode="Static">

                    <% 'LPD %>
                    <asp:Panel ID="pnlLpd" runat="server" Visible="false">
                        <div id="booklopdcheck">

                            <asp:CheckBox ID="aceptolopd" runat="server" ClientIDMode="Static" onclick="javascript:lopdCheckBoxOnClick();" />&nbsp;&nbsp;<a id="lopdlink" href="javascript:showlopdwindow();"><%=Me.langCaption.getCaption(281)%></a>
                            <div id="ReqaceptolopdDiv">
                                <asp:CustomValidator runat="server" ID="Reqaceptolopd" ClientIDMode="Static" EnableClientScript="true"
                                    OnServerValidate="CheckBoxRequired_ServerValidate"
                                    ClientValidationFunction="lopdCheckBoxRequired_ClientValidate"
                                    CssClass="required" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ValidationGroup="clientValidationGroup">You must select this box to proceed.</asp:CustomValidator>
                            </div>

                        </div>
                    </asp:Panel>

                    <div id="bookconfirmationdiv">
                        <asp:LinkButton ID="BookConfirmationBtn" runat="server" CssClass="btn btn-primary btn-lg" ValidationGroup="clientValidationGroup"><%=Me.langCaption.getCaption(144)%></asp:LinkButton>
                        <asp:CustomValidator runat="server" ID="CustomValidator1" ClientIDMode="Static" EnableClientScript="true"
                            OnServerValidate="booking_ServerValidate"
                            ClientValidationFunction="validateBooking" Display="None" ValidationGroup="clientValidationGroup"></asp:CustomValidator>
                    </div>
                </asp:Panel>

            </asp:Panel>


            <br />

            <asp:Literal ID="litPowered" runat="server"></asp:Literal>
            <%  'bookerForm %>
        </div>

        <asp:Panel ID="pnlinfobook" runat="server" ClientIDMode="static" CssClass="col-xs-12" Style="display: none;">

            <h3 class="book-info"><i class="fa fa-info-circle fa-2"></i><% =Me.langCaption.GetCaptionBy(285)%></h3>
            <ul class="list-icons">
                <li><i class="fa fa-check"></i><%=Me.langCaption.GetCaptionBy(93)%></li>
                <li><i class="fa fa-check"></i><%=Me.langCaption.GetCaptionBy(94)%> </li>
                <% If Me.holdprop.Tipo = CFRConst.unitTypeEnum.hotel Then %>
                <li><i class="fa fa-check"></i><%=Me.langCaption.GetCaptionBy(63)%><%=Me.bookData.hotel.BookParam.childAgeFree%> <%=Me.langCaption.GetCaptionBy(64)%></li>
                <li><i class="fa fa-check"></i><%=Me.langCaption.GetCaptionBy(65)%> <%=Me.bookData.hotel.BookParam.childAgeFree%> <%=Me.langCaption.GetCaptionBy(66)%> <%=Me.bookData.hotel.BookParam.childAgeToPay%> <%=Me.langCaption.GetCaptionBy(67)%></li>
                <%End If  %>
                <% If Me.bookData.hotel.BookParam.politicyBookConditions <> "" Then
                                Dim islist As Boolean = False
                                Dim withcheck As String = "<i class='fa fa-check'></i>"
                                Dim politicyBookConditions As String = Me.bookData.hotel.BookParam.politicyBookConditions
                                islist = politicyBookConditions.StartsWith("<ul")
                                
                                If islist = True Then
                                    withcheck = ""
                                End If
                %>
                <li><%=withcheck %> <%=Me.bookData.hotel.BookParam.politicyBookConditions%> </li>

                <%End if%>
            </ul>
            <%--<button type="button" class="btn btn-lg btn-danger" data-toggle="popover" title="Popover title" data-content="And here's some amazing content. It's very engaging. Right?">Click to toggle popover</button>--%>
        </asp:Panel>


        <%  'body %>
    </div>
    <!-- Modal -->
    <div class="modal" id="bookingErrorModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel"><%=Me.langCaption.GetCaptionBy(Language.msgEnum.CheckYourDataforBook)%></h4>
                </div>
                <div class="modal-body">

                    <p id="msgroomrequired" class="" style="display: none;"><span class="glyphicon glyphicon-exclamation-sign msgred" aria-hidden="true"></span><%=Me.langCaption.getCaptionby(Language.msgEnum.SelectRoomField)%></p>
                    <p id="msgclientdatarequired" class="" style="display: none;"><span class="glyphicon glyphicon-exclamation-sign msgred" aria-hidden="true"></span><%=Me.langCaption.getCaptionby(Language.msgEnum.CheckYourDataforBook)%></p>
                    <p id="p2" class="" style="display: none;"><span class="glyphicon glyphicon-exclamation-sign msgred" aria-hidden="true"></span></p>
                    <p id="msgcountryrequired" style="display: none;"><span class="glyphicon glyphicon-exclamation-sign msgred" aria-hidden="true"></span><%=Me.langCaption.getCaption(315)%></p>
                    <p id="msgpaidtyperequired" class="" style="display: none;"><span class="glyphicon glyphicon-exclamation-sign msgred" aria-hidden="true"></span><%=Me.langCaption.getCaption(98)%></p>
                    <p id="msgpaidtyperequired311" class="" style="display: none;"><span class="glyphicon glyphicon-exclamation-sign msgred" aria-hidden="true"></span><%=Me.langCaption.getCaption(311)%></p>
                    <div style="padding-left: 10px;">
                        <p id="msgpaidtyperequired313" class="" style="display: none;"><span class="glyphicon glyphicon-arrow-right msgred" aria-hidden="true"></span><%=Me.langCaption.getCaption(313)%></p>
                        <p id="msgpaidtyperequired289" class="" style="display: none;"><span class="glyphicon glyphicon-arrow-right msgred" aria-hidden="true"></span><%=Me.langCaption.getCaption(289)%></p>
                        <p id="msgpaidtyperequired314" class="" style="display: none;"><span class="glyphicon glyphicon-arrow-right msgred" aria-hidden="true"></span><%=Me.langCaption.getCaption(314)%></p>
                        <p id="msgpaidtyperequired99" class="" style="display: none;"><span class="glyphicon glyphicon-arrow-right msgred" aria-hidden="true"></span><%=Me.langCaption.getCaption(99)%></p>
                    </div>
                    <p id="msgpaidtyperequired280" class="" style="display: none;"><span class="glyphicon glyphicon-exclamation-sign msgred" aria-hidden="true"></span><%=Me.langCaption.getCaption(280)%></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal"><% =Me.langCaption.GetCaptionBy ( Language.msgEnum.close) %></button>
                </div>
            </div>
        </div>
    </div>


    <div class="modal" id="divdesglosedmodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H1"><%=Me.langCaption.getCaptionby(Language.msgEnum.roomPricebyDayTitle)%></h4>
                </div>
                <div class="modal-body">
                    <div id="desglosedpriceinner">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal"><% =Me.langCaption.GetCaptionBy ( Language.msgEnum.close) %></button>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

<asp:Content ID="bottomScript1" ContentPlaceHolderID="bottomScriptPlaceHolder" runat="Server">
    <!-- Magnific Popup javascript -->
    <script type="text/javascript" src="plugins/magnific-popup/jquery.magnific-popup.min.js"></script>
    <!-- Important Owl stylesheet -->
    <link rel="stylesheet" href="plugins/owl-carousel/owl.carousel.css">
    <!-- Default Theme -->
    <%--<link rel="stylesheet" href="plugins/owl-carousel/owl.theme.css">--%>
    <!-- Include js plugin -->
    <script src="plugins/owl-carousel/owl.carousel.js"></script>
    <script src="js/momentjs.js"></script>
    <script language="JavaScript" src="js/bookValidation.js?v=9" type="text/javascript"></script>
    <%-- script calendario bootstrap --%>
    <asp:Literal ID="zpcalendarLiteral" runat="Server"></asp:Literal>

    <script>
        $(document).ready(function () {


            $(function () {
                $('[data-toggle="popover"]').popover();
            });
        });

        if ($("#cbolocationpickup").length > 0) {

            $("#cbolocationpickup").change(function () {
                caroptionssummary();
                cbolocationchange();
            });
            $("#cbolocationdrop").change(function () {
                caroptionssummary();
                cbolocationchange();
            });
        }


        //Re-bind for callbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            unbindcontrolevents();
            bindcontrolevents();
            console.log("prm.add_endRequest");
        });



        function unbindcontrolevents() {
            //$("[data-toggle=tooltip]").unbind();
            //$(".editbook_click").unbind();
            var $owl = $(".owl-carousel.content-slider-with-controls.owl-room-pic");
            //$owl.destroy();
            console.log("owl destroy");
        }

        function bindcontrolevents() {

            //jQuery is wrapped in BindEvents function so it can be re-bound after each callback.
            //Your code would replace the following line:
            //$("[data-toggle=tooltip]").tooltip();
            bindpopupunit();
            bindcarouselunit();
            console.log("owl&pop bind");

        }






    </script>
    <asp:Literal ID="litcboloactionjs" runat="server"></asp:Literal>
</asp:Content>
