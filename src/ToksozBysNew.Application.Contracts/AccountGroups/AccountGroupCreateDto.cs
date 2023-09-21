using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.AccountGroups
{
    public class AccountGroupCreateDto
    {
        [Required]
        [StringLength(AccountGroupConsts.AccountGroupNameMaxLength)]
        public string AccountGroupName { get; set; }
        public bool IsUnitEnterable { get; set; }
    }
}