<%@ Page Title="CFR-Clientes" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="true" CodeFile="Rep_Stat_requests.aspx.vb" Inherits="Admin_PromotionList" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Linq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
    <style>
        
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
     <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">           
            <ContentTemplate>
    
            </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSaveRelations" />
            </Triggers>
        </asp:UpdatePanel>--%>
 
     <div class="container">
        <h1 class="sectiontitle"><i class="fa fa-bar-chart"></i> Estadisticas de peticiones realizadas por fechas</h1>
        <div class="row">
             <div class="col-sm-8">
                 <p>Busquedas realizadas por los clientes en las fechas seleccionadas y conversión en reservas.</p>
                 </div>

            <div class="col-sm-4 pull-right">
                <div class="text-right">

                </div>
            </div>
        </div>
        <div class="separator-2"></div>
    </div>
    
<div class="container">
        <div class="" style="padding-bottom: 20px;" >
                <ul id="tabsearch" class="nav nav-tabs style-3" role="tablist">
                    <li role="presentation" class="active"><a href="#tabsearchbydates" role="tab" data-toggle="tab"  class="tab-col-5"><i class="fa fa-calendar-o"></i> Buscar por fechas</a></li>
                </ul>
                <div id="myTabContent" class="tab-content tab-fixed" style="height: 115px;">
                    <div role="tabpanel" class="tab-pane active" id="tabsearchbydates" >
                        <div class="form-stacked">
                            

                            <div class="input-daterange" id="datepicker" >
                            <div class="form-group col-xs-6 col-sm-4" style="width: 160px;">
                                <label class="sflabel control-label" for="checkInDate">Desde</label>
                                <div class="input-group input-group-sm">
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                    <asp:TextBox ClientIDMode="static" ID="checkindate" runat="server" CssClass="checkindate form-control sfinput dtpreadonly " MaxLength="10" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group col-xs-6 col-sm-4" style="width: 160px;">
                                <label for="checkOutDate" class="sflabel control-label">Hasta</label>
                                <div class="input-group input-group-sm">
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                    <asp:TextBox ClientIDMode="static" ID="checkoutdate" runat="server" CssClass="checkoutdate date-picker form-control sfinput dtpreadonly" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                                </div>

                            
                            <div class="form-btn col-xs-12 col-sm-4" style="">
                                <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-default btn-lg">Ver <i class="fa fa-search"></i></asp:LinkButton>

                            </div>
                        </div>

                    </div>
                    
                     
                    
                                   
                                    
                    </div>

                </div>
            </div>
    

<div class="container">
    <div class="row">
        <div class="col-xs-10">
            <div id="chart_div"></div>
        </div>
    </div>
    <div class="clearfix"></div>
	


    

</div><!-- container end -->
    

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
    <style>
        .wb-local-chart { max-height: 600px; overflow: auto; padding: 18px; border: 1px solid #d7dee8; background: #fff; }
        .wb-local-chart-title, .wb-local-chart-subtitle { display: block; }
        .wb-local-chart-title { color: #22344a; font-size: 18px; font-weight: 600; }
        .wb-local-chart-subtitle { margin: 4px 0 16px; color: #65758a; }
        .wb-local-chart-legend { display: flex; flex-wrap: wrap; gap: 18px; margin-bottom: 16px; }
        .wb-local-chart-legend span { border-left: 12px solid; padding-left: 6px; }
        .wb-local-chart-group { display: grid; grid-template-columns: 120px 1fr; gap: 4px 12px; margin-bottom: 12px; }
        .wb-local-chart-date { grid-row: 1 / span 3; align-self: center; font-weight: 600; }
        .wb-local-chart-series { position: relative; min-height: 20px; background: #eef2f7; }
        .wb-local-chart-bar { display: block; min-width: 2px; height: 20px; }
        .wb-local-chart-value { position: absolute; top: 1px; left: 8px; color: #172435; font-weight: 600; }
    </style>
    <script src="js/wbooking-chart.js"></script>
    <script>
        renderWBookingBarChart('chart_div', [
            ['Fecha', 'Busquedas', 'Reservas', 'Disponibilidad'], <%=Me.barchartbookdata%>
        ], {
            title: 'Busquedas x reservas x disponibilidad. La disponibilidad es la sobrante.',
            subtitle: '<%=Me.barchartsubtitle%>'
        });
    </script>
    
</asp:Content>

