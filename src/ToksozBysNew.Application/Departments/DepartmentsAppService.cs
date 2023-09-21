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
using ToksozBysNew.Departments;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Departments
{

    [Authorize(ToksozBysNewPermissions.Departments.Default)]
    public class DepartmentsAppService : ApplicationService, IDepartmentsAppService
    {
        private readonly IDistributedCache<DepartmentExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly DepartmentManager _departmentManager;
        private readonly IRepository<Company, Guid> _companyRepository;

        public DepartmentsAppService(IDepartmentRepository departmentRepository, DepartmentManager departmentManager, IDistributedCache<DepartmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Company, Guid> companyRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _departmentRepository = departmentRepository;
            _departmentManager = departmentManager; _companyRepository = companyRepository;
        }

        public virtual async Task<PagedResultDto<DepartmentWithNavigationPropertiesDto>> GetListAsync(GetDepartmentsInput input)
        {
            var totalCount = await _departmentRepository.GetCountAsync(input.FilterText, input.DepartmentName, input.CompanyId);
            var items = await _departmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.DepartmentName, input.CompanyId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DepartmentWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DepartmentWithNavigationProperties>, List<DepartmentWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<DepartmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<DepartmentWithNavigationProperties, DepartmentWithNavigationPropertiesDto>
                (await _departmentRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<DepartmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Department, DepartmentDto>(await _departmentRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            var query = (await _companyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.CompanyName != null &&
                         x.CompanyName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Company>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Company>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(ToksozBysNewPermissions.Departments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _departmentRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Departments.Create)]
        public virtual async Task<DepartmentDto> CreateAsync(DepartmentCreateDto input)
        {

            var department = await _departmentManager.CreateAsync(
            input.CompanyId, input.DepartmentName
            );

            return ObjectMapper.Map<Department, DepartmentDto>(department);
        }

        [Authorize(ToksozBysNewPermissions.Departments.Edit)]
        public virtual async Task<DepartmentDto> UpdateAsync(Guid id, DepartmentUpdateDto input)
        {

            var department = await _departmentManager.UpdateAsync(
            id,
            input.CompanyId, input.DepartmentName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Department, DepartmentDto>(department);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DepartmentExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _departmentRepository.GetListAsync(input.FilterText, input.DepartmentName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Department>, List<DepartmentExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Departments.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new DepartmentExcelDownloadTokenCacheItem { Token = token },
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