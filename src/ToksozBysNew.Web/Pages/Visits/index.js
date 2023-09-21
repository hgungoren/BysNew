$(function () {
    var l = abp.localization.getResource("ToksozBysNew");
	
	var visitService = window.toksozBysNew.visits.visits;
	
        var lastNpIdId = '';
        var lastNpDisplayNameId = '';

        var _lookupModal = new abp.ModalManager({
            viewUrl: abp.appPath + "Shared/LookupModal",
            scriptUrl: "/Pages/Shared/lookupModal.js",
            modalClass: "navigationPropertyLookup"
        });

        $('.lookupCleanButton').on('click', '', function () {
            $(this).parent().find('input').val('');
        });

        _lookupModal.onClose(function () {
            var modal = $(_lookupModal.getModal());
            $('#' + lastNpIdId).val(modal.find('#CurrentLookupId').val());
            $('#' + lastNpDisplayNameId).val(modal.find('#CurrentLookupDisplayName').val());
        });
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Visits/CreateModal",
        scriptUrl: "/Pages/Visits/createModal.js",
        modalClass: "visitCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Visits/EditModal",
        scriptUrl: "/Pages/Visits/editModal.js",
        modalClass: "visitEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            visitDateMin: $("#VisitDateFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			visitDateMax: $("#VisitDateFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			visitNotes: $("#VisitNotesFilter").val(),
			doctorId: $("#DoctorIdFilter").val(),			unitId: $("#UnitIdFilter").val(),			clinicId: $("#ClinicIdFilter").val(),			brickId: $("#BrickIdFilter").val(),			identityUserId: $("#IdentityUserIdFilter").val(),			specId: $("#SpecIdFilter").val()
        };
    };

    var dataTable = $("#VisitsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(visitService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ToksozBysNew.Visits.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.visit.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ToksozBysNew.Visits.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    visitService.delete(data.record.visit.id)
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
                data: "visit.visitDate",
                render: function (visitDate) {
                    if (!visitDate) {
                        return "";
                    }
                    
					var date = Date.parse(visitDate);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
			{ data: "visit.visitNotes" },
            {
                data: "doctor.nameSurname",
                defaultContent : ""
            },
            {
                data: "unit.unitName",
                defaultContent : ""
            },
            {
                data: "clinic.clinicName",
                defaultContent : ""
            },
            {
                data: "brick.brickName",
                defaultContent : ""
            },
            {
                data: "identityUser.userName",
                defaultContent : ""
            },
            {
                data: "spec.specCode",
                defaultContent : ""
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewVisitButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        visitService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/visits/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText },
                            { name: 'visitDateMin', value: input.visitDateMin },
                            { name: 'visitDateMax', value: input.visitDateMax }, 
                            { name: 'visitNotes', value: input.visitNotes }, 
                            { name: 'doctorId', value: input.doctorId }
, 
                            { name: 'unitId', value: input.unitId }
, 
                            { name: 'clinicId', value: input.clinicId }
, 
                            { name: 'brickId', value: input.brickId }
, 
                            { name: 'identityUserId', value: input.identityUserId }
, 
                            { name: 'specId', value: input.specId }
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
