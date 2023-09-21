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

namespace ToksozBysNew.Masters
{
    public class EfCoreMasterRepository : EfCoreRepository<ToksozBysNewDbContext, Master, Guid>, IMasterRepository
    {
        public EfCoreMasterRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<MasterWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(master => new MasterWithNavigationProperties
                {
                    Master = master,
                    Company = dbContext.Companies.FirstOrDefault(c => c.Id == master.CompanyId)
                }).FirstOrDefault();
        }

        public async Task<List<MasterWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string invoiceSerialNo = null,
            decimal? invoicePriceMin = null,
            decimal? invoicePriceMax = null,
            DateTime? invoiceDateMin = null,
            DateTime? invoiceDateMax = null,
            string invoiceNote = null,
            Guid? companyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, invoiceSerialNo, invoicePriceMin, invoicePriceMax, invoiceDateMin, invoiceDateMax, invoiceNote, companyId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? MasterConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<MasterWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from master in (await GetDbSetAsync())
                   join company in (await GetDbContextAsync()).Companies on master.CompanyId equals company.Id into companies
                   from company in companies.DefaultIfEmpty()

                   select new MasterWithNavigationProperties
                   {
                       Master = master,
                       Company = company
                   };
        }

        protected virtual IQueryable<MasterWithNavigationProperties> ApplyFilter(
            IQueryable<MasterWithNavigationProperties> query,
            string filterText,
            string invoiceSerialNo = null,
            decimal? invoicePriceMin = null,
            decimal? invoicePriceMax = null,
            DateTime? invoiceDateMin = null,
            DateTime? invoiceDateMax = null,
            string invoiceNote = null,
            Guid? companyId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Master.InvoiceSerialNo.Contains(filterText) || e.Master.InvoiceNote.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(invoiceSerialNo), e => e.Master.InvoiceSerialNo.Contains(invoiceSerialNo))
                    .WhereIf(invoicePriceMin.HasValue, e => e.Master.InvoicePrice >= invoicePriceMin.Value)
                    .WhereIf(invoicePriceMax.HasValue, e => e.Master.InvoicePrice <= invoicePriceMax.Value)
                    .WhereIf(invoiceDateMin.HasValue, e => e.Master.InvoiceDate >= invoiceDateMin.Value)
                    .WhereIf(invoiceDateMax.HasValue, e => e.Master.InvoiceDate <= invoiceDateMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(invoiceNote), e => e.Master.InvoiceNote.Contains(invoiceNote))
                    .WhereIf(companyId != null && companyId != Guid.Empty, e => e.Company != null && e.Company.Id == companyId);
        }

        public async Task<List<Master>> GetListAsync(
            string filterText = null,
            string invoiceSerialNo = null,
            decimal? invoicePriceMin = null,
            decimal? invoicePriceMax = null,
            DateTime? invoiceDateMin = null,
            DateTime? invoiceDateMax = null,
            string invoiceNote = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, invoiceSerialNo, invoicePriceMin, invoicePriceMax, invoiceDateMin, invoiceDateMax, invoiceNote);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? MasterConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string invoiceSerialNo = null,
            decimal? invoicePriceMin = null,
            decimal? invoicePriceMax = null,
            DateTime? invoiceDateMin = null,
            DateTime? invoiceDateMax = null,
            string invoiceNote = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, invoiceSerialNo, invoicePriceMin, invoicePriceMax, invoiceDateMin, invoiceDateMax, invoiceNote, companyId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Master> ApplyFilter(
            IQueryable<Master> query,
            string filterText,
            string invoiceSerialNo = null,
            decimal? invoicePriceMin = null,
            decimal? invoicePriceMax = null,
            DateTime? invoiceDateMin = null,
            DateTime? invoiceDateMax = null,
            string invoiceNote = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.InvoiceSerialNo.Contains(filterText) || e.InvoiceNote.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(invoiceSerialNo), e => e.InvoiceSerialNo.Contains(invoiceSerialNo))
                    .WhereIf(invoicePriceMin.HasValue, e => e.InvoicePrice >= invoicePriceMin.Value)
                    .WhereIf(invoicePriceMax.HasValue, e => e.InvoicePrice <= invoicePriceMax.Value)
                    .WhereIf(invoiceDateMin.HasValue, e => e.InvoiceDate >= invoiceDateMin.Value)
                    .WhereIf(invoiceDateMax.HasValue, e => e.InvoiceDate <= invoiceDateMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(invoiceNote), e => e.InvoiceNote.Contains(invoiceNote));
        }
    }
}