using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.CustomerAddresses;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.CustomerAddresses
{
    public class CustomerAddressRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly ICustomerAddressRepository _customerAddressRepository;

        public CustomerAddressRepositoryTests()
        {
            _customerAddressRepository = GetRequiredService<ICustomerAddressRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerAddressRepository.GetListAsync(
                    address: "cba3211337284fc0ac5c1f7d6fc2d2ac"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("b3ab3ed9-bc4e-4af2-8969-60ef731c3aa5"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerAddressRepository.GetCountAsync(
                    address: "87ab8c25559"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}