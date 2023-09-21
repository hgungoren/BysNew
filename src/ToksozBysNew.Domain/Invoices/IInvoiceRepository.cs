using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Invoices
{
    public interface IInvoiceRepository : IRepository<Invoice, Guid>
    {
        Task<List<Invoice>> GetListAsync(
            string filterText = null,
            string invoiceSerialNo = null,
            DateTime? invoiceDateMin = null,
            DateTime? invoiceDateMax = null,
            string notes = null,
            DateTime? paymentDateMin = null,
            DateTime? paymentDateMax = null,
            decimal? amountMin = null,
            decimal? amountMax = null,
            int? approvalStatusMin = null,
            int? approvalStatusMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string invoiceSerialNo = null,
            DateTime? invoiceDateMin = null,
            DateTime? invoiceDateMax = null,
            string notes = null,
            DateTime? paymentDateMin = null,
            DateTime? paymentDateMax = null,
            decimal? amountMin = null,
            decimal? amountMax = null,
            int? approvalStatusMin = null,
            int? approvalStatusMax = null,
            CancellationToken cancellationToken = default);
    }
}