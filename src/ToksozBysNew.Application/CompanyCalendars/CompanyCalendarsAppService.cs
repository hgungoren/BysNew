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
using ToksozBysNew.CompanyCalendars;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.CompanyCalendars
{

    [Authorize(ToksozBysNewPermissions.CompanyCalendars.Default)]
    public class CompanyCalendarsAppService : ApplicationService, ICompanyCalendarsAppService
    {
        private readonly IDistributedCache<CompanyCalendarExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICompanyCalendarRepository _companyCalendarRepository;
        private readonly CompanyCalendarManager _companyCalendarManager;

        public CompanyCalendarsAppService(ICompanyCalendarRepository companyCalendarRepository, CompanyCalendarManager companyCalendarManager, IDistributedCache<CompanyCalendarExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _companyCalendarRepository = companyCalendarRepository;
            _companyCalendarManager = companyCalendarManager;
        }

        public virtual async Task<PagedResultDto<CompanyCalendarDto>> GetListAsync(GetCompanyCalendarsInput input)
        {
            var totalCount = await _companyCalendarRepository.GetCountAsync(input.FilterText, input.CompanyCalendarDateMin, input.CompanyCalendarDateMax, input.IsWeekend, input.IsHoliday);
            var items = await _companyCalendarRepository.GetListAsync(input.FilterText, input.CompanyCalendarDateMin, input.CompanyCalendarDateMax, input.IsWeekend, input.IsHoliday, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CompanyCalendarDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CompanyCalendar>, List<CompanyCalendarDto>>(items)
            };
        }

        public virtual async Task<CompanyCalendarDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CompanyCalendar, CompanyCalendarDto>(await _companyCalendarRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.CompanyCalendars.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _companyCalendarRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.CompanyCalendars.Create)]
        public virtual async Task<CompanyCalendarDto> CreateAsync(CompanyCalendarCreateDto input)
        {

            var companyCalendar = await _companyCalendarManager.CreateAsync(
            input.CompanyCalendarDate, input.IsWeekend, input.IsHoliday
            );

            return ObjectMapper.Map<CompanyCalendar, CompanyCalendarDto>(companyCalendar);
        }

        [Authorize(ToksozBysNewPermissions.CompanyCalendars.Edit)]
        public virtual async Task<CompanyCalendarDto> UpdateAsync(Guid id, CompanyCalendarUpdateDto input)
        {

            var companyCalendar = await _companyCalendarManager.UpdateAsync(
            id,
            input.CompanyCalendarDate, input.IsWeekend, input.IsHoliday, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CompanyCalendar, CompanyCalendarDto>(companyCalendar);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CompanyCalendarExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _companyCalendarRepository.GetListAsync(input.FilterText, input.CompanyCalendarDateMin, input.CompanyCalendarDateMax, input.IsWeekend, input.IsHoliday);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CompanyCalendar>, List<CompanyCalendarExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CompanyCalendars.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CompanyCalendarExcelDownloadTokenCacheItem { Token = token },
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