using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.InvoiceDetails;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.InvoiceDetails
{
    public class InvoiceDetailRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;

        public InvoiceDetailRepositoryTests()
        {
            _invoiceDetailRepository = GetRequiredService<IInvoiceDetailRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _invoiceDetailRepository.GetListAsync(
                    invoiceDetailNote: "2cdd7798881648e99f2fcb7cb4ec4519ba7b6c8a7754432783be65ad8926d8de28f82fca"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("ab6a2013-2db3-4150-ac97-8571940307f7"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _invoiceDetailRepository.GetCountAsync(
                    invoiceDetailNote: "7be21c7a77c64457adb8e21c2e35a5e06a"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}