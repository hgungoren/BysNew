namespace ToksozBysNew.CustomerTitles
{
    public static class CustomerTitleConsts
    {
        private const string DefaultSorting = "{0}TitleName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerTitle." : string.Empty);
        }

    }
}