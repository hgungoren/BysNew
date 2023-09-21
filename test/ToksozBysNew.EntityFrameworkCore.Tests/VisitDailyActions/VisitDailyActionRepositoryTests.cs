using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.VisitDailyActions;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.VisitDailyActions
{
    public class VisitDailyActionRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IVisitDailyActionRepository _visitDailyActionRepository;

        public VisitDailyActionRepositoryTests()
        {
            _visitDailyActionRepository = GetRequiredService<IVisitDailyActionRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _visitDailyActionRepository.GetListAsync(
                    visitDailyNote: "120e2371216c4"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("ea7d8753-4ba8-40a2-8475-20fe3d2c231e"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _visitDailyActionRepository.GetCountAsync(
                    visitDailyNote: "07be081911"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}