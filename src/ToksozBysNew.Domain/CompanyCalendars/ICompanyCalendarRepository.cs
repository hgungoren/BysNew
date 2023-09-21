using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.CompanyCalendars
{
    public interface ICompanyCalendarRepository : IRepository<CompanyCalendar, Guid>
    {
        Task<List<CompanyCalendar>> GetListAsync(
            string filterText = null,
            DateTime? companyCalendarDateMin = null,
            DateTime? companyCalendarDateMax = null,
            bool? isWeekend = null,
            bool? isHoliday = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            DateTime? companyCalendarDateMin = null,
            DateTime? companyCalendarDateMax = null,
            bool? isWeekend = null,
            bool? isHoliday = null,
            CancellationToken cancellationToken = default);
    }
}