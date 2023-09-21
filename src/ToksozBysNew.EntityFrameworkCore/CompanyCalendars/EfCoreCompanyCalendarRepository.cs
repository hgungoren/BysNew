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

namespace ToksozBysNew.CompanyCalendars
{
    public class EfCoreCompanyCalendarRepository : EfCoreRepository<ToksozBysNewDbContext, CompanyCalendar, Guid>, ICompanyCalendarRepository
    {
        public EfCoreCompanyCalendarRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<CompanyCalendar>> GetListAsync(
            string filterText = null,
            DateTime? companyCalendarDateMin = null,
            DateTime? companyCalendarDateMax = null,
            bool? isWeekend = null,
            bool? isHoliday = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, companyCalendarDateMin, companyCalendarDateMax, isWeekend, isHoliday);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CompanyCalendarConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            DateTime? companyCalendarDateMin = null,
            DateTime? companyCalendarDateMax = null,
            bool? isWeekend = null,
            bool? isHoliday = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, companyCalendarDateMin, companyCalendarDateMax, isWeekend, isHoliday);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CompanyCalendar> ApplyFilter(
            IQueryable<CompanyCalendar> query,
            string filterText,
            DateTime? companyCalendarDateMin = null,
            DateTime? companyCalendarDateMax = null,
            bool? isWeekend = null,
            bool? isHoliday = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(companyCalendarDateMin.HasValue, e => e.CompanyCalendarDate >= companyCalendarDateMin.Value)
                    .WhereIf(companyCalendarDateMax.HasValue, e => e.CompanyCalendarDate <= companyCalendarDateMax.Value)
                    .WhereIf(isWeekend.HasValue, e => e.IsWeekend == isWeekend)
                    .WhereIf(isHoliday.HasValue, e => e.IsHoliday == isHoliday);
        }
    }
}