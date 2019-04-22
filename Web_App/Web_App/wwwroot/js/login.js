$(document).ready(function () {
    $("#UserName").keyup(function() {
        isValidForm();
    });
    $("#Password").keyup(function() {
        isValidForm();
    });
    
    isValidForm();
});

function isValidForm() {
    $("#form").validate({
        rules : {
            UserName : {
                required : true,
            },
            Password : {
                required: true,
            },
        },
        messages : {
            UserName : {
                required : "The field is required",
            },
            Password : {
                required : "The field is required",
            },
        }
    });
}