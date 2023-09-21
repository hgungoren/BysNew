using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Masters
{
    public interface IMasterRepository : IRepository<Master, Guid>
    {
        Task<MasterWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<MasterWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<Master>> GetListAsync(
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
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string invoiceSerialNo = null,
            decimal? invoicePriceMin = null,
            decimal? invoicePriceMax = null,
            DateTime? invoiceDateMin = null,
            DateTime? invoiceDateMax = null,
            string invoiceNote = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default);
    }
}