<%@ Control Language="VB" AutoEventWireup="true" CodeFile="deletemodal.ascx.vb" Inherits="Admin_adminUC_deletemodal" %>

   <!--UCX Delete Record Modal starts here -->
 <div class="modal " id="modaldeleteItem" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" >
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">
                        <asp:Literal ID="litmodaltitle" runat="server"></asp:Literal></h4>
                </div>
                <asp:UpdatePanel ID="deleteupadatepanelucx" runat="server">
                  <ContentTemplate>
                      <div class="modal-body">
                          <asp:HiddenField ID="itemdeletecode" runat="server" ClientIDMode="static"  />
                            <asp:Literal ID="litMessage" runat="server"></asp:Literal>
                                <h3 id="itemplacenameto" class="text-center"></h3>
                                <div id="resultmessageplace"></div>
                      </div>
                      <div class="modal-footer">
                          
                          <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                          <asp:LinkButton ID="btndeletemodal" runat="server" CssClass="btn btn-danger"><asp:Literal ID="litbtnprimarycaption" runat="server"></asp:Literal> <i class="fa fa-times"></i></asp:LinkButton>
                          
                      </div>
                      </ContentTemplate>
                      <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="btndeletemodal" EventName="Click" />
                      </Triggers>
                </asp:UpdatePanel>                
            </div>
        </div>
    </div>
<%--<script>
    $('#modaldeleteItemucx').on('show.bs.modal', function (event) {
        $('#modaldeleteItemucx').appendTo("form");
        $('#resultmessageplace').html('');
        //$('#btndelete').removeClass('hidden');
    });
</script>--%>
    <!--UCX Delete Record Modal Ends here -->