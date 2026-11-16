<%@ Page Title="" Language="VB" MasterPageFile="~/CFRMasterPage.master" AutoEventWireup="false" CodeFile="hoteles.aspx.vb" Inherits="hoteles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadScript" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="hotelsearchwrapper">
             <%--<h2><%=idiom(320)%></h2>--%>
            <%
                For Each hsearch As hotelsSearchedForView In Me.hotelsAvailables.Values
                    Dim aloj2Show As cfr.obj.Hotel = hsearch.hotel
       
                    Dim localizacion As String = aloj2Show.Direccion
                    
            %>

        <div class="hotelsearchresult">
            <div>
                <div class="hotelimg">
                    <%If aloj2Show.photos.Count > 0 Then%>
                    <img class="img-thumbnail" src='<%=cfr.iu.HotelIU.getPhotoFirstImgURL(aloj2Show)%>' />

                    <%Else%>
                    <img class="img-thumbnail" src="./fotos/280340_2.jpg" />
                    <!--<img src='./images/sinfoto.gif' >-->
                    <%End If%>
                </div>
                <div class="hoteldescbody">
                    <h4 class ="hotelname"><a href="hotelview.aspx?hotelcode=<%=aloj2Show.id%>"><%=aloj2Show.hotel%></a> <span class="hotelstars"><%=(cfr.iu.HotelIU.getCategoryImghtml(aloj2Show.cod_Categoria, 1))%></span></h4>
                    
                    <p style="font-weight: bold;"><%=localizacion%></p>
                    <div class="hotelsearchresultdetails">
                        <div class="hotelsearchdescription">
                            <p><%=aloj2Show.description.DescriptionByLang%></p>
                        </div>
                    </div>
                </div>

            </div>
                
                <div class="hotelsearchprice">
                    <span style="font-size: 90%">Desde </span><span class="hotelsearchprice-big"><%=Math.Round(hsearch.minimum_price, 0)%>&#8364;</span>

                    <div class="">
                        <a role="button" class="btn btn-lg btn-primary" href="b_request.aspx?<%=me.myQueryBase%>&<%=cfr.REQCte.query.cod_hotel %>=<%=aloj2Show.id%>"><%=cfr.Language.GetCaptionBy(Language.msgEnum.E92_bookButton)%></a>
                    </div>


                </div>

            <div class="hotelsearchresultclear"></div>
        </div>

            <%  
                
            Next
	 
            %>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="bottomScriptPlaceHolder" Runat="Server">
</asp:Content>

