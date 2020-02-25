'use strict';



$(document).ready(function () {
         
});



function ReturnAntiForgeryToken() {
    return $('input[name=__RequestVerificationToken]').val();
}
