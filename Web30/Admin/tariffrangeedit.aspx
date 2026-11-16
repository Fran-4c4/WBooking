<%@ Page Language="VB" AutoEventWireup="false" CodeFile="tariffrangeedit.aspx.vb" Inherits="Admin_tariffrangeedit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="modal " id="modaleditunitavailability" tabindex="-1" role="dialog" data-backdrop="static">
        <div class="modal-dialog modal-md" style="min-width: 650px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Editar disponibilidad para la unidad seleccionada</h4>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hddmodaledittariffprice_id_Tar_Hab" runat="server" ClientIDMode="static" />
                    <div class="input-daterange" id="datepicker" style="display: inline-block;">
                        <div class="form-group col-sm-5 padleftclear">
                            <label class="sflabel control-label" for="checkInDate">Desde el dia</label>
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
                    <div class="form-group">
                        <h4 >Seleccione para que dias es valida usando las opciones inferiores</h4>
                        <div class="day-range form-options">
                            <div class="checkbox">
                            <label class="checkbox" for="weekday1">
                                <asp:CheckBox runat="server" id="weekday1" name="week_valid_days"  value="1" Checked="True"  ClientIDMode="Static" />
                                lunes
                            </label>
                            <label class="checkbox" for="weekday2">
                                <asp:CheckBox runat="server" id="weekday2" name="week_valid_days"  value="2" Checked="True"  ClientIDMode="Static" />
                                martes
                            </label>
                            <label class="checkbox" for="weekday3">
                                <asp:CheckBox runat="server" id="weekday3" name="week_valid_days"  value="3" Checked="True"  ClientIDMode="Static" />
                                miércoles
                            </label>
                            <label class="checkbox" for="weekday4">
                                <asp:CheckBox runat="server" id="weekday4" name="week_valid_days"  value="4" Checked="True"  ClientIDMode="Static" />
                                jueves
                            </label>
                            <label class="checkbox" for="weekday5">
                                <asp:CheckBox runat="server" id="weekday5" name="week_valid_days"  value="5" Checked="True"  ClientIDMode="Static" />
                                viernes
                            </label>
                            <label class="checkbox" for="weekday6">
                                <asp:CheckBox runat="server" id="weekday6" name="week_valid_days"  value="6" Checked="True"  ClientIDMode="Static"/>
                                sábado
                            </label>
                            <label class="checkbox" for="weekday7">
                                <asp:CheckBox runat="server" id="weekday7" name="week_valid_days"  value="7" Checked="True"  ClientIDMode="Static" />
                                domingo
                            </label>
                                 </div>
                        </div>

                    </div>
                    <div class="form-group">
                        <h4>Precio para los dias seleccionados</h4>
                        <asp:TextBox ID="txtmodaledittariffprice" runat="server" ClientIDMode="Static" CssClass="form-control onlynum" Width="120px">0</asp:TextBox>
                    </div>

                    <div id="editavailabilitycontainer"></div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <asp:LinkButton ID="btnmodaledittariffpricesave" runat="server" CssClass="btn btn-primary">Guardar datos <i class="fa fa-save"></i></asp:LinkButton>

                </div>
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
