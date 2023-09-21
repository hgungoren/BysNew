$(function () {
    var l = abp.localization.getResource("ToksozBysNew");
	
	var companyCalendarService = window.toksozBysNew.companyCalendars.companyCalendars;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CompanyCalendars/CreateModal",
        scriptUrl: "/Pages/CompanyCalendars/createModal.js",
        modalClass: "companyCalendarCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CompanyCalendars/EditModal",
        scriptUrl: "/Pages/CompanyCalendars/editModal.js",
        modalClass: "companyCalendarEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            companyCalendarDateMin: $("#CompanyCalendarDateFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			companyCalendarDateMax: $("#CompanyCalendarDateFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            isWeekend: (function () {
                var value = $("#IsWeekendFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            isHoliday: (function () {
                var value = $("#IsHolidayFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })()
        };
    };

    var dataTable = $("#CompanyCalendarsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(companyCalendarService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ToksozBysNew.CompanyCalendars.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ToksozBysNew.CompanyCalendars.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    companyCalendarService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{
                data: "companyCalendarDate",
                render: function (companyCalendarDate) {
                    if (!companyCalendarDate) {
                        return "";
                    }
                    
					var date = Date.parse(companyCalendarDate);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
            {
                data: "isWeekend",
                render: function (isWeekend) {
                    return isWeekend ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "isHoliday",
                render: function (isHoliday) {
                    return isHoliday ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewCompanyCalendarButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        companyCalendarService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/company-calendars/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText },
                            { name: 'companyCalendarDateMin', value: input.companyCalendarDateMin },
                            { name: 'companyCalendarDateMax', value: input.companyCalendarDateMax }, 
                            { name: 'isWeekend', value: input.isWeekend }, 
                            { name: 'isHoliday', value: input.isHoliday }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
    
    
});
