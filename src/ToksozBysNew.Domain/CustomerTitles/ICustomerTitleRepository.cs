using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.CustomerTitles
{
    public interface ICustomerTitleRepository : IRepository<CustomerTitle, Guid>
    {
        Task<List<CustomerTitle>> GetListAsync(
            string filterText = null,
            string titleName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string titleName = null,
            CancellationToken cancellationToken = default);
    }
}