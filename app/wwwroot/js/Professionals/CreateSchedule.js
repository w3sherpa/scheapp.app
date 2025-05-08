var startDate;
var endDate;
var startTime;
var endTime;
let startDTTouched = false;
let endDTTouched = false;

var today = new Date();

////Date Range Section

flatpickr("#start-datetime-picker", {
    enableTime: true,
    dateFormat: "Y-m-d H:i",
    defaultDate: today,
    //onChange: function (selectedDates) {
    //    if (selectedDates.length > 0) {
    //        console.log(selectedDates[0].toLocaleString());
    //    }
    //}
});
flatpickr("#end-datetime-picker", {
    enableTime: true,
    dateFormat: "Y-m-d H:i",
    defaultDate: today.setHours(today.getHours() + 1),
    //onChange: function (selectedDates) {
    //    if (selectedDates.length > 0) {
    //        console.log(selectedDates[0]);
    //    }
    //}
});

function Validate_CreateSchedule(type) {
    startDateTime = $("#start-datetime-picker").val();
    endDateTime = $("#end-datetime-picker").val();
    if (type == 'start') startDTTouched = true;
    else if (type == 'end') endDTTouched = true;
    if (startDTTouched && endDTTouched) {

        if (scheappadmin.ValidateStartAndEnd_DateTIme(startDateTime, endDateTime)) {
            $("#btnSubmit_CreateSchedule").prop('disabled', false);
        } else {
            $("#btnSubmit_CreateSchedule").prop('disabled', true);
        }
            
    }
}
function ConfirmAndCreate(proScheId) {
    Swal.fire({
        title: "Are you sure?",
        text: "You will be adding new availability to schedule appointment system!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, create it!"
    }).then((result) => {
        if (result.isConfirmed) {
            var reqObj = new Object();
            reqObj.BusinessId = $("#hdnBusinessId_CreateSchedule").val();
            reqObj.ProfessionalId = $("#hdnProfessionalId_CreateSchedule").val();
            reqObj.StartDateTime = startDateTime;
            reqObj.EndDateTime = endDateTime;
            reqObj.DaysOfWeek = '';
            console.log(reqObj)
            $.ajax({
                'url': '/ProfessionalsData/SaveProfessionalSchedules',
                'contentType': "application/json",
                'type': 'POST',
                'data': JSON.stringify(reqObj),
                'success': function (response) {
                    console.log(response)
                    let repsonseMessage = 'New schedule created';
                    let repsonseicon = 'success';
                    let isSuccess = true; 
                    if (response.status != 200) {
                        repsonseMessage = response.message;
                        repsonseicon = 'error';
                        isSuccess = false; 
                    }
                    Swal.fire({
                        position: "top-end",
                        icon: repsonseicon,
                        title: repsonseMessage,
                        showConfirmButton: false,
                        timer: 1500
                    }).then(function () {
                        if (isSuccess)
                        window.location.href = "/Professionals/Schedules?businessId=" + reqObj.BusinessId + "&professionalId=" + reqObj.ProfessionalId + ""
                    });
                },
                'error': function (request, error) {
                    alert("Request: " + JSON.stringify(request));
                }
            });
        }
    });
}


////Calendar Selection

let startCalendarTimeTouched = false;
let endCalendarTimeTouched = false;

//full-calendar

// Initial visible days
let visibleDays = [0, 1, 2, 3, 4, 5, 6];

// Initialize FullCalendar
let selectedDates = new Set();
let calendar = new FullCalendar.Calendar(document.getElementById('calendar'), {
    themeSystem: 'bootstrap',
    initialView: 'dayGridMonth',
    selectable: true,
    headerToolbar: {
        left: 'prev,next today',
        center: 'title',
        right: 'timeGridWeek,dayGridMonth'
    },
    slotMinTime: "07:00:00",
    slotMaxTime: "21:00:00",
    hiddenDays: [],
    selectable: true,
    unselectAuto: false,
    longPressDelay: 0,
    selectMirror: true,
    height: 'auto',
    eventClick: function (arg) {
        console.log('event clicked:' + new Date());
        var multipleDaysMode = $("#btnEndMultiDate").is(':visible');
        if (multipleDaysMode == true) {
            if (arg.event.title == 'multi-select') {
                arg.event.remove();
            } else {
                calendarObj.addEvent({
                    title: 'multi-select',
                    start: arg.event._instance.range.end
                    //end: arg.end
                });
            }

            calendarObj.unselect();
        } else {
            var startDate = arg.event._instance.range.start;
            var endDate = arg.event._instance.range.end;
            var dateInfo = getDateInfo(endDate);
            selectDatePopup($(calendarEl), startDate, startDate, null, dateInfo.LocationType, dateInfo.LocationValue, dateInfo.Purpose, dateInfo.OverlapBusinessMeal);
        }
    },
    select: function (arg) {

        ////minus 1 day from end day selection
        let endDate = arg.end;
        endDate.setDate(arg.end.getDate() - 1)
        const year = endDate.getFullYear();
        const month = String(endDate.getMonth() + 1).padStart(2, '0'); // Month is 0-indexed
        const day = String(endDate.getDate()).padStart(2, '0');
        //console.log(endDate.toString())
        //console.log(arg.startStr)

        let endDateStr = `${year}-${month}-${day}`;

        $("#btnValidatCalendarSelectionAndSubmit").prop('disabled', true);
        $("#start-time-picker").val('');
        $("#end-time-picker").val('');
        $('#spnCalendarStartDateSelection').text(arg.startStr)
        $('#spnCalendarEndDateSelection').text(endDateStr)
        $('#timePickerModal').modal('show');

    },
    editable: false
});

calendar.render();

var myModalEl = document.getElementById('timePickerModal');
myModalEl.addEventListener('hidden.bs.modal', function (event) {
    $("#start-time-picker").val('');
    $("#end-time-picker").val('');
});

function CancelCalendarSelection(){
    $("#start-time-picker").val('');
    $("#end-time-picker").val('');
    $('#timePickerModal').modal('hide');
}

///Time picker after calender selection not closing the dropdow, so following two event binding takes care of it.

let startTimePicker = document.getElementById("start-time-picker")
startTimePicker.addEventListener('input', updateStartTime);
function updateStartTime() {
    startTimePicker.type = 'text';
    startTimePicker.type = 'time';
}
let endTimePicker = document.getElementById("end-time-picker")
endTimePicker.addEventListener('input', updateEndTime);
function updateEndTime() {
    endTimePicker.type = 'text';
    endTimePicker.type = 'time';
}

///////////

function Validate_CalendarTimeSelection(type) {
    if (type == 'start') {
        startCalendarTimeTouched = true;
    }
    else if (type == 'end') {
        endCalendarTimeTouched = true;
    } 
    if (startCalendarTimeTouched && endCalendarTimeTouched) {
        $("#btnValidatCalendarSelectionAndSubmit").prop('disabled', false);
    }
}
function ValidatCalendarSelectionAndSubmit() {
    let cal_startTime = $("#start-time-picker").val();
    let cal_endTime = $("#end-time-picker").val();
    if (cal_startTime != '' && cal_endTime != '') {

        let cal_startDate = $('#spnCalendarStartDateSelection').text() + ' ' + cal_startTime;
        let cal_endDate = $('#spnCalendarEndDateSelection').text() + ' ' + cal_endTime;

        console.log(cal_startDate)
        console.log(cal_endDate)

        if (scheappadmin.ValidateStartAndEnd_DateTIme(cal_startDate, cal_endDate)) {
            alert('all good call api')
        } else {
            $('#timePickerModal').modal('show');
        }
    } else {
        Swal.fire({
            title: "Invalid form.",
            text: "Please enter start time and end time.",
            icon: "error"
        });
    }
}

// Handle day-of-week toggles
document.querySelectorAll('.day-toggle').forEach(checkbox => {
    checkbox.addEventListener('change', () => {
        visibleDays = Array.from(document.querySelectorAll('.day-toggle'))
            .filter(cb => cb.checked)
            .map(cb => parseInt(cb.value));

        const hiddenDays = [0, 1, 2, 3, 4, 5, 6].filter(d => !visibleDays.includes(d));
        calendar.setOption('hiddenDays', hiddenDays);
    });
});
