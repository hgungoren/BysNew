using ToksozBysNew.Countries;
using ToksozBysNew.Provinces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Districts
{
    public class District : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string DistrictName { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? ProvinceId { get; set; }

        public District()
        {

        }

        public District(Guid id, Guid? countryId, Guid? provinceId, string districtName)
        {

            Id = id;
            DistrictName = districtName;
            CountryId = countryId;
            ProvinceId = provinceId;
        }

    }
}