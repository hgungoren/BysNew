$(function () {
    var l = abp.localization.getResource("ToksozBysNew");
	
	var expenseMonthlyService = window.toksozBysNew.expenseMonthlies.expenseMonthlies;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "ExpenseMonthlies/CreateModal",
        scriptUrl: "/Pages/ExpenseMonthlies/createModal.js",
        modalClass: "expenseMonthlyCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "ExpenseMonthlies/EditModal",
        scriptUrl: "/Pages/ExpenseMonthlies/editModal.js",
        modalClass: "expenseMonthlyEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            accountId: $("#AccountIdFilter").val(),
			accountGroup: $("#AccountGroupFilter").val(),
			account: $("#AccountFilter").val(),
			department: $("#DepartmentFilter").val(),
			expenseType: $("#ExpenseTypeFilter").val(),
			product: $("#ProductFilter").val(),
			proje: $("#ProjeFilter").val(),
			comment: $("#CommentFilter").val(),
			month: $("#MonthFilter").val(),
			yearMin: $("#YearFilterMin").val(),
			yearMax: $("#YearFilterMax").val(),
			unitMin: $("#UnitFilterMin").val(),
			unitMax: $("#UnitFilterMax").val(),
			unitValueMin: $("#UnitValueFilterMin").val(),
			unitValueMax: $("#UnitValueFilterMax").val(),
			amountMin: $("#AmountFilterMin").val(),
			amountMax: $("#AmountFilterMax").val(),
			memoMin: $("#MemoFilterMin").val(),
			memoMax: $("#MemoFilterMax").val(),
			invoice: $("#InvoiceFilter").val(),
			remainMin: $("#RemainFilterMin").val(),
			remainMax: $("#RemainFilterMax").val()
        };
    };

    var dataTable = $("#ExpenseMonthliesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(expenseMonthlyService.getExpensesByMonth),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ToksozBysNew.ExpenseMonthlies.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ToksozBysNew.ExpenseMonthlies.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    expenseMonthlyService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "accountId" },
			{ data: "accountGroup" },
			{ data: "account" },
			{ data: "department" },
			{ data: "expenseType" },
			{ data: "product" },
			{ data: "proje" },
			{ data: "comment" },
			{ data: "month" },
			{ data: "year" },
			{ data: "unit" },
			{ data: "unitValue" },
			{ data: "amount" },
			{ data: "memo" },
			{ data: "invoice" },
			{ data: "remain" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewExpenseMonthlyButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        expenseMonthlyService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/expense-monthlies/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'accountId', value: input.accountId }, 
                            { name: 'accountGroup', value: input.accountGroup }, 
                            { name: 'account', value: input.account }, 
                            { name: 'department', value: input.department }, 
                            { name: 'expenseType', value: input.expenseType }, 
                            { name: 'product', value: input.product }, 
                            { name: 'proje', value: input.proje }, 
                            { name: 'comment', value: input.comment }, 
                            { name: 'month', value: input.month },
                            { name: 'yearMin', value: input.yearMin },
                            { name: 'yearMax', value: input.yearMax },
                            { name: 'unitMin', value: input.unitMin },
                            { name: 'unitMax', value: input.unitMax },
                            { name: 'unitValueMin', value: input.unitValueMin },
                            { name: 'unitValueMax', value: input.unitValueMax },
                            { name: 'amountMin', value: input.amountMin },
                            { name: 'amountMax', value: input.amountMax },
                            { name: 'memoMin', value: input.memoMin },
                            { name: 'memoMax', value: input.memoMax }, 
                            { name: 'invoice', value: input.invoice },
                            { name: 'remainMin', value: input.remainMin },
                            { name: 'remainMax', value: input.remainMax }
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
