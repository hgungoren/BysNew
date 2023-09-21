using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.Companies
{
    public class CompanyCreateDto
    {
        [Required]
        [StringLength(CompanyConsts.CompanyNameMaxLength)]
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
    }
}