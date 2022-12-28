$(document).ready(function () {

    function ValidateEmail(inputText) {
        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        if (mailformat.test(inputText)) {
            return true;
        }
        else {
            return false;
        }
    }
    $("#login").click(function () {
        var candidateEmail = $("#user_email").val();
        if (ValidateEmail(candidateEmail)) {
            $.ajax({
                url: '/Home/Login',
                type: 'post',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                data: {
                    email: candidateEmail
                },
                success: function (res, data) {
                    if (res.status != false) {
                        var imgSrc = 'data:image/png;base64,' + res.qrcodeData;
                        $("#qrcode").attr("src", imgSrc);
                        $("#container_QRCode").addClass("d-flex justify-content-center");
                        //preview QR code section
                        $("#header_command").html("Scan OR code with your phone and select from one of your images or logos."); 
                        $("#login")[0].disabled = "true";
                        $("#emailInputContainer").attr("style", "display:none");
                    }
                    else {
                        alert(res.message);
                    }
                }
            });
        }
        else {
            alert("You have entered an invalid email address!");
        }
    });
});