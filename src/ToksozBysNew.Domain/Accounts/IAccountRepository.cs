using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Accounts
{
    public interface IAccountRepository : IRepository<Account, Guid>
    {
        Task<List<Account>> GetListAsync(
            string filterText = null,
            string accountCode = null,
            string accountName = null,
            string description = null,
            bool? isActive = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string accountCode = null,
            string accountName = null,
            string description = null,
            bool? isActive = null,
            CancellationToken cancellationToken = default);
    }
}