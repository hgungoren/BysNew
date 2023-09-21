using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Positions
{
    public class PositionsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IPositionsAppService _positionsAppService;
        private readonly IRepository<Position, Guid> _positionRepository;

        public PositionsAppServiceTests()
        {
            _positionsAppService = GetRequiredService<IPositionsAppService>();
            _positionRepository = GetRequiredService<IRepository<Position, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _positionsAppService.GetListAsync(new GetPositionsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("491e8315-8ffe-458d-a483-9e9f5ba8e394")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("fd327612-d00d-4d6a-b0e5-7c88b971a5f1")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _positionsAppService.GetAsync(Guid.Parse("491e8315-8ffe-458d-a483-9e9f5ba8e394"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("491e8315-8ffe-458d-a483-9e9f5ba8e394"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PositionCreateDto
            {
                PositionCode = "a86f27d19e1340229fe6c5c0dcb7041d89996b9429864594a56d8da107b22cee3eb85f6b42f54580b21fe2c",
                PositionName = "f16ca3ecc025447f87eb467f87d03ea40d3a1272686"
            };

            // Act
            var serviceResult = await _positionsAppService.CreateAsync(input);

            // Assert
            var result = await _positionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.PositionCode.ShouldBe("a86f27d19e1340229fe6c5c0dcb7041d89996b9429864594a56d8da107b22cee3eb85f6b42f54580b21fe2c");
            result.PositionName.ShouldBe("f16ca3ecc025447f87eb467f87d03ea40d3a1272686");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PositionUpdateDto()
            {
                PositionCode = "cbf86b708e764ada92eb1f1a4a794bd7b10f601d630640f59b58f0c41ec",
                PositionName = "96e8288d4fe74348b43bb86539bb561fce"
            };

            // Act
            var serviceResult = await _positionsAppService.UpdateAsync(Guid.Parse("491e8315-8ffe-458d-a483-9e9f5ba8e394"), input);

            // Assert
            var result = await _positionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.PositionCode.ShouldBe("cbf86b708e764ada92eb1f1a4a794bd7b10f601d630640f59b58f0c41ec");
            result.PositionName.ShouldBe("96e8288d4fe74348b43bb86539bb561fce");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _positionsAppService.DeleteAsync(Guid.Parse("491e8315-8ffe-458d-a483-9e9f5ba8e394"));

            // Assert
            var result = await _positionRepository.FindAsync(c => c.Id == Guid.Parse("491e8315-8ffe-458d-a483-9e9f5ba8e394"));

            result.ShouldBeNull();
        }
    }
}