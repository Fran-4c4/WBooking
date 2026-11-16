<%@ Page Title="CFR-Informe Tarifas" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="rep_rates.aspx.vb" Inherits="Admin_rep_rates" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
    
        <div class="container">
            <h1 class="sectiontitle"><i class="fa fa-table"></i> Informe de tarifas</h1>
            <div class="separator-2"></div>
        </div>

    <div class="container">
        <div class="form-stacked row">
                        <div class="form-group col-xs-12 col-sm-5">
                            <label class="control-label" for="lsttariffs">Seleccione la tarifa</label>
                            <div class="input-group ">
                                <div class="input-group-addon">
                                    <span class="fa fa-euro"></span>
                                </div>
                                <asp:DropDownList ID="lsttariffs" runat="server" CssClass="form-control" AutoPostBack="True" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-btn col-xs-4 col-sm-2">
                            <asp:LinkButton ID="btnviewreport" runat="server" class="btn btn-default btn-block" role="button">Ver</asp:LinkButton>
                        </div>
        </div>
        
        </div>
    
     <div class="container">
    
    Fecha del Informe: <%=Date.Now.ToLongDateString%>
    <TABLE class="table table-striped table-bordered table-condensed  ">
            <thead>                                                               
                <tr>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th colspan="2" class="rprices-strip-1">Suplemento
                       <%-- <%If minimumdays = 0 Then%>
                        :<small>No aplica suplemento porque no tiene minimo de dias.</small>
                        <%End If%>--%>
                    </th>
                    <%--<th></th>--%>
                    <asp:PlaceHolder ID="plhhotelboardth" runat="server" Visible="true">
                    <th colspan="4" ><i class="fa fa-cutlery"></i> Precio Regimen Adultos</th>
                    <%-- <th></th>
                    <th></th>
                    <th></th>  --%>     
                    <th colspan="4" class="rprices-strip-1"><i class="fa fa-cutlery"></i> Precio Regimen Niños</th> 
                    <th colspan="2">Descuento adultos extra</th>
                    <%--<th></th>--%>                                                                               
                        <%--Discount child extra--%>
                    <th colspan="2" class="rprices-strip-1">Descuento Niños extra</th>
                    <%--<th></th>--%>
                        </asp:PlaceHolder>                                                                                                       
                </tr>
                 
                <tr>

                    <tr >
                    <th >Temporada</th>
                    <th >Periodo</th>
                    <th >Habitación</th>
                    <th>Precio base €</th>
                    <th class="rprices-strip-1 minday"><i class="fa fa-info-circle"></i> Dias minimos</th>
                    <th class="rprices-strip-1">Fin de semana</th>
                    <asp:PlaceHolder ID="plhhotel_board" runat="server" Visible="true">
                    <th>Desayuno</th>
                    <th>Media pension</th>
                    <th>Pension completa</th>
                    <th>Todo incluido</th>
                    <th class="rprices-strip-1">Desayuno</th>
                    <th class="rprices-strip-1">Media pension</th>
                    <th class="rprices-strip-1">Pension completa</th>
                    <th class="rprices-strip-1">Todo incluido</th>
                    <%--Discount Pax extra--%>
                    <th>Pax-1 extra</th>
                    <th>Pax-2 extra</th>                                                                               
                    <%--Discount child extra--%>
                    <th class="rprices-strip-1">Niño-1 extra</th>
                    <th class="rprices-strip-1">Niño-2 extra</th>
                    </asp:PlaceHolder>

                </tr>

          </thead>   
        <tbody>
    <%
        Dim strtemporada As String = ""
        Dim tempactual As String = ""
        Dim fecper1 As String = ""
        Dim fecper2 As String = ""
        Dim strfecha As String = ""
        
        For Each ors As DataRowView In Me.dvrep
            If fecper1 <> Trim(CStr(ors("fech1"))) Or fecper2 <> Trim(CStr(ors("fech2"))) Then
                strfecha = ors("fech1") & "<BR>" & ors("fech2")
                fecper1 = ors("fech1")
                fecper2 = ors("fech2")
            Else
                strfecha = Chr(187)
            End If
   
            If tempactual <> Trim(CStr(ors("temporada"))) Then
                strtemporada = Trim(CStr(ors("temporada")))
                tempactual = ors("temporada")
                strfecha = ors("fech1") & "<BR>" & ors("fech2")
            Else
                strtemporada = ""
            End If
            
            'Dim DescAdultosExtra1 As Double = ors("DescAdultosExtra1")
            'Dim DescAdultosExtra2 As Double = ors("DescAdultosExtra2")
            'If DescAdultosExtra1 = 0 Then
            '    'check percentage
                
            'End If
            Dim priceinfo As New ind.bk.reports.pricesInfoObject(ors)
            priceinfo.configurePriceForUI(ors)
            
            %>

            <tr>
                <td class="txtn"><%=strtemporada%></td>
                <td class="txtn"><%=strfecha%></td>
                <td class="txtn"><%=ors("habitacionhotel")%>
                   <%-- <br />
                    <small><%=priceinfo.priceinfo%></small>--%>
                </td>
                <td><% If ors("precio") <> 0 Then Response.Write(ors("precio"))%></td>
                <td><%=ors(ind.bk.obj.R_Tarifa_habitacion.DBCol.SupDiasMinimos).ToString%></td>
                <td><%=ors(ind.bk.obj.R_Tarifa_habitacion.DBCol.SupFinDeSemana).ToString%></td>


                <%If Me.hotel.Tipo = 1 Then%>
                <%--Adults board--%>

                <td><% If ors("precioad") <> 0 Then Response.Write(ors("precioad"))%></td>
                <td><% If ors("preciomp") <> 0 Then Response.Write(ors("preciomp"))%></td>
                <td><% If ors("preciopc") <> 0 Then Response.Write(ors("preciopc"))%></td>
                <td><% If ors("precioTI") <> 0 Then Response.Write(ors("precioTI"))%></td>
                <%--Child board--%>
                <td><% If ors("desayunonino") <> 0 Then Response.Write(ors("desayunonino"))%></td>
                <td><% If ors("mpnino") <> 0 Then Response.Write(ors("mpnino"))%></td>
                <td><% If ors("pcnino") <> 0 Then Response.Write(ors("pcnino"))%></td>
                <td><% If ors("TodoIncluidoNino") <> 0 Then Response.Write(ors("TodoIncluidoNino"))%></td>
                

                <td><%= priceinfo.UI_DiscountPax1%></td>
                <td><%= priceinfo.UI_DiscountPax2%></td>
                <td><%= priceinfo.UI_DiscountChild1%></td>
                <td><%= priceinfo.UI_DiscountChild2%></td>

                <%End If%>
            </tr>
            
            
            
            
            
            
            
            
            
            
            
            

        <%Next%>
        </tbody>   
    
    </TABLE>
             </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
</asp:Content>

