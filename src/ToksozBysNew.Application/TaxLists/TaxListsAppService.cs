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
using ToksozBysNew.TaxLists;

namespace ToksozBysNew.TaxLists
{

    [Authorize(ToksozBysNewPermissions.TaxLists.Default)]
    public class TaxListsAppService : ApplicationService, ITaxListsAppService
    {

        private readonly ITaxListRepository _taxListRepository;
        private readonly TaxListManager _taxListManager;

        public TaxListsAppService(ITaxListRepository taxListRepository, TaxListManager taxListManager)
        {

            _taxListRepository = taxListRepository;
            _taxListManager = taxListManager;
        }

        public virtual async Task<PagedResultDto<TaxListDto>> GetListAsync(GetTaxListsInput input)
        {
            var totalCount = await _taxListRepository.GetCountAsync(input.FilterText, input.TaxName, input.TaxValueMin, input.TaxValueMax);
            var items = await _taxListRepository.GetListAsync(input.FilterText, input.TaxName, input.TaxValueMin, input.TaxValueMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TaxListDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TaxList>, List<TaxListDto>>(items)
            };
        }

        public virtual async Task<TaxListDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<TaxList, TaxListDto>(await _taxListRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.TaxLists.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _taxListRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.TaxLists.Create)]
        public virtual async Task<TaxListDto> CreateAsync(TaxListCreateDto input)
        {

            var taxList = await _taxListManager.CreateAsync(
            input.TaxName, input.TaxValue
            );

            return ObjectMapper.Map<TaxList, TaxListDto>(taxList);
        }

        [Authorize(ToksozBysNewPermissions.TaxLists.Edit)]
        public virtual async Task<TaxListDto> UpdateAsync(Guid id, TaxListUpdateDto input)
        {

            var taxList = await _taxListManager.UpdateAsync(
            id,
            input.TaxName, input.TaxValue, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<TaxList, TaxListDto>(taxList);
        }
    }
}