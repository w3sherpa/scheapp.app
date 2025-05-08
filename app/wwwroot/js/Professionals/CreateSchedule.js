var startDate;
var endDate;
var startTime;
var endTime;
function Validate_CreateSchedule() {
    startDateTime = $("#start-datetime-picker").val();
    endDateTime = $("#end-datetime-picker").val();
    if (startDateTime !== "" && endDateTime != "") {
        if (startDateTime < endDateTime)
            $("#btnSubmit_CreateSchedule").removeAttr("disabled");
        else {
            Swal.fire({
                title: "Start datetime cannot be equal or after end datetime.",
                text: "Please enter start datetime before end datetime.",
                icon: "error"
            });
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
                'success': function () {
                    Swal.fire({
                        position: "top-end",
                        icon: "success",
                        title: "New schedule created",
                        showConfirmButton: false,
                        timer: 1500
                    }).then(function () {
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

//full-calendar
flatpickr("#start-datetime-picker", {
    enableTime: true,
    dateFormat: "Y-m-d H:i",
    defaultDate: new Date(),
    onChange: function (selectedDates) {
        if (selectedDates.length > 0) {
            console.log(selectedDates[0].toLocaleString());
        }
    }
});
flatpickr("#end-datetime-picker", {
    enableTime: true,
    dateFormat: "Y-m-d H:i",
    defaultDate: new Date(),
    onChange: function (selectedDates) {
        if (selectedDates.length > 0) {
            console.log(selectedDates[0]);
        }
    }
});

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
        console.log(arg);
        $('#exampleModal').modal('show');

    },
    editable: false
});

calendar.render();

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
