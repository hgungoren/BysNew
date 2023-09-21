using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Invoices
{
    public class InvoicesAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IInvoicesAppService _invoicesAppService;
        private readonly IRepository<Invoice, Guid> _invoiceRepository;

        public InvoicesAppServiceTests()
        {
            _invoicesAppService = GetRequiredService<IInvoicesAppService>();
            _invoiceRepository = GetRequiredService<IRepository<Invoice, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _invoicesAppService.GetListAsync(new GetInvoicesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("d5f66ecf-55af-4878-b649-6d9bc1a5afc3")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("50ada226-89e7-4026-9edb-f3d6e800bc74")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _invoicesAppService.GetAsync(Guid.Parse("d5f66ecf-55af-4878-b649-6d9bc1a5afc3"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d5f66ecf-55af-4878-b649-6d9bc1a5afc3"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new InvoiceCreateDto
            {
                InvoiceSerialNo = "3b81515",
                InvoiceDate = new DateTime(2017, 11, 2),
                Notes = "19044748a2474e948d3f2d94d99bb6d1f3ceaa986a4f4c96ab2a26f1067039ae1aebdaf5dc324efbbbc1c9c3",
                PaymentDate = new DateTime(2009, 9, 11),
                Amount = 1559159879,
                ApprovalStatus = 1032330237
            };

            // Act
            var serviceResult = await _invoicesAppService.CreateAsync(input);

            // Assert
            var result = await _invoiceRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.InvoiceSerialNo.ShouldBe("3b81515");
            result.InvoiceDate.ShouldBe(new DateTime(2017, 11, 2));
            result.Notes.ShouldBe("19044748a2474e948d3f2d94d99bb6d1f3ceaa986a4f4c96ab2a26f1067039ae1aebdaf5dc324efbbbc1c9c3");
            result.PaymentDate.ShouldBe(new DateTime(2009, 9, 11));
            result.Amount.ShouldBe(1559159879);
            result.ApprovalStatus.ShouldBe(1032330237);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new InvoiceUpdateDto()
            {
                InvoiceSerialNo = "c48f371f7e61468ea",
                InvoiceDate = new DateTime(2015, 5, 15),
                Notes = "dc54aef5e811",
                PaymentDate = new DateTime(2015, 3, 12),
                Amount = 899305455,
                ApprovalStatus = 1350289378
            };

            // Act
            var serviceResult = await _invoicesAppService.UpdateAsync(Guid.Parse("d5f66ecf-55af-4878-b649-6d9bc1a5afc3"), input);

            // Assert
            var result = await _invoiceRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.InvoiceSerialNo.ShouldBe("c48f371f7e61468ea");
            result.InvoiceDate.ShouldBe(new DateTime(2015, 5, 15));
            result.Notes.ShouldBe("dc54aef5e811");
            result.PaymentDate.ShouldBe(new DateTime(2015, 3, 12));
            result.Amount.ShouldBe(899305455);
            result.ApprovalStatus.ShouldBe(1350289378);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _invoicesAppService.DeleteAsync(Guid.Parse("d5f66ecf-55af-4878-b649-6d9bc1a5afc3"));

            // Assert
            var result = await _invoiceRepository.FindAsync(c => c.Id == Guid.Parse("d5f66ecf-55af-4878-b649-6d9bc1a5afc3"));

            result.ShouldBeNull();
        }
    }
}