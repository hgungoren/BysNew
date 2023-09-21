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

namespace ToksozBysNew.Positions
{
    public class EfCorePositionRepository : EfCoreRepository<ToksozBysNewDbContext, Position, Guid>, IPositionRepository
    {
        public EfCorePositionRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Position>> GetListAsync(
            string filterText = null,
            string positionCode = null,
            string positionName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, positionCode, positionName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PositionConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string positionCode = null,
            string positionName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, positionCode, positionName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Position> ApplyFilter(
            IQueryable<Position> query,
            string filterText,
            string positionCode = null,
            string positionName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.PositionCode.Contains(filterText) || e.PositionName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(positionCode), e => e.PositionCode.Contains(positionCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(positionName), e => e.PositionName.Contains(positionName));
        }
    }
}