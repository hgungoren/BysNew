namespace ToksozBysNew.CustomerAddresses
{
    public static class CustomerAddressConsts
    {
        private const string DefaultSorting = "{0}Address asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerAddress." : string.Empty);
        }

    }
}