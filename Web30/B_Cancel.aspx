<%@ Page Language="VB" MasterPageFile="~/blank.master" AutoEventWireup="false" CodeFile="B_Cancel.aspx.vb" Inherits="B_Cancel" %>
<%@ Import Namespace="cfr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="divcancelbodybg">
        <h3 class="divCancelBook">Cancel booking</h3>
        <div class="row">
            <div class="hidden-xs col-sm-3"></div>
            <div class="col-xs-12 col-sm-6">
                <div class="divcancelbody">
                    <p>Enter the booking number and the single-use cancellation code from the confirmation email.</p>
                    <div class="form-group">
                        <label for="Localizador">Booking number</label>
                        <asp:TextBox ID="Localizador" runat="server" MaxLength="12" Columns="20" CssClass="form-control"></asp:TextBox>
                        <div class="validatorbox">
                            <asp:RequiredFieldValidator ID="ReqLocalizator" runat="server" ControlToValidate="Localizador"
                                Display="Dynamic" ErrorMessage="Required" ValidationGroup="cancelValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="CancellationCode">Cancellation code</label>
                        <asp:TextBox ID="CancellationCode" runat="server" Columns="46" MaxLength="43" CssClass="form-control"
                            autocomplete="off" autocapitalize="none" spellcheck="false"></asp:TextBox>
                        <div class="validatorbox">
                            <asp:RequiredFieldValidator ID="ReqCancellationCode" runat="server" ControlToValidate="CancellationCode"
                                Display="Dynamic" ErrorMessage="Required" ValidationGroup="cancelValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="text-right">
                        <asp:Button ID="btnCancelBook" runat="server" Text="Cancel booking" CssClass="btn btn-danger"
                            ValidationGroup="cancelValidation" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Literal ID="litPowered" runat="server"></asp:Literal>
</asp:Content>
