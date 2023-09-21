namespace ToksozBysNew.Positions
{
    public static class PositionConsts
    {
        private const string DefaultSorting = "{0}PositionCode asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Position." : string.Empty);
        }

    }
}