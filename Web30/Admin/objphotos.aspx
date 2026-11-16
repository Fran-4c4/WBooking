<%@ Page Title="" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="objphotos.aspx.vb" Inherits="Admin_objphotos" %>

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
        <h2>Galeria de imagenes del hotel</h2>
        <p>Aqui podrá subir imagenes del hotel para luego ser usadas en la galeria del hotel.</p>
        <script>
            function showModalFotos(textbox) {

                $("#modal-editor").modal("show");
                $("#textboxcaller").val(textbox);
                

            }

            function showEditor(textbox) {

                $("#modal-editor").modal("show");
                $("#textboxcaller").val(textbox);
                $('#editor').val($(textbox + "_html").html());

            }
            function applychangesfromeditor(textbox) {
                var callertxt = $("#textboxcaller").val();
                var unsafehtml = $('#editor').val();
                var safehtml = encodeURIComponent(unsafehtml);
                $(callertxt + "id").val(safehtml);
                $(callertxt + "_html").html($('#editor').val());
                $("#modal-editor").modal("hide");

            }
            function savedesc() {
                $('#editdesc').val($('#editor').html());
            }


        </script>
        <div class="row">
            <%
                For x As Integer = 1 To 30
            %>
            <div class="col-lg-3 col-md-4 col-xs-6 thumb">
                <a class="thumbnail" href="#" onclick="showModalFotos('#<%=Me.codhotel%>_<%=x%>');">
                    <img class="img-responsive" src="../fotos/<%=Me.codhotel%>_<%=x%>.jpg" alt="">
                </a>
            </div>
            <%
            Next
            %>
        </div>
    </div>
    

        <div class="modal" id="modal-editor" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h3>Seleccione la imagen que desea subir</h3>
                </div>
                <div class="modal-body">
                    <asp:FileUpload ID="FileUpload1" runat="server"  />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-default" onclick="applychangesfromeditor()">Aplicar imagen</button>
                    <input type="hidden" id="textboxcaller">
                </div>
            </div>
        </div>
    </div>
    

</asp:Content>

