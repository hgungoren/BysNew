$(function () {
    var l = abp.localization.getResource("ToksozBysNew");
	
	var departmentService = window.toksozBysNew.departments.departments;
	
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
        viewUrl: abp.appPath + "Departments/CreateModal",
        scriptUrl: "/Pages/Departments/createModal.js",
        modalClass: "departmentCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Departments/EditModal",
        scriptUrl: "/Pages/Departments/editModal.js",
        modalClass: "departmentEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            departmentName: $("#DepartmentNameFilter").val(),
			companyId: $("#CompanyIdFilter").val()
        };
    };

    var dataTable = $("#DepartmentsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(departmentService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ToksozBysNew.Departments.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.department.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ToksozBysNew.Departments.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    departmentService.delete(data.record.department.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "department.departmentName" },
            {
                data: "company.companyName",
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

    $("#NewDepartmentButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        departmentService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/departments/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'departmentName', value: input.departmentName }, 
                            { name: 'companyId', value: input.companyId }
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
