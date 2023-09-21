using ToksozBysNew.Shared;
using Volo.Abp.Identity;
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
using ToksozBysNew.VisitDailyActions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;
using ToksozBysNew.Visits;
using ToksozBysNew.Doctors;
using ToksozBysNew.CompanyCalendars;
using Polly;
using static ToksozBysNew.Permissions.ToksozBysNewPermissions;
using ToksozBysNew.CustomerAddresses;

namespace ToksozBysNew.VisitDailyActions
{

    [Authorize(ToksozBysNewPermissions.VisitDailyActions.Default)]
    public class VisitDailyActionsAppService : ApplicationService, IVisitDailyActionsAppService
    {
        private readonly IDistributedCache<VisitDailyActionExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IVisitDailyActionRepository _visitDailyActionRepository;
        private readonly VisitDailyActionManager _visitDailyActionManager;
        private readonly IRepository<IdentityUser, Guid> _identityUserRepository;
        private readonly IRepository<Visit, Guid> _visitRepository;
        private readonly IRepository<Doctor, Guid> _doctorRepository;
        private readonly IRepository<CompanyCalendar, Guid> _calendarRepository;
        private readonly IRepository<VisitDailyAction, Guid> _visitDailyRepository;

        public VisitDailyActionsAppService(IVisitDailyActionRepository visitDailyActionRepository, VisitDailyActionManager visitDailyActionManager, IDistributedCache<VisitDailyActionExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<IdentityUser, Guid> identityUserRepository, IRepository<Visit, Guid> visitRepository, IRepository<Doctor, Guid> doctorRepository, IRepository<CompanyCalendar, Guid> calendarRepository, IRepository<VisitDailyAction, Guid> visitDailyRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _visitDailyActionRepository = visitDailyActionRepository;
            _visitDailyActionManager = visitDailyActionManager;
            _identityUserRepository = identityUserRepository;
            _visitRepository = visitRepository;
            _doctorRepository = doctorRepository;
            _calendarRepository = calendarRepository;
            _visitDailyActionRepository = visitDailyActionRepository;
        }

        public virtual async Task<PagedResultDto<VisitDailyActionWithNavigationPropertiesDto>> GetListAsync(GetVisitDailyActionsInput input)
        {
            var totalCount = await _visitDailyActionRepository.GetCountAsync(input.FilterText, input.VisitDailyDateMin, input.VisitDailyDateMax, input.VisitDaily1Min, input.VisitDaily1Max, input.VisitDaily2Min, input.VisitDaily2Max, input.VisitDaily3Min, input.VisitDaily3Max, input.VisitDaily4Min, input.VisitDaily4Max, input.VisitDaily5Min, input.VisitDaily5Max, input.VisitDaily6Min, input.VisitDaily6Max, input.VisitDaily7Min, input.VisitDaily7Max, input.VisitDaily8Min, input.VisitDaily8Max, input.VisitDaily9Min, input.VisitDaily9Max, input.VisitDaily10Min, input.VisitDaily10Max, input.VisitDaily11Min, input.VisitDaily11Max, input.VisitDaily12Min, input.VisitDaily12Max, input.VisitDaily13Min, input.VisitDaily13Max, input.VisitDaily14Min, input.VisitDaily14Max, input.VisitDaily15Min, input.VisitDaily15Max, input.VisitDailyCloseDateMin, input.VisitDailyCloseDateMax, input.VisitDailyNote, input.IdentityUserId);
            var items = await _visitDailyActionRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.VisitDailyDateMin, input.VisitDailyDateMax, input.VisitDaily1Min, input.VisitDaily1Max, input.VisitDaily2Min, input.VisitDaily2Max, input.VisitDaily3Min, input.VisitDaily3Max, input.VisitDaily4Min, input.VisitDaily4Max, input.VisitDaily5Min, input.VisitDaily5Max, input.VisitDaily6Min, input.VisitDaily6Max, input.VisitDaily7Min, input.VisitDaily7Max, input.VisitDaily8Min, input.VisitDaily8Max, input.VisitDaily9Min, input.VisitDaily9Max, input.VisitDaily10Min, input.VisitDaily10Max, input.VisitDaily11Min, input.VisitDaily11Max, input.VisitDaily12Min, input.VisitDaily12Max, input.VisitDaily13Min, input.VisitDaily13Max, input.VisitDaily14Min, input.VisitDaily14Max, input.VisitDaily15Min, input.VisitDaily15Max, input.VisitDailyCloseDateMin, input.VisitDailyCloseDateMax, input.VisitDailyNote, input.IdentityUserId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<VisitDailyActionWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VisitDailyActionWithNavigationProperties>, List<VisitDailyActionWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<VisitDailyActionWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<VisitDailyActionWithNavigationProperties, VisitDailyActionWithNavigationPropertiesDto>
                (await _visitDailyActionRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<VisitDailyActionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<VisitDailyAction, VisitDailyActionDto>(await _visitDailyActionRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input)
        {
            var query = (await _identityUserRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.UserName != null &&
                         x.UserName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<IdentityUser>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<IdentityUser>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(ToksozBysNewPermissions.VisitDailyActions.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _visitDailyActionRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.VisitDailyActions.Create)]
        public virtual async Task<VisitDailyActionDto> CreateAsync(VisitDailyActionCreateDto input)
        {

            var visitDailyAction = await _visitDailyActionManager.CreateAsync(
            input.IdentityUserId, input.VisitDailyDate, input.VisitDaily1, input.VisitDaily2, input.VisitDaily3, input.VisitDaily4, input.VisitDaily5, input.VisitDaily6, input.VisitDaily7, input.VisitDaily8, input.VisitDaily9, input.VisitDaily10, input.VisitDaily11, input.VisitDaily12, input.VisitDaily13, input.VisitDaily14, input.VisitDaily15, input.VisitDailyCloseDate, input.VisitDailyNote
            );

            return ObjectMapper.Map<VisitDailyAction, VisitDailyActionDto>(visitDailyAction);
        }

        [Authorize(ToksozBysNewPermissions.VisitDailyActions.Edit)]
        public virtual async Task<VisitDailyActionDto> UpdateAsync(Guid id, VisitDailyActionUpdateDto input)
        {

            var visitDailyAction = await _visitDailyActionManager.UpdateAsync(
            id,
            input.IdentityUserId, input.VisitDailyDate, input.VisitDaily1, input.VisitDaily2, input.VisitDaily3, input.VisitDaily4, input.VisitDaily5, input.VisitDaily6, input.VisitDaily7, input.VisitDaily8, input.VisitDaily9, input.VisitDaily10, input.VisitDaily11, input.VisitDaily12, input.VisitDaily13, input.VisitDaily14, input.VisitDaily15, input.VisitDailyCloseDate, input.VisitDailyNote, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<VisitDailyAction, VisitDailyActionDto>(visitDailyAction);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(VisitDailyActionExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var visitDailyActions = await _visitDailyActionRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.VisitDailyDateMin, input.VisitDailyDateMax, input.VisitDaily1Min, input.VisitDaily1Max, input.VisitDaily2Min, input.VisitDaily2Max, input.VisitDaily3Min, input.VisitDaily3Max, input.VisitDaily4Min, input.VisitDaily4Max, input.VisitDaily5Min, input.VisitDaily5Max, input.VisitDaily6Min, input.VisitDaily6Max, input.VisitDaily7Min, input.VisitDaily7Max, input.VisitDaily8Min, input.VisitDaily8Max, input.VisitDaily9Min, input.VisitDaily9Max, input.VisitDaily10Min, input.VisitDaily10Max, input.VisitDaily11Min, input.VisitDaily11Max, input.VisitDaily12Min, input.VisitDaily12Max, input.VisitDaily13Min, input.VisitDaily13Max, input.VisitDaily14Min, input.VisitDaily14Max, input.VisitDaily15Min, input.VisitDaily15Max, input.VisitDailyCloseDateMin, input.VisitDailyCloseDateMax, input.VisitDailyNote);
            var items = visitDailyActions.Select(item => new
            {
                VisitDailyDate = item.VisitDailyAction.VisitDailyDate,
                VisitDaily1 = item.VisitDailyAction.VisitDaily1,
                VisitDaily2 = item.VisitDailyAction.VisitDaily2,
                VisitDaily3 = item.VisitDailyAction.VisitDaily3,
                VisitDaily4 = item.VisitDailyAction.VisitDaily4,
                VisitDaily5 = item.VisitDailyAction.VisitDaily5,
                VisitDaily6 = item.VisitDailyAction.VisitDaily6,
                VisitDaily7 = item.VisitDailyAction.VisitDaily7,
                VisitDaily8 = item.VisitDailyAction.VisitDaily8,
                VisitDaily9 = item.VisitDailyAction.VisitDaily9,
                VisitDaily10 = item.VisitDailyAction.VisitDaily10,
                VisitDaily11 = item.VisitDailyAction.VisitDaily11,
                VisitDaily12 = item.VisitDailyAction.VisitDaily12,
                VisitDaily13 = item.VisitDailyAction.VisitDaily13,
                VisitDaily14 = item.VisitDailyAction.VisitDaily14,
                VisitDaily15 = item.VisitDailyAction.VisitDaily15,
                VisitDailyCloseDate = item.VisitDailyAction.VisitDailyCloseDate,
                VisitDailyNote = item.VisitDailyAction.VisitDailyNote,

                IdentityUserUserName = item.IdentityUser?.UserName,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "VisitDailyActions.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new VisitDailyActionExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }

        //public static int GetVisitCountByType(Guid typeId, Guid userId, DateTime date)
        //{
        //    var visitList = _visitRepository.GetListAsync().Result.ToList();
        //    var doctorList = _doctorRepository.GetListAsync().Result.ToList();
              
        //    var query = (visitList)
        //        .Join(doctorList, d => d.DoctorId, a => a.Id, (d, a) => new { visit = d, doctor = a })
        //        .Where(x => x.doctor.CustomerTypeId == typeId && x.visit.IdentityUserId == userId && x.visit.VisitDate == date)
        //        .Select(x => new VisitDailyActionDto
        //        {
        //            Id = x.visit.Id
        //        });

        //    var totalCount = query.Count();
        //    return totalCount;
        //}

        public async Task<PagedResultDto<VisitDailyActionDto>> GetCustomListAsync(Guid userId)
        {
            IQueryable<CompanyCalendar> calendar = await _calendarRepository.GetQueryableAsync();
            IQueryable<VisitDailyAction> visitDaily = await _visitDailyActionRepository.GetQueryableAsync();
            IQueryable<Visit> visit = await _visitRepository.GetQueryableAsync(); 
            IQueryable<Doctor> doctor = await _doctorRepository.GetQueryableAsync(); 

            var query = from c in calendar
                        from v in visitDaily.Where(x => c.CompanyCalendarDate == x.VisitDailyDate && x.IdentityUserId == userId)
                                   .DefaultIfEmpty()
                        select new VisitDailyActionDto
                        {
                            CompanyCalendarDate = c.CompanyCalendarDate,
                            IsHoliday = c.IsHoliday,
                            IsWeekend = c.IsWeekend,
                            Id = v.Id,
                            IdentityUserId = v.IdentityUserId,
                            LastModificationTime = v.LastModificationTime,
                            VisitDaily1 = v.VisitDaily1,
                            VisitDaily2 = v.VisitDaily2,
                            VisitDaily3 = v.VisitDaily3,
                            VisitDaily4 = v.VisitDaily4,
                            VisitDaily5 = v.VisitDaily5,
                            VisitDaily6 = v.VisitDaily6,
                            VisitDaily7 = v.VisitDaily7,
                            VisitDaily8 = v.VisitDaily8,
                            VisitDaily9 = v.VisitDaily9,
                            VisitDaily10 = v.VisitDaily10,
                            VisitDaily11 = v.VisitDaily11,
                            VisitDaily12 = v.VisitDaily12,
                            VisitDaily13 = v.VisitDaily13,
                            VisitDaily14 = v.VisitDaily14,
                            VisitDaily15 = v.VisitDaily15,
                            VisitDailyCloseDate = v.VisitDailyCloseDate,
                            VisitDailyDate = v.VisitDailyDate,
                            VisitDailyNote = v.VisitDailyNote,
                            DoctorVisitCount = 
                            //(from vs in visit
                            //join d in doctor on vs.DoctorId equals d.Id
                            //where d.CustomerTypeId == Guid.Parse("E2B70D1F-802C-4F50-8C44-F30BA371896A")&&vs.IdentityUserId==userId&&vs.VisitDate==c.CompanyCalendarDate
                            //select new VisitDto
                            //{
                            //       Id = vs.Id
                            //}).Count(),
                            0,
                            PharmaVisitCount = 
                            //(from vs in visit
                            // join d in doctor on vs.DoctorId equals d.Id
                            // where d.CustomerTypeId == Guid.Parse("62211596-389B-4755-8CE6-760741E0393F") && vs.IdentityUserId == userId && vs.VisitDate == c.CompanyCalendarDate
                            // select new VisitDto
                            // {
                            //     Id = vs.Id
                            // }).Count(),
                            0
                        };
            var lookupData = query.ToList();
            var totalCount = query.Count();
            return new PagedResultDto<VisitDailyActionDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VisitDailyActionDto>, List<VisitDailyActionDto>>(lookupData)
            };

        }
    }
}