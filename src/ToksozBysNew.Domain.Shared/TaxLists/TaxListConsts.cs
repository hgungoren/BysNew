namespace ToksozBysNew.TaxLists
{
    public static class TaxListConsts
    {
        private const string DefaultSorting = "{0}TaxName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "TaxList." : string.Empty);
        }

        public const int TaxValueMinLength = 0;
        public const int TaxValueMaxLength = 18;
    }
}