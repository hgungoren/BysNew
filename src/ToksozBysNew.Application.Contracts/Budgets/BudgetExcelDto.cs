using System;

namespace ToksozBysNew.Budgets
{
    public class BudgetExcelDto
    {
        public string BudgetName { get; set; }
        public int Year { get; set; }
        public string Comment { get; set; }
        public bool IsActive { get; set; }
        public DateTime? OpenUntil { get; set; }
    }
}