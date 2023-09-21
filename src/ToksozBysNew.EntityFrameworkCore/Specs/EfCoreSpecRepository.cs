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

namespace ToksozBysNew.Specs
{
    public class EfCoreSpecRepository : EfCoreRepository<ToksozBysNewDbContext, Spec, Guid>, ISpecRepository
    {
        public EfCoreSpecRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Spec>> GetListAsync(
            string filterText = null,
            string specCode = null,
            string specName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, specCode, specName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SpecConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string specCode = null,
            string specName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, specCode, specName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Spec> ApplyFilter(
            IQueryable<Spec> query,
            string filterText,
            string specCode = null,
            string specName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.SpecCode.Contains(filterText) || e.SpecName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(specCode), e => e.SpecCode.Contains(specCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(specName), e => e.SpecName.Contains(specName));
        }
    }
}