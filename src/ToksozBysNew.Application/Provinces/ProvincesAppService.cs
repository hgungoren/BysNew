using ToksozBysNew.Shared;
using ToksozBysNew.Countries;
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
using ToksozBysNew.Provinces;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Provinces
{

    [Authorize(ToksozBysNewPermissions.Provinces.Default)]
    public class ProvincesAppService : ApplicationService, IProvincesAppService
    {
        private readonly IDistributedCache<ProvinceExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IProvinceRepository _provinceRepository;
        private readonly ProvinceManager _provinceManager;
        private readonly IRepository<Country, Guid> _countryRepository;

        public ProvincesAppService(IProvinceRepository provinceRepository, ProvinceManager provinceManager, IDistributedCache<ProvinceExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Country, Guid> countryRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _provinceRepository = provinceRepository;
            _provinceManager = provinceManager; _countryRepository = countryRepository;
        }

        public virtual async Task<PagedResultDto<ProvinceWithNavigationPropertiesDto>> GetListAsync(GetProvincesInput input)
        {
            var totalCount = await _provinceRepository.GetCountAsync(input.FilterText, input.ProvinceName, input.CountryId);
            var items = await _provinceRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.ProvinceName, input.CountryId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ProvinceWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ProvinceWithNavigationProperties>, List<ProvinceWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ProvinceWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ProvinceWithNavigationProperties, ProvinceWithNavigationPropertiesDto>
                (await _provinceRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ProvinceDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Province, ProvinceDto>(await _provinceRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCountryLookupAsync(LookupRequestDto input)
        {
            var query = (await _countryRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.CountryName != null &&
                         x.CountryName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Country>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Country>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(ToksozBysNewPermissions.Provinces.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _provinceRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Provinces.Create)]
        public virtual async Task<ProvinceDto> CreateAsync(ProvinceCreateDto input)
        {

            var province = await _provinceManager.CreateAsync(
            input.CountryId, input.ProvinceName
            );

            return ObjectMapper.Map<Province, ProvinceDto>(province);
        }

        [Authorize(ToksozBysNewPermissions.Provinces.Edit)]
        public virtual async Task<ProvinceDto> UpdateAsync(Guid id, ProvinceUpdateDto input)
        {

            var province = await _provinceManager.UpdateAsync(
            id,
            input.CountryId, input.ProvinceName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Province, ProvinceDto>(province);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProvinceExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var provinces = await _provinceRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.ProvinceName);
            var items = provinces.Select(item => new
            {
                ProvinceName = item.Province.ProvinceName,

                CountryCountryName = item.Country?.CountryName,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Provinces.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ProvinceExcelDownloadTokenCacheItem { Token = token },
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