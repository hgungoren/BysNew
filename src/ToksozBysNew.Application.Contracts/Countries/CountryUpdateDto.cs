using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Countries
{
    public class CountryUpdateDto : IHasConcurrencyStamp
    {
        public string CountryName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}