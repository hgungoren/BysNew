namespace ToksozBysNew.Units
{
    public static class UnitConsts
    {
        private const string DefaultSorting = "{0}UnitName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Unit." : string.Empty);
        }

    }
}