using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Countries;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Countries
{
    public class CountryRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountryRepositoryTests()
        {
            _countryRepository = GetRequiredService<ICountryRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _countryRepository.GetListAsync(
                    countryName: "2fa9d369e48a411bb4cb96e82a68bc1"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("f8fd88e5-a5ef-4908-a8e7-d9b43426521c"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _countryRepository.GetCountAsync(
                    countryName: "128d502983dd4c9b94b347589770acdc16fada87e4a34fba90f1c"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}