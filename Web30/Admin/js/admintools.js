/* Author:Francisco R. Moreno Santana
 * Author e-mail:
 * Version:1.1.0
 * Created:March 2015
 */


/*
sections use name as is in each sectio to find quick
-sectionmodals
    functions to show modals
-sectionprint
    functions for printing
-sectionMASTERPAGEMETHODS
    functions used from master page and can be used in any page

-sectionOTHERS
    control numbers

-sectionOBJECTS
    objects used 
-sectionDATEpickerjquery
    functions to configure datepicker jquery only
*/

var prm = Sys.WebForms.PageRequestManager.getInstance();

//Sys.WebForms.PageRequestManager.getInstance().add_EndRequest(Request_End);
function Request_EndController(sender, args) {

    if (args.get_error() != undefined) {
        var errorMessage = "";
        if (args.get_response().get_statusCode() == 200) {
            errorMessage = args.get_error().message;
        }
        else {
            // the server wasn't the problem...
            errorMessage = "Se ha producido un error en el sistema.";
        }
        // do something with the errorMessage here.
        // now make sure the system knows we handled the error.
        args.set_errorHandled(true);
        Request_EndControllerShowMessageError(errorMessage);
    }
}

function Request_EndControllerShowMessageError(errorMessage) {
    $("#alertmessage").html(errorMessage);
    $("#alertdiv").show(500);
}

function ClearErrorState() {
    $("#alertdiv").hide(500);
    $("#alertmessage").innerHTML = "";
}


$(document).ready(function() {
    
/*sectionmodals*/

    $("#modaldeleteItem").on('show.bs.modal', function (event) {
        //event.preventDefault();
        $('#modaldeleteItem').appendTo("form");
        var button = $(event.relatedTarget); // Button that triggered the modal
        var itemid = button.data('objid'); // Extract info from data-* attributes

        var itemname = button.data('objname'); // Extract info from data-* attributes
        if (itemname==null) {
            var itemnamebyid = button.data("objnameid");
            itemname = $("#" + itemnamebyid + itemid).html();
        }
        
        // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
        // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
        var modal = $(this);
        modal.find('.resultmessageplace').html("");
        modal.find('#resultmessageplace').html("");
        modal.find('.btndelete').removeClass("hidden");
        modal.find('#itemplacenameto').html(itemname);
        modal.find('#itemdeletecode').val(itemid);

        $("#field_itemobject").val( itemid);
    });


    $("#modaledit").on('show.bs.modal', function (event) {
        $('#modaledit').appendTo("form");
        var button = $(event.relatedTarget); // Button that triggered the modal
        var itemid = button.data('objid'); // Extract info from data-* attributes
        $("#field_itemobject").val(itemid);
        // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
        // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
        var modal = $(this);
        var pnlerror = $("#pnlerror");
        if(pnlerror!=null) {
            pnlerror.remove();
        }
    });

    $("#modalmastereditbook").on('show.bs.modal', function (event) {
        var modal = $(this);
        modal.appendTo("form");
        
        var pnlerror = $("#modalmastereditbook .modalmessage");
        if(pnlerror!=null) {
            pnlerror.html("");
        }
    });
    
    $(".openedititem-newitem").click(function (event) {
        //event.preventDefault();
        var button = $(event.currentTarget); // Button that triggered the modal
        var title = button.data('modaltitle'); // Extract info from data-* attributes
        var divplace = button.data('modaldiv'); // Extract info from data-* attributes
        var defaultbutton = button.data('modalbuttonid'); // Extract info from data-* attributes

        showmodaledit(title, divplace, defaultbutton);
    });

    

});

//show modal moving selected container inside modal-body
function showmodaledit(title, divplace, defaultbutton) {

    var modal = $("#modaledit");
    modal.appendTo("form");
    $('#modaledit #modaledit_title').html(title);
    //$("#modaledit .modal-body").html("");//clean before add
    $(divplace).appendTo("#modaledit .modal-body");
    $(defaultbutton).appendTo("#modaledit #modalfooterdefaultbuttonplace");
    
    modal.modal("show");

}

function configuremodal(modalid, modaltitle) {
    $("#" + modalid + " .modal-title").html(modaltitle);
}

function showmodal(modalid,titlemodal, modalbody,oktextbtn,okcallbackfunction) {
    var msg = "<div class='modal' id='" + modalid + "' tabindex='-1' role='dialog' data-backdrop='static'><div class='modal-dialog' role='document'>" +
        "<div class='modal-content'>" +
        "<div class='modal-header'><button type='button' class='close' data-dismiss='modal'></button>" +
        "<h4 class='modal-title' id='myModalLabel'>" + titlemodal + "</h4>" +
        "</div>" +
        "<div class='modal-body'>" + modalbody + "</div>" +
        "<div class='modal-footer'>" +
        "<button type='button' class='btn btn-primary' data-dismiss='modal'>Cancelar</button>" +
        "<button type='button' class='btn btn-danger' data-dismiss='modal' onclick='" + okcallbackfunction + "'>"+oktextbtn+"</button>" +
        "" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>";
    $("#" + modalid).remove();
    $('body').append(msg);
    $("#" + modalid).modal();
    return $("#" + modalid);


}

function showmessageerror( modalbody) {
        var msg = "<div class='modal' id='errmodal' tabindex='-1' role='dialog' data-backdrop='static'><div class='modal-dialog' role='document'>" +
        "<div class='modal-content'>" +
        "<div class='modal-header' style='background-color: red;'><button type='button' class='close' data-dismiss='modal'></button>" +
        "<h4 class='modal-title' id='myModalLabel'><i class='fa fa-exclamation-triangle fa-2x'></i> Se ha producido un error</h4>" +
        "</div>" +
        "<div class='modal-body'>" + modalbody + "</div>" +
        "<div class='modal-footer'>" +
        "<button type='button' class='btn btn-primary' data-dismiss='modal'>Cerrar</button>" +
        //"<button type='button' class='btn btn-danger' data-dismiss='modal' onclick='" + okcallbackfunction + "'>"+oktextbtn+"</button>" +
        "" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>";
        $("#errmodal").remove();
        $('body').append(msg);
        $("#errmodal").modal();
        return $("#errmodal");
}
function showmessageerrorbyid(modalid, titlemodal, modalbody) {
    var modal = $("#" + modalid);
    if (modal.length > 0) {
        $(modal).find(".modal-header").addClass("modal-header-alert");
        $(modal).find(".modal-title").html(titlemodal);
        $(modal).find(".modal-body").html(modalbody);
        $(modal).modal();
    }
}

function showmessage(modalid, titlemodal, modalbody) {
        var msg = "<div class='modal' id='" + modalid + "' tabindex='-1' role='dialog' data-backdrop='static'><div class='modal-dialog' role='document'>" +
        "<div class='modal-content'>" +
        "<div class='modal-header' ><button type='button' class='close' data-dismiss='modal'></button>" +
        "<h4 class='modal-title'>" + titlemodal + "</h4>" +
        "</div>" +
        "<div class='modal-body'>" + modalbody + "</div>" +
        "<div class='modal-footer'>" +
        "<button type='button' class='btn btn-primary' data-dismiss='modal'>Cerrar</button>" +
        "" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>";
        var modal = $("#" + modalid);
        if (modal.length > 0)
        {
            $(modal).find(".modal-header").removeClass("modal-header-alert");
            $(modal).find(".modal-title").html(titlemodal);
            $(modal).find(".modal-body").html(modalbody);
            $(modal).modal();
        }
        else {
            $('body').append(msg);
            $("#" + modalid).modal();
        }


        return modal;
}
/*sectionprint functions*/

function printdiv(divtoprint) {
    var headstr = "<html><head><title></title></head><body>";
    var footstr = "</body>";
    var newstr = document.all.item(divtoprint).innerHTML;

    var w = window.open("bookprint","Imprimir reserva",  "width=800px , height=600px;");
    w.document.write(headstr + newstr + footstr);
    w.document.close();
    w.focus();
    w.print();
    //w.print(); Don't do this otherwise chrome won't work. Look at the onload on the body of the newly created window.
    ///w.close(); Don't do this otherwise chrome won't work

}


/*sectionMASTERPAGEMETHODS*/
function masterviewbook(bookid) {

    //var bookdata = $("#bookdata_id_" + bookid).data("bookdata");

    $("#hddmasterbookidbookview").val(bookid);
    var modal = $("#modalmasterbookview");
    modal.appendTo("form");
    modal.modal("show");
    
    document.getElementById("btnmastershowbook").click();
    
}
/*set values to edit book*/
function editbook_click123(bookid) {
    var button = $(event.currentTarget); // Button that triggered 
    var objval = $("#bookdata_id_" + bookid);

    var bookdata = $("#bookdata_id_" + bookid).data("bookdata");


    $("#bookidhddedit").val(bookdata.bkid);
    $("#field_itemobject").val(bookdata.bkid);

    /*reset styles*/
    $("#msgbookstatus").hide();
    $("#msgsendemail").hide();
    $('#modaleditprimarybutton').prop('disabled', false);


    //if (bookstate==1||bookstate==4||bookstate==6)
    $("#hddbookstatus").val(bookdata.state);
    $("#cbobookstate").val(bookdata.state);
    $("#tbookidproperty").val(bookdata.bookidproperty);

    $("#cbobookpaystate").val(bookdata.paystate);
    $("#tbookpricefinal").val(bookdata.pricechanged);
    $("#modalmastereditbook").modal("show");
}

/*set values to edit book*/
function editbook_click(bookid) {
    var button = $(event.currentTarget); // Button that triggered 
    var objval = $("#bookdata_id_" + bookid);

    var bookdata = $("#bookdata_id_" + bookid).data("bookdata");


    $("#bookidhddedit").val(bookdata.bkid);
    //$("#field_itemobject").val(bookdata.bkid);

    /*reset styles*/
    $("#modalmastereditbook").find( ".modalmessage").hide();
    $("#msgbookstatus").hide();
    $("#msgsendemail").hide();
    $('#modaleditprimarybutton').prop('disabled', false);


    //if (bookstate==1||bookstate==4||bookstate==6)
    //$("#hddbookstatus").val(bookdata.state);
    //$("#cbobookstate").val(bookdata.state);
    //$("#tbookidproperty").val(bookdata.bookidproperty);

    //$("#cbobookpaystate").val(bookdata.paystate);
    //$("#tbookpricefinal").val(bookdata.pricechanged);
    $("#litbookidtit").html(bookdata.bkid);
    $("#modalmastereditbook .editbookform").hide();
    $("#modalmastereditbook").modal("show");
    //__doPostBack("btneditbook_master1", "");
    document.getElementById("btneditbookload_master").click();
}


/*for cbos in editbook modal form*/
function checkstatusvalue() {
    var valorig = parseInt($("#hddbookstatus").val());
    var val = parseInt($("#cbobookstate").val());
    if (val == 4 && valorig != 4) {
        $("#msgsendemail").show("slow");
    } else {
        $("#msgsendemail").hide();
    }
    if (val == 1 && valorig != 1) {
        $("#msgbookstatus").show("slow");
        $('#modaleditprimarybutton').prop('disabled', true);
    }
    else {
        $("#msgbookstatus").hide();
        $('#modaleditprimarybutton').prop('disabled', false);
    }
}
/*validate edit book data before submit*/
function validateeditbookinput() {
    if (Page_ClientValidate("bookeditgroup")) {
        document.getElementById('btneditbook_master').click();
    }
}

/*sectionOTHERS*/

function tabopen(tab) {
    $('.nav-tabs a[href="#' + tab + '"]').tab('show');
};

function tabopenlist(tablist) {
    $.each(tablist, function () {
        var id = $(this).prop("id");
        tabopen(id);
    });
};

$(document).ready(function () {
    $(".onlynum").keydown(function (e) {
        console.log(" e.keyCode :" + e.keyCode);
        // Allow: backspace, delete, tab, escape, enter and ,(comma=188) .(point=190)  (percentage=37)           
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 188]) !== -1 ||
            // Allow: Ctrl+A
            (e.keyCode == 53 && e.shiftKey === true) ||
            // Allow: Ctrl+A
            (e.keyCode == 65 && e.ctrlKey === true) ||
            // Allow: Ctrl+C
            (e.keyCode == 67 && e.ctrlKey === true) ||
            // Allow: Ctrl+X
            (e.keyCode == 88 && e.ctrlKey === true) ||
            // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything

            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

$(".onlynumsign").keydown(function (e) {
        console.log(" e.keyCode :" + e.keyCode);
        // Allow: backspace, delete, tab, escape, enter and ,(comma=188) .(point=190)  (percentage=37)           
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 188]) !== -1 ||
            // Allow: sign -
            (e.keyCode == 189) || (e.keyCode == 109) ||
            // Allow: Ctrl+A
            (e.keyCode == 53 && e.shiftKey === true) ||
            // Allow: Ctrl+A
            (e.keyCode == 65 && e.ctrlKey === true) ||
            // Allow: Ctrl+C
            (e.keyCode == 67 && e.ctrlKey === true) ||
            // Allow: Ctrl+X
            (e.keyCode == 88 && e.ctrlKey === true) ||
            // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything

            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
});

function solonums() {
    var s = window.event.keyCode;
    if (s == 46 || s == 44) {
        return true;
    }
    if (s < 48 || s > 57) {
        return false;
    }
    return true;
}

function startsessiontimer(duration, display) {
    var timer = duration, minutes, seconds;
    setInterval(function () {
        minutes = parseInt(timer / 60, 10);
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.text(minutes + ":" + seconds);

        if (--timer < 0) {
            timer = 0;
        }
    }, 1000);
}

$(document).ready(function () {
    var fiveMinutes = 60 * 30,
        display = $('#sessiontime');
        if (display!=null) {
            startsessiontimer(fiveMinutes, display);
        }
        
});



/*sectionOBJECTS*/

function bookdataobj(bkid, state, pricefinal, pricechanged, paystate, bookidproperty) {
    this.bkid = bkid;
    this.state = state;
    this.pricefinal = pricefinal;
    this.pricechanged = pricechanged;
    this.paystate = paystate;
    this.bookidproperty = bookidproperty;

}

/*sectionDATEpickerjquery*/
$(document).ready(function() {
        $.datepicker.regional['es'] = {
            closeText: 'Cerrar',
            prevText: '<Ant',
            nextText: 'Sig>',
            currentText: 'Hoy',
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Mi�rcoles', 'Jueves', 'Viernes', 'S�bado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mi�', 'Juv', 'Vie', 'Sab'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
            weekHeader: 'Sm',
            dateFormat: 'dd/mm/yy',
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ''
        };
        $.datepicker.setDefaults($.datepicker.regional['es']);


        $(".checkindate").datepicker({
            dateFormat: "dd/mm/yy",
            defaultDate: "+1w",
            changeMonth: true,
            numberOfMonths: 3,
            onClose: function (selectedDate) {
               $(this).closest(".input-daterange").find(".checkoutdate").datepicker("option", "minDate", selectedDate);
            }
        });

        $(".checkoutdate").datepicker({
            dateFormat: "dd/mm/yy",
            defaultDate: "+1w",
            changeMonth: true,
            numberOfMonths: 3
            //onClose: function (selectedDate) {
            //    $("#checkindate").datepicker("option", "maxDate", selectedDate);
            //}
        });

        $(".chevronview").click(function (e) {
            var chevState = $(e.target)
            //$(chevState).find(".chevronview").toggleClass('chevronopen chevronclose');
        //$("i.indicator").not(chevState).removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up");
    });

});


