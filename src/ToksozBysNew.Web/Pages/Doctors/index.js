$(function () {
    var l = abp.localization.getResource("ToksozBysNew");
	
	var doctorService = window.toksozBysNew.doctors.doctors;
	
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
        viewUrl: abp.appPath + "Doctors/CreateModal",
        scriptUrl: "/Pages/Doctors/createModal.js",
        modalClass: "doctorCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Doctors/EditModal",
        scriptUrl: "/Pages/Doctors/editModal.js",
        modalClass: "doctorEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            isActive: (function () {
                var value = $("#IsActiveFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
			nameSurname: $("#NameSurnameFilter").val(),
			pharmacyName: $("#PharmacyNameFilter").val(),
			positionId: $("#PositionIdFilter").val(),			specId: $("#SpecIdFilter").val(),			customerTitleId: $("#CustomerTitleIdFilter").val(),			unitId: $("#UnitIdFilter").val(),			customerTypeId: $("#CustomerTypeIdFilter").val()
        };
    };

    var dataTable = $("#DoctorsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(doctorService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ToksozBysNew.Doctors.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.doctor.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ToksozBysNew.Doctors.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    doctorService.delete(data.record.doctor.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            },
                            {
                                text: l("Detail"),
                                action: function (data) {
                                    let id = data.record.doctor.id; 
                                    window.location.href = "/Doctors/Detail?id=" + id + "";
                                }
                            }
                        ]
                }
            },
			{
                data: "doctor.isActive",
                render: function (isActive) {
                    return isActive ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
			{ data: "doctor.nameSurname" },
			{ data: "doctor.pharmacyName" },
            {
                data: "position.positionName",
                defaultContent : ""
            },
            {
                data: "spec.specName",
                defaultContent : ""
            },
            {
                data: "customerTitle.titleName",
                defaultContent : ""
            },
            {
                data: "unit.unitName",
                defaultContent : ""
            },
            {
                data: "customerType.typeName",
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

    $("#NewDoctorButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        doctorService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/doctors/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'isActive', value: input.isActive }, 
                            { name: 'nameSurname', value: input.nameSurname }, 
                            { name: 'pharmacyName', value: input.pharmacyName }, 
                            { name: 'positionId', value: input.positionId }
, 
                            { name: 'specId', value: input.specId }
, 
                            { name: 'customerTitleId', value: input.customerTitleId }
, 
                            { name: 'unitId', value: input.unitId }
, 
                            { name: 'customerTypeId', value: input.customerTypeId }
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
