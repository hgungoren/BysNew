using ToksozBysNew.Doctors;
using ToksozBysNew.Bricks;
using ToksozBysNew.Districts;
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

namespace ToksozBysNew.CustomerAddresses
{
    public class CustomerAddress : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string Address { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? BrickId { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? ProvinceId { get; set; }

        public CustomerAddress()
        {

        }

        public CustomerAddress(Guid id, Guid? doctorId, Guid? brickId, Guid? districtId, Guid? countryId, Guid? provinceId, string address)
        {

            Id = id;
            Address = address;
            DoctorId = doctorId;
            BrickId = brickId;
            DistrictId = districtId;
            CountryId = countryId;
            ProvinceId = provinceId;
        }

    }
}