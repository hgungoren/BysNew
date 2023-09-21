using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.TaxLists
{
    public class TaxListCreateDto
    {
        public string TaxName { get; set; }
        [Range(TaxListConsts.TaxValueMinLength, TaxListConsts.TaxValueMaxLength)]
        public int TaxValue { get; set; }
    }
}