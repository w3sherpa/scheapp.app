var scheappadmin = scheappadmin || {};

document.addEventListener("DOMContentLoaded", function () {
    //scheappadmin.LoadScheduledAppointments(2);


    $.ajax({
        'url': '/Admin/GetMessage?name=padat',
        'type': 'GET',
        'contentType': 'application/json',
        'success': function (response) {
            console.log(response);
        },
        'error': function (request, error) {
            console.log(error)
        }
    });
});
scheappadmin.LoadScheduledAppointments = function (businessId) {

    var isAdminView = true;

    var datatable = $('#tblScheApp_BusinessAdminAppointments')
        .DataTable({
            ajax: {
                url: '/Admin/GetProfessionalScheduleAppointmentRequestsDetails',
                type: 'POST',
                contentType : 'application/json',
                data: function (req) {
                    var body = {};
                    body.BusinessId = businessId;
                    body.sortColumn = req.columns[req.order[0]?.column]?.name;
                    body.sortOrder = req.order[0]?.dir?.toUpperCase();
                    body.pageSize = req.length;
                    body.pageNumber = (req.start / req.length) + 1;
                    return body;
                },
                dataSrc: function (res) {
                    console.log(res);
                    //if (res && res.opsHubRoboCallMessages && res.opsHubRoboCallMessages.length > 0) {
                    //    res.recordsTotal = res.recordCount;
                    //    res.recordsFiltered = res.recordCount;
                    //    res.data = res.opsHubRoboCallMessages;
                    //    return res.data;
                    //} else {
                    //    res.recordsTotal = 0;
                    //    res.recordsFiltered = 0;
                    //    res.data = [];

                    //    return res;
                    //}
                }
            },
            "fnInitComplete": function (oSettings, json) {
                //fixTableUI();
            },
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": false,
            "responsive": true,
            "bSort": false,
            paging: true,
            searching: false,
            destroy: true,
            "language": {
                "emptyTable": "No record found.",
                //"processing": busyLoader
            },
            "columns": [
                {
                    "data": "id",
                    "name": "Id",
                    "render": function (data) {
                        return '<a id="' + data + '" class="btn btn-link" href="/Admin/UserPrefsAndLogs?username=' + data + '"' + '>' + data + '</a>'
                    },
                    visible: isAdminView
                },
                {
                    "data": "startDT",
                    "name": "Starts"
                },
                {
                    "data": "endDT",
                    "name": "Ends"
                },
                {
                    "data": "professional",
                    "name": "Professional"
                },
                {
                    "data": "service",
                    "name": "Service"
                },
                {
                    "data": "customer",
                    "name": "Customer"
                },
                {
                    "data": "requestDate",
                    "name": "Requested Date"
                },
                {
                    "data": "professionalConfirmed",
                    "name": "ProfessionalConfirmed",
                },
                {
                    "data": "customerConfirmed",
                    "name": "CustomerConfirmed",
                }
            ]
        });
}