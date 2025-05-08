
$(function () {
    if (window.screen.width >= 1024) {
        $("#divSidebarNavMain").addClass("toggled");
    }
});

var scheappadmin = scheappadmin || {};
$(".sidebar-dropdown > a").click(function () {
    $(".sidebar-submenu").slideUp(200);
    if (
        $(this)
            .parent()
            .hasClass("active")
    ) {
        $(".sidebar-dropdown").removeClass("active");
        $(this)
            .parent()
            .removeClass("active");
    } else {
        $(".sidebar-dropdown").removeClass("active");
        $(this)
            .next(".sidebar-submenu")
            .slideDown(200);
        $(this)
            .parent()
            .addClass("active");
    }
});

$("#close-sidebar").click(function () {
    $(".page-wrapper").removeClass("toggled");
});
$("#show-sidebar").click(function () {
    $(".page-wrapper").addClass("toggled");
});


scheappadmin.loadView = function (controller, action, callBackAfterViewLoad) {
    if (window.screen.width < 1024) {
        $("#divSidebarNavMain").removeClass("toggled");
    }
    $.ajax({
        type: "GET",
        url: '/'+controller+'/'+action,
        success: function (data) {
            $('#divScheAppContentDisplay').html(data);
            if (callBackAfterViewLoad != undefined) {
                callBackAfterViewLoad();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Sorry something went wrong. Please contact IT.")
        }
    });
}
scheappadmin.NotifyCustomer = function (scheAppId) {
    alert(scheAppId)
}

scheappadmin.GetTimeNumberFromDateTime = function (dt) {
    return Number(dt.getHours() + dt.getMinutes())
}

scheappadmin.ValidateStartAndEnd_DateTIme = function (sDTString, eDTString) {
    let isValid = false;
    let startDateTime = new Date(sDTString);
    let endDateTime = new Date(eDTString);
    if (startDateTime.getTime() > new Date().getTime()) {
        if (startDateTime !== "" && endDateTime != "") {
            if (startDateTime < endDateTime) {
                if (scheappadmin.GetTimeNumberFromDateTime(startDateTime) >= scheappadmin.GetTimeNumberFromDateTime(endDateTime)) {
                    Swal.fire({
                        title: "Start time cannot be equal or before end time.",
                        text: "Please enter start time before end time.",
                        icon: "error"
                    });
                } else {
                    isValid = true;
                }
            }
            else {
                Swal.fire({
                    title: "Start datetime cannot be equal or after end datetime.",
                    text: "Please enter start datetime before end datetime.",
                    icon: "error"
                });
                $("#btnSubmit_CreateSchedule").prop('disabled', true);
            }
        }
    } else {
        Swal.fire({
            title: "Start can not be in past.",
            text: "Please enter future start datetime.",
            icon: "error"
        });
    }
    return isValid;
}