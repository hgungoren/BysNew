using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.TaxLists
{
    public interface ITaxListRepository : IRepository<TaxList, Guid>
    {
        Task<List<TaxList>> GetListAsync(
            string filterText = null,
            string taxName = null,
            int? taxValueMin = null,
            int? taxValueMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string taxName = null,
            int? taxValueMin = null,
            int? taxValueMax = null,
            CancellationToken cancellationToken = default);
    }
}