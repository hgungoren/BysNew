using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Specs
{
    public class Spec : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string SpecCode { get; set; }

        [CanBeNull]
        public virtual string SpecName { get; set; }

        public Spec()
        {

        }

        public Spec(Guid id, string specCode, string specName)
        {

            Id = id;
            SpecCode = specCode;
            SpecName = specName;
        }

    }
}