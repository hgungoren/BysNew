namespace ToksozBysNew.SpendingGroups
{
    public static class SpendingGroupConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "SpendingGroup." : string.Empty);
        }

    }
}