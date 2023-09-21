$(function () {
    var l = abp.localization.getResource("ToksozBysNew");
	
	var customerAddressService = window.toksozBysNew.customerAddresses.customerAddresses;
	
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
        viewUrl: abp.appPath + "CustomerAddresses/CreateModal",
        scriptUrl: "/Pages/CustomerAddresses/createModal.js",
        modalClass: "customerAddressCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CustomerAddresses/EditModal",
        scriptUrl: "/Pages/CustomerAddresses/editModal.js",
        modalClass: "customerAddressEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            address: $("#AddressFilter").val(),
			doctorId: $("#DoctorIdFilter").val(),			brickId: $("#BrickIdFilter").val(),			districtId: $("#DistrictIdFilter").val(),			countryId: $("#CountryIdFilter").val(),			provinceId: $("#ProvinceIdFilter").val()
        };
    };

    var dataTable = $("#CustomerAddressesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(customerAddressService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ToksozBysNew.CustomerAddresses.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.customerAddress.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ToksozBysNew.CustomerAddresses.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    customerAddressService.delete(data.record.customerAddress.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "customerAddress.address" },
            {
                data: "doctor.nameSurname",
                defaultContent : ""
            },
            {
                data: "brick.brickName",
                defaultContent : ""
            },
            {
                data: "district.districtName",
                defaultContent : ""
            },
            {
                data: "country.countryName",
                defaultContent : ""
            },
            {
                data: "province.provinceName",
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

    $("#NewCustomerAddressButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        customerAddressService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/customer-addresses/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'address', value: input.address }, 
                            { name: 'doctorId', value: input.doctorId }
, 
                            { name: 'brickId', value: input.brickId }
, 
                            { name: 'districtId', value: input.districtId }
, 
                            { name: 'countryId', value: input.countryId }
, 
                            { name: 'provinceId', value: input.provinceId }
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
