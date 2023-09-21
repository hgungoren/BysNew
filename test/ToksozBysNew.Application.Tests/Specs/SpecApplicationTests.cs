using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Specs
{
    public class SpecsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly ISpecsAppService _specsAppService;
        private readonly IRepository<Spec, Guid> _specRepository;

        public SpecsAppServiceTests()
        {
            _specsAppService = GetRequiredService<ISpecsAppService>();
            _specRepository = GetRequiredService<IRepository<Spec, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _specsAppService.GetListAsync(new GetSpecsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("4ed69100-26c6-4519-9a70-9e52bcc90594")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("6fef81ad-4ea7-4ffb-ac07-6b3c402a5661")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _specsAppService.GetAsync(Guid.Parse("4ed69100-26c6-4519-9a70-9e52bcc90594"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("4ed69100-26c6-4519-9a70-9e52bcc90594"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SpecCreateDto
            {
                SpecCode = "a99b85457be54597bf62fd6ee3874f37568f2918b5254f7aa9201dc5966bc142541b58f2f",
                SpecName = "9f3bbcd8ea58407abb1d0d5bd89c741166b9"
            };

            // Act
            var serviceResult = await _specsAppService.CreateAsync(input);

            // Assert
            var result = await _specRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.SpecCode.ShouldBe("a99b85457be54597bf62fd6ee3874f37568f2918b5254f7aa9201dc5966bc142541b58f2f");
            result.SpecName.ShouldBe("9f3bbcd8ea58407abb1d0d5bd89c741166b9");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SpecUpdateDto()
            {
                SpecCode = "f7810300e5844a59af12a1e6e0b1e982674911788742471c9bd8f771fb0ac6d033944b385fc14480b879ef822c3869839f",
                SpecName = "1584b0022ab641ca978a49b"
            };

            // Act
            var serviceResult = await _specsAppService.UpdateAsync(Guid.Parse("4ed69100-26c6-4519-9a70-9e52bcc90594"), input);

            // Assert
            var result = await _specRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.SpecCode.ShouldBe("f7810300e5844a59af12a1e6e0b1e982674911788742471c9bd8f771fb0ac6d033944b385fc14480b879ef822c3869839f");
            result.SpecName.ShouldBe("1584b0022ab641ca978a49b");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _specsAppService.DeleteAsync(Guid.Parse("4ed69100-26c6-4519-9a70-9e52bcc90594"));

            // Assert
            var result = await _specRepository.FindAsync(c => c.Id == Guid.Parse("4ed69100-26c6-4519-9a70-9e52bcc90594"));

            result.ShouldBeNull();
        }
    }
}