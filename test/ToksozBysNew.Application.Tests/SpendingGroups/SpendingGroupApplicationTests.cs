using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.SpendingGroups
{
    public class SpendingGroupsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly ISpendingGroupsAppService _spendingGroupsAppService;
        private readonly IRepository<SpendingGroup, Guid> _spendingGroupRepository;

        public SpendingGroupsAppServiceTests()
        {
            _spendingGroupsAppService = GetRequiredService<ISpendingGroupsAppService>();
            _spendingGroupRepository = GetRequiredService<IRepository<SpendingGroup, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _spendingGroupsAppService.GetListAsync(new GetSpendingGroupsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("cc378bb3-3f05-459c-9259-1e59d3d267ef")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("e861d57e-a7c4-48ab-9857-c5a8c3a7edfa")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _spendingGroupsAppService.GetAsync(Guid.Parse("cc378bb3-3f05-459c-9259-1e59d3d267ef"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("cc378bb3-3f05-459c-9259-1e59d3d267ef"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SpendingGroupCreateDto
            {
                Name = "d705bc6de46147e29eb09d0391e6c1a2b2bb6ea7329b453b93b4b10"
            };

            // Act
            var serviceResult = await _spendingGroupsAppService.CreateAsync(input);

            // Assert
            var result = await _spendingGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("d705bc6de46147e29eb09d0391e6c1a2b2bb6ea7329b453b93b4b10");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SpendingGroupUpdateDto()
            {
                Name = "5c30f635a7ef41b8b5b3ec110c62f88afad94048f4e74c95a959ddf8608d2f8a318b"
            };

            // Act
            var serviceResult = await _spendingGroupsAppService.UpdateAsync(Guid.Parse("cc378bb3-3f05-459c-9259-1e59d3d267ef"), input);

            // Assert
            var result = await _spendingGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("5c30f635a7ef41b8b5b3ec110c62f88afad94048f4e74c95a959ddf8608d2f8a318b");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _spendingGroupsAppService.DeleteAsync(Guid.Parse("cc378bb3-3f05-459c-9259-1e59d3d267ef"));

            // Assert
            var result = await _spendingGroupRepository.FindAsync(c => c.Id == Guid.Parse("cc378bb3-3f05-459c-9259-1e59d3d267ef"));

            result.ShouldBeNull();
        }
    }
}