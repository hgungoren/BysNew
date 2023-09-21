using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Departments
{
    public class DepartmentManager : DomainService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentManager(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> CreateAsync(
        Guid? companyId, string departmentName)
        {
            var department = new Department(
             GuidGenerator.Create(),
             companyId, departmentName
             );

            return await _departmentRepository.InsertAsync(department);
        }

        public async Task<Department> UpdateAsync(
            Guid id,
            Guid? companyId, string departmentName, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _departmentRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var department = await AsyncExecuter.FirstOrDefaultAsync(query);

            department.CompanyId = companyId;
            department.DepartmentName = departmentName;

            department.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _departmentRepository.UpdateAsync(department);
        }

    }
}