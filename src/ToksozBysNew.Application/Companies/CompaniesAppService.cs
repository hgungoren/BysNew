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
using ToksozBysNew.Companies;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;
using ToksozBysNew.Departments;

namespace ToksozBysNew.Companies
{

    [Authorize(ToksozBysNewPermissions.Companies.Default)]
    public class CompaniesAppService : ApplicationService, ICompaniesAppService
    {
        private readonly IDistributedCache<CompanyExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICompanyRepository _companyRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly CompanyManager _companyManager;

        public CompaniesAppService(ICompanyRepository companyRepository, IDepartmentRepository departmentRepository,CompanyManager companyManager, IDistributedCache<CompanyExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _companyRepository = companyRepository;
            _companyManager = companyManager;
            _departmentRepository = departmentRepository;
        }

        public virtual async Task<PagedResultDto<CompanyDto>> GetListAsync(GetCompaniesInput input)
        {
            var totalCount = await _companyRepository.GetCountAsync(input.FilterText, input.CompanyName, input.IsActive);
            var items = await _companyRepository.GetListAsync(input.FilterText, input.CompanyName, input.IsActive, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CompanyDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Company>, List<CompanyDto>>(items)
            };
        }

        public virtual async Task<CompanyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Company, CompanyDto>(await _companyRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.Companies.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _companyRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Companies.Create)]
        public virtual async Task<CompanyDto> CreateAsync(CompanyCreateDto input)
        {

            var company = await _companyManager.CreateAsync(
            input.CompanyName, input.IsActive
            );

            return ObjectMapper.Map<Company, CompanyDto>(company);
        }

        [Authorize(ToksozBysNewPermissions.Companies.Edit)]
        public virtual async Task<CompanyDto> UpdateAsync(Guid id, CompanyUpdateDto input)
        {

            var company = await _companyManager.UpdateAsync(
            id,
            input.CompanyName, input.IsActive, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Company, CompanyDto>(company);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CompanyExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _companyRepository.GetListAsync(input.FilterText, input.CompanyName, input.IsActive);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Company>, List<CompanyExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Companies.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        } 

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CompanyExcelDownloadTokenCacheItem { Token = token },
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