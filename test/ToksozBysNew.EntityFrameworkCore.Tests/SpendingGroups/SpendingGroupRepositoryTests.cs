using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.SpendingGroups;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.SpendingGroups
{
    public class SpendingGroupRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly ISpendingGroupRepository _spendingGroupRepository;

        public SpendingGroupRepositoryTests()
        {
            _spendingGroupRepository = GetRequiredService<ISpendingGroupRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _spendingGroupRepository.GetListAsync(
                    name: "62cbb81"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("cc378bb3-3f05-459c-9259-1e59d3d267ef"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _spendingGroupRepository.GetCountAsync(
                    name: "b16401ec066d41fab583d02213553eed06f1f3be113a4782ae7df78bb3cf80ba71c15e87b70343ca87a8bf06a3e641"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}