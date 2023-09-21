using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.AccountGroups
{
    public class AccountGroupUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(AccountGroupConsts.AccountGroupNameMaxLength)]
        public string AccountGroupName { get; set; }
        public bool IsUnitEnterable { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}