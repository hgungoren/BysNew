using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.CompanyCalendars;

namespace ToksozBysNew.CompanyCalendars
{
    public class CompanyCalendarsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICompanyCalendarRepository _companyCalendarRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CompanyCalendarsDataSeedContributor(ICompanyCalendarRepository companyCalendarRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _companyCalendarRepository = companyCalendarRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _companyCalendarRepository.InsertAsync(new CompanyCalendar
            (
                id: Guid.Parse("080ed2c9-83d0-44fd-b024-591ffb3d1f4c"),
                companyCalendarDate: new DateTime(2012, 7, 23),
                isWeekend: true,
                isHoliday: true
            ));

            await _companyCalendarRepository.InsertAsync(new CompanyCalendar
            (
                id: Guid.Parse("c8a22c86-59b4-46e0-86ce-bfae430ef960"),
                companyCalendarDate: new DateTime(2020, 6, 8),
                isWeekend: true,
                isHoliday: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}