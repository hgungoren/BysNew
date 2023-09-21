$(function () {
    var l = abp.localization.getResource("ToksozBysNew");
	
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
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "InvoiceDetails/CreateModal",
        scriptUrl: "/Pages/InvoiceDetails/createModal.js",
        modalClass: "invoiceDetailCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "InvoiceDetails/EditModal",
        scriptUrl: "/Pages/InvoiceDetails/editModal.js",
        modalClass: "invoiceDetailEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            invoiceDetailQuantityMin: $("#InvoiceDetailQuantityFilterMin").val(),
			invoiceDetailQuantityMax: $("#InvoiceDetailQuantityFilterMax").val(),
			invoiceDetailPriceMin: $("#InvoiceDetailPriceFilterMin").val(),
			invoiceDetailPriceMax: $("#InvoiceDetailPriceFilterMax").val(),
			invoiceDetailNote: $("#InvoiceDetailNoteFilter").val(),
			invoiceDetailDateMin: $("#InvoiceDetailDateFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			invoiceDetailDateMax: $("#InvoiceDetailDateFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			taxMin: $("#TaxFilterMin").val(),
			taxMax: $("#TaxFilterMax").val(),
			invoiceId: $("#InvoiceIdFilter").val()
        };
    };

    var dataTable = $("#InvoiceDetailsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(invoiceDetailService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ToksozBysNew.InvoiceDetails.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.invoiceDetail.id
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
                                    invoiceDetailService.delete(data.record.invoiceDetail.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "invoiceDetail.invoiceDetailQuantity" },
			{ data: "invoiceDetail.invoiceDetailPrice" },
			{ data: "invoiceDetail.invoiceDetailNote" },
            {
                data: "invoiceDetail.invoiceDetailDate",
                render: function (invoiceDetailDate) {
                    if (!invoiceDetailDate) {
                        return "";
                    }
                    
					var date = Date.parse(invoiceDetailDate);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
			{ data: "invoiceDetail.tax" },
            {
                data: "invoice.invoiceSerialNo",
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

    $("#NewInvoiceDetailButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        invoiceDetailService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/invoice-details/as-excel-file' + 
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
                            { name: 'taxMin', value: input.taxMin },
                            { name: 'taxMax', value: input.taxMax }, 
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
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
    
    
});
