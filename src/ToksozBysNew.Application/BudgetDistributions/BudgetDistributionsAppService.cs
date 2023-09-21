using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using ToksozBysNew.AccountGroups;
using ToksozBysNew.Accounts;
using ToksozBysNew.Budgets;
using ToksozBysNew.Departments;
using ToksozBysNew.Permissions;
using ToksozBysNew.Products;
using ToksozBysNew.Shared;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace ToksozBysNew.BudgetDistributions
{

    [Authorize(ToksozBysNewPermissions.BudgetDistributions.Default)]
    public class BudgetDistributionsAppService : ApplicationService, IBudgetDistributionsAppService
    {
        private readonly IDistributedCache<BudgetDistributionExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IBudgetDistributionRepository _budgetDistributionRepository;
        private readonly BudgetDistributionManager _budgetDistributionManager;
        private readonly IRepository<Department, Guid> _departmentRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<Budget, Guid> _budgetRepository;
        private readonly IRepository<AccountGroup, Guid> _accountGroupRepository;
        private readonly IRepository<Account, Guid> _accountRepository;
        private readonly IRepository<IdentityUser, Guid> _identityUserRepository;

        public BudgetDistributionsAppService(IBudgetDistributionRepository budgetDistributionRepository, BudgetDistributionManager budgetDistributionManager, IDistributedCache<BudgetDistributionExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Department, Guid> departmentRepository, IRepository<Product, Guid> productRepository, IRepository<Budget, Guid> budgetRepository, IRepository<AccountGroup, Guid> accountGroupRepository, IRepository<Account, Guid> accountRepository, IRepository<IdentityUser, Guid> identityUserRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _budgetDistributionRepository = budgetDistributionRepository;
            _budgetDistributionManager = budgetDistributionManager; _departmentRepository = departmentRepository;
            _productRepository = productRepository;
            _budgetRepository = budgetRepository;
            _accountGroupRepository = accountGroupRepository;
            _accountRepository = accountRepository;
            _identityUserRepository = identityUserRepository;
        }

        public virtual async Task<PagedResultDto<BudgetDistributionWithNavigationPropertiesDto>> GetListAsync(GetBudgetDistributionsInput input)
        {
            var totalCount = await _budgetDistributionRepository.GetCountAsync(input.FilterText, input.CostCenter, input.ExpenseType, input.ProjectItemMin, input.ProjectItemMax, input.TypeMin, input.TypeMax, input.UnitMin, input.UnitMax, input.UnitValueMin, input.UnitValueMax, input.MonthMin, input.MonthMax, input.YearMin, input.YearMax, input.RatioMin, input.RatioMax, input.AmountMin, input.AmountMax, input.MemoMin, input.MemoMax, input.InvoiceMin, input.InvoiceMax, input.CurrencyMin, input.CurrencyMax, input.CurrencyAmountMin, input.CurrencyAmountMax, input.ExpenseCategoryMin, input.ExpenseCategoryMax, input.ExpenseNecessityMin, input.ExpenseNecessityMax, input.Comment, input.Status, input.ApprovalMin, input.ApprovalMax, input.IsActive, input.DepartmentId, input.ProductId, input.BudgetId, input.AccountGroupId, input.AccountId, input.IdentityUserId);
            var items = await _budgetDistributionRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.CostCenter, input.ExpenseType, input.ProjectItemMin, input.ProjectItemMax, input.TypeMin, input.TypeMax, input.UnitMin, input.UnitMax, input.UnitValueMin, input.UnitValueMax, input.MonthMin, input.MonthMax, input.YearMin, input.YearMax, input.RatioMin, input.RatioMax, input.AmountMin, input.AmountMax, input.MemoMin, input.MemoMax, input.InvoiceMin, input.InvoiceMax, input.CurrencyMin, input.CurrencyMax, input.CurrencyAmountMin, input.CurrencyAmountMax, input.ExpenseCategoryMin, input.ExpenseCategoryMax, input.ExpenseNecessityMin, input.ExpenseNecessityMax, input.Comment, input.Status, input.ApprovalMin, input.ApprovalMax, input.IsActive, input.DepartmentId, input.ProductId, input.BudgetId, input.AccountGroupId, input.AccountId, input.IdentityUserId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<BudgetDistributionWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<BudgetDistributionWithNavigationProperties>, List<BudgetDistributionWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<BudgetDistributionWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<BudgetDistributionWithNavigationProperties, BudgetDistributionWithNavigationPropertiesDto>
                (await _budgetDistributionRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<BudgetDistributionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<BudgetDistribution, BudgetDistributionDto>(await _budgetDistributionRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input)
        {
            var query = (await _departmentRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.DepartmentName != null &&
                         x.DepartmentName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Department>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Department>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetProductLookupAsync(LookupRequestDto input)
        {
            var query = (await _productRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.ProductName != null &&
                         x.ProductName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Product>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Product>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetBudgetLookupAsync(LookupRequestDto input)
        {
            var query = (await _budgetRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.BudgetName != null &&
                         x.BudgetName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Budget>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Budget>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetAccountGroupLookupAsync(LookupRequestDto input)
        {
            var query = (await _accountGroupRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AccountGroupName != null &&
                         x.AccountGroupName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<AccountGroup>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AccountGroup>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetAccountLookupAsync(LookupRequestDto input)
        {
            var query = (await _accountRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AccountName != null &&
                         x.AccountName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Account>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Account>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetIdentityUserLookupAsync(LookupRequestDto input)
        {
            var query = (await _identityUserRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.UserName != null &&
                         x.UserName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<IdentityUser>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<IdentityUser>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(ToksozBysNewPermissions.BudgetDistributions.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _budgetDistributionRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.BudgetDistributions.Create)]
        public virtual async Task<BudgetDistributionDto> CreateAsync(BudgetDistributionCreateDto input)
        {
            if (input.DepartmentId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Department"]]);
            }
            if (input.BudgetId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Budget"]]);
            }

            var budgetDistribution = await _budgetDistributionManager.CreateAsync(
            input.DepartmentId, input.ProductId, input.BudgetId, input.AccountGroupId, input.AccountId, input.IdentityUserId, input.CostCenter, input.ExpenseType, input.Month, input.Amount, input.Comment, input.Status, input.IsActive, input.ProjectItem, input.Type, input.Unit, input.UnitValue, input.Year, input.Ratio, input.Memo, input.Invoice, input.Currency, input.CurrencyAmount, input.ExpenseCategory, input.ExpenseNecessity, input.Approval
            );

            return ObjectMapper.Map<BudgetDistribution, BudgetDistributionDto>(budgetDistribution);
        }

        [Authorize(ToksozBysNewPermissions.BudgetDistributions.Edit)]
        public virtual async Task<BudgetDistributionDto> UpdateAsync(Guid id, BudgetDistributionUpdateDto input)
        {
            if (input.DepartmentId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Department"]]);
            }
            if (input.BudgetId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Budget"]]);
            }

            var budgetDistribution = await _budgetDistributionManager.UpdateAsync(
            id,
            input.DepartmentId, input.ProductId, input.BudgetId, input.AccountGroupId, input.AccountId, input.IdentityUserId, input.CostCenter, input.ExpenseType, input.Month, input.Amount, input.Comment, input.Status, input.IsActive, input.ProjectItem, input.Type, input.Unit, input.UnitValue, input.Year, input.Ratio, input.Memo, input.Invoice, input.Currency, input.CurrencyAmount, input.ExpenseCategory, input.ExpenseNecessity, input.Approval, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<BudgetDistribution, BudgetDistributionDto>(budgetDistribution);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(BudgetDistributionExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _budgetDistributionRepository.GetListAsync(input.FilterText, input.CostCenter, input.ExpenseType, input.ProjectItemMin, input.ProjectItemMax, input.TypeMin, input.TypeMax, input.UnitMin, input.UnitMax, input.UnitValueMin, input.UnitValueMax, input.MonthMin, input.MonthMax, input.YearMin, input.YearMax, input.RatioMin, input.RatioMax, input.AmountMin, input.AmountMax, input.MemoMin, input.MemoMax, input.InvoiceMin, input.InvoiceMax, input.CurrencyMin, input.CurrencyMax, input.CurrencyAmountMin, input.CurrencyAmountMax, input.ExpenseCategoryMin, input.ExpenseCategoryMax, input.ExpenseNecessityMin, input.ExpenseNecessityMax, input.Comment, input.Status, input.ApprovalMin, input.ApprovalMax, input.IsActive);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<BudgetDistribution>, List<BudgetDistributionExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "BudgetDistributions.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new BudgetDistributionExcelDownloadTokenCacheItem { Token = token },
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