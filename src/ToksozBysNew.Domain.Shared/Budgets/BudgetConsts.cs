namespace ToksozBysNew.Budgets
{
    public static class BudgetConsts
    {
        private const string DefaultSorting = "{0}BudgetName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Budget." : string.Empty);
        }

        public const int BudgetNameMaxLength = 50;
        public const int CommentMaxLength = 255;
    }
}