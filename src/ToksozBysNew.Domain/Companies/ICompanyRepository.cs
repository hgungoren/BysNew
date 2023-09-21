using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Companies
{
    public interface ICompanyRepository : IRepository<Company, Guid>
    {
        Task<List<Company>> GetListAsync(
            string filterText = null,
            string companyName = null,
            bool? isActive = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string companyName = null,
            bool? isActive = null,
            CancellationToken cancellationToken = default);
    }
}