using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.CustomerAddresses
{
    public class CustomerAddressesAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly ICustomerAddressesAppService _customerAddressesAppService;
        private readonly IRepository<CustomerAddress, Guid> _customerAddressRepository;

        public CustomerAddressesAppServiceTests()
        {
            _customerAddressesAppService = GetRequiredService<ICustomerAddressesAppService>();
            _customerAddressRepository = GetRequiredService<IRepository<CustomerAddress, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerAddressesAppService.GetListAsync(new GetCustomerAddressesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerAddress.Id == Guid.Parse("b3ab3ed9-bc4e-4af2-8969-60ef731c3aa5")).ShouldBe(true);
            result.Items.Any(x => x.CustomerAddress.Id == Guid.Parse("f9e6fef0-e413-4534-bba6-f107df3dbb68")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerAddressesAppService.GetAsync(Guid.Parse("b3ab3ed9-bc4e-4af2-8969-60ef731c3aa5"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b3ab3ed9-bc4e-4af2-8969-60ef731c3aa5"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerAddressCreateDto
            {
                Address = "eadf653aaf004d2cb726693830a75f2cbe48515d19664e62ab656b9c18ae1adcac097c4d54e"
            };

            // Act
            var serviceResult = await _customerAddressesAppService.CreateAsync(input);

            // Assert
            var result = await _customerAddressRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Address.ShouldBe("eadf653aaf004d2cb726693830a75f2cbe48515d19664e62ab656b9c18ae1adcac097c4d54e");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerAddressUpdateDto()
            {
                Address = "c46e4e3a04f34ab48226726e68e695f63c2bf727cfc0491e9d18"
            };

            // Act
            var serviceResult = await _customerAddressesAppService.UpdateAsync(Guid.Parse("b3ab3ed9-bc4e-4af2-8969-60ef731c3aa5"), input);

            // Assert
            var result = await _customerAddressRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Address.ShouldBe("c46e4e3a04f34ab48226726e68e695f63c2bf727cfc0491e9d18");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerAddressesAppService.DeleteAsync(Guid.Parse("b3ab3ed9-bc4e-4af2-8969-60ef731c3aa5"));

            // Assert
            var result = await _customerAddressRepository.FindAsync(c => c.Id == Guid.Parse("b3ab3ed9-bc4e-4af2-8969-60ef731c3aa5"));

            result.ShouldBeNull();
        }
    }
}