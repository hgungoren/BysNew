$(function () {
    var l = abp.localization.getResource("ToksozBysNew");

    var invoiceService = window.toksozBysNew.invoices.invoices;

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Invoices/CreateModal",
        scriptUrl: "/Pages/Invoices/createModal.js",
        modalClass: "invoiceCreate"
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Invoices/EditModal",
        scriptUrl: "/Pages/Invoices/editModal.js",
        modalClass: "invoiceEdit"
    });

    var getFilter = function () {
        return {
            filterText: $("#FilterText").val(),
            invoiceSerialNo: $("#InvoiceSerialNoFilter").val(),
            invoiceDateMin: $("#InvoiceDateFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            invoiceDateMax: $("#InvoiceDateFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            notes: $("#NotesFilter").val(),
            paymentDateMin: $("#PaymentDateFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            paymentDateMax: $("#PaymentDateFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            amountMin: $("#AmountFilterMin").val(),
            amountMax: $("#AmountFilterMax").val()
        };
    };

    var dataTable = $("#InvoicesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(invoiceService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ToksozBysNew.Invoices.Edit'),
                                action: function (data) {
                                    editModal.open({
                                        id: data.record.id
                                    });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ToksozBysNew.Invoices.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    invoiceService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            },
                            {
                                text: l("Detail"),
                                action: function (data) {
                                    let id = data.record.id;
                                    let a = data.record.invoiceSerialNo;
                                    window.location.href = "/Invoices/Index?id=" + id + "";
                                }
                            },
                            {
                                text: l("DetailWithGrid"),
                                action: function (data) {
                                    let id = data.record.id;
                                    let a = data.record.invoiceSerialNo;
                                    window.location.href = "/Invoices/InvoiceGrid?id=" + id + "";
                                }
                            }
                        ]
                }
            },
            { data: "invoiceSerialNo" },
            {
                data: "invoiceDate",
                render: function (invoiceDate) {
                    if (!invoiceDate) {
                        return "";
                    }

                    var date = Date.parse(invoiceDate);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
            { data: "notes" },
            {
                data: "paymentDate",
                render: function (paymentDate) {
                    if (!paymentDate) {
                        return "";
                    }

                    var date = Date.parse(paymentDate);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
            { data: "amount" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    }); 

    $("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        invoiceService.getDownloadToken().then(
            function (result) {
                var input = getFilter();
                var url = abp.appPath + 'api/app/invoices/as-excel-file' +
                    abp.utils.buildQueryString([
                        { name: 'downloadToken', value: result.token },
                        { name: 'filterText', value: input.filterText },
                        { name: 'invoiceSerialNo', value: input.invoiceSerialNo },
                        { name: 'invoiceDateMin', value: input.invoiceDateMin },
                        { name: 'invoiceDateMax', value: input.invoiceDateMax },
                        { name: 'notes', value: input.notes },
                        { name: 'paymentDateMin', value: input.paymentDateMin },
                        { name: 'paymentDateMax', value: input.paymentDateMax },
                        { name: 'amountMin', value: input.amountMin },
                        { name: 'amountMax', value: input.amountMax }
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
