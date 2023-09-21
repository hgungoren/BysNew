using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Accounts
{
    public class AccountUpdateDto : IHasConcurrencyStamp
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

        public string ConcurrencyStamp { get; set; }
    }
}