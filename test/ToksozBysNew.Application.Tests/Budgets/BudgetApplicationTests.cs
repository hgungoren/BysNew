using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Budgets
{
    public class BudgetsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IBudgetsAppService _budgetsAppService;
        private readonly IRepository<Budget, Guid> _budgetRepository;

        public BudgetsAppServiceTests()
        {
            _budgetsAppService = GetRequiredService<IBudgetsAppService>();
            _budgetRepository = GetRequiredService<IRepository<Budget, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _budgetsAppService.GetListAsync(new GetBudgetsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Budget.Id == Guid.Parse("c175814f-d2bb-4939-a226-080e2f05ad6f")).ShouldBe(true);
            result.Items.Any(x => x.Budget.Id == Guid.Parse("c894d8dd-58ca-4b3e-8456-a05842e69d1f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _budgetsAppService.GetAsync(Guid.Parse("c175814f-d2bb-4939-a226-080e2f05ad6f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c175814f-d2bb-4939-a226-080e2f05ad6f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new BudgetCreateDto
            {
                BudgetName = "98198f6b88c04a8a859c5a43af907f8d625ffc2a57814bd595",
                Year = 2082091803,
                Comment = "da5b5cc0241c482e9c7adefba32df365f9c07b51de6a4521a256f4850bfa0fc8afca9cfdec544793a886f8373d197cca43eff034320a4c4eb8a3f3eb45f8f18ce7f22133742e4930bc1deb03869d1e90645c8a3b30c24140ac712073f39fc9a2d5a0293ecc554e0f833e7a6526c1bbc23364b131a729461d8ad0588258fbc0f",
                IsActive = true,
                OpenUntil = new DateTime(2002, 7, 14)
            };

            // Act
            var serviceResult = await _budgetsAppService.CreateAsync(input);

            // Assert
            var result = await _budgetRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.BudgetName.ShouldBe("98198f6b88c04a8a859c5a43af907f8d625ffc2a57814bd595");
            result.Year.ShouldBe(2082091803);
            result.Comment.ShouldBe("da5b5cc0241c482e9c7adefba32df365f9c07b51de6a4521a256f4850bfa0fc8afca9cfdec544793a886f8373d197cca43eff034320a4c4eb8a3f3eb45f8f18ce7f22133742e4930bc1deb03869d1e90645c8a3b30c24140ac712073f39fc9a2d5a0293ecc554e0f833e7a6526c1bbc23364b131a729461d8ad0588258fbc0f");
            result.IsActive.ShouldBe(true);
            result.OpenUntil.ShouldBe(new DateTime(2002, 7, 14));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BudgetUpdateDto()
            {
                BudgetName = "4a8635a436f8405e94e4986e04a0109f168ede04519549308d",
                Year = 692262515,
                Comment = "424ec238bc934c31ba4db9d65310326dbb75cf398647493aa10b9e7e3076041f7fb949103c6f45c6bd93f070a5813e14fa96afe3837b4d16a0aa24fff2733a0cb245108bb9814783a69d2350b95f12c245f0abe3dd17488b98daf8a943dd66d2a90f6cb93d414218b9b60907f3886d8f6f391008cd7d491188c12826adb38d9",
                IsActive = true,
                OpenUntil = new DateTime(2002, 2, 21)
            };

            // Act
            var serviceResult = await _budgetsAppService.UpdateAsync(Guid.Parse("c175814f-d2bb-4939-a226-080e2f05ad6f"), input);

            // Assert
            var result = await _budgetRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.BudgetName.ShouldBe("4a8635a436f8405e94e4986e04a0109f168ede04519549308d");
            result.Year.ShouldBe(692262515);
            result.Comment.ShouldBe("424ec238bc934c31ba4db9d65310326dbb75cf398647493aa10b9e7e3076041f7fb949103c6f45c6bd93f070a5813e14fa96afe3837b4d16a0aa24fff2733a0cb245108bb9814783a69d2350b95f12c245f0abe3dd17488b98daf8a943dd66d2a90f6cb93d414218b9b60907f3886d8f6f391008cd7d491188c12826adb38d9");
            result.IsActive.ShouldBe(true);
            result.OpenUntil.ShouldBe(new DateTime(2002, 2, 21));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _budgetsAppService.DeleteAsync(Guid.Parse("c175814f-d2bb-4939-a226-080e2f05ad6f"));

            // Assert
            var result = await _budgetRepository.FindAsync(c => c.Id == Guid.Parse("c175814f-d2bb-4939-a226-080e2f05ad6f"));

            result.ShouldBeNull();
        }
    }
}