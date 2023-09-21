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

namespace ToksozBysNew.Doctors
{
    public class EfCoreDoctorRepository : EfCoreRepository<ToksozBysNewDbContext, Doctor, Guid>, IDoctorRepository
    {
        public EfCoreDoctorRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<DoctorWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(doctor => new DoctorWithNavigationProperties
                {
                    Doctor = doctor,
                    Position = dbContext.Positions.FirstOrDefault(c => c.Id == doctor.PositionId),
                    Spec = dbContext.Specs.FirstOrDefault(c => c.Id == doctor.SpecId),
                    CustomerTitle = dbContext.CustomerTitles.FirstOrDefault(c => c.Id == doctor.CustomerTitleId),
                    Unit = dbContext.Units.FirstOrDefault(c => c.Id == doctor.UnitId),
                    CustomerType = dbContext.CustomerTypes.FirstOrDefault(c => c.Id == doctor.CustomerTypeId)
                }).FirstOrDefault();
        }

        public async Task<List<DoctorWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            bool? isActive = null,
            string nameSurname = null,
            string pharmacyName = null,
            Guid? positionId = null,
            Guid? specId = null,
            Guid? customerTitleId = null,
            Guid? unitId = null,
            Guid? customerTypeId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, isActive, nameSurname, pharmacyName, positionId, specId, customerTitleId, unitId, customerTypeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DoctorConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<DoctorWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from doctor in (await GetDbSetAsync())
                   join position in (await GetDbContextAsync()).Positions on doctor.PositionId equals position.Id into positions
                   from position in positions.DefaultIfEmpty()
                   join spec in (await GetDbContextAsync()).Specs on doctor.SpecId equals spec.Id into specs
                   from spec in specs.DefaultIfEmpty()
                   join customerTitle in (await GetDbContextAsync()).CustomerTitles on doctor.CustomerTitleId equals customerTitle.Id into customerTitles
                   from customerTitle in customerTitles.DefaultIfEmpty()
                   join unit in (await GetDbContextAsync()).Units on doctor.UnitId equals unit.Id into units
                   from unit in units.DefaultIfEmpty()
                   join customerType in (await GetDbContextAsync()).CustomerTypes on doctor.CustomerTypeId equals customerType.Id into customerTypes
                   from customerType in customerTypes.DefaultIfEmpty()

                   select new DoctorWithNavigationProperties
                   {
                       Doctor = doctor,
                       Position = position,
                       Spec = spec,
                       CustomerTitle = customerTitle,
                       Unit = unit,
                       CustomerType = customerType
                   };
        }

        protected virtual IQueryable<DoctorWithNavigationProperties> ApplyFilter(
            IQueryable<DoctorWithNavigationProperties> query,
            string filterText,
            bool? isActive = null,
            string nameSurname = null,
            string pharmacyName = null,
            Guid? positionId = null,
            Guid? specId = null,
            Guid? customerTitleId = null,
            Guid? unitId = null,
            Guid? customerTypeId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Doctor.NameSurname.Contains(filterText) || e.Doctor.PharmacyName.Contains(filterText))
                    .WhereIf(isActive.HasValue, e => e.Doctor.IsActive == isActive)
                    .WhereIf(!string.IsNullOrWhiteSpace(nameSurname), e => e.Doctor.NameSurname.Contains(nameSurname))
                    .WhereIf(!string.IsNullOrWhiteSpace(pharmacyName), e => e.Doctor.PharmacyName.Contains(pharmacyName))
                    .WhereIf(positionId != null && positionId != Guid.Empty, e => e.Position != null && e.Position.Id == positionId)
                    .WhereIf(specId != null && specId != Guid.Empty, e => e.Spec != null && e.Spec.Id == specId)
                    .WhereIf(customerTitleId != null && customerTitleId != Guid.Empty, e => e.CustomerTitle != null && e.CustomerTitle.Id == customerTitleId)
                    .WhereIf(unitId != null && unitId != Guid.Empty, e => e.Unit != null && e.Unit.Id == unitId)
                    .WhereIf(customerTypeId != null && customerTypeId != Guid.Empty, e => e.CustomerType != null && e.CustomerType.Id == customerTypeId);
        }

        public async Task<List<Doctor>> GetListAsync(
            string filterText = null,
            bool? isActive = null,
            string nameSurname = null,
            string pharmacyName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, isActive, nameSurname, pharmacyName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DoctorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            bool? isActive = null,
            string nameSurname = null,
            string pharmacyName = null,
            Guid? positionId = null,
            Guid? specId = null,
            Guid? customerTitleId = null,
            Guid? unitId = null,
            Guid? customerTypeId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, isActive, nameSurname, pharmacyName, positionId, specId, customerTitleId, unitId, customerTypeId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Doctor> ApplyFilter(
            IQueryable<Doctor> query,
            string filterText,
            bool? isActive = null,
            string nameSurname = null,
            string pharmacyName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NameSurname.Contains(filterText) || e.PharmacyName.Contains(filterText))
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive)
                    .WhereIf(!string.IsNullOrWhiteSpace(nameSurname), e => e.NameSurname.Contains(nameSurname))
                    .WhereIf(!string.IsNullOrWhiteSpace(pharmacyName), e => e.PharmacyName.Contains(pharmacyName));
        }
    }
}