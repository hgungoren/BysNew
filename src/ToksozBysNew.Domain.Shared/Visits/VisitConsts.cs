namespace ToksozBysNew.Visits
{
    public static class VisitConsts
    {
        private const string DefaultSorting = "{0}VisitDate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Visit." : string.Empty);
        }

    }
}