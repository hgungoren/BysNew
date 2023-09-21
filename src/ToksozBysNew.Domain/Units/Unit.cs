using ToksozBysNew.Bricks;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Units
{
    public class Unit : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string UnitName { get; set; }
        public Guid? BrickId { get; set; }

        public Unit()
        {

        }

        public Unit(Guid id, Guid? brickId, string unitName)
        {

            Id = id;
            UnitName = unitName;
            BrickId = brickId;
        }

    }
}