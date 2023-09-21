using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Bricks
{
    public class Brick : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string BrickName { get; set; }

        public Brick()
        {

        }

        public Brick(Guid id, string brickName)
        {

            Id = id;
            BrickName = brickName;
        }

    }
}