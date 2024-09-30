Support for ASP.NET Core Identity was added to your project.

For setup and configuration information, see https://go.microsoft.com/fwlink/?linkid=2116645.


/create hub class
 public class StatusUpdateHub : Hub
 {
     //public async Task SendMessage(string conversation_uuid, string status)
     //{
     //    await Clients.All.SendAsync("ReceiveMessage", conversation_uuid, status);
     //}
 }
 
 
// inject hub in controller
 private readonly IHubContext<StatusUpdateHub> _hub;
 
 
 //send message to clint
 await _hub.Clients.All.SendAsync("UpdateStatus", voiceApiEvent.conversation_uuid, voiceApiEvent.status);
 
 
 //include these in view files
   var connection = new signalR.HubConnectionBuilder().withUrl("/StatusUpdateHub").build();
  connection.on("UpdateStatus", function (conversation_uuid, status) {
      $('#' + conversation_uuid).html(status);
      if (status == 'completed')
          loadOpsHubRoboCallMessages(true,'*', 'tblCompletedAndIncompleteTripCalls', false, true, true,false,false);
      ////debugger;
      //$('#' + conversation_uuid + '-' + status).show();
      //if (status == 'completed') $('#' + conversation_uuid + '-loader').hide();
  });
  connection.start();
 
   
function loadOpsHubRoboCallMessages(isAdminView,username, tableId, inprogress, completed, incomplete,testOnly,snoozedOnly=false) {

    var datatable = $('#' + tableId)
        .DataTable({
            ajax: {
                url: '/Admin/GetOpsHubRoboCallMessage',
                type: 'POST',
                data: function (req) {
                    var body = {};
                    body.Username = username;
                    body.GetInProgressStatus = inprogress;
                    body.GetCompletedStatus = completed;
                    body.GetInCompleteStatus = incomplete;
                    body.GetTestMessagesOnly = testOnly;
                    body.GetSnoozedMessagesOnly = snoozedOnly;
                    body.sortColumn = req.columns[req.order[0]?.column]?.name;
                    body.sortOrder = req.order[0]?.dir?.toUpperCase();
                    body.pageSize = req.length;
                    body.pageNumber = (req.start / req.length) + 1;
                    return body;
                },
                dataSrc: function (res) {
                    console.log(res);
                    if (res && res.opsHubRoboCallMessages && res.opsHubRoboCallMessages.length > 0) {
                        res.recordsTotal = res.recordCount;
                        res.recordsFiltered = res.recordCount;
                        res.data = res.opsHubRoboCallMessages;
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
                    "data": "empNo",
                    "name": "Emp No",
                    "render": function (data) {
                        return '<a id="' + data + '" class="btn btn-link" href="/Admin/UserPrefsAndLogs?username=' + data + '"' + '>' + data + '</a>'
                    },
                    visible: isAdminView
                },
                {
                    "data": "empNo",
                    "name": "View Prefs",
                    "render": function (data, type, full, meta) {
                        return '<button class="btn" onclick="viewUserPreferenceSettings(' + "'" + full.empNo + "'" + ');"><span  class="material-icons">visibility</span></button > '
                    },
                    visible: isAdminView
                },
                {
                    "data": "phoneCallStatus",
                    "name": "Status",
                    "render": function (data, type, full, meta) {
                        if (full.phoneCallDetails == "" || full.phoneCallDetails == null || full.phoneCallDetails == undefined) {
                            return full.phoneCallStatus
                        } else {
                            return full.phoneCallStatus + " - " + full.phoneCallDetails;
                        }
                    }
                },
                {
                    "data": "receiverNumber",
                    "name": "Phone"
                },
                {
                    "data": "seqNumber",
                    "name": "Sequence"
                },
                {
                    "data": "originAirport",
                    "name": "Origin Airport"
                },
                {
                    "data": "addCode",
                    "name": "Add Code"
                },
                {
                    "data": "flightDepartureUTCDatetime",
                    "name": "Flight",
                    "render": function (data, type, full, meta) {
                        return full.addCode == "TEST" ? "" : data;
                    }
                },
                {
                    "data": "sourceTimeStamp",
                    "name": "SourceTimeStamp",
                    "render": function (data, type, full, meta) {
                        return full.addCode == "TEST" ? "" : data;
                    }
                }
                ,
                {
                    "data": "createdDateTime",
                    "name": "APA ProcessedUTC",
                    "render": function (data, type, full, meta) {
                        return full.addCode == "TEST" ? "" : data;
                    }
                },
                {
                    "data": "messageCapturedDateTime",
                    "name": "RobocallProcessed"
                },
                {
                    "data": "callMadeAt",
                    "name": "Call Made at"
                },
                {
                    "data": "isRequeued",
                    "name": "Snoozed",
                    "render": function (data) {
                        return data == true ? "Y" : "N";
                    }
                }
            ]
        });
}

