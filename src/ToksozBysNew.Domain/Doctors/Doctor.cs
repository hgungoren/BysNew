using ToksozBysNew.Positions;
using ToksozBysNew.Specs;
using ToksozBysNew.CustomerTitles;
using ToksozBysNew.Units;
using ToksozBysNew.CustomerTypes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Doctors
{
    public class Doctor : FullAuditedAggregateRoot<Guid>
    {
        public virtual bool IsActive { get; set; }

        [CanBeNull]
        public virtual string NameSurname { get; set; }

        [CanBeNull]
        public virtual string PharmacyName { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? SpecId { get; set; }
        public Guid? CustomerTitleId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? CustomerTypeId { get; set; }

        public Doctor()
        {

        }

        public Doctor(Guid id, Guid? positionId, Guid? specId, Guid? customerTitleId, Guid? unitId, Guid? customerTypeId, bool isActive, string nameSurname, string pharmacyName)
        {

            Id = id;
            IsActive = isActive;
            NameSurname = nameSurname;
            PharmacyName = pharmacyName;
            PositionId = positionId;
            SpecId = specId;
            CustomerTitleId = customerTitleId;
            UnitId = unitId;
            CustomerTypeId = customerTypeId;
        }

    }
}