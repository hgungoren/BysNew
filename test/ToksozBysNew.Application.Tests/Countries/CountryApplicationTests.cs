using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Countries
{
    public class CountriesAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly ICountriesAppService _countriesAppService;
        private readonly IRepository<Country, Guid> _countryRepository;

        public CountriesAppServiceTests()
        {
            _countriesAppService = GetRequiredService<ICountriesAppService>();
            _countryRepository = GetRequiredService<IRepository<Country, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _countriesAppService.GetListAsync(new GetCountriesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("f8fd88e5-a5ef-4908-a8e7-d9b43426521c")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("b1414ea5-7f82-4879-a0c0-a55d06d4376b")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _countriesAppService.GetAsync(Guid.Parse("f8fd88e5-a5ef-4908-a8e7-d9b43426521c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("f8fd88e5-a5ef-4908-a8e7-d9b43426521c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CountryCreateDto
            {
                CountryName = "bac84a3e040b48f4b1226f4943fd6ed36f57c446b3b54409a3e39ec9155735bbd6e5"
            };

            // Act
            var serviceResult = await _countriesAppService.CreateAsync(input);

            // Assert
            var result = await _countryRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CountryName.ShouldBe("bac84a3e040b48f4b1226f4943fd6ed36f57c446b3b54409a3e39ec9155735bbd6e5");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CountryUpdateDto()
            {
                CountryName = "946880e78f6d4174ab0e1d01d1d9bcb1f29387c40a2f4bacae61c0ed126bcc564fcec1c8d7f5405da4"
            };

            // Act
            var serviceResult = await _countriesAppService.UpdateAsync(Guid.Parse("f8fd88e5-a5ef-4908-a8e7-d9b43426521c"), input);

            // Assert
            var result = await _countryRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CountryName.ShouldBe("946880e78f6d4174ab0e1d01d1d9bcb1f29387c40a2f4bacae61c0ed126bcc564fcec1c8d7f5405da4");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _countriesAppService.DeleteAsync(Guid.Parse("f8fd88e5-a5ef-4908-a8e7-d9b43426521c"));

            // Assert
            var result = await _countryRepository.FindAsync(c => c.Id == Guid.Parse("f8fd88e5-a5ef-4908-a8e7-d9b43426521c"));

            result.ShouldBeNull();
        }
    }
}