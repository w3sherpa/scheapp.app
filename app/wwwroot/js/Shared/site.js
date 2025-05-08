
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