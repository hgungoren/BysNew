$(function () {
    var l = abp.localization.getResource("ToksozBysNew");
	
	var budgetService = window.toksozBysNew.budgets.budgets;
	
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
        viewUrl: abp.appPath + "Budgets/CreateModal",
        scriptUrl: "/Pages/Budgets/createModal.js",
        modalClass: "budgetCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Budgets/EditModal",
        scriptUrl: "/Pages/Budgets/editModal.js",
        modalClass: "budgetEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            budgetName: $("#BudgetNameFilter").val(),
			yearMin: $("#YearFilterMin").val(),
			yearMax: $("#YearFilterMax").val(),
			comment: $("#CommentFilter").val(),
            isActive: (function () {
                var value = $("#IsActiveFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
			openUntilMin: $("#OpenUntilFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			openUntilMax: $("#OpenUntilFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			companyId: $("#CompanyIdFilter").val()
        };
    };

    var dataTable = $("#BudgetsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(budgetService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ToksozBysNew.Budgets.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.budget.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ToksozBysNew.Budgets.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    budgetService.delete(data.record.budget.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "budget.budgetName" },
			{ data: "budget.year" },
			{ data: "budget.comment" },
            {
                data: "budget.isActive",
                render: function (isActive) {
                    return isActive ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "budget.openUntil",
                render: function (openUntil) {
                    if (!openUntil) {
                        return "";
                    }
                    
					var date = Date.parse(openUntil);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
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

    $("#NewBudgetButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        budgetService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/budgets/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'budgetName', value: input.budgetName },
                            { name: 'yearMin', value: input.yearMin },
                            { name: 'yearMax', value: input.yearMax }, 
                            { name: 'comment', value: input.comment }, 
                            { name: 'isActive', value: input.isActive },
                            { name: 'openUntilMin', value: input.openUntilMin },
                            { name: 'openUntilMax', value: input.openUntilMax }, 
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
