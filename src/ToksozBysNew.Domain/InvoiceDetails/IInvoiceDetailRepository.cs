using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.InvoiceDetails
{
    public interface IInvoiceDetailRepository : IRepository<InvoiceDetail, Guid>
    {
        Task<InvoiceDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<InvoiceDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<InvoiceDetail>> GetListAsync(
                    string filterText = null,
                    int? invoiceDetailQuantityMin = null,
                    int? invoiceDetailQuantityMax = null,
                    decimal? invoiceDetailPriceMin = null,
                    decimal? invoiceDetailPriceMax = null,
                    string invoiceDetailNote = null,
                    DateTime? invoiceDetailDateMin = null,
                    DateTime? invoiceDetailDateMax = null,
                    string tax=null,
                    string taxName = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}