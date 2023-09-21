using ToksozBysNew.Countries;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.Provinces
{
    public class ProvinceWithNavigationPropertiesDto
    {
        public ProvinceDto Province { get; set; }

        public CountryDto Country { get; set; }

    }
}