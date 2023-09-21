using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.CustomerTitles
{
    public class CustomerTitlesAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly ICustomerTitlesAppService _customerTitlesAppService;
        private readonly IRepository<CustomerTitle, Guid> _customerTitleRepository;

        public CustomerTitlesAppServiceTests()
        {
            _customerTitlesAppService = GetRequiredService<ICustomerTitlesAppService>();
            _customerTitleRepository = GetRequiredService<IRepository<CustomerTitle, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerTitlesAppService.GetListAsync(new GetCustomerTitlesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("3d057538-9269-4c85-b4c0-a7d2fdbfe3b5")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("94b86765-49f2-49fd-9df3-60da564009a1")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerTitlesAppService.GetAsync(Guid.Parse("3d057538-9269-4c85-b4c0-a7d2fdbfe3b5"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("3d057538-9269-4c85-b4c0-a7d2fdbfe3b5"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerTitleCreateDto
            {
                TitleName = "e23753120e454fa3b7c6ce9f4c7fbf7130806e665f32471b8956f801506ebd021871f20"
            };

            // Act
            var serviceResult = await _customerTitlesAppService.CreateAsync(input);

            // Assert
            var result = await _customerTitleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.TitleName.ShouldBe("e23753120e454fa3b7c6ce9f4c7fbf7130806e665f32471b8956f801506ebd021871f20");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerTitleUpdateDto()
            {
                TitleName = "b3310c66edce4d408a2a05d8c659f2a10c8286432c9d4df4b8707e35e52bceed45d00"
            };

            // Act
            var serviceResult = await _customerTitlesAppService.UpdateAsync(Guid.Parse("3d057538-9269-4c85-b4c0-a7d2fdbfe3b5"), input);

            // Assert
            var result = await _customerTitleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.TitleName.ShouldBe("b3310c66edce4d408a2a05d8c659f2a10c8286432c9d4df4b8707e35e52bceed45d00");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerTitlesAppService.DeleteAsync(Guid.Parse("3d057538-9269-4c85-b4c0-a7d2fdbfe3b5"));

            // Assert
            var result = await _customerTitleRepository.FindAsync(c => c.Id == Guid.Parse("3d057538-9269-4c85-b4c0-a7d2fdbfe3b5"));

            result.ShouldBeNull();
        }
    }
}