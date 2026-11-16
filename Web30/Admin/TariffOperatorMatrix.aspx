<%@ Page Title="CFR-Asignar tarifas a operadores" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="TariffOperatorMatrix.aspx.vb" Inherits="Admin_tariffOperatorMatrix" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
    
    <style>
        .mixseason {
           background-color: #fff!important;
           border: 1px solid red!important;
        }

        .seasonview {
            background-color: #cc0099!important;
        }
        .seasonviewinfo {
            background-color: #cc0099;
        }

        .seasonviewclick {
            cursor: pointer; 
        }
        .seasonviewclick:hover {
            background-color: #d7dbe4;
            /*border-bottom: 2px solid black;*/
        }


        .divcolorseason {
            display: inline-block;
            width: 30px;
            height: 30px; 
            padding: 3px 7px;
            font-size: 12px;
            font-weight: 700;
            line-height: 1;
            color: #fff;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            border-radius: 15px;
        }

        td.chk-click-reltemp {
            text-align:center;
        }
        <asp:Literal id="litcsscal" runat="server"></asp:Literal>
      
         
        
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container">
           <div class="row">
             <div class="col-sm-8">
                 <h1 class="sectiontitle"><i class="fa fa-calendar-o"></i> Asociación de tarifas a operadores</h1>
                 <p>Matriz para asociar tarifas a operadores</p>
             </div>
             <div class="col-sm-4">

             </div>
         </div>
            <div class="separator-2"></div>
     </div>
    

        <div class="container">
            <div class="row">

    <table class="table table-bordered table-condensed">
        <thead>
        <tr>
            <th style="padding-left: 0px;    padding-right: 0px;    padding-bottom: 0px;">
                <span style="text-align:right;width: 100%;display: inline-block;">Operadores/Contratos<i class="fa fa-arrow-right"></i></span><br />
                <span style="text-align:left;width: 100%;display: inline-block; background-color:skyblue;padding:5px;"><i class="fa fa-arrow-down"></i>Tarifa</span> </th>
    <%
        Dim headerCount As Integer

        headerCount = Me.dvOperators.Count
        For Each drvList As DataRowView In Me.dvOperators
            Dim objid As String = drvList(ind.bk.obj.R_operadores.DBCol.cod_operador)
            Dim objidCrypt As String = UIHelper.URLencryptData(objid)
            %>
             <th><%= drvList(ind.bk.obj.R_operadores.DBCol.Operador) %>
             </th>
        <%Next
         %>
           
        </tr>
            
        </thead>
        <tbody>
            <%

                Dim dvMatTariffOperator As SortedList = Me.getTariffTempMatrix

                For Each tartemp As mattt In dvMatTariffOperator.Values
                    Dim objid As String = tartemp.cod_tarifa

                %>
                    <tr>
                    <td style="background-color:skyblue;"><%= tartemp.tarifa  %></td>
                    <% For each kv As mattt.tariffOperadorcls In tartemp.operadorList

                            Dim id As String = tartemp.cod_tarifa & "_" & kv.cod_operador
                            Dim checked As String = ""
                            If kv.value = 1 Then
                                checked = " checked "
                            End If
                            %>
                        <td class="chk-click-reltemp rowclick"><input name="chkreltartemp" class="chk-rel-temtar" id="chkreltartemp" type="checkbox" value="<%=id %>"  <%=checked %>/></td>
                        
            <%
                Next
                         %>
                        </tr>
            <%Next
                %>
        </tbody>

    </table>
                    <asp:Literal ID="litResRelationsSaved" runat="server"></asp:Literal>
                    <asp:LinkButton ID="btnsaverelations" runat="server" class="btn btn-animated btn-primary " >Guardar asociaciones <i class="fa  fa-save"></i></asp:LinkButton>
                                            
                </div>
            </div>


        <div class="container">
        <div class="footerinfo">
         <h3>Información </h3>
        <ul>
            <li>Seleccione para cada operador que tarifas podrá utilizar.
            </li>
        </ul>           
        </div>


         <asp:HiddenField ID="field_operation" runat="server" ClientIDMode="Static" Value="0" />
         <asp:HiddenField ID="field_itemobject" runat="server" ClientIDMode="Static" Value="0" />
         <asp:HiddenField ID="field_itemname" runat="server" ClientIDMode="Static" Value="" />

         </div>  <%--container--%>

        <div class="modal" id="modaldeleteItem" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="H1">Borrar Temporada</h4>
                    </div>
                    <div class="modal-body">
                        Esta seguro de que quiere borrar la temporada seleccionada?
                                <h3 id="itemplacenameto" class="text-center"></h3>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Cancelar</button>

                        <asp:Button ID="btnDeleteItem" CssClass="btn btn-danger" runat="server" Text="Borrar" UseSubmitBehavior="false" ClientIDMode="Static" />
                    </div>
                </div>
            </div>
        </div>
        


         <div class="modal" id="modaledit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
             <div class="modal-dialog">
                 <div class="modal-content">
                     <div class="modal-header">
                         <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                         <h4 class="modal-title" id="myModalLabel">Gestion de temporadas</h4>
                     </div>
                     <div class="modal-body">                              
                        <div id="objnewnametxtdiv" class="form-group">
                            <label for="objnewnametxt">Introduzca el nuevo nombre:</label>
                            <asp:TextBox ID="objnewnametxt" name="objnewnametxt" runat="server" ClientIDMode="Static" cssclass="form-control form-ctrol-admin" ></asp:TextBox>
                        </div>
                         <asp:UpdatePanel ID="UpdatePanel_litresmessageedit" runat="server">
                             <ContentTemplate>
                                  <asp:Literal ID="litresmessageedit" runat="server"></asp:Literal>
                             </ContentTemplate>
                            
                         </asp:UpdatePanel>
                         
                     </div>
                     <div class="modal-footer">
                         <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                         <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClientClick="return validateinput();"  ClientIDMode="Static">Crear Temporada <i class="fa fa-plus"></i></asp:LinkButton>
                         
                     </div>
                 </div>
             </div>
         </div>


</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
      <script lang="javascript">
      
          $(document).on("click", ".openedititem-changename", function (e) {
              var objname = $(this).data('objname');
              var objid = $(this).data('objid'); // Extract info from data-* attributes
              $(".modal-body #objnewnametxtdiv").show();
              $(".modal-body #objnewnametxt").val(objname);
              $(".modal-footer #btnSubmit").text("Cambiar nombre");
              $('#objnewnametxtdiv').removeClass('has-error', 'has-feedback');

           
              $("#field_itemobject").val(objid);
              $("#field_operation").val("changename");
              var pnlerror = $("#pnlerror");
              if (pnlerror != null) {
                  pnlerror.remove();
              }
          });

          $(document).on("click", ".openedititem-newitemtemporada", function (e) {
              var objname = $(this).data('objname');
              var objid = $(this).data('objid'); // Extract info from data-* attributes
              $(".modal-body #objnewnametxtdiv").show();
              $(".modal-body #objnewnametxt").val("");
              $(".modal-body #objnewnametxt").attr("placeholder", "Introduce aqui el nuevo nombre");
              $(".modal-footer #btnSubmit").text("Crear temporada");
              $(".modal-footer #btnSubmit").addClass("btn-primary");
              
              $("#field_itemobject").val(objid);
              $("#field_operation").val("newitem");
              var pnlerror = $("#pnlerror");
                  if (pnlerror!=null) {
                      pnlerror.remove();
                  }
          });

          function validateinput(e) {
              var actionas = $("#field_operation").val();
              var txt = $(".modal-body #objnewnametxt").val();
              if (txt == "") {
                  $('#objnewnametxtdiv').addClass('has-error','has-feedback');
                  
                  return false;
              }
                 
              $("#field_itemname").val(txt);
              return true;
          }

          var lastgpc = -1;
          $(document).on("click", ".seasonviewclick", function (e) {
              $(".seasonview").removeClass("seasonview");
              var gpcid= $(this).data('gpc');
              if (lastgpc == gpcid) {
                  lastgpc = -1;
                  return;
              }
              var gpcclass = "gpc_" + gpcid;
               
              $("." + gpcclass).addClass("seasonview");
              lastgpc = gpcid;

          });

          $(document).on("click", ".chk-click-reltemp", function (e) {
              var chkselected = $(this).find('.chk-rel-temtar');
              var checked = chkselected.prop("checked");
              chkselected.prop("checked", !checked);
          });

      </script>

 
    <link href="plugins/bootstrap-switch/bootstrap-switch.css" rel="stylesheet">
    <script src="plugins/bootstrap-switch/bootstrap-switch.js"></script>
    <script>
        $(document).ready(function () {
            //$(".chk-rel-temtar").bootstrapSwitch();
        });
      
    </script>
</asp:Content>




