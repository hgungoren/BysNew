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

namespace ToksozBysNew.TaxLists
{
    public class EfCoreTaxListRepository : EfCoreRepository<ToksozBysNewDbContext, TaxList, Guid>, ITaxListRepository
    {
        public EfCoreTaxListRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<TaxList>> GetListAsync(
            string filterText = null,
            string taxName = null,
            int? taxValueMin = null,
            int? taxValueMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, taxName, taxValueMin, taxValueMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TaxListConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string taxName = null,
            int? taxValueMin = null,
            int? taxValueMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, taxName, taxValueMin, taxValueMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<TaxList> ApplyFilter(
            IQueryable<TaxList> query,
            string filterText,
            string taxName = null,
            int? taxValueMin = null,
            int? taxValueMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.TaxName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(taxName), e => e.TaxName.Contains(taxName))
                    .WhereIf(taxValueMin.HasValue, e => e.TaxValue >= taxValueMin.Value)
                    .WhereIf(taxValueMax.HasValue, e => e.TaxValue <= taxValueMax.Value);
        }
    }
}