var login = (function () {

    function checkEmail(url) {
        var email = $("#emailInput").val();
        var btnCheckEmail = $("#checkEmailButton");

            $.ajax({
                url: url,
                type: "POST",
                data: { email: email },
                success: function (response) {
                    if (response) {
                        $("#changePasswordForm").show();
                        btnCheckEmail.hide();
                    } else {
                        $("#changePasswordForm").hide();
                        alert("Email não encontrado.");
                    }
                },
            });
    }

    function loadPartialView(url, viewName) {
        $.get(url, { viewName: viewName }, function (data) {
            $("#partialViewContainer").html(data);
        });
    }

    function loadView (url) {
        $.get(url, function (data) {
            $("#mainContainer").html(data);
        });
    }

    return {
        loadPartialView: loadPartialView,
        checkEmail: checkEmail,
    };
})();