using ToksozBysNew.Shared;
using ToksozBysNew.Specs;
using ToksozBysNew.Units;
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
using ToksozBysNew.Clinics;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Clinics
{

    [Authorize(ToksozBysNewPermissions.Clinics.Default)]
    public class ClinicsAppService : ApplicationService, IClinicsAppService
    {
        private readonly IDistributedCache<ClinicExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IClinicRepository _clinicRepository;
        private readonly ClinicManager _clinicManager;
        private readonly IRepository<Unit, Guid> _unitRepository;
        private readonly IRepository<Spec, Guid> _specRepository;

        public ClinicsAppService(IClinicRepository clinicRepository, ClinicManager clinicManager, IDistributedCache<ClinicExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Unit, Guid> unitRepository, IRepository<Spec, Guid> specRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _clinicRepository = clinicRepository;
            _clinicManager = clinicManager; _unitRepository = unitRepository;
            _specRepository = specRepository;
        }

        public virtual async Task<PagedResultDto<ClinicWithNavigationPropertiesDto>> GetListAsync(GetClinicsInput input)
        {
            var totalCount = await _clinicRepository.GetCountAsync(input.FilterText, input.ClinicName, input.UnitId, input.SpecId);
            var items = await _clinicRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.ClinicName, input.UnitId, input.SpecId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ClinicWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ClinicWithNavigationProperties>, List<ClinicWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ClinicWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ClinicWithNavigationProperties, ClinicWithNavigationPropertiesDto>
                (await _clinicRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ClinicDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Clinic, ClinicDto>(await _clinicRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetUnitLookupAsync(LookupRequestDto input)
        {
            var query = (await _unitRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.UnitName != null &&
                         x.UnitName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Unit>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Unit>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSpecLookupAsync(LookupRequestDto input)
        {
            var query = (await _specRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.SpecName != null &&
                         x.SpecName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Spec>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Spec>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(ToksozBysNewPermissions.Clinics.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _clinicRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Clinics.Create)]
        public virtual async Task<ClinicDto> CreateAsync(ClinicCreateDto input)
        {

            var clinic = await _clinicManager.CreateAsync(
            input.UnitId, input.SpecId, input.ClinicName
            );

            return ObjectMapper.Map<Clinic, ClinicDto>(clinic);
        }

        [Authorize(ToksozBysNewPermissions.Clinics.Edit)]
        public virtual async Task<ClinicDto> UpdateAsync(Guid id, ClinicUpdateDto input)
        {

            var clinic = await _clinicManager.UpdateAsync(
            id,
            input.UnitId, input.SpecId, input.ClinicName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Clinic, ClinicDto>(clinic);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ClinicExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var clinics = await _clinicRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.ClinicName);
            var items = clinics.Select(item => new
            {
                ClinicName = item.Clinic.ClinicName,

                UnitUnitName = item.Unit?.UnitName,
                SpecSpecName = item.Spec?.SpecName,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Clinics.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ClinicExcelDownloadTokenCacheItem { Token = token },
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