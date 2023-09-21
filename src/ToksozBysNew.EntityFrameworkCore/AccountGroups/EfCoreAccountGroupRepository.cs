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

namespace ToksozBysNew.AccountGroups
{
    public class EfCoreAccountGroupRepository : EfCoreRepository<ToksozBysNewDbContext, AccountGroup, Guid>, IAccountGroupRepository
    {
        public EfCoreAccountGroupRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<AccountGroup>> GetListAsync(
            string filterText = null,
            string accountGroupName = null,
            bool? isUnitEnterable = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, accountGroupName, isUnitEnterable);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AccountGroupConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string accountGroupName = null,
            bool? isUnitEnterable = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, accountGroupName, isUnitEnterable);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<AccountGroup> ApplyFilter(
            IQueryable<AccountGroup> query,
            string filterText,
            string accountGroupName = null,
            bool? isUnitEnterable = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AccountGroupName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(accountGroupName), e => e.AccountGroupName.Contains(accountGroupName))
                    .WhereIf(isUnitEnterable.HasValue, e => e.IsUnitEnterable == isUnitEnterable);
        }
    }
}