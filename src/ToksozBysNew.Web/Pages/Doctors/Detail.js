

var l = abp.localization.getResource("ToksozBysNew");
var doctorService = window.toksozBysNew.doctors.doctors;
var addressService = window.toksozBysNew.customerAddresses.customerAddresses;

var dataTable = $("#UnitsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
    processing: true,
    serverSide: true,
    paging: true,
    searching: false,
    scrollX: true,
    autoWidth: true,
    scrollCollapse: true,
    order: [[1, "asc"]],
    ajax: abp.libs.datatables.createAjax(doctorService.getListWithBrickNameByDoctorId, function () {
        var doctorId = $('#doctorId').val();
        return doctorId;
    }),
    columnDefs: [
        {
            rowAction: {
                items:
                    [
                        {
                            text: l("Edit"),
                            visible: abp.auth.isGranted('ToksozBysNew.Doctors.Edit'),
                            action: function (data) {
                                editModal.open({
                                    id: data.record.doctor.id
                                });
                            }
                        },
                        {
                            text: l("Delete"),
                            visible: abp.auth.isGranted('ToksozBysNew.Doctors.Delete'),
                            confirmMessage: function () {
                                return l("DeleteConfirmationMessage");
                            },
                            action: function (data) {
                                doctorService.delete(data.record.doctor.id)
                                    .then(function () {
                                        abp.notify.info(l("SuccessfullyDeleted"));
                                        dataTable.ajax.reload();
                                    });
                            }
                        },
                        {
                            text: l("Detail"),
                            action: function (data) {
                                let id = data.record.doctor.id;
                                window.location.href = "/Doctors/Detail?id=" + id + "";
                            }
                        }
                    ]
            }
        },
        {
            
            data: "isActive",
            render: function (isActive) {
                return isActive ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
            }
        },
        { data: "nameSurname" },
        { data: "unit" },
        { data: "pharmacyName" },
        {
            data: "position" 
        },
        {
            data: "spec" 
        },
        {
            data: "title" 
        },
        {
            data: "brickName" 
        }
    ]
}));
var dataTable = $("#AddressTable").DataTable(abp.libs.datatables.normalizeConfiguration({
    processing: true,
    serverSide: true,
    paging: true,
    searching: false,
    scrollX: true,
    autoWidth: true,
    scrollCollapse: true,
    order: [[1, "asc"]],
    ajax: abp.libs.datatables.createAjax(addressService.getListWithDoctorId, function () {
        var doctorId = $('#doctorId').val();
        return doctorId;
    }),
    columnDefs: [
        {
            rowAction: {
                items:
                    [
                        {
                            text: l("Edit"),
                            visible: abp.auth.isGranted('ToksozBysNew.Doctors.Edit'),
                            action: function (data) {
                                editModal.open({
                                    id: data.record.doctor.id
                                });
                            }
                        },
                        {
                            text: l("Delete"),
                            visible: abp.auth.isGranted('ToksozBysNew.Doctors.Delete'),
                            confirmMessage: function () {
                                return l("DeleteConfirmationMessage");
                            },
                            action: function (data) {
                                doctorService.delete(data.record.doctor.id)
                                    .then(function () {
                                        abp.notify.info(l("SuccessfullyDeleted"));
                                        dataTable.ajax.reload();
                                    });
                            }
                        },
                        {
                            text: l("Detail"),
                            action: function (data) {
                                let id = data.record.doctor.id;
                                window.location.href = "/Doctors/Detail?id=" + id + "";
                            }
                        }
                    ]
            }
        }, 
        { data: "doctorNameSurname" },
        { data: "address" },
        { data: "country" },
        {
            data: "province"
        },
        {
            data: "district"
        },
        {
            data: "brick"
        } 
    ]
}));