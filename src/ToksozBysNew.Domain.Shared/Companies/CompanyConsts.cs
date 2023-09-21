namespace ToksozBysNew.Companies
{
    public static class CompanyConsts
    {
        private const string DefaultSorting = "{0}CompanyName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Company." : string.Empty);
        }

        public const int CompanyNameMaxLength = 50;
    }
}