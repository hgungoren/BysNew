using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.CustomerTitles;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.CustomerTitles
{
    public class CustomerTitleRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly ICustomerTitleRepository _customerTitleRepository;

        public CustomerTitleRepositoryTests()
        {
            _customerTitleRepository = GetRequiredService<ICustomerTitleRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerTitleRepository.GetListAsync(
                    titleName: "614656effd494506adcbaae80143e6724a28697e891a4b7ca5664f8e364724a4904047468b1e4f418236398de9db9b"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("3d057538-9269-4c85-b4c0-a7d2fdbfe3b5"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerTitleRepository.GetCountAsync(
                    titleName: "f730c5f034b5431c8f1b3736ad1174ff92c5"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}