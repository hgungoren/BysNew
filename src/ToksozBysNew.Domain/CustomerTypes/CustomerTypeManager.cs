using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.CustomerTypes
{
    public class CustomerTypeManager : DomainService
    {
        private readonly ICustomerTypeRepository _customerTypeRepository;

        public CustomerTypeManager(ICustomerTypeRepository customerTypeRepository)
        {
            _customerTypeRepository = customerTypeRepository;
        }

        public async Task<CustomerType> CreateAsync(
        string typeName)
        {

            var customerType = new CustomerType(
             GuidGenerator.Create(),
             typeName
             );

            return await _customerTypeRepository.InsertAsync(customerType);
        }

        public async Task<CustomerType> UpdateAsync(
            Guid id,
            string typeName, [CanBeNull] string concurrencyStamp = null
        )
        {

            var customerType = await _customerTypeRepository.GetAsync(id);

            customerType.TypeName = typeName;

            customerType.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerTypeRepository.UpdateAsync(customerType);
        }

    }
}