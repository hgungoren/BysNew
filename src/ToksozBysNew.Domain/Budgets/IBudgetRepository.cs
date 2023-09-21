using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Budgets
{
    public interface IBudgetRepository : IRepository<Budget, Guid>
    {
        Task<BudgetWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<BudgetWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<Budget>> GetListAsync(
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
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string budgetName = null,
            int? yearMin = null,
            int? yearMax = null,
            string comment = null,
            bool? isActive = null,
            DateTime? openUntilMin = null,
            DateTime? openUntilMax = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default);
    }
}