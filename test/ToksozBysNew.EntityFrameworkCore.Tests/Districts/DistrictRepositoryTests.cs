using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Districts;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Districts
{
    public class DistrictRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IDistrictRepository _districtRepository;

        public DistrictRepositoryTests()
        {
            _districtRepository = GetRequiredService<IDistrictRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _districtRepository.GetListAsync(
                    districtName: "da18ad44621742dba57f7be"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("767a4bd1-8029-45ac-a48d-d0cfac57fc9c"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _districtRepository.GetCountAsync(
                    districtName: "20554e626f694a528a1720302c879083ea0ea0f9b71349798bff7168acce4bf2731402d209db42b"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}