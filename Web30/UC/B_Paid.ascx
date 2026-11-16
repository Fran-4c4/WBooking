<%@ Control Language="VB" AutoEventWireup="false" CodeFile="B_Paid.ascx.vb" Inherits="UC_B_Paid" %>
<% If Me.bookOperator IsNot Nothing AndAlso Me.bookOperator.forma_pago = cfr.CFRConst.formaPago.Transfer Then %>
<fieldset class="book" >
	<legend class ="book">
		<asp:Label ID="lblPaidTypes" runat="server" Text="Formas de pago label" ></asp:Label>
	</legend>
    <div class="paidstyle">
            <h3><asp:Label ID="lblTransferOnly" runat="server" Text="Transferencia bancaria"></asp:Label></h3>
    </div>
</fieldset>
<% End If %>


