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

namespace ToksozBysNew.BudgetDistributions
{
    public class EfCoreBudgetDistributionRepository : EfCoreRepository<ToksozBysNewDbContext, BudgetDistribution, Guid>, IBudgetDistributionRepository
    {
        public EfCoreBudgetDistributionRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<BudgetDistributionWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(budgetDistribution => new BudgetDistributionWithNavigationProperties
                {
                    BudgetDistribution = budgetDistribution,
                    Department = dbContext.Departments.FirstOrDefault(c => c.Id == budgetDistribution.DepartmentId),
                    Product = dbContext.Products.FirstOrDefault(c => c.Id == budgetDistribution.ProductId),
                    Budget = dbContext.Budgets.FirstOrDefault(c => c.Id == budgetDistribution.BudgetId),
                    AccountGroup = dbContext.AccountGroups.FirstOrDefault(c => c.Id == budgetDistribution.AccountGroupId),
                    Account = dbContext.Accounts.FirstOrDefault(c => c.Id == budgetDistribution.AccountId),
                    IdentityUser = dbContext.Users.FirstOrDefault(c => c.Id == budgetDistribution.IdentityUserId)
                }).FirstOrDefault();
        }

        public async Task<List<BudgetDistributionWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string costCenter = null,
            string expenseType = null,
            int? projectItemMin = null,
            int? projectItemMax = null,
            int? typeMin = null,
            int? typeMax = null,
            int? unitMin = null,
            int? unitMax = null,
            float? unitValueMin = null,
            float? unitValueMax = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            float? ratioMin = null,
            float? ratioMax = null,
            float? amountMin = null,
            float? amountMax = null,
            float? memoMin = null,
            float? memoMax = null,
            float? invoiceMin = null,
            float? invoiceMax = null,
            int? currencyMin = null,
            int? currencyMax = null,
            float? currencyAmountMin = null,
            float? currencyAmountMax = null,
            int? expenseCategoryMin = null,
            int? expenseCategoryMax = null,
            int? expenseNecessityMin = null,
            int? expenseNecessityMax = null,
            string comment = null,
            string status = null,
            int? approvalMin = null,
            int? approvalMax = null,
            bool? isActive = null,
            Guid? departmentId = null,
            Guid? productId = null,
            Guid? budgetId = null,
            Guid? accountGroupId = null,
            Guid? accountId = null,
            Guid? identityUserId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, costCenter, expenseType, projectItemMin, projectItemMax, typeMin, typeMax, unitMin, unitMax, unitValueMin, unitValueMax, monthMin, monthMax, yearMin, yearMax, ratioMin, ratioMax, amountMin, amountMax, memoMin, memoMax, invoiceMin, invoiceMax, currencyMin, currencyMax, currencyAmountMin, currencyAmountMax, expenseCategoryMin, expenseCategoryMax, expenseNecessityMin, expenseNecessityMax, comment, status, approvalMin, approvalMax, isActive, departmentId, productId, budgetId, accountGroupId, accountId, identityUserId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BudgetDistributionConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<BudgetDistributionWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from budgetDistribution in (await GetDbSetAsync())
                   join department in (await GetDbContextAsync()).Departments on budgetDistribution.DepartmentId equals department.Id into departments
                   from department in departments.DefaultIfEmpty()
                   join product in (await GetDbContextAsync()).Products on budgetDistribution.ProductId equals product.Id into products
                   from product in products.DefaultIfEmpty()
                   join budget in (await GetDbContextAsync()).Budgets on budgetDistribution.BudgetId equals budget.Id into budgets
                   from budget in budgets.DefaultIfEmpty()
                   join accountGroup in (await GetDbContextAsync()).AccountGroups on budgetDistribution.AccountGroupId equals accountGroup.Id into accountGroups
                   from accountGroup in accountGroups.DefaultIfEmpty()
                   join account in (await GetDbContextAsync()).Accounts on budgetDistribution.AccountId equals account.Id into accounts
                   from account in accounts.DefaultIfEmpty()
                   join identityUser in (await GetDbContextAsync()).Users on budgetDistribution.IdentityUserId equals identityUser.Id into users
                   from identityUser in users.DefaultIfEmpty()

                   select new BudgetDistributionWithNavigationProperties
                   {
                       BudgetDistribution = budgetDistribution,
                       Department = department,
                       Product = product,
                       Budget = budget,
                       AccountGroup = accountGroup,
                       Account = account,
                       IdentityUser = identityUser
                   };
        }

        protected virtual IQueryable<BudgetDistributionWithNavigationProperties> ApplyFilter(
            IQueryable<BudgetDistributionWithNavigationProperties> query,
            string filterText,
            string costCenter = null,
            string expenseType = null,
            int? projectItemMin = null,
            int? projectItemMax = null,
            int? typeMin = null,
            int? typeMax = null,
            int? unitMin = null,
            int? unitMax = null,
            float? unitValueMin = null,
            float? unitValueMax = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            float? ratioMin = null,
            float? ratioMax = null,
            float? amountMin = null,
            float? amountMax = null,
            float? memoMin = null,
            float? memoMax = null,
            float? invoiceMin = null,
            float? invoiceMax = null,
            int? currencyMin = null,
            int? currencyMax = null,
            float? currencyAmountMin = null,
            float? currencyAmountMax = null,
            int? expenseCategoryMin = null,
            int? expenseCategoryMax = null,
            int? expenseNecessityMin = null,
            int? expenseNecessityMax = null,
            string comment = null,
            string status = null,
            int? approvalMin = null,
            int? approvalMax = null,
            bool? isActive = null,
            Guid? departmentId = null,
            Guid? productId = null,
            Guid? budgetId = null,
            Guid? accountGroupId = null,
            Guid? accountId = null,
            Guid? identityUserId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.BudgetDistribution.CostCenter.Contains(filterText) || e.BudgetDistribution.ExpenseType.Contains(filterText) || e.BudgetDistribution.Comment.Contains(filterText) || e.BudgetDistribution.Status.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(costCenter), e => e.BudgetDistribution.CostCenter.Contains(costCenter))
                    .WhereIf(!string.IsNullOrWhiteSpace(expenseType), e => e.BudgetDistribution.ExpenseType.Contains(expenseType))
                    .WhereIf(projectItemMin.HasValue, e => e.BudgetDistribution.ProjectItem >= projectItemMin.Value)
                    .WhereIf(projectItemMax.HasValue, e => e.BudgetDistribution.ProjectItem <= projectItemMax.Value)
                    .WhereIf(typeMin.HasValue, e => e.BudgetDistribution.Type >= typeMin.Value)
                    .WhereIf(typeMax.HasValue, e => e.BudgetDistribution.Type <= typeMax.Value)
                    .WhereIf(unitMin.HasValue, e => e.BudgetDistribution.Unit >= unitMin.Value)
                    .WhereIf(unitMax.HasValue, e => e.BudgetDistribution.Unit <= unitMax.Value)
                    .WhereIf(unitValueMin.HasValue, e => e.BudgetDistribution.UnitValue >= unitValueMin.Value)
                    .WhereIf(unitValueMax.HasValue, e => e.BudgetDistribution.UnitValue <= unitValueMax.Value)
                    .WhereIf(monthMin.HasValue, e => e.BudgetDistribution.Month >= monthMin.Value)
                    .WhereIf(monthMax.HasValue, e => e.BudgetDistribution.Month <= monthMax.Value)
                    .WhereIf(yearMin.HasValue, e => e.BudgetDistribution.Year >= yearMin.Value)
                    .WhereIf(yearMax.HasValue, e => e.BudgetDistribution.Year <= yearMax.Value)
                    .WhereIf(ratioMin.HasValue, e => e.BudgetDistribution.Ratio >= ratioMin.Value)
                    .WhereIf(ratioMax.HasValue, e => e.BudgetDistribution.Ratio <= ratioMax.Value)
                    .WhereIf(amountMin.HasValue, e => e.BudgetDistribution.Amount >= amountMin.Value)
                    .WhereIf(amountMax.HasValue, e => e.BudgetDistribution.Amount <= amountMax.Value)
                    .WhereIf(memoMin.HasValue, e => e.BudgetDistribution.Memo >= memoMin.Value)
                    .WhereIf(memoMax.HasValue, e => e.BudgetDistribution.Memo <= memoMax.Value)
                    .WhereIf(invoiceMin.HasValue, e => e.BudgetDistribution.Invoice >= invoiceMin.Value)
                    .WhereIf(invoiceMax.HasValue, e => e.BudgetDistribution.Invoice <= invoiceMax.Value)
                    .WhereIf(currencyMin.HasValue, e => e.BudgetDistribution.Currency >= currencyMin.Value)
                    .WhereIf(currencyMax.HasValue, e => e.BudgetDistribution.Currency <= currencyMax.Value)
                    .WhereIf(currencyAmountMin.HasValue, e => e.BudgetDistribution.CurrencyAmount >= currencyAmountMin.Value)
                    .WhereIf(currencyAmountMax.HasValue, e => e.BudgetDistribution.CurrencyAmount <= currencyAmountMax.Value)
                    .WhereIf(expenseCategoryMin.HasValue, e => e.BudgetDistribution.ExpenseCategory >= expenseCategoryMin.Value)
                    .WhereIf(expenseCategoryMax.HasValue, e => e.BudgetDistribution.ExpenseCategory <= expenseCategoryMax.Value)
                    .WhereIf(expenseNecessityMin.HasValue, e => e.BudgetDistribution.ExpenseNecessity >= expenseNecessityMin.Value)
                    .WhereIf(expenseNecessityMax.HasValue, e => e.BudgetDistribution.ExpenseNecessity <= expenseNecessityMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(comment), e => e.BudgetDistribution.Comment.Contains(comment))
                    .WhereIf(!string.IsNullOrWhiteSpace(status), e => e.BudgetDistribution.Status.Contains(status))
                    .WhereIf(approvalMin.HasValue, e => e.BudgetDistribution.Approval >= approvalMin.Value)
                    .WhereIf(approvalMax.HasValue, e => e.BudgetDistribution.Approval <= approvalMax.Value)
                    .WhereIf(isActive.HasValue, e => e.BudgetDistribution.IsActive == isActive)
                    .WhereIf(departmentId != null && departmentId != Guid.Empty, e => e.Department != null && e.Department.Id == departmentId)
                    .WhereIf(productId != null && productId != Guid.Empty, e => e.Product != null && e.Product.Id == productId)
                    .WhereIf(budgetId != null && budgetId != Guid.Empty, e => e.Budget != null && e.Budget.Id == budgetId)
                    .WhereIf(accountGroupId != null && accountGroupId != Guid.Empty, e => e.AccountGroup != null && e.AccountGroup.Id == accountGroupId)
                    .WhereIf(accountId != null && accountId != Guid.Empty, e => e.Account != null && e.Account.Id == accountId)
                    .WhereIf(identityUserId != null && identityUserId != Guid.Empty, e => e.IdentityUser != null && e.IdentityUser.Id == identityUserId);
        }

        public async Task<List<BudgetDistribution>> GetListAsync(
            string filterText = null,
            string costCenter = null,
            string expenseType = null,
            int? projectItemMin = null,
            int? projectItemMax = null,
            int? typeMin = null,
            int? typeMax = null,
            int? unitMin = null,
            int? unitMax = null,
            float? unitValueMin = null,
            float? unitValueMax = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            float? ratioMin = null,
            float? ratioMax = null,
            float? amountMin = null,
            float? amountMax = null,
            float? memoMin = null,
            float? memoMax = null,
            float? invoiceMin = null,
            float? invoiceMax = null,
            int? currencyMin = null,
            int? currencyMax = null,
            float? currencyAmountMin = null,
            float? currencyAmountMax = null,
            int? expenseCategoryMin = null,
            int? expenseCategoryMax = null,
            int? expenseNecessityMin = null,
            int? expenseNecessityMax = null,
            string comment = null,
            string status = null,
            int? approvalMin = null,
            int? approvalMax = null,
            bool? isActive = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, costCenter, expenseType, projectItemMin, projectItemMax, typeMin, typeMax, unitMin, unitMax, unitValueMin, unitValueMax, monthMin, monthMax, yearMin, yearMax, ratioMin, ratioMax, amountMin, amountMax, memoMin, memoMax, invoiceMin, invoiceMax, currencyMin, currencyMax, currencyAmountMin, currencyAmountMax, expenseCategoryMin, expenseCategoryMax, expenseNecessityMin, expenseNecessityMax, comment, status, approvalMin, approvalMax, isActive);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BudgetDistributionConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string costCenter = null,
            string expenseType = null,
            int? projectItemMin = null,
            int? projectItemMax = null,
            int? typeMin = null,
            int? typeMax = null,
            int? unitMin = null,
            int? unitMax = null,
            float? unitValueMin = null,
            float? unitValueMax = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            float? ratioMin = null,
            float? ratioMax = null,
            float? amountMin = null,
            float? amountMax = null,
            float? memoMin = null,
            float? memoMax = null,
            float? invoiceMin = null,
            float? invoiceMax = null,
            int? currencyMin = null,
            int? currencyMax = null,
            float? currencyAmountMin = null,
            float? currencyAmountMax = null,
            int? expenseCategoryMin = null,
            int? expenseCategoryMax = null,
            int? expenseNecessityMin = null,
            int? expenseNecessityMax = null,
            string comment = null,
            string status = null,
            int? approvalMin = null,
            int? approvalMax = null,
            bool? isActive = null,
            Guid? departmentId = null,
            Guid? productId = null,
            Guid? budgetId = null,
            Guid? accountGroupId = null,
            Guid? accountId = null,
            Guid? identityUserId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, costCenter, expenseType, projectItemMin, projectItemMax, typeMin, typeMax, unitMin, unitMax, unitValueMin, unitValueMax, monthMin, monthMax, yearMin, yearMax, ratioMin, ratioMax, amountMin, amountMax, memoMin, memoMax, invoiceMin, invoiceMax, currencyMin, currencyMax, currencyAmountMin, currencyAmountMax, expenseCategoryMin, expenseCategoryMax, expenseNecessityMin, expenseNecessityMax, comment, status, approvalMin, approvalMax, isActive, departmentId, productId, budgetId, accountGroupId, accountId, identityUserId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<BudgetDistribution> ApplyFilter(
            IQueryable<BudgetDistribution> query,
            string filterText,
            string costCenter = null,
            string expenseType = null,
            int? projectItemMin = null,
            int? projectItemMax = null,
            int? typeMin = null,
            int? typeMax = null,
            int? unitMin = null,
            int? unitMax = null,
            float? unitValueMin = null,
            float? unitValueMax = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            float? ratioMin = null,
            float? ratioMax = null,
            float? amountMin = null,
            float? amountMax = null,
            float? memoMin = null,
            float? memoMax = null,
            float? invoiceMin = null,
            float? invoiceMax = null,
            int? currencyMin = null,
            int? currencyMax = null,
            float? currencyAmountMin = null,
            float? currencyAmountMax = null,
            int? expenseCategoryMin = null,
            int? expenseCategoryMax = null,
            int? expenseNecessityMin = null,
            int? expenseNecessityMax = null,
            string comment = null,
            string status = null,
            int? approvalMin = null,
            int? approvalMax = null,
            bool? isActive = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CostCenter.Contains(filterText) || e.ExpenseType.Contains(filterText) || e.Comment.Contains(filterText) || e.Status.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(costCenter), e => e.CostCenter.Contains(costCenter))
                    .WhereIf(!string.IsNullOrWhiteSpace(expenseType), e => e.ExpenseType.Contains(expenseType))
                    .WhereIf(projectItemMin.HasValue, e => e.ProjectItem >= projectItemMin.Value)
                    .WhereIf(projectItemMax.HasValue, e => e.ProjectItem <= projectItemMax.Value)
                    .WhereIf(typeMin.HasValue, e => e.Type >= typeMin.Value)
                    .WhereIf(typeMax.HasValue, e => e.Type <= typeMax.Value)
                    .WhereIf(unitMin.HasValue, e => e.Unit >= unitMin.Value)
                    .WhereIf(unitMax.HasValue, e => e.Unit <= unitMax.Value)
                    .WhereIf(unitValueMin.HasValue, e => e.UnitValue >= unitValueMin.Value)
                    .WhereIf(unitValueMax.HasValue, e => e.UnitValue <= unitValueMax.Value)
                    .WhereIf(monthMin.HasValue, e => e.Month >= monthMin.Value)
                    .WhereIf(monthMax.HasValue, e => e.Month <= monthMax.Value)
                    .WhereIf(yearMin.HasValue, e => e.Year >= yearMin.Value)
                    .WhereIf(yearMax.HasValue, e => e.Year <= yearMax.Value)
                    .WhereIf(ratioMin.HasValue, e => e.Ratio >= ratioMin.Value)
                    .WhereIf(ratioMax.HasValue, e => e.Ratio <= ratioMax.Value)
                    .WhereIf(amountMin.HasValue, e => e.Amount >= amountMin.Value)
                    .WhereIf(amountMax.HasValue, e => e.Amount <= amountMax.Value)
                    .WhereIf(memoMin.HasValue, e => e.Memo >= memoMin.Value)
                    .WhereIf(memoMax.HasValue, e => e.Memo <= memoMax.Value)
                    .WhereIf(invoiceMin.HasValue, e => e.Invoice >= invoiceMin.Value)
                    .WhereIf(invoiceMax.HasValue, e => e.Invoice <= invoiceMax.Value)
                    .WhereIf(currencyMin.HasValue, e => e.Currency >= currencyMin.Value)
                    .WhereIf(currencyMax.HasValue, e => e.Currency <= currencyMax.Value)
                    .WhereIf(currencyAmountMin.HasValue, e => e.CurrencyAmount >= currencyAmountMin.Value)
                    .WhereIf(currencyAmountMax.HasValue, e => e.CurrencyAmount <= currencyAmountMax.Value)
                    .WhereIf(expenseCategoryMin.HasValue, e => e.ExpenseCategory >= expenseCategoryMin.Value)
                    .WhereIf(expenseCategoryMax.HasValue, e => e.ExpenseCategory <= expenseCategoryMax.Value)
                    .WhereIf(expenseNecessityMin.HasValue, e => e.ExpenseNecessity >= expenseNecessityMin.Value)
                    .WhereIf(expenseNecessityMax.HasValue, e => e.ExpenseNecessity <= expenseNecessityMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(comment), e => e.Comment.Contains(comment))
                    .WhereIf(!string.IsNullOrWhiteSpace(status), e => e.Status.Contains(status))
                    .WhereIf(approvalMin.HasValue, e => e.Approval >= approvalMin.Value)
                    .WhereIf(approvalMax.HasValue, e => e.Approval <= approvalMax.Value)
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive);
        }
    }
}