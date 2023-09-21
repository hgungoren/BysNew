using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Districts
{
    public interface IDistrictRepository : IRepository<District, Guid>
    {
        Task<DistrictWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<DistrictWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string districtName = null,
            Guid? countryId = null,
            Guid? provinceId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<District>> GetListAsync(
                    string filterText = null,
                    string districtName = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string districtName = null,
            Guid? countryId = null,
            Guid? provinceId = null,
            CancellationToken cancellationToken = default);
    }
}