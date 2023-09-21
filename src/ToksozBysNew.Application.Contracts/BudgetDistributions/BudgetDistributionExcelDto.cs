using System;

namespace ToksozBysNew.BudgetDistributions
{
    public class BudgetDistributionExcelDto
    {
        public string CostCenter { get; set; }
        public string ExpenseType { get; set; }
        public int? ProjectItem { get; set; }
        public int? Type { get; set; }
        public int? Unit { get; set; }
        public float? UnitValue { get; set; }
        public int Month { get; set; }
        public int? Year { get; set; }
        public float? Ratio { get; set; }
        public float Amount { get; set; }
        public float? Memo { get; set; }
        public float? Invoice { get; set; }
        public int? Currency { get; set; }
        public float? CurrencyAmount { get; set; }
        public int? ExpenseCategory { get; set; }
        public int? ExpenseNecessity { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public int? Approval { get; set; }
        public bool IsActive { get; set; }
    }
}