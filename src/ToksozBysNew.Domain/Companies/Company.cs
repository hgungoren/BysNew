using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Companies
{
    public class Company : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string CompanyName { get; set; }

        public virtual bool IsActive { get; set; }

        public Company()
        {

        }

        public Company(Guid id, string companyName, bool isActive)
        {

            Id = id;
            Check.NotNull(companyName, nameof(companyName));
            Check.Length(companyName, nameof(companyName), CompanyConsts.CompanyNameMaxLength, 0);
            CompanyName = companyName;
            IsActive = isActive;
        }

    }
}