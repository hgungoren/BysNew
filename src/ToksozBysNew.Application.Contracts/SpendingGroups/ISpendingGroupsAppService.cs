using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ToksozBysNew.SpendingGroups
{
    public interface ISpendingGroupsAppService : IApplicationService
    {
        Task<PagedResultDto<SpendingGroupDto>> GetListAsync(GetSpendingGroupsInput input);

        Task<SpendingGroupDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SpendingGroupDto> CreateAsync(SpendingGroupCreateDto input);

        Task<SpendingGroupDto> UpdateAsync(Guid id, SpendingGroupUpdateDto input);
    }
}