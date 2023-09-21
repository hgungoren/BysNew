using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Districts
{
    public class DistrictsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IDistrictsAppService _districtsAppService;
        private readonly IRepository<District, Guid> _districtRepository;

        public DistrictsAppServiceTests()
        {
            _districtsAppService = GetRequiredService<IDistrictsAppService>();
            _districtRepository = GetRequiredService<IRepository<District, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _districtsAppService.GetListAsync(new GetDistrictsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.District.Id == Guid.Parse("767a4bd1-8029-45ac-a48d-d0cfac57fc9c")).ShouldBe(true);
            result.Items.Any(x => x.District.Id == Guid.Parse("4aff8faf-7f79-4c95-b2ef-1c7710dcf324")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _districtsAppService.GetAsync(Guid.Parse("767a4bd1-8029-45ac-a48d-d0cfac57fc9c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("767a4bd1-8029-45ac-a48d-d0cfac57fc9c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DistrictCreateDto
            {
                DistrictName = "2c17372950824374b4800f81264a3360565aee01e5394e408fcb8d75fe44c28eba319a00af38474ebbb1d"
            };

            // Act
            var serviceResult = await _districtsAppService.CreateAsync(input);

            // Assert
            var result = await _districtRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DistrictName.ShouldBe("2c17372950824374b4800f81264a3360565aee01e5394e408fcb8d75fe44c28eba319a00af38474ebbb1d");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DistrictUpdateDto()
            {
                DistrictName = "7ba0d6bbc6a04cbc8f041cd094fcbd7305c3f8b4df464c638358eb3ed69b55bb69d065"
            };

            // Act
            var serviceResult = await _districtsAppService.UpdateAsync(Guid.Parse("767a4bd1-8029-45ac-a48d-d0cfac57fc9c"), input);

            // Assert
            var result = await _districtRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DistrictName.ShouldBe("7ba0d6bbc6a04cbc8f041cd094fcbd7305c3f8b4df464c638358eb3ed69b55bb69d065");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _districtsAppService.DeleteAsync(Guid.Parse("767a4bd1-8029-45ac-a48d-d0cfac57fc9c"));

            // Assert
            var result = await _districtRepository.FindAsync(c => c.Id == Guid.Parse("767a4bd1-8029-45ac-a48d-d0cfac57fc9c"));

            result.ShouldBeNull();
        }
    }
}