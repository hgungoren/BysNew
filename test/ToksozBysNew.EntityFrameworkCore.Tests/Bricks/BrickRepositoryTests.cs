using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Bricks;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Bricks
{
    public class BrickRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IBrickRepository _brickRepository;

        public BrickRepositoryTests()
        {
            _brickRepository = GetRequiredService<IBrickRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _brickRepository.GetListAsync(
                    brickName: "620718b744964f58bf6092cf56c8"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("cb141fd3-dc82-4923-b8b3-2d8a41cd9908"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _brickRepository.GetCountAsync(
                    brickName: "a5174e0389054232a485753f33bdf2b533a087c27264437abf764c93086b2191ff7f63b6541346cf97b09439c2187c1d35"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}