using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Clinics
{
    public interface IClinicRepository : IRepository<Clinic, Guid>
    {
        Task<ClinicWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ClinicWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string clinicName = null,
            Guid? unitId = null,
            Guid? specId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Clinic>> GetListAsync(
                    string filterText = null,
                    string clinicName = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string clinicName = null,
            Guid? unitId = null,
            Guid? specId = null,
            CancellationToken cancellationToken = default);
    }
}