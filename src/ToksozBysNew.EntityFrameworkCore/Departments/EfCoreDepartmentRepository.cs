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
using Polly;

namespace ToksozBysNew.Departments
{
    public class EfCoreDepartmentRepository : EfCoreRepository<ToksozBysNewDbContext, Department, Guid>, IDepartmentRepository
    {
        public EfCoreDepartmentRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<DepartmentWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(department => new DepartmentWithNavigationProperties
                {
                    Department = department,
                    Company = dbContext.Companies.FirstOrDefault(c => c.Id == department.CompanyId)
                }).FirstOrDefault();
        }

        public async Task<List<DepartmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string departmentName = null,
            Guid? companyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, departmentName, companyId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DepartmentConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<DepartmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from department in (await GetDbSetAsync())
                   join company in (await GetDbContextAsync()).Companies on department.CompanyId equals company.Id into companies
                   from company in companies.DefaultIfEmpty()

                   select new DepartmentWithNavigationProperties
                   {
                       Department = department,
                       Company = company
                   };
        }

        protected virtual IQueryable<DepartmentWithNavigationProperties> ApplyFilter(
            IQueryable<DepartmentWithNavigationProperties> query,
            string filterText,
            string departmentName = null,
            Guid? companyId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Department.DepartmentName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(departmentName), e => e.Department.DepartmentName.Contains(departmentName))
                    .WhereIf(companyId != null && companyId != Guid.Empty, e => e.Company != null && e.Company.Id == companyId);
        }

        public async Task<List<Department>> GetListAsync(
            string filterText = null,
            string departmentName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, departmentName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DepartmentConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string departmentName = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, departmentName, companyId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Department> ApplyFilter(
            IQueryable<Department> query,
            string filterText,
            string departmentName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.DepartmentName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(departmentName), e => e.DepartmentName.Contains(departmentName));
        } 
    }
}