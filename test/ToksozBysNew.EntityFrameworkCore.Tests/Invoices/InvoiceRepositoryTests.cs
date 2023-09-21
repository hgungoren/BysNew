using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Invoices;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Invoices
{
    public class InvoiceRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceRepositoryTests()
        {
            _invoiceRepository = GetRequiredService<IInvoiceRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _invoiceRepository.GetListAsync(
                    invoiceSerialNo: "70c7dee2cab742a5b005dc0c71a5496231b20a7cc81f4f209408f470a9e40a54bd4f0e66adcc4160837849cdcf1e936",
                    notes: "7e23ca6bd9b1435d9dfd7c1f964e8edecd40620d23d2424bbcfd40d12c5c6ff07e86b3b2211d4c46a4cea39e7d"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d5f66ecf-55af-4878-b649-6d9bc1a5afc3"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _invoiceRepository.GetCountAsync(
                    invoiceSerialNo: "0b12a20e202e4b8682d620acbdb0f3f19a26895dc19947c79d49324e82b0634dd70c51323b664182a9bc4ef307b9a6914",
                    notes: "e8de158fbca0490c86e1b1c"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}