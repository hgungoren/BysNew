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

namespace ToksozBysNew.InvoiceDetails
{
    public class EfCoreInvoiceDetailRepository : EfCoreRepository<ToksozBysNewDbContext, InvoiceDetail, Guid>, IInvoiceDetailRepository
    {
        public EfCoreInvoiceDetailRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<InvoiceDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(invoiceDetail => new InvoiceDetailWithNavigationProperties
                {
                    InvoiceDetail = invoiceDetail,
                    Invoice = dbContext.Invoices.FirstOrDefault(c => c.Id == invoiceDetail.InvoiceId),
                    TaxList = dbContext.TaxLists.FirstOrDefault(c => c.Id == invoiceDetail.TaxListId)
                }).FirstOrDefault();
        }

        public async Task<List<InvoiceDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? invoiceDetailQuantityMin = null,
            int? invoiceDetailQuantityMax = null,
            decimal? invoiceDetailPriceMin = null,
            decimal? invoiceDetailPriceMax = null,
            string invoiceDetailNote = null,
            DateTime? invoiceDetailDateMin = null,
            DateTime? invoiceDetailDateMax = null,
            string tax = null, 
            string taxName = null,
            Guid? invoiceId = null,
            Guid? taxListId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, invoiceDetailQuantityMin, invoiceDetailQuantityMax, invoiceDetailPriceMin, invoiceDetailPriceMax, invoiceDetailNote, invoiceDetailDateMin, invoiceDetailDateMax,tax, taxName, invoiceId, taxListId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? InvoiceDetailConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<InvoiceDetailWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from invoiceDetail in (await GetDbSetAsync())
                   join invoice in (await GetDbContextAsync()).Invoices on invoiceDetail.InvoiceId equals invoice.Id into invoices
                   from invoice in invoices.DefaultIfEmpty()
                   join taxList in (await GetDbContextAsync()).TaxLists on invoiceDetail.TaxListId equals taxList.Id into taxLists
                   from taxList in taxLists.DefaultIfEmpty()

                   select new InvoiceDetailWithNavigationProperties
                   {
                       InvoiceDetail = invoiceDetail,
                       Invoice = invoice,
                       TaxList = taxList
                   };
        }

        protected virtual IQueryable<InvoiceDetailWithNavigationProperties> ApplyFilter(
            IQueryable<InvoiceDetailWithNavigationProperties> query,
            string filterText,
            int? invoiceDetailQuantityMin = null,
            int? invoiceDetailQuantityMax = null,
            decimal? invoiceDetailPriceMin = null,
            decimal? invoiceDetailPriceMax = null,
            string invoiceDetailNote = null,
            DateTime? invoiceDetailDateMin = null,
            DateTime? invoiceDetailDateMax = null,
            string tax = null, 
            string taxName = null,
            Guid? invoiceId = null,
            Guid? taxListId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.InvoiceDetail.InvoiceDetailNote.Contains(filterText) || e.InvoiceDetail.TaxName.Contains(filterText))
                    .WhereIf(invoiceDetailQuantityMin.HasValue, e => e.InvoiceDetail.InvoiceDetailQuantity >= invoiceDetailQuantityMin.Value)
                    .WhereIf(invoiceDetailQuantityMax.HasValue, e => e.InvoiceDetail.InvoiceDetailQuantity <= invoiceDetailQuantityMax.Value)
                    .WhereIf(invoiceDetailPriceMin.HasValue, e => e.InvoiceDetail.InvoiceDetailPrice >= invoiceDetailPriceMin.Value)
                    .WhereIf(invoiceDetailPriceMax.HasValue, e => e.InvoiceDetail.InvoiceDetailPrice <= invoiceDetailPriceMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(invoiceDetailNote), e => e.InvoiceDetail.InvoiceDetailNote.Contains(invoiceDetailNote))
                    .WhereIf(invoiceDetailDateMin.HasValue, e => e.InvoiceDetail.InvoiceDetailDate >= invoiceDetailDateMin.Value)
                    .WhereIf(invoiceDetailDateMax.HasValue, e => e.InvoiceDetail.InvoiceDetailDate <= invoiceDetailDateMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(tax), e => e.InvoiceDetail.Tax.Contains(tax))
                    .WhereIf(!string.IsNullOrWhiteSpace(taxName), e => e.InvoiceDetail.TaxName.Contains(taxName))
                    .WhereIf(invoiceId != null && invoiceId != Guid.Empty, e => e.Invoice != null && e.Invoice.Id == invoiceId)
                    .WhereIf(taxListId != null && taxListId != Guid.Empty, e => e.TaxList != null && e.TaxList.Id == taxListId);
        }

        public async Task<List<InvoiceDetail>> GetListAsync(
            string filterText = null,
            int? invoiceDetailQuantityMin = null,
            int? invoiceDetailQuantityMax = null,
            decimal? invoiceDetailPriceMin = null,
            decimal? invoiceDetailPriceMax = null,
            string invoiceDetailNote = null,
            DateTime? invoiceDetailDateMin = null,
            DateTime? invoiceDetailDateMax = null,
            string tax = null,
            string taxName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, invoiceDetailQuantityMin, invoiceDetailQuantityMax, invoiceDetailPriceMin, invoiceDetailPriceMax, invoiceDetailNote, invoiceDetailDateMin, invoiceDetailDateMax, tax, taxName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? InvoiceDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? invoiceDetailQuantityMin = null,
            int? invoiceDetailQuantityMax = null,
            decimal? invoiceDetailPriceMin = null,
            decimal? invoiceDetailPriceMax = null,
            string invoiceDetailNote = null,
            DateTime? invoiceDetailDateMin = null,
            DateTime? invoiceDetailDateMax = null,
            string tax = null, 
            string taxName = null,
            Guid? invoiceId = null,
            Guid? taxListId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, invoiceDetailQuantityMin, invoiceDetailQuantityMax, invoiceDetailPriceMin, invoiceDetailPriceMax, invoiceDetailNote, invoiceDetailDateMin, invoiceDetailDateMax, tax, taxName, invoiceId, taxListId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<InvoiceDetail> ApplyFilter(
            IQueryable<InvoiceDetail> query,
            string filterText,
            int? invoiceDetailQuantityMin = null,
            int? invoiceDetailQuantityMax = null,
            decimal? invoiceDetailPriceMin = null,
            decimal? invoiceDetailPriceMax = null,
            string invoiceDetailNote = null,
            DateTime? invoiceDetailDateMin = null,
            DateTime? invoiceDetailDateMax = null,
             string tax = null,
            string taxName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.InvoiceDetailNote.Contains(filterText) || e.TaxName.Contains(filterText))
                    .WhereIf(invoiceDetailQuantityMin.HasValue, e => e.InvoiceDetailQuantity >= invoiceDetailQuantityMin.Value)
                    .WhereIf(invoiceDetailQuantityMax.HasValue, e => e.InvoiceDetailQuantity <= invoiceDetailQuantityMax.Value)
                    .WhereIf(invoiceDetailPriceMin.HasValue, e => e.InvoiceDetailPrice >= invoiceDetailPriceMin.Value)
                    .WhereIf(invoiceDetailPriceMax.HasValue, e => e.InvoiceDetailPrice <= invoiceDetailPriceMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(invoiceDetailNote), e => e.InvoiceDetailNote.Contains(invoiceDetailNote))
                    .WhereIf(invoiceDetailDateMin.HasValue, e => e.InvoiceDetailDate >= invoiceDetailDateMin.Value)
                    .WhereIf(invoiceDetailDateMax.HasValue, e => e.InvoiceDetailDate <= invoiceDetailDateMax.Value)
                   .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Tax.Contains(tax))
                    .WhereIf(!string.IsNullOrWhiteSpace(taxName), e => e.TaxName.Contains(taxName));
        }
    }
}