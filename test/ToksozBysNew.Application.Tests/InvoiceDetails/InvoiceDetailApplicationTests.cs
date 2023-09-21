using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.InvoiceDetails
{
    public class InvoiceDetailsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IInvoiceDetailsAppService _invoiceDetailsAppService;
        private readonly IRepository<InvoiceDetail, Guid> _invoiceDetailRepository;

        public InvoiceDetailsAppServiceTests()
        {
            _invoiceDetailsAppService = GetRequiredService<IInvoiceDetailsAppService>();
            _invoiceDetailRepository = GetRequiredService<IRepository<InvoiceDetail, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _invoiceDetailsAppService.GetListAsync(new GetInvoiceDetailsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.InvoiceDetail.Id == Guid.Parse("ab6a2013-2db3-4150-ac97-8571940307f7")).ShouldBe(true);
            result.Items.Any(x => x.InvoiceDetail.Id == Guid.Parse("4faa83e7-3ed4-4e14-b6a5-fc5f487818f7")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _invoiceDetailsAppService.GetAsync(Guid.Parse("ab6a2013-2db3-4150-ac97-8571940307f7"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("ab6a2013-2db3-4150-ac97-8571940307f7"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new InvoiceDetailCreateDto
            {
                InvoiceDetailQuantity = 9503696,
                InvoiceDetailPrice = 34484902,
                InvoiceDetailNote = "84d4882d4d56487484946c742ce217ea39609c84067a4fb29e0fea1967739ab098f1599dd1114393a2b29f",
                InvoiceDetailDate = new DateTime(2003, 5, 19),
                Tax = "8"
            };

            // Act
            var serviceResult = await _invoiceDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _invoiceDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.InvoiceDetailQuantity.ShouldBe(9503696);
            result.InvoiceDetailPrice.ShouldBe(34484902);
            result.InvoiceDetailNote.ShouldBe("84d4882d4d56487484946c742ce217ea39609c84067a4fb29e0fea1967739ab098f1599dd1114393a2b29f");
            result.InvoiceDetailDate.ShouldBe(new DateTime(2003, 5, 19));
            result.Tax.ShouldBe("8");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new InvoiceDetailUpdateDto()
            {
                InvoiceDetailQuantity = 6279750,
                InvoiceDetailPrice = 26140708,
                InvoiceDetailNote = "31508fe0732b4724adad9e2d93965b05c54cb651557840978737025ca19bc173783f7d7523174c558b437",
                InvoiceDetailDate = new DateTime(2006, 4, 5),
                Tax = "8"
            };

            // Act
            var serviceResult = await _invoiceDetailsAppService.UpdateAsync(Guid.Parse("ab6a2013-2db3-4150-ac97-8571940307f7"), input);

            // Assert
            var result = await _invoiceDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.InvoiceDetailQuantity.ShouldBe(6279750);
            result.InvoiceDetailPrice.ShouldBe(26140708);
            result.InvoiceDetailNote.ShouldBe("31508fe0732b4724adad9e2d93965b05c54cb651557840978737025ca19bc173783f7d7523174c558b437");
            result.InvoiceDetailDate.ShouldBe(new DateTime(2006, 4, 5));
            result.Tax.ShouldBe("8");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _invoiceDetailsAppService.DeleteAsync(Guid.Parse("ab6a2013-2db3-4150-ac97-8571940307f7"));

            // Assert
            var result = await _invoiceDetailRepository.FindAsync(c => c.Id == Guid.Parse("ab6a2013-2db3-4150-ac97-8571940307f7"));

            result.ShouldBeNull();
        }
    }
}