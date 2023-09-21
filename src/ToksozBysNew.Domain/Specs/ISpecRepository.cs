using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Specs
{
    public interface ISpecRepository : IRepository<Spec, Guid>
    {
        Task<List<Spec>> GetListAsync(
            string filterText = null,
            string specCode = null,
            string specName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string specCode = null,
            string specName = null,
            CancellationToken cancellationToken = default);
    }
}