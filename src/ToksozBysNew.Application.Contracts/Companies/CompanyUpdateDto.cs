using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Companies
{
    public class CompanyUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(CompanyConsts.CompanyNameMaxLength)]
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}