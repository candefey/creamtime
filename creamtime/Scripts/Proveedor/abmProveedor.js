$(document).ready(function () {
    today = new Date();
    dd = today.getDate();
    mm = today.getMonth() + 1;
    yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    var today = dd + '/' + mm + '/' + yyyy;
    document.getElementById('lbl_fecha_actual').innerHTML = today;
    setTimeout(function () {
        $("#lbl_success").fadeOut().empty();
        $("#lbl_error").fadeOut().empty();
        $("#lbl_warning").fadeOut().empty();
    }, 5000);
   
});
