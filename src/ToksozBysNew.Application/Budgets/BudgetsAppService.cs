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
using ToksozBysNew.Budgets;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Budgets
{

    [Authorize(ToksozBysNewPermissions.Budgets.Default)]
    public class BudgetsAppService : ApplicationService, IBudgetsAppService
    {
        private readonly IDistributedCache<BudgetExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IBudgetRepository _budgetRepository;
        private readonly BudgetManager _budgetManager;
        private readonly IRepository<Company, Guid> _companyRepository;

        public BudgetsAppService(IBudgetRepository budgetRepository, BudgetManager budgetManager, IDistributedCache<BudgetExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Company, Guid> companyRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _budgetRepository = budgetRepository;
            _budgetManager = budgetManager; _companyRepository = companyRepository;
        }

        public virtual async Task<PagedResultDto<BudgetWithNavigationPropertiesDto>> GetListAsync(GetBudgetsInput input)
        {
            var totalCount = await _budgetRepository.GetCountAsync(input.FilterText, input.BudgetName, input.YearMin, input.YearMax, input.Comment, input.IsActive, input.OpenUntilMin, input.OpenUntilMax, input.CompanyId);
            var items = await _budgetRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.BudgetName, input.YearMin, input.YearMax, input.Comment, input.IsActive, input.OpenUntilMin, input.OpenUntilMax, input.CompanyId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<BudgetWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<BudgetWithNavigationProperties>, List<BudgetWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<BudgetWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<BudgetWithNavigationProperties, BudgetWithNavigationPropertiesDto>
                (await _budgetRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<BudgetDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Budget, BudgetDto>(await _budgetRepository.GetAsync(id));
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

        [Authorize(ToksozBysNewPermissions.Budgets.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _budgetRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Budgets.Create)]
        public virtual async Task<BudgetDto> CreateAsync(BudgetCreateDto input)
        {

            var budget = await _budgetManager.CreateAsync(
            input.CompanyId, input.BudgetName, input.Year, input.Comment, input.IsActive, input.OpenUntil
            );

            return ObjectMapper.Map<Budget, BudgetDto>(budget);
        }

        [Authorize(ToksozBysNewPermissions.Budgets.Edit)]
        public virtual async Task<BudgetDto> UpdateAsync(Guid id, BudgetUpdateDto input)
        {

            var budget = await _budgetManager.UpdateAsync(
            id,
            input.CompanyId, input.BudgetName, input.Year, input.Comment, input.IsActive, input.OpenUntil, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Budget, BudgetDto>(budget);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(BudgetExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _budgetRepository.GetListAsync(input.FilterText, input.BudgetName, input.YearMin, input.YearMax, input.Comment, input.IsActive, input.OpenUntilMin, input.OpenUntilMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Budget>, List<BudgetExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Budgets.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new BudgetExcelDownloadTokenCacheItem { Token = token },
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