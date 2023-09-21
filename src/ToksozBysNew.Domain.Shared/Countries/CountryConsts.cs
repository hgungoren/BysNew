namespace ToksozBysNew.Countries
{
    public static class CountryConsts
    {
        private const string DefaultSorting = "{0}CountryName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Country." : string.Empty);
        }

    }
}