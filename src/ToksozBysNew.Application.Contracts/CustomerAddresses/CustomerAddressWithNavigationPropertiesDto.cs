using ToksozBysNew.Doctors;
using ToksozBysNew.Bricks;
using ToksozBysNew.Districts;
using ToksozBysNew.Countries;
using ToksozBysNew.Provinces;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.CustomerAddresses
{
    public class CustomerAddressWithNavigationPropertiesDto
    {
        public CustomerAddressDto CustomerAddress { get; set; }

        public DoctorDto Doctor { get; set; }
        public BrickDto Brick { get; set; }
        public DistrictDto District { get; set; }
        public CountryDto Country { get; set; }
        public ProvinceDto Province { get; set; }

    }
}