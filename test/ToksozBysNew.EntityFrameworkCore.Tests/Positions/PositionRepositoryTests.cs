using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Positions;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Positions
{
    public class PositionRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IPositionRepository _positionRepository;

        public PositionRepositoryTests()
        {
            _positionRepository = GetRequiredService<IPositionRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _positionRepository.GetListAsync(
                    positionCode: "9710d65208d342ca9437494c5dce000f21bc596f2bc6451481b08c30f75c3a22a79954456c0b49bda659ec186fd5f7f70",
                    positionName: "071492897a2749989fc7c4288d"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("491e8315-8ffe-458d-a483-9e9f5ba8e394"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _positionRepository.GetCountAsync(
                    positionCode: "2467e379c5994aa38a96d7f62723b2f464221214a80f4ecfbddee0433d68f204f",
                    positionName: "00623fa0b5dc458787371d04f71b55e26ce7"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}