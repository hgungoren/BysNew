$(function () {
    var l = abp.localization.getResource("ToksozBysNew");

    var budgetDistributionService = window.toksozBysNew.budgetDistributions.budgetDistributions;

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
        viewUrl: abp.appPath + "BudgetDistributions/CreateModal",
        scriptUrl: "/Pages/BudgetDistributions/createModal.js",
        modalClass: "budgetDistributionCreate"
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "BudgetDistributions/EditModal",
        scriptUrl: "/Pages/BudgetDistributions/editModal.js",
        modalClass: "budgetDistributionEdit"
    });

    var getFilter = function () {
        return {
            filterText: $("#FilterText").val(),
            costCenter: $("#CostCenterFilter").val(),
            expenseType: $("#ExpenseTypeFilter").val(),
            projectItemMin: $("#ProjectItemFilterMin").val(),
            projectItemMax: $("#ProjectItemFilterMax").val(),
            typeMin: $("#TypeFilterMin").val(),
            typeMax: $("#TypeFilterMax").val(),
            unitMin: $("#UnitFilterMin").val(),
            unitMax: $("#UnitFilterMax").val(),
            unitValueMin: $("#UnitValueFilterMin").val(),
            unitValueMax: $("#UnitValueFilterMax").val(),
            monthMin: $("#MonthFilterMin").val(),
            monthMax: $("#MonthFilterMax").val(),
            yearMin: $("#YearFilterMin").val(),
            yearMax: $("#YearFilterMax").val(),
            ratioMin: $("#RatioFilterMin").val(),
            ratioMax: $("#RatioFilterMax").val(),
            amountMin: $("#AmountFilterMin").val(),
            amountMax: $("#AmountFilterMax").val(),
            memoMin: $("#MemoFilterMin").val(),
            memoMax: $("#MemoFilterMax").val(),
            invoiceMin: $("#InvoiceFilterMin").val(),
            invoiceMax: $("#InvoiceFilterMax").val(),
            currencyMin: $("#CurrencyFilterMin").val(),
            currencyMax: $("#CurrencyFilterMax").val(),
            currencyAmountMin: $("#CurrencyAmountFilterMin").val(),
            currencyAmountMax: $("#CurrencyAmountFilterMax").val(),
            expenseCategoryMin: $("#ExpenseCategoryFilterMin").val(),
            expenseCategoryMax: $("#ExpenseCategoryFilterMax").val(),
            expenseNecessityMin: $("#ExpenseNecessityFilterMin").val(),
            expenseNecessityMax: $("#ExpenseNecessityFilterMax").val(),
            comment: $("#CommentFilter").val(),
            status: $("#StatusFilter").val(),
            approvalMin: $("#ApprovalFilterMin").val(),
            approvalMax: $("#ApprovalFilterMax").val(),
            isActive: (function () {
                var value = $("#IsActiveFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            departmentId: $("#DepartmentIdFilter").val(), productId: $("#ProductIdFilter").val(), budgetId: $("#BudgetIdFilter").val(), accountGroupId: $("#AccountGroupIdFilter").val(), accountId: $("#AccountIdFilter").val(), identityUserId: $("#IdentityUserIdFilter").val()
        };
    };

    var dataTable = $("#BudgetDistributionsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(budgetDistributionService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ToksozBysNew.BudgetDistributions.Edit'),
                                action: function (data) {
                                    editModal.open({
                                        id: data.record.budgetDistribution.id
                                    });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ToksozBysNew.BudgetDistributions.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    budgetDistributionService.delete(data.record.budgetDistribution.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            { data: "budgetDistribution.costCenter" },
            { data: "budgetDistribution.expenseType" },
            { data: "budgetDistribution.projectItem" },
            { data: "budgetDistribution.type" },
            { data: "budgetDistribution.unit" },
            { data: "budgetDistribution.unitValue" },
            { data: "budgetDistribution.month" },
            { data: "budgetDistribution.year" },
            { data: "budgetDistribution.ratio" },
            { data: "budgetDistribution.amount" },
            { data: "budgetDistribution.memo" },
            { data: "budgetDistribution.invoice" },
            { data: "budgetDistribution.currency" },
            { data: "budgetDistribution.currencyAmount" },
            { data: "budgetDistribution.expenseCategory" },
            { data: "budgetDistribution.expenseNecessity" },
            { data: "budgetDistribution.comment" },
            { data: "budgetDistribution.status" },
            { data: "budgetDistribution.approval" },
            {
                data: "budgetDistribution.isActive",
                render: function (isActive) {
                    return isActive ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "department.departmentName",
                defaultContent: ""
            },
            {
                data: "product.productName",
                defaultContent: ""
            },
            {
                data: "budget.budgetName",
                defaultContent: ""
            },
            {
                data: "accountGroup.accountGroupName",
                defaultContent: ""
            },
            {
                data: "account.accountName",
                defaultContent: ""
            },
            {
                data: "identityUser.userName",
                defaultContent: ""
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewBudgetDistributionButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        budgetDistributionService.getDownloadToken().then(
            function (result) {
                var input = getFilter();
                var url = abp.appPath + 'api/app/budget-distributions/as-excel-file' +
                    abp.utils.buildQueryString([
                        { name: 'downloadToken', value: result.token },
                        { name: 'filterText', value: input.filterText },
                        { name: 'costCenter', value: input.costCenter },
                        { name: 'expenseType', value: input.expenseType },
                        { name: 'projectItemMin', value: input.projectItemMin },
                        { name: 'projectItemMax', value: input.projectItemMax },
                        { name: 'typeMin', value: input.typeMin },
                        { name: 'typeMax', value: input.typeMax },
                        { name: 'unitMin', value: input.unitMin },
                        { name: 'unitMax', value: input.unitMax },
                        { name: 'unitValueMin', value: input.unitValueMin },
                        { name: 'unitValueMax', value: input.unitValueMax },
                        { name: 'monthMin', value: input.monthMin },
                        { name: 'monthMax', value: input.monthMax },
                        { name: 'yearMin', value: input.yearMin },
                        { name: 'yearMax', value: input.yearMax },
                        { name: 'ratioMin', value: input.ratioMin },
                        { name: 'ratioMax', value: input.ratioMax },
                        { name: 'amountMin', value: input.amountMin },
                        { name: 'amountMax', value: input.amountMax },
                        { name: 'memoMin', value: input.memoMin },
                        { name: 'memoMax', value: input.memoMax },
                        { name: 'invoiceMin', value: input.invoiceMin },
                        { name: 'invoiceMax', value: input.invoiceMax },
                        { name: 'currencyMin', value: input.currencyMin },
                        { name: 'currencyMax', value: input.currencyMax },
                        { name: 'currencyAmountMin', value: input.currencyAmountMin },
                        { name: 'currencyAmountMax', value: input.currencyAmountMax },
                        { name: 'expenseCategoryMin', value: input.expenseCategoryMin },
                        { name: 'expenseCategoryMax', value: input.expenseCategoryMax },
                        { name: 'expenseNecessityMin', value: input.expenseNecessityMin },
                        { name: 'expenseNecessityMax', value: input.expenseNecessityMax },
                        { name: 'comment', value: input.comment },
                        { name: 'status', value: input.status },
                        { name: 'approvalMin', value: input.approvalMin },
                        { name: 'approvalMax', value: input.approvalMax },
                        { name: 'isActive', value: input.isActive },
                        { name: 'departmentId', value: input.departmentId },
                        { name: 'productId', value: input.productId },
                        { name: 'budgetId', value: input.budgetId },
                        { name: 'accountGroupId', value: input.accountGroupId },
                        { name: 'accountId', value: input.accountId },
                        { name: 'identityUserId', value: input.identityUserId }
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

    $('#AdvancedFilterSection select').change(function () {
        dataTable.ajax.reload();
    });


});
