using ToksozBysNew.Shared;
using ToksozBysNew.Specs;
using Volo.Abp.Identity;
using ToksozBysNew.Bricks;
using ToksozBysNew.Clinics;
using ToksozBysNew.Units;
using ToksozBysNew.Doctors;
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
using ToksozBysNew.Visits;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;
using Microsoft.CodeAnalysis;
using ToksozBysNew.VisitDailyActions;
using Volo.Abp.ObjectMapping;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToksozBysNew.Visits
{

    [Authorize(ToksozBysNewPermissions.Visits.Default)]
    public class VisitsAppService : ApplicationService, IVisitsAppService
    {
        private readonly IDistributedCache<VisitExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IVisitRepository _visitRepository;
        private readonly VisitManager _visitManager;
        private readonly IRepository<Doctor, Guid> _doctorRepository;
        private readonly IRepository<Unit, Guid> _unitRepository;
        private readonly IRepository<Clinic, Guid> _clinicRepository;
        private readonly IRepository<Brick, Guid> _brickRepository;
        private readonly IRepository<IdentityUser, Guid> _identityUserRepository;
        private readonly IRepository<Spec, Guid> _specRepository;

        public VisitsAppService(IVisitRepository visitRepository, VisitManager visitManager, IDistributedCache<VisitExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Doctor, Guid> doctorRepository, IRepository<Unit, Guid> unitRepository, IRepository<Clinic, Guid> clinicRepository, IRepository<Brick, Guid> brickRepository, IRepository<IdentityUser, Guid> identityUserRepository, IRepository<Spec, Guid> specRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _visitRepository = visitRepository;
            _visitManager = visitManager; _doctorRepository = doctorRepository;
            _unitRepository = unitRepository;
            _clinicRepository = clinicRepository;
            _brickRepository = brickRepository;
            _identityUserRepository = identityUserRepository;
            _specRepository = specRepository;
        }

        public virtual async Task<PagedResultDto<VisitWithNavigationPropertiesDto>> GetListAsync(GetVisitsInput input)
        {
            var totalCount = await _visitRepository.GetCountAsync(input.FilterText, input.VisitDateMin, input.VisitDateMax, input.VisitNotes, input.DoctorId, input.UnitId, input.ClinicId, input.BrickId, input.IdentityUserId, input.SpecId);
            var items = await _visitRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.VisitDateMin, input.VisitDateMax, input.VisitNotes, input.DoctorId, input.UnitId, input.ClinicId, input.BrickId, input.IdentityUserId, input.SpecId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<VisitWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VisitWithNavigationProperties>, List<VisitWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<VisitWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<VisitWithNavigationProperties, VisitWithNavigationPropertiesDto>
                (await _visitRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<VisitDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Visit, VisitDto>(await _visitRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input)
        {
            var query = (await _doctorRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.NameSurname != null &&
                         x.NameSurname.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Doctor>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Doctor>, List<LookupDto<Guid>>>(lookupData)
            };
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetClinicLookupAsync(LookupRequestDto input)
        {
            var query = (await _clinicRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.ClinicName != null &&
                         x.ClinicName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Clinic>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Clinic>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetBrickLookupAsync(LookupRequestDto input)
        {
            var query = (await _brickRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.BrickName != null &&
                         x.BrickName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Brick>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Brick>, List<LookupDto<Guid>>>(lookupData)
            };
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSpecLookupAsync(LookupRequestDto input)
        {
            var query = (await _specRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.SpecCode != null &&
                         x.SpecCode.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Spec>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Spec>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(ToksozBysNewPermissions.Visits.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _visitRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Visits.Create)]
        public virtual async Task<VisitDto> CreateAsync(VisitCreateDto input)
        {

            var visit = await _visitManager.CreateAsync(
            input.DoctorId, input.UnitId, input.ClinicId, input.BrickId, input.IdentityUserId, input.SpecId, input.VisitDate, input.VisitNotes
            );

            return ObjectMapper.Map<Visit, VisitDto>(visit);
        }

        [Authorize(ToksozBysNewPermissions.Visits.Edit)]
        public virtual async Task<VisitDto> UpdateAsync(Guid id, VisitUpdateDto input)
        {

            var visit = await _visitManager.UpdateAsync(
            id,
            input.DoctorId, input.UnitId, input.ClinicId, input.BrickId, input.IdentityUserId, input.SpecId, input.VisitDate, input.VisitNotes, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Visit, VisitDto>(visit);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(VisitExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var visits = await _visitRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.VisitDateMin, input.VisitDateMax, input.VisitNotes);
            var items = visits.Select(item => new
            {
                VisitDate = item.Visit.VisitDate,
                VisitNotes = item.Visit.VisitNotes,

                DoctorNameSurname = item.Doctor?.NameSurname,
                UnitUnitName = item.Unit?.UnitName,
                ClinicClinicName = item.Clinic?.ClinicName,
                BrickBrickName = item.Brick?.BrickName,
                IdentityUserUserName = item.IdentityUser?.UserName,
                SpecSpecCode = item.Spec?.SpecCode,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Visits.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new VisitExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }

        public async Task<PagedResultDto<VisitDto>> GetVisitListAsync(Guid id, DateTime date)
        {
            var visitList =await _visitRepository.GetListAsync().Result.ToDynamicListAsync();
            var doctorList = _doctorRepository.GetListAsync().Result.ToList();
            var specList = _specRepository.GetListAsync().Result.ToList();
            var unitList = _unitRepository.GetListAsync().Result.ToList();

            var query = (visitList)
                .Join(doctorList, d => d.DoctorId, a => a.Id, (d, a) => new { visit = d, doctor = a })
                .Join(specList, b => b.doctor.SpecId, c => c.Id, (b, c) => new { doctor = b, spec = c })
                .Join(unitList, x => x.doctor.visit.UnitId, f => f.Id, (x, f) => new { doctor = x, unit = f })
                .Where(g => g.doctor.doctor.visit.IdentityUserId == id && g.doctor.doctor.visit.VisitDate== date)
                .Select(x => new VisitDto
                {
                    Id = x.doctor.doctor.visit.Id,
                    CustomerName=x.doctor.doctor.doctor.NameSurname,
                    VisitNotes= x.doctor.doctor.visit.VisitNotes,
                    SpecCode= x.doctor.spec.SpecCode,
                    UnitName= x.unit.UnitName,
                    CustomerTypeId= x.doctor.doctor.doctor.CustomerTypeId,
                    IdentityUserId= x.doctor.doctor.visit.IdentityUserId 
                });
            var lookupData = query.ToList();
            var totalCount = query.Count();
            return new PagedResultDto<VisitDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VisitDto>, List<VisitDto>>(lookupData)
            };
        }

        public async Task<PagedResultDto<VisitDto>> GetVisitListWithCustomerNameAsync(string name)
        {
            var visitList = await _visitRepository.GetListAsync().Result.ToDynamicListAsync();
            var doctorList = _doctorRepository.GetListAsync().Result.ToList();
            var specList = _specRepository.GetListAsync().Result.ToList();
            var unitList = _unitRepository.GetListAsync().Result.ToList();

            var query = (visitList)
                .Join(doctorList, d => d.DoctorId, a => a.Id, (d, a) => new { visit = d, doctor = a })
                .Join(specList, b => b.doctor.SpecId, c => c.Id, (b, c) => new { doctor = b, spec = c })
                .Join(unitList, x => x.doctor.visit.UnitId, f => f.Id, (x, f) => new { doctor = x, unit = f })
                .Where(g => g.doctor.doctor.doctor.NameSurname.Contains(name))
                .Select(x => new VisitDto
                {
                    Id = x.doctor.doctor.visit.Id,
                    CustomerName = x.doctor.doctor.doctor.NameSurname,
                    VisitNotes = x.doctor.doctor.visit.VisitNotes,
                    SpecCode = x.doctor.spec.SpecCode,
                    UnitName = x.unit.UnitName,
                    CustomerTypeId = x.doctor.doctor.doctor.CustomerTypeId,
                    IdentityUserId = x.doctor.doctor.visit.IdentityUserId
                });
            var lookupData = query.ToList();
            var totalCount = query.Count();
            return new PagedResultDto<VisitDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VisitDto>, List<VisitDto>>(lookupData)
            };
        }
    }
}