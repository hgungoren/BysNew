using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.CustomerTitles
{
    public class CustomerTitle : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string TitleName { get; set; }

        public CustomerTitle()
        {

        }

        public CustomerTitle(Guid id, string titleName)
        {

            Id = id;
            TitleName = titleName;
        }

    }
}