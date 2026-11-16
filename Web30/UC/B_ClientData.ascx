<%@ Control Language="VB" AutoEventWireup="false" CodeFile="B_ClientData.ascx.vb" Inherits="UC_B_ClientData" %>
<%@ Register Src="B_Paid.ascx" TagName="B_Paid" TagPrefix="uc1" %>

<asp:Panel ClientIDMode="static"  ID="pnlBookClientData" runat="server" cssclass="row">
    
<div class="form-horizontal">
    <fieldset class="book" >
	<legend class ="book">
        <asp:Literal   ID="lblClientLegend" runat="server" Text="Datos de cliente"></asp:Literal>
	</legend>
	<div class="wb-form-hint">
	 <asp:Label ID="lblClientDataMandatory" runat="server" Text="Los campos con * son obligatorios"></asp:Label>
    </div>
    
        <div class="form-group">
            <label for="clientname" class="col-sm-2 control-label">*&nbsp;<asp:Literal ID="lblClientName" runat="server" Text=""></asp:Literal></label>
            <div class="col-sm-6">
                <asp:TextBox ClientIDMode="static" type="text" ID="clientname" runat="server" Columns="15" MaxLength="50" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requiredName" runat="server" ControlToValidate="clientname"
                    ErrorMessage="RequiredFieldValidator" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regExpName" runat="server" ControlToValidate="clientname"
                    Display="Dynamic" ErrorMessage="RegularExpressionValidator" ValidationExpression="[\w\ ]{3,50}" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RegularExpressionValidator>

            </div>
        </div>
        <div class="form-group">
                <label for="clientsurname" class="col-sm-2 control-label">*&nbsp;<asp:Literal ID="lblSurname1" runat="server" Text=""></asp:Literal></label>
                <div class="col-sm-6">
                    <asp:TextBox ClientIDMode="static" type="text"  ID="clientsurname" columns=30 MAXLENGTH=100 runat="server" CssClass="form-control" ></asp:TextBox>
	        <asp:RequiredFieldValidator ID="requiredSurname" runat="server" ControlToValidate="clientsurname"
            ErrorMessage="RequiredFieldValidator" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RequiredFieldValidator>
                </div>
            </div>
        <div class="form-group">
                <label for="clientphone" class="col-sm-2 control-label">*&nbsp;<asp:Literal ID="lblTlf" runat="server" Text=""></asp:Literal></label>
                <div class="col-sm-6">
                    <div class="input-group">
                        <asp:TextBox  ClientIDMode="static" type="tel" ID="clientphone" columns=15 MAXLENGTH=20 onkeypress="return solonums2(event);"  runat="server" CssClass="form-control" ></asp:TextBox>
                        <span class="input-group-addon"><i class="fa fa-phone fa-lg text-white"></i></span>
                    </div>

  
                    	
        <asp:RegularExpressionValidator ID="regexptlf" runat="server" ErrorMessage="RegularExpressionValidator"
            ValidationExpression="\d{9,}" ControlToValidate="clientphone" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="reqTlf" runat="server" ControlToValidate="clientphone" ErrorMessage="RequiredFieldValidator" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RequiredFieldValidator>
                </div>
            </div>
        
      <%If Me.bookData.hotel.Tipo =CFRConst.unitTypeEnum.car    %>
        <div class="form-group">
                <label for="cboclientbirth" class="col-sm-2 control-label">*&nbsp;<asp:Literal ID="lblclientbirth" runat="server"></asp:Literal></label>
                <div class="col-sm-6">
                    <asp:DropDownList ID="cboclientbirth" runat="server" ClientIDMode="static" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
      <%End If%>
        

            <asp:PlaceHolder ID="pnlClientDataAll" runat="server">
                <div class="form-group">
                    <label for="clientemail" class="col-sm-2 control-label">*&nbsp;<asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></label>
                    <div class="col-sm-6">
                        <div class="input-group">
                                  <asp:TextBox ClientIDMode="static"  type="email" ID="clientemail" Columns="30" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>
                                  <span class="input-group-addon"><i class="fa fa-envelope fa-lg text-white"></i></span>
                       </div>                      
                        <asp:RegularExpressionValidator ID="regExpEmail" runat="server" ControlToValidate="clientemail"
                            ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ValidationGroup="clientValidationGroup"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="ReqEmail" runat="server" ControlToValidate="clientemail" CssClass="required"
                            Display="Dynamic" ErrorMessage="RequiredFieldValidator" ValidationGroup="clientValidationGroup"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="clientcheckemail" class="col-sm-2 control-label" ></label>
                    <div class="col-sm-6">
                        <span>*<%=cfr.language.getCaption(122)%></span>
                        <asp:TextBox ClientIDMode="static" type="email" ID="clientcheckemail" Columns="30" MaxLength="100" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CompareValidator ID="compEmail" runat="server" ControlToCompare="clientcheckemail" ControlToValidate="clientemail" CssClass="required"
                            ErrorMessage="CompareValidator" Operator="Equal" Display="Dynamic" EnableClientScript="true" ValidationGroup="clientValidationGroup"></asp:CompareValidator>
                    </div>
                </div>
            </asp:PlaceHolder>

        <div class="form-group">
            <label for="" class="col-sm-2 control-label">
                <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label></label>
            <div class="col-sm-6">
                <div class="input-group">
                    <asp:DropDownList ClientIDMode="static" ID="cboCountry" runat="server" CssClass="form-control">
                    </asp:DropDownList>                    
                    <span class="input-group-addon"><i class="fa fa-map-marker fa-lg text-white"></i></span>
                </div>

                <asp:CompareValidator ID="reqCountry" runat="server" Display="Dynamic" ErrorMessage="CompareValidator" ValidationGroup="clientValidationGroup" ControlToValidate="cboCountry" Operator="GreaterThan" ValueToCompare="-1" Type="Integer" CssClass ="required"></asp:CompareValidator>
            </div>
        </div>
        
</fieldset>
<asp:PlaceHolder ID="pnlCompanyData" runat="server" Visible="false">
    <fieldset class="book" >
	<legend class ="book">
		<asp:Label ID="lblCompanyTitle" runat="server" Text="Nombre de empresa"></asp:Label>
	</legend>

    <div class="form-group">
        <label for="" class="col-sm-2 control-label">*&nbsp;<asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label></label>
        <div class="col-sm-6"><asp:TextBox ClientIDMode="static"  ID="empresa" columns=20  MAXLENGTH="50"  runat="server" CssClass="res" ></asp:TextBox>
	        <asp:RequiredFieldValidator ID="reqEmpresa" runat="server" ControlToValidate="empresa" ErrorMessage="RequiredFieldValidator" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="form-group">
        <label for="" class="col-sm-2 control-label">*&nbsp;<asp:Label ID="lblCompanyAddress" runat="server" Text=""></asp:Label></label>
        <div class="col-sm-6"><asp:TextBox ClientIDMode="static"  ID="empresadireccion" columns=25 MAXLENGTH="50"  runat="server" CssClass="res" ></asp:TextBox>
	        <asp:RequiredFieldValidator ID="ReqEmpresaDireccion" runat="server" ControlToValidate="empresadireccion" ErrorMessage="RequiredFieldValidator" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RequiredFieldValidator>
        </div>
    </div>   
    <div class="form-group">
        <label for="" class="col-sm-2 control-label">*&nbsp;<asp:Label ID="lblCompanyTlf" runat="server" Text=""></asp:Label></label>
        <div class="col-sm-6"><asp:TextBox ID="empresaTlf" columns=15 MAXLENGTH="20"   runat="server" CssClass="res" ></asp:TextBox>
	         <asp:RegularExpressionValidator ID="regEmpresaTlf" runat="server" ErrorMessage="RegularExpressionValidator"
            ValidationExpression="\d{9,}" ControlToValidate="empresaTlf" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="reqEmpresaTlf" runat="server" ControlToValidate="empresaTlf" ErrorMessage="RequiredFieldValidator" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="form-group">
        <label for="" class="col-sm-2 control-label">*&nbsp;<asp:Label ID="lblCompanyEmail" runat="server" Text=""></asp:Label></label>
        <div class="col-sm-6"><asp:TextBox ClientIDMode="static"  ID="empresaemail" columns=25 MAXLENGTH="100"  runat="server" CssClass="res" ></asp:TextBox>
	        <asp:RegularExpressionValidator ID="regCompanyEmail" runat="server" ControlToValidate="empresaemail"
            ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display=Dynamic ValidationGroup="clientValidationGroup" ></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="ReqCompanyEmail" runat="server" ControlToValidate="empresaemail"
                    CssClass="required" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ValidationGroup="clientValidationGroup"></asp:RequiredFieldValidator>
        </div>
    </div>    
 </fieldset>
</asp:PlaceHolder>
    
    
    <asp:PlaceHolder ID="pnlAgencyData" runat="server" Visible="false">
<fieldset class="book" >
	<legend class ="book">
		<asp:Label ID="lblAgencyTitle" runat="server" Text="Nombre de Agencia"></asp:Label>
	</legend>
        <div class="form-group">
            <label for="" class="col-sm-2 control-label">*&nbsp;<asp:Label ID="lblAgencyName" runat="server" Text="Agencia"></asp:Label></label>
            <div class="col-sm-6"><asp:TextBox ClientIDMode="static"  ID="Agency" columns=20  MAXLENGTH="50"  runat="server" CssClass="res" ></asp:TextBox>
		        <asp:RequiredFieldValidator ID="reqAgency" runat="server" ControlToValidate="Agency" ErrorMessage="RequiredFieldValidator" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <label for="" class="col-sm-2 control-label">*&nbsp;<asp:Label ID="lblAgencyTravelAgent" runat="server" Text="Agente de viajes"></asp:Label></label>
            <div class="col-sm-6"><asp:TextBox ClientIDMode="static"  ID="AgencyTravelAgent" columns=30 MAXLENGTH="50"   runat="server" CssClass="res" ></asp:TextBox>
		 <asp:RequiredFieldValidator ID="reqAgencyTravelAgent" runat="server" ControlToValidate="AgencyTravelAgent" ErrorMessage="RequiredFieldValidator" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <label for="" class="col-sm-2 control-label">*&nbsp;<asp:Label ID="lblAgencyAddress" runat="server" Text="Direccion"></asp:Label></label>
            <div class="col-sm-6"><asp:TextBox ClientIDMode="static"  ID="Agencydireccion" columns=30 MAXLENGTH="150"  runat="server" CssClass="res" ></asp:TextBox>
		<asp:RequiredFieldValidator ID="ReqAgencyDireccion" runat="server" ControlToValidate="Agencydireccion" ErrorMessage="RequiredFieldValidator" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <label for="" class="col-sm-2 control-label">*&nbsp;<asp:Label ID="lblAgencyCity" runat="server" Text="Ciudad"></asp:Label></label>
            <div class="col-sm-6"><asp:TextBox ClientIDMode="static"  ID="AgencyCity" columns=25 MAXLENGTH="50"   runat="server" CssClass="res" ></asp:TextBox>
		 <asp:RequiredFieldValidator ID="reqAgencyCity" runat="server" ControlToValidate="AgencyCity" ErrorMessage="RequiredFieldValidator" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <label for="" class="col-sm-2 control-label">*&nbsp;<asp:Label ID="lblAgencyCountry" runat="server" Text="Pais"></asp:Label></label>
            <div class="col-sm-6"><asp:TextBox ClientIDMode="static"  ID="AgencyCountry" columns=15 MAXLENGTH="20"   runat="server" CssClass="res" ></asp:TextBox>
		 <asp:RequiredFieldValidator ID="reqAgencyCountry" runat="server" ControlToValidate="AgencyCountry" ErrorMessage="RequiredFieldValidator" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RequiredFieldValidator>
            </div>
        </div>
            <div class="form-group">
            <label for="" class="col-sm-2 control-label">*&nbsp;<asp:Label ID="lblAgencyTlf" runat="server" Text="Telefono"></asp:Label></label>
            <div class="col-sm-6"><asp:TextBox ClientIDMode="static"  ID="AgencyTlf" columns=15 MAXLENGTH="20"   runat="server" CssClass="res" ></asp:TextBox>
		 <asp:RegularExpressionValidator ID="regAgencyTlf" runat="server" ErrorMessage="RegularExpressionValidator"
		ValidationExpression="\d{9,}" ControlToValidate="AgencyTlf" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RegularExpressionValidator>
	<asp:RequiredFieldValidator ID="reqAgencyTlf" runat="server" ControlToValidate="AgencyTlf" ErrorMessage="RequiredFieldValidator" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RequiredFieldValidator>
            </div>
        </div>
            <div class="form-group">
            <label for="" class="col-sm-2 control-label">*&nbsp;<asp:Label ID="lblAgencyFax" runat="server" Text="Fax"></asp:Label></label>
            <div class="col-sm-6"><asp:TextBox ClientIDMode="static"  ID="AgencyFax" columns=15 MAXLENGTH="20"   runat="server" CssClass="res" ></asp:TextBox>
		 <asp:RegularExpressionValidator ID="regAgencyFax" runat="server" ErrorMessage="RegularExpressionValidator"
		ValidationExpression="\d{9,}" ControlToValidate="AgencyFax" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RegularExpressionValidator>
	<asp:RequiredFieldValidator ID="reqAgencyFax" runat="server" ControlToValidate="AgencyFax" ErrorMessage="RequiredFieldValidator" Display="Dynamic" ValidationGroup="clientValidationGroup" CssClass="required"></asp:RequiredFieldValidator>
            </div>
        </div>
            <div class="form-group">
            <label for="" class="col-sm-2 control-label">*&nbsp;<asp:Label ID="lblAgencyEmail" runat="server" ></asp:Label></label>
            <div class="col-sm-6"><asp:TextBox ClientIDMode="static"  ID="Agencyemail" columns=25 MAXLENGTH="100"  runat="server" CssClass="res" ></asp:TextBox>
		<asp:RegularExpressionValidator ID="regAgencyEmail" runat="server" ControlToValidate="Agencyemail"
		ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display=Dynamic ValidationGroup="clientValidationGroup" ></asp:RegularExpressionValidator>
		<asp:RequiredFieldValidator ID="ReqAgencyEmail" runat="server" ControlToValidate="Agencyemail"
				CssClass="required" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ValidationGroup="clientValidationGroup"></asp:RequiredFieldValidator>
            </div>
        </div>
            <div class="form-group">
            <label for="" class="col-sm-2 control-label"></label>
            <div class="col-sm-6">
            </div>
        </div>

 </fieldset>

</asp:PlaceHolder>

</div> 

<% ' COMENTARIOS%>

    <fieldset class="book">
        <legend class="book"><%=cfr.language.getCaption(139)%></legend>
        
        <div class="form-group">
            <asp:TextBox ClientIDMode="static" ID="comentarios" runat="server" Rows="2" Columns="50" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
        </div>

    </fieldset>


    <asp:PlaceHolder ID="pnlpolitica" runat="server" Visible=false >
    <fieldset class="book" >
	    <legend class ="book"><%=cfr.language.getCaption(140)%></legend>
	       <div id="policiesfieldset">
            <asp:Literal ID="litBookCondition" runat="server" Visible="false"></asp:Literal>
            <asp:Literal ID="litpoliticaCancel" runat="server" Visible="false"></asp:Literal>
            <div id="tariffcancelconditionsforfieldset"></div>
          </div>
    </fieldset>
 </asp:PlaceHolder>

<asp:PlaceHolder ID="pnlPaidType" runat="server" Visible ="false"  >
                <uc1:B_Paid ClientIDMode="static"  ID="B_Paid1" runat="server" />
</asp:PlaceHolder>


</asp:Panel>
       
