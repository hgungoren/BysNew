using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.CustomerTypes
{
    public interface ICustomerTypeRepository : IRepository<CustomerType, Guid>
    {
        Task<List<CustomerType>> GetListAsync(
            string filterText = null,
            string typeName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string typeName = null,
            CancellationToken cancellationToken = default);
    }
}