namespace ToksozBysNew.Masters
{
    public static class MasterConsts
    {
        private const string DefaultSorting = "{0}InvoiceSerialNo asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Master." : string.Empty);
        }

    }
}