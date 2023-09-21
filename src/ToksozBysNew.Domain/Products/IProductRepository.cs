using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Products
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<List<Product>> GetListAsync(
            string filterText = null,
            string productName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string productName = null,
            CancellationToken cancellationToken = default);
    }
}