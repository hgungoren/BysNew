using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.BudgetDistributions
{
    public class BudgetDistributionsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IBudgetDistributionsAppService _budgetDistributionsAppService;
        private readonly IRepository<BudgetDistribution, Guid> _budgetDistributionRepository;

        public BudgetDistributionsAppServiceTests()
        {
            _budgetDistributionsAppService = GetRequiredService<IBudgetDistributionsAppService>();
            _budgetDistributionRepository = GetRequiredService<IRepository<BudgetDistribution, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _budgetDistributionsAppService.GetListAsync(new GetBudgetDistributionsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.BudgetDistribution.Id == Guid.Parse("2adf600c-203a-4ffe-ad5a-748b09b557b0")).ShouldBe(true);
            result.Items.Any(x => x.BudgetDistribution.Id == Guid.Parse("66ab922e-f314-485f-9649-81e0c5825e0d")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _budgetDistributionsAppService.GetAsync(Guid.Parse("2adf600c-203a-4ffe-ad5a-748b09b557b0"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("2adf600c-203a-4ffe-ad5a-748b09b557b0"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new BudgetDistributionCreateDto
            {
                CostCenter = "bcd66783833c480fab09ed5c53bd277bb5267f6a3d1b4fe796",
                ExpenseType = "0015889e5cbe4bf5bc766f2d1e9fd5bb6e749f33946a47b79e30e9d721833f561636990de70743aca1598df94606020155a530898d1f4fadb7eeaba20c4fdc84fd26d78756774e59903ef868dc1020faa962d03db76b4e7299e5f35a245f02c74d64648e3f6c46c09e43b90d04fd9e3203000b0ca6dd48c0a9e1719a5efc431b1542aafc1eb44d57a534a48bc3cef0e9259ab3a4178b4b12b48170d722732af6ef196c5df49e442d81bf3f8964d7ed23ee69fcdb1dae47789d60bfa9d4b06c878c74f369f73e419a8ff1c5c2a0e57b13bf940912111a4658afeef0df4aa96cac20c308d459104ca1ab3c67763c676d21a8fcd6e4365444be899ae5b0ebce816d8cfe6ffdf6c046a1ad423f075c7ad76ff0382ef1f0c24c5b889a78081bf242efa09a10ada83d42a4bb94a564",
                ProjectItem = 1623329946,
                Type = 1455903562,
                Unit = 1431245862,
                UnitValue = 960227858,
                Month = 142052245,
                Year = 1359057361,
                Ratio = 1709502612,
                Amount = 268889896,
                Memo = 1375864610,
                Invoice = 46334054,
                Currency = 819236449,
                CurrencyAmount = 1439667424,
                ExpenseCategory = 1015205765,
                ExpenseNecessity = 306446540,
                Comment = "06dfb2",
                Status = "ea568",
                Approval = 1112962210,
                IsActive = true,
                DepartmentId = Guid.Parse("1b174f96-2c84-4616-8d5d-7469f680b366"),
                BudgetId = Guid.Parse("c175814f-d2bb-4939-a226-080e2f05ad6f"),

            };

            // Act
            var serviceResult = await _budgetDistributionsAppService.CreateAsync(input);

            // Assert
            var result = await _budgetDistributionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CostCenter.ShouldBe("bcd66783833c480fab09ed5c53bd277bb5267f6a3d1b4fe796");
            result.ExpenseType.ShouldBe("0015889e5cbe4bf5bc766f2d1e9fd5bb6e749f33946a47b79e30e9d721833f561636990de70743aca1598df94606020155a530898d1f4fadb7eeaba20c4fdc84fd26d78756774e59903ef868dc1020faa962d03db76b4e7299e5f35a245f02c74d64648e3f6c46c09e43b90d04fd9e3203000b0ca6dd48c0a9e1719a5efc431b1542aafc1eb44d57a534a48bc3cef0e9259ab3a4178b4b12b48170d722732af6ef196c5df49e442d81bf3f8964d7ed23ee69fcdb1dae47789d60bfa9d4b06c878c74f369f73e419a8ff1c5c2a0e57b13bf940912111a4658afeef0df4aa96cac20c308d459104ca1ab3c67763c676d21a8fcd6e4365444be899ae5b0ebce816d8cfe6ffdf6c046a1ad423f075c7ad76ff0382ef1f0c24c5b889a78081bf242efa09a10ada83d42a4bb94a564");
            result.ProjectItem.ShouldBe(1623329946);
            result.Type.ShouldBe(1455903562);
            result.Unit.ShouldBe(1431245862);
            result.UnitValue.ShouldBe(960227858);
            result.Month.ShouldBe(142052245);
            result.Year.ShouldBe(1359057361);
            result.Ratio.ShouldBe(1709502612);
            result.Amount.ShouldBe(268889896);
            result.Memo.ShouldBe(1375864610);
            result.Invoice.ShouldBe(46334054);
            result.Currency.ShouldBe(819236449);
            result.CurrencyAmount.ShouldBe(1439667424);
            result.ExpenseCategory.ShouldBe(1015205765);
            result.ExpenseNecessity.ShouldBe(306446540);
            result.Comment.ShouldBe("06dfb2");
            result.Status.ShouldBe("ea568");
            result.Approval.ShouldBe(1112962210);
            result.IsActive.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BudgetDistributionUpdateDto()
            {
                CostCenter = "e31775cb0762420986bcfe08e0ffc7955c31d7b4443e4aa3ba",
                ExpenseType = "651282fb02a94e2689c06b866a5c033a9a392b4742294548b9bd5c39c7b07ba7aa6b4901ecf840ed994f00d21ebbf3bb04212e1e7b144886a6741bdcc85af06ac6ebf44e25c74951815a7b50a082c02eb4fa94c4842e4dabb8b58264666f91200997359546f849e3b1e74a29d6e1728b46cc1404e6444536a783a173b897e22be1f4bbbcb0304226b111d98d389c68b13d87727317bc4627887e9c57929b7ac5d7b8925f3b0b42d69dc7e7d9fafe77713d5a182b33de40c29482e531d15141141783eaa6ea74436dbcb94cdc8d24b66131846743aa094f29b27cb2cd9ebff617f8b42336b8d54b2d8279b1cc5929c9d894a905a474944a5faf3782c776a38ec315de620d99e84e36a4e78c0e05790c26f26b66793b134fa69cf1ee1b79312f05cae6325c21af4d60bfb8659a",
                ProjectItem = 540813734,
                Type = 1142531040,
                Unit = 512784079,
                UnitValue = 1190034434,
                Month = 1941803301,
                Year = 431534927,
                Ratio = 750550815,
                Amount = 450248940,
                Memo = 122287560,
                Invoice = 1444057376,
                Currency = 375764328,
                CurrencyAmount = 2114989769,
                ExpenseCategory = 1726892638,
                ExpenseNecessity = 135340701,
                Comment = "6dad3ba",
                Status = "96825",
                Approval = 1725864604,
                IsActive = true,
                DepartmentId = Guid.Parse("1b174f96-2c84-4616-8d5d-7469f680b366"),
                BudgetId = Guid.Parse("c175814f-d2bb-4939-a226-080e2f05ad6f"),

            };

            // Act
            var serviceResult = await _budgetDistributionsAppService.UpdateAsync(Guid.Parse("2adf600c-203a-4ffe-ad5a-748b09b557b0"), input);

            // Assert
            var result = await _budgetDistributionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CostCenter.ShouldBe("e31775cb0762420986bcfe08e0ffc7955c31d7b4443e4aa3ba");
            result.ExpenseType.ShouldBe("651282fb02a94e2689c06b866a5c033a9a392b4742294548b9bd5c39c7b07ba7aa6b4901ecf840ed994f00d21ebbf3bb04212e1e7b144886a6741bdcc85af06ac6ebf44e25c74951815a7b50a082c02eb4fa94c4842e4dabb8b58264666f91200997359546f849e3b1e74a29d6e1728b46cc1404e6444536a783a173b897e22be1f4bbbcb0304226b111d98d389c68b13d87727317bc4627887e9c57929b7ac5d7b8925f3b0b42d69dc7e7d9fafe77713d5a182b33de40c29482e531d15141141783eaa6ea74436dbcb94cdc8d24b66131846743aa094f29b27cb2cd9ebff617f8b42336b8d54b2d8279b1cc5929c9d894a905a474944a5faf3782c776a38ec315de620d99e84e36a4e78c0e05790c26f26b66793b134fa69cf1ee1b79312f05cae6325c21af4d60bfb8659a");
            result.ProjectItem.ShouldBe(540813734);
            result.Type.ShouldBe(1142531040);
            result.Unit.ShouldBe(512784079);
            result.UnitValue.ShouldBe(1190034434);
            result.Month.ShouldBe(1941803301);
            result.Year.ShouldBe(431534927);
            result.Ratio.ShouldBe(750550815);
            result.Amount.ShouldBe(450248940);
            result.Memo.ShouldBe(122287560);
            result.Invoice.ShouldBe(1444057376);
            result.Currency.ShouldBe(375764328);
            result.CurrencyAmount.ShouldBe(2114989769);
            result.ExpenseCategory.ShouldBe(1726892638);
            result.ExpenseNecessity.ShouldBe(135340701);
            result.Comment.ShouldBe("6dad3ba");
            result.Status.ShouldBe("96825");
            result.Approval.ShouldBe(1725864604);
            result.IsActive.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _budgetDistributionsAppService.DeleteAsync(Guid.Parse("2adf600c-203a-4ffe-ad5a-748b09b557b0"));

            // Assert
            var result = await _budgetDistributionRepository.FindAsync(c => c.Id == Guid.Parse("2adf600c-203a-4ffe-ad5a-748b09b557b0"));

            result.ShouldBeNull();
        }
    }
}