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

namespace ToksozBysNew.Bricks
{
    public class EfCoreBrickRepository : EfCoreRepository<ToksozBysNewDbContext, Brick, Guid>, IBrickRepository
    {
        public EfCoreBrickRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Brick>> GetListAsync(
            string filterText = null,
            string brickName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, brickName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BrickConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string brickName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, brickName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Brick> ApplyFilter(
            IQueryable<Brick> query,
            string filterText,
            string brickName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.BrickName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(brickName), e => e.BrickName.Contains(brickName));
        }
    }
}