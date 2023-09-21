namespace ToksozBysNew.Bricks
{
    public static class BrickConsts
    {
        private const string DefaultSorting = "{0}BrickName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Brick." : string.Empty);
        }

    }
}