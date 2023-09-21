using ToksozBysNew.Shared;
using ToksozBysNew.Provinces;
using ToksozBysNew.Countries;
using ToksozBysNew.Districts;
using ToksozBysNew.Bricks;
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
using ToksozBysNew.CustomerAddresses;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;
using ToksozBysNew.CustomerTitles;
using ToksozBysNew.CustomerTypes;
using ToksozBysNew.Positions;
using ToksozBysNew.Specs;
using ToksozBysNew.Units;

namespace ToksozBysNew.CustomerAddresses
{

    [Authorize(ToksozBysNewPermissions.CustomerAddresses.Default)]
    public class CustomerAddressesAppService : ApplicationService, ICustomerAddressesAppService
    {
        private readonly IDistributedCache<CustomerAddressExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICustomerAddressRepository _customerAddressRepository;
        private readonly CustomerAddressManager _customerAddressManager;
        private readonly IRepository<Doctor, Guid> _doctorRepository;
        private readonly IRepository<Brick, Guid> _brickRepository;
        private readonly IRepository<District, Guid> _districtRepository;
        private readonly IRepository<Country, Guid> _countryRepository;
        private readonly IRepository<Province, Guid> _provinceRepository;
        private readonly IRepository<Position, Guid> _positionRepository;
        private readonly IRepository<Unit, Guid> _unitRepository;
        private readonly IRepository<Spec, Guid> _specRepository;
        private readonly IRepository<CustomerTitle, Guid> _titleRepository;

        public CustomerAddressesAppService(ICustomerAddressRepository customerAddressRepository, CustomerAddressManager customerAddressManager, IDistributedCache<CustomerAddressExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Doctor, Guid> doctorRepository, IRepository<Brick, Guid> brickRepository, IRepository<District, Guid> districtRepository, IRepository<Country, Guid> countryRepository, IRepository<Province, Guid> provinceRepository, IRepository<Position, Guid> positionRepository, IRepository<Unit, Guid> unitRepository, IRepository<Spec, Guid> specRepository, IRepository<CustomerTitle, Guid> titleRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerAddressRepository = customerAddressRepository;
            _customerAddressManager = customerAddressManager; _doctorRepository = doctorRepository;
            _brickRepository = brickRepository;
            _districtRepository = districtRepository;
            _countryRepository = countryRepository;
            _provinceRepository = provinceRepository;
            _positionRepository = positionRepository;
            _unitRepository = unitRepository;
            _specRepository = specRepository;
            _titleRepository = titleRepository;
        }

        public virtual async Task<PagedResultDto<CustomerAddressWithNavigationPropertiesDto>> GetListAsync(GetCustomerAddressesInput input)
        {
            var totalCount = await _customerAddressRepository.GetCountAsync(input.FilterText, input.Address, input.DoctorId, input.BrickId, input.DistrictId, input.CountryId, input.ProvinceId);
            var items = await _customerAddressRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Address, input.DoctorId, input.BrickId, input.DistrictId, input.CountryId, input.ProvinceId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerAddressWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerAddressWithNavigationProperties>, List<CustomerAddressWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CustomerAddressWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerAddressWithNavigationProperties, CustomerAddressWithNavigationPropertiesDto>
                (await _customerAddressRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CustomerAddressDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerAddress, CustomerAddressDto>(await _customerAddressRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDistrictLookupAsync(LookupRequestDto input)
        {
            var query = (await _districtRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.DistrictName != null &&
                         x.DistrictName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<District>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<District>, List<LookupDto<Guid>>>(lookupData)
            };
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

        [Authorize(ToksozBysNewPermissions.CustomerAddresses.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerAddressRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.CustomerAddresses.Create)]
        public virtual async Task<CustomerAddressDto> CreateAsync(CustomerAddressCreateDto input)
        {

            var customerAddress = await _customerAddressManager.CreateAsync(
            input.DoctorId, input.BrickId, input.DistrictId, input.CountryId, input.ProvinceId, input.Address
            );

            return ObjectMapper.Map<CustomerAddress, CustomerAddressDto>(customerAddress);
        }

        [Authorize(ToksozBysNewPermissions.CustomerAddresses.Edit)]
        public virtual async Task<CustomerAddressDto> UpdateAsync(Guid id, CustomerAddressUpdateDto input)
        {

            var customerAddress = await _customerAddressManager.UpdateAsync(
            id,
            input.DoctorId, input.BrickId, input.DistrictId, input.CountryId, input.ProvinceId, input.Address, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerAddress, CustomerAddressDto>(customerAddress);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAddressExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var customerAddresses = await _customerAddressRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Address);
            var items = customerAddresses.Select(item => new
            {
                Address = item.CustomerAddress.Address,

                DoctorNameSurname = item.Doctor?.NameSurname,
                BrickBrickName = item.Brick?.BrickName,
                DistrictDistrictName = item.District?.DistrictName,
                CountryCountryName = item.Country?.CountryName,
                ProvinceProvinceName = item.Province?.ProvinceName,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerAddresses.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerAddressExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }

        public async Task<PagedResultDto<CustomerAddressDto>> GetListWithDoctorIdAsync(Guid id)
        { 
            var brickList = _brickRepository.GetListAsync().Result.ToList();
            var doctorList = await _doctorRepository.GetListAsync().Result.ToDynamicListAsync(); 
            var addressList = _customerAddressRepository.GetListAsync().Result.ToList(); 
            var countryList = _countryRepository.GetListAsync().Result.ToList(); 
            var provinceList = _provinceRepository.GetListAsync().Result.ToList(); 
            var districtList = _districtRepository.GetListAsync().Result.ToList(); 
            var positionList = _positionRepository.GetListAsync().Result.ToList(); 
            var unitList = _unitRepository.GetListAsync().Result.ToList(); 
            var specList = _specRepository.GetListAsync().Result.ToList(); 
            var titleList = _titleRepository.GetListAsync().Result.ToList(); 

            var query = (doctorList)
                .Join(addressList, d => d.Id, a => a.DoctorId, (d, a) => new { d, a }) 
                .Join(countryList, b => b.a.CountryId, c => c.Id, (b, c) => new { b, c }) 
                .Join(provinceList, e => e.b.a.ProvinceId, f => f.Id, (e, f) => new { e, f }) 
                .Join(districtList, g => g.e.b.a.DistrictId, h => h.Id, (g, h) => new { g, h }) 
                .Join(brickList, i => i.g.e.b.a.BrickId, j => j.Id, (i, j) => new { i, j })  
                .Join(positionList, k => k.i.g.e.b.d.PositionId, l => l.Id, (k, l) => new { k, l })  
                .Join(unitList, m => m.k.i.g.e.b.d.UnitId, n => n.Id, (m, n) => new { m, n })  
                .Join(specList, o =>o.m.k.i.g.e.b.d.SpecId, p => p.Id, (o, p) => new { o, p })   
                .Join(titleList, r =>r.o.m.k.i.g.e.b.d.CustomerTitleId, s => s.Id, (r, s) => new { r, s })   
                .Select(x => new CustomerAddressDto
                {
                    Brick = x.r.o.m.k.j.BrickName,
                    //IsActive = x.r.o.m.k.i.g.e.b.d.IsActive,
                    District=x.r.o.m.k.i.h.DistrictName,
                    Address=x.r.o.m.k.i.g.e.b.a.Address,
                    Country=x.r.o.m.k.i.g.e.c.CountryName,
                    Province=x.r.o.m.k.i.g.f.ProvinceName,
                    DoctorNameSurname = x.r.o.m.k.i.g.e.b.d.NameSurname,
                    LastModificationTime = x.r.o.m.k.i.g.e.b.d.LastModificationTime, 
                    DoctorId = x.r.o.m.k.i.g.e.b.d.Id,
                    Id= x.r.o.m.k.i.g.e.b.a.Id
                }).Where(x => x.DoctorId == id);
            var lookupData = query.ToList();
            var totalCount = query.Count();
            return  new PagedResultDto<CustomerAddressDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerAddressDto>, List<CustomerAddressDto>>(lookupData)
            };
        }
    }
}