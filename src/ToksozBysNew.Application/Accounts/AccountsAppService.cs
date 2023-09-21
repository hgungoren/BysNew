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
using ToksozBysNew.Accounts;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Accounts
{

    [Authorize(ToksozBysNewPermissions.Accounts.Default)]
    public class AccountsAppService : ApplicationService, IAccountsAppService
    {
        private readonly IDistributedCache<AccountExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IAccountRepository _accountRepository;
        private readonly AccountManager _accountManager;

        public AccountsAppService(IAccountRepository accountRepository, AccountManager accountManager, IDistributedCache<AccountExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _accountRepository = accountRepository;
            _accountManager = accountManager;
        }

        public virtual async Task<PagedResultDto<AccountDto>> GetListAsync(GetAccountsInput input)
        {
            var totalCount = await _accountRepository.GetCountAsync(input.FilterText, input.AccountCode, input.AccountName, input.Description, input.IsActive);
            var items = await _accountRepository.GetListAsync(input.FilterText, input.AccountCode, input.AccountName, input.Description, input.IsActive, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AccountDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Account>, List<AccountDto>>(items)
            };
        }

        public virtual async Task<AccountDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Account, AccountDto>(await _accountRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.Accounts.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _accountRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Accounts.Create)]
        public virtual async Task<AccountDto> CreateAsync(AccountCreateDto input)
        {

            var account = await _accountManager.CreateAsync(
            input.AccountCode, input.AccountName, input.Description, input.IsActive
            );

            return ObjectMapper.Map<Account, AccountDto>(account);
        }

        [Authorize(ToksozBysNewPermissions.Accounts.Edit)]
        public virtual async Task<AccountDto> UpdateAsync(Guid id, AccountUpdateDto input)
        {

            var account = await _accountManager.UpdateAsync(
            id,
            input.AccountCode, input.AccountName, input.Description, input.IsActive, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Account, AccountDto>(account);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(AccountExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _accountRepository.GetListAsync(input.FilterText, input.AccountCode, input.AccountName, input.Description, input.IsActive);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Account>, List<AccountExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Accounts.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new AccountExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}