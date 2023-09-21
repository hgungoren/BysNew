using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Invoices
{
    public class InvoiceManager : DomainService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceManager(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<Invoice> CreateAsync(
        string invoiceSerialNo, DateTime invoiceDate, string notes, DateTime paymentDate, decimal amount, int? approvalStatus = null)
        {
            Check.NotNull(invoiceDate, nameof(invoiceDate));
            Check.NotNull(paymentDate, nameof(paymentDate));

            var invoice = new Invoice(
             GuidGenerator.Create(),
             invoiceSerialNo, invoiceDate, notes, paymentDate, amount, approvalStatus
             );

            return await _invoiceRepository.InsertAsync(invoice);
        }

        public async Task<Invoice> UpdateAsync(
            Guid id,
            string invoiceSerialNo, DateTime invoiceDate, string notes, DateTime paymentDate, decimal amount, int? approvalStatus = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(invoiceDate, nameof(invoiceDate));
            Check.NotNull(paymentDate, nameof(paymentDate));

            var invoice = await _invoiceRepository.GetAsync(id);

            invoice.InvoiceSerialNo = invoiceSerialNo;
            invoice.InvoiceDate = invoiceDate;
            invoice.Notes = notes;
            invoice.PaymentDate = paymentDate;
            invoice.Amount = amount;
            invoice.ApprovalStatus = approvalStatus;

            invoice.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _invoiceRepository.UpdateAsync(invoice);
        }

    }
}