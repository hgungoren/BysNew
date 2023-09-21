using ToksozBysNew.Units;
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

namespace ToksozBysNew.Clinics
{
    public class Clinic : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string ClinicName { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? SpecId { get; set; }

        public Clinic()
        {

        }

        public Clinic(Guid id, Guid? unitId, Guid? specId, string clinicName)
        {

            Id = id;
            ClinicName = clinicName;
            UnitId = unitId;
            SpecId = specId;
        }

    }
}