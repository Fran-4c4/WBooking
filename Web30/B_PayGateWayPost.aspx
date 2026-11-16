<%@ Page Language="VB" AutoEventWireup="false" CodeFile="B_PayGateWayPost.aspx.vb" Inherits="B_PayGateWayPost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Booking Pay Gateway</title>
    <link rel="stylesheet" type="text/css" href="css/reset.css"  >
    <link rel="stylesheet" type="text/css" href="css/box.css" >
    <link href="css/core.css" rel="stylesheet" type="text/css">    
    <asp:Literal id="StyleSheetLiteral" runat="Server" />
    <asp:literal id="StyleSheetLiteralBooking" runat="Server"></asp:literal>
</head>
<body>
    <form id="form1" runat="server">
    <div>

	<br /><br />
	
		<H2><%= STRING.Format (cfr.Language.getCaption(324),"<BR/>")   %></H2>
        <br />
        <br />	
        <asp:Image ID="Image1" runat="server"  ImageUrl="~/images/cards.png"/>
    </div>
    </form>
</body>
</html>
