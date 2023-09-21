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

namespace ToksozBysNew.CustomerTypes
{
    public class EfCoreCustomerTypeRepository : EfCoreRepository<ToksozBysNewDbContext, CustomerType, Guid>, ICustomerTypeRepository
    {
        public EfCoreCustomerTypeRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<CustomerType>> GetListAsync(
            string filterText = null,
            string typeName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, typeName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerTypeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string typeName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, typeName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerType> ApplyFilter(
            IQueryable<CustomerType> query,
            string filterText,
            string typeName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.TypeName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(typeName), e => e.TypeName.Contains(typeName));
        }
    }
}