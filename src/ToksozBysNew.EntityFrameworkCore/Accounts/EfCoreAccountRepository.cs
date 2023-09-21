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

namespace ToksozBysNew.Accounts
{
    public class EfCoreAccountRepository : EfCoreRepository<ToksozBysNewDbContext, Account, Guid>, IAccountRepository
    {
        public EfCoreAccountRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Account>> GetListAsync(
            string filterText = null,
            string accountCode = null,
            string accountName = null,
            string description = null,
            bool? isActive = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, accountCode, accountName, description, isActive);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AccountConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string accountCode = null,
            string accountName = null,
            string description = null,
            bool? isActive = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, accountCode, accountName, description, isActive);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Account> ApplyFilter(
            IQueryable<Account> query,
            string filterText,
            string accountCode = null,
            string accountName = null,
            string description = null,
            bool? isActive = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AccountCode.Contains(filterText) || e.AccountName.Contains(filterText) || e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(accountCode), e => e.AccountCode.Contains(accountCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(accountName), e => e.AccountName.Contains(accountName))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive);
        }
    }
}