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
using ToksozBysNew.AccountGroups;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.AccountGroups
{

    [Authorize(ToksozBysNewPermissions.AccountGroups.Default)]
    public class AccountGroupsAppService : ApplicationService, IAccountGroupsAppService
    {
        private readonly IDistributedCache<AccountGroupExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IAccountGroupRepository _accountGroupRepository;
        private readonly AccountGroupManager _accountGroupManager;

        public AccountGroupsAppService(IAccountGroupRepository accountGroupRepository, AccountGroupManager accountGroupManager, IDistributedCache<AccountGroupExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _accountGroupRepository = accountGroupRepository;
            _accountGroupManager = accountGroupManager;
        }

        public virtual async Task<PagedResultDto<AccountGroupDto>> GetListAsync(GetAccountGroupsInput input)
        {
            var totalCount = await _accountGroupRepository.GetCountAsync(input.FilterText, input.AccountGroupName, input.IsUnitEnterable);
            var items = await _accountGroupRepository.GetListAsync(input.FilterText, input.AccountGroupName, input.IsUnitEnterable, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AccountGroupDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AccountGroup>, List<AccountGroupDto>>(items)
            };
        }

        public virtual async Task<AccountGroupDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<AccountGroup, AccountGroupDto>(await _accountGroupRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.AccountGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _accountGroupRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.AccountGroups.Create)]
        public virtual async Task<AccountGroupDto> CreateAsync(AccountGroupCreateDto input)
        {

            var accountGroup = await _accountGroupManager.CreateAsync(
            input.AccountGroupName, input.IsUnitEnterable
            );

            return ObjectMapper.Map<AccountGroup, AccountGroupDto>(accountGroup);
        }

        [Authorize(ToksozBysNewPermissions.AccountGroups.Edit)]
        public virtual async Task<AccountGroupDto> UpdateAsync(Guid id, AccountGroupUpdateDto input)
        {

            var accountGroup = await _accountGroupManager.UpdateAsync(
            id,
            input.AccountGroupName, input.IsUnitEnterable, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<AccountGroup, AccountGroupDto>(accountGroup);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(AccountGroupExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _accountGroupRepository.GetListAsync(input.FilterText, input.AccountGroupName, input.IsUnitEnterable);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<AccountGroup>, List<AccountGroupExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "AccountGroups.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new AccountGroupExcelDownloadTokenCacheItem { Token = token },
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