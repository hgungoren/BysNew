using ToksozBysNew.Doctors;
using ToksozBysNew.Units;
using ToksozBysNew.Clinics;
using ToksozBysNew.Bricks;
using Volo.Abp.Identity;
using ToksozBysNew.Specs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Visits
{
    public class Visit : FullAuditedAggregateRoot<Guid>
    {
        public virtual DateTime VisitDate { get; set; }

        [CanBeNull]
        public virtual string VisitNotes { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? ClinicId { get; set; }
        public Guid? BrickId { get; set; }
        public Guid? IdentityUserId { get; set; }
        public Guid? SpecId { get; set; }

        public Visit()
        {

        }

        public Visit(Guid id, Guid? doctorId, Guid? unitId, Guid? clinicId, Guid? brickId, Guid? identityUserId, Guid? specId, DateTime visitDate, string visitNotes)
        {

            Id = id;
            VisitDate = visitDate;
            VisitNotes = visitNotes;
            DoctorId = doctorId;
            UnitId = unitId;
            ClinicId = clinicId;
            BrickId = brickId;
            IdentityUserId = identityUserId;
            SpecId = specId;
        }

    }
}