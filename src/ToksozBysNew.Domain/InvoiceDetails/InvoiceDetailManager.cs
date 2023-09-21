using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.InvoiceDetails
{
    public class InvoiceDetailManager : DomainService
    {
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;

        public InvoiceDetailManager(IInvoiceDetailRepository invoiceDetailRepository)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
        }

        public async Task<InvoiceDetail> CreateAsync(
        Guid? invoiceId, Guid taxListId, int invoiceDetailQuantity, decimal invoiceDetailPrice, string invoiceDetailNote, DateTime invoiceDetailDate, string taxName, string tax = null)
        {
            Check.NotNull(taxListId, nameof(taxListId));
            Check.Range(invoiceDetailQuantity, nameof(invoiceDetailQuantity), InvoiceDetailConsts.InvoiceDetailQuantityMinLength, InvoiceDetailConsts.InvoiceDetailQuantityMaxLength);
            Check.Range(invoiceDetailPrice, nameof(invoiceDetailPrice), InvoiceDetailConsts.InvoiceDetailPriceMinLength, InvoiceDetailConsts.InvoiceDetailPriceMaxLength);
            Check.NotNullOrWhiteSpace(invoiceDetailNote, nameof(invoiceDetailNote));
            Check.NotNull(invoiceDetailDate, nameof(invoiceDetailDate));
            Check.NotNull(tax, nameof(tax));

            var invoiceDetail = new InvoiceDetail(
             GuidGenerator.Create(),
             invoiceId, taxListId, invoiceDetailQuantity, invoiceDetailPrice, invoiceDetailNote, invoiceDetailDate, taxName, tax
             );

            return await _invoiceDetailRepository.InsertAsync(invoiceDetail);
        }

        public async Task<InvoiceDetail> UpdateAsync(
            Guid id,
            Guid? invoiceId, Guid taxListId, int invoiceDetailQuantity, decimal invoiceDetailPrice, string invoiceDetailNote, DateTime invoiceDetailDate, string taxName, string tax = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(taxListId, nameof(taxListId));
            Check.Range(invoiceDetailQuantity, nameof(invoiceDetailQuantity), InvoiceDetailConsts.InvoiceDetailQuantityMinLength, InvoiceDetailConsts.InvoiceDetailQuantityMaxLength);
            Check.Range(invoiceDetailPrice, nameof(invoiceDetailPrice), InvoiceDetailConsts.InvoiceDetailPriceMinLength, InvoiceDetailConsts.InvoiceDetailPriceMaxLength);
            Check.NotNullOrWhiteSpace(invoiceDetailNote, nameof(invoiceDetailNote));
            Check.NotNull(invoiceDetailDate, nameof(invoiceDetailDate));
            Check.NotNull(tax, nameof(tax));

            var invoiceDetail = await _invoiceDetailRepository.GetAsync(id);

            invoiceDetail.InvoiceId = invoiceId;
            invoiceDetail.TaxListId = taxListId;
            invoiceDetail.InvoiceDetailQuantity = invoiceDetailQuantity;
            invoiceDetail.InvoiceDetailPrice = invoiceDetailPrice;
            invoiceDetail.InvoiceDetailNote = invoiceDetailNote;
            invoiceDetail.InvoiceDetailDate = invoiceDetailDate;
            invoiceDetail.TaxName = taxName;
            invoiceDetail.Tax = tax;

            invoiceDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _invoiceDetailRepository.UpdateAsync(invoiceDetail);
        }

    }
}