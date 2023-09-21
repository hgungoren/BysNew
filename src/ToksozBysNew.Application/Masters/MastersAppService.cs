using ToksozBysNew.Shared;
using ToksozBysNew.Companies;
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
using ToksozBysNew.Masters;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Masters
{

    [Authorize(ToksozBysNewPermissions.Masters.Default)]
    public class MastersAppService : ApplicationService, IMastersAppService
    {
        private readonly IDistributedCache<MasterExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IMasterRepository _masterRepository;
        private readonly MasterManager _masterManager;
        private readonly IRepository<Company, Guid> _companyRepository;

        public MastersAppService(IMasterRepository masterRepository, MasterManager masterManager, IDistributedCache<MasterExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Company, Guid> companyRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _masterRepository = masterRepository;
            _masterManager = masterManager; _companyRepository = companyRepository;
        }

        public virtual async Task<PagedResultDto<MasterWithNavigationPropertiesDto>> GetListAsync(GetMastersInput input)
        {
            var totalCount = await _masterRepository.GetCountAsync(input.FilterText, input.InvoiceSerialNo, input.InvoicePriceMin, input.InvoicePriceMax, input.InvoiceDateMin, input.InvoiceDateMax, input.InvoiceNote, input.CompanyId);
            var items = await _masterRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.InvoiceSerialNo, input.InvoicePriceMin, input.InvoicePriceMax, input.InvoiceDateMin, input.InvoiceDateMax, input.InvoiceNote, input.CompanyId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<MasterWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<MasterWithNavigationProperties>, List<MasterWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<MasterWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<MasterWithNavigationProperties, MasterWithNavigationPropertiesDto>
                (await _masterRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<MasterDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Master, MasterDto>(await _masterRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            var query = (await _companyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.CompanyName != null &&
                         x.CompanyName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Company>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Company>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(ToksozBysNewPermissions.Masters.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _masterRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Masters.Create)]
        public virtual async Task<MasterDto> CreateAsync(MasterCreateDto input)
        {

            var master = await _masterManager.CreateAsync(
            input.CompanyId, input.InvoiceSerialNo, input.InvoicePrice, input.InvoiceNote, input.InvoiceDate
            );

            return ObjectMapper.Map<Master, MasterDto>(master);
        }

        [Authorize(ToksozBysNewPermissions.Masters.Edit)]
        public virtual async Task<MasterDto> UpdateAsync(Guid id, MasterUpdateDto input)
        {

            var master = await _masterManager.UpdateAsync(
            id,
            input.CompanyId, input.InvoiceSerialNo, input.InvoicePrice, input.InvoiceNote, input.InvoiceDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Master, MasterDto>(master);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(MasterExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var masters = await _masterRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.InvoiceSerialNo, input.InvoicePriceMin, input.InvoicePriceMax, input.InvoiceDateMin, input.InvoiceDateMax, input.InvoiceNote);
            var items = masters.Select(item => new
            {
                InvoiceSerialNo = item.Master.InvoiceSerialNo,
                InvoicePrice = item.Master.InvoicePrice,
                InvoiceDate = item.Master.InvoiceDate,
                InvoiceNote = item.Master.InvoiceNote,

                CompanyCompanyName = item.Company?.CompanyName,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Masters.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new MasterExcelDownloadTokenCacheItem { Token = token },
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