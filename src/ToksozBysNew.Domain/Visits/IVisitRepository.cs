using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Visits
{
    public interface IVisitRepository : IRepository<Visit, Guid>
    {
        Task<VisitWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<VisitWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? visitDateMin = null,
            DateTime? visitDateMax = null,
            string visitNotes = null,
            Guid? doctorId = null,
            Guid? unitId = null,
            Guid? clinicId = null,
            Guid? brickId = null,
            Guid? identityUserId = null,
            Guid? specId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Visit>> GetListAsync(
                    string filterText = null,
                    DateTime? visitDateMin = null,
                    DateTime? visitDateMax = null,
                    string visitNotes = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            DateTime? visitDateMin = null,
            DateTime? visitDateMax = null,
            string visitNotes = null,
            Guid? doctorId = null,
            Guid? unitId = null,
            Guid? clinicId = null,
            Guid? brickId = null,
            Guid? identityUserId = null,
            Guid? specId = null,
            CancellationToken cancellationToken = default);
    }
}