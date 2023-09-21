using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Companies;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Companies
{
    public class CompanyRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyRepositoryTests()
        {
            _companyRepository = GetRequiredService<ICompanyRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _companyRepository.GetListAsync(
                    companyName: "4a665442c45e4d68bee0c0c8e7486ecbc505ea98fb694a43b8",
                    isActive: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("7d3f3766-a5f8-421e-8696-bf63ca6a302f"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _companyRepository.GetCountAsync(
                    companyName: "b0be913dbd774f4ba38ec1ed6fef17535a8179198b3a4431a3",
                    isActive: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}