<%@ Page Language="VB" MasterPageFile="~/CFRMasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" Title="WBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
    <style>
        .wb-home { max-width: 920px; margin: 90px auto 40px; padding: 0 20px; }
        .wb-home h1 { margin-bottom: 18px; }
        .wb-home-actions { display: flex; flex-wrap: wrap; gap: 12px; margin-top: 24px; }
    </style>
</asp:Content>

<asp:Content ID="HeadScript1" ContentPlaceHolderID="HeadScript" runat="Server" />

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <main class="wb-home">
        <h1>WBooking</h1>
        <p class="lead">An open-source booking engine and hotel administration application.</p>
        <p>
            Start with the integration example to test the public booking flow, or open the
            administration area to configure the demo hotel.
        </p>
        <div class="wb-home-actions">
            <a class="btn btn-primary btn-lg" href="examples/booking-widget.html">Booking example</a>
            <a class="btn btn-default btn-lg" href="Admin/">Hotel administration</a>
        </div>
    </main>
</asp:Content>
