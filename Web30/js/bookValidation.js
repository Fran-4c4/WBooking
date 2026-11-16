
/*Booking validations helpers
User interface helpers
*/


var df = document.forms[0];
var clientidstr="ctl00_ContentPlaceHolder1_";

function mda(nombreCapa,vTexto,idpos){
	md(nombreCapa,vTexto);
	return true;
}
function oda(nombreCapa){
	od(nombreCapa);
    recargarCapa(nombreCapa, "");
	return true;
}
function showDescHab(cod_hab){
	var dlName="ldes_" + cod_hab;
	vocapa(dlName);
}
function cancelBook(url){
	var pagecancelar = window.open(url,"pagecancelar","width=610,height=400,top=70,left=70,alwaysraised=yes"); 
	pagecancelar.focus();
	return false;
}
function bluron() {
    $(this).blur();
}

function showroomdesglosedprice(strPrec, strReg, strtar) {
    var roominfo = configroominfodesglosed(strPrec, strReg, strtar);
    recargarCapa("desglosedpriceinner", roominfo);
    $("#divdesglosedmodal").modal('show');
}

function configroominfodesglosed(strPrec,strReg,strtar){
	var cabnoches=nNoches.split("|");
	var cabNameDias=nNameDias.split("|");
	var cabprecios=strPrec.split("|");
	var cabreg=strReg.split("|");
	var cabtar=strtar.split("|");
	var cadena = "";
	var cadDia="";
	var cadPrec="";
	var cadreg="";

	    cadena += "<div class='divdesglosedprice'>";
		cadena += "<table border=0 cellpadding=2 cellspacing=2 class='bookdesglosedPriceTable table table-striped'><tr>";
		cadena += "<td colspan=2 class=booktddesglosed>" + titDays + "</TD>";
		cadena += "<td class=booktddesglosed ALIGN=RIGHT>" + titPrice + "</TD>";
		cadena += "<td class=booktddesglosed  ALIGN=CENTER>" + titReg + "</TD>";
		cadena += "<td class=booktddesglosed ALIGN=CENTER>" + titTariff + "</TD>";
		cadena +="</tr>";
		for (var i=0; i < cabnoches.length-1; i++) {
		switch (parseInt(cabreg[i])){
			case 1:
				cadreg=regsa;
				break;
			case 2:
				cadreg=regad;
				break;
			case 3:
				cadreg=regmp;
				break;
			case 4:
				cadreg=regpc;
				break;
			case 5:
				cadreg=regti;
				break;
		}
		cadena += "<tr>";
		cadena += "<td class=booktddesglosed>" + cabNameDias[i] + "</TD>";
		cadena += "<TD>" + cabnoches[i] + "</TD>";
		cadena += "<td align=right>" + cabprecios[i] + "&euro;</TD>";
		cadena += "<TD align=center>" + cadreg + "</TD>";
		cadena += "<TD>&nbsp;"+cabtar[i]+"</TD>";
		cadena += "</tr>";
		}

		cadena += "</table>";
		cadena += "</div>";
	return cadena;

}

function showcardlayer(oper)
{
    var varfpcard = BuscarObjeto("fpcard");
    var varfpccc = BuscarObjeto("fpccc");

	if (oper==0)
	{
	    if (varfpcard != null) {
	        varfpcard.style.display = "block";
	    }
	    if (varfpccc != null) {
	        varfpccc.style.display = "none";
	    }	        
	}
	else if (oper==1)
	{
	    if (varfpcard != null) {
	        varfpcard.style.display = "none";
	    }
	    if (varfpccc != null) {
	        varfpccc.style.display = "block";
	    }	
	}
}

function showcancelcondition(divid) {
    $("#" + divid).toggle(500);
    return false;
}
function selectunitnorefund(roomId, roomPrice) {
    $("#norefundtariffselected").val(1);
    addcancelconditionspolicy(roomId,1);
    selectunit(roomId, roomPrice);
}
function selectunitstd(roomId, roomPrice) {
    $("#norefundtariffselected").val(0);
    addcancelconditionspolicy(roomId, 0);
    selectunit(roomId, roomPrice);
}
function selectunit(roomId, roomPrice) {
    var str1 = ""; //$("#roomtitle_" + roomId + " .roomtitlecapacity").html();
    str1 += " " + $("#roomname_" + roomId + "").html();
    $("#roomselecteddescription").html(str1);
    //$("#btnbook_sp_"+roomId).html("Descartar");
    
    $("body").data("bookunitprice", roomPrice);

    var norefundtariffselected = $("#norefundtariffselected").val();
    if (norefundtariffselected == 1)
    {
        $("#bookpricetotaldiv .norefundtariff").show();
    } else {
       $("#bookpricetotaldiv .norefundtariff").hide(); 
    }
	
    $("#selectedroomid").val(roomId);

    cbolocationchange();
    calctotalprice();
    $("a.changepref").show();
    showhiderooms(true, roomId);
    $("#bookpricetotaldiv").removeClass('hidden');
    $('#pnlClientData').removeClass('hidden');
    $('#pnlClientData').show();
    $("#pnlbooksummary").show();
    $('#pnlBookButton').removeClass('hidden');
    $('#pnlBookButton').show();
    bsetfocus("clientname");
}

function calctotalprice() {
    var pricetotal = 0;
    var unitprice = $("body").data("bookunitprice");
    if (unitprice != null) {
        pricetotal += parseFloat(getnumber(unitprice));
    }
    var pickprice = $("body").data("booklocationpickprice");
    if (pickprice != null) {
        pricetotal += parseFloat (getnumber(pickprice));
    }

    $("#bookpricetotal").html(pricetotal);
}

function getnumber(mynumber) {
    console.log("bookvalidation.getnumber: " + mynumber);
    var newnum = mynumber;
    if (mynumber !=undefined  && isNaN(mynumber)==true ) {
       newnum= mynumber.replace(/,/g, ".");
    } 
    
    return (newnum);
}

function addcancelconditionspolicy(id, type) {
    var policyvalues = $("#tariffcancelcondition_" + id + "_" + type).data("cancelpolicy");
    $("#hdfpolicycondition").val(policyvalues);
    var pol = $("#cancelconditiondiv_" + id + "_" + type + " .tariffconditions").html();
    $("#tariffcancelconditionsforfieldset").html(pol); 
    return false;
}


function changeroom() {
    showhiderooms(false, -1);
    $("a.changepref").hide();
}

function showhiderooms(hide,roomId) {
    $(".wb-room-card").each(function(index, element) {
        // element == this
        if ($(this).is("#roomfindedbysearch_" + roomId)) {
            
        } else {
            if (hide==true) {
                $(this).hide(1000);
            } else {
                 $(this).show(1000);
            }
            
        }
    });
}

function bsetfocus(idname) {
    var idnamestr = "#" + idname;
    var varObjeto = BuscarObjeto(idname);
    //varObjeto.focus();
    $(idnamestr).mouseup(function (e) {
        e.preventDefault();
    });
   //use delay for chrome bug
    setTimeout(function () { $(idnamestr).focus() }, 500);
}

function validateDates()
{
    
    //var WhereToMove = jQuery("#scroll2results").position().top;
    //jQuery("html,body").animate({ scrollTop: WhereToMove }, 0);
    //$("#divbookloading").show();
    $('#divbookloading').addClass('open');
    $("#promotioncodeerrordiv").hide();
    $('#pnlResults').hide();
    $('#pnlClientData').hide();
    $("#pnlbooksummary").hide(1000);
    
    $("#lblDateIn").html(formatday($('#checkindate').datepicker('getDate')));
    $("#lblDateOut").html(formatday($('#checkoutdate').datepicker('getDate')));

    $("#pnlinfobook").hide();

    caroptionssummary();
    return true;
}

function caroptionssummary() {
    var divlocation = $("#cbolocationpickupsummary");
    if (divlocation == null) {
        return true;
    }

    
    $("#cbolocationpickupsummarydesc").html($("#cbolocationpickup option:selected").text());

    var checklocation = $("#chkreturntolocation");
    //console.log(" chkreturntolocation:" + checklocation.is(':checked'));

    if (checklocation.is(':checked')) {
        $("#cbolocationdropsummarydesc").html($("#cbolocationdrop option:selected").text());
   }
    else {
        $("#cbolocationdropsummarydesc").html($("#cbolocationpickup option:selected").text());
    }

    var timein = $("#cbotimecheckin option:selected").text();
    var timeout =$("#cbotimecheckout option:selected").text();

    $("#lblDateIn").html(formatday($('#checkindate').datepicker('getDate')) + " " + timein);
    $("#lblDateOut").html(formatday($('#checkoutdate').datepicker('getDate')) + " " + timeout);
}

function cbolocationchange() {

    var cbovalpick = $("#cbolocationpickup option:selected").val();
    var cbovaldrop = $("#cbolocationdrop option:selected").val();

    $('#selectedlocationpickid').val(cbovalpick);
    $('#selectedlocationdropid').val(cbovaldrop);


    var pricevarpick = getnumber($("body").data("cbolocationprice_" + cbovalpick));
    var pricevardrop = getnumber($("body").data("cbolocationprice_" + cbovaldrop));

    $('#selectedlocationpickprice').val(pricevarpick);
    $('#selectedlocationdropprice').val(pricevardrop);

    var pricevar = 0;

    if (pricevarpick >= pricevardrop) {
        pricevar = pricevarpick;
    } else if (pricevarpick <= pricevardrop) {
        pricevar = pricevardrop;
    }

    $("body").data("booklocationpickprice", pricevar);

    calctotalprice();

    if (pricevar > 0) {
        //var msg = msglocationcost + " " + pricevar + " \u20AC";
        $('#locationpickcostvalue').html(pricevar);
        $('#locationpickcost').show(500);
        $('#locationpickcostsummary').html(pricevar);
        $('#locationpickcostsummaryrow').show();

    }
    else {
        $('#locationpickcost').hide();
        $('#locationpickcostsummaryrow').hide();
    }
}

/*
main validation form
*/
function validateBooking(source,args) {
   
    var haserror = false;
    if (validateRoom()==false) {
        haserror = true;
    }

    if (validateClientData() == false) {
        $("#msgclientdatarequired").show();
        haserror = true;
    } else {
        $("#msgclientdatarequired").hide();
    }

    if (validateCountry() == false) {
        haserror = true;
    }    


    if (paidTypeControl() == false) {
        haserror = true;
    }

    if (validateLPD()==false) {
        haserror = true;
    }
    
    
    if (haserror == false) {
        args.IsValid = true;
        return true;
    } else {
        $("#bookingErrorModal").modal('show');
        args.IsValid = false;
        return false;
    }

}

function showloginform() {
 var modal = $("#bookingloginmodal");
    modal.appendTo("form");
    modal.modal("show");
}
/*
checks if the room was selected, anyway is controlled on server.
*/
function validateRoom() {

	var varselectedRoom= BuscarObjeto("selectedroomid");
    $("#msgroomrequired").hide();

	if (varselectedRoom == null) {
	    $("#msgroomrequired").show();
		return false;
	}
	else {
	    if (varselectedRoom.value == 0) {
	        $("#msgroomrequired").show();
	        return false;
	    }
	}
}

function validateClientData() {
    var haserror = false;

    if (!validateemptyfield("clientname")) {
            return false;
    }
    if (!validateemptyfield("clientsurname")) {
            return false;
    }
    if (!validateemptyfield("clientphone")) {
            return false;
    }
    if (!validateemptyfield("clientemail")) {
            return false;
    }
    if (!validateemptyfield("clientcheckemail")) {
            return false;
    }
    return true;
}

function validateemptyfield(idname) {
    var varrequired=null;
    varrequired = BuscarObjeto(idname);
    if (varrequired !=null &&  varrequired.value == "") {
            return false;
    }
    return true;
}
function replacespace(idname) {
    var varrequired = null;
    varrequired = BuscarObjeto(idname);
    if (varrequired != null ) {
        varrequired.value = varrequired.value.replace(/ /g,"");
    }
}

function validateCountry(){
	var varObj=BuscarObjeto("cboCountry");
	if (varObj == null) {
	    return true;
	}
	else{
	    if (varObj.value == "-1") {
	        $("#msgcountryrequired").show();
			return false;
		}
	    else {
	        $("#msgcountryrequired").hide();
			return true;
		}
	}
}

/*
show message indicating to select the room
*/
function showInfoBookLayer(showHide,msg) {
    
    //if (showHide==true) {
    //    varLayer=BuscarObjeto("infoBook");
    //	varLayer.style.display="block";
    //	varlblInfo=BuscarObjeto("lblInfo");
    //	varlblInfo.innerHTML=msg;
    //}
    //else {
    //    varLayer=BuscarObjeto("infoBook");
    //	varLayer.style.display="none";
    //	varlblInfo=BuscarObjeto("lblInfo");
    //	varlblInfo.innerHTML="";	    
    //}
}

function validateLPD(){

	var lpd=BuscarObjeto("aceptolopd");
	if (lpd==null) {
	    return true;
	}
	if (!lpd.checked)
	{
	    $("#msgpaidtyperequired280").show();
		return false;
	} else {
	    $("#msgpaidtyperequired280").hide();
	}
}
/*function used by asp.net custom validator*/
function lopdCheckBoxRequired_ClientValidate(sender, e) {

    e.IsValid = jQuery("#aceptolopd").is(':checked');
    if (e.IsValid == false) {
        //jQuery("#Reqaceptolopd").addClass("requiredblock");

    }
    else {
        jQuery("#Reqaceptolopd").display="none";
    }
}

/*function used by asp.net custom validator*/
function lopdCheckBoxOnClick() {

   var result = jQuery("#aceptolopd").is(':checked');
   if (result == true) {
       jQuery("#Reqaceptolopd").css("display", "none");
    }
}

function showlopdwindow()
{
    features="width=450,height=500,top=20,left=20,status=yes,scrollbars=yes";
    win=window.open('lopd.aspx','bar',features);
}

function showpnlbooksummary(options) {
    //var option1 = options[0];
    //var option2 = options[1];

    $("#divbookloading").removeClass('open');
    $("#pnlbooksummary").hide("1000");
    $("#pnlinfobook").show();
    console.log("showpnlbooksummary: " + options);
}

function scrolltoresulttop() {
    $('html,body').animate({scrollTop: $("#resultdivscroll").offset().top},'slow');
    console.log("scrolltoresulttop");
}

function bookpromotioncode() {
    $("#promotioncodeerrordiv").show();
}


function formatday(day2format) {
     
    day2format = moment(day2format);
    day2format.locale(datepickerinputdaterange);
    var dayformated = day2format.format("dddd, D MMMM YYYY");
    return dayformated;
}


function paidTypeControl()
{
    /*reset fields*/
    $("#msgpaidtyperequired311").hide();
    $("#msgpaidtyperequired").hide();
    $("#msgpaidtyperequired314").hide();
    
	var paidTypesAdmitedObj=BuscarObjeto("paidTypesAdmited");
	var pt = paidTypesAdmitedObj.value;
    
	if (pt == 5 || pt == 6) {
	    return true;
	}
	//any type
	if (pt == 0) {
        //there 2 radiobuttons if the hotel admits any type
	    var radCCC=BuscarObjeto("radTransfer");
	    var radCredit = BuscarObjeto("radCreditCard");
	    
	    if (!radCCC.checked && !radCredit.checked) {

	        $("#msgpaidtyperequired").show();
			return false;
		}
	    else if (radCCC.checked) {
            //nothing to do, user receives the account number of the hotel
		    return true;
		}
	    else if (radCredit.checked) {

	        var creditcarderror = validatecreditcardselection();
            if (creditcarderror ==true) {
                return false;
            }

	    }
	}
    if(pt==1) {
        var creditcarderror = validatecreditcardselection();
            if (creditcarderror ==true) {
                return false;
            } else {
                return true;
            }
    }

	return true;
	
}


function validatecreditcardselection() {
    var creditcarderror = false;

    var cc1 = BuscarObjeto("Card_Type");
    var cc2 = BuscarObjeto("Card_Num");
    var cc3 = BuscarObjeto("ExpireMM");
    var cc4 = BuscarObjeto("ExpireYY");
    var CCUCard = BuscarObjeto("CCUTarjeta");

    replacespace("Card_Num");

    var retVal = CheckCardNumber(cc1, cc2, cc3, cc4);

    if (cc2.value == "") {
        creditcarderror = true;
    } else {
        if (retVal == 2) {
            $("#msgpaidtyperequired313").show();
            creditcarderror = true;
        } else {
            $("#msgpaidtyperequired313").hide();
        }
    }


    if (CCUCard.value == "" || CCUCard.value.length < 3) {
        creditcarderror = true;
    }

    //caducity date	
    if (retVal == 1) {
        $("#msgpaidtyperequired314").show();
        creditcarderror = true;
    }


    var cardHolder = BuscarObjeto("Card_Holder");
    if (cardHolder.value == "") {
        creditcarderror = true;
    }

    if (creditcarderror == true) {
        $("#msgpaidtyperequired311").show();

    } else {
        $("#msgpaidtyperequired314").hide();
        $("#msgpaidtyperequired313").hide();
    }

    return creditcarderror;

}

function printdiv(divtoprint) {
    var headstr = "<html><head><title></title></head><body>";
    var footstr = "</body>";
    var newstr = document.all.item(divtoprint).innerHTML;

    var w = window.open("bookprint", "Imprimir/print", "width=800px , height=600px;");
    w.document.write(headstr + newstr + footstr);
    w.document.close();
    w.focus();
    w.print();
    //w.print(); Don't do this otherwise chrome won't work. Look at the onload on the body of the newly created window.
    ///w.close(); Don't do this otherwise chrome won't work

}


    $(document).ready(function() {
        //$(".dropdownflags img.flag").addClass("flagvisibility");

        $(".dropdownflags dt a").click(function() {
            $(".dropdownflags dd ul").toggle();
        });
                        
        $(".dropdownflags dd ul li a").click(function() {
            var text = $(this).html();
            $(".dropdownflags dt a span").html(text);
            $(".dropdownflags dd ul").hide();
        });
                        
        function getSelectedValue(id) {
            return $("#" + id).find("dt a span.value").html();
        }

        $(document).bind('click', function(e) {
            var $clicked = $(e.target);
            if (! $clicked.parents().hasClass("dropdownflags"))
                $(".dropdownflags dd ul").hide();
        });


        //$("#flagSwitcher").click(function() {
        //    $(".dropdownflags img.flag").toggleClass("flagvisibility");
        //});

        $('#mydatepicker').datepicker().on('changeDate', function (ev) {
        var checkin = $('#checkindate').datepicker('getDate');
        var checkout = $('#checkoutdate').datepicker('getDate');
        var oneDay = 24 * 60 * 60 * 1000;	// hours*minutes*seconds*milliseconds
        var diffDays = Math.floor(Math.abs((checkin.getTime() - checkout.getTime()) / (oneDay)));
        $('.cal-nights').html(diffDays);

    });

    });

    // Magnific popup and caroussel
    //-----------------------------------------------
    $(document).ready(function () {
        if (($(".pop-gallery-bk").length > 0) || ($(".popup-img").length > 0) || ($(".popup-iframe").length > 0) || ($(".popup-img-single").length > 0)) {
            console.log("popgallery init");

            bindpopupunit();
            bindcarouselunit();


                $(".popup-img-single").magnificPopup({
                    type: "image",
                    gallery: {
                        enabled: false
                    }
                });
                $('.popup-iframe').magnificPopup({
                    disableOn: 700,
                    type: 'iframe',
                    preloader: false,
                    fixedContentPos: false
                });

            };

       


    });/*$(document).ready(function () end*/


function bindpopupunit() {
    $('.pop-gallery-bk').each(function () { // the containers for all your galleries
        console.log("pop bind");
        var whothis = $(this);
        $(this).magnificPopup({
            delegate: 'a', // child items selector, by clicking on it popup will open
            type: 'image',
            gallery: {
                enabled: true
            }
        });
    });
}

function bindcarouselunit() {
    $(".owl-carousel.content-slider-with-controls.owl-room-pic").owlCarousel(
    {
        autoPlay: false, //Set AutoPlay to 3 seconds
        singleItem: true,
        lazyLoad: true
    });
}







