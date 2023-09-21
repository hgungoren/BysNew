using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Provinces
{
    public class ProvinceUpdateDto : IHasConcurrencyStamp
    {
        public string ProvinceName { get; set; }
        public Guid? CountryId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}