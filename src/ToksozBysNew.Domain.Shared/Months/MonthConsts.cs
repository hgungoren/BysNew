namespace ToksozBysNew.Months
{
    public static class MonthConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Month." : string.Empty);
        }

    }
}