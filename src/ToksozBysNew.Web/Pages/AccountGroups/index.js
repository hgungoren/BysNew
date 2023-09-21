$(function () {
    var l = abp.localization.getResource("ToksozBysNew");
	
	var accountGroupService = window.toksozBysNew.accountGroups.accountGroups;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "AccountGroups/CreateModal",
        scriptUrl: "/Pages/AccountGroups/createModal.js",
        modalClass: "accountGroupCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "AccountGroups/EditModal",
        scriptUrl: "/Pages/AccountGroups/editModal.js",
        modalClass: "accountGroupEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            accountGroupName: $("#AccountGroupNameFilter").val(),
            isUnitEnterable: (function () {
                var value = $("#IsUnitEnterableFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })()
        };
    };

    var dataTable = $("#AccountGroupsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(accountGroupService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ToksozBysNew.AccountGroups.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ToksozBysNew.AccountGroups.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    accountGroupService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "accountGroupName" },
            {
                data: "isUnitEnterable",
                render: function (isUnitEnterable) {
                    return isUnitEnterable ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
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

    $("#NewAccountGroupButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        accountGroupService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/account-groups/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'accountGroupName', value: input.accountGroupName }, 
                            { name: 'isUnitEnterable', value: input.isUnitEnterable }
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
