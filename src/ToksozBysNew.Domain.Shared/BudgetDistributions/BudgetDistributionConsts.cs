namespace ToksozBysNew.BudgetDistributions
{
    public static class BudgetDistributionConsts
    {
        private const string DefaultSorting = "{0}CostCenter asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "BudgetDistribution." : string.Empty);
        }

        public const int CostCenterMaxLength = 50;
        public const int ExpenseTypeMaxLength = 600;
        public const int StatusMaxLength = 5;
    }
}