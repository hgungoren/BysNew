using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Budgets
{
    public class BudgetDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string BudgetName { get; set; }
        public int Year { get; set; }
        public string Comment { get; set; }
        public bool IsActive { get; set; }
        public DateTime? OpenUntil { get; set; }
        public Guid? CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}