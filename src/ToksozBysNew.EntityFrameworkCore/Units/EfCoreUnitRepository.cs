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

namespace ToksozBysNew.Units
{
    public class EfCoreUnitRepository : EfCoreRepository<ToksozBysNewDbContext, Unit, Guid>, IUnitRepository
    {
        public EfCoreUnitRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<UnitWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(unit => new UnitWithNavigationProperties
                {
                    Unit = unit,
                    Brick = dbContext.Bricks.FirstOrDefault(c => c.Id == unit.BrickId)
                }).FirstOrDefault();
        }

        public async Task<List<UnitWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string unitName = null,
            Guid? brickId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, unitName, brickId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? UnitConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<UnitWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from unit in (await GetDbSetAsync())
                   join brick in (await GetDbContextAsync()).Bricks on unit.BrickId equals brick.Id into bricks
                   from brick in bricks.DefaultIfEmpty()

                   select new UnitWithNavigationProperties
                   {
                       Unit = unit,
                       Brick = brick
                   };
        }

        protected virtual IQueryable<UnitWithNavigationProperties> ApplyFilter(
            IQueryable<UnitWithNavigationProperties> query,
            string filterText,
            string unitName = null,
            Guid? brickId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Unit.UnitName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(unitName), e => e.Unit.UnitName.Contains(unitName))
                    .WhereIf(brickId != null && brickId != Guid.Empty, e => e.Brick != null && e.Brick.Id == brickId);
        }

        public async Task<List<Unit>> GetListAsync(
            string filterText = null,
            string unitName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, unitName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? UnitConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string unitName = null,
            Guid? brickId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, unitName, brickId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Unit> ApplyFilter(
            IQueryable<Unit> query,
            string filterText,
            string unitName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.UnitName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(unitName), e => e.UnitName.Contains(unitName));
        }
    }
}