using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.CompanyCalendars
{
    public class CompanyCalendarsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly ICompanyCalendarsAppService _companyCalendarsAppService;
        private readonly IRepository<CompanyCalendar, Guid> _companyCalendarRepository;

        public CompanyCalendarsAppServiceTests()
        {
            _companyCalendarsAppService = GetRequiredService<ICompanyCalendarsAppService>();
            _companyCalendarRepository = GetRequiredService<IRepository<CompanyCalendar, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _companyCalendarsAppService.GetListAsync(new GetCompanyCalendarsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("080ed2c9-83d0-44fd-b024-591ffb3d1f4c")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("c8a22c86-59b4-46e0-86ce-bfae430ef960")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _companyCalendarsAppService.GetAsync(Guid.Parse("080ed2c9-83d0-44fd-b024-591ffb3d1f4c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("080ed2c9-83d0-44fd-b024-591ffb3d1f4c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CompanyCalendarCreateDto
            {
                CompanyCalendarDate = new DateTime(2007, 5, 22),
                IsWeekend = true,
                IsHoliday = true
            };

            // Act
            var serviceResult = await _companyCalendarsAppService.CreateAsync(input);

            // Assert
            var result = await _companyCalendarRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyCalendarDate.ShouldBe(new DateTime(2007, 5, 22));
            result.IsWeekend.ShouldBe(true);
            result.IsHoliday.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CompanyCalendarUpdateDto()
            {
                CompanyCalendarDate = new DateTime(2011, 3, 16),
                IsWeekend = true,
                IsHoliday = true
            };

            // Act
            var serviceResult = await _companyCalendarsAppService.UpdateAsync(Guid.Parse("080ed2c9-83d0-44fd-b024-591ffb3d1f4c"), input);

            // Assert
            var result = await _companyCalendarRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyCalendarDate.ShouldBe(new DateTime(2011, 3, 16));
            result.IsWeekend.ShouldBe(true);
            result.IsHoliday.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _companyCalendarsAppService.DeleteAsync(Guid.Parse("080ed2c9-83d0-44fd-b024-591ffb3d1f4c"));

            // Assert
            var result = await _companyCalendarRepository.FindAsync(c => c.Id == Guid.Parse("080ed2c9-83d0-44fd-b024-591ffb3d1f4c"));

            result.ShouldBeNull();
        }
    }
}