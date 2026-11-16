<%@ Page Language="VB" AutoEventWireup="false" CodeFile="B_PayGateWayConfirmation.aspx.vb" Inherits="B_PayGateWayConfirmation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    INTERNAL BOOK CONFIRMATION
        <asp:Panel ID="Panel1" runat="server" Height="50px" Width="125px" Visible =false >
            <p>
                <asp:Label ID="lblErr" runat="server" Text="-----"></asp:Label>
            </p>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
