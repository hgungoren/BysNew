namespace ToksozBysNew.Provinces
{
    public static class ProvinceConsts
    {
        private const string DefaultSorting = "{0}ProvinceName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Province." : string.Empty);
        }

    }
}