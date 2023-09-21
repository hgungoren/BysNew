using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Budgets
{
    public class BudgetUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(BudgetConsts.BudgetNameMaxLength)]
        public string BudgetName { get; set; }
        public int Year { get; set; }
        [StringLength(BudgetConsts.CommentMaxLength)]
        public string Comment { get; set; }
        public bool IsActive { get; set; }
        public DateTime? OpenUntil { get; set; }
        public Guid? CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}