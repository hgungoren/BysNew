$(function () {
    var l = abp.localization.getResource("ToksozBysNew");

    var visitDailyActionService = window.toksozBysNew.visitDailyActions.visitDailyActions;

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
        viewUrl: abp.appPath + "VisitDailyActions/CreateModal",
        scriptUrl: "/Pages/VisitDailyActions/createModal.js",
        modalClass: "visitDailyActionCreate"
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "VisitDailyActions/EditModal",
        scriptUrl: "/Pages/VisitDailyActions/editModal.js",
        modalClass: "visitDailyActionEdit"
    });

    var getFilter = function () {
        return {
            filterText: $("#FilterText").val(),
            visitDailyDateMin: $("#VisitDailyDateFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            visitDailyDateMax: $("#VisitDailyDateFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            visitDaily1Min: $("#VisitDaily1FilterMin").val(),
            visitDaily1Max: $("#VisitDaily1FilterMax").val(),
            visitDaily2Min: $("#VisitDaily2FilterMin").val(),
            visitDaily2Max: $("#VisitDaily2FilterMax").val(),
            visitDaily3Min: $("#VisitDaily3FilterMin").val(),
            visitDaily3Max: $("#VisitDaily3FilterMax").val(),
            visitDaily4Min: $("#VisitDaily4FilterMin").val(),
            visitDaily4Max: $("#VisitDaily4FilterMax").val(),
            visitDaily5Min: $("#VisitDaily5FilterMin").val(),
            visitDaily5Max: $("#VisitDaily5FilterMax").val(),
            visitDaily6Min: $("#VisitDaily6FilterMin").val(),
            visitDaily6Max: $("#VisitDaily6FilterMax").val(),
            visitDaily7Min: $("#VisitDaily7FilterMin").val(),
            visitDaily7Max: $("#VisitDaily7FilterMax").val(),
            visitDaily8Min: $("#VisitDaily8FilterMin").val(),
            visitDaily8Max: $("#VisitDaily8FilterMax").val(),
            visitDaily9Min: $("#VisitDaily9FilterMin").val(),
            visitDaily9Max: $("#VisitDaily9FilterMax").val(),
            visitDaily10Min: $("#VisitDaily10FilterMin").val(),
            visitDaily10Max: $("#VisitDaily10FilterMax").val(),
            visitDaily11Min: $("#VisitDaily11FilterMin").val(),
            visitDaily11Max: $("#VisitDaily11FilterMax").val(),
            visitDaily12Min: $("#VisitDaily12FilterMin").val(),
            visitDaily12Max: $("#VisitDaily12FilterMax").val(),
            visitDaily13Min: $("#VisitDaily13FilterMin").val(),
            visitDaily13Max: $("#VisitDaily13FilterMax").val(),
            visitDaily14Min: $("#VisitDaily14FilterMin").val(),
            visitDaily14Max: $("#VisitDaily14FilterMax").val(),
            visitDaily15Min: $("#VisitDaily15FilterMin").val(),
            visitDaily15Max: $("#VisitDaily15FilterMax").val(),
            visitDailyCloseDateMin: $("#VisitDailyCloseDateFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            visitDailyCloseDateMax: $("#VisitDailyCloseDateFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            visitDailyNote: $("#VisitDailyNoteFilter").val(),
            identityUserId: $("#IdentityUserIdFilter").val()
        };
    };

    var dataTable = $("#VisitDailyActionsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(visitDailyActionService.getCustomList, function () {
            var userId = $('#userId').val();
            return userId;
        }),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ToksozBysNew.VisitDailyActions.Edit'),
                                action: function (data) {
                                    editModal.open({
                                        id: data.record.visitDailyAction.id
                                    });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ToksozBysNew.VisitDailyActions.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    visitDailyActionService.delete(data.record.visitDailyAction.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            },
                            {
                                text: l("Detail"),
                                action: function (data) {
                                    console.log("data=>", data.record)
                                    var invoiceId = $('#userId').val();
                                    window.location.href = "/Visits/Index?id=" + invoiceId + "&date=" + data.record.visitDailyDate + "";
                                }
                            }
                        ]
                }
            },
            {
                data: "visitDailyDate",
                render: function (visitDailyDate) {
                    if (!visitDailyDate) {
                        return "";
                    }

                    var date = Date.parse(visitDailyDate);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
            { data: "doctorVisitCount" },
            { data: "pharmaVisitCount" },
            { data: "visitDaily1" },
            { data: "visitDaily2" },
            { data: "visitDaily3" },
            { data: "visitDaily4" },
            { data: "visitDaily5" },
            { data: "visitDaily6" },
            { data: "visitDaily7" },
            { data: "visitDaily8" },
            { data: "visitDaily9" },
            { data: "visitDaily10" },
            { data: "visitDaily11" },
            { data: "visitDaily12" },
            { data: "visitDaily13" },
            { data: "visitDaily14" },
            { data: "visitDaily15" }
            //         {
            //             data: "visitDailyAction.visitDailyCloseDate",
            //             render: function (visitDailyCloseDate) {
            //                 if (!visitDailyCloseDate) {
            //                     return "";
            //                 }

            //		var date = Date.parse(visitDailyCloseDate);
            //                 return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
            //             }
            //         },
            //{ data: "visitDailyAction.visitDailyNote" },
            //         {
            //             data: "identityUser.userName",
            //             defaultContent : ""
            //         }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewVisitDailyActionButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        visitDailyActionService.getDownloadToken().then(
            function (result) {
                var input = getFilter();
                var url = abp.appPath + 'api/app/visit-daily-actions/as-excel-file' +
                    abp.utils.buildQueryString([
                        { name: 'downloadToken', value: result.token },
                        { name: 'filterText', value: input.filterText },
                        { name: 'visitDailyDateMin', value: input.visitDailyDateMin },
                        { name: 'visitDailyDateMax', value: input.visitDailyDateMax },
                        { name: 'visitDaily1Min', value: input.visitDaily1Min },
                        { name: 'visitDaily1Max', value: input.visitDaily1Max },
                        { name: 'visitDaily2Min', value: input.visitDaily2Min },
                        { name: 'visitDaily2Max', value: input.visitDaily2Max },
                        { name: 'visitDaily3Min', value: input.visitDaily3Min },
                        { name: 'visitDaily3Max', value: input.visitDaily3Max },
                        { name: 'visitDaily4Min', value: input.visitDaily4Min },
                        { name: 'visitDaily4Max', value: input.visitDaily4Max },
                        { name: 'visitDaily5Min', value: input.visitDaily5Min },
                        { name: 'visitDaily5Max', value: input.visitDaily5Max },
                        { name: 'visitDaily6Min', value: input.visitDaily6Min },
                        { name: 'visitDaily6Max', value: input.visitDaily6Max },
                        { name: 'visitDaily7Min', value: input.visitDaily7Min },
                        { name: 'visitDaily7Max', value: input.visitDaily7Max },
                        { name: 'visitDaily8Min', value: input.visitDaily8Min },
                        { name: 'visitDaily8Max', value: input.visitDaily8Max },
                        { name: 'visitDaily9Min', value: input.visitDaily9Min },
                        { name: 'visitDaily9Max', value: input.visitDaily9Max },
                        { name: 'visitDaily10Min', value: input.visitDaily10Min },
                        { name: 'visitDaily10Max', value: input.visitDaily10Max },
                        { name: 'visitDaily11Min', value: input.visitDaily11Min },
                        { name: 'visitDaily11Max', value: input.visitDaily11Max },
                        { name: 'visitDaily12Min', value: input.visitDaily12Min },
                        { name: 'visitDaily12Max', value: input.visitDaily12Max },
                        { name: 'visitDaily13Min', value: input.visitDaily13Min },
                        { name: 'visitDaily13Max', value: input.visitDaily13Max },
                        { name: 'visitDaily14Min', value: input.visitDaily14Min },
                        { name: 'visitDaily14Max', value: input.visitDaily14Max },
                        { name: 'visitDaily15Min', value: input.visitDaily15Min },
                        { name: 'visitDaily15Max', value: input.visitDaily15Max },
                        { name: 'visitDailyCloseDateMin', value: input.visitDailyCloseDateMin },
                        { name: 'visitDailyCloseDateMax', value: input.visitDailyCloseDateMax },
                        { name: 'visitDailyNote', value: input.visitDailyNote },
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

    //var visitDataTable = $("#visitSearchTable").DataTable(abp.libs.datatables.normalizeConfiguration({
    //    processing: true,
    //    serverSide: true,
    //    paging: true,
    //    searching: false,
    //    scrollX: true,
    //    autoWidth: true,
    //    scrollCollapse: true,
    //    order: [[1, "asc"]],
    //    columnDefs: [
    //        {
    //            rowAction: {
    //                items:
    //                    [
    //                        {
    //                            text: l("Edit"),
    //                            visible: abp.auth.isGranted('ToksozBysNew.VisitDailyActions.Edit'),
    //                            action: function (data) {
    //                                editModal.open({
    //                                    id: data.record.visitDailyAction.id
    //                                });
    //                            }
    //                        },
    //                        {
    //                            text: l("Delete"),
    //                            visible: abp.auth.isGranted('ToksozBysNew.VisitDailyActions.Delete'),
    //                            confirmMessage: function () {
    //                                return l("DeleteConfirmationMessage");
    //                            },
    //                            action: function (data) {
    //                                visitDailyActionService.delete(data.record.visitDailyAction.id)
    //                                    .then(function () {
    //                                        abp.notify.info(l("SuccessfullyDeleted"));
    //                                        dataTable.ajax.reload();
    //                                    });
    //                            }
    //                        }
    //                    ]
    //            }
    //        },
    //        { data: "customerName" },
    //        { data: "specCode" },
    //        { data: "unitName" }
    //    ]
    //}));

    //$('#customerSearchButton').on('click', function () {

    //    var table = $("#VisitDailyActionsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
    //        processing: true,
    //        serverSide: true,
    //        paging: true,
    //        searching: false,
    //        scrollX: true,
    //        autoWidth: true,
    //        scrollCollapse: true,
    //        order: [[1, "asc"]],
    //        ajax: abp.libs.datatables.createAjax(visitDailyActionService.getVisitListWithCustomerName, function () {
    //            var customerName = $('#customerName').val();
    //            return customerName;
    //        }),
    //        columnDefs: [
    //            {
    //                rowAction: {
    //                    items:
    //                        [
    //                            {
    //                                text: l("Edit"),
    //                                visible: abp.auth.isGranted('ToksozBysNew.VisitDailyActions.Edit'),
    //                                action: function (data) {
    //                                    editModal.open({
    //                                        id: data.record.visitDailyAction.id
    //                                    });
    //                                }
    //                            },
    //                            {
    //                                text: l("Delete"),
    //                                visible: abp.auth.isGranted('ToksozBysNew.VisitDailyActions.Delete'),
    //                                confirmMessage: function () {
    //                                    return l("DeleteConfirmationMessage");
    //                                },
    //                                action: function (data) {
    //                                    visitDailyActionService.delete(data.record.visitDailyAction.id)
    //                                        .then(function () {
    //                                            abp.notify.info(l("SuccessfullyDeleted"));
    //                                            dataTable.ajax.reload();
    //                                        });
    //                                }
    //                            }
    //                        ]
    //                }
    //            },
    //            { data: "customerName" },
    //            { data: "specCode" },
    //            { data: "unitName" }
    //        ]
    //    }))
    //});

    //var tableVisit = $('#VisitSearchTable').DataTable({
    //    data: [],
    //    "order": [[0, "asc"]],
    //    columns: [{
    //        data: "customerName"
    //    }, {
    //        data: "specCode"
    //    }, {
    //        data: "unitName"
    //    } ]
    //})

    //$('#customerSearchButton').on('click', function () {
    //    var customerName = $('#customerName').val();
    //    tableVisit.ajax.url("api/app/visits/visit-list-with-customer-name?name=" + customerName +"").load();
    //})

    $("#customerSearchButton").click(function (e) {
        alert("asd");
    });

    $("#searchButton").click(function (e) {
        alert("asd");
    });
     
    //    $("#customerSearchButton").click(function () {
    //        alert("asds");
    //        //if ($(".table-responsive").hasClass("hidden")) {
    //        //    $(".table-responsive").removeClass("hidden");
    //        //    $.ajax({
    //        //        url: "api/app/visits/visit-list-with-customer-name?name=" + customerName +"",
    //        //        type: "GET"
    //        //    }).done(function (result) {
    //        //        visitTable.clear().draw();
    //        //        visitTable.rows.add(result).draw();
    //        //    });
    //        //}
    //    }); 

    //let visitTable = $("#VisitSearchTable").DataTable({
    //    pageLength: 20,
    //    lengthMenu: [20, 30, 50, 75, 100],
    //    order: [],
    //    paging: true,
    //    searching: true,
    //    info: true,
    //    data: [],
    //    columns: [
    //        {
    //            data: "customerName"
    //        }, {
    //            data: "specCode"
    //        }, {
    //            data: "unitName"
    //        } 
    //    ]
    //});
});
