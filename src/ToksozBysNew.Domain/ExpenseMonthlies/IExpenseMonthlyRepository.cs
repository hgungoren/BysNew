using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.ExpenseMonthlies
{
    public interface IExpenseMonthlyRepository : IRepository<ExpenseMonthly, Guid>
    {
        Task<List<ExpenseMonthly>> GetListAsync(
            string filterText = null,
            string accountId = null,
            string accountGroup = null,
            string account = null,
            string department = null,
            string expenseType = null,
            string product = null,
            string proje = null,
            string comment = null,
            string month = null,
            int? yearMin = null,
            int? yearMax = null,
            int? unitMin = null,
            int? unitMax = null,
            float? unitValueMin = null,
            float? unitValueMax = null,
            float? amountMin = null,
            float? amountMax = null,
            float? memoMin = null,
            float? memoMax = null,
            string invoice = null,
            float? remainMin = null,
            float? remainMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string accountId = null,
            string accountGroup = null,
            string account = null,
            string department = null,
            string expenseType = null,
            string product = null,
            string proje = null,
            string comment = null,
            string month = null,
            int? yearMin = null,
            int? yearMax = null,
            int? unitMin = null,
            int? unitMax = null,
            float? unitValueMin = null,
            float? unitValueMax = null,
            float? amountMin = null,
            float? amountMax = null,
            float? memoMin = null,
            float? memoMax = null,
            string invoice = null,
            float? remainMin = null,
            float? remainMax = null,
            CancellationToken cancellationToken = default);

        Task<List<ExpenseMonthly>> GetExpensesMonthly();
    }
}