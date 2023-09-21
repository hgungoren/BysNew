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
using ToksozBysNew.Specs;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;
using ToksozBysNew.InvoiceDetails;

namespace ToksozBysNew.Specs
{

    [Authorize(ToksozBysNewPermissions.Specs.Default)]
    public class SpecsAppService : ApplicationService, ISpecsAppService
    {
        private readonly IDistributedCache<SpecExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ISpecRepository _specRepository;
        private readonly SpecManager _specManager;

        public SpecsAppService(ISpecRepository specRepository, SpecManager specManager, IDistributedCache<SpecExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _specRepository = specRepository;
            _specManager = specManager;
        }

        public virtual async Task<PagedResultDto<SpecDto>> GetListAsync(GetSpecsInput input)
        {
            var totalCount = await _specRepository.GetCountAsync(input.FilterText, input.SpecCode, input.SpecName);
            var items = await _specRepository.GetListAsync(input.FilterText, input.SpecCode, input.SpecName, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SpecDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Spec>, List<SpecDto>>(items)
            };
        }

        public virtual async Task<SpecDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Spec, SpecDto>(await _specRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.Specs.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _specRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Specs.Create)]
        public virtual async Task<SpecDto> CreateAsync(SpecCreateDto input)
        {

            var spec = await _specManager.CreateAsync(
            input.SpecCode, input.SpecName
            );

            return ObjectMapper.Map<Spec, SpecDto>(spec);
        }

        [Authorize(ToksozBysNewPermissions.Specs.Edit)]
        public virtual async Task<SpecDto> UpdateAsync(Guid id, SpecUpdateDto input)
        {

            var spec = await _specManager.UpdateAsync(
            id,
            input.SpecCode, input.SpecName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Spec, SpecDto>(spec);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SpecExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _specRepository.GetListAsync(input.FilterText, input.SpecCode, input.SpecName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Spec>, List<SpecExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Specs.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new SpecExcelDownloadTokenCacheItem { Token = token },
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