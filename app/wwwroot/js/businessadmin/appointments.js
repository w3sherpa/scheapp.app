let businessIdFromHdnInput = $('#hdnBusinessId').val();
$(function () {
    console.log('hi')
    console.log(businessIdFromHdnInput)
    $("#chkbxAppointmentsToday").prop("checked", true);
    LoadScheduledAppointments(businessIdFromHdnInput, GetToday());
});

var connection = new signalR.HubConnectionBuilder().withUrl("/ScheAppViewUpdateHub"
    // ,{  
    //     // skipNegotiation: true,
    //     // transport: signalR.HttpTransportType.WebSockets
    //     transport: signalR.HttpTransportType.WebSockets | signalR.HttpTransportType.ServerSentEvents | signalR.HttpTransportType.LongPolling,
    // }
).build();
connection.on("UpdateAppointmentsView", function (conversation_uuid, status) {
    LoadScheduledAppointments(businessIdFromHdnInput, GetToday());
});
connection.start();
function TodayCheckboxCheck() {
    $("#chkbxAppointmentsAllDate").prop("checked", false);
    LoadScheduledAppointments(businessIdFromHdnInput, GetToday());
}
function AllDateCheckboxCheck() {
    $("#chkbxAppointmentsToday").prop("checked", false);
    LoadScheduledAppointments(businessIdFromHdnInput, null);
}
function AppointmentsFilterByDate(chkboxID) {
    $("#chkbxAppointmentsToday").prop("checked", false);
    $("#chkbxAppointmentsAllDate").prop("checked", false);
    let selDate = $("#txtbxAppointmentsSelectedDate").val();
    LoadScheduledAppointments(businessIdFromHdnInput, selDate);
}

function GetAppointmentDispalyCard(appointment) {
    var newAppoitmentCardHtml =
        '<div class="col-md-6 mt-3 col-lg-4 column">' +
            '<div class="card" style="border-radius:3.25rem;">' +
                '<div class="txt">' +
                    '<div class="row">' +
                        '<div class="col-3">' +
                            '<div class="user-pic">' +
                                '<img class="img-responsive img-rounded" src="/images/user-01.jpg" alt="User picture">' +
                            '</div>' +
                        '</div>' +
                        '<div class="col-9 d-flex flex-column">' +
                            '<div class="d-block"><h6 class="float-start">' + appointment.customer + '</h6>' +
                                '<span style="margin-right:15px;"><span style="margin-right:5px;">' + appointment.serviceName + '</span>' +
                                '<i class="fa fa-cut ml-2"></i></span>' +
                            '</div>' +
                            '<div class="d-block">' +
                                '<span class="float-start">' + appointment.startDT + '</span>' +   
                                '<a style="margin-right:15px;" href="#" onclick="scheappadmin.NotifyCustomer(' + appointment.scheduleAppointmentId + ')">I am ready</a>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
            '</div>' +
        '</div>'
    return newAppoitmentCardHtml;
}
function LoadScheduledAppointments(businessId, appointmentsDate) {

    var reqObj = new Object();
    reqObj.BusinessId = businessId;
    reqObj.AppointmentDate = appointmentsDate; GetToday();
    $.ajax({
        'url': '/AdminData/GetProfessionalScheduleAppointmentRequestsDetails',
        'contentType': "application/json",
        'type': 'POST',
        'data': JSON.stringify(reqObj),
        'success': function (response) {
            let apppointmentCardsHTML = '';
            response.appointments.forEach((a, i) => apppointmentCardsHTML += GetAppointmentDispalyCard(a))
            $("#div_DisplayAppointmentList").html(apppointmentCardsHTML)
        },
        'error': function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}

function GetToday() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    // return today;
    return yyyy + '-' + mm + '-' + dd;
}