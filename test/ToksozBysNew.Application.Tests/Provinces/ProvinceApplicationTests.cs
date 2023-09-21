using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Provinces
{
    public class ProvincesAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IProvincesAppService _provincesAppService;
        private readonly IRepository<Province, Guid> _provinceRepository;

        public ProvincesAppServiceTests()
        {
            _provincesAppService = GetRequiredService<IProvincesAppService>();
            _provinceRepository = GetRequiredService<IRepository<Province, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _provincesAppService.GetListAsync(new GetProvincesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Province.Id == Guid.Parse("2733dc16-33e4-46b3-980f-a904ea0b38f5")).ShouldBe(true);
            result.Items.Any(x => x.Province.Id == Guid.Parse("ddbc210d-7497-4970-a91b-8c4f113aaf2d")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _provincesAppService.GetAsync(Guid.Parse("2733dc16-33e4-46b3-980f-a904ea0b38f5"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("2733dc16-33e4-46b3-980f-a904ea0b38f5"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ProvinceCreateDto
            {
                ProvinceName = "f122191546154a9393a7d770dd976601ee0134d98f6447468c4d472dc7ab8a625fc4c30f1aa245d9b4728c"
            };

            // Act
            var serviceResult = await _provincesAppService.CreateAsync(input);

            // Assert
            var result = await _provinceRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ProvinceName.ShouldBe("f122191546154a9393a7d770dd976601ee0134d98f6447468c4d472dc7ab8a625fc4c30f1aa245d9b4728c");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ProvinceUpdateDto()
            {
                ProvinceName = "efc0d22fe66c4ea7a1ca7cb98a2a5029ddbdfb88b8e24420a02a336e07c88e1e39a48d8792364"
            };

            // Act
            var serviceResult = await _provincesAppService.UpdateAsync(Guid.Parse("2733dc16-33e4-46b3-980f-a904ea0b38f5"), input);

            // Assert
            var result = await _provinceRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ProvinceName.ShouldBe("efc0d22fe66c4ea7a1ca7cb98a2a5029ddbdfb88b8e24420a02a336e07c88e1e39a48d8792364");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _provincesAppService.DeleteAsync(Guid.Parse("2733dc16-33e4-46b3-980f-a904ea0b38f5"));

            // Assert
            var result = await _provinceRepository.FindAsync(c => c.Id == Guid.Parse("2733dc16-33e4-46b3-980f-a904ea0b38f5"));

            result.ShouldBeNull();
        }
    }
}