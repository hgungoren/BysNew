using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.AccountGroups
{
    public class AccountGroupsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IAccountGroupsAppService _accountGroupsAppService;
        private readonly IRepository<AccountGroup, Guid> _accountGroupRepository;

        public AccountGroupsAppServiceTests()
        {
            _accountGroupsAppService = GetRequiredService<IAccountGroupsAppService>();
            _accountGroupRepository = GetRequiredService<IRepository<AccountGroup, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _accountGroupsAppService.GetListAsync(new GetAccountGroupsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("bbc2929d-a3fc-4fbc-afed-39c6ddafe257")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("5d3e6ada-be2e-42b5-9a0e-521681041088")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _accountGroupsAppService.GetAsync(Guid.Parse("bbc2929d-a3fc-4fbc-afed-39c6ddafe257"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("bbc2929d-a3fc-4fbc-afed-39c6ddafe257"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new AccountGroupCreateDto
            {
                AccountGroupName = "7303f2b0638f4864b29afb8b79e80f318eb6f292f70942469cb36d5b285b48ea7603f0ee58d94027b8abbbf0b10160ec308f8c9e0dec45e9a66cf8c9242a531b845c010dadda4536bfba5a82f3fc92593b577301239f471e87858048c70321d5924a71b3b95240f5b72b0e4c2a949b0f5e539cad7f9445b692218fd16187310",
                IsUnitEnterable = true
            };

            // Act
            var serviceResult = await _accountGroupsAppService.CreateAsync(input);

            // Assert
            var result = await _accountGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AccountGroupName.ShouldBe("7303f2b0638f4864b29afb8b79e80f318eb6f292f70942469cb36d5b285b48ea7603f0ee58d94027b8abbbf0b10160ec308f8c9e0dec45e9a66cf8c9242a531b845c010dadda4536bfba5a82f3fc92593b577301239f471e87858048c70321d5924a71b3b95240f5b72b0e4c2a949b0f5e539cad7f9445b692218fd16187310");
            result.IsUnitEnterable.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new AccountGroupUpdateDto()
            {
                AccountGroupName = "ed991fa712ab4526aa384a8d3fafe4076cc9e90dfd5648aa88aede4156c08392613d2013ecf84863a4d477ebec1f63408f899a55f38041a1a61ac93a7ba546eabde2fc9b765d40598cf1f27f42cfb9659449aa3386b441e58a32cf9d116d9561195c8e3f934f4521854af0f9f78fedbc6ec74f5fd65f47829294fee56c2c503",
                IsUnitEnterable = true
            };

            // Act
            var serviceResult = await _accountGroupsAppService.UpdateAsync(Guid.Parse("bbc2929d-a3fc-4fbc-afed-39c6ddafe257"), input);

            // Assert
            var result = await _accountGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AccountGroupName.ShouldBe("ed991fa712ab4526aa384a8d3fafe4076cc9e90dfd5648aa88aede4156c08392613d2013ecf84863a4d477ebec1f63408f899a55f38041a1a61ac93a7ba546eabde2fc9b765d40598cf1f27f42cfb9659449aa3386b441e58a32cf9d116d9561195c8e3f934f4521854af0f9f78fedbc6ec74f5fd65f47829294fee56c2c503");
            result.IsUnitEnterable.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _accountGroupsAppService.DeleteAsync(Guid.Parse("bbc2929d-a3fc-4fbc-afed-39c6ddafe257"));

            // Assert
            var result = await _accountGroupRepository.FindAsync(c => c.Id == Guid.Parse("bbc2929d-a3fc-4fbc-afed-39c6ddafe257"));

            result.ShouldBeNull();
        }
    }
}