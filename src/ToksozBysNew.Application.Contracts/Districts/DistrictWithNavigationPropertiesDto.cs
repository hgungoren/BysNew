using ToksozBysNew.Countries;
using ToksozBysNew.Provinces;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.Districts
{
    public class DistrictWithNavigationPropertiesDto
    {
        public DistrictDto District { get; set; }

        public CountryDto Country { get; set; }
        public ProvinceDto Province { get; set; }

    }
}