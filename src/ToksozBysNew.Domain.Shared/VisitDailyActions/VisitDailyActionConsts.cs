namespace ToksozBysNew.VisitDailyActions
{
    public static class VisitDailyActionConsts
    {
        private const string DefaultSorting = "{0}VisitDailyDate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VisitDailyAction." : string.Empty);
        }

    }
}