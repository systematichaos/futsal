'use strict';


$(document).ready(function () {

    //configure user roles button click 
    $('[name="btnConfigureUserRole"]').on('click', function () { // attach the onclick

        var userId = $('[data-ddl-users]').find(':selected').val();//get selected user from ddl 
        var role = $('[data-ddl-roles]').find(':selected').text();//get selected role from ddl 
        $("[data-error-message-container]").hide();

        if ($(this).val() !== null && $(this).val() !== "") {
            $.post({
                url: "/AccountManagement/ConfigureUserToRole",
                data: { userId: userId, role: role, __RequestVerificationToken: ReturnAntiForgeryToken() },
                success: function (data) {
                    // go to search page 
                    $.ajax({ URL: "/AccountManagement/SearchUserRoles" });
                },
                error: function (jqXHR) {
                    console.log(jqXHR);
                    $("[data-error-message-container]").show();//.text(jqXHR);
                }
            });
        }
                     
    })

});