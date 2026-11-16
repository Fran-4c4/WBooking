<%@ Control Language="VB" AutoEventWireup="false" CodeFile="assignrelationsmodal.ascx.vb" Inherits="Admin_adminUC_assignrelationsmodal" %>


        <div class="modal" id="modalasignrelations" tabindex="-1" role="dialog"  data-backdrop="static">
             <div class="modal-dialog" role="document">
                 <div class="modal-content">
                     <div class="modal-header">
                         <button type="button" class="close" data-dismiss="modal" ><span>&times;</span></button>
                         <h4 class="modal-title" id="myModalLabel">Asociar Operadores/Contratos a esta tarifa</h4>
                     </div>
                     <div class="modal-body">                              
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >           
                            <ContentTemplate>
                                <asp:Literal ID="litrelations" runat="server" ClientIDMode="static" ></asp:Literal>
                            </ContentTemplate>
                            <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="btnshowcustomerrelations" />--%>
                            
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
    <div id="kkdelavaca"></div>
                     </div>
                     <div class="modal-footer">
                         <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                         <asp:LinkButton ID="btnsavetariffrelations" runat="server" cssclass="btn btn-animated btn-primary">Guardar asociación<i class="fa fa-save"></i></asp:LinkButton>                        
                     </div>
                 </div>
             </div>
         </div>
<%--<script>
    $(document).ready(function () {
        $('#modalasignrelations').on('show.bs.modal', function (event) {
            $('#modalasignrelations').appendTo("form");
        });

        $(".showrelations").click(function () {
            var button = $(event.currentTarget); // Button that triggered the modal
            var par = button.parent();
            $("#field_itemobject").val(par.data('objid'));
        });
        function setnewrelations(objid, direldiv) {
            var direl = $('#' + direldiv + "_" + objid);
            $(direl).find('li').remove();
            var iconclass = "fa fa-user";

            $(".chk-rel").each(function (index) {
                var relationid = $(this).attr('id');
                var isChecked = $(this).is(':checked');

                if (isChecked) {
                    var label = $("label[for='" + relationid + "']").text();
                    direl.append("<li><i class='" + iconclass + "'></i> " + label + "</li>");
                }
            });
        }

    });

</script>--%>