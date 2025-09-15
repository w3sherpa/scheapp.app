
let businessIdFromHdnInput = $('#hdnBusinessId').val();

const $professinalsTable = $('#professinalsTable')
const $remove = $('#remove')
let selections = []

function getIdSelections() {
    return $.map($professinalsTable.bootstrapTable('getSelections'), function (row) {
        return row.id
    })
}

////this method get called first after the api result comes back, and adds state checkboxs
//window.responseHandler = res => {
//    // console.log(res);
//    $.each(res.rows, function (i, row) {
//        row.state = $.inArray(row.id, selections) !== -1
//    })
//    // console.log(res)
//    return res
//}

window.detailFormatter = (index, row) => {
    const html = []
    $.each(row, function (key, value) {
        html.push(`<p><b>${key}:</b> ${value}</p>`)
    })
    return html.join('')
}

function operateFormatter() {
    return [
        '<a class="scheapp-schedules-action btn btn-outline-info me-2" href="javascript:void(0)" title="Schedules">',
        '<i class="fa fa-calendar"></i>',
        '</a>  ',
        '<a class="scheapp-services-action btn btn-outline-info me-2" href="javascript:void(0)" title="Services">',
        '<i class="fa fa-wrench"></i>',
        '</a> ',
        '<a class="scheapp-edit-action btn btn-outline-warning me-2" href="javascript:void(0)" title="Edit">',
        '<i class="fa fa-edit"></i>',
        '</a>',
        '<a class="scheapp-delete-action btn btn-outline-danger" href="javascript:void(0)" title="Delete">',
        '<i class="fa fa-trash"></i>',
        '</a>  ',

    ].join('')
}

window.operateEvents = {
    'click .scheapp-edit-action'(e, value, row) {
        alert(`You click like action, row: ${JSON.stringify(row)}`)
    },
    'click .scheapp-delete-action'(e, value, row) {
        alert('confirm delete ' + row.id)
    },
    'click .scheapp-schedules-action'(e, value, row) {
        window.open('/Professionals/Schedules?businessId=' + row.businessId + '&professionalId=' + row.id, "_blank");
    },
    'click .scheapp-services-action'(e, value, row) {
        try {
            console.log(businessIdFromHdnInput)
            fetch("/ServicesData/GetServicesByBusinessId", GetScheAppPOSTFetchObject({ BusinessId: businessIdFromHdnInput }))
                .then(response => response.json())
                .then(data => {
                    fetch("/ProfessionalsData/GetServicesByProfessionalId", GetScheAppPOSTFetchObject({ ProfessionalId: row.id }))
                        .then(response2 => response2.json())
                        .then(data2 => {
                            console.log(data2)
                            $('#addServicesToProfessional').modal('toggle');
                        }
                    );
                }
            );
            
        } catch (error) {
            console.error(error.message);
        }
        $remove.prop('disabled', true)
    }
}

function totalTextFormatter() {
    return 'Total'
}

function totalNameFormatter(data) {
    return data.length
}

function initTable() {
    console.log('professional table created')
    $professinalsTable.bootstrapTable('destroy').bootstrapTable({
        height: 600,
        pageSize: 10,
        width: 1000,
        locale: "en-US", //language US English
        columns: [
            [
                {
                    title: 'ID',
                    field: 'id',
                    rowspan: 2,
                    align: 'center',
                    valign: 'middle',
                    sortable: true,
                    footerFormatter: totalTextFormatter
                },
                {
                    title: 'Professional',
                    colspan: 5,
                    align: 'center'
                }
            ],
            [
                {
                    field: 'firstName',
                    title: 'First Name',
                    sortable: true,
                    footerFormatter: totalNameFormatter,
                    align: 'center'
                },
                {
                    field: 'middleName',
                    title: 'Middle Name',
                    align: 'center'
                },
                {
                    field: 'lastName',
                    title: 'Last Name',
                    align: 'center',
                    sortable: true,
                },
                {
                    field: 'isActive',
                    title: 'Active',
                    align: 'center',
                    sortable: true,
                },
                {
                    field: 'operate',
                    title: 'Edit And More',
                    align: 'center',
                    clickToSelect: false,
                    events: window.operateEvents,
                    formatter: operateFormatter
                }
            ]
        ]
    })
    $professinalsTable.on('load-error.bs.table', function (e, status, res) {
       
        if (res && res.status === 404) {
            console.log('laksdjfalskdfjasldfkjasldfkjasdlfkjsdf')
        } else {
            $('#errorContainer').html(`
            <div class="alert alert-danger" role="alert">
                Failed to load data: ${status}
            </div>
        `);
        }
    });
    $professinalsTable.on('check.bs.table uncheck.bs.table ' +
        'check-all.bs.table uncheck-all.bs.table',
        function () {
            $remove.prop('disabled', !$professinalsTable.bootstrapTable('getSelections').length)

            // save your data, here just save the current page
            selections = getIdSelections();
            console.log(selections);
            // push or splice the selections if you want to save all data selections
        })

    // //uncomment following to see how table is build
    // $professinalsTable.on('all.bs.table', function (e, name, args) {
    //   console.log(name, args)
    // })

    ////Call when Delete putton is clicked after selecting the checkboxes
    $remove.click(async function () {

        Swal.fire({
            title: "Are you sure?",
            text: "You want to delete the selected schedule",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then(async (result) => {
            if (result.isConfirmed) {
                const ids = getIdSelections()
                const url = "/ProfessionalsData/DeleteProfessionalSchedules";
                try {
                    console.log(GetScheAppPOSTFetchObject({ BusinessId: 3, ProfessionalScheduleIds: ids }))
                    const response = await fetch(url, GetScheAppPOSTFetchObject({ BusinessId: businessIdFromHdnInput, ProfessionalScheduleIds: ids }));
                    if (!response.ok) {
                        throw new Error(`Response status: ${response.status}`);
                    }
                    //const json = await response.json();
                    //console.log(json);

                    window.location.reload();
                } catch (error) {
                    console.error(error.message);
                }
                $remove.prop('disabled', true)
            }
        });
    })
}

$(function () {
    initTable()

    $('#locale').change(initTable)
})