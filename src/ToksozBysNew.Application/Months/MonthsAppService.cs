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
using ToksozBysNew.Months;

namespace ToksozBysNew.Months
{

    [Authorize(ToksozBysNewPermissions.Months.Default)]
    public class MonthsAppService : ApplicationService, IMonthsAppService
    {

        private readonly IMonthRepository _monthRepository;
        private readonly MonthManager _monthManager;

        public MonthsAppService(IMonthRepository monthRepository, MonthManager monthManager)
        {

            _monthRepository = monthRepository;
            _monthManager = monthManager;
        }

        public virtual async Task<PagedResultDto<MonthDto>> GetListAsync(GetMonthsInput input)
        {
            var totalCount = await _monthRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await _monthRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<MonthDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Month>, List<MonthDto>>(items)
            };
        }

        public virtual async Task<MonthDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Month, MonthDto>(await _monthRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.Months.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _monthRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Months.Create)]
        public virtual async Task<MonthDto> CreateAsync(MonthCreateDto input)
        {

            var month = await _monthManager.CreateAsync(
            input.Name
            );

            return ObjectMapper.Map<Month, MonthDto>(month);
        }

        [Authorize(ToksozBysNewPermissions.Months.Edit)]
        public virtual async Task<MonthDto> UpdateAsync(Guid id, MonthUpdateDto input)
        {

            var month = await _monthManager.UpdateAsync(
            id,
            input.Name, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Month, MonthDto>(month);
        }
    }
}