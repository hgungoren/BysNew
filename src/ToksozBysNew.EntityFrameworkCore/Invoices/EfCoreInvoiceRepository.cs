using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using ToksozBysNew.EntityFrameworkCore;

namespace ToksozBysNew.Invoices
{
    public class EfCoreInvoiceRepository : EfCoreRepository<ToksozBysNewDbContext, Invoice, Guid>, IInvoiceRepository
    {
        public EfCoreInvoiceRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Invoice>> GetListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, invoiceSerialNo, invoiceDateMin, invoiceDateMax, notes, paymentDateMin, paymentDateMax, amountMin, amountMax, approvalStatusMin, approvalStatusMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? InvoiceConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, invoiceSerialNo, invoiceDateMin, invoiceDateMax, notes, paymentDateMin, paymentDateMax, amountMin, amountMax, approvalStatusMin, approvalStatusMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Invoice> ApplyFilter(
            IQueryable<Invoice> query,
            string filterText,
            string invoiceSerialNo = null,
            DateTime? invoiceDateMin = null,
            DateTime? invoiceDateMax = null,
            string notes = null,
            DateTime? paymentDateMin = null,
            DateTime? paymentDateMax = null,
            decimal? amountMin = null,
            decimal? amountMax = null,
            int? approvalStatusMin = null,
            int? approvalStatusMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.InvoiceSerialNo.Contains(filterText) || e.Notes.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(invoiceSerialNo), e => e.InvoiceSerialNo.Contains(invoiceSerialNo))
                    .WhereIf(invoiceDateMin.HasValue, e => e.InvoiceDate >= invoiceDateMin.Value)
                    .WhereIf(invoiceDateMax.HasValue, e => e.InvoiceDate <= invoiceDateMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(notes), e => e.Notes.Contains(notes))
                    .WhereIf(paymentDateMin.HasValue, e => e.PaymentDate >= paymentDateMin.Value)
                    .WhereIf(paymentDateMax.HasValue, e => e.PaymentDate <= paymentDateMax.Value)
                    .WhereIf(amountMin.HasValue, e => e.Amount >= amountMin.Value)
                    .WhereIf(amountMax.HasValue, e => e.Amount <= amountMax.Value)
                    .WhereIf(approvalStatusMin.HasValue, e => e.ApprovalStatus >= approvalStatusMin.Value)
                    .WhereIf(approvalStatusMax.HasValue, e => e.ApprovalStatus <= approvalStatusMax.Value);
        }
    }
}