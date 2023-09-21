using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Months;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Months
{
    public class MonthRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IMonthRepository _monthRepository;

        public MonthRepositoryTests()
        {
            _monthRepository = GetRequiredService<IMonthRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _monthRepository.GetListAsync(
                    name: "19b9434dbf7e4236af74730bcb4179c692b67f663813409884455b2707e882efe14414a49fbc4b9eba02746aaabde4f7e"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("f8176482-db5a-4228-bb07-f9b45e7bf5bb"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _monthRepository.GetCountAsync(
                    name: "3b64a7b1037c4cf293f5c9ab236fd722082da2bb6bb544f6aa6c39bb60f6193feaa7bd4ce70b4315"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}