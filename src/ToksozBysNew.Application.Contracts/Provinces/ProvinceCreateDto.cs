using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.Provinces
{
    public class ProvinceCreateDto
    {
        public string ProvinceName { get; set; }
        public Guid? CountryId { get; set; }
    }
}