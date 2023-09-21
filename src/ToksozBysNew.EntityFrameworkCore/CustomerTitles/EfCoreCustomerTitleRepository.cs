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

namespace ToksozBysNew.CustomerTitles
{
    public class EfCoreCustomerTitleRepository : EfCoreRepository<ToksozBysNewDbContext, CustomerTitle, Guid>, ICustomerTitleRepository
    {
        public EfCoreCustomerTitleRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<CustomerTitle>> GetListAsync(
            string filterText = null,
            string titleName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, titleName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerTitleConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string titleName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, titleName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerTitle> ApplyFilter(
            IQueryable<CustomerTitle> query,
            string filterText,
            string titleName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.TitleName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(titleName), e => e.TitleName.Contains(titleName));
        }
    }
}