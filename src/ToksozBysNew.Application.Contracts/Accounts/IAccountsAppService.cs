using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Accounts
{
    public interface IAccountsAppService : IApplicationService
    {
        Task<PagedResultDto<AccountDto>> GetListAsync(GetAccountsInput input);

        Task<AccountDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<AccountDto> CreateAsync(AccountCreateDto input);

        Task<AccountDto> UpdateAsync(Guid id, AccountUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(AccountExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}