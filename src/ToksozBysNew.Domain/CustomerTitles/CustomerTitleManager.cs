using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.CustomerTitles
{
    public class CustomerTitleManager : DomainService
    {
        private readonly ICustomerTitleRepository _customerTitleRepository;

        public CustomerTitleManager(ICustomerTitleRepository customerTitleRepository)
        {
            _customerTitleRepository = customerTitleRepository;
        }

        public async Task<CustomerTitle> CreateAsync(
        string titleName)
        {

            var customerTitle = new CustomerTitle(
             GuidGenerator.Create(),
             titleName
             );

            return await _customerTitleRepository.InsertAsync(customerTitle);
        }

        public async Task<CustomerTitle> UpdateAsync(
            Guid id,
            string titleName, [CanBeNull] string concurrencyStamp = null
        )
        {

            var customerTitle = await _customerTitleRepository.GetAsync(id);

            customerTitle.TitleName = titleName;

            customerTitle.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerTitleRepository.UpdateAsync(customerTitle);
        }

    }
}