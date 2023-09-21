using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Doctors
{
    public interface IDoctorRepository : IRepository<Doctor, Guid>
    {
        Task<DoctorWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<DoctorWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            bool? isActive = null,
            string nameSurname = null,
            string pharmacyName = null,
            Guid? positionId = null,
            Guid? specId = null,
            Guid? customerTitleId = null,
            Guid? unitId = null,
            Guid? customerTypeId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Doctor>> GetListAsync(
                    string filterText = null,
                    bool? isActive = null,
                    string nameSurname = null,
                    string pharmacyName = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            bool? isActive = null,
            string nameSurname = null,
            string pharmacyName = null,
            Guid? positionId = null,
            Guid? specId = null,
            Guid? customerTitleId = null,
            Guid? unitId = null,
            Guid? customerTypeId = null,
            CancellationToken cancellationToken = default);
    }
}