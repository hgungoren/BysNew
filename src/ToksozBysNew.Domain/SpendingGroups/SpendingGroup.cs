using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.SpendingGroups
{
    public class SpendingGroup : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string Name { get; set; }

        public SpendingGroup()
        {

        }

        public SpendingGroup(Guid id, string name)
        {

            Id = id;
            Name = name;
        }

    }
}