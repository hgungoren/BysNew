using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Positions
{
    public interface IPositionRepository : IRepository<Position, Guid>
    {
        Task<List<Position>> GetListAsync(
            string filterText = null,
            string positionCode = null,
            string positionName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string positionCode = null,
            string positionName = null,
            CancellationToken cancellationToken = default);
    }
}