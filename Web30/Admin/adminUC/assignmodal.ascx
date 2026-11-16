<%@ Control Language="VB" AutoEventWireup="false" CodeFile="assignmodal.ascx.vb" Inherits="Admin_adminUC_assignmodal" %>


<div class="modal " id="modalasigncustomer" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalLabel">Asociar clientes a esta tarifa</h4>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Literal ID="litcustomers" runat="server" ClientIDMode="static"></asp:Literal>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnshocustomerrelations" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Literal ID="litmsgresult" runat="server"></asp:Literal>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnsavetariffrelations" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                <asp:LinkButton ID="btnsavetariffrelations" runat="server" CssClass="btn btn-animated btn-primary">Guardar asociación<i class="fa fa-save"></i></asp:LinkButton>
            </div>
        </div>
    </div>
</div>


