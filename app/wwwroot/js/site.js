var scheappadmin = scheappadmin || {};

document.addEventListener("DOMContentLoaded", function () {
    scheappadmin.LoadScheduledAppointments(2);


    $.ajax({
        'url': '/AdminData/GetMessage?name=padat',
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
                url: '/AdminData/GetProfessionalScheduleAppointmentRequestsDetails',
                type: 'POST',
                contentType: 'application/json',
                dataType:'json',
                data: function (req) {
                    var body = {};
                    body.BusinessId = businessId;
                    body.SortColumn = "";// req.columns[req.order[0]?.column]?.name;
                    body.SortOrder = "";//req.order[0]?.dir?.toUpperCase();
                    body.PageSize = req.length;
                    body.PageNumber = (req.start / req.length) + 1;
                    return JSON.stringify(body);
                },
                dataSrc: function (res) {
                    
                    if (res && res.records.length > 0) {
                        console.log(res);
                        res.recordsTotal = res.recordsCount;
                        res.data = res.records;
                        return res.data;
                    } else {
                        res.recordsTotal = 0;
                        res.recordsFiltered = 0;
                        res.data = [];

                        return res;
                    }
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
                    "data": "scheduleAppointmentId",
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