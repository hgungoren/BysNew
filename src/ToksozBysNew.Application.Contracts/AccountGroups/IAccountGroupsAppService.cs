using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.AccountGroups
{
    public interface IAccountGroupsAppService : IApplicationService
    {
        Task<PagedResultDto<AccountGroupDto>> GetListAsync(GetAccountGroupsInput input);

        Task<AccountGroupDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<AccountGroupDto> CreateAsync(AccountGroupCreateDto input);

        Task<AccountGroupDto> UpdateAsync(Guid id, AccountGroupUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(AccountGroupExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}