<%@ Page Language="VB" MasterPageFile="~/blank.master" AutoEventWireup="false" CodeFile="B_PayGateReturn.aspx.vb" Inherits="B_PayGateReturn" %>

<%@ Import Namespace="cfr" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div class="container">
        <div class="row">
            <div class="col-xs-6">

            <asp:Panel ID="pnlOk" runat="server" Visible="false">

                <asp:Literal ID="litOperationScript" runat="server"></asp:Literal>


                <p class="ok">
                    <%=cfr.Language.getCaption(cfr.Language.msgEnum.bookConfirmated   )%>
                    <br />
                    <%=cfr.Language.getCaption(357)%>
                    <br />
                    <%=cfr.Language.getCaption(356)%>
                    <br />
                    <br />
                    <%=cfr.Language.getCaption(355)%>
                    <br />

                    <div class="center-block" style="max-width: 200px;">
                        <asp:HyperLink ID="btnContinueBook" runat="server" CssClass="btn btn-success btn-lg" Target="_parent"><%=cfr.Language.getCaption(cfr.Language.msgEnum.continueMsg   )%></asp:HyperLink>
                    </div>
                </p>
            </asp:Panel>

            <asp:Panel ID="pnlErr" runat="server" Visible="true">
                
                <p class="fail">
                    <%=cfr.Language.getCaption(cfr.Language.msgEnum.bankError)%>
                </p>
                <p>
                 <asp:HyperLink ID="btnCancelBack" runat="server" Target="_parent"><%=cfr.Language.getCaption(cfr.Language.msgEnum.initBookAgainByHotel)%></asp:HyperLink>                   
                </p>


            </asp:Panel>
                </div>
        </div>
    </div>

</asp:Content>
