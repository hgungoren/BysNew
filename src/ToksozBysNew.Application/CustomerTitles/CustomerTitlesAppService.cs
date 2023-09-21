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
using ToksozBysNew.CustomerTitles;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.CustomerTitles
{

    [Authorize(ToksozBysNewPermissions.CustomerTitles.Default)]
    public class CustomerTitlesAppService : ApplicationService, ICustomerTitlesAppService
    {
        private readonly IDistributedCache<CustomerTitleExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICustomerTitleRepository _customerTitleRepository;
        private readonly CustomerTitleManager _customerTitleManager;

        public CustomerTitlesAppService(ICustomerTitleRepository customerTitleRepository, CustomerTitleManager customerTitleManager, IDistributedCache<CustomerTitleExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerTitleRepository = customerTitleRepository;
            _customerTitleManager = customerTitleManager;
        }

        public virtual async Task<PagedResultDto<CustomerTitleDto>> GetListAsync(GetCustomerTitlesInput input)
        {
            var totalCount = await _customerTitleRepository.GetCountAsync(input.FilterText, input.TitleName);
            var items = await _customerTitleRepository.GetListAsync(input.FilterText, input.TitleName, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerTitleDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerTitle>, List<CustomerTitleDto>>(items)
            };
        }

        public virtual async Task<CustomerTitleDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerTitle, CustomerTitleDto>(await _customerTitleRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.CustomerTitles.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerTitleRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.CustomerTitles.Create)]
        public virtual async Task<CustomerTitleDto> CreateAsync(CustomerTitleCreateDto input)
        {

            var customerTitle = await _customerTitleManager.CreateAsync(
            input.TitleName
            );

            return ObjectMapper.Map<CustomerTitle, CustomerTitleDto>(customerTitle);
        }

        [Authorize(ToksozBysNewPermissions.CustomerTitles.Edit)]
        public virtual async Task<CustomerTitleDto> UpdateAsync(Guid id, CustomerTitleUpdateDto input)
        {

            var customerTitle = await _customerTitleManager.UpdateAsync(
            id,
            input.TitleName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerTitle, CustomerTitleDto>(customerTitle);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerTitleExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _customerTitleRepository.GetListAsync(input.FilterText, input.TitleName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CustomerTitle>, List<CustomerTitleExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerTitles.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerTitleExcelDownloadTokenCacheItem { Token = token },
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