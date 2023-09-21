using ToksozBysNew.Countries;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Provinces
{
    public class Province : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string ProvinceName { get; set; }
        public Guid? CountryId { get; set; }

        public Province()
        {

        }

        public Province(Guid id, Guid? countryId, string provinceName)
        {

            Id = id;
            ProvinceName = provinceName;
            CountryId = countryId;
        }

    }
}