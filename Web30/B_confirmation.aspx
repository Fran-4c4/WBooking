<%@ Page Language="VB"  Title="Book-Confirmation" MasterPageFile="~/CFRMasterPage.master" AutoEventWireup="false" CodeFile="B_confirmation.aspx.vb" Inherits="B_confirmation" %>
<%@ Import Namespace="cfr" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
    <script language="JavaScript" src="js/bookValidation.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-md-8">
                <div id="bookbody">
                    <div id="bookerForm" class="">

                        <div class="">
                            <div class="col-xs-2 step"><% =cfr.Language.getCaption (319) %>&nbsp;4</div>
                            <div class="col-xs-10 stepTit"><%=cfr.Language.GetCaption(151)%></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <% 'book confirmation here %>
                                <asp:Literal ID="litBookConfirmation" runat="server"></asp:Literal>
                            </div>
                        </div>

                        
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div>
                    <h3 class="moreoptions"><%=cfr.Language.getCaption(354)%></h3>
                    <p>
                        <asp:LinkButton ID="lnkFindAgain" runat="server" CssClass="bookagain"></asp:LinkButton>
                    </p>
                    <p>
                        <a href="javascript:cancelBook('b_cancel.aspx?userType=<%=CInt(Me.bookData.bookOperator.tipoOperador)%>&<%=Me.myQuery%>')" class="cancelbooklabel"><%=cfr.Language.GetCaption(353)%></a>
                    </p>

                </div>
            </div>
        </div>
    </div>

    







        
</asp:Content>
