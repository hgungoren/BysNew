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

namespace ToksozBysNew.Visits
{
    public class EfCoreVisitRepository : EfCoreRepository<ToksozBysNewDbContext, Visit, Guid>, IVisitRepository
    {
        public EfCoreVisitRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<VisitWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(visit => new VisitWithNavigationProperties
                {
                    Visit = visit,
                    Doctor = dbContext.Doctors.FirstOrDefault(c => c.Id == visit.DoctorId),
                    Unit = dbContext.Units.FirstOrDefault(c => c.Id == visit.UnitId),
                    Clinic = dbContext.Clinics.FirstOrDefault(c => c.Id == visit.ClinicId),
                    Brick = dbContext.Bricks.FirstOrDefault(c => c.Id == visit.BrickId),
                    IdentityUser = dbContext.Users.FirstOrDefault(c => c.Id == visit.IdentityUserId),
                    Spec = dbContext.Specs.FirstOrDefault(c => c.Id == visit.SpecId)
                }).FirstOrDefault();
        }

        public async Task<List<VisitWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? visitDateMin = null,
            DateTime? visitDateMax = null,
            string visitNotes = null,
            Guid? doctorId = null,
            Guid? unitId = null,
            Guid? clinicId = null,
            Guid? brickId = null,
            Guid? identityUserId = null,
            Guid? specId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, visitDateMin, visitDateMax, visitNotes, doctorId, unitId, clinicId, brickId, identityUserId, specId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VisitConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<VisitWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from visit in (await GetDbSetAsync())
                   join doctor in (await GetDbContextAsync()).Doctors on visit.DoctorId equals doctor.Id into doctors
                   from doctor in doctors.DefaultIfEmpty()
                   join unit in (await GetDbContextAsync()).Units on visit.UnitId equals unit.Id into units
                   from unit in units.DefaultIfEmpty()
                   join clinic in (await GetDbContextAsync()).Clinics on visit.ClinicId equals clinic.Id into clinics
                   from clinic in clinics.DefaultIfEmpty()
                   join brick in (await GetDbContextAsync()).Bricks on visit.BrickId equals brick.Id into bricks
                   from brick in bricks.DefaultIfEmpty()
                   join identityUser in (await GetDbContextAsync()).Users on visit.IdentityUserId equals identityUser.Id into users
                   from identityUser in users.DefaultIfEmpty()
                   join spec in (await GetDbContextAsync()).Specs on visit.SpecId equals spec.Id into specs
                   from spec in specs.DefaultIfEmpty()

                   select new VisitWithNavigationProperties
                   {
                       Visit = visit,
                       Doctor = doctor,
                       Unit = unit,
                       Clinic = clinic,
                       Brick = brick,
                       IdentityUser = identityUser,
                       Spec = spec
                   };
        }

        protected virtual IQueryable<VisitWithNavigationProperties> ApplyFilter(
            IQueryable<VisitWithNavigationProperties> query,
            string filterText,
            DateTime? visitDateMin = null,
            DateTime? visitDateMax = null,
            string visitNotes = null,
            Guid? doctorId = null,
            Guid? unitId = null,
            Guid? clinicId = null,
            Guid? brickId = null,
            Guid? identityUserId = null,
            Guid? specId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Visit.VisitNotes.Contains(filterText))
                    .WhereIf(visitDateMin.HasValue, e => e.Visit.VisitDate >= visitDateMin.Value)
                    .WhereIf(visitDateMax.HasValue, e => e.Visit.VisitDate <= visitDateMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(visitNotes), e => e.Visit.VisitNotes.Contains(visitNotes))
                    .WhereIf(doctorId != null && doctorId != Guid.Empty, e => e.Doctor != null && e.Doctor.Id == doctorId)
                    .WhereIf(unitId != null && unitId != Guid.Empty, e => e.Unit != null && e.Unit.Id == unitId)
                    .WhereIf(clinicId != null && clinicId != Guid.Empty, e => e.Clinic != null && e.Clinic.Id == clinicId)
                    .WhereIf(brickId != null && brickId != Guid.Empty, e => e.Brick != null && e.Brick.Id == brickId)
                    .WhereIf(identityUserId != null && identityUserId != Guid.Empty, e => e.IdentityUser != null && e.IdentityUser.Id == identityUserId)
                    .WhereIf(specId != null && specId != Guid.Empty, e => e.Spec != null && e.Spec.Id == specId);
        }

        public async Task<List<Visit>> GetListAsync(
            string filterText = null,
            DateTime? visitDateMin = null,
            DateTime? visitDateMax = null,
            string visitNotes = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, visitDateMin, visitDateMax, visitNotes);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VisitConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            DateTime? visitDateMin = null,
            DateTime? visitDateMax = null,
            string visitNotes = null,
            Guid? doctorId = null,
            Guid? unitId = null,
            Guid? clinicId = null,
            Guid? brickId = null,
            Guid? identityUserId = null,
            Guid? specId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, visitDateMin, visitDateMax, visitNotes, doctorId, unitId, clinicId, brickId, identityUserId, specId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Visit> ApplyFilter(
            IQueryable<Visit> query,
            string filterText,
            DateTime? visitDateMin = null,
            DateTime? visitDateMax = null,
            string visitNotes = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.VisitNotes.Contains(filterText))
                    .WhereIf(visitDateMin.HasValue, e => e.VisitDate >= visitDateMin.Value)
                    .WhereIf(visitDateMax.HasValue, e => e.VisitDate <= visitDateMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(visitNotes), e => e.VisitNotes.Contains(visitNotes));
        }
    }
}