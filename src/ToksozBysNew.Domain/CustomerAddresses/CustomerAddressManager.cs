using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.CustomerAddresses
{
    public class CustomerAddressManager : DomainService
    {
        private readonly ICustomerAddressRepository _customerAddressRepository;

        public CustomerAddressManager(ICustomerAddressRepository customerAddressRepository)
        {
            _customerAddressRepository = customerAddressRepository;
        }

        public async Task<CustomerAddress> CreateAsync(
        Guid? doctorId, Guid? brickId, Guid? districtId, Guid? countryId, Guid? provinceId, string address)
        {

            var customerAddress = new CustomerAddress(
             GuidGenerator.Create(),
             doctorId, brickId, districtId, countryId, provinceId, address
             );

            return await _customerAddressRepository.InsertAsync(customerAddress);
        }

        public async Task<CustomerAddress> UpdateAsync(
            Guid id,
            Guid? doctorId, Guid? brickId, Guid? districtId, Guid? countryId, Guid? provinceId, string address, [CanBeNull] string concurrencyStamp = null
        )
        {

            var customerAddress = await _customerAddressRepository.GetAsync(id);

            customerAddress.DoctorId = doctorId;
            customerAddress.BrickId = brickId;
            customerAddress.DistrictId = districtId;
            customerAddress.CountryId = countryId;
            customerAddress.ProvinceId = provinceId;
            customerAddress.Address = address;

            customerAddress.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerAddressRepository.UpdateAsync(customerAddress);
        }

    }
}