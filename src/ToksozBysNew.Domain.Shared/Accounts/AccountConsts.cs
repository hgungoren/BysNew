namespace ToksozBysNew.Accounts
{
    public static class AccountConsts
    {
        private const string DefaultSorting = "{0}AccountCode asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Account." : string.Empty);
        }

        public const int AccountCodeMaxLength = 50;
        public const int AccountNameMaxLength = 255;
        public const int DescriptionMaxLength = 2000;
    }
}