
let businessIdFromHdnInput = $('#hdnBusinessId').val();

const $table = $('#table')
const $remove = $('#remove')
let selections = []

function getIdSelections() {
    return $.map($table.bootstrapTable('getSelections'), function (row) {
        return row.id
    })
}

////this method get called first after the api result comes back, and adds state checkboxs
window.responseHandler = res => {
    // console.log(res);
    $.each(res.rows, function (i, row) {
        row.state = $.inArray(row.id, selections) !== -1
    })
    // console.log(res)
    return res
}

window.detailFormatter = (index, row) => {
    const html = []
    $.each(row, function (key, value) {
        html.push(`<p><b>${key}:</b> ${value}</p>`)
    })
    return html.join('')
}

function operateFormatter() {
    return [
        '<a class="scheapp-edit-action btn btn-outline-warning" href="javascript:void(0)" title="Edit">',
        '<i class="fa fa-edit"></i>',
        '</a>'
    ].join('')
}

window.operateEvents = {
    'click .scheapp-edit-action'(e, value, row) {
        alert(`show editing modal`)
    }
}

function totalTextFormatter() {
    return 'Total'
}

function totalNameFormatter(data) {
    return data.length
}

function totalPriceFormatter(data) {
    const field = this.field

    return `$${data.map(function (row) {
        return +row[field].substring(1)
    }).reduce(function (sum, i) {
        return sum + i
    }, 0)}`
}

function initTable() {
    $table.bootstrapTable('destroy').bootstrapTable({
        height: 900,
        pageSize: 30,
        width: 1000,
        locale: "en-US", //language US English
        columns: [
            [
                {
                    field: 'state',
                    checkbox: true,
                    rowspan: 2,
                    align: 'center',
                    valign: 'middle'
                },
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
                    title: 'Schedule Detail',
                    colspan: 5,
                    align: 'center'
                }
            ],
            [
                {
                    field: 'startDate',
                    title: 'Start Date',
                    sortable: true,
                    footerFormatter: totalNameFormatter,
                    align: 'center'
                },
                {
                    field: 'startTime',
                    title: 'Start Time',
                    align: 'center'
                },
                {
                    field: 'endDate',
                    title: 'End Date',
                    align: 'center',
                    sortable: true,
                },
                {
                    field: 'endTime',
                    title: 'End Time',
                    align: 'center',
                    sortable: true,
                },
                {
                    field: 'operate',
                    title: 'Edit Schedule',
                    align: 'center',
                    clickToSelect: false,
                    events: window.operateEvents,
                    formatter: operateFormatter
                }
            ]
        ]
    })
    $table.on('check.bs.table uncheck.bs.table ' +
        'check-all.bs.table uncheck-all.bs.table',
        function () {
            $remove.prop('disabled', !$table.bootstrapTable('getSelections').length)

            // save your data, here just save the current page
            selections = getIdSelections();
            console.log(selections);
            // push or splice the selections if you want to save all data selections
        })

    // //uncomment following to see how table is build
    // $table.on('all.bs.table', function (e, name, args) {
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