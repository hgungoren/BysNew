using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Visits;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Visits
{
    public class VisitRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IVisitRepository _visitRepository;

        public VisitRepositoryTests()
        {
            _visitRepository = GetRequiredService<IVisitRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _visitRepository.GetListAsync(
                    visitNotes: "df83fd091b8c45b28c70a3a49"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("a0fd5129-a0d6-411e-b509-9371bf807aa0"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _visitRepository.GetCountAsync(
                    visitNotes: "0b63b6b6c4cf4fe292bc11f841090f2e15a26ac7fe"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}