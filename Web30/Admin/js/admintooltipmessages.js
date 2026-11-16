$(document).ready(function () {
    console.log("Tooltip messages initialized");
    $(".ttlocalizadorinterno")
        .popover({ trigger: 'focus', title: 'Localizador', content: "Introduzca el localizador interno que haya creado en otro sistema de reservas si fuera necesario." })
        .blur(function () {
            $(this).popover('hide');
        });

});