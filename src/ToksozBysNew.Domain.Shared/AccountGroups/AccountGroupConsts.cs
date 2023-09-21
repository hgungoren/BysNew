namespace ToksozBysNew.AccountGroups
{
    public static class AccountGroupConsts
    {
        private const string DefaultSorting = "{0}AccountGroupName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "AccountGroup." : string.Empty);
        }

        public const int AccountGroupNameMaxLength = 255;
    }
}