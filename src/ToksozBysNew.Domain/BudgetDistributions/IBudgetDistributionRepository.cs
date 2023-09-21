using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.BudgetDistributions
{
    public interface IBudgetDistributionRepository : IRepository<BudgetDistribution, Guid>
    {
        Task<BudgetDistributionWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<BudgetDistributionWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<BudgetDistribution>> GetListAsync(
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
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}