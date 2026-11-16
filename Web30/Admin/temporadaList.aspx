<%@ Page Title="CFR-Temporadas" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="temporadaList.aspx.vb" Inherits="Admin_temporadaList" %>

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
                 <h1 class="sectiontitle"><i class="fa fa-calendar-o"></i> Temporadas</h1>
                 <p>Agrupacion de fechas para aplicar las tarifas</p>
             </div>
             <div class="col-sm-4">
                 <div class="text-right">
                     <button type="button" name="newitem" value="<%=UIHelper.URLencryptData("0")%>" class="openedititem-newitemtemporada  btn btn-animated btn-primary btn-lg" data-toggle="modal" data-target="#modaledit" data-objname="Nueva temporada" data-objid="0">Crear nueva Temporada<i class="glyphicon glyphicon-plus-sign"></i></button>
                     
                 </div>
             </div>
         </div>
            <div class="separator-2"></div>
         </div>

        <div class="container">
            <div class="row">
                <div class="col-xs-12 col-sm-4">
                    <asp:UpdatePanel ID="UpdatePanel_temporadaslist" runat="server" UpdateMode="Conditional" >
                   <ContentTemplate>
                    <% 'Loop configuration
                        Dim dvList As DataView = Me.dvTemporadas
                    %>
                    <% 'HEADER TEMPLATE 
                        'what to do if no rows?
                        If dvList.Count = 0 Then
                    %>
                    <div class="alert  alert-icon alert-info" role="alert">
                        <i class="fa fa-info-circle"></i>
                        No se han encontrado registros.
                    </div>
                    <%  
                           
                    Else 'LOOP OVER ROWS 
                        Dim x_gpc As Integer = 0
                        For Each drvList As DataRowView In dvList
                            x_gpc += 1
                            'CONFIG VALUES NEEDED FOR PRESENTATION
                            Dim objid As String = drvList(ind.bk.obj.R_Temporadas.DBCol.cod_Temporada)
                            Dim objidCrypt As String = UIHelper.URLencryptData(objid)
                   
                            'START ITEMTEMPLATE +++++++++++++++++++++++++++++++++
                    %>


                    <div class="row">
                        <div class="col-sm-12">
                            <div class="" style="margin-bottom: 20px;">
                                <h3 class="title margin-clear seasonviewclick" data-gpc="<%=x_gpc%>"> <span class="divcolorseason gpc_<%=x_gpc%>"></span> <small><%= objid%></small> - <%= drvList(ind.bk.obj.R_Temporadas.DBCol.Temporada)%></h3>
                                <div class="separator-2 mt-10"></div>

                                <button type="button" name="delete_<%=objidCrypt%>" value="<%=objidCrypt%>" class="btn btn-animated btn-danger btn-sm" data-toggle="modal" data-target="#modaldeleteItem" data-objname="<%=drvList(ind.bk.obj.R_Temporadas.DBCol.Temporada)%>" data-objid="<%=objidCrypt%>">Borrar<i class="fa fa-times"></i></button>
                                <a class="btn btn-animated btn-primary btn-sm" href="<%= linkcnf("temporada.aspx?objid=" & objid)%>">Asignar fechas<i class="glyphicon glyphicon-pencil"></i></a>
                                <button type="button" name="editname_<%=objidCrypt%>" value="<%=objidCrypt%>" class="openedititem-changename  btn btn-animated btn-primary btn-sm" data-toggle="modal" data-target="#modaledit" data-objname="<%=drvList(ind.bk.obj.R_Temporadas.DBCol.Temporada)%>" data-objid="<%=objidCrypt%>">Cambiar nombre<i class="glyphicon glyphicon-pencil"></i></button>
                            </div>
                        </div>
                    </div>
                    <%'END ITEMTEMPLATE +++++++++++++++++++++++++++++++++
                    Next
                        
                End If 'END CONDITIONAL LOOP +++++++++++++++++++++++++++++++++
                    %>
                       </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                            </Triggers>

                    </asp:UpdatePanel>
                </div>

                <div class="col-xs-12 col-sm-8">
                    <div> <span class="mixseason" style="width: 20px;display: inline-block;height: 20px;"></span> Los dias marcados con un recuadro rojo tienen mas de una temporada asignada.</div>
                    <asp:Literal ID="litTemporadasContainer" runat="server"></asp:Literal>
                    <div class="clearfix"></div>
<h2><i class="fa fa-table "></i> Matriz de Tarifas asociadas a temporadas</h2>
<p>Puede asociar directamente las tarifas y las temporadas activas.</p>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>


    <table class="table table-bordered table-condensed">
        <thead>
        <tr>
            <th style="padding-left: 0px;    padding-right: 0px;    padding-bottom: 0px;">
                <span style="text-align:right;width: 100%;display: inline-block;">Temporadas<i class="fa fa-arrow-right"></i></span><br />
                <span style="text-align:left;width: 100%;display: inline-block; background-color:skyblue;padding:5px;"><i class="fa fa-arrow-down"></i>Tarifa</span> </th>
    <%
            Dim headerCount As Integer
            Dim dvTemporadas As DataView = Me.dvTemporadas
            headerCount = dvTemporadas.Count
            For Each drvList As DataRowView In dvTemporadas
                Dim objid As String = drvList(ind.bk.obj.R_Temporadas.DBCol.cod_Temporada)
                Dim objidCrypt As String = UIHelper.URLencryptData(objid)
            %>
             <th><%= drvList(ind.bk.obj.R_Temporadas.DBCol.Temporada) %>
             </th>
        <%Next
         %>
           
        </tr>
            
        </thead>
        <tbody>
            <%

                Dim dvMatTariffTemporadas As SortedList = Me.getTariffTempMatrix

                For Each tartemp As mattt In dvMatTariffTemporadas.Values
                    Dim objid As String = tartemp.cod_tarifa

                %>
                    <tr>
                    <td style="background-color:skyblue;"><%= tartemp.tarifa  %></td>
                    <% For each kv As mattt.tartempcls In tartemp.temporadasList

                            Dim id As String = tartemp.cod_tarifa & "_" & kv.cod_temporada
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
                                            </ContentTemplate>


                    </asp:UpdatePanel>
                </div>
            </div>


</div>  <%--container--%>

<div class="container">
    

</div>  <%--container--%>


        <div class="container">
        <div class="footerinfo">
         <h3>Información </h3>
        <ul>
            <li>Pulse sobre la tarifa para verla en el calendario en un color <span class="seasonviewinfo" style="width: 20px;display: inline-block;height: 20px;"></span>
            </li>
            <li><span class="mixseason" style="width: 20px;display: inline-block;height: 20px;"></span> Los dias marcados con un recuadro rojo tienen mas de una temporada asignada.</li>
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




