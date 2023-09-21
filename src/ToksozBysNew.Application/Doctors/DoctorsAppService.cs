using ToksozBysNew.Shared;
using ToksozBysNew.CustomerTypes;
using ToksozBysNew.Units;
using ToksozBysNew.CustomerTitles;
using ToksozBysNew.Specs;
using ToksozBysNew.Positions;
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
using ToksozBysNew.Doctors;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;
using ToksozBysNew.Bricks;
using ToksozBysNew.CustomerAddresses;

namespace ToksozBysNew.Doctors
{

    [Authorize(ToksozBysNewPermissions.Doctors.Default)]
    public class DoctorsAppService : ApplicationService, IDoctorsAppService
    {
        private readonly IDistributedCache<DoctorExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IDoctorRepository _doctorRepository;
        private readonly DoctorManager _doctorManager;
        private readonly IRepository<Position, Guid> _positionRepository;
        private readonly IRepository<Spec, Guid> _specRepository;
        private readonly IRepository<CustomerTitle, Guid> _customerTitleRepository;
        private readonly IRepository<Unit, Guid> _unitRepository;
        private readonly IRepository<CustomerType, Guid> _customerTypeRepository;
        private readonly IRepository<Brick, Guid> _brickRepository; 
        private readonly IRepository<CustomerAddress, Guid> _addressRepository;

        public DoctorsAppService(IDoctorRepository doctorRepository, DoctorManager doctorManager, IDistributedCache<DoctorExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Position, Guid> positionRepository, IRepository<Spec, Guid> specRepository, IRepository<CustomerTitle, Guid> customerTitleRepository, IRepository<Unit, Guid> unitRepository, IRepository<CustomerType, Guid> customerTypeRepository, IRepository<Brick, Guid> brickRepository, IRepository<CustomerAddress, Guid> addressRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _doctorRepository = doctorRepository;
            _doctorManager = doctorManager; _positionRepository = positionRepository;
            _specRepository = specRepository;
            _customerTitleRepository = customerTitleRepository;
            _unitRepository = unitRepository;
            _customerTypeRepository = customerTypeRepository;
            _brickRepository= brickRepository;
            _addressRepository= addressRepository;
        }

        public virtual async Task<PagedResultDto<DoctorWithNavigationPropertiesDto>> GetListAsync(GetDoctorsInput input)
        {
            var totalCount = await _doctorRepository.GetCountAsync(input.FilterText, input.IsActive, input.NameSurname, input.PharmacyName, input.PositionId, input.SpecId, input.CustomerTitleId, input.UnitId, input.CustomerTypeId);
            var items = await _doctorRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.IsActive, input.NameSurname, input.PharmacyName, input.PositionId, input.SpecId, input.CustomerTitleId, input.UnitId, input.CustomerTypeId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DoctorWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DoctorWithNavigationProperties>, List<DoctorWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<DoctorWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<DoctorWithNavigationProperties, DoctorWithNavigationPropertiesDto>
                (await _doctorRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<DoctorDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Doctor, DoctorDto>(await _doctorRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPositionLookupAsync(LookupRequestDto input)
        {
            var query = (await _positionRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.PositionName != null &&
                         x.PositionName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Position>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Position>, List<LookupDto<Guid>>>(lookupData)
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerTitleLookupAsync(LookupRequestDto input)
        {
            var query = (await _customerTitleRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.TitleName != null &&
                         x.TitleName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<CustomerTitle>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerTitle>, List<LookupDto<Guid>>>(lookupData)
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerTypeLookupAsync(LookupRequestDto input)
        {
            var query = (await _customerTypeRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.TypeName != null &&
                         x.TypeName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<CustomerType>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerType>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(ToksozBysNewPermissions.Doctors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _doctorRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Doctors.Create)]
        public virtual async Task<DoctorDto> CreateAsync(DoctorCreateDto input)
        {

            var doctor = await _doctorManager.CreateAsync(
            input.PositionId, input.SpecId, input.CustomerTitleId, input.UnitId, input.CustomerTypeId, input.IsActive, input.NameSurname, input.PharmacyName
            );

            return ObjectMapper.Map<Doctor, DoctorDto>(doctor);
        }

        [Authorize(ToksozBysNewPermissions.Doctors.Edit)]
        public virtual async Task<DoctorDto> UpdateAsync(Guid id, DoctorUpdateDto input)
        {

            var doctor = await _doctorManager.UpdateAsync(
            id,
            input.PositionId, input.SpecId, input.CustomerTitleId, input.UnitId, input.CustomerTypeId, input.IsActive, input.NameSurname, input.PharmacyName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Doctor, DoctorDto>(doctor);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DoctorExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var doctors = await _doctorRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.IsActive, input.NameSurname, input.PharmacyName);
            var items = doctors.Select(item => new
            {
                IsActive = item.Doctor.IsActive,
                NameSurname = item.Doctor.NameSurname,
                PharmacyName = item.Doctor.PharmacyName,

                PositionPositionName = item.Position?.PositionName,
                SpecSpecName = item.Spec?.SpecName,
                CustomerTitleTitleName = item.CustomerTitle?.TitleName,
                UnitUnitName = item.Unit?.UnitName,
                CustomerTypeTypeName = item.CustomerType?.TypeName,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Doctors.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new DoctorExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
        public async Task<SpecDto> GetSpecById(Guid id)
        {
            var data = await _specRepository.GetAsync(id);

            return ObjectMapper.Map<Spec, SpecDto>(data);
        }

        public async Task<PagedResultDto<DoctorDto>> GetListWithBrickNameByDoctorIdAsync(Guid id)
        {
            var unitList = await _unitRepository.GetListAsync().Result.ToDynamicListAsync();
            var brickList = _brickRepository.GetListAsync().Result.ToList();
            var doctorList = _doctorRepository.GetListAsync().Result.ToList();
            var positionList = _positionRepository.GetListAsync().Result.ToList();
            var customerTitleList = _customerTitleRepository.GetListAsync().Result.ToList();
            var specList = _specRepository.GetListAsync().Result.ToList();
            var typeList = _customerTypeRepository.GetListAsync().Result.ToList();
            var addressList = _addressRepository.GetListAsync().Result.ToList();

            var query = (doctorList)
                .Join(unitList, u => u.UnitId, d => d.Id, (u, d) => new { u, d })
                .Join(brickList, a => a.d.BrickId, b => b.Id, (a, b) => new { a, b })
                .Join(positionList, z => z.a.u.PositionId, p => p.Id, (z, p) => new { z, p })
                .Join(customerTitleList, k => k.z.a.u.CustomerTitleId, n => n.Id, (k, n) => new { k, n })
                .Join(specList, s => s.k.z.a.u.SpecId, n => n.Id, (s, n) => new { s, n })
                .Join(typeList, h =>h.s.k.z.a.u.CustomerTypeId, j => j.Id, (h, j) => new { h, j })
                .Join(addressList, c =>c.h.s.k.z.a.u.Id, e => e.DoctorId, (c, e) => new { c, e})
                .Select(m => new DoctorDto
                {
                    BrickName =m.c.h.s.k.z.b.BrickName,
                    IsActive = m.c.h.s.k.z.a.u.IsActive,
                    NameSurname = m.c.h.s.k.z.a.u.NameSurname,
                    LastModificationTime = m.c.h.s.k.z.a.u.LastModificationTime,
                    PharmacyName = m.c.h.s.k.z.a.u.PharmacyName,
                    Position = m.c.h.s.k.p.PositionName,
                    UnitId = m.c.h.s.k.z.a.d.Id,
                    Unit = m.c.h.s.k.z.a.d.UnitName,
                    Title = m.c.h.s.n.TitleName,
                    Spec = m.c.h.n.SpecName,
                    Id = m.c.h.s.k.z.a.u.Id,
                    CustomerType= m.c.j.TypeName,
                    Address=m.e.Address,
                    ProvinceId=m.e.ProvinceId,
                    DistrictId=m.e.DistrictId,
                    CountryId=m.e.CountryId
                }).Where(x => x.Id == id);
            var lookupData = query.ToList();
            var totalCount = query.Count();
            return new PagedResultDto<DoctorDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DoctorDto>, List<DoctorDto>>(lookupData)
            };
        }
    }
}