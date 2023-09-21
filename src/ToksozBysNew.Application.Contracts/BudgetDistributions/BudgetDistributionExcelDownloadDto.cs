using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.BudgetDistributions
{
    public class BudgetDistributionExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string CostCenter { get; set; }
        public string ExpenseType { get; set; }
        public int? ProjectItemMin { get; set; }
        public int? ProjectItemMax { get; set; }
        public int? TypeMin { get; set; }
        public int? TypeMax { get; set; }
        public int? UnitMin { get; set; }
        public int? UnitMax { get; set; }
        public float? UnitValueMin { get; set; }
        public float? UnitValueMax { get; set; }
        public int? MonthMin { get; set; }
        public int? MonthMax { get; set; }
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public float? RatioMin { get; set; }
        public float? RatioMax { get; set; }
        public float? AmountMin { get; set; }
        public float? AmountMax { get; set; }
        public float? MemoMin { get; set; }
        public float? MemoMax { get; set; }
        public float? InvoiceMin { get; set; }
        public float? InvoiceMax { get; set; }
        public int? CurrencyMin { get; set; }
        public int? CurrencyMax { get; set; }
        public float? CurrencyAmountMin { get; set; }
        public float? CurrencyAmountMax { get; set; }
        public int? ExpenseCategoryMin { get; set; }
        public int? ExpenseCategoryMax { get; set; }
        public int? ExpenseNecessityMin { get; set; }
        public int? ExpenseNecessityMax { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public int? ApprovalMin { get; set; }
        public int? ApprovalMax { get; set; }
        public bool? IsActive { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? BudgetId { get; set; }
        public Guid? AccountGroupId { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? IdentityUserId { get; set; }

        public BudgetDistributionExcelDownloadDto()
        {

        }
    }
}