using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using ToksozBysNew.EntityFrameworkCore;

namespace ToksozBysNew.Countries
{
    public class EfCoreCountryRepository : EfCoreRepository<ToksozBysNewDbContext, Country, Guid>, ICountryRepository
    {
        public EfCoreCountryRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Country>> GetListAsync(
            string filterText = null,
            string countryName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, countryName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CountryConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string countryName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, countryName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Country> ApplyFilter(
            IQueryable<Country> query,
            string filterText,
            string countryName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CountryName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(countryName), e => e.CountryName.Contains(countryName));
        }
    }
}