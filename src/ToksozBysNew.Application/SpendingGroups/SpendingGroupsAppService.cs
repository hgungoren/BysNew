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
using ToksozBysNew.SpendingGroups;

namespace ToksozBysNew.SpendingGroups
{

    [Authorize(ToksozBysNewPermissions.SpendingGroups.Default)]
    public class SpendingGroupsAppService : ApplicationService, ISpendingGroupsAppService
    {

        private readonly ISpendingGroupRepository _spendingGroupRepository;
        private readonly SpendingGroupManager _spendingGroupManager;

        public SpendingGroupsAppService(ISpendingGroupRepository spendingGroupRepository, SpendingGroupManager spendingGroupManager)
        {

            _spendingGroupRepository = spendingGroupRepository;
            _spendingGroupManager = spendingGroupManager;
        }

        public virtual async Task<PagedResultDto<SpendingGroupDto>> GetListAsync(GetSpendingGroupsInput input)
        {
            var totalCount = await _spendingGroupRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await _spendingGroupRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SpendingGroupDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SpendingGroup>, List<SpendingGroupDto>>(items)
            };
        }

        public virtual async Task<SpendingGroupDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SpendingGroup, SpendingGroupDto>(await _spendingGroupRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.SpendingGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _spendingGroupRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.SpendingGroups.Create)]
        public virtual async Task<SpendingGroupDto> CreateAsync(SpendingGroupCreateDto input)
        {

            var spendingGroup = await _spendingGroupManager.CreateAsync(
            input.Name
            );

            return ObjectMapper.Map<SpendingGroup, SpendingGroupDto>(spendingGroup);
        }

        [Authorize(ToksozBysNewPermissions.SpendingGroups.Edit)]
        public virtual async Task<SpendingGroupDto> UpdateAsync(Guid id, SpendingGroupUpdateDto input)
        {

            var spendingGroup = await _spendingGroupManager.UpdateAsync(
            id,
            input.Name, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<SpendingGroup, SpendingGroupDto>(spendingGroup);
        }
    }
}