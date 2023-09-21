using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.Accounts
{
    public class AccountCreateDto
    {
        [Required]
        [StringLength(AccountConsts.AccountCodeMaxLength)]
        public string AccountCode { get; set; }
        [Required]
        [StringLength(AccountConsts.AccountNameMaxLength)]
        public string AccountName { get; set; }
        [StringLength(AccountConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}