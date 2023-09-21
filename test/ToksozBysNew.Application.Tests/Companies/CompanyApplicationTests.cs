using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Companies
{
    public class CompaniesAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly ICompaniesAppService _companiesAppService;
        private readonly IRepository<Company, Guid> _companyRepository;

        public CompaniesAppServiceTests()
        {
            _companiesAppService = GetRequiredService<ICompaniesAppService>();
            _companyRepository = GetRequiredService<IRepository<Company, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _companiesAppService.GetListAsync(new GetCompaniesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("7d3f3766-a5f8-421e-8696-bf63ca6a302f")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("da810093-82b5-41cf-abab-5098585b385a")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _companiesAppService.GetAsync(Guid.Parse("7d3f3766-a5f8-421e-8696-bf63ca6a302f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("7d3f3766-a5f8-421e-8696-bf63ca6a302f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CompanyCreateDto
            {
                CompanyName = "b982b12aade94587acda34efc4fc8c7c2c265bfa08ff4010b8",
                IsActive = true
            };

            // Act
            var serviceResult = await _companiesAppService.CreateAsync(input);

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyName.ShouldBe("b982b12aade94587acda34efc4fc8c7c2c265bfa08ff4010b8");
            result.IsActive.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CompanyUpdateDto()
            {
                CompanyName = "143c007673154e858bb947afd7a6a84cb40c2efe60ae48e093",
                IsActive = true
            };

            // Act
            var serviceResult = await _companiesAppService.UpdateAsync(Guid.Parse("7d3f3766-a5f8-421e-8696-bf63ca6a302f"), input);

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyName.ShouldBe("143c007673154e858bb947afd7a6a84cb40c2efe60ae48e093");
            result.IsActive.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _companiesAppService.DeleteAsync(Guid.Parse("7d3f3766-a5f8-421e-8696-bf63ca6a302f"));

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == Guid.Parse("7d3f3766-a5f8-421e-8696-bf63ca6a302f"));

            result.ShouldBeNull();
        }
    }
}