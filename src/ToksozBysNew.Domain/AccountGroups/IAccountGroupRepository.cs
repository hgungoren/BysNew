using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.AccountGroups
{
    public interface IAccountGroupRepository : IRepository<AccountGroup, Guid>
    {
        Task<List<AccountGroup>> GetListAsync(
            string filterText = null,
            string accountGroupName = null,
            bool? isUnitEnterable = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string accountGroupName = null,
            bool? isUnitEnterable = null,
            CancellationToken cancellationToken = default);
    }
}