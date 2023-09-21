using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.BudgetDistributions
{
    public class BudgetDistributionManager : DomainService
    {
        private readonly IBudgetDistributionRepository _budgetDistributionRepository;

        public BudgetDistributionManager(IBudgetDistributionRepository budgetDistributionRepository)
        {
            _budgetDistributionRepository = budgetDistributionRepository;
        }

        public async Task<BudgetDistribution> CreateAsync(
        Guid departmentId, Guid? productId, Guid budgetId, Guid? accountGroupId, Guid? accountId, Guid? identityUserId, string costCenter, string expenseType, int month, float amount, string comment, string status, bool isActive, int? projectItem = null, int? type = null, int? unit = null, float? unitValue = null, int? year = null, float? ratio = null, float? memo = null, float? invoice = null, int? currency = null, float? currencyAmount = null, int? expenseCategory = null, int? expenseNecessity = null, int? approval = null)
        {
            var budgetDistribution = new BudgetDistribution(
             GuidGenerator.Create(),
             departmentId, productId, budgetId, accountGroupId, accountId, identityUserId, costCenter, expenseType, month, amount, comment, status, isActive, projectItem, type, unit, unitValue, year, ratio, memo, invoice, currency, currencyAmount, expenseCategory, expenseNecessity, approval
             );

            return await _budgetDistributionRepository.InsertAsync(budgetDistribution);
        }

        public async Task<BudgetDistribution> UpdateAsync(
            Guid id,
            Guid departmentId, Guid? productId, Guid budgetId, Guid? accountGroupId, Guid? accountId, Guid? identityUserId, string costCenter, string expenseType, int month, float amount, string comment, string status, bool isActive, int? projectItem = null, int? type = null, int? unit = null, float? unitValue = null, int? year = null, float? ratio = null, float? memo = null, float? invoice = null, int? currency = null, float? currencyAmount = null, int? expenseCategory = null, int? expenseNecessity = null, int? approval = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _budgetDistributionRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var budgetDistribution = await AsyncExecuter.FirstOrDefaultAsync(query);

            budgetDistribution.DepartmentId = departmentId;
            budgetDistribution.ProductId = productId;
            budgetDistribution.BudgetId = budgetId;
            budgetDistribution.AccountGroupId = accountGroupId;
            budgetDistribution.AccountId = accountId;
            budgetDistribution.IdentityUserId = identityUserId;
            budgetDistribution.CostCenter = costCenter;
            budgetDistribution.ExpenseType = expenseType;
            budgetDistribution.Month = month;
            budgetDistribution.Amount = amount;
            budgetDistribution.Comment = comment;
            budgetDistribution.Status = status;
            budgetDistribution.IsActive = isActive;
            budgetDistribution.ProjectItem = projectItem;
            budgetDistribution.Type = type;
            budgetDistribution.Unit = unit;
            budgetDistribution.UnitValue = unitValue;
            budgetDistribution.Year = year;
            budgetDistribution.Ratio = ratio;
            budgetDistribution.Memo = memo;
            budgetDistribution.Invoice = invoice;
            budgetDistribution.Currency = currency;
            budgetDistribution.CurrencyAmount = currencyAmount;
            budgetDistribution.ExpenseCategory = expenseCategory;
            budgetDistribution.ExpenseNecessity = expenseNecessity;
            budgetDistribution.Approval = approval;

            budgetDistribution.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _budgetDistributionRepository.UpdateAsync(budgetDistribution);
        }

    }
}