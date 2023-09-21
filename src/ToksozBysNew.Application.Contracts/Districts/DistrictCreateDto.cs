using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.Districts
{
    public class DistrictCreateDto
    {
        public string DistrictName { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? ProvinceId { get; set; }
    }
}