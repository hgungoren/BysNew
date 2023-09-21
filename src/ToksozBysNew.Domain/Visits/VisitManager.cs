using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Visits
{
    public class VisitManager : DomainService
    {
        private readonly IVisitRepository _visitRepository;

        public VisitManager(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public async Task<Visit> CreateAsync(
        Guid? doctorId, Guid? unitId, Guid? clinicId, Guid? brickId, Guid? identityUserId, Guid? specId, DateTime visitDate, string visitNotes)
        {
            Check.NotNull(visitDate, nameof(visitDate));

            var visit = new Visit(
             GuidGenerator.Create(),
             doctorId, unitId, clinicId, brickId, identityUserId, specId, visitDate, visitNotes
             );

            return await _visitRepository.InsertAsync(visit);
        }

        public async Task<Visit> UpdateAsync(
            Guid id,
            Guid? doctorId, Guid? unitId, Guid? clinicId, Guid? brickId, Guid? identityUserId, Guid? specId, DateTime visitDate, string visitNotes, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(visitDate, nameof(visitDate));

            var visit = await _visitRepository.GetAsync(id);

            visit.DoctorId = doctorId;
            visit.UnitId = unitId;
            visit.ClinicId = clinicId;
            visit.BrickId = brickId;
            visit.IdentityUserId = identityUserId;
            visit.SpecId = specId;
            visit.VisitDate = visitDate;
            visit.VisitNotes = visitNotes;

            visit.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _visitRepository.UpdateAsync(visit);
        }

    }
}