using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Bricks
{
    public class BricksAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IBricksAppService _bricksAppService;
        private readonly IRepository<Brick, Guid> _brickRepository;

        public BricksAppServiceTests()
        {
            _bricksAppService = GetRequiredService<IBricksAppService>();
            _brickRepository = GetRequiredService<IRepository<Brick, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _bricksAppService.GetListAsync(new GetBricksInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("cb141fd3-dc82-4923-b8b3-2d8a41cd9908")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("d9dbb40d-1c41-4143-a8df-263b85f69967")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _bricksAppService.GetAsync(Guid.Parse("cb141fd3-dc82-4923-b8b3-2d8a41cd9908"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("cb141fd3-dc82-4923-b8b3-2d8a41cd9908"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new BrickCreateDto
            {
                BrickName = "96b721a05ba84d23"
            };

            // Act
            var serviceResult = await _bricksAppService.CreateAsync(input);

            // Assert
            var result = await _brickRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.BrickName.ShouldBe("96b721a05ba84d23");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BrickUpdateDto()
            {
                BrickName = "edccf1d026ae4610ac"
            };

            // Act
            var serviceResult = await _bricksAppService.UpdateAsync(Guid.Parse("cb141fd3-dc82-4923-b8b3-2d8a41cd9908"), input);

            // Assert
            var result = await _brickRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.BrickName.ShouldBe("edccf1d026ae4610ac");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _bricksAppService.DeleteAsync(Guid.Parse("cb141fd3-dc82-4923-b8b3-2d8a41cd9908"));

            // Assert
            var result = await _brickRepository.FindAsync(c => c.Id == Guid.Parse("cb141fd3-dc82-4923-b8b3-2d8a41cd9908"));

            result.ShouldBeNull();
        }
    }
}