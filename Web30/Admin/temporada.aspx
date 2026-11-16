<%@ Page Title="CFR-Temporada-Edición" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="temporada.aspx.vb" Inherits="Admin_temporada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
    <style>
        .cal-day {
                cursor: pointer;
        }
        .cal-day:hover {
                border: solid;
                border-color: red;
                border-width: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="breadcrumb-container">
				<div class="container">
					<ol class="breadcrumb">
						<li><i class="fa fa-home pr-10"></i><a href="index.html">Inicio</a></li>
                        <li><a href='<%= linkcnf("temporadalist.aspx")%>'>Temporadas</a> </li>
						<li class="active">Editar Temporada</li>
					</ol>
				</div>
			</div>
    <div class="container">
        <h1 class="sectiontitle">Editar temporada   </h1>
        <strong>Seleccione una temporada: </strong><asp:DropDownList ID="temporadaCbo" runat="server" AutoPostBack="True"></asp:DropDownList>
        <br />
        <div class="separator-2"></div>

        <h3 class="title margin-clear">
            <asp:HiddenField ID="objidhidden" runat="server" value="0"/>
            <small><asp:Label ID="objidLbl" runat="server" Text="00000"></asp:Label></small> - <asp:Label ID="objNameLbl" runat="server" text="Nueva temporada"></asp:Label>
            
        </h3>
        
        <div class="separator-2 mt-10"></div>

        <div class="container">
            <div class="row">
                <div class="col-sm-5">
                    <div >
                        <h3 class="sectionoption">1-Para crear un nuevo periodo seleccione el intervalo </h3>
                        <div class="input-daterange" id="datepicker" style="display: inline-block;">
                            <div class="form-group col-sm-5 padleftclear">
                                <label class="sflabel control-label" for="checkInDate">Fecha de inicio del periodo</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                    <asp:TextBox ClientIDMode="static" ID="checkindate" runat="server" CssClass="checkindate form-control sfinput dtpreadonly " MaxLength="10" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group col-sm-5">
                                <label for="checkOutDate" class="sflabel control-label">Hasta</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                    <asp:TextBox ClientIDMode="static" ID="checkoutdate" runat="server" CssClass="checkoutdate date-picker form-control sfinput dtpreadonly" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <h3 class="sectionoption">2-Seleccione para que dias es valida usando las opciones inferiores</h3>
                        
                        <div class="form-group">
                            <div class="radio">
                                <label>
                                    <asp:RadioButton ID="rdballdays" runat="server" ClientIDMode="Static" GroupName="periodtype" Checked="True" />El periodo aplica a todos los dias
                                </label>
                            </div>
                            <div class="radio">
                                <label>
                                    <asp:RadioButton ID="rdbweekendonly" runat="server" ClientIDMode="Static" GroupName="periodtype" />El periodo aplica solo a fines de semana(v,s,d)
                                </label>
                            </div>
                            <div class="radio">
                                <label>
                                    <asp:RadioButton ID="rdbselecteddays" runat="server" ClientIDMode="Static" GroupName="periodtype" />El periodo aplica solo entre los dias marcados
                                </label>
                                <div style="padding-left: 20px; padding-top: 15px;">
                                    <asp:DropDownList ID="firstdaycbo" runat="server"></asp:DropDownList>
                                     <strong> hasta </strong> 
                                    <asp:DropDownList ID="lastdaycbo" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                             <div class="separator-2 mt-10"></div>
                    <div class="form-group">
                        <div class="col-sm-offset-5 col-sm-5 ">
                            
                            <asp:LinkButton ID="btnsubmit" runat="server" CssClass="btn btn-default btn-lg btn-block">Crear periodo <i class="fa fa-plus"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div  class="row">
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
                <div class="col-sm-7">
                    <div class="row">
                        <div class="panel panel-primary">
                        <div class="panel-heading">
                            <span class="glyphicon glyphicon-list"></span> Calendario con las fechas de cada periodo
                        </div>
                            <div class="panel-body">
                                <div id="seasoncal_<%=objId%>">
                                    <%= Me.showCalendar(Me.objId)%>
                                </div>
                                </div>
                            </div> 
                        
                    </div>
                    <div class="row">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <span class="glyphicon glyphicon-list"></span> Lista de periodos creados para esta temporada
                                        </div>
                                        <div class="panel-body">
                                            <asp:UpdatePanel ID="UpdatePanel1periodos" runat="server">
                                                <ContentTemplate>
                                                    <asp:ListView ID="ListView1" runat="server">
                                                        <LayoutTemplate>
                                                            <ul class="list-group">
                                                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                                            </ul>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <li class="list-group-item">
                                                                <%# Eval(ind.bk.obj.R_Temporadas_Periodos.DBCol.fechainicio,"{0:dd/MM/yyyy}")%> a 
                                                                <%# Eval(ind.bk.obj.R_Temporadas_Periodos.DBCol.fechafin, "{0:dd/MM/yyyy}")%>
                                                                <div class="pull-right action-buttons">
                                                                    <asp:HiddenField ID="hdditem2delete" runat="server" Value="<%# Eval(ind.bk.obj.R_Temporadas_Periodos.DBCol.cod_periodo)%>" />
                                                                    <asp:LinkButton ID="lnkitem2delete" runat="server" CssClass="trash" CommandName="delete" CommandArgument="<%# Eval(ind.bk.obj.R_Temporadas_Periodos.DBCol.cod_periodo)%>"><span class="glyphicon glyphicon-trash"></span></asp:LinkButton>
                                                                </div>
                                                            </li>
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
    
</asp:Content>

