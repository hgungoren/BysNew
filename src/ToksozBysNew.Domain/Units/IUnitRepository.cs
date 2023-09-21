using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Units
{
    public interface IUnitRepository : IRepository<Unit, Guid>
    {
        Task<UnitWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<UnitWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string unitName = null,
            Guid? brickId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Unit>> GetListAsync(
                    string filterText = null,
                    string unitName = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string unitName = null,
            Guid? brickId = null,
            CancellationToken cancellationToken = default);
    }
}