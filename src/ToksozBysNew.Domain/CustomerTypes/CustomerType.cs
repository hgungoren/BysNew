using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.CustomerTypes
{
    public class CustomerType : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string TypeName { get; set; }

        public CustomerType()
        {

        }

        public CustomerType(Guid id, string typeName)
        {

            Id = id;
            TypeName = typeName;
        }

    }
}