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
        ajax: abp.libs.datatables.createAjax(invoiceService.getById, function () {
            var invoiceId =  $('#invoiceId').val();
            console.log("id->", invoiceId);
            return invoiceId;
        }),
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

    $("#NewInvoiceButton").click(function (e) {
        e.preventDefault();
        createModal.open();
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


    var invoiceDetailService = window.toksozBysNew.invoiceDetails.invoiceDetails;

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

    var invId = $('#invoiceId').val(); 

    var createModalDetail = new abp.ModalManager({
        viewUrl: abp.appPath + "InvoiceDetails/CreateModal?id=" + invId +"",
        scriptUrl: "/Pages/InvoiceDetails/createModal.js",
        modalClass: "invoiceDetailCreate"
    });

    var editModalDetail = new abp.ModalManager({
        viewUrl: abp.appPath + "InvoiceDetails/EditModal",
        scriptUrl: "/Pages/InvoiceDetails/editModal.js",
        modalClass: "invoiceDetailEdit"
    });

    var getFilter = function () {
        return {
            filterText: $("#FilterText").val(),
            invoiceDetailQuantityMin: $("#InvoiceDetailQuantityFilterMin").val(),
            invoiceDetailQuantityMax: $("#InvoiceDetailQuantityFilterMax").val(),
            invoiceDetailPriceMin: $("#InvoiceDetailPriceFilterMin").val(),
            invoiceDetailPriceMax: $("#InvoiceDetailPriceFilterMax").val(),
            invoiceDetailNote: $("#InvoiceDetailNoteFilter").val(),
            invoiceSerialNo: $("#InvoiceSerialNoFilter").val(),
            invoiceDateMin: $("#InvoiceDateFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            invoiceDateMax: $("#InvoiceDateFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            invoiceNotes: $("#InvoiceNotesFilter").val(),
            invoicePaymentDateMin: $("#InvoicePaymentDateFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            invoicePaymentDateMax: $("#InvoicePaymentDateFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            invoiceAmountMin: $("#InvoiceAmountFilterMin").val(),
            invoiceAmountMax: $("#InvoiceAmountFilterMax").val(),
            invoiceId: $("#InvoiceIdFilter").val()
        };
    };

    var dataTableDetail = $("#InvoiceDetailsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(invoiceDetailService.getListByInvoiceId, function () {
            var invoiceId = $('#invoiceId').val(); 
            return invoiceId;
        }),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ToksozBysNew.InvoiceDetails.Edit'),
                                action: function (data) {
                                    editModalDetail.open({
                                        id: data.record.id
                                    });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ToksozBysNew.InvoiceDetails.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    invoiceDetailService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTableDetail.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            { data: "invoiceDetailQuantity" },
            { data: "invoiceDetailPrice" },
            { data: "invoiceDetailNote" } ,
            {
                data: "invoiceDetailDate",
                render: function (invoiceDetailDate) {
                    if (!invoiceDetailDate) {
                        return "";
                    }

                    var date = Date.parse(invoiceDetailDate);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
        ]
    }));

    createModalDetail.onResult(function () {
        dataTableDetail.ajax.reload();
    });

    editModalDetail.onResult(function () {
        dataTableDetail.ajax.reload();
    });

    $("#NewInvoiceDetailButton").click(function (e) {
        e.preventDefault();
        createModalDetail.open();
    });

    $("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTableDetail.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        invoiceDetailService.getDownloadToken().then(
            function (result) {
                var input = getFilter();
                var url = abp.appPath + 'api/app/invoice-details/as-excel-file' +
                    abp.utils.buildQueryString([
                        { name: 'downloadToken', value: result.token },
                        { name: 'filterText', value: input.filterText },
                        { name: 'invoiceDetailQuantityMin', value: input.invoiceDetailQuantityMin },
                        { name: 'invoiceDetailQuantityMax', value: input.invoiceDetailQuantityMax },
                        { name: 'invoiceDetailPriceMin', value: input.invoiceDetailPriceMin },
                        { name: 'invoiceDetailPriceMax', value: input.invoiceDetailPriceMax },
                        { name: 'invoiceDetailNote', value: input.invoiceDetailNote },
                        { name: 'invoiceDetailDateMin', value: input.invoiceDetailDateMin },
                        { name: 'invoiceDetailDateMax', value: input.invoiceDetailDateMax },
                        { name: 'invoiceId', value: input.invoiceId }
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
            dataTableDetail.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function () {
        dataTableDetail.ajax.reload();
    });

});