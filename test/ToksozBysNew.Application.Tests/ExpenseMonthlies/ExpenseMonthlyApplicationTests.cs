using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.ExpenseMonthlies
{
    public class ExpenseMonthliesAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IExpenseMonthliesAppService _expenseMonthliesAppService;
        private readonly IRepository<ExpenseMonthly, Guid> _expenseMonthlyRepository;

        public ExpenseMonthliesAppServiceTests()
        {
            _expenseMonthliesAppService = GetRequiredService<IExpenseMonthliesAppService>();
            _expenseMonthlyRepository = GetRequiredService<IRepository<ExpenseMonthly, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _expenseMonthliesAppService.GetListAsync(new GetExpenseMonthliesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("84e95c1c-ac60-457f-a834-1a9de1a1f9b2")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("cbef080d-7a8e-4f0a-a09d-e3e01705d8ab")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _expenseMonthliesAppService.GetAsync(Guid.Parse("84e95c1c-ac60-457f-a834-1a9de1a1f9b2"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("84e95c1c-ac60-457f-a834-1a9de1a1f9b2"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ExpenseMonthlyCreateDto
            {
                AccountId = "93badabb2bb64189ab2249ed14b61dcdb96aa0f525ba4e93bb739bdc2608d0227f04f4",
                AccountGroup = "f9f742cb52e448babf64e6d46bccc915354a86afbb84475babc717c",
                Account = "749966ede0804885bbf1cc90f8edce3f2615eeecb2b748fc8a98a9225c38e43fe9d47fed00f3472394c03dfbbf4ecb9efd",
                Department = "459f3f607f4f46a49135300f1e2b3afe56e2c6613d584be9a486bf119c120672a62a3b792",
                ExpenseType = "b80d45bdcc4f48e2aa0d831dcbf893799038fc1a4fdd422aa0bf",
                Product = "bede9bf3a8b1497a854cde59779460fd",
                Proje = "f9942937ce9c4e0b9fee8300e0d6ee1c30cfab81",
                Comment = "994e73ef84a54a6182e45a90504bad54f040f5d7e979440ea26afede864a1370df9",
                Month = "465a67a513b14b1f83e7723b5af819057bb9490b57eb4da",
                Year = 1776105710,
                Unit = 961284417,
                UnitValue = 1492920233,
                Amount = 290598683,
                Memo = 176857577,
                Invoice = "a32ce64ce7524073bf18280fd9fa52fe24ca973bf7bb4a09be2c264ada57ad25e778933b053b4d2dad27480",
                Remain = 1607672991
            };

            // Act
            var serviceResult = await _expenseMonthliesAppService.CreateAsync(input);

            // Assert
            var result = await _expenseMonthlyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AccountId.ShouldBe("93badabb2bb64189ab2249ed14b61dcdb96aa0f525ba4e93bb739bdc2608d0227f04f4");
            result.AccountGroup.ShouldBe("f9f742cb52e448babf64e6d46bccc915354a86afbb84475babc717c");
            result.Account.ShouldBe("749966ede0804885bbf1cc90f8edce3f2615eeecb2b748fc8a98a9225c38e43fe9d47fed00f3472394c03dfbbf4ecb9efd");
            result.Department.ShouldBe("459f3f607f4f46a49135300f1e2b3afe56e2c6613d584be9a486bf119c120672a62a3b792");
            result.ExpenseType.ShouldBe("b80d45bdcc4f48e2aa0d831dcbf893799038fc1a4fdd422aa0bf");
            result.Product.ShouldBe("bede9bf3a8b1497a854cde59779460fd");
            result.Proje.ShouldBe("f9942937ce9c4e0b9fee8300e0d6ee1c30cfab81");
            result.Comment.ShouldBe("994e73ef84a54a6182e45a90504bad54f040f5d7e979440ea26afede864a1370df9");
            result.Month.ShouldBe("465a67a513b14b1f83e7723b5af819057bb9490b57eb4da");
            result.Year.ShouldBe(1776105710);
            result.Unit.ShouldBe(961284417);
            result.UnitValue.ShouldBe(1492920233);
            result.Amount.ShouldBe(290598683);
            result.Memo.ShouldBe(176857577);
            result.Invoice.ShouldBe("a32ce64ce7524073bf18280fd9fa52fe24ca973bf7bb4a09be2c264ada57ad25e778933b053b4d2dad27480");
            result.Remain.ShouldBe(1607672991);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ExpenseMonthlyUpdateDto()
            {
                AccountId = "f76cf3461904401884dcf6d488d7622966fbb6ee1f594037",
                AccountGroup = "39bfb8cc06ce45648a5a349c1124eafc6114afcc377c436983630f90fab3bb3ae347b69b978f4d12ae3af7b4b83f08e73",
                Account = "3adb07dbde76",
                Department = "da40df58f9ba42e985ec06b617c42222eef894445bc2487f8b54ca7cfcaa16a8a",
                ExpenseType = "c75bf1b224fd49faa",
                Product = "f8f0c5cb649b48b19e289ae4f3e82b4db478d41add9b491593",
                Proje = "1c9d3374093d4c32abea23a5aec7e41421ea889d9e49451d81d099fc14109ab602fb733bbee6415a8ecc920a81",
                Comment = "70cb1b6bec1f4b6784fcc6ad048375e242a9c699bf82437ab2e94f80f86647d94354a08f",
                Month = "1e7aa146f33043ad977624da",
                Year = 1608127558,
                Unit = 618660121,
                UnitValue = 561525175,
                Amount = 1743447647,
                Memo = 1231754125,
                Invoice = "5fadc70986ea44499849c35a55c6a8a92e28a0d3cddf455fa39fb0e30860c6ff90762991c484474c84c2f95a133d9e",
                Remain = 1318489496
            };

            // Act
            var serviceResult = await _expenseMonthliesAppService.UpdateAsync(Guid.Parse("84e95c1c-ac60-457f-a834-1a9de1a1f9b2"), input);

            // Assert
            var result = await _expenseMonthlyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AccountId.ShouldBe("f76cf3461904401884dcf6d488d7622966fbb6ee1f594037");
            result.AccountGroup.ShouldBe("39bfb8cc06ce45648a5a349c1124eafc6114afcc377c436983630f90fab3bb3ae347b69b978f4d12ae3af7b4b83f08e73");
            result.Account.ShouldBe("3adb07dbde76");
            result.Department.ShouldBe("da40df58f9ba42e985ec06b617c42222eef894445bc2487f8b54ca7cfcaa16a8a");
            result.ExpenseType.ShouldBe("c75bf1b224fd49faa");
            result.Product.ShouldBe("f8f0c5cb649b48b19e289ae4f3e82b4db478d41add9b491593");
            result.Proje.ShouldBe("1c9d3374093d4c32abea23a5aec7e41421ea889d9e49451d81d099fc14109ab602fb733bbee6415a8ecc920a81");
            result.Comment.ShouldBe("70cb1b6bec1f4b6784fcc6ad048375e242a9c699bf82437ab2e94f80f86647d94354a08f");
            result.Month.ShouldBe("1e7aa146f33043ad977624da");
            result.Year.ShouldBe(1608127558);
            result.Unit.ShouldBe(618660121);
            result.UnitValue.ShouldBe(561525175);
            result.Amount.ShouldBe(1743447647);
            result.Memo.ShouldBe(1231754125);
            result.Invoice.ShouldBe("5fadc70986ea44499849c35a55c6a8a92e28a0d3cddf455fa39fb0e30860c6ff90762991c484474c84c2f95a133d9e");
            result.Remain.ShouldBe(1318489496);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _expenseMonthliesAppService.DeleteAsync(Guid.Parse("84e95c1c-ac60-457f-a834-1a9de1a1f9b2"));

            // Assert
            var result = await _expenseMonthlyRepository.FindAsync(c => c.Id == Guid.Parse("84e95c1c-ac60-457f-a834-1a9de1a1f9b2"));

            result.ShouldBeNull();
        }
    }
}