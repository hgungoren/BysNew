using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Provinces
{
    public interface IProvinceRepository : IRepository<Province, Guid>
    {
        Task<ProvinceWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ProvinceWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string provinceName = null,
            Guid? countryId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Province>> GetListAsync(
                    string filterText = null,
                    string provinceName = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string provinceName = null,
            Guid? countryId = null,
            CancellationToken cancellationToken = default);
    }
}