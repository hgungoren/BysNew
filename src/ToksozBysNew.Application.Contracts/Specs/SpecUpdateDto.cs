using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Specs
{
    public class SpecUpdateDto : IHasConcurrencyStamp
    {
        public string SpecCode { get; set; }
        public string SpecName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}