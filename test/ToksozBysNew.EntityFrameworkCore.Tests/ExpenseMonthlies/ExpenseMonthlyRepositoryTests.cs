using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.ExpenseMonthlies;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.ExpenseMonthlies
{
    public class ExpenseMonthlyRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IExpenseMonthlyRepository _expenseMonthlyRepository;

        public ExpenseMonthlyRepositoryTests()
        {
            _expenseMonthlyRepository = GetRequiredService<IExpenseMonthlyRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _expenseMonthlyRepository.GetListAsync(
                    accountId: "37bc9b784e5049b4a3f2333f5877491f6e3bd7a6956b4c12b9ffa105bc4844f740ed12b863b94356a6e6507f4ff36",
                    accountGroup: "a27330d850594aa3a454c710d0c683bec04af13326064ba0b01e5327f32dbfa9b4b61224b4b34283a6ca0113c86caf53a1",
                    account: "f071e4566e95429bbcb7a6c427a02ee4c1296a768b4742e5a2fa96118ad2f526815c1",
                    department: "b03209542f29",
                    expenseType: "9793b1eb0a3d4c2881d243ed85ac37f4d0",
                    product: "b94b1ee75d8e46e48bfbdb28f5e45414bc26b24dfcaa44c8815b2d3ca4fbb",
                    proje: "5c79cbf3144e4a89a181b488039b0cf2ef178ae13cb74915b6c3f8c",
                    comment: "fb891358e01e4921ab85a50c3b26c35ff1645a41277346f89f081a4d4517454557b00dd3f",
                    month: "43cbc7cc5503491eaf132d7416952fb5311a34138bc94049a6b3a086a1d8e188f5e439b5",
                    invoice: "f6ba6603d1db456e8d0503274488f0a5ba011eb4c1ab4fe7874ea7616902c2"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("84e95c1c-ac60-457f-a834-1a9de1a1f9b2"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _expenseMonthlyRepository.GetCountAsync(
                    accountId: "efefd60674f047edad1cc3630",
                    accountGroup: "80ae3def6d9b491",
                    account: "9d87f1382c364368abebe4e1a1ec745211dd16931513423ca06ec4232888ca9",
                    department: "10fd9d9fa46e4759ac03e70fb883bd1a2c930da13a0e406d90c409e0ab3ec6",
                    expenseType: "6860862ffb6f48beb9ad67758cab7c43561444412cf74",
                    product: "1efd2c1b32aa44c78179ecb321520d2cffebe189af38401dbdc680be82149c9ff7af12d3718449b9b0397",
                    proje: "c58d4c4fdcf54da3abfc",
                    comment: "1e82d4a59cca4a66bf3b619ad3ad5175bb10070ddf864ddf86b5ad699fc74b248654ced",
                    month: "2217027079a6",
                    invoice: "3a76cdf441774364a72572280d9c1da1328918de2bc14ade8f084a9b8964f0224abd34"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}