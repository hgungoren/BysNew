$(function () {
    var l = abp.localization.getResource("ToksozBysNew");

    var invoiceDetailService = window.toksozBysNew.invoiceDetails.invoiceDetails;
    var invoiceId = $('#invoiceId').val();

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "InvoiceDetails/CreateModal?id=" + invoiceId + "",
        scriptUrl: "/Pages/InvoiceDetails/createModal.js",
        modalClass: "invoiceDetailCreate"
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "InvoiceDetails/EditModal",
        scriptUrl: "/Pages/InvoiceDetails/editModal.js",
        modalClass: "invoiceDetailEdit"
    });


    if (invoiceId) {
        var dataTable = $("#InvoiceDetailsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                        editModal.open({
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
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                { data: "invoiceDetailQuantity" },
                { data: "invoiceDetailPrice" },
                { data: "invoiceDetailNote" },
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
                { data: "tax" }
            ]
        }));
    }
    else {
        $('#InvoiceDetailsTable').first().hide();
    }



    createModal.onResult(function () {
        abp.message.success("Saved Successfully!", "Congrats!");
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewInvoiceDetailButton").click(function (e) {
        if (invoiceId) { 
            e.preventDefault();
            createModal.open();
        }
        else {
            abp.message.error("You must save the invoice first!","Error");
        }
    });

});
