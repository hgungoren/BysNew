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

namespace ToksozBysNew.Clinics
{
    public class EfCoreClinicRepository : EfCoreRepository<ToksozBysNewDbContext, Clinic, Guid>, IClinicRepository
    {
        public EfCoreClinicRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ClinicWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(clinic => new ClinicWithNavigationProperties
                {
                    Clinic = clinic,
                    Unit = dbContext.Units.FirstOrDefault(c => c.Id == clinic.UnitId),
                    Spec = dbContext.Specs.FirstOrDefault(c => c.Id == clinic.SpecId)
                }).FirstOrDefault();
        }

        public async Task<List<ClinicWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string clinicName = null,
            Guid? unitId = null,
            Guid? specId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, clinicName, unitId, specId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ClinicConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ClinicWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from clinic in (await GetDbSetAsync())
                   join unit in (await GetDbContextAsync()).Units on clinic.UnitId equals unit.Id into units
                   from unit in units.DefaultIfEmpty()
                   join spec in (await GetDbContextAsync()).Specs on clinic.SpecId equals spec.Id into specs
                   from spec in specs.DefaultIfEmpty()

                   select new ClinicWithNavigationProperties
                   {
                       Clinic = clinic,
                       Unit = unit,
                       Spec = spec
                   };
        }

        protected virtual IQueryable<ClinicWithNavigationProperties> ApplyFilter(
            IQueryable<ClinicWithNavigationProperties> query,
            string filterText,
            string clinicName = null,
            Guid? unitId = null,
            Guid? specId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Clinic.ClinicName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(clinicName), e => e.Clinic.ClinicName.Contains(clinicName))
                    .WhereIf(unitId != null && unitId != Guid.Empty, e => e.Unit != null && e.Unit.Id == unitId)
                    .WhereIf(specId != null && specId != Guid.Empty, e => e.Spec != null && e.Spec.Id == specId);
        }

        public async Task<List<Clinic>> GetListAsync(
            string filterText = null,
            string clinicName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, clinicName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ClinicConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string clinicName = null,
            Guid? unitId = null,
            Guid? specId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, clinicName, unitId, specId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Clinic> ApplyFilter(
            IQueryable<Clinic> query,
            string filterText,
            string clinicName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ClinicName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(clinicName), e => e.ClinicName.Contains(clinicName));
        }
    }
}