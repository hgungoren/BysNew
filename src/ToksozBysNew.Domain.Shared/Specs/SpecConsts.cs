namespace ToksozBysNew.Specs
{
    public static class SpecConsts
    {
        private const string DefaultSorting = "{0}SpecCode asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Spec." : string.Empty);
        }

    }
}