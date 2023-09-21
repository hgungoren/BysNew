using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Countries
{
    public class Country : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string CountryName { get; set; }

        public Country()
        {

        }

        public Country(Guid id, string countryName)
        {

            Id = id;
            CountryName = countryName;
        }

    }
}