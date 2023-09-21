using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Provinces;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Provinces
{
    public class ProvinceRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IProvinceRepository _provinceRepository;

        public ProvinceRepositoryTests()
        {
            _provinceRepository = GetRequiredService<IProvinceRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _provinceRepository.GetListAsync(
                    provinceName: "0d6804024686465eba04072d1d81"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("2733dc16-33e4-46b3-980f-a904ea0b38f5"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _provinceRepository.GetCountAsync(
                    provinceName: "32ae2136716f4693a25548928096c0abb285264272e94d658ad98bea9ec13b153e54a6108f964e948fa4343a7e6e4"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}