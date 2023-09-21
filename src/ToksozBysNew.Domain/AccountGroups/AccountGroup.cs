using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.AccountGroups
{
    public class AccountGroup : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string AccountGroupName { get; set; }

        public virtual bool IsUnitEnterable { get; set; }

        public AccountGroup()
        {

        }

        public AccountGroup(Guid id, string accountGroupName, bool isUnitEnterable)
        {

            Id = id;
            Check.NotNull(accountGroupName, nameof(accountGroupName));
            Check.Length(accountGroupName, nameof(accountGroupName), AccountGroupConsts.AccountGroupNameMaxLength, 0);
            AccountGroupName = accountGroupName;
            IsUnitEnterable = isUnitEnterable;
        }

    }
}