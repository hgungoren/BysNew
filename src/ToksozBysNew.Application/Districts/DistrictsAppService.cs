using ToksozBysNew.Shared;
using ToksozBysNew.Provinces;
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
using ToksozBysNew.Districts;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Districts
{

    [Authorize(ToksozBysNewPermissions.Districts.Default)]
    public class DistrictsAppService : ApplicationService, IDistrictsAppService
    {
        private readonly IDistributedCache<DistrictExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IDistrictRepository _districtRepository;
        private readonly DistrictManager _districtManager;
        private readonly IRepository<Country, Guid> _countryRepository;
        private readonly IRepository<Province, Guid> _provinceRepository;

        public DistrictsAppService(IDistrictRepository districtRepository, DistrictManager districtManager, IDistributedCache<DistrictExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Country, Guid> countryRepository, IRepository<Province, Guid> provinceRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _districtRepository = districtRepository;
            _districtManager = districtManager; _countryRepository = countryRepository;
            _provinceRepository = provinceRepository;
        }

        public virtual async Task<PagedResultDto<DistrictWithNavigationPropertiesDto>> GetListAsync(GetDistrictsInput input)
        {
            var totalCount = await _districtRepository.GetCountAsync(input.FilterText, input.DistrictName, input.CountryId, input.ProvinceId);
            var items = await _districtRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.DistrictName, input.CountryId, input.ProvinceId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DistrictWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DistrictWithNavigationProperties>, List<DistrictWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<DistrictWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<DistrictWithNavigationProperties, DistrictWithNavigationPropertiesDto>
                (await _districtRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<DistrictDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<District, DistrictDto>(await _districtRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetProvinceLookupAsync(LookupRequestDto input)
        {
            var query = (await _provinceRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.ProvinceName != null &&
                         x.ProvinceName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Province>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Province>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(ToksozBysNewPermissions.Districts.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _districtRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Districts.Create)]
        public virtual async Task<DistrictDto> CreateAsync(DistrictCreateDto input)
        {

            var district = await _districtManager.CreateAsync(
            input.CountryId, input.ProvinceId, input.DistrictName
            );

            return ObjectMapper.Map<District, DistrictDto>(district);
        }

        [Authorize(ToksozBysNewPermissions.Districts.Edit)]
        public virtual async Task<DistrictDto> UpdateAsync(Guid id, DistrictUpdateDto input)
        {

            var district = await _districtManager.UpdateAsync(
            id,
            input.CountryId, input.ProvinceId, input.DistrictName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<District, DistrictDto>(district);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DistrictExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var districts = await _districtRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.DistrictName);
            var items = districts.Select(item => new
            {
                DistrictName = item.District.DistrictName,

                CountryCountryName = item.Country?.CountryName,
                ProvinceProvinceName = item.Province?.ProvinceName,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Districts.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new DistrictExcelDownloadTokenCacheItem { Token = token },
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