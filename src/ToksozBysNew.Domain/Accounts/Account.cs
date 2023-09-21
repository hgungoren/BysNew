using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Accounts
{
    public class Account : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string AccountCode { get; set; }

        [NotNull]
        public virtual string AccountName { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual bool IsActive { get; set; }

        public Account()
        {

        }

        public Account(Guid id, string accountCode, string accountName, string description, bool isActive)
        {

            Id = id;
            Check.NotNull(accountCode, nameof(accountCode));
            Check.Length(accountCode, nameof(accountCode), AccountConsts.AccountCodeMaxLength, 0);
            Check.NotNull(accountName, nameof(accountName));
            Check.Length(accountName, nameof(accountName), AccountConsts.AccountNameMaxLength, 0);
            Check.Length(description, nameof(description), AccountConsts.DescriptionMaxLength, 0);
            AccountCode = accountCode;
            AccountName = accountName;
            Description = description;
            IsActive = isActive;
        }

    }
}