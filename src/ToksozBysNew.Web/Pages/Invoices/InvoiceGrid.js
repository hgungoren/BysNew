$(function () {
    var l = abp.localization.getResource("ToksozBysNew");

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

    if (!invoiceId) {
        $('#gridContainer').first().hide();
    }

    //$(() => {
    //    const URL = '/api/InvoiceDetails';
    //    const dataGrid = $('#gridContainer').dxDataGrid({

    //        editing: {
    //            mode: 'row',
    //            allowUpdating: true,
    //            allowAdding: true,
    //            allowDeleting: true,
    //        },
    //        selection: {
    //            mode: 'multiple',
    //        } ,
    //        toolbar: {
    //            items: [
    //                {
    //                    name: 'addRowButton',
    //                    showText: 'always',
    //                }, {
    //                    location: 'after',
    //                    widget: 'dxButton',
    //                    options: {
    //                        text: 'Delete Selected Records',
    //                        icon: 'trash',
    //                        disabled: true,
    //                        onClick() {
    //                            dataGrid.getSelectedRowKeys().forEach((key) => {
    //                                employeesStore.remove(key);
    //                            });
    //                            dataGrid.refresh();
    //                        },
    //                    },
    //                },
    //            ],
    //        },
    //        onSelectionChanged(data) {
    //            dataGrid.option('toolbar.items[1].options.disabled', !data.selectedRowsData.length);
    //        },
    //    }).dxDataGrid('instance');
    //});




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
            abp.message.error("You must save the invoice first!", "Error");
        }
    });

});

$('#btnMail').click(function () {

    $.ajax({
        url: "https://localhost:44316/v2/documents",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            "Id":1,
            "Body": "Fatura",
            "Author": {
                "Name": "Huseyin",
                "Email":"huseyingungoren@toksozgrup.com.tr"
            },
            "InvoiceId": document.getElementById("invoiceId").value
        }),
        success: function (res) {
            console.log(res);
        }
    });
});
$('#btnApprove').click(function () {

    $.ajax({
        url: "https://localhost:44316/sendmailapprove",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            "Id": 1,
            "Body": "Fatura",
            "Author": {
                "Name": "Huseyin",
                "Email": "huseyingungoren@toksozgrup.com.tr"
            }
        })
    }); 
    //document.getElementById("approved").value = "Onaylandı";
});
$('#btnReject').click(function () {

    $.ajax({
        url: "https://localhost:44316/sendmailreject",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            "Id": 1,
            "Body": "Fatura",
            "Author": {
                "Name": "Huseyin",
                "Email": "huseyingungoren@toksozgrup.com.tr"
            }
        })
    });
    //document.getElementById("approved").value = "Reddedildi";
});
 


