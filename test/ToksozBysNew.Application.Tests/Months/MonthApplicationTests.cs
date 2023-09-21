using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Months
{
    public class MonthsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IMonthsAppService _monthsAppService;
        private readonly IRepository<Month, Guid> _monthRepository;

        public MonthsAppServiceTests()
        {
            _monthsAppService = GetRequiredService<IMonthsAppService>();
            _monthRepository = GetRequiredService<IRepository<Month, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _monthsAppService.GetListAsync(new GetMonthsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("f8176482-db5a-4228-bb07-f9b45e7bf5bb")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("30e1f009-4785-4d03-909b-2d3c631032bd")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _monthsAppService.GetAsync(Guid.Parse("f8176482-db5a-4228-bb07-f9b45e7bf5bb"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("f8176482-db5a-4228-bb07-f9b45e7bf5bb"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new MonthCreateDto
            {
                Name = "7095b88105b8470ea0aae78c750efcda7c14cfdf760d"
            };

            // Act
            var serviceResult = await _monthsAppService.CreateAsync(input);

            // Assert
            var result = await _monthRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("7095b88105b8470ea0aae78c750efcda7c14cfdf760d");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new MonthUpdateDto()
            {
                Name = "6df55e0ed95d4169909128ef5f9ab25781b9"
            };

            // Act
            var serviceResult = await _monthsAppService.UpdateAsync(Guid.Parse("f8176482-db5a-4228-bb07-f9b45e7bf5bb"), input);

            // Assert
            var result = await _monthRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("6df55e0ed95d4169909128ef5f9ab25781b9");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _monthsAppService.DeleteAsync(Guid.Parse("f8176482-db5a-4228-bb07-f9b45e7bf5bb"));

            // Assert
            var result = await _monthRepository.FindAsync(c => c.Id == Guid.Parse("f8176482-db5a-4228-bb07-f9b45e7bf5bb"));

            result.ShouldBeNull();
        }
    }
}