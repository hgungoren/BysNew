namespace ToksozBysNew.ExpenseMonthlies
{
    public static class ExpenseMonthlyConsts
    {
        private const string DefaultSorting = "{0}AccountId asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ExpenseMonthly." : string.Empty);
        }

    }
}