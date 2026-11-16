<%@ Page Title="CFR-Cierre de ventas" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="ym_close.aspx.vb" Inherits="Admin_ym_close" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
    <link href="css/bootstrapdatepicker/bootstrap-datepicker3.standalone.css" rel="stylesheet" media="screen">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div class="container">
        <h1 class="sectiontitle"><i class="fa fa-lock"></i> Cierre de ventas </h1>
       
        <div class="separator-2"></div>

        <div class="container">
            <div class="row">
                <div class="col-sm-5">
                    <div>
                        <h3 class="sectionoption">1-Para crear un nuevo periodo seleccione el intervalo </h3>
                        
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
                       


                        <div class="form-group col-xs-12 col-sm-10">
                            <label class="sflabel control-label" for="lstClients">Aplicar al operador</label>
                            <div class="input-group ">
                                <div class="input-group-addon">
                                    <span class="fa fa-users"></span>
                                </div>
                                <asp:DropDownList ID="cbooperatorsymclose" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group col-xs-12 col-sm-10">
                            <label class="sflabel control-label" for="lstrooms">Aplicar a las unidades</label>
                            <div class="input-group ">
                                <div class="input-group-addon">
                                    <span class="fa fa-asterisk"></span>
                                </div>
                                <asp:DropDownList ID="lstrooms" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <div class="col-sm-offset-4 col-sm-6 ">

                                <asp:LinkButton ID="btnsubmit" runat="server" CssClass="btn btn-default btn-lg btn-block">Crear cierre de ventas <i class="fa fa-plus"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:UpdatePanel ID="UpdatePanel1season" runat="server">
                                    <ContentTemplate>
                                        <asp:Literal ID="litmsgresult" runat="server"></asp:Literal>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnsubmit" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>

                </div>
                <div class="col-sm-7">
                    <div class="row">
                        <div class="panel panel-primary">
                        <div class="panel-heading">
                            <span class="glyphicon glyphicon-list"></span> Calendario con las fechas de cada periodo
                        </div>
                            <div class="panel-body">
                                <div id="seasoncal_<%=objId%>">
                                    <%= Me.showCalendar()%>
                                </div>
                                </div>
                            </div> 
                        
                    </div>
                    <div class="row">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <span class="glyphicon glyphicon-list"></span> Lista de cierre de ventas creados
                                        </div>
                                        <div class="panel-body">
                                            <asp:UpdatePanel ID="UpdatePanel1periodos" runat="server">
                                                <ContentTemplate>
                                                    <asp:ListView ID="ListView1" runat="server">
                                                        <LayoutTemplate>
                                                            <table class="table table-hover">
                                                                <thead>
							                                    <tr>
								                                    <th>Inicio</th>
								                                    <th>Hasta</th>
								                                    <th>Para el operador</th>
								                                    <th>Para la unidad</th>
                                                                    <th></th>
							                                    </tr>
						                                    </thead>
                                                                <tbody>
                                                                 <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                            </table>
                                                               
                                                            
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval(ind.bk.obj.R_Temporadas_Periodos.DBCol.fechainicio,"{0:dd/MM/yyyy}")%></td>
                                                                <td><%# Eval(ind.bk.obj.R_Temporadas_Periodos.DBCol.fechafin, "{0:dd/MM/yyyy}")%></td> 
                                                                <td><%# Eval("operador")%></td>
                                                                <td><%# Eval("unitdescription")%></td>
                                                                <td>
                                                                 <div class="pull-right action-buttons">
                                                                    <asp:HiddenField ID="hdditem2delete" runat="server" Value='<%# Eval("ID")%>' />
                                                                    <asp:LinkButton ID="lnkitem2delete" runat="server" CssClass="trash" CommandName="delete" CommandArgument='<%# Eval("ID")%>'><span class="glyphicon glyphicon-trash alert-danger"></span></asp:LinkButton>
                                                                </div>
                                                                </td>
                                                                 
                                                      
                                                            </tr>
                                                        </ItemTemplate>
                                                        <EmptyDataTemplate>
                                                            Debe crear los periodos.
                                                        </EmptyDataTemplate>
                                                    </asp:ListView>
                                                </ContentTemplate>

                                            </asp:UpdatePanel>

                                        </div>
                                        <div class="panel-footer">
                                            <div class="row">
                                                <div class="col-md-6">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    
                </div>
            </div>

            
        </div>

                   
       

        <div id="footerinfo" class="footerinfo">
        <h3>Información</h3>
            <div class="separator-2 mt-10"></div>
        <ul>
            <li>
                Los periodos se asignan a la temporada para saber en que fechas es aplicable una tarifa.
            </li>
            <li>
                Una temporada puede ser asignada a varias tarifas.
            </li>
            <li>
                El sistema permite temporadas solopadas, pero puede producir confusión cuando se apliquen las diferentes tarifas. Le aconsejamos no solapar temporadas.
            </li>
        </ul>
            </div>

    </div><!--end container -->
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
    <asp:Literal ID="calendarjsLit" runat="server"></asp:Literal>
       
    

</asp:Content>

