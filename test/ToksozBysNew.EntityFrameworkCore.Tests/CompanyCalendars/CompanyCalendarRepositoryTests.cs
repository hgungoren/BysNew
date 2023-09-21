using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.CompanyCalendars;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.CompanyCalendars
{
    public class CompanyCalendarRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly ICompanyCalendarRepository _companyCalendarRepository;

        public CompanyCalendarRepositoryTests()
        {
            _companyCalendarRepository = GetRequiredService<ICompanyCalendarRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _companyCalendarRepository.GetListAsync(
                    isWeekend: true,
                    isHoliday: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("080ed2c9-83d0-44fd-b024-591ffb3d1f4c"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _companyCalendarRepository.GetCountAsync(
                    isWeekend: true,
                    isHoliday: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}