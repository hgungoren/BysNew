using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.TaxLists
{
    public class TaxListUpdateDto : IHasConcurrencyStamp
    {
        public string TaxName { get; set; }
        [Range(TaxListConsts.TaxValueMinLength, TaxListConsts.TaxValueMaxLength)]
        public int TaxValue { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}