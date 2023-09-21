using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.CompanyCalendars
{
    public class CompanyCalendarManager : DomainService
    {
        private readonly ICompanyCalendarRepository _companyCalendarRepository;

        public CompanyCalendarManager(ICompanyCalendarRepository companyCalendarRepository)
        {
            _companyCalendarRepository = companyCalendarRepository;
        }

        public async Task<CompanyCalendar> CreateAsync(
        DateTime companyCalendarDate, bool isWeekend, bool isHoliday)
        {
            Check.NotNull(companyCalendarDate, nameof(companyCalendarDate));

            var companyCalendar = new CompanyCalendar(
             GuidGenerator.Create(),
             companyCalendarDate, isWeekend, isHoliday
             );

            return await _companyCalendarRepository.InsertAsync(companyCalendar);
        }

        public async Task<CompanyCalendar> UpdateAsync(
            Guid id,
            DateTime companyCalendarDate, bool isWeekend, bool isHoliday, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(companyCalendarDate, nameof(companyCalendarDate));

            var companyCalendar = await _companyCalendarRepository.GetAsync(id);

            companyCalendar.CompanyCalendarDate = companyCalendarDate;
            companyCalendar.IsWeekend = isWeekend;
            companyCalendar.IsHoliday = isHoliday;

            companyCalendar.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _companyCalendarRepository.UpdateAsync(companyCalendar);
        }

    }
}