using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Months
{
    public class Month : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string Name { get; set; }

        public Month()
        {

        }

        public Month(Guid id, string name)
        {

            Id = id;
            Name = name;
        }

    }
}