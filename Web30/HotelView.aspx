<%@ Page Language="VB" MasterPageFile="~/CFRMasterPage.master" AutoEventWireup="false" CodeFile="HotelView.aspx.vb" Inherits="HotelView" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">

<SCRIPT LANGUAGE="JavaScript">
    function verfoto(codhotel,nfoto){
        var URL = "fotog.asp?hotel=" +  codhotel + "&f=" + nfoto;
        var vheight="410", vwidth="410";
        if (nfoto==-1){ 
        vheight="410";
        vwidth="410";
        }
        var sitmapa = window.open(URL,"<%=siteWeb.description%>","width=" + vwidth + ",height=" + vheight + ",top=40,left=40,alwaysraised=yes"); 
        sitmapa.focus() ;

    }
    function vercity(){
        var URL = "callejero_1.aspx?hotel=<%=aloj.id%>";
        var sitkyj = window.open(URL,"<%=siteWeb.description%>","width=650,height=580, top=30,left=30,alwaysraised=yes,location=no,menubar=no,scrollbars=yes,resizable=yes"); 
        sitkyj.focus()  ;

    }
</script>
<STYLE type=text/css>#divUpControl {
    LEFT: 10px; POSITION: absolute; TEXT-ALIGN: right; TOP: 10px; WIDTH: 320px; Z-INDEX: 1
}
#divDownControl {
    LEFT: 10px; POSITION: absolute; TEXT-ALIGN: right; TOP: 220px; WIDTH: 320px; Z-INDEX: 1
}
#divContainer {
    CLIP: rect(0px 320px 190px 0px); HEIGHT: 190px; LEFT: 10px; OVERFLOW: hidden; POSITION: absolute; TOP: 30px; VISIBILITY: hidden; WIDTH: 320px
}
#divContent {
    LEFT: 0px; POSITION: absolute; TOP: 0px
}
</STYLE>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


    <div class="hotelview container">
        <div class="row">
            <div class="col-xs-9">
                <div class="bookhotelheader hoteldescbody">
                    <span class="hotelname"><%=Me.aloj.hotelName%></span>
                    <span class="hotelstars"><%=(cfr.iu.HotelIU.getCategoryImghtml(Me.aloj.cod_Categoria, 1))%></span>
                    <p class="bookhotellocalization">
                    <p class="bookhotellocalization"><%=aloj.Direccion%>,&nbsp;<%=aloj.codpostal%></p>
                </div>

            </div>

        </div>
        
        <div class="row blurbcont">

            <div class="col-xs-4">
                <address>
                    <abbr title="<%=idhotel(18 )%>"><span class="glyphicon glyphicon-earphone" aria-hidden="true"></span></abbr> <%=aloj.Telefono%><br />
                    <a href="mailto:#"><span class="glyphicon glyphicon-envelope" aria-hidden="true"></span> <%=aloj.Email%></a><br />
                    <span class="hotelroomnumber" aria-hidden="true"></span><span> <%=aloj.Habitaciones%></span> <%=idhotel(17)%><br />
                </address>


                <%  If aloj.photos.Count > 0 Then
                %>

                <table width="100%" border="0" cellspacing="5px" cellpadding="0">
                    <tr>

                        <%
                            For Each hPhoto As cfr.obj.Photo In aloj.photos.Values
                        %>
                        <td width="25%">
                            <div align="center">
                                <div class="hoverbox">
                                    <a href="#"><%=cfr.iu.HotelIU.getPhotoImgURL(hPhoto)%></a>
                                </div>
                            </div>
                        </td>
                        <% 
                        Next
                        %>
                    </tr>
                </table>
                <%End If%>
            </div>

            <div class="col-xs-6">
                <div class="">
                    <div class="hoteldescription">
                        <p><%=aloj.description.DescriptionByLang%></p>
                    </div>
                </div>
            </div>

            <%--end blurb--%>
        </div>


        <div style="float: right; width:100%;"> 
            <asp:HyperLink ID="lnkBook1" runat="server" CssClass="btn btn-primary btn-lg" NavigateUrl="~/b_Request.aspx"><span class="glyphicon glyphicon-tag" aria-hidden="true"></span>&nbsp;<B><%=idhotel(14 )%></B></asp:HyperLink>
        </div>


    </div>
    
        <!-- begin home boxes -->
    <div class="container">
        
        <%--<div id="homeBoxes" class="clearfix" style="padding-bottom: 15px;">
            <div class="homeBox">--%>
        <div class="row">
			<div class="col-xs-12 col-sm-4">
                <h3><%=idhotel(7)%></h3>
                <img class="img-thumbnail" title="<%=idhotel(7)%>" src="./images/hotellocation.png" alt="" width="274" height="101">
                <p><%=Me.GetPositionString()%></p>
                
            </div>

            <div class="col-xs-12 col-sm-4">
                <h3><%=idhotel(2)%></h3>
                <img class="img-thumbnail" title="<%=idhotel(2)%>" src="./images/hotelservices001.png" alt="" width="274" height="101">
                <p><%=Me.GetServicesString(1)%></p>


            </div>
            <div class="col-xs-12 col-sm-4">
                <h3><%=idhotel(3)%></h3>
                <img class="img-thumbnail" title="<%=idhotel(3)%>" src="./images/room-service.png" alt="" width="274" height="101">
                 <p><%=Me.GetServicesString(2)%></p>
               
            </div>
        </div>
    </div>
    <!-- end home boxes -->
    

<div class="hotelview container">

<%  
    '-------------Servicios Generales
    If aloj.services.DefaultView.Count = 0 Then
        Me.noInfo(idhotel(3))
    Else
        Me.showServic(aloj.services, idhotel(4), 3)
        Me.showServic(aloj.services, idhotel(5), 4)
        Me.showServic(aloj.services, idhotel(6), 5)
    End If
 
%>


    <%  'SALONES        
        If aloj.saloons.DefaultView.Count > 0 Then
    %>

    <div class="" style="margin-bottom: 60px;margin-top:20px">
        
    <h4 class="c2"><%= idhotel(12 )%></h4>
    <table width='100%' border='1' cellspacing='0' cellpadding='1' bordercolor="#ffffff" class="table table-striped">
        <thead >
        <tr>
            <th width="212">&nbsp;</th>
            <th width="45" class='Cabecera'>m2</th>
            <th width="44" class='Cabecera'><%=idhotel(22 )%></th>
            <th width="52" class='Cabecera'><%=idhotel(23 )%></th>
            <th width="54" class='Cabecera'><%=idhotel(24 )%></th>
            <th width="61" class='Cabecera'><%=idhotel(25 )%></th>
            <th width="50" class='Cabecera'><%=idhotel(26 )%></th>
            <th width="50" class='Cabecera'><%=idhotel(27 )%></th>
        </tr>
        </thead>
        <tbody >
        <%  For Each drv As Data.DataRowView In aloj.saloons.DefaultView

        %>
        <tr>
            <td class='Estandard1'><b>&nbsp;<%=drv("Nombre")%></b></td>
            <td class='Estandard'><%=drv("m2")%></td>
            <td class='Estandard'><%=drv("Teatro")%></td>
            <td class='Estandard'><%=drv("Escuela")%></td>
            <td class='Estandard'><%=drv("Cocktail")%></td>
            <td class='Estandard'><%=drv("Banquete")%></td>
            <td class='Estandard'><%=drv("mesa_imperial")%></td>
            <td class='Estandard'><%=drv("mesa_u")%></td>
        </tr>
        <%
                  
        Next%>
            </tbody>
    </table>
    </div>
    <%  
    End If
 
    %>
        <div style="float: right; width:100%;"> 
            <asp:HyperLink ID="lnkBook2" runat="server" CssClass="btn btn-primary btn-lg" NavigateUrl="~/b_Request.aspx"><span class="glyphicon glyphicon-tag" aria-hidden="true"></span>&nbsp;<B><%=idhotel(14 )%></B></asp:HyperLink>
        </div>

    
    </div>


</asp:Content>




