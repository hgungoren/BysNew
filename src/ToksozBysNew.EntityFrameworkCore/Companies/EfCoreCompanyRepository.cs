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

namespace ToksozBysNew.Companies
{
    public class EfCoreCompanyRepository : EfCoreRepository<ToksozBysNewDbContext, Company, Guid>, ICompanyRepository
    {
        public EfCoreCompanyRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Company>> GetListAsync(
            string filterText = null,
            string companyName = null,
            bool? isActive = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, companyName, isActive);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CompanyConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string companyName = null,
            bool? isActive = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, companyName, isActive);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Company> ApplyFilter(
            IQueryable<Company> query,
            string filterText,
            string companyName = null,
            bool? isActive = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CompanyName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(companyName), e => e.CompanyName.Contains(companyName))
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive);
        }
    }
}