using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ToksozBysNew.Permissions;
using ToksozBysNew.CustomerTypes;

namespace ToksozBysNew.CustomerTypes
{

    [Authorize(ToksozBysNewPermissions.CustomerTypes.Default)]
    public class CustomerTypesAppService : ApplicationService, ICustomerTypesAppService
    {

        private readonly ICustomerTypeRepository _customerTypeRepository;
        private readonly CustomerTypeManager _customerTypeManager;

        public CustomerTypesAppService(ICustomerTypeRepository customerTypeRepository, CustomerTypeManager customerTypeManager)
        {

            _customerTypeRepository = customerTypeRepository;
            _customerTypeManager = customerTypeManager;
        }

        public virtual async Task<PagedResultDto<CustomerTypeDto>> GetListAsync(GetCustomerTypesInput input)
        {
            var totalCount = await _customerTypeRepository.GetCountAsync(input.FilterText, input.TypeName);
            var items = await _customerTypeRepository.GetListAsync(input.FilterText, input.TypeName, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerTypeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerType>, List<CustomerTypeDto>>(items)
            };
        }

        public virtual async Task<CustomerTypeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerType, CustomerTypeDto>(await _customerTypeRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.CustomerTypes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerTypeRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.CustomerTypes.Create)]
        public virtual async Task<CustomerTypeDto> CreateAsync(CustomerTypeCreateDto input)
        {

            var customerType = await _customerTypeManager.CreateAsync(
            input.TypeName
            );

            return ObjectMapper.Map<CustomerType, CustomerTypeDto>(customerType);
        }

        [Authorize(ToksozBysNewPermissions.CustomerTypes.Edit)]
        public virtual async Task<CustomerTypeDto> UpdateAsync(Guid id, CustomerTypeUpdateDto input)
        {

            var customerType = await _customerTypeManager.UpdateAsync(
            id,
            input.TypeName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerType, CustomerTypeDto>(customerType);
        }
    }
}