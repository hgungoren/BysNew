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

namespace ToksozBysNew.VisitDailyActions
{
    public class EfCoreVisitDailyActionRepository : EfCoreRepository<ToksozBysNewDbContext, VisitDailyAction, Guid>, IVisitDailyActionRepository
    {
        public EfCoreVisitDailyActionRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<VisitDailyActionWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(visitDailyAction => new VisitDailyActionWithNavigationProperties
                {
                    VisitDailyAction = visitDailyAction,
                    IdentityUser = dbContext.Users.FirstOrDefault(c => c.Id == visitDailyAction.IdentityUserId)
                }).FirstOrDefault();
        }

        public async Task<List<VisitDailyActionWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? visitDailyDateMin = null,
            DateTime? visitDailyDateMax = null,
            decimal? visitDaily1Min = null,
            decimal? visitDaily1Max = null,
            decimal? visitDaily2Min = null,
            decimal? visitDaily2Max = null,
            decimal? visitDaily3Min = null,
            decimal? visitDaily3Max = null,
            decimal? visitDaily4Min = null,
            decimal? visitDaily4Max = null,
            decimal? visitDaily5Min = null,
            decimal? visitDaily5Max = null,
            decimal? visitDaily6Min = null,
            decimal? visitDaily6Max = null,
            decimal? visitDaily7Min = null,
            decimal? visitDaily7Max = null,
            decimal? visitDaily8Min = null,
            decimal? visitDaily8Max = null,
            decimal? visitDaily9Min = null,
            decimal? visitDaily9Max = null,
            decimal? visitDaily10Min = null,
            decimal? visitDaily10Max = null,
            decimal? visitDaily11Min = null,
            decimal? visitDaily11Max = null,
            decimal? visitDaily12Min = null,
            decimal? visitDaily12Max = null,
            decimal? visitDaily13Min = null,
            decimal? visitDaily13Max = null,
            decimal? visitDaily14Min = null,
            decimal? visitDaily14Max = null,
            decimal? visitDaily15Min = null,
            decimal? visitDaily15Max = null,
            DateTime? visitDailyCloseDateMin = null,
            DateTime? visitDailyCloseDateMax = null,
            string visitDailyNote = null,
            Guid? identityUserId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, visitDailyDateMin, visitDailyDateMax, visitDaily1Min, visitDaily1Max, visitDaily2Min, visitDaily2Max, visitDaily3Min, visitDaily3Max, visitDaily4Min, visitDaily4Max, visitDaily5Min, visitDaily5Max, visitDaily6Min, visitDaily6Max, visitDaily7Min, visitDaily7Max, visitDaily8Min, visitDaily8Max, visitDaily9Min, visitDaily9Max, visitDaily10Min, visitDaily10Max, visitDaily11Min, visitDaily11Max, visitDaily12Min, visitDaily12Max, visitDaily13Min, visitDaily13Max, visitDaily14Min, visitDaily14Max, visitDaily15Min, visitDaily15Max, visitDailyCloseDateMin, visitDailyCloseDateMax, visitDailyNote, identityUserId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VisitDailyActionConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<VisitDailyActionWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from visitDailyAction in (await GetDbSetAsync())
                   join identityUser in (await GetDbContextAsync()).Users on visitDailyAction.IdentityUserId equals identityUser.Id into users
                   from identityUser in users.DefaultIfEmpty()

                   select new VisitDailyActionWithNavigationProperties
                   {
                       VisitDailyAction = visitDailyAction,
                       IdentityUser = identityUser
                   };
        }

        protected virtual IQueryable<VisitDailyActionWithNavigationProperties> ApplyFilter(
            IQueryable<VisitDailyActionWithNavigationProperties> query,
            string filterText,
            DateTime? visitDailyDateMin = null,
            DateTime? visitDailyDateMax = null,
            decimal? visitDaily1Min = null,
            decimal? visitDaily1Max = null,
            decimal? visitDaily2Min = null,
            decimal? visitDaily2Max = null,
            decimal? visitDaily3Min = null,
            decimal? visitDaily3Max = null,
            decimal? visitDaily4Min = null,
            decimal? visitDaily4Max = null,
            decimal? visitDaily5Min = null,
            decimal? visitDaily5Max = null,
            decimal? visitDaily6Min = null,
            decimal? visitDaily6Max = null,
            decimal? visitDaily7Min = null,
            decimal? visitDaily7Max = null,
            decimal? visitDaily8Min = null,
            decimal? visitDaily8Max = null,
            decimal? visitDaily9Min = null,
            decimal? visitDaily9Max = null,
            decimal? visitDaily10Min = null,
            decimal? visitDaily10Max = null,
            decimal? visitDaily11Min = null,
            decimal? visitDaily11Max = null,
            decimal? visitDaily12Min = null,
            decimal? visitDaily12Max = null,
            decimal? visitDaily13Min = null,
            decimal? visitDaily13Max = null,
            decimal? visitDaily14Min = null,
            decimal? visitDaily14Max = null,
            decimal? visitDaily15Min = null,
            decimal? visitDaily15Max = null,
            DateTime? visitDailyCloseDateMin = null,
            DateTime? visitDailyCloseDateMax = null,
            string visitDailyNote = null,
            Guid? identityUserId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.VisitDailyAction.VisitDailyNote.Contains(filterText))
                    .WhereIf(visitDailyDateMin.HasValue, e => e.VisitDailyAction.VisitDailyDate >= visitDailyDateMin.Value)
                    .WhereIf(visitDailyDateMax.HasValue, e => e.VisitDailyAction.VisitDailyDate <= visitDailyDateMax.Value)
                    .WhereIf(visitDaily1Min.HasValue, e => e.VisitDailyAction.VisitDaily1 >= visitDaily1Min.Value)
                    .WhereIf(visitDaily1Max.HasValue, e => e.VisitDailyAction.VisitDaily1 <= visitDaily1Max.Value)
                    .WhereIf(visitDaily2Min.HasValue, e => e.VisitDailyAction.VisitDaily2 >= visitDaily2Min.Value)
                    .WhereIf(visitDaily2Max.HasValue, e => e.VisitDailyAction.VisitDaily2 <= visitDaily2Max.Value)
                    .WhereIf(visitDaily3Min.HasValue, e => e.VisitDailyAction.VisitDaily3 >= visitDaily3Min.Value)
                    .WhereIf(visitDaily3Max.HasValue, e => e.VisitDailyAction.VisitDaily3 <= visitDaily3Max.Value)
                    .WhereIf(visitDaily4Min.HasValue, e => e.VisitDailyAction.VisitDaily4 >= visitDaily4Min.Value)
                    .WhereIf(visitDaily4Max.HasValue, e => e.VisitDailyAction.VisitDaily4 <= visitDaily4Max.Value)
                    .WhereIf(visitDaily5Min.HasValue, e => e.VisitDailyAction.VisitDaily5 >= visitDaily5Min.Value)
                    .WhereIf(visitDaily5Max.HasValue, e => e.VisitDailyAction.VisitDaily5 <= visitDaily5Max.Value)
                    .WhereIf(visitDaily6Min.HasValue, e => e.VisitDailyAction.VisitDaily6 >= visitDaily6Min.Value)
                    .WhereIf(visitDaily6Max.HasValue, e => e.VisitDailyAction.VisitDaily6 <= visitDaily6Max.Value)
                    .WhereIf(visitDaily7Min.HasValue, e => e.VisitDailyAction.VisitDaily7 >= visitDaily7Min.Value)
                    .WhereIf(visitDaily7Max.HasValue, e => e.VisitDailyAction.VisitDaily7 <= visitDaily7Max.Value)
                    .WhereIf(visitDaily8Min.HasValue, e => e.VisitDailyAction.VisitDaily8 >= visitDaily8Min.Value)
                    .WhereIf(visitDaily8Max.HasValue, e => e.VisitDailyAction.VisitDaily8 <= visitDaily8Max.Value)
                    .WhereIf(visitDaily9Min.HasValue, e => e.VisitDailyAction.VisitDaily9 >= visitDaily9Min.Value)
                    .WhereIf(visitDaily9Max.HasValue, e => e.VisitDailyAction.VisitDaily9 <= visitDaily9Max.Value)
                    .WhereIf(visitDaily10Min.HasValue, e => e.VisitDailyAction.VisitDaily10 >= visitDaily10Min.Value)
                    .WhereIf(visitDaily10Max.HasValue, e => e.VisitDailyAction.VisitDaily10 <= visitDaily10Max.Value)
                    .WhereIf(visitDaily11Min.HasValue, e => e.VisitDailyAction.VisitDaily11 >= visitDaily11Min.Value)
                    .WhereIf(visitDaily11Max.HasValue, e => e.VisitDailyAction.VisitDaily11 <= visitDaily11Max.Value)
                    .WhereIf(visitDaily12Min.HasValue, e => e.VisitDailyAction.VisitDaily12 >= visitDaily12Min.Value)
                    .WhereIf(visitDaily12Max.HasValue, e => e.VisitDailyAction.VisitDaily12 <= visitDaily12Max.Value)
                    .WhereIf(visitDaily13Min.HasValue, e => e.VisitDailyAction.VisitDaily13 >= visitDaily13Min.Value)
                    .WhereIf(visitDaily13Max.HasValue, e => e.VisitDailyAction.VisitDaily13 <= visitDaily13Max.Value)
                    .WhereIf(visitDaily14Min.HasValue, e => e.VisitDailyAction.VisitDaily14 >= visitDaily14Min.Value)
                    .WhereIf(visitDaily14Max.HasValue, e => e.VisitDailyAction.VisitDaily14 <= visitDaily14Max.Value)
                    .WhereIf(visitDaily15Min.HasValue, e => e.VisitDailyAction.VisitDaily15 >= visitDaily15Min.Value)
                    .WhereIf(visitDaily15Max.HasValue, e => e.VisitDailyAction.VisitDaily15 <= visitDaily15Max.Value)
                    .WhereIf(visitDailyCloseDateMin.HasValue, e => e.VisitDailyAction.VisitDailyCloseDate >= visitDailyCloseDateMin.Value)
                    .WhereIf(visitDailyCloseDateMax.HasValue, e => e.VisitDailyAction.VisitDailyCloseDate <= visitDailyCloseDateMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(visitDailyNote), e => e.VisitDailyAction.VisitDailyNote.Contains(visitDailyNote))
                    .WhereIf(identityUserId != null && identityUserId != Guid.Empty, e => e.IdentityUser != null && e.IdentityUser.Id == identityUserId);
        }

        public async Task<List<VisitDailyAction>> GetListAsync(
            string filterText = null,
            DateTime? visitDailyDateMin = null,
            DateTime? visitDailyDateMax = null,
            decimal? visitDaily1Min = null,
            decimal? visitDaily1Max = null,
            decimal? visitDaily2Min = null,
            decimal? visitDaily2Max = null,
            decimal? visitDaily3Min = null,
            decimal? visitDaily3Max = null,
            decimal? visitDaily4Min = null,
            decimal? visitDaily4Max = null,
            decimal? visitDaily5Min = null,
            decimal? visitDaily5Max = null,
            decimal? visitDaily6Min = null,
            decimal? visitDaily6Max = null,
            decimal? visitDaily7Min = null,
            decimal? visitDaily7Max = null,
            decimal? visitDaily8Min = null,
            decimal? visitDaily8Max = null,
            decimal? visitDaily9Min = null,
            decimal? visitDaily9Max = null,
            decimal? visitDaily10Min = null,
            decimal? visitDaily10Max = null,
            decimal? visitDaily11Min = null,
            decimal? visitDaily11Max = null,
            decimal? visitDaily12Min = null,
            decimal? visitDaily12Max = null,
            decimal? visitDaily13Min = null,
            decimal? visitDaily13Max = null,
            decimal? visitDaily14Min = null,
            decimal? visitDaily14Max = null,
            decimal? visitDaily15Min = null,
            decimal? visitDaily15Max = null,
            DateTime? visitDailyCloseDateMin = null,
            DateTime? visitDailyCloseDateMax = null,
            string visitDailyNote = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, visitDailyDateMin, visitDailyDateMax, visitDaily1Min, visitDaily1Max, visitDaily2Min, visitDaily2Max, visitDaily3Min, visitDaily3Max, visitDaily4Min, visitDaily4Max, visitDaily5Min, visitDaily5Max, visitDaily6Min, visitDaily6Max, visitDaily7Min, visitDaily7Max, visitDaily8Min, visitDaily8Max, visitDaily9Min, visitDaily9Max, visitDaily10Min, visitDaily10Max, visitDaily11Min, visitDaily11Max, visitDaily12Min, visitDaily12Max, visitDaily13Min, visitDaily13Max, visitDaily14Min, visitDaily14Max, visitDaily15Min, visitDaily15Max, visitDailyCloseDateMin, visitDailyCloseDateMax, visitDailyNote);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VisitDailyActionConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            DateTime? visitDailyDateMin = null,
            DateTime? visitDailyDateMax = null,
            decimal? visitDaily1Min = null,
            decimal? visitDaily1Max = null,
            decimal? visitDaily2Min = null,
            decimal? visitDaily2Max = null,
            decimal? visitDaily3Min = null,
            decimal? visitDaily3Max = null,
            decimal? visitDaily4Min = null,
            decimal? visitDaily4Max = null,
            decimal? visitDaily5Min = null,
            decimal? visitDaily5Max = null,
            decimal? visitDaily6Min = null,
            decimal? visitDaily6Max = null,
            decimal? visitDaily7Min = null,
            decimal? visitDaily7Max = null,
            decimal? visitDaily8Min = null,
            decimal? visitDaily8Max = null,
            decimal? visitDaily9Min = null,
            decimal? visitDaily9Max = null,
            decimal? visitDaily10Min = null,
            decimal? visitDaily10Max = null,
            decimal? visitDaily11Min = null,
            decimal? visitDaily11Max = null,
            decimal? visitDaily12Min = null,
            decimal? visitDaily12Max = null,
            decimal? visitDaily13Min = null,
            decimal? visitDaily13Max = null,
            decimal? visitDaily14Min = null,
            decimal? visitDaily14Max = null,
            decimal? visitDaily15Min = null,
            decimal? visitDaily15Max = null,
            DateTime? visitDailyCloseDateMin = null,
            DateTime? visitDailyCloseDateMax = null,
            string visitDailyNote = null,
            Guid? identityUserId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, visitDailyDateMin, visitDailyDateMax, visitDaily1Min, visitDaily1Max, visitDaily2Min, visitDaily2Max, visitDaily3Min, visitDaily3Max, visitDaily4Min, visitDaily4Max, visitDaily5Min, visitDaily5Max, visitDaily6Min, visitDaily6Max, visitDaily7Min, visitDaily7Max, visitDaily8Min, visitDaily8Max, visitDaily9Min, visitDaily9Max, visitDaily10Min, visitDaily10Max, visitDaily11Min, visitDaily11Max, visitDaily12Min, visitDaily12Max, visitDaily13Min, visitDaily13Max, visitDaily14Min, visitDaily14Max, visitDaily15Min, visitDaily15Max, visitDailyCloseDateMin, visitDailyCloseDateMax, visitDailyNote, identityUserId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<VisitDailyAction> ApplyFilter(
            IQueryable<VisitDailyAction> query,
            string filterText,
            DateTime? visitDailyDateMin = null,
            DateTime? visitDailyDateMax = null,
            decimal? visitDaily1Min = null,
            decimal? visitDaily1Max = null,
            decimal? visitDaily2Min = null,
            decimal? visitDaily2Max = null,
            decimal? visitDaily3Min = null,
            decimal? visitDaily3Max = null,
            decimal? visitDaily4Min = null,
            decimal? visitDaily4Max = null,
            decimal? visitDaily5Min = null,
            decimal? visitDaily5Max = null,
            decimal? visitDaily6Min = null,
            decimal? visitDaily6Max = null,
            decimal? visitDaily7Min = null,
            decimal? visitDaily7Max = null,
            decimal? visitDaily8Min = null,
            decimal? visitDaily8Max = null,
            decimal? visitDaily9Min = null,
            decimal? visitDaily9Max = null,
            decimal? visitDaily10Min = null,
            decimal? visitDaily10Max = null,
            decimal? visitDaily11Min = null,
            decimal? visitDaily11Max = null,
            decimal? visitDaily12Min = null,
            decimal? visitDaily12Max = null,
            decimal? visitDaily13Min = null,
            decimal? visitDaily13Max = null,
            decimal? visitDaily14Min = null,
            decimal? visitDaily14Max = null,
            decimal? visitDaily15Min = null,
            decimal? visitDaily15Max = null,
            DateTime? visitDailyCloseDateMin = null,
            DateTime? visitDailyCloseDateMax = null,
            string visitDailyNote = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.VisitDailyNote.Contains(filterText))
                    .WhereIf(visitDailyDateMin.HasValue, e => e.VisitDailyDate >= visitDailyDateMin.Value)
                    .WhereIf(visitDailyDateMax.HasValue, e => e.VisitDailyDate <= visitDailyDateMax.Value)
                    .WhereIf(visitDaily1Min.HasValue, e => e.VisitDaily1 >= visitDaily1Min.Value)
                    .WhereIf(visitDaily1Max.HasValue, e => e.VisitDaily1 <= visitDaily1Max.Value)
                    .WhereIf(visitDaily2Min.HasValue, e => e.VisitDaily2 >= visitDaily2Min.Value)
                    .WhereIf(visitDaily2Max.HasValue, e => e.VisitDaily2 <= visitDaily2Max.Value)
                    .WhereIf(visitDaily3Min.HasValue, e => e.VisitDaily3 >= visitDaily3Min.Value)
                    .WhereIf(visitDaily3Max.HasValue, e => e.VisitDaily3 <= visitDaily3Max.Value)
                    .WhereIf(visitDaily4Min.HasValue, e => e.VisitDaily4 >= visitDaily4Min.Value)
                    .WhereIf(visitDaily4Max.HasValue, e => e.VisitDaily4 <= visitDaily4Max.Value)
                    .WhereIf(visitDaily5Min.HasValue, e => e.VisitDaily5 >= visitDaily5Min.Value)
                    .WhereIf(visitDaily5Max.HasValue, e => e.VisitDaily5 <= visitDaily5Max.Value)
                    .WhereIf(visitDaily6Min.HasValue, e => e.VisitDaily6 >= visitDaily6Min.Value)
                    .WhereIf(visitDaily6Max.HasValue, e => e.VisitDaily6 <= visitDaily6Max.Value)
                    .WhereIf(visitDaily7Min.HasValue, e => e.VisitDaily7 >= visitDaily7Min.Value)
                    .WhereIf(visitDaily7Max.HasValue, e => e.VisitDaily7 <= visitDaily7Max.Value)
                    .WhereIf(visitDaily8Min.HasValue, e => e.VisitDaily8 >= visitDaily8Min.Value)
                    .WhereIf(visitDaily8Max.HasValue, e => e.VisitDaily8 <= visitDaily8Max.Value)
                    .WhereIf(visitDaily9Min.HasValue, e => e.VisitDaily9 >= visitDaily9Min.Value)
                    .WhereIf(visitDaily9Max.HasValue, e => e.VisitDaily9 <= visitDaily9Max.Value)
                    .WhereIf(visitDaily10Min.HasValue, e => e.VisitDaily10 >= visitDaily10Min.Value)
                    .WhereIf(visitDaily10Max.HasValue, e => e.VisitDaily10 <= visitDaily10Max.Value)
                    .WhereIf(visitDaily11Min.HasValue, e => e.VisitDaily11 >= visitDaily11Min.Value)
                    .WhereIf(visitDaily11Max.HasValue, e => e.VisitDaily11 <= visitDaily11Max.Value)
                    .WhereIf(visitDaily12Min.HasValue, e => e.VisitDaily12 >= visitDaily12Min.Value)
                    .WhereIf(visitDaily12Max.HasValue, e => e.VisitDaily12 <= visitDaily12Max.Value)
                    .WhereIf(visitDaily13Min.HasValue, e => e.VisitDaily13 >= visitDaily13Min.Value)
                    .WhereIf(visitDaily13Max.HasValue, e => e.VisitDaily13 <= visitDaily13Max.Value)
                    .WhereIf(visitDaily14Min.HasValue, e => e.VisitDaily14 >= visitDaily14Min.Value)
                    .WhereIf(visitDaily14Max.HasValue, e => e.VisitDaily14 <= visitDaily14Max.Value)
                    .WhereIf(visitDaily15Min.HasValue, e => e.VisitDaily15 >= visitDaily15Min.Value)
                    .WhereIf(visitDaily15Max.HasValue, e => e.VisitDaily15 <= visitDaily15Max.Value)
                    .WhereIf(visitDailyCloseDateMin.HasValue, e => e.VisitDailyCloseDate >= visitDailyCloseDateMin.Value)
                    .WhereIf(visitDailyCloseDateMax.HasValue, e => e.VisitDailyCloseDate <= visitDailyCloseDateMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(visitDailyNote), e => e.VisitDailyNote.Contains(visitDailyNote));
        }
    }
}