using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.Departments
{
    public interface IDepartmentRepository : IRepository<Department, Guid>
    {
        Task<DepartmentWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<DepartmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string departmentName = null,
            Guid? companyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Department>> GetListAsync(
                    string filterText = null,
                    string departmentName = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string departmentName = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default); 
    }
}