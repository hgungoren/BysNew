using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.CustomerTypes
{
    public class CustomerTypesAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly ICustomerTypesAppService _customerTypesAppService;
        private readonly IRepository<CustomerType, Guid> _customerTypeRepository;

        public CustomerTypesAppServiceTests()
        {
            _customerTypesAppService = GetRequiredService<ICustomerTypesAppService>();
            _customerTypeRepository = GetRequiredService<IRepository<CustomerType, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerTypesAppService.GetListAsync(new GetCustomerTypesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("fb807768-3ca4-4e77-aafa-9a79829f5ca4")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("94d54d9f-4dd2-4498-9763-a09d4ded569a")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerTypesAppService.GetAsync(Guid.Parse("fb807768-3ca4-4e77-aafa-9a79829f5ca4"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("fb807768-3ca4-4e77-aafa-9a79829f5ca4"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerTypeCreateDto
            {
                TypeName = "31bfcc7ca2c44d2c9fb4562d7c37ac9470387b97fba94385bb07e54b79425f702a98708e047543618284573e9d9c85a"
            };

            // Act
            var serviceResult = await _customerTypesAppService.CreateAsync(input);

            // Assert
            var result = await _customerTypeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.TypeName.ShouldBe("31bfcc7ca2c44d2c9fb4562d7c37ac9470387b97fba94385bb07e54b79425f702a98708e047543618284573e9d9c85a");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerTypeUpdateDto()
            {
                TypeName = "c8f2912bb8634b61a8a96525b06a60b8ce369d0d0ad144e0"
            };

            // Act
            var serviceResult = await _customerTypesAppService.UpdateAsync(Guid.Parse("fb807768-3ca4-4e77-aafa-9a79829f5ca4"), input);

            // Assert
            var result = await _customerTypeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.TypeName.ShouldBe("c8f2912bb8634b61a8a96525b06a60b8ce369d0d0ad144e0");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerTypesAppService.DeleteAsync(Guid.Parse("fb807768-3ca4-4e77-aafa-9a79829f5ca4"));

            // Assert
            var result = await _customerTypeRepository.FindAsync(c => c.Id == Guid.Parse("fb807768-3ca4-4e77-aafa-9a79829f5ca4"));

            result.ShouldBeNull();
        }
    }
}