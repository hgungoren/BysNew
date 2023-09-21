using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Bricks
{
    public interface IBrickRepository : IRepository<Brick, Guid>
    {
        Task<List<Brick>> GetListAsync(
            string filterText = null,
            string brickName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string brickName = null,
            CancellationToken cancellationToken = default);
    }
}