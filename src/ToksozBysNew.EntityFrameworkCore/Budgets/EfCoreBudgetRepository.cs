using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using ToksozBysNew.EntityFrameworkCore;

namespace ToksozBysNew.Budgets
{
    public class EfCoreBudgetRepository : EfCoreRepository<ToksozBysNewDbContext, Budget, Guid>, IBudgetRepository
    {
        public EfCoreBudgetRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<BudgetWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(budget => new BudgetWithNavigationProperties
                {
                    Budget = budget,
                    Company = dbContext.Companies.FirstOrDefault(c => c.Id == budget.CompanyId)
                }).FirstOrDefault();
        }

        public async Task<List<BudgetWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string budgetName = null,
            int? yearMin = null,
            int? yearMax = null,
            string comment = null,
            bool? isActive = null,
            DateTime? openUntilMin = null,
            DateTime? openUntilMax = null,
            Guid? companyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, budgetName, yearMin, yearMax, comment, isActive, openUntilMin, openUntilMax, companyId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BudgetConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<BudgetWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from budget in (await GetDbSetAsync())
                   join company in (await GetDbContextAsync()).Companies on budget.CompanyId equals company.Id into companies
                   from company in companies.DefaultIfEmpty()

                   select new BudgetWithNavigationProperties
                   {
                       Budget = budget,
                       Company = company
                   };
        }

        protected virtual IQueryable<BudgetWithNavigationProperties> ApplyFilter(
            IQueryable<BudgetWithNavigationProperties> query,
            string filterText,
            string budgetName = null,
            int? yearMin = null,
            int? yearMax = null,
            string comment = null,
            bool? isActive = null,
            DateTime? openUntilMin = null,
            DateTime? openUntilMax = null,
            Guid? companyId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Budget.BudgetName.Contains(filterText) || e.Budget.Comment.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(budgetName), e => e.Budget.BudgetName.Contains(budgetName))
                    .WhereIf(yearMin.HasValue, e => e.Budget.Year >= yearMin.Value)
                    .WhereIf(yearMax.HasValue, e => e.Budget.Year <= yearMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(comment), e => e.Budget.Comment.Contains(comment))
                    .WhereIf(isActive.HasValue, e => e.Budget.IsActive == isActive)
                    .WhereIf(openUntilMin.HasValue, e => e.Budget.OpenUntil >= openUntilMin.Value)
                    .WhereIf(openUntilMax.HasValue, e => e.Budget.OpenUntil <= openUntilMax.Value)
                    .WhereIf(companyId != null && companyId != Guid.Empty, e => e.Company != null && e.Company.Id == companyId);
        }

        public async Task<List<Budget>> GetListAsync(
            string filterText = null,
            string budgetName = null,
            int? yearMin = null,
            int? yearMax = null,
            string comment = null,
            bool? isActive = null,
            DateTime? openUntilMin = null,
            DateTime? openUntilMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, budgetName, yearMin, yearMax, comment, isActive, openUntilMin, openUntilMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BudgetConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string budgetName = null,
            int? yearMin = null,
            int? yearMax = null,
            string comment = null,
            bool? isActive = null,
            DateTime? openUntilMin = null,
            DateTime? openUntilMax = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, budgetName, yearMin, yearMax, comment, isActive, openUntilMin, openUntilMax, companyId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Budget> ApplyFilter(
            IQueryable<Budget> query,
            string filterText,
            string budgetName = null,
            int? yearMin = null,
            int? yearMax = null,
            string comment = null,
            bool? isActive = null,
            DateTime? openUntilMin = null,
            DateTime? openUntilMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.BudgetName.Contains(filterText) || e.Comment.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(budgetName), e => e.BudgetName.Contains(budgetName))
                    .WhereIf(yearMin.HasValue, e => e.Year >= yearMin.Value)
                    .WhereIf(yearMax.HasValue, e => e.Year <= yearMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(comment), e => e.Comment.Contains(comment))
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive)
                    .WhereIf(openUntilMin.HasValue, e => e.OpenUntil >= openUntilMin.Value)
                    .WhereIf(openUntilMax.HasValue, e => e.OpenUntil <= openUntilMax.Value);
        }
    }
}