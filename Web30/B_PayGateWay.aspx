<%@ Page Language="VB" MasterPageFile="~/CFRMasterPage.master" AutoEventWireup="false" CodeFile="B_PayGateWay.aspx.vb" Inherits="B_PayGateWay" %>
<%@ Import Namespace="cfr" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">

<%--    <asp:Literal id="StyleSheetLiteral" runat="Server" />
    <asp:literal id="StyleSheetLiteralBooking" runat="Server"></asp:literal>--%>

<!--[if IE]>
<LINK media="screen, projection" href="css/ie_fixes.css" type="text/css" rel="stylesheet"> 
<![endif]-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <asp:HiddenField ID="Ds_Merchant_Order" runat="server" />   
        <asp:HiddenField ID="Ds_Merchant_MerchantSignature"  runat=server />
        <asp:HiddenField ID="OPER" runat="server" />

    <div id="bookbody" class="container">
                        <div class="row">
                            <div class="col-xs-2 step"><% =cfr.Language.getCaption (319) %>&nbsp;4</div>
                            <div class="col-xs-10 stepTit"><%=cfr.Language.GetCaption(323)%></div>
                            <div class="clearfix" ></div>
                            <div class="alert alert-warning alert-dismissible" role="alert">
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <strong><% =cfr.Language.getCaption(325)%></strong>
                            </div>
                            <div class="divCancelBook" >
                                <asp:HyperLink ID="returnToStep1" runat="server" CssClass="" Text="Pulse aqui si desea cancelar el proceso y volver a buscar."></asp:HyperLink>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div style="height:800px;text-align:center;margin:10px;padding:4px;">
                                  <asp:Literal ID="lit_B_PayGateWayPostIframe" runat="server"></asp:Literal>
                              </div>                              
                            </div>
                        </div>
    </div>
    
	


</asp:Content>