$(document).ready(function () {
    $("#UserName").focusin(function() {
        isValidForm();
    });
    $("#Password").focusin(function() {
        isValidForm();
    });
    $("#PasswordConfirm").focusin(function() {
        isValidForm();
    });
    $("#RoleId").focusin(function() {
        isValidForm();
    });

    isValidForm();
});

function isValidForm() {
    var userName = $("#UserName").val();
    
    $("#form").validate({
        rules : {
            UserName : {
                required : true,
                remote : {
                    type: "Get",
                    url: "/Home/CheckUserByRegister",
                    data: { userName: userName },
                    },
            },
            Password : {
                required: true,
            },
            PasswordConfirm : {
                required : true,
                equalTo : "#Password",
            },
            RoleId : {
                required : true,
            }
        },
        messages : {
            UserName : {
                required : "The field is required",
                remote : "User Name is already exist",
            },
            Password : {
                required : "The field is required",
            },
            PasswordConfirm : {
                required : "The field is required",
                equalTo : "The field is same to password",
            },
            RoleId : {
                required : "Please select Role",
            }
        }
    });
}