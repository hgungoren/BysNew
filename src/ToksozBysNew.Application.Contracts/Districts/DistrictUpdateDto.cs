using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Districts
{
    public class DistrictUpdateDto : IHasConcurrencyStamp
    {
        public string DistrictName { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? ProvinceId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}