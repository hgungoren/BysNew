using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Countries
{
    public interface ICountryRepository : IRepository<Country, Guid>
    {
        Task<List<Country>> GetListAsync(
            string filterText = null,
            string countryName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string countryName = null,
            CancellationToken cancellationToken = default);
    }
}