<%@ Page Title="CFR-Unidades" Language="VB" MasterPageFile="MPAdmin1.master" AutoEventWireup="false" CodeFile="RoomList.aspx.vb" Inherits="RoomList" Debug="true"%>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../vendor/jquery-1.11.1/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadScript" Runat="Server">
    <style>



    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" Runat="Server">
    
     <div class="container">
         <div class="row">
             <div class="col-sm-8">
                 <h1 class="sectiontitle"><i class="glyphicon glyphicon-home"></i> Tipos de unidades a la venta</h1>
                 <p><%=Me.unitnameDesc%></p>
             </div>
             <div class="col-sm-4">
                 <div class="text-right"><a class="btn btn-animated btn-primary btn-lg" href="<%= linkcnf("roomedit.aspx?roomid=0")%>">Crear nueva unidad<i class="glyphicon glyphicon-plus-sign"></i></a></div>
             </div>
         </div>



         </div>
        <div class="container">  
        <div class="separator-2"></div>
        <% 'Loop configuration
            Dim dvList As DataView = Me.getDvList
        %>
        <% 'HEADER TEMPLATE %>
         
        <% 'what to do if no rows?
            If dvList.Count = 0 Then
            %>    
            <div class="alert  alert-icon alert-info" role="alert">
						<i class="fa fa-info-circle"></i>
						No se han encontrado registros.
			</div>
            <%  
                           
            Else
                'LOOP OVER ROWS 
                For Each drvList As DataRowView In dvList
                    'CONFIG VALUES NEEDED FOR PRESENTATION
                    Dim roomid As String = drvList(ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.codhabitacionhotel)
                    Dim roomidCrypt As String = UIHelper.URLencryptData(drvList(ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.codhabitacionhotel))
                    Dim childIncludedInCapacity As String = "No"
                    Dim mavl As String = drvList(ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.NinoOcupaplaza)
                    If mavl = 1 Then
                        childIncludedInCapacity = "Si"
                    End If
                    Dim combinedRoom As String = "No"
                    If drvList(ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.cod_hab_combinada) > 0 Then
                        combinedRoom = CStr(drvList(ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.HabitacionCombinadaNombre))
                    End If

                   Dim photoURL As String = Me.getImgHtmlForRoomViewCaroussel(roomid)
                   
                    'START ITEMTEMPLATE +++++++++++++++++++++++++++++++++
                    %>

                    <div class="row">
				<!-- main start -->
				<!-- ================ -->
				<div class="main col-md-12">
					<!-- ================ -->
	
					<!-- page-title end -->
					<div class="image-box team-member style-3-b">
						<div class="row">
							<div class="col-sm-3 col-md-4 col-lg-3">
                                <%=photoURL%>
							</div>
							<div class="col-sm-9 col-md-8 col-lg-9">
								<div class="body">
                                    <div class="title-block-2">
                                        <h3 class="" ><span class="unit-code label label-default" data-toggle="tooltip" title="Código de unidad"><%= drvList(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.codhabitacionhotel)%></span> <%= drvList(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.MaxPersonas)%><i class="fa fa-male"></i> <%= drvList(ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel)%> </h3>
                                        <div class="edit-buttons">
                                            <a class="btn btn-animated btn-primary btn-sm" href="<%= linkcnf("roomedit.aspx?roomid=" & roomidCrypt)%>">Editar<i class="glyphicon glyphicon-pencil"></i></a>
                                             <button type="button" name="delete" value="<%=roomidCrypt%>" class="btn btn-danger btn-sm" data-toggle="modal" data-target="#modaldeleteItem" data-objname="<%=drvList(ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.habitacionhotel)%>" data-objid="<%=roomidCrypt%>"><i class="fa fa-trash"></i></button>
                                             
                                    
                                        </div>
                                    </div>
                                    <%If Me.hotel.Tipo = 1 Then%>
                                    <p>basada en generica <strong> <%= drvList(ind.bk.obj.T_HABITACIONES.DBCol.Descripción)%></strong></p>
                                    <%End If%>
                                    <ul>
                                        <%If Me.hotel.Tipo = 1 Then%>
                                        <li>
                                            <strong>Habitacion combinada?:</strong> <%=combinedRoom%>
                                        </li>
                                        <%End If%>
                                        <li>
                                            <strong>Numero maximo de personas: </strong><%= drvList(ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.MaxPersonas)%>
                                            <%If Me.hotel.Tipo = 1 Then%>
                                            <div class="small " style="display: inline;">&nbsp;<b>Admite? Adulto Extra 1:</b><%=UIHelper.cboYesNo(drvList(ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.AdultosExtra1))%>&nbsp;<b>Adulto Extra 2:</b>&nbsp;<%=UIHelper.cboYesNo(drvList(ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.AdultosExtra1))%></div>
                                            <%End If%>
                                        </li>
                                        <%If Me.hotel.Tipo = 1 Then%>
                                        <li>
                                            <strong>Numero personas incluidas en precio base:</strong> <%= drvList(ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.Adultos)%> <small>(Es el numero utilizado para los calculos en el precio por persona)</small>
                                        </li>
                                        <li>
                                            <strong>Niños pequeños ocupan plaza en computo? </strong><%=childIncludedInCapacity%>
                                        </li>
                                        <%End If%>
                                    </ul>
                                    <div class="panel-group collapse-style-2">
                                        <div class="panel panel-default">
                                            <div class="panel-heading" role="tab" id="headingDescriptions_<%=roomid%>">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" href="#descriptioncollapse_<%=roomid%>" aria-expanded="false" aria-controls="descriptioncollapse" class="collapsed">Descripcion en varios idiomas
                                                    </a>
                                                </h4>
                                            </div>
                                            <div id="descriptioncollapse_<%=roomid%>" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingDescriptions_<%=roomid%>" aria-expanded="false">
                                                <div class="panel-body">
                                                    <p class="small">Descripcion en español. Para ver el resto de idiomas pulse en editar.</p>
                                                    <div class="html-desc "><%= drvList(ind.bk.obj.T_HOTELES_HABITACIONES.DBCol.ServiciosEsp)%></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                   
								</div>
							</div>
									
						</div>
					</div>
							
				</div>
				<!-- main end -->


			</div>

                    <%'END ITEMTEMPLATE +++++++++++++++++++++++++++++++++
                            Next

                        End If 'END CONDITIONAL LOOP +++++++++++++++++++++++++++++++++

        %>

         <div class="modal" id="modaldeleteItem" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
             <div class="modal-dialog" role="document">
                 <div class="modal-content">
                     <div class="modal-header">
                         <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                         <h4 class="modal-title" id="myModalLabel">Borrar habitacion</h4>
                     </div>
                     <div class="modal-body">
                         Esta seguro de que quiere borrar la unidad seleccionada?
                                <h3 id="itemplacenameto" class="text-center"></h3>
                     </div>
                     <div class="modal-footer">
                         <button type="button" class="btn btn-primary" data-dismiss="modal">Cancelar</button>

                         <asp:Button ID="btnDeleteItem" CssClass="btn btn-danger" runat="server" Text="Borrar" UseSubmitBehavior="false" ClientIDMode="Static" />
                     </div>
                 </div>
             </div>
         </div>

         <asp:HiddenField ID="field_operation" runat="server" ClientIDMode="Static" Value="0" />
         <asp:HiddenField ID="field_itemobject" runat="server" ClientIDMode="Static" Value="0" /> 

         </div>  <%--container--%>
    
  </asp:Content>

<asp:Content ID="cntPlaceHolderForPageJavascriptAfterMainJs" ContentPlaceHolderID="PlaceHolderForPageJavascriptAfterMainJs" Runat="Server">
  
    <SCRIPT>
        

    </SCRIPT>
  
</asp:Content>
